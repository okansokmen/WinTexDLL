Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace UHFAPP
	Partial Public Class BaseForm
		Inherits Form

		Public uhf As UHFAPI = Nothing

		Public Sub New()
			InitializeComponent()
			uhf = UHFAPI.getInstance()

		End Sub

	End Class
End Namespace
