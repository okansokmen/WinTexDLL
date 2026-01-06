Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections
Imports UHFAPP.utils

Namespace UHFAPP
	Public Class ReadEPCFormData
		Public Shared filter_Data As String = ""
		Public Shared filter_Ptr As String = "32"
		Public Shared filter_len As String = "0"
		Public Shared filter_bank As Integer = 1
		Public Shared filter_save As Boolean = False
		Public Shared Total As String = "0"
		Public Shared selectedText As String = ""
		Public Shared Time As String = "0"
		Public Shared epcList As New List(Of EpcInfo)()
		Public Shared listviewdata As New List(Of ListViewItem)()
	End Class
End Namespace
