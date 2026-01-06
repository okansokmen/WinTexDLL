Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions

Namespace UHFAPP
	Public Class StringUtils
'''        
'''         * 判断是否是十六进制
'''         *
'''         * @param str
'''         * @return
'''         
		Public Shared Function IsHexNumber(ByVal str As String) As Boolean
			If String.IsNullOrEmpty(str) Then
				Return False
			End If
			str = str.Trim()
			If str.Length = 0 OrElse (str.Length Mod 2 <> 0) Then
				Return False
			End If
			If Regex.IsMatch(str, "^[0-9A-Fa-f]+$") Then
				Return True
			End If
			Return False
		End Function
		Public Shared Function IsHexNumber2(ByVal strChar As Char) As Boolean
			If strChar = "0"c OrElse strChar = "1"c OrElse strChar = "2"c OrElse strChar = "3"c OrElse strChar = "4"c OrElse strChar = "5"c OrElse strChar = "6"c OrElse strChar = "7"c OrElse strChar = "8"c OrElse strChar = "9"c OrElse strChar = "a"c OrElse strChar = "b"c OrElse strChar = "c"c OrElse strChar = "d"c OrElse strChar = "e"c OrElse strChar = "f"c OrElse strChar = "A"c OrElse strChar = "B"c OrElse strChar = "C"c OrElse strChar = "D"c OrElse strChar = "E"c OrElse strChar = "F"c Then
				Return True
			End If

			Return False
		End Function
		''' <summary>
		''' 检测是不是数字
		''' </summary>
		''' <param name="strNumber"></param>
		''' <returns></returns>
		Public Shared Function IsNumber(ByVal strNumber As String) As Boolean
			If String.IsNullOrEmpty(strNumber) Then
				Return False
			End If
			Dim regex As New Regex("^\d+(\.\d)?$")
			Return regex.IsMatch(strNumber)

		End Function

		Public Shared Function isIP(ByVal ip As String) As Boolean
			If ip Is Nothing OrElse ip.Length = 0 Then
				Return False
			End If

			Dim flag0 As Integer = 0
			Dim flag1 As Integer = 0
			Dim flag2 As Integer = 0 '统计点好出现的次数
			Dim cIP() As Char = ip.ToCharArray()
			For k As Integer = 0 To cIP.Length - 1
				If cIP(0) = "."c OrElse cIP(cIP.Length - 1) = "."c Then
					Return False
				End If

				If cIP(k) <> "0"c AndAlso cIP(k) <> "1"c AndAlso cIP(k) <> "2"c AndAlso cIP(k) <> "3"c AndAlso cIP(k) <> "4"c AndAlso cIP(k) <> "5"c AndAlso cIP(k) <> "6"c AndAlso cIP(k) <> "7"c AndAlso cIP(k) <> "8"c AndAlso cIP(k) <> "9"c AndAlso cIP(k) <> "."c Then
					Return False
				End If

				If cIP(k) = "."c Then
					flag1 = flag1 + 1
					flag2 = flag2 + 1
					If flag1 > 1 Then
						Return False
					End If
					If flag2 > 3 Then
						Return False
					End If
					flag0 = 0
				Else
					flag0 = flag0 + 1

					If flag0 > 3 Then
						Return False
					End If
					flag1 = 0
				End If
			Next k
			Return True
		End Function

	End Class
End Namespace
