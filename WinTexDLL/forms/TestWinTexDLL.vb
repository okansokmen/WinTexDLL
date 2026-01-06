Option Explicit On
Option Strict On

Imports Microsoft.InteropFormTools

<InteropForm()> Public Class TestWinTexDLL

    Dim oHTMain As HTMain

    Public Sub init(oForm As HTMain)
        oHTMain = oForm
        lGlobalDebugMode = True
        Me.Show()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        oHTMain.TakePicture(-1)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        oHTMain.ReportDesigner()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        oHTMain.ReportToViewer()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        oHTMain.StiReportsBackup()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        oHTMain.SuperEditor("")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        oHTMain.DevXPDFViewer()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        oHTMain.DevXSpreadSheet()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        oHTMain.SendGMail("okansokmen@gmail.com")
    End Sub

    Private Sub TestWinTexDLL_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        lGlobalDebugMode = False
    End Sub
End Class