Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace UHFAPP.Entity
	Public Class ReaderDeviceInfo
		Public mac As String
		Public ip As String
		Public port As Integer
		Public macBytes() As Byte
		Public ipBytes() As Byte
		Public lastTime As Integer = 0
		Public Sub New(ByVal macBytes() As Byte, ByVal ipBytes() As Byte, ByVal port As Integer)
			Me.macBytes = macBytes
			Me.ipBytes = ipBytes
			Me.port = port
			Me.lastTime = Environment.TickCount

			Dim macSB As New StringBuilder()
			macSB.Append(String.Format("{0:X2}", macBytes(0))) ' Convert.ToString(macBytes[0], 16)
			macSB.Append(":")
			macSB.Append(String.Format("{0:X2}", macBytes(1)))
			macSB.Append(":")
			macSB.Append(String.Format("{0:X2}", macBytes(2)))
			macSB.Append(":")
			macSB.Append(String.Format("{0:X2}", macBytes(3)))
			macSB.Append(":")
			macSB.Append(String.Format("{0:X2}", macBytes(4)))
			macSB.Append(":")
			macSB.Append(String.Format("{0:X2}", macBytes(5)))
			mac = macSB.ToString()

			Dim macIP As New StringBuilder()
			macIP.Append(ipBytes(0))
			macIP.Append(":")
			macIP.Append(ipBytes(1))
			macIP.Append(":")
			macIP.Append(ipBytes(2))
			macIP.Append(":")
			macIP.Append(ipBytes(3))
			ip = macIP.ToString()

		End Sub

		Public Function GetIpAndMac() As Byte()
			If String.IsNullOrEmpty(mac) OrElse String.IsNullOrEmpty(ip) Then
				Return Nothing
			End If

			Dim data(9) As Byte
			For k As Integer = 0 To 3
				data(k) = ipBytes(k)
			Next k
			For k As Integer = 0 To 5
				data(4 + k) = macBytes(k)
			Next k

			Return data
		End Function
	End Class
End Namespace
