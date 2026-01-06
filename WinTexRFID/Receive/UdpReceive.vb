Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports UHFAPP.interfaces
Imports System.Net.Sockets
Imports System.Net
Imports System.Windows.Forms
Imports System.Threading

Namespace UHFAPP.Receive
	Friend Class UdpReceive
		Implements IAutoReceive

		Public Delegate Sub ReceiveData(ByVal data() As Byte)
		Public ReceiveDataDelegate As ReceiveData = Nothing

		Private remote As IPEndPoint = Nothing
		Private ReceiveUdpClient As UdpClient = Nothing
		Private isOpen As Boolean = False
		Private MyIPAddress As String = ""
		Private PortName As Integer = 0

		Private threadReceiveOriginalData As Thread=Nothing

		Public Sub SetIP(ByVal MyIPAddress As String, ByVal PortName As Integer)
			Me.MyIPAddress = MyIPAddress
			Me.PortName = PortName
		End Sub

		Public Function Connect() As Boolean Implements IAutoReceive.Connect
			If MyIPAddress = "" OrElse PortName = 0 Then
				Return False
			End If

			Try
				Dim local As New IPEndPoint(IPAddress.Parse(MyIPAddress), PortName)
				ReceiveUdpClient = New UdpClient(local)
				remote = New IPEndPoint(IPAddress.Any, 0)
				isOpen = True
				If threadReceiveOriginalData Is Nothing Then
					threadReceiveOriginalData = New Thread(New ThreadStart(AddressOf ReceiveOriginalData))
					threadReceiveOriginalData.IsBackground = True
					threadReceiveOriginalData.Start()
				End If
				Return True
			Catch ex As Exception
				MessageBox.Show(ex.Message)
			End Try
			Return False
		End Function

		Public Sub DisConnect() Implements IAutoReceive.DisConnect
			If ReceiveUdpClient IsNot Nothing Then
				Try
					isOpen = False
					threadReceiveOriginalData.Interrupt()
					threadReceiveOriginalData = Nothing
					ReceiveUdpClient.Close()
				Catch ex As Exception

				End Try
			End If
		End Sub



		'获取epc
		Private Sub ReceiveOriginalData()
			Try
				Do While isOpen
					If isOpen Then
						Try
							' 接收
							Dim receiveBytes() As Byte = ReceiveUdpClient.Receive(remote) 'Receive the original
							If receiveBytes IsNot Nothing Then
								If ReceiveDataDelegate IsNot Nothing Then
									ReceiveDataDelegate(receiveBytes)
								End If
							End If
						Catch ex As Exception

						End Try
					End If
				Loop
			Catch ex As Exception

			End Try

		End Sub

		Public Function GetRemoteIP() As IPEndPoint
			Return remote
		End Function

	End Class
End Namespace
