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

Namespace UHFAPP
	Partial Public Class ConfigForm
		Inherits BaseForm

		Private isFlag As Boolean = False
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
			cmbAntWorkTime.SelectedIndex = 0
		End Sub

		Private Sub MainForm_eventOpen(ByVal open As Boolean)
			If open Then
				panel1.Enabled = True
				If MainForm.MODE = 2 Then
					gbAnt.Enabled = False
					gbIP.Enabled = False
					gbIp2.Enabled = False
					bgGPIO.Enabled = False
					gbWorkMode.Enabled = False
					groupBox25.Enabled = False
					btnReset.Enabled = False

					gbIP.Visible = False
					gbIp2.Visible = False
					groupBox7.Visible = False
					gbWorkMode.Visible = False
					gbAnt.Visible = False
					bgGPIO.Visible = False
					'btnReset.Visible = false;
					groupBox4.Visible = False
				Else
					gbAnt.Enabled = True
					gbIP.Enabled = True
					gbIp2.Enabled = True
					bgGPIO.Enabled = True
					gbWorkMode.Enabled = True
					groupBox25.Enabled = True
					btnReset.Enabled = True

					gbIP.Visible = True
					gbIp2.Visible = True
					groupBox7.Visible = True
					gbWorkMode.Visible = True
					gbAnt.Visible = True
					bgGPIO.Visible = True
					btnReset.Visible = True
					groupBox4.Visible = True
				End If
			Else
				panel1.Enabled = False
			End If
		End Sub

		Private Sub ConfigForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			AddHandler MainForm.eventMainSizeChanged, AddressOf MainForm_SizeChanged

			AddHandler MainForm.eventOpen, AddressOf MainForm_eventOpen
			cmbLinkFrequency.SelectedIndex = 3

			AddHandler MainForm.eventSwitchUI, AddressOf MainForm_eventSwitchUI
			MainForm_eventSwitchUI()
			comboBox1.SelectedIndex = 0
			cmbOutStatus.SelectedIndex = 0
			cmbInput.SelectedIndex = 0
			comRM.SelectedIndex = 0

			If MainForm.MODE = 2 Then
				gbAnt.Enabled = False
				gbIP.Enabled = False
				gbIp2.Enabled = False
				bgGPIO.Enabled = False
				gbWorkMode.Enabled = False
				groupBox25.Enabled = False
			  '  btnReset.Enabled = false;

				gbIP.Visible = False
				gbIp2.Visible = False
				groupBox7.Visible = False
				gbWorkMode.Visible = False
				gbAnt.Visible = False
				bgGPIO.Visible = False
			   ' btnReset.Visible = false;
				groupBox4.Visible = False
			Else
				gbAnt.Enabled = True
				gbIP.Enabled = True
				gbIp2.Enabled = True
				bgGPIO.Enabled = True
				gbWorkMode.Enabled = True
				groupBox25.Enabled = True
				btnReset.Enabled = True

				gbIP.Visible = True
				gbIp2.Visible = True
				groupBox7.Visible = True
				gbWorkMode.Visible = True
				gbAnt.Visible = True
				bgGPIO.Visible = True
				btnReset.Visible = True
				groupBox4.Visible = True
			End If

		End Sub

		Private Sub ConfigForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
			RemoveHandler MainForm.eventMainSizeChanged, AddressOf MainForm_SizeChanged

			RemoveHandler MainForm.eventOpen, AddressOf MainForm_eventOpen
			RemoveHandler MainForm.eventSwitchUI, AddressOf MainForm_eventSwitchUI
		End Sub
		#Region "功率"
		Private Sub btnPowerGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPowerGet_ANT1.Click
			Dim msg As String = If(Common.isEnglish, "Get the power failure!", "获取功率失败!")
			Dim power As Byte =0
			If uhf.GetPower(power) Then
				cmbPower_ANT1.SelectedIndex = power - 1
				msg = If(Common.isEnglish, "Get the power success", "获取功率成功!")
			End If
			showMessage(msg)
		End Sub

		Private Sub btnPowerSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPowerSet_ANT1.Click
			Dim msg As String = If(Common.isEnglish, "Set the power failure!", "设置功率失败!")
			If cmbPower_ANT1.SelectedIndex >= 0 Then
				Dim power1 As Byte = CByte(cmbPower_ANT1.SelectedIndex + 1)

				Dim save As Byte = CByte(If(cbPower.Checked, 1, 0))
				If uhf.SetPower(save, power1) Then
					msg = If(Common.isEnglish, "Set the power success!", "设置功率成功!")
				End If

			End If
			showMessage(msg)
		End Sub

		#End Region

		#Region "工作频率"
'917.1
'917.3
'917.5
'917.7
'917.9
'918.1
'918.3
'918.5
'918.7
'918.9
'919.1
'919.3
'919.5
'919.7
'919.9
'920.1
'920.3
'920.5
'920.7
'920.9
'921.1
'921.3
'921.5
'921.7
'921.9
'922.1
'922.3
'922.5
'922.7
'922.9
'923.1
'923.3

		Private Sub btnWorkModeSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWorkModeSet.Click
			 Dim msg As String =If(Common.isEnglish, "failure!", "频点失败!")
			 Try
				 If comboBox1.Text <> "" Then
					 Dim frequency As String = comboBox1.Text.Replace(".", "")
					 If frequency.Length = 3 Then
						 frequency = frequency & "000"
					 ElseIf frequency.Length = 4 Then
						 frequency = frequency & "00"
					 ElseIf frequency.Length = 5 Then
						 frequency = frequency & "0"
					 End If
					 If StringUtils.IsNumber(frequency) Then
						 Dim ifrequency() As Integer = { Integer.Parse(frequency) }
						 If uhf.SetJumpFrequency(1, ifrequency) Then
							 msg = If(Common.isEnglish, "success!", "设置频点成功!")
						 End If
					 End If
				 End If
			 Catch ex As Exception

			 End Try

			 showMessage(msg)
		End Sub
		Private Sub btnWorkModeGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWorkModeGet.Click
			Dim msg As String =If(Common.isEnglish, "failure!", "获取频点失败!")
			Dim ifrequency(0) As Integer
			If uhf.GetJumpFrequency(ifrequency) Then
				comboBox1.Text = ifrequency(0).ToString().Insert(3, ".")
				msg = If(Common.isEnglish, "success!", "获取频点成功!")
			End If

			showMessage(msg)
		End Sub
		#End Region

		#Region "Gen2"
		Private Sub btnGen2Get_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGen2Get.Click
			Dim Target As Byte = 0
			Dim Action As Byte = 0
			Dim T As Byte = 0
			Dim Q As Byte = 0
			Dim StartQ As Byte = 0
			Dim MinQ As Byte = 0
			Dim MaxQ As Byte = 0
			Dim D As Byte = 0
			Dim Coding As Byte = 0
			Dim P As Byte = 0
			Dim Sel As Byte = 0
			Dim Session As Byte = 0
			Dim G As Byte = 0
			Dim LF As Byte = 0
			Dim msg As String = If(Common.isEnglish, "failure", "获取失败!")
			Dim start As Integer = Environment.TickCount

			Dim result As Boolean=uhf.GetGen2(Target, Action, T, Q, StartQ, MinQ, MaxQ, D, Coding, P, Sel, Session, G, LF)
		  '  MessageBox.Show("耗时：" + (Environment.TickCount-start));
			If result Then
				cmbTarget.SelectedIndex = Target
				cmbAction.SelectedIndex = Action
				cmbT.SelectedIndex = T
				cmbQ.SelectedIndex = Q
				cmbCoding.SelectedIndex = Coding
				cmbP.SelectedIndex = P
				cmbSel.SelectedIndex = Sel
				cmbStartQ.SelectedIndex = StartQ
				cmbMinQ.SelectedIndex = MinQ
				cmbMaxQ.SelectedIndex = MaxQ
				cmbDr.SelectedIndex = D
				cmbSession.SelectedIndex = Session
				cmbG.SelectedIndex = G
				cmbLinkFrequency.SelectedIndex = LF
				msg = If(Common.isEnglish, "success", "获取成功!")
				btnGen2Set.Enabled = True
			End If


			showMessage(msg)
		End Sub
		Private Sub btnGen2Set_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGen2Set.Click
			Dim msg As String =If(Common.isEnglish, "Set the Gen2 failure!", "设置失败!")
			Try
				Dim Target As Byte =CByte(cmbTarget.SelectedIndex)
				Dim Action As Byte = CByte(cmbAction.SelectedIndex)
				Dim T As Byte = CByte(cmbT.SelectedIndex)
				Dim Q As Byte = CByte(cmbQ.SelectedIndex)
				Dim StartQ As Byte = CByte(cmbStartQ.SelectedIndex)
				Dim MinQ As Byte = CByte(cmbMinQ.SelectedIndex)
				Dim MaxQ As Byte = CByte(cmbMaxQ.SelectedIndex)
				Dim D As Byte = CByte(cmbDr.SelectedIndex)
				Dim Coding As Byte = CByte(cmbCoding.SelectedIndex)
				Dim P As Byte = CByte(cmbP.SelectedIndex)
				Dim Sel As Byte = CByte(cmbSel.SelectedIndex)
				Dim Session As Byte = CByte(cmbSession.SelectedIndex)
				Dim G As Byte = CByte(cmbG.SelectedIndex)
				Dim LF As Byte = CByte(cmbLinkFrequency.SelectedIndex)
				If uhf.SetGen2(Target, Action, T, Q, StartQ, MinQ, MaxQ, D, Coding, P, Sel, Session, G, LF) Then
					msg = If(Common.isEnglish, "Set the Gen2 success!", "设置成功!")
				End If

			Catch ex As Exception

			End Try
			showMessage(msg)
		End Sub
		#End Region

		#Region "CW"
		Private Sub btnGetCW_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetCW.Click
			Dim msg As String = "failure!"
			If uhf.SetCW(1) Then
				msg = "success!"
			End If
			showMessage(msg)
		End Sub
		Private Sub btnSetCW_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetCW.Click
			Dim msg As String = "failure!"
			If uhf.SetCW(0) Then
				msg = "success!"
			End If
			showMessage(msg)
		End Sub
		#End Region

		#Region "天线"
		Private Sub btnGetANT_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetANT.Click
			 cmbAnt8.Checked = False
			 cmbAnt7.Checked = False
			 cmbAnt6.Checked = False
			 cmbAnt5.Checked = False
			 cmbAnt4.Checked = False
			 cmbAnt3.Checked = False
			 cmbAnt2.Checked = False
			 cmbAnt1.Checked = False
			 cmbAnt16.Checked = False
			 cmbAnt15.Checked = False
			 cmbAnt14.Checked = False
			 cmbAnt13.Checked = False
			 cmbAnt12.Checked = False
			 cmbAnt11.Checked = False
			 cmbAnt10.Checked = False
			 cmbAnt9.Checked = False

			Dim msg As String = If(Common.isEnglish, "failure!", "获取天线失败!")
			Dim ant(1) As Byte
			If uhf.GetANT(ant) Then
				For Each control As Control In Me.gbAnt.Controls
					If TypeOf control Is CheckBox Then
						Dim checkBox As CheckBox = CType(control, CheckBox)
						checkBox.Checked = False
					End If
				Next control
			   ' ant[0] = 00;
			   ' ant[1] = 03;

			 '   MessageBox.Show("ant[0]=" + DataConvert.ByteArrayToHexString(new byte[] { ant[0] }) + "ant[1]="+DataConvert.ByteArrayToHexString(new byte[] { ant[1] }));

				Dim t1 As String = System.Convert.ToString(ant(0), 2)
				Dim t2 As String = System.Convert.ToString(ant(1), 2)

				Dim temp1 As String = "00000000".Substring(0, 8 - t1.Length) & t1
				Dim temp2 As String = "00000000".Substring(0, 8 - t2.Length) & t2

				If temp2.Substring(0, 1) = "1" Then
					cmbAnt8.Checked = True
				End If
				If temp2.Substring(1, 1) = "1" Then
					cmbAnt7.Checked = True
				End If
				If temp2.Substring(2, 1) = "1" Then
					cmbAnt6.Checked = True
				End If
				If temp2.Substring(3, 1) = "1" Then
					cmbAnt5.Checked = True
				End If
				If temp2.Substring(4, 1) = "1" Then
					cmbAnt4.Checked = True
				End If
				If temp2.Substring(5, 1) = "1" Then
					cmbAnt3.Checked = True
				End If
				If temp2.Substring(6, 1) = "1" Then
					cmbAnt2.Checked = True
				End If
				If temp2.Substring(7, 1) = "1" Then
					cmbAnt1.Checked = True
				End If

				If temp1.Substring(0, 1) = "1" Then
					cmbAnt16.Checked = True
				End If
				If temp1.Substring(1, 1) = "1" Then
					cmbAnt15.Checked = True
				End If
				If temp1.Substring(2, 1) = "1" Then
					cmbAnt14.Checked = True
				End If
				If temp1.Substring(3, 1) = "1" Then
					cmbAnt13.Checked = True
				End If
				If temp1.Substring(4, 1) = "1" Then
					cmbAnt12.Checked = True
				End If
				If temp1.Substring(5, 1) = "1" Then
					cmbAnt11.Checked = True
				End If
				If temp1.Substring(6, 1) = "1" Then
					cmbAnt10.Checked = True
				End If
				If temp1.Substring(7, 1) = "1" Then
					cmbAnt9.Checked = True
				End If

				msg = If(Common.isEnglish, "success", "获取天线成功!")
			  '  msg = Common.isEnglish?"success":"获取天线成功!("+ DataConvert.ByteArrayToHexString(ant)+")";
			End If

			showMessage(msg)
		End Sub
		Private Sub btnSetAnt_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetAnt.Click
			Dim msg As String = If(Common.isEnglish, "failure", "设置天线失败!")
			Dim sb1 As New StringBuilder()
			If cmbAnt8.Checked Then
				sb1.Append("1")
			Else
				sb1.Append("0")
			End If
			If cmbAnt7.Checked Then
				sb1.Append("1")
			Else
				sb1.Append("0")
			End If
			If cmbAnt6.Checked Then
				sb1.Append("1")
			Else
				sb1.Append("0")
			End If
			If cmbAnt5.Checked Then
				sb1.Append("1")
			Else
				sb1.Append("0")
			End If
			If cmbAnt4.Checked Then
				sb1.Append("1")
			Else
				sb1.Append("0")
			End If
			If cmbAnt3.Checked Then
				sb1.Append("1")
			Else
				sb1.Append("0")
			End If
			If cmbAnt2.Checked Then
				sb1.Append("1")
			Else
				sb1.Append("0")
			End If
			If cmbAnt1.Checked Then
				sb1.Append("1")
			Else
				sb1.Append("0")
			End If

			Dim sb2 As New StringBuilder()
			If cmbAnt16.Checked Then
				sb2.Append("1")
			Else
				sb2.Append("0")
			End If
			If cmbAnt15.Checked Then
				sb2.Append("1")
			Else
				sb2.Append("0")
			End If
			If cmbAnt14.Checked Then
				sb2.Append("1")
			Else
				sb2.Append("0")
			End If
			If cmbAnt13.Checked Then
				sb2.Append("1")
			Else
				sb2.Append("0")
			End If
			If cmbAnt12.Checked Then
				sb2.Append("1")
			Else
				sb2.Append("0")
			End If
			If cmbAnt11.Checked Then
				sb2.Append("1")
			Else
				sb2.Append("0")
			End If
			If cmbAnt10.Checked Then
				sb2.Append("1")
			Else
				sb2.Append("0")
			End If
			If cmbAnt9.Checked Then
				sb2.Append("1")
			Else
				sb2.Append("0")
			End If

			Dim ant() As Byte = { Convert.ToByte(sb2.ToString(),2), Convert.ToByte(sb1.ToString(),2) }

			Dim flag As Byte = If(cbAntSet.Checked, CByte(1), CByte(0))
			If uhf.SetANT(flag, ant) Then
				msg = If(Common.isEnglish, "success", "设置天线成功!")

			   ' msg = Common.isEnglish ? "success" : "设置天线成功!(" + DataConvert.ByteArrayToHexString(ant) + ")"; ;
			End If
			showMessage(msg)

		End Sub


		Private Sub btnGetANTWorkTime_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetANTWorkTime.Click
			Dim msg As String = "failure"
			Dim ant As Integer = cmbAntWorkTime.SelectedIndex + 1
			Dim time As Integer = 0
			If uhf.GetANTWorkTime(CByte(ant), time) Then
				txtAntWorkTime.Text = time.ToString()
				msg = "success"
			End If

			showMessage(msg)
		End Sub

		Private Sub btnSetANTWorkTime_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetANTWorkTime.Click
			Dim msg As String = "failure"
			Dim ant As Integer = cmbAntWorkTime.SelectedIndex+1
			Dim time As Integer = Integer.Parse(txtAntWorkTime.Text)
			Dim flag As Integer = If(cbAntWorkTime.Checked, 1, 0)
			If uhf.SetANTWorkTime(CByte(ant), CByte(flag), time) Then
				msg = "success"
			End If
			showMessage(msg)
		End Sub
		Private Sub txtWorkTime_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAntWorkTime.TextChanged
			Try
				txtAntWorkTime.Text = txtAntWorkTime.Text.Trim()
				Dim workTime As String = txtAntWorkTime.Text
				If Not StringUtils.IsNumber(workTime) Then
					txtAntWorkTime.Text = ""
					Return
				End If
				Dim time As Integer = Integer.Parse(workTime)
				If time > 65535 Then
					txtAntWorkTime.Text = "65535"
				End If
			Catch ex As Exception
				txtAntWorkTime.Text = ""
			End Try
		End Sub
		Private Sub txtWorkTime_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtAntWorkTime.LostFocus
			Try
				txtAntWorkTime.Text = txtAntWorkTime.Text.Trim()
				Dim workTime As String = txtAntWorkTime.Text
				If Not StringUtils.IsNumber(workTime) Then
					txtAntWorkTime.Text = ""
					Return
				End If
				Dim time As Integer = Integer.Parse(workTime)
				If time <10 Then
					txtAntWorkTime.Text = "10"
				End If
			Catch ex As Exception
				txtAntWorkTime.Text = ""
			End Try
		End Sub


		#End Region

		#Region "区域"
		Private Sub btnRegionGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRegionGet.Click
			'0x01(China1),0x02(China2),0x04(Europe),0x08(USA),0x16(Korea),0x32(Japan)
			Dim msg As String = If(Common.isEnglish, "Get the region failure!", "获取区域失败!")
			Dim region As Byte=0
			If uhf.GetRegion(region) Then
				Select Case region
					Case &H1
						cmbRegion.SelectedIndex = 0
					Case &H2
						cmbRegion.SelectedIndex = 1
					Case &H4
						cmbRegion.SelectedIndex = 2
					Case &H8
						cmbRegion.SelectedIndex = 3
					Case &H16
						cmbRegion.SelectedIndex = 4
					Case &H32
						cmbRegion.SelectedIndex = 5
					Case &H34
						cmbRegion.SelectedIndex = 6
					Case &H33
						cmbRegion.SelectedIndex = 7
					Case &H36
						cmbRegion.SelectedIndex = 8
					Case &H37
						cmbRegion.SelectedIndex = 9
					Case &H3B
						cmbRegion.SelectedIndex = 10
					Case &H3E
						cmbRegion.SelectedIndex = 11
					Case &H3F
						cmbRegion.SelectedIndex = 12


				End Select
				msg=If(Common.isEnglish, "Get the region success", "获取区域成功!")
			End If

			showMessage(msg)
		End Sub

		Private Sub btnRegionSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRegionSet.Click
			'0x01(China1),0x02(China2),0x04(Europe),0x08(USA),0x16(Korea),0x32(Japan)
			Dim flag As Integer = If(cbRegionSave.Checked, 1, 0)
			Dim region As Integer = -1
			Select Case cmbRegion.SelectedIndex
				Case 0
					region = &H1
				Case 1
					region = &H2
				Case 2
					region = &H4
				Case 3
					region = &H8
				Case 4
					region = &H16
				Case 5
					region = &H32
				Case 6
					region = &H34
				Case 7
					region = &H33
				Case 8
					region = &H36
				Case 9
					region = &H37
				Case 10
					region = &H3B
				Case 11
					region = &H3E
				Case 12
					region = &H3F

			End Select
			Dim msg As String = If(Common.isEnglish, "Set the region failure!", "设置区域失败!")
			If region >= 0 Then
				If uhf.SetRegion(CByte(flag), CByte(region)) Then
					msg=If(Common.isEnglish, "Set the region success", "设置区域成功!")
				End If

			End If
			showMessage(msg)
		End Sub
		#End Region
		#Region "温度保护"
		Private Sub GetTemperatureProtect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles GetTemperatureProtect.Click
			Dim flag As Byte = 0
			Dim msg As String = If(Common.isEnglish, "failure!", "失败!")
			If uhf.GetTemperatureProtect(flag) Then
				If flag = 1 Then
					rbEnable.Checked = True
					rbDisable.Checked = False
					  msg = If(Common.isEnglish, "success!", "成功!")
				ElseIf flag =0 Then
					rbEnable.Checked = False
					rbDisable.Checked = True
					  msg = If(Common.isEnglish, "success!", "成功!")
				End If

			End If
			showMessage(msg)
		End Sub
		Private Sub btnSetTemperatureProtect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetTemperatureProtect.Click
			Dim msg As String = If(Common.isEnglish, "failure!", "失败!")
			Dim flag As Integer = 0
			If rbDisable.Checked Then
				flag = 0
			ElseIf rbEnable.Checked Then
				flag = 1
			End If
			If uhf.SetTemperatureProtect(CByte(flag)) Then
				msg = If(Common.isEnglish, "success!", "成功!")
			End If
			showMessage(msg)
		End Sub
		#End Region

		#Region "链路组合"
		Private Sub btnRFLinkGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRFLinkGet.Click
			Dim msg As String = "failure"
			Dim mode As Byte = 0
			If uhf.GetRFLink(mode) Then
				cmbRFLink.SelectedIndex = mode
				msg = "success"
			End If

			showMessage(msg)
		End Sub
		Private Sub btnRFLinkSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRFLinkSet.Click
			Dim msg As String = "failure"
			Dim flag As Integer = If(cbRFLink.Checked, 1, 0)
			If cmbRFLink.SelectedIndex >= 0 Then
				If uhf.SetRFLink(CByte(flag), CByte(cmbRFLink.SelectedIndex)) Then
					msg = "success"
				End If
			End If

			showMessage(msg)
		End Sub

		#End Region

		#Region "FastID"
		Private Sub btnFastIDGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFastIDGet.Click
			Dim flag As Byte = 0
			Dim msg As String = "failure"
			If uhf.GetFastID(flag) Then
				If flag = 0 Then
					rbFastIDEnable.Checked = False
					rbFastIDDisable.Checked = True
					msg = "success"
				ElseIf flag = 1 Then
					rbFastIDEnable.Checked = True
					rbFastIDDisable.Checked = False
					msg = "success"
				End If
			End If
			showMessage(msg)
		End Sub
		Private Sub btnFastIDSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFastIDSet.Click
			Dim flag As Integer = -1
			Dim msg As String = "failure"
			If rbFastIDEnable.Checked Then
				flag = 1
			ElseIf rbFastIDDisable.Checked Then
				flag = 0
			End If

			If flag >= 0 Then
				If uhf.SetFastID(CByte(flag)) Then
					msg = "success"

					If flag = 1 Then
						If uhf.SetTagfocus(0) Then
							rbTagfocusDisable.Checked = True
						End If
						If uhf.setEPCMode(False) Then
							cbInventoryMode.SelectedIndex = 0
						End If
					End If

				End If

			End If

			showMessage(msg)
		End Sub
		#End Region

		#Region "Tagfocus"
		Private Sub btnrbTagfocusGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnrbTagfocusGet.Click
			Dim msg As String = "failure"
			Dim flag As Byte = 0
			If uhf.GetTagfocus(flag) Then
				If flag = 0 Then
					rbTagfocusEnable.Checked = False
					rbTagfocusDisable.Checked = True
					msg = "success"
				ElseIf flag = 1 Then
					rbTagfocusEnable.Checked = True
					rbTagfocusDisable.Checked = False
					msg = "success"
				End If
			End If

			showMessage(msg)
		End Sub
		Private Sub btnrbTagfocusSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnrbTagfocusSet.Click
			Dim flag As Integer = -1
			Dim msg As String = "failure"
			If rbTagfocusEnable.Checked Then
				flag = 1
			ElseIf rbTagfocusDisable.Checked Then
				flag = 0
			End If

			If flag >= 0 Then
				If uhf.SetTagfocus(CByte(flag)) Then
					msg = "success"
					If flag = 1 Then

						If uhf.SetFastID(0) Then
							rbFastIDDisable.Checked = True
						End If

						If uhf.setEPCMode(False) Then
							cbInventoryMode.SelectedIndex = 0
						End If
					End If
				End If

			End If
			showMessage(msg)
		End Sub
		#End Region
		#Region "设置软复位"
		Private Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
			Dim msg As String = If(Common.isEnglish, "failure", "设置软复位失败!")
			If uhf.SetSoftReset() Then
				msg = If(Common.isEnglish, "success", "设置软复位成功!")
			End If

			showMessage(msg)
		End Sub
		#End Region
		#Region "DualSingel"
		Private Sub btnDualSingelGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDualSingelGet.Click
			Dim flag As Byte = 0
			Dim msg As String = "failure"
			If uhf.GetDualSingelMode(flag) Then
				If flag = 0 Then
					rbDual.Checked = True
					rbSingel.Checked = False
					msg = "success"
				ElseIf flag = 1 Then
					rbDual.Checked = False
					rbSingel.Checked = True
					msg = "success"
				End If
			End If
			showMessage(msg)
		End Sub
		Private Sub btnDualSingelSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDualSingelSet.Click
			Dim flag As Integer = -1

			If rbDual.Checked Then
				flag = 0
			ElseIf rbSingel.Checked Then
				flag =1
			End If
			Dim msg As String = "failure"
			If flag >= 0 Then
				Dim save As Integer = If(cbDualSingelSave.Checked, 1, 0)
				If uhf.SetDualSingelMode(CByte(save), CByte(flag)) Then
					msg = "success"
				End If
			End If
			showMessage(msg)
		End Sub
		#End Region



		#Region "协议"
		Private Sub button2_Click_1(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
			Dim msg As String = If(Common.isEnglish, "failure", "获取失败!")
			Dim type As Integer = uhf.GetProtocol()
			If type>-1 Then
				msg = If(Common.isEnglish, "success", "获取成功!")
				cmbProtocol.SelectedIndex = type
			End If
			showMessage(msg)
		End Sub

		Private Sub button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button5.Click
			Dim msg As String = If(Common.isEnglish, "failure", "设置失败!")
			Dim type As Integer = cmbProtocol.SelectedIndex
			If type >= 0 Then
				If uhf.SetProtocol(CByte(type)) Then
					msg = If(Common.isEnglish, "success", "设置成功!")
				End If
			End If
			showMessage(msg)
		End Sub


		#End Region  

		Private Sub panel1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles panel1.Paint

		End Sub

		Private Sub textBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles textBox1.TextChanged
			Dim data As String = textBox1.Text.Trim().Replace(" ", "")
			If data.Length > 0 Then


				Dim index As Integer=textBox1.SelectionStart-1
				If index >= 0 Then
					Dim charData As String = data.Substring(index, 1)
					If charData <> "0" AndAlso charData <> "1" AndAlso charData <> "2" AndAlso charData <> "3" AndAlso charData <> "4" AndAlso charData <> "5" AndAlso charData <> "6" AndAlso charData <> "7" AndAlso charData <> "8" AndAlso charData <> "9" AndAlso charData <> "." Then
						textBox1.Text = textBox1.Text.Remove(index, 1)
						textBox1.SelectionStart = textBox1.Text.Length
					End If
				End If
			End If

		End Sub



		Private Sub MainForm_eventSwitchUI()
		   ' groupBox25.Location = new Point(903, 743);
		   ' groupBox25.Visible = false;
			If Common.isEnglish Then
				Dim index2 As Integer = comboBox2.SelectedIndex
				comboBox2.Items.Clear()
				comboBox2.Items.Add("low voltage")
				comboBox2.Items.Add("high voltage")
				If index2 >= 0 Then
					comboBox2.SelectedIndex = index2
				End If


				Dim index3 As Integer = comboBox3.SelectedIndex
				comboBox3.Items.Clear()
				comboBox3.Items.Add("low voltage")
				comboBox3.Items.Add("high voltage")
				If index2 >= 0 Then
					comboBox3.SelectedIndex = index3
				End If


				Dim index1 As Integer = cmbOutStatus.SelectedIndex
				cmbOutStatus.Items.Clear()
				cmbOutStatus.Items.Add("Disconnect")
				cmbOutStatus.Items.Add("close")
				If index1>=0 Then
				cmbOutStatus.SelectedIndex = index1
				End If

				groupBox19.Text = "Protocol"
				label35.Text = "Protocol:"

				label2.Text = ""
				groupBox6.Text = "Power"
				label24.Text = "Output Power:"
			   ' cbSave.Text = "Save";


				groupBox11.Text = "Region"
				label1.Text = "Region:"
				cbRegionSave.Text = "Save"

				label5.Text = "RFLink:"
				groupBox3.Text = "RFLink"
				cbRFLink.Text = "cbSave"

				groupBox7.Text = "Rrequency"
				label28.Text = "Rrequency:"

				btnReset.Text = "Reset"

				gbAnt.Text = "ANT"
				groupBox8.Text = "TemperatureProtect"
				 label24.Location = New Point(8, 34)
			   ' label1.Location = new Point(51, 39);
			  '  label5.Location = new Point(51, 33);
			  '  label28.Location = new Point(27, 42);

'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: rbFastIDEnable.Text = rbTagfocusEnable.Text = rbEnable.Text = "Enable";
				rbEnable.Text = "Enable"
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: rbFastIDEnable.Text = rbTagfocusEnable.Text = rbEnable.Text
				rbTagfocusEnable.Text = rbEnable.Text
				rbFastIDEnable.Text = rbTagfocusEnable.Text
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: rbFastIDDisable.Text = rbTagfocusDisable.Text = rbDisable.Text = "Disable";
				rbDisable.Text = "Disable"
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: rbFastIDDisable.Text = rbTagfocusDisable.Text = rbDisable.Text
				rbTagfocusDisable.Text = rbDisable.Text
				rbFastIDDisable.Text = rbTagfocusDisable.Text

				gbIP.Text = "Local IP"
				gbIp2.Text = "Destination IP"
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: label9.Text = label31.Text = "IP:";
				label31.Text = "IP:"
				label9.Text = label31.Text
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: label25.Text = label30.Text = "Port:";
				label30.Text = "Port:"
				label25.Text = label30.Text
				groupBox9.Text = "Buzzer="

				Dim index As Integer = If(workMode.SelectedIndex = -1, 0, workMode.SelectedIndex)
				workMode.Items.Clear()
				workMode.Items.Add("command mode")
				workMode.Items.Add("auto mode")
				workMode.Items.Add("trigger mode")

				groupBox1.Text = "cw"

				workMode.SelectedIndex = index


				label39.Text = "input1:"
				label40.Text = "input2:"
				label38.Text = "Relay:"

				gbInventoryMode.Text = "Inventory Mode"
				label45.Text = "Mode:"
				label46.Text = "User Ptr:"
				label47.Text = "User Len:"

				label53.Text = "Subnet mask:"
				label54.Text = "Gateway:"
			Else

				label53.Text = "   子网掩码:"
				label54.Text = "   网关:"

				Dim index1 As Integer = cmbOutStatus.SelectedIndex
				cmbOutStatus.Items.Clear()
				cmbOutStatus.Items.Add("断开")
				cmbOutStatus.Items.Add("闭合")
				If index1 >= 0 Then
				cmbOutStatus.SelectedIndex = index1
				End If


				groupBox19.Text = "协议"
				label35.Text = "协议"

				label2.Text = "设置Gen2之前先获取"
				groupBox6.Text = "功率"
				 label24.Text = "输出功率:"
				'cbSave.Text = "保存";


				groupBox11.Text = "区域"
				label1.Text = "区域:"
				cbRegionSave.Text = "保存"

				label5.Text = "链路组合:"
				groupBox3.Text = "链路"
				cbRFLink.Text = "保存"

				groupBox7.Text = "定频"
				label28.Text = "频点:"

				btnReset.Text = "软件复位"

				gbAnt.Text = "天线"
				groupBox8.Text = "温度保护"

				groupBox1.Text = "连续波"


				label39.Text = "输入1:"
				label40.Text = "输入2:"
				label38.Text = "继电器:"

				label24.Location = New Point(30, 34)
		  '      label1.Location = new Point(63, 39);
		  '      label5.Location = new Point(40, 33);
		 '       label28.Location = new Point(55, 42);

'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: rbFastIDEnable.Text = rbTagfocusEnable.Text = rbEnable.Text = "启用";
				rbEnable.Text = "启用"
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: rbFastIDEnable.Text = rbTagfocusEnable.Text = rbEnable.Text
				rbTagfocusEnable.Text = rbEnable.Text
				rbFastIDEnable.Text = rbTagfocusEnable.Text
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: rbFastIDDisable.Text = rbTagfocusDisable.Text = rbDisable.Text = "禁用";
				rbDisable.Text = "禁用"
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: rbFastIDDisable.Text = rbTagfocusDisable.Text = rbDisable.Text
				rbTagfocusDisable.Text = rbDisable.Text
				rbFastIDDisable.Text = rbTagfocusDisable.Text

				gbIP.Text = "本地IP"
				gbIp2.Text = "目标IP"
			  '  label9.Text = label31.Text = "IP地址:";
			  '  label25.Text = label30.Text = "端口号:";
				groupBox9.Text = "蜂鸣器"


				Dim index As Integer = If(workMode.SelectedIndex = -1, 0, workMode.SelectedIndex)
				workMode.Items.Clear()
				workMode.Items.Add("命令工作模式")
				workMode.Items.Add("自动工作模式")
				workMode.Items.Add("触发模式")
				workMode.SelectedIndex = index

				gbInventoryMode.Text = "盘点模式"
				label45.Text = "模式        :"
				label46.Text = "User起始地址:"
				label47.Text = "User长度    :"

			End If

		End Sub

		Private Sub showMessage(ByVal msg As String, ByVal time As Integer)
			If msg.Contains("失败") OrElse msg.ToLower().Contains("fail") Then
				Dim f As New frmWaitingBox(Sub(obj, args)
					System.Threading.Thread.Sleep(time)
				End Sub, msg)
				f.ShowDialog(Me)
			End If
		End Sub
		Private Sub showMessage(ByVal msg As String)
			If msg.Contains("失败") OrElse msg.ToLower().Contains("fail") Then
				Dim f As New frmWaitingBox(Sub(obj, args)
					System.Threading.Thread.Sleep(500)
				End Sub, msg)
				f.ShowDialog(Me)
			End If
		End Sub


		'设置本地IP
		Private Sub btnSetIPLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetIPLocal.Click
			Dim port As String = txtLocalPort.Text.Trim()

			Dim ip As New StringBuilder()
			ip.Append(ipControlLocal.IpData(0))
			ip.Append(".")
			ip.Append(ipControlLocal.IpData(1))
			ip.Append(".")
			ip.Append(ipControlLocal.IpData(2))
			ip.Append(".")
			ip.Append(ipControlLocal.IpData(3))


			Dim sbMask As New StringBuilder()
			sbMask.Append(ipControlSubnetMask.IpData(0))
			sbMask.Append(".")
			sbMask.Append(ipControlSubnetMask.IpData(1))
			sbMask.Append(".")
			sbMask.Append(ipControlSubnetMask.IpData(2))
			sbMask.Append(".")
			sbMask.Append(ipControlSubnetMask.IpData(3))


			Dim gate As New StringBuilder()
			gate.Append(ipGateway.IpData(0))
			gate.Append(".")
			gate.Append(ipGateway.IpData(1))
			gate.Append(".")
			gate.Append(ipGateway.IpData(2))
			gate.Append(".")
			gate.Append(ipGateway.IpData(3))

			If Not StringUtils.isIP(ip.ToString()) OrElse Not StringUtils.isIP(sbMask.ToString()) OrElse Not StringUtils.isIP(gate.ToString()) Then
				Dim msg As String = If(Common.isEnglish, "failure!", "设置IP失败!")
				showMessage(msg)
				Return
			End If
			If Not StringUtils.IsNumber(port) Then
				Dim msg As String = If(Common.isEnglish, "failure!", "设置IP失败!")
				showMessage(msg)
				Return
			End If

			If ipControlLocal.IpData(0) = "255" Then
				Dim msg As String = If(Common.isEnglish, "failure!", "IP地址不能255开头,设置IP失败!")
				showMessage(msg)
				Return
			End If
			If ipControlSubnetMask.IpData(0) <> "255" Then
				Dim msg As String = If(Common.isEnglish, "failure!", "子网掩码必须255开头,设置IP失败!")
				showMessage(msg)
				Return
			End If
			If ipGateway.IpData(0) = "255" Then
				Dim msg As String = If(Common.isEnglish, "failure!", "网关不能255开头,设置IP失败!")
				showMessage(msg)
				Return
			End If
			If Not uhf.SetLocalIP(ip.ToString(), Integer.Parse(port), sbMask.ToString(), gate.ToString()) Then
				Dim msg As String = If(Common.isEnglish, "failure!", "设置IP失败!")
				showMessage(msg)
				Return
			End If
		End Sub
		'获取本地IP
		Private Sub btnGetIPLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetIPLocal.Click
			Dim startTime As Integer = Environment.TickCount
			Dim sIP As New StringBuilder(20)
			Dim sPort As New StringBuilder(20)
			Dim mask As New StringBuilder(20)
			Dim gate As New StringBuilder(20)
			If uhf.GetLocalIP(sIP, sPort, mask, gate) Then
				ipControlLocal.IpData = sIP.ToString().Split("."c) ' txtLocalIP.Text = sIP.ToString();
				txtLocalPort.Text = sPort.ToString()
				ipGateway.IpData = gate.ToString().Split("."c)
				ipControlSubnetMask.IpData = mask.ToString().Split("."c)
			Else
				Dim msg As String = If(Common.isEnglish, "failure!", "获取IP失败!")
				showMessage(msg)
				Return
			End If

		End Sub

		'获取目标IP
		Private Sub btnGetIpDest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetIpDest.Click
			Dim sIP As New StringBuilder()
			Dim sPort As New StringBuilder()
			If uhf.GetDestIP(sIP, sPort) Then
			   ' txtIPDest.Text = sIP.ToString();
				ipControlDest.IpData = sIP.ToString().Split("."c)
				txtPortDest.Text = sPort.ToString()
			Else
				Dim msg As String = If(Common.isEnglish, "failure!", "获取IP失败!")
				showMessage(msg)
				Return
			End If
		End Sub

		'设置目标IP
		Private Sub btnSetIpDest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetIpDest.Click
			Dim port As String = txtPortDest.Text.Trim()

			Dim tempIp() As String = ipControlDest.IpData
			Dim sb As New StringBuilder()
			sb.Append(tempIp(0))
			sb.Append(".")
			sb.Append(tempIp(1))
			sb.Append(".")
			sb.Append(tempIp(2))
			sb.Append(".")
			sb.Append(tempIp(3))
			Dim ip As String = sb.ToString()

			If Not StringUtils.isIP(ip) Then
				Dim msg As String = If(Common.isEnglish, "failure!", "设置IP失败!")
				showMessage(msg)
				Return
			End If
			If Not StringUtils.IsNumber(port) Then
				Dim msg As String = If(Common.isEnglish, "failure!", "设置IP失败!")
				showMessage(msg)
				Return
			End If
			If ipControlDest.IpData(0) = "255" Then
				Dim msg As String = If(Common.isEnglish, "failure!", "IP地址不能255开头，设置IP失败!")
				showMessage(msg)
				Return
			End If

			If Not uhf.SetDestIP(ip, Integer.Parse(port)) Then
				Dim msg As String = If(Common.isEnglish, "failure!", "设置IP失败!")
				showMessage(msg)
				Return
			End If
		End Sub

		'获取蜂鸣器
		Private Sub btnGetBuzzer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetBuzzer.Click
			Dim mode(9) As Byte
			If Not uhf.UHFGetBuzzer(mode) Then
				Dim msg As String = If(Common.isEnglish, "failure!", "获取失败!")
				showMessage(msg)
				Return
			Else
				If mode(0) = 0 Then

					rbEnableBuzzer.Checked = False
					rbDisableBuzzer.Checked = True
				ElseIf mode(0) = 1 Then
					rbDisableBuzzer.Checked = False
					rbEnableBuzzer.Checked = True
				End If

			End If
		End Sub
		'设置蜂鸣器
		Private Sub btnSetBuzzer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetBuzzer.Click
			'0x00表示关闭蜂鸣器；0x01表示打开蜂鸣器
			Dim mode As Byte =0
			If rbEnableBuzzer.Checked Then
				mode = 1
			ElseIf rbDisableBuzzer.Checked Then
				mode = 0
			Else

				Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
				showMessage(msg)
				Return
			End If

			If Not uhf.UHFSetBuzzer(mode) Then
				Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
				showMessage(msg)
				Return
			End If
		End Sub

		#Region "工作模式"
		Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetWorkMode.Click
			'get
			Dim mode(1) As Byte
			If uhf.GetWorkMode(mode) Then
				If mode(0) = 0 Then
					workMode.SelectedIndex = 0
				ElseIf mode(0) = 1 Then
					workMode.SelectedIndex = 1
				ElseIf mode(0) = 2 Then
					workMode.SelectedIndex = 2
				End If
			Else
				Dim msg As String = If(Common.isEnglish, "failure!", "获取失败!")
				showMessage(msg)
				Return

			End If
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			Dim mode As Byte=CByte(workMode.SelectedIndex)
			If Not uhf.SetWorkMode(mode) Then
				Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
				showMessage(msg)
				Return

			End If

		End Sub

		Private Sub btnWorkModeParaSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWorkModeParaSet.Click
			Dim uhfAPI As UHFAPI = TryCast(uhf, UHFAPI)
			If uhfAPI IsNot Nothing Then
				If txtWT.Text.Trim().Length = 0 Then
					Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
					showMessage(msg)
				End If
				If txtIT.Text.Trim().Length = 0 Then
					Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
					showMessage(msg)
				End If

				Dim input As Integer = cmbInput.SelectedIndex
				Dim workTime As Integer = Integer.Parse(txtWT.Text)
				Dim waitTime As Integer = Integer.Parse(txtIT.Text)
				Dim receiveMode As Integer = comRM.SelectedIndex
				If Not uhfAPI.SetWorkModePara(CByte(input), workTime, waitTime, CByte(receiveMode)) Then
					Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
					showMessage(msg)
				End If
			End If

		End Sub

		Private Sub btnWorkModeParaGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWorkModeParaGet.Click
			Dim uhfAPI As UHFAPI = TryCast(uhf, UHFAPI)
			If uhfAPI IsNot Nothing Then
				 Dim ioControl As Byte=0
				 Dim workTime As Integer=100
				 Dim intervalTime As Integer=0
				 Dim mode As Byte=0
				If uhfAPI.GetWorkModePara(ioControl, workTime, intervalTime, mode) Then
					cmbInput.SelectedIndex = ioControl
					txtWT.Text = workTime.ToString()
					txtIT.Text = intervalTime.ToString()
					comRM.SelectedIndex = mode

				Else
					Dim msg As String = If(Common.isEnglish, "failure!", "失败!")
					showMessage(msg)
				End If
			End If
		End Sub

		#End Region


		Private Sub button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button4.Click

			Dim temp As Integer = uhf.GetTemperatureVal()
			textBox3.Text = temp & ""
			If temp = -1 Then
				Dim msg As String = If(Common.isEnglish, "failure!", "获取失败!")
				showMessage(msg)
			End If
		End Sub

		Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button3.Click
			Try
				Dim temp As String = textBox3.Text
				If temp Is Nothing OrElse temp.Length = 0 Then
					Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
					showMessage(msg)
					Return
				End If
				Dim t As Integer = Integer.Parse(temp)

				If t < 50 OrElse t > 75 Then
					Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
					showMessage(msg)
					Return
				End If


				If Not uhf.SetTemperatureVal(CByte(t)) Then
					Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
					showMessage(msg)
					Return
				End If
			Catch ex As Exception
				Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
				showMessage(msg)
				Return
			End Try
		End Sub

		#Region "GPIO"
		Private Sub button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button6.Click
			Dim data(1) As Byte
			If Not uhf.getIOControl(data) Then
				Dim msg As String = If(Common.isEnglish, "failure!", "获取失败!")
				showMessage(msg)
				Return
			Else
				comboBox2.SelectedIndex = data(0)
				comboBox3.SelectedIndex = data(1)
			End If
		End Sub

		Private Sub button7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button7.Click
			Dim status As Integer = cmbOutStatus.SelectedIndex
			Dim ouput2 As Integer = 1 ' cmbOutPut2.SelectedIndex;
			Dim ouput1 As Integer = 1 ' cmbOutPut1.SelectedIndex;



			If Not uhf.setIOControl(CByte(ouput1), CByte(ouput2), CByte(status)) Then
				Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
				showMessage(msg)
				Return
			End If
		End Sub
		#End Region

		#Region "占空比"
		Private Sub button9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button9.Click
			Dim workTime As Integer = Nothing
			Dim waitTime As Integer = Nothing

			If uhf.getWorkAndWaitTime(workTime, waitTime) Then
				txtWaitTime.Text = waitTime & ""
				txtworkTime.Text = workTime & ""
				Dim msg As String = If(Common.isEnglish, "failure!", "获取成功!")
				showMessage(msg)
				Return
			Else
				Dim msg As String = If(Common.isEnglish, "failure!", "获取失败!")
				showMessage(msg)
				Return
			End If
		End Sub

		Private Sub button8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button8.Click
			'设置
			Dim waitTime As String = txtWaitTime.Text
			Dim workTime As String = txtworkTime.Text
			If waitTime = "" OrElse workTime = "" Then
				Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
				showMessage(msg)
				Return
			End If

			Try

				Dim iwaitTime As Integer = Integer.Parse(waitTime)
				Dim iworkTime As Integer = Integer.Parse(workTime)
				If uhf.setWorkAndWaitTime(iworkTime, iwaitTime, checkBox1.Checked) Then

					Dim msg As String = If(Common.isEnglish, "failure!", "设置成功!")
					showMessage(msg)
				Else
					Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
					showMessage(msg)
				End If

			Catch ex As Exception
				Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
				showMessage(msg)
			End Try


		End Sub
		#End Region

		#Region " 盘点模式设置"

		Private Sub button11_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button11.Click
			Dim userPtr As Byte = 0
			Dim userLen As Byte = 0
			Dim mode As Integer=uhf.getEPCTIDUSERMode(userPtr,userLen)
			Select Case mode
				Case 0
					cbInventoryMode.SelectedIndex = 0
				Case 1
					cbInventoryMode.SelectedIndex = 1
				Case 2
					cbInventoryMode.SelectedIndex = 2
					txtUserLen.Text = userLen & ""
					txtUserPtr.Text = userPtr & ""
				Case Else
					cbInventoryMode.SelectedIndex = -1
			End Select
		End Sub

		Private Sub button10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button10.Click
			Dim mode As Integer = cbInventoryMode.SelectedIndex
			Dim result As Boolean = False
			Dim isSave As Boolean=checkBox2.Checked
			Select Case mode
				Case 0
					result=uhf.setEPCMode(isSave)
				Case 1
					result=uhf.setEPCAndTIDMode(isSave)
				Case 2
					Dim userPtr As Integer = Integer.Parse(txtUserPtr.Text)
					Dim userLen As Integer = Integer.Parse(txtUserLen.Text)
					result = uhf.setEPCAndTIDUSERMode(isSave,CByte(userPtr), CByte(userLen))
			End Select

			If Not result Then
				Dim msg As String = If(Common.isEnglish, "failure!", "设置失败!")
				showMessage(msg)
				Return
			End If
		End Sub

		Private Sub cbInventoryMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbInventoryMode.SelectedIndexChanged
			If cbInventoryMode.SelectedIndex = 2 Then
				txtUserLen.Visible = True
				txtUserPtr.Visible = True
				label46.Visible = True
				label47.Visible = True
			Else
				txtUserLen.Visible = False
				txtUserPtr.Visible = False
				label46.Visible = False
				label47.Visible = False
			End If
		End Sub
		#End Region



		Private Sub workMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles workMode.SelectedIndexChanged
			If workMode.SelectedIndex = 2 Then
				plWorkModePara.Visible = True
				btnWorkModeParaGet_Click(Nothing,Nothing)
			Else
				plWorkModePara.Visible = False

			End If
		End Sub

		Private Sub btnAntennaConnectionState_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAntennaConnectionState.Click
			cbANT1_state.Checked = False
			cbANT2_state.Checked = False
			cbANT3_state.Checked = False
			cbANT4_state.Checked = False
			cbANT5_state.Checked = False
			cbANT6_state.Checked = False
			cbANT7_state.Checked = False
			cbANT8_state.Checked = False

			Dim msg As String = "failure!"
			Dim antstate(0) As Short
			If uhf.GetANTLinkStatus(antstate) Then
				Dim antS As Short = antstate(0)
				If ((antS >> 7) And 1) = 1 Then
					cbANT8_state.Checked = True
				End If
				If ((antS >> 6) And 1) = 1 Then
					cbANT7_state.Checked = True
				End If
				If ((antS >> 5) And 1) = 1 Then
					cbANT6_state.Checked = True
				End If
				If ((antS >> 4) And 1) = 1 Then
					cbANT5_state.Checked = True
				End If
				If ((antS >> 3) And 1) = 1 Then
					cbANT4_state.Checked = True
				End If
				If ((antS >> 2) And 1) = 1 Then
					cbANT3_state.Checked = True
				End If
				If ((antS >> 1) And 1) = 1 Then
					cbANT2_state.Checked = True
				End If
				If (antS And 1) = 1 Then
					cbANT1_state.Checked = True
				End If
				msg = "success"
			End If
			showMessage(msg)
		End Sub

		Private Sub btnCalibration_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCalibration.Click
			Dim result As Integer = uhf.CalibrationVoltage()
			txtCalibration.Text = result & ""

		End Sub

		Private Sub button12_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button12.Click
			Dim statusData(1) As Byte
			Dim msg As String = "failure!"
			If uhf.GetInputStatus(statusData) Then
				cmbInput1.SelectedIndex = statusData(0)
				cmbInput2.SelectedIndex = statusData(1)
				msg = "success"
			End If
			showMessage(msg)

		End Sub

		Private Sub button13_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button13.Click
			Dim outData(4) As Byte
			outData(3) = CByte(cmbOutput1.SelectedIndex)
			outData(4) = CByte(cmbOutput2.SelectedIndex)
			Dim msg As String = "failure!"
			If uhf.SetOutput(outData) Then

				msg = "success"
			End If
			showMessage(msg)
		End Sub


		Private Sub MainForm_SizeChanged(ByVal state As FormWindowState)
			'判断是否选择的是最小化按钮
			panel1.Left = 308
		End Sub



	End Class
End Namespace
