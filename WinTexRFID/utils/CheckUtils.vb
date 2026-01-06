Imports BLEDeviceAPI
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports UHFAPP.utils
Imports System.Net
Imports UHFAPP.Entity
Namespace UHFAPP
	Public Class CheckUtils

		Public Shared Function getInsertIndex(ByVal listIp As List(Of ReaderDeviceInfo), ByVal info As ReaderDeviceInfo, ByVal exists() As Boolean) As Integer

			Dim startIndex As Integer = 0
			Dim endIndex As Integer = listIp.Count
			Dim judgeIndex As Integer
			Dim ret As Integer
			If endIndex = 0 Then
				exists(0) = False
				Return 0
			End If

			Dim ipAndMac() As Byte = info.GetIpAndMac() 'ip.Split('.'); //System.Text.ASCIIEncoding.ASCII.GetBytes(ip.Replace(".",""));
			endIndex -= 1
			Do
				judgeIndex = (startIndex + endIndex) \ 2
				Dim temp() As Byte = listIp(judgeIndex).GetIpAndMac() ' System.Text.ASCIIEncoding.ASCII.GetBytes(listIp[judgeIndex].Replace(".", ""));
				ret = compareBytes(ipAndMac, temp)
				If ret > 0 Then
					If judgeIndex = endIndex Then
						exists(0) = False
						Return judgeIndex + 1
					End If
					startIndex = judgeIndex + 1
				ElseIf ret < 0 Then
					If judgeIndex = startIndex Then
						exists(0) = False
						Return judgeIndex
					End If
					endIndex = judgeIndex - 1
				Else
					exists(0) = True
					Return judgeIndex
				End If
			Loop

		End Function


		Public Shared Function getInsertIndex(ByVal listEpc As List(Of EpcInfo), ByVal Epc As String, ByVal tid As String, ByVal exists() As Boolean) As Integer

			Dim startIndex As Integer = 0
			Dim endIndex As Integer = listEpc.Count
			Dim judgeIndex As Integer
			Dim ret As Integer
			If endIndex = 0 Then
				exists(0) = False
				Return 0
			End If
			endIndex -= 1
			'----------�ϲ�EPC��TID----------------
			Dim tidLen As Integer = If(String.IsNullOrEmpty(tid), 0, tid.Length \ 2)
			Dim EpcAndTidBytes((Epc.Length \ 2 + tidLen) - 1) As Byte
			Dim epcBytes() As Byte = DataConvert.HexStringToByteArray(Epc)
			Dim k As Integer = 0
			Do While epcBytes IsNot Nothing AndAlso k < epcBytes.Length
				EpcAndTidBytes(k) = epcBytes(k)
				k += 1
			Loop
			If tidLen > 0 Then

				Dim tidBytes() As Byte = DataConvert.HexStringToByteArray(tid)
				For k = 0 To tidLen - 1
					If epcBytes IsNot Nothing Then
						EpcAndTidBytes(k + epcBytes.Length) = tidBytes(k)
					Else
						EpcAndTidBytes(k) = tidBytes(k)
					End If

				Next k
			End If


			Do
				judgeIndex = (startIndex + endIndex) \ 2
				If EpcAndTidBytes Is Nothing AndAlso listEpc(judgeIndex).EpcAndTidBytes Is Nothing Then
					exists(0) = True
					Return judgeIndex
				End If
				ret = compareBytes(EpcAndTidBytes, listEpc(judgeIndex).EpcAndTidBytes)
				If ret > 0 Then
					If judgeIndex = endIndex Then
						exists(0) = False
						Return judgeIndex + 1
					End If
					startIndex = judgeIndex + 1
				ElseIf ret < 0 Then
					If judgeIndex = startIndex Then
						exists(0) = False
						Return judgeIndex
					End If
					endIndex = judgeIndex - 1
				Else
					exists(0) = True
					Return judgeIndex
				End If
			Loop

		End Function

		'return 1,2 b1>b2
		'return -1,-2 b1<b2
		'retrurn 0;b1 == b2
		Private Shared Function compareBytes(ByVal b1() As Byte, ByVal b2() As Byte) As Integer
			Dim len As Integer = If(b1.Length < b2.Length, b1.Length, b2.Length)
			Dim value1 As Integer
			Dim value2 As Integer
			For i As Integer = 0 To len - 1
				value1 = b1(i) And &HFF
				value2 = b2(i) And &HFF
				If value1 > value2 Then
					Return 1
				ElseIf value1 < value2 Then
					Return -1
				End If
			Next i
			If b1.Length > b2.Length Then
				Return 2
			ElseIf b1.Length < b2.Length Then
				Return -2
			Else
				Return 0
			End If
		End Function
	End Class
End Namespace



