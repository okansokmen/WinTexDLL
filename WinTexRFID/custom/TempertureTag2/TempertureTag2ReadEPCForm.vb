Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Threading
Imports System.Collections
Imports UHFAPP.utils
Imports UHFAPP.custom.TempertureTag2
Imports BLEDeviceAPI
Imports WinForm_Test

Namespace UHFAPP
	Partial Public Class TempertureTag2ReadEPCForm
		Inherits BaseForm

		Private tempertureTag As New TempertureTag2()
		Private mainform As TempertureTag2MainForm
		Private strStart As String = "盘点温度"
		Private strStart2 As String = "盘点温度"
		Private strStop As String = "停止盘点"
		Private strStop2 As String = "停止盘点"
		Private isRuning As Boolean = False
		Private isComplete As Boolean = True
		Private beginTime As Long = 0
		Private total As Integer = 0
		Private epcList As New List(Of EpcInfo)()
		' 将text更新的界面控件的委托类型
		Private Delegate Sub SetTextCallback(ByVal taginfo As TempertureInfo)
		Private setTextCallback2 As SetTextCallback
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
		Public Sub New(ByVal isOpen As Boolean, ByVal mainform As TempertureTag2MainForm)
			InitializeComponent()
			If isOpen Then
				panel1.Enabled = True
			Else
				panel1.Enabled = False
			End If
			Me.mainform = mainform
		End Sub

		Private Sub MainForm_eventOpen(ByVal open As Boolean)
			If open Then
				panel1.Enabled = True
				button11_Click(Nothing, Nothing)
			Else
				panel1.Enabled = False
				If btnScanEPC.Text = strStop Then
					StopEPC(True)
				End If
			End If
		End Sub
		Private Sub MainForm_eventSwitchUI()
			If Common.isEnglish Then
				contextMenuStrip1.Items(0).Text = "Copy Tag"
				groupBox8.Text = "Filter"
				label29.Text = "Data:"
				label30.Text = "Ptr:"
				lto.Text = "Tag Count:"
				label7.Text = "Total:"
				label2.Text = "Time:"
				button1.Text = "Clear"
				label1.Text = "len:"

				If btnScanEPC.Text = strStart2 Then
					btnScanEPC.Text = strStart
				ElseIf btnScanEPC.Text = strStop2 Then
					btnScanEPC.Text = strStop
				End If


				' label1.Location = new Point(785, 34); 
			Else
				contextMenuStrip1.Items(0).Text = "复制标签"
				groupBox8.Text = "过滤"
				label29.Text = "数据:"
				'label30.Text = "起始地址:";

				lto.Text = "标签数:"
				label7.Text = "次数:"
				label2.Text = "时间:"
				button1.Text = "清空"
				'label1.Text = "长度:";

				If btnScanEPC.Text = strStart Then
					btnScanEPC.Text = strStart2
				ElseIf btnScanEPC.Text = strStop Then
					btnScanEPC.Text = strStop2
				End If
				'  label30.Location = new Point(640, 33);
				'  label1.Location = new Point(801, 33);
			End If
		End Sub


		Private Sub ScanEPCForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

			AddHandler TempertureTag2MainForm.eventOpen, AddressOf MainForm_eventOpen
			setTextCallback2 = New SetTextCallback(AddressOf UpdataEPC)

			AddHandler TempertureTag2MainForm.eventSwitchUI, AddressOf MainForm_eventSwitchUI
			MainForm_eventSwitchUI()

			If mainform.isOpen Then
				panel1.Enabled = True
				button11_Click(Nothing, Nothing)
			End If

		End Sub

		Private Sub ScanEPCForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
			RemoveHandler MainForm2.eventOpen, AddressOf MainForm_eventOpen

			If btnScanEPC.Text = strStop OrElse btnScanEPC.Text = strStop2 Then
				StopEPC(True)
			End If
		End Sub
		#Region " 设置过滤"


		Private Sub txtData_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtData.TextChanged
			FormatHex(txtData)
			Dim data As String = txtData.Text.Replace(" ", "")
			If data.Length > 0 Then
				label5.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				label5.Text = "0"
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
		Private Sub txtPtr_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtPtr.TextChanged
			Try
				Dim ptr As String = txtPtr.Text
				If Not StringUtils.IsNumber(ptr) Then
					If rbEPC.Checked Then
						txtPtr.Text = "32"
					Else
						txtPtr.Text = "0"
					End If
					Return
				End If

			Catch ex As Exception
				If rbEPC.Checked Then
					txtPtr.Text = "32"
				Else
					txtPtr.Text = "0"
				End If
			End Try
		End Sub
		#End Region
		'start
		Private Sub btnScanEPC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnScanEPC.Click


			If btnScanEPC.Text = strStop OrElse btnScanEPC.Text = strStop2 Then
				StopEPC(True)
				Dim msg As String = If(Common.isEnglish, "wait...", "正在停止...")
				Dim f As New frmWaitingBox(Sub(obj, args)
					Thread.Sleep(1000)

				End Sub, msg)
				f.ShowDialog(Me)
			Else
				If ReadMultiClickNumber > 0 Then
					button1_Click(Nothing, Nothing)
				End If
				ReadMultiClickNumber = 0

				If Not isRuning AndAlso isComplete Then
					mainform.disableControls()
					isRuning = True
					isComplete = False
					If uhf.Inventory() Then

						StartEPC()
					Else
						MessageBoxEx.Show(Me, "Inventory failure!")
						isRuning = False
						isComplete = True
						mainform.enableControls()
					End If
				End If
			End If
		End Sub






		'Clear
		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			ReadMultiClickNumber = 0

			tempCount = 0
			label6.Text = "0"
			epcList.Clear()
			lvEPC.Items.Clear()
			lblTime.Text = "0"
			lblTotal.Text = "0"
			total = 0
			beginTime = Environment.TickCount
		End Sub

		'开始读取epc
		Private Sub StartEPC()
			groupBox8.Enabled = False
			btnScanEPC.Text = If(Common.isEnglish, strStop, strStop2)
			Call (New Thread(New ThreadStart(Sub()
				ReadEPC()
			End Sub))).Start()

			groupBox2.Enabled = False
			groupBox3.Enabled = False
			groupBox4.Enabled = False
			groupBox5.Enabled = False
			gbInventoryMode.Enabled = False

		End Sub
		'停止读取epc
		Private Sub StopEPC(ByVal isStop As Boolean)

			Dim reuslt As Boolean = uhf.StopGet()
			If Not reuslt Then
				MessageBox.Show("停止失败")
			End If
			Thread.Sleep(50)
			isRuning = False
			groupBox8.Enabled = True
			btnScanEPC.Text = If(Common.isEnglish, strStart, strStart2)
			mainform.enableControls()

			groupBox2.Enabled = True
			groupBox3.Enabled = True
			groupBox4.Enabled = True
			groupBox5.Enabled = True
			gbInventoryMode.Enabled = True
		End Sub

		'获取epc
		Private Sub ReadEPC()
			Try
				beginTime = Environment.TickCount
				Do While isRuning

					Dim result As TempertureInfo = tempertureTag.uhfGetReceivedTempertureInfo()
					If result IsNot Nothing Then
						Me.BeginInvoke(setTextCallback2, New Object() {result})
					End If
				Loop
			Catch ex As Exception

			End Try
			isComplete = True

		End Sub

		Private tempCount As Integer = 0
		Private sb As New StringBuilder(100)
		Private Sub UpdataEPC(ByVal info As TempertureInfo)
			Dim count As String = "1"
			Dim uhfinfo As UHFTAGInfo = info.UhfTagInfo
			Dim time As Long = Environment.TickCount - beginTime
			lblTime.Text = (time) & "ms"
 ' (System.Environment.TickCount - beginTime) + "ms";//((System.Environment.TickCount - beginTime) / 1000) + "(s)";// sb.ToString();//
			tempCount += Integer.Parse(count)
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: label6.Text = (tempCount += int.Parse(count)).ToString();
			label6.Text = tempCount.ToString()

			Dim exist(0) As Boolean
			Dim index As Integer = CheckUtils.getInsertIndex(epcList, uhfinfo.Epc,uhfinfo.Tid, exist)
			If exist(0) Then
				lvEPC.Items(index).SubItems(2).Text = info.UhfTagInfo.Tid
				lvEPC.Items(index).SubItems(3).Text = info.Temperture
				lvEPC.Items(index).SubItems(4).Text = info.Time
				lvEPC.Items(index).SubItems(5).Text = uhfinfo.Rssi
				lvEPC.Items(index).SubItems(6).Text = (Integer.Parse(lvEPC.Items(index).SubItems(6).Text) + Integer.Parse(count)).ToString()
				lvEPC.Items(index).SubItems(7).Text = uhfinfo.Ant
			Else
				total += 1
				Dim lv As New ListViewItem()
				lv.Text = (lvEPC.Items.Count + 1).ToString()
				lv.SubItems.Add(uhfinfo.Epc)
				lv.SubItems.Add(uhfinfo.Tid)
				lv.SubItems.Add(info.Temperture)
				lv.SubItems.Add(info.Time)
				lv.SubItems.Add(uhfinfo.Rssi)
				lv.SubItems.Add(count)
				lv.SubItems.Add(uhfinfo.Ant)
				lvEPC.Items.Add(lv)
				epcList.Add(New EpcInfo(uhfinfo.Epc, Integer.Parse(count), DataConvert.HexStringToByteArray(uhfinfo.Epc), DataConvert.HexStringToByteArray(uhfinfo.Tid)))
				lblTotal.Text = epcList.Count & ""
			End If



		End Sub




		Private Sub lvEPC_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
			If lvEPC.SelectedItems.Count <= 0 Then
				Return
			End If
			If btnScanEPC.Text = strStop OrElse btnScanEPC.Text = strStop2 Then
				StopEPC(True)
			End If
			Dim tag As String = lvEPC.SelectedItems(0).SubItems(1).Text
			Common.tag = tag
			mainform.ReadWriteTag(tag)
		End Sub


		Private Sub contextMenuStrip1_Click(ByVal sender As Object, ByVal e As EventArgs)
			If lvEPC.SelectedItems.Count <= 0 Then
				Return
			End If
			Dim str As String = lvEPC.SelectedItems(0).SubItems(1).Text
			Clipboard.SetDataObject(str)
		End Sub

		Private Sub textBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
			Try
				Dim ptr As String = txtfilerLen.Text
				If Not StringUtils.IsNumber(ptr) Then
					txtfilerLen.Text = "0"
					Return
				End If
			Catch ex As Exception
				txtfilerLen.Text = "0"
			End Try
		End Sub

		Private Sub rbTID_Click(ByVal sender As Object, ByVal e As EventArgs)
			txtPtr.Text = "0"
		End Sub

		Private Sub rbUser_Click(ByVal sender As Object, ByVal e As EventArgs)
			txtPtr.Text = "0"
		End Sub

		Private Sub rbEPC_Click(ByVal sender As Object, ByVal e As EventArgs)
			txtPtr.Text = "32"
		End Sub

		Private isDelete As Boolean = False
		Private Sub ReadEPCForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
			If e.KeyCode = Keys.Back Then
				isDelete = True
			Else
				isDelete = False
			End If
		End Sub



		Private Sub timer1_Tick(ByVal sender As Object, ByVal e As EventArgs)
			lblTime.Text = (Environment.TickCount - beginTime) & "(ms)"
		End Sub

		Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button3.Click
			Dim filter_bank As Byte = 1
			If rbTID.Checked Then
				filter_bank = 2
			ElseIf rbUser.Checked Then
				filter_bank = 3
			End If
			Dim filter_addr As Byte = Byte.Parse(txtPtr.Text)
			Dim filter_len As Byte = Byte.Parse(txtfilerLen.Text)
			Dim filter_data() As Byte = DataConvert.HexStringToByteArray("00")
			If filter_len > 0 Then
				filter_data = DataConvert.HexStringToByteArray(txtData.Text)
			End If


			Dim min_temp As Single = Single.Parse(txtMin.Text)
			Dim max_temp As Single = Single.Parse(txtMax.Text)
			Dim work_delay As Integer = Integer.Parse(txtdelay.Text)
			Dim work_interval As Integer = Integer.Parse(txtinterval.Text)


			If tempertureTag.StartLogging(filter_bank, filter_addr, filter_len, filter_data, min_temp, max_temp, work_delay, work_interval) Then
				showMessage("成功 !")
			Else
				showMessage("失败!")
			End If
		End Sub
		Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
			Dim filter_bank As Byte = 1
			If rbTID.Checked Then
				filter_bank = 2
			ElseIf rbUser.Checked Then
				filter_bank = 3
			End If
			Dim filter_addr As Byte = Byte.Parse(txtPtr.Text)
			Dim filter_len As Byte = Byte.Parse(txtfilerLen.Text)
			Dim filter_data() As Byte = DataConvert.HexStringToByteArray("00")
			If filter_len > 0 Then
				filter_data = DataConvert.HexStringToByteArray(txtData.Text)
			End If
			Dim data() As Byte = DataConvert.HexStringToByteArray(txtPwd.Text)
			Dim password As Integer = data(3)
			password = (data(2) << 8) Or password
			password = (data(1) << 16) Or password
			password = (data(0) << 24) Or password
			'txtPwd
			If tempertureTag.StopLogging(filter_bank, filter_addr, filter_len, filter_data, password) Then
				showMessage("停止成功!")

			Else
				showMessage("停止失败!")

			End If
		End Sub

		Private Sub button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button5.Click

			Dim filerData As String = txtData.Text.Replace(" ", "")


			'过滤----------------------------------
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtfilerLen.Text)

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
			Dim msg As String = ""
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

		Private Sub button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button4.Click
			Dim filerData As String = txtData.Text.Replace(" ", "")


			'过滤----------------------------------
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtfilerLen.Text)

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
			Dim outtemp(9) As Single
			Dim result As Boolean = uhf.ReadTagTemperature(CByte(filerBank), filerPtr, filerLen, filerBuff, outtemp)
			If result Then
				label39.Text = "温度:" & outtemp(0) & "℃"
			Else
				Dim msg As String = If(Common.isEnglish, "failure!", "失败!")
				showMessage(msg)
				label39.Text = "温度:--"
			End If
			If label39.ForeColor = Color.Black Then
				label39.ForeColor = Color.Blue
			Else
				label39.ForeColor = Color.Black
			End If
		End Sub

		Private Sub button10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button10.Click
			Dim mode As Integer = cbInventoryMode.SelectedIndex
			Dim result As Boolean = False

			Select Case mode
				Case 0
					result = uhf.setEPCMode(False)
				Case 1
					result = tempertureTag.setEPCAndTemperature()
			End Select

			If Not result Then
				Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
				showMessage(msg)
				Return
			Else
				Dim msg As String = If(Common.isEnglish, "success!", "设置成功!")
				showMessage(msg)
			End If
		End Sub
		Private Sub showMessage(ByVal msg As String)
			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(500)
			End Sub, msg)
			f.ShowDialog(Me)
		End Sub

		Private Sub button11_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button11.Click
			Dim userPtr As Byte = 0
			Dim userLen As Byte = 0
			Dim mode As Integer = uhf.getEPCTIDUSERMode(userPtr, userLen)
			Select Case mode
				Case 0
					cbInventoryMode.SelectedIndex = 0
				Case 3
					cbInventoryMode.SelectedIndex = 1
			End Select
		End Sub

		Private Sub button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button6.Click

			'过滤----------------------------------
			Dim filerData As String = txtData.Text.Replace(" ", "")
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtfilerLen.Text)

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
			Dim result As Integer = tempertureTag.CheckOpMode(CByte(filerBank), filerPtr, filerLen, filerBuff)

			If result > 0 Then
				Dim msg As New StringBuilder()
				msg.Append("状态:(0x" & DataConvert.DecimalToHexString(result) & ")" & vbCrLf)
				If (result And 1) = 1 OrElse (result And 2) = 2 OrElse (result And 4) = 4 OrElse (result And 8) = 8 OrElse (result And 16) = 16 OrElse (result And 32) = 32 OrElse (result And 64) = 64 OrElse (result And 128) = 128 OrElse (result And 1024) = 1024 OrElse (result And 16384) = 16384 OrElse (result And 32768) = 32768 Then
					' msg.Append("RFU\r\n"); 
				End If
				If (result And 256) = 256 Then
					msg.Append("电池电压高于0.9V" & vbCrLf)
				End If
				If (result And 512) = 512 Then
					msg.Append("光强超过预设值" & vbCrLf)
				End If
				If (result And 2048) = 2048 Then
					msg.Append("一次测温流程被打断" & vbCrLf)
				End If
				If (result And 4096) = 4096 Then
					msg.Append("当前处于rtc测温流程" & vbCrLf)
				End If
				If (result And 8192) = 8192 Then
					' msg.Append("当前用户权限无效");  
				End If
				'frmWaitingBox f = new frmWaitingBox((obj, args) =>
				'{
				'    System.Threading.Thread.Sleep(4000);
				'}, msg.ToString());
				'f.ShowDialog(this);
				MessageBoxEx.Show(msg.ToString())

			Else
				Dim msg As String = "返回：" & result
				showMessage(msg)
			End If

		End Sub

		Private Sub btnVoltage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVoltage.Click
			Dim filerData As String = txtData.Text.Replace(" ", "")
			label17.Text = "电压:--"

			'过滤----------------------------------
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtfilerLen.Text)

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
			Dim outtemp(9) As Single
			Dim result As Boolean = tempertureTag.ReadTagVoltage(CByte(filerBank), filerPtr, filerLen, filerBuff, outtemp)
			If result Then
				label17.Text = "电压:" & outtemp(0)
			Else
				Dim msg As String = If(Common.isEnglish, "failure!", "失败!")
				showMessage(msg)
				label17.Text = "电压:--"
			End If
			If label17.ForeColor = Color.Black Then
				label17.ForeColor = Color.Blue
			Else
				label17.ForeColor = Color.Black
			End If
		End Sub

		Private ReadMultiClickNumber As Integer = 0
		Private Sub button7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button7.Click
			Dim filerData As String = txtData.Text.Replace(" ", "")


			'过滤----------------------------------
			Dim filerBank As Integer = 1
			Dim filerBuff() As Byte = DataConvert.HexStringToByteArray(filerData)
			Dim filerPtr As Integer = Integer.Parse(txtPtr.Text)
			Dim filerLen As Integer = Integer.Parse(txtfilerLen.Text)

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
			Dim totalNum(0) As Integer
			Dim returnNum(0) As Byte
			Dim temp(99) As Single
			Dim start As Integer = Integer.Parse(txtStart.Text)
			Dim result As Boolean = tempertureTag.ReadMultiTemp(CByte(filerBank), filerPtr, filerLen, filerBuff, Integer.Parse(txtStart.Text), Byte.Parse(txtNumber.Text), totalNum, returnNum, temp)
			If result Then
				If ReadMultiClickNumber = 0 Then
					button1_Click(Nothing, Nothing)
				End If
				ReadMultiClickNumber += 1

				Dim index As Integer = 0
				Do While index < returnNum(0)
					Dim inserIndex As Integer = -1
					Dim isExist As Boolean = False
					For m As Integer = 0 To lvEPC.Items.Count - 1
						If Integer.Parse(lvEPC.Items(m).Text) = index + start Then
							inserIndex = m
							isExist = True
							Exit For
						ElseIf inserIndex = -1 AndAlso Integer.Parse(lvEPC.Items(m).Text)> index + start Then
							inserIndex = m
						End If
					Next m

					If isExist Then
						lvEPC.Items(inserIndex).SubItems(3).Text = temp(index) & ""
					Else
						Dim lv As New ListViewItem()
						lv.Text = (index + start).ToString()
						lv.SubItems.Add("")
						lv.SubItems.Add("")
						lv.SubItems.Add(temp(index) & "")
						lv.SubItems.Add("")
						lv.SubItems.Add("")
						lv.SubItems.Add("")
						lv.SubItems.Add("")
						If inserIndex <> -1 Then
							lvEPC.Items.Insert(inserIndex, lv)
						Else
							lvEPC.Items.Add(lv)
						End If
					End If

					index += 1
				Loop
				label21.Text = "温度值总数量:" & totalNum(0)
				label22.Text = "本次读取到的温度值数量:" & returnNum(0)
			Else
				label21.Text = "温度值总数量:--"
				label22.Text = "本次读取到的温度值数量:--"
				Dim msg As String = If(Common.isEnglish, "failure!", "失败!")
				showMessage(msg)
			End If
		End Sub

		Private Sub button1_Click_1(ByVal sender As Object, ByVal e As EventArgs)

		End Sub



		'public void ss() { 
		'     ReadMultiTemp(byte mask_bank, int mask_addr, int mask_len, byte[] mask_data, int t_start, byte t_num, int[] totalNum, byte[] returnNum, float[] temp)
		'}

	End Class
End Namespace
