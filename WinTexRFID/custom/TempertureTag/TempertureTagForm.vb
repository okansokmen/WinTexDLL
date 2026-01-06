Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports WinForm_Test
Imports System.Threading
Imports System.Collections
Imports UHFAPP.multidevice

Namespace UHFAPP
	Partial Public Class TempertureTagForm
		Inherits Form

		Private uhf As UHFAPI_RFMicronMagnus_S3 = UHFAPI_RFMicronMagnus_S3.getInstance()
		Private mainform As MainForm2
		Private total As Integer = 0
		Private isDelete As Boolean = False
		Private isRuning As Boolean = False
		Private isComplete As Boolean = True
		Private beginTime As Integer = 0
		Private epcList As New Hashtable()
		' 将text更新的界面控件的委托类型
		Private Delegate Sub SetTextCallback(ByVal epc As String, ByVal calibrationData As String, ByVal sensorCode As String, ByVal rssiCode As String, ByVal tempeCode As String, ByVal rssi As String, ByVal ant As String, ByVal count As String) '(string epc, string tid, string rssi, string count, string ant);
		Private setTextCallback2 As SetTextCallback

		'0  CalibrationData+SensorCode+ On-ChipRSSI+TempeCode
		'1  On-ChipRSSI+ TempeCode
		Private mode As Integer = 0


		Public Sub New()
			InitializeComponent()
		End Sub
		Public Sub New(ByVal isOpen As Boolean, ByVal mainform As MainForm2)
			Me.mainform = mainform
			InitializeComponent()
		End Sub
		Private Sub ConfigForm2_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			cmbAnt.SelectedIndex = 0
			cmbPower.SelectedIndex = 19

			cmbWAnt.SelectedIndex = 0
			cmbWPower.SelectedIndex = 19
			cmbMode.SelectedIndex = 0


			cmbAnt_2.SelectedIndex = 0
			cmbPower_2.SelectedIndex = 19

			rbSensorCode.Checked = True
			label2.Text = "0"

			AddHandler MainForm2.eventOpen, AddressOf MainForm_eventOpen
			setTextCallback2 = New SetTextCallback(AddressOf UpdataEPC)

			AddHandler MainForm2.eventSwitchUI, AddressOf MainForm_eventSwitchUI
			MainForm_eventSwitchUI()

			'uhf.SetDebug(true);
		End Sub
		Private Sub btnRead_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRead.Click
			If Not Me.btnRead.IsHandleCreated Then Return

			Dim epc As String = txtEPC.Text.Replace(" ", "")

			If epc.Length <> 32 Then
				MessageBox.Show("请输入16个字节的EPC数据!")
				Return
			End If

			Dim ant As Integer = cmbAnt.SelectedIndex + 1
			Dim power As Integer = cmbPower.SelectedIndex + 1
			Dim buffEPC() As Byte = DataConvert.HexStringToByteArray(epc)

			Dim data As String = ""
			If rbSensorCode.Checked Then
				data = UHFAPI_RFMicronMagnus_S3.getInstance().ReadSensorCode(buffEPC, ant, power)
			ElseIf rbRssi.Checked Then
				data = UHFAPI_RFMicronMagnus_S3.getInstance().ReadOnChipRSSI(buffEPC, ant, power)
			ElseIf rbTempertureCode.Checked Then
				data = UHFAPI_RFMicronMagnus_S3.getInstance().ReadTempertureCode(buffEPC, ant, power)
			ElseIf rbCalibrationData.Checked Then
				data = UHFAPI_RFMicronMagnus_S3.getInstance().ReadCalibrationData(buffEPC, ant, power)
			ElseIf rbRssiTempCode.Checked Then
				data = UHFAPI_RFMicronMagnus_S3.getInstance().ReadOnChipRSSIAndTempCode(buffEPC, ant, power)
			ElseIf rbOnChipRSSI_TempCode_CalibrationData.Checked Then
				data = UHFAPI_RFMicronMagnus_S3.getInstance().ReadOnChipRSSI_TempCode_CalibrationData(buffEPC, ant, power)
			End If
			Dim msg As String = ""
			Dim time As Integer = 500
			If data Is Nothing OrElse data.Length = 0 Then
				msg = If(Common.isEnglish, "Read failure!", "读取数据失败!")
				txtData.Text = ""
			Else
				time = 100
				txtData.Text = data
				msg = If(Common.isEnglish, "Read success!", "读取数据成功!")
			End If

			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(time)
			End Sub, msg)
			f.ShowDialog(Me)
		End Sub

		Private Sub txtEPC_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtEPC.TextChanged
			If Not Me.txtEPC.IsHandleCreated Then Return

			FormatHex(txtEPC)
			Dim data As String = txtEPC.Text.Replace(" ", "")
			If data.Length > 0 Then
				label2.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				label2.Text = "0"
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


		Private Sub ConfigForm2_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
			If e.KeyCode = Keys.Back Then
				isDelete = True
			Else
				isDelete = False
			End If

		End Sub

		Private Sub disableControls()
			groupBox1.Enabled = False
			cmbAnt_2.Enabled = False
			cmbPower_2.Enabled = False
			cmbMode.Enabled = False
			groupBox3.Enabled = False
			mainform.disableControls()

		End Sub
		Private Sub enableControls()
			groupBox1.Enabled = True
			cmbAnt_2.Enabled = True
			cmbPower_2.Enabled = True
			cmbMode.Enabled = True
			groupBox3.Enabled = True
			mainform.enableControls()
		End Sub
		Private Sub btnScanEPC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnScanEPC.Click
			If Not Me.btnScanEPC.IsHandleCreated Then Return

			If btnScanEPC.Text = "Stop" Then
				StopEPC(True)
			Else
				mode = cmbMode.SelectedIndex
				Dim power As Integer = cmbPower_2.SelectedIndex + 1
				Dim ant As Integer = cmbAnt_2.SelectedIndex + 1

				If Not isRuning AndAlso isComplete Then
					disableControls()
					isRuning = True
					isComplete = False

					Dim reault As Boolean = False ' reault = uhf.InventoryTempTag(ant, power);
					If mode = 0 Then
						 reault = uhf.InventoryTempTag(ant, power)
					ElseIf mode = 1 Then
						reault = uhf.InventoryTempTag_OnChipRSSI_TempeCode(ant, power)
					ElseIf mode = 2 Then
						reault = uhf.PerformInventory(ant, power)
					End If

					If reault Then
						StartEPC()
					Else
						MessageBoxEx.Show(Me, "Inventory failure!")
						isRuning = False
						isComplete = True
					End If
				End If
			End If
		End Sub



		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			If Not Me.button1.IsHandleCreated Then Return

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
			btnScanEPC.Text = "Stop"
			Call (New Thread(New ThreadStart(Sub()
				ReadEPC()
			End Sub))).Start()

		End Sub
		'停止读取epc
		Private Sub StopEPC(ByVal isStop As Boolean)
			UHFAPI_RFMicronMagnus_S3.getInstance().StopGet()
			isRuning = False
			btnScanEPC.Text = "Start"
			enableControls()
		End Sub

		'获取epc
		Private Sub ReadEPC()
			Try
				beginTime = Environment.TickCount
				Do While isRuning
					Dim epc As String = ""
					Dim rssi As String = ""

					Dim calibrationData As String = ""
					Dim sensorCode As String = ""
					Dim rssiCode As String = ""
					Dim tempeCode As String = ""
					Dim ant As Integer = 0

					Dim result As Boolean = False
					If mode = 0 Then
						result=uhf.uhfGetTempTagReceived(epc, calibrationData, sensorCode, rssiCode, tempeCode, rssi, ant)
					ElseIf mode =1 Then
						result = uhf.uhfGetTempTagReceived_OnChipRSSI_TempeCode(epc, rssiCode, tempeCode, rssi, ant)
					ElseIf mode = 2 Then
						'SensorCode+On-ChipRSSI
						Dim tagInfo As TagInfo = uhf.getTagData()
						If tagInfo IsNot Nothing AndAlso tagInfo.UhfTagInfo IsNot Nothing Then
							ant = Integer.Parse(tagInfo.UhfTagInfo.Ant)
							epc = tagInfo.UhfTagInfo.Epc
							sensorCode = tagInfo.UhfTagInfo.Sensor
							rssi = tagInfo.UhfTagInfo.Rssi
							result = True
						End If


					End If

					If result Then

						Me.BeginInvoke(setTextCallback2, New Object() {epc, calibrationData, sensorCode, rssiCode, tempeCode, rssi, ant & "", "1"})
					Else
						Thread.Sleep(10)
					End If
				Loop

			Catch ex As Exception

			End Try
			isComplete = True

		End Sub

		Private tempCount As Integer = 0
		Private sb As New StringBuilder(100)
		Private Sub UpdataEPC(ByVal epc As String, ByVal calibrationData As String, ByVal sensorCode As String, ByVal rssiCode As String, ByVal tempeCode As String, ByVal rssi As String, ByVal ant As String, ByVal count As String)
			Dim time As Long = Environment.TickCount - beginTime
			lblTime.Text = (time) & "ms"
			tempCount += Integer.Parse(count)
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: label6.Text = (tempCount += int.Parse(count)).ToString();
			label6.Text = tempCount.ToString()
			If epcList(epc) IsNot Nothing Then
				For i As Integer = 0 To lvEPC.Items.Count - 1
					If Me.lvEPC.Items(i).SubItems(1).Text = epc Then
						lvEPC.Items(i).SubItems(2).Text = "tid"
						lvEPC.Items(i).SubItems(3).Text = calibrationData
						lvEPC.Items(i).SubItems(4).Text = sensorCode
						lvEPC.Items(i).SubItems(5).Text = rssiCode
						lvEPC.Items(i).SubItems(6).Text = tempeCode
						lvEPC.Items(i).SubItems(7).Text = rssi
						lvEPC.Items(i).SubItems(8).Text = (Integer.Parse(lvEPC.Items(i).SubItems(8).Text) + Integer.Parse(count)).ToString()
						lvEPC.Items(i).SubItems(9).Text = ant
						Exit For
					End If
				Next i
			Else
				total += 1
				Dim lv As New ListViewItem()
				lv.Text = (lvEPC.Items.Count + 1).ToString()
				lv.SubItems.Add(epc)
				lv.SubItems.Add("tid")


				lv.SubItems.Add(calibrationData)
				lv.SubItems.Add(sensorCode)
				lv.SubItems.Add(rssiCode)
				lv.SubItems.Add(tempeCode)
				lv.SubItems.Add(rssi)
				lv.SubItems.Add(count)
				lv.SubItems.Add(ant)



				lvEPC.Items.Add(lv)
				epcList.Add(epc, count)
				lblTotal.Text = epcList.Count & ""
			End If
		End Sub

		Private Sub ConfigForm2_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
			RemoveHandler MainForm2.eventOpen, AddressOf MainForm_eventOpen
			RemoveHandler MainForm2.eventSwitchUI, AddressOf MainForm_eventSwitchUI
		End Sub


		Private Sub MainForm_eventOpen(ByVal open As Boolean)
			If open Then
				groupBox1.Enabled = True
				groupBox2.Enabled = True
			Else
				groupBox1.Enabled = False
				groupBox2.Enabled = False
				If btnScanEPC.Text = "Stop" Then
					StopEPC(True)
				End If
			End If
		End Sub
		Private Sub MainForm_eventSwitchUI()
			If Common.isEnglish Then
			End If
		End Sub

		Private Sub contextMenuStrip1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles contextMenuStrip1.Click
			If Not Me.contextMenuStrip1.IsHandleCreated Then Return

			If lvEPC.SelectedItems.Count <= 0 Then
				Return
			End If
			Dim str As String = lvEPC.SelectedItems(0).SubItems(1).Text
			Clipboard.SetDataObject(str)
		End Sub



		Private Sub btnWrite_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWrite.Click
			If Not Me.btnWrite.IsHandleCreated Then Return

			Dim epc As String = txtWEPC.Text.Replace(" ","")
			Dim data As String = txtWData.Text.Replace(" ", "")
			Dim ant As Integer = cmbWAnt.SelectedIndex + 1
			Dim power As Integer = cmbWPower.SelectedIndex + 1
			If epc.Length <> 32 Then
				MessageBox.Show("请输入16个字节的EPC数据!")
				Return
			End If
			If data.Length Mod 2 <>0 Then
				MessageBox.Show("请输入16进制数据!")
				Return
			End If
			Dim buffEPC() As Byte = DataConvert.HexStringToByteArray(epc)
			Dim buffData() As Byte = DataConvert.HexStringToByteArray(data)

			Dim result As Boolean = UHFAPI_RFMicronMagnus_S3.getInstance().WriteCalibrationData(buffEPC, ant, power, buffData)
			Dim msg As String = ""
			Dim time As Integer = 500
			If Not result Then
				msg = If(Common.isEnglish, "failure!", "失败!")
			Else
				time = 100
				msg = If(Common.isEnglish, "success!", "成功!")
			End If

			Dim f As New frmWaitingBox(Sub(obj, args)
				System.Threading.Thread.Sleep(time)
			End Sub, msg)
			f.ShowDialog(Me)

		End Sub

		Private Sub txtWEPC_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtWEPC.TextChanged
			If Not Me.txtWEPC.IsHandleCreated Then Return

			FormatHex(txtWEPC)
			Dim data As String = txtWEPC.Text.Replace(" ", "")
			If data.Length > 0 Then
				label12.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				label12.Text = "0"
			End If
		End Sub

		Private Sub txtWData_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtWData.TextChanged
			If Not Me.txtWData.IsHandleCreated Then Return

			FormatHex(txtWData)
			Dim data As String = txtWData.Text.Replace(" ", "")
			If data.Length > 0 Then
				label13.Text = ((data.Length \ 2) + (If((data.Length Mod 2) = 0, 0, 1))).ToString() ' txtRead_Length.Text = ((data.Length / 4) + ((data.Length % 4) == 0 ? 0 : 1)).ToString();
			Else
				label13.Text = "0"
			End If
		End Sub

		Private Sub cmbMode_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbMode.SelectedValueChanged
			If Not Me.cmbMode.IsHandleCreated Then Return

			button1_Click(Nothing,Nothing)
		End Sub

	End Class
End Namespace
