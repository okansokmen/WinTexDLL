Namespace UHFAPP
	Partial Public Class TempertureTagForm
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.rbOnChipRSSI_TempCode_CalibrationData = New System.Windows.Forms.RadioButton()
			Me.label1 = New System.Windows.Forms.Label()
			Me.txtData = New System.Windows.Forms.TextBox()
			Me.rbRssiTempCode = New System.Windows.Forms.RadioButton()
			Me.rbCalibrationData = New System.Windows.Forms.RadioButton()
			Me.rbTempertureCode = New System.Windows.Forms.RadioButton()
			Me.rbRssi = New System.Windows.Forms.RadioButton()
			Me.rbSensorCode = New System.Windows.Forms.RadioButton()
			Me.btnRead = New System.Windows.Forms.Button()
			Me.txtEPC = New System.Windows.Forms.TextBox()
			Me.cmbPower = New System.Windows.Forms.ComboBox()
			Me.cmbAnt = New System.Windows.Forms.ComboBox()
			Me.lblPower = New System.Windows.Forms.Label()
			Me.lblAnt = New System.Windows.Forms.Label()
			Me.lblEPC = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.cmbMode = New System.Windows.Forms.ComboBox()
			Me.cmbPower_2 = New System.Windows.Forms.ComboBox()
			Me.cmbAnt_2 = New System.Windows.Forms.ComboBox()
			Me.label4 = New System.Windows.Forms.Label()
			Me.label14 = New System.Windows.Forms.Label()
			Me.label5 = New System.Windows.Forms.Label()
			Me.lblTotal = New System.Windows.Forms.Label()
			Me.lblTime = New System.Windows.Forms.Label()
			Me.label6 = New System.Windows.Forms.Label()
			Me.label7 = New System.Windows.Forms.Label()
			Me.lvEPC = New System.Windows.Forms.ListView()
			Me.columnHeaderID = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderEPC = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderTID = New System.Windows.Forms.ColumnHeader()
			Me.CalibrationData = New System.Windows.Forms.ColumnHeader()
			Me.SensorCode = New System.Windows.Forms.ColumnHeader()
			Me.RssiCode = New System.Windows.Forms.ColumnHeader()
			Me.TempeCode = New System.Windows.Forms.ColumnHeader()
			Me.columnHeader1 = New System.Windows.Forms.ColumnHeader()
			Me.columnHeader2 = New System.Windows.Forms.ColumnHeader()
			Me.columnHeader3 = New System.Windows.Forms.ColumnHeader()
			Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
			Me.ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.button1 = New System.Windows.Forms.Button()
			Me.btnScanEPC = New System.Windows.Forms.Button()
			Me.label3 = New System.Windows.Forms.Label()
			Me.lto = New System.Windows.Forms.Label()
			Me.groupBox3 = New System.Windows.Forms.GroupBox()
			Me.label13 = New System.Windows.Forms.Label()
			Me.label8 = New System.Windows.Forms.Label()
			Me.txtWData = New System.Windows.Forms.TextBox()
			Me.btnWrite = New System.Windows.Forms.Button()
			Me.txtWEPC = New System.Windows.Forms.TextBox()
			Me.cmbWPower = New System.Windows.Forms.ComboBox()
			Me.cmbWAnt = New System.Windows.Forms.ComboBox()
			Me.label9 = New System.Windows.Forms.Label()
			Me.label10 = New System.Windows.Forms.Label()
			Me.label11 = New System.Windows.Forms.Label()
			Me.label12 = New System.Windows.Forms.Label()
			Me.groupBox1.SuspendLayout()
			Me.groupBox2.SuspendLayout()
			Me.contextMenuStrip1.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			Me.SuspendLayout()
			' 
			' groupBox1
			' 
			Me.groupBox1.Controls.Add(Me.rbOnChipRSSI_TempCode_CalibrationData)
			Me.groupBox1.Controls.Add(Me.label1)
			Me.groupBox1.Controls.Add(Me.txtData)
			Me.groupBox1.Controls.Add(Me.rbRssiTempCode)
			Me.groupBox1.Controls.Add(Me.rbCalibrationData)
			Me.groupBox1.Controls.Add(Me.rbTempertureCode)
			Me.groupBox1.Controls.Add(Me.rbRssi)
			Me.groupBox1.Controls.Add(Me.rbSensorCode)
			Me.groupBox1.Controls.Add(Me.btnRead)
			Me.groupBox1.Controls.Add(Me.txtEPC)
			Me.groupBox1.Controls.Add(Me.cmbPower)
			Me.groupBox1.Controls.Add(Me.cmbAnt)
			Me.groupBox1.Controls.Add(Me.lblPower)
			Me.groupBox1.Controls.Add(Me.lblAnt)
			Me.groupBox1.Controls.Add(Me.lblEPC)
			Me.groupBox1.Controls.Add(Me.label2)
			Me.groupBox1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox1.Location = New System.Drawing.Point(11, -3)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(520, 262)
			Me.groupBox1.TabIndex = 0
			Me.groupBox1.TabStop = False
			' 
			' rbOnChipRSSI_TempCode_CalibrationData
			' 
			Me.rbOnChipRSSI_TempCode_CalibrationData.AutoSize = True
			Me.rbOnChipRSSI_TempCode_CalibrationData.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbOnChipRSSI_TempCode_CalibrationData.Location = New System.Drawing.Point(67, 189)
			Me.rbOnChipRSSI_TempCode_CalibrationData.Name = "rbOnChipRSSI_TempCode_CalibrationData"
			Me.rbOnChipRSSI_TempCode_CalibrationData.Size = New System.Drawing.Size(370, 20)
			Me.rbOnChipRSSI_TempCode_CalibrationData.TabIndex = 30
			Me.rbOnChipRSSI_TempCode_CalibrationData.TabStop = True
			Me.rbOnChipRSSI_TempCode_CalibrationData.Text = "On-Chip RSSI + Temp Code + Calibration Data"
			Me.rbOnChipRSSI_TempCode_CalibrationData.UseVisualStyleBackColor = True
			' 
			' label1
			' 
			Me.label1.Anchor = System.Windows.Forms.AnchorStyles.Top
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label1.ForeColor = System.Drawing.Color.Black
			Me.label1.Location = New System.Drawing.Point(16, 101)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(48, 16)
			Me.label1.TabIndex = 21
			Me.label1.Text = "数据:"
			' 
			' txtData
			' 
			Me.txtData.Anchor = System.Windows.Forms.AnchorStyles.Top
			Me.txtData.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtData.Location = New System.Drawing.Point(65, 95)
			Me.txtData.Multiline = True
			Me.txtData.Name = "txtData"
			Me.txtData.ReadOnly = True
			Me.txtData.Size = New System.Drawing.Size(416, 28)
			Me.txtData.TabIndex = 22
			' 
			' rbRssiTempCode
			' 
			Me.rbRssiTempCode.AutoSize = True
			Me.rbRssiTempCode.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbRssiTempCode.Location = New System.Drawing.Point(261, 148)
			Me.rbRssiTempCode.Name = "rbRssiTempCode"
			Me.rbRssiTempCode.Size = New System.Drawing.Size(218, 20)
			Me.rbRssiTempCode.TabIndex = 28
			Me.rbRssiTempCode.TabStop = True
			Me.rbRssiTempCode.Text = "On-Chip RSSI + Temp Code"
			Me.rbRssiTempCode.UseVisualStyleBackColor = True
			' 
			' rbCalibrationData
			' 
			Me.rbCalibrationData.AutoSize = True
			Me.rbCalibrationData.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbCalibrationData.Location = New System.Drawing.Point(67, 148)
			Me.rbCalibrationData.Name = "rbCalibrationData"
			Me.rbCalibrationData.Size = New System.Drawing.Size(154, 20)
			Me.rbCalibrationData.TabIndex = 27
			Me.rbCalibrationData.TabStop = True
			Me.rbCalibrationData.Text = "Calibration Data"
			Me.rbCalibrationData.UseVisualStyleBackColor = True
			' 
			' rbTempertureCode
			' 
			Me.rbTempertureCode.AutoSize = True
			Me.rbTempertureCode.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbTempertureCode.Location = New System.Drawing.Point(261, 128)
			Me.rbTempertureCode.Name = "rbTempertureCode"
			Me.rbTempertureCode.Size = New System.Drawing.Size(146, 20)
			Me.rbTempertureCode.TabIndex = 26
			Me.rbTempertureCode.TabStop = True
			Me.rbTempertureCode.Text = "Temperture Code"
			Me.rbTempertureCode.UseVisualStyleBackColor = True
			' 
			' rbRssi
			' 
			Me.rbRssi.AutoSize = True
			Me.rbRssi.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbRssi.Location = New System.Drawing.Point(67, 167)
			Me.rbRssi.Name = "rbRssi"
			Me.rbRssi.Size = New System.Drawing.Size(122, 20)
			Me.rbRssi.TabIndex = 25
			Me.rbRssi.TabStop = True
			Me.rbRssi.Text = "On-Chip RSSI"
			Me.rbRssi.UseVisualStyleBackColor = True
			' 
			' rbSensorCode
			' 
			Me.rbSensorCode.AutoSize = True
			Me.rbSensorCode.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbSensorCode.Location = New System.Drawing.Point(67, 128)
			Me.rbSensorCode.Name = "rbSensorCode"
			Me.rbSensorCode.Size = New System.Drawing.Size(114, 20)
			Me.rbSensorCode.TabIndex = 24
			Me.rbSensorCode.TabStop = True
			Me.rbSensorCode.Text = "Sensor Code"
			Me.rbSensorCode.UseVisualStyleBackColor = True
			' 
			' btnRead
			' 
			Me.btnRead.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnRead.Location = New System.Drawing.Point(59, 219)
			Me.btnRead.Name = "btnRead"
			Me.btnRead.Size = New System.Drawing.Size(122, 34)
			Me.btnRead.TabIndex = 23
			Me.btnRead.Text = "读取"
			Me.btnRead.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
			' 
			' txtEPC
			' 
			Me.txtEPC.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtEPC.Location = New System.Drawing.Point(66, 20)
			Me.txtEPC.Multiline = True
			Me.txtEPC.Name = "txtEPC"
			Me.txtEPC.Size = New System.Drawing.Size(361, 43)
			Me.txtEPC.TabIndex = 20
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtEPC.TextChanged += new System.EventHandler(this.txtEPC_TextChanged);
			' 
			' cmbPower
			' 
			Me.cmbPower.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbPower.FormattingEnabled = True
			Me.cmbPower.Items.AddRange(New Object() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"})
			Me.cmbPower.Location = New System.Drawing.Point(205, 66)
			Me.cmbPower.Name = "cmbPower"
			Me.cmbPower.Size = New System.Drawing.Size(70, 24)
			Me.cmbPower.TabIndex = 19
			' 
			' cmbAnt
			' 
			Me.cmbAnt.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt.FormattingEnabled = True
			Me.cmbAnt.Items.AddRange(New Object() { "1", "2", "3", "4", "5", "6", "7", "8"})
			Me.cmbAnt.Location = New System.Drawing.Point(65, 66)
			Me.cmbAnt.Name = "cmbAnt"
			Me.cmbAnt.Size = New System.Drawing.Size(64, 24)
			Me.cmbAnt.TabIndex = 18
			' 
			' lblPower
			' 
			Me.lblPower.AutoSize = True
			Me.lblPower.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lblPower.ForeColor = System.Drawing.Color.Black
			Me.lblPower.Location = New System.Drawing.Point(135, 70)
			Me.lblPower.Name = "lblPower"
			Me.lblPower.Size = New System.Drawing.Size(64, 16)
			Me.lblPower.TabIndex = 17
			Me.lblPower.Text = "功率值:"
			' 
			' lblAnt
			' 
			Me.lblAnt.AutoSize = True
			Me.lblAnt.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lblAnt.ForeColor = System.Drawing.Color.Black
			Me.lblAnt.Location = New System.Drawing.Point(5, 70)
			Me.lblAnt.Name = "lblAnt"
			Me.lblAnt.Size = New System.Drawing.Size(64, 16)
			Me.lblAnt.TabIndex = 16
			Me.lblAnt.Text = "天线号:"
			' 
			' lblEPC
			' 
			Me.lblEPC.AutoSize = True
			Me.lblEPC.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lblEPC.ForeColor = System.Drawing.Color.Black
			Me.lblEPC.Location = New System.Drawing.Point(17, 22)
			Me.lblEPC.Name = "lblEPC"
			Me.lblEPC.Size = New System.Drawing.Size(40, 16)
			Me.lblEPC.TabIndex = 15
			Me.lblEPC.Text = "EPC:"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label2.Location = New System.Drawing.Point(429, 25)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(32, 16)
			Me.label2.TabIndex = 29
			Me.label2.Text = "len"
			' 
			' groupBox2
			' 
			Me.groupBox2.Controls.Add(Me.cmbMode)
			Me.groupBox2.Controls.Add(Me.cmbPower_2)
			Me.groupBox2.Controls.Add(Me.cmbAnt_2)
			Me.groupBox2.Controls.Add(Me.label4)
			Me.groupBox2.Controls.Add(Me.label14)
			Me.groupBox2.Controls.Add(Me.label5)
			Me.groupBox2.Controls.Add(Me.lblTotal)
			Me.groupBox2.Controls.Add(Me.lblTime)
			Me.groupBox2.Controls.Add(Me.label6)
			Me.groupBox2.Controls.Add(Me.label7)
			Me.groupBox2.Controls.Add(Me.lvEPC)
			Me.groupBox2.Controls.Add(Me.button1)
			Me.groupBox2.Controls.Add(Me.btnScanEPC)
			Me.groupBox2.Controls.Add(Me.label3)
			Me.groupBox2.Controls.Add(Me.lto)
			Me.groupBox2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox2.Location = New System.Drawing.Point(12, 260)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(1257, 357)
			Me.groupBox2.TabIndex = 1
			Me.groupBox2.TabStop = False
			' 
			' cmbMode
			' 
			Me.cmbMode.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbMode.FormattingEnabled = True
			Me.cmbMode.Items.AddRange(New Object() { "CalibrationData+SensorCode+ On-ChipRSSI+TempeCode", "On-ChipRSSI+ TempeCode", "SensorCode"})
			Me.cmbMode.Location = New System.Drawing.Point(74, 250)
			Me.cmbMode.Name = "cmbMode"
			Me.cmbMode.Size = New System.Drawing.Size(423, 24)
			Me.cmbMode.TabIndex = 53
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cmbMode.SelectedValueChanged += new System.EventHandler(this.cmbMode_SelectedValueChanged);
			' 
			' cmbPower_2
			' 
			Me.cmbPower_2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbPower_2.FormattingEnabled = True
			Me.cmbPower_2.Items.AddRange(New Object() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"})
			Me.cmbPower_2.Location = New System.Drawing.Point(828, 249)
			Me.cmbPower_2.Name = "cmbPower_2"
			Me.cmbPower_2.Size = New System.Drawing.Size(128, 24)
			Me.cmbPower_2.TabIndex = 51
			' 
			' cmbAnt_2
			' 
			Me.cmbAnt_2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbAnt_2.FormattingEnabled = True
			Me.cmbAnt_2.Items.AddRange(New Object() { "1", "2", "3", "4", "5", "6", "7", "8"})
			Me.cmbAnt_2.Location = New System.Drawing.Point(589, 249)
			Me.cmbAnt_2.Name = "cmbAnt_2"
			Me.cmbAnt_2.Size = New System.Drawing.Size(147, 24)
			Me.cmbAnt_2.TabIndex = 50
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label4.ForeColor = System.Drawing.Color.Black
			Me.label4.Location = New System.Drawing.Point(758, 254)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(64, 16)
			Me.label4.TabIndex = 49
			Me.label4.Text = "功率值:"
			' 
			' label14
			' 
			Me.label14.AutoSize = True
			Me.label14.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label14.Location = New System.Drawing.Point(20, 253)
			Me.label14.Name = "label14"
			Me.label14.Size = New System.Drawing.Size(48, 16)
			Me.label14.TabIndex = 52
			Me.label14.Text = "模式:"
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label5.ForeColor = System.Drawing.Color.Black
			Me.label5.Location = New System.Drawing.Point(519, 255)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(64, 16)
			Me.label5.TabIndex = 48
			Me.label5.Text = "天线号:"
			' 
			' lblTotal
			' 
			Me.lblTotal.AutoSize = True
			Me.lblTotal.Font = New System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold)
			Me.lblTotal.Location = New System.Drawing.Point(230, 290)
			Me.lblTotal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
			Me.lblTotal.Name = "lblTotal"
			Me.lblTotal.Size = New System.Drawing.Size(23, 24)
			Me.lblTotal.TabIndex = 47
			Me.lblTotal.Text = "0"
			' 
			' lblTime
			' 
			Me.lblTime.AutoSize = True
			Me.lblTime.Font = New System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold)
			Me.lblTime.Location = New System.Drawing.Point(356, 324)
			Me.lblTime.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
			Me.lblTime.Name = "lblTime"
			Me.lblTime.Size = New System.Drawing.Size(23, 24)
			Me.lblTime.TabIndex = 46
			Me.lblTime.Text = "0"
			' 
			' label6
			' 
			Me.label6.AutoSize = True
			Me.label6.BackColor = System.Drawing.Color.Transparent
			Me.label6.Font = New System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label6.ForeColor = System.Drawing.Color.FromArgb(CInt(CByte(0)), CInt(CByte(64)), CInt(CByte(0)))
			Me.label6.Location = New System.Drawing.Point(360, 293)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(23, 24)
			Me.label6.TabIndex = 45
			Me.label6.Text = "0"
			' 
			' label7
			' 
			Me.label7.AutoSize = True
			Me.label7.BackColor = System.Drawing.Color.Transparent
			Me.label7.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label7.ForeColor = System.Drawing.Color.Black
			Me.label7.Location = New System.Drawing.Point(285, 295)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(60, 19)
			Me.label7.TabIndex = 44
			Me.label7.Text = "次数:"
			' 
			' lvEPC
			' 
			Me.lvEPC.BackColor = System.Drawing.SystemColors.ControlLightLight
			Me.lvEPC.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.columnHeaderID, Me.columnHeaderEPC, Me.columnHeaderTID, Me.CalibrationData, Me.SensorCode, Me.RssiCode, Me.TempeCode, Me.columnHeader1, Me.columnHeader2, Me.columnHeader3})
			Me.lvEPC.ContextMenuStrip = Me.contextMenuStrip1
			Me.lvEPC.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lvEPC.FullRowSelect = True
			Me.lvEPC.Location = New System.Drawing.Point(8, 20)
			Me.lvEPC.Name = "lvEPC"
			Me.lvEPC.Size = New System.Drawing.Size(1240, 219)
			Me.lvEPC.TabIndex = 39
			Me.lvEPC.UseCompatibleStateImageBehavior = False
			Me.lvEPC.View = System.Windows.Forms.View.Details
			' 
			' columnHeaderID
			' 
			Me.columnHeaderID.Text = "ID"
			Me.columnHeaderID.Width = 77
			' 
			' columnHeaderEPC
			' 
			Me.columnHeaderEPC.Text = "EPC"
			Me.columnHeaderEPC.Width = 490
			' 
			' columnHeaderTID
			' 
			Me.columnHeaderTID.Text = "TID"
			Me.columnHeaderTID.Width = 0
			' 
			' CalibrationData
			' 
			Me.CalibrationData.Text = "CalibrationData"
			Me.CalibrationData.Width = 210
			' 
			' SensorCode
			' 
			Me.SensorCode.Text = "SensorCode"
			Me.SensorCode.Width = 100
			' 
			' RssiCode
			' 
			Me.RssiCode.Text = "RssiCode"
			Me.RssiCode.Width = 90
			' 
			' TempeCode
			' 
			Me.TempeCode.Text = "TempeCode"
			Me.TempeCode.Width = 90
			' 
			' columnHeader1
			' 
			Me.columnHeader1.Text = "RSSI"
			Me.columnHeader1.Width = 70
			' 
			' columnHeader2
			' 
			Me.columnHeader2.Text = "Count"
			Me.columnHeader2.Width = 70
			' 
			' columnHeader3
			' 
			Me.columnHeader3.Text = "ANT"
			Me.columnHeader3.Width = 50
			' 
			' contextMenuStrip1
			' 
			Me.contextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
			Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.ToolStripMenuItem})
			Me.contextMenuStrip1.Name = "contextMenuStrip1"
			Me.contextMenuStrip1.ShowCheckMargin = True
			Me.contextMenuStrip1.Size = New System.Drawing.Size(147, 26)
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.contextMenuStrip1.Click += new System.EventHandler(this.contextMenuStrip1_Click);
			' 
			' ToolStripMenuItem
			' 
			Me.ToolStripMenuItem.Name = "ToolStripMenuItem"
			Me.ToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
			Me.ToolStripMenuItem.Text = "复制标签"
			' 
			' button1
			' 
			Me.button1.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button1.Location = New System.Drawing.Point(685, 295)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(91, 48)
			Me.button1.TabIndex = 42
			Me.button1.Text = "Clear"
			Me.button1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button1.Click += new System.EventHandler(this.button1_Click);
			' 
			' btnScanEPC
			' 
			Me.btnScanEPC.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnScanEPC.Location = New System.Drawing.Point(555, 295)
			Me.btnScanEPC.Name = "btnScanEPC"
			Me.btnScanEPC.Size = New System.Drawing.Size(93, 48)
			Me.btnScanEPC.TabIndex = 38
			Me.btnScanEPC.Text = "Start"
			Me.btnScanEPC.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnScanEPC.Click += new System.EventHandler(this.btnScanEPC_Click);
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.BackColor = System.Drawing.Color.Transparent
			Me.label3.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label3.ForeColor = System.Drawing.Color.Black
			Me.label3.Location = New System.Drawing.Point(281, 324)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(64, 19)
			Me.label3.TabIndex = 41
			Me.label3.Text = "Time:"
			' 
			' lto
			' 
			Me.lto.AutoSize = True
			Me.lto.BackColor = System.Drawing.Color.Transparent
			Me.lto.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lto.ForeColor = System.Drawing.Color.Black
			Me.lto.Location = New System.Drawing.Point(142, 290)
			Me.lto.Name = "lto"
			Me.lto.Size = New System.Drawing.Size(75, 19)
			Me.lto.TabIndex = 40
			Me.lto.Text = "Total:"
			' 
			' groupBox3
			' 
			Me.groupBox3.Controls.Add(Me.label13)
			Me.groupBox3.Controls.Add(Me.label8)
			Me.groupBox3.Controls.Add(Me.txtWData)
			Me.groupBox3.Controls.Add(Me.btnWrite)
			Me.groupBox3.Controls.Add(Me.txtWEPC)
			Me.groupBox3.Controls.Add(Me.cmbWPower)
			Me.groupBox3.Controls.Add(Me.cmbWAnt)
			Me.groupBox3.Controls.Add(Me.label9)
			Me.groupBox3.Controls.Add(Me.label10)
			Me.groupBox3.Controls.Add(Me.label11)
			Me.groupBox3.Controls.Add(Me.label12)
			Me.groupBox3.Location = New System.Drawing.Point(567, -2)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(693, 262)
			Me.groupBox3.TabIndex = 2
			Me.groupBox3.TabStop = False
			' 
			' label13
			' 
			Me.label13.AutoSize = True
			Me.label13.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label13.Location = New System.Drawing.Point(486, 138)
			Me.label13.Name = "label13"
			Me.label13.Size = New System.Drawing.Size(32, 16)
			Me.label13.TabIndex = 40
			Me.label13.Text = "len"
			' 
			' label8
			' 
			Me.label8.Anchor = System.Windows.Forms.AnchorStyles.Top
			Me.label8.AutoSize = True
			Me.label8.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label8.ForeColor = System.Drawing.Color.Black
			Me.label8.Location = New System.Drawing.Point(15, 116)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(48, 16)
			Me.label8.TabIndex = 36
			Me.label8.Text = "数据:"
			' 
			' txtWData
			' 
			Me.txtWData.Anchor = System.Windows.Forms.AnchorStyles.Top
			Me.txtWData.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtWData.Location = New System.Drawing.Point(64, 110)
			Me.txtWData.Multiline = True
			Me.txtWData.Name = "txtWData"
			Me.txtWData.Size = New System.Drawing.Size(416, 48)
			Me.txtWData.TabIndex = 37
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtWData.TextChanged += new System.EventHandler(this.txtWData_TextChanged);
			' 
			' btnWrite
			' 
			Me.btnWrite.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnWrite.Location = New System.Drawing.Point(130, 218)
			Me.btnWrite.Name = "btnWrite"
			Me.btnWrite.Size = New System.Drawing.Size(122, 34)
			Me.btnWrite.TabIndex = 38
			Me.btnWrite.Text = "写"
			Me.btnWrite.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
			' 
			' txtWEPC
			' 
			Me.txtWEPC.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtWEPC.Location = New System.Drawing.Point(64, 20)
			Me.txtWEPC.Multiline = True
			Me.txtWEPC.Name = "txtWEPC"
			Me.txtWEPC.Size = New System.Drawing.Size(361, 43)
			Me.txtWEPC.TabIndex = 35
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtWEPC.TextChanged += new System.EventHandler(this.txtWEPC_TextChanged);
			' 
			' cmbWPower
			' 
			Me.cmbWPower.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbWPower.FormattingEnabled = True
			Me.cmbWPower.Items.AddRange(New Object() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"})
			Me.cmbWPower.Location = New System.Drawing.Point(206, 77)
			Me.cmbWPower.Name = "cmbWPower"
			Me.cmbWPower.Size = New System.Drawing.Size(70, 24)
			Me.cmbWPower.TabIndex = 34
			' 
			' cmbWAnt
			' 
			Me.cmbWAnt.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbWAnt.FormattingEnabled = True
			Me.cmbWAnt.Items.AddRange(New Object() { "1", "2", "3", "4", "5", "6", "7", "8"})
			Me.cmbWAnt.Location = New System.Drawing.Point(66, 77)
			Me.cmbWAnt.Name = "cmbWAnt"
			Me.cmbWAnt.Size = New System.Drawing.Size(64, 24)
			Me.cmbWAnt.TabIndex = 33
			' 
			' label9
			' 
			Me.label9.AutoSize = True
			Me.label9.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label9.ForeColor = System.Drawing.Color.Black
			Me.label9.Location = New System.Drawing.Point(140, 81)
			Me.label9.Name = "label9"
			Me.label9.Size = New System.Drawing.Size(64, 16)
			Me.label9.TabIndex = 32
			Me.label9.Text = "功率值:"
			' 
			' label10
			' 
			Me.label10.AutoSize = True
			Me.label10.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label10.ForeColor = System.Drawing.Color.Black
			Me.label10.Location = New System.Drawing.Point(6, 81)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(64, 16)
			Me.label10.TabIndex = 31
			Me.label10.Text = "天线号:"
			' 
			' label11
			' 
			Me.label11.AutoSize = True
			Me.label11.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label11.ForeColor = System.Drawing.Color.Black
			Me.label11.Location = New System.Drawing.Point(18, 25)
			Me.label11.Name = "label11"
			Me.label11.Size = New System.Drawing.Size(40, 16)
			Me.label11.TabIndex = 30
			Me.label11.Text = "EPC:"
			' 
			' label12
			' 
			Me.label12.AutoSize = True
			Me.label12.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label12.Location = New System.Drawing.Point(431, 46)
			Me.label12.Name = "label12"
			Me.label12.Size = New System.Drawing.Size(32, 16)
			Me.label12.TabIndex = 39
			Me.label12.Text = "len"
			' 
			' TempertureTagForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.ClientSize = New System.Drawing.Size(1364, 903)
			Me.Controls.Add(Me.groupBox3)
			Me.Controls.Add(Me.groupBox2)
			Me.Controls.Add(Me.groupBox1)
			Me.KeyPreview = True
			Me.Name = "TempertureTagForm"
			Me.Text = "TempertureTagForm"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.ConfigForm2_Load);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigForm2_FormClosed);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConfigForm2_KeyDown);
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.contextMenuStrip1.ResumeLayout(False)
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox3.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private groupBox1 As System.Windows.Forms.GroupBox
		Private lblPower As System.Windows.Forms.Label
		Private lblAnt As System.Windows.Forms.Label
		Private lblEPC As System.Windows.Forms.Label
		Private label1 As System.Windows.Forms.Label
		Private WithEvents txtEPC As System.Windows.Forms.TextBox
		Private cmbPower As System.Windows.Forms.ComboBox
		Private cmbAnt As System.Windows.Forms.ComboBox
		Private WithEvents btnRead As System.Windows.Forms.Button
		Private txtData As System.Windows.Forms.TextBox
		Private rbRssiTempCode As System.Windows.Forms.RadioButton
		Private rbCalibrationData As System.Windows.Forms.RadioButton
		Private rbTempertureCode As System.Windows.Forms.RadioButton
		Private rbRssi As System.Windows.Forms.RadioButton
		Private rbSensorCode As System.Windows.Forms.RadioButton
		Private label2 As System.Windows.Forms.Label
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private lblTotal As System.Windows.Forms.Label
		Private lblTime As System.Windows.Forms.Label
		Private label6 As System.Windows.Forms.Label
		Private label7 As System.Windows.Forms.Label
		Private lvEPC As System.Windows.Forms.ListView
		Private columnHeaderID As System.Windows.Forms.ColumnHeader
		Private columnHeaderEPC As System.Windows.Forms.ColumnHeader
		Private columnHeaderTID As System.Windows.Forms.ColumnHeader
		Private WithEvents button1 As System.Windows.Forms.Button
		Private WithEvents btnScanEPC As System.Windows.Forms.Button
		Private label3 As System.Windows.Forms.Label
		Private lto As System.Windows.Forms.Label
		Private cmbPower_2 As System.Windows.Forms.ComboBox
		Private cmbAnt_2 As System.Windows.Forms.ComboBox
		Private label4 As System.Windows.Forms.Label
		Private label5 As System.Windows.Forms.Label
		Private CalibrationData As System.Windows.Forms.ColumnHeader
		Private SensorCode As System.Windows.Forms.ColumnHeader
		Private RssiCode As System.Windows.Forms.ColumnHeader
		Private TempeCode As System.Windows.Forms.ColumnHeader
		Private columnHeader1 As System.Windows.Forms.ColumnHeader
		Private columnHeader2 As System.Windows.Forms.ColumnHeader
		Private columnHeader3 As System.Windows.Forms.ColumnHeader
		Private WithEvents contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
		Private ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private groupBox3 As System.Windows.Forms.GroupBox
		Private label8 As System.Windows.Forms.Label
		Private WithEvents txtWData As System.Windows.Forms.TextBox
		Private WithEvents btnWrite As System.Windows.Forms.Button
		Private WithEvents txtWEPC As System.Windows.Forms.TextBox
		Private cmbWPower As System.Windows.Forms.ComboBox
		Private cmbWAnt As System.Windows.Forms.ComboBox
		Private label9 As System.Windows.Forms.Label
		Private label10 As System.Windows.Forms.Label
		Private label11 As System.Windows.Forms.Label
		Private label12 As System.Windows.Forms.Label
		Private label13 As System.Windows.Forms.Label
		Private WithEvents cmbMode As System.Windows.Forms.ComboBox
		Private label14 As System.Windows.Forms.Label
		Private rbOnChipRSSI_TempCode_CalibrationData As System.Windows.Forms.RadioButton
	End Class
End Namespace
