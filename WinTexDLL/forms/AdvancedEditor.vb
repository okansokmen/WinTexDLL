Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraBars.Ribbon
Imports System.Reflection

Public Class AdvancedEditor

    Dim cFile As String

    Public Sub init(cFileName As String)

        cFile = cFileName

        RichEditControl1.Options.HorizontalRuler.Visibility = RichEditRulerVisibility.Auto
        RichEditControl1.Options.HorizontalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Auto
        RichEditControl1.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Auto
        RichEditControl1.Options.VerticalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Auto

        If My.Computer.FileSystem.FileExists(cFile) Then
            RichEditControl1.LoadDocument(cFile, DevExpress.XtraRichEdit.DocumentFormat.PlainText)
        End If

        Me.Show()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Me.Close()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        DestroyFile(cFile)
        RichEditControl1.SaveDocument(cFile, DevExpress.XtraRichEdit.DocumentFormat.PlainText)
        Me.Close()
    End Sub

    Private Sub AdvancedEditor_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
    End Sub

End Class