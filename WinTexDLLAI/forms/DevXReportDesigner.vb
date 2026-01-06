Imports System
Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient

Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraCharts
Imports DevExpress.XtraCharts.Wizard
Imports DevExpress.Data.PivotGrid
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.UserDesigner

Imports Microsoft.InteropFormTools
Imports Microsoft.VisualBasic

<InteropForm()> Public Class DevXReportDesigner

    Dim cViewName As String = ""

    Public Sub initNewReport()
        Try
            oReportDevX.cReportID = GetFisNo("reportid")
            DevXConnectData(True)
            ReportDesigner1.OpenReport(oReportDevX.oReportDeveloperExpress)
            Me.Show()
        Catch ex As Exception
            ErrDisp("initNewReport", "DevXReportDesigner")
        End Try
    End Sub

    Public Sub init()
        Try
            If Not DevXLoadReport() Then
                Me.Close()
                Exit Sub
            End If
            DevXConnectData()
            DevXLoadLayout()
            ReportDesigner1.OpenReport(oReportDevX.oReportDeveloperExpress)
            Me.ShowDialog()
        Catch ex As Exception
            ErrDisp("init", "DevXReportDesigner")
        End Try
    End Sub

    Private Sub DevXReportDesigner_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ' Enables form skins in the application (if required).
            DevExpress.Skins.SkinManager.EnableFormSkins()
            DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged()
            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            ErrDisp("DevXReportDesigner_Load", "DevXReportDesigner")
        End Try
    End Sub

    Public Sub New()
        Try
            InitializeComponent()
            AddHandler ReportDesigner1.DesignPanelLoaded, AddressOf DevXDesignPanelLoaded

        Catch ex As Exception
            ErrDisp("New", "DevXReportDesigner")
        End Try
    End Sub

    Private Sub DevXDesignPanelLoaded(ByVal sender As Object, ByVal e As DesignerLoadedEventArgs)
        Try
            Dim panel As XRDesignPanel = CType(sender, XRDesignPanel)
            ReportDesigner1.AddCommandHandler(New SaveCommandHandler(panel))
        Catch ex As Exception
            ErrDisp("DevXDesignPanelLoaded", "DevXReportDesigner")
        End Try
    End Sub

    Public Class SaveCommandHandler
        Implements DevExpress.XtraReports.UserDesigner.ICommandHandler
        Private panel As XRDesignPanel

        Public Sub New(ByVal panel As XRDesignPanel)
            Try
                Me.panel = panel
            Catch ex As Exception
                ErrDisp("New", "DevXReportDesigner")
            End Try
        End Sub

        Public Sub HandleCommand(ByVal command As DevExpress.XtraReports.UserDesigner.ReportCommand, ByVal args() As Object) Implements DevExpress.XtraReports.UserDesigner.ICommandHandler.HandleCommand
            ' Save the report.
            Try
                Save()
            Catch ex As Exception
                ErrDisp("HandleCommand", "DevXReportDesigner")
            End Try
        End Sub

        Public Function CanHandleCommand(ByVal command As DevExpress.XtraReports.UserDesigner.ReportCommand, ByRef useNextHandler As Boolean) As Boolean Implements DevExpress.XtraReports.UserDesigner.ICommandHandler.CanHandleCommand

            CanHandleCommand = False

            Try
                useNextHandler = Not (command = ReportCommand.SaveFile OrElse command = ReportCommand.SaveFileAs OrElse command = ReportCommand.Closing)
                Return Not useNextHandler
            Catch ex As Exception
                ErrDisp("CanHandleCommand", "DevXReportDesigner")
            End Try
        End Function

        Private Sub Save()
            Try
                Dim cSQL As String = ""
                Dim cReportName As String = ""
                Dim cReportClass As String = ""
                Dim cReport As String
                Dim cReportID As String
                Dim cTempFile As String

                If oReportDevX.cReportName.Trim = "" Then
                    cReportName = InputBox("Rapor Adı", "Kaydetmeyi iptal etmek için BOŞ bırakınız", oReportDevX.cReportName)
                Else
                    cReportName = oReportDevX.cReportName
                End If
                If cReportName.Trim = "" Then Exit Sub

                If oReportDevX.cReportClass.Trim = "" Then
                    cReportClass = InputBox("Rapor Sınıfı", "Kaydetmeyi iptal etmek için BOŞ bırakınız", oReportDevX.cReportClass)
                Else
                    cReportClass = oReportDevX.cReportClass
                End If
                If cReportClass.Trim = "" Then Exit Sub

                If oReportDevX.cReportID.Trim = "" Then
                    cReportID = GetFisNo("reportid")
                Else
                    cReportID = oReportDevX.cReportID
                End If

                cTempFile = GetTempFile("xml")
                panel.Report.SaveLayoutToXml(cTempFile)
                cReport = My.Computer.FileSystem.ReadAllText(cTempFile)
                cReport = Replace(cReport, "'", "||")
                DestroyFile(cTempFile)

                oReportDevX.cReportID = cReportID
                oReportDevX.cReportName = cReportName
                oReportDevX.cReportClass = cReportClass
                oReportDevX.cReport = cReport

                cSQL = "delete devxreports " +
                        " where reportid = " + cReportID

                ExecuteSQLCommand(cSQL)

                cSQL = "insert into devxreports (reportid, reportname, report, reportviewname, reportclass)  " +
                        " values (" + oReportDevX.cReportID + ", " +
                        " '" + SQLWriteString(oReportDevX.cReportName) + "', " +
                        " '" + SQLWriteString(oReportDevX.cReport) + "', " +
                        " '" + SQLWriteString(oReportDevX.cReportViewName) + "', " +
                        " '" + SQLWriteString(oReportDevX.cReportClass) + "') "

                ExecuteSQLCommand(cSQL)

                MsgBox(oReportDevX.cReportName + " " + oReportDevX.cReportClass + vbCrLf + "Report saved")

                ' Prevent the "Report has been changed" dialog from being shown.
                panel.ReportState = ReportState.Saved

            Catch ex As Exception
                ErrDisp("Save", "DevXReportDesigner")
            End Try
        End Sub
    End Class

End Class
