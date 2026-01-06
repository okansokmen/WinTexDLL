Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace UHFAPP.custom
	Partial Public Class R5ModifyParametersForm
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub
		Public Sub New(ByVal name As String, ByVal value As String, ByVal specification As String)
			InitializeComponent()

			txtName.Text = name
			txtValue.Text = value
			textBox3.Text = specification
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			If Not Me.button1.IsHandleCreated Then Return

			DialogResult = System.Windows.Forms.DialogResult.OK
			name_Conflict = txtName.Text
			value = txtValue.Text
			specification= textBox3.Text
			Me.Close()

		End Sub

'INSTANT VB NOTE: The field name was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Public name_Conflict As String
		Public value As String
		Public specification As String
	End Class
End Namespace
