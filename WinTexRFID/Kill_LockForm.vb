Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports WinForm_Test
Imports System.Collections

Namespace UHFAPP
	Partial Public Class Kill_LockForm
		Inherits BaseForm

		Private comboAction1 As String = "可读可写"
		Private comboAction2 As String = "可读不可写"
		Private comboAction3 As String = "不可读可写"
		Private comboAction4 As String = "不可读不可写"

		Private comboAction5 As String = "保留"
		Private comboAction6 As String = "不需要鉴别"
		Private comboAction7 As String = "需要鉴别,不需要安全通信"
		Private comboAction8 As String = "需要鉴别,需要安全通信"

		Private txtUserNumber As String = "用户区编号:"

		Private userNumber As Integer = 0

		Public Sub New()
			InitializeComponent()
		End Sub
		Public Sub New(ByVal isOpen As Boolean)
			InitializeComponent()
			If isOpen Then
				panel1.Enabled = True
			Else
				panel1.Enabled = False
			End If
		End Sub
		Private Sub MainForm_eventOpen(ByVal open As Boolean)
			If open Then
				panel1.Enabled = True
			Else
				panel1.Enabled = False
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
		#Region "过滤"
		Private Sub txtFilter_EPC_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilter_EPC.TextChanged
			FormatHex(txtFilter_EPC)
			Dim data As String = txtFilter_EPC.Text.Replace(" ", "")
			If data.Length > 0 Then
				label29.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				label29.Text = "0"
			End If
		End Sub

		Private Sub rbEPC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbEPC.Click
			txtPtr.Text = "32"
		End Sub

		Private Sub rbTID_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbTID.Click
			txtPtr.Text = "0"
		End Sub

		Private Sub rbUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbUser.Click
			txtPtr.Text = "0"
		End Sub
		Private Sub txtPtr_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtPtr.TextChanged
			Dim txt As TextBox = DirectCast(sender, TextBox)
			If Not StringUtils.IsNumber(txt.Text) Then
				If rbEPC.Checked Then
					txt.Text = "32"
				Else
					txt.Text = "0"
				End If
			End If
		End Sub

		Private Sub txtLen_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtLen.TextChanged
			Dim txt As TextBox = DirectCast(sender, TextBox)
			If Not StringUtils.IsNumber(txt.Text) Then
				txt.Text = "0"
			End If
		End Sub
		#End Region


		#Region "BlockPermalock"

		Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button3.Click

			If Not DetectionFiltration() Then
				Return
			End If

			Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
			Dim accessPwd As String = txtBlockPermalockPwd.Text.Replace(" ", "")

			If accessPwd.Length <> 8 Then
				MessageBoxEx.Show(Me,If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须为8位!"))
				Return
			End If
			Dim bank As Integer = cmbBlockPermalockBank.SelectedIndex
			Dim pwd() As Byte = DataConvert.HexStringToByteArray(accessPwd)
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim Ptr As Integer = Integer.Parse(txtBlockPermalockPtr.Text)

			Dim FilterStartaddr As Integer = Integer.Parse(txtPtr.Text)
			Dim FilterLength As Integer = Integer.Parse(txtLen.Text)
			Dim FilterBank As Integer = 1
			If rbTID.Checked Then
				FilterBank = 2
			ElseIf rbUser.Checked Then
				FilterBank = 3
			End If


			Dim ReadLock As Integer = cmbBlockPermalockReadLock.SelectedIndex
			Dim uRange As Integer = Ptr + 1

			Dim data(15) As Integer
			If cbBlock1.Checked Then
				data(0) = 1
			End If
			If cbBlock2.Checked Then
				data(1) = 1
			End If
			If cbBlock3.Checked Then
				data(2) = 1
			End If
			If cbBlock4.Checked Then
				data(3) = 1
			End If
			If cbBlock5.Checked Then
				data(4) = 1
			End If
			If cbBlock6.Checked Then
				data(5) = 1
			End If
			If cbBlock7.Checked Then
				data(6) = 1
			End If
			If cbBlock8.Checked Then
				data(7) = 1
			End If
			If cbBlock9.Checked Then
				data(8) = 1
			End If
			If cbBlock10.Checked Then
				data(9) = 1
			End If
			If cbBlock11.Checked Then
				data(10) = 1
			End If
			If cbBlock12.Checked Then
				data(11) = 1
			End If
			If cbBlock13.Checked Then
				data(12) = 1
			End If
			If cbBlock14.Checked Then
				data(13) = 1
			End If
			If cbBlock15.Checked Then
				data(14) = 1
			End If
			If cbBlock16.Checked Then
				data(15) = 1
			End If


			Dim sb As New StringBuilder()
			For k As Integer = 0 To data.Length - 1
				sb.Append(data(k))
			Next k
			Dim uMaskbuf(1) As Byte
			uMaskbuf(0) = Convert.ToByte(sb.ToString().Substring(0, 8), 2)
			uMaskbuf(1) = Convert.ToByte(sb.ToString().Substring(8, 8), 2)


			label8.Text = "Maskbuf:" & DataConvert.ByteArrayToHexString(uMaskbuf)

			Dim msg As String = ""
			If uhf.BlockPermalock(pwd, CByte(FilterBank), FilterStartaddr, FilterLength, filerBuff, CByte(ReadLock), CByte(bank), Ptr, CByte(uRange), uMaskbuf) Then
				msg = "success"
				If ReadLock = 0 Then
					label8.Text = "Maskbuf:" & DataConvert.ByteArrayToHexString(uMaskbuf)
					Dim str1 As String = System.Convert.ToString(uMaskbuf(0), 2)
					str1 = "0000000".Substring(0, 8 - str1.Length) & str1
					Dim str2 As String = System.Convert.ToString(uMaskbuf(1), 2)
					str2 = "0000000".Substring(0, 8 - str2.Length) & str2
					cbBlock1.Checked = (If(str1.Substring(0, 1) = "1", True, False))
					cbBlock2.Checked = (If(str1.Substring(1, 1) = "1", True, False))
					cbBlock3.Checked = (If(str1.Substring(2, 1) = "1", True, False))
					cbBlock4.Checked = (If(str1.Substring(3, 1) = "1", True, False))
					cbBlock5.Checked = (If(str1.Substring(4, 1) = "1", True, False))
					cbBlock6.Checked = (If(str1.Substring(5, 1) = "1", True, False))
					cbBlock7.Checked = (If(str1.Substring(6, 1) = "1", True, False))
					cbBlock8.Checked = (If(str1.Substring(7, 1) = "1", True, False))

					cbBlock9.Checked = (If(str2.Substring(0, 1) = "1", True, False))
					cbBlock10.Checked = (If(str2.Substring(1, 1) = "1", True, False))
					cbBlock11.Checked = (If(str2.Substring(2, 1) = "1", True, False))
					cbBlock12.Checked = (If(str2.Substring(3, 1) = "1", True, False))
					cbBlock13.Checked = (If(str2.Substring(4, 1) = "1", True, False))
					cbBlock14.Checked = (If(str2.Substring(5, 1) = "1", True, False))
					cbBlock15.Checked = (If(str2.Substring(6, 1) = "1", True, False))
					cbBlock16.Checked = (If(str2.Substring(7, 1) = "1", True, False))
				End If
			Else
				msg = "failure"
			End If
			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(500)
			End Sub, msg)
			f.ShowDialog(Me)
		End Sub



		Private Sub txtBlockPermalockPtr_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtBlockPermalockPtr.TextChanged
			Dim txt As TextBox = DirectCast(sender, TextBox)
			If Not StringUtils.IsNumber(txt.Text) Then
				txt.Text = "0"
			End If
			Dim index As Integer = cmbBlockPermalockBank.SelectedIndex
			If index = 0 Then
				If Integer.Parse(txt.Text) < 2 Then
					txt.Text = "2"
				End If
			End If
		End Sub



		Private Sub txtBlockPermalockPwd_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtBlockPermalockPwd.TextChanged
			FormatHex_PWD(txtBlockPermalockPwd)
		End Sub

		Private Sub cmbBlockPermalockBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbBlockPermalockBank.TextChanged

			Dim index As Integer = cmbBlockPermalockBank.SelectedIndex
			If index = 1 Then
				If Integer.Parse(txtBlockPermalockPtr.Text) < 2 Then
					txtBlockPermalockPtr.Text = "2"
				End If
			End If

			If index = 3 Then
				enabledBlock(False)
			Else
				enabledBlock(True)
			End If
		End Sub

		#End Region
		#Region "lock"



		Private Sub txtLockPwd_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtLockPwd.TextChanged
			FormatHex_PWD(txtLockPwd)
		End Sub


		Private Sub button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button5.Click
			If Not DetectionFiltration() Then
				Return
			End If

			Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
			Dim accessPwd As String = txtLockPwd.Text.Replace(" ", "")
			Dim FilterStartaddr As Integer = Integer.Parse(txtPtr.Text)
			Dim FilterLength As Integer = Integer.Parse(txtLen.Text)


			If accessPwd.Length <> 8 Then
				MessageBoxEx.Show(Me,If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须为8位!"))
				Return
			End If

			If (FilterLength \ 8 + (If(FilterLength Mod 8 = 0, 0, 1))) * 2 > filerData.Length Then
				MessageBox.Show(If(Common.isEnglish, "filter data length error!", "过滤数据和长度不匹配!")) 'to do
				Return
			End If

			Dim pwd() As Byte = DataConvert.HexStringToByteArray(accessPwd)
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)



			Dim FilterBank As Integer = 1
			If rbTID.Checked Then
				FilterBank = 2
			ElseIf rbUser.Checked Then
				FilterBank = 3
			End If

			Dim lockbuf(2) As Byte

			Dim ilockCode(19) As Integer
			If cbKill.Checked Then
				If rbTemporaryOpen.Checked OrElse rbTemporaryLock.Checked Then
					ilockCode(19) = 1
					ilockCode(9) = If(rbTemporaryOpen.Checked, 0, 1)
				ElseIf rbPermanentOpen.Checked OrElse rbPermanentLock.Checked Then
					ilockCode(18) = 1
					ilockCode(8) = 1
					ilockCode(19) = 1
					ilockCode(9) = If(rbPermanentOpen.Checked, 0, 1)
				End If
			End If
			If cbAccess.Checked Then
				If rbTemporaryOpen.Checked OrElse rbTemporaryLock.Checked Then
					ilockCode(17) = 1
					ilockCode(7) = If(rbTemporaryOpen.Checked, 0, 1)
				ElseIf rbPermanentOpen.Checked OrElse rbPermanentLock.Checked Then
					ilockCode(16) = 1
					ilockCode(6) = 1
					ilockCode(17) = 1
					ilockCode(7) = If(rbPermanentOpen.Checked, 0, 1)
				End If
			End If
			If cbEPC.Checked Then
				If rbTemporaryOpen.Checked OrElse rbTemporaryLock.Checked Then
					ilockCode(15) = 1
					ilockCode(5) = If(rbTemporaryOpen.Checked, 0, 1)
				ElseIf rbPermanentOpen.Checked OrElse rbPermanentLock.Checked Then
					ilockCode(14) = 1
					ilockCode(4) = 1
					ilockCode(15) = 1
					ilockCode(5) = If(rbPermanentOpen.Checked, 0, 1)
				End If
			End If
			If cbTID.Checked Then
				If rbTemporaryOpen.Checked OrElse rbTemporaryLock.Checked Then
					ilockCode(13) = 1
					ilockCode(3) = If(rbTemporaryOpen.Checked, 0, 1)
				ElseIf rbPermanentOpen.Checked OrElse rbPermanentLock.Checked Then
					ilockCode(2) = 1
					ilockCode(12) = 1
					ilockCode(13) = 1
					ilockCode(3) = If(rbPermanentOpen.Checked, 0, 1)
				End If
			End If
			If cbUser.Checked Then
				If rbTemporaryOpen.Checked OrElse rbTemporaryLock.Checked Then
					ilockCode(11) = 1
					ilockCode(1) = If(rbTemporaryOpen.Checked, 0, 1)
				ElseIf rbPermanentOpen.Checked OrElse rbPermanentLock.Checked Then
					ilockCode(0) = 1
					ilockCode(10) = 1
					ilockCode(11) = 1
					ilockCode(1) = If(rbPermanentOpen.Checked, 0, 1)
				End If
			End If

			Dim sb As New StringBuilder()
			sb.Append("0000")
			For k As Integer = ilockCode.Length-1 To 0 Step -1
				sb.Append(ilockCode(k))
			Next k
			lockbuf(0)=Convert.ToByte(sb.ToString().Substring(0,8),2)
			lockbuf(1) = Convert.ToByte(sb.ToString().Substring(8, 8), 2)
			lockbuf(2) = Convert.ToByte(sb.ToString().Substring(16, 8), 2)

			label7.Text ="LockData:" & DataConvert.ByteArrayToHexString(lockbuf)
			Dim msg As String = ""
			If uhf.LockTag(pwd, CByte(FilterBank), FilterStartaddr, FilterLength, filerBuff, lockbuf) Then
				msg = If(Common.isEnglish, "success", "成功!")
			Else
				msg = If(Common.isEnglish, "failure", "失败!")
			End If
			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(500)
			End Sub, msg)
			f.ShowDialog(Me)

		End Sub

		#End Region
		#Region "kill"

		Private Sub txtRead_Epc_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
			FormatHex(txtFilter_EPC)
		End Sub

		Private Sub txtRead_AccessPwd_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtKill_AccessPwd.TextChanged
			FormatHex_PWD(txtKill_AccessPwd)
		End Sub
		Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
			If Not DetectionFiltration() Then
				Return
			End If

			Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
			Dim accessPwd As String = txtKill_AccessPwd.Text.Replace(" ", "")
			Dim pwd() As Byte = DataConvert.HexStringToByteArray(accessPwd)
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim FilterStartaddr As Integer = Integer.Parse(txtPtr.Text)
			Dim FilterLength As Integer = Integer.Parse(txtLen.Text)
			Dim FilterBank As Integer = 1
			If rbTID.Checked Then
				FilterBank = 2
			ElseIf rbUser.Checked Then
				FilterBank = 3
			End If

			If accessPwd.Length <> 8 Then
				MessageBoxEx.Show(Me,If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须为8位!"))
				Return
			End If
			If uhf.KillTag(pwd, CByte(FilterBank), FilterStartaddr, FilterLength, filerBuff) Then
				MessageBoxEx.Show(Me,If(Common.isEnglish, "Kill success!", "Kill成功!"))
			Else
				MessageBoxEx.Show(Me,If(Common.isEnglish, "Kill failure!", "Kill失败!"))
			End If
		End Sub
		#End Region

		Private Sub Kill_LockForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			lock_CheckedChanged(Nothing,Nothing)
			AddHandler MainForm.eventMainSizeChanged, AddressOf MainForm_SizeChanged
			AddHandler MainForm.eventOpen, AddressOf MainForm_eventOpen
			cmbBlockPermalockReadLock.SelectedIndex = 0
			cmbBlockPermalockBank.SelectedIndex = 3
			enabledBlock(False)

			AddHandler MainForm.eventSwitchUI, AddressOf MainForm_eventSwitchUI
			MainForm_eventSwitchUI()
			txtFilter_EPC.Text = Common.tag
			textBox2.Text = "E003"

			If txtFilter_EPC.Text.Replace(" ", "").Length > 0 Then
				txtLen.Text = (txtFilter_EPC.Text.Replace(" ","").Length\2 *8) & ""
			End If
			label17.Text = ""


			Dim hashtable As Hashtable = Common.GetControlValues(Me.Name)
			If hashtable IsNot Nothing Then
				Me.txtFilter_EPC.Text = DirectCast(hashtable(txtFilter_EPC.Name), String)
				Me.txtPtr.Text = DirectCast(hashtable(txtPtr.Name), String)
				Me.txtLen.Text = DirectCast(hashtable(txtLen.Name), String)
				Me.rbEPC.Checked = DirectCast(hashtable(rbEPC.Name), Boolean)
				Me.rbTID.Checked = DirectCast(hashtable(rbTID.Name), Boolean)
				Me.rbUser.Checked = DirectCast(hashtable(rbUser.Name), Boolean)

				Me.txtLockPwd.Text = DirectCast(hashtable(txtLockPwd.Name), String)
				Me.rbTemporaryOpen.Checked = DirectCast(hashtable(rbTemporaryOpen.Name), Boolean)
				Me.rbTemporaryLock.Checked = DirectCast(hashtable(rbTemporaryLock.Name), Boolean)
				Me.rbPermanentOpen.Checked = DirectCast(hashtable(rbPermanentOpen.Name), Boolean)
				Me.rbPermanentLock.Checked = DirectCast(hashtable(rbPermanentLock.Name), Boolean)

				Me.label7.Text = DirectCast(hashtable(label7.Name), String)
				Me.txtGBPWD.Text = DirectCast(hashtable(txtGBPWD.Name), String)
				Me.comboBank.SelectedIndex = DirectCast(hashtable(comboBank.Name), Integer)
				Me.comboConfig.SelectedIndex = DirectCast(hashtable(comboConfig.Name), Integer)
				Me.comboAction.SelectedIndex = DirectCast(hashtable(comboAction.Name), Integer)
				Me.cmbBlockPermalockBank.SelectedIndex = DirectCast(hashtable(cmbBlockPermalockBank.Name), Integer)
				Me.txtBlockPermalockPtr.Text = DirectCast(hashtable(txtBlockPermalockPtr.Name), String)
				Me.txtBlockPermalockPwd.Text = DirectCast(hashtable(txtBlockPermalockPwd.Name), String)
				Me.cmbBlockPermalockReadLock.SelectedIndex = DirectCast(hashtable(cmbBlockPermalockReadLock.Name), Integer)
				Me.cbBlock1.Checked = DirectCast(hashtable(cbBlock1.Name), Boolean)
				Me.cbBlock2.Checked = DirectCast(hashtable(cbBlock2.Name), Boolean)
				Me.cbBlock3.Checked = DirectCast(hashtable(cbBlock3.Name), Boolean)
				Me.cbBlock4.Checked = DirectCast(hashtable(cbBlock4.Name), Boolean)
				Me.cbBlock5.Checked = DirectCast(hashtable(cbBlock5.Name), Boolean)
				Me.cbBlock6.Checked = DirectCast(hashtable(cbBlock6.Name), Boolean)
				Me.cbBlock7.Checked = DirectCast(hashtable(cbBlock7.Name), Boolean)
				Me.cbBlock8.Checked = DirectCast(hashtable(cbBlock8.Name), Boolean)
				Me.cbBlock9.Checked = DirectCast(hashtable(cbBlock9.Name), Boolean)
				Me.cbBlock10.Checked = DirectCast(hashtable(cbBlock10.Name), Boolean)
				Me.cbBlock11.Checked = DirectCast(hashtable(cbBlock11.Name), Boolean)
				Me.cbBlock12.Checked = DirectCast(hashtable(cbBlock12.Name), Boolean)
				Me.cbBlock13.Checked = DirectCast(hashtable(cbBlock13.Name), Boolean)
				Me.cbBlock14.Checked = DirectCast(hashtable(cbBlock14.Name), Boolean)
				Me.cbBlock15.Checked = DirectCast(hashtable(cbBlock15.Name), Boolean)
				Me.cbBlock16.Checked = DirectCast(hashtable(cbBlock16.Name), Boolean)
				Me.label8.Text = DirectCast(hashtable(label8.Name), String)
				Me.txtKill_AccessPwd.Text = DirectCast(hashtable(txtKill_AccessPwd.Name), String)
			End If
		End Sub

		Private Sub Kill_LockForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
			RemoveHandler MainForm.eventMainSizeChanged, AddressOf MainForm_SizeChanged
			Common.SaveControlValues(Me)
			RemoveHandler MainForm.eventOpen, AddressOf MainForm_eventOpen
			RemoveHandler MainForm.eventSwitchUI, AddressOf MainForm_eventSwitchUI
		End Sub

		Private Sub lock_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbUser.Click, cbTID.Click, cbEPC.Click, cbAccess.Click, cbKill.Click, rbPermanentLock.Click, rbPermanentOpen.Click, rbTemporaryLock.Click, rbTemporaryOpen.Click
			Dim lockbuf(2) As Byte

			Dim ilockCode(19) As Integer
			If cbKill.Checked Then
				If rbTemporaryOpen.Checked OrElse rbTemporaryLock.Checked Then
					ilockCode(19) = 1
					ilockCode(9) = If(rbTemporaryOpen.Checked, 0, 1)
				ElseIf rbPermanentOpen.Checked OrElse rbPermanentLock.Checked Then
					ilockCode(18) = 1
					ilockCode(8) = 1
					ilockCode(19) = 1
					ilockCode(9) = If(rbPermanentOpen.Checked, 0, 1)
				End If
			End If
			If cbAccess.Checked Then
				If rbTemporaryOpen.Checked OrElse rbTemporaryLock.Checked Then
					ilockCode(17) = 1
					ilockCode(7) = If(rbTemporaryOpen.Checked, 0, 1)
				ElseIf rbPermanentOpen.Checked OrElse rbPermanentLock.Checked Then
					ilockCode(16) = 1
					ilockCode(6) = 1
					ilockCode(17) = 1
					ilockCode(7) = If(rbPermanentOpen.Checked, 0, 1)
				End If
			End If
			If cbEPC.Checked Then
				If rbTemporaryOpen.Checked OrElse rbTemporaryLock.Checked Then
					ilockCode(15) = 1
					ilockCode(5) = If(rbTemporaryOpen.Checked, 0, 1)
				ElseIf rbPermanentOpen.Checked OrElse rbPermanentLock.Checked Then
					ilockCode(14) = 1
					ilockCode(4) = 1
					ilockCode(15) = 1
					ilockCode(5) = If(rbPermanentOpen.Checked, 0, 1)
				End If
			End If
			If cbTID.Checked Then
				If rbTemporaryOpen.Checked OrElse rbTemporaryLock.Checked Then
					ilockCode(13) = 1
					ilockCode(3) = If(rbTemporaryOpen.Checked, 0, 1)
				ElseIf rbPermanentOpen.Checked OrElse rbPermanentLock.Checked Then
					ilockCode(12) = 1
					ilockCode(12) = 1
					ilockCode(13) = 1
					ilockCode(3) = If(rbPermanentOpen.Checked, 0, 1)
				End If
			End If
			If cbUser.Checked Then
				If rbTemporaryOpen.Checked OrElse rbTemporaryLock.Checked Then
					ilockCode(11) = 1
					ilockCode(1) = If(rbTemporaryOpen.Checked, 0, 1)
				ElseIf rbPermanentOpen.Checked OrElse rbPermanentLock.Checked Then
					ilockCode(0) = 1
					ilockCode(10) = 1
					ilockCode(11) = 1
					ilockCode(1) = If(rbPermanentOpen.Checked, 0, 1)
				End If
			End If

			Dim sb As New StringBuilder()
			sb.Append("0000")
			For k As Integer = ilockCode.Length - 1 To 0 Step -1
				sb.Append(ilockCode(k))
			Next k
			lockbuf(0) = Convert.ToByte(sb.ToString().Substring(0, 8), 2)
			lockbuf(1) = Convert.ToByte(sb.ToString().Substring(8, 8), 2)
			lockbuf(2) = Convert.ToByte(sb.ToString().Substring(16, 8), 2)

			label7.Text = "LockData:" & DataConvert.ByteArrayToHexString(lockbuf)
		End Sub



		Private Sub FormatHex_PWD(ByVal txt As TextBox)
			If isDelete Then
				Return
			End If
			Dim data As String = txt.Text.Trim().Replace(" ", "")
			If data <> String.Empty Then
				If data.Length > 8 Then
					data = data.Substring(0, 8)
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

		Private Sub cbBlock1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cbBlock16.Click, cbBlock15.Click, cbBlock14.Click, cbBlock13.Click, cbBlock12.Click, cbBlock11.Click, cbBlock10.Click, cbBlock9.Click, cbBlock8.Click, cbBlock7.Click, cbBlock6.Click, cbBlock5.Click, cbBlock4.Click, cbBlock3.Click, cbBlock2.Click, cbBlock1.Click

			Dim data(15) As Integer
			If cbBlock1.Checked Then
				data(0) = 1
			End If
			If cbBlock2.Checked Then
				data(1) = 1
			End If
			If cbBlock3.Checked Then
				data(2) = 1
			End If
			If cbBlock4.Checked Then
				data(3) = 1
			End If
			If cbBlock5.Checked Then
				data(4) = 1
			End If
			If cbBlock6.Checked Then
				data(5) = 1
			End If
			If cbBlock7.Checked Then
				data(6) = 1
			End If
			If cbBlock8.Checked Then
				data(7) = 1
			End If
			If cbBlock9.Checked Then
				data(8) = 1
			End If
			If cbBlock10.Checked Then
				data(9) = 1
			End If
			If cbBlock11.Checked Then
				data(10) = 1
			End If
			If cbBlock12.Checked Then
				data(11) = 1
			End If
			If cbBlock13.Checked Then
				data(12) = 1
			End If
			If cbBlock14.Checked Then
				data(13) = 1
			End If
			If cbBlock15.Checked Then
				data(14) = 1
			End If
			If cbBlock16.Checked Then
				data(15) = 1
			End If


			Dim sb As New StringBuilder()
			For k As Integer = 0 To data.Length - 1
				sb.Append(data(k))
			Next k
			Dim uMaskbuf(1) As Byte
			uMaskbuf(0) = Convert.ToByte(sb.ToString().Substring(0, 8), 2)
			uMaskbuf(1) = Convert.ToByte(sb.ToString().Substring(8, 8), 2)


			label8.Text = "Maskbuf:" & DataConvert.ByteArrayToHexString(uMaskbuf)
		End Sub


		Private Sub enabledBlock(ByVal enable As Boolean)

			cbBlock16.Enabled = enable
			cbBlock10.Enabled = enable
			cbBlock11.Enabled = enable
			cbBlock12.Enabled = enable
			cbBlock13.Enabled = enable
			cbBlock14.Enabled = enable
			cbBlock15.Enabled = enable
			cbBlock16.Enabled = enable
			cbBlock9.Enabled = enable
		End Sub

		Private Sub MainForm_eventSwitchUI()

			Dim comboBank_index As Integer = comboBank.SelectedIndex
			Dim comboConfig_index As Integer = comboConfig.SelectedIndex
			Dim comboAction_index As Integer = comboAction.SelectedIndex

			If Common.isEnglish Then
				groupBox2.Text = "lock"

				label2.Text = "Access Pwd:"
				label4.Text = "Can't use the default password"
				rbTemporaryOpen.Text = "Open"
				rbTemporaryLock.Text = "Lock"
				rbPermanentOpen.Text = "Permanent Open"
				rbPermanentLock.Text = "Permanent Lock"
				cbKill.Text = "Kill-pwd"
				cbAccess.Text = "Access-pwd"
				button5.Text = "Confirm"

				groupBox1.Text = "Kill"

				label19.Text = "Access Pwd:"
				label5.Text = "Access Pwd:"
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: label18.Text=label1.Text = label6.Text = "Can't use the default password";
				label6.Text = "Can't use the default password"
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: label18.Text=label1.Text = label6.Text
				label1.Text = label6.Text
				label18.Text=label1.Text
				button2.Text = "kill"

				label14.Text = "Bank:"
				label15.Text = "Config"
				label16.Text = "Action:"

				label26.Text = "Bank:"
				label25.Text = "Ptr:"
				label23.Text = "Access-pwd:"
				label20.Text = "ReadLock:"
				button3.Text = "Confirm"
				label9.Text = "Access-pwd:"
				label10.Text = "cmd:"

				button1.Text = "Confirm"
				groupBox6.Text = "filter"
				button4.Text = "Confirm"

				groupBox9.Text = "GB/GJB Lock"

				 comboBank.Items.Clear()
				 comboBank.Items.Add("TagInfo")
				 comboBank.Items.Add("Encode")
				 comboBank.Items.Add("Secure")
				 comboBank.Items.Add("User")

				 comboConfig.Items.Clear()
				 comboConfig.Items.Add("Storage area property")
				 comboConfig.Items.Add("Secure")

				comboAction1 = "Read-write"
				comboAction2 = "Read only"
				comboAction3 = "Write only"
				comboAction4 = "Unreadable and Writable"

				comboAction5 = "Reserved"
				comboAction6 = "No identification is required."
				comboAction7 = "Need authentication, no need for secure communication"
				comboAction8 = "Need identification, need secure communication"

				txtUserNumber = "User Area Number:"
			Else

				groupBox2.Text = "锁"

				label2.Text = "访问密码:"
				label4.Text = "不能使用默认密码"
				rbTemporaryOpen.Text = "开放"
				rbTemporaryLock.Text = "锁"
				rbPermanentOpen.Text = "永久开放"
				rbPermanentLock.Text = "永久锁定"
				button5.Text = "确定"

				groupBox1.Text = "Kill"

				label19.Text = "访问密码:"
				label5.Text = "访问密码:"
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: label18.Text=label1.Text = label6.Text = "不能使用默认密码";
				label6.Text = "不能使用默认密码"
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: label18.Text=label1.Text = label6.Text
				label1.Text = label6.Text
				label18.Text=label1.Text
				button2.Text = "kill标签"

				label14.Text = "存储区域:"
				label15.Text = "配置:"
				label16.Text = "动作:"

				label26.Text = "存储区域:"
				label25.Text = "起始地址:"
				label23.Text = "访问密码:"
				label20.Text = "操作方式:"
				button3.Text = "确定"
				groupBox6.Text = "过滤"

				label9.Text = "密码:"
				label10.Text = "命令:"
				button1.Text="确定"
				button4.Text = "确定"


				comboAction1 = "可读可写"
				comboAction2 = "可读不可写"
				comboAction3 = "不可读可写"
				comboAction4 = "不可读不可写"

				comboAction5 = "保留"
				comboAction6 = "不需要鉴别"
				comboAction7 = "需要鉴别,不需要安全通信"
				comboAction8 = "需要鉴别,需要安全通信"

				txtUserNumber = "用户区编号:"
				groupBox9.Text = "国标/国军标 Lock"

				comboBank.Items.Clear()
				comboBank.Items.Add("标签信息区")
				comboBank.Items.Add("编码区")
				comboBank.Items.Add("安全区")
				comboBank.Items.Add("用户区")

				comboConfig.Items.Clear()
				comboConfig.Items.Add("存储区属性")
				comboConfig.Items.Add("安全模式")
			End If

			If comboBank_index >= 0 Then
				comboBank.SelectedIndex = comboBank_index
			End If

			If comboConfig_index >= 0 Then
				comboConfig.SelectedIndex = comboConfig_index
			End If

			If comboAction_index >= 0 Then
				comboAction.SelectedIndex = comboAction_index
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

		#Region "Deactivate/re-activation"
			Private Sub textBox2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles textBox2.TextChanged
				'FormatHex(textBox2);
			End Sub

			Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
				If Not DetectionFiltration() Then
					Return
				End If

				Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
				Dim accessPwd As String = textBox1.Text.Replace(" ", "")
				Dim cmd As String = textBox2.Text.Replace(" ","")
				If accessPwd.Length <> 8 Then
					MessageBoxEx.Show(Me,If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须为8位!"))
					Return
				End If
				If cmd.Length <> 4 Then
					MessageBoxEx.Show(Me,If(Common.isEnglish, "The length of the cmd must be 4!", "命令必须是两个字节!"))
					Return
				End If

				If Not StringUtils.IsHexNumber(cmd) Then
					MessageBox.Show(If(Common.isEnglish, "Please input the hex cmd!", "请输入十六进制命令!"))
					Return
				End If


				Dim pwd() As Byte = DataConvert.HexStringToByteArray(accessPwd)
				Dim FilterStartaddr As Integer = Integer.Parse(txtPtr.Text)
				Dim FilterLength As Integer = Integer.Parse(txtLen.Text)
				Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
				Dim FilterBank As Integer = 1
				If rbTID.Checked Then
					FilterBank = 2
				ElseIf rbUser.Checked Then
					FilterBank = 3
				End If

				Dim icmd As Integer = Int32.Parse(cmd, System.Globalization.NumberStyles.HexNumber)

				If uhf.Deactivate(icmd, pwd, CByte(FilterBank), FilterStartaddr, FilterLength, filerBuff) Then
					MessageBoxEx.Show(Me,If(Common.isEnglish, "success!", "成功!"))
				Else
					MessageBoxEx.Show(Me,If(Common.isEnglish, "fail!", "失败!"))
				End If
			End Sub
			Private Sub textBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles textBox1.TextChanged
				FormatHex_PWD(textBox1)
			End Sub
		#End Region


			Private isDelete As Boolean = False
			Private Sub Kill_LockForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
				If e.KeyCode = Keys.Back Then
					isDelete = True
				Else
					isDelete = False
				End If
			End Sub
			#Region "国际标签锁"

				Private Sub txtGBPWD_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtGBPWD.TextChanged
					FormatHex_PWD(txtGBPWD)
				End Sub


				'配置
				Private Sub comboConfig_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles comboConfig.SelectedIndexChanged
					comboAction.Items.Clear()
					comboAction.Enabled = True
					If comboConfig.SelectedIndex = 0 Then
						comboAction.Items.Add(comboAction1)
						comboAction.Items.Add(comboAction2)
						comboAction.Items.Add(comboAction3)
						comboAction.Items.Add(comboAction4)
						comboAction.SelectedIndex = 0
					Else
						comboAction.Items.Add(comboAction5)
						comboAction.Items.Add(comboAction6)
						comboAction.Items.Add(comboAction7)
						comboAction.Items.Add(comboAction8)
						comboAction.SelectedIndex =1
					End If
				End Sub

				 '存储区
				Private Sub comboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles comboBank.SelectedIndexChanged
					If comboBank.SelectedIndex = 3 Then
						Dim choiceForm As New ChoiceNumberForm()
						choiceForm.StartPosition = FormStartPosition.CenterParent
						choiceForm.ShowDialog()
						userNumber = choiceForm.number
						label17.Text = txtUserNumber & userNumber

					Else
						label17.Text = ""
					End If
				End Sub

				Private Sub button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button4.Click
					If Not DetectionFiltration() Then
						Return
					End If

					Dim filerData As String = txtFilter_EPC.Text.Replace(" ", "")
					Dim accessPwd As String = txtGBPWD.Text.Replace(" ", "")
					Dim pwd() As Byte = DataConvert.HexStringToByteArray(accessPwd)
					Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
					Dim FilterStartaddr As Integer = Integer.Parse(txtPtr.Text)
					Dim FilterLength As Integer = Integer.Parse(txtLen.Text)
					Dim FilterBank As Integer = 1
					If rbTID.Checked Then
						FilterBank = 2
					ElseIf rbUser.Checked Then
						FilterBank = 3
					End If

					If accessPwd.Length <> 8 Then
						MessageBoxEx.Show(Me,If(Common.isEnglish, "The length of the password must be 8!", "密码长度必须为8位!"))
						Return
					End If

					If comboBank.SelectedIndex < 0 OrElse comboConfig.SelectedIndex < 0 OrElse comboAction.SelectedIndex < 0 Then

						MessageBoxEx.Show(Me,If(Common.isEnglish, "failure!", "失败!"))
						Return
					End If

					Dim memory As Byte = 0
					If comboBank.SelectedIndex = 0 Then
						memory = 0
					ElseIf comboBank.SelectedIndex = 1 Then
						memory = &H10
					ElseIf comboBank.SelectedIndex = 2 Then
						memory = &H20
					ElseIf comboBank.SelectedIndex = 3 Then
						memory = CByte(&H30 + userNumber)
					End If
					Dim config As Byte = CByte(comboConfig.SelectedIndex)
					Dim action As Byte = CByte(comboAction.SelectedIndex)

					Dim msg As String = ""
					If uhf.GBTagLock(pwd, CByte(FilterBank), FilterStartaddr, FilterLength, filerBuff, memory,config,action) Then
						msg = If(Common.isEnglish, "success", "成功!")
					Else
						msg = If(Common.isEnglish, "failure", "失败!")
					End If
					Dim f As New frmWaitingBox(Sub(obj, args)
						System.Threading.Thread.Sleep(500)
					End Sub, msg)
					f.ShowDialog(Me)

				End Sub


			#End Region

				Private Sub label15_Click(ByVal sender As Object, ByVal e As EventArgs) Handles label15.Click

				End Sub

				Private Sub label17_Click(ByVal sender As Object, ByVal e As EventArgs)

				End Sub

				Private Sub label17_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles label17.LinkClicked
					Dim choiceForm As New ChoiceNumberForm()
					choiceForm.StartPosition = FormStartPosition.CenterParent
					choiceForm.ShowDialog()
					userNumber = choiceForm.number
					label17.Text = txtUserNumber & userNumber
				End Sub

				Private Sub cbAccess_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)

				End Sub


				Private Sub MainForm_SizeChanged(ByVal state As FormWindowState)
					'判断是否选择的是最小化按钮
					panel1.Left = 308
				End Sub




	End Class



End Namespace
