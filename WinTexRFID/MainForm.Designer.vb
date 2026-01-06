Namespace UHFAPP
	Partial Public Class MainForm
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
            Me.menuStrip1 = New System.Windows.Forms.MenuStrip()
            Me.MenuItemScanEPC = New System.Windows.Forms.ToolStripMenuItem()
            Me.MenuItemReadWriteTag = New System.Windows.Forms.ToolStripMenuItem()
            Me.configToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.killLockToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.uHFVersionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.toolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
            Me.MenuItemReceiveEPC = New System.Windows.Forms.ToolStripMenuItem()
            Me.testToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.uHFUpgradeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.SetR3ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.MultiUR4ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.加密传输ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.hFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.认证ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.hIDModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
            Me.toolStripLabel4 = New System.Windows.Forms.ToolStripLabel()
            Me.combCommunicationMode = New System.Windows.Forms.ToolStripComboBox()
            Me.toolStripOpen = New System.Windows.Forms.ToolStripButton()
            Me.toolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
            Me.lblPortName = New System.Windows.Forms.ToolStripLabel()
            Me.cmbComPort = New System.Windows.Forms.ToolStripComboBox()
            Me.toolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
            Me.toolStripLabel5 = New System.Windows.Forms.ToolStripLabel()
            Me.toolStripLabel6 = New System.Windows.Forms.ToolStripLabel()
            Me.toolStripLabel8 = New System.Windows.Forms.ToolStripLabel()
            Me.toolStripLabel7 = New System.Windows.Forms.ToolStripLabel()
            Me.toolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
            Me.toolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
            Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
            Me.toolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.lblPort = New System.Windows.Forms.Label()
            Me.ipControl1 = New WindowsFormsControlLibrary1.IPControl()
            Me.label1 = New System.Windows.Forms.Label()
            Me.txtPort = New System.Windows.Forms.TextBox()
            Me.lvDevcies = New System.Windows.Forms.ListView()
            Me.columnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.columnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.columnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.button1 = New System.Windows.Forms.Button()
            Me.btnSearch = New System.Windows.Forms.Button()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.label2 = New System.Windows.Forms.Label()
            Me.menuStrip1.SuspendLayout()
            Me.toolStrip1.SuspendLayout()
            Me.statusStrip1.SuspendLayout()
            Me.panel1.SuspendLayout()
            Me.panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'menuStrip1
            '
            Me.menuStrip1.BackColor = System.Drawing.Color.LightSteelBlue
            Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemScanEPC, Me.MenuItemReadWriteTag, Me.configToolStripMenuItem, Me.killLockToolStripMenuItem, Me.uHFVersionToolStripMenuItem, Me.toolStripMenuItem1, Me.MenuItemReceiveEPC, Me.testToolStripMenuItem, Me.uHFUpgradeToolStripMenuItem, Me.SetR3ToolStripMenuItem, Me.MultiUR4ToolStripMenuItem, Me.加密传输ToolStripMenuItem, Me.hFToolStripMenuItem, Me.认证ToolStripMenuItem, Me.hIDModeToolStripMenuItem})
            Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
            Me.menuStrip1.Name = "menuStrip1"
            Me.menuStrip1.Size = New System.Drawing.Size(1634, 24)
            Me.menuStrip1.TabIndex = 2
            Me.menuStrip1.Text = "ScanEPC"
            '
            'MenuItemScanEPC
            '
            Me.MenuItemScanEPC.Name = "MenuItemScanEPC"
            Me.MenuItemScanEPC.Size = New System.Drawing.Size(66, 20)
            Me.MenuItemScanEPC.Text = "ReadEPC"
            '
            'MenuItemReadWriteTag
            '
            Me.MenuItemReadWriteTag.Name = "MenuItemReadWriteTag"
            Me.MenuItemReadWriteTag.Size = New System.Drawing.Size(91, 20)
            Me.MenuItemReadWriteTag.Text = "ReadWriteTag"
            '
            'configToolStripMenuItem
            '
            Me.configToolStripMenuItem.Name = "configToolStripMenuItem"
            Me.configToolStripMenuItem.Size = New System.Drawing.Size(93, 20)
            Me.configToolStripMenuItem.Text = "Configuration"
            '
            'killLockToolStripMenuItem
            '
            Me.killLockToolStripMenuItem.Name = "killLockToolStripMenuItem"
            Me.killLockToolStripMenuItem.Size = New System.Drawing.Size(65, 20)
            Me.killLockToolStripMenuItem.Text = "Kill-Lock"
            '
            'uHFVersionToolStripMenuItem
            '
            Me.uHFVersionToolStripMenuItem.Name = "uHFVersionToolStripMenuItem"
            Me.uHFVersionToolStripMenuItem.Size = New System.Drawing.Size(66, 20)
            Me.uHFVersionToolStripMenuItem.Text = "UHF Info"
            '
            'toolStripMenuItem1
            '
            Me.toolStripMenuItem1.Name = "toolStripMenuItem1"
            Me.toolStripMenuItem1.Size = New System.Drawing.Size(85, 20)
            Me.toolStripMenuItem1.Text = "Temperature"
            '
            'MenuItemReceiveEPC
            '
            Me.MenuItemReceiveEPC.Name = "MenuItemReceiveEPC"
            Me.MenuItemReceiveEPC.Size = New System.Drawing.Size(108, 20)
            Me.MenuItemReceiveEPC.Text = "UDP-ReceiveEPC"
            '
            'testToolStripMenuItem
            '
            Me.testToolStripMenuItem.Name = "testToolStripMenuItem"
            Me.testToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
            Me.testToolStripMenuItem.Text = "测试"
            Me.testToolStripMenuItem.Visible = False
            '
            'uHFUpgradeToolStripMenuItem
            '
            Me.uHFUpgradeToolStripMenuItem.Enabled = False
            Me.uHFUpgradeToolStripMenuItem.Name = "uHFUpgradeToolStripMenuItem"
            Me.uHFUpgradeToolStripMenuItem.Size = New System.Drawing.Size(90, 20)
            Me.uHFUpgradeToolStripMenuItem.Text = "UHF Upgrade"
            '
            'SetR3ToolStripMenuItem
            '
            Me.SetR3ToolStripMenuItem.Name = "SetR3ToolStripMenuItem"
            Me.SetR3ToolStripMenuItem.Size = New System.Drawing.Size(82, 20)
            Me.SetR3ToolStripMenuItem.Text = "User Setting"
            '
            'MultiUR4ToolStripMenuItem
            '
            Me.MultiUR4ToolStripMenuItem.Name = "MultiUR4ToolStripMenuItem"
            Me.MultiUR4ToolStripMenuItem.Size = New System.Drawing.Size(89, 20)
            Me.MultiUR4ToolStripMenuItem.Text = "连接多台UR4"
            Me.MultiUR4ToolStripMenuItem.Visible = False
            '
            '加密传输ToolStripMenuItem
            '
            Me.加密传输ToolStripMenuItem.Name = "加密传输ToolStripMenuItem"
            Me.加密传输ToolStripMenuItem.Size = New System.Drawing.Size(69, 20)
            Me.加密传输ToolStripMenuItem.Text = "加密传输"
            Me.加密传输ToolStripMenuItem.Visible = False
            '
            'hFToolStripMenuItem
            '
            Me.hFToolStripMenuItem.Name = "hFToolStripMenuItem"
            Me.hFToolStripMenuItem.Size = New System.Drawing.Size(34, 20)
            Me.hFToolStripMenuItem.Text = "HF"
            '
            '认证ToolStripMenuItem
            '
            Me.认证ToolStripMenuItem.Name = "认证ToolStripMenuItem"
            Me.认证ToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
            Me.认证ToolStripMenuItem.Text = "认证"
            Me.认证ToolStripMenuItem.Visible = False
            '
            'hIDModeToolStripMenuItem
            '
            Me.hIDModeToolStripMenuItem.Enabled = False
            Me.hIDModeToolStripMenuItem.Name = "hIDModeToolStripMenuItem"
            Me.hIDModeToolStripMenuItem.Size = New System.Drawing.Size(73, 20)
            Me.hIDModeToolStripMenuItem.Text = "HID Mode"
            '
            'toolStrip1
            '
            Me.toolStrip1.AutoSize = False
            Me.toolStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
            Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripLabel4, Me.combCommunicationMode, Me.toolStripOpen, Me.toolStripLabel2, Me.lblPortName, Me.cmbComPort, Me.toolStripLabel1, Me.toolStripLabel5, Me.toolStripLabel6, Me.toolStripLabel8, Me.toolStripLabel7, Me.toolStripLabel3, Me.toolStripComboBox1})
            Me.toolStrip1.Location = New System.Drawing.Point(0, 24)
            Me.toolStrip1.Name = "toolStrip1"
            Me.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
            Me.toolStrip1.Size = New System.Drawing.Size(1634, 37)
            Me.toolStrip1.TabIndex = 5
            Me.toolStrip1.Text = "Open"
            '
            'toolStripLabel4
            '
            Me.toolStripLabel4.Name = "toolStripLabel4"
            Me.toolStripLabel4.Size = New System.Drawing.Size(55, 34)
            Me.toolStripLabel4.Text = "通信方式"
            '
            'combCommunicationMode
            '
            Me.combCommunicationMode.Items.AddRange(New Object() {"串口", "网络"})
            Me.combCommunicationMode.Name = "combCommunicationMode"
            Me.combCommunicationMode.Size = New System.Drawing.Size(121, 37)
            '
            'toolStripOpen
            '
            Me.toolStripOpen.BackColor = System.Drawing.SystemColors.ButtonShadow
            Me.toolStripOpen.BackgroundImage = CType(resources.GetObject("toolStripOpen.BackgroundImage"), System.Drawing.Image)
            Me.toolStripOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.toolStripOpen.Checked = True
            Me.toolStripOpen.CheckState = System.Windows.Forms.CheckState.Checked
            Me.toolStripOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.toolStripOpen.Font = New System.Drawing.Font("Microsoft YaHei", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
            Me.toolStripOpen.Image = CType(resources.GetObject("toolStripOpen.Image"), System.Drawing.Image)
            Me.toolStripOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
            Me.toolStripOpen.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.toolStripOpen.Name = "toolStripOpen"
            Me.toolStripOpen.Size = New System.Drawing.Size(60, 34)
            Me.toolStripOpen.Text = "  Open  "
            Me.toolStripOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
            '
            'toolStripLabel2
            '
            Me.toolStripLabel2.Name = "toolStripLabel2"
            Me.toolStripLabel2.Size = New System.Drawing.Size(61, 34)
            Me.toolStripLabel2.Text = "                  "
            '
            'lblPortName
            '
            Me.lblPortName.Name = "lblPortName"
            Me.lblPortName.Size = New System.Drawing.Size(35, 34)
            Me.lblPortName.Text = "COM"
            '
            'cmbComPort
            '
            Me.cmbComPort.Name = "cmbComPort"
            Me.cmbComPort.Size = New System.Drawing.Size(121, 37)
            '
            'toolStripLabel1
            '
            Me.toolStripLabel1.Name = "toolStripLabel1"
            Me.toolStripLabel1.Size = New System.Drawing.Size(61, 34)
            Me.toolStripLabel1.Text = "                  "
            '
            'toolStripLabel5
            '
            Me.toolStripLabel5.Name = "toolStripLabel5"
            Me.toolStripLabel5.Size = New System.Drawing.Size(25, 34)
            Me.toolStripLabel5.Text = "      "
            '
            'toolStripLabel6
            '
            Me.toolStripLabel6.Name = "toolStripLabel6"
            Me.toolStripLabel6.Size = New System.Drawing.Size(0, 34)
            '
            'toolStripLabel8
            '
            Me.toolStripLabel8.Name = "toolStripLabel8"
            Me.toolStripLabel8.Size = New System.Drawing.Size(40, 34)
            Me.toolStripLabel8.Text = "           "
            '
            'toolStripLabel7
            '
            Me.toolStripLabel7.Name = "toolStripLabel7"
            Me.toolStripLabel7.Size = New System.Drawing.Size(322, 34)
            Me.toolStripLabel7.Text = "                                                                                 " &
    "                        "
            '
            'toolStripLabel3
            '
            Me.toolStripLabel3.Name = "toolStripLabel3"
            Me.toolStripLabel3.Size = New System.Drawing.Size(59, 34)
            Me.toolStripLabel3.Text = "Language"
            '
            'toolStripComboBox1
            '
            Me.toolStripComboBox1.Name = "toolStripComboBox1"
            Me.toolStripComboBox1.Size = New System.Drawing.Size(121, 37)
            '
            'statusStrip1
            '
            Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripStatusLabel1})
            Me.statusStrip1.Location = New System.Drawing.Point(0, 739)
            Me.statusStrip1.Name = "statusStrip1"
            Me.statusStrip1.Size = New System.Drawing.Size(1634, 22)
            Me.statusStrip1.TabIndex = 4
            Me.statusStrip1.Text = "statusStrip1"
            '
            'toolStripStatusLabel1
            '
            Me.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control
            Me.toolStripStatusLabel1.Name = "toolStripStatusLabel1"
            Me.toolStripStatusLabel1.Size = New System.Drawing.Size(118, 17)
            Me.toolStripStatusLabel1.Text = "toolStripStatusLabel1"
            '
            'panel1
            '
            Me.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
            Me.panel1.Controls.Add(Me.lblPort)
            Me.panel1.Controls.Add(Me.ipControl1)
            Me.panel1.Controls.Add(Me.label1)
            Me.panel1.Controls.Add(Me.txtPort)
            Me.panel1.Location = New System.Drawing.Point(274, 26)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(344, 37)
            Me.panel1.TabIndex = 8
            '
            'lblPort
            '
            Me.lblPort.AutoSize = True
            Me.lblPort.Font = New System.Drawing.Font("SimSun", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
            Me.lblPort.Location = New System.Drawing.Point(247, 7)
            Me.lblPort.Name = "lblPort"
            Me.lblPort.Size = New System.Drawing.Size(40, 16)
            Me.lblPort.TabIndex = 1
            Me.lblPort.Text = "Port"
            '
            'ipControl1
            '
            Me.ipControl1.BackColor = System.Drawing.SystemColors.Control
            Me.ipControl1.IpData = New String() {"", "", "", ""}
            Me.ipControl1.Location = New System.Drawing.Point(35, 0)
            Me.ipControl1.Name = "ipControl1"
            Me.ipControl1.Size = New System.Drawing.Size(198, 37)
            Me.ipControl1.TabIndex = 3
            '
            'label1
            '
            Me.label1.AutoSize = True
            Me.label1.Font = New System.Drawing.Font("SimSun", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
            Me.label1.Location = New System.Drawing.Point(5, 10)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(24, 16)
            Me.label1.TabIndex = 2
            Me.label1.Text = "IP"
            '
            'txtPort
            '
            Me.txtPort.Font = New System.Drawing.Font("SimSun", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
            Me.txtPort.Location = New System.Drawing.Point(293, 3)
            Me.txtPort.MaxLength = 6
            Me.txtPort.Name = "txtPort"
            Me.txtPort.Size = New System.Drawing.Size(47, 26)
            Me.txtPort.TabIndex = 0
            '
            'lvDevcies
            '
            Me.lvDevcies.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2, Me.columnHeader3})
            Me.lvDevcies.FullRowSelect = True
            Me.lvDevcies.HideSelection = False
            Me.lvDevcies.Location = New System.Drawing.Point(3, 23)
            Me.lvDevcies.Name = "lvDevcies"
            Me.lvDevcies.Size = New System.Drawing.Size(297, 498)
            Me.lvDevcies.TabIndex = 42
            Me.lvDevcies.UseCompatibleStateImageBehavior = False
            Me.lvDevcies.View = System.Windows.Forms.View.Details
            '
            'columnHeader1
            '
            Me.columnHeader1.Text = "ID"
            Me.columnHeader1.Width = 30
            '
            'columnHeader2
            '
            Me.columnHeader2.Text = "IP"
            Me.columnHeader2.Width = 130
            '
            'columnHeader3
            '
            Me.columnHeader3.Text = "MAC"
            Me.columnHeader3.Width = 127
            '
            'button1
            '
            Me.button1.Font = New System.Drawing.Font("SimSun", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
            Me.button1.Location = New System.Drawing.Point(30, 545)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(174, 52)
            Me.button1.TabIndex = 44
            Me.button1.Text = "Clear"
            Me.button1.UseVisualStyleBackColor = True
            '
            'btnSearch
            '
            Me.btnSearch.Font = New System.Drawing.Font("SimSun", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
            Me.btnSearch.Location = New System.Drawing.Point(67, 570)
            Me.btnSearch.Name = "btnSearch"
            Me.btnSearch.Size = New System.Drawing.Size(93, 52)
            Me.btnSearch.TabIndex = 43
            Me.btnSearch.Text = "开始搜索"
            Me.btnSearch.UseVisualStyleBackColor = True
            Me.btnSearch.Visible = False
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.label2)
            Me.panel2.Controls.Add(Me.button1)
            Me.panel2.Controls.Add(Me.btnSearch)
            Me.panel2.Controls.Add(Me.lvDevcies)
            Me.panel2.Location = New System.Drawing.Point(0, 67)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(303, 666)
            Me.panel2.TabIndex = 44
            '
            'label2
            '
            Me.label2.AutoSize = True
            Me.label2.Font = New System.Drawing.Font("SimSun", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
            Me.label2.Location = New System.Drawing.Point(64, 2)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(104, 16)
            Me.label2.TabIndex = 45
            Me.label2.Text = "Devices List"
            '
            'MainForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.SystemColors.InactiveCaption
            Me.ClientSize = New System.Drawing.Size(1634, 761)
            Me.Controls.Add(Me.panel2)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.toolStrip1)
            Me.Controls.Add(Me.statusStrip1)
            Me.Controls.Add(Me.menuStrip1)
            Me.KeyPreview = True
            Me.MainMenuStrip = Me.menuStrip1
            Me.Name = "MainForm"
            Me.Text = "UHF(1.3.8)"
            Me.menuStrip1.ResumeLayout(False)
            Me.menuStrip1.PerformLayout()
            Me.toolStrip1.ResumeLayout(False)
            Me.toolStrip1.PerformLayout()
            Me.statusStrip1.ResumeLayout(False)
            Me.statusStrip1.PerformLayout()
            Me.panel1.ResumeLayout(False)
            Me.panel1.PerformLayout()
            Me.panel2.ResumeLayout(False)
            Me.panel2.PerformLayout()
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
		Private toolStripLabel5 As System.Windows.Forms.ToolStripLabel
		Private toolStripLabel1 As System.Windows.Forms.ToolStripLabel
		Private WithEvents MenuItemReceiveEPC As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents testToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents uHFUpgradeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripLabel6 As System.Windows.Forms.ToolStripLabel
		Private toolStripLabel7 As System.Windows.Forms.ToolStripLabel
		Private panel1 As System.Windows.Forms.Panel
		Private lblPort As System.Windows.Forms.Label
		Private ipControl1 As WindowsFormsControlLibrary1.IPControl
		Private label1 As System.Windows.Forms.Label
		Private txtPort As System.Windows.Forms.TextBox
		Private toolStripLabel8 As System.Windows.Forms.ToolStripLabel
		Private WithEvents MultiUR4ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents SetR3ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents 加密传输ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents hFToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents 认证ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents hIDModeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents lvDevcies As System.Windows.Forms.ListView
		Private columnHeader1 As System.Windows.Forms.ColumnHeader
		Private columnHeader2 As System.Windows.Forms.ColumnHeader
		Private columnHeader3 As System.Windows.Forms.ColumnHeader
		Private WithEvents btnSearch As System.Windows.Forms.Button
		Private WithEvents button1 As System.Windows.Forms.Button
		Private panel2 As System.Windows.Forms.Panel
		Private label2 As System.Windows.Forms.Label

	End Class
End Namespace

