Public Class frmGetString

    Dim cSonuc As String

    Public Function init(Optional cWindowsHeader As String = "", Optional cLabel As String = "") As String

        If cWindowsHeader.Trim <> "" Then
            Me.Text = cWindowsHeader.Trim
        End If

        If cLabel.Trim <> "" Then
            Label1.Text = cLabel.Trim
        End If

        Me.ShowDialog()

        init = cSonuc
    End Function

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        cSonuc = TextBox1.Text.Trim
        Me.Close()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        cSonuc = ""
        Me.Close()
    End Sub
End Class