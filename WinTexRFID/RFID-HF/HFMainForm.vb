Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports WinForm_Test
Imports UHFAPP.RFID_HF

Namespace UHFAPP.RFID
'''    
'''     * 
'''     * 使用之前先调用 OpenUsb();打开设备。
'''     * 退出之前先调用 CloseUsb()关闭设备。
'''     * 
'''     * 以上两个函数和UHFAPI 里面的同名函数通用.
'''     * 
'''     
	Partial Public Class RFIDMainForm
		Inherits Form

		Private hfAPI As New HF14443API()
		Private hf15693API As New HF15693API()
		Private psam As New PSAMAPI()
		Public Sub New()
			InitializeComponent()
			'hfAPI.OpenUsb();
			cmbM1TagType.SelectedIndex = 0
			cmbM1KeyType.SelectedIndex = 0
			cmbM1Sector.SelectedIndex = 1
			cmbM1Block.SelectedIndex = 0

			cmb15693Block.SelectedIndex = 0

		End Sub
		Private Sub RFIDMainForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
		   'hfAPI.CloseUsb();
		End Sub



		#Region "14443A"

		''' <summary>
		''' 读
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnRead_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRead.Click
			If Not Me.btnRead.IsHandleCreated Then Return

			Dim buffType() As Byte = hfAPI.RequestTypeA(&H52)
			If buffType Is Nothing Then
				ShowMessage(If(Common.isEnglish, "Card not found!", "寻卡失败!"))
				Return
			End If

			Dim buffCardID() As Byte = hfAPI.AnticollTypeA()
			If buffType Is Nothing Then
				ShowMessage(If(Common.isEnglish, "Read failure", "获取卡片号失败!"))
				Return
			End If

			Dim type As Integer = hfAPI.SelectTypeA()
			If type = -1 Then
				ShowMessage(If(Common.isEnglish, "Read failure", "获取卡片类型失败!"))
				Return
			End If

			Dim cMode As Byte = CByte(&H60 + cmbM1KeyType.SelectedIndex) '0x60:A  ;  0x61:B
			Dim cBlock As Integer = (cmbM1Sector.SelectedIndex * 4) + cmbM1Block.SelectedIndex
			Dim pcKey() As Byte = DataConvert.HexStringToByteArray(txtM1Key.Text)
			Dim result As Boolean = hfAPI.Authentication(cMode, CByte(cBlock), pcKey)
			If Not result Then
				ShowMessage(If(Common.isEnglish, "The key validation fail", "卡片认证失败!"))
				Return
			End If

			Dim data() As Byte = hfAPI.ReadBlock(CByte(cBlock))
			If data Is Nothing Then
				ShowMessage(If(Common.isEnglish, "Read failure", "获取block数据失败!"))
				Return
			End If

			txtM1Data.Text = DataConvert.ByteArrayToHexString(data)
			ShowMessage(If(Common.isEnglish, "Read Success", "获取成功!"))
		End Sub

		''' <summary>
		''' 写
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnWrite_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWrite.Click
			If Not Me.btnWrite.IsHandleCreated Then Return

			Dim sector As Integer = cmbM1Sector.SelectedIndex
			Dim block As Integer = cmbM1Block.SelectedIndex

			If sector = 0 AndAlso block = 0 Then
				ShowMessage(If(Common.isEnglish, "failure", "0扇区的0数据块不能写入"))
				Return
			ElseIf sector < 32 AndAlso block = 3 Then
				ShowMessage(If(Common.isEnglish, "This program does not support the data block write operation, the data is password control block are not familiar with the tag structure please do not write to", "此程序不支持该数据块的写入操作,该数据块是密码控制块"))
				Return
			ElseIf sector > 31 AndAlso block = 15 Then
				ShowMessage(If(Common.isEnglish, "This program does not support the data block write operation, the data is password control block are not familiar with the tag structure please do not write to", "此程序不支持该数据块的写入操作,该数据块是密码控制块"))
				Return
			End If

			Dim strData As String = txtM1Data.Text.Replace(" ","")
			If strData.Length = 0 Then
				ShowMessage(If(Common.isEnglish, "Content to be written can not be empty!", "写入内容不能为空"))
				Return
			ElseIf Not StringUtils.IsHexNumber(strData) Then
				ShowMessage(If(Common.isEnglish, "Please enter the hexadecimal number content", "请输入十六进制数"))
				Return
			End If

			Dim buffType() As Byte = hfAPI.RequestTypeA(&H52)
			If buffType Is Nothing Then
				ShowMessage(If(Common.isEnglish, "Card not found!", "寻卡失败!"))
				Return
			End If

			Dim buffCardID() As Byte = hfAPI.AnticollTypeA()
			If buffType Is Nothing Then
				ShowMessage(If(Common.isEnglish, "failure", "获取卡片号失败!"))
				Return
			End If

			Dim type As Integer = hfAPI.SelectTypeA()
			If type = -1 Then
				ShowMessage(If(Common.isEnglish, "failure", "获取卡片类型失败!"))
				Return
			End If
			Dim cMode As Byte = CByte(&H60 + cmbM1KeyType.SelectedIndex) '0x60:A  ;  0x61:B
			Dim cBlock As Integer = (cmbM1Sector.SelectedIndex * 4) + cmbM1Block.SelectedIndex
			Dim pcKey() As Byte = DataConvert.HexStringToByteArray(txtM1Key.Text)
			Dim result As Boolean = hfAPI.Authentication(cMode, CByte(cBlock), pcKey)
			If Not result Then
				ShowMessage(If(Common.isEnglish, "The key validation fail", "卡片认证失败!"))
				Return
			End If

			 result = hfAPI.WriteBlock(CByte(cBlock), DataConvert.HexStringToByteArray(strData))
			If Not result Then
				ShowMessage(If(Common.isEnglish, "failure", "写卡失败!"))
				Return
			End If

			ShowMessage(If(Common.isEnglish, "Success", "写卡成功!"))

		End Sub
		Private Sub txtM1Data_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtM1Data.TextChanged
			If Not Me.txtM1Data.IsHandleCreated Then Return

			FormatHex(txtM1Data)
		End Sub

		Private Sub cmbM1TagType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbM1TagType.SelectedIndexChanged
			If Not Me.cmbM1TagType.IsHandleCreated Then Return

			If cmbM1TagType.SelectedIndex = 0 Then
				'S50
				cmbM1Sector.Items.Clear()
				Dim items() As Object = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }
				cmbM1Sector.Items.AddRange(items)

				cmbM1Sector.SelectedIndex = 1
			ElseIf cmbM1TagType.SelectedIndex = 1 Then
				'S70
				cmbM1Sector.Items.Clear()
				Dim items() As Object = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 }
				cmbM1Sector.Items.AddRange(items)

				cmbM1Sector.SelectedIndex = 1
			End If
		End Sub

		Private Sub cmbM1Sector_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbM1Sector.SelectedIndexChanged
			If Not Me.cmbM1Sector.IsHandleCreated Then Return

			If cmbM1Sector.SelectedIndex >= 32 Then
				Dim oldSelect As Integer = cmbM1Block.SelectedIndex

				cmbM1Block.Items.Clear()
				Dim items() As Object = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }
				cmbM1Block.Items.AddRange(items)

				If oldSelect <= 15 Then
					cmbM1Block.SelectedIndex = oldSelect
				Else
					cmbM1Block.SelectedIndex = 0
				End If
			Else
				Dim oldSelect As Integer = cmbM1Block.SelectedIndex

				cmbM1Block.Items.Clear()
				Dim items() As Object = { 0, 1, 2, 3 }
				cmbM1Block.Items.AddRange(items)

				If oldSelect <= 3 Then
					cmbM1Block.SelectedIndex = oldSelect
				Else
					cmbM1Block.SelectedIndex = 0
				End If

			End If

		End Sub

	   #End Region

		#Region "14443A CPU"
		 Private Sub btn14443ACPUInit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn14443ACPUInit.Click
			 If Not Me.btn14443ACPUInit.IsHandleCreated Then Return

			 txtCPUReceive.Text = ""
			 Dim hexData As String = txt14443ACPUData.Text.Replace(" ", "")
			 If hexData.Length = 0 Then
				 ShowMessage(If(Common.isEnglish, "Content to be send can not be empty!", "内容不能为空"))
				 Return
			 ElseIf Not StringUtils.IsHexNumber(hexData) Then
				 ShowMessage(If(Common.isEnglish, "Please enter the hexadecimal number content", "请输入十六进制数"))
				 Return
			 End If




			 Dim buffType() As Byte = hfAPI.RequestTypeA(&H52)
			 If buffType Is Nothing Then
				 ShowMessage(If(Common.isEnglish, "Card not found!", "寻卡失败!"))
				 Return
			 End If

			 Dim buffCardID() As Byte = hfAPI.AnticollTypeA()
			 If buffType Is Nothing Then
				 ShowMessage(If(Common.isEnglish, "failure", "获取卡片号失败!"))
				 Return
			 End If

			 Dim type As Integer = hfAPI.SelectTypeA()
			 If type = -1 Then
				 ShowMessage(If(Common.isEnglish, "failure", "获取卡片类型失败!"))
				 Return
			 End If

			 Dim data() As Byte=hfAPI.RatsTypeA()
			 If data Is Nothing Then
				 ShowMessage(If(Common.isEnglish, ">RATS and PPS error", "RATS和PPS出错!"))
				 Return
			 End If
			 'RATS返回码-->data

			 Dim result() As Byte = hfAPI.CpuCommand(DataConvert.HexStringToByteArray(hexData))
			 If result Is Nothing Then
				 ShowMessage(If(Common.isEnglish, "Send failure", "命令发送出错!"))
				 Return
			 End If

			 txtCPUReceive.Text = DataConvert.ByteArrayToHexString(result)

			 ShowMessage(If(Common.isEnglish, "Success", "命令发送成功!"))
		 End Sub
	   #End Region

		#Region "14443B"
		 ''' <summary>
		 ''' 14443B
		 ''' </summary>
		 ''' <param name="sender"></param>
		 ''' <param name="e"></param>
		 Private Sub btnGetUID_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetUID.Click
			 If Not Me.btnGetUID.IsHandleCreated Then Return

			 txtUID.Text = ""
			 Dim data() As Byte= hfAPI.GetUidTypeB()
			 If data Is Nothing Then
				 ShowMessage(If(Common.isEnglish, "failure", "获取失败!"))
				 Return
			 End If
			 txtUID.Text = DataConvert.ByteArrayToHexString(data)
			 ShowMessage(If(Common.isEnglish, "Success", "获取成功!"))
		 End Sub

		 #End Region 

		#Region "15693"
		Private Sub btn15693Read_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn15693Read.Click
			If Not Me.btn15693Read.IsHandleCreated Then Return

			Dim entity As ISO15693Entity = hf15693API.Inventory()
			If entity Is Nothing Then
				ShowMessage(If(Common.isEnglish, "Card not found!", "寻卡失败!"))
				Return
			End If

			Dim block As Integer = cmb15693Block.SelectedIndex
			Dim data() As Byte = hf15693API.Read(entity, block)
			If data Is Nothing Then
				ShowMessage(If(Common.isEnglish, "failure", "读取卡片失败!"))
				Return
			End If

			txt15693Data.Text = DataConvert.ByteArrayToHexString(data)
			ShowMessage(If(Common.isEnglish, "Success", "读取成功!"))
		End Sub

		Private Sub btn15693Write_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn15693Write.Click
			If Not Me.btn15693Write.IsHandleCreated Then Return

			Dim strData As String = txt15693Data.Text.Replace(" ","")
			If strData.Length = 0 Then
				ShowMessage(If(Common.isEnglish, "Content to be written can not be empty!", "写入内容不能为空"))
				Return
			ElseIf Not StringUtils.IsHexNumber(strData) Then
				ShowMessage(If(Common.isEnglish, "Please enter the hexadecimal number content", "请输入十六进制数"))
				Return
			End If

			Dim entity As ISO15693Entity = hf15693API.Inventory()
			If entity Is Nothing Then
				ShowMessage(If(Common.isEnglish, "Card not found!", "寻卡失败!"))
				Return
			End If
			Dim block As Integer = cmb15693Block.SelectedIndex
			Dim data() As Byte = DataConvert.HexStringToByteArray(txt15693Data.Text)
			Dim result As Boolean = hf15693API.Write(entity, block, data)
			If Not result Then
				ShowMessage(If(Common.isEnglish, "failure", "写卡片失败!"))
				Return
			End If
			ShowMessage(If(Common.isEnglish, "Success", "写入成功!"))

		End Sub

		Private Sub btnAFIWrite_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAFIWrite.Click
			If Not Me.btnAFIWrite.IsHandleCreated Then Return

			Dim afi As String = txtAFI.Text

			If afi.Length <> 2 Then
				ShowMessage(If(Common.isEnglish, "Data should be a byte", "写入的数据必须是1个字节!"))
				Return
			ElseIf Not StringUtils.IsHexNumber(afi) Then
				ShowMessage(If(Common.isEnglish, "Please enter the hexadecimal number content", "请输入十六进制数"))
				Return
			End If

			Dim entity As ISO15693Entity = hf15693API.Inventory()
			If entity Is Nothing Then
				ShowMessage(If(Common.isEnglish, "Card not found!", "寻卡失败!"))
				Return
			End If
			Dim result As Boolean = hf15693API.WriteAFI(entity, Convert.ToByte(afi, 16))
			If Not result Then
				ShowMessage(If(Common.isEnglish, "failure", "写失败!"))
				Return
			End If
			ShowMessage(If(Common.isEnglish, "Success", "写成功!"))

		End Sub

		Private Sub btnAFILock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAFILock.Click
			If Not Me.btnAFILock.IsHandleCreated Then Return

			Dim dialogResult As DialogResult = MessageBox.Show(If(Common.isEnglish, "Are you sure you want to lock it?", "确定要锁吗？"),If(Common.isEnglish, "Lock", "锁"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If dialogResult = System.Windows.Forms.DialogResult.No Then
				Return
			End If

			Dim entity As ISO15693Entity = hf15693API.Inventory()
			If entity Is Nothing Then
				ShowMessage(If(Common.isEnglish, "Card not found!", "寻卡失败!"))
				Return
			End If

			Dim result As Boolean = hf15693API.LockAFI()
			If Not result Then
				ShowMessage(If(Common.isEnglish, "failure", "锁失败!"))
				Return
			End If
			ShowMessage(If(Common.isEnglish, "Success", "锁成功!"))
		End Sub

		Private Sub btnDsfidWrite_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDsfidWrite.Click
			If Not Me.btnDsfidWrite.IsHandleCreated Then Return

			Dim dsfid As String = txtDsfid.Text

			If dsfid.Length <> 2 Then
				ShowMessage(If(Common.isEnglish, "Data should be a byte", "写入的数据必须是1个字节!"))
				Return
			ElseIf Not StringUtils.IsHexNumber(dsfid) Then
				ShowMessage(If(Common.isEnglish, "Please enter the hexadecimal number content", "请输入十六进制数"))
				Return
			End If

			Dim entity As ISO15693Entity = hf15693API.Inventory()
			If entity Is Nothing Then
				ShowMessage(If(Common.isEnglish, "Card not found!", "寻卡失败!"))
				Return
			End If
			Dim result As Boolean = hf15693API.WriteDsfid(entity, Convert.ToByte(dsfid, 16))
			If Not result Then
				ShowMessage(If(Common.isEnglish, "failure", "写失败!"))
				Return
			End If
			ShowMessage(If(Common.isEnglish, "Success", "写成功!"))
		End Sub

		Private Sub btnDsfidLock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDsfidLock.Click
			If Not Me.btnDsfidLock.IsHandleCreated Then Return

			Dim dialogResult As DialogResult = MessageBox.Show(If(Common.isEnglish, "Are you sure you want to lock it?", "确定要锁吗？"),If(Common.isEnglish, "Lock", "锁"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If dialogResult = System.Windows.Forms.DialogResult.No Then
				Return
			End If

			Dim entity As ISO15693Entity = hf15693API.Inventory()
			If entity Is Nothing Then
				ShowMessage(If(Common.isEnglish, "Card not found!", "寻卡失败!"))
				Return
			End If

			Dim result As Boolean = hf15693API.LockDsfid()
			If Not result Then
				ShowMessage(If(Common.isEnglish, "failure", "锁失败!"))
				Return
			End If
			ShowMessage(If(Common.isEnglish, "Success", "锁成功!"))
		End Sub



		Private Sub txt15693Data_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt15693Data.TextChanged
			If Not Me.txt15693Data.IsHandleCreated Then Return

			FormatHex(txt15693Data)
		End Sub
	   #End Region

		#Region "PSAM"
			Private Sub btnInit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInit.Click
				If Not Me.btnInit.IsHandleCreated Then Return

				txtPsamReceive.Text = ""
				Dim card As Integer = If(rbCard1.Checked, 0, 1)
				If Not psam.Init(CByte(card)) Then
					ShowMessage(If(Common.isEnglish, "Initialization failed", "初始化失败"))
					Return
				End If
				Dim resetData() As Byte=psam.Reset(CByte(card))
				If resetData Is Nothing Then
					ShowMessage(If(Common.isEnglish, "failure", "复位失败"))
					Return
				End If
				txtPsamReceive.Text = DataConvert.ByteArrayToHexString(resetData)
				btnPsamSend.Enabled = True
				btnInit.Enabled = False
				btnFree.Enabled = True
				rbCard1.Enabled = False
				rbCard2.Enabled = False
				ShowMessage(If(Common.isEnglish, "Success", "成功"))

			End Sub

			Private Sub btnFree_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFree.Click
				If Not Me.btnFree.IsHandleCreated Then Return

				Dim card As Integer = If(rbCard1.Checked, 0, 1)
				If psam.Free(CByte(card)) Then
					btnPsamSend.Enabled = False
					btnInit.Enabled = True
					btnFree.Enabled = False
					rbCard1.Enabled = True
					rbCard2.Enabled = True
					ShowMessage(If(Common.isEnglish, "Success", "成功"))
					Return
				End If
				ShowMessage(If(Common.isEnglish, "failure", "失败"))
			End Sub

			Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPsamSend.Click
				If Not Me.btnPsamSend.IsHandleCreated Then Return

				txtPsamReceive.Text = ""
				Dim psamData As String = txtPsamData.Text.Replace(" ", "")

				If psamData.Length = 0 Then
					ShowMessage(If(Common.isEnglish, "The content cannot be empty", "内容不能为空"))
					Return
				ElseIf Not StringUtils.IsHexNumber(psamData) Then
					ShowMessage(If(Common.isEnglish, "Please enter the hexadecimal number content", "请输入十六进制数"))
					Return
				End If

				Dim card As Integer = If(rbCard1.Checked, 0, 1)
				Dim data() As Byte = psam.TransferCmd(CByte(card), DataConvert.HexStringToByteArray(psamData))
				If data Is Nothing Then
					ShowMessage(If(Common.isEnglish, "failure", "失败"))
					Return
				End If
				txtPsamReceive.Text = DataConvert.ByteArrayToHexString(data)
				ShowMessage(If(Common.isEnglish, "Success", "成功"))
			End Sub

		#End Region

		Private Sub ShowMessage(ByVal msg As String)

				If True Then
					Dim f As New frmWaitingBox(Sub(obj, args)
						System.Threading.Thread.Sleep(500)
					End Sub, msg)
					f.ShowDialog(Me)
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
		Private Sub RFIDMainForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
			If e.KeyCode = Keys.Back Then
				isDelete = True
			Else
				isDelete = False
			End If
		End Sub

		Private Sub txt14443ACPUData_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt14443ACPUData.TextChanged
			If Not Me.txt14443ACPUData.IsHandleCreated Then Return

			FormatHex(txt14443ACPUData)
		End Sub

		Private Sub txtPsamData_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtPsamData.TextChanged
			If Not Me.txtPsamData.IsHandleCreated Then Return

			FormatHex(txt14443ACPUData)
		End Sub

		Private Sub btn14443B_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn14443B.Click
			If Not Me.btn14443B.IsHandleCreated Then Return


			Dim data() As Byte=hfAPI.ResetTypeB()
			If data Is Nothing Then
				ShowMessage(If(Common.isEnglish, "reset fail!", "重置卡片失败!"))
				Return
			End If

			txt14443BReceive.Text = ""
			Dim hexData As String = txt14443BSendData.Text.Replace(" ", "")
			If hexData.Length = 0 Then
				ShowMessage(If(Common.isEnglish, "Content to be send can not be empty!", "内容不能为空"))
				Return
			ElseIf Not StringUtils.IsHexNumber(hexData) Then
				ShowMessage(If(Common.isEnglish, "Please enter the hexadecimal number content", "请输入十六进制数"))
				Return
			End If

			Dim result() As Byte = hfAPI.CpuCommand(DataConvert.HexStringToByteArray(hexData))
			If result Is Nothing Then
				ShowMessage(If(Common.isEnglish, "Send failure", "命令发送出错!"))
				Return
			End If

			txt14443BReceive.Text = DataConvert.ByteArrayToHexString(result)
			ShowMessage(If(Common.isEnglish, "Success", "命令发送成功!"))

		End Sub









	End Class
End Namespace
