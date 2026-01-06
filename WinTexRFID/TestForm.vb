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

Namespace UHFAPP
	Partial Public Class TestForm
		Inherits BaseForm

		Private Shared ReadOnly locker1 As New Object()

		Private epcList As New Hashtable()
		' 将text更新的界面控件的委托类型
		Private Delegate Sub SetTextCallback(ByVal epc As String, ByVal tid As String, ByVal rssi As String, ByVal count As String, ByVal ant As String)
		Private setTextCallback2 As SetTextCallback

		Private Delegate Sub UpdateCallback(ByVal epc As String, ByVal op As Integer)
		Private updateCallback2 As UpdateCallback

		Private mainform As MainForm
		Private strStart As String = "Start"
		Private strStart2 As String = "Start"
		Private strStop As String = "Stop"
		Private strStop2 As String = "Stop"
		Private isRuning As Boolean = False
		Private isComplete As Boolean = True

		Private Shared path As String = Environment.CurrentDirectory & "\data.txt"


	   Private arrayEPC() As String=Nothing 'epc
	   Private arrayName() As String=Nothing '食品名称
	   Private arrayType() As String = Nothing '食品类型
	   Private arrayTime() As Long = Nothing '最后一次显示的时间
	   Private alreadyEpc As New List(Of String)() '已经读到过的标签
	   Private xjEpc2 As New List(Of String)() '已经下架的标签
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
				If btnScanEPC.Text = strStop Then
					StopEPC()
				End If
			End If
		End Sub
		Public Sub New(ByVal isOpen As Boolean, ByVal mainform As MainForm)
			InitializeComponent()
			If isOpen Then
				panel1.Enabled = True
			Else
				panel1.Enabled = False
			End If
			Me.mainform = mainform
		End Sub
		Public Sub getData()
			Try
				Dim data As String = FileManage.ReadFile(path)
				If data = "" Then
					Return
				End If

				Dim arrData() As String = data.Split(ControlChars.Lf)
				Dim len As Integer = arrData.Length
				 arrayEPC = New String(len - 1){} 'epc
				 arrayName = New String(len - 1){} '食品名称
				 arrayType = New String(len - 1){} '食品类型
				 arrayTime = New Long(len - 1){} '最后一次显示的时间

				For k As Integer = 0 To len - 1
					Dim temp() As String = arrData(k).ToString().Replace(vbCr, "").Replace(" ", "").Split("="c)
					If temp(0) <> "" Then
						arrayEPC(k) = temp(0)
						arrayName(k) = temp(1)
					End If
				Next k
			Catch ex As Exception

			End Try
		End Sub
		Private Sub Test_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			getData()
			'arrayEPC[0] = "000000000958";
			'arrayEPC[1] = "000000000992";
			'arrayEPC[2] = "000000000959";
			'arrayEPC[3] = "000000000956";
			'arrayEPC[4] = "000000000955";
			'arrayEPC[5] = "000000000954";
			'arrayEPC[6] = "000000000953";
			'arrayEPC[7] = "000000000921";
			'arrayEPC[8] = "000000000922";
			'arrayEPC[9] = "000000000914";
			'arrayEPC[10] = "000000000947";
			'arrayEPC[11] = "000000000946";
			'arrayEPC[12] = "000000000919";
			'arrayEPC[13] = "000000000945";
			'arrayEPC[14] = "000000000942";
			'arrayEPC[15] = "000000000929";
			'arrayEPC[16] = "000000000913";     
			'arrayEPC[17] = "000000000927";
			'arrayEPC[18] = "000000000926";
			'arrayEPC[19] = "000000000923";
			'arrayEPC[20] = "000000000912";
			'arrayEPC[21] = "000000000920";
			'arrayEPC[22] = "000000000930";
			'arrayName[0] = "芝麻味";
			'arrayName[1] = "芝麻味";
			'arrayName[2] = "巧克力味";
			'arrayName[3] = "巧克力味";
			'arrayName[4] = "巧克力味";
			'arrayName[5] = "巧克力味";
			'arrayName[6] = "芝麻味";
			'arrayName[7] = "芝麻味";
			'arrayName[8] = "巧克力味";
			'arrayName[9] = "蜜桃汁";
			'arrayName[10] = "蜜桃汁";
			'arrayName[11] = "蜜桃汁";
			'arrayName[12] = "青柠汁";
			'arrayName[13] = "青柠汁";
			'arrayName[14] = "蜜桃汁";
			'arrayName[15] = "青柠汁";
			'arrayName[16] = "青柠汁";
			'arrayEPC[17] = "纯净水";
			'arrayEPC[18] = "纯净水";
			'arrayEPC[19] = "纯净水";
			'arrayEPC[20] = "纯净水";
			'arrayEPC[21] = "纯净水";
			'arrayEPC[22] = "纯净水";

			'arrayName[0] = "饼干";
			'arrayName[1] = "饼干";
			'arrayName[2] = "饼干";
			'arrayName[3] = "饼干";
			'arrayName[4] = "饼干";
			'arrayName[5] = "饼干";
			'arrayName[6] = "饼干";
			'arrayName[7] = "饼干";
			'arrayName[8] = "饼干";
			'arrayName[9] = "屈臣氏苏打水";
			'arrayName[10] = "屈臣氏苏打水";
			'arrayName[11] = "屈臣氏苏打水";
			'arrayName[12] = "屈臣氏苏打水";
			'arrayName[13] = "屈臣氏苏打水";
			'arrayName[14] = "屈臣氏苏打水";
			'arrayName[15] = "屈臣氏苏打水";
			'arrayName[16] = "屈臣氏苏打水";
			'arrayName[17] = "景田矿泉水";
			'arrayName[18] = "景田矿泉水";
			'arrayName[19] = "景田矿泉水";
			'arrayName[20] = "景田矿泉水";
			'arrayName[21] = "景田矿泉水";
			'arrayName[22] = "景田矿泉水";

			setTextCallback2 = New SetTextCallback(AddressOf UpdataEPC)
			updateCallback2 = New UpdateCallback(AddressOf UpdateListView)
			AddHandler MainForm.eventOpen, AddressOf MainForm_eventOpen
		End Sub

		Private Sub btnScanEPC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnScanEPC.Click
			If btnScanEPC.Text = strStop OrElse btnScanEPC.Text = strStop2 Then
				StopEPC()
			Else
				If Not isRuning AndAlso isComplete Then
					mainform.disableControls()
					isRuning = True
					isComplete = False

					If UHFAPI.getInstance().Inventory() Then
						button1.Enabled = False
						StartEPC()
					Else
						MessageBoxEx.Show(Me, "Inventory failure!")
						isRuning = False
						isComplete = True
						mainform.enableControls()
						button1.Enabled = True
					End If
				End If
			End If
		End Sub


		'开始读取epc
		Private Sub StartEPC()
			btnScanEPC.Text = strStop2
			Dim epcT As New Thread(New ThreadStart(AddressOf ReadEPC))
			epcT.IsBackground = True
			epcT.Start()

			Dim cheakT As New Thread(New ThreadStart(AddressOf CheakEPC))
			cheakT.IsBackground = True
			cheakT.Start()

		End Sub
		'停止读取epc
		Private Sub StopEPC()
			button1.Enabled = True
			btnScanEPC.Text = strStart2
			isRuning = False
			Thread.Sleep(100)
			UHFAPI.getInstance().StopGet()
			mainform.enableControls()
		End Sub
		'获取epc
		Private Sub ReadEPC()
			Try
				Do While isRuning
					' k =  random.Next(1,300);
					Dim epc As String = ""
					Dim tid As String = ""
					Dim rssi As String = ""
					Dim ant As String = ""
					Dim user As String = ""
					Dim result As Boolean = UHFAPI.getInstance().uhfGetReceived(epc, tid, rssi, ant)
					If result Then
						Me.BeginInvoke(setTextCallback2, New Object() {epc, tid, rssi, "1", ant})
						Console.Out.Write("刷新ui的" & vbLf) '这行打印很重要，打印会加延时，界面刷新才正常
					End If
				Loop
			Catch ex As Exception

			End Try
			isComplete = True

		End Sub

		Private Sub CheakEPC()
			Try
			  '  int timeOut = 1000;
				Do While isRuning

					For k As Integer = 0 To arrayTime.Length - 1

						Dim time As Integer = Environment.TickCount
						If arrayTime(k) <> 0 AndAlso (time - arrayTime(k) > Common.time) Then
							Dim epc As String = arrayEPC(k)
							Me.BeginInvoke(updateCallback2, New Object() {epc, ADD})
						ElseIf arrayTime(k) <> 0 Then
							Dim epc As String = arrayEPC(k)
							Me.BeginInvoke(updateCallback2, New Object() {epc, DELETE})

						End If
					Next k
					Thread.Sleep(100)
				Loop
			Catch ex As Exception

			End Try
			isComplete = True

		End Sub


		Private Sub Test_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
			If btnScanEPC.Text = strStop OrElse btnScanEPC.Text = strStop2 Then
				StopEPC()
			End If
			RemoveHandler MainForm.eventOpen, AddressOf MainForm_eventOpen
		End Sub

		Private Sub label2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles label2.Click

		End Sub

		Private Sub panel1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles panel1.Paint

		End Sub
		Private ADD As Integer = 0
		Private DELETE As Integer = 1
		Private Sub UpdateListView(ByVal epc As String, ByVal op As Integer)
			Try
				If op = ADD Then

					For k As Integer = 0 To xjEpc2.Count - 1
						If xjEpc2(k) = epc Then
							'已经存在不需要重复增加
							Return
						End If
					Next k
					xjEpc2.Add(epc)
					SyncLock locker1

						Dim i As Integer = 0
						Do While i < lvEPC.Items.Count
							If Me.lvEPC.Items(i).SubItems(1).Text = epc Then
								lvEPC.Items.Remove(Me.lvEPC.Items(i))
								label3.Text = lvEPC.Items.Count.ToString()
								Exit Do
							End If
							i += 1
						Loop
					End SyncLock

					For i As Integer = 0 To listView1.Items.Count - 1
						If Me.listView1.Items(i).SubItems(1).Text = epc Then
							Return
						End If
					Next i

					Dim name As String = ""
					For k As Integer = 0 To arrayEPC.Length - 1
						If arrayEPC(k) = epc Then
							name = arrayName(k)
							Exit For
						End If
					Next k

					Dim lv As New ListViewItem()
					lv.Text = (listView1.Items.Count + 1).ToString()
					lv.SubItems.Add(epc)
					lv.SubItems.Add(name)
					listView1.Items.Add(lv)
					label4.Text = listView1.Items.Count.ToString()

				ElseIf op = DELETE Then

					Dim k As Integer = 0
					Do While k < xjEpc2.Count
						If xjEpc2(k) = epc Then
							Dim i As Integer = 0
							Do While i < listView1.Items.Count
								If Me.listView1.Items(i).SubItems(1).Text = epc Then
									listView1.Items.Remove(Me.listView1.Items(i))
									Exit Do
								End If
								i += 1
							Loop
							label4.Text = listView1.Items.Count.ToString()
							xjEpc2.RemoveAt(k)
							Return
						End If
						k += 1
					Loop

				End If
			Catch ex As Exception

			End Try

		End Sub

		Private Sub UpdataEPC(ByVal epc As String, ByVal tid As String, ByVal rssi As String, ByVal count As String, ByVal ant As String)
			Try
				Dim index As Integer = 0
				'--------------------------------------------------------
				Dim isFlag As Boolean = False
				For k As Integer = 0 To arrayEPC.Length - 1
					If arrayEPC(k) = epc Then
						index = k
						arrayTime(index) = Environment.TickCount
						isFlag = True
						Exit For
					End If
				Next k
				'不属于已知的epc 直接返回不显示
				If Not isFlag Then
					Return
				End If
				'--------------------------------------------------------
				Dim isFlag2 As Boolean = False
				For k As Integer = 0 To alreadyEpc.Count - 1
					If alreadyEpc(k) = epc Then
						isFlag2 = True
						Exit For
					End If
				Next k
				If Not isFlag2 Then
					alreadyEpc.Add(epc)
				End If
				'--------------------------------------------------------


				SyncLock locker1
					For i As Integer = 0 To lvEPC.Items.Count - 1
						If Me.lvEPC.Items(i).SubItems(1).Text = epc Then
							lvEPC.Items(i).SubItems(3).Text = (Integer.Parse(lvEPC.Items(i).SubItems(3).Text) + 1).ToString()
							Return
						End If
					Next i


					Dim lv As New ListViewItem()
					lv.Text = (lvEPC.Items.Count + 1).ToString()
					lv.SubItems.Add(epc)
					lv.SubItems.Add(arrayName(index))
					lv.SubItems.Add("1")
					lvEPC.Items.Add(lv)
					label3.Text = lvEPC.Items.Count.ToString()
				End SyncLock

			Catch ex As Exception

			End Try
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			SyncLock locker1
				lvEPC.Items.Clear()
				listView1.Items.Clear()
				xjEpc2.Clear()
				label3.Text = "0"
				label4.Text = "0"
				If arrayTime IsNot Nothing Then
					For k As Integer = 0 To arrayTime.Length - 1
						arrayTime(k) = 0
					Next k
				End If
			End SyncLock
		End Sub
	End Class
End Namespace
