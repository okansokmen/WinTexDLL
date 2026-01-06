Namespace UHFAPP
	Partial Public Class ReceiveEPC
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
			Me.lvEPC = New System.Windows.Forms.ListView()
			Me.columnHeaderID = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderEPC = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderTID = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderUser = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderRssi = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderCount = New System.Windows.Forms.ColumnHeader()
			Me.columnHeaderANT = New System.Windows.Forms.ColumnHeader()
			Me.label1 = New System.Windows.Forms.Label()
			Me.lblTotal = New System.Windows.Forms.Label()
			Me.button1 = New System.Windows.Forms.Button()
			Me.lblTime = New System.Windows.Forms.Label()
			Me.btnScanEPC = New System.Windows.Forms.Button()
			Me.label2 = New System.Windows.Forms.Label()
			Me.lto = New System.Windows.Forms.Label()
			Me.textBox1 = New System.Windows.Forms.TextBox()
			Me.label4 = New System.Windows.Forms.Label()
			Me.textBox2 = New System.Windows.Forms.TextBox()
			Me.cmbMode = New System.Windows.Forms.ComboBox()
			Me.label5 = New System.Windows.Forms.Label()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.panel2 = New System.Windows.Forms.Panel()
			Me.cmbCom = New System.Windows.Forms.ComboBox()
			Me.label6 = New System.Windows.Forms.Label()
			Me.panel3 = New System.Windows.Forms.Panel()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label7 = New System.Windows.Forms.Label()
			Me.panel1.SuspendLayout()
			Me.panel2.SuspendLayout()
			Me.panel3.SuspendLayout()
			Me.SuspendLayout()
			' 
			' lvEPC
			' 
			Me.lvEPC.BackColor = System.Drawing.SystemColors.ControlLightLight
			Me.lvEPC.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.columnHeaderID, Me.columnHeaderEPC, Me.columnHeaderTID, Me.columnHeaderUser, Me.columnHeaderRssi, Me.columnHeaderCount, Me.columnHeaderANT})
			Me.lvEPC.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lvEPC.FullRowSelect = True
			Me.lvEPC.Location = New System.Drawing.Point(-3, 1)
			Me.lvEPC.Name = "lvEPC"
			Me.lvEPC.Size = New System.Drawing.Size(1287, 466)
			Me.lvEPC.TabIndex = 3
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
			Me.columnHeaderEPC.Width = 230
			' 
			' columnHeaderTID
			' 
			Me.columnHeaderTID.Text = "TID"
			Me.columnHeaderTID.Width = 230
			' 
			' columnHeaderUser
			' 
			Me.columnHeaderUser.Text = "User"
			Me.columnHeaderUser.Width = 300
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
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label1.Location = New System.Drawing.Point(138, 15)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(88, 16)
			Me.label1.TabIndex = 4
			Me.label1.Text = "remote IP:"
			' 
			' lblTotal
			' 
			Me.lblTotal.AutoSize = True
			Me.lblTotal.BackColor = System.Drawing.Color.Transparent
			Me.lblTotal.Font = New System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(CInt(CByte(0)), CInt(CByte(64)), CInt(CByte(0)))
			Me.lblTotal.Location = New System.Drawing.Point(121, 544)
			Me.lblTotal.Name = "lblTotal"
			Me.lblTotal.Size = New System.Drawing.Size(23, 24)
			Me.lblTotal.TabIndex = 29
			Me.lblTotal.Text = "0"
			' 
			' button1
			' 
			Me.button1.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button1.Location = New System.Drawing.Point(855, 534)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(129, 48)
			Me.button1.TabIndex = 30
			Me.button1.Text = "Clear"
			Me.button1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button1.Click += new System.EventHandler(this.button1_Click);
			' 
			' lblTime
			' 
			Me.lblTime.AutoSize = True
			Me.lblTime.BackColor = System.Drawing.Color.Transparent
			Me.lblTime.Font = New System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lblTime.ForeColor = System.Drawing.Color.Black
			Me.lblTime.Location = New System.Drawing.Point(519, 544)
			Me.lblTime.Name = "lblTime"
			Me.lblTime.Size = New System.Drawing.Size(23, 24)
			Me.lblTime.TabIndex = 28
			Me.lblTime.Text = "0"
			' 
			' btnScanEPC
			' 
			Me.btnScanEPC.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold)
			Me.btnScanEPC.Location = New System.Drawing.Point(624, 534)
			Me.btnScanEPC.Name = "btnScanEPC"
			Me.btnScanEPC.Size = New System.Drawing.Size(125, 48)
			Me.btnScanEPC.TabIndex = 25
			Me.btnScanEPC.Text = "Start"
			Me.btnScanEPC.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnScanEPC.Click += new System.EventHandler(this.btnScanEPC_Click);
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.BackColor = System.Drawing.Color.Transparent
			Me.label2.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label2.ForeColor = System.Drawing.Color.Black
			Me.label2.Location = New System.Drawing.Point(438, 549)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(64, 19)
			Me.label2.TabIndex = 27
			Me.label2.Text = "Time:"
			' 
			' lto
			' 
			Me.lto.AutoSize = True
			Me.lto.BackColor = System.Drawing.Color.Transparent
			Me.lto.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lto.ForeColor = System.Drawing.Color.Black
			Me.lto.Location = New System.Drawing.Point(40, 546)
			Me.lto.Name = "lto"
			Me.lto.Size = New System.Drawing.Size(75, 19)
			Me.lto.TabIndex = 26
			Me.lto.Text = "Total:"
			' 
			' textBox1
			' 
			Me.textBox1.Enabled = False
			Me.textBox1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.textBox1.Location = New System.Drawing.Point(229, 7)
			Me.textBox1.Name = "textBox1"
			Me.textBox1.Size = New System.Drawing.Size(175, 26)
			Me.textBox1.TabIndex = 31
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label4.Location = New System.Drawing.Point(9, 16)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(48, 16)
			Me.label4.TabIndex = 34
			Me.label4.Text = "Port:"
			' 
			' textBox2
			' 
			Me.textBox2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.textBox2.Location = New System.Drawing.Point(56, 9)
			Me.textBox2.Name = "textBox2"
			Me.textBox2.Size = New System.Drawing.Size(60, 26)
			Me.textBox2.TabIndex = 35
			Me.textBox2.Text = "8888"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			' 
			' cmbMode
			' 
			Me.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cmbMode.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbMode.FormattingEnabled = True
			Me.cmbMode.Items.AddRange(New Object() { "UDP", "SerialPort"})
			Me.cmbMode.Location = New System.Drawing.Point(121, 13)
			Me.cmbMode.Name = "cmbMode"
			Me.cmbMode.Size = New System.Drawing.Size(137, 24)
			Me.cmbMode.TabIndex = 36
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cmbMode.SelectedIndexChanged += new System.EventHandler(this.cmbMode_SelectedIndexChanged);
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label5.Location = New System.Drawing.Point(19, 15)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(96, 16)
			Me.label5.TabIndex = 37
			Me.label5.Text = "OutputMode:"
			' 
			' panel1
			' 
			Me.panel1.Controls.Add(Me.label1)
			Me.panel1.Controls.Add(Me.textBox1)
			Me.panel1.Controls.Add(Me.textBox2)
			Me.panel1.Controls.Add(Me.label4)
			Me.panel1.Location = New System.Drawing.Point(518, 3)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(413, 38)
			Me.panel1.TabIndex = 38
			' 
			' panel2
			' 
			Me.panel2.Controls.Add(Me.cmbCom)
			Me.panel2.Controls.Add(Me.label6)
			Me.panel2.Location = New System.Drawing.Point(300, 3)
			Me.panel2.Name = "panel2"
			Me.panel2.Size = New System.Drawing.Size(163, 38)
			Me.panel2.TabIndex = 39
			' 
			' cmbCom
			' 
			Me.cmbCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cmbCom.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbCom.FormattingEnabled = True
			Me.cmbCom.Location = New System.Drawing.Point(54, 9)
			Me.cmbCom.Name = "cmbCom"
			Me.cmbCom.Size = New System.Drawing.Size(96, 24)
			Me.cmbCom.TabIndex = 35
			' 
			' label6
			' 
			Me.label6.AutoSize = True
			Me.label6.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label6.Location = New System.Drawing.Point(9, 14)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(40, 16)
			Me.label6.TabIndex = 34
			Me.label6.Text = "Com:"
			' 
			' panel3
			' 
			Me.panel3.BackColor = System.Drawing.SystemColors.GradientActiveCaption
			Me.panel3.Controls.Add(Me.panel2)
			Me.panel3.Controls.Add(Me.cmbMode)
			Me.panel3.Controls.Add(Me.panel1)
			Me.panel3.Controls.Add(Me.label5)
			Me.panel3.Location = New System.Drawing.Point(-3, 468)
			Me.panel3.Name = "panel3"
			Me.panel3.Size = New System.Drawing.Size(1287, 51)
			Me.panel3.TabIndex = 40
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.BackColor = System.Drawing.Color.Transparent
			Me.label3.Font = New System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label3.ForeColor = System.Drawing.Color.FromArgb(CInt(CByte(0)), CInt(CByte(64)), CInt(CByte(0)))
			Me.label3.Location = New System.Drawing.Point(270, 542)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(23, 24)
			Me.label3.TabIndex = 42
			Me.label3.Text = "0"
			' 
			' label7
			' 
			Me.label7.AutoSize = True
			Me.label7.BackColor = System.Drawing.Color.Transparent
			Me.label7.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label7.ForeColor = System.Drawing.Color.Black
			Me.label7.Location = New System.Drawing.Point(204, 546)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(60, 19)
			Me.label7.TabIndex = 41
			Me.label7.Text = "次数:"
			' 
			' ReceiveEPC
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.ClientSize = New System.Drawing.Size(1280, 591)
			Me.Controls.Add(Me.label3)
			Me.Controls.Add(Me.label7)
			Me.Controls.Add(Me.panel3)
			Me.Controls.Add(Me.lblTotal)
			Me.Controls.Add(Me.button1)
			Me.Controls.Add(Me.lblTime)
			Me.Controls.Add(Me.btnScanEPC)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.lto)
			Me.Controls.Add(Me.lvEPC)
			Me.Name = "ReceiveEPC"
			Me.Text = "ReceiveEPC"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.ReceiveEPC_Load);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReceiveEPC_FormClosing);
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.panel2.ResumeLayout(False)
			Me.panel2.PerformLayout()
			Me.panel3.ResumeLayout(False)
			Me.panel3.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private lvEPC As System.Windows.Forms.ListView
		Private columnHeaderID As System.Windows.Forms.ColumnHeader
		Private columnHeaderEPC As System.Windows.Forms.ColumnHeader
		Private columnHeaderTID As System.Windows.Forms.ColumnHeader
		Private columnHeaderRssi As System.Windows.Forms.ColumnHeader
		Private columnHeaderCount As System.Windows.Forms.ColumnHeader
		Private columnHeaderANT As System.Windows.Forms.ColumnHeader
		Private label1 As System.Windows.Forms.Label
		Private lblTotal As System.Windows.Forms.Label
		Private WithEvents button1 As System.Windows.Forms.Button
		Private lblTime As System.Windows.Forms.Label
		Private WithEvents btnScanEPC As System.Windows.Forms.Button
		Private label2 As System.Windows.Forms.Label
		Private lto As System.Windows.Forms.Label
		Private textBox1 As System.Windows.Forms.TextBox
		Private label4 As System.Windows.Forms.Label
		Private WithEvents textBox2 As System.Windows.Forms.TextBox
		Private WithEvents cmbMode As System.Windows.Forms.ComboBox
		Private label5 As System.Windows.Forms.Label
		Private panel1 As System.Windows.Forms.Panel
		Private panel2 As System.Windows.Forms.Panel
		Private cmbCom As System.Windows.Forms.ComboBox
		Private label6 As System.Windows.Forms.Label
		Private panel3 As System.Windows.Forms.Panel
		Private columnHeaderUser As System.Windows.Forms.ColumnHeader
		Private label3 As System.Windows.Forms.Label
		Private label7 As System.Windows.Forms.Label
	End Class
End Namespace
