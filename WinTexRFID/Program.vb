Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Threading

Namespace UHFAPP
	Friend Module Program
		''' <summary>
		''' 应用程序的主入口点。
		''' </summary>
		<STAThread>
		Sub Main()
			Try
			  ' Test();
				Application.EnableVisualStyles()
				Application.SetCompatibleTextRenderingDefault(False)
				'Application.Run(New MainForm())

				init()

				Dim ofrmRFIDRead As New frmRFIDRead
				ofrmRFIDRead.init()

			Catch ex As Exception
				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub init()

			Dim cServer As String = ""
			Dim cDatabase As String = ""
			Dim cUsername As String = ""
			Dim cPassword As String = ""

			Dim cPath As String = System.Reflection.Assembly.GetExecutingAssembly().Location
			Dim aLines() As String
			Dim pos As Integer = 0
			Dim resstr As String = ""
			Dim lstr As String = ""
			Dim rstr As String = ""

			Try

				cPath = Strings.Replace(cPath, "UHFAPP.exe", "config.wtx")
				aLines = IO.File.ReadAllLines(cPath)

				For Each cLine In aLines
					lstr = ""
					rstr = ""
					resstr = cLine
					If resstr.Trim <> "" Then
						resstr = resstr.Trim
						pos = InStr(resstr, "=")
						If pos > 0 Then
							' EXTRACT left and right
							lstr = LCase$(Trim$(Left(resstr, pos - 1)))
							rstr = Trim$(Right(resstr, Len(resstr) - pos))
							Select Case lstr
								Case "server"
									If cServer = "" Then
										cServer = rstr.Trim
									End If
								Case "database"
									If cDatabase = "" Then
										cDatabase = rstr.Trim
									End If
								Case "user"
									If cUsername = "" Then
										cUsername = rstr.Trim
									End If
								Case "password"
									If cPassword = "" Then
										cPassword = rstr.Trim
									End If
							End Select
							If cServer.Trim <> "" And cDatabase.Trim <> "" And cUsername.Trim <> "" And cPassword.Trim <> "" Then
								Exit For
							End If
						End If
					End If
				Next

				oConnection.cServer = cServer
				oConnection.cDatabase = cDatabase
				oConnection.cUser = cUsername
				oConnection.cPassword = cPassword

			Catch ex As Exception
				ErrDisp("Maint : " + ex.Message + " " + cServer.Trim + " " + cDatabase.Trim + " " + cUsername.Trim + " " + cPassword.Trim,,,, ex)
			End Try
		End Sub

		Public Sub Test()
			Dim len As Integer
			Dim iRes As Integer
			Dim sendMsg(1023) As Byte
			Dim recvMsg((1024 * 8) - 1) As Byte


			Dim callback As Program.SDK_UHF.OnTagReceivedCallDelegate = New SDK_UHF.OnTagReceivedCallDelegate(AddressOf OnTagReceivedCall)
			SDK_UHF.ZHX_OCX_Init(callback)

			iRes = SDK_UHF.ZHX_OCX_GetConnect(recvMsg)
			Console.WriteLine("recv message:" & recvMsg.ToString)

			'open
			sendMsg = System.Text.ASCIIEncoding.ASCII.GetBytes("{""tranTypeId"":""SDVRFIDCO001""}""")
			iRes = SDK_UHF.ZHX_OCX_ExeMessage(sendMsg, recvMsg)
			Console.WriteLine("open: recvMsg:" & System.Text.ASCIIEncoding.ASCII.GetString(recvMsg, 0, 100))

			'get version
			sendMsg = System.Text.ASCIIEncoding.ASCII.GetBytes("{""tranTypeId"":""SDVRFIDCO003""}""")
			iRes = SDK_UHF.ZHX_OCX_ExeMessage(sendMsg, recvMsg)
			Console.WriteLine("get: version:" & System.Text.ASCIIEncoding.ASCII.GetString(recvMsg, 0, 100))

			'inventory
			sendMsg = System.Text.ASCIIEncoding.ASCII.GetBytes("{""tranTypeId"":""SDVRFIDCO014""}""")
			iRes = SDK_UHF.ZHX_OCX_ExeMessage(sendMsg, recvMsg)
			Console.WriteLine("inventory:" & System.Text.ASCIIEncoding.ASCII.GetString(recvMsg, 0, 100))

			Dim start As Integer = Environment.TickCount
			Do
				Thread.Sleep(50)
			Loop While Environment.TickCount - start < 5000


			sendMsg = System.Text.ASCIIEncoding.ASCII.GetBytes("{""tranTypeId"":""SDVRFIDCO015""}""")
			iRes = SDK_UHF.ZHX_OCX_ExeMessage(sendMsg, recvMsg)
			Console.WriteLine("stop inventory:" & System.Text.ASCIIEncoding.ASCII.GetString(recvMsg, 0, 100))
			SDK_UHF.ZHX_OCX_Free()

		End Sub
		Public Sub OnTagReceivedCall(ByVal epc() As Byte)
			Console.WriteLine("epc:回调数据")
		End Sub

		Public Class SDK_UHF

		   <DllImport("UHFAPI_back.dll", CallingConvention := CallingConvention.Cdecl)>
		   Public Shared Function CryptoTransmit(ByVal pin() As Byte, ByVal inLen As Integer, ByVal pout() As Byte, ByVal outLen() As Integer, ByVal wait_recv_ms As Integer) As Integer
		   End Function

			<DllImport("UHFAPI_back.dll", CallingConvention := CallingConvention.Cdecl)>
			Public Shared Sub ZHX_OCX_Init(ByVal [call] As OnTagReceivedCallDelegate)
			End Sub

			<DllImport("UHFAPI_back.dll", CallingConvention := CallingConvention.Cdecl)>
			Public Shared Sub ZHX_OCX_Free()
			End Sub

			<DllImport("UHFAPI_back.dll", CallingConvention := CallingConvention.Cdecl)>
			Public Shared Function ZHX_OCX_GetConnect(ByVal msgOut() As Byte) As Integer
			End Function

			<DllImport("UHFAPI_back.dll", CallingConvention := CallingConvention.Cdecl)>
			Public Shared Function ZHX_OCX_ExeMessage(ByVal msgIn() As Byte, ByVal msgOut() As Byte) As Integer
			End Function

			Public Delegate Sub OnTagReceivedCallDelegate(ByVal epc() As Byte)



		End Class
	End Module
End Namespace
