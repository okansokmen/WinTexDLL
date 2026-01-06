Imports System.Windows.Forms
Imports wclSerial
Imports wclCommon
Imports wclCommunication

Public Class fmMain

    Private WithEvents wclSerialClient As wclSerialClient
    Private wclSerialMonitor As wclSerialMonitor

    Private Sub fmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        wclSerialMonitor = New wclSerialMonitor()

        wclSerialClient = New wclSerialClient()

        EnumComPorts()

        ClearConfig()
        ClearTiemouts()
        ClearBuffers()

        edWriteTimeout.Text = wclSerialClient.WriteTimeout.ToString()

        cbFunction.SelectedIndex = 0
        cbLineFeed.SelectedIndex = 0
    End Sub

    Private Sub wclSerialClient_OnConnect(ByVal Sender As Object, ByVal [Error] As Integer) Handles wclSerialClient.OnConnect
        If [Error] = wclErrors.WCL_E_SUCCESS Then
            lbEvents.Items.Add("Connected to Serial Device: " + wclSerialClient.DeviceName)

            ReadConfiguration()
            ReadTiemouts()
            ReadBuffers()
        Else
            lbEvents.Items.Add("Connect error: 0x" + [Error].ToString("X8"))
        End If
    End Sub

    Private Sub wclSerialClient_OnData(ByVal Sender As Object, ByVal Data() As Byte) Handles wclSerialClient.OnData
        If Data IsNot Nothing AndAlso Data.Length > 0 Then
            Dim Str As String = System.Text.Encoding.ASCII.GetString(Data)

            lbEvents.Items.Add("Received: " + Str)
        End If
    End Sub

    Private Sub wclSerialClient_OnDisconnect(ByVal Sender As Object, ByVal Reason As Integer) Handles wclSerialClient.OnDisconnect
        lbEvents.Items.Add("Disconnected: 0x" + Reason.ToString("X8"))

        ClearConfig()
        ClearTiemouts()
        ClearBuffers()
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
        lbEvents.Items.Add("Error: " + Str)

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
        lbEvents.Items.Add("States: " + Str)
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
        lbEvents.Items.Add("Event: " + Str)

        If Events <> 0 Then
            Dim Status As wclModemStatus
            Dim Res As Int32 = wclSerialClient.GetModemStatus(Status)
            If Res <> wclErrors.WCL_E_SUCCESS Then
                lbEvents.Items.Add("GetModemStatus error: 0x" + Res.ToString("X8"))
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
                    lbEvents.Items.Add("Modem status: " + Str)
                End If
            End If
        End If
    End Sub

    Private Sub wclSerialClient_OnReadError(ByVal Sender As Object, ByVal [Error] As Integer) Handles wclSerialClient.OnReadError
        lbEvents.Items.Add("Read error: 0x" + [Error].ToString("X8"))
    End Sub

    Private Sub fmMain_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        wclSerialClient.Disconnect()
    End Sub

    Private Sub btClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btClear.Click
        lbEvents.Items.Clear()
    End Sub

    Private Sub ReadConfiguration()
        Dim Config As wclSerialConfig
        Dim Res As Int32 = wclSerialClient.GetConfig(Config)
        If Res <> wclErrors.WCL_E_SUCCESS Then
            lbEvents.Items.Add("Read configuration error: 0x" + Res.ToString("X8"))
        Else
            edbaudRate.Text = Config.BaudRate.ToString()
            edXonLimit.Text = Config.XonLim.ToString()
            edXoffLimit.Text = Config.XoffLim.ToString()
            edXonChar.Text = Config.XonChar.ToString()
            edXoffChar.Text = Config.XoffChar.ToString()
            edErrorChar.Text = Config.ErrorChar.ToString()
            edEofChar.Text = Config.EofChar.ToString()
            edEvtChar.Text = Config.EvtChar.ToString()

            cbParityCheck.Checked = Config.ParityCheck
            cbOutxCtsFlow.Checked = Config.OutxCtsFlow
            cbOutxDsrFlow.Checked = Config.OutxDsrFlow
            cbDsrSensitivity.Checked = Config.DsrSensitivity
            cbTXContinueOnXoff.Checked = Config.TXContinueOnXoff
            cbOutX.Checked = Config.OutX
            cbInX.Checked = Config.InX
            cbErrorCharReplace.Checked = Config.ErrorCharReplace
            cbNullStrip.Checked = Config.NullStrip
            cbAbortOnError.Checked = Config.AbortOnError

            cbRtsControl.SelectedIndex = Config.RtsControl
            cbDtrControl.SelectedIndex = Config.DtrControl
            cbByteSize.SelectedIndex = Config.ByteSize - 4
            cbParity.SelectedIndex = Config.Parity
            cbStopBits.SelectedIndex = Config.StopBits
        End If
    End Sub

    Private Sub ReadTiemouts()
        Dim Times As wclSerialTimeouts
        Dim Res As Int32 = wclSerialClient.GetTimeouts(Times)
        If Res <> wclErrors.WCL_E_SUCCESS Then
            lbEvents.Items.Add("Get timeouts error: 0x" + Res.ToString("X8"))
        Else
            edReadInterval.Text = Times.ReadInterval.ToString()
            edReadMultiplier.Text = Times.ReadMultiplier.ToString()
            edReadConstant.Text = Times.ReadConstant.ToString()
            edWriteMultiplier.Text = Times.WriteMultiplier.ToString()
            edWriteConstant.Text = Times.WriteConstant.ToString()
        End If
    End Sub

    Private Sub btEnum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEnum.Click
        EnumComPorts()
    End Sub

    Private Sub EnumComPorts()
        cbPorts.Items.Clear()

        Dim Ports() As wclSerialDevice = Nothing
        Dim Res As Int32 = wclSerialMonitor.EnumDevices(Ports)
        If Res <> wclErrors.WCL_E_SUCCESS Then
            lbEvents.Items.Add("Error enumerating COM ports: 0x" + Res.ToString("X8"))
        Else
            If Ports IsNot Nothing Then
                Dim i As Integer
                For i = 0 To Ports.Length - 1
                    cbPorts.Items.Add(Ports(i).DeviceName)
                Next i

                If cbPorts.Items.Count > 0 Then
                    cbPorts.SelectedIndex = 0
                Else
                    cbPorts.SelectedIndex = -1
                End If
            End If
        End If
    End Sub

    Private Sub btConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btConnect.Click
        If cbPorts.SelectedIndex = -1 Then
            MessageBox.Show("Select COM port")
        Else
            wclSerialClient.DeviceName = cbPorts.Items(cbPorts.SelectedIndex)

            Dim Res As Int32 = wclSerialClient.Connect()
            If Res <> wclErrors.WCL_E_SUCCESS Then
                MessageBox.Show("Error: 0x" + Res.ToString("X8"))
            End If
        End If
    End Sub

    Private Sub btDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDisconnect.Click
        Dim Res As Int32 = wclSerialClient.Disconnect()
        If Res <> wclErrors.WCL_E_SUCCESS Then
            MessageBox.Show("Error: 0x" + Res.ToString("X8"))
        End If
    End Sub

    Private Sub ClearConfig()
        edbaudRate.Text = ""
        edXoffLimit.Text = ""
        edXonLimit.Text = ""
        edXonChar.Text = ""
        edXoffChar.Text = ""
        edErrorChar.Text = ""
        edEofChar.Text = ""
        edEvtChar.Text = ""

        cbParityCheck.Checked = False
        cbOutxCtsFlow.Checked = False
        cbOutxDsrFlow.Checked = False
        cbDsrSensitivity.Checked = False
        cbTXContinueOnXoff.Checked = False
        cbOutX.Checked = False
        cbInX.Checked = False
        cbErrorCharReplace.Checked = False
        cbNullStrip.Checked = False
        cbAbortOnError.Checked = False

        cbRtsControl.SelectedIndex = -1
        cbDtrControl.SelectedIndex = -1
        cbByteSize.SelectedIndex = -1
        cbParity.SelectedIndex = -1
        cbStopBits.SelectedIndex = -1
    End Sub

    Private Sub btSetConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSetConfig.Click
        Dim Config As wclSerialConfig = New wclSerialConfig()

        Config.BaudRate = Convert.ToUInt32(edbaudRate.Text)
        Config.XonLim = Convert.ToUInt16(edXonLimit.Text)
        Config.XoffLim = Convert.ToUInt16(edXoffLimit.Text)
        Config.XonChar = Convert.ToByte(edXonChar.Text)
        Config.XoffChar = Convert.ToByte(edXoffChar.Text)
        Config.ErrorChar = Convert.ToByte(edErrorChar.Text)
        Config.EofChar = Convert.ToByte(edEofChar.Text)
        Config.EvtChar = Convert.ToByte(edEvtChar.Text)

        Config.ParityCheck = cbParityCheck.Checked
        Config.OutxCtsFlow = cbOutxCtsFlow.Checked
        Config.OutxDsrFlow = cbOutxDsrFlow.Checked
        Config.DsrSensitivity = cbDsrSensitivity.Checked
        Config.TXContinueOnXoff = cbTXContinueOnXoff.Checked
        Config.OutX = cbOutX.Checked
        Config.InX = cbInX.Checked
        Config.ErrorCharReplace = cbErrorCharReplace.Checked
        Config.NullStrip = cbNullStrip.Checked
        Config.AbortOnError = cbAbortOnError.Checked

        Config.RtsControl = cbRtsControl.SelectedIndex
        Config.DtrControl = cbDtrControl.SelectedIndex
        Config.ByteSize = cbByteSize.SelectedIndex + 4
        Config.Parity = cbParity.SelectedIndex
        Config.StopBits = cbStopBits.SelectedIndex

        Dim Res As Int32 = wclSerialClient.SetConfig(Config)
        If Res <> wclErrors.WCL_E_SUCCESS Then
            MessageBox.Show("Error: 0x" + Res.ToString("X8"))
        End If
    End Sub

    Private Sub btGetConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGetConfig.Click
        ReadConfiguration()
    End Sub

    Private Sub ClearBuffers()
        edReadBufferSize.Text = ""
        edWriteBufferSize.Text = ""
    End Sub

    Private Sub ClearTiemouts()
        edReadInterval.Text = ""
        edReadMultiplier.Text = ""
        edReadConstant.Text = ""
        edWriteMultiplier.Text = ""
        edWriteConstant.Text = ""
    End Sub

    Private Sub ReadBuffers()
        Dim Size As UInt32
        Dim Res As Int32 = wclSerialClient.GetReadBufferSize(Size)
        If Res <> wclErrors.WCL_E_SUCCESS Then
            lbEvents.Items.Add("Get read buffer size error: 0x" + Res.ToString("X8"))
        Else
            edReadBufferSize.Text = Size.ToString()
        End If

        Res = wclSerialClient.GetWriteBufferSize(Size)
        If Res <> wclErrors.WCL_E_SUCCESS Then
            lbEvents.Items.Add("Get write buffer size error: 0x" + Res.ToString("X8"))
        Else
            edWriteBufferSize.Text = Size.ToString()
        End If
    End Sub

    Private Sub btGetBuffers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGetBuffers.Click
        ReadBuffers()
    End Sub

    Private Sub btSetBuffers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSetBuffers.Click
        Dim Res As Int32 = wclSerialClient.SetReadBufferSize(Convert.ToUInt32(edReadBufferSize.Text))
        If Res <> wclErrors.WCL_E_SUCCESS Then
            lbEvents.Items.Add("Set read buffer size error: 0x" + Res.ToString("X8"))
        End If

        Res = wclSerialClient.SetWriteBufferSize(Convert.ToUInt32(edWriteBufferSize.Text))
        If Res <> wclErrors.WCL_E_SUCCESS Then
            lbEvents.Items.Add("Set write buffer size error: 0x" + Res.ToString("X8"))
        End If
    End Sub

    Private Sub btGetTimeouts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGetTimeouts.Click
        ReadTiemouts()
    End Sub

    Private Sub btSetTimeouts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSetTimeouts.Click
        Dim Times As wclSerialTimeouts = New wclSerialTimeouts()
        Times.ReadInterval = Convert.ToUInt32(edReadInterval.Text)
        Times.ReadMultiplier = Convert.ToUInt32(edReadMultiplier.Text)
        Times.ReadConstant = Convert.ToUInt32(edReadConstant.Text)
        Times.WriteMultiplier = Convert.ToUInt32(edWriteMultiplier.Text)
        Times.WriteConstant = Convert.ToUInt32(edWriteConstant.Text)
        Dim Res As Int32 = wclSerialClient.SetTimeouts(Times)
        If Res <> wclErrors.WCL_E_SUCCESS Then
            lbEvents.Items.Add("Set timeouts error: 0x" + Res.ToString("X8"))
        End If
    End Sub

    Private Sub btClearCommBreak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btClearCommBreak.Click
        Dim Res As Int32 = wclSerialClient.ClearCommBreak()
        If Res <> wclErrors.WCL_E_SUCCESS Then
            MessageBox.Show("Error: 0x" + Res.ToString("X8"))
        End If
    End Sub

    Private Sub btSetCommBreak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSetCommBreak.Click
        Dim Res As Int32 = wclSerialClient.SetCommBreak()
        If Res <> wclErrors.WCL_E_SUCCESS Then
            MessageBox.Show("Error: 0x" + Res.ToString("X8"))
        End If
    End Sub

    Private Sub btFlushBuffers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFlushBuffers.Click
        Dim Res As Int32 = wclSerialClient.FlushBuffers()
        If Res <> wclErrors.WCL_E_SUCCESS Then
            MessageBox.Show("Error: 0x" + Res.ToString("X8"))
        End If
    End Sub

    Private Sub btExecFunc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btExecFunc.Click
        Dim Res As Int32 = wclSerialClient.EscapeCommFunction(cbFunction.SelectedIndex)
        If Res <> wclErrors.WCL_E_SUCCESS Then
            MessageBox.Show("Error: 0x" + Res.ToString("X8"))
        End If
    End Sub

    Private Sub btPurge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btPurge.Click
        Dim Flags As wclSerialPurgeFlag = 0
        If cbpurgeRxAbort.Checked Then
            Flags = Flags Or wclSerialPurgeFlag.purgeRxAbort
        End If
        If cbpurgeRxClear.Checked Then
            Flags = Flags Or wclSerialPurgeFlag.purgeRxClear
        End If
        If cbpurgeTxAbort.Checked Then
            Flags = Flags Or wclSerialPurgeFlag.purgeTxAbort
        End If
        If cbpurgeTxClear.Checked Then
            Flags = Flags Or wclSerialPurgeFlag.purgeTxClear
        End If

        Dim Res As Int32 = wclSerialClient.PurgeComm(Flags)
        If Res <> wclErrors.WCL_E_SUCCESS Then
            MessageBox.Show("Error: 0x" + Res.ToString("X8"))
        End If
    End Sub

    Private Sub btSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSend.Click
        Dim s As String = edText.Text
        Select Case cbLineFeed.SelectedIndex
            Case 1
                s = s + Asc(13)
            Case 2
                s = s + Asc(10)
            Case 3
                s = s + Asc(13) + Asc(10)
        End Select
        Dim Ansi() As Byte = System.Text.Encoding.ASCII.GetBytes(s)
        Dim Sent As UInt32 = 0
        Dim Res As Int32 = wclSerialClient.Write(Ansi, Sent)
        lbEvents.Items.Add("Sent: " + Sent.ToString() + " bytes from " + Ansi.Length.ToString())
        If Res <> wclErrors.WCL_E_SUCCESS Then
            lbEvents.Items.Add("Write error: 0x" + Res.ToString("X8"))
        End If
    End Sub

    Private Sub btTransmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btTransmit.Click
        Dim Res As Int32 = wclSerialClient.TransmitCommChar(Convert.ToByte(edCharCode.Text))
        If Res <> wclErrors.WCL_E_SUCCESS Then
            MessageBox.Show("Error: 0x" + Res.ToString("X8"))
        End If
    End Sub

    Private Sub btSetWriteTimeout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSetWriteTimeout.Click
        wclSerialClient.WriteTimeout = Convert.ToUInt32(edWriteTimeout.Text)
    End Sub
End Class
