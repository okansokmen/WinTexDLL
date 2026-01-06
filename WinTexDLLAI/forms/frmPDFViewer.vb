Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports DevExpress.Pdf
Imports DevExpress.XtraPdfViewer
Imports System.Data.SqlClient

Public Class frmPDFViewer

    Dim cPDFFileName As String

    Public Sub init(Optional cFileName As String = "")

        cPDFFileName = cFileName.Trim
        Me.Show()
    End Sub

    Private Sub frmPDFViewer_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If cPDFFileName.Trim <> "" Then
            Me.PdfViewer1.LoadDocument(cPDFFileName)
        End If
        Me.WindowState = FormWindowState.Maximized

    End Sub
End Class