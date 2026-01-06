Imports wclCommon
Imports wclCommunication
Imports wclSerial
Imports System.Windows.Forms
Imports System.Threading

Public Class Terazi

    Public cMessages As String = ""

    Private cResult As String = ""
    Private WithEvents wclSerialClient As wclSerialClient
    Private wclSerialMonitor As wclSerialMonitor
    Private cPort As String = "COM4"
    Private nBaud As UInteger = 9600

    Dim lConnected As Boolean = False
    Dim lConnectionFailed As Boolean = False
    Dim oConfig As wclSerialConfig
    Dim oTimes As wclSerialTimeouts
    Dim aPorts() As wclSerialDevice
    Dim nReadBufferSize As UInt32
    Dim nWriteBufferSize As UInt32

    Public Sub init(Optional cComPort As String = "COM4", Optional nBaudRate As Integer = 9600)

        Try
            cPort = cComPort.Trim
            nBaud = Convert.ToUInt32(nBaudRate)

            wclSerialMonitor = New wclSerialMonitor()
            wclSerialClient = New wclSerialClient()
            oConfig = New wclSerialConfig()

            wclSerialClient.DeviceName = cPort.Trim

        Catch ex As Exception
            ErrDisp("init  " + ex.Message, "Terazi",,, ex)
        End Try
    End Sub

    Public Sub EnumComPorts()

        Try
            Dim Res As Int32 = wclSerialMonitor.EnumDevices(aPorts)

            If Res <> wclErrors.WCL_E_SUCCESS Then
                AddMessage("Error enumerating COM ports: 0x" + Res.ToString("X8"))
            End If

        Catch ex As Exception
            ErrDisp("EnumComPorts  " + ex.Message, "Terazi",,, ex)
        End Try
    End Sub

    Public Function Connect() As Boolean

        Connect = False

        Try
            Dim Res As Int32 = wclSerialClient.Connect()

            If Res <> wclErrors.WCL_E_SUCCESS And Res <> 196608 Then
                AddMessage("Error: 0x" + Res.ToString("X8"))
            Else
                Do While True
                    Application.DoEvents()
                    If lConnected Or lConnectionFailed Then
                        Exit Do
                    End If
                Loop
                If lConnectionFailed Then
                    Connect = False
                Else
                    Connect = True
                End If
            End If

        Catch ex As Exception
            ErrDisp("Connect  " + ex.Message, "Terazi",,, ex)
        End Try

    End Function

    Public Function SetConfig() As Boolean

        SetConfig = False

        Try
            Dim Res As Int32 = 0

            oConfig.BaudRate = nBaud

            Res = wclSerialClient.SetConfig(oConfig)

            If Res <> wclErrors.WCL_E_SUCCESS Then
                AddMessage("Error: 0x" + Res.ToString("X8"))
            Else
                SetConfig = True
            End If

        Catch ex As Exception
            ErrDisp("SetConfig  " + ex.Message, "Terazi",,, ex)
        End Try
    End Function

    Public Sub Disconnect()
        On Error Resume Next
        wclSerialClient.Disconnect()
    End Sub

    Private Sub wclSerialClient_OnConnect(ByVal Sender As Object, ByVal [Error] As Integer) Handles wclSerialClient.OnConnect

        Try
            lConnected = False
            lConnectionFailed = False

            If [Error] = wclErrors.WCL_E_SUCCESS Then
                ReadConfiguration()
                ReadTimeouts()
                ReadBuffers()
                lConnected = True
            Else
                AddMessage("Connect error: 0x" + [Error].ToString("X8"))
                lConnectionFailed = True
            End If

        Catch ex As Exception
            ErrDisp("wclSerialClient_OnConnect  " + ex.Message, "Terazi",,, ex)
        End Try
    End Sub

    Private Function ReadConfiguration() As Boolean

        ReadConfiguration = True

        Try
            Dim Res As Int32 = wclSerialClient.GetConfig(oConfig)

            If Res <> wclErrors.WCL_E_SUCCESS Then
                AddMessage("Read configuration error: 0x" + Res.ToString("X8"))
                ReadConfiguration = False
            End If

        Catch ex As Exception
            ErrDisp("ReadConfiguration  " + ex.Message, "Terazi",,, ex)
        End Try
    End Function

    Private Function ReadTimeouts() As Boolean

        ReadTimeouts = True

        Try
            Dim Res As Int32 = wclSerialClient.GetTimeouts(oTimes)

            If Res <> wclErrors.WCL_E_SUCCESS Then
                AddMessage("Get timeouts error: 0x" + Res.ToString("X8"))
                ReadTimeouts = False
            End If

        Catch ex As Exception
            ErrDisp("ReadTimeouts  " + ex.Message, "Terazi",,, ex)
        End Try
    End Function

    Private Function ReadBuffers() As Boolean

        ReadBuffers = True

        Try
            Dim Res As Int32 = 0

            Res = wclSerialClient.GetReadBufferSize(nReadBufferSize)
            If Res <> wclErrors.WCL_E_SUCCESS Then
                AddMessage("Get read buffer size error: 0x" + Res.ToString("X8"))
                ReadBuffers = False
            End If

            Res = wclSerialClient.GetWriteBufferSize(nWriteBufferSize)
            If Res <> wclErrors.WCL_E_SUCCESS Then
                AddMessage("Get write buffer size error: 0x" + Res.ToString("X8"))
                ReadBuffers = False
            End If

        Catch ex As Exception
            ErrDisp("ReadBuffers  " + ex.Message, "Terazi",,, ex)
        End Try
    End Function

    Private Sub wclSerialClient_OnDisconnect(ByVal Sender As Object, ByVal Reason As Integer) Handles wclSerialClient.OnDisconnect
        lConnected = False
        AddMessage("Disconnected: 0x" + Reason.ToString("X8"))
    End Sub

    Private Sub wclSerialClient_OnError(ByVal Sender As Object, ByVal Errors As wclSerial.wclSerialError, ByVal States As wclSerial.wclSerialCommunicationState) Handles wclSerialClient.OnError
        Dim Str As String = ""
        If (Errors And wclSerialError.erBreak) <> 0 Then
            Str += "erBreak "
        End If
        If (Errors And wclSerialError.erFrame) <> 0 Then
            Str += "erFrame "
        End If
        If (Errors And wclSerialError.erOverrun) <> 0 Then
            Str += "erOverrun "
        End If
        If (Errors And wclSerialError.erRxOver) <> 0 Then
            Str += "erRxOver "
        End If
        If (Errors And wclSerialError.erRxParity) <> 0 Then
            Str += "erRxParity "
        End If
        AddMessage("Error: " + Str)

        Str = ""
        If (States And wclSerialCommunicationState.csCtsHold) <> 0 Then
            Str += "csCtsHold "
        End If
        If (States And wclSerialCommunicationState.csDsrHold) <> 0 Then
            Str += "csDsrHold "
        End If
        If (States And wclSerialCommunicationState.csRlsdHold) <> 0 Then
            Str += "csRlsdHold "
        End If
        If (States And wclSerialCommunicationState.csXoffHold) <> 0 Then
            Str += "csXoffHold "
        End If
        If (States And wclSerialCommunicationState.csXoffSent) <> 0 Then
            Str += "csXoffSent "
        End If
        If (States And wclSerialCommunicationState.csEof) <> 0 Then
            Str += "csEof "
        End If
        If (States And wclSerialCommunicationState.csTxim) <> 0 Then
            Str += "csTxim "
        End If
        AddMessage("States: " + Str)
    End Sub

    Private Sub wclSerialClient_OnEvents(ByVal Sender As Object, ByVal Events As wclSerial.wclSerialEvent) Handles wclSerialClient.OnEvents
        Dim Str As String = ""
        If (Events And wclSerialEvent.evBreak) <> 0 Then
            Str += "evBreak "
        End If
        If (Events And wclSerialEvent.evCts) <> 0 Then
            Str += "evCts "
        End If
        If (Events And wclSerialEvent.evDsr) <> 0 Then
            Str += "evDsr "
        End If
        If (Events And wclSerialEvent.evRing) <> 0 Then
            Str += "evRing "
        End If
        If (Events And wclSerialEvent.evRlsd) <> 0 Then
            Str += "evRlsd "
        End If
        If (Events And wclSerialEvent.evChar) <> 0 Then
            Str += "evChar "
        End If
        AddMessage("Event: " + Str)

        If Events <> 0 Then
            Dim Status As wclModemStatus
            Dim Res As Int32 = wclSerialClient.GetModemStatus(Status)
            If Res <> wclErrors.WCL_E_SUCCESS Then
                AddMessage("GetModemStatus error: 0x" + Res.ToString("X8"))
            Else
                Str = ""
                If (Status And wclModemStatus.msCtsOn) <> 0 Then
                    Str += "msCtsOn "
                End If
                If (Status And wclModemStatus.msDsrOn) <> 0 Then
                    Str += "msDsrOn "
                End If
                If (Status And wclModemStatus.msRingOn) <> 0 Then
                    Str += "msRingOn "
                End If
                If (Status And wclModemStatus.msDsrOn) <> 0 Then
                    Str += "msDsrOn "
                End If
                If (Status And wclModemStatus.msRlsdOn) <> 0 Then
                    Str += "msRlsdOn "
                End If
                If Str <> "" Then
                    AddMessage("Modem status: " + Str)
                End If
            End If
        End If
    End Sub

    Private Sub wclSerialClient_OnReadError(ByVal Sender As Object, ByVal [Error] As Integer) Handles wclSerialClient.OnReadError
        AddMessage("Read error: 0x" + [Error].ToString("X8"))
    End Sub

    Private Sub wclSerialClient_OnData(ByVal Sender As Object, ByVal Data() As Byte) Handles wclSerialClient.OnData
        Dim Str As String = ""
        If Data IsNot Nothing AndAlso Data.Length > 0 Then
            Str = System.Text.Encoding.ASCII.GetString(Data)
            cResult = Str
        End If
    End Sub

    Private Sub AddMessage(cMessage As String)
        cMessages = cMessage.Trim + vbCrLf
    End Sub

    Public Function GetData() As Double

        GetData = 0

        Dim cBuffer As String = ""

        Do While True
            Application.DoEvents()
            If InStr(cResult, "+") > 0 Then
                cBuffer = cResult
                cBuffer = Replace(cResult, "g", "")
                GetData = Val(cBuffer)
                Exit Function
            End If
        Loop
    End Function

End Class
