Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections
Imports System.Reflection

Namespace UHFAPP
	Public Class Common
		Public Shared isEnglish As Boolean = False
		Public Shared tag As String = ""

		Public Shared time As Integer = 2000

		Private Shared hash As New Hashtable()

		Private Shared dictionary As New Dictionary(Of String, Hashtable)()

		Public Shared Function GetForm(ByVal FormName As String, ByVal mainForm As Form) As Form
			If hash.Contains(FormName) Then
				Dim frm As Form = DirectCast(hash(FormName), Form)
				Return frm
			Else

				Dim form As Form = DirectCast(System.Reflection.Assembly.Load("UHFAPP").CreateInstance("UHFAPP." & FormName), Form) '注意: 窗体命  名空间后面一定要加一个点
				'form.MdiParent = mainForm; //如果是Mdi窗体的话请加这一行
				hash.Add(FormName, form)
				'form.Show();
				Return form
			End If

		End Function

		Public Shared Sub SaveForm(ByVal form As Form)
			If hash.Contains(form.Name) Then
				hash(form.Name) = form
			Else
				hash.Add(form.Name, form)
			End If
		End Sub
		Public Shared Function GetForm(ByVal FormName As String) As Form
			If hash.Contains(FormName) Then
				Return DirectCast(hash(FormName), Form)
			Else
				Return Nothing
			End If
		End Function
		Public Shared Sub SaveControlValues(ByVal FormName As Form)
			Dim sonControls As System.Windows.Forms.Control.ControlCollection = FormName.Controls
			Dim hash As New Hashtable()

			GetControlValues(hash, sonControls)

			If dictionary.ContainsKey(FormName.Name) Then
				dictionary(FormName.Name) = hash
			Else
				dictionary.Add(FormName.Name, hash)
			End If
		End Sub
		Private Shared Sub GetControlValues(ByVal hash As Hashtable, ByVal sonControls As System.Windows.Forms.Control.ControlCollection)
			For Each control As Control In sonControls
				If TypeOf control Is TextBox Then
					hash.Add(control.Name, control.Text)
				ElseIf TypeOf control Is RadioButton Then
					hash.Add(control.Name, CType(control, RadioButton).Checked)
				ElseIf TypeOf control Is ComboBox Then
					hash.Add(control.Name, CType(control, ComboBox).SelectedIndex)
				ElseIf TypeOf control Is CheckBox Then
					hash.Add(control.Name, CType(control, CheckBox).Checked)
				ElseIf TypeOf control Is Panel Then
					GetControlValues(hash, control.Controls)
				ElseIf TypeOf control Is GroupBox Then
					GetControlValues(hash, control.Controls)
				End If
			Next control
		End Sub

		Public Shared Function GetControlValues(ByVal FormName As String) As Hashtable
			If dictionary.ContainsKey(FormName) Then
				Return CType(dictionary(FormName), Hashtable)
			Else
				Return Nothing
			End If
		End Function

	End Class


End Namespace
