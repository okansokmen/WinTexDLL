Namespace UHFAPP.multidevice
	Partial Public Class MainForm
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
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.btnStart1 = New System.Windows.Forms.Button()
			Me.txtPort = New System.Windows.Forms.TextBox()
			Me.btnDisConn = New System.Windows.Forms.Button()
			Me.label2 = New System.Windows.Forms.Label()
			Me.btnConn = New System.Windows.Forms.Button()
			Me.label1 = New System.Windows.Forms.Label()
			Me.txtIP = New System.Windows.Forms.TextBox()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.btnStart2 = New System.Windows.Forms.Button()
			Me.btnDisConn2 = New System.Windows.Forms.Button()
			Me.txtPort2 = New System.Windows.Forms.TextBox()
			Me.btnConn2 = New System.Windows.Forms.Button()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.txtIP2 = New System.Windows.Forms.TextBox()
			Me.lvEPC = New System.Windows.Forms.ListView()
			Me.columnHeaderID = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderEPC = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderRssi = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderCount = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderANT = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderIP = New System.Windows.Forms.ColumnHeader()
			Me.groupBox3 = New System.Windows.Forms.GroupBox()
			Me.groupBox6 = New System.Windows.Forms.GroupBox()
			Me.cbPower = New System.Windows.Forms.CheckBox()
			Me.cmbPower = New System.Windows.Forms.ComboBox()
			Me.label24 = New System.Windows.Forms.Label()
			Me.btnPowerGet = New System.Windows.Forms.Button()
			Me.label23 = New System.Windows.Forms.Label()
			Me.btnPowerSet = New System.Windows.Forms.Button()
			Me.lblCount = New System.Windows.Forms.Label()
			Me.button1 = New System.Windows.Forms.Button()
			Me.groupBox1.SuspendLayout()
			Me.groupBox2.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			Me.groupBox6.SuspendLayout()
			Me.SuspendLayout()
			' 
			' groupBox1
			' 
			Me.groupBox1.BackColor = System.Drawing.SystemColors.Control
			Me.groupBox1.Controls.Add(Me.btnStart1)
			Me.groupBox1.Controls.Add(Me.txtPort)
			Me.groupBox1.Controls.Add(Me.btnDisConn)
			Me.groupBox1.Controls.Add(Me.label2)
			Me.groupBox1.Controls.Add(Me.btnConn)
			Me.groupBox1.Controls.Add(Me.label1)
			Me.groupBox1.Controls.Add(Me.txtIP)
			Me.groupBox1.Location = New System.Drawing.Point(12, 12)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(530, 54)
			Me.groupBox1.TabIndex = 4
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "IP-1"
			' 
			' btnStart1
			' 
			Me.btnStart1.Enabled = False
			Me.btnStart1.Location = New System.Drawing.Point(459, 13)
			Me.btnStart1.Name = "btnStart1"
			Me.btnStart1.Size = New System.Drawing.Size(51, 35)
			Me.btnStart1.TabIndex = 11
			Me.btnStart1.Text = "Start"
			Me.btnStart1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnStart1.Click += new System.EventHandler(this.btnStart1_Click);
			' 
			' txtPort
			' 
			Me.txtPort.Location = New System.Drawing.Point(220, 20)
			Me.txtPort.Name = "txtPort"
			Me.txtPort.Size = New System.Drawing.Size(70, 21)
			Me.txtPort.TabIndex = 7
			Me.txtPort.Text = "8002"
			' 
			' btnDisConn
			' 
			Me.btnDisConn.Enabled = False
			Me.btnDisConn.Location = New System.Drawing.Point(377, 13)
			Me.btnDisConn.Name = "btnDisConn"
			Me.btnDisConn.Size = New System.Drawing.Size(75, 35)
			Me.btnDisConn.TabIndex = 10
			Me.btnDisConn.Text = "DisConnect"
			Me.btnDisConn.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnDisConn.Click += new System.EventHandler(this.btnDisConn_Click);
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(185, 23)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(29, 12)
			Me.label2.TabIndex = 6
			Me.label2.Text = "Port"
			' 
			' btnConn
			' 
			Me.btnConn.Location = New System.Drawing.Point(296, 13)
			Me.btnConn.Name = "btnConn"
			Me.btnConn.Size = New System.Drawing.Size(75, 35)
			Me.btnConn.TabIndex = 9
			Me.btnConn.Text = "Connect"
			Me.btnConn.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(7, 23)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(17, 12)
			Me.label1.TabIndex = 5
			Me.label1.Text = "IP"
			' 
			' txtIP
			' 
			Me.txtIP.Location = New System.Drawing.Point(30, 20)
			Me.txtIP.Name = "txtIP"
			Me.txtIP.Size = New System.Drawing.Size(124, 21)
			Me.txtIP.TabIndex = 4
			Me.txtIP.Text = "192.168.99.66"
			' 
			' groupBox2
			' 
			Me.groupBox2.BackColor = System.Drawing.SystemColors.Control
			Me.groupBox2.Controls.Add(Me.btnStart2)
			Me.groupBox2.Controls.Add(Me.btnDisConn2)
			Me.groupBox2.Controls.Add(Me.txtPort2)
			Me.groupBox2.Controls.Add(Me.btnConn2)
			Me.groupBox2.Controls.Add(Me.label3)
			Me.groupBox2.Controls.Add(Me.label4)
			Me.groupBox2.Controls.Add(Me.txtIP2)
			Me.groupBox2.Location = New System.Drawing.Point(565, 12)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(541, 54)
			Me.groupBox2.TabIndex = 8
			Me.groupBox2.TabStop = False
			Me.groupBox2.Text = "IP-2"
			' 
			' btnStart2
			' 
			Me.btnStart2.Enabled = False
			Me.btnStart2.Location = New System.Drawing.Point(459, 13)
			Me.btnStart2.Name = "btnStart2"
			Me.btnStart2.Size = New System.Drawing.Size(60, 35)
			Me.btnStart2.TabIndex = 13
			Me.btnStart2.Text = "Start"
			Me.btnStart2.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnStart2.Click += new System.EventHandler(this.btnStart2_Click);
			' 
			' btnDisConn2
			' 
			Me.btnDisConn2.Enabled = False
			Me.btnDisConn2.Location = New System.Drawing.Point(377, 13)
			Me.btnDisConn2.Name = "btnDisConn2"
			Me.btnDisConn2.Size = New System.Drawing.Size(75, 35)
			Me.btnDisConn2.TabIndex = 12
			Me.btnDisConn2.Text = "DisConnect"
			Me.btnDisConn2.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnDisConn2.Click += new System.EventHandler(this.btnDisConn2_Click);
			' 
			' txtPort2
			' 
			Me.txtPort2.Location = New System.Drawing.Point(220, 20)
			Me.txtPort2.Name = "txtPort2"
			Me.txtPort2.Size = New System.Drawing.Size(70, 21)
			Me.txtPort2.TabIndex = 7
			Me.txtPort2.Text = "8888"
			' 
			' btnConn2
			' 
			Me.btnConn2.Location = New System.Drawing.Point(296, 13)
			Me.btnConn2.Name = "btnConn2"
			Me.btnConn2.Size = New System.Drawing.Size(75, 35)
			Me.btnConn2.TabIndex = 11
			Me.btnConn2.Text = "Connect"
			Me.btnConn2.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnConn2.Click += new System.EventHandler(this.btnConn2_Click);
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(185, 23)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(29, 12)
			Me.label3.TabIndex = 6
			Me.label3.Text = "Port"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(7, 23)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(17, 12)
			Me.label4.TabIndex = 5
			Me.label4.Text = "IP"
			' 
			' txtIP2
			' 
			Me.txtIP2.Location = New System.Drawing.Point(30, 20)
			Me.txtIP2.Name = "txtIP2"
			Me.txtIP2.Size = New System.Drawing.Size(124, 21)
			Me.txtIP2.TabIndex = 4
			Me.txtIP2.Text = "192.168.99.203"
			' 
			' lvEPC
			' 
			Me.lvEPC.BackColor = System.Drawing.SystemColors.ControlLightLight
			Me.lvEPC.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.columnHeaderID, Me.columnHeaderEPC, Me.columnHeaderRssi, Me.columnHeaderCount, Me.columnHeaderANT, Me.columnHeaderIP})
			Me.lvEPC.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lvEPC.FullRowSelect = True
			Me.lvEPC.Location = New System.Drawing.Point(12, 72)
			Me.lvEPC.Name = "lvEPC"
			Me.lvEPC.Size = New System.Drawing.Size(835, 401)
			Me.lvEPC.TabIndex = 11
			Me.lvEPC.UseCompatibleStateImageBehavior = False
			Me.lvEPC.View = System.Windows.Forms.View.Details
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
			' columnHeaderIP
			' 
			Me.columnHeaderIP.Text = "IP"
			Me.columnHeaderIP.Width = 180
			' 
			' groupBox3
			' 
			Me.groupBox3.Controls.Add(Me.groupBox6)
			Me.groupBox3.Location = New System.Drawing.Point(857, 72)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(277, 401)
			Me.groupBox3.TabIndex = 15
			Me.groupBox3.TabStop = False
			Me.groupBox3.Text = "Settings"
			' 
			' groupBox6
			' 
			Me.groupBox6.BackColor = System.Drawing.SystemColors.Control
			Me.groupBox6.Controls.Add(Me.cbPower)
			Me.groupBox6.Controls.Add(Me.cmbPower)
			Me.groupBox6.Controls.Add(Me.label24)
			Me.groupBox6.Controls.Add(Me.btnPowerGet)
			Me.groupBox6.Controls.Add(Me.label23)
			Me.groupBox6.Controls.Add(Me.btnPowerSet)
			Me.groupBox6.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox6.ForeColor = System.Drawing.Color.Black
			Me.groupBox6.Location = New System.Drawing.Point(16, 20)
			Me.groupBox6.Name = "groupBox6"
			Me.groupBox6.Size = New System.Drawing.Size(249, 92)
			Me.groupBox6.TabIndex = 32
			Me.groupBox6.TabStop = False
			Me.groupBox6.Text = "Power"
			' 
			' cbPower
			' 
			Me.cbPower.AutoSize = True
			Me.cbPower.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbPower.Location = New System.Drawing.Point(185, 49)
			Me.cbPower.Name = "cbPower"
			Me.cbPower.Size = New System.Drawing.Size(48, 16)
			Me.cbPower.TabIndex = 26
			Me.cbPower.Text = "Save"
			Me.cbPower.UseVisualStyleBackColor = True
			' 
			' cmbPower
			' 
			Me.cmbPower.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbPower.FormattingEnabled = True
			Me.cmbPower.Items.AddRange(New Object() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"})
			Me.cmbPower.Location = New System.Drawing.Point(47, 14)
			Me.cmbPower.Name = "cmbPower"
			Me.cmbPower.Size = New System.Drawing.Size(104, 20)
			Me.cmbPower.TabIndex = 6
			' 
			' label24
			' 
			Me.label24.AutoSize = True
			Me.label24.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label24.Location = New System.Drawing.Point(6, 17)
			Me.label24.Name = "label24"
			Me.label24.Size = New System.Drawing.Size(41, 12)
			Me.label24.TabIndex = 14
			Me.label24.Text = "Power:"
			' 
			' btnPowerGet
			' 
			Me.btnPowerGet.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnPowerGet.ForeColor = System.Drawing.Color.Black
			Me.btnPowerGet.Location = New System.Drawing.Point(39, 49)
			Me.btnPowerGet.Name = "btnPowerGet"
			Me.btnPowerGet.Size = New System.Drawing.Size(70, 31)
			Me.btnPowerGet.TabIndex = 13
			Me.btnPowerGet.Text = "Get"
			Me.btnPowerGet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnPowerGet.Click += new System.EventHandler(this.btnPowerGet_Click);
			' 
			' label23
			' 
			Me.label23.AutoSize = True
			Me.label23.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label23.Location = New System.Drawing.Point(172, 17)
			Me.label23.Name = "label23"
			Me.label23.Size = New System.Drawing.Size(23, 12)
			Me.label23.TabIndex = 12
			Me.label23.Text = "dBm"
			' 
			' btnPowerSet
			' 
			Me.btnPowerSet.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnPowerSet.ForeColor = System.Drawing.Color.Black
			Me.btnPowerSet.Location = New System.Drawing.Point(115, 49)
			Me.btnPowerSet.Name = "btnPowerSet"
			Me.btnPowerSet.Size = New System.Drawing.Size(64, 31)
			Me.btnPowerSet.TabIndex = 11
			Me.btnPowerSet.Text = "Set"
			Me.btnPowerSet.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnPowerSet.Click += new System.EventHandler(this.btnPowerSet_Click);
			' 
			' lblCount
			' 
			Me.lblCount.AutoSize = True
			Me.lblCount.Location = New System.Drawing.Point(387, 490)
			Me.lblCount.Name = "lblCount"
			Me.lblCount.Size = New System.Drawing.Size(11, 12)
			Me.lblCount.TabIndex = 16
			Me.lblCount.Text = "0"
			' 
			' button1
			' 
			Me.button1.Location = New System.Drawing.Point(447, 485)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(75, 23)
			Me.button1.TabIndex = 17
			Me.button1.Text = "清空"
			Me.button1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button1.Click += new System.EventHandler(this.button1_Click);
			' 
			' MainForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(1145, 542)
			Me.Controls.Add(Me.button1)
			Me.Controls.Add(Me.lblCount)
			Me.Controls.Add(Me.groupBox3)
			Me.Controls.Add(Me.lvEPC)
			Me.Controls.Add(Me.groupBox2)
			Me.Controls.Add(Me.groupBox1)
			Me.Name = "MainForm"
			Me.Text = "MainForm"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox6.ResumeLayout(False)
			Me.groupBox6.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private groupBox1 As System.Windows.Forms.GroupBox
		Private txtPort As System.Windows.Forms.TextBox
		Private label2 As System.Windows.Forms.Label
		Private label1 As System.Windows.Forms.Label
		Private txtIP As System.Windows.Forms.TextBox
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private txtPort2 As System.Windows.Forms.TextBox
		Private label3 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private txtIP2 As System.Windows.Forms.TextBox
		Private WithEvents btnConn As System.Windows.Forms.Button
		Private WithEvents btnDisConn As System.Windows.Forms.Button
		Private WithEvents btnDisConn2 As System.Windows.Forms.Button
		Private WithEvents btnConn2 As System.Windows.Forms.Button
		Private lvEPC As System.Windows.Forms.ListView
		Private columnHeaderID As System.Windows.Forms.ColumnHeader
		Private columnHeaderEPC As System.Windows.Forms.ColumnHeader
		Private columnHeaderRssi As System.Windows.Forms.ColumnHeader
		Private columnHeaderCount As System.Windows.Forms.ColumnHeader
		Private columnHeaderANT As System.Windows.Forms.ColumnHeader
		Private groupBox3 As System.Windows.Forms.GroupBox
		Private groupBox6 As System.Windows.Forms.GroupBox
		Private cbPower As System.Windows.Forms.CheckBox
		Private cmbPower As System.Windows.Forms.ComboBox
		Private label24 As System.Windows.Forms.Label
		Private WithEvents btnPowerGet As System.Windows.Forms.Button
		Private label23 As System.Windows.Forms.Label
		Private WithEvents btnPowerSet As System.Windows.Forms.Button
		Private lblCount As System.Windows.Forms.Label
		Private columnHeaderIP As System.Windows.Forms.ColumnHeader
		Private WithEvents btnStart1 As System.Windows.Forms.Button
		Private WithEvents btnStart2 As System.Windows.Forms.Button
		Private WithEvents button1 As System.Windows.Forms.Button

	End Class
End Namespace
