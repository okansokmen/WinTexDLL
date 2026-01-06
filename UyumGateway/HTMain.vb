Public Class HTMain
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = My.Resources.wintex
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Application.Exit()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Dim oForm As UyumAktar1

        oForm = New UyumAktar1
        oForm.Show()
    End Sub

    Private Sub HTMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Uyumsoft Aktarım Programı V " + My.Application.Info.Version.ToString.Trim
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Dim oForm As UyumAktarWebServis

        oForm = New UyumAktarWebServis
        oForm.Show()
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick

        Dim cPath As String = Application.ExecutablePath

        Try
            System.Diagnostics.Process.Start("notepad.exe", cPath + ".config")

        Catch ex As Exception
            ErrDisp(ex, Me.Name)
        End Try
    End Sub

    Private Sub BarButtonItem5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick

        Dim cPath As String = ""

        Try
            cPath = GetErrFilename()
            System.Diagnostics.Process.Start("notepad.exe", cPath)

        Catch ex As Exception
            ErrDisp(ex, Me.Name)
        End Try
    End Sub
End Class