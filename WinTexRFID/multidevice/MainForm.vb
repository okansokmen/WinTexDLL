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
Imports BLEDeviceAPI
Imports UHFAPP.utils

Namespace UHFAPP.multidevice
	Partial Public Class MainForm
		Inherits BaseForm

		Private isRuning As Boolean = False
		Private Delegate Sub SetTextCallback(ByVal epc As String, ByVal rssi As String, ByVal count As String, ByVal ant As String, ByVal ip As String)
		Private setTextCallback2 As SetTextCallback
		Private epcList As New List(Of EpcInfo)()
		Private FlagInventory1 As Boolean = False
		Private FlagInventory2 As Boolean = False
		Public Sub New()
			InitializeComponent()
			setTextCallback2 = New SetTextCallback(AddressOf UpdataEPC)
		End Sub
		''' <summary>
		''' IP1连接
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnConn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConn.Click
			 Dim ip As String = txtIP.Text
			 Dim port As UInteger = UInteger.Parse(txtPort.Text)
			 btnConn.Enabled = False
			 Dim f As New frmWaitingBox(Sub(obj, args)
				 Dim result As Boolean = uhf.TcpConnect(ip, port)
				 If Not result Then
					 frmWaitingBox.message_Conflict = "fail"
					 Thread.Sleep(1000)
					 Me.Invoke(New EventHandler(Sub()
						 btnConn.Enabled = True
					 End Sub))
				 Else
					 Me.Invoke(New EventHandler(Sub()
						 btnStart1.Enabled = True
						 btnDisConn.Enabled = True
					 End Sub))

				 End If

			 End Sub, "connecting...")
			 f.ShowDialog(Me)

		End Sub
		''' <summary>
		''' IP1断开
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnDisConn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDisConn.Click
			If FlagInventory1 Then
				btnStart1.Text = "Start"
				Dim id As Integer = getId(txtIP.Text)
				If id >= 0 Then
					Dim result As Boolean = uhf.StopById(id)
				End If
				FlagInventory1 = False
				StopThread()
			End If

			If SelectDevice(txtIP.Text) Then
				uhf.TcpDisconnect2()
			End If
			btnDisConn.Enabled = False
			btnStart1.Enabled = False
			btnConn.Enabled = True

		End Sub
		''' <summary>
		''' IP2连接
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnConn2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConn2.Click
			Dim ip As String = txtIP2.Text
			Dim port As UInteger = UInteger.Parse(txtPort2.Text)
			btnConn2.Enabled = False

			Dim f As New frmWaitingBox(Sub(obj, args)
				Dim result As Boolean = uhf.TcpConnect(ip, port)
				If Not result Then
					frmWaitingBox.message_Conflict = "fail"
					Thread.Sleep(1000)
					Me.Invoke(New EventHandler(Sub()
						btnConn2.Enabled = True
					End Sub))
				Else
					Me.Invoke(New EventHandler(Sub()
						btnDisConn2.Enabled = True
						btnStart2.Enabled = True
					End Sub))

				End If

			End Sub, "connecting...")
			f.ShowDialog(Me)
		End Sub
		''' <summary>
		''' IP2断开
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnDisConn2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDisConn2.Click
			If FlagInventory2 Then
				btnStart2.Text = "Start"
				Dim id As Integer = getId(txtIP2.Text)
				If id >= 0 Then
					Dim result As Boolean = uhf.StopById(id)
				End If
				FlagInventory2 = False
				StopThread()
			End If

			If SelectDevice(txtIP2.Text) Then
				uhf.TcpDisconnect2()
			End If
			btnConn2.Enabled = True
			btnDisConn2.Enabled = False
			btnStart2.Enabled = False
		End Sub



		''' <summary>
		''' IP1开始盘点、停止盘点
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnStart1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStart1.Click
			If btnStart1.Text = "Start" Then
				Dim id As Integer = getId(txtIP.Text)
				If id >= 0 Then
					If uhf.InventoryById(id) Then
						btnStart1.Text = "Stop"
						FlagInventory1 = True
						StartThread()
						Return
					End If
				End If
				MessageBox.Show("失败!")
			Else
				If FlagInventory1 Then
					btnStart1.Text = "Start"
					Dim id As Integer = getId(txtIP.Text)
					If id >= 0 Then
						Dim result As Boolean = uhf.StopById(id)
					End If
					FlagInventory1 = False
					StopThread()
				End If
			End If


		End Sub
		''' <summary>
		''' IP2开始盘点、停止盘点
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnStart2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStart2.Click
			If btnStart2.Text = "Start" Then

				Dim id As Integer = getId(txtIP2.Text)
				If id >= 0 Then
					Dim result As Boolean = uhf.InventoryById(id)
					If result Then
						btnStart2.Text = "Stop"
						FlagInventory2 = True
						StartThread()
						Return
					End If
				End If
				MessageBox.Show("失败!")
			Else
				If FlagInventory2 Then
					btnStart2.Text = "Start"
					Dim id As Integer = getId(txtIP2.Text)
					If id >= 0 Then
						Dim result As Boolean = uhf.StopById(id)
					End If
					FlagInventory2 = False
					StopThread()
				End If
			End If
		End Sub
		''' <summary>
		''' 开始盘点
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub StartThread()
			If Not isRuning Then
				isRuning = True
				Call (New Thread(New ThreadStart(Sub()
					ReadEPC()
				End Sub))).Start()
			End If
		End Sub
		''' <summary>
		''' 结束盘点
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub StopThread()
			If Not FlagInventory1 AndAlso Not FlagInventory2 Then
				isRuning = False
				Dim msg As String = If(Common.isEnglish, "wait...", "正在停止...")
				Dim f As New frmWaitingBox(Sub(obj, args)
					Thread.Sleep(1000)

				End Sub, msg)
				f.ShowDialog(Me)
			End If

		End Sub

		''' <summary>
		''' 获取功率
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnPowerGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPowerGet.Click
			If SelectDevice() Then
				Dim msg As String = "failure"
				Dim power As Byte = 0
				If uhf.GetPower(power) Then
					cmbPower.SelectedIndex = power - 1
					msg = "success"
				End If
				showMessage(msg)
			End If
		End Sub
		''' <summary>
		''' 设置功率
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnPowerSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPowerSet.Click
			If SelectDevice() Then
				Dim msg As String = "failure"
				If cmbPower.SelectedIndex >= 0 Then
					Dim power1 As Byte = CByte(cmbPower.SelectedIndex + 1)

					Dim save As Integer = If(cbPower.Checked, 1, 0)
					If uhf.SetPower(CByte(save), power1) Then
						msg = "success"
					End If

				End If
				showMessage(msg)
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
		Private Function SelectDevice(ByVal ip As String) As Boolean
			Dim list As List(Of DeviceInfo) = uhf.LinkGetDeviceInfo()
			If list IsNot Nothing Then
				For k As Integer = 0 To list.Count - 1
					If list(k).Ip = ip Then
						uhf.LinkSelectDevice(list(k).Id)
						Return True
					End If
				Next k

			End If
			Return False
		End Function

		Private Function SelectDevice() As Boolean
			Dim list As List(Of DeviceInfo) = uhf.LinkGetDeviceInfo()
			If list Is Nothing OrElse list.Count = 0 Then
				MessageBoxEx.Show(Me, "请先连接设备!")
				Return False
			End If
			Dim f As New DeviceListForm(list)
			f.ShowDialog()
			If Not SelectDevice(f.ip) Then
				MessageBoxEx.Show(Me, "failure!")
				Return False
			End If
			Return True
		End Function
		Public Function getId(ByVal ip As String) As Integer
			Dim deviceList As List(Of DeviceInfo) = uhf.LinkGetDeviceInfo()
			If deviceList IsNot Nothing AndAlso deviceList.Count > 0 Then
				For k As Integer = 0 To deviceList.Count - 1
					Dim deviceInfo As DeviceInfo = deviceList(k)
					If ip = deviceInfo.Ip Then
						Return deviceInfo.Id
					End If
				Next k
			End If
			Return -1
		End Function
		'获取epc
		Private Sub ReadEPC()

			Do
				Dim tagInfo As TagInfo = uhf.getTagData()
				If tagInfo IsNot Nothing AndAlso tagInfo.UhfTagInfo IsNot Nothing Then
					Dim ip As String = getIP(tagInfo.Id)
					Dim info As UHFTAGInfo = tagInfo.UhfTagInfo
					Dim data As String = info.Epc
					If info.Tid IsNot Nothing AndAlso info.Tid.Length > 0 Then
						data = "EPC:" & data
						data = data & vbCrLf & "TID:" & info.Tid
					End If
					If info.User IsNot Nothing AndAlso info.User.Length > 0 Then
						data = data & vbCrLf & "USER:" & info.User
					End If
					If Me.IsHandleCreated Then
						Me.BeginInvoke(setTextCallback2, New Object() {data, info.Rssi, "1", info.Ant, ip})
					End If
				Else
					If isRuning Then
						Thread.Sleep(5)
					Else
						Exit Do
					End If
				End If
			Loop

		End Sub

		Private Function getIP(ByVal id As Integer) As String
			Dim deviceList As List(Of DeviceInfo) = uhf.LinkGetDeviceInfo()
			Dim k As Integer = 0
			Do While deviceList IsNot Nothing AndAlso k < deviceList.Count
				If deviceList(k).Id = id Then
					Return deviceList(k).Ip
				End If
				k += 1
			Loop
			Return ""
		End Function

		Private Sub UpdataEPC(ByVal epc As String, ByVal rssi As String, ByVal count As String, ByVal ant As String, ByVal ip As String)
			Dim exist(0) As Boolean
			Dim index As Integer = CheckUtils.getInsertIndex(epcList, epc,Nothing, exist)
			If exist(0) Then
				lvEPC.Items(index).SubItems("RSSI").Text = rssi
				lvEPC.Items(index).SubItems("COUNT").Text = (Integer.Parse(lvEPC.Items(index).SubItems("COUNT").Text) + Integer.Parse(count)).ToString()
				lvEPC.Items(index).SubItems("ANT").Text = ant
				lvEPC.Items(index).SubItems("IP").Text = ip
			Else
				Dim lv As New ListViewItem()
				lv.Text = (index + 1).ToString()
				Dim itemEPC As New ListViewItem.ListViewSubItem()
				itemEPC.Name = "EPC"
				itemEPC.Text = epc
				lv.SubItems.Add(itemEPC)

				Dim itemRssi As New ListViewItem.ListViewSubItem()
				itemRssi.Name = "RSSI"
				itemRssi.Text = rssi
				lv.SubItems.Add(itemRssi)

				Dim itemCount As New ListViewItem.ListViewSubItem()
				itemCount.Name = "COUNT"
				itemCount.Text = count
				lv.SubItems.Add(itemCount)

				Dim itemAnt As New ListViewItem.ListViewSubItem()
				itemAnt.Name = "ANT"
				itemAnt.Text = ant
				lv.SubItems.Add(itemAnt)

				Dim itemIP As New ListViewItem.ListViewSubItem()
				itemIP.Name = "IP"
				itemIP.Text = ip
				lv.SubItems.Add(itemIP)


				lvEPC.Items.Insert(index, lv) ' Add(lv);
				epcList.Insert(index, New EpcInfo(epc, Integer.Parse(count), DataConvert.HexStringToByteArray(epc), Nothing))
			End If
			lblCount.Text = (Integer.Parse(lblCount.Text) + 1).ToString()

		End Sub

		Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
			isRuning = False
			uhf.LinkDisConnectAllDevice()
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			epcList.Clear()
			lvEPC.Items.Clear()
			lblCount.Text = "0"
		End Sub


	End Class
End Namespace
