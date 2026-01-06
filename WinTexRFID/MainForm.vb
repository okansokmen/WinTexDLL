Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports WinForm_Test
Imports System.Net
Imports System.Threading
Imports UHFAPP.RFID
Imports UHFAPP.custom.authenticate
Imports UHFAPP.custom.m775Authenticate
Imports UHFAPP.barcode
Imports UHFAPP.Entity
Imports System.Net.Sockets
Imports BLEDeviceAPI

Namespace UHFAPP
	Partial Public Class MainForm
		Inherits BaseForm

		Public Shared MODE As Integer = 1 '0:串口   1:网口    2:usb
		Public Shared ip As String = ""
		Public Shared portData As UInteger = 0

		Public Delegate Sub DelegateOpen(ByVal open As Boolean)
		Public Shared Event eventOpen As DelegateOpen

		Public Delegate Sub DelegateSwitchUI()
		Public Shared Event eventSwitchUI As DelegateSwitchUI

		Public Delegate Sub MainSizeChanged(ByVal state As FormWindowState)
		Public Shared Event eventMainSizeChanged As MainSizeChanged

		Private strOpen As String = "  Open  "
		Private strClose As String = "  Close  "

		Private currentFormName As String = ""
		Private isOpen As Boolean = False
		'INSTANT VB NOTE: The field mainform was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
		Public mainform_Conflict As MainForm = Nothing

		Public Sub New()
			InitializeComponent()
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
			Me.IsMdiContainer = True
			mainform_Conflict = Me
			toolStripComboBox1.Items.Add("中文简体")
			toolStripComboBox1.Items.Add("English")
			toolStripComboBox1.SelectedIndex = 1
			toolStripOpen.Text = "  Open  "
			SwitchShowUI()

			Dim ipEntity As UHFAPP.IPConfig.IPEntity = IPConfig.getIPConfig()
			If ipEntity IsNot Nothing Then
				txtPort.Text = ipEntity.Port.ToString()
				ipControl1.IpData = New String() {ipEntity.Ip(0), ipEntity.Ip(1), ipEntity.Ip(2), ipEntity.Ip(3)}
			End If
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			MenuItemScanEPC_Click(Nothing, Nothing)
			setComPort()
			disableControls()
			combCommunicationMode.SelectedIndex = 2
			' uhf.SetDebug(true);
			btnSearch_Click(Nothing, Nothing)
		End Sub

		Public Sub enableControls()
			MenuItemScanEPC.Enabled = True
			MenuItemReadWriteTag.Enabled = True
			configToolStripMenuItem.Enabled = True
			uHFVersionToolStripMenuItem.Enabled = True
			killLockToolStripMenuItem.Enabled = True
			toolStripMenuItem1.Enabled = True
			uHFUpgradeToolStripMenuItem.Enabled = True
			SetR3ToolStripMenuItem.Enabled = True
			hFToolStripMenuItem.Enabled = True

			combCommunicationMode.Enabled = False
			cmbComPort.Enabled = False
			panel1.Enabled = False

		End Sub
		Public Sub disableControls()
			MenuItemScanEPC.Enabled = False
			MenuItemReadWriteTag.Enabled = False
			configToolStripMenuItem.Enabled = False
			uHFVersionToolStripMenuItem.Enabled = False
			killLockToolStripMenuItem.Enabled = False
			toolStripMenuItem1.Enabled = False
			uHFUpgradeToolStripMenuItem.Enabled = False
			SetR3ToolStripMenuItem.Enabled = False
			hFToolStripMenuItem.Enabled = False

			combCommunicationMode.Enabled = True
			cmbComPort.Enabled = True
			panel1.Enabled = True

		End Sub

		'读写数据
		Private Sub MenuItemReadWriteTag_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuItemReadWriteTag.Click
			ReadWriteTag("")
		End Sub

		Public Sub ReadWriteTag(ByVal tag As String)
			Dim form As Form = ShowForm(New ReadWriteTagForm(), True)
			If form IsNot Nothing Then
				If TypeOf form Is ReadWriteTagForm Then
					DirectCast(form, ReadWriteTagForm).SetTAG(isOpen, tag)
				End If
			End If

		End Sub

		Private Sub MenuItemScanEPC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuItemScanEPC.Click
			Dim form As Form = ShowForm(New ReadEPCForm(isOpen, mainform_Conflict), False)

		End Sub

		Private Sub configToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles configToolStripMenuItem.Click
			Dim form As Form = ShowForm(New ConfigForm(isOpen), False)
		End Sub
		''' <summary>
		''' kill
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub killLockToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles killLockToolStripMenuItem.Click
			ShowForm(New Kill_LockForm(), True)
		End Sub

		Private Sub receiveEPCToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuItemReceiveEPC.Click
			ShowForm(New ReceiveEPC(), True)
		End Sub

		Private Sub testToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles testToolStripMenuItem.Click
			ShowForm(New TestForm(isOpen, mainform_Conflict), False)
		End Sub
		'UHF版本号
		Private Sub uHFVersionToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles uHFVersionToolStripMenuItem.Click
			If isOpen Then
				Dim sb As New StringBuilder()
				Dim f As New frmWaitingBox(Sub(obj, args)
											   Dim hardwareV As String = uhf.GetHardwareVersion().Replace(vbNullChar, "")
											   Dim softWareV As String = uhf.GetSoftwareVersion().Replace(vbNullChar, "")
											   Dim mainboardVer As String = uhf.GetSTM32Version().Replace(vbNullChar, "")
											   Dim version As String = uhf.GetAPIVersion().Replace(vbNullChar, "")
											   'int id = uhf.GetUHFGetDeviceID();
											   If Common.isEnglish Then
												   sb.Append("Hardware version:  ")
												   sb.Append(hardwareV)
												   sb.Append(vbCrLf & "Firmware  version:  ")
												   sb.Append(softWareV)
												   If mainboardVer <> "" Then
													   sb.Append(vbCrLf & "Mainboard  version:  ")
													   sb.Append(mainboardVer)
												   End If
												   'sb.Append("\r\nDevice ID:  ");
												   'sb.Append(id);
											   Else
												   sb.Append("固件版本:  ")
												   sb.Append(softWareV)
												   sb.Append(vbCrLf & "硬件版本:  ")
												   sb.Append(hardwareV)
												   If mainboardVer <> "" Then
													   sb.Append(vbCrLf & "主板版本:  ")
													   sb.Append(mainboardVer)
												   End If
											   End If

											   If version IsNot Nothing AndAlso version <> "" Then
												   sb.Append(vbCrLf & "API Version:  ")
												   sb.Append(version)
											   End If
										   End Sub)
				f.ShowDialog(Me)

				MessageBoxEx.Show(Me, sb.ToString())

			End If
		End Sub


		'打开 
		Private Sub toolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripOpen.Click
			If toolStripOpen.Text = strOpen Then
				Dim type As Integer = combCommunicationMode.SelectedIndex '0
				Dim msg As String = If(Common.isEnglish, "connecting...", "连接中...")

				Dim strCom As Object = cmbComPort.SelectedItem
				Dim f As New frmWaitingBox(Sub(obj, args)
											   Dim result As Boolean = False
											   If type = 0 Then
												   If strCom IsNot Nothing Then
													   Dim ComPort As Integer = Integer.Parse(strCom.ToString().Replace("COM", ""))
													   result = uhf.Open(ComPort)
												   End If
											   ElseIf type = 1 Then
												   If getIPAndPort() Then
													   result = uhf.TcpConnect(ip, portData)
												   End If
											   Else
												   result = uhf.OpenUsb()
											   End If


											   If result Then
												   Me.Invoke(New EventHandler(Sub()
																				  toolStripOpen.Text = strClose
																				  isOpen = True
																				  RaiseEvent eventOpen(True)
																				  enableControls()
																				  If type = 2 Then
																					  SetR3ToolStripMenuItem.Visible = True
																					  hFToolStripMenuItem.Visible = True
																					  hIDModeToolStripMenuItem.Enabled = True
																				  Else
																					  SetR3ToolStripMenuItem.Visible = False
																					  hFToolStripMenuItem.Visible = False
																				  End If

																			  End Sub))

											   Else
												   frmWaitingBox.message_Conflict = "fail"
												   Thread.Sleep(1000)
											   End If
										   End Sub, msg)
				f.ShowDialog(Me)

			Else
				If UHFClose() Then
					disableControls()
					toolStripOpen.Text = strOpen
					hIDModeToolStripMenuItem.Enabled = False
					isOpen = False
					RaiseEvent eventOpen(False)
				End If
			End If

		End Sub

		Private Function getIPAndPort() As Boolean
			If txtPort.Text = "" Then
				MessageBox.Show("fail!")
				Return False
			End If
			Dim port() As Char = txtPort.Text.ToCharArray()
			For k As Integer = 0 To port.Length - 1
				If port(k) <> "0"c AndAlso port(k) <> "1"c AndAlso port(k) <> "2"c AndAlso port(k) <> "3"c AndAlso port(k) <> "4"c AndAlso port(k) <> "5"c AndAlso port(k) <> "6"c AndAlso port(k) <> "7"c AndAlso port(k) <> "8"c AndAlso port(k) <> "9"c Then

					MessageBox.Show("端口号只能输入数字!")
					Return False
				End If
			Next k
			Dim tempIp() As String = ipControl1.IpData
			Dim sb As New StringBuilder()
			sb.Append(tempIp(0))
			sb.Append(".")
			sb.Append(tempIp(1))
			sb.Append(".")
			sb.Append(tempIp(2))
			sb.Append(".")
			sb.Append(tempIp(3))
			ip = sb.ToString()
			portData = UInteger.Parse(txtPort.Text)


			Dim entity As UHFAPP.IPConfig.IPEntity = New IPConfig.IPEntity()
			entity.Port = CInt(portData)
			entity.Ip = tempIp
			IPConfig.setIPConfig(entity)

			Return True
		End Function

		'设置串口
		Private Sub setComPort()
			Dim ArryPort() As String = System.IO.Ports.SerialPort.GetPortNames()
			cmbComPort.Items.Clear()
			For i As Integer = 0 To ArryPort.Length - 1
				cmbComPort.Items.Add(ArryPort(i))
			Next i
			If cmbComPort.Items.Count > 0 Then
				cmbComPort.SelectedIndex = cmbComPort.Items.Count - 1
			End If

		End Sub

		Private Sub MainForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
			btnSearch_Click(Nothing, Nothing)
			UHFClose()
		End Sub

		Private Function UHFClose() As Boolean

			If toolStripOpen.Text.Trim() = strClose.Trim() Then
				If combCommunicationMode.SelectedIndex = 1 Then
					uhf.TcpDisconnect2()
					Return True
				ElseIf combCommunicationMode.SelectedIndex = 0 Then
					Return uhf.Close()
				Else
					uhf.CloseUsb()
					Return True
				End If
				Return False

			End If
			Return False
		End Function



		Private Sub SwitchShowUI()
			If Common.isEnglish Then
				toolStripStatusLabel1.Text = "" '"                                                         "+ "                                                          tip: 1. right key can copy the selected label.     2. double-click the selected label can jump to the r/w  UI.";
				MenuItemScanEPC.Text = "ReadEPC"
				MenuItemReadWriteTag.Text = "ReadWriteTag"
				configToolStripMenuItem.Text = "Configuration"
				killLockToolStripMenuItem.Text = "Kill-Lock"
				uHFVersionToolStripMenuItem.Text = "UHF Info"
				toolStripMenuItem1.Text = "Temperature"
				MenuItemReceiveEPC.Text = "UDP-ReceiveEPC"
				uHFUpgradeToolStripMenuItem.Text = "UHF Upgrade"

				toolStripLabel4.Text = "Mode"
				Dim index As Integer = combCommunicationMode.SelectedIndex '记录上一次的选择记录
				combCommunicationMode.Items.Clear()
				combCommunicationMode.Items.Add("SerialPort")
				combCommunicationMode.Items.Add("network")
				combCommunicationMode.Items.Add("USB")
				combCommunicationMode.SelectedIndex = index
				strOpen = "  Open  "
				strClose = "  Close  "
				toolStripLabel3.Text = "语言"
				MultiUR4ToolStripMenuItem.Text = "Connecting multiple devices"
			Else
				toolStripStatusLabel1.Text = "                                                        " & "                                                        提示：1.右键可以复制选中的标签    2.双击选中的标签可以跳转到读写界面"
				MenuItemScanEPC.Text = "盘点EPC"
				MenuItemReadWriteTag.Text = "读写标签"
				configToolStripMenuItem.Text = "配置"
				killLockToolStripMenuItem.Text = "锁标签"
				MenuItemReceiveEPC.Text = "UDP-ReceiveEPC"
				uHFVersionToolStripMenuItem.Text = "UHF信息"
				toolStripMenuItem1.Text = "温度"
				uHFUpgradeToolStripMenuItem.Text = "UHF固件升级"

				toolStripLabel4.Text = "通信方式"
				Dim index As Integer = combCommunicationMode.SelectedIndex '记录上一次的选择记录
				combCommunicationMode.Items.Clear()
				combCommunicationMode.Items.Add("串口")
				combCommunicationMode.Items.Add("网络")
				combCommunicationMode.Items.Add("USB")
				combCommunicationMode.SelectedIndex = index
				strOpen = " 打开 "
				strClose = " 关闭 "
				toolStripLabel3.Text = "Language"
				MultiUR4ToolStripMenuItem.Text = "连接多台UR4"
			End If

			If toolStripOpen.Text.Trim() = "Open" OrElse toolStripOpen.Text.Trim() = "打开" Then
				toolStripOpen.Text = strOpen
			Else
				toolStripOpen.Text = strClose
			End If

		End Sub


		Private Sub toolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripComboBox1.SelectedIndexChanged
			If toolStripComboBox1.SelectedIndex = 0 Then
				Common.isEnglish = False
			Else
				Common.isEnglish = True
			End If
			SwitchShowUI()

			RaiseEvent eventSwitchUI()
		End Sub


		Private Sub toolStripComboBox2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles combCommunicationMode.TextChanged

			If combCommunicationMode.SelectedIndex = 0 Then
				setComPort()
				panel1.Visible = False
				MODE = 0
				cmbComPort.Visible = True
				lblPortName.Visible = True
				MultiUR4ToolStripMenuItem.Visible = False
				hIDModeToolStripMenuItem.Visible = False
			ElseIf combCommunicationMode.SelectedIndex = 1 Then
				cmbComPort.Visible = False
				lblPortName.Visible = False
				panel1.Visible = True
				MODE = 1
				hIDModeToolStripMenuItem.Visible = False
				' MultiUR4ToolStripMenuItem.Visible = true;
			ElseIf combCommunicationMode.SelectedIndex = 2 Then
				MODE = 2
				panel1.Visible = False
				cmbComPort.Visible = False
				lblPortName.Visible = False
				MultiUR4ToolStripMenuItem.Visible = False
				hIDModeToolStripMenuItem.Visible = True
			End If
		End Sub


		Private Sub toolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripMenuItem1.Click
			If isOpen Then
				Dim f As New frmWaitingBox(Sub(obj, args)
											   Dim Temperature As String = uhf.GetTemperature()
											   Dim temp As String = (If(Common.isEnglish, "Temperature:", "温度:")) & Temperature & "℃"
											   frmWaitingBox.message_Conflict = temp
											   System.Threading.Thread.Sleep(1500)
										   End Sub)
				f.ShowDialog(Me)
			End If
		End Sub

		Private Sub menuStrip1_ItemAdded(ByVal sender As Object, ByVal e As ToolStripItemEventArgs) Handles menuStrip1.ItemAdded
			If e.Item.Text.Length = 0 OrElse e.Item.Text = "最小化(&N)" OrElse e.Item.Text = "还原(&R)" OrElse e.Item.Text = "关闭(&C)" Then '隐藏关闭按钮
				e.Item.Visible = False
			End If
		End Sub
		Private Sub uHFUpgradeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles uHFUpgradeToolStripMenuItem.Click
			Try
				Dim configForm As New UHFUpgradeForm(Common.isEnglish)
				configForm.StartPosition = FormStartPosition.CenterParent
				configForm.ShowDialog()

			Catch ex As Exception

			End Try
		End Sub
		Private Sub MultiUR4ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MultiUR4ToolStripMenuItem.Click
			Me.Hide()
			UHFClose()
			disableControls()
			toolStripOpen.Text = strOpen
			isOpen = False
			RaiseEvent eventOpen(False)
			Dim f As New UHFAPP.multidevice.MainForm()
			f.ShowDialog()
			Me.Show()
		End Sub

		Private Sub SetR3ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SetR3ToolStripMenuItem.Click
			Me.Hide()
			Dim f As New UHFAPP.custom.SetR3Form()
			f.ShowDialog()
			Me.Show()
		End Sub

		Private Sub 加密传输ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles 加密传输ToolStripMenuItem.Click

			Me.Hide()
			Dim f As New UHFAPP.custom.CryptoTransmitForm()
			f.ShowDialog()
			Me.Show()
		End Sub
		Private Sub hFToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles hFToolStripMenuItem.Click
			Me.Hide()
			Dim f As New RFIDMainForm()
			f.ShowDialog()
			Me.Show()
		End Sub

		Public Function ShowForm(ByVal nextForm As Form, ByVal isCache As Boolean) As Form
			isCache = False

			toolStripStatusLabel1.Visible = False
			Dim currForm As Form = Me.ActiveMdiChild
			Dim from As Form = nextForm
			If currForm IsNot Nothing Then
				If currForm.Name = from.Name Then
					Return Nothing
				End If

				If Not isCache Then ' (currForm.Name == "ReadEPCForm" || currForm.Name == "ConfigForm")
					'Common.SaveForm(currForm);
					currForm.Close()
				Else
					currForm.Hide()
					' from = Common.GetForm(nextForm.GetType().Namespace, nextForm.Name, this);
				End If
			End If

			from.WindowState = FormWindowState.Maximized
			from.MdiParent = Me '设置当前窗体为子窗体的父窗体
			from.AutoScaleMode = AutoScaleMode.Inherit
			from.Left = 303
			from.Show() '显示窗体
			Return from
		End Function

		Private Sub 认证ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles 认证ToolStripMenuItem.Click
			Dim authenticateForm As New M775AuthenticateForm()
			authenticateForm.ShowDialog()
		End Sub

		Private Sub hIDModeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles hIDModeToolStripMenuItem.Click
			Dim form As Form = ShowForm(New HidInputForm(), True)
			If form IsNot Nothing Then
				If TypeOf form Is HidInputForm Then
					DirectCast(form, HidInputForm).openState(isOpen)
				End If
			End If
		End Sub

		Private Sub MainForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown

		End Sub






		'**************************
		Public isSearch As Boolean = False
		Private listIP As New List(Of ReaderDeviceInfo)()
		Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
			If btnSearch.Text = "开始搜索" Then
				btnSearch.Text = "停止搜索"
				Dim thred As New Thread(New ThreadStart(AddressOf search))
				thred.Start()
			Else
				'btnSearch.Text = "开始搜索";
				isSearch = False
				' Thread.Sleep(1000);
			End If
		End Sub

		Private Sub search()
			isSearch = True
			Dim UDPrece As New UdpClient(New IPEndPoint(IPAddress.Any, 1111))
			Dim endpoint As New IPEndPoint(IPAddress.Any, 0)
			UDPrece.Client.ReceiveTimeout = 500
			Do While isSearch
				Try
					Dim buf() As Byte = UDPrece.Receive(endpoint)
					'string msg = Encoding.Default.GetString(buf);
					If buf IsNot Nothing AndAlso buf.Length >= 12 Then
						'
						Dim macBytes() As Byte = BLEDeviceAPI.Utils.CopyArray(buf, 0, 6)
						Dim ipBytes() As Byte = BLEDeviceAPI.Utils.CopyArray(buf, 6, 4)

						Dim port As Integer = ((buf(10) And &HFF) << 8) Or (buf(11) And &HFF)

						Dim exists(0) As Boolean
						Dim info As New ReaderDeviceInfo(macBytes, ipBytes, port)
						Dim index As Integer = CheckUtils.getInsertIndex(listIP, info, exists)
						If Not exists(0) Then
							listIP.Insert(index, info)
							lvDevcies.Invoke(New EventHandler(Sub()
																  Dim lv As New ListViewItem()
																  lv.Text = (listIP.Count).ToString()

																  Dim itemIP As New ListViewItem.ListViewSubItem()
																  itemIP.Name = "IP"
																  itemIP.Text = info.ip & ":" & info.port
																  lv.SubItems.Add(itemIP)

																  Dim itemPort As New ListViewItem.ListViewSubItem()
																  itemPort.Name = "MAC"
																  itemPort.Text = info.mac & ""
																  lv.SubItems.Add(itemPort)


																  Dim itemIPAndPort As New ListViewItem.ListViewSubItem()
																  itemIPAndPort.Name = "IPANDMAC"
																  itemIPAndPort.Text = info.ip & info.mac
																  lv.SubItems.Add(itemIPAndPort)

																  lvDevcies.Items.Insert(index, lv)

																  For k As Integer = 0 To lvDevcies.Items.Count - 1
																	  lvDevcies.Items(k).Text = (k + 1) & ""
																  Next k

															  End Sub))
						Else
							listIP(index).lastTime = Environment.TickCount
						End If

					End If


				Catch ex As Exception
					Console.WriteLine("SearchNearbyDevicesForm ex=" & ex.Message)
				End Try



				Dim tempC As Integer = listIP.Count
				For k As Integer = listIP.Count - 1 To 0 Step -1
					If Environment.TickCount - listIP(k).lastTime > 1000 * 30 Then
						listIP.RemoveAt(k)
					End If
				Next k
				If listIP.Count < tempC Then
					lvDevcies.Invoke(New EventHandler(Sub()
														  For k As Integer = lvDevcies.Items.Count - 1 To 0 Step -1
															  Dim ipAndMac As String = lvDevcies.Items(k).SubItems("IPANDMAC").Text
															  Dim flag As Boolean = False
															  For m As Integer = listIP.Count - 1 To 0 Step -1
																  If ipAndMac = listIP(m).ip & listIP(m).mac Then
																	  flag = True

																	  Exit For
																  End If
															  Next m
															  If Not flag Then
																  lvDevcies.Items.RemoveAt(k)
															  End If
														  Next k
														  For k As Integer = 0 To lvDevcies.Items.Count - 1
															  lvDevcies.Items(k).Text = (k + 1) & ""
														  Next k

													  End Sub))
				End If


			Loop

			Try
				UDPrece.Client.Close()
			Catch ex As Exception
				Console.WriteLine("SearchUDPrece.Client.Close  ex=" & ex.Message)
			End Try


		End Sub

		Private Sub lvDevcies_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvDevcies.DoubleClick
			If lvDevcies.SelectedItems.Count <= 0 Then
				Return
			End If
			Dim mainForm As MainForm = DirectCast(Me.ParentForm, MainForm)
			Dim ipAndPort As String = lvDevcies.SelectedItems(0).SubItems(1).Text
			Dim ipdata() As String = ipAndPort.Split(":"c)
			'  string ip = ipAndPort.Split(':')[0] ;
			'  string port = ipAndPort.Split(':')[1];  
			' mainForm.Connect(ip, port);
			If toolStripOpen.Text <> strOpen Then
				If UHFClose() Then
					disableControls()
					toolStripOpen.Text = strOpen
					hIDModeToolStripMenuItem.Enabled = False
					isOpen = False
					RaiseEvent eventOpen(False)
				End If
			End If

			combCommunicationMode.SelectedIndex = 1
			txtPort.Text = ipdata(4)
			ipControl1.IpData = New String() {ipdata(0), ipdata(1), ipdata(2), ipdata(3)}
			toolStripButton1_Click(Nothing, Nothing)
		End Sub

		Public Sub StopSearch()
			btnSearch.Text = "开始搜索"
			isSearch = False
			Thread.Sleep(50)
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			listIP.Clear()
			lvDevcies.Items.Clear()
		End Sub


		Public Shared currState As FormWindowState = FormWindowState.Normal
		Private Sub MainForm_SizeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.SizeChanged
			currState = WindowState
			panel2.Height = Me.Height - 113
			'判断是否选择的是最小化按钮
			RaiseEvent eventMainSizeChanged(WindowState)
		End Sub

	End Class
End Namespace
