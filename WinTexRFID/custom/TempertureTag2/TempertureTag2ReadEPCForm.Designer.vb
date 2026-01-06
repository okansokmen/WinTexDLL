Namespace UHFAPP
	Partial Public Class TempertureTag2ReadEPCForm
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
			Me.btnScanEPC = New System.Windows.Forms.Button()
			Me.lvEPC = New System.Windows.Forms.ListView()
			Me.columnHeaderID = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderEPC = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderTID = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderTemperature = New System.Windows.Forms.ColumnHeader()
			Me.columnHeader1 = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderRssi = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderCount = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderANT = New System.Windows.Forms.ColumnHeader()
			Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
			Me.qqqqqqqqqqqToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.lto = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.button1 = New System.Windows.Forms.Button()
			Me.groupBox8 = New System.Windows.Forms.GroupBox()
			Me.txtData = New System.Windows.Forms.TextBox()
			Me.label5 = New System.Windows.Forms.Label()
			Me.txtfilerLen = New System.Windows.Forms.TextBox()
			Me.txtPtr = New System.Windows.Forms.TextBox()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label1 = New System.Windows.Forms.Label()
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.rbUser = New System.Windows.Forms.RadioButton()
			Me.rbEPC = New System.Windows.Forms.RadioButton()
			Me.rbTID = New System.Windows.Forms.RadioButton()
			Me.label29 = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.label30 = New System.Windows.Forms.Label()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.groupBox7 = New System.Windows.Forms.GroupBox()
			Me.label22 = New System.Windows.Forms.Label()
			Me.label21 = New System.Windows.Forms.Label()
			Me.label20 = New System.Windows.Forms.Label()
			Me.txtNumber = New System.Windows.Forms.TextBox()
			Me.label19 = New System.Windows.Forms.Label()
			Me.txtStart = New System.Windows.Forms.TextBox()
			Me.label18 = New System.Windows.Forms.Label()
			Me.button7 = New System.Windows.Forms.Button()
			Me.groupBox6 = New System.Windows.Forms.GroupBox()
			Me.label17 = New System.Windows.Forms.Label()
			Me.btnVoltage = New System.Windows.Forms.Button()
			Me.groupBox5 = New System.Windows.Forms.GroupBox()
			Me.button6 = New System.Windows.Forms.Button()
			Me.gbInventoryMode = New System.Windows.Forms.GroupBox()
			Me.cbInventoryMode = New System.Windows.Forms.ComboBox()
			Me.label45 = New System.Windows.Forms.Label()
			Me.button10 = New System.Windows.Forms.Button()
			Me.button11 = New System.Windows.Forms.Button()
			Me.groupBox4 = New System.Windows.Forms.GroupBox()
			Me.label39 = New System.Windows.Forms.Label()
			Me.button4 = New System.Windows.Forms.Button()
			Me.button5 = New System.Windows.Forms.Button()
			Me.groupBox3 = New System.Windows.Forms.GroupBox()
			Me.txtPwd = New System.Windows.Forms.TextBox()
			Me.label11 = New System.Windows.Forms.Label()
			Me.button2 = New System.Windows.Forms.Button()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.label16 = New System.Windows.Forms.Label()
			Me.label15 = New System.Windows.Forms.Label()
			Me.label14 = New System.Windows.Forms.Label()
			Me.label10 = New System.Windows.Forms.Label()
			Me.button3 = New System.Windows.Forms.Button()
			Me.txtinterval = New System.Windows.Forms.TextBox()
			Me.label12 = New System.Windows.Forms.Label()
			Me.txtdelay = New System.Windows.Forms.TextBox()
			Me.label13 = New System.Windows.Forms.Label()
			Me.txtMax = New System.Windows.Forms.TextBox()
			Me.label9 = New System.Windows.Forms.Label()
			Me.txtMin = New System.Windows.Forms.TextBox()
			Me.label8 = New System.Windows.Forms.Label()
			Me.lblTotal = New System.Windows.Forms.Label()
			Me.lblTime = New System.Windows.Forms.Label()
			Me.label6 = New System.Windows.Forms.Label()
			Me.label7 = New System.Windows.Forms.Label()
			Me.contextMenuStrip1.SuspendLayout()
			Me.groupBox8.SuspendLayout()
			Me.groupBox1.SuspendLayout()
			Me.panel1.SuspendLayout()
			Me.groupBox7.SuspendLayout()
			Me.groupBox6.SuspendLayout()
			Me.groupBox5.SuspendLayout()
			Me.gbInventoryMode.SuspendLayout()
			Me.groupBox4.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			Me.groupBox2.SuspendLayout()
			Me.SuspendLayout()
			' 
			' btnScanEPC
			' 
			Me.btnScanEPC.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnScanEPC.Location = New System.Drawing.Point(240, 513)
			Me.btnScanEPC.Name = "btnScanEPC"
			Me.btnScanEPC.Size = New System.Drawing.Size(154, 48)
			Me.btnScanEPC.TabIndex = 0
			Me.btnScanEPC.Text = "盘点温度"
			Me.btnScanEPC.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnScanEPC.Click += new System.EventHandler(this.btnScanEPC_Click);
			' 
			' lvEPC
			' 
			Me.lvEPC.BackColor = System.Drawing.SystemColors.ControlLightLight
			Me.lvEPC.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.columnHeaderID, Me.columnHeaderEPC, Me.columnHeaderTID, Me.columnHeaderTemperature, Me.columnHeader1, Me.columnHeaderRssi, Me.columnHeaderCount, Me.columnHeaderANT})
			Me.lvEPC.ContextMenuStrip = Me.contextMenuStrip1
			Me.lvEPC.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lvEPC.FullRowSelect = True
			Me.lvEPC.Location = New System.Drawing.Point(3, 10)
			Me.lvEPC.Name = "lvEPC"
			Me.lvEPC.Size = New System.Drawing.Size(708, 412)
			Me.lvEPC.TabIndex = 2
			Me.lvEPC.UseCompatibleStateImageBehavior = False
			Me.lvEPC.View = System.Windows.Forms.View.Details
			' 
			' columnHeaderID
			' 
			Me.columnHeaderID.Text = "ID"
			Me.columnHeaderID.Width = 28
			' 
			' columnHeaderEPC
			' 
			Me.columnHeaderEPC.Text = "EPC"
			Me.columnHeaderEPC.Width = 213
			' 
			' columnHeaderTID
			' 
			Me.columnHeaderTID.Text = "TID"
			Me.columnHeaderTID.Width = 213
			' 
			' columnHeaderTemperature
			' 
			Me.columnHeaderTemperature.Text = "温度"
			Me.columnHeaderTemperature.Width = 55
			' 
			' columnHeader1
			' 
			Me.columnHeader1.Text = "温度测量次数"
			Me.columnHeader1.Width = 108
			' 
			' columnHeaderRssi
			' 
			Me.columnHeaderRssi.Text = "Rssi"
			' 
			' columnHeaderCount
			' 
			Me.columnHeaderCount.Text = "Count"
			' 
			' columnHeaderANT
			' 
			Me.columnHeaderANT.Text = "ANT"
			Me.columnHeaderANT.Width = 40
			' 
			' contextMenuStrip1
			' 
			Me.contextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
			Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.qqqqqqqqqqqToolStripMenuItem})
			Me.contextMenuStrip1.Name = "contextMenuStrip1"
			Me.contextMenuStrip1.ShowCheckMargin = True
			Me.contextMenuStrip1.Size = New System.Drawing.Size(187, 26)
			' 
			' qqqqqqqqqqqToolStripMenuItem
			' 
			Me.qqqqqqqqqqqToolStripMenuItem.Name = "qqqqqqqqqqqToolStripMenuItem"
			Me.qqqqqqqqqqqToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
			Me.qqqqqqqqqqqToolStripMenuItem.Text = "qqqqqqqqqqq"
			' 
			' lto
			' 
			Me.lto.AutoSize = True
			Me.lto.BackColor = System.Drawing.Color.Transparent
			Me.lto.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lto.ForeColor = System.Drawing.Color.Black
			Me.lto.Location = New System.Drawing.Point(24, 467)
			Me.lto.Name = "lto"
			Me.lto.Size = New System.Drawing.Size(75, 19)
			Me.lto.TabIndex = 3
			Me.lto.Text = "Total:"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.BackColor = System.Drawing.Color.Transparent
			Me.label2.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label2.ForeColor = System.Drawing.Color.Black
			Me.label2.Location = New System.Drawing.Point(24, 565)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(64, 19)
			Me.label2.TabIndex = 4
			Me.label2.Text = "Time:"
			' 
			' button1
			' 
			Me.button1.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button1.Location = New System.Drawing.Point(445, 513)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(89, 45)
			Me.button1.TabIndex = 24
			Me.button1.Text = "清空"
			Me.button1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button1.Click += new System.EventHandler(this.button1_Click);
			' 
			' groupBox8
			' 
			Me.groupBox8.BackColor = System.Drawing.Color.Transparent
			Me.groupBox8.Controls.Add(Me.txtData)
			Me.groupBox8.Controls.Add(Me.label5)
			Me.groupBox8.Controls.Add(Me.txtfilerLen)
			Me.groupBox8.Controls.Add(Me.txtPtr)
			Me.groupBox8.Controls.Add(Me.label3)
			Me.groupBox8.Controls.Add(Me.label1)
			Me.groupBox8.Controls.Add(Me.groupBox1)
			Me.groupBox8.Controls.Add(Me.label29)
			Me.groupBox8.Controls.Add(Me.label4)
			Me.groupBox8.Controls.Add(Me.label30)
			Me.groupBox8.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox8.ForeColor = System.Drawing.Color.Black
			Me.groupBox8.Location = New System.Drawing.Point(717, 3)
			Me.groupBox8.Name = "groupBox8"
			Me.groupBox8.Size = New System.Drawing.Size(572, 117)
			Me.groupBox8.TabIndex = 30
			Me.groupBox8.TabStop = False
			Me.groupBox8.Text = "Filter"
			' 
			' txtData
			' 
			Me.txtData.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtData.Location = New System.Drawing.Point(46, 20)
			Me.txtData.Multiline = True
			Me.txtData.Name = "txtData"
			Me.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtData.Size = New System.Drawing.Size(360, 41)
			Me.txtData.TabIndex = 7
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtData.TextChanged += new System.EventHandler(this.txtData_TextChanged);
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label5.ForeColor = System.Drawing.Color.Black
			Me.label5.Location = New System.Drawing.Point(407, 30)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(16, 16)
			Me.label5.TabIndex = 38
			Me.label5.Text = "0"
			' 
			' txtfilerLen
			' 
			Me.txtfilerLen.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtfilerLen.Location = New System.Drawing.Point(481, 41)
			Me.txtfilerLen.MaxLength = 4
			Me.txtfilerLen.Name = "txtfilerLen"
			Me.txtfilerLen.Size = New System.Drawing.Size(36, 26)
			Me.txtfilerLen.TabIndex = 34
			Me.txtfilerLen.Tag = "Number"
			Me.txtfilerLen.Text = "0"
			' 
			' txtPtr
			' 
			Me.txtPtr.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtPtr.Location = New System.Drawing.Point(482, 12)
			Me.txtPtr.MaxLength = 4
			Me.txtPtr.Name = "txtPtr"
			Me.txtPtr.Size = New System.Drawing.Size(36, 26)
			Me.txtPtr.TabIndex = 6
			Me.txtPtr.Tag = "Number"
			Me.txtPtr.Text = "32"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtPtr.TextChanged += new System.EventHandler(this.txtPtr_TextChanged);
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label3.Location = New System.Drawing.Point(513, 19)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(42, 14)
			Me.label3.TabIndex = 35
			Me.label3.Text = "(bit)"
			Me.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label1.Location = New System.Drawing.Point(449, 44)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(40, 16)
			Me.label1.TabIndex = 33
			Me.label1.Text = "Len:"
			Me.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight
			' 
			' groupBox1
			' 
			Me.groupBox1.Controls.Add(Me.rbUser)
			Me.groupBox1.Controls.Add(Me.rbEPC)
			Me.groupBox1.Controls.Add(Me.rbTID)
			Me.groupBox1.Location = New System.Drawing.Point(9, 64)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(534, 47)
			Me.groupBox1.TabIndex = 31
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "bank"
			' 
			' rbUser
			' 
			Me.rbUser.AutoSize = True
			Me.rbUser.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbUser.Location = New System.Drawing.Point(118, 20)
			Me.rbUser.Name = "rbUser"
			Me.rbUser.Size = New System.Drawing.Size(58, 20)
			Me.rbUser.TabIndex = 12
			Me.rbUser.Text = "User"
			Me.rbUser.UseVisualStyleBackColor = True
			' 
			' rbEPC
			' 
			Me.rbEPC.AutoSize = True
			Me.rbEPC.Checked = True
			Me.rbEPC.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbEPC.Location = New System.Drawing.Point(6, 19)
			Me.rbEPC.Name = "rbEPC"
			Me.rbEPC.Size = New System.Drawing.Size(50, 20)
			Me.rbEPC.TabIndex = 8
			Me.rbEPC.TabStop = True
			Me.rbEPC.Text = "EPC"
			Me.rbEPC.UseVisualStyleBackColor = True
			' 
			' rbTID
			' 
			Me.rbTID.AutoSize = True
			Me.rbTID.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbTID.Location = New System.Drawing.Point(62, 20)
			Me.rbTID.Name = "rbTID"
			Me.rbTID.Size = New System.Drawing.Size(50, 20)
			Me.rbTID.TabIndex = 9
			Me.rbTID.Text = "TID"
			Me.rbTID.UseVisualStyleBackColor = True
			' 
			' label29
			' 
			Me.label29.AutoSize = True
			Me.label29.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label29.Location = New System.Drawing.Point(2, 30)
			Me.label29.Name = "label29"
			Me.label29.Size = New System.Drawing.Size(48, 16)
			Me.label29.TabIndex = 5
			Me.label29.Text = "Data:"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label4.Location = New System.Drawing.Point(513, 47)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(42, 14)
			Me.label4.TabIndex = 36
			Me.label4.Text = "(bit)"
			Me.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight
			' 
			' label30
			' 
			Me.label30.AutoSize = True
			Me.label30.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label30.Location = New System.Drawing.Point(449, 15)
			Me.label30.Name = "label30"
			Me.label30.Size = New System.Drawing.Size(40, 16)
			Me.label30.TabIndex = 4
			Me.label30.Text = "Ptr:"
			' 
			' panel1
			' 
			Me.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
			Me.panel1.Controls.Add(Me.groupBox7)
			Me.panel1.Controls.Add(Me.groupBox6)
			Me.panel1.Controls.Add(Me.groupBox5)
			Me.panel1.Controls.Add(Me.gbInventoryMode)
			Me.panel1.Controls.Add(Me.groupBox4)
			Me.panel1.Controls.Add(Me.groupBox3)
			Me.panel1.Controls.Add(Me.groupBox2)
			Me.panel1.Controls.Add(Me.lblTotal)
			Me.panel1.Controls.Add(Me.lblTime)
			Me.panel1.Controls.Add(Me.label6)
			Me.panel1.Controls.Add(Me.label7)
			Me.panel1.Controls.Add(Me.lvEPC)
			Me.panel1.Controls.Add(Me.groupBox8)
			Me.panel1.Controls.Add(Me.button1)
			Me.panel1.Controls.Add(Me.btnScanEPC)
			Me.panel1.Controls.Add(Me.label2)
			Me.panel1.Controls.Add(Me.lto)
			Me.panel1.Location = New System.Drawing.Point(0, 2)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(1296, 629)
			Me.panel1.TabIndex = 31
			' 
			' groupBox7
			' 
			Me.groupBox7.Controls.Add(Me.label22)
			Me.groupBox7.Controls.Add(Me.label21)
			Me.groupBox7.Controls.Add(Me.label20)
			Me.groupBox7.Controls.Add(Me.txtNumber)
			Me.groupBox7.Controls.Add(Me.label19)
			Me.groupBox7.Controls.Add(Me.txtStart)
			Me.groupBox7.Controls.Add(Me.label18)
			Me.groupBox7.Controls.Add(Me.button7)
			Me.groupBox7.Location = New System.Drawing.Point(717, 410)
			Me.groupBox7.Name = "groupBox7"
			Me.groupBox7.Size = New System.Drawing.Size(561, 122)
			Me.groupBox7.TabIndex = 44
			Me.groupBox7.TabStop = False
			Me.groupBox7.Text = "温度标签"
			' 
			' label22
			' 
			Me.label22.AutoSize = True
			Me.label22.Font = New System.Drawing.Font("宋体", 12F)
			Me.label22.Location = New System.Drawing.Point(215, 84)
			Me.label22.Name = "label22"
			Me.label22.Size = New System.Drawing.Size(208, 16)
			Me.label22.TabIndex = 46
			Me.label22.Text = "本次读取到的温度值数量：0"
			' 
			' label21
			' 
			Me.label21.AutoSize = True
			Me.label21.Font = New System.Drawing.Font("宋体", 12F)
			Me.label21.Location = New System.Drawing.Point(32, 84)
			Me.label21.Name = "label21"
			Me.label21.Size = New System.Drawing.Size(128, 16)
			Me.label21.TabIndex = 45
			Me.label21.Text = "温度值总数量：0"
			' 
			' label20
			' 
			Me.label20.AutoSize = True
			Me.label20.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label20.ForeColor = System.Drawing.Color.Red
			Me.label20.Location = New System.Drawing.Point(512, 31)
			Me.label20.Name = "label20"
			Me.label20.Size = New System.Drawing.Size(41, 12)
			Me.label20.TabIndex = 44
			Me.label20.Text = "最大50"
			Me.label20.TextAlign = System.Drawing.ContentAlignment.BottomRight
			' 
			' txtNumber
			' 
			Me.txtNumber.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtNumber.Location = New System.Drawing.Point(467, 24)
			Me.txtNumber.MaxLength = 4
			Me.txtNumber.Name = "txtNumber"
			Me.txtNumber.Size = New System.Drawing.Size(44, 26)
			Me.txtNumber.TabIndex = 43
			Me.txtNumber.Tag = "Number"
			Me.txtNumber.Text = "50"
			' 
			' label19
			' 
			Me.label19.AutoSize = True
			Me.label19.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label19.Location = New System.Drawing.Point(374, 27)
			Me.label19.Name = "label19"
			Me.label19.Size = New System.Drawing.Size(96, 16)
			Me.label19.TabIndex = 42
			Me.label19.Text = "温度值数量:"
			Me.label19.TextAlign = System.Drawing.ContentAlignment.BottomRight
			' 
			' txtStart
			' 
			Me.txtStart.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtStart.Location = New System.Drawing.Point(297, 24)
			Me.txtStart.MaxLength = 4
			Me.txtStart.Name = "txtStart"
			Me.txtStart.Size = New System.Drawing.Size(41, 26)
			Me.txtStart.TabIndex = 41
			Me.txtStart.Tag = "Number"
			Me.txtStart.Text = "0"
			' 
			' label18
			' 
			Me.label18.AutoSize = True
			Me.label18.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label18.Location = New System.Drawing.Point(203, 27)
			Me.label18.Name = "label18"
			Me.label18.Size = New System.Drawing.Size(96, 16)
			Me.label18.TabIndex = 40
			Me.label18.Text = "温度值地址:"
			Me.label18.TextAlign = System.Drawing.ContentAlignment.BottomRight
			' 
			' button7
			' 
			Me.button7.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button7.ForeColor = System.Drawing.Color.Black
			Me.button7.Location = New System.Drawing.Point(25, 22)
			Me.button7.Name = "button7"
			Me.button7.Size = New System.Drawing.Size(172, 34)
			Me.button7.TabIndex = 39
			Me.button7.Text = "读取多个测温温度值"
			Me.button7.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button7.Click += new System.EventHandler(this.button7_Click);
			' 
			' groupBox6
			' 
			Me.groupBox6.Controls.Add(Me.label17)
			Me.groupBox6.Controls.Add(Me.btnVoltage)
			Me.groupBox6.Location = New System.Drawing.Point(717, 345)
			Me.groupBox6.Name = "groupBox6"
			Me.groupBox6.Size = New System.Drawing.Size(561, 59)
			Me.groupBox6.TabIndex = 75
			Me.groupBox6.TabStop = False
			Me.groupBox6.Text = "获取电压"
			' 
			' label17
			' 
			Me.label17.AutoSize = True
			Me.label17.Location = New System.Drawing.Point(370, 31)
			Me.label17.Name = "label17"
			Me.label17.Size = New System.Drawing.Size(53, 12)
			Me.label17.TabIndex = 38
			Me.label17.Text = "电压：--"
			' 
			' btnVoltage
			' 
			Me.btnVoltage.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnVoltage.ForeColor = System.Drawing.Color.Black
			Me.btnVoltage.Location = New System.Drawing.Point(25, 20)
			Me.btnVoltage.Name = "btnVoltage"
			Me.btnVoltage.Size = New System.Drawing.Size(143, 31)
			Me.btnVoltage.TabIndex = 36
			Me.btnVoltage.Text = "标签电池电压"
			Me.btnVoltage.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnVoltage.Click += new System.EventHandler(this.btnVoltage_Click);
			' 
			' groupBox5
			' 
			Me.groupBox5.Controls.Add(Me.button6)
			Me.groupBox5.Location = New System.Drawing.Point(1033, 213)
			Me.groupBox5.Name = "groupBox5"
			Me.groupBox5.Size = New System.Drawing.Size(245, 55)
			Me.groupBox5.TabIndex = 74
			Me.groupBox5.TabStop = False
			Me.groupBox5.Text = "模式检查"
			' 
			' button6
			' 
			Me.button6.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button6.Location = New System.Drawing.Point(45, 17)
			Me.button6.Name = "button6"
			Me.button6.Size = New System.Drawing.Size(93, 32)
			Me.button6.TabIndex = 47
			Me.button6.Text = "Get"
			Me.button6.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button6.Click += new System.EventHandler(this.button6_Click);
			' 
			' gbInventoryMode
			' 
			Me.gbInventoryMode.Controls.Add(Me.cbInventoryMode)
			Me.gbInventoryMode.Controls.Add(Me.label45)
			Me.gbInventoryMode.Controls.Add(Me.button10)
			Me.gbInventoryMode.Controls.Add(Me.button11)
			Me.gbInventoryMode.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.gbInventoryMode.Location = New System.Drawing.Point(717, 538)
			Me.gbInventoryMode.Name = "gbInventoryMode"
			Me.gbInventoryMode.Size = New System.Drawing.Size(561, 65)
			Me.gbInventoryMode.TabIndex = 73
			Me.gbInventoryMode.TabStop = False
			' 
			' cbInventoryMode
			' 
			Me.cbInventoryMode.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbInventoryMode.FormattingEnabled = True
			Me.cbInventoryMode.Items.AddRange(New Object() { "EPC", "EPC+TID+温度标签"})
			Me.cbInventoryMode.Location = New System.Drawing.Point(113, 25)
			Me.cbInventoryMode.Name = "cbInventoryMode"
			Me.cbInventoryMode.Size = New System.Drawing.Size(197, 24)
			Me.cbInventoryMode.TabIndex = 66
			' 
			' label45
			' 
			Me.label45.AutoSize = True
			Me.label45.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label45.Location = New System.Drawing.Point(7, 29)
			Me.label45.Name = "label45"
			Me.label45.Size = New System.Drawing.Size(48, 16)
			Me.label45.TabIndex = 66
			Me.label45.Text = "Mode:"
			' 
			' button10
			' 
			Me.button10.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button10.ForeColor = System.Drawing.Color.Black
			Me.button10.Location = New System.Drawing.Point(316, 22)
			Me.button10.Name = "button10"
			Me.button10.Size = New System.Drawing.Size(90, 31)
			Me.button10.TabIndex = 30
			Me.button10.Text = "Set"
			Me.button10.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button10.Click += new System.EventHandler(this.button10_Click);
			' 
			' button11
			' 
			Me.button11.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button11.ForeColor = System.Drawing.Color.Black
			Me.button11.Location = New System.Drawing.Point(417, 22)
			Me.button11.Name = "button11"
			Me.button11.Size = New System.Drawing.Size(90, 31)
			Me.button11.TabIndex = 29
			Me.button11.Text = "Get"
			Me.button11.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button11.Click += new System.EventHandler(this.button11_Click);
			' 
			' groupBox4
			' 
			Me.groupBox4.Controls.Add(Me.label39)
			Me.groupBox4.Controls.Add(Me.button4)
			Me.groupBox4.Controls.Add(Me.button5)
			Me.groupBox4.Location = New System.Drawing.Point(717, 274)
			Me.groupBox4.Name = "groupBox4"
			Me.groupBox4.Size = New System.Drawing.Size(561, 65)
			Me.groupBox4.TabIndex = 43
			Me.groupBox4.TabStop = False
			Me.groupBox4.Text = "温度标签"
			' 
			' label39
			' 
			Me.label39.AutoSize = True
			Me.label39.Location = New System.Drawing.Point(370, 32)
			Me.label39.Name = "label39"
			Me.label39.Size = New System.Drawing.Size(53, 12)
			Me.label39.TabIndex = 37
			Me.label39.Text = "温度：--"
			' 
			' button4
			' 
			Me.button4.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button4.ForeColor = System.Drawing.Color.Black
			Me.button4.Location = New System.Drawing.Point(174, 20)
			Me.button4.Name = "button4"
			Me.button4.Size = New System.Drawing.Size(123, 33)
			Me.button4.TabIndex = 36
			Me.button4.Text = "即时单次测温"
			Me.button4.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button4.Click += new System.EventHandler(this.button4_Click);
			' 
			' button5
			' 
			Me.button5.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button5.ForeColor = System.Drawing.Color.Black
			Me.button5.Location = New System.Drawing.Point(25, 20)
			Me.button5.Name = "button5"
			Me.button5.Size = New System.Drawing.Size(143, 33)
			Me.button5.TabIndex = 35
			Me.button5.Text = "初始化温度标签"
			Me.button5.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button5.Click += new System.EventHandler(this.button5_Click);
			' 
			' groupBox3
			' 
			Me.groupBox3.Controls.Add(Me.txtPwd)
			Me.groupBox3.Controls.Add(Me.label11)
			Me.groupBox3.Controls.Add(Me.button2)
			Me.groupBox3.Location = New System.Drawing.Point(1033, 128)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(245, 79)
			Me.groupBox3.TabIndex = 42
			Me.groupBox3.TabStop = False
			Me.groupBox3.Text = "停止测温"
			' 
			' txtPwd
			' 
			Me.txtPwd.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtPwd.Location = New System.Drawing.Point(45, 29)
			Me.txtPwd.MaxLength = 4
			Me.txtPwd.Name = "txtPwd"
			Me.txtPwd.Size = New System.Drawing.Size(83, 26)
			Me.txtPwd.TabIndex = 39
			Me.txtPwd.Tag = ""
			Me.txtPwd.Text = "00000000"
			' 
			' label11
			' 
			Me.label11.AutoSize = True
			Me.label11.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label11.Location = New System.Drawing.Point(6, 34)
			Me.label11.Name = "label11"
			Me.label11.Size = New System.Drawing.Size(40, 16)
			Me.label11.TabIndex = 39
			Me.label11.Text = "密码"
			' 
			' button2
			' 
			Me.button2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button2.Location = New System.Drawing.Point(129, 28)
			Me.button2.Name = "button2"
			Me.button2.Size = New System.Drawing.Size(110, 29)
			Me.button2.TabIndex = 0
			Me.button2.Text = "StopLogging"
			Me.button2.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button2.Click += new System.EventHandler(this.button2_Click);
			' 
			' groupBox2
			' 
			Me.groupBox2.Controls.Add(Me.label16)
			Me.groupBox2.Controls.Add(Me.label15)
			Me.groupBox2.Controls.Add(Me.label14)
			Me.groupBox2.Controls.Add(Me.label10)
			Me.groupBox2.Controls.Add(Me.button3)
			Me.groupBox2.Controls.Add(Me.txtinterval)
			Me.groupBox2.Controls.Add(Me.label12)
			Me.groupBox2.Controls.Add(Me.txtdelay)
			Me.groupBox2.Controls.Add(Me.label13)
			Me.groupBox2.Controls.Add(Me.txtMax)
			Me.groupBox2.Controls.Add(Me.label9)
			Me.groupBox2.Controls.Add(Me.txtMin)
			Me.groupBox2.Controls.Add(Me.label8)
			Me.groupBox2.Location = New System.Drawing.Point(717, 126)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(310, 142)
			Me.groupBox2.TabIndex = 40
			Me.groupBox2.TabStop = False
			Me.groupBox2.Text = "开始测温"
			' 
			' label16
			' 
			Me.label16.AutoSize = True
			Me.label16.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label16.Location = New System.Drawing.Point(174, 50)
			Me.label16.Name = "label16"
			Me.label16.Size = New System.Drawing.Size(24, 16)
			Me.label16.TabIndex = 50
			Me.label16.Text = "℃"
			' 
			' label15
			' 
			Me.label15.AutoSize = True
			Me.label15.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label15.Location = New System.Drawing.Point(174, 22)
			Me.label15.Name = "label15"
			Me.label15.Size = New System.Drawing.Size(24, 16)
			Me.label15.TabIndex = 49
			Me.label15.Text = "℃"
			' 
			' label14
			' 
			Me.label14.AutoSize = True
			Me.label14.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label14.Location = New System.Drawing.Point(174, 74)
			Me.label14.Name = "label14"
			Me.label14.Size = New System.Drawing.Size(24, 16)
			Me.label14.TabIndex = 48
			Me.label14.Text = "分"
			' 
			' label10
			' 
			Me.label10.AutoSize = True
			Me.label10.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label10.Location = New System.Drawing.Point(175, 103)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(24, 16)
			Me.label10.TabIndex = 47
			Me.label10.Text = "秒"
			' 
			' button3
			' 
			Me.button3.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button3.Location = New System.Drawing.Point(204, 47)
			Me.button3.Name = "button3"
			Me.button3.Size = New System.Drawing.Size(93, 32)
			Me.button3.TabIndex = 46
			Me.button3.Text = "StartLogging"
			Me.button3.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button3.Click += new System.EventHandler(this.button3_Click);
			' 
			' txtinterval
			' 
			Me.txtinterval.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtinterval.Location = New System.Drawing.Point(123, 98)
			Me.txtinterval.MaxLength = 5
			Me.txtinterval.Name = "txtinterval"
			Me.txtinterval.Size = New System.Drawing.Size(49, 26)
			Me.txtinterval.TabIndex = 45
			Me.txtinterval.Tag = ""
			Me.txtinterval.Text = "60"
			' 
			' label12
			' 
			Me.label12.AutoSize = True
			Me.label12.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label12.Location = New System.Drawing.Point(13, 103)
			Me.label12.Name = "label12"
			Me.label12.Size = New System.Drawing.Size(80, 16)
			Me.label12.TabIndex = 44
			Me.label12.Text = "间隔时间:"
			' 
			' txtdelay
			' 
			Me.txtdelay.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtdelay.Location = New System.Drawing.Point(123, 70)
			Me.txtdelay.MaxLength = 4
			Me.txtdelay.Name = "txtdelay"
			Me.txtdelay.Size = New System.Drawing.Size(49, 26)
			Me.txtdelay.TabIndex = 42
			Me.txtdelay.Tag = ""
			Me.txtdelay.Text = "0"
			' 
			' label13
			' 
			Me.label13.AutoSize = True
			Me.label13.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label13.Location = New System.Drawing.Point(12, 75)
			Me.label13.Name = "label13"
			Me.label13.Size = New System.Drawing.Size(112, 16)
			Me.label13.TabIndex = 43
			Me.label13.Text = "延时测温时间:"
			' 
			' txtMax
			' 
			Me.txtMax.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtMax.Location = New System.Drawing.Point(123, 42)
			Me.txtMax.MaxLength = 4
			Me.txtMax.Name = "txtMax"
			Me.txtMax.Size = New System.Drawing.Size(49, 26)
			Me.txtMax.TabIndex = 41
			Me.txtMax.Tag = ""
			Me.txtMax.Text = "50"
			' 
			' label9
			' 
			Me.label9.AutoSize = True
			Me.label9.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label9.Location = New System.Drawing.Point(13, 47)
			Me.label9.Name = "label9"
			Me.label9.Size = New System.Drawing.Size(72, 16)
			Me.label9.TabIndex = 40
			Me.label9.Text = "最高温度"
			' 
			' txtMin
			' 
			Me.txtMin.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtMin.Location = New System.Drawing.Point(123, 14)
			Me.txtMin.MaxLength = 4
			Me.txtMin.Name = "txtMin"
			Me.txtMin.Size = New System.Drawing.Size(49, 26)
			Me.txtMin.TabIndex = 39
			Me.txtMin.Tag = ""
			Me.txtMin.Text = "-20"
			' 
			' label8
			' 
			Me.label8.AutoSize = True
			Me.label8.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label8.Location = New System.Drawing.Point(13, 22)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(72, 16)
			Me.label8.TabIndex = 39
			Me.label8.Text = "最低温度"
			' 
			' lblTotal
			' 
			Me.lblTotal.AutoSize = True
			Me.lblTotal.Font = New System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold)
			Me.lblTotal.Location = New System.Drawing.Point(104, 462)
			Me.lblTotal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
			Me.lblTotal.Name = "lblTotal"
			Me.lblTotal.Size = New System.Drawing.Size(23, 24)
			Me.lblTotal.TabIndex = 35
			Me.lblTotal.Text = "0"
			' 
			' lblTime
			' 
			Me.lblTime.AutoSize = True
			Me.lblTime.Font = New System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold)
			Me.lblTime.Location = New System.Drawing.Point(104, 565)
			Me.lblTime.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
			Me.lblTime.Name = "lblTime"
			Me.lblTime.Size = New System.Drawing.Size(23, 24)
			Me.lblTime.TabIndex = 34
			Me.lblTime.Text = "0"
			' 
			' label6
			' 
			Me.label6.AutoSize = True
			Me.label6.BackColor = System.Drawing.Color.Transparent
			Me.label6.Font = New System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label6.ForeColor = System.Drawing.Color.FromArgb(CInt(CByte(0)), CInt(CByte(64)), CInt(CByte(0)))
			Me.label6.Location = New System.Drawing.Point(104, 513)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(23, 24)
			Me.label6.TabIndex = 33
			Me.label6.Text = "0"
			' 
			' label7
			' 
			Me.label7.AutoSize = True
			Me.label7.BackColor = System.Drawing.Color.Transparent
			Me.label7.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label7.ForeColor = System.Drawing.Color.Black
			Me.label7.Location = New System.Drawing.Point(24, 513)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(60, 19)
			Me.label7.TabIndex = 32
			Me.label7.Text = "次数:"
			' 
			' TempertureTag2ReadEPCForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.ClientSize = New System.Drawing.Size(1290, 631)
			Me.Controls.Add(Me.panel1)
			Me.ForeColor = System.Drawing.Color.Black
			Me.KeyPreview = True
			Me.Name = "TempertureTag2ReadEPCForm"
			Me.Text = "ReadEPC"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.ScanEPCForm_Load);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanEPCForm_FormClosing);
			Me.contextMenuStrip1.ResumeLayout(False)
			Me.groupBox8.ResumeLayout(False)
			Me.groupBox8.PerformLayout()
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.groupBox7.ResumeLayout(False)
			Me.groupBox7.PerformLayout()
			Me.groupBox6.ResumeLayout(False)
			Me.groupBox6.PerformLayout()
			Me.groupBox5.ResumeLayout(False)
			Me.gbInventoryMode.ResumeLayout(False)
			Me.gbInventoryMode.PerformLayout()
			Me.groupBox4.ResumeLayout(False)
			Me.groupBox4.PerformLayout()
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox3.PerformLayout()
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private WithEvents btnScanEPC As System.Windows.Forms.Button
		Private lvEPC As System.Windows.Forms.ListView
		Private columnHeaderID As System.Windows.Forms.ColumnHeader
		Private columnHeaderEPC As System.Windows.Forms.ColumnHeader
		Private label2 As System.Windows.Forms.Label
		Private lto As System.Windows.Forms.Label
		Private WithEvents button1 As System.Windows.Forms.Button
		Private groupBox8 As System.Windows.Forms.GroupBox
		Private rbTID As System.Windows.Forms.RadioButton
		Private rbEPC As System.Windows.Forms.RadioButton
		Private WithEvents txtData As System.Windows.Forms.TextBox
		Private WithEvents txtPtr As System.Windows.Forms.TextBox
		Private label29 As System.Windows.Forms.Label
		Private label30 As System.Windows.Forms.Label
		Private columnHeaderTemperature As System.Windows.Forms.ColumnHeader
		Private panel1 As System.Windows.Forms.Panel
		Private columnHeaderRssi As System.Windows.Forms.ColumnHeader
		Private columnHeaderCount As System.Windows.Forms.ColumnHeader
		Private rbUser As System.Windows.Forms.RadioButton
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private txtfilerLen As System.Windows.Forms.TextBox
		Private label1 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private label3 As System.Windows.Forms.Label
		Private label5 As System.Windows.Forms.Label
		Private columnHeaderANT As System.Windows.Forms.ColumnHeader
		Private contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
		Private qqqqqqqqqqqToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private label6 As System.Windows.Forms.Label
		Private label7 As System.Windows.Forms.Label
		Private lblTime As System.Windows.Forms.Label
		Private lblTotal As System.Windows.Forms.Label
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private label8 As System.Windows.Forms.Label
		Private groupBox3 As System.Windows.Forms.GroupBox
		Private txtPwd As System.Windows.Forms.TextBox
		Private label11 As System.Windows.Forms.Label
		Private WithEvents button2 As System.Windows.Forms.Button
		Private txtMax As System.Windows.Forms.TextBox
		Private label9 As System.Windows.Forms.Label
		Private txtMin As System.Windows.Forms.TextBox
		Private txtinterval As System.Windows.Forms.TextBox
		Private label12 As System.Windows.Forms.Label
		Private txtdelay As System.Windows.Forms.TextBox
		Private label13 As System.Windows.Forms.Label
		Private WithEvents button3 As System.Windows.Forms.Button
		Private groupBox4 As System.Windows.Forms.GroupBox
		Private label39 As System.Windows.Forms.Label
		Private WithEvents button4 As System.Windows.Forms.Button
		Private WithEvents button5 As System.Windows.Forms.Button
		Private gbInventoryMode As System.Windows.Forms.GroupBox
		Private cbInventoryMode As System.Windows.Forms.ComboBox
		Private label45 As System.Windows.Forms.Label
		Private WithEvents button10 As System.Windows.Forms.Button
		Private WithEvents button11 As System.Windows.Forms.Button
		Private groupBox5 As System.Windows.Forms.GroupBox
		Private WithEvents button6 As System.Windows.Forms.Button
		Private label14 As System.Windows.Forms.Label
		Private label10 As System.Windows.Forms.Label
		Private label16 As System.Windows.Forms.Label
		Private label15 As System.Windows.Forms.Label
		Private columnHeader1 As System.Windows.Forms.ColumnHeader
		Private columnHeaderTID As System.Windows.Forms.ColumnHeader
		Private groupBox6 As System.Windows.Forms.GroupBox
		Private label17 As System.Windows.Forms.Label
		Private WithEvents btnVoltage As System.Windows.Forms.Button
		Private WithEvents button7 As System.Windows.Forms.Button
		Private groupBox7 As System.Windows.Forms.GroupBox
		Private txtNumber As System.Windows.Forms.TextBox
		Private label19 As System.Windows.Forms.Label
		Private txtStart As System.Windows.Forms.TextBox
		Private label18 As System.Windows.Forms.Label
		Private label20 As System.Windows.Forms.Label
		Private label22 As System.Windows.Forms.Label
		Private label21 As System.Windows.Forms.Label
	End Class
End Namespace
