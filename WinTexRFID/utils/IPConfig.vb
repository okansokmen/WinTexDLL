Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace UHFAPP
	Public Class IPConfig
		Private Shared path As String = Environment.CurrentDirectory &"\ipConfig.txt"

		Public Shared Sub setIPConfig(ByVal ipEntity As IPEntity)
			Try
				Dim sb As New StringBuilder()
				sb.Append("ip=")
				sb.Append(ipEntity.Ip(0))
				sb.Append(".")
				sb.Append(ipEntity.Ip(1))
				sb.Append(".")
				sb.Append(ipEntity.Ip(2))
				sb.Append(".")
				sb.Append(ipEntity.Ip(3))
				sb.Append(vbCrLf)
				sb.Append("port=")
				sb.Append(ipEntity.Port)
				FileManage.WriterFile(path, sb.ToString(), False)
			Catch ex As Exception

			End Try
		End Sub
		Public Shared Function getIPConfig() As IPEntity
			Try
				Dim data As String = FileManage.ReadFile(path)
				If data = "" Then
					Return Nothing
				End If

				Dim ip() As String = data.Split(ControlChars.Lf)(0).Replace("ip=", "").Replace(" ", "").Replace(vbCr, "").Split("."c)
				If ip.Length <> 4 Then
					Return Nothing
				End If

				Dim port As Integer = Integer.Parse(data.Split(ControlChars.Lf)(1).Replace("port=", "").Replace(vbCr, "").Replace(" ", ""))
				Dim ipEntity As New IPEntity()
				ipEntity.Ip = ip
				ipEntity.Port = port
				Return ipEntity
			Catch ex As Exception

			End Try

			Return Nothing
		End Function


		Public Class IPEntity
'INSTANT VB NOTE: The field ip was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
			Private ip_Conflict() As String

'INSTANT VB NOTE: The field strIp was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
			Private strIp_Conflict As String

			Public Property StrIp As String
				Get
					Return strIp_Conflict
				End Get
				Set(ByVal value As String)
					strIp_Conflict = value
				End Set
			End Property

			Public Property Ip As String()
				Get
					Return ip_Conflict
				End Get
				Set(ByVal value As String())
					ip_Conflict = value
				End Set
			End Property
'INSTANT VB NOTE: The field port was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
			Private port_Conflict As Integer

			Public Property Port As Integer
				Get
					Return port_Conflict
				End Get
				Set(ByVal value As Integer)
					port_Conflict = value
				End Set
			End Property

		End Class
	End Class
End Namespace
