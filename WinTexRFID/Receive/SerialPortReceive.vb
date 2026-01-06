Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports UHFAPP.interfaces
Imports System.IO.Ports

Namespace UHFAPP.Receive
	Friend Class SerialPortReceive
		Implements IAutoReceive

		Public Delegate Sub ReceiveData(ByVal data() As Byte)
		Public ReceiveDataDelegate As ReceiveData = Nothing

		Private serialPort As SerialPort = Nothing
		Public Sub New()
			Me.New("COM1", 115200)

		End Sub
		Public Sub New(ByVal PortName As String)
			Me.New(PortName, 115200)

		End Sub
		Public Sub New(ByVal PortName As String, ByVal BaudRate As Integer)
			If serialPort Is Nothing Then
				serialPort = New SerialPort(PortName, BaudRate)
				AddHandler serialPort.DataReceived, AddressOf SerialDataReceivedEvent
			End If
		End Sub

		Public Sub SetPortName(ByVal PortName As String)
			If serialPort IsNot Nothing Then
				serialPort.PortName = PortName
			End If
		End Sub
		Public Sub SetBaudRate(ByVal BaudRate As Integer)
			If serialPort IsNot Nothing Then
				serialPort.BaudRate = BaudRate
			End If
		End Sub

		Public Sub SetPortNameAndBaudRate(ByVal PortName As String, ByVal BaudRate As Integer)
			If serialPort IsNot Nothing Then
				serialPort.PortName = PortName
				serialPort.BaudRate = BaudRate
			End If
		End Sub
		Public Function Connect() As Boolean Implements IAutoReceive.Connect
			Try
				If serialPort IsNot Nothing AndAlso Not serialPort.IsOpen Then
					serialPort.Open()
					Return True
				End If
			Catch ex As Exception

			End Try
			Return False
		End Function

		Public Sub DisConnect() Implements IAutoReceive.DisConnect
			If serialPort IsNot Nothing Then
				serialPort.Close()
			End If
		End Sub

		Public Sub SerialDataReceivedEvent(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
			Dim serialPort As SerialPort=TryCast(sender, SerialPort)
			If serialPort IsNot Nothing Then
			   Dim len As Integer= serialPort.BytesToRead
			   If len > 0 Then
				   Dim data(len - 1) As Byte
				   len = serialPort.Read(data, 0, len)
				   If len > 0 Then
					   If ReceiveDataDelegate IsNot Nothing Then
						   If len >= data.Length Then
							   ReceiveDataDelegate(data)
						   Else
							   Dim temp(len - 1) As Byte
							   Array.Copy(data, 0, temp,0,len)
							   ReceiveDataDelegate(temp)
						   End If

					   End If
				   End If
			   End If
			End If
		End Sub


	End Class
End Namespace
