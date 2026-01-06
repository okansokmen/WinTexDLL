Option Explicit On
Option Strict On

Imports Stimulsoft.Report
Imports Stimulsoft.Report.Dictionary

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.IO

Imports Microsoft.SqlServer.Server
Imports Microsoft.InteropFormTools

<InteropForm()> Public Class ReportViewer

    Private Structure oStimulusReport
        Dim cReportID As String
        Dim cReportName As String
        Dim cReportClass As String
        Dim cReportSQL As String
        Dim cReport As String
        Dim cPrinterName As String
        Dim nCopies As Double
        Dim oReportStimulus As Stimulsoft.Report.StiReport
        Dim cDigerDiller As String
    End Structure

    Dim oReport As oStimulusReport

    Public Sub initViewer(Optional cReportID As String = "", Optional cReportVariable1 As String = "", Optional cReportVariable2 As String = "", Optional cReportVariable3 As String = "",
                          Optional cReportVariable4 As String = "", Optional cReportVariable5 As String = "", Optional cReportVariable6 As String = "", Optional cReportVariable7 As String = "",
                          Optional cReportVariable8 As String = "", Optional cReportVariable9 As String = "", Optional cReportVariable10 As String = "")

        Try
            oReport.cReportID = cReportID

            If Not StiLoadReport() Then
                Me.Close()
                Exit Sub
            End If

            StiRibbonViewerControl1.Report = oReport.oReportStimulus

            oReport.oReportStimulus.Compile()
            If Not (oReport.oReportStimulus.Item("cVariable1") Is Nothing) Then oReport.oReportStimulus.Item("cVariable1") = cReportVariable1.Trim
            If Not (oReport.oReportStimulus.Item("cVariable2") Is Nothing) Then oReport.oReportStimulus.Item("cVariable2") = cReportVariable2.Trim
            If Not (oReport.oReportStimulus.Item("cVariable3") Is Nothing) Then oReport.oReportStimulus.Item("cVariable3") = cReportVariable3.Trim
            If Not (oReport.oReportStimulus.Item("cVariable4") Is Nothing) Then oReport.oReportStimulus.Item("cVariable4") = cReportVariable4.Trim
            If Not (oReport.oReportStimulus.Item("cVariable5") Is Nothing) Then oReport.oReportStimulus.Item("cVariable5") = cReportVariable5.Trim
            If Not (oReport.oReportStimulus.Item("cVariable6") Is Nothing) Then oReport.oReportStimulus.Item("cVariable6") = cReportVariable6.Trim
            If Not (oReport.oReportStimulus.Item("cVariable7") Is Nothing) Then oReport.oReportStimulus.Item("cVariable7") = cReportVariable7.Trim
            If Not (oReport.oReportStimulus.Item("cVariable8") Is Nothing) Then oReport.oReportStimulus.Item("cVariable8") = cReportVariable8.Trim
            If Not (oReport.oReportStimulus.Item("cVariable9") Is Nothing) Then oReport.oReportStimulus.Item("cVariable9") = cReportVariable9.Trim
            If Not (oReport.oReportStimulus.Item("cVariable10") Is Nothing) Then oReport.oReportStimulus.Item("cVariable10") = cReportVariable10.Trim
            oReport.oReportStimulus.Render()

            'oReport.RenderWithWpf()
            'oReport.ShowWithWpfRibbonGUI()
            'StiRibbonViewerControl1.InvokeFullScreen()

            Me.Show()

        Catch ex As Exception
            ErrDisp("ReportViewer InitViewer : ", Me.Name)
        End Try
    End Sub

    Private Sub ReportViewer_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized

        Catch ex As Exception
            ErrDisp("ReportViewer_Load", Me.Name)
        End Try
    End Sub

    Private Sub StiRibbonViewerControl1_Close(sender As Object, e As EventArgs) Handles StiRibbonViewerControl1.Close
        Try
            Me.Close()

        Catch ex As Exception
            ErrDisp("StiRibbonViewerControl1_Close", Me.Name)
        End Try
    End Sub

    Private Function StiLoadReport() As Boolean

        Dim cSQL As String = ""
        Dim ConnYage As SqlConnection
        Dim dr As SqlDataReader
        Dim oDataBase As StiSqlDatabase
        Dim nRefreshTime As Integer = 0

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
                oReport.cDigerDiller = SQLReadString(dr, "digerdiller")
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
            oDataBase = New StiSqlDatabase()
            oDataBase.Name = "WinTex" ' oConnection.cDatabase
            oDataBase.Alias = "WinTex" ' oConnection.cDatabase
            oDataBase.ConnectionString = oConnection.cConnStr
            oReport.oReportStimulus.Dictionary.Databases.Add(oDataBase)

            nRefreshTime = GetReportRefreshTime()
            If nRefreshTime <> 0 Then
                oReport.oReportStimulus.RefreshTime = nRefreshTime * 60
            End If

            StiLoadReport = True

        Catch ex As Exception
            ErrDisp("StiLoadReport : " + ex.Message.Trim, "utilSReports", cSQL,, ex)
        End Try
    End Function

End Class