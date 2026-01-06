Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports System.Text

Namespace UHFAPP
	Public Class DataConvert
		''' <summary>
		''' 16进制字符串转字节数组 
		''' </summary>
		''' <param name="s"></param>
		''' <returns></returns>
		Public Shared Function HexStringToByteArray(ByVal s As String) As Byte()
			s = s.Replace(" ", "")

			If String.IsNullOrEmpty(s) Then
				Return Nothing
			End If

			Dim buffer((s.Length \ 2) - 1) As Byte
			Try
				For i As Integer = 0 To s.Length - 1 Step 2
					buffer(i \ 2) = CByte(Convert.ToByte(s.Substring(i, 2), 16))
				Next i
			Catch ex As System.Exception
			End Try
			Return buffer
		End Function
		''' <summary>
		''' 字节数组转换十六进制字符串
		''' </summary>
		''' <param name="data"></param>
		''' <returns></returns>
		Public Shared Function ByteArrayToHexString(ByVal data() As Byte) As String
			Try
				Dim sb As New StringBuilder(data.Length * 3)
				For Each b As Byte In data
					sb.Append(Convert.ToString(b, 16).PadLeft(2, "0"c).PadRight(3, " "c))
				Next b
				Return sb.ToString().ToUpper()
			Catch ex As System.Exception
				Return String.Empty
			End Try

		End Function
		''' <summary>
		''' 字节数组转换十六进制字符串
		''' </summary>
		''' <param name="data"></param>
		''' <returns></returns>
		Public Shared Function ByteArrayToHexString(ByVal data() As Byte, ByVal leng As Integer) As String
			Try
				Dim sb As New StringBuilder(data.Length * 3)
				For k As Integer = 0 To leng - 1
					sb.Append(Convert.ToString(data(k), 16).PadLeft(2, "0"c).PadRight(3, " "c))
				Next k
				Return sb.ToString().ToUpper()
			Catch ex As System.Exception
				Return String.Empty
			End Try

		End Function

		''' <summary>
		''' 十进制转十六进制
		''' </summary>
		''' <returns></returns>
		Public Shared Function DecimalToHexString(ByVal data As Integer) As String
		   Return Convert.ToString(data, 16) '69为被转值
		End Function

		''' <summary>
		''' 十六进制制转十进
		''' </summary>
		''' <returns></returns>
		Public Shared Function HexStringToDecimal(ByVal hex As String) As Integer
			Return Convert.ToInt32(hex, 16) '69为被转值
		End Function

	End Class
End Namespace
