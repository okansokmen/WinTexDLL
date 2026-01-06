Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace UHFAPP.usb.hid_new
	Friend Class Debug
		Public Shared isDebug As Boolean = False

		Public Shared Sub PrintLog(ByVal msg As String)
			If isDebug Then
				Console.WriteLine("msg=" & msg)
				FileManage.WriterLog(FileManage.LogType.Debug, DateTime.Now & " " & msg & vbCrLf)
			End If
		End Sub
		Public Shared Sub PrintLog(ByVal tag As String, ByVal msg As String)
			If isDebug Then
				Console.WriteLine(tag & "=" & msg)
				FileManage.WriterLog(FileManage.LogType.Debug, DateTime.Now & " " & tag & " " & msg & vbCrLf)
			End If
		End Sub
		Public Shared Sub PrintLog(ByVal tag As String, ByVal msg() As Byte)
			If isDebug Then
				Dim sb As New StringBuilder()
				For k As Integer = 0 To msg.Length - 1
					sb.Append(String.Format("{0:X2} ", msg(k)))
				Next k
				Console.WriteLine(tag & "=" & sb.ToString())
				FileManage.WriterLog(FileManage.LogType.Debug, DateTime.Now & " " & tag & " " & sb.ToString() & vbCrLf)
			End If
		End Sub
	End Class
End Namespace
