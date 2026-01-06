Namespace UHFAPP
	Partial Public Class MainForm2
		''' <summary>
		''' 必需的设计器变量。
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' 清理所有正在使用的资源。
		''' </summary>
		''' <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows 窗体设计器生成的代码"

		''' <summary>
		''' 设计器支持所需的方法 - 不要
		''' 使用代码编辑器修改此方法的内容。
		''' </summary>
		Private Sub InitializeComponent()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(MainForm2))
			Me.menuStrip1 = New System.Windows.Forms.MenuStrip()
			Me.MenuItemScanEPC = New System.Windows.Forms.ToolStripMenuItem()
			Me.MenuItemReadWriteTag = New System.Windows.Forms.ToolStripMenuItem()
			Me.configToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.killLockToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.uHFVersionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
			Me.MenuItemReceiveEPC = New System.Windows.Forms.ToolStripMenuItem()
			Me.ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.testToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
			Me.toolStripLabel4 = New System.Windows.Forms.ToolStripLabel()
			Me.combCommunicationMode = New System.Windows.Forms.ToolStripComboBox()
			Me.toolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
			Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
			Me.lblPortName = New System.Windows.Forms.ToolStripLabel()
			Me.cmbComPort = New System.Windows.Forms.ToolStripComboBox()
			Me.toolStripOpen = New System.Windows.Forms.ToolStripButton()
			Me.toolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
			Me.toolStripLabel5 = New System.Windows.Forms.ToolStripLabel()
			Me.toolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
			Me.toolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
			Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
			Me.toolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.btnConnect = New System.Windows.Forms.Button()
			Me.lblPort = New System.Windows.Forms.Label()
			Me.ipControl1 = New WindowsFormsControlLibrary1.IPControl()
			Me.label1 = New System.Windows.Forms.Label()
			Me.txtPort = New System.Windows.Forms.TextBox()
			Me.plUsb = New System.Windows.Forms.Panel()
			Me.btnUsbOpen = New System.Windows.Forms.Button()
			Me.menuStrip1.SuspendLayout()
			Me.toolStrip1.SuspendLayout()
			Me.statusStrip1.SuspendLayout()
			Me.panel1.SuspendLayout()
			Me.plUsb.SuspendLayout()
			Me.SuspendLayout()
			' 
			' menuStrip1
			' 
			Me.menuStrip1.BackColor = System.Drawing.Color.LightSteelBlue
			Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.MenuItemScanEPC, Me.MenuItemReadWriteTag, Me.configToolStripMenuItem, Me.killLockToolStripMenuItem, Me.uHFVersionToolStripMenuItem, Me.toolStripMenuItem1, Me.MenuItemReceiveEPC, Me.ToolStripMenuItem, Me.testToolStripMenuItem})
			Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
			Me.menuStrip1.Name = "menuStrip1"
			Me.menuStrip1.Size = New System.Drawing.Size(1290, 25)
			Me.menuStrip1.TabIndex = 2
			Me.menuStrip1.Text = "ScanEPC"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.menuStrip1.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.menuStrip1_ItemAdded);
			' 
			' MenuItemScanEPC
			' 
			Me.MenuItemScanEPC.Name = "MenuItemScanEPC"
			Me.MenuItemScanEPC.Size = New System.Drawing.Size(72, 21)
			Me.MenuItemScanEPC.Text = "ReadEPC"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.MenuItemScanEPC.Click += new System.EventHandler(this.MenuItemScanEPC_Click);
			' 
			' MenuItemReadWriteTag
			' 
			Me.MenuItemReadWriteTag.Name = "MenuItemReadWriteTag"
			Me.MenuItemReadWriteTag.Size = New System.Drawing.Size(103, 21)
			Me.MenuItemReadWriteTag.Text = "ReadWriteTag"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.MenuItemReadWriteTag.Click += new System.EventHandler(this.MenuItemReadWriteTag_Click);
			' 
			' configToolStripMenuItem
			' 
			Me.configToolStripMenuItem.Name = "configToolStripMenuItem"
			Me.configToolStripMenuItem.Size = New System.Drawing.Size(99, 21)
			Me.configToolStripMenuItem.Text = "Configuration"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
			' 
			' killLockToolStripMenuItem
			' 
			Me.killLockToolStripMenuItem.Name = "killLockToolStripMenuItem"
			Me.killLockToolStripMenuItem.Size = New System.Drawing.Size(69, 21)
			Me.killLockToolStripMenuItem.Text = "Kill-Lock"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.killLockToolStripMenuItem.Click += new System.EventHandler(this.killLockToolStripMenuItem_Click);
			' 
			' uHFVersionToolStripMenuItem
			' 
			Me.uHFVersionToolStripMenuItem.Name = "uHFVersionToolStripMenuItem"
			Me.uHFVersionToolStripMenuItem.Size = New System.Drawing.Size(71, 21)
			Me.uHFVersionToolStripMenuItem.Text = "UHF Info"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.uHFVersionToolStripMenuItem.Click += new System.EventHandler(this.uHFVersionToolStripMenuItem_Click);
			' 
			' toolStripMenuItem1
			' 
			Me.toolStripMenuItem1.Name = "toolStripMenuItem1"
			Me.toolStripMenuItem1.Size = New System.Drawing.Size(95, 21)
			Me.toolStripMenuItem1.Text = "Temperature"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
			' 
			' MenuItemReceiveEPC
			' 
			Me.MenuItemReceiveEPC.Name = "MenuItemReceiveEPC"
			Me.MenuItemReceiveEPC.Size = New System.Drawing.Size(116, 21)
			Me.MenuItemReceiveEPC.Text = "UDP-ReceiveEPC"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.MenuItemReceiveEPC.Click += new System.EventHandler(this.receiveEPCToolStripMenuItem_Click);
			' 
			' ToolStripMenuItem
			' 
			Me.ToolStripMenuItem.Name = "ToolStripMenuItem"
			Me.ToolStripMenuItem.Size = New System.Drawing.Size(68, 21)
			Me.ToolStripMenuItem.Text = "温度标签"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
			' 
			' testToolStripMenuItem
			' 
			Me.testToolStripMenuItem.Name = "testToolStripMenuItem"
			Me.testToolStripMenuItem.Size = New System.Drawing.Size(44, 21)
			Me.testToolStripMenuItem.Text = "测试"
			Me.testToolStripMenuItem.Visible = False
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
			' 
			' toolStrip1
			' 
			Me.toolStrip1.AutoSize = False
			Me.toolStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
			Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.toolStripLabel4, Me.combCommunicationMode, Me.toolStripLabel2, Me.toolStripSeparator1, Me.lblPortName, Me.cmbComPort, Me.toolStripOpen, Me.toolStripLabel1, Me.toolStripLabel5, Me.toolStripLabel3, Me.toolStripComboBox1})
			Me.toolStrip1.Location = New System.Drawing.Point(0, 25)
			Me.toolStrip1.Name = "toolStrip1"
			Me.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
			Me.toolStrip1.Size = New System.Drawing.Size(1290, 34)
			Me.toolStrip1.TabIndex = 5
			Me.toolStrip1.Text = "Open"
			' 
			' toolStripLabel4
			' 
			Me.toolStripLabel4.Name = "toolStripLabel4"
			Me.toolStripLabel4.Size = New System.Drawing.Size(56, 31)
			Me.toolStripLabel4.Text = "通信方式"
			' 
			' combCommunicationMode
			' 
			Me.combCommunicationMode.Items.AddRange(New Object() { "串口", "网络"})
			Me.combCommunicationMode.Name = "combCommunicationMode"
			Me.combCommunicationMode.Size = New System.Drawing.Size(121, 34)
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.combCommunicationMode.TextChanged += new System.EventHandler(this.toolStripComboBox2_TextChanged);
			' 
			' toolStripLabel2
			' 
			Me.toolStripLabel2.Name = "toolStripLabel2"
			Me.toolStripLabel2.Size = New System.Drawing.Size(80, 31)
			Me.toolStripLabel2.Text = "                  "
			' 
			' toolStripSeparator1
			' 
			Me.toolStripSeparator1.Name = "toolStripSeparator1"
			Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 34)
			' 
			' lblPortName
			' 
			Me.lblPortName.Name = "lblPortName"
			Me.lblPortName.Size = New System.Drawing.Size(38, 31)
			Me.lblPortName.Text = "COM"
			' 
			' cmbComPort
			' 
			Me.cmbComPort.Name = "cmbComPort"
			Me.cmbComPort.Size = New System.Drawing.Size(121, 34)
			' 
			' toolStripOpen
			' 
			Me.toolStripOpen.BackColor = System.Drawing.SystemColors.ButtonShadow
			Me.toolStripOpen.BackgroundImage = CType(resources.GetObject("toolStripOpen.BackgroundImage"), System.Drawing.Image)
			Me.toolStripOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
			Me.toolStripOpen.Checked = True
			Me.toolStripOpen.CheckState = System.Windows.Forms.CheckState.Checked
			Me.toolStripOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
			Me.toolStripOpen.Font = New System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.toolStripOpen.Image = CType(resources.GetObject("toolStripOpen.Image"), System.Drawing.Image)
			Me.toolStripOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
			Me.toolStripOpen.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.toolStripOpen.Name = "toolStripOpen"
			Me.toolStripOpen.Size = New System.Drawing.Size(60, 31)
			Me.toolStripOpen.Text = "  Open  "
			Me.toolStripOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.toolStripOpen.Click += new System.EventHandler(this.toolStripButton1_Click);
			' 
			' toolStripLabel1
			' 
			Me.toolStripLabel1.Name = "toolStripLabel1"
			Me.toolStripLabel1.Size = New System.Drawing.Size(80, 31)
			Me.toolStripLabel1.Text = "                  "
			' 
			' toolStripLabel5
			' 
			Me.toolStripLabel5.Name = "toolStripLabel5"
			Me.toolStripLabel5.Size = New System.Drawing.Size(80, 31)
			Me.toolStripLabel5.Text = "                  "
			' 
			' toolStripLabel3
			' 
			Me.toolStripLabel3.Name = "toolStripLabel3"
			Me.toolStripLabel3.Size = New System.Drawing.Size(65, 31)
			Me.toolStripLabel3.Text = "Language"
			' 
			' toolStripComboBox1
			' 
			Me.toolStripComboBox1.Name = "toolStripComboBox1"
			Me.toolStripComboBox1.Size = New System.Drawing.Size(121, 34)
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.toolStripComboBox1.Click += new System.EventHandler(this.toolStripComboBox1_Click);
			' 
			' statusStrip1
			' 
			Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.toolStripStatusLabel1})
			Me.statusStrip1.Location = New System.Drawing.Point(0, 680)
			Me.statusStrip1.Name = "statusStrip1"
			Me.statusStrip1.Size = New System.Drawing.Size(1290, 22)
			Me.statusStrip1.TabIndex = 4
			Me.statusStrip1.Text = "statusStrip1"
			' 
			' toolStripStatusLabel1
			' 
			Me.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control
			Me.toolStripStatusLabel1.Name = "toolStripStatusLabel1"
			Me.toolStripStatusLabel1.Size = New System.Drawing.Size(131, 17)
			Me.toolStripStatusLabel1.Text = "toolStripStatusLabel1"
			' 
			' panel1
			' 
			Me.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
			Me.panel1.Controls.Add(Me.btnConnect)
			Me.panel1.Controls.Add(Me.lblPort)
			Me.panel1.Controls.Add(Me.ipControl1)
			Me.panel1.Controls.Add(Me.label1)
			Me.panel1.Controls.Add(Me.txtPort)
			Me.panel1.Location = New System.Drawing.Point(200, 25)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(443, 34)
			Me.panel1.TabIndex = 6
			' 
			' btnConnect
			' 
			Me.btnConnect.BackColor = System.Drawing.SystemColors.Control
			Me.btnConnect.BackgroundImage = CType(resources.GetObject("btnConnect.BackgroundImage"), System.Drawing.Image)
			Me.btnConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
			Me.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
			Me.btnConnect.Font = New System.Drawing.Font("微软雅黑", 9F)
			Me.btnConnect.ForeColor = System.Drawing.Color.Black
			Me.btnConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
			Me.btnConnect.Location = New System.Drawing.Point(346, 1)
			Me.btnConnect.Name = "btnConnect"
			Me.btnConnect.Size = New System.Drawing.Size(60, 31)
			Me.btnConnect.TabIndex = 4
			Me.btnConnect.Text = "  Open  "
			Me.btnConnect.UseVisualStyleBackColor = False
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			' 
			' lblPort
			' 
			Me.lblPort.AutoSize = True
			Me.lblPort.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lblPort.Location = New System.Drawing.Point(247, 6)
			Me.lblPort.Name = "lblPort"
			Me.lblPort.Size = New System.Drawing.Size(40, 16)
			Me.lblPort.TabIndex = 1
			Me.lblPort.Text = "Port"
			' 
			' ipControl1
			' 
			Me.ipControl1.BackColor = System.Drawing.SystemColors.Control
			Me.ipControl1.IpData = New String() { "", "", "", ""}
			Me.ipControl1.Location = New System.Drawing.Point(35, 0)
			Me.ipControl1.Name = "ipControl1"
			Me.ipControl1.Size = New System.Drawing.Size(198, 34)
			Me.ipControl1.TabIndex = 3
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label1.Location = New System.Drawing.Point(5, 9)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(24, 16)
			Me.label1.TabIndex = 2
			Me.label1.Text = "IP"
			' 
			' txtPort
			' 
			Me.txtPort.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtPort.Location = New System.Drawing.Point(293, 3)
			Me.txtPort.MaxLength = 6
			Me.txtPort.Name = "txtPort"
			Me.txtPort.Size = New System.Drawing.Size(47, 26)
			Me.txtPort.TabIndex = 0
			' 
			' plUsb
			' 
			Me.plUsb.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
			Me.plUsb.Controls.Add(Me.btnUsbOpen)
			Me.plUsb.Location = New System.Drawing.Point(203, 25)
			Me.plUsb.Name = "plUsb"
			Me.plUsb.Size = New System.Drawing.Size(443, 34)
			Me.plUsb.TabIndex = 7
			' 
			' btnUsbOpen
			' 
			Me.btnUsbOpen.BackColor = System.Drawing.SystemColors.Control
			Me.btnUsbOpen.BackgroundImage = CType(resources.GetObject("btnUsbOpen.BackgroundImage"), System.Drawing.Image)
			Me.btnUsbOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
			Me.btnUsbOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
			Me.btnUsbOpen.Font = New System.Drawing.Font("微软雅黑", 9F)
			Me.btnUsbOpen.ForeColor = System.Drawing.Color.Black
			Me.btnUsbOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
			Me.btnUsbOpen.Location = New System.Drawing.Point(18, 1)
			Me.btnUsbOpen.Name = "btnUsbOpen"
			Me.btnUsbOpen.Size = New System.Drawing.Size(60, 31)
			Me.btnUsbOpen.TabIndex = 5
			Me.btnUsbOpen.Text = "  Open  "
			Me.btnUsbOpen.UseVisualStyleBackColor = False
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnUsbOpen.Click += new System.EventHandler(this.btnUsbOpen_Click);
			' 
			' MainForm2
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.Color.Black
			Me.ClientSize = New System.Drawing.Size(1290, 702)
			Me.Controls.Add(Me.plUsb)
			Me.Controls.Add(Me.panel1)
			Me.Controls.Add(Me.toolStrip1)
			Me.Controls.Add(Me.statusStrip1)
			Me.Controls.Add(Me.menuStrip1)
			Me.MainMenuStrip = Me.menuStrip1
			Me.Name = "MainForm2"
			Me.Text = "UHF(1.2.6)"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.Form1_Load);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			Me.menuStrip1.ResumeLayout(False)
			Me.menuStrip1.PerformLayout()
			Me.toolStrip1.ResumeLayout(False)
			Me.toolStrip1.PerformLayout()
			Me.statusStrip1.ResumeLayout(False)
			Me.statusStrip1.PerformLayout()
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.plUsb.ResumeLayout(False)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private WithEvents menuStrip1 As System.Windows.Forms.MenuStrip
		Private WithEvents MenuItemScanEPC As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents MenuItemReadWriteTag As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents configToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStrip1 As System.Windows.Forms.ToolStrip
		Private lblPortName As System.Windows.Forms.ToolStripLabel
		Private cmbComPort As System.Windows.Forms.ToolStripComboBox
		Private WithEvents toolStripOpen As System.Windows.Forms.ToolStripButton
		Private statusStrip1 As System.Windows.Forms.StatusStrip
		Private WithEvents uHFVersionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents killLockToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents toolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripLabel2 As System.Windows.Forms.ToolStripLabel
		Private WithEvents toolStripComboBox1 As System.Windows.Forms.ToolStripComboBox
		Private toolStripLabel3 As System.Windows.Forms.ToolStripLabel
		Private toolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
		Private toolStripLabel4 As System.Windows.Forms.ToolStripLabel
		Private WithEvents combCommunicationMode As System.Windows.Forms.ToolStripComboBox
		Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
		Private toolStripLabel5 As System.Windows.Forms.ToolStripLabel
		Private panel1 As System.Windows.Forms.Panel
		Private lblPort As System.Windows.Forms.Label
		Private txtPort As System.Windows.Forms.TextBox
		Private ipControl1 As WindowsFormsControlLibrary1.IPControl
		Private label1 As System.Windows.Forms.Label
		Private toolStripLabel1 As System.Windows.Forms.ToolStripLabel
		Private WithEvents btnConnect As System.Windows.Forms.Button
		Private WithEvents MenuItemReceiveEPC As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents testToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private plUsb As System.Windows.Forms.Panel
		Private WithEvents btnUsbOpen As System.Windows.Forms.Button
		Private WithEvents ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

	End Class
End Namespace

