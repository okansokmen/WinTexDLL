Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports BLEDeviceAPI

Namespace UHFAPP.custom.m775Authenticate
	Partial Public Class M775AuthenticateForm
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub
		Private api As New M775AuthenticateAPI()

		Private Sub btnAuthenticate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAuthenticate.Click
			If Not Me.btnAuthenticate.IsHandleCreated Then Return

			If Not DetectionFiltration() Then
				Return
			End If
			 txtAuthenticateData.Text = "00000000000000000000"

			If txtAuthenticateData.Text.Replace(" ", "").Length <> 20 Then
				MessageBox.Show("The length of the message must be 10 bytes!")
				Return
			End If

			Dim filterBank As Byte = 1
			If rbTID.Checked Then
				filterBank = 2
			ElseIf rbUser.Checked Then
				filterBank = 3
			End If
			Dim filterAddr As Integer = Integer.Parse(txtPtr.Text)
			Dim filterDataLen As Integer = Integer.Parse(txtLen.Text)
			Dim filterData() As Byte = DataConvert.HexStringToByteArray(txtFilter_EPC.Text)
			Dim keyID As Byte = 0
			Dim tData() As Byte = DataConvert.HexStringToByteArray(txtAuthenticateData.Text)
			Dim password As Integer = 0

			Dim result() As Byte = api.UHFAuthenticate(password, filterBank, filterAddr, filterDataLen, filterData, keyID, tData)
			If result Is Nothing OrElse result.Length = 0 Then
				MessageBox.Show("fail")
				txtTid.Text = ""
				Return
			ElseIf result.Length<>22 Then
				MessageBox.Show("返回数据长度错误")
				txtTid.Text = ""
				Return
			End If
			'Challenge：6个字节，Tags Shortened TID：8个字节，Tag Response：8个字节

			txtChallenge.Text = DataConvert.ByteArrayToHexString(BLEDeviceAPI.Utils.CopyArray(result, 0, 6))
			txtTid.Text = DataConvert.ByteArrayToHexString(BLEDeviceAPI.Utils.CopyArray(result, 6, 8))
			txtResponse.Text = DataConvert.ByteArrayToHexString(BLEDeviceAPI.Utils.CopyArray(result, 14, 8))
			' MessageBox.Show("success");
		End Sub

		Private Sub txtFilter_EPC_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilter_EPC.TextChanged
			If Not Me.txtFilter_EPC.IsHandleCreated Then Return

			FormatHex(txtFilter_EPC)
			Dim data As String = txtFilter_EPC.Text.Replace(" ", "")
			If data.Length > 0 Then
				txtFilter_EPCLen.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				txtFilter_EPCLen.Text = "0"
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
		Private isDelete As Boolean = False

		Private Sub M775AuthenticateForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
			If e.KeyCode = Keys.Back Then
				isDelete = True
			Else
				isDelete = False
			End If
		End Sub
		Private Function HexFormat(ByVal hex As String) As String
			Try
				Dim sb As New StringBuilder()
				For k As Integer = 0 To (hex.Length \ 2) - 1
					sb.Append(hex.Substring(k * 2, 2))
					If k < hex.Length \ 2 - 1 Then
						sb.Append(" ")
					End If
				Next k
				Return sb.ToString().ToUpper()
			Catch ex As System.Exception
				Return String.Empty
			End Try

		End Function

		Private Function DetectionFiltration() As Boolean

			If txtLen.Text.Trim().Length <> 0 Then
				If Integer.Parse(txtLen.Text) > 0 Then
					Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
					If Not StringUtils.IsHexNumber(filerData) Then
						MessageBox.Show(If(Common.isEnglish, "Please input the hex filter data!", "请输入十六进制过滤数据!"))
						Return False
					End If

					Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
					Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
					Dim filerLen As Integer = Integer.Parse(txtLen.Text)

					If (filerLen \ 8 + (If(filerLen Mod 8 = 0, 0, 1))) * 2 > filerData.Length Then
						MessageBox.Show(If(Common.isEnglish, "filter data length error!", "过滤数据和长度不匹配!")) 'to do
						Return False
					End If
				End If

			End If
			Return True
		End Function
		Private Sub rbEPC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbEPC.Click
			If Not Me.rbEPC.IsHandleCreated Then Return

			txtPtr.Text = "32"
		End Sub

		Private Sub rbTID_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbTID.Click
			If Not Me.rbTID.IsHandleCreated Then Return

			txtPtr.Text = "0"
		End Sub

		Private Sub rbUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbUser.Click
			If Not Me.rbUser.IsHandleCreated Then Return

			txtPtr.Text = "0"
		End Sub

		Private Sub txtAuthenticateData_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAuthenticateData.TextChanged
			If Not Me.txtAuthenticateData.IsHandleCreated Then Return

			FormatHex(txtAuthenticateData)
			Dim data As String = txtAuthenticateData.Text.Replace(" ", "")
			If data.Length > 0 Then
				txtDataLen.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				txtDataLen.Text = "0"
			End If
		End Sub
	End Class
End Namespace
