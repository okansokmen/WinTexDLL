Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Mscc.GenerativeAI
Imports SkiaSharp

Module util1
    Private ReadOnly Property ApiKey As String
        Get
            ' IMPORTANT: Replace with your actual Google Gemini API key
            Return "AIzaSyArioNIp3nir2tBQVXxUg_hmrUJaKbeDUA"
        End Get
    End Property

    <STAThread>
    Sub Main(args As String())
        Dim pdfPath As String = SelectPdfFile()
        If String.IsNullOrEmpty(pdfPath) Then
            Console.WriteLine("No file selected.")
            Return
        End If

        Dim jsonOutput As String = ConvertPdfToJsonAsync(pdfPath).GetAwaiter().GetResult()

        If Not String.IsNullOrEmpty(jsonOutput) Then
            Dim outputFilename As String = Path.ChangeExtension(pdfPath, ".txt")
            File.WriteAllText(outputFilename, jsonOutput)
            Console.WriteLine($"Successfully created {outputFilename}")
        Else
            Console.WriteLine("Could not extract JSON from the document.")
        End If
    End Sub

    Function SelectPdfFile() As String
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.InitialDirectory = "c:\"
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
            openFileDialog.FilterIndex = 1
            openFileDialog.RestoreDirectory = True

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Return openFileDialog.FileName
            End If
        End Using
        Return Nothing
    End Function

    Async Function ConvertPdfToJsonAsync(pdfPath As String) As Task(Of String)
        Console.WriteLine("Converting PDF to images...")
        Dim images As IEnumerable(Of SKBitmap) = ConvertPdfToImages(pdfPath)

        If images IsNot Nothing AndAlso images.Any() Then
            Console.WriteLine("Extracting text and converting to JSON...")
            Return Await GetJsonResponseFromImagesAsync(images)
        Else
            Console.WriteLine("Could not convert PDF to images.")
            Return Nothing
        End If
    End Function

    Function ConvertPdfToImages(pdfPath As String) As IEnumerable(Of SKBitmap)
        Try
            Return PDFtoImage.Conversion.ToImages(pdfPath)
        Catch ex As Exception
            Console.WriteLine($"Error converting PDF to images: {ex.Message}")
            Return Nothing
        End Try
    End Function

    Async Function GetJsonResponseFromImagesAsync(images As IEnumerable(Of SKBitmap)) As Task(Of String)
        Dim prompt = "Please analyze the following document images and extract the information into a structured JSON format. I need you to identify all the key-value pairs in the document. The output should be a single JSON object."
        Dim googleAI = New GoogleAI(apiKey:=ApiKey)
        Dim model = googleAI.GenerativeModel("gemini-pro-vision")
        Dim request = New GenerateContentRequest()

        request.Contents.Add(New Content("") With {
            .Parts = New List(Of IPart) From {
                New Part With {.Text = prompt}
            }
        })

        For Each img In images
            Using ms = New MemoryStream()
                img.Encode(ms, SKEncodedImageFormat.Jpeg, 100)
                Dim imageBytes = ms.ToArray()
                request.Contents.Add(New Content("") With {
                    .Parts = New List(Of IPart) From {
                        New Part With {
                            .InlineData = New InlineData With {
                                .MimeType = "image/jpeg",
                                .Data = Convert.ToBase64String(imageBytes)
                            }
                        }
                    }
                })
            End Using
        Next

        Try
            Dim response = Await model.GenerateContent(request)
            Dim jsonString = response.Text.Trim()

            If jsonString.StartsWith("```json") Then
                jsonString = jsonString.Substring(7)
            End If
            If jsonString.EndsWith("```") Then
                jsonString = jsonString.Substring(0, jsonString.Length - 3)
            End If

            Return jsonString
        Catch ex As Exception
            Console.WriteLine($"Error getting JSON from Gemini: {ex.Message}")
            Return Nothing
        End Try
    End Function

End Module
