Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports Microsoft.SqlServer.Server
Imports Stimulsoft.Base
Imports Stimulsoft.Report
Imports Stimulsoft.Report.Dictionary
Imports Stimulsoft.Report.Export
Imports Stimulsoft.Report.Print

Module UtilStimulus

    Public Structure oStimulusReport
        Dim cReportID As String
        Dim cReportName As String
        Dim cReportClass As String
        Dim cReportSQL As String
        Dim cReportVariable1 As String
        Dim cReportVariable2 As String
        Dim cReportVariable3 As String
        Dim cReportVariable4 As String
        Dim cReportVariable5 As String
        Dim cReportVariable6 As String
        Dim cReportVariable7 As String
        Dim cReportVariable8 As String
        Dim cReportVariable9 As String
        Dim cReportVariable10 As String
        Dim cReport As String
        Dim cPrinterName As String
        Dim nCopies As Double
        Dim oReportStimulus As Stimulsoft.Report.StiReport
    End Structure

    Public oReport As oStimulusReport

    Public Function StiReportToPDF(cReportID As String, cFileName As String, cReportVariable1 As String, Optional cReportVariable2 As String = "",
                                   Optional cReportVariable3 As String = "", Optional cReportVariable4 As String = "", Optional cReportVariable5 As String = "",
                                   Optional cReportVariable6 As String = "", Optional cReportVariable7 As String = "", Optional cReportVariable8 As String = "",
                                   Optional cReportVariable9 As String = "", Optional cReportVariable10 As String = "", Optional cDatabase As String = "") As Boolean
        StiReportToPDF = False

        Try
            InitStimulus()

            oReport.cReportID = cReportID.Trim
            oReport.cReportVariable1 = cReportVariable1.Trim
            oReport.cReportVariable2 = cReportVariable2.Trim
            oReport.cReportVariable3 = cReportVariable3.Trim
            oReport.cReportVariable4 = cReportVariable4.Trim
            oReport.cReportVariable5 = cReportVariable5.Trim
            oReport.cReportVariable6 = cReportVariable6.Trim
            oReport.cReportVariable7 = cReportVariable7.Trim
            oReport.cReportVariable8 = cReportVariable8.Trim
            oReport.cReportVariable9 = cReportVariable9.Trim
            oReport.cReportVariable10 = cReportVariable10.Trim

            If Not STSaveToPDF(cFileName, , cDatabase) Then Exit Function

            StiReportToPDF = True

        Catch ex As Exception
            ErrDisp(ex, "StiReportToPDF")
        End Try
    End Function

    Public Function StiReportToExcel(cReportID As String, cFileName As String, cReportVariable1 As String, Optional cReportVariable2 As String = "",
                                     Optional cReportVariable3 As String = "", Optional cReportVariable4 As String = "", Optional cReportVariable5 As String = "",
                                     Optional cReportVariable6 As String = "", Optional cReportVariable7 As String = "", Optional cReportVariable8 As String = "",
                                     Optional cReportVariable9 As String = "", Optional cReportVariable10 As String = "", Optional cDatabase As String = "") As Boolean
        StiReportToExcel = False

        Try
            InitStimulus()

            oReport.cReportID = cReportID.Trim
            oReport.cReportVariable1 = cReportVariable1.Trim
            oReport.cReportVariable2 = cReportVariable2.Trim
            oReport.cReportVariable3 = cReportVariable3.Trim
            oReport.cReportVariable4 = cReportVariable4.Trim
            oReport.cReportVariable5 = cReportVariable5.Trim
            oReport.cReportVariable6 = cReportVariable6.Trim
            oReport.cReportVariable7 = cReportVariable7.Trim
            oReport.cReportVariable8 = cReportVariable8.Trim
            oReport.cReportVariable9 = cReportVariable9.Trim
            oReport.cReportVariable10 = cReportVariable10.Trim

            If Not STSaveToFile(cFileName, "Excel2007", cDatabase) Then Exit Function

            StiReportToExcel = True

        Catch ex As Exception
            ErrDisp(ex, "StiReportToExcel")
        End Try
    End Function

    Public Function STSaveToFile(Optional cFileName As String = "c:\StimulusReport.PDF", Optional cFileType As String = "PDF", Optional cDatabase As String = "") As Boolean

        STSaveToFile = False

        Try
            Dim oFileStream As New IO.FileStream(cFileName, FileMode.Create, FileAccess.ReadWrite)

            If Not StiLoadReport(cDatabase) Then Exit Function

            oReport.oReportStimulus.Compile()
            AssingReportVariables()
            oReport.oReportStimulus.Render()

            Select Case cFileType
                Case "PDF"
                    Dim oService As New StiPdfExportService
                    Dim oSettings As New StiPdfExportSettings

                    oSettings.Compressed = True
                    oSettings.EmbeddedFonts = True
                    oSettings.ImageCompressionMethod = StiPdfImageCompressionMethod.Jpeg
                    oSettings.ImageQuality = 1
                    oSettings.ImageResolution = 100
                    oSettings.CreatorString = "WinTex Programi"
                    oSettings.KeywordsString = "WinTex Raporu"

                    oService.ExportPdf(oReport.oReportStimulus, oFileStream, oSettings)

                Case "XPS"
                    Dim oService As New StiXpsExportService
                    Dim oSettings As New StiXpsExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "Ppt2007"
                    Dim oService As New StiPpt2007ExportService
                    Dim oSettings As New StiPpt2007ExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "HtmlDiv"
                    Dim oService As New StiHtmlExportService
                    Dim oSettings As New StiHtmlExportSettings

                    oSettings.ExportMode = StiHtmlExportMode.Div
                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "HtmlSpan"
                    Dim oService As New StiHtmlExportService
                    Dim oSettings As New StiHtmlExportSettings

                    oSettings.ExportMode = StiHtmlExportMode.Span
                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "HtmlTable"
                    Dim oService As New StiHtmlExportService
                    Dim oSettings As New StiHtmlExportSettings

                    oSettings.ExportMode = StiHtmlExportMode.Table
                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "Mht"
                    Dim oService As New StiMhtExportService
                    Dim oSettings As New StiMhtExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "Text"
                    Dim oService As New StiTxtExportService
                    Dim oSettings As New StiTxtExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "RtfFrame"
                    Dim oService As New StiRtfExportService
                    Dim oSettings As New StiRtfExportSettings

                    oSettings.ExportMode = StiRtfExportMode.Frame
                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "RtfTabbedText"
                    Dim oService As New StiRtfExportService
                    Dim oSettings As New StiRtfExportSettings

                    oSettings.ExportMode = StiRtfExportMode.TabbedText
                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "RtfTable"
                    Dim oService As New StiRtfExportService
                    Dim oSettings As New StiRtfExportSettings

                    oSettings.ExportMode = StiRtfExportMode.Table
                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "RtfWinWord"
                    Dim oService As New StiRtfExportService
                    Dim oSettings As New StiRtfExportSettings

                    oSettings.ExportMode = StiRtfExportMode.WinWord
                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "Word2007"
                    Dim oService As New StiWord2007ExportService
                    Dim oSettings As New StiWord2007ExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "Odt"
                    Dim oService As New StiOdtExportService
                    Dim oSettings As New StiOdtExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "Excel"
                    Dim oService As New StiExcelExportService
                    Dim oSettings As New StiExcelExportSettings

                    oSettings.UseOnePageHeaderAndFooter = True
                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "ExcelXml"
                    Dim oService As New StiExcelXmlExportService
                    Dim oSettings As New StiExcelXmlExportSettings

                    oSettings.UseOnePageHeaderAndFooter = True
                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "Excel2007"
                    Dim oService As New StiExcel2007ExportService
                    Dim oSettings As New StiExcel2007ExportSettings

                    oSettings.UseOnePageHeaderAndFooter = True
                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "Ods"
                    Dim oService As New StiOdsExportService
                    Dim oSettings As New StiOdsExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "Csv"
                    Dim oService As New StiCsvExportService
                    Dim oSettings As New StiCsvExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "Dbf"
                    Dim oService As New StiDbfExportService
                    Dim oSettings As New StiDbfExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "Xml"
                    Dim oService As New StiXmlExportService
                    Dim oSettings As New StiXmlExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "Dif"
                    Dim oService As New StiDifExportService
                    Dim oSettings As New StiDifExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "Sylk"
                    Dim oService As New StiSylkExportService
                    Dim oSettings As New StiSylkExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "ImageGif"
                    Dim oService As New StiGifExportService
                    Dim oSettings As New StiGifExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "ImageBmp"
                    Dim oService As New StiBmpExportService
                    Dim oSettings As New StiBmpExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "ImagePcx"
                    Dim oService As New StiPcxExportService
                    Dim oSettings As New StiPcxExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "ImagePng"
                    Dim oService As New StiPngExportService
                    Dim oSettings As New StiPngExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "ImageTiff"
                    Dim oService As New StiTiffExportService
                    Dim oSettings As New StiTiffExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "ImageJpeg"
                    Dim oService As New StiJpegExportService
                    Dim oSettings As New StiJpegExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)

                Case "ImageEmf"
                    Dim oService As New StiEmfExportService
                    Dim oSettings As New StiEmfExportSettings

                    oService.ExportTo(oReport.oReportStimulus, oFileStream, oSettings)
            End Select

            oFileStream.Flush()
            oFileStream.Close()

            STSaveToFile = True

        Catch ex As Exception
            ErrDisp(ex, "STSaveToFile")
        End Try
    End Function

    Private Sub InitStimulus()
        Try
            ' Stimulus Ultimate 2020.2.2
            Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHkcgIvwL0jnpsDqRpWg5FI5kt2G7A0tYIcUygBh1sPs7koivWV0htru4Pn2682" +
                                             "yhdY3+9jxMCVTKcKAjiEjgJzqXgLFCpe62hxJ7/VJZ9Hq5l39md0pyydqd5Dc1fSWhCtYqC042BVmGNkukYJQN0ufCozjA/qsNxzNMy" +
                                             "Eql26oHE6wWE77pHutroj+tKfOO1skJ52cbZklqPm8OiH/9mfU4rrkLffOhDQFnIxxhzhr2BL5pDFFCZ7axXX12y/4qzn5QLPBn1AVL" +
                                             "o3NVrSmJB2KiwGwR4RL4RsYVxGScsYoCZbwqK2YrdbPHP0t5vOiLjBQ+Oy6F4rNtDYHn7SNMpthfkYiRoOibqDkPaX+RyCany0Z+uz8" +
                                             "bzAg0oprJEn6qpkQ56WMEppdMJ9/CBnEbTFwn1s/9s8kYsmXCvtI4iQcz+RkUWspLcBzlmj0lJXWjTKMRZz+e9PmY11Au16wOnBU3NH" +
                                             "vRc9T/Zk0YFh439GKd/fRwQrk8nJevYU65ENdAOqiP5po7Vnhif5FCiHRpxgF"

        Catch ex As Exception
            ErrDisp(ex, "InitStimulus")
        End Try
    End Sub

    Private Function STSaveToPDF(Optional cFileName As String = "c:\StimulusReport.PDF", Optional nQuality As Integer = 1, Optional cDatabase As String = "") As Boolean

        STSaveToPDF = False

        Try
            Dim pdfService As New StiPdfExportService
            Dim pdfSettings As New StiPdfExportSettings
            Dim oFileStream As New IO.FileStream(cFileName, FileMode.Create, FileAccess.ReadWrite)

            If Not StiLoadReport(cDatabase) Then Exit Function

            Select Case nQuality
                Case 1 ' default
                    pdfSettings.Compressed = True
                    pdfSettings.EmbeddedFonts = True
                    pdfSettings.ImageCompressionMethod = StiPdfImageCompressionMethod.Jpeg
                    pdfSettings.ImageQuality = 1
                    pdfSettings.ImageResolution = 100
                Case 2 ' mid
                    pdfSettings.Compressed = True
                    pdfSettings.EmbeddedFonts = False
                    pdfSettings.ImageCompressionMethod = StiPdfImageCompressionMethod.Jpeg
                    pdfSettings.ImageQuality = 0.4
                    pdfSettings.ImageResolution = 50
                Case 3 ' high
                    pdfSettings.Compressed = False
                    pdfSettings.EmbeddedFonts = True
                    pdfSettings.ImageCompressionMethod = StiPdfImageCompressionMethod.Flate
                    pdfSettings.ImageQuality = 2
                    pdfSettings.ImageResolutionMode = StiImageResolutionMode.Auto
            End Select

            pdfSettings.CreatorString = "WinTex Programi"
            pdfSettings.KeywordsString = "WinTex Raporu"

            oReport.oReportStimulus.Compile()
            AssingReportVariables()
            oReport.oReportStimulus.Render()

            pdfService.ExportPdf(oReport.oReportStimulus, oFileStream, pdfSettings)

            oFileStream.Flush()
            oFileStream.Close()

            STSaveToPDF = True

        Catch ex As Exception
            ErrDisp(ex, "STSaveToPDF")
        End Try
    End Function

    Private Function StiLoadReport(Optional cDatabase As String = "") As Boolean

        Dim cSQL As String = ""
        Dim oSQL As SQLServerClass
        Dim oDataBase As New StiSqlDatabase
        Dim cConnStr As String = ""

        StiLoadReport = False

        Try
            If oReport.cReportID = "" Then
                Exit Function
            End If

            oReport.cReportName = ""
            oReport.cReportClass = ""
            oReport.cReport = ""

            oSQL = New SQLServerClass(True,, cDatabase)

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select reportname, report, reportclass " +
                             " from stireports with (NOLOCK) " +
                             " where reportid = " + oReport.cReportID

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                oReport.cReportName = oSQL.SQLReadString("reportname")
                oReport.cReportClass = oSQL.SQLReadString("reportclass")
                oReport.cReport = oSQL.SQLReadString("report")
                oReport.cReport = Replace(oReport.cReport, "||", "'")
            End If
            oSQL.oReader.Close()

            oReport.oReportStimulus = New Stimulsoft.Report.StiReport
            oReport.oReportStimulus.LoadFromString(oReport.cReport) ' Load("..\..\Reports\SimpleList.mrt")
            oReport.oReportStimulus.Dictionary.Databases.Clear()

            oDataBase.Name = "WinTex" ' oConnection.cDatabase
            oDataBase.Alias = "WinTex" ' oConnection.cDatabase
            oDataBase.ConnectionString = oSQL.cConnectionString
            oReport.oReportStimulus.Dictionary.Databases.Add(oDataBase)

            oSQL.CloseConn()
            oSQL = Nothing

            StiLoadReport = True

        Catch ex As Exception
            ErrDisp(ex, "StiLoadReport")
        End Try
    End Function

    Private Sub AssingReportVariables()
        Try
            oReport.oReportStimulus.Item("cVariable1") = oReport.cReportVariable1
            oReport.oReportStimulus.Item("cVariable2") = oReport.cReportVariable2
            oReport.oReportStimulus.Item("cVariable3") = oReport.cReportVariable3
            oReport.oReportStimulus.Item("cVariable4") = oReport.cReportVariable4
            oReport.oReportStimulus.Item("cVariable5") = oReport.cReportVariable5
            oReport.oReportStimulus.Item("cVariable6") = oReport.cReportVariable6
            oReport.oReportStimulus.Item("cVariable7") = oReport.cReportVariable7
            oReport.oReportStimulus.Item("cVariable8") = oReport.cReportVariable8
            oReport.oReportStimulus.Item("cVariable9") = oReport.cReportVariable9
            oReport.oReportStimulus.Item("cVariable10") = oReport.cReportVariable10

        Catch ex As Exception
            ErrDisp(ex, "AssingReportVariables")
        End Try
    End Sub

    Public Function STIEvent(ByVal cMail As String, ByVal nReportID As Double, Optional ByVal cFilter As String = "", Optional ByVal cSubject As String = "",
                             Optional ByVal lOK As Boolean = True, Optional ByVal cDatabase As String = "", Optional cSQLDoBefore As String = "",
                             Optional cSQLDoAfter As String = "") As Boolean

        STIEvent = False

        Try
            Dim cFileName As String = ""
            Dim cTodaysDate As String = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")
            Dim oSQL As SQLServerClass

            If cMail.Trim = "" Then Exit Function
            If cDatabase.Trim = "" Then Exit Function
            If nReportID = 0 Then Exit Function

            If cSubject.Trim = "" Then
                cSubject = "Periyodik WinTex Raporu Ektedir"
            End If

            If lOK Then

                oSQL = New SQLServerClass(True,, cDatabase)

                oSQL.OpenConn()

                oSQL.SQLExecute(cSQLDoBefore)

                If (My.Computer.FileSystem.DirectoryExists("C:\wintexservice\WinTexServiceTemp") = False) Then
                    My.Computer.FileSystem.CreateDirectory("C:\wintexservice\WinTexServiceTemp")
                End If

                cFileName = "C:\wintexservice\WinTexServiceTemp\WinTexRaporu-" + cDatabase + "-" + nReportID.ToString + "-" + cTodaysDate + ".pdf"

                If My.Computer.FileSystem.FileExists(cFileName) Then
                    My.Computer.FileSystem.DeleteFile(cFileName)
                End If

                If StiReportToPDF(nReportID.ToString, cFileName, cFilter,,,,,,,,,, cDatabase) Then
                    If SendGoogleMail(cMail, cSubject, "Periyodik WinTex Raporu Ektedir", cFileName, cDatabase) Then
                        STIEvent = True
                        oWinTexServiceMain.CreateLog(cSubject + " " + cDatabase + " rapor başarıyla eMaillendi : " + cMail)
                    Else
                        oWinTexServiceMain.CreateLog(cSubject + " " + cDatabase + " rapor eMail gönderim hatası")
                    End If
                    My.Computer.FileSystem.DeleteFile(cFileName)
                Else
                    oWinTexServiceMain.CreateLog(cSubject + " " + cDatabase + " rapor STI hatası")
                End If

                oSQL.SQLExecute(cSQLDoAfter)

                oSQL.CloseConn()
            Else
                oWinTexServiceMain.CreateLog(cSubject + " " + cDatabase + " raporlanacak bilgi bulunamadı")
                'STIEvent = SendGoogleMail(cMail, cSubject, "Raporlanacak bilgi bulunamadı",, cDatabase)
            End If

        Catch ex As Exception
            ErrDisp(ex, "STIEvent")
        End Try
    End Function

End Module
