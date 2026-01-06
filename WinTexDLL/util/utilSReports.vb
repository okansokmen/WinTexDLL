Option Explicit On
Option Strict On

Imports Stimulsoft.Report
Imports Stimulsoft.Report.Dictionary
Imports Stimulsoft.Report.Print
Imports Stimulsoft.Report.Export

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Windows.Forms

Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess.Sql
Imports DevExpress.DataAccess.ConnectionParameters

Imports DevExpress.XtraEditors
Imports DevExpress.DataAccess

Imports Microsoft.SqlServer.Server

Imports iTextSharp.text.pdf

Module utilSReports

    Public Sub InitReporting()

        'Dim cAppDir As String = cRootDir ' System.AppDomain.CurrentDomain.BaseDirectory
        'Dim cNewDir As String = ""

        Try
            'cAppDir = "C:\"
            'cNewDir = cAppDir + "Users\" + cUserName
            'If Not My.Computer.FileSystem.DirectoryExists(cNewDir) Then
            '    My.Computer.FileSystem.CreateDirectory(cNewDir)
            'End If

            'StiOptions.Configuration.DefaultReportSettingsPath = cNewDir
            'StiOptions.Configuration.DefaultReportConfigPath = cNewDir
            'StiOptions.Configuration.SearchLocalizationFromRegistry = False 

        Catch ex As Exception
            ErrDisp("InitReporting : " + ex.Message.Trim, "utilSReports")
        End Try
    End Sub

    Public Function DDBBackup(Optional cBackupDir As String = "") As Boolean

        Dim cSQL As String = ""
        Dim cReportName As String = ""
        Dim cReport As String = ""
        Dim cReportClass As String = ""
        Dim ConnYage As SqlConnection
        Dim dr As SqlDataReader
        Dim oFileDialogue As New SaveFileDialog
        Dim cPath As String = ""
        Dim cFileName As String = ""

        DDBBackup = False

        Try
            If cBackupDir.Trim = "" Then
                cPath = GetSharePath("DDBReportsBackup")
                If cPath.Trim = "" Then
                    cPath = cBackupDir.Trim
                End If
            Else
                cPath = cBackupDir.Trim
            End If

            My.Computer.FileSystem.CreateDirectory(cPath)

            ConnYage = OpenConn()

            cSQL = "select reportname, reportclass, report " +
                " from devxdashboards with (NOLOCK) " +
                " where reportid is not null " +
                " and reportid <> '' " +
                " order by reportid "

            dr = New SqlCommand(cSQL, ConnYage).ExecuteReader()

            Do While dr.Read()
                cReportName = SQLReadString(dr, "reportname")
                cReportClass = SQLReadString(dr, "reportclass")
                cReport = SQLReadString(dr, "report")
                cReport = Replace(cReport, "||", "'")

                cFileName = cPath + "\" + cReportClass + "_" + StrStripLettersNumbers(cReportName) + ".xml"
                My.Computer.FileSystem.WriteAllText(cFileName, cReport, False)
            Loop
            dr.Close()

            CloseConn(ConnYage)

            DDBBackup = True

        Catch ex As Exception
            ErrDisp("DDBBackup : " + ex.Message.Trim, "utilSReports", cSQL)
        End Try
    End Function

    Public Function StiBackup(Optional cBackupDir As String = "", Optional lOUS2 As Boolean = False) As Boolean

        Dim cSQL As String = ""
        Dim ConnYage As SqlConnection
        Dim dr As SqlDataReader
        Dim cPath As String = ""
        Dim cReportName As String = ""
        Dim cReportClass As String = ""
        Dim cReport As String = ""
        Dim cFileName As String = ""

        StiBackup = False
        Try
            If cBackupDir.Trim = "" Then
                cPath = GetSharePath("STIReportsBackup")
                If cPath.Trim = "" Then
                    cPath = cBackupDir.Trim
                End If
            Else
                cPath = cBackupDir.Trim
            End If

            My.Computer.FileSystem.CreateDirectory(cPath)

            ConnYage = OpenConn()

            cSQL = "select reportid, reportname, reportclass, report " +
                    " from stireports with (NOLOCK) " +
                    " where reportid is not null " +
                    " and reportid <> '' "

            If lOUS2 Then
                cSQL = cSQL +
                    " and exists (select reportid  " +
                                " from ous2 with (NOLOCK) " +
                                " where ous = 'STI-Report' " +
                                " and reportid = stireports.reportid ) "
            End If

            cSQL = cSQL +
                    " order by reportid "

            dr = New SqlCommand(cSQL, ConnYage).ExecuteReader()

            Do While dr.Read()
                cReportName = SQLReadString(dr, "reportname")
                cReportClass = SQLReadString(dr, "reportclass")
                cReport = SQLReadString(dr, "report")
                cReport = Replace(cReport, "||", "'")

                cFileName = cPath + "\" + cReportClass + "_" + StrStripLettersNumbers(cReportName) + ".mrt"
                My.Computer.FileSystem.WriteAllText(cFileName, cReport, False)
            Loop
            dr.Close()

            CloseConn(ConnYage)

            StiBackup = True

        Catch ex As Exception
            ErrDisp("StiBackup : " + ex.Message.Trim, "utilSReports", cSQL)
        End Try
    End Function

    Public Function StiLoadReport() As Boolean

        Dim cSQL As String = ""
        Dim ConnYage As SqlConnection
        Dim dr As SqlDataReader

        StiLoadReport = False

        Try
            If oReport.cReportID = "" Then
                Exit Function
            End If

            ConnYage = OpenConn()

            oReport.cReportName = ""
            oReport.cReportClass = ""
            oReport.cReport = ""

            cSQL = "select reportname, report, nvreport, reportclass, digerdiller " +
                    " from stireports with (NOLOCK) " +
                    " where reportid = " + oReport.cReportID

            dr = New SqlCommand(cSQL, ConnYage).ExecuteReader(CommandBehavior.SingleRow)

            If dr.Read Then
                oReport.cReportName = SQLReadString(dr, "reportname")
                oReport.cReportClass = SQLReadString(dr, "reportclass")
                oReport.cDigerDiller = SQLReadString(dr, "digerdiller").ToUpper
                If oReport.cDigerDiller = "E" Then
                    If SQLReadString(dr, "nvreport") = "" Then
                        oReport.cReport = SQLReadString(dr, "report")
                    Else
                        oReport.cReport = SQLReadString(dr, "nvreport")
                    End If
                Else
                    oReport.cReport = SQLReadString(dr, "report")
                End If
                oReport.cReport = Replace(oReport.cReport, "||", "'")
            End If
            dr.Close()

            CloseConn(ConnYage)

            If oReport.cReportName = "" Then
                Exit Function
            End If

            oReport.oReportStimulus = New Stimulsoft.Report.StiReport
            oReport.oReportStimulus.LoadFromString(oReport.cReport) ' Load("..\..\Reports\SimpleList.mrt")
            oReport.oReportStimulus.Dictionary.Databases.Clear()
            AddDataBase(oReport.oReportStimulus)
            StiLoadReport = True

        Catch ex As Exception
            ErrDisp("StiLoadReport : " + ex.Message.Trim, "utilSReports", cSQL)
        End Try
    End Function

    Public Sub AddDataBase(ByRef oReport As StiReport)

        Dim oDataBase As StiSqlDatabase

        oDataBase = New StiSqlDatabase()
        oDataBase.Name = "WinTex" ' oConnection.cDatabase
        oDataBase.Alias = "WinTex" ' oConnection.cDatabase
        oDataBase.ConnectionString = oConnection.cConnStr
        oReport.Dictionary.Databases.Add(oDataBase)
    End Sub

    Public Sub AddDataSourceToDatabase(ByRef oReport As StiReport, ByVal cNameinSource As String, ByVal cName As String, ByVal cSQL As String)

        Dim oDataSource As StiSqlSource

        oDataSource = New StiSqlSource()
        oDataSource.NameInSource = cNameinSource
        oDataSource.Name = cName
        oDataSource.Alias = cName
        oDataSource.ConnectOnStart = False
        oDataSource.ReconnectOnEachRow = False
        oDataSource.SqlCommand = cSQL
        oReport.Dictionary.DataSources.Add(oDataSource)
    End Sub

    Public Sub AddVariableToDatabase(ByRef oReport As StiReport, Optional ByVal cValue As String = " where 1=1 ", Optional ByVal cVariableName As String = "cVariable")

        Dim oVar As StiVariable

        oVar = New StiVariable
        oVar.Name = cVariableName
        oVar.Alias = cVariableName
        oVar.Description = "Filter expression"
        oVar.ReadOnly = False
        oVar.Value = cValue
        oReport.Dictionary.Variables.Add(oVar)
    End Sub

    Private Function GetWeekStartDate(nYear As Integer, nWeek As Integer) As Date

        Dim nDay As Integer
        Dim jan1 As DateTime
        Dim nDelta As Integer

        jan1 = New DateTime(nYear, 1, 1)
        nDay = jan1.DayOfWeek - 1
        If nDay < 4 Then
            nDelta = -nDay + 7 * (nWeek - 1)
        Else
            nDelta = 7 - nDay + 7 * (nWeek - 1)
        End If
        GetWeekStartDate = jan1.AddDays(nDelta)
    End Function

    Public Sub DatabaseUpdates()

        Dim cSQL As String = ""
        Dim nCnt As Integer = 0
        Dim aGenSQL(2) As String

        Try
            aGenSQL(0) = "CREATE TABLE stireports(reportid int NOT NULL, " +
                            " reportname char(100) NULL, " +
                            " report text NULL, " +
                            " ts timestamp NULL, " +
                            " reportclass char(30) NULL, " +
                            " CONSTRAINT PK_stireports PRIMARY KEY CLUSTERED (reportid ASC)) "

            aGenSQL(1) = "alter table stireports add reportactive char(1) null "
            aGenSQL(2) = "alter table stireports add varsayilan char(1) null, printername char(250) null, copies decimal(5,0) null, defaultfilter text null, emailnotify char(100) null, webreport char(1) null  "

            For nCnt = 0 To UBound(aGenSQL)
                If aGenSQL(nCnt).Trim <> "" Then
                    GenSQL(aGenSQL(nCnt))
                End If
            Next

        Catch ex As Exception
            ErrDisp("DatabaseUpdates : " + ex.Message.Trim, "DBUpdate-1", cSQL, False)
        End Try
    End Sub

    Private Sub GenSQL(ByVal cSQL As String)

        Dim ConnYage As SqlClient.SqlConnection

        Try
            ConnYage = OpenConn()
            ExecuteSQLCommandConnected(cSQL, ConnYage)
            CloseConn(ConnYage)

        Catch ex As Exception
            'ErrDisp("GenSQL : " + ex.Message.Trim, "DBUpdate-2", cSQL, False)
        End Try
    End Sub

    Public Sub STDirectPrint()

        'Dim oPrinterSettings As New System.Drawing.Printing.PrinterSettings

        Try
            If Not StiLoadReport() Then Exit Sub
            oReport.oReportStimulus.Compile()
            AssingReportVariables
            oReport.oReportStimulus.Render()
            oReport.oReportStimulus.PrinterSettings.Copies = CInt(oReport.nCopies)
            If oReport.cPrinterName.Trim <> "" Then
                oReport.oReportStimulus.PrinterSettings.PrinterName = oReport.cPrinterName
            End If
            oReport.oReportStimulus.Print(False)

            'oPrinterSettings.Copies = 1
            'oReport.Print(oPrinterSettings)

        Catch ex As Exception
            ErrDisp("STDirectPrint : " + ex.Message.Trim, "UtilSReports")
        End Try
    End Sub

    Public Sub STSaveToPDF(Optional cFileName As String = "c:\StimulusReport.PDF", Optional nQuality As Integer = 1)
        Try
            Dim pdfService As New StiPdfExportService
            Dim pdfSettings As New StiPdfExportSettings
            Dim oFileStream As New IO.FileStream(cFileName, FileMode.Create, FileAccess.ReadWrite)

            If Not StiLoadReport() Then Exit Sub

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

            'oReport.oReportStimulus.ExportDocument(Stimulsoft.Report.StiExportFormat.Pdf, cFileName, pdfSettings)

        Catch ex As Exception
            ErrDisp("STSaveToPDF : " + ex.Message.Trim, "UtilSReports")
        End Try
    End Sub

    Public Sub STSaveToFile(Optional cFileName As String = "c:\StimulusReport.PDF", Optional cFileType As String = "PDF")
        Try
            Dim oFileStream As New IO.FileStream(cFileName, FileMode.Create, FileAccess.ReadWrite)

            If Not StiLoadReport() Then Exit Sub

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

        Catch ex As Exception
            ErrDisp("STSaveToFile FileName : " + cFileName + " FileType : " + cFileType + " Error : " + ex.Message.Trim, "UtilSReports")
        End Try
    End Sub

    Public Sub STExportSourceToFile(ByVal cRecID As String)

        Dim cSQL As String = ""
        Dim cReportName As String = ""
        Dim cReport As String = ""
        Dim cReportClass As String = ""
        Dim cDigerDiller As String = ""
        Dim ConnYage As SqlConnection
        Dim dr As SqlDataReader
        Dim oFileDialogue As New SaveFileDialog

        Try
            ConnYage = OpenConn()

            cSQL = "select reportname, report, nvreport, reportclass, digerdiller " +
                    " from stireports with (NOLOCK) " +
                    " where reportid = " + cRecID

            dr = New SqlCommand(cSQL, ConnYage).ExecuteReader(CommandBehavior.SingleRow)

            If dr.Read Then
                cReportName = SQLReadString(dr, "reportname")
                cReportClass = SQLReadString(dr, "reportclass")
                cDigerDiller = SQLReadString(dr, "digerdiller")
                If cDigerDiller = "E" Then
                    If SQLReadString(dr, "nvreport") = "" Then
                        cReport = SQLReadString(dr, "report")
                    Else
                        cReport = SQLReadString(dr, "nvreport")
                    End If
                Else
                    cReport = SQLReadString(dr, "report")
                End If
                cReport = Replace(cReport, "||", "'")
            End If
            dr.Close()

            CloseConn(ConnYage)

            oFileDialogue.Title = "Dosya konumunu seçiniz"
            oFileDialogue.DefaultExt = ".mrt"
            oFileDialogue.FileName = cReportClass + "_" + cReportName
            oFileDialogue.ShowDialog()
            My.Computer.FileSystem.WriteAllText(oFileDialogue.FileName, cReport, False)

        Catch ex As Exception
            ErrDisp("STExportSourceToFile : " + ex.Message, "UtilSReports", cSQL)
        End Try
    End Sub

    Public Sub DDBExportSourceToFile(ByVal cRecID As String)

        Dim cSQL As String = ""
        Dim cReportName As String = ""
        Dim cReport As String = ""
        Dim cReportClass As String = ""
        Dim cDigerDiller As String = ""
        Dim ConnYage As SqlConnection
        Dim dr As SqlDataReader
        Dim oFileDialogue As New SaveFileDialog

        Try
            ConnYage = OpenConn()

            cSQL = "select reportname, reportclass, report " +
                    " from devxdashboards with (NOLOCK) " +
                    " where reportid = " + cRecID

            dr = New SqlCommand(cSQL, ConnYage).ExecuteReader(CommandBehavior.SingleRow)
            If dr.Read Then
                cReportName = SQLReadString(dr, "reportname")
                cReportClass = SQLReadString(dr, "reportclass")
                cReport = SQLReadString(dr, "report")
                cReport = Replace(cReport, "||", "'")
            End If
            dr.Close()

            CloseConn(ConnYage)

            oFileDialogue.Title = "Dosya konumunu seçiniz"
            oFileDialogue.DefaultExt = ".xml"
            oFileDialogue.FileName = cReportClass + "_" + cReportName
            oFileDialogue.ShowDialog()

            My.Computer.FileSystem.WriteAllText(oFileDialogue.FileName, cReport, False)

        Catch ex As Exception
            ErrDisp("DDBExportSourceToFile : " + ex.Message, "UtilSReports", cSQL)
        End Try
    End Sub

    Public Sub STImportFileToReport()

        Dim cReportName As String = ""
        Dim cReportClass As String = ""
        Dim cReport As String = ""
        Dim cSQL As String = ""
        Dim cRecID As String = ""
        Dim nPos As Integer = 0
        Dim nCnt As Integer = 0
        Dim oFileDialogue As New OpenFileDialog

        Try
            oFileDialogue.Title = "Dosya konumunu seçiniz"
            oFileDialogue.DefaultExt = ".mrt"
            oFileDialogue.FileName = ""
            oFileDialogue.Multiselect = True
            oFileDialogue.ShowDialog()

            If oFileDialogue.FileNames.Count = 0 Then
                MsgBox("Dikkat dosya seçilmedi, aktarım yapılmayacaktır")

            ElseIf oFileDialogue.FileNames.Count = 1 Then
                cReportName = oFileDialogue.FileNames(0)
                cReport = My.Computer.FileSystem.ReadAllText(cReportName)

                cReportName = My.Computer.FileSystem.GetName(cReportName)
                nPos = InStr(cReportName, "_")
                If nPos > 0 Then
                    cReportClass = Mid(cReportName, 1, nPos - 1)
                    cReportName = Mid(cReportName, nPos + 1)
                End If

                nPos = InStr(cReportName, ".")
                If nPos > 0 Then
                    cReportName = Mid(cReportName, 1, nPos - 1)
                End If
                cReportName = InputBox("Rapor adı", "Çıkış için boş bırakınız", cReportName)
                If cReportName.Trim = "" Then Exit Sub

                cReportClass = InputBox("Rapor sınıfı", "Çıkış için boş bırakınız", cReportClass)
                If cReportClass.Trim = "" Then Exit Sub

                cRecID = GetFisNo("reportid")

                cSQL = "delete stireports " +
                        " where reportname = '" + cReportName.Trim + "' " +
                        " and reportclass = '" + cReportClass.Trim + "' "

                ExecuteSQLCommand(cSQL)

                cReport = Replace(cReport, "'", "||")

                cSQL = "insert into stireports (reportid, reportname, reportclass, report)  " +
                    " values (" + cRecID + ", " +
                    " '" + SQLWriteString(cReportName) + "', " +
                    " '" + SQLWriteString(cReportClass) + "', " +
                    " '" + SQLWriteString(cReport) + "') "

                ExecuteSQLCommand(cSQL)
            Else
                For nCnt = 0 To oFileDialogue.FileNames.Count - 1
                    cReportName = oFileDialogue.FileNames(nCnt)
                    cReport = My.Computer.FileSystem.ReadAllText(cReportName)

                    cReportName = My.Computer.FileSystem.GetName(cReportName)
                    nPos = InStr(cReportName, "_")
                    If nPos > 0 Then
                        cReportClass = Mid(cReportName, 1, nPos - 1)
                        cReportName = Mid(cReportName, nPos + 1)
                    End If

                    nPos = InStr(cReportName, ".")
                    If nPos > 0 Then
                        cReportName = Mid(cReportName, 1, nPos - 1)
                    End If

                    If cReportName.Trim <> "" And cReportClass.Trim <> "" Then

                        cRecID = GetFisNo("reportid")

                        cSQL = "delete stireports " +
                                " where reportname = '" + cReportName.Trim + "' " +
                                " and reportclass = '" + cReportClass.Trim + "' "

                        ExecuteSQLCommand(cSQL)

                        cReport = Replace(cReport, "'", "||")

                        cSQL = "insert into stireports (reportid, reportname, reportclass, report)  " +
                                " values (" + cRecID + ", " +
                                " '" + SQLWriteString(cReportName) + "', " +
                                " '" + SQLWriteString(cReportClass) + "', " +
                                " '" + SQLWriteString(cReport) + "') "

                        ExecuteSQLCommand(cSQL)
                    End If
                Next
            End If
        Catch ex As Exception
            ErrDisp("STImportFileToReport : " + ex.Message, "UtilSReports", cSQL)
        End Try
    End Sub

    Public Sub DDBImportFileToReport()

        Dim cReportName As String = ""
        Dim cReportClass As String = ""
        Dim cReport As String = ""
        Dim cSQL As String = ""
        Dim cRecID As String = ""
        Dim nPos As Integer = 0
        Dim nCnt As Integer = 0
        Dim oFileDialogue As New OpenFileDialog

        Try
            oFileDialogue.Title = "Dosya konumunu seçiniz"
            oFileDialogue.DefaultExt = ".xml"
            oFileDialogue.FileName = ""
            oFileDialogue.Multiselect = True
            oFileDialogue.ShowDialog()

            If oFileDialogue.FileNames.Count = 0 Then
                MsgBox("Dikkat dosya seçilmedi, aktarım yapılmayacaktır")

            ElseIf oFileDialogue.FileNames.Count = 1 Then
                cReportName = oFileDialogue.FileNames(0)
                cReport = My.Computer.FileSystem.ReadAllText(cReportName)

                cReportName = My.Computer.FileSystem.GetName(cReportName)
                nPos = InStr(cReportName, "_")
                If nPos > 0 Then
                    cReportClass = Mid(cReportName, 1, nPos - 1)
                    cReportName = Mid(cReportName, nPos + 1)
                End If

                nPos = InStr(cReportName, ".")
                If nPos > 0 Then
                    cReportName = Mid(cReportName, 1, nPos - 1)
                End If
                cReportName = InputBox("Rapor adı", "Çıkış için boş bırakınız", cReportName)
                If cReportName.Trim = "" Then Exit Sub

                cReportClass = InputBox("Rapor sınıfı", "Çıkış için boş bırakınız", cReportClass)
                If cReportClass.Trim = "" Then Exit Sub

                cRecID = GetFisNo("reportid")

                cSQL = "delete devxdashboards " +
                        " where reportname = '" + cReportName.Trim + "' " +
                        " and reportclass = '" + cReportClass.Trim + "' "

                ExecuteSQLCommand(cSQL)

                cReport = Replace(cReport, "'", "||")

                cSQL = "insert into devxdashboards (reportid, reportname, reportclass, report)  " +
                        " values (" + cRecID + ", " +
                        " '" + SQLWriteString(cReportName) + "', " +
                        " '" + SQLWriteString(cReportClass) + "', " +
                        " '" + SQLWriteString(cReport) + "') "

                ExecuteSQLCommand(cSQL)
            Else
                For nCnt = 0 To oFileDialogue.FileNames.Count - 1
                    cReportName = oFileDialogue.FileNames(nCnt)
                    cReport = My.Computer.FileSystem.ReadAllText(cReportName)

                    cReportName = My.Computer.FileSystem.GetName(cReportName)
                    nPos = InStr(cReportName, "_")
                    If nPos > 0 Then
                        cReportClass = Mid(cReportName, 1, nPos - 1)
                        cReportName = Mid(cReportName, nPos + 1)
                    End If

                    nPos = InStr(cReportName, ".")
                    If nPos > 0 Then
                        cReportName = Mid(cReportName, 1, nPos - 1)
                    End If

                    If cReportName.Trim <> "" And cReportClass.Trim <> "" Then

                        cRecID = GetFisNo("reportid")

                        cSQL = "delete devxdashboards " +
                                " where reportname = '" + cReportName.Trim + "' " +
                                " and reportclass = '" + cReportClass.Trim + "' "

                        ExecuteSQLCommand(cSQL)

                        cReport = Replace(cReport, "'", "||")

                        cSQL = "insert into devxdashboards (reportid, reportname, reportclass, report)  " +
                                " values (" + cRecID + ", " +
                                " '" + SQLWriteString(cReportName) + "', " +
                                " '" + SQLWriteString(cReportClass) + "', " +
                                " '" + SQLWriteString(cReport) + "') "

                        ExecuteSQLCommand(cSQL)
                    End If
                Next
            End If

        Catch ex As Exception
            ErrDisp("DDBImportFileToReport : " + ex.Message, "UtilSReports", cSQL)
        End Try
    End Sub

    Public Function DevXLoadReport(Optional nMode As Integer = 2) As Boolean

        ' nMode = 2 DevX
        ' nMode = 3 DevX Dashboard

        Dim cSQL As String = ""
        Dim ConnYage As SqlConnection
        Dim dr As SqlDataReader

        DevXLoadReport = False

        Try
            If oReportDevX.cReportID = "" Then
                Exit Function
            End If

            ConnYage = OpenConn()

            Select Case nMode
                Case 2
                    cSQL = "select reportname, report, reportclass, reportviewname " +
                            " from devxreports with (NOLOCK) " +
                            " where reportid = " + oReportDevX.cReportID

                    dr = New SqlCommand(cSQL, ConnYage).ExecuteReader(CommandBehavior.SingleRow)

                    If dr.Read Then
                        oReportDevX.cReportName = SQLReadString(dr, "reportname")
                        oReportDevX.cReportClass = SQLReadString(dr, "reportclass")
                        oReportDevX.cReport = SQLReadString(dr, "report")
                        oReportDevX.cReport = Replace(oReportDevX.cReport, "||", "'")
                        oReportDevX.cReportViewName = SQLReadString(dr, "reportviewname")
                    End If
                    dr.Close()

                Case 3
                    cSQL = "select reportname, report, reportclass " +
                            " from devxdashboards with (NOLOCK) " +
                            " where reportid = " + oReportDevX.cReportID

                    dr = New SqlCommand(cSQL, ConnYage).ExecuteReader(CommandBehavior.SingleRow)

                    If dr.Read Then
                        oReportDevX.cReportName = SQLReadString(dr, "reportname")
                        oReportDevX.cReportClass = SQLReadString(dr, "reportclass")
                        oReportDevX.cReport = SQLReadString(dr, "report")
                        oReportDevX.cReport = Replace(oReportDevX.cReport, "||", "'")
                    End If
                    dr.Close()

            End Select

            CloseConn(ConnYage)

            If oReportDevX.cReportName = "" Then
                Exit Function
            End If

            DevXLoadReport = True

        Catch ex As Exception
            ErrDisp("DevXLoadReport : " + ex.Message.Trim, "utilSReports", cSQL)
        End Try
    End Function

    Public Sub DevXConnectData(Optional lNewReport As Boolean = False)

        Dim ConnYage As SqlClient.SqlConnection
        Dim oDS As New DataSet()
        Dim oDataAdapter As SqlDataAdapter
        Dim cSQL As String = ""

        Try
            oReportDevX.oReportDeveloperExpress = New DevExpress.XtraReports.UI.XtraReport

            If lNewReport Then
                ' raporun veri kaynağı olarak bir view oluşturulur
                oReportDevX.cReportViewName = CreateTempView(oReportDevX.cReportSQL, , oReportDevX.cReportClass, False)
            Else
                If oReportDevX.cReportViewName.Trim = "" Then
                    MsgBox("Rapor view adı bulunamadı")
                    Exit Sub
                End If
            End If

            ConnYage = OpenConn()

            cSQL = "select * from " + oReportDevX.cReportViewName

            oDataAdapter = New SqlDataAdapter(cSQL, ConnYage)
            oDataAdapter.Fill(oDS, oReportDevX.cReportViewName)
            oReportDevX.oReportDeveloperExpress.DataSource = oDS

            Call CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("DevXConnectData : " + ex.Message.Trim, "utilSReports", cSQL)
        End Try
    End Sub

    Public Sub DevXLoadLayout()

        Dim cTempFile As String = ""

        Try
            cTempFile = GetTempFile("xml")
            My.Computer.FileSystem.WriteAllText(cTempFile, oReportDevX.cReport, False)
            oReportDevX.oReportDeveloperExpress.LoadLayoutFromXml(cTempFile)
            DestroyFile(cTempFile)

        Catch ex As Exception
            ErrDisp("DevXLoadLayout : " + ex.Message.Trim, "utilSReports")
        End Try
    End Sub

    Public Sub STILoadTrLocalization()

        Dim cFileName As String = ""

        Try
            ' Stimulus 2017.2.2
            'Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHnjv55lAH/CvbzcEDqdPW2pMPjRi9UKpZ4a3/0LMIt" +
            '                                "UxmM37ckzcCuCqK0F5uPDaLs0YA3fm+taNyDS1Sq6JV1aF9TEyU8PQQI3aDgL1NtyMT/nZZNnRuGt207LiXll3KftBClD8TVlv+a" +
            '                                "pSCCk6SBJrcZswI1pOWq11JbXPV87IkLq28CjZr5QGpqKxNSVSWQh2vPZywgplsYcZhEAVaV0hY8vUSnWJwinzRJXOMA9DPkMu3k" +
            '                                "vo1jvzK70Tj868aj84hzpeHm4+TIIjCHBp0m5dTHsjysrPHtf6OiMOLqjx994vy4AjasZ9TUin22t3npZmRp8XKrUZ8tZBxnO3Yq" +
            '                                "WzxDmQFq6CgofwNIQI0J8JmOGxQX5xo+rnOsOfUF2fmBiTCA8d4Gn/D6QfG/w3yVO8P/pdKuwyT2oOXg4KqKiVO0BWegywvC6d43" +
            '                                "lvOUCancaBZmIOVjYQ6Nrcr2iLF46t9mIquHxMJw/6zD/zguTBuTo0WHHOYjzDFWP87PMESxDsAWfe5MLa9N6/Yz45oGE1MO10tJ" +
            '                                "LzX/M0Ayu/EyUarQ0hBwhck6rEgL+AWSGrlf3iqrtt8zp1azDh3QoWrBSWHGDdAnhcATyUTIWmMAkZhqhh367dcFivSV9ML7yiKS" +
            '                                "vcn+8jUW2xVJmNR6dnzImE76jG2pYmNFMSxDU+enGzePH+NVSD+pI4OdlcOm3/gI4GiYKe5SGHFerL+1izadJbLGtKOWVw2UcUd/" +
            '                                "GUT0Sv1yg6UjSazOXWIS8T2RVudHcDeDGnCjWDwtRY6aHDOnF+Eaq3fEKTsevuEfzo+YCt8l13HGKWXHhotSPKbiXlRn4YjFjiKQ" +
            '                                "UXet/pk45zZmqUt1vSzI1hoqY8V5foBXvFOerEDNdBngp9i/WH/w9MRbdw6MEokVIDkpLRlFIL9IAHOLNr4zT7l4T5y+9Ynh1Ett" +
            '                                "7ep36Hzxm4jwuPwWcedS03qbgviglUc/6NaayGBfcVSydWsErNGlkM22v6/Jm8zHhYL/Bpm2NhNbhs9iPN23vUf/Xg9XyaIGa/57" +
            '                                "3Mv4/d7Pg8aYkrgcxs1nCEv3tBFV6+6is0IKkTL5OIHqoprUrm9xNmQT8QIkbqxCK7Gh6jeArdtJby/tpQWAe14XyHgJZ5EphTpo" +
            '                                "2Jj89CRqqvKRl/bwBZzjlaw4eV8nxN6Dhfm3KiKyBfEXUrYEaGgErdQfck0NoOoWi0q0oIgB/NtBXMIVRxR7z2EG/avhs+9TZTEj" +
            '                                "Mj+m/Foarq9ndcI8kK8+1d+xcI3d8u90oxF9bgfOLFlVCHt2AHBL5SRlXjbLtk39ipVtgzTQfZzIfgx0/vMDLqr12MU7i/HBKD3t" +
            '                                "9IU3xLmoCS7DlOSnHoAdJdss2rf2BBa6vE8d6gO/NY/0Ayw1HWZLIhNjsdFoQm6QRH+EZAFusj/jMQsdu+QUwifxaICjsa3K3ZtU" +
            '                                "e6yifvHPuyi1WOFj1eYCdlSxIzeKpahL5EBYf0j3eV4NFoiLUQWaP50utm5L6iOTamLmUEJ7kTBP9K++TAWQ+1faZQIA0rhQ="

            ' Stimulus Ultimate 2019.1.1
            'Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHlyBxErOHyaSR9igWOmIpe54D459EnV27B85mUVtGMJoAJ5J4RRiq3EfolQ" +
            '                                 "PYY1F49n0mb+F3MG+0ljOxwN4HATAf6fYoKQ1/iUgY6vaEeC5sHh8gfGTprH6x1fC/NVcPUJ/i6KWSxBY4nIv4lRlX3HOUO3USw0i" +
            '                                 "4dW/9jx9b0QNwcDq4rFeRQw7kTTgkRKmY919lyMvTzfZR8CDGeZLM/HMpmj4ECf64h80SPx1SsMENkOUYkdxWM6RgqOgEc44sMRrb+" +
            '                                 "fGswOItPe+m4VPrei7ioUJDxdHtMlA6PPimgjVQdmfF71YEZn01y2M3by3Y3NoELAswiU5MMV2QeaiPTV/eUpEUyICJPT8qLm8NvbBJ" +
            '                                 "ytSFUM6wCi7NM6rSsjFaVWmpSmfZNJYIGS1DjNadeN62H7tGzPd8WwmKojrERdKtV1df3Uh3FK72EIyeIvc6E5XQCemL1C9dNvvcXax" +
            '                                 "SDRwPP2ax7In67L6Wb6pOKS+4KqxU+ga/dUN0ayxngW/BIl/KCSaMXCot2h1L/1q0x7"

            ' Stimulus Ultimate 2020.2.2
            Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHkcgIvwL0jnpsDqRpWg5FI5kt2G7A0tYIcUygBh1sPs7koivWV0htru4Pn2682" +
                                             "yhdY3+9jxMCVTKcKAjiEjgJzqXgLFCpe62hxJ7/VJZ9Hq5l39md0pyydqd5Dc1fSWhCtYqC042BVmGNkukYJQN0ufCozjA/qsNxzNMy" +
                                             "Eql26oHE6wWE77pHutroj+tKfOO1skJ52cbZklqPm8OiH/9mfU4rrkLffOhDQFnIxxhzhr2BL5pDFFCZ7axXX12y/4qzn5QLPBn1AVL" +
                                             "o3NVrSmJB2KiwGwR4RL4RsYVxGScsYoCZbwqK2YrdbPHP0t5vOiLjBQ+Oy6F4rNtDYHn7SNMpthfkYiRoOibqDkPaX+RyCany0Z+uz8" +
                                             "bzAg0oprJEn6qpkQ56WMEppdMJ9/CBnEbTFwn1s/9s8kYsmXCvtI4iQcz+RkUWspLcBzlmj0lJXWjTKMRZz+e9PmY11Au16wOnBU3NH" +
                                             "vRc9T/Zk0YFh439GKd/fRwQrk8nJevYU65ENdAOqiP5po7Vnhif5FCiHRpxgF"

            Stimulsoft.Base.Design.StiDesignerAppStatus.GetDesignerPath(Stimulsoft.Base.Design.StiPlatformType.WinForms)

            If My.Computer.FileSystem.FileExists("tr.xml") Then
                Stimulsoft.Base.Localization.StiLocalization.Load("tr.xml")
                Exit Sub
            End If

            'Stimulsoft.Report.StiOptions.Engine.FullTrust = False

            cFileName = GetSharePath("Localization")
            cFileName = cFileName + "\tr.xml"
            If My.Computer.FileSystem.FileExists(cFileName) Then
                Stimulsoft.Base.Localization.StiLocalization.Load(cFileName)
            End If
        Catch ex As Exception
            ' do nothing
        End Try
    End Sub

    Public Sub RotatePDFFile(cFileName As String, Optional nRotateAngle As Integer = 270)

        Try
            If cFileName.Trim = "" Then Exit Sub

            Dim nCnt As Integer = 0
            Dim oPageDict As PdfDictionary
            Dim oOutput As New System.IO.FileStream(Replace(cFileName, ".pdf", "_rotated.pdf"), FileMode.Create, FileAccess.ReadWrite)
            Dim oPDFReader As New PdfReader(cFileName)
            Dim nPageCnt As Integer = oPDFReader.NumberOfPages
            Dim oPDFStamper As New PdfStamper(oPDFReader, oOutput)

            For nCnt = 1 To nPageCnt
                oPageDict = oPDFReader.GetPageN(nCnt)
                oPageDict.Put(PdfName.ROTATE, New PdfNumber(nRotateAngle))
            Next

            oPDFStamper.Close()

        Catch ex As Exception
            ErrDisp("RotatePDFFile : " + ex.Message.Trim, "utilSReports")
        End Try
    End Sub


    Public Sub LoadDashboardData(ByRef oDashboard As Dashboard)
        Try
            oDashboard.DataSources.Clear()

            Dim oSqlParams As New MsSqlConnectionParameters()
            oSqlParams.AuthorizationType = MsSqlAuthorizationType.SqlServer
            oSqlParams.ServerName = oConnection.cServer
            oSqlParams.DatabaseName = oConnection.cDatabase
            oSqlParams.UserName = oConnection.cUser
            oSqlParams.Password = oConnection.cPassword

            Dim oDataSource As New DashboardSqlDataSource("WinTexDataSource", oSqlParams)

            AddDataSourceNewCustomSQLQuery(oReportDevX.cReportSQL1, oReportDevX.cReportQueryName1, oDataSource)
            AddDataSourceNewCustomSQLQuery(oReportDevX.cReportSQL2, oReportDevX.cReportQueryName2, oDataSource)
            AddDataSourceNewCustomSQLQuery(oReportDevX.cReportSQL3, oReportDevX.cReportQueryName3, oDataSource)
            AddDataSourceNewCustomSQLQuery(oReportDevX.cReportSQL4, oReportDevX.cReportQueryName4, oDataSource)
            AddDataSourceNewCustomSQLQuery(oReportDevX.cReportSQL5, oReportDevX.cReportQueryName5, oDataSource)
            AddDataSourceNewCustomSQLQuery(oReportDevX.cReportSQL6, oReportDevX.cReportQueryName6, oDataSource)
            AddDataSourceNewCustomSQLQuery(oReportDevX.cReportSQL7, oReportDevX.cReportQueryName7, oDataSource)
            AddDataSourceNewCustomSQLQuery(oReportDevX.cReportSQL8, oReportDevX.cReportQueryName8, oDataSource)
            AddDataSourceNewCustomSQLQuery(oReportDevX.cReportSQL9, oReportDevX.cReportQueryName9, oDataSource)
            AddDataSourceNewCustomSQLQuery(oReportDevX.cReportSQL10, oReportDevX.cReportQueryName10, oDataSource)

            oDataSource.Fill()

            oDashboard.DataSources.Add(oDataSource)

        Catch ex As Exception
            ErrDisp("LoadDashboardData : " + ex.Message.Trim, "utilSReports")
        End Try
    End Sub

    Private Sub AddDataSourceNewCustomSQLQuery(cSQL As String, cName As String, ByRef oDataSource As DashboardSqlDataSource)
        Try
            If cSQL.Trim <> "" Then
                Dim oQuery As New CustomSqlQuery
                oQuery.Name = cName.Trim
                oQuery.Sql = cSQL.Trim
                oDataSource.Queries.Add(oQuery)
            End If

        Catch ex As Exception
            ErrDisp("AddDataSourceNewCustomSQLQuery : " + ex.Message.Trim, "utilSReports", cSQL.Trim)
        End Try
    End Sub

    Public Function GetReportRefreshTime() As Integer

        GetReportRefreshTime = 0

        Dim ConnYage As SqlConnection
        Dim dr As SqlDataReader
        Dim cSQL As String = ""
        Dim cValue As String = ""

        Try
            ConnYage = OpenConn()

            cSQL = "select top 1 parametervalue " +
                    " from syspar with (NOLOCK) " +
                    " where parametername = 'refreshtime' "

            dr = New SqlCommand(cSQL, ConnYage).ExecuteReader(CommandBehavior.SingleRow)

            If dr.Read Then
                cValue = SQLReadString(dr, "parametervalue")
                If IsNumeric(cValue) Then
                    GetReportRefreshTime = CInt(cValue)
                End If
            End If
            dr.Close()

            CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("GetReportRefreshTime : " + ex.Message.Trim, "utilSReports", cSQL.Trim)
        End Try
    End Function

    Public Sub OlcuTablosuYazdir1(cFisNo As String)

        Try
            Dim oSQL As New SQLServerClass
            Dim oSQL2 As New SQLServerClass
            Dim cOlcuTablosu As String = ""
            Dim nEnCekmezlik As Double = 0
            Dim nBoyCekmezlik As Double = 0
            Dim cBedenSeti As String = ""
            Dim nBedenAdet As Integer = 0
            Dim nBedenAdet2 As Integer = 0
            Dim aBeden() As String
            Dim aBeden2() As String
            Dim nCol As Integer = 0
            Dim cOlcuYeri As String = ""
            Dim cAciklama1 As String = ""
            Dim cAciklama2 As String = ""
            Dim nSatirNo As Double = 0
            Dim cOlcu As String = ""
            Dim cqcolcutablosu As String = ""
            Dim cSiparisNo As String = ""
            Dim cOlculen As String = ""
            Dim nOlculen As Double = 0
            Dim cIstenen As String = ""
            Dim nIstenen As Double = 0
            Dim lOK As Boolean = False
            Dim nBdnCnt As Integer = 0
            Dim cSonuc As String = ""

            oSQL2.OpenConn()
            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 siparisno, olcutablosuno " +
                        " from qcfis with (NOLOCK) " +
                        " where fisno = '" + cFisNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cSiparisNo = oSQL.SQLReadString("siparisno")
                cOlcuTablosu = oSQL.SQLReadString("olcutablosuno")
            End If
            oSQL.oReader.Close()

            ' güncel yıkama sonrası kullanılan ölçü tablosu
            oSQL.cSQLQuery = "select top 1 OlcuTablosuNo, EnCekmezlik, BoyCekmezlik, BedenSeti " +
                        " from sipolcu with (NOLOCK) " +
                        " where OlcuTablosuNo = '" + cOlcuTablosu + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                nEnCekmezlik = oSQL.SQLReadDouble("EnCekmezlik")
                nBoyCekmezlik = oSQL.SQLReadDouble("BoyCekmezlik")
                cBedenSeti = oSQL.SQLReadString("BedenSeti")
            End If
            oSQL.oReader.Close()

            If cOlcuTablosu.Trim = "" Then Exit Sub

            aBeden = GetBedenlerFromBedenSeti(cBedenSeti)
            nBedenAdet = aBeden.GetUpperBound(0)

            ' başlığı oluştur
            oSQL.cSQLQuery = "delete bedenisimleri " +
                        " where anahtar1  = '" + cFisNo + "' "

            oSQL.SQLExecute()

            oSQL.cSQLQuery = "insert bedenisimleri (anahtar1) " +
                        " values ('" + cFisNo + "' ) "

            oSQL.SQLExecute()

            ReDim aBeden2(0)
            nBdnCnt = 0

            For nCol = 0 To nBedenAdet

                oSQL2.cSQLQuery = "select olcu1, olcu2 " +
                            " from qcfisolculines with (NOLOCK) " +
                            " where fisno = '" + cFisNo + "' " +
                            " and olcu1 is not null " +
                            " and olcu1 <> '' " +
                            " and olcu2 is not null " +
                            " and olcu2 <> '' " +
                            " and beden = '" + aBeden(nCol) + "' "

                If oSQL2.CheckExists Then

                    nBdnCnt = nBdnCnt + 1

                    ReDim Preserve aBeden2(nBdnCnt - 1)
                    aBeden2(nBdnCnt - 1) = aBeden(nCol)

                    oSQL.cSQLQuery = "update bedenisimleri " +
                                " set b" + (nBdnCnt).ToString("00") + " = '" + aBeden(nCol) + "' " +
                                " where anahtar1 = '" + cFisNo + "' "
                    oSQL.SQLExecute()
                End If
            Next

            nBedenAdet2 = aBeden2.GetUpperBound(0)

            ' satırları oluştur
            oSQL.cSQLQuery = "delete bedenolculeri " +
                        " where anahtar1  = '" + cFisNo + "' "

            oSQL.SQLExecute()

            oSQL.cSQLQuery = "select distinct satirno, bolum, renk, aciklama " +
                        " from sipolcu with (NOLOCK) " +
                        " where olcutablosuno = '" + cOlcuTablosu + "' " +
                        " and notlar is not null " +
                        " and notlar <> '' " +
                        " and exists (select olcu1, olcu2 " +
                                    " from qcfisolculines with (NOLOCK) " +
                                    " where fisno = '" + cFisNo + "' " +
                                    " and olcumyeri = sipolcu.bolum " +
                                    " and olcu1 is not null " +
                                    " and olcu1 <> '' " +
                                    " and olcu2 is not null " +
                                    " and olcu2 <> '' ) " +
                        " order by satirno "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                cOlcuYeri = oSQL.SQLReadString("bolum")
                cAciklama1 = oSQL.SQLReadString("renk")
                cAciklama2 = oSQL.SQLReadString("aciklama")
                nSatirNo = oSQL.SQLReadDouble("satirno")
                lOK = False

                oSQL2.cSQLQuery = "insert bedenolculeri (anahtar1, olcuyeri, aciklama1, aciklama2, satirno) " +
                            " values ('" + cFisNo + "', " +
                            " '" + cOlcuYeri + "', " +
                            " '" + cAciklama1 + "', " +
                            " '" + cAciklama2 + "', " +
                            SQLWriteDecimal(nSatirNo) + " ) "

                oSQL2.SQLExecute()

                For nCol = 0 To nBedenAdet2

                    ' istenen ve ölçülen
                    cIstenen = ""
                    cOlculen = ""

                    oSQL2.cSQLQuery = "select top 1 olcu1, olcu2 " +
                                " from qcfisolculines with (NOLOCK) " +
                                " where fisno = '" + cFisNo + "' " +
                                " and olcumyeri = '" + cOlcuYeri + "' " +
                                " and beden = '" + aBeden2(nCol) + "' " +
                                " and olcu1 is not null " +
                                " and olcu1 <> '' " +
                                " and olcu2 is not null " +
                                " and olcu2 <> '' "

                    oSQL2.GetSQLReader()

                    If oSQL2.oReader.Read Then
                        cIstenen = oSQL2.SQLReadString("olcu1")
                        cOlculen = oSQL2.SQLReadString("olcu2")
                    End If
                    oSQL2.oReader.Close()

                    If IsNumeric(cIstenen) And IsNumeric(cOlculen) Then

                        lOK = True

                        nIstenen = StringToNumber(cIstenen)
                        nOlculen = StringToNumber(cOlculen)

                        If nOlculen - nIstenen = 0 Then
                            cSonuc = "OK"
                        Else
                            cSonuc = (Math.Round(nOlculen - nIstenen, 2)).ToString.Replace(",", ".")
                        End If

                        oSQL2.cSQLQuery = "update bedenolculeri " +
                                " set b" + (nCol + 1).ToString("00") + " = '" + cSonuc + "' " +
                                " where anahtar1 = '" + cFisNo + "' " +
                                " and olcuyeri = '" + cOlcuYeri + "' "

                        oSQL2.SQLExecute()
                    End If
                Next

                If Not lOK Then
                    oSQL2.cSQLQuery = "delete bedenolculeri " +
                                " where anahtar1 = '" + cFisNo + "' " +
                                " and olcuyeri = '" + cOlcuYeri + "' "

                    oSQL2.SQLExecute()
                End If
            Loop
            oSQL.oReader.Close()

            oSQL.CloseConn()
            oSQL2.CloseConn()

        Catch ex As Exception
            ErrDisp("OlcuTablosuYazdir", "utilSReports",,, ex)
        End Try
    End Sub

End Module
