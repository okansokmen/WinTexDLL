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

Namespace UHFAPP
	Partial Public Class TempertureTag2MainForm
		Inherits BaseForm

		Public Delegate Sub DelegateOpen(ByVal open As Boolean)
		Public Shared Event eventOpen As DelegateOpen

		Public Delegate Sub DelegateSwitchUI()
		Public Shared Event eventSwitchUI As DelegateSwitchUI

		Private strOpen1 As String = "  Open  "
		Private strOpen2 As String = "  打开  "
		Private strClose1 As String = "  Close  "
		Private strClose2 As String = "  关闭  "

		Private currentType As Integer = 0
		Public isOpen As Boolean = False
		Public mainform As TempertureTag2MainForm = Nothing
		Public Sub New()
			InitializeComponent()
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
			Me.IsMdiContainer = True
			mainform = Me
			toolStripComboBox1.Items.Add("中文简体")
			toolStripComboBox1.Items.Add("English")
			toolStripComboBox1.SelectedIndex = 0
			toolStripOpen.Text = "  Open  "
			SwitchShowUI()

			Dim ipEntity As UHFAPP.IPConfig.IPEntity= IPConfig.getIPConfig()
			If ipEntity IsNot Nothing Then
				txtPort.Text = ipEntity.Port.ToString()
				ipControl1.IpData = New String() { ipEntity.Ip(0), ipEntity.Ip(1), ipEntity.Ip(2), ipEntity.Ip(3) }
			End If
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		   ScanEPC()
		   getComPort()
		   MenuItemScanEPC.Enabled = False
		   MenuItemReadWriteTag.Enabled = False
		   configToolStripMenuItem.Enabled = False
		   uHFVersionToolStripMenuItem.Enabled = False
		   killLockToolStripMenuItem.Enabled = False
		   ToolStripMenuItem.Enabled = False
		   toolStripMenuItem1.Enabled = False
		  ' MenuItemReceiveEPC.Enabled = false;
		   combCommunicationMode.SelectedIndex = 1


		End Sub
		Public Sub enableControls()
			menuStrip1.Enabled = True
		End Sub
		Public Sub disableControls()
			menuStrip1.Enabled = False
		End Sub

		'读写数据
		Private Sub MenuItemReadWriteTag_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuItemReadWriteTag.Click
			 ReadWriteTag("")
		End Sub
		'扫描EPC
		Private Sub MenuItemScanEPC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuItemScanEPC.Click
			ScanEPC()
		End Sub
		Private Sub menuStrip1_ItemAdded(ByVal sender As Object, ByVal e As ToolStripItemEventArgs) Handles menuStrip1.ItemAdded
			If e.Item.Text.Length = 0 OrElse e.Item.Text = "最小化(&N)" OrElse e.Item.Text = "还原(&R)" OrElse e.Item.Text = "关闭(&C)" Then '隐藏关闭按钮
				e.Item.Visible = False
			End If
		End Sub
		'读取EPC窗体
		Private Sub ScanEPC()
			Try
				toolStripStatusLabel1.Visible = True
			   Dim currForm As Form= Me.ActiveMdiChild
			   If currForm Is Nothing OrElse currentType <> 0 Then
					'currentType = 0;
					'ReadEPCForm frm_epcScan = new ReadEPCForm(isOpen, mainform);//子窗体实例化
					'frm_epcScan.WindowState = FormWindowState.Maximized;
					'frm_epcScan.MdiParent = this;//设置当前窗体为子窗体的父窗体
					'frm_epcScan.Show();//显示窗体
					'if (currForm != null)
					'    currForm.Close();

					Dim old As Integer = currentType
					currentType = 0
					Dim frm_epcScan As New ReadEPCFormT2(isOpen, mainform) '子窗体实例化// ReadEPCForm frm_epcScan = (ReadEPCForm)Common.getForm("ReadEPCForm",this);//子窗体实例化
					frm_epcScan.WindowState = FormWindowState.Maximized
					frm_epcScan.MdiParent = Me '设置当前窗体为子窗体的父窗体
					frm_epcScan.Show() '显示窗体
					If currForm IsNot Nothing Then
						If old = 0 OrElse old = 2 Then
							currForm.Close()
						Else
							currForm.Hide()
						End If
					End If
			   End If
			Catch ex As Exception

			End Try
		End Sub

		'读写数据窗体
		Public Sub ReadWriteTag(ByVal tag As String)
			toolStripStatusLabel1.Visible = False
			Try
				Dim currForm As Form = Me.ActiveMdiChild
				If currForm Is Nothing OrElse currentType <> 1 Then

					'currentType = 1;
					 'ReadWriteTagForm frm_readWriter = new ReadWriteTagForm(isOpen,tag);//子窗体实例化
					'frm_readWriter.WindowState = FormWindowState.Maximized;
					'frm_readWriter.MdiParent = this;//设置当前窗体为子窗体的父窗体
					'frm_readWriter.Show();//显示窗体
					'if (currForm != null)
					'    currForm.Close();

					Dim old As Integer = currentType
					currentType = 1
					Dim frm_readWriter As ReadWriteTagForm = DirectCast(Common.GetForm("ReadWriteTagForm", Me), ReadWriteTagForm)
					frm_readWriter.SetTAG(isOpen, tag)
					frm_readWriter.WindowState = FormWindowState.Maximized
					frm_readWriter.MdiParent = Me '设置当前窗体为子窗体的父窗体
					frm_readWriter.Show() '显示窗体
					If currForm IsNot Nothing Then
						If old = 0 OrElse old = 2 Then
							currForm.Close()
						Else
							currForm.Hide()
						End If
					End If
				End If


			Catch ex As Exception

			End Try
		End Sub

		'配置界面的窗体
		Private Sub configToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles configToolStripMenuItem.Click
			toolStripStatusLabel1.Visible = False
			Try
			   Dim currForm As Form= Me.ActiveMdiChild
			   If currForm Is Nothing OrElse currentType <> 2 Then
					'currentType =2;
					'ConfigForm configForm = new ConfigForm(isOpen);//子窗体实例化
					'configForm.WindowState = FormWindowState.Maximized;
					'configForm.MdiParent = this;//设置当前窗体为子窗体的父窗体
					'configForm.Show();//显示窗体
					'if (currForm != null)
					'    currForm.Close();

					Dim old As Integer = currentType
					currentType = 2
					Dim configForm As New ConfigForm(isOpen) '子窗体实例化 ConfigForm configForm = (ConfigForm)Common.getForm("ConfigForm", this);
					configForm.WindowState = FormWindowState.Maximized
					configForm.MdiParent = Me '设置当前窗体为子窗体的父窗体
					configForm.Show() '显示窗体
					If currForm IsNot Nothing Then
						If old = 0 OrElse old = 2 Then
							currForm.Close()
						Else
							currForm.Hide()
						End If
					End If


			   End If
			Catch ex As Exception

			End Try
		End Sub


		'lock窗体
		Private Sub Lock()
			toolStripStatusLabel1.Visible = False
			Try
				Dim currForm As Form = Me.ActiveMdiChild
				If currForm Is Nothing OrElse currentType <> 3 Then
					'currentType = 3;
					'Kill_LockForm frm_readWriter = new Kill_LockForm(isOpen);//子窗体实例化
					'frm_readWriter.WindowState = FormWindowState.Maximized;
					'frm_readWriter.MdiParent = this;//设置当前窗体为子窗体的父窗体
					'frm_readWriter.Show();//显示窗体
					'if (currForm != null)
					'    currForm.close();

					Dim old As Integer = currentType
					currentType = 3
					Dim frm_readWriter As Kill_LockForm = DirectCast(Common.GetForm("Kill_LockForm", Me), Kill_LockForm)
					frm_readWriter.WindowState = FormWindowState.Maximized
					frm_readWriter.MdiParent = Me '设置当前窗体为子窗体的父窗体
					frm_readWriter.Show() '显示窗体
					If currForm IsNot Nothing Then
						If old = 0 OrElse old = 2 Then
							currForm.Close()
						Else
							currForm.Hide()
						End If
					End If
				End If

			Catch ex As Exception

			End Try
		End Sub

		'UHF版本号
		Private Sub uHFVersionToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles uHFVersionToolStripMenuItem.Click
			If isOpen Then
				Dim sb As New StringBuilder()
				Dim f As New frmWaitingBox(Sub(obj, args)
					Dim hardwareV As String = uhf.GetHardwareVersion()
					Dim softWareV As String = uhf.GetSoftwareVersion()
					'int id = uhf.GetUHFGetDeviceID();
					If Common.isEnglish Then
						sb.Append("Hardware version:  ")
						sb.Append(hardwareV)
						sb.Append(vbCrLf & "Firmware  version:  ")
						sb.Append(softWareV)
						'sb.Append("\r\nDevice ID:  ");
						'sb.Append(id);
					Else
						sb.Append("硬件版本:  ")
						sb.Append(hardwareV)
						sb.Append(vbCrLf & "固件版本:  ")
						sb.Append(softWareV)
						'sb.Append("\r\n模块ID:  ");
						'sb.Append(id);
					End If
				End Sub)
				f.ShowDialog(Me)

				MessageBoxEx.Show(Me,sb.ToString())

			End If
		End Sub

		'打开串口
		Private Sub toolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripOpen.Click

			If toolStripOpen.Text = strOpen1 OrElse toolStripOpen.Text = strOpen2 Then
				'----------------------------

				Dim com As Integer = 0 '串口号
				Dim ip As String = "" 'ip地址
				Dim portData As UInteger = 0 '端口
				Dim type As Integer = combCommunicationMode.SelectedIndex '0

				If type = 0 Then
					'串口
					com = Integer.Parse(cmbComPort.SelectedItem.ToString().Replace("COM", ""))
				ElseIf type = 1 Then
					If txtPort.Text = "" Then
						MessageBox.Show("fail!")
						Return
					End If
					'网口
					Dim port() As Char = txtPort.Text.ToCharArray()
					For k As Integer = 0 To port.Length - 1
						If port(k) <> "0"c AndAlso port(k) <> "1"c AndAlso port(k) <> "2"c AndAlso port(k) <> "3"c AndAlso port(k) <> "4"c AndAlso port(k) <> "5"c AndAlso port(k) <> "6"c AndAlso port(k) <> "7"c AndAlso port(k) <> "8"c AndAlso port(k) <> "9"c Then
							MessageBox.Show("fail!")
							Return
						End If
					Next k
					portData = UInteger.Parse(txtPort.Text)
					ip = cmbComPort.Text
				Else
					MessageBox.Show("fail!")
					Return

				End If
				'---------------------------
				Dim msg As String = If(Common.isEnglish, "connecting...", "连接中...")
				Dim f As New frmWaitingBox(Sub(obj, args)
					Dim result As Boolean = False ' UHFOpen();

					If type = 0 Then
						result = CType(uhf, UHFAPI).Open(com)
					Else
						result = CType(uhf, UHFAPI).TcpConnect(ip, portData)
					End If
					If result Then
						Me.Invoke(New EventHandler(Sub()
							combCommunicationMode.Enabled = False
							cmbComPort.Enabled = False
							If toolStripOpen.Text = strOpen1 Then
								toolStripOpen.Text = strClose1
							Else
								toolStripOpen.Text = strClose2
							End If

							isOpen = True
							RaiseEvent eventOpen(True)

							MenuItemScanEPC.Enabled = True
							MenuItemReadWriteTag.Enabled = True
							configToolStripMenuItem.Enabled = True
							uHFVersionToolStripMenuItem.Enabled = True
							killLockToolStripMenuItem.Enabled = True
						  '  MenuItemReceiveEPC.Enabled = true;
							toolStripMenuItem1.Enabled = True
							ToolStripMenuItem.Enabled = True
						End Sub))
					Else
						'MessageBox.Show("f");
						  frmWaitingBox.message_Conflict = "fail"
						  Thread.Sleep(1000)
					End If
				End Sub, msg)
				f.ShowDialog(Me)

			Else
				If UHFClose() Then
					combCommunicationMode.Enabled = True
					cmbComPort.Enabled = True
					If toolStripOpen.Text = strClose1 Then
						toolStripOpen.Text = strOpen1
					Else
						toolStripOpen.Text = strOpen2
					End If

					isOpen = False
					RaiseEvent eventOpen(False)
					MenuItemScanEPC.Enabled = False
					MenuItemReadWriteTag.Enabled = False
					configToolStripMenuItem.Enabled = False
					uHFVersionToolStripMenuItem.Enabled = False
					killLockToolStripMenuItem.Enabled = False
					toolStripMenuItem1.Enabled = False
					ToolStripMenuItem.Enabled = False
				   ' MenuItemReceiveEPC.Enabled = false;


				End If
			End If

		End Sub
		'设置串口
		Private Sub getComPort()
			Dim ArryPort() As String = System.IO.Ports.SerialPort.GetPortNames()
			cmbComPort.Items.Clear()
			For i As Integer = 0 To ArryPort.Length - 1
				cmbComPort.Items.Add(ArryPort(i))
			Next i
			If cmbComPort.Items.Count > 0 Then
				cmbComPort.SelectedIndex = cmbComPort.Items.Count-1
			End If

		End Sub

		Private Sub MainForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
			UHFClose()
		End Sub

		Private Sub killLockToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles killLockToolStripMenuItem.Click
			Lock()
		End Sub

		Private Sub toolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripMenuItem1.Click
			If isOpen Then

				Dim f As New frmWaitingBox(Sub(obj, args)
					Dim Temperature As String = uhf.GetTemperature()
					Dim temp As String =(If(Common.isEnglish, "Temperature:", "温度:")) & Temperature & "℃"
					frmWaitingBox.message_Conflict = temp
					System.Threading.Thread.Sleep(1500)
				End Sub)
				f.ShowDialog(Me)



			End If
		End Sub




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

				toolStripLabel4.Text = "Mode"
				Dim index As Integer = combCommunicationMode.SelectedIndex '记录上一次的选择记录
				combCommunicationMode.Items.Clear()
				combCommunicationMode.Items.Add("SerialPort")
				combCommunicationMode.Items.Add("network")
				combCommunicationMode.Items.Add("USB")
				combCommunicationMode.SelectedIndex = index

				If toolStripOpen.Text = strOpen2 Then
					toolStripOpen.Text = strOpen1
				ElseIf toolStripOpen.Text =strClose2 Then
					toolStripOpen.Text = strClose1
				End If

				If btnConnect.Text = strOpen2 Then
					btnConnect.Text = strOpen1
				ElseIf btnConnect.Text = strClose2 Then
					btnConnect.Text = strClose1
				End If
				toolStripLabel3.Text = "语言"
			Else
				toolStripStatusLabel1.Text = "                                                        " & "                                                        提示：1.右键可以复制选中的标签    2.双击选中的标签可以跳转到读写界面"
				MenuItemScanEPC.Text = "盘点EPC"
				MenuItemReadWriteTag.Text = "读写标签"
				configToolStripMenuItem.Text = "配置"
				killLockToolStripMenuItem.Text = "锁标签"
				MenuItemReceiveEPC.Text = "UDP-ReceiveEPC"
				uHFVersionToolStripMenuItem.Text = "UHF信息"
				toolStripMenuItem1.Text = "温度"

				toolStripLabel4.Text = "通信方式"
				Dim index As Integer = combCommunicationMode.SelectedIndex '记录上一次的选择记录
				combCommunicationMode.Items.Clear()
				combCommunicationMode.Items.Add("串口")
				combCommunicationMode.Items.Add("网络")
				combCommunicationMode.Items.Add("USB")
				combCommunicationMode.SelectedIndex = index


				If toolStripOpen.Text = strOpen1 Then
					toolStripOpen.Text = strOpen2
				ElseIf toolStripOpen.Text = strClose1 Then
					toolStripOpen.Text = strClose2
				End If


				If btnConnect.Text = strOpen1 Then
					btnConnect.Text = strOpen2
				ElseIf btnConnect.Text = strClose1 Then
					btnConnect.Text = strClose2
				End If
				toolStripLabel3.Text = "Language"

			End If

		End Sub

		Private Sub toolStripComboBox1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripComboBox1.Click


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
				getComPort()
				panel1.Visible = False
				plUsb.Visible = False
			ElseIf combCommunicationMode.SelectedIndex = 1 Then
				panel1.Visible = True
				plUsb.Visible = False
			ElseIf combCommunicationMode.SelectedIndex = 2 Then
				plUsb.Visible = True
			End If
		End Sub


		Private Sub getIP()

			Dim ArryPort() As String = System.IO.Ports.SerialPort.GetPortNames()
			cmbComPort.Items.Clear()

			Dim myEntry As IPHostEntry = Dns.GetHostByName(Dns.GetHostName())
			For k As Integer = 0 To myEntry.AddressList.Length - 1

				Dim myIPAddress As New IPAddress(myEntry.AddressList(k).Address)

				cmbComPort.Items.Add(myIPAddress.ToString())
			Next k

			If cmbComPort.Items.Count > 0 Then
				cmbComPort.SelectedIndex = cmbComPort.Items.Count - 1
			End If
		End Sub



		Private Function UHFOpen() As Boolean
			Dim result As Boolean = False
			result = CType(uhf, UHFAPI).Open(Integer.Parse(cmbComPort.SelectedItem.ToString().Replace("COM", "")))
			Return result
		End Function

		Private Function UHFClose() As Boolean

			If combCommunicationMode.SelectedIndex = 1 Then
				CType(uhf, UHFAPI).TcpDisconnect2()
				Return True
			Else
				Return uhf.Close()
			End If
			Return False

		End Function



		Private Sub btnConnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConnect.Click

			If btnConnect.Text = strOpen1 OrElse btnConnect.Text = strOpen2 Then
				'----------------------------
				If txtPort.Text = "" Then
					MessageBox.Show("fail!")
					Return
				End If
				Dim port() As Char = txtPort.Text.ToCharArray()
				For k As Integer = 0 To port.Length - 1
					If port(k) <> "0"c AndAlso port(k) <> "1"c AndAlso port(k) <> "2"c AndAlso port(k) <> "3"c AndAlso port(k) <> "4"c AndAlso port(k) <> "5"c AndAlso port(k) <> "6"c AndAlso port(k) <> "7"c AndAlso port(k) <> "8"c AndAlso port(k) <> "9"c Then

						MessageBox.Show("端口号只能输入数字!")
						Return
					End If
				Next k


				Dim portData As UInteger = UInteger.Parse(txtPort.Text)
				Dim tempIp() As String = ipControl1.IpData
				Dim sb As New StringBuilder()
				sb.Append(tempIp(0))
				sb.Append(".")
				sb.Append(tempIp(1))
				sb.Append(".")
				sb.Append(tempIp(2))
				sb.Append(".")
				sb.Append(tempIp(3))
				Dim ip As String = sb.ToString()


				'---------------------------
				Dim msg As String = If(Common.isEnglish, "connecting...", "连接中...")
				Dim f As New frmWaitingBox(Sub(obj, args)
					Dim result As Boolean = False
					result = CType(uhf, UHFAPI).TcpConnect(ip, portData)
					If result Then
						Me.Invoke(New EventHandler(Sub()
							combCommunicationMode.Enabled = False
							txtPort.Enabled = False
							ipControl1.Enabled = False
							If btnConnect.Text = strOpen1 Then
								btnConnect.Text = strClose1
							Else
								btnConnect.Text = strClose2
							End If

							isOpen = True
							RaiseEvent eventOpen(True)

							Dim entity As UHFAPP.IPConfig.IPEntity = New IPConfig.IPEntity()
							entity.Port = CInt(portData)
							entity.Ip = ipControl1.IpData
							IPConfig.setIPConfig(entity)

							MenuItemScanEPC.Enabled = True
							MenuItemReadWriteTag.Enabled = True
							configToolStripMenuItem.Enabled = True
							uHFVersionToolStripMenuItem.Enabled = True
							killLockToolStripMenuItem.Enabled = True
							toolStripMenuItem1.Enabled = True
							ToolStripMenuItem.Enabled = True
						  '  MenuItemReceiveEPC.Enabled = true;
						End Sub))
					Else
						'MessageBox.Show("f");
						frmWaitingBox.message_Conflict = "fail"
						Thread.Sleep(1000)
					End If
				End Sub, msg)
				f.ShowDialog(Me)

			Else

				CType(uhf, UHFAPI).TcpDisconnect2()
				combCommunicationMode.Enabled = True
					txtPort.Enabled = True
					ipControl1.Enabled = True
					If btnConnect.Text = strClose1 Then
						btnConnect.Text = strOpen1
					Else
						btnConnect.Text = strOpen2
					End If

					isOpen = False
					RaiseEvent eventOpen(False)
					MenuItemScanEPC.Enabled = False
					MenuItemReadWriteTag.Enabled = False
					configToolStripMenuItem.Enabled = False
					uHFVersionToolStripMenuItem.Enabled = False
					killLockToolStripMenuItem.Enabled = False
					toolStripMenuItem1.Enabled = False
					ToolStripMenuItem.Enabled = False
					'MenuItemReceiveEPC.Enabled = false;
			End If

		End Sub

		Private Sub receiveEPCToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MenuItemReceiveEPC.Click
			toolStripStatusLabel1.Visible = False
			Try
				Dim currForm As Form = Me.ActiveMdiChild
				If currForm Is Nothing OrElse currentType <> 4 Then
					'currentType = 4;
					'ReceiveEPC configForm = new ReceiveEPC();//子窗体实例化
					'configForm.WindowState = FormWindowState.Maximized;
					'configForm.MdiParent = this;//设置当前窗体为子窗体的父窗体
					'configForm.Show();//显示窗体
					'if (currForm != null)
					'    currForm.Close();

					Dim old As Integer = currentType
					currentType = 4
					Dim configForm As ReceiveEPC = DirectCast(Common.GetForm("ReceiveEPC", Me), ReceiveEPC) '子窗体实例化
					configForm.WindowState = FormWindowState.Maximized
					configForm.MdiParent = Me '设置当前窗体为子窗体的父窗体
					configForm.Show() '显示窗体
					If currForm IsNot Nothing Then
						 If old = 0 OrElse old = 2 Then
							currForm.Close()
						Else
							currForm.Hide()
						End If
					End If
				End If
			Catch ex As Exception

			End Try
		End Sub

		Private Sub testToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles testToolStripMenuItem.Click

		End Sub

		Private Sub btnUsbOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUsbOpen.Click

			If btnUsbOpen.Text = strOpen1 OrElse btnUsbOpen.Text = strOpen2 Then
				'----------------------------
				Dim result As Boolean = False ' uhf.Open(this);
				If result Then
					If btnUsbOpen.Text = strOpen1 Then
						btnUsbOpen.Text = strClose1
					Else
						btnUsbOpen.Text = strClose2
					End If

					isOpen = True
					RaiseEvent eventOpen(True)
					combCommunicationMode.Enabled = False
					MenuItemScanEPC.Enabled = True
					MenuItemReadWriteTag.Enabled = True
					configToolStripMenuItem.Enabled = True
					uHFVersionToolStripMenuItem.Enabled = True
					killLockToolStripMenuItem.Enabled = True
					toolStripMenuItem1.Enabled = True
					ToolStripMenuItem.Enabled = True
				Else
					MessageBox.Show("fail")

				End If

			Else
				If UHFClose() Then
					If btnUsbOpen.Text = strClose1 Then
						btnUsbOpen.Text = strOpen1
					Else
						btnUsbOpen.Text = strOpen2
					End If

					isOpen = False
					RaiseEvent eventOpen(False)
					combCommunicationMode.Enabled = True
					MenuItemScanEPC.Enabled = False
					MenuItemReadWriteTag.Enabled = False
					configToolStripMenuItem.Enabled = False
					uHFVersionToolStripMenuItem.Enabled = False
					killLockToolStripMenuItem.Enabled = False
					toolStripMenuItem1.Enabled = False
					ToolStripMenuItem.Enabled = False
				End If
			End If
		End Sub


		Private Sub ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripMenuItem.Click
			toolStripStatusLabel1.Visible = False
			Try
				Dim currForm As Form = Me.ActiveMdiChild
				If currForm Is Nothing OrElse currentType <> 11 Then
					Dim old As Integer = currentType
					currentType = 11
				   ' ConfigForm2 config = (ConfigForm2)Common.getForm("ConfigForm2", this);
					Dim config As New TempertureTag2ReadEPCForm(isOpen, mainform)
					config.WindowState = FormWindowState.Maximized
					config.MdiParent = Me '设置当前窗体为子窗体的父窗体
					config.Show() '显示窗体
					If currForm IsNot Nothing Then
						If old = 0 OrElse old = 2 Then
							currForm.Close()
						Else
							currForm.Hide()
						End If
					End If
				End If


			Catch ex As Exception

			End Try
		End Sub
	End Class
End Namespace
