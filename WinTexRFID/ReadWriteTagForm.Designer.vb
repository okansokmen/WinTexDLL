Namespace UHFAPP
	Partial Public Class ReadWriteTagForm
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
			Me.lblLeng = New System.Windows.Forms.Label()
			Me.btnRead = New System.Windows.Forms.Button()
			Me.btnWrite = New System.Windows.Forms.Button()
			Me.label13 = New System.Windows.Forms.Label()
			Me.label3 = New System.Windows.Forms.Label()
			Me.txtRead_Data = New System.Windows.Forms.TextBox()
			Me.label6 = New System.Windows.Forms.Label()
			Me.txtRead_AccessPwd = New System.Windows.Forms.TextBox()
			Me.label5 = New System.Windows.Forms.Label()
			Me.txtRead_Length = New System.Windows.Forms.TextBox()
			Me.txtRead_Ptr = New System.Windows.Forms.TextBox()
			Me.label4 = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.cmbRead_Bank = New System.Windows.Forms.ComboBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.btnErase = New System.Windows.Forms.Button()
			Me.label25 = New System.Windows.Forms.Label()
			Me.btWrite = New System.Windows.Forms.Button()
			Me.label20 = New System.Windows.Forms.Label()
			Me.txtBlockWrite__Data = New System.Windows.Forms.TextBox()
			Me.label7 = New System.Windows.Forms.Label()
			Me.txtBlockWrite__AccessPwd = New System.Windows.Forms.TextBox()
			Me.label8 = New System.Windows.Forms.Label()
			Me.txtBlockWrite__Length = New System.Windows.Forms.TextBox()
			Me.txtBlockWrite__Ptr = New System.Windows.Forms.TextBox()
			Me.label9 = New System.Windows.Forms.Label()
			Me.label10 = New System.Windows.Forms.Label()
			Me.cmbBlockWrite__Bank = New System.Windows.Forms.ComboBox()
			Me.label11 = New System.Windows.Forms.Label()
			Me.groupBox4 = New System.Windows.Forms.GroupBox()
			Me.label29 = New System.Windows.Forms.Label()
			Me.txtLen = New System.Windows.Forms.TextBox()
			Me.label24 = New System.Windows.Forms.Label()
			Me.label23 = New System.Windows.Forms.Label()
			Me.txtPtr = New System.Windows.Forms.TextBox()
			Me.groupBox3 = New System.Windows.Forms.GroupBox()
			Me.rbUser = New System.Windows.Forms.RadioButton()
			Me.rbEPC = New System.Windows.Forms.RadioButton()
			Me.rbTID = New System.Windows.Forms.RadioButton()
			Me.label22 = New System.Windows.Forms.Label()
			Me.label30 = New System.Windows.Forms.Label()
			Me.txtFilter_EPC = New System.Windows.Forms.TextBox()
			Me.label12 = New System.Windows.Forms.Label()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.groupBox7 = New System.Windows.Forms.GroupBox()
			Me.textBox2 = New System.Windows.Forms.TextBox()
			Me.label38 = New System.Windows.Forms.Label()
			Me.label31 = New System.Windows.Forms.Label()
			Me.btnSelectFile = New System.Windows.Forms.Button()
			Me.btnWriteScreen = New System.Windows.Forms.Button()
			Me.label32 = New System.Windows.Forms.Label()
			Me.label33 = New System.Windows.Forms.Label()
			Me.txtWriteScreenData = New System.Windows.Forms.TextBox()
			Me.label34 = New System.Windows.Forms.Label()
			Me.WriteScreenPwd = New System.Windows.Forms.TextBox()
			Me.label35 = New System.Windows.Forms.Label()
			Me.WriteScreenLength = New System.Windows.Forms.TextBox()
			Me.txtWriteScreenPtr = New System.Windows.Forms.TextBox()
			Me.label36 = New System.Windows.Forms.Label()
			Me.label37 = New System.Windows.Forms.Label()
			Me.groupBox5 = New System.Windows.Forms.GroupBox()
			Me.QT_Length = New System.Windows.Forms.TextBox()
			Me.label26 = New System.Windows.Forms.Label()
			Me.btnQTWrite = New System.Windows.Forms.Button()
			Me.btnQTRead = New System.Windows.Forms.Button()
			Me.label21 = New System.Windows.Forms.Label()
			Me.cmbQT2 = New System.Windows.Forms.ComboBox()
			Me.label19 = New System.Windows.Forms.Label()
			Me.cmbQT1 = New System.Windows.Forms.ComboBox()
			Me.txtQT_data = New System.Windows.Forms.TextBox()
			Me.label14 = New System.Windows.Forms.Label()
			Me.txtQT_pwd = New System.Windows.Forms.TextBox()
			Me.label15 = New System.Windows.Forms.Label()
			Me.txtQT_ptr = New System.Windows.Forms.TextBox()
			Me.label16 = New System.Windows.Forms.Label()
			Me.label17 = New System.Windows.Forms.Label()
			Me.cmbQT_bank = New System.Windows.Forms.ComboBox()
			Me.label18 = New System.Windows.Forms.Label()
			Me.groupBox6 = New System.Windows.Forms.GroupBox()
			Me.btnSetQT = New System.Windows.Forms.Button()
			Me.btnGetQT = New System.Windows.Forms.Button()
			Me.comboBox2 = New System.Windows.Forms.ComboBox()
			Me.comboBox1 = New System.Windows.Forms.ComboBox()
			Me.label28 = New System.Windows.Forms.Label()
			Me.textBox1 = New System.Windows.Forms.TextBox()
			Me.label27 = New System.Windows.Forms.Label()
			Me.groupBox1.SuspendLayout()
			Me.groupBox2.SuspendLayout()
			Me.groupBox4.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			Me.panel1.SuspendLayout()
			Me.groupBox7.SuspendLayout()
			Me.groupBox5.SuspendLayout()
			Me.groupBox6.SuspendLayout()
			Me.SuspendLayout()
			' 
			' groupBox1
			' 
			Me.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox1.Controls.Add(Me.lblLeng)
			Me.groupBox1.Controls.Add(Me.btnRead)
			Me.groupBox1.Controls.Add(Me.btnWrite)
			Me.groupBox1.Controls.Add(Me.label13)
			Me.groupBox1.Controls.Add(Me.label3)
			Me.groupBox1.Controls.Add(Me.txtRead_Data)
			Me.groupBox1.Controls.Add(Me.label6)
			Me.groupBox1.Controls.Add(Me.txtRead_AccessPwd)
			Me.groupBox1.Controls.Add(Me.label5)
			Me.groupBox1.Controls.Add(Me.txtRead_Length)
			Me.groupBox1.Controls.Add(Me.txtRead_Ptr)
			Me.groupBox1.Controls.Add(Me.label4)
			Me.groupBox1.Controls.Add(Me.label2)
			Me.groupBox1.Controls.Add(Me.cmbRead_Bank)
			Me.groupBox1.Controls.Add(Me.label1)
			Me.groupBox1.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox1.ForeColor = System.Drawing.Color.Black
			Me.groupBox1.Location = New System.Drawing.Point(7, 105)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(616, 243)
			Me.groupBox1.TabIndex = 0
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "Read-write"
			' 
			' lblLeng
			' 
			Me.lblLeng.AutoSize = True
			Me.lblLeng.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lblLeng.ForeColor = System.Drawing.Color.Black
			Me.lblLeng.Location = New System.Drawing.Point(571, 158)
			Me.lblLeng.Name = "lblLeng"
			Me.lblLeng.Size = New System.Drawing.Size(16, 16)
			Me.lblLeng.TabIndex = 35
			Me.lblLeng.Text = "0"
			' 
			' btnRead
			' 
			Me.btnRead.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnRead.ForeColor = System.Drawing.Color.Black
			Me.btnRead.Location = New System.Drawing.Point(159, 209)
			Me.btnRead.Name = "btnRead"
			Me.btnRead.Size = New System.Drawing.Size(90, 31)
			Me.btnRead.TabIndex = 34
			Me.btnRead.Text = "Read"
			Me.btnRead.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
			' 
			' btnWrite
			' 
			Me.btnWrite.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnWrite.ForeColor = System.Drawing.Color.Black
			Me.btnWrite.Location = New System.Drawing.Point(283, 209)
			Me.btnWrite.Name = "btnWrite"
			Me.btnWrite.Size = New System.Drawing.Size(90, 29)
			Me.btnWrite.TabIndex = 33
			Me.btnWrite.Text = "Write"
			Me.btnWrite.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
			' 
			' label13
			' 
			Me.label13.AutoSize = True
			Me.label13.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label13.ForeColor = System.Drawing.Color.Black
			Me.label13.Location = New System.Drawing.Point(417, 85)
			Me.label13.Name = "label13"
			Me.label13.Size = New System.Drawing.Size(56, 16)
			Me.label13.TabIndex = 32
			Me.label13.Text = "(word)"
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(417, 121)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(0, 14)
			Me.label3.TabIndex = 31
			' 
			' txtRead_Data
			' 
			Me.txtRead_Data.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtRead_Data.Location = New System.Drawing.Point(108, 137)
			Me.txtRead_Data.Multiline = True
			Me.txtRead_Data.Name = "txtRead_Data"
			Me.txtRead_Data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtRead_Data.Size = New System.Drawing.Size(457, 66)
			Me.txtRead_Data.TabIndex = 23
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtRead_Data.TextChanged += new System.EventHandler(this.txtRead_Data_TextChanged);
			' 
			' label6
			' 
			Me.label6.AutoSize = True
			Me.label6.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label6.ForeColor = System.Drawing.Color.Black
			Me.label6.Location = New System.Drawing.Point(15, 149)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(48, 16)
			Me.label6.TabIndex = 22
			Me.label6.Text = "Data:"
			' 
			' txtRead_AccessPwd
			' 
			Me.txtRead_AccessPwd.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtRead_AccessPwd.Location = New System.Drawing.Point(108, 107)
			Me.txtRead_AccessPwd.Name = "txtRead_AccessPwd"
			Me.txtRead_AccessPwd.Size = New System.Drawing.Size(303, 26)
			Me.txtRead_AccessPwd.TabIndex = 21
			Me.txtRead_AccessPwd.Text = "00000000"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtRead_AccessPwd.TextChanged += new System.EventHandler(this.txtRead_AccessPwd_TextChanged);
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label5.ForeColor = System.Drawing.Color.Black
			Me.label5.Location = New System.Drawing.Point(14, 111)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(96, 16)
			Me.label5.TabIndex = 20
			Me.label5.Text = "Access Pwd:"
			' 
			' txtRead_Length
			' 
			Me.txtRead_Length.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtRead_Length.Location = New System.Drawing.Point(108, 77)
			Me.txtRead_Length.Name = "txtRead_Length"
			Me.txtRead_Length.Size = New System.Drawing.Size(303, 26)
			Me.txtRead_Length.TabIndex = 19
			Me.txtRead_Length.Text = "6"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtRead_Length.TextChanged += new System.EventHandler(this.TextChanged);
			' 
			' txtRead_Ptr
			' 
			Me.txtRead_Ptr.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtRead_Ptr.Location = New System.Drawing.Point(108, 47)
			Me.txtRead_Ptr.Name = "txtRead_Ptr"
			Me.txtRead_Ptr.Size = New System.Drawing.Size(303, 26)
			Me.txtRead_Ptr.TabIndex = 18
			Me.txtRead_Ptr.Text = "2"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtRead_Ptr.TextChanged += new System.EventHandler(this.TextChangedPtr);
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label4.ForeColor = System.Drawing.Color.Black
			Me.label4.Location = New System.Drawing.Point(15, 77)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(64, 16)
			Me.label4.TabIndex = 17
			Me.label4.Text = "Length:"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label2.ForeColor = System.Drawing.Color.Black
			Me.label2.Location = New System.Drawing.Point(14, 53)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(40, 16)
			Me.label2.TabIndex = 16
			Me.label2.Text = "Ptr:"
			' 
			' cmbRead_Bank
			' 
			Me.cmbRead_Bank.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbRead_Bank.FormattingEnabled = True
			Me.cmbRead_Bank.Items.AddRange(New Object() { "RESERVED", "EPC", "TID", "USER"})
			Me.cmbRead_Bank.Location = New System.Drawing.Point(108, 20)
			Me.cmbRead_Bank.Name = "cmbRead_Bank"
			Me.cmbRead_Bank.Size = New System.Drawing.Size(303, 24)
			Me.cmbRead_Bank.TabIndex = 15
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cmbRead_Bank.SelectedIndexChanged += new System.EventHandler(this.cmbRead_Bank_SelectedIndexChanged);
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label1.ForeColor = System.Drawing.Color.Black
			Me.label1.Location = New System.Drawing.Point(14, 28)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(48, 16)
			Me.label1.TabIndex = 14
			Me.label1.Text = "Bank:"
			' 
			' groupBox2
			' 
			Me.groupBox2.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox2.Controls.Add(Me.btnErase)
			Me.groupBox2.Controls.Add(Me.label25)
			Me.groupBox2.Controls.Add(Me.btWrite)
			Me.groupBox2.Controls.Add(Me.label20)
			Me.groupBox2.Controls.Add(Me.txtBlockWrite__Data)
			Me.groupBox2.Controls.Add(Me.label7)
			Me.groupBox2.Controls.Add(Me.txtBlockWrite__AccessPwd)
			Me.groupBox2.Controls.Add(Me.label8)
			Me.groupBox2.Controls.Add(Me.txtBlockWrite__Length)
			Me.groupBox2.Controls.Add(Me.txtBlockWrite__Ptr)
			Me.groupBox2.Controls.Add(Me.label9)
			Me.groupBox2.Controls.Add(Me.label10)
			Me.groupBox2.Controls.Add(Me.cmbBlockWrite__Bank)
			Me.groupBox2.Controls.Add(Me.label11)
			Me.groupBox2.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox2.ForeColor = System.Drawing.Color.Black
			Me.groupBox2.Location = New System.Drawing.Point(647, 105)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(634, 243)
			Me.groupBox2.TabIndex = 25
			Me.groupBox2.TabStop = False
			Me.groupBox2.Text = "BlockWrite"
			' 
			' btnErase
			' 
			Me.btnErase.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnErase.ForeColor = System.Drawing.Color.Black
			Me.btnErase.Location = New System.Drawing.Point(178, 207)
			Me.btnErase.Name = "btnErase"
			Me.btnErase.Size = New System.Drawing.Size(90, 31)
			Me.btnErase.TabIndex = 33
			Me.btnErase.Text = "Erase"
			Me.btnErase.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnErase.Click += new System.EventHandler(this.btnErase_Click);
			' 
			' label25
			' 
			Me.label25.AutoSize = True
			Me.label25.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label25.ForeColor = System.Drawing.Color.Black
			Me.label25.Location = New System.Drawing.Point(570, 158)
			Me.label25.Name = "label25"
			Me.label25.Size = New System.Drawing.Size(16, 16)
			Me.label25.TabIndex = 36
			Me.label25.Text = "0"
			' 
			' btWrite
			' 
			Me.btWrite.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btWrite.ForeColor = System.Drawing.Color.Black
			Me.btWrite.Location = New System.Drawing.Point(336, 207)
			Me.btWrite.Name = "btWrite"
			Me.btWrite.Size = New System.Drawing.Size(90, 31)
			Me.btWrite.TabIndex = 34
			Me.btWrite.Text = "Write"
			Me.btWrite.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btWrite.Click += new System.EventHandler(this.btWrite_Click);
			' 
			' label20
			' 
			Me.label20.AutoSize = True
			Me.label20.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label20.ForeColor = System.Drawing.Color.Black
			Me.label20.Location = New System.Drawing.Point(417, 85)
			Me.label20.Name = "label20"
			Me.label20.Size = New System.Drawing.Size(56, 16)
			Me.label20.TabIndex = 33
			Me.label20.Text = "(word)"
			' 
			' txtBlockWrite__Data
			' 
			Me.txtBlockWrite__Data.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtBlockWrite__Data.Location = New System.Drawing.Point(108, 136)
			Me.txtBlockWrite__Data.Multiline = True
			Me.txtBlockWrite__Data.Name = "txtBlockWrite__Data"
			Me.txtBlockWrite__Data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtBlockWrite__Data.Size = New System.Drawing.Size(456, 67)
			Me.txtBlockWrite__Data.TabIndex = 23
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtBlockWrite__Data.TextChanged += new System.EventHandler(this.txtBlockWrite__Data_TextChanged);
			' 
			' label7
			' 
			Me.label7.AutoSize = True
			Me.label7.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label7.ForeColor = System.Drawing.Color.Black
			Me.label7.Location = New System.Drawing.Point(21, 149)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(48, 16)
			Me.label7.TabIndex = 22
			Me.label7.Text = "Data:"
			' 
			' txtBlockWrite__AccessPwd
			' 
			Me.txtBlockWrite__AccessPwd.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtBlockWrite__AccessPwd.Location = New System.Drawing.Point(108, 106)
			Me.txtBlockWrite__AccessPwd.Name = "txtBlockWrite__AccessPwd"
			Me.txtBlockWrite__AccessPwd.Size = New System.Drawing.Size(303, 26)
			Me.txtBlockWrite__AccessPwd.TabIndex = 21
			Me.txtBlockWrite__AccessPwd.Text = "00000000"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtBlockWrite__AccessPwd.TextChanged += new System.EventHandler(this.txtBlockWrite__AccessPwd_TextChanged);
			' 
			' label8
			' 
			Me.label8.AutoSize = True
			Me.label8.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label8.ForeColor = System.Drawing.Color.Black
			Me.label8.Location = New System.Drawing.Point(16, 108)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(96, 16)
			Me.label8.TabIndex = 20
			Me.label8.Text = "Access Pwd:"
			' 
			' txtBlockWrite__Length
			' 
			Me.txtBlockWrite__Length.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtBlockWrite__Length.Location = New System.Drawing.Point(108, 76)
			Me.txtBlockWrite__Length.Name = "txtBlockWrite__Length"
			Me.txtBlockWrite__Length.Size = New System.Drawing.Size(303, 26)
			Me.txtBlockWrite__Length.TabIndex = 19
			Me.txtBlockWrite__Length.Text = "6"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtBlockWrite__Length.TextChanged += new System.EventHandler(this.txtBlockWrite__Length_TextChanged);
			' 
			' txtBlockWrite__Ptr
			' 
			Me.txtBlockWrite__Ptr.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtBlockWrite__Ptr.Location = New System.Drawing.Point(108, 47)
			Me.txtBlockWrite__Ptr.Name = "txtBlockWrite__Ptr"
			Me.txtBlockWrite__Ptr.Size = New System.Drawing.Size(303, 26)
			Me.txtBlockWrite__Ptr.TabIndex = 18
			Me.txtBlockWrite__Ptr.Text = "2"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtBlockWrite__Ptr.TextChanged += new System.EventHandler(this.txtBlockWrite__Ptr_TextChanged);
			' 
			' label9
			' 
			Me.label9.AutoSize = True
			Me.label9.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label9.ForeColor = System.Drawing.Color.Black
			Me.label9.Location = New System.Drawing.Point(16, 80)
			Me.label9.Name = "label9"
			Me.label9.Size = New System.Drawing.Size(64, 16)
			Me.label9.TabIndex = 17
			Me.label9.Text = "Length:"
			' 
			' label10
			' 
			Me.label10.AutoSize = True
			Me.label10.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label10.ForeColor = System.Drawing.Color.Black
			Me.label10.Location = New System.Drawing.Point(16, 53)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(40, 16)
			Me.label10.TabIndex = 16
			Me.label10.Text = "Ptr:"
			' 
			' cmbBlockWrite__Bank
			' 
			Me.cmbBlockWrite__Bank.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbBlockWrite__Bank.FormattingEnabled = True
			Me.cmbBlockWrite__Bank.Items.AddRange(New Object() { "RESERVED", "EPC", "TID", "USER"})
			Me.cmbBlockWrite__Bank.Location = New System.Drawing.Point(108, 20)
			Me.cmbBlockWrite__Bank.Name = "cmbBlockWrite__Bank"
			Me.cmbBlockWrite__Bank.Size = New System.Drawing.Size(303, 24)
			Me.cmbBlockWrite__Bank.TabIndex = 15
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cmbBlockWrite__Bank.SelectedIndexChanged += new System.EventHandler(this.cmbBlockWrite__Bank_SelectedIndexChanged);
			' 
			' label11
			' 
			Me.label11.AutoSize = True
			Me.label11.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label11.ForeColor = System.Drawing.Color.Black
			Me.label11.Location = New System.Drawing.Point(16, 28)
			Me.label11.Name = "label11"
			Me.label11.Size = New System.Drawing.Size(48, 16)
			Me.label11.TabIndex = 14
			Me.label11.Text = "Bank:"
			' 
			' groupBox4
			' 
			Me.groupBox4.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox4.Controls.Add(Me.label29)
			Me.groupBox4.Controls.Add(Me.txtLen)
			Me.groupBox4.Controls.Add(Me.label24)
			Me.groupBox4.Controls.Add(Me.label23)
			Me.groupBox4.Controls.Add(Me.txtPtr)
			Me.groupBox4.Controls.Add(Me.groupBox3)
			Me.groupBox4.Controls.Add(Me.label22)
			Me.groupBox4.Controls.Add(Me.label30)
			Me.groupBox4.Controls.Add(Me.txtFilter_EPC)
			Me.groupBox4.Controls.Add(Me.label12)
			Me.groupBox4.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox4.ForeColor = System.Drawing.Color.Black
			Me.groupBox4.Location = New System.Drawing.Point(7, 14)
			Me.groupBox4.Name = "groupBox4"
			Me.groupBox4.Size = New System.Drawing.Size(1274, 72)
			Me.groupBox4.TabIndex = 28
			Me.groupBox4.TabStop = False
			Me.groupBox4.Text = "filter"
			' 
			' label29
			' 
			Me.label29.AutoSize = True
			Me.label29.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label29.ForeColor = System.Drawing.Color.Black
			Me.label29.Location = New System.Drawing.Point(592, 37)
			Me.label29.Name = "label29"
			Me.label29.Size = New System.Drawing.Size(16, 16)
			Me.label29.TabIndex = 36
			Me.label29.Text = "0"
			' 
			' txtLen
			' 
			Me.txtLen.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtLen.Location = New System.Drawing.Point(1094, 31)
			Me.txtLen.MaxLength = 3
			Me.txtLen.Name = "txtLen"
			Me.txtLen.Size = New System.Drawing.Size(82, 26)
			Me.txtLen.TabIndex = 37
			Me.txtLen.Tag = "Number"
			Me.txtLen.Text = "0"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtLen.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			' 
			' label24
			' 
			Me.label24.AutoSize = True
			Me.label24.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label24.Location = New System.Drawing.Point(1175, 37)
			Me.label24.Name = "label24"
			Me.label24.Size = New System.Drawing.Size(48, 16)
			Me.label24.TabIndex = 38
			Me.label24.Text = "(bit)"
			' 
			' label23
			' 
			Me.label23.AutoSize = True
			Me.label23.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label23.Location = New System.Drawing.Point(1036, 36)
			Me.label23.Name = "label23"
			Me.label23.Size = New System.Drawing.Size(64, 16)
			Me.label23.TabIndex = 36
			Me.label23.Text = "Length:"
			' 
			' txtPtr
			' 
			Me.txtPtr.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtPtr.Location = New System.Drawing.Point(891, 31)
			Me.txtPtr.MaxLength = 3
			Me.txtPtr.Name = "txtPtr"
			Me.txtPtr.Size = New System.Drawing.Size(82, 26)
			Me.txtPtr.TabIndex = 33
			Me.txtPtr.Tag = "Number"
			Me.txtPtr.Text = "32"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtPtr.TextChanged += new System.EventHandler(this.txtPtr_TextChanged);
			' 
			' groupBox3
			' 
			Me.groupBox3.Controls.Add(Me.rbUser)
			Me.groupBox3.Controls.Add(Me.rbEPC)
			Me.groupBox3.Controls.Add(Me.rbTID)
			Me.groupBox3.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox3.Location = New System.Drawing.Point(648, 15)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(178, 47)
			Me.groupBox3.TabIndex = 34
			Me.groupBox3.TabStop = False
			Me.groupBox3.Text = "bank"
			' 
			' rbUser
			' 
			Me.rbUser.AutoSize = True
			Me.rbUser.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbUser.Location = New System.Drawing.Point(114, 20)
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
			Me.rbEPC.Location = New System.Drawing.Point(11, 19)
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
			Me.rbTID.Location = New System.Drawing.Point(67, 20)
			Me.rbTID.Name = "rbTID"
			Me.rbTID.Size = New System.Drawing.Size(50, 20)
			Me.rbTID.TabIndex = 9
			Me.rbTID.Text = "TID"
			Me.rbTID.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rbTID.Click += new System.EventHandler(this.rbTID_Click);
			' 
			' label22
			' 
			Me.label22.AutoSize = True
			Me.label22.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label22.Location = New System.Drawing.Point(973, 37)
			Me.label22.Name = "label22"
			Me.label22.Size = New System.Drawing.Size(48, 16)
			Me.label22.TabIndex = 35
			Me.label22.Text = "(bit)"
			' 
			' label30
			' 
			Me.label30.AutoSize = True
			Me.label30.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label30.Location = New System.Drawing.Point(855, 35)
			Me.label30.Name = "label30"
			Me.label30.Size = New System.Drawing.Size(40, 16)
			Me.label30.TabIndex = 32
			Me.label30.Text = "Ptr:"
			' 
			' txtFilter_EPC
			' 
			Me.txtFilter_EPC.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtFilter_EPC.Location = New System.Drawing.Point(99, 15)
			Me.txtFilter_EPC.Multiline = True
			Me.txtFilter_EPC.Name = "txtFilter_EPC"
			Me.txtFilter_EPC.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtFilter_EPC.Size = New System.Drawing.Size(487, 50)
			Me.txtFilter_EPC.TabIndex = 12
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtFilter_EPC.TextChanged += new System.EventHandler(this.txtFilter_EPC_TextChanged);
			' 
			' label12
			' 
			Me.label12.AutoSize = True
			Me.label12.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label12.ForeColor = System.Drawing.Color.Black
			Me.label12.Location = New System.Drawing.Point(18, 31)
			Me.label12.Name = "label12"
			Me.label12.Size = New System.Drawing.Size(48, 16)
			Me.label12.TabIndex = 11
			Me.label12.Text = "Data:"
			' 
			' panel1
			' 
			Me.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.panel1.Controls.Add(Me.groupBox7)
			Me.panel1.Controls.Add(Me.groupBox1)
			Me.panel1.Controls.Add(Me.groupBox4)
			Me.panel1.Controls.Add(Me.groupBox5)
			Me.panel1.Controls.Add(Me.groupBox2)
			Me.panel1.Controls.Add(Me.groupBox6)
			Me.panel1.Location = New System.Drawing.Point(2, -4)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(1290, 699)
			Me.panel1.TabIndex = 26
			' 
			' groupBox7
			' 
			Me.groupBox7.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox7.Controls.Add(Me.textBox2)
			Me.groupBox7.Controls.Add(Me.label38)
			Me.groupBox7.Controls.Add(Me.label31)
			Me.groupBox7.Controls.Add(Me.btnSelectFile)
			Me.groupBox7.Controls.Add(Me.btnWriteScreen)
			Me.groupBox7.Controls.Add(Me.label32)
			Me.groupBox7.Controls.Add(Me.label33)
			Me.groupBox7.Controls.Add(Me.txtWriteScreenData)
			Me.groupBox7.Controls.Add(Me.label34)
			Me.groupBox7.Controls.Add(Me.WriteScreenPwd)
			Me.groupBox7.Controls.Add(Me.label35)
			Me.groupBox7.Controls.Add(Me.WriteScreenLength)
			Me.groupBox7.Controls.Add(Me.txtWriteScreenPtr)
			Me.groupBox7.Controls.Add(Me.label36)
			Me.groupBox7.Controls.Add(Me.label37)
			Me.groupBox7.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox7.ForeColor = System.Drawing.Color.Black
			Me.groupBox7.Location = New System.Drawing.Point(7, 366)
			Me.groupBox7.Name = "groupBox7"
			Me.groupBox7.Size = New System.Drawing.Size(1274, 276)
			Me.groupBox7.TabIndex = 39
			Me.groupBox7.TabStop = False
			Me.groupBox7.Text = "按块写无源电子标签带水墨屏显示"
			Me.groupBox7.Visible = False
			' 
			' textBox2
			' 
			Me.textBox2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.textBox2.Location = New System.Drawing.Point(664, 29)
			Me.textBox2.Name = "textBox2"
			Me.textBox2.Size = New System.Drawing.Size(120, 26)
			Me.textBox2.TabIndex = 37
			Me.textBox2.Text = "3"
			' 
			' label38
			' 
			Me.label38.AutoSize = True
			Me.label38.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label38.ForeColor = System.Drawing.Color.Black
			Me.label38.Location = New System.Drawing.Point(622, 34)
			Me.label38.Name = "label38"
			Me.label38.Size = New System.Drawing.Size(48, 16)
			Me.label38.TabIndex = 36
			Me.label38.Text = "type:"
			' 
			' label31
			' 
			Me.label31.AutoSize = True
			Me.label31.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label31.ForeColor = System.Drawing.Color.Black
			Me.label31.Location = New System.Drawing.Point(1084, 214)
			Me.label31.Name = "label31"
			Me.label31.Size = New System.Drawing.Size(16, 16)
			Me.label31.TabIndex = 35
			Me.label31.Text = "0"
			' 
			' btnSelectFile
			' 
			Me.btnSelectFile.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnSelectFile.ForeColor = System.Drawing.Color.Black
			Me.btnSelectFile.Location = New System.Drawing.Point(381, 214)
			Me.btnSelectFile.Name = "btnSelectFile"
			Me.btnSelectFile.Size = New System.Drawing.Size(90, 31)
			Me.btnSelectFile.TabIndex = 34
			Me.btnSelectFile.Text = "选择文件"
			Me.btnSelectFile.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
			' 
			' btnWriteScreen
			' 
			Me.btnWriteScreen.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnWriteScreen.ForeColor = System.Drawing.Color.Black
			Me.btnWriteScreen.Location = New System.Drawing.Point(176, 214)
			Me.btnWriteScreen.Name = "btnWriteScreen"
			Me.btnWriteScreen.Size = New System.Drawing.Size(90, 29)
			Me.btnWriteScreen.TabIndex = 33
			Me.btnWriteScreen.Text = "写"
			Me.btnWriteScreen.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnWriteScreen.Click += new System.EventHandler(this.btnWriteScreen_Click);
			' 
			' label32
			' 
			Me.label32.AutoSize = True
			Me.label32.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label32.ForeColor = System.Drawing.Color.Black
			Me.label32.Location = New System.Drawing.Point(309, 34)
			Me.label32.Name = "label32"
			Me.label32.Size = New System.Drawing.Size(56, 16)
			Me.label32.TabIndex = 32
			Me.label32.Text = "(word)"
			' 
			' label33
			' 
			Me.label33.AutoSize = True
			Me.label33.Location = New System.Drawing.Point(417, 121)
			Me.label33.Name = "label33"
			Me.label33.Size = New System.Drawing.Size(0, 14)
			Me.label33.TabIndex = 31
			' 
			' txtWriteScreenData
			' 
			Me.txtWriteScreenData.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtWriteScreenData.Location = New System.Drawing.Point(52, 61)
			Me.txtWriteScreenData.Multiline = True
			Me.txtWriteScreenData.Name = "txtWriteScreenData"
			Me.txtWriteScreenData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtWriteScreenData.Size = New System.Drawing.Size(1133, 151)
			Me.txtWriteScreenData.TabIndex = 23
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtWriteScreenData.TextChanged += new System.EventHandler(this.txtWriteScreenData_TextChanged);
			' 
			' label34
			' 
			Me.label34.AutoSize = True
			Me.label34.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label34.ForeColor = System.Drawing.Color.Black
			Me.label34.Location = New System.Drawing.Point(3, 88)
			Me.label34.Name = "label34"
			Me.label34.Size = New System.Drawing.Size(48, 16)
			Me.label34.TabIndex = 22
			Me.label34.Text = "数据:"
			' 
			' WriteScreenPwd
			' 
			Me.WriteScreenPwd.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.WriteScreenPwd.Location = New System.Drawing.Point(467, 29)
			Me.WriteScreenPwd.Name = "WriteScreenPwd"
			Me.WriteScreenPwd.Size = New System.Drawing.Size(120, 26)
			Me.WriteScreenPwd.TabIndex = 21
			Me.WriteScreenPwd.Text = "00000000"
			' 
			' label35
			' 
			Me.label35.AutoSize = True
			Me.label35.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label35.ForeColor = System.Drawing.Color.Black
			Me.label35.Location = New System.Drawing.Point(375, 34)
			Me.label35.Name = "label35"
			Me.label35.Size = New System.Drawing.Size(96, 16)
			Me.label35.TabIndex = 20
			Me.label35.Text = "Access Pwd:"
			' 
			' WriteScreenLength
			' 
			Me.WriteScreenLength.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.WriteScreenLength.Location = New System.Drawing.Point(216, 29)
			Me.WriteScreenLength.Name = "WriteScreenLength"
			Me.WriteScreenLength.Size = New System.Drawing.Size(87, 26)
			Me.WriteScreenLength.TabIndex = 19
			Me.WriteScreenLength.Text = "0"
			' 
			' txtWriteScreenPtr
			' 
			Me.txtWriteScreenPtr.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtWriteScreenPtr.Location = New System.Drawing.Point(61, 29)
			Me.txtWriteScreenPtr.Name = "txtWriteScreenPtr"
			Me.txtWriteScreenPtr.Size = New System.Drawing.Size(72, 26)
			Me.txtWriteScreenPtr.TabIndex = 18
			Me.txtWriteScreenPtr.Text = "0"
			' 
			' label36
			' 
			Me.label36.AutoSize = True
			Me.label36.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label36.ForeColor = System.Drawing.Color.Black
			Me.label36.Location = New System.Drawing.Point(150, 34)
			Me.label36.Name = "label36"
			Me.label36.Size = New System.Drawing.Size(64, 16)
			Me.label36.TabIndex = 17
			Me.label36.Text = "Length:"
			' 
			' label37
			' 
			Me.label37.AutoSize = True
			Me.label37.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label37.ForeColor = System.Drawing.Color.Black
			Me.label37.Location = New System.Drawing.Point(15, 29)
			Me.label37.Name = "label37"
			Me.label37.Size = New System.Drawing.Size(40, 16)
			Me.label37.TabIndex = 16
			Me.label37.Text = "Ptr:"
			' 
			' groupBox5
			' 
			Me.groupBox5.BackColor = System.Drawing.Color.Transparent
			Me.groupBox5.Controls.Add(Me.QT_Length)
			Me.groupBox5.Controls.Add(Me.label26)
			Me.groupBox5.Controls.Add(Me.btnQTWrite)
			Me.groupBox5.Controls.Add(Me.btnQTRead)
			Me.groupBox5.Controls.Add(Me.label21)
			Me.groupBox5.Controls.Add(Me.cmbQT2)
			Me.groupBox5.Controls.Add(Me.label19)
			Me.groupBox5.Controls.Add(Me.cmbQT1)
			Me.groupBox5.Controls.Add(Me.txtQT_data)
			Me.groupBox5.Controls.Add(Me.label14)
			Me.groupBox5.Controls.Add(Me.txtQT_pwd)
			Me.groupBox5.Controls.Add(Me.label15)
			Me.groupBox5.Controls.Add(Me.txtQT_ptr)
			Me.groupBox5.Controls.Add(Me.label16)
			Me.groupBox5.Controls.Add(Me.label17)
			Me.groupBox5.Controls.Add(Me.cmbQT_bank)
			Me.groupBox5.Controls.Add(Me.label18)
			Me.groupBox5.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox5.ForeColor = System.Drawing.Color.Black
			Me.groupBox5.Location = New System.Drawing.Point(157, 514)
			Me.groupBox5.Name = "groupBox5"
			Me.groupBox5.Size = New System.Drawing.Size(348, 91)
			Me.groupBox5.TabIndex = 26
			Me.groupBox5.TabStop = False
			Me.groupBox5.Text = "QT"
			Me.groupBox5.Visible = False
			' 
			' QT_Length
			' 
			Me.QT_Length.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.QT_Length.Location = New System.Drawing.Point(105, 75)
			Me.QT_Length.Name = "QT_Length"
			Me.QT_Length.Size = New System.Drawing.Size(303, 26)
			Me.QT_Length.TabIndex = 19
			Me.QT_Length.Text = "6"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.QT_Length.TextChanged += new System.EventHandler(this.QT_Length_TextChanged);
			' 
			' label26
			' 
			Me.label26.AutoSize = True
			Me.label26.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label26.ForeColor = System.Drawing.Color.Black
			Me.label26.Location = New System.Drawing.Point(568, 172)
			Me.label26.Name = "label26"
			Me.label26.Size = New System.Drawing.Size(16, 16)
			Me.label26.TabIndex = 36
			Me.label26.Text = "0"
			' 
			' btnQTWrite
			' 
			Me.btnQTWrite.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnQTWrite.ForeColor = System.Drawing.Color.Black
			Me.btnQTWrite.Location = New System.Drawing.Point(305, 252)
			Me.btnQTWrite.Name = "btnQTWrite"
			Me.btnQTWrite.Size = New System.Drawing.Size(90, 31)
			Me.btnQTWrite.TabIndex = 37
			Me.btnQTWrite.Text = "Write"
			Me.btnQTWrite.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnQTWrite.Click += new System.EventHandler(this.btnQTWrite_Click);
			' 
			' btnQTRead
			' 
			Me.btnQTRead.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnQTRead.ForeColor = System.Drawing.Color.Black
			Me.btnQTRead.Location = New System.Drawing.Point(156, 252)
			Me.btnQTRead.Name = "btnQTRead"
			Me.btnQTRead.Size = New System.Drawing.Size(90, 31)
			Me.btnQTRead.TabIndex = 33
			Me.btnQTRead.Text = "Read"
			Me.btnQTRead.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnQTRead.Click += new System.EventHandler(this.btnQTRead_Click);
			' 
			' label21
			' 
			Me.label21.AutoSize = True
			Me.label21.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label21.ForeColor = System.Drawing.Color.Black
			Me.label21.Location = New System.Drawing.Point(430, 78)
			Me.label21.Name = "label21"
			Me.label21.Size = New System.Drawing.Size(56, 16)
			Me.label21.TabIndex = 36
			Me.label21.Text = "(word)"
			' 
			' cmbQT2
			' 
			Me.cmbQT2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbQT2.FormattingEnabled = True
			Me.cmbQT2.Items.AddRange(New Object() { "private Memory map", "public memory map"})
			Me.cmbQT2.Location = New System.Drawing.Point(305, 222)
			Me.cmbQT2.Name = "cmbQT2"
			Me.cmbQT2.Size = New System.Drawing.Size(195, 24)
			Me.cmbQT2.TabIndex = 35
			' 
			' label19
			' 
			Me.label19.AutoSize = True
			Me.label19.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label19.ForeColor = System.Drawing.Color.Black
			Me.label19.Location = New System.Drawing.Point(19, 225)
			Me.label19.Name = "label19"
			Me.label19.Size = New System.Drawing.Size(32, 16)
			Me.label19.TabIndex = 34
			Me.label19.Text = "QT:"
			' 
			' cmbQT1
			' 
			Me.cmbQT1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbQT1.FormattingEnabled = True
			Me.cmbQT1.Items.AddRange(New Object() { "Not reduces range", "Reduces range"})
			Me.cmbQT1.Location = New System.Drawing.Point(105, 222)
			Me.cmbQT1.Name = "cmbQT1"
			Me.cmbQT1.Size = New System.Drawing.Size(194, 24)
			Me.cmbQT1.TabIndex = 33
			' 
			' txtQT_data
			' 
			Me.txtQT_data.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtQT_data.Location = New System.Drawing.Point(105, 134)
			Me.txtQT_data.Multiline = True
			Me.txtQT_data.Name = "txtQT_data"
			Me.txtQT_data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtQT_data.Size = New System.Drawing.Size(457, 84)
			Me.txtQT_data.TabIndex = 23
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtQT_data.TextChanged += new System.EventHandler(this.txtQT_data_TextChanged);
			' 
			' label14
			' 
			Me.label14.AutoSize = True
			Me.label14.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label14.ForeColor = System.Drawing.Color.Black
			Me.label14.Location = New System.Drawing.Point(19, 161)
			Me.label14.Name = "label14"
			Me.label14.Size = New System.Drawing.Size(48, 16)
			Me.label14.TabIndex = 22
			Me.label14.Text = "Data:"
			' 
			' txtQT_pwd
			' 
			Me.txtQT_pwd.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtQT_pwd.Location = New System.Drawing.Point(105, 104)
			Me.txtQT_pwd.Name = "txtQT_pwd"
			Me.txtQT_pwd.Size = New System.Drawing.Size(303, 26)
			Me.txtQT_pwd.TabIndex = 21
			Me.txtQT_pwd.Text = "00000000"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtQT_pwd.TextChanged += new System.EventHandler(this.txtQT_pwd_TextChanged);
			' 
			' label15
			' 
			Me.label15.AutoSize = True
			Me.label15.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label15.ForeColor = System.Drawing.Color.Black
			Me.label15.Location = New System.Drawing.Point(17, 107)
			Me.label15.Name = "label15"
			Me.label15.Size = New System.Drawing.Size(96, 16)
			Me.label15.TabIndex = 20
			Me.label15.Text = "Access Pwd:"
			Me.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			' 
			' txtQT_ptr
			' 
			Me.txtQT_ptr.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtQT_ptr.Location = New System.Drawing.Point(105, 47)
			Me.txtQT_ptr.Name = "txtQT_ptr"
			Me.txtQT_ptr.Size = New System.Drawing.Size(303, 26)
			Me.txtQT_ptr.TabIndex = 18
			Me.txtQT_ptr.Text = "2"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtQT_ptr.TextChanged += new System.EventHandler(this.txtQT_ptr_TextChanged);
			' 
			' label16
			' 
			Me.label16.AutoSize = True
			Me.label16.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label16.ForeColor = System.Drawing.Color.Black
			Me.label16.Location = New System.Drawing.Point(17, 78)
			Me.label16.Name = "label16"
			Me.label16.Size = New System.Drawing.Size(64, 16)
			Me.label16.TabIndex = 17
			Me.label16.Text = "Length:"
			' 
			' label17
			' 
			Me.label17.AutoSize = True
			Me.label17.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label17.ForeColor = System.Drawing.Color.Black
			Me.label17.Location = New System.Drawing.Point(19, 50)
			Me.label17.Name = "label17"
			Me.label17.Size = New System.Drawing.Size(40, 16)
			Me.label17.TabIndex = 16
			Me.label17.Text = "Ptr:"
			' 
			' cmbQT_bank
			' 
			Me.cmbQT_bank.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbQT_bank.FormattingEnabled = True
			Me.cmbQT_bank.Items.AddRange(New Object() { "RESERVED", "EPC", "TID", "USER"})
			Me.cmbQT_bank.Location = New System.Drawing.Point(105, 20)
			Me.cmbQT_bank.Name = "cmbQT_bank"
			Me.cmbQT_bank.Size = New System.Drawing.Size(303, 24)
			Me.cmbQT_bank.TabIndex = 15
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cmbQT_bank.SelectedIndexChanged += new System.EventHandler(this.cmbQT_bank_SelectedIndexChanged);
			' 
			' label18
			' 
			Me.label18.AutoSize = True
			Me.label18.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label18.ForeColor = System.Drawing.Color.Black
			Me.label18.Location = New System.Drawing.Point(19, 23)
			Me.label18.Name = "label18"
			Me.label18.Size = New System.Drawing.Size(48, 16)
			Me.label18.TabIndex = 14
			Me.label18.Text = "Bank:"
			Me.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			' 
			' groupBox6
			' 
			Me.groupBox6.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox6.Controls.Add(Me.btnSetQT)
			Me.groupBox6.Controls.Add(Me.btnGetQT)
			Me.groupBox6.Controls.Add(Me.comboBox2)
			Me.groupBox6.Controls.Add(Me.comboBox1)
			Me.groupBox6.Controls.Add(Me.label28)
			Me.groupBox6.Controls.Add(Me.textBox1)
			Me.groupBox6.Controls.Add(Me.label27)
			Me.groupBox6.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox6.Location = New System.Drawing.Point(7, 368)
			Me.groupBox6.Name = "groupBox6"
			Me.groupBox6.Size = New System.Drawing.Size(616, 100)
			Me.groupBox6.TabIndex = 38
			Me.groupBox6.TabStop = False
			Me.groupBox6.Text = "Set QT"
			Me.groupBox6.Visible = False
			' 
			' btnSetQT
			' 
			Me.btnSetQT.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnSetQT.ForeColor = System.Drawing.Color.Black
			Me.btnSetQT.Location = New System.Drawing.Point(299, 62)
			Me.btnSetQT.Name = "btnSetQT"
			Me.btnSetQT.Size = New System.Drawing.Size(90, 31)
			Me.btnSetQT.TabIndex = 41
			Me.btnSetQT.Text = "Set"
			Me.btnSetQT.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnSetQT.Click += new System.EventHandler(this.btnSetQT_Click);
			' 
			' btnGetQT
			' 
			Me.btnGetQT.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnGetQT.ForeColor = System.Drawing.Color.Black
			Me.btnGetQT.Location = New System.Drawing.Point(150, 62)
			Me.btnGetQT.Name = "btnGetQT"
			Me.btnGetQT.Size = New System.Drawing.Size(90, 31)
			Me.btnGetQT.TabIndex = 40
			Me.btnGetQT.Text = "Get"
			Me.btnGetQT.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnGetQT.Click += new System.EventHandler(this.btnGetQT_Click);
			' 
			' comboBox2
			' 
			Me.comboBox2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.comboBox2.FormattingEnabled = True
			Me.comboBox2.Items.AddRange(New Object() { "private Memory map", "public memory map"})
			Me.comboBox2.Location = New System.Drawing.Point(298, 20)
			Me.comboBox2.Name = "comboBox2"
			Me.comboBox2.Size = New System.Drawing.Size(175, 24)
			Me.comboBox2.TabIndex = 39
			' 
			' comboBox1
			' 
			Me.comboBox1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.comboBox1.FormattingEnabled = True
			Me.comboBox1.Items.AddRange(New Object() { "Not reduces range", "Reduces range"})
			Me.comboBox1.Location = New System.Drawing.Point(108, 20)
			Me.comboBox1.Name = "comboBox1"
			Me.comboBox1.Size = New System.Drawing.Size(184, 24)
			Me.comboBox1.TabIndex = 36
			' 
			' label28
			' 
			Me.label28.AutoSize = True
			Me.label28.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label28.ForeColor = System.Drawing.Color.Black
			Me.label28.Location = New System.Drawing.Point(70, 23)
			Me.label28.Name = "label28"
			Me.label28.Size = New System.Drawing.Size(32, 16)
			Me.label28.TabIndex = 35
			Me.label28.Text = "QT:"
			' 
			' textBox1
			' 
			Me.textBox1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.textBox1.Location = New System.Drawing.Point(52, 67)
			Me.textBox1.Name = "textBox1"
			Me.textBox1.Size = New System.Drawing.Size(92, 26)
			Me.textBox1.TabIndex = 23
			Me.textBox1.Text = "00000000"
			Me.textBox1.Visible = False
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
			' 
			' label27
			' 
			Me.label27.AutoSize = True
			Me.label27.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label27.ForeColor = System.Drawing.Color.Black
			Me.label27.Location = New System.Drawing.Point(3, 72)
			Me.label27.Name = "label27"
			Me.label27.Size = New System.Drawing.Size(48, 16)
			Me.label27.TabIndex = 22
			Me.label27.Text = "密码:"
			Me.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			Me.label27.Visible = False
			' 
			' ReadWriteTagForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.ClientSize = New System.Drawing.Size(1290, 685)
			Me.Controls.Add(Me.panel1)
			Me.KeyPreview = True
			Me.Name = "ReadWriteTagForm"
			Me.Text = "ReadWriteTagForm"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.ReadWriteTagForm_Load);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReadWriteTagForm_FormClosing);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadWriteTagForm_KeyDown);
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.groupBox4.ResumeLayout(False)
			Me.groupBox4.PerformLayout()
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox3.PerformLayout()
			Me.panel1.ResumeLayout(False)
			Me.groupBox7.ResumeLayout(False)
			Me.groupBox7.PerformLayout()
			Me.groupBox5.ResumeLayout(False)
			Me.groupBox5.PerformLayout()
			Me.groupBox6.ResumeLayout(False)
			Me.groupBox6.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private groupBox1 As System.Windows.Forms.GroupBox
		Private label1 As System.Windows.Forms.Label
		Private WithEvents cmbRead_Bank As System.Windows.Forms.ComboBox
		Private WithEvents txtRead_Data As System.Windows.Forms.TextBox
		Private label6 As System.Windows.Forms.Label
		Private WithEvents txtRead_AccessPwd As System.Windows.Forms.TextBox
		Private label5 As System.Windows.Forms.Label
		Private WithEvents txtRead_Length As System.Windows.Forms.TextBox
		Private WithEvents txtRead_Ptr As System.Windows.Forms.TextBox
		Private label4 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private WithEvents txtBlockWrite__Data As System.Windows.Forms.TextBox
		Private label7 As System.Windows.Forms.Label
		Private WithEvents txtBlockWrite__AccessPwd As System.Windows.Forms.TextBox
		Private label8 As System.Windows.Forms.Label
		Private WithEvents txtBlockWrite__Length As System.Windows.Forms.TextBox
		Private WithEvents txtBlockWrite__Ptr As System.Windows.Forms.TextBox
		Private label9 As System.Windows.Forms.Label
		Private label10 As System.Windows.Forms.Label
		Private WithEvents cmbBlockWrite__Bank As System.Windows.Forms.ComboBox
		Private label11 As System.Windows.Forms.Label
		Private panel1 As System.Windows.Forms.Panel
		Private groupBox4 As System.Windows.Forms.GroupBox
		Private WithEvents txtFilter_EPC As System.Windows.Forms.TextBox
		Private label12 As System.Windows.Forms.Label
		Private groupBox5 As System.Windows.Forms.GroupBox
		Private WithEvents txtQT_data As System.Windows.Forms.TextBox
		Private label14 As System.Windows.Forms.Label
		Private WithEvents txtQT_pwd As System.Windows.Forms.TextBox
		Private label15 As System.Windows.Forms.Label
		Private WithEvents QT_Length As System.Windows.Forms.TextBox
		Private WithEvents txtQT_ptr As System.Windows.Forms.TextBox
		Private label16 As System.Windows.Forms.Label
		Private label17 As System.Windows.Forms.Label
		Private WithEvents cmbQT_bank As System.Windows.Forms.ComboBox
		Private label18 As System.Windows.Forms.Label
		Private label19 As System.Windows.Forms.Label
		Private cmbQT1 As System.Windows.Forms.ComboBox
		Private cmbQT2 As System.Windows.Forms.ComboBox
		Private label3 As System.Windows.Forms.Label
		Private label13 As System.Windows.Forms.Label
		Private label20 As System.Windows.Forms.Label
		Private label21 As System.Windows.Forms.Label
		Private groupBox3 As System.Windows.Forms.GroupBox
		Private WithEvents rbUser As System.Windows.Forms.RadioButton
		Private WithEvents rbEPC As System.Windows.Forms.RadioButton
		Private WithEvents rbTID As System.Windows.Forms.RadioButton
		Private WithEvents txtPtr As System.Windows.Forms.TextBox
		Private label30 As System.Windows.Forms.Label
		Private label24 As System.Windows.Forms.Label
		Private WithEvents txtLen As System.Windows.Forms.TextBox
		Private label23 As System.Windows.Forms.Label
		Private label22 As System.Windows.Forms.Label
		Private WithEvents btnWrite As System.Windows.Forms.Button
		Private WithEvents btnRead As System.Windows.Forms.Button
		Private lblLeng As System.Windows.Forms.Label
		Private WithEvents btWrite As System.Windows.Forms.Button
		Private label25 As System.Windows.Forms.Label
		Private WithEvents btnErase As System.Windows.Forms.Button
		Private WithEvents btnQTWrite As System.Windows.Forms.Button
		Private WithEvents btnQTRead As System.Windows.Forms.Button
		Private label26 As System.Windows.Forms.Label
		Private groupBox6 As System.Windows.Forms.GroupBox
		Private comboBox2 As System.Windows.Forms.ComboBox
		Private comboBox1 As System.Windows.Forms.ComboBox
		Private label28 As System.Windows.Forms.Label
		Private WithEvents textBox1 As System.Windows.Forms.TextBox
		Private label27 As System.Windows.Forms.Label
		Private WithEvents btnSetQT As System.Windows.Forms.Button
		Private WithEvents btnGetQT As System.Windows.Forms.Button
		Private label29 As System.Windows.Forms.Label
		Private groupBox7 As System.Windows.Forms.GroupBox
		Private label31 As System.Windows.Forms.Label
		Private WithEvents btnSelectFile As System.Windows.Forms.Button
		Private WithEvents btnWriteScreen As System.Windows.Forms.Button
		Private label32 As System.Windows.Forms.Label
		Private label33 As System.Windows.Forms.Label
		Private WithEvents txtWriteScreenData As System.Windows.Forms.TextBox
		Private label34 As System.Windows.Forms.Label
		Private WriteScreenPwd As System.Windows.Forms.TextBox
		Private label35 As System.Windows.Forms.Label
		Private WriteScreenLength As System.Windows.Forms.TextBox
		Private txtWriteScreenPtr As System.Windows.Forms.TextBox
		Private label36 As System.Windows.Forms.Label
		Private label37 As System.Windows.Forms.Label
		Private label38 As System.Windows.Forms.Label
		Private textBox2 As System.Windows.Forms.TextBox
	End Class
End Namespace
