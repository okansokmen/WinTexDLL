Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace UHFAPP.multidevice
	Partial Public Class DeviceListForm
		Inherits Form

		Public Sub New(ByVal list As List(Of DeviceInfo))
			InitializeComponent()
			For k As Integer = 0 To list.Count - 1
				comboBox1.Items.Add(list(k).Ip)
			Next k

		End Sub


		Public ip As String

		Private Sub comboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles comboBox1.SelectedIndexChanged
			If Not Me.comboBox1.IsHandleCreated Then Return

			ip = comboBox1.Items(comboBox1.SelectedIndex).ToString()
			Me.Close()
		End Sub
	End Class
End Namespace
