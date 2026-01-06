Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Globalization

Namespace UHFAPP.custom.authenticate
	Partial Public Class AuthenticateForm
		Inherits Form

		Private api As New AuthenticateAPI()

		Public Sub New()
			InitializeComponent()
		End Sub
		''' <summary>
		''' 激活
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnRead_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRead.Click
			If Not Me.btnRead.IsHandleCreated Then Return

			If Not DetectionFiltration() Then
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
			Dim pwd(7) As Byte

			Dim result As String = UHFAPI.getInstance().ReadData(pwd, filterBank, filterAddr, filterDataLen, filterData, &H3, 192, 8)
			If result Is Nothing OrElse result.Length = 0 Then
				MessageBox.Show("fail")
				txtKey0Data.Text = ""
				Return
			End If
			txtKey0Data.Text = result
			MessageBox.Show("success")
		End Sub
		 ''' <summary>
		 ''' 激活
		 ''' </summary>
		 ''' <param name="sender"></param>
		 ''' <param name="e"></param>
		Private Sub btnActivate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActivate.Click
			If Not Me.btnActivate.IsHandleCreated Then Return

			If Not DetectionFiltration() Then
				Return
			End If

			If MessageBox.Show("Are you sure you want to activate the secret key?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
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
			Dim data() As Byte = DataConvert.HexStringToByteArray("E200")
			Dim pwd(7) As Byte

			Dim result As Boolean = UHFAPI.getInstance().WriteData(pwd, filterBank, filterAddr, filterDataLen, filterData, &H3, 200, 1, data)
			If Not result Then
				MessageBox.Show("fail")
				Return
			End If

			MessageBox.Show("success!")
			Return

		End Sub
		''' <summary>
		''' 写
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnWrite_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWrite.Click
			If Not Me.btnWrite.IsHandleCreated Then Return

			If Not DetectionFiltration() Then
				Return
			End If
			Dim key As String = txtKey0Data.Text.Replace(" ", "")
			If key.Length = 0 Then
				MessageBox.Show("The data cannot be empty!")
				Return
			End If

			If Not StringUtils.IsHexNumber(key) Then
				MessageBox.Show("Please input the hex data!")
				Return
			End If
			If key.Length <> 32 Then
				MessageBox.Show("The length of the message must be 16 bytes!")
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
			Dim pwd(7) As Byte

			Dim result As Boolean = UHFAPI.getInstance().WriteData(pwd, filterBank, filterAddr, filterDataLen, filterData, &H3, 192, 8, DataConvert.HexStringToByteArray(key))
			If Not result Then
				MessageBox.Show("fail")
				Return
			End If
			MessageBox.Show("success!")

		End Sub

		''' <summary>
		''' 认证
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnAuthenticate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAuthenticate.Click
			If Not Me.btnAuthenticate.IsHandleCreated Then Return

			If Not DetectionFiltration() Then
				Return
			End If

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
			Dim keyID As Byte = Byte.Parse(txtAuthenticateKeyID.Text)
			Dim tData() As Byte = DataConvert.HexStringToByteArray(txtAuthenticateData.Text)
			Dim password As Integer = 0

			Dim result() As Byte = api.UHFAuthenticate(password, filterBank, filterAddr, filterDataLen, filterData, keyID, tData)
			If result Is Nothing OrElse result.Length = 0 Then
				MessageBox.Show("fail")
				txtAuthenticateEncryptionData.Text = ""
				Return
			End If
			txtAuthenticateEncryptionData.Text = DataConvert.ByteArrayToHexString(result)
			MessageBox.Show("success")
		End Sub
		''' <summary>
		''' 解密
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnDecode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDecode.Click
			If Not Me.btnDecode.IsHandleCreated Then Return

			If decodeKey.Text.Replace(" ", "").Length <> 32 Then
				MessageBox.Show("The length of the key must be 16 bytes!")
				Return
			End If

			If Not StringUtils.IsHexNumber(decodeKey.Text.Replace(" ","")) Then
				MessageBox.Show("Please input the hex key!")
				Return
			End If





			Dim key() As Byte = DataConvert.HexStringToByteArray(decodeKey.Text)
			Dim data() As Byte = DataConvert.HexStringToByteArray(txtDecodeCiphertext.Text)
			Dim result() As Byte = api.AesDecrypto(key,data)
			If result Is Nothing OrElse result.Length = 0 Then
				txtC.Text = ""
				txtRnd.Text = ""
				txtMsg.Text = ""
				MessageBox.Show("fail")
				Return
			Else
				Dim hexData As String = DataConvert.ByteArrayToHexString(result).Replace(" ","")
				txtC.Text = HexFormat(hexData.Substring(0,4))
				txtRnd.Text = HexFormat(hexData.Substring(4, 8))
				txtMsg.Text = HexFormat(hexData.Substring(12, 20))
				MessageBox.Show("success")
			End If
		End Sub

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
		Private Sub txtKey0Data_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtKey0Data.TextChanged
			If Not Me.txtKey0Data.IsHandleCreated Then Return

			FormatHex(txtKey0Data)
			Dim data As String = txtKey0Data.Text.Replace(" ", "")
			If data.Length > 0 Then
				txtKey0DataLen.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				txtKey0DataLen.Text = "0"
			End If
		End Sub
		Private Sub txtData_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAuthenticateData.TextChanged
			If Not Me.txtAuthenticateData.IsHandleCreated Then Return

			FormatHex(txtAuthenticateData)
			Dim data As String = txtAuthenticateData.Text.Replace(" ", "")
			If data.Length > 0 Then
				txtDataLen.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				txtDataLen.Text = "0"
			End If

			'if (txtDataLen.Text.Replace(" ", "").Length > 20)
			'{
			'    txtDataLen.Text = txtDataLen.Text.Replace(" ", "").Substring(0, 20);
			'}
		End Sub
		Private Sub txtEncryptionData2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAuthenticateEncryptionData.TextChanged
			If Not Me.txtAuthenticateEncryptionData.IsHandleCreated Then Return

			FormatHex(txtAuthenticateEncryptionData)
			Dim data As String = txtAuthenticateEncryptionData.Text.Replace(" ", "")
			If data.Length > 0 Then
				txtEncryptionData2Len.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				txtEncryptionData2Len.Text = "0"
			End If
		End Sub
		Private Sub decodeKey_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles decodeKey.TextChanged
			If Not Me.decodeKey.IsHandleCreated Then Return

			FormatHex(decodeKey)
			Dim data As String = decodeKey.Text.Replace(" ", "")
			If data.Length > 0 Then
				lblencryptoKeyLen.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				lblencryptoKeyLen.Text = "0"
			End If
		End Sub
		Private Sub txtData2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtDecodeCiphertext.TextChanged
			If Not Me.txtDecodeCiphertext.IsHandleCreated Then Return

			FormatHex(txtDecodeCiphertext)
			Dim data As String = txtDecodeCiphertext.Text.Replace(" ", "")
			If data.Length > 0 Then
				txtData2Len.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				txtData2Len.Text = "0"
			End If
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
		Private Sub AuthenticateForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
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






	End Class
End Namespace
