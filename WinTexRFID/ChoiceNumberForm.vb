Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace UHFAPP
	Partial Public Class ChoiceNumberForm
		Inherits Form

		Public number As Integer = 0
		Public Sub New()
			InitializeComponent()
			comboBox1.SelectedIndex = 0

			If Common.isEnglish Then

				button1.Text = "OK"
				label1.Text = "Number:"

			End If
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			If Not Me.button1.IsHandleCreated Then Return

			number = Integer.Parse(comboBox1.Text) 'comboBox1.SelectedIndex;
			Me.Close()
		End Sub

		Private Sub comboBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles comboBox1.TextChanged
			If Not Me.comboBox1.IsHandleCreated Then Return

			Dim txt As ComboBox = DirectCast(sender, ComboBox)
			If Not StringUtils.IsNumber(txt.Text) Then
				txt.Text = "0"
			Else
				If Integer.Parse(txt.Text) > 15 Then

					txt.Text = "15"
				End If

			End If

		End Sub
	End Class
End Namespace
