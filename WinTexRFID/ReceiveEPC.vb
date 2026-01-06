Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Net.Sockets
Imports System.Net
Imports System.Threading
Imports System.Collections
Imports UHFAPP.interfaces
Imports UHFAPP.Receive
Imports UHFAPP.utils
Imports BLEDeviceAPI

Namespace UHFAPP
	Partial Public Class ReceiveEPC
		Inherits BaseForm

	   Private Const max As Integer =1024 * 1024
	   Private uhfOriginalData(max - 1) As Byte

	   Private isRuning As Boolean = True
	   Private isOpen As Boolean = False


	   Private total As Integer = 0
	   Private beginTime As Long = Environment.TickCount

	   Private epcList As New List(Of EpcInfo)()


	   ' 将text更新的界面控件的委托类型
	   Private Delegate Sub SetTextCallback(ByVal epc As String, ByVal tid As String, ByVal rssi As String, ByVal count As String, ByVal ant As String, ByVal user As String)
		Private setTextCallback2 As SetTextCallback



		Private Delegate Sub GetRemotelyIPCallback(ByVal remoteip As String)
	   Private RemotelyIPCallback As GetRemotelyIPCallback
		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub ReceiveEPC_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			AddHandler MainForm.eventMainSizeChanged, AddressOf MainForm_SizeChanged
			setTextCallback2 = New SetTextCallback(AddressOf UpdataEPC)
			cmbMode.SelectedIndex = 0
			RemotelyIPCallback = New GetRemotelyIPCallback(AddressOf GetRemoteIP)
			InitIPAndSerialPort()
		End Sub
		Private Sub ReceiveEPC_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
			RemoveHandler MainForm.eventMainSizeChanged, AddressOf MainForm_SizeChanged
			isOpen = False
			isRuning = False
			DisConnect()
		End Sub

		Private Sub btnScanEPC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnScanEPC.Click
			If btnScanEPC.Text = "Start" Then
				If Connect() Then
					isRuning = True
					cmbCom.Enabled = False
					cmbMode.Enabled = False
					btnScanEPC.Text = "Stop"
					Call (New Thread(New ThreadStart(Sub()
						ReadEPC()
					End Sub))).Start()
				Else
					MessageBox.Show("fail")
				End If
			Else
				isRuning = False
				cmbCom.Enabled = True
				cmbMode.Enabled = True
				btnScanEPC.Text = "Start"
				DisConnect()
			End If
		End Sub


		Private Sub InitIPAndSerialPort()
			Dim ArryPort() As String = System.IO.Ports.SerialPort.GetPortNames()
			cmbCom.Items.Clear()
			For i As Integer = 0 To ArryPort.Length - 1
				cmbCom.Items.Add(ArryPort(i))
			Next i
			If cmbCom.Items.Count > 0 Then
				cmbCom.SelectedIndex = cmbCom.Items.Count - 1
			End If

		End Sub

		Private Function Connect() As Boolean
		   ' int ComPort = int.Parse(cmbCom.SelectedItem.ToString().ToString().Replace("COM", ""));
			If cmbMode.SelectedIndex = 0 Then
				Dim result As Boolean = UHFAPI.BindUDP(Integer.Parse(textBox2.Text)) = 0
				isOpen = result
				Return result
			Else
				Dim ComPort As Integer = Integer.Parse(cmbCom.SelectedItem.ToString().ToString().Replace("COM", ""))
				Dim result As Boolean = UHFAPI.getInstance().Open(ComPort)
				isOpen = result
				Return result

			End If


		End Function

		Private Sub DisConnect()
			If cmbMode.SelectedIndex = 0 Then
				UHFAPI.UnbindUDP()
				isOpen = False
			Else
				UHFAPI.getInstance().Close()
				isOpen = False
			End If
		End Sub




		'获取epc
		Private Sub ReadEPC()
			Try
				beginTime = Environment.TickCount

				Do
					Dim info As UHFTAGInfo = uhf.uhfGetReceived()
					If info IsNot Nothing Then
						Me.BeginInvoke(setTextCallback2, New Object() {info.Epc, info.Tid, info.Rssi, "1", info.Ant, info.User})
					Else
						If isRuning Then
							Thread.Sleep(5)
						Else
							Exit Do
						End If
					End If



				Loop

				lblTime.Invoke(New EventHandler(Sub()
					lblTime.Text = ((Environment.TickCount - beginTime) \ 1000) & "(s)"

				End Sub))

			Catch ex As Exception

			End Try


		End Sub












		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			epcList.Clear()
			lvEPC.Items.Clear()
			lblTime.Text = "0"
			lblTotal.Text = "0"
			label3.Text = "0"
			total = 0
			beginTime = Environment.TickCount
		End Sub



		Private Sub UpdataEPC(ByVal epc As String, ByVal tid As String, ByVal rssi As String, ByVal count As String, ByVal ant As String, ByVal user As String)
			Dim exist(0) As Boolean
			Dim id As Integer = CheckUtils.getInsertIndex(epcList, epc, tid, exist)
			If exist(0) Then
				lvEPC.Items(id).SubItems(2).Text = tid
				lvEPC.Items(id).SubItems(3).Text = user
				lvEPC.Items(id).SubItems(4).Text = rssi
				lvEPC.Items(id).SubItems(5).Text = (Integer.Parse(lvEPC.Items(id).SubItems(5).Text) + Integer.Parse(count)).ToString()
				lvEPC.Items(id).SubItems(6).Text = ant
			Else
				total += 1
				Dim lv As New ListViewItem()
				Dim index As Integer = lvEPC.Items.Count + 1
				lv.Text = index.ToString()
				lv.SubItems.Add(epc)
				lv.SubItems.Add(tid)
				lv.SubItems.Add(user)
				lv.SubItems.Add(rssi)
				lv.SubItems.Add(count)
				lv.SubItems.Add(ant)
				lvEPC.Items.Insert(id, lv)
				lblTotal.Text = total.ToString()
				epcList.Insert(id, New EpcInfo(epc, Integer.Parse(count), DataConvert.HexStringToByteArray(epc), DataConvert.HexStringToByteArray(tid)))
			End If
			label3.Text = (Integer.Parse(label3.Text) + 1).ToString()
			lblTime.Text = ((Environment.TickCount - beginTime) \ 1000) & "(s)"
		End Sub

		Private Sub GetRemoteIP(ByVal ip As String)
			textBox1.Text = ip
		End Sub

		Private Sub textBox2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles textBox2.TextChanged
			If textBox2.Text <> "" Then
				Dim port() As Char = textBox2.Text.ToCharArray()
				For k As Integer = 0 To port.Length - 1
					If port(k) <> "0"c AndAlso port(k) <> "1"c AndAlso port(k) <> "2"c AndAlso port(k) <> "3"c AndAlso port(k) <> "4"c AndAlso port(k) <> "5"c AndAlso port(k) <> "6"c AndAlso port(k) <> "7"c AndAlso port(k) <> "8"c AndAlso port(k) <> "9"c Then
						textBox2.Text = ""
						Return
					End If
				Next k
			End If

		End Sub


		Private Sub cmbMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbMode.SelectedIndexChanged
			If cmbMode.SelectedIndex = 0 Then
				panel2.Visible = False
				panel1.Visible = True
			ElseIf cmbMode.SelectedIndex = 1 Then
				panel1.Visible = False
				panel2.Visible = True
			End If
		End Sub

		Private Sub MainForm_SizeChanged(ByVal state As FormWindowState)
			'判断是否选择的是最小化按钮
			panel1.Left = 308
		End Sub
	End Class
End Namespace
