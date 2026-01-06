Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Threading
Imports System.IO

Namespace UHFAPP
	Partial Public Class UHFUpgradeForm
		Inherits BaseForm

		Private path As String = ""
		Public Sub New(ByVal isEnglish As Boolean)
			InitializeComponent()
			If isEnglish Then
				label1.Text = "path:"
				btnPath.Text = "Select file"
				btnStart.Text = "Upgrade"
			Else
				label1.Text = "路径:"
				btnPath.Text = "选择文件"
				btnStart.Text = "升级"
			End If

		End Sub

		Private Sub btnPath_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPath.Click
			Dim openDlg As New OpenFileDialog()
			openDlg.Filter = "bin|*.bin"
			If openDlg.ShowDialog() = DialogResult.OK Then
				' 显示文件路径名
				txtPath.Text = openDlg.FileName
			End If
		End Sub

		Private Sub btnStart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStart.Click
			path = txtPath.Text
			If path Is Nothing OrElse path.Length = 0 Then
				MessageBox.Show("fail")
				Return
			End If


			btnPath.Enabled = False
			btnStart.Enabled = False
			Me.ControlBox = False
			Call (New Thread(New ThreadStart(AddressOf startUpdate))).Start()
		End Sub
		Private isR2000 As Boolean = True
		Private Sub startUpdate()
			setMsg("Updating......", True)

			Dim stream As FileStream = Nothing
			Dim binary As BinaryReader = Nothing
			Try
				Dim type As Byte = 0
				Cursor.Current = Cursors.WaitCursor
				setPprogress(0)
				stream = New FileStream(path, FileMode.Open)
				binary = New BinaryReader(stream)

				Dim uFileSize As Long = stream.Length
				Dim packageCount As Integer = CInt(uFileSize \ 64) + (If(uFileSize Mod 64 > 0, 1, 0))

				Dim strversion As String = ""

				Me.Invoke(New EventHandler(Sub()
					If rbUHFModule.Checked Then
						strversion = "uhf version:" & uhf.GetSoftwareVersion()
						isR2000 = True
						type = 1
					Else
						isR2000 = False
						type = 0
						strversion = "uhf version:" & uhf.GetSTM32Version()
					End If
				End Sub))

				Me.Invoke(New EventHandler(Sub()
					label2.Text = strversion
				End Sub))


				If Not uhf.jump2Boot(type) Then
					setMsg("uhfJump2Boot fail", True)
					'return;
				End If
				Thread.Sleep(2000)



				If MainForm.MODE = 1 Then
					If Not isR2000 Then
						setMsg("断开连接", True)
						uhf.TcpDisconnect2()
						Thread.Sleep(1000)
						setMsg("开始连接", True)
						Dim result As Boolean = uhf.TcpConnect(MainForm.ip, MainForm.portData)
						If Not result Then
							setMsg("TcpConnect fail", True)
							Return
						End If
						setMsg("连接成功!", True)
						Thread.Sleep(1000)
					End If
				ElseIf MainForm.MODE = 2 Then
					Thread.Sleep(2000)
					uhf.CloseUsb()
					Thread.Sleep(1000)
					uhf.OpenUsb()
				End If

				If Not uhf.startUpd() Then
					setMsg("uhfStartUpdate fail", True)
					Return
				End If
				Thread.Sleep(2000)
				For k As Integer = 0 To packageCount - 1
					Try

						Dim data() As Byte = binary.ReadBytes(64)
						setMsg("uhfUpdating  packageCount=" & packageCount & "       " & k, False)

						If uhf.updating(data, data.Length) Then

							Dim r As Double = Math.Round(CDbl(k + 1) / CDbl(packageCount), 2) * 100
							setPprogress(CInt(Math.Truncate(r)))
						Else

							setMsg("uhfUpdating 失败 ,package=" & k, True)
							uhf.stopUpdate()
							Return
						End If
						Thread.Sleep(5)
					Catch e As Exception
						setMsg("ex=" & e.Message, True)
					End Try

				Next k
				setPprogress(100)

			Catch ex As Exception
				setMsg("ex=" & ex.Message, True)
			Finally
				Try
					If uhf.stopUpdate() Then
						setMsg("升级完成!", True)
					Else
						setMsg("升级失败!", True)
					End If
					Thread.Sleep(2000)

					btnPath.Invoke(New EventHandler(Sub()
						btnPath.Enabled = True
						btnStart.Enabled = True
						ControlBox = True
					End Sub))

					getVersion()
					If binary IsNot Nothing Then
						binary.Close()
					End If
					If stream IsNot Nothing Then
						stream.Close()
					End If
					Cursor.Current = Cursors.Default
				Catch e As Exception
					setMsg("222 ex=" & e.Message, True)
				End Try

			End Try



		End Sub

		Private Sub setPprogress(ByVal progress As Integer)
			progressBar1.Invoke(New EventHandler(Sub()
				progressBar1.Value = progress
			End Sub))
		End Sub


		Private Sub setMsg(ByVal msg As String, ByVal isAppend As Boolean)
			textBox1.Invoke(New EventHandler(Sub()
				If isAppend Then
					If textBox1.Text.Length > 2000 Then
						textBox1.Text = msg
					Else
						textBox1.AppendText(vbCrLf)
						textBox1.AppendText(msg)

					End If
				Else
					textBox1.Text = msg
				End If
			End Sub))
		End Sub

		Private Sub getVersion()

			label2.Invoke(New EventHandler(Sub()
				If isR2000 Then
					MessageBox.Show("uhf version:" & uhf.GetSoftwareVersion())
					label2.Text = "uhf version:" & uhf.GetSoftwareVersion()
				Else
					If MainForm.MODE = 1 Then
						If Not isR2000 Then
							setMsg("断开连接", True)
														   uhf.TcpDisconnect2()
														   Thread.Sleep(1000)
							setMsg("开始连接", True)
							Dim result As Boolean = uhf.TcpConnect(MainForm.ip, MainForm.portData)
							If Not result Then
								setMsg("TcpConnect fail", True)
								Return
							End If
						End If
					End If

					MessageBox.Show("uhf version:" & uhf.GetSTM32Version())
					label2.Text = "uhf version:" & uhf.GetSTM32Version()
				End If
			End Sub))

		End Sub

		Private Sub MainForm_eventOpen(ByVal open As Boolean)

		End Sub

		Private Sub UHFUpgradeForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

		End Sub


	End Class
End Namespace
