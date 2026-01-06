Option Explicit On
Option Strict On

Public Class frmEliar
    Private Sub frmEliar_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            TextEdit1.Text = oSQL.GetSysPar("eliarserver")
            TextEdit2.Text = oSQL.GetSysPar("eliardatabase")
            TextEdit3.Text = oSQL.GetSysPar("eliarusername")
            TextEdit4.Text = oSQL.GetSysPar("eliarpassword")

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("frmEliar_Load", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        ' save & exit
        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.SetSysPar("eliarserver", TextEdit1.Text.Trim)
            oSQL.SetSysPar("eliardatabase", TextEdit2.Text.Trim)
            oSQL.SetSysPar("eliarusername", TextEdit3.Text.Trim)
            oSQL.SetSysPar("eliarpassword", TextEdit4.Text.Trim)

            oSQL.CloseConn()

            Me.Close()

        Catch ex As Exception
            ErrDisp("SimpleButton1_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub
End Class