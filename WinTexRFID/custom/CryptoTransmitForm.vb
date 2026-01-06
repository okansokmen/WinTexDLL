Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports BLEDeviceAPI

Namespace UHFAPP.custom
	Partial Public Class CryptoTransmitForm
		Inherits Form

		'crypto module transmit
		'pin--point of data which send to crypto mudle 
		'inLen--the length of send data 
		'pout--point of receive crypto returned data
		'outLen--the length of received data
		'wait_recv_ms--the maxinum millsecond while waiting for crypto module return 

	   <DllImport("UHFAPI.dll", CallingConvention := CallingConvention.Cdecl)>
	   Private Shared Function CryptoTransmit(ByVal pin() As Byte, ByVal inLen As Short, ByVal pout() As Byte, ByVal outLen() As Byte, ByVal wait_recv_ms As Short) As Integer
	   End Function


		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub btnSend_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSend.Click
			If Not Me.btnSend.IsHandleCreated Then Return

			Dim pin() As Byte = DataConvert.HexStringToByteArray(txtSendData.Text)
			If pin IsNot Nothing AndAlso pin.Length > 0 Then
				Dim pout(511) As Byte
				Dim outLen(0) As Byte
				Dim wait_recv_ms As Short = 1000
				Dim result As Integer = CryptoTransmit(pin, CShort(pin.Length), pout, outLen, wait_recv_ms)
				If result = 0 Then
					Dim outData() As Byte = BLEDeviceAPI.Utils.CopyArray(pout, outLen(0))
					textBox2.Text = DataConvert.ByteArrayToHexString(outData)
				Else
					MessageBox.Show("失败!")
				End If
			Else
				MessageBox.Show("输入数据不能为空!")
			End If


		End Sub


		Private isDelete As Boolean = False
		Private Sub ReadWriteTagForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
			If e.KeyCode = Keys.Back Then
				isDelete = True
			Else
				isDelete = False
			End If

		End Sub

		Private Sub txtSendData_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSendData.TextChanged
			If Not Me.txtSendData.IsHandleCreated Then Return

			FormatHex(txtSendData)
			Dim data As String = txtSendData.Text.Replace(" ", "")
			If data.Length > 0 Then
				label3.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				label3.Text = "0"
			End If
		End Sub


		Private Sub FormatHex(ByVal txt As TextBox)
			If isDelete Then
				Return
			End If
			Dim data As String = txt.Text.Trim().Replace(" ", "")
			If data <> String.Empty Then
				Dim SelectIndex As Integer = txt.SelectionStart - 1
				Dim charData() As Char = data.ToCharArray(0, data.Length)
				Dim newChar(charData.Length - 1) As Char
				Dim index As Integer = 0
				For k As Integer = 0 To charData.Length - 1
					If StringUtils.IsHexNumber2(charData(k)) Then
						newChar(index) = charData(k)
						index += 1
					End If
				Next k
				Dim newData As New String(newChar, 0, index)
				Dim sb As New StringBuilder()
				Dim count As Integer = (newData.Length \ 2) + (newData.Length Mod 2)

				For k As Integer = 0 To count - 1
					If (k * 2 + 2) <= newData.Length Then
						sb.Append(newData.Substring(k * 2, 2))
					Else
						sb.Append(newData.Substring(k * 2, 1))
					End If
					sb.Append(" ")
				Next k
				txt.Text = sb.ToString()

				If SelectIndex >= 0 Then
					txt.SelectionStart = SelectIndex + 2
				End If
				'txt.Select(txt.Text.Length - 1, 0);

			End If
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			If Not Me.button1.IsHandleCreated Then Return

			textBox2.Text = ""
		End Sub
	End Class
End Namespace
