Namespace UHFAPP
	Partial Public Class TestForm
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
			Me.columnHeaderName = New System.Windows.Forms.ColumnHeader()
			Me.columnHeader5 = New System.Windows.Forms.ColumnHeader()
			Me.label1 = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.listView1 = New System.Windows.Forms.ListView()
			Me.columnHeader1 = New System.Windows.Forms.ColumnHeader()
			Me.columnHeader2 = New System.Windows.Forms.ColumnHeader()
			Me.columnHeader3 = New System.Windows.Forms.ColumnHeader()
			Me.button1 = New System.Windows.Forms.Button()
			Me.btnScanEPC = New System.Windows.Forms.Button()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.label4 = New System.Windows.Forms.Label()
			Me.label3 = New System.Windows.Forms.Label()
			Me.panel1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' lvEPC
			' 
			Me.lvEPC.BackColor = System.Drawing.SystemColors.ControlLightLight
			Me.lvEPC.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.columnHeaderID, Me.columnHeaderEPC, Me.columnHeaderName, Me.columnHeader5})
			Me.lvEPC.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lvEPC.FullRowSelect = True
			Me.lvEPC.Location = New System.Drawing.Point(3, 62)
			Me.lvEPC.Name = "lvEPC"
			Me.lvEPC.Size = New System.Drawing.Size(627, 453)
			Me.lvEPC.TabIndex = 3
			Me.lvEPC.UseCompatibleStateImageBehavior = False
			Me.lvEPC.View = System.Windows.Forms.View.Details
			' 
			' columnHeaderID
			' 
			Me.columnHeaderID.Text = "序列号"
			Me.columnHeaderID.Width = 0
			' 
			' columnHeaderEPC
			' 
			Me.columnHeaderEPC.Text = "ID"
			Me.columnHeaderEPC.Width = 240
			' 
			' columnHeaderName
			' 
			Me.columnHeaderName.Text = "食品名称"
			Me.columnHeaderName.Width = 220
			' 
			' columnHeader5
			' 
			Me.columnHeader5.Text = "读取次数"
			Me.columnHeader5.Width = 80
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label1.ForeColor = System.Drawing.Color.Black
			Me.label1.Location = New System.Drawing.Point(7, 27)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(85, 16)
			Me.label1.TabIndex = 4
			Me.label1.Text = "上架食品:"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label2.ForeColor = System.Drawing.Color.Black
			Me.label2.Location = New System.Drawing.Point(641, 26)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(85, 16)
			Me.label2.TabIndex = 6
			Me.label2.Text = "下架食品:"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.label2.Click += new System.EventHandler(this.label2_Click);
			' 
			' listView1
			' 
			Me.listView1.BackColor = System.Drawing.SystemColors.ControlLightLight
			Me.listView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.columnHeader1, Me.columnHeader2, Me.columnHeader3})
			Me.listView1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.listView1.FullRowSelect = True
			Me.listView1.Location = New System.Drawing.Point(636, 62)
			Me.listView1.Name = "listView1"
			Me.listView1.Size = New System.Drawing.Size(627, 453)
			Me.listView1.TabIndex = 5
			Me.listView1.UseCompatibleStateImageBehavior = False
			Me.listView1.View = System.Windows.Forms.View.Details
			' 
			' columnHeader1
			' 
			Me.columnHeader1.Text = "序列号"
			Me.columnHeader1.Width = 0
			' 
			' columnHeader2
			' 
			Me.columnHeader2.Text = "ID"
			Me.columnHeader2.Width = 270
			' 
			' columnHeader3
			' 
			Me.columnHeader3.Text = "食品名称"
			Me.columnHeader3.Width = 220
			' 
			' button1
			' 
			Me.button1.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button1.Location = New System.Drawing.Point(484, 533)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(116, 48)
			Me.button1.TabIndex = 32
			Me.button1.Text = "Clear"
			Me.button1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button1.Click += new System.EventHandler(this.button1_Click);
			' 
			' btnScanEPC
			' 
			Me.btnScanEPC.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold)
			Me.btnScanEPC.Location = New System.Drawing.Point(666, 533)
			Me.btnScanEPC.Name = "btnScanEPC"
			Me.btnScanEPC.Size = New System.Drawing.Size(116, 48)
			Me.btnScanEPC.TabIndex = 31
			Me.btnScanEPC.Text = "Start"
			Me.btnScanEPC.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnScanEPC.Click += new System.EventHandler(this.btnScanEPC_Click);
			' 
			' panel1
			' 
			Me.panel1.AutoScroll = True
			Me.panel1.Controls.Add(Me.label4)
			Me.panel1.Controls.Add(Me.label3)
			Me.panel1.Controls.Add(Me.label1)
			Me.panel1.Controls.Add(Me.label2)
			Me.panel1.Controls.Add(Me.lvEPC)
			Me.panel1.Controls.Add(Me.btnScanEPC)
			Me.panel1.Controls.Add(Me.listView1)
			Me.panel1.Controls.Add(Me.button1)
			Me.panel1.Enabled = False
			Me.panel1.Location = New System.Drawing.Point(12, -2)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(1265, 590)
			Me.panel1.TabIndex = 33
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label4.ForeColor = System.Drawing.Color.Blue
			Me.label4.Location = New System.Drawing.Point(732, 26)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(20, 19)
			Me.label4.TabIndex = 34
			Me.label4.Text = "0"
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label3.ForeColor = System.Drawing.Color.Blue
			Me.label3.Location = New System.Drawing.Point(98, 27)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(20, 19)
			Me.label3.TabIndex = 33
			Me.label3.Text = "0"
			' 
			' TestForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.ClientSize = New System.Drawing.Size(1280, 591)
			Me.Controls.Add(Me.panel1)
			Me.Name = "TestForm"
			Me.Text = "Test"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.Test_Load);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Test_FormClosing);
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private lvEPC As System.Windows.Forms.ListView
		Private columnHeaderID As System.Windows.Forms.ColumnHeader
		Private columnHeaderEPC As System.Windows.Forms.ColumnHeader
		Private columnHeaderName As System.Windows.Forms.ColumnHeader
		Private label1 As System.Windows.Forms.Label
		Private WithEvents label2 As System.Windows.Forms.Label
		Private listView1 As System.Windows.Forms.ListView
		Private columnHeader1 As System.Windows.Forms.ColumnHeader
		Private columnHeader2 As System.Windows.Forms.ColumnHeader
		Private columnHeader3 As System.Windows.Forms.ColumnHeader
		Private WithEvents button1 As System.Windows.Forms.Button
		Private WithEvents btnScanEPC As System.Windows.Forms.Button
		Private WithEvents panel1 As System.Windows.Forms.Panel
		Private columnHeader5 As System.Windows.Forms.ColumnHeader
		Private label4 As System.Windows.Forms.Label
		Private label3 As System.Windows.Forms.Label
	End Class
End Namespace
