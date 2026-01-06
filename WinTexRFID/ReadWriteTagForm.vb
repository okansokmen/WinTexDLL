Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports WinForm_Test
Imports BLEDeviceAPI
Imports System.Threading
Imports System.Collections

Namespace UHFAPP
	Partial Public Class ReadWriteTagForm
		Inherits BaseForm

		Public tag2 As String = ""

		Public Sub New()
			InitializeComponent()
		End Sub

		Public Sub New(ByVal isOpen As Boolean, ByVal tag3 As String)
			InitializeComponent()
			SetTAG(isOpen, tag3)
		End Sub

		Public Sub SetTAG(ByVal isOpen As Boolean, ByVal tag3 As String)
			If isOpen Then
				panel1.Enabled = True
			Else
				panel1.Enabled = False
			End If
			If tag3 <> "" Then
				Me.tag2 = tag3
				txtFilter_EPC.Text = tag3
				txtLen.Text = (tag3.Length \ 2 * 8).ToString()
			End If
		End Sub

		Private Sub MainForm_eventOpen(ByVal open As Boolean)
			If open Then
				panel1.Enabled = True
			Else
				panel1.Enabled = False
			End If
		End Sub

		Private Sub ReadWriteTagForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
			Common.SaveControlValues(Me)
			RemoveHandler MainForm.eventOpen, AddressOf MainForm_eventOpen
			RemoveHandler MainForm.eventSwitchUI, AddressOf MainForm_eventSwitchUI
			RemoveHandler MainForm.eventMainSizeChanged, AddressOf MainForm_SizeChanged
		End Sub

		Private Sub ReadWriteTagForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			AddHandler MainForm.eventMainSizeChanged, AddressOf MainForm_SizeChanged

			txtFilter_EPC.Text = Common.tag
			AddHandler MainForm.eventOpen, AddressOf MainForm_eventOpen
			cmbRead_Bank.SelectedIndex = 1
			cmbBlockWrite__Bank.SelectedIndex = 1

'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: comboBox1.SelectedIndex = cmbQT1.SelectedIndex = 0;
			cmbQT1.SelectedIndex = 0
			comboBox1.SelectedIndex = cmbQT1.SelectedIndex
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: comboBox2.SelectedIndex = cmbQT2.SelectedIndex = 0;
			cmbQT2.SelectedIndex = 0
			comboBox2.SelectedIndex = cmbQT2.SelectedIndex
			cmbQT_bank.SelectedIndex = 1

			AddHandler MainForm.eventSwitchUI, AddressOf MainForm_eventSwitchUI
			MainForm_eventSwitchUI()

			AddHandler txtPtr.LostFocus, AddressOf txt_LostFocus '失去焦点后发生事件
			AddHandler txtLen.LostFocus, AddressOf txt_LostFocus '失去焦点后发生事件

			If txtFilter_EPC.Text.Replace(" ", "").Length > 0 Then
				txtLen.Text = (txtFilter_EPC.Text.Replace(" ", "").Length \ 2 * 8) & ""
			End If

			Dim hashtable As Hashtable = Common.GetControlValues(Me.Name)
			If hashtable IsNot Nothing Then
				Me.txtFilter_EPC.Text = DirectCast(hashtable(txtFilter_EPC.Name), String)
				Me.txtPtr.Text = DirectCast(hashtable(txtPtr.Name), String)
				Me.txtLen.Text = DirectCast(hashtable(txtLen.Name), String)
				Me.rbEPC.Checked = DirectCast(hashtable(rbEPC.Name), Boolean)
				Me.rbTID.Checked = DirectCast(hashtable(rbTID.Name), Boolean)
				Me.rbUser.Checked = DirectCast(hashtable(rbUser.Name), Boolean)
				Me.cmbRead_Bank.SelectedIndex = DirectCast(hashtable(cmbRead_Bank.Name), Integer)
				Me.txtRead_Ptr.Text = DirectCast(hashtable(txtRead_Ptr.Name), String)
				Me.txtRead_Length.Text = DirectCast(hashtable(txtRead_Length.Name), String)
				Me.txtRead_AccessPwd.Text = DirectCast(hashtable(txtRead_AccessPwd.Name), String)
				Me.txtRead_Data.Text = DirectCast(hashtable(txtRead_Data.Name), String)
				Me.cmbBlockWrite__Bank.SelectedIndex = DirectCast(hashtable(cmbBlockWrite__Bank.Name), Integer)
				Me.txtBlockWrite__Ptr.Text = DirectCast(hashtable(txtBlockWrite__Ptr.Name), String)
				Me.txtBlockWrite__Length.Text = DirectCast(hashtable(txtBlockWrite__Length.Name), String)
				Me.txtBlockWrite__AccessPwd.Text = DirectCast(hashtable(txtBlockWrite__AccessPwd.Name), String)
				Me.txtBlockWrite__Data.Text = DirectCast(hashtable(txtBlockWrite__Data.Name), String)
			End If
		End Sub


		Private Sub MainForm_eventSwitchUI()
			If Common.isEnglish Then
				groupBox4.Text = "filter"
				'btnFilterEPC.Text = "Read EPC";
				groupBox1.Text = "Read-write"
				label1.Text = "Bank:"
				label2.Text = "Prt:"
				label4.Text = "Length:"
				label5.Text = "Access Pwd:"
				label6.Text = "Data:"

				label13.Text = "(word)"

				label11.Text = "Bank:"
				label10.Text = "Prt:"
				label9.Text = "Length:"
				label8.Text = "Access Pwd:"
				label7.Text = "Data:"

				label20.Text = "(word)"

				label18.Text = "Bank:"
				label17.Text = "Prt:"
				label16.Text = "Length:"
				label15.Text = "Access Pwd:"
				label14.Text = "Data:"

				label21.Text = "(word)"



				btnQTRead.Text = "Read"
				btnWrite.Text = "Write"
				btWrite.Text = "Write"
				btnErase.Text = "Erase"
				btnRead.Text = "Read"
				btnQTWrite.Text = "Write"

				btnGetQT.Text = "Get"
				btnSetQT.Text = "Set"

				label27.Text = "Access Pwd:"
				label27.Location = New Point(2, 28)

				groupBox2.Text = "BlockWrite/Erase"

			Else
				groupBox4.Text = "过滤"
				'btnFilterEPC.Text = "读取EPC";
				groupBox1.Text = "读-写"
				label1.Text = "存储区:"
				label2.Text = "起始地址:"
				label4.Text = "长度:"
				label5.Text = "密码:"
				label6.Text = "数据:"

				label13.Text = "(字)"

				label11.Text = "存储区:"
				label10.Text = "起始地址:"
				label9.Text = "长度:"
				label8.Text = "密码:"
				label7.Text = "数据:"

				label20.Text = "(字)"

				label18.Text = "存储区:"
				label17.Text = "起始地址:"
				label16.Text = "长度:"
				label15.Text = "密码:"
				label14.Text = "数据:"

				label21.Text = "(字)"


				btnQTRead.Text = "读"
				btnWrite.Text = "写"
				btWrite.Text = "写"
				btnErase.Text = "擦除"
				btnRead.Text = "读"
				btnQTWrite.Text = "写"

				btnGetQT.Text = "获取"
				btnSetQT.Text = "设置"

				label27.Text = "密码"
				label27.Location = New Point(50, 30)

				groupBox2.Text = "数据块写/擦除"
			End If
		End Sub



		#Region "读写数据"
		Private Sub btnRead_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRead.Click
			If Not DetectionFiltration() Then
				Return
			End If

			Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
			Dim accessPwd As String = txtRead_AccessPwd.Text.Replace(" ", "")


			If Not StringUtils.IsHexNumber(accessPwd) OrElse accessPwd.Length <> 8 Then
				MessageBox.Show(If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须是8位!"))
				Return
			End If

			'过滤----------------------------------
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtLen.Text)

			If (filerLen \ 8 + (If(filerLen Mod 8 = 0, 0, 1))) * 2 > filerData.Length Then
				MessageBox.Show(If(Common.isEnglish, "filter data length error!", "过滤数据和长度不匹配!")) 'to do
				Return
			End If

			If rbTID.Checked Then
				filerBank = 2
			End If
			If rbUser.Checked Then
				filerBank = 3
			End If
			'-----------------------------------------
			Dim pwd() As Byte = DataConvert.HexStringToByteArray(accessPwd)
			Dim bank As Integer = cmbRead_Bank.SelectedIndex
			Dim Ptr As Integer = Integer.Parse(txtRead_Ptr.Text)
			Dim leng As Integer = Integer.Parse(txtRead_Length.Text)
			Dim msg As String = ""

			txtRead_Data.Text = ""
			Dim result As String = uhf.ReadData(pwd, CByte(filerBank), filerPtr, filerLen, filerBuff, CByte(bank), Ptr, leng)

			Dim time As Integer = 500
			If result <> String.Empty Then
				time = 100
				txtRead_Data.Text = result
				msg = If(Common.isEnglish, "Read success!", "读取数据成功!")
			Else
				msg = If(Common.isEnglish, "Read failure!", "读取数据失败!")
			End If

			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(time)
			End Sub, msg)
			f.ShowDialog(Me)
		End Sub
		Private Sub btnWrite_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWrite.Click
			If Not DetectionFiltration() Then
				Return
			End If

			Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
			Dim accessPwd As String = txtRead_AccessPwd.Text.Replace(" ", "")


			If Not StringUtils.IsHexNumber(accessPwd) OrElse accessPwd.Length <> 8 Then
				MessageBox.Show(If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须是8位!"))
				Return
			End If

			'过滤--------
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtLen.Text)

			If (filerLen \ 8 + (If(filerLen Mod 8 = 0, 0, 1))) * 2 > filerData.Length Then
				MessageBox.Show(If(Common.isEnglish, "filter data length error!", "过滤数据和长度不匹配!")) 'to do
				Return
			End If

			If rbTID.Checked Then
				filerBank = 2
			End If
			If rbUser.Checked Then
				filerBank = 3
			End If
			'----------
			Dim pwd() As Byte = DataConvert.HexStringToByteArray(accessPwd)
			Dim bank As Integer = cmbRead_Bank.SelectedIndex
			Dim Ptr As Integer = Integer.Parse(txtRead_Ptr.Text)
			Dim leng As Integer = Integer.Parse(txtRead_Length.Text)
			Dim msg As String = ""
			Dim Databuf As String = txtRead_Data.Text.Replace(" ", "")
			If Not StringUtils.IsHexNumber(Databuf) Then
				MessageBox.Show(If(Common.isEnglish, "Please input hexadecimal data!", "请输入十六进制数据!"))
				Return
			End If
			If Databuf.Length Mod 4 <> 0 Then
				MessageBox.Show(If(Common.isEnglish, "Write data of the length of the string must be in multiples of four!", "写入的十六进制字符串长度必须是4的倍数!"))
				Return
			End If
			If leng > (Databuf.Length\4) Then
				MessageBox.Show(If(Common.isEnglish, "Write data length error! ", "写入的数据和长度不匹配!"))
				Return
			End If
			Dim time As Integer = 500
			Dim uDatabuf() As Byte = DataConvert.HexStringToByteArray(Databuf)
			Dim result As Boolean = uhf.WriteData(pwd, CByte(filerBank), filerPtr, filerLen, filerBuff, CByte(bank), Ptr, CByte(leng), uDatabuf)
			'bool result = uhf.writeDataToEpc(pwd, (byte)filerBank, filerPtr, filerLen, filerBuff, uDatabuf);  
			If result Then
				time = 100
				msg = If(Common.isEnglish, "Write success!", "写入成功!")
			Else
				msg = If(Common.isEnglish, "Write failure!", "写入失败!")
			End If

			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(time)
			End Sub, msg)
			f.ShowDialog(Me)
		End Sub

		Private Sub txtRead_AccessPwd_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtRead_AccessPwd.TextChanged
			FormatHex_PWD(txtRead_AccessPwd)
		End Sub
		Private Sub txtRead_Data_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtRead_Data.TextChanged
			FormatHex(txtRead_Data)
			Dim data As String = txtRead_Data.Text.Replace(" ", "")
			If data.Length > 0 Then
				lblLeng.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				lblLeng.Text = "0"
			End If
		End Sub

		Private Sub cmbRead_Bank_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRead_Bank.SelectedIndexChanged
			Dim index As Integer = cmbRead_Bank.SelectedIndex
			If index = 1 Then
				If Integer.Parse(txtRead_Ptr.Text) < 2 Then
					txtRead_Ptr.Text = "2"
				End If
			Else
				txtRead_Ptr.Text = "0"
			End If

			If index = 1 OrElse index = 2 Then
				txtRead_Length.Text = "6"
			Else
				txtRead_Length.Text = "4"
			End If


		End Sub
		Private Sub TextChangedPtr(ByVal sender As Object, ByVal e As EventArgs) Handles txtRead_Ptr.TextChanged
			Dim txt As TextBox = DirectCast(sender, TextBox)
			If Not StringUtils.IsNumber(txt.Text) Then
				txt.Text = "0"
			End If
			Dim index As Integer = cmbRead_Bank.SelectedIndex
			If index = 1 Then
				If Integer.Parse(txt.Text) < 2 Then
				  '  txt.Text = "2";
				End If
			End If
		End Sub

		Private Sub TextChanged2(ByVal sender As Object, ByVal e As EventArgs) Handles txtRead_Length.TextChanged
			Dim txt As TextBox = DirectCast(sender, TextBox)
			If Not StringUtils.IsNumber(txt.Text) Then
				txt.Text = "0"
			End If
			If Integer.Parse(txt.Text) < 1 Then
				txt.Text = "1"
			End If
		End Sub
#End Region


#Region "BlockWrite"

		Private Sub btnErase_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnErase.Click
			If Not DetectionFiltration() Then
				Return
			End If


			Dim pws As String = txtBlockWrite__AccessPwd.Text.Replace(" ", "")

			If pws.Length <> 8 Then
				MessageBox.Show(If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须是8位!"))
				Return
			End If
			Dim bank As Integer = cmbBlockWrite__Bank.SelectedIndex
			Dim startIndex As Integer = Integer.Parse(txtBlockWrite__Ptr.Text)
			Dim leng As Integer = Integer.Parse(txtBlockWrite__Length.Text)
			Dim uAccessPwd() As Byte = DataConvert.HexStringToByteArray(pws)

			'------------过滤
			Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtLen.Text)

			If (filerLen \ 8 + (If(filerLen Mod 8 = 0, 0, 1))) * 2 > filerData.Length Then
				MessageBox.Show(If(Common.isEnglish, "filter data length error!", "过滤数据和长度不匹配!")) 'to do
				Return
			End If
			If rbTID.Checked Then
				filerBank = 2
			End If
			If rbUser.Checked Then
				filerBank = 3
			End If
			'-------------------
			Dim msg As String = ""
			Dim time As Integer = 500
			Dim result As Boolean = uhf.BlockEraseData(uAccessPwd, CByte(filerBank), filerPtr, filerLen, filerBuff, CByte(bank), startIndex, CByte(leng))
			If result Then
				time = 100
				msg = If(Common.isEnglish, "Erase success!", "擦除成功!")
			Else
				msg = If(Common.isEnglish, "Erase failure!", "擦除失败!")
			End If

			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(time)
			End Sub, msg)
			f.ShowDialog(Me)
		End Sub


		Private Sub btWrite_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btWrite.Click
			If Not DetectionFiltration() Then
				Return
			End If


			Dim pws As String = txtBlockWrite__AccessPwd.Text.Replace(" ", "")

			If pws.Length <> 8 Then
				MessageBox.Show(If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须是8位!"))
				Return
			End If
			Dim bank As Integer = cmbBlockWrite__Bank.SelectedIndex
			Dim startIndex As Integer = Integer.Parse(txtBlockWrite__Ptr.Text)
			Dim leng As Integer = Integer.Parse(txtBlockWrite__Length.Text)
			Dim uAccessPwd() As Byte = DataConvert.HexStringToByteArray(pws)

			'------------过滤
			Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtLen.Text)

			If (filerLen \ 8 + (If(filerLen Mod 8 = 0, 0, 1))) * 2 > filerData.Length Then
				MessageBox.Show(If(Common.isEnglish, "filter data length error!", "过滤数据和长度不匹配!")) 'to do
				Return
			End If
			If rbTID.Checked Then
				filerBank = 2
			End If
			If rbUser.Checked Then
				filerBank = 3
			End If
			'-------------------
			Dim msg As String = ""
			Dim data As String = txtBlockWrite__Data.Text.Replace(" ", "")
			If Not StringUtils.IsHexNumber(data) Then
				MessageBox.Show(If(Common.isEnglish, "Please input hexadecimal data!", "请输入十六进制数据!"))
				Return
			End If
			If data.Length Mod 4 <> 0 Then
				MessageBox.Show(If(Common.isEnglish, "Write data of the length of the string must be in multiples of four!", "写入的十六进制字符串长度必须是4的倍数!"))
				Return
			End If
			If leng > (data.Length \ 4) Then
				MessageBox.Show(If(Common.isEnglish, "Write data length error! ", "写入的数据和长度不匹配!"))
				Return
			End If
			Dim byteData() As Byte = DataConvert.HexStringToByteArray(data)

			Dim time As Integer = 500
			Dim result As Boolean = uhf.BlockWriteData(uAccessPwd, CByte(filerBank), filerPtr, filerLen, filerBuff, CByte(bank), startIndex, leng, byteData)
			If result Then
				time = 100
				msg = If(Common.isEnglish, "Write success!", "写入成功!")
			Else
				msg = If(Common.isEnglish, "Write failure!", "写入失败!")
			End If

			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(time)
			End Sub, msg)
			f.ShowDialog(Me)
		End Sub



		Private Sub txtBlockWrite__Data_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtBlockWrite__Data.TextChanged
			FormatHex(txtBlockWrite__Data)

			Dim data As String = txtBlockWrite__Data.Text.Replace(" ", "")
			If data.Length > 0 Then
				label25.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString()
			Else
				label25.Text = "0"
			End If
		End Sub

		Private Sub txtBlockWrite__AccessPwd_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtBlockWrite__AccessPwd.TextChanged
			FormatHex_PWD(txtBlockWrite__AccessPwd)
		End Sub

		Private Sub txtBlockWrite__Length_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtBlockWrite__Length.TextChanged
			Dim txt As TextBox = DirectCast(sender, TextBox)
			If Not StringUtils.IsNumber(txt.Text) Then
				txt.Text = "0"
			End If
			If Integer.Parse(txt.Text) < 1 Then
				txt.Text = "1"
			End If
		End Sub
		Private Sub txtBlockWrite__Ptr_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtBlockWrite__Ptr.TextChanged
			Dim txt As TextBox = DirectCast(sender, TextBox)
			If Not StringUtils.IsNumber(txt.Text) Then
				txt.Text = "0"
			End If
			Dim index As Integer = cmbBlockWrite__Bank.SelectedIndex
			If index = 1 Then
				If Integer.Parse(txt.Text) < 2 Then
				   ' txt.Text = "2";
				End If
			End If

		End Sub
		Private Sub cmbBlockWrite__Bank_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbBlockWrite__Bank.SelectedIndexChanged
			If cmbBlockWrite__Bank.SelectedIndex = 1 Then
				If Integer.Parse(txtBlockWrite__Ptr.Text) < 2 Then
					txtBlockWrite__Ptr.Text = "2"
				End If
			Else
				txtBlockWrite__Ptr.Text = "0"
			End If
			Dim index As Integer = cmbBlockWrite__Bank.SelectedIndex

			If index = 1 OrElse index = 2 Then
				txtBlockWrite__Length.Text = "6"
			Else
				txtBlockWrite__Length.Text = "4"
			End If

		End Sub
		#End Region

		#Region "QT"
		Private Sub textBox1_TextChanged_1(ByVal sender As Object, ByVal e As EventArgs) Handles textBox1.TextChanged
			FormatHex_PWD(textBox1)
		End Sub
		Private Sub txtQT_data_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtQT_data.TextChanged
			FormatHex(txtQT_data)
			Dim data As String = txtQT_data.Text.Replace(" ", "")
			If data.Length > 0 Then
				label26.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString()
			Else
				label26.Text = "0"
			End If
		End Sub


		Private Sub txtQT_pwd_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtQT_pwd.TextChanged
			FormatHex_PWD(txtQT_pwd)
		End Sub

		Private Sub QT_Length_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles QT_Length.TextChanged
			Dim txt As TextBox = DirectCast(sender, TextBox)
			If Not StringUtils.IsNumber(txt.Text) Then
				txt.Text = "0"
			End If
			If Integer.Parse(txt.Text) < 1 Then
				txt.Text = "1"
			End If
		End Sub

		Private Sub txtQT_ptr_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtQT_ptr.TextChanged
			Dim txt As TextBox = DirectCast(sender, TextBox)
			If Not StringUtils.IsNumber(txt.Text) Then
				txt.Text = "0"
			End If
			Dim index As Integer = cmbQT_bank.SelectedIndex
			If index = 0 Then
				If Integer.Parse(txt.Text) < 2 Then
				   ' txt.Text = "2";
				End If
			End If
		End Sub

		Private Sub cmbQT_bank_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbQT_bank.SelectedIndexChanged
			Dim index As Integer = cmbQT_bank.SelectedIndex
			If index = 1 Then
				If Integer.Parse(txtQT_ptr.Text) < 2 Then
					txtQT_ptr.Text = "2"
				End If
			Else
				txtQT_ptr.Text = "0"
			End If


			If index = 1 OrElse index = 2 Then
				QT_Length.Text = "6"
			Else
				QT_Length.Text = "4"
			End If
		End Sub

		Private Sub btnQTWrite_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnQTWrite.Click
			If Not DetectionFiltration() Then
				Return
			End If

			Dim pws As String = txtQT_pwd.Text.Replace(" ", "")
			If pws.Length <> 8 Then
				MessageBox.Show(If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须是8位!"))
				Return
			End If

			Dim bank As Integer = cmbQT_bank.SelectedIndex
			Dim startIndex As Integer = Integer.Parse(txtQT_ptr.Text)
			Dim leng As Integer = Integer.Parse(QT_Length.Text)
			Dim uAccessPwd() As Byte = DataConvert.HexStringToByteArray(pws)
			'-------过滤----------------------
			Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtLen.Text)

			If (filerLen \ 8 + (If(filerLen Mod 8 = 0, 0, 1))) * 2 > filerData.Length Then
				MessageBox.Show(If(Common.isEnglish, "filter data length error!", "过滤数据和长度不匹配!")) 'to do
				Return
			End If
			If rbTID.Checked Then
				filerBank = 2
			End If
			If rbUser.Checked Then
				filerBank = 3
			End If
			'--------------------------------------

			Dim QTData As Integer = Convert.ToByte("000000" & cmbQT2.SelectedIndex + cmbQT1.SelectedIndex)
			Dim msg As String = ""

			'写数据
'INSTANT VB NOTE: The variable qtData was renamed since Visual Basic will not allow local variables with the same case-insensitive name as parameters or other local variables:
			Dim qtData_Conflict As String = txtQT_data.Text.Replace(" ", "")
			If Not StringUtils.IsHexNumber(qtData_Conflict) Then
				MessageBox.Show("Please input hexadecimal data!")
				Return
			End If
			If qtData_Conflict.Length Mod 4 <> 0 Then
				MessageBox.Show(If(Common.isEnglish, "Write data of the length of the string must be in multiples of four!", "写入的十六进制字符串长度必须是4的倍数!"))
				Return
			End If
			If leng > (qtData_Conflict.Length \ 2) Then
				MessageBox.Show(If(Common.isEnglish, "Write data length error! ", "写入的数据和长度不匹配!"))
				Return
			End If
			Dim uDatabuf() As Byte = DataConvert.HexStringToByteArray(qtData_Conflict)

			Dim result As Boolean = uhf.WriteQT(uAccessPwd, CByte(filerBank), filerPtr, filerLen, filerBuff, CByte(QTData), CByte(bank), startIndex, CByte(leng), uDatabuf)
			If result Then
				msg = If(Common.isEnglish, "Write success", "写入成功!")
			Else
				msg = If(Common.isEnglish, "Write failure", "写入失败!")
			End If
			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(500)
			End Sub, msg)
			f.ShowDialog(Me)
		End Sub


		Private Sub btnQTRead_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnQTRead.Click
			If Not DetectionFiltration() Then
				Return
			End If

			Dim pws As String = txtQT_pwd.Text.Replace(" ", "")
			If pws.Length <> 8 Then
				MessageBox.Show(If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须是8位!"))
				Return
			End If

			Dim bank As Integer = cmbQT_bank.SelectedIndex
			Dim startIndex As Integer = Integer.Parse(txtQT_ptr.Text)
			Dim leng As Integer = Integer.Parse(QT_Length.Text)
			Dim uAccessPwd() As Byte = DataConvert.HexStringToByteArray(pws)
			'-------过滤----------------------
			Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtLen.Text)

			If (filerLen \ 8 + (If(filerLen Mod 8 = 0, 0, 1))) * 2 > filerData.Length Then
				MessageBox.Show(If(Common.isEnglish, "filter data length error!", "过滤数据和长度不匹配!")) 'to do
				Return
			End If
			If rbTID.Checked Then
				filerBank = 2
			End If
			If rbUser.Checked Then
				filerBank = 3
			End If
			'--------------------------------------

			Dim QTData As Integer = Convert.ToByte("000000" & cmbQT2.SelectedIndex + cmbQT1.SelectedIndex)
			Dim msg As String = ""
			txtQT_data.Text = ""
			'读数据
			Dim data As String = uhf.ReadQT(uAccessPwd, CByte(filerBank), filerPtr, filerLen, filerBuff, CByte(QTData), CByte(bank), startIndex, CByte(leng))
			If data <> String.Empty Then
				txtQT_data.Text = data
				msg = "  success"
			Else
				msg = "  failure"
			End If

			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(500)
			End Sub, msg)
			f.ShowDialog(Me)
		End Sub


		Private Sub btnGetQT_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetQT.Click
			If Not DetectionFiltration() Then
				Return
			End If

			Dim pws As String = textBox1.Text.Replace(" ", "")
			If pws.Length <> 8 Then
				MessageBox.Show(If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须是8位!"))
				Return
			End If

			Dim uAccessPwd() As Byte = DataConvert.HexStringToByteArray(pws)
			'-------过滤----------------------
			Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtLen.Text)

			If (filerLen \ 8 + (If(filerLen Mod 8 = 0, 0, 1))) * 2 > filerData.Length Then
				MessageBox.Show(If(Common.isEnglish, "filter data length error!", "过滤数据和长度不匹配!")) 'to do
				Return
			End If
			If rbTID.Checked Then
				filerBank = 2
			End If
			If rbUser.Checked Then
				filerBank = 3
			End If
			'--------------------------------------
			Dim time As Integer = 500
			Dim QTData As Integer = Convert.ToByte("000000" & comboBox2.SelectedIndex + comboBox1.SelectedIndex)
			Dim msg As String = ""
			'获取QT
			Dim qt_byte As Byte = 0
			If uhf.GetQT(uAccessPwd, CByte(filerBank), filerPtr, filerLen, filerBuff, qt_byte) Then
				time = 100
				msg = If(Common.isEnglish, "  success", "获取QT成功!")
				Dim b0 As Integer = ((qt_byte >> 0) And &H1)
				Dim b1 As Integer = ((qt_byte >> 1) And &H1)
				comboBox1.SelectedIndex = b0
				comboBox2.SelectedIndex = b1
			Else
				msg = If(Common.isEnglish, "  failure", "获取QT失败!")
			End If
			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(time)
			End Sub, msg)
			f.ShowDialog(Me)
		End Sub

		Private Sub btnSetQT_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetQT.Click
			If Not DetectionFiltration() Then
				Return
			End If

			Dim pws As String = textBox1.Text.Replace(" ", "")
			If pws.Length <> 8 Then
				MessageBox.Show(If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须是8位!"))
				Return
			End If

			Dim uAccessPwd() As Byte = DataConvert.HexStringToByteArray(pws)
			'-------过滤----------------------
			Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtLen.Text)

			If (filerLen \ 8 + (If(filerLen Mod 8 = 0, 0, 1))) * 2 > filerData.Length Then
				MessageBox.Show(If(Common.isEnglish, "filter data length error!", "过滤数据和长度不匹配!")) 'to do
				Return
			End If
			If rbTID.Checked Then
				filerBank = 2
			End If
			If rbUser.Checked Then
				filerBank = 3
			End If
			'--------------------------------------
			Dim time As Integer = 500
			Dim QTData As Integer = Convert.ToByte("000000" & comboBox2.SelectedIndex + comboBox1.SelectedIndex)
			Dim msg As String = ""
			 '设置QT
			If uhf.SetQT(uAccessPwd, CByte(filerBank), filerPtr, filerLen, filerBuff, CByte(QTData)) Then
				time = 100
				msg = If(Common.isEnglish, "  success", "设置QT成功!")
			Else
				msg = If(Common.isEnglish, "  failure", "设置QT失败!")
			End If

			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(time)
			End Sub, msg)
			f.ShowDialog(Me)
		End Sub

		#End Region



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
					txt.SelectionStart = SelectIndex+2
				End If
				'txt.Select(txt.Text.Length - 1, 0);

			End If
		End Sub
		Private Sub FormatHex_PWD(ByVal txt As TextBox)
			If isDelete Then
				Return
			End If
			Dim data As String = txt.Text.Trim().Replace(" ", "")
			If data <> String.Empty Then
				If data.Length > 8 Then
					data = data.Substring(0,8)
				End If
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
		#Region "过滤"
		Private Sub btnFilterEPC_Click(ByVal sender As Object, ByVal e As EventArgs)
			Dim epc As String = ""
			Dim msg As String = If(Common.isEnglish, "failure", "读取EPC失败!")
			If uhf.InventorySingle(epc) Then
				txtFilter_EPC.Text = epc
				Common.tag = epc
				msg = If(Common.isEnglish, "success", "读取EP成功!")
			End If

			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(500)
			End Sub, msg)
			f.ShowDialog(Me)
		End Sub
		Private Sub txtFilter_EPC_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilter_EPC.TextChanged
			FormatHex(txtFilter_EPC)
			Dim data As String = txtFilter_EPC.Text.Replace(" ", "")
			If data.Length > 0 Then
				label29.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				label29.Text = "0"
			End If
		End Sub

		Public Function DetectionFiltration() As Boolean
			If Integer.Parse(txtLen.Text) > 0 Then
				Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
				If Not StringUtils.IsHexNumber(filerData) Then
					MessageBox.Show(If(Common.isEnglish, "Please input the hex filter data!", "请输入十六进制过滤数据!"))
					Return False
				End If
			End If
			Return True
		End Function
		Private Sub rbEPC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbEPC.Click
			txtPtr.Text = "32"
		End Sub

		Private Sub rbTID_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbTID.Click
			txtPtr.Text = "0"
		End Sub

		Private Sub rbUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbUser.Click
			txtPtr.Text = "0"
		End Sub
		#End Region

		Private Sub txtPtr_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtPtr.TextChanged
			Try
				If txtPtr.Text = "" Then
					Return
				End If
				Dim ptr As String = txtPtr.Text
				If Not StringUtils.IsNumber(ptr) Then
					txtPtr.Text = "0"
					Return
				End If

			Catch ex As Exception
				txtPtr.Text = "0"
			End Try
		End Sub

		Private Sub textBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtLen.TextChanged
			Try
				If txtLen.Text = "" Then
					Return
				End If

				Dim ptr As String = txtLen.Text
				If Not StringUtils.IsNumber(ptr) Then
					txtLen.Text = "0"
					Return
				End If

			Catch ex As Exception
				txtLen.Text = "0"
			End Try
		End Sub




		Private Sub txt_LostFocus(ByVal sender As Object, ByVal e As EventArgs)
			Dim text As TextBox = DirectCast(sender, TextBox)

			If text.Text = "" Then
				text.Text = "0"
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

		Private Sub btnSelectFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSelectFile.Click
			Dim openDlg As New OpenFileDialog()
			openDlg.Filter = "(*.bmp,*.txt)|*.txt;*.bmp"
			If openDlg.ShowDialog() = DialogResult.OK Then
				' 显示文件路径名
				Dim txtPath As String = openDlg.FileName
				If txtPath.EndsWith(".txt") Then
					txtWriteScreenData.Text = FileManage.ReadFile(txtPath)
				Else
					txtWriteScreenData.Text = FileManage.ReadFileBmp(txtPath)
				End If
			End If
		End Sub

		Private Sub txtWriteScreenData_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtWriteScreenData.TextChanged

			FormatHex(txtWriteScreenData)

			Dim data As String = txtWriteScreenData.Text.Replace(" ", "")
			If data.Length > 0 Then
				label31.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString()
				WriteScreenLength.Text = "" & data.Length \ 4
			Else
				label31.Text = "0"
				WriteScreenLength.Text = "0"
			End If
		End Sub

		Private Sub btnWriteScreen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWriteScreen.Click
			If Not DetectionFiltration() Then
				Return
			End If

			Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
			Dim accessPwd As String = txtRead_AccessPwd.Text.Replace(" ", "")


			If Not StringUtils.IsHexNumber(accessPwd) OrElse accessPwd.Length <> 8 Then
				MessageBox.Show(If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须是8位!"))
				Return
			End If

			'过滤--------
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtLen.Text)

			If (filerLen \ 8 + (If(filerLen Mod 8 = 0, 0, 1))) * 2 > filerData.Length Then
				MessageBox.Show(If(Common.isEnglish, "filter data length error!", "过滤数据和长度不匹配!")) 'to do
				Return
			End If

			If rbTID.Checked Then
				filerBank = 2
			End If
			If rbUser.Checked Then
				filerBank = 3
			End If
			'----------

			Dim type As Byte = Byte.Parse(textBox2.Text)
			Dim pwd() As Byte = DataConvert.HexStringToByteArray(WriteScreenPwd.Text)
			Dim Ptr As Integer = Integer.Parse(txtWriteScreenPtr.Text)
			Dim leng As Integer = Integer.Parse(WriteScreenLength.Text)
			Dim msg As String = ""
			Dim Databuf As String = txtWriteScreenData.Text.Replace(" ", "")
			If Not StringUtils.IsHexNumber(Databuf) Then
				MessageBox.Show(If(Common.isEnglish, "Please input hexadecimal data!", "请输入十六进制数据!"))
				Return
			End If
			If Databuf.Length Mod 4 <> 0 Then
				MessageBox.Show(If(Common.isEnglish, "Write data of the length of the string must be in multiples of four!", "写入的十六进制字符串长度必须是4的倍数!"))
				Return
			End If
			If leng > (Databuf.Length \ 4) Then
				MessageBox.Show(If(Common.isEnglish, "Write data length error! ", "写入的数据和长度不匹配!"))
				Return
			End If
			Dim time As Integer = 500
			Dim uDatabuf() As Byte = DataConvert.HexStringToByteArray(Databuf)

			'-------------------------------------------------
			Dim count As Integer = leng \ 200
			If leng Mod 200 > 0 Then
				count = count + 1
			End If
			Dim start As Integer=0
			Dim result As Boolean=False
			For k As Integer = 0 To count - 1
				start = k * 400
				Dim tempLen As Integer = 400
				Dim data() As Byte
				If start + 400 < leng*2 Then
					data = BLEDeviceAPI.Utils.CopyArray(uDatabuf, start, tempLen)
				Else
					tempLen= leng*2 - start
					data = BLEDeviceAPI.Utils.CopyArray(uDatabuf, start, tempLen)
				End If
				result = uhf.WriteScreenBlockData(pwd, CByte(filerBank), CShort(filerPtr), CShort(filerLen), filerBuff, type, CShort(Ptr), CShort(tempLen \ 2), data)
				If Not result Then
				   Exit For
				End If
			Next k
		   '---------------------------------------
			If result Then
				time = 100
				msg = If(Common.isEnglish, "Write success!", "写入成功!")
			Else
				msg = If(Common.isEnglish, "Write failure!", "写入失败!")
			End If

			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(time)
			End Sub, msg)
			f.ShowDialog(Me)


		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs)

			If Not DetectionFiltration() Then
				Return
			End If

			Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
			Dim accessPwd As String = txtRead_AccessPwd.Text.Replace(" ", "")


			If Not StringUtils.IsHexNumber(accessPwd) OrElse accessPwd.Length <> 8 Then
				MessageBox.Show(If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须是8位!"))
				Return
			End If

			'过滤----------------------------------
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtLen.Text)

			If (filerLen \ 8 + (If(filerLen Mod 8 = 0, 0, 1))) * 2 > filerData.Length Then
				MessageBox.Show(If(Common.isEnglish, "filter data length error!", "过滤数据和长度不匹配!")) 'to do
				Return
			End If

			If rbTID.Checked Then
				filerBank = 2
			End If
			If rbUser.Checked Then
				filerBank = 3
			End If
			'-----------------------------------------

			Dim result As Boolean = uhf.InitRegFile(CByte(filerBank), filerPtr, filerLen, filerBuff)
			Dim msg As String=""
			Dim time As Integer = 500
			If result Then
				time = 100
				msg = If(Common.isEnglish, " success!", "初始化成功!")
			Else
				msg = If(Common.isEnglish, " failure!", "初始化失败!")
			End If

			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(time)
			End Sub, msg)
			f.ShowDialog(Me)
		End Sub


		Private Sub MainForm_SizeChanged(ByVal state As FormWindowState)
			'判断是否选择的是最小化按钮
			panel1.Left = 308
		End Sub













	End Class
End Namespace
