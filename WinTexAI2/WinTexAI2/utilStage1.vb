Option Explicit On

Imports Microsoft
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Outlook

Imports System
Imports System.IO
Imports System.Net
Imports System.Linq
Imports System.Text
Imports System.Collections.Generic

Imports Google.Cloud.DocumentAI.V1

Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module utilStage1

    Public Function GoogleMagic(filePath As String) As String
        Dim projectId As String = "my-project-1572287285896" ' Replace with your Google Cloud project ID
        Dim locationId As String = "us" ' or "eu"
        'Dim processorId As String = "6025f7c34e8d173f" ' form
        Dim processorId As String = "c3824be2330b7556" ' ocr
        'Dim processorId As String = "5e9bcb430ab0bf17" ' parser

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\WinTexDLL\WinTexAI2\my-project-1572287285896-192b97d3a513.json")

        GoogleMagic = ProcessDocument(projectId, locationId, processorId, filePath)
    End Function

    ' Helper function to extract text from a TextAnchor
    Private Function GetTextAnchorText(anchor As Google.Cloud.DocumentAI.V1.Document.Types.TextAnchor, doc As Google.Cloud.DocumentAI.V1.Document) As String
        If anchor Is Nothing OrElse anchor.TextSegments.Count = 0 Then Return ""
        Dim sb As New StringBuilder()
        For Each segment In anchor.TextSegments
            Dim startIdx = CInt(segment.StartIndex)
            Dim endIdx = CInt(segment.EndIndex)
            sb.Append(doc.Text.Substring(startIdx, endIdx - startIdx))
        Next
        Return sb.ToString()
    End Function

    Private Function ProcessDocument(projectId As String, locationId As String, processorId As String, filePath As String) As String

        ProcessDocument = ""

        ' Initialize Document AI client
        Dim clientBuilder As New DocumentProcessorServiceClientBuilder With {
            .Endpoint = $"{locationId}-documentai.googleapis.com"
        }
        Dim client = clientBuilder.Build()

        ' Read the PDF file
        Dim fileContent As Byte() = File.ReadAllBytes(filePath)

        ' Create the process request
        Dim request As New ProcessRequest With {
            .Name = $"projects/{projectId}/locations/{locationId}/processors/{processorId}",
            .RawDocument = New RawDocument With {
                .Content = Google.Protobuf.ByteString.CopyFrom(fileContent),
                .MimeType = "application/pdf"
            }
        }

        ' Process the document
        Dim response = client.ProcessDocument(request)
        Dim document = response.Document

        ' Build JSON output
        Dim jsonOutput = New With {
            .document = New With {
                .filename = Path.GetFileName(filePath),
                .total_pages = document.Pages.Count,
                .extraction_date = DateTime.UtcNow.ToString("yyyy-MM-dd")
            },
            .pages = document.Pages.Select(Function(page, index)
                                               Dim content As New StringBuilder()
                                               ' Extract page text
                                               content.AppendLine(GetTextAnchorText(page.Layout.TextAnchor, document))
                                               ' Handle tables
                                               If page.Tables.Any() Then
                                                   content.AppendLine(vbLf & "### Tables:")
                                                   For Each table In page.Tables
                                                       ' Header row
                                                       Dim headers = If(table.HeaderRows.FirstOrDefault() IsNot Nothing,
                                                           table.HeaderRows(0).Cells.Select(Function(c) GetTextAnchorText(c.Layout.TextAnchor, document).Trim()).ToArray(),
                                                           Array.Empty(Of String)())
                                                       content.AppendLine(vbLf & "| " & String.Join(" | ", headers) & " |")
                                                       content.AppendLine("| " & String.Join(" | ", Enumerable.Repeat("---", headers.Length)) & " |")
                                                       ' Body rows
                                                       For Each row In table.BodyRows
                                                           content.AppendLine("| " & String.Join(" | ", row.Cells.Select(Function(c) GetTextAnchorText(c.Layout.TextAnchor, document).Trim())) & " |")
                                                       Next
                                                   Next
                                               End If
                                               ' Handle images
                                               If page.Image IsNot Nothing Then
                                                   content.AppendLine(vbLf & "[Image: Detected image on page]")
                                               End If
                                               Return New With {
                                                   .page_number = index + 1,
                                                   .content = content.ToString()
                                               }
                                           End Function).ToList()
        }

        ' Serialize to JSON using Newtonsoft.Json
        Dim jsonString = JsonConvert.SerializeObject(jsonOutput, Formatting.Indented)
        ProcessDocument = jsonString.Trim

        'Console.WriteLine(jsonString)
        ' Save to file
        'File.WriteAllText("output.json", jsonString)
    End Function

    Private Function ProcessDocument2(projectId As String, locationId As String, processorId As String, filePath As String) As String

        ProcessDocument2 = ""

        ' Initialize Document AI client
        Dim client = DocumentProcessorServiceClient.Create()

        ' Build the processor name
        Dim name = $"projects/{projectId}/locations/{locationId}/processors/{processorId}"

        ' Read the PDF file
        Dim fileContent As Byte() = File.ReadAllBytes(filePath)

        ' Create the raw document
        Dim rawDocument As New RawDocument With {
            .Content = Google.Protobuf.ByteString.CopyFrom(fileContent),
            .MimeType = "application/pdf"
        }

        ' Create the process request
        Dim request As New ProcessRequest With {
            .Name = name,
            .RawDocument = rawDocument
        }

        ' Process the document
        Dim result = client.ProcessDocument(request)
        Dim document = result.Document

        ' Build JSON output
        Dim output = New With {
            .document = New With {
                .filename = Path.GetFileName(filePath),
                .total_pages = document.Pages.Count,
                .extraction_date = "2025-08-02"
            },
            .pages = New List(Of Object)()
        }

        For i = 0 To document.Pages.Count - 1
            Dim page = document.Pages(i)
            Dim content As New StringBuilder()
            content.AppendLine(GetTextAnchorText(page.Layout.TextAnchor, document))
            ' Handle tables
            If page.Tables.Any() Then
                content.AppendLine(vbLf & "### Tables:")
                For Each table In page.Tables
                    Dim headers As String() = If(table.HeaderRows.Any(),
                        table.HeaderRows(0).Cells.Select(Function(cell) GetTextAnchorText(cell.Layout.TextAnchor, document).Trim()).ToArray(),
                        {})
                    content.AppendLine(vbLf & "| " & String.Join(" | ", headers) & " |")
                    content.AppendLine("| " & String.Join(" | ", Enumerable.Repeat("---", headers.Length)) & " |")
                    For Each row In table.BodyRows
                        content.AppendLine("| " & String.Join(" | ", row.Cells.Select(Function(cell) GetTextAnchorText(cell.Layout.TextAnchor, document).Trim())) & " |")
                    Next
                Next
            End If
            ' Handle images
            If page.Image IsNot Nothing Then
                content.AppendLine(vbLf & "[Image: Detected image on page]")
            End If
            output.pages.Add(New With {
                .page_number = i + 1,
                .content = content.ToString()
            })
        Next

        ' Serialize to JSON using Newtonsoft.Json
        Dim jsonString = JsonConvert.SerializeObject(output, Formatting.Indented)
        ProcessDocument2 = jsonString.Trim

        'Console.WriteLine(jsonString)
        ' Save to file
        'File.WriteAllText("output.json", jsonString)
    End Function

End Module
