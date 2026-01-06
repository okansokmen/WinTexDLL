<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btClear = New System.Windows.Forms.Button()
        Me.lbEvents = New System.Windows.Forms.ListBox()
        Me.btSend = New System.Windows.Forms.Button()
        Me.edText = New System.Windows.Forms.TextBox()
        Me.btFlushBuffers = New System.Windows.Forms.Button()
        Me.btTransmit = New System.Windows.Forms.Button()
        Me.edCharCode = New System.Windows.Forms.TextBox()
        Me.laCharCode = New System.Windows.Forms.Label()
        Me.btPurge = New System.Windows.Forms.Button()
        Me.cbpurgeTxClear = New System.Windows.Forms.CheckBox()
        Me.cbpurgeRxClear = New System.Windows.Forms.CheckBox()
        Me.cbpurgeTxAbort = New System.Windows.Forms.CheckBox()
        Me.cbpurgeRxAbort = New System.Windows.Forms.CheckBox()
        Me.btExecFunc = New System.Windows.Forms.Button()
        Me.cbFunction = New System.Windows.Forms.ComboBox()
        Me.laFunction = New System.Windows.Forms.Label()
        Me.btSetCommBreak = New System.Windows.Forms.Button()
        Me.btClearCommBreak = New System.Windows.Forms.Button()
        Me.edWriteConstant = New System.Windows.Forms.TextBox()
        Me.edWriteMultiplier = New System.Windows.Forms.TextBox()
        Me.edReadConstant = New System.Windows.Forms.TextBox()
        Me.edReadMultiplier = New System.Windows.Forms.TextBox()
        Me.laWriteConstant = New System.Windows.Forms.Label()
        Me.laWriteMultiplier = New System.Windows.Forms.Label()
        Me.laReadConstant = New System.Windows.Forms.Label()
        Me.laReadMultiplier = New System.Windows.Forms.Label()
        Me.edReadInterval = New System.Windows.Forms.TextBox()
        Me.laReadInterval = New System.Windows.Forms.Label()
        Me.btSetTimeouts = New System.Windows.Forms.Button()
        Me.btGetTimeouts = New System.Windows.Forms.Button()
        Me.edWriteBufferSize = New System.Windows.Forms.TextBox()
        Me.laWriteBufferSize = New System.Windows.Forms.Label()
        Me.edReadBufferSize = New System.Windows.Forms.TextBox()
        Me.laReadBufferSize = New System.Windows.Forms.Label()
        Me.btSetBuffers = New System.Windows.Forms.Button()
        Me.btGetBuffers = New System.Windows.Forms.Button()
        Me.cbAbortOnError = New System.Windows.Forms.CheckBox()
        Me.cbInX = New System.Windows.Forms.CheckBox()
        Me.cbOutX = New System.Windows.Forms.CheckBox()
        Me.cbDsrSensitivity = New System.Windows.Forms.CheckBox()
        Me.cbOutxCtsFlow = New System.Windows.Forms.CheckBox()
        Me.cbNullStrip = New System.Windows.Forms.CheckBox()
        Me.cbErrorCharReplace = New System.Windows.Forms.CheckBox()
        Me.cbTXContinueOnXoff = New System.Windows.Forms.CheckBox()
        Me.cbOutxDsrFlow = New System.Windows.Forms.CheckBox()
        Me.cbParityCheck = New System.Windows.Forms.CheckBox()
        Me.edEvtChar = New System.Windows.Forms.TextBox()
        Me.laEvtChar = New System.Windows.Forms.Label()
        Me.edEofChar = New System.Windows.Forms.TextBox()
        Me.laEofChar = New System.Windows.Forms.Label()
        Me.edErrorChar = New System.Windows.Forms.TextBox()
        Me.laErrorChar = New System.Windows.Forms.Label()
        Me.edXoffChar = New System.Windows.Forms.TextBox()
        Me.laXoffChar = New System.Windows.Forms.Label()
        Me.edXonChar = New System.Windows.Forms.TextBox()
        Me.laXonChar = New System.Windows.Forms.Label()
        Me.edXoffLimit = New System.Windows.Forms.TextBox()
        Me.cbStopBits = New System.Windows.Forms.ComboBox()
        Me.cbByteSize = New System.Windows.Forms.ComboBox()
        Me.cbDtrControl = New System.Windows.Forms.ComboBox()
        Me.laXoffLimit = New System.Windows.Forms.Label()
        Me.laStopBits = New System.Windows.Forms.Label()
        Me.laByteSize = New System.Windows.Forms.Label()
        Me.laDtrControl = New System.Windows.Forms.Label()
        Me.laXonLimit = New System.Windows.Forms.Label()
        Me.edXonLimit = New System.Windows.Forms.TextBox()
        Me.laParity = New System.Windows.Forms.Label()
        Me.cbParity = New System.Windows.Forms.ComboBox()
        Me.laRtsControl = New System.Windows.Forms.Label()
        Me.cbRtsControl = New System.Windows.Forms.ComboBox()
        Me.edbaudRate = New System.Windows.Forms.TextBox()
        Me.laBaudRate = New System.Windows.Forms.Label()
        Me.btSetConfig = New System.Windows.Forms.Button()
        Me.btGetConfig = New System.Windows.Forms.Button()
        Me.btSetWriteTimeout = New System.Windows.Forms.Button()
        Me.edWriteTimeout = New System.Windows.Forms.TextBox()
        Me.laWriteTimeout = New System.Windows.Forms.Label()
        Me.btDisconnect = New System.Windows.Forms.Button()
        Me.btConnect = New System.Windows.Forms.Button()
        Me.cbPorts = New System.Windows.Forms.ComboBox()
        Me.btEnum = New System.Windows.Forms.Button()
        Me.cbLineFeed = New System.Windows.Forms.ComboBox()
        Me.laLineFeed = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btClear
        '
        Me.btClear.Location = New System.Drawing.Point(958, 341)
        Me.btClear.Name = "btClear"
        Me.btClear.Size = New System.Drawing.Size(75, 23)
        Me.btClear.TabIndex = 161
        Me.btClear.Text = "Clear"
        Me.btClear.UseVisualStyleBackColor = True
        '
        'lbEvents
        '
        Me.lbEvents.FormattingEnabled = True
        Me.lbEvents.Location = New System.Drawing.Point(12, 369)
        Me.lbEvents.Name = "lbEvents"
        Me.lbEvents.Size = New System.Drawing.Size(1018, 238)
        Me.lbEvents.TabIndex = 160
        '
        'btSend
        '
        Me.btSend.Location = New System.Drawing.Point(554, 339)
        Me.btSend.Name = "btSend"
        Me.btSend.Size = New System.Drawing.Size(75, 23)
        Me.btSend.TabIndex = 159
        Me.btSend.Text = "Send"
        Me.btSend.UseVisualStyleBackColor = True
        '
        'edText
        '
        Me.edText.Location = New System.Drawing.Point(15, 341)
        Me.edText.Name = "edText"
        Me.edText.Size = New System.Drawing.Size(533, 20)
        Me.edText.TabIndex = 158
        Me.edText.Text = "Something to send to serial"
        '
        'btFlushBuffers
        '
        Me.btFlushBuffers.Location = New System.Drawing.Point(769, 218)
        Me.btFlushBuffers.Name = "btFlushBuffers"
        Me.btFlushBuffers.Size = New System.Drawing.Size(89, 23)
        Me.btFlushBuffers.TabIndex = 157
        Me.btFlushBuffers.Text = "Flush Buffers"
        Me.btFlushBuffers.UseVisualStyleBackColor = True
        '
        'btTransmit
        '
        Me.btTransmit.Location = New System.Drawing.Point(947, 185)
        Me.btTransmit.Name = "btTransmit"
        Me.btTransmit.Size = New System.Drawing.Size(75, 23)
        Me.btTransmit.TabIndex = 156
        Me.btTransmit.Text = "Transmit"
        Me.btTransmit.UseVisualStyleBackColor = True
        '
        'edCharCode
        '
        Me.edCharCode.Location = New System.Drawing.Point(864, 187)
        Me.edCharCode.Name = "edCharCode"
        Me.edCharCode.Size = New System.Drawing.Size(77, 20)
        Me.edCharCode.TabIndex = 155
        '
        'laCharCode
        '
        Me.laCharCode.AutoSize = True
        Me.laCharCode.Location = New System.Drawing.Point(766, 190)
        Me.laCharCode.Name = "laCharCode"
        Me.laCharCode.Size = New System.Drawing.Size(92, 13)
        Me.laCharCode.TabIndex = 154
        Me.laCharCode.Text = "Char code (ASCII)"
        '
        'btPurge
        '
        Me.btPurge.Location = New System.Drawing.Point(938, 130)
        Me.btPurge.Name = "btPurge"
        Me.btPurge.Size = New System.Drawing.Size(75, 23)
        Me.btPurge.TabIndex = 153
        Me.btPurge.Text = "Purge"
        Me.btPurge.UseVisualStyleBackColor = True
        '
        'cbpurgeTxClear
        '
        Me.cbpurgeTxClear.AutoSize = True
        Me.cbpurgeTxClear.Location = New System.Drawing.Point(842, 148)
        Me.cbpurgeTxClear.Name = "cbpurgeTxClear"
        Me.cbpurgeTxClear.Size = New System.Drawing.Size(65, 17)
        Me.cbpurgeTxClear.TabIndex = 152
        Me.cbpurgeTxClear.Text = "Tx Clear"
        Me.cbpurgeTxClear.UseVisualStyleBackColor = True
        '
        'cbpurgeRxClear
        '
        Me.cbpurgeRxClear.AutoSize = True
        Me.cbpurgeRxClear.Location = New System.Drawing.Point(769, 148)
        Me.cbpurgeRxClear.Name = "cbpurgeRxClear"
        Me.cbpurgeRxClear.Size = New System.Drawing.Size(66, 17)
        Me.cbpurgeRxClear.TabIndex = 151
        Me.cbpurgeRxClear.Text = "Rx Clear"
        Me.cbpurgeRxClear.UseVisualStyleBackColor = True
        '
        'cbpurgeTxAbort
        '
        Me.cbpurgeTxAbort.AutoSize = True
        Me.cbpurgeTxAbort.Location = New System.Drawing.Point(842, 125)
        Me.cbpurgeTxAbort.Name = "cbpurgeTxAbort"
        Me.cbpurgeTxAbort.Size = New System.Drawing.Size(66, 17)
        Me.cbpurgeTxAbort.TabIndex = 150
        Me.cbpurgeTxAbort.Text = "Tx Abort"
        Me.cbpurgeTxAbort.UseVisualStyleBackColor = True
        '
        'cbpurgeRxAbort
        '
        Me.cbpurgeRxAbort.AutoSize = True
        Me.cbpurgeRxAbort.Location = New System.Drawing.Point(769, 125)
        Me.cbpurgeRxAbort.Name = "cbpurgeRxAbort"
        Me.cbpurgeRxAbort.Size = New System.Drawing.Size(67, 17)
        Me.cbpurgeRxAbort.TabIndex = 149
        Me.cbpurgeRxAbort.Text = "Rx Abort"
        Me.cbpurgeRxAbort.UseVisualStyleBackColor = True
        '
        'btExecFunc
        '
        Me.btExecFunc.Location = New System.Drawing.Point(947, 83)
        Me.btExecFunc.Name = "btExecFunc"
        Me.btExecFunc.Size = New System.Drawing.Size(75, 23)
        Me.btExecFunc.TabIndex = 148
        Me.btExecFunc.Text = "Exec func"
        Me.btExecFunc.UseVisualStyleBackColor = True
        '
        'cbFunction
        '
        Me.cbFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFunction.FormattingEnabled = True
        Me.cbFunction.Items.AddRange(New Object() {"escClrBreak", "escClrDtr", "escClrRts", "escSetBreak", "escSetDtr", "escSetRts", "escSetXoff", "escSetXon"})
        Me.cbFunction.Location = New System.Drawing.Point(820, 84)
        Me.cbFunction.Name = "cbFunction"
        Me.cbFunction.Size = New System.Drawing.Size(121, 21)
        Me.cbFunction.TabIndex = 147
        '
        'laFunction
        '
        Me.laFunction.AutoSize = True
        Me.laFunction.Location = New System.Drawing.Point(766, 87)
        Me.laFunction.Name = "laFunction"
        Me.laFunction.Size = New System.Drawing.Size(48, 13)
        Me.laFunction.TabIndex = 146
        Me.laFunction.Text = "Function"
        '
        'btSetCommBreak
        '
        Me.btSetCommBreak.Location = New System.Drawing.Point(889, 55)
        Me.btSetCommBreak.Name = "btSetCommBreak"
        Me.btSetCommBreak.Size = New System.Drawing.Size(114, 23)
        Me.btSetCommBreak.TabIndex = 145
        Me.btSetCommBreak.Text = "Set Comm Break"
        Me.btSetCommBreak.UseVisualStyleBackColor = True
        '
        'btClearCommBreak
        '
        Me.btClearCommBreak.Location = New System.Drawing.Point(769, 55)
        Me.btClearCommBreak.Name = "btClearCommBreak"
        Me.btClearCommBreak.Size = New System.Drawing.Size(114, 23)
        Me.btClearCommBreak.TabIndex = 144
        Me.btClearCommBreak.Text = "Clear Comm Break"
        Me.btClearCommBreak.UseVisualStyleBackColor = True
        '
        'edWriteConstant
        '
        Me.edWriteConstant.Location = New System.Drawing.Point(632, 273)
        Me.edWriteConstant.Name = "edWriteConstant"
        Me.edWriteConstant.Size = New System.Drawing.Size(100, 20)
        Me.edWriteConstant.TabIndex = 143
        '
        'edWriteMultiplier
        '
        Me.edWriteMultiplier.Location = New System.Drawing.Point(632, 246)
        Me.edWriteMultiplier.Name = "edWriteMultiplier"
        Me.edWriteMultiplier.Size = New System.Drawing.Size(100, 20)
        Me.edWriteMultiplier.TabIndex = 142
        '
        'edReadConstant
        '
        Me.edReadConstant.Location = New System.Drawing.Point(632, 220)
        Me.edReadConstant.Name = "edReadConstant"
        Me.edReadConstant.Size = New System.Drawing.Size(100, 20)
        Me.edReadConstant.TabIndex = 141
        '
        'edReadMultiplier
        '
        Me.edReadMultiplier.Location = New System.Drawing.Point(632, 194)
        Me.edReadMultiplier.Name = "edReadMultiplier"
        Me.edReadMultiplier.Size = New System.Drawing.Size(100, 20)
        Me.edReadMultiplier.TabIndex = 140
        '
        'laWriteConstant
        '
        Me.laWriteConstant.AutoSize = True
        Me.laWriteConstant.Location = New System.Drawing.Point(548, 276)
        Me.laWriteConstant.Name = "laWriteConstant"
        Me.laWriteConstant.Size = New System.Drawing.Size(77, 13)
        Me.laWriteConstant.TabIndex = 139
        Me.laWriteConstant.Text = "Write Constant"
        '
        'laWriteMultiplier
        '
        Me.laWriteMultiplier.AutoSize = True
        Me.laWriteMultiplier.Location = New System.Drawing.Point(548, 253)
        Me.laWriteMultiplier.Name = "laWriteMultiplier"
        Me.laWriteMultiplier.Size = New System.Drawing.Size(76, 13)
        Me.laWriteMultiplier.TabIndex = 138
        Me.laWriteMultiplier.Text = "Write Multiplier"
        '
        'laReadConstant
        '
        Me.laReadConstant.AutoSize = True
        Me.laReadConstant.Location = New System.Drawing.Point(548, 226)
        Me.laReadConstant.Name = "laReadConstant"
        Me.laReadConstant.Size = New System.Drawing.Size(78, 13)
        Me.laReadConstant.TabIndex = 137
        Me.laReadConstant.Text = "Read Constant"
        '
        'laReadMultiplier
        '
        Me.laReadMultiplier.AutoSize = True
        Me.laReadMultiplier.Location = New System.Drawing.Point(548, 197)
        Me.laReadMultiplier.Name = "laReadMultiplier"
        Me.laReadMultiplier.Size = New System.Drawing.Size(77, 13)
        Me.laReadMultiplier.TabIndex = 136
        Me.laReadMultiplier.Text = "Read Multiplier"
        '
        'edReadInterval
        '
        Me.edReadInterval.Location = New System.Drawing.Point(632, 169)
        Me.edReadInterval.Name = "edReadInterval"
        Me.edReadInterval.Size = New System.Drawing.Size(100, 20)
        Me.edReadInterval.TabIndex = 135
        '
        'laReadInterval
        '
        Me.laReadInterval.AutoSize = True
        Me.laReadInterval.Location = New System.Drawing.Point(548, 172)
        Me.laReadInterval.Name = "laReadInterval"
        Me.laReadInterval.Size = New System.Drawing.Size(70, 13)
        Me.laReadInterval.TabIndex = 134
        Me.laReadInterval.Text = "Read interval"
        '
        'btSetTimeouts
        '
        Me.btSetTimeouts.Location = New System.Drawing.Point(632, 140)
        Me.btSetTimeouts.Name = "btSetTimeouts"
        Me.btSetTimeouts.Size = New System.Drawing.Size(75, 23)
        Me.btSetTimeouts.TabIndex = 133
        Me.btSetTimeouts.Text = "Set timeouts"
        Me.btSetTimeouts.UseVisualStyleBackColor = True
        '
        'btGetTimeouts
        '
        Me.btGetTimeouts.Location = New System.Drawing.Point(551, 140)
        Me.btGetTimeouts.Name = "btGetTimeouts"
        Me.btGetTimeouts.Size = New System.Drawing.Size(75, 23)
        Me.btGetTimeouts.TabIndex = 132
        Me.btGetTimeouts.Text = "Get timeouts"
        Me.btGetTimeouts.UseVisualStyleBackColor = True
        '
        'edWriteBufferSize
        '
        Me.edWriteBufferSize.Location = New System.Drawing.Point(638, 110)
        Me.edWriteBufferSize.Name = "edWriteBufferSize"
        Me.edWriteBufferSize.Size = New System.Drawing.Size(100, 20)
        Me.edWriteBufferSize.TabIndex = 131
        '
        'laWriteBufferSize
        '
        Me.laWriteBufferSize.AutoSize = True
        Me.laWriteBufferSize.Location = New System.Drawing.Point(548, 113)
        Me.laWriteBufferSize.Name = "laWriteBufferSize"
        Me.laWriteBufferSize.Size = New System.Drawing.Size(83, 13)
        Me.laWriteBufferSize.TabIndex = 130
        Me.laWriteBufferSize.Text = "Write buffer size"
        '
        'edReadBufferSize
        '
        Me.edReadBufferSize.Location = New System.Drawing.Point(638, 80)
        Me.edReadBufferSize.Name = "edReadBufferSize"
        Me.edReadBufferSize.Size = New System.Drawing.Size(100, 20)
        Me.edReadBufferSize.TabIndex = 129
        '
        'laReadBufferSize
        '
        Me.laReadBufferSize.AutoSize = True
        Me.laReadBufferSize.Location = New System.Drawing.Point(548, 87)
        Me.laReadBufferSize.Name = "laReadBufferSize"
        Me.laReadBufferSize.Size = New System.Drawing.Size(84, 13)
        Me.laReadBufferSize.TabIndex = 128
        Me.laReadBufferSize.Text = "Read buffer size"
        '
        'btSetBuffers
        '
        Me.btSetBuffers.Location = New System.Drawing.Point(632, 55)
        Me.btSetBuffers.Name = "btSetBuffers"
        Me.btSetBuffers.Size = New System.Drawing.Size(75, 23)
        Me.btSetBuffers.TabIndex = 127
        Me.btSetBuffers.Text = "Set buffers"
        Me.btSetBuffers.UseVisualStyleBackColor = True
        '
        'btGetBuffers
        '
        Me.btGetBuffers.Location = New System.Drawing.Point(551, 55)
        Me.btGetBuffers.Name = "btGetBuffers"
        Me.btGetBuffers.Size = New System.Drawing.Size(75, 23)
        Me.btGetBuffers.TabIndex = 126
        Me.btGetBuffers.Text = "Get buffers"
        Me.btGetBuffers.UseVisualStyleBackColor = True
        '
        'cbAbortOnError
        '
        Me.cbAbortOnError.AutoSize = True
        Me.cbAbortOnError.Location = New System.Drawing.Point(266, 318)
        Me.cbAbortOnError.Name = "cbAbortOnError"
        Me.cbAbortOnError.Size = New System.Drawing.Size(93, 17)
        Me.cbAbortOnError.TabIndex = 125
        Me.cbAbortOnError.Text = "Abort On Error"
        Me.cbAbortOnError.UseVisualStyleBackColor = True
        '
        'cbInX
        '
        Me.cbInX.AutoSize = True
        Me.cbInX.Location = New System.Drawing.Point(266, 295)
        Me.cbInX.Name = "cbInX"
        Me.cbInX.Size = New System.Drawing.Size(45, 17)
        Me.cbInX.TabIndex = 124
        Me.cbInX.Text = "In X"
        Me.cbInX.UseVisualStyleBackColor = True
        '
        'cbOutX
        '
        Me.cbOutX.AutoSize = True
        Me.cbOutX.Location = New System.Drawing.Point(266, 272)
        Me.cbOutX.Name = "cbOutX"
        Me.cbOutX.Size = New System.Drawing.Size(53, 17)
        Me.cbOutX.TabIndex = 123
        Me.cbOutX.Text = "Out X"
        Me.cbOutX.UseVisualStyleBackColor = True
        '
        'cbDsrSensitivity
        '
        Me.cbDsrSensitivity.AutoSize = True
        Me.cbDsrSensitivity.Location = New System.Drawing.Point(266, 249)
        Me.cbDsrSensitivity.Name = "cbDsrSensitivity"
        Me.cbDsrSensitivity.Size = New System.Drawing.Size(92, 17)
        Me.cbDsrSensitivity.TabIndex = 122
        Me.cbDsrSensitivity.Text = "Dsr Sensitivity"
        Me.cbDsrSensitivity.UseVisualStyleBackColor = True
        '
        'cbOutxCtsFlow
        '
        Me.cbOutxCtsFlow.AutoSize = True
        Me.cbOutxCtsFlow.Location = New System.Drawing.Point(266, 226)
        Me.cbOutxCtsFlow.Name = "cbOutxCtsFlow"
        Me.cbOutxCtsFlow.Size = New System.Drawing.Size(91, 17)
        Me.cbOutxCtsFlow.TabIndex = 121
        Me.cbOutxCtsFlow.Text = "Outx Cts Flow"
        Me.cbOutxCtsFlow.UseVisualStyleBackColor = True
        '
        'cbNullStrip
        '
        Me.cbNullStrip.AutoSize = True
        Me.cbNullStrip.Location = New System.Drawing.Point(83, 318)
        Me.cbNullStrip.Name = "cbNullStrip"
        Me.cbNullStrip.Size = New System.Drawing.Size(68, 17)
        Me.cbNullStrip.TabIndex = 120
        Me.cbNullStrip.Text = "Null Strip"
        Me.cbNullStrip.UseVisualStyleBackColor = True
        '
        'cbErrorCharReplace
        '
        Me.cbErrorCharReplace.AutoSize = True
        Me.cbErrorCharReplace.Location = New System.Drawing.Point(83, 295)
        Me.cbErrorCharReplace.Name = "cbErrorCharReplace"
        Me.cbErrorCharReplace.Size = New System.Drawing.Size(116, 17)
        Me.cbErrorCharReplace.TabIndex = 119
        Me.cbErrorCharReplace.Text = "Error Char Replace"
        Me.cbErrorCharReplace.UseVisualStyleBackColor = True
        '
        'cbTXContinueOnXoff
        '
        Me.cbTXContinueOnXoff.AutoSize = True
        Me.cbTXContinueOnXoff.Location = New System.Drawing.Point(83, 272)
        Me.cbTXContinueOnXoff.Name = "cbTXContinueOnXoff"
        Me.cbTXContinueOnXoff.Size = New System.Drawing.Size(124, 17)
        Me.cbTXContinueOnXoff.TabIndex = 118
        Me.cbTXContinueOnXoff.Text = "TX Continue OnX off"
        Me.cbTXContinueOnXoff.UseVisualStyleBackColor = True
        '
        'cbOutxDsrFlow
        '
        Me.cbOutxDsrFlow.AutoSize = True
        Me.cbOutxDsrFlow.Location = New System.Drawing.Point(83, 249)
        Me.cbOutxDsrFlow.Name = "cbOutxDsrFlow"
        Me.cbOutxDsrFlow.Size = New System.Drawing.Size(104, 17)
        Me.cbOutxDsrFlow.TabIndex = 117
        Me.cbOutxDsrFlow.Text = "OUTX DSR flow"
        Me.cbOutxDsrFlow.UseVisualStyleBackColor = True
        '
        'cbParityCheck
        '
        Me.cbParityCheck.AutoSize = True
        Me.cbParityCheck.Location = New System.Drawing.Point(83, 226)
        Me.cbParityCheck.Name = "cbParityCheck"
        Me.cbParityCheck.Size = New System.Drawing.Size(85, 17)
        Me.cbParityCheck.TabIndex = 116
        Me.cbParityCheck.Text = "Parity check"
        Me.cbParityCheck.UseVisualStyleBackColor = True
        '
        'edEvtChar
        '
        Me.edEvtChar.Location = New System.Drawing.Point(491, 190)
        Me.edEvtChar.Name = "edEvtChar"
        Me.edEvtChar.Size = New System.Drawing.Size(36, 20)
        Me.edEvtChar.TabIndex = 115
        '
        'laEvtChar
        '
        Me.laEvtChar.AutoSize = True
        Me.laEvtChar.Location = New System.Drawing.Point(426, 193)
        Me.laEvtChar.Name = "laEvtChar"
        Me.laEvtChar.Size = New System.Drawing.Size(59, 13)
        Me.laEvtChar.TabIndex = 114
        Me.laEvtChar.Text = "Event char"
        '
        'edEofChar
        '
        Me.edEofChar.Location = New System.Drawing.Point(384, 190)
        Me.edEofChar.Name = "edEofChar"
        Me.edEofChar.Size = New System.Drawing.Size(36, 20)
        Me.edEofChar.TabIndex = 113
        '
        'laEofChar
        '
        Me.laEofChar.AutoSize = True
        Me.laEofChar.Location = New System.Drawing.Point(326, 193)
        Me.laEofChar.Name = "laEofChar"
        Me.laEofChar.Size = New System.Drawing.Size(52, 13)
        Me.laEofChar.TabIndex = 112
        Me.laEofChar.Text = "EOF char"
        '
        'edErrorChar
        '
        Me.edErrorChar.Location = New System.Drawing.Point(284, 190)
        Me.edErrorChar.Name = "edErrorChar"
        Me.edErrorChar.Size = New System.Drawing.Size(36, 20)
        Me.edErrorChar.TabIndex = 111
        '
        'laErrorChar
        '
        Me.laErrorChar.AutoSize = True
        Me.laErrorChar.Location = New System.Drawing.Point(225, 193)
        Me.laErrorChar.Name = "laErrorChar"
        Me.laErrorChar.Size = New System.Drawing.Size(53, 13)
        Me.laErrorChar.TabIndex = 110
        Me.laErrorChar.Text = "Error char"
        '
        'edXoffChar
        '
        Me.edXoffChar.Location = New System.Drawing.Point(178, 190)
        Me.edXoffChar.Name = "edXoffChar"
        Me.edXoffChar.Size = New System.Drawing.Size(36, 20)
        Me.edXoffChar.TabIndex = 109
        '
        'laXoffChar
        '
        Me.laXoffChar.AutoSize = True
        Me.laXoffChar.Location = New System.Drawing.Point(114, 193)
        Me.laXoffChar.Name = "laXoffChar"
        Me.laXoffChar.Size = New System.Drawing.Size(58, 13)
        Me.laXoffChar.TabIndex = 108
        Me.laXoffChar.Text = "XOFF char"
        '
        'edXonChar
        '
        Me.edXonChar.Location = New System.Drawing.Point(72, 190)
        Me.edXonChar.Name = "edXonChar"
        Me.edXonChar.Size = New System.Drawing.Size(36, 20)
        Me.edXonChar.TabIndex = 107
        '
        'laXonChar
        '
        Me.laXonChar.AutoSize = True
        Me.laXonChar.Location = New System.Drawing.Point(12, 193)
        Me.laXonChar.Name = "laXonChar"
        Me.laXonChar.Size = New System.Drawing.Size(54, 13)
        Me.laXonChar.TabIndex = 106
        Me.laXonChar.Text = "XON char"
        '
        'edXoffLimit
        '
        Me.edXoffLimit.Location = New System.Drawing.Point(360, 164)
        Me.edXoffLimit.Name = "edXoffLimit"
        Me.edXoffLimit.Size = New System.Drawing.Size(167, 20)
        Me.edXoffLimit.TabIndex = 105
        '
        'cbStopBits
        '
        Me.cbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStopBits.FormattingEnabled = True
        Me.cbStopBits.Items.AddRange(New Object() {"sbOne", "sbOne5", "sbTwo"})
        Me.cbStopBits.Location = New System.Drawing.Point(360, 137)
        Me.cbStopBits.Name = "cbStopBits"
        Me.cbStopBits.Size = New System.Drawing.Size(167, 21)
        Me.cbStopBits.TabIndex = 104
        '
        'cbByteSize
        '
        Me.cbByteSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbByteSize.FormattingEnabled = True
        Me.cbByteSize.Items.AddRange(New Object() {"4", "5", "6", "7", "8"})
        Me.cbByteSize.Location = New System.Drawing.Point(360, 110)
        Me.cbByteSize.Name = "cbByteSize"
        Me.cbByteSize.Size = New System.Drawing.Size(167, 21)
        Me.cbByteSize.TabIndex = 103
        '
        'cbDtrControl
        '
        Me.cbDtrControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDtrControl.FormattingEnabled = True
        Me.cbDtrControl.Items.AddRange(New Object() {"dtrControlDisable", "dtrControlEnable", "dtrControlHandshake"})
        Me.cbDtrControl.Location = New System.Drawing.Point(360, 83)
        Me.cbDtrControl.Name = "cbDtrControl"
        Me.cbDtrControl.Size = New System.Drawing.Size(167, 21)
        Me.cbDtrControl.TabIndex = 102
        '
        'laXoffLimit
        '
        Me.laXoffLimit.AutoSize = True
        Me.laXoffLimit.Location = New System.Drawing.Point(287, 167)
        Me.laXoffLimit.Name = "laXoffLimit"
        Me.laXoffLimit.Size = New System.Drawing.Size(54, 13)
        Me.laXoffLimit.TabIndex = 101
        Me.laXoffLimit.Text = "XOFF limit"
        '
        'laStopBits
        '
        Me.laStopBits.AutoSize = True
        Me.laStopBits.Location = New System.Drawing.Point(287, 140)
        Me.laStopBits.Name = "laStopBits"
        Me.laStopBits.Size = New System.Drawing.Size(48, 13)
        Me.laStopBits.TabIndex = 100
        Me.laStopBits.Text = "Stop bits"
        '
        'laByteSize
        '
        Me.laByteSize.AutoSize = True
        Me.laByteSize.Location = New System.Drawing.Point(287, 113)
        Me.laByteSize.Name = "laByteSize"
        Me.laByteSize.Size = New System.Drawing.Size(49, 13)
        Me.laByteSize.TabIndex = 99
        Me.laByteSize.Text = "Byte size"
        '
        'laDtrControl
        '
        Me.laDtrControl.AutoSize = True
        Me.laDtrControl.Location = New System.Drawing.Point(287, 87)
        Me.laDtrControl.Name = "laDtrControl"
        Me.laDtrControl.Size = New System.Drawing.Size(65, 13)
        Me.laDtrControl.TabIndex = 98
        Me.laDtrControl.Text = "DTR control"
        '
        'laXonLimit
        '
        Me.laXonLimit.AutoSize = True
        Me.laXonLimit.Location = New System.Drawing.Point(12, 167)
        Me.laXonLimit.Name = "laXonLimit"
        Me.laXonLimit.Size = New System.Drawing.Size(50, 13)
        Me.laXonLimit.TabIndex = 97
        Me.laXonLimit.Text = "XON limit"
        '
        'edXonLimit
        '
        Me.edXonLimit.Location = New System.Drawing.Point(93, 164)
        Me.edXonLimit.Name = "edXonLimit"
        Me.edXonLimit.Size = New System.Drawing.Size(167, 20)
        Me.edXonLimit.TabIndex = 96
        '
        'laParity
        '
        Me.laParity.AutoSize = True
        Me.laParity.Location = New System.Drawing.Point(12, 140)
        Me.laParity.Name = "laParity"
        Me.laParity.Size = New System.Drawing.Size(33, 13)
        Me.laParity.TabIndex = 95
        Me.laParity.Text = "Parity"
        '
        'cbParity
        '
        Me.cbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbParity.FormattingEnabled = True
        Me.cbParity.Items.AddRange(New Object() {"spNo", "spOdd", "spEven", "spMark", "spSpace"})
        Me.cbParity.Location = New System.Drawing.Point(93, 137)
        Me.cbParity.Name = "cbParity"
        Me.cbParity.Size = New System.Drawing.Size(167, 21)
        Me.cbParity.TabIndex = 94
        '
        'laRtsControl
        '
        Me.laRtsControl.AutoSize = True
        Me.laRtsControl.Location = New System.Drawing.Point(12, 113)
        Me.laRtsControl.Name = "laRtsControl"
        Me.laRtsControl.Size = New System.Drawing.Size(64, 13)
        Me.laRtsControl.TabIndex = 93
        Me.laRtsControl.Text = "RTS control"
        '
        'cbRtsControl
        '
        Me.cbRtsControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRtsControl.FormattingEnabled = True
        Me.cbRtsControl.Items.AddRange(New Object() {"rtsControlDisable", "rtsControlEnable", "rtsControlHandshake", "rtsControlToggle"})
        Me.cbRtsControl.Location = New System.Drawing.Point(93, 110)
        Me.cbRtsControl.Name = "cbRtsControl"
        Me.cbRtsControl.Size = New System.Drawing.Size(167, 21)
        Me.cbRtsControl.TabIndex = 92
        '
        'edbaudRate
        '
        Me.edbaudRate.Location = New System.Drawing.Point(93, 84)
        Me.edbaudRate.Name = "edbaudRate"
        Me.edbaudRate.Size = New System.Drawing.Size(167, 20)
        Me.edbaudRate.TabIndex = 91
        '
        'laBaudRate
        '
        Me.laBaudRate.AutoSize = True
        Me.laBaudRate.Location = New System.Drawing.Point(12, 87)
        Me.laBaudRate.Name = "laBaudRate"
        Me.laBaudRate.Size = New System.Drawing.Size(53, 13)
        Me.laBaudRate.TabIndex = 90
        Me.laBaudRate.Text = "Baud rate"
        '
        'btSetConfig
        '
        Me.btSetConfig.Location = New System.Drawing.Point(93, 55)
        Me.btSetConfig.Name = "btSetConfig"
        Me.btSetConfig.Size = New System.Drawing.Size(75, 23)
        Me.btSetConfig.TabIndex = 89
        Me.btSetConfig.Text = "Set Config"
        Me.btSetConfig.UseVisualStyleBackColor = True
        '
        'btGetConfig
        '
        Me.btGetConfig.Location = New System.Drawing.Point(12, 55)
        Me.btGetConfig.Name = "btGetConfig"
        Me.btGetConfig.Size = New System.Drawing.Size(75, 23)
        Me.btGetConfig.TabIndex = 88
        Me.btGetConfig.Text = "Get Config"
        Me.btGetConfig.UseVisualStyleBackColor = True
        '
        'btSetWriteTimeout
        '
        Me.btSetWriteTimeout.Location = New System.Drawing.Point(551, 14)
        Me.btSetWriteTimeout.Name = "btSetWriteTimeout"
        Me.btSetWriteTimeout.Size = New System.Drawing.Size(105, 23)
        Me.btSetWriteTimeout.TabIndex = 87
        Me.btSetWriteTimeout.Text = "Set write timeout"
        Me.btSetWriteTimeout.UseVisualStyleBackColor = True
        '
        'edWriteTimeout
        '
        Me.edWriteTimeout.Location = New System.Drawing.Point(445, 16)
        Me.edWriteTimeout.Name = "edWriteTimeout"
        Me.edWriteTimeout.Size = New System.Drawing.Size(100, 20)
        Me.edWriteTimeout.TabIndex = 86
        '
        'laWriteTimeout
        '
        Me.laWriteTimeout.AutoSize = True
        Me.laWriteTimeout.Location = New System.Drawing.Point(370, 19)
        Me.laWriteTimeout.Name = "laWriteTimeout"
        Me.laWriteTimeout.Size = New System.Drawing.Size(69, 13)
        Me.laWriteTimeout.TabIndex = 85
        Me.laWriteTimeout.Text = "Write timeout"
        '
        'btDisconnect
        '
        Me.btDisconnect.Location = New System.Drawing.Point(266, 14)
        Me.btDisconnect.Name = "btDisconnect"
        Me.btDisconnect.Size = New System.Drawing.Size(75, 23)
        Me.btDisconnect.TabIndex = 84
        Me.btDisconnect.Text = "Disconnect"
        Me.btDisconnect.UseVisualStyleBackColor = True
        '
        'btConnect
        '
        Me.btConnect.Location = New System.Drawing.Point(185, 14)
        Me.btConnect.Name = "btConnect"
        Me.btConnect.Size = New System.Drawing.Size(75, 23)
        Me.btConnect.TabIndex = 83
        Me.btConnect.Text = "Connect"
        Me.btConnect.UseVisualStyleBackColor = True
        '
        'cbPorts
        '
        Me.cbPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPorts.FormattingEnabled = True
        Me.cbPorts.Location = New System.Drawing.Point(93, 16)
        Me.cbPorts.Name = "cbPorts"
        Me.cbPorts.Size = New System.Drawing.Size(86, 21)
        Me.cbPorts.TabIndex = 82
        '
        'btEnum
        '
        Me.btEnum.Location = New System.Drawing.Point(12, 14)
        Me.btEnum.Name = "btEnum"
        Me.btEnum.Size = New System.Drawing.Size(75, 23)
        Me.btEnum.TabIndex = 81
        Me.btEnum.Text = "Enum"
        Me.btEnum.UseVisualStyleBackColor = True
        '
        'cbLineFeed
        '
        Me.cbLineFeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLineFeed.FormattingEnabled = True
        Me.cbLineFeed.Items.AddRange(New Object() {"None", "CR", "LF", "CR & LF"})
        Me.cbLineFeed.Location = New System.Drawing.Point(693, 341)
        Me.cbLineFeed.Name = "cbLineFeed"
        Me.cbLineFeed.Size = New System.Drawing.Size(121, 21)
        Me.cbLineFeed.TabIndex = 163
        '
        'laLineFeed
        '
        Me.laLineFeed.AutoSize = True
        Me.laLineFeed.Location = New System.Drawing.Point(636, 345)
        Me.laLineFeed.Name = "laLineFeed"
        Me.laLineFeed.Size = New System.Drawing.Size(51, 13)
        Me.laLineFeed.TabIndex = 162
        Me.laLineFeed.Text = "Line feed"
        '
        'fmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1042, 621)
        Me.Controls.Add(Me.cbLineFeed)
        Me.Controls.Add(Me.laLineFeed)
        Me.Controls.Add(Me.btClear)
        Me.Controls.Add(Me.lbEvents)
        Me.Controls.Add(Me.btSend)
        Me.Controls.Add(Me.edText)
        Me.Controls.Add(Me.btFlushBuffers)
        Me.Controls.Add(Me.btTransmit)
        Me.Controls.Add(Me.edCharCode)
        Me.Controls.Add(Me.laCharCode)
        Me.Controls.Add(Me.btPurge)
        Me.Controls.Add(Me.cbpurgeTxClear)
        Me.Controls.Add(Me.cbpurgeRxClear)
        Me.Controls.Add(Me.cbpurgeTxAbort)
        Me.Controls.Add(Me.cbpurgeRxAbort)
        Me.Controls.Add(Me.btExecFunc)
        Me.Controls.Add(Me.cbFunction)
        Me.Controls.Add(Me.laFunction)
        Me.Controls.Add(Me.btSetCommBreak)
        Me.Controls.Add(Me.btClearCommBreak)
        Me.Controls.Add(Me.edWriteConstant)
        Me.Controls.Add(Me.edWriteMultiplier)
        Me.Controls.Add(Me.edReadConstant)
        Me.Controls.Add(Me.edReadMultiplier)
        Me.Controls.Add(Me.laWriteConstant)
        Me.Controls.Add(Me.laWriteMultiplier)
        Me.Controls.Add(Me.laReadConstant)
        Me.Controls.Add(Me.laReadMultiplier)
        Me.Controls.Add(Me.edReadInterval)
        Me.Controls.Add(Me.laReadInterval)
        Me.Controls.Add(Me.btSetTimeouts)
        Me.Controls.Add(Me.btGetTimeouts)
        Me.Controls.Add(Me.edWriteBufferSize)
        Me.Controls.Add(Me.laWriteBufferSize)
        Me.Controls.Add(Me.edReadBufferSize)
        Me.Controls.Add(Me.laReadBufferSize)
        Me.Controls.Add(Me.btSetBuffers)
        Me.Controls.Add(Me.btGetBuffers)
        Me.Controls.Add(Me.cbAbortOnError)
        Me.Controls.Add(Me.cbInX)
        Me.Controls.Add(Me.cbOutX)
        Me.Controls.Add(Me.cbDsrSensitivity)
        Me.Controls.Add(Me.cbOutxCtsFlow)
        Me.Controls.Add(Me.cbNullStrip)
        Me.Controls.Add(Me.cbErrorCharReplace)
        Me.Controls.Add(Me.cbTXContinueOnXoff)
        Me.Controls.Add(Me.cbOutxDsrFlow)
        Me.Controls.Add(Me.cbParityCheck)
        Me.Controls.Add(Me.edEvtChar)
        Me.Controls.Add(Me.laEvtChar)
        Me.Controls.Add(Me.edEofChar)
        Me.Controls.Add(Me.laEofChar)
        Me.Controls.Add(Me.edErrorChar)
        Me.Controls.Add(Me.laErrorChar)
        Me.Controls.Add(Me.edXoffChar)
        Me.Controls.Add(Me.laXoffChar)
        Me.Controls.Add(Me.edXonChar)
        Me.Controls.Add(Me.laXonChar)
        Me.Controls.Add(Me.edXoffLimit)
        Me.Controls.Add(Me.cbStopBits)
        Me.Controls.Add(Me.cbByteSize)
        Me.Controls.Add(Me.cbDtrControl)
        Me.Controls.Add(Me.laXoffLimit)
        Me.Controls.Add(Me.laStopBits)
        Me.Controls.Add(Me.laByteSize)
        Me.Controls.Add(Me.laDtrControl)
        Me.Controls.Add(Me.laXonLimit)
        Me.Controls.Add(Me.edXonLimit)
        Me.Controls.Add(Me.laParity)
        Me.Controls.Add(Me.cbParity)
        Me.Controls.Add(Me.laRtsControl)
        Me.Controls.Add(Me.cbRtsControl)
        Me.Controls.Add(Me.edbaudRate)
        Me.Controls.Add(Me.laBaudRate)
        Me.Controls.Add(Me.btSetConfig)
        Me.Controls.Add(Me.btGetConfig)
        Me.Controls.Add(Me.btSetWriteTimeout)
        Me.Controls.Add(Me.edWriteTimeout)
        Me.Controls.Add(Me.laWriteTimeout)
        Me.Controls.Add(Me.btDisconnect)
        Me.Controls.Add(Me.btConnect)
        Me.Controls.Add(Me.cbPorts)
        Me.Controls.Add(Me.btEnum)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "fmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Serial Client Demo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents btClear As System.Windows.Forms.Button
    Private WithEvents lbEvents As System.Windows.Forms.ListBox
    Private WithEvents btSend As System.Windows.Forms.Button
    Private WithEvents edText As System.Windows.Forms.TextBox
    Private WithEvents btFlushBuffers As System.Windows.Forms.Button
    Private WithEvents btTransmit As System.Windows.Forms.Button
    Private WithEvents edCharCode As System.Windows.Forms.TextBox
    Private WithEvents laCharCode As System.Windows.Forms.Label
    Private WithEvents btPurge As System.Windows.Forms.Button
    Private WithEvents cbpurgeTxClear As System.Windows.Forms.CheckBox
    Private WithEvents cbpurgeRxClear As System.Windows.Forms.CheckBox
    Private WithEvents cbpurgeTxAbort As System.Windows.Forms.CheckBox
    Private WithEvents cbpurgeRxAbort As System.Windows.Forms.CheckBox
    Private WithEvents btExecFunc As System.Windows.Forms.Button
    Private WithEvents cbFunction As System.Windows.Forms.ComboBox
    Private WithEvents laFunction As System.Windows.Forms.Label
    Private WithEvents btSetCommBreak As System.Windows.Forms.Button
    Private WithEvents btClearCommBreak As System.Windows.Forms.Button
    Private WithEvents edWriteConstant As System.Windows.Forms.TextBox
    Private WithEvents edWriteMultiplier As System.Windows.Forms.TextBox
    Private WithEvents edReadConstant As System.Windows.Forms.TextBox
    Private WithEvents edReadMultiplier As System.Windows.Forms.TextBox
    Private WithEvents laWriteConstant As System.Windows.Forms.Label
    Private WithEvents laWriteMultiplier As System.Windows.Forms.Label
    Private WithEvents laReadConstant As System.Windows.Forms.Label
    Private WithEvents laReadMultiplier As System.Windows.Forms.Label
    Private WithEvents edReadInterval As System.Windows.Forms.TextBox
    Private WithEvents laReadInterval As System.Windows.Forms.Label
    Private WithEvents btSetTimeouts As System.Windows.Forms.Button
    Private WithEvents btGetTimeouts As System.Windows.Forms.Button
    Private WithEvents edWriteBufferSize As System.Windows.Forms.TextBox
    Private WithEvents laWriteBufferSize As System.Windows.Forms.Label
    Private WithEvents edReadBufferSize As System.Windows.Forms.TextBox
    Private WithEvents laReadBufferSize As System.Windows.Forms.Label
    Private WithEvents btSetBuffers As System.Windows.Forms.Button
    Private WithEvents btGetBuffers As System.Windows.Forms.Button
    Private WithEvents cbAbortOnError As System.Windows.Forms.CheckBox
    Private WithEvents cbInX As System.Windows.Forms.CheckBox
    Private WithEvents cbOutX As System.Windows.Forms.CheckBox
    Private WithEvents cbDsrSensitivity As System.Windows.Forms.CheckBox
    Private WithEvents cbOutxCtsFlow As System.Windows.Forms.CheckBox
    Private WithEvents cbNullStrip As System.Windows.Forms.CheckBox
    Private WithEvents cbErrorCharReplace As System.Windows.Forms.CheckBox
    Private WithEvents cbTXContinueOnXoff As System.Windows.Forms.CheckBox
    Private WithEvents cbOutxDsrFlow As System.Windows.Forms.CheckBox
    Private WithEvents cbParityCheck As System.Windows.Forms.CheckBox
    Private WithEvents edEvtChar As System.Windows.Forms.TextBox
    Private WithEvents laEvtChar As System.Windows.Forms.Label
    Private WithEvents edEofChar As System.Windows.Forms.TextBox
    Private WithEvents laEofChar As System.Windows.Forms.Label
    Private WithEvents edErrorChar As System.Windows.Forms.TextBox
    Private WithEvents laErrorChar As System.Windows.Forms.Label
    Private WithEvents edXoffChar As System.Windows.Forms.TextBox
    Private WithEvents laXoffChar As System.Windows.Forms.Label
    Private WithEvents edXonChar As System.Windows.Forms.TextBox
    Private WithEvents laXonChar As System.Windows.Forms.Label
    Private WithEvents edXoffLimit As System.Windows.Forms.TextBox
    Private WithEvents cbStopBits As System.Windows.Forms.ComboBox
    Private WithEvents cbByteSize As System.Windows.Forms.ComboBox
    Private WithEvents cbDtrControl As System.Windows.Forms.ComboBox
    Private WithEvents laXoffLimit As System.Windows.Forms.Label
    Private WithEvents laStopBits As System.Windows.Forms.Label
    Private WithEvents laByteSize As System.Windows.Forms.Label
    Private WithEvents laDtrControl As System.Windows.Forms.Label
    Private WithEvents laXonLimit As System.Windows.Forms.Label
    Private WithEvents edXonLimit As System.Windows.Forms.TextBox
    Private WithEvents laParity As System.Windows.Forms.Label
    Private WithEvents cbParity As System.Windows.Forms.ComboBox
    Private WithEvents laRtsControl As System.Windows.Forms.Label
    Private WithEvents cbRtsControl As System.Windows.Forms.ComboBox
    Private WithEvents edbaudRate As System.Windows.Forms.TextBox
    Private WithEvents laBaudRate As System.Windows.Forms.Label
    Private WithEvents btSetConfig As System.Windows.Forms.Button
    Private WithEvents btGetConfig As System.Windows.Forms.Button
    Private WithEvents btSetWriteTimeout As System.Windows.Forms.Button
    Private WithEvents edWriteTimeout As System.Windows.Forms.TextBox
    Private WithEvents laWriteTimeout As System.Windows.Forms.Label
    Private WithEvents btDisconnect As System.Windows.Forms.Button
    Private WithEvents btConnect As System.Windows.Forms.Button
    Private WithEvents cbPorts As System.Windows.Forms.ComboBox
    Private WithEvents btEnum As System.Windows.Forms.Button
    Private WithEvents cbLineFeed As System.Windows.Forms.ComboBox
    Private WithEvents laLineFeed As System.Windows.Forms.Label

End Class
