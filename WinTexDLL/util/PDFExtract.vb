Imports System
Imports System.Drawing
Imports Bytescout.PDFExtractor
Imports Newtonsoft.Json

Module PDFExtract

    Private Structure oDocument
        Dim pageCount As String
        Dim pageCountWithOCRPerformed As String
        Dim page As Object
    End Structure

    Public Sub ZaraPDFtoSiparis1(Optional cPDF As String = "")

        Dim cOutputFile As String = "c:\docs\result.json"
        Dim cSiparisJSON As String = ""
        Dim oDoc As New oDocument

        Try
            cPDF = "c:\pedido_42706.pdf"
            If cPDF.Trim = "" Then Exit Sub

            Dim oExtractor As New Bytescout.PDFExtractor.JSONExtractor

            oExtractor.RegistrationName = "DownloadDevTools.com"
            oExtractor.RegistrationKey = "DownloadDevTools.com"

            oExtractor.LoadDocumentFromFile(cPDF)

            oExtractor.PreserveFormattingOnTextExtraction = False
            oExtractor.ImageFormat = OutputImageFormat.JPEG
            oExtractor.SaveImages = ImageHandling.Embed

            cSiparisJSON = oExtractor.GetJSONData
            'cSiparisJSON = cSiparisJSON.Replace(vbCrLf, "")
            'cSiparisJSON = cSiparisJSON.Replace(Chr(34), "'")

            oDoc = JsonConvert.DeserializeObject(Of oDocument)(cSiparisJSON)
            MsgBox("pageCount " + oDoc.pageCount + vbCrLf +
                    "pageCountWithOCRPerformed " + oDoc.pageCountWithOCRPerformed)

            'oExtractor.SaveJSONToFile(cOutputFile)

            ' Open result file in default associated application (for demo purposes)
            'System.Diagnostics.Process.Start(cOutputFile)

        Catch ex As Exception
            ErrDisp("ZaraPDFtoSiparis1 : " + ex.Message, "PDFExtract",,, ex)
        End Try
    End Sub

End Module
