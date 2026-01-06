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

Namespace UHFAPP
	Partial Public Class ReadEPCForm2
		Inherits BaseForm

		Private isPz As Boolean = False
		Private mainform As MainForm2
		Private strStart As String = "Start"
		Private strStart2 As String = "开始"
		Private strStop As String = "Stop"
		Private strStop2 As String = "停止"
		Private isRuning As Boolean = False
		Private isComplete As Boolean = True
		Private beginTime As Long = 0
		Private total As Integer = 0
		Private epcList As New List(Of EpcInfo)()
		' 将text更新的界面控件的委托类型
		Private Delegate Sub SetTextCallback(ByVal epc As String, ByVal tid As String, ByVal rssi As String, ByVal count As String, ByVal ant As String)
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
		Public Sub New(ByVal isOpen As Boolean, ByVal mainform As MainForm2)
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
				cbSave.Text = "Save"
				btnSet.Text = "Set"
				button2.Text = "reset"
				lto.Text = "Tag Count:"
				label7.Text = "Total:"
				label2.Text = "Time:"
				button1.Text = "Clear"
				label1.Text = "Length:"
				label8.Text = "Speed:"
				If btnScanEPC.Text = strStart2 Then
					btnScanEPC.Text = strStart
				ElseIf btnScanEPC.Text = strStop2 Then
					btnScanEPC.Text = strStop
				End If

				label30.Location = New Point(669, 33)
				label1.Location = New Point(785, 34)
			Else
				contextMenuStrip1.Items(0).Text = "复制标签"
				groupBox8.Text = "过滤"
				label29.Text = "数据:"
				label30.Text = "起始地址:"
				cbSave.Text = "保存"
				btnSet.Text = "设置"
				button2.Text = "重置"
				lto.Text = "标签数:"
				label7.Text = "次数:"
				label2.Text = "时间:"
				button1.Text = "清空"
				label1.Text = "长度:"
				label8.Text = "速率："
				If btnScanEPC.Text = strStart Then
					btnScanEPC.Text = strStart2
				ElseIf btnScanEPC.Text = strStop Then
					btnScanEPC.Text = strStop2
				End If
				label30.Location = New Point(640, 33)
				label1.Location = New Point(801, 33)
			End If
		End Sub


		Private Sub ScanEPCForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			AddHandler MainForm2.eventOpen, AddressOf MainForm_eventOpen
			setTextCallback2 = New SetTextCallback(AddressOf UpdataEPC)

			AddHandler MainForm2.eventSwitchUI, AddressOf MainForm_eventSwitchUI
			 MainForm_eventSwitchUI()

			  '还原之前读取的数据......................................

				  filerLen.Text = ReadEPCFormData.filter_len
				 txtData.Text = ReadEPCFormData.filter_Data
				 txtPtr.Text = ReadEPCFormData.filter_Ptr
				 cbSave.Checked =ReadEPCFormData.filter_save
				 lblTime.Text = ReadEPCFormData.Time
				 lblTotal.Text = ReadEPCFormData.Total
				 If ReadEPCFormData.epcList IsNot Nothing Then
					 epcList = ReadEPCFormData.epcList
				 End If
				 Select Case ReadEPCFormData.filter_bank
					 Case 1
						 rbEPC.Checked = True
					 Case 2
						 rbTID.Checked = True
					 Case 3
						 rbUser.Checked = True
				 End Select

				 lvEPC.Select()
				 For k As Integer = 0 To ReadEPCFormData.listviewdata.Count - 1
					 lvEPC.Items.Add(ReadEPCFormData.listviewdata(k))
					 If ReadEPCFormData.listviewdata(k).Text = ReadEPCFormData.selectedText Then
						 lvEPC.Items(k).Selected = True
					 End If
				 Next k
				 If Not isPz Then
					 button3.Visible = False
				 End If
				 'private void UpdataEPC(string epc, string tid, string rssi, string count,string ant)
				' UpdataEPC("112233","","","1","1");
				'sUpdataEPC("445566778899", "", "", "1", "1");
		End Sub

		Private Sub ScanEPCForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
			RemoveHandler MainForm2.eventOpen, AddressOf MainForm_eventOpen
			RemoveHandler MainForm2.eventSwitchUI, AddressOf MainForm_eventSwitchUI

			'记录之前的数据......................................
			Try
				ReadEPCFormData.filter_len = filerLen.Text
				ReadEPCFormData.filter_Data = txtData.Text
				ReadEPCFormData.filter_Ptr = txtPtr.Text
				ReadEPCFormData.filter_save = cbSave.Checked
				ReadEPCFormData.Time = lblTime.Text
				ReadEPCFormData.Total = lblTotal.Text
				ReadEPCFormData.epcList = epcList
				If rbEPC.Checked Then
					ReadEPCFormData.filter_bank = 1
				ElseIf rbTID.Checked Then
					ReadEPCFormData.filter_bank = 2
				ElseIf rbUser.Checked Then
					ReadEPCFormData.filter_bank = 3
				End If

				ReadEPCFormData.listviewdata.Clear()
				ReadEPCFormData.selectedText = ""
				If lvEPC IsNot Nothing AndAlso lvEPC.Items.Count > 0 Then
					For k As Integer = 0 To lvEPC.Items.Count - 1
						ReadEPCFormData.listviewdata.Add(lvEPC.Items(k))
					Next k
					If lvEPC.SelectedItems.Count > 0 Then
						ReadEPCFormData.selectedText = lvEPC.SelectedItems(0).Text
					End If
				End If
			Catch ex As Exception
				MessageBox.Show(ex.Message)
			End Try

			If btnScanEPC.Text = strStop OrElse btnScanEPC.Text = strStop2 Then
				StopEPC(True)
			End If
		End Sub
		#Region " 设置过滤"

		Private Sub btnSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSet.Click
			Dim ptr As Integer = Integer.Parse(txtPtr.Text)
			Dim leng As Integer = Integer.Parse(filerLen.Text)
			Dim save As Integer = If(cbSave.Checked, 1, 0)

			Dim txtPtr1 As String = txtPtr.Text
			Dim data As String = txtData.Text.Replace(" ","")
			If Not StringUtils.IsHexNumber(data) AndAlso leng>0 Then
				MessageBoxEx.Show(Me,If(Common.isEnglish, "Please input filter hexadecimal data!", "请输入过滤数据!"))
				Return
			End If
			If (leng \ 8 + (If(leng Mod 8 = 0, 0, 1))) * 2 > data.Length Then
				MessageBox.Show(If(Common.isEnglish, "filter data length error!", "过滤数据和长度不匹配!")) 'to do
				Return
			End If
			Dim bank As Byte=&H1
			If rbEPC.Checked Then
				 bank=&H1
			ElseIf rbTID.Checked Then
				 bank=&H2
			ElseIf rbUser.Checked Then
				 bank=&H3
			End If

			If leng = 0 Then
				data = "00"
			End If

			Dim buff() As Byte = DataConvert.HexStringToByteArray(data)
			If uhf.SetFilter(CByte(save), bank, ptr, leng, buff) Then
				MessageBoxEx.Show(Me,If(Common.isEnglish, "Success!", "设置过滤成功!"))
			Else
				MessageBoxEx.Show(Me,If(Common.isEnglish, "failure!", "设置过滤失败"))
			End If
		End Sub
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
			Else
				If Not isRuning AndAlso isComplete Then
					mainform.disableControls()
					isRuning = True
					isComplete = False
					If uhf.Inventory() Then
						label9.Text = ""
						StartEPC()
					Else
						MessageBoxEx.Show(Me,"Inventory failure!")
						isRuning = False
						isComplete = True
						mainform.enableControls()
					End If
				End If
			End If
		End Sub

		'Clear
		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
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

		End Sub
		'停止读取epc
		Private Sub StopEPC(ByVal isStop As Boolean)
			uhf.StopGet()
			isRuning = False
			groupBox8.Enabled = True
			btnScanEPC.Text = If(Common.isEnglish, strStart, strStart2)
			mainform.enableControls()
		End Sub

		'获取epc
		Private Sub ReadEPC()
			Try
				beginTime = Environment.TickCount
				Do While isRuning
					Dim epc As String =""
					Dim tid As String = ""
					Dim rssi As String = ""
					Dim ant As String = ""
					Dim user As String = ""
					Dim result As Boolean = uhf.uhfGetReceived(epc, tid, rssi, ant)
					If result Then
						Me.BeginInvoke(setTextCallback2, New Object() {epc, tid, rssi, "1", ant})
					End If
				Loop

				If isPz Then
					Dim result As Boolean = False
					Dim k As Integer = 0
					Do While (k < 2) OrElse result
						Thread.Sleep(1)
						' k =  random.Next(1,300);
						Dim epc As String = ""
						Dim tid As String = ""
						Dim rssi As String = ""
						Dim ant As String = ""
						Dim user As String = ""
						result = uhf.uhfGetReceived(epc, tid, rssi, ant)
						If result Then
							Me.BeginInvoke(setTextCallback2, New Object() {epc, tid, rssi, "1", ant})
							' Console.Out.Write("刷新ui的\n"); //这行打印很重要，打印会加延时，界面刷新才正常
						End If
						k += 1
					Loop
				End If


			Catch ex As Exception

			End Try
			isComplete = True

		End Sub

		Private tempCount As Integer = 0
		Private sb As New StringBuilder(100)
		Private Sub UpdataEPC(ByVal epc As String, ByVal tid As String, ByVal rssi As String, ByVal count As String, ByVal ant As String)
			Dim time As Long = Environment.TickCount - beginTime
			lblTime.Text = (time) & "ms"
 ' (System.Environment.TickCount - beginTime) + "ms";//((System.Environment.TickCount - beginTime) / 1000) + "(s)";// sb.ToString();//
			tempCount += Integer.Parse(count)
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: label6.Text = (tempCount += int.Parse(count)).ToString();
			label6.Text = tempCount.ToString()

			Dim exist(0) As Boolean
			Dim index As Integer = CheckUtils.getInsertIndex(epcList, epc, tid,exist)
			If exist(0) Then
				lvEPC.Items(index).SubItems(2).Text = tid
				lvEPC.Items(index).SubItems(3).Text = rssi
				lvEPC.Items(index).SubItems(4).Text = (Integer.Parse(lvEPC.Items(index).SubItems(4).Text) + Integer.Parse(count)).ToString()
				lvEPC.Items(index).SubItems(5).Text = ant
			Else
				total += 1
				Dim lv As New ListViewItem()
				lv.Text = (lvEPC.Items.Count+1).ToString()
				lv.SubItems.Add(epc)
				lv.SubItems.Add(tid)
				lv.SubItems.Add(rssi)
				lv.SubItems.Add(count)
				lv.SubItems.Add(ant)
				lvEPC.Items.Add(lv)
				epcList.Add(New EpcInfo(epc, Integer.Parse(count), DataConvert.HexStringToByteArray(epc), DataConvert.HexStringToByteArray(tid)))
				lblTotal.Text = epcList.Count & ""
			End If
			If time < 1000 Then
				label9.Text = (tempCount & "/s")
			Else
				label9.Text = (tempCount \ (time \ 1000)) & "/s"

			End If


		End Sub


		Private Sub lblTotal1_Click(ByVal sender As Object, ByVal e As EventArgs)

		End Sub

		Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
			Dim save As Integer = If(cbSave.Checked, 1, 0)
			If uhf.SetFilter(CByte(save), 1, 4, 0, New Byte() { 0 }) AndAlso uhf.SetFilter(CByte(save), 2, 4, 0, New Byte() { 0 }) AndAlso uhf.SetFilter(CByte(save), 3, 4, 0, New Byte() { 0 }) Then
				MessageBoxEx.Show(Me,If(Common.isEnglish, "Success!", "重置成功!"))
			Else
				MessageBoxEx.Show(Me,If(Common.isEnglish, "failure!", "重置失败!"))
			End If
		End Sub

		Private Sub radioButton1_Click(ByVal sender As Object, ByVal e As EventArgs)

		End Sub

		Private Sub lvEPC_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvEPC.DoubleClick
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


		Private Sub contextMenuStrip1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles contextMenuStrip1.Click
			If lvEPC.SelectedItems.Count <= 0 Then
				Return
			End If
			Dim str As String = lvEPC.SelectedItems(0).SubItems(1).Text
			Clipboard.SetDataObject(str)
		End Sub

		Private Sub textBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles filerLen.TextChanged
			Try
				Dim ptr As String = filerLen.Text
				If Not StringUtils.IsNumber(ptr) Then
					filerLen.Text = "0"
					Return
				End If
			Catch ex As Exception
				filerLen.Text = "0"
			End Try
		End Sub

		Private Sub rbTID_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbTID.Click
			txtPtr.Text = "0"
		End Sub

		Private Sub rbUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbUser.Click
			txtPtr.Text = "0"
		End Sub

		Private Sub rbEPC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbEPC.Click
			txtPtr.Text = "32"
		End Sub

		Private Sub lto_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lto.Click

		End Sub

		Private Sub label2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles label2.Click

		End Sub

		Private Sub lblTime_Click(ByVal sender As Object, ByVal e As EventArgs)

		End Sub
		Private isDelete As Boolean = False
		Private Sub ReadEPCForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
			If e.KeyCode = Keys.Back Then
				isDelete = True
			Else
				isDelete = False
			End If
		End Sub

		Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button3.Click
			If btnScanEPC.Text = strStop OrElse btnScanEPC.Text = strStop2 Then
				StopEPC(True)
			Else
				If Not isRuning AndAlso isComplete Then
					mainform.disableControls()
					isRuning = True
					isComplete = False
					beginTime = Environment.TickCount
					If uhf.Inventory() Then
						StartEPC()
						Call (New Thread(New ThreadStart(Sub()
							Thread.Sleep(2000)
							Me.Invoke(New EventHandler(Sub()
								StopEPC(True)

							End Sub))

						End Sub))).Start()
					Else
						MessageBoxEx.Show(Me, "Inventory failure!")
						isRuning = False
						isComplete = True
						mainform.enableControls()
					End If
				End If
			End If
		End Sub

		Private Sub timer1_Tick(ByVal sender As Object, ByVal e As EventArgs)
			lblTime.Text = (Environment.TickCount - beginTime) & "(ms)"
		End Sub

		'获取epc
		Private Sub ReadEPC2()
			Try
				Dim listEPC As New List(Of String)()
				Dim listCount As New List(Of Integer)()
				Dim listTID As New List(Of String)()
				Dim listRssi As New List(Of String)()
				Dim listAnt As New List(Of String)()

				Dim epcStart As Integer = Environment.TickCount
				Dim random As New Random()
				Dim bengin As Integer = Environment.TickCount
				Do While isRuning
					Dim epc As String = ""
					Dim tid As String = ""
					Dim rssi As String = ""
					Dim ant As String = ""
					Dim user As String = ""
					Dim result As Boolean = uhf.uhfGetReceived(epc, tid, rssi, ant)
					If result Then
						'                        
						'                        if (listEPC.Contains(epc))
						'                        {
						'                            int index = listEPC.IndexOf(epc);
						'                            listRssi[index] = rssi;
						'                            listAnt[index] = ant;
						'                            listCount[index] = listCount.Count + 1;
						'                        }
						'                        else {
						'                            listEPC.Add(epc);
						'                            listTID.Add(tid);
						'                            listRssi.Add(rssi);
						'                            listAnt.Add(ant);
						'                            listCount.Add(1);
						'                        }
						'                        
						Me.BeginInvoke(setTextCallback2, New Object() {epc, tid, rssi, "1", ant})
					End If
					If Environment.TickCount - bengin > -1 Then
						If listEPC.Count > 0 Then
							For k As Integer = 0 To listEPC.Count - 1
								' this.BeginInvoke(setTextCallback, new object[] { epc, tid, rssi, "1", ant });
								Me.BeginInvoke(setTextCallback2, New Object() {listEPC(k), listTID(k), listRssi(k), listCount(k) & "", listAnt(k)})
							Next k
							listEPC.Clear()
							listTID.Clear()
							listRssi.Clear()
							listAnt.Clear()
							listCount.Clear()

						End If
					End If
				Loop
				If listEPC.Count > 0 Then
					For k As Integer = 0 To listEPC.Count - 1
						Me.BeginInvoke(setTextCallback2, New Object() {listEPC(k), listTID(k), listRssi(k), listCount(k) & "", listAnt(k)})
					Next k
				End If


				If isPz Then
					Dim result As Boolean = False
					Dim k As Integer = 0
					Do While (k < 2) OrElse result
						Thread.Sleep(1)
						' k =  random.Next(1,300);
						Dim epc As String = ""
						Dim tid As String = ""
						Dim rssi As String = ""
						Dim ant As String = ""
						Dim user As String = ""
						result = uhf.uhfGetReceived(epc, tid, rssi, ant)
						If result Then
							Me.BeginInvoke(setTextCallback2, New Object() {epc, tid, rssi, "1", ant})
							' Console.Out.Write("刷新ui的\n"); //这行打印很重要，打印会加延时，界面刷新才正常
						End If
						k += 1
					Loop
				End If


			Catch ex As Exception

			End Try
			isComplete = True

		End Sub

	End Class
End Namespace
