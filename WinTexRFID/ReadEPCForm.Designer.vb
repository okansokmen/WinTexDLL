Namespace UHFAPP
	Partial Public Class ReadEPCForm
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
			Me.columnHeaderUser = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderRssi = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderCount = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderANT = New System.Windows.Forms.ColumnHeader()
			Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
			Me.qqqqqqqqqqqToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.lto = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.button1 = New System.Windows.Forms.Button()
			Me.groupBox8 = New System.Windows.Forms.GroupBox()
			Me.label5 = New System.Windows.Forms.Label()
			Me.filerLen = New System.Windows.Forms.TextBox()
			Me.txtPtr = New System.Windows.Forms.TextBox()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label1 = New System.Windows.Forms.Label()
			Me.button2 = New System.Windows.Forms.Button()
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.rbUser = New System.Windows.Forms.RadioButton()
			Me.rbEPC = New System.Windows.Forms.RadioButton()
			Me.rbTID = New System.Windows.Forms.RadioButton()
			Me.cbSave = New System.Windows.Forms.CheckBox()
			Me.btnSet = New System.Windows.Forms.Button()
			Me.txtData = New System.Windows.Forms.TextBox()
			Me.label29 = New System.Windows.Forms.Label()
			Me.label30 = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.gbAuto = New System.Windows.Forms.GroupBox()
			Me.label11 = New System.Windows.Forms.Label()
			Me.txtTime = New System.Windows.Forms.TextBox()
			Me.label10 = New System.Windows.Forms.Label()
			Me.rb10 = New System.Windows.Forms.RadioButton()
			Me.rb5s = New System.Windows.Forms.RadioButton()
			Me.rb3s = New System.Windows.Forms.RadioButton()
			Me.rb4s = New System.Windows.Forms.RadioButton()
			Me.rb2s = New System.Windows.Forms.RadioButton()
			Me.btnExport = New System.Windows.Forms.Button()
			Me.label9 = New System.Windows.Forms.Label()
			Me.label8 = New System.Windows.Forms.Label()
			Me.lblTotal = New System.Windows.Forms.Label()
			Me.lblTime = New System.Windows.Forms.Label()
			Me.label6 = New System.Windows.Forms.Label()
			Me.label7 = New System.Windows.Forms.Label()
			Me.button3 = New System.Windows.Forms.Button()
			Me.contextMenuStrip1.SuspendLayout()
			Me.groupBox8.SuspendLayout()
			Me.groupBox1.SuspendLayout()
			Me.panel1.SuspendLayout()
			Me.gbAuto.SuspendLayout()
			Me.SuspendLayout()
			' 
			' btnScanEPC
			' 
			Me.btnScanEPC.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnScanEPC.Location = New System.Drawing.Point(552, 553)
			Me.btnScanEPC.Name = "btnScanEPC"
			Me.btnScanEPC.Size = New System.Drawing.Size(93, 48)
			Me.btnScanEPC.TabIndex = 0
			Me.btnScanEPC.Text = "Start"
			Me.btnScanEPC.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnScanEPC.Click += new System.EventHandler(this.btnScanEPC_Click);
			' 
			' lvEPC
			' 
			Me.lvEPC.BackColor = System.Drawing.SystemColors.ControlLightLight
			Me.lvEPC.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.columnHeaderID, Me.columnHeaderEPC, Me.columnHeaderTID, Me.columnHeaderUser, Me.columnHeaderRssi, Me.columnHeaderCount, Me.columnHeaderANT})
			Me.lvEPC.ContextMenuStrip = Me.contextMenuStrip1
			Me.lvEPC.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lvEPC.FullRowSelect = True
			Me.lvEPC.Location = New System.Drawing.Point(0, 94)
			Me.lvEPC.Name = "lvEPC"
			Me.lvEPC.Size = New System.Drawing.Size(1287, 382)
			Me.lvEPC.TabIndex = 2
			Me.lvEPC.UseCompatibleStateImageBehavior = False
			Me.lvEPC.View = System.Windows.Forms.View.Details
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.lvEPC.DoubleClick += new System.EventHandler(this.lvEPC_DoubleClick);
			' 
			' columnHeaderID
			' 
			Me.columnHeaderID.Text = "ID"
			Me.columnHeaderID.Width = 0
			' 
			' columnHeaderEPC
			' 
			Me.columnHeaderEPC.Text = "EPC"
			Me.columnHeaderEPC.Width = 420
			' 
			' columnHeaderTID
			' 
			Me.columnHeaderTID.Text = "TID"
			Me.columnHeaderTID.Width = 270
			' 
			' columnHeaderUser
			' 
			Me.columnHeaderUser.Text = "USER"
			Me.columnHeaderUser.Width = 270
			' 
			' columnHeaderRssi
			' 
			Me.columnHeaderRssi.Text = "Rssi"
			Me.columnHeaderRssi.Width = 90
			' 
			' columnHeaderCount
			' 
			Me.columnHeaderCount.Text = "Count"
			Me.columnHeaderCount.Width = 80
			' 
			' columnHeaderANT
			' 
			Me.columnHeaderANT.Text = "ANT"
			Me.columnHeaderANT.Width = 50
			' 
			' contextMenuStrip1
			' 
			Me.contextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
			Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.qqqqqqqqqqqToolStripMenuItem})
			Me.contextMenuStrip1.Name = "contextMenuStrip1"
			Me.contextMenuStrip1.ShowCheckMargin = True
			Me.contextMenuStrip1.Size = New System.Drawing.Size(187, 26)
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.contextMenuStrip1.Click += new System.EventHandler(this.contextMenuStrip1_Click);
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
			Me.lto.Location = New System.Drawing.Point(56, 553)
			Me.lto.Name = "lto"
			Me.lto.Size = New System.Drawing.Size(75, 19)
			Me.lto.TabIndex = 3
			Me.lto.Text = "Total:"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.lto.Click += new System.EventHandler(this.lto_Click);
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.BackColor = System.Drawing.Color.Transparent
			Me.label2.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label2.ForeColor = System.Drawing.Color.Black
			Me.label2.Location = New System.Drawing.Point(230, 586)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(64, 19)
			Me.label2.TabIndex = 4
			Me.label2.Text = "Time:"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.label2.Click += new System.EventHandler(this.label2_Click);
			' 
			' button1
			' 
			Me.button1.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button1.Location = New System.Drawing.Point(688, 553)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(91, 48)
			Me.button1.TabIndex = 24
			Me.button1.Text = "Clear"
			Me.button1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button1.Click += new System.EventHandler(this.button1_Click);
			' 
			' groupBox8
			' 
			Me.groupBox8.BackColor = System.Drawing.Color.Transparent
			Me.groupBox8.Controls.Add(Me.label5)
			Me.groupBox8.Controls.Add(Me.filerLen)
			Me.groupBox8.Controls.Add(Me.txtPtr)
			Me.groupBox8.Controls.Add(Me.label3)
			Me.groupBox8.Controls.Add(Me.label1)
			Me.groupBox8.Controls.Add(Me.button2)
			Me.groupBox8.Controls.Add(Me.groupBox1)
			Me.groupBox8.Controls.Add(Me.cbSave)
			Me.groupBox8.Controls.Add(Me.btnSet)
			Me.groupBox8.Controls.Add(Me.txtData)
			Me.groupBox8.Controls.Add(Me.label29)
			Me.groupBox8.Controls.Add(Me.label30)
			Me.groupBox8.Controls.Add(Me.label4)
			Me.groupBox8.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox8.ForeColor = System.Drawing.Color.Black
			Me.groupBox8.Location = New System.Drawing.Point(0, 3)
			Me.groupBox8.Name = "groupBox8"
			Me.groupBox8.Size = New System.Drawing.Size(1287, 74)
			Me.groupBox8.TabIndex = 30
			Me.groupBox8.TabStop = False
			Me.groupBox8.Text = "Filter"
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label5.ForeColor = System.Drawing.Color.Black
			Me.label5.Location = New System.Drawing.Point(612, 35)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(16, 16)
			Me.label5.TabIndex = 38
			Me.label5.Text = "0"
			' 
			' filerLen
			' 
			Me.filerLen.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.filerLen.Location = New System.Drawing.Point(846, 28)
			Me.filerLen.MaxLength = 4
			Me.filerLen.Name = "filerLen"
			Me.filerLen.Size = New System.Drawing.Size(49, 26)
			Me.filerLen.TabIndex = 34
			Me.filerLen.Tag = "Number"
			Me.filerLen.Text = "0"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.filerLen.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			' 
			' txtPtr
			' 
			Me.txtPtr.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtPtr.Location = New System.Drawing.Point(716, 29)
			Me.txtPtr.MaxLength = 4
			Me.txtPtr.Name = "txtPtr"
			Me.txtPtr.Size = New System.Drawing.Size(49, 26)
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
			Me.label3.Location = New System.Drawing.Point(761, 37)
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
			Me.label1.Location = New System.Drawing.Point(801, 33)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(48, 16)
			Me.label1.TabIndex = 33
			Me.label1.Text = "长度:"
			Me.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight
			' 
			' button2
			' 
			Me.button2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button2.ForeColor = System.Drawing.Color.Black
			Me.button2.Location = New System.Drawing.Point(1207, 42)
			Me.button2.Name = "button2"
			Me.button2.Size = New System.Drawing.Size(69, 29)
			Me.button2.TabIndex = 32
			Me.button2.Text = "reset"
			Me.button2.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button2.Click += new System.EventHandler(this.button2_Click);
			' 
			' groupBox1
			' 
			Me.groupBox1.Controls.Add(Me.rbUser)
			Me.groupBox1.Controls.Add(Me.rbEPC)
			Me.groupBox1.Controls.Add(Me.rbTID)
			Me.groupBox1.Location = New System.Drawing.Point(932, 14)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(181, 47)
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
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rbUser.Click += new System.EventHandler(this.rbUser_Click);
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
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rbEPC.Click += new System.EventHandler(this.rbEPC_Click);
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
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rbTID.Click += new System.EventHandler(this.rbTID_Click);
			' 
			' cbSave
			' 
			Me.cbSave.AutoSize = True
			Me.cbSave.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbSave.Location = New System.Drawing.Point(1119, 35)
			Me.cbSave.Name = "cbSave"
			Me.cbSave.Size = New System.Drawing.Size(59, 20)
			Me.cbSave.TabIndex = 11
			Me.cbSave.Text = "Save"
			Me.cbSave.UseVisualStyleBackColor = True
			' 
			' btnSet
			' 
			Me.btnSet.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnSet.ForeColor = System.Drawing.Color.Black
			Me.btnSet.Location = New System.Drawing.Point(1207, 14)
			Me.btnSet.Name = "btnSet"
			Me.btnSet.Size = New System.Drawing.Size(69, 29)
			Me.btnSet.TabIndex = 10
			Me.btnSet.Text = "Set"
			Me.btnSet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
			' 
			' txtData
			' 
			Me.txtData.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtData.Location = New System.Drawing.Point(50, 20)
			Me.txtData.Multiline = True
			Me.txtData.Name = "txtData"
			Me.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtData.Size = New System.Drawing.Size(558, 41)
			Me.txtData.TabIndex = 7
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtData.TextChanged += new System.EventHandler(this.txtData_TextChanged);
			' 
			' label29
			' 
			Me.label29.AutoSize = True
			Me.label29.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label29.Location = New System.Drawing.Point(6, 30)
			Me.label29.Name = "label29"
			Me.label29.Size = New System.Drawing.Size(48, 16)
			Me.label29.TabIndex = 5
			Me.label29.Text = "Data:"
			' 
			' label30
			' 
			Me.label30.AutoSize = True
			Me.label30.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label30.Location = New System.Drawing.Point(670, 34)
			Me.label30.Name = "label30"
			Me.label30.Size = New System.Drawing.Size(40, 16)
			Me.label30.TabIndex = 4
			Me.label30.Text = "Ptr:"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label4.Location = New System.Drawing.Point(893, 37)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(42, 14)
			Me.label4.TabIndex = 36
			Me.label4.Text = "(bit)"
			Me.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight
			' 
			' panel1
			' 
			Me.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.panel1.Controls.Add(Me.gbAuto)
			Me.panel1.Controls.Add(Me.btnExport)
			Me.panel1.Controls.Add(Me.label9)
			Me.panel1.Controls.Add(Me.label8)
			Me.panel1.Controls.Add(Me.lblTotal)
			Me.panel1.Controls.Add(Me.lblTime)
			Me.panel1.Controls.Add(Me.label6)
			Me.panel1.Controls.Add(Me.label7)
			Me.panel1.Controls.Add(Me.button3)
			Me.panel1.Controls.Add(Me.lvEPC)
			Me.panel1.Controls.Add(Me.groupBox8)
			Me.panel1.Controls.Add(Me.button1)
			Me.panel1.Controls.Add(Me.btnScanEPC)
			Me.panel1.Controls.Add(Me.label2)
			Me.panel1.Controls.Add(Me.lto)
			Me.panel1.Location = New System.Drawing.Point(0, 0)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(1296, 629)
			Me.panel1.TabIndex = 31
			' 
			' gbAuto
			' 
			Me.gbAuto.Controls.Add(Me.label11)
			Me.gbAuto.Controls.Add(Me.txtTime)
			Me.gbAuto.Controls.Add(Me.label10)
			Me.gbAuto.Controls.Add(Me.rb10)
			Me.gbAuto.Controls.Add(Me.rb5s)
			Me.gbAuto.Controls.Add(Me.rb3s)
			Me.gbAuto.Controls.Add(Me.rb4s)
			Me.gbAuto.Controls.Add(Me.rb2s)
			Me.gbAuto.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.gbAuto.Location = New System.Drawing.Point(3, 474)
			Me.gbAuto.Name = "gbAuto"
			Me.gbAuto.Size = New System.Drawing.Size(1284, 50)
			Me.gbAuto.TabIndex = 40
			Me.gbAuto.TabStop = False
			' 
			' label11
			' 
			Me.label11.AutoSize = True
			Me.label11.Location = New System.Drawing.Point(540, 20)
			Me.label11.Name = "label11"
			Me.label11.Size = New System.Drawing.Size(24, 16)
			Me.label11.TabIndex = 7
			Me.label11.Text = "秒"
			' 
			' txtTime
			' 
			Me.txtTime.Font = New System.Drawing.Font("宋体", 12F)
			Me.txtTime.Location = New System.Drawing.Point(434, 14)
			Me.txtTime.Name = "txtTime"
			Me.txtTime.Size = New System.Drawing.Size(100, 26)
			Me.txtTime.TabIndex = 5
			Me.txtTime.Text = "0"
			' 
			' label10
			' 
			Me.label10.AutoSize = True
			Me.label10.Location = New System.Drawing.Point(350, 20)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(80, 16)
			Me.label10.TabIndex = 6
			Me.label10.Text = "盘点时间:"
			' 
			' rb10
			' 
			Me.rb10.AutoSize = True
			Me.rb10.Location = New System.Drawing.Point(250, 20)
			Me.rb10.Name = "rb10"
			Me.rb10.Size = New System.Drawing.Size(58, 20)
			Me.rb10.TabIndex = 4
			Me.rb10.TabStop = True
			Me.rb10.Text = "10秒"
			Me.rb10.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rb10.Click += new System.EventHandler(this.rb10_Click);
			' 
			' rb5s
			' 
			Me.rb5s.AutoSize = True
			Me.rb5s.Location = New System.Drawing.Point(194, 20)
			Me.rb5s.Name = "rb5s"
			Me.rb5s.Size = New System.Drawing.Size(50, 20)
			Me.rb5s.TabIndex = 3
			Me.rb5s.TabStop = True
			Me.rb5s.Text = "5秒"
			Me.rb5s.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rb5s.Click += new System.EventHandler(this.rb5s_Click);
			' 
			' rb3s
			' 
			Me.rb3s.AutoSize = True
			Me.rb3s.Location = New System.Drawing.Point(82, 20)
			Me.rb3s.Name = "rb3s"
			Me.rb3s.Size = New System.Drawing.Size(50, 20)
			Me.rb3s.TabIndex = 2
			Me.rb3s.TabStop = True
			Me.rb3s.Text = "3秒"
			Me.rb3s.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rb3s.Click += new System.EventHandler(this.rb3s_Click);
			' 
			' rb4s
			' 
			Me.rb4s.AutoSize = True
			Me.rb4s.Location = New System.Drawing.Point(138, 20)
			Me.rb4s.Name = "rb4s"
			Me.rb4s.Size = New System.Drawing.Size(50, 20)
			Me.rb4s.TabIndex = 1
			Me.rb4s.TabStop = True
			Me.rb4s.Text = "4秒"
			Me.rb4s.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rb4s.Click += new System.EventHandler(this.rb4s_Click);
			' 
			' rb2s
			' 
			Me.rb2s.AutoSize = True
			Me.rb2s.Location = New System.Drawing.Point(26, 20)
			Me.rb2s.Name = "rb2s"
			Me.rb2s.Size = New System.Drawing.Size(50, 20)
			Me.rb2s.TabIndex = 0
			Me.rb2s.TabStop = True
			Me.rb2s.Text = "2秒"
			Me.rb2s.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rb2s.Click += new System.EventHandler(this.rb2s_Click);
			' 
			' btnExport
			' 
			Me.btnExport.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnExport.Location = New System.Drawing.Point(1001, 551)
			Me.btnExport.Name = "btnExport"
			Me.btnExport.Size = New System.Drawing.Size(177, 48)
			Me.btnExport.TabIndex = 39
			Me.btnExport.Text = "导出数据"
			Me.btnExport.UseVisualStyleBackColor = True
			Me.btnExport.Visible = False
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			' 
			' label9
			' 
			Me.label9.AutoSize = True
			Me.label9.Font = New System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold)
			Me.label9.Location = New System.Drawing.Point(433, 553)
			Me.label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
			Me.label9.Name = "label9"
			Me.label9.Size = New System.Drawing.Size(23, 24)
			Me.label9.TabIndex = 37
			Me.label9.Text = "0"
			Me.label9.Visible = False
			' 
			' label8
			' 
			Me.label8.AutoSize = True
			Me.label8.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold)
			Me.label8.Location = New System.Drawing.Point(375, 556)
			Me.label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(60, 19)
			Me.label8.TabIndex = 36
			Me.label8.Text = "速率:"
			Me.label8.Visible = False
			' 
			' lblTotal
			' 
			Me.lblTotal.AutoSize = True
			Me.lblTotal.Font = New System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold)
			Me.lblTotal.Location = New System.Drawing.Point(163, 551)
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
			Me.lblTime.Location = New System.Drawing.Point(305, 586)
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
			Me.label6.Location = New System.Drawing.Point(304, 551)
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
			Me.label7.Location = New System.Drawing.Point(229, 553)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(60, 19)
			Me.label7.TabIndex = 32
			Me.label7.Text = "次数:"
			' 
			' button3
			' 
			Me.button3.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button3.Location = New System.Drawing.Point(810, 549)
			Me.button3.Name = "button3"
			Me.button3.Size = New System.Drawing.Size(156, 48)
			Me.button3.TabIndex = 31
			Me.button3.Text = "自动"
			Me.button3.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button3.Click += new System.EventHandler(this.button3_Click);
			' 
			' ReadEPCForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.ClientSize = New System.Drawing.Size(1290, 631)
			Me.Controls.Add(Me.panel1)
			Me.ForeColor = System.Drawing.Color.Black
			Me.KeyPreview = True
			Me.Name = "ReadEPCForm"
			Me.Text = "ReadEPC"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.ScanEPCForm_Load);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanEPCForm_FormClosing);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadEPCForm_KeyDown);
			Me.contextMenuStrip1.ResumeLayout(False)
			Me.groupBox8.ResumeLayout(False)
			Me.groupBox8.PerformLayout()
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.gbAuto.ResumeLayout(False)
			Me.gbAuto.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private WithEvents btnScanEPC As System.Windows.Forms.Button
		Private WithEvents lvEPC As System.Windows.Forms.ListView
		Private columnHeaderID As System.Windows.Forms.ColumnHeader
		Private columnHeaderEPC As System.Windows.Forms.ColumnHeader
		Private WithEvents label2 As System.Windows.Forms.Label
		Private WithEvents lto As System.Windows.Forms.Label
		Private WithEvents button1 As System.Windows.Forms.Button
		Private groupBox8 As System.Windows.Forms.GroupBox
		Private WithEvents btnSet As System.Windows.Forms.Button
		Private WithEvents rbTID As System.Windows.Forms.RadioButton
		Private WithEvents rbEPC As System.Windows.Forms.RadioButton
		Private WithEvents txtData As System.Windows.Forms.TextBox
		Private WithEvents txtPtr As System.Windows.Forms.TextBox
		Private label29 As System.Windows.Forms.Label
		Private label30 As System.Windows.Forms.Label
		Private columnHeaderTID As System.Windows.Forms.ColumnHeader
		Private panel1 As System.Windows.Forms.Panel
		Private columnHeaderRssi As System.Windows.Forms.ColumnHeader
		Private columnHeaderCount As System.Windows.Forms.ColumnHeader
		Private cbSave As System.Windows.Forms.CheckBox
		Private WithEvents rbUser As System.Windows.Forms.RadioButton
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private WithEvents button2 As System.Windows.Forms.Button
		Private WithEvents filerLen As System.Windows.Forms.TextBox
		Private label1 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private label3 As System.Windows.Forms.Label
		Private label5 As System.Windows.Forms.Label
		Private columnHeaderANT As System.Windows.Forms.ColumnHeader
		Private WithEvents contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
		Private qqqqqqqqqqqToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents button3 As System.Windows.Forms.Button
		Private label6 As System.Windows.Forms.Label
		Private label7 As System.Windows.Forms.Label
		Private lblTime As System.Windows.Forms.Label
		Private lblTotal As System.Windows.Forms.Label
		Private label8 As System.Windows.Forms.Label
		Private label9 As System.Windows.Forms.Label
		Private columnHeaderUser As System.Windows.Forms.ColumnHeader
		Private WithEvents btnExport As System.Windows.Forms.Button
		Private gbAuto As System.Windows.Forms.GroupBox
		Private WithEvents rb5s As System.Windows.Forms.RadioButton
		Private WithEvents rb3s As System.Windows.Forms.RadioButton
		Private WithEvents rb4s As System.Windows.Forms.RadioButton
		Private WithEvents rb2s As System.Windows.Forms.RadioButton
		Private txtTime As System.Windows.Forms.TextBox
		Private label10 As System.Windows.Forms.Label
		Private WithEvents rb10 As System.Windows.Forms.RadioButton
		Private label11 As System.Windows.Forms.Label
	End Class
End Namespace
