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

<InteropForm()> Public Class DevXReportViewer

    Public Sub init()
        Try
            If Not DevXLoadReport() Then
                Me.Close()
                Exit Sub
            End If
            DevXConnectData()
            DevXLoadLayout()
            DocumentViewer1.DocumentSource = oReportDevX.oReportDeveloperExpress
            DocumentViewer1.Refresh()
            Me.Show()

        Catch ex As Exception
            ErrDisp("DevXReportViewer : " + Err.Description, Me.Name)
        End Try
    End Sub

    Private Sub DevXReportViewer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.WindowState = FormWindowState.Maximized

        Catch ex As Exception
            ' do not care
        End Try
    End Sub
End Class