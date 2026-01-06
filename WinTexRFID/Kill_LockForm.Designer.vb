Namespace UHFAPP
	Partial Public Class Kill_LockForm
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
			Me.label6 = New System.Windows.Forms.Label()
			Me.button2 = New System.Windows.Forms.Button()
			Me.txtKill_AccessPwd = New System.Windows.Forms.TextBox()
			Me.label5 = New System.Windows.Forms.Label()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.groupBox5 = New System.Windows.Forms.GroupBox()
			Me.cbUser = New System.Windows.Forms.RadioButton()
			Me.cbTID = New System.Windows.Forms.RadioButton()
			Me.cbEPC = New System.Windows.Forms.RadioButton()
			Me.cbAccess = New System.Windows.Forms.RadioButton()
			Me.cbKill = New System.Windows.Forms.RadioButton()
			Me.groupBox3 = New System.Windows.Forms.GroupBox()
			Me.rbPermanentLock = New System.Windows.Forms.RadioButton()
			Me.rbPermanentOpen = New System.Windows.Forms.RadioButton()
			Me.rbTemporaryLock = New System.Windows.Forms.RadioButton()
			Me.rbTemporaryOpen = New System.Windows.Forms.RadioButton()
			Me.label7 = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.txtLockPwd = New System.Windows.Forms.TextBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.button5 = New System.Windows.Forms.Button()
			Me.groupBox7 = New System.Windows.Forms.GroupBox()
			Me.label8 = New System.Windows.Forms.Label()
			Me.cbBlock16 = New System.Windows.Forms.CheckBox()
			Me.cbBlock15 = New System.Windows.Forms.CheckBox()
			Me.cbBlock14 = New System.Windows.Forms.CheckBox()
			Me.cbBlock13 = New System.Windows.Forms.CheckBox()
			Me.cbBlock12 = New System.Windows.Forms.CheckBox()
			Me.cbBlock11 = New System.Windows.Forms.CheckBox()
			Me.cbBlock10 = New System.Windows.Forms.CheckBox()
			Me.cbBlock9 = New System.Windows.Forms.CheckBox()
			Me.cbBlock8 = New System.Windows.Forms.CheckBox()
			Me.cbBlock7 = New System.Windows.Forms.CheckBox()
			Me.cbBlock6 = New System.Windows.Forms.CheckBox()
			Me.cbBlock5 = New System.Windows.Forms.CheckBox()
			Me.cbBlock4 = New System.Windows.Forms.CheckBox()
			Me.cbBlock3 = New System.Windows.Forms.CheckBox()
			Me.cbBlock2 = New System.Windows.Forms.CheckBox()
			Me.cbBlock1 = New System.Windows.Forms.CheckBox()
			Me.cmbBlockPermalockReadLock = New System.Windows.Forms.ComboBox()
			Me.label20 = New System.Windows.Forms.Label()
			Me.button3 = New System.Windows.Forms.Button()
			Me.txtBlockPermalockPwd = New System.Windows.Forms.TextBox()
			Me.label23 = New System.Windows.Forms.Label()
			Me.txtBlockPermalockPtr = New System.Windows.Forms.TextBox()
			Me.label25 = New System.Windows.Forms.Label()
			Me.cmbBlockPermalockBank = New System.Windows.Forms.ComboBox()
			Me.label26 = New System.Windows.Forms.Label()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.groupBox9 = New System.Windows.Forms.GroupBox()
			Me.label17 = New System.Windows.Forms.LinkLabel()
			Me.label18 = New System.Windows.Forms.Label()
			Me.txtGBPWD = New System.Windows.Forms.TextBox()
			Me.label19 = New System.Windows.Forms.Label()
			Me.button4 = New System.Windows.Forms.Button()
			Me.comboAction = New System.Windows.Forms.ComboBox()
			Me.label16 = New System.Windows.Forms.Label()
			Me.comboConfig = New System.Windows.Forms.ComboBox()
			Me.label15 = New System.Windows.Forms.Label()
			Me.comboBank = New System.Windows.Forms.ComboBox()
			Me.label14 = New System.Windows.Forms.Label()
			Me.groupBox6 = New System.Windows.Forms.GroupBox()
			Me.label29 = New System.Windows.Forms.Label()
			Me.txtLen = New System.Windows.Forms.TextBox()
			Me.label24 = New System.Windows.Forms.Label()
			Me.label3 = New System.Windows.Forms.Label()
			Me.txtPtr = New System.Windows.Forms.TextBox()
			Me.groupBox8 = New System.Windows.Forms.GroupBox()
			Me.rbUser = New System.Windows.Forms.RadioButton()
			Me.rbEPC = New System.Windows.Forms.RadioButton()
			Me.rbTID = New System.Windows.Forms.RadioButton()
			Me.label22 = New System.Windows.Forms.Label()
			Me.label30 = New System.Windows.Forms.Label()
			Me.txtFilter_EPC = New System.Windows.Forms.TextBox()
			Me.label12 = New System.Windows.Forms.Label()
			Me.groupBox4 = New System.Windows.Forms.GroupBox()
			Me.label13 = New System.Windows.Forms.Label()
			Me.label11 = New System.Windows.Forms.Label()
			Me.textBox2 = New System.Windows.Forms.TextBox()
			Me.label10 = New System.Windows.Forms.Label()
			Me.label1 = New System.Windows.Forms.Label()
			Me.button1 = New System.Windows.Forms.Button()
			Me.textBox1 = New System.Windows.Forms.TextBox()
			Me.label9 = New System.Windows.Forms.Label()
			Me.groupBox1.SuspendLayout()
			Me.groupBox2.SuspendLayout()
			Me.groupBox5.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			Me.groupBox7.SuspendLayout()
			Me.panel1.SuspendLayout()
			Me.groupBox9.SuspendLayout()
			Me.groupBox6.SuspendLayout()
			Me.groupBox8.SuspendLayout()
			Me.groupBox4.SuspendLayout()
			Me.SuspendLayout()
			' 
			' groupBox1
			' 
			Me.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox1.Controls.Add(Me.label6)
			Me.groupBox1.Controls.Add(Me.button2)
			Me.groupBox1.Controls.Add(Me.txtKill_AccessPwd)
			Me.groupBox1.Controls.Add(Me.label5)
			Me.groupBox1.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox1.Location = New System.Drawing.Point(657, 470)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(611, 113)
			Me.groupBox1.TabIndex = 0
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "Kill"
			' 
			' label6
			' 
			Me.label6.AutoSize = True
			Me.label6.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label6.ForeColor = System.Drawing.Color.Maroon
			Me.label6.Location = New System.Drawing.Point(414, 29)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(185, 12)
			Me.label6.TabIndex = 43
			Me.label6.Text = "Can't use the default password"
			' 
			' button2
			' 
			Me.button2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button2.ForeColor = System.Drawing.Color.Black
			Me.button2.Location = New System.Drawing.Point(253, 56)
			Me.button2.Name = "button2"
			Me.button2.Size = New System.Drawing.Size(90, 31)
			Me.button2.TabIndex = 14
			Me.button2.Text = "Confirm"
			Me.button2.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button2.Click += new System.EventHandler(this.button2_Click);
			' 
			' txtKill_AccessPwd
			' 
			Me.txtKill_AccessPwd.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtKill_AccessPwd.Location = New System.Drawing.Point(105, 22)
			Me.txtKill_AccessPwd.Name = "txtKill_AccessPwd"
			Me.txtKill_AccessPwd.Size = New System.Drawing.Size(303, 26)
			Me.txtKill_AccessPwd.TabIndex = 30
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtKill_AccessPwd.TextChanged += new System.EventHandler(this.txtRead_AccessPwd_TextChanged);
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label5.Location = New System.Drawing.Point(12, 26)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(96, 16)
			Me.label5.TabIndex = 29
			Me.label5.Text = "Access Pwd:"
			' 
			' groupBox2
			' 
			Me.groupBox2.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox2.Controls.Add(Me.groupBox5)
			Me.groupBox2.Controls.Add(Me.groupBox3)
			Me.groupBox2.Controls.Add(Me.label7)
			Me.groupBox2.Controls.Add(Me.label4)
			Me.groupBox2.Controls.Add(Me.txtLockPwd)
			Me.groupBox2.Controls.Add(Me.label2)
			Me.groupBox2.Controls.Add(Me.button5)
			Me.groupBox2.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox2.Location = New System.Drawing.Point(15, 98)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(625, 249)
			Me.groupBox2.TabIndex = 1
			Me.groupBox2.TabStop = False
			Me.groupBox2.Text = "lock"
			' 
			' groupBox5
			' 
			Me.groupBox5.Controls.Add(Me.cbUser)
			Me.groupBox5.Controls.Add(Me.cbTID)
			Me.groupBox5.Controls.Add(Me.cbEPC)
			Me.groupBox5.Controls.Add(Me.cbAccess)
			Me.groupBox5.Controls.Add(Me.cbKill)
			Me.groupBox5.Location = New System.Drawing.Point(51, 115)
			Me.groupBox5.Name = "groupBox5"
			Me.groupBox5.Size = New System.Drawing.Size(519, 76)
			Me.groupBox5.TabIndex = 30
			Me.groupBox5.TabStop = False
			' 
			' cbUser
			' 
			Me.cbUser.AutoSize = True
			Me.cbUser.Checked = True
			Me.cbUser.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbUser.Location = New System.Drawing.Point(408, 31)
			Me.cbUser.Name = "cbUser"
			Me.cbUser.Size = New System.Drawing.Size(58, 20)
			Me.cbUser.TabIndex = 39
			Me.cbUser.TabStop = True
			Me.cbUser.Text = "USER"
			Me.cbUser.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbUser.Click += new System.EventHandler(this.lock_CheckedChanged);
			' 
			' cbTID
			' 
			Me.cbTID.AutoSize = True
			Me.cbTID.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbTID.Location = New System.Drawing.Point(324, 30)
			Me.cbTID.Name = "cbTID"
			Me.cbTID.Size = New System.Drawing.Size(50, 20)
			Me.cbTID.TabIndex = 38
			Me.cbTID.Text = "TID"
			Me.cbTID.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbTID.Click += new System.EventHandler(this.lock_CheckedChanged);
			' 
			' cbEPC
			' 
			Me.cbEPC.AutoSize = True
			Me.cbEPC.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbEPC.Location = New System.Drawing.Point(242, 30)
			Me.cbEPC.Name = "cbEPC"
			Me.cbEPC.Size = New System.Drawing.Size(50, 20)
			Me.cbEPC.TabIndex = 37
			Me.cbEPC.Text = "EPC"
			Me.cbEPC.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbEPC.Click += new System.EventHandler(this.lock_CheckedChanged);
			' 
			' cbAccess
			' 
			Me.cbAccess.AutoSize = True
			Me.cbAccess.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbAccess.Location = New System.Drawing.Point(125, 30)
			Me.cbAccess.Name = "cbAccess"
			Me.cbAccess.Size = New System.Drawing.Size(106, 20)
			Me.cbAccess.TabIndex = 36
			Me.cbAccess.Text = "Access-pwd"
			Me.cbAccess.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbAccess.Click += new System.EventHandler(this.lock_CheckedChanged);
			' 
			' cbKill
			' 
			Me.cbKill.AutoSize = True
			Me.cbKill.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbKill.Location = New System.Drawing.Point(14, 31)
			Me.cbKill.Name = "cbKill"
			Me.cbKill.Size = New System.Drawing.Size(90, 20)
			Me.cbKill.TabIndex = 35
			Me.cbKill.Text = "Kill-pwd"
			Me.cbKill.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbKill.Click += new System.EventHandler(this.lock_CheckedChanged);
			' 
			' groupBox3
			' 
			Me.groupBox3.Controls.Add(Me.rbPermanentLock)
			Me.groupBox3.Controls.Add(Me.rbPermanentOpen)
			Me.groupBox3.Controls.Add(Me.rbTemporaryLock)
			Me.groupBox3.Controls.Add(Me.rbTemporaryOpen)
			Me.groupBox3.Location = New System.Drawing.Point(13, 45)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(605, 64)
			Me.groupBox3.TabIndex = 30
			Me.groupBox3.TabStop = False
			' 
			' rbPermanentLock
			' 
			Me.rbPermanentLock.AutoSize = True
			Me.rbPermanentLock.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbPermanentLock.Location = New System.Drawing.Point(459, 23)
			Me.rbPermanentLock.Name = "rbPermanentLock"
			Me.rbPermanentLock.Size = New System.Drawing.Size(138, 20)
			Me.rbPermanentLock.TabIndex = 43
			Me.rbPermanentLock.TabStop = True
			Me.rbPermanentLock.Text = "Permanent Lock"
			Me.rbPermanentLock.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rbPermanentLock.Click += new System.EventHandler(this.lock_CheckedChanged);
			' 
			' rbPermanentOpen
			' 
			Me.rbPermanentOpen.AutoSize = True
			Me.rbPermanentOpen.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbPermanentOpen.Location = New System.Drawing.Point(315, 23)
			Me.rbPermanentOpen.Name = "rbPermanentOpen"
			Me.rbPermanentOpen.Size = New System.Drawing.Size(138, 20)
			Me.rbPermanentOpen.TabIndex = 42
			Me.rbPermanentOpen.TabStop = True
			Me.rbPermanentOpen.Text = "Permanent Open"
			Me.rbPermanentOpen.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rbPermanentOpen.Click += new System.EventHandler(this.lock_CheckedChanged);
			' 
			' rbTemporaryLock
			' 
			Me.rbTemporaryLock.AutoSize = True
			Me.rbTemporaryLock.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbTemporaryLock.Location = New System.Drawing.Point(160, 23)
			Me.rbTemporaryLock.Name = "rbTemporaryLock"
			Me.rbTemporaryLock.Size = New System.Drawing.Size(58, 20)
			Me.rbTemporaryLock.TabIndex = 41
			Me.rbTemporaryLock.TabStop = True
			Me.rbTemporaryLock.Text = "Lock"
			Me.rbTemporaryLock.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rbTemporaryLock.Click += new System.EventHandler(this.lock_CheckedChanged);
			' 
			' rbTemporaryOpen
			' 
			Me.rbTemporaryOpen.AutoSize = True
			Me.rbTemporaryOpen.Checked = True
			Me.rbTemporaryOpen.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.rbTemporaryOpen.Location = New System.Drawing.Point(16, 23)
			Me.rbTemporaryOpen.Name = "rbTemporaryOpen"
			Me.rbTemporaryOpen.Size = New System.Drawing.Size(58, 20)
			Me.rbTemporaryOpen.TabIndex = 40
			Me.rbTemporaryOpen.TabStop = True
			Me.rbTemporaryOpen.Text = "Open"
			Me.rbTemporaryOpen.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rbTemporaryOpen.Click += new System.EventHandler(this.lock_CheckedChanged);
			' 
			' label7
			' 
			Me.label7.AutoSize = True
			Me.label7.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label7.ForeColor = System.Drawing.Color.FromArgb(CInt(CByte(0)), CInt(CByte(0)), CInt(CByte(192)))
			Me.label7.Location = New System.Drawing.Point(52, 220)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(59, 12)
			Me.label7.TabIndex = 43
			Me.label7.Text = "LockData:"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.ForeColor = System.Drawing.Color.Maroon
			Me.label4.Location = New System.Drawing.Point(414, 30)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(247, 14)
			Me.label4.TabIndex = 42
			Me.label4.Text = "Can't use the default password"
			' 
			' txtLockPwd
			' 
			Me.txtLockPwd.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtLockPwd.Location = New System.Drawing.Point(108, 20)
			Me.txtLockPwd.Name = "txtLockPwd"
			Me.txtLockPwd.Size = New System.Drawing.Size(303, 26)
			Me.txtLockPwd.TabIndex = 41
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtLockPwd.TextChanged += new System.EventHandler(this.txtLockPwd_TextChanged);
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label2.Location = New System.Drawing.Point(15, 24)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(96, 16)
			Me.label2.TabIndex = 40
			Me.label2.Text = "Access Pwd:"
			' 
			' button5
			' 
			Me.button5.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button5.Location = New System.Drawing.Point(253, 209)
			Me.button5.Name = "button5"
			Me.button5.Size = New System.Drawing.Size(90, 31)
			Me.button5.TabIndex = 37
			Me.button5.Text = "Confirm"
			Me.button5.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button5.Click += new System.EventHandler(this.button5_Click);
			' 
			' groupBox7
			' 
			Me.groupBox7.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox7.Controls.Add(Me.label8)
			Me.groupBox7.Controls.Add(Me.cbBlock16)
			Me.groupBox7.Controls.Add(Me.cbBlock15)
			Me.groupBox7.Controls.Add(Me.cbBlock14)
			Me.groupBox7.Controls.Add(Me.cbBlock13)
			Me.groupBox7.Controls.Add(Me.cbBlock12)
			Me.groupBox7.Controls.Add(Me.cbBlock11)
			Me.groupBox7.Controls.Add(Me.cbBlock10)
			Me.groupBox7.Controls.Add(Me.cbBlock9)
			Me.groupBox7.Controls.Add(Me.cbBlock8)
			Me.groupBox7.Controls.Add(Me.cbBlock7)
			Me.groupBox7.Controls.Add(Me.cbBlock6)
			Me.groupBox7.Controls.Add(Me.cbBlock5)
			Me.groupBox7.Controls.Add(Me.cbBlock4)
			Me.groupBox7.Controls.Add(Me.cbBlock3)
			Me.groupBox7.Controls.Add(Me.cbBlock2)
			Me.groupBox7.Controls.Add(Me.cbBlock1)
			Me.groupBox7.Controls.Add(Me.cmbBlockPermalockReadLock)
			Me.groupBox7.Controls.Add(Me.label20)
			Me.groupBox7.Controls.Add(Me.button3)
			Me.groupBox7.Controls.Add(Me.txtBlockPermalockPwd)
			Me.groupBox7.Controls.Add(Me.label23)
			Me.groupBox7.Controls.Add(Me.txtBlockPermalockPtr)
			Me.groupBox7.Controls.Add(Me.label25)
			Me.groupBox7.Controls.Add(Me.cmbBlockPermalockBank)
			Me.groupBox7.Controls.Add(Me.label26)
			Me.groupBox7.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox7.ForeColor = System.Drawing.Color.Black
			Me.groupBox7.Location = New System.Drawing.Point(657, 100)
			Me.groupBox7.Name = "groupBox7"
			Me.groupBox7.Size = New System.Drawing.Size(611, 348)
			Me.groupBox7.TabIndex = 28
			Me.groupBox7.TabStop = False
			Me.groupBox7.Text = "BlockPermalock"
			' 
			' label8
			' 
			Me.label8.AutoSize = True
			Me.label8.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label8.ForeColor = System.Drawing.Color.FromArgb(CInt(CByte(0)), CInt(CByte(0)), CInt(CByte(192)))
			Me.label8.Location = New System.Drawing.Point(71, 285)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(53, 12)
			Me.label8.TabIndex = 49
			Me.label8.Text = "Maskbuf:"
			' 
			' cbBlock16
			' 
			Me.cbBlock16.AutoSize = True
			Me.cbBlock16.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock16.Location = New System.Drawing.Point(535, 242)
			Me.cbBlock16.Name = "cbBlock16"
			Me.cbBlock16.Size = New System.Drawing.Size(72, 16)
			Me.cbBlock16.TabIndex = 48
			Me.cbBlock16.Text = "block-16"
			Me.cbBlock16.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock16.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock15
			' 
			Me.cbBlock15.AutoSize = True
			Me.cbBlock15.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock15.Location = New System.Drawing.Point(463, 242)
			Me.cbBlock15.Name = "cbBlock15"
			Me.cbBlock15.Size = New System.Drawing.Size(72, 16)
			Me.cbBlock15.TabIndex = 47
			Me.cbBlock15.Text = "block-15"
			Me.cbBlock15.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock15.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock14
			' 
			Me.cbBlock14.AutoSize = True
			Me.cbBlock14.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock14.Location = New System.Drawing.Point(391, 242)
			Me.cbBlock14.Name = "cbBlock14"
			Me.cbBlock14.Size = New System.Drawing.Size(72, 16)
			Me.cbBlock14.TabIndex = 46
			Me.cbBlock14.Text = "block-14"
			Me.cbBlock14.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock14.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock13
			' 
			Me.cbBlock13.AutoSize = True
			Me.cbBlock13.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock13.Location = New System.Drawing.Point(317, 242)
			Me.cbBlock13.Name = "cbBlock13"
			Me.cbBlock13.Size = New System.Drawing.Size(72, 16)
			Me.cbBlock13.TabIndex = 45
			Me.cbBlock13.Text = "block-13"
			Me.cbBlock13.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock13.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock12
			' 
			Me.cbBlock12.AutoSize = True
			Me.cbBlock12.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock12.Location = New System.Drawing.Point(245, 242)
			Me.cbBlock12.Name = "cbBlock12"
			Me.cbBlock12.Size = New System.Drawing.Size(72, 16)
			Me.cbBlock12.TabIndex = 44
			Me.cbBlock12.Text = "block-12"
			Me.cbBlock12.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock12.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock11
			' 
			Me.cbBlock11.AutoSize = True
			Me.cbBlock11.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock11.Location = New System.Drawing.Point(165, 242)
			Me.cbBlock11.Name = "cbBlock11"
			Me.cbBlock11.Size = New System.Drawing.Size(72, 16)
			Me.cbBlock11.TabIndex = 43
			Me.cbBlock11.Text = "block-11"
			Me.cbBlock11.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock11.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock10
			' 
			Me.cbBlock10.AutoSize = True
			Me.cbBlock10.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock10.Location = New System.Drawing.Point(93, 242)
			Me.cbBlock10.Name = "cbBlock10"
			Me.cbBlock10.Size = New System.Drawing.Size(72, 16)
			Me.cbBlock10.TabIndex = 42
			Me.cbBlock10.Text = "block-10"
			Me.cbBlock10.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock10.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock9
			' 
			Me.cbBlock9.AutoSize = True
			Me.cbBlock9.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock9.Location = New System.Drawing.Point(23, 242)
			Me.cbBlock9.Name = "cbBlock9"
			Me.cbBlock9.Size = New System.Drawing.Size(66, 16)
			Me.cbBlock9.TabIndex = 41
			Me.cbBlock9.Text = "block-9"
			Me.cbBlock9.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock9.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock8
			' 
			Me.cbBlock8.AutoSize = True
			Me.cbBlock8.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock8.Location = New System.Drawing.Point(535, 198)
			Me.cbBlock8.Name = "cbBlock8"
			Me.cbBlock8.Size = New System.Drawing.Size(66, 16)
			Me.cbBlock8.TabIndex = 40
			Me.cbBlock8.Text = "block-8"
			Me.cbBlock8.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock8.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock7
			' 
			Me.cbBlock7.AutoSize = True
			Me.cbBlock7.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock7.Location = New System.Drawing.Point(463, 198)
			Me.cbBlock7.Name = "cbBlock7"
			Me.cbBlock7.Size = New System.Drawing.Size(66, 16)
			Me.cbBlock7.TabIndex = 39
			Me.cbBlock7.Text = "block-7"
			Me.cbBlock7.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock7.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock6
			' 
			Me.cbBlock6.AutoSize = True
			Me.cbBlock6.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock6.Location = New System.Drawing.Point(391, 198)
			Me.cbBlock6.Name = "cbBlock6"
			Me.cbBlock6.Size = New System.Drawing.Size(66, 16)
			Me.cbBlock6.TabIndex = 38
			Me.cbBlock6.Text = "block-6"
			Me.cbBlock6.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock6.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock5
			' 
			Me.cbBlock5.AutoSize = True
			Me.cbBlock5.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock5.Location = New System.Drawing.Point(317, 198)
			Me.cbBlock5.Name = "cbBlock5"
			Me.cbBlock5.Size = New System.Drawing.Size(66, 16)
			Me.cbBlock5.TabIndex = 37
			Me.cbBlock5.Text = "block-5"
			Me.cbBlock5.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock5.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock4
			' 
			Me.cbBlock4.AutoSize = True
			Me.cbBlock4.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock4.Location = New System.Drawing.Point(245, 198)
			Me.cbBlock4.Name = "cbBlock4"
			Me.cbBlock4.Size = New System.Drawing.Size(66, 16)
			Me.cbBlock4.TabIndex = 36
			Me.cbBlock4.Text = "block-4"
			Me.cbBlock4.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock4.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock3
			' 
			Me.cbBlock3.AutoSize = True
			Me.cbBlock3.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock3.Location = New System.Drawing.Point(165, 198)
			Me.cbBlock3.Name = "cbBlock3"
			Me.cbBlock3.Size = New System.Drawing.Size(66, 16)
			Me.cbBlock3.TabIndex = 35
			Me.cbBlock3.Text = "block-3"
			Me.cbBlock3.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock3.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock2
			' 
			Me.cbBlock2.AutoSize = True
			Me.cbBlock2.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock2.Location = New System.Drawing.Point(93, 198)
			Me.cbBlock2.Name = "cbBlock2"
			Me.cbBlock2.Size = New System.Drawing.Size(66, 16)
			Me.cbBlock2.TabIndex = 34
			Me.cbBlock2.Text = "block-2"
			Me.cbBlock2.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock2.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cbBlock1
			' 
			Me.cbBlock1.AutoSize = True
			Me.cbBlock1.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cbBlock1.Location = New System.Drawing.Point(23, 198)
			Me.cbBlock1.Name = "cbBlock1"
			Me.cbBlock1.Size = New System.Drawing.Size(66, 16)
			Me.cbBlock1.TabIndex = 33
			Me.cbBlock1.Text = "block-1"
			Me.cbBlock1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cbBlock1.Click += new System.EventHandler(this.cbBlock1_Click);
			' 
			' cmbBlockPermalockReadLock
			' 
			Me.cmbBlockPermalockReadLock.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbBlockPermalockReadLock.FormattingEnabled = True
			Me.cmbBlockPermalockReadLock.Items.AddRange(New Object() { "Read", "Permalock"})
			Me.cmbBlockPermalockReadLock.Location = New System.Drawing.Point(108, 148)
			Me.cmbBlockPermalockReadLock.Name = "cmbBlockPermalockReadLock"
			Me.cmbBlockPermalockReadLock.Size = New System.Drawing.Size(303, 24)
			Me.cmbBlockPermalockReadLock.TabIndex = 32
			' 
			' label20
			' 
			Me.label20.AutoSize = True
			Me.label20.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label20.Location = New System.Drawing.Point(15, 154)
			Me.label20.Name = "label20"
			Me.label20.Size = New System.Drawing.Size(80, 16)
			Me.label20.TabIndex = 31
			Me.label20.Text = "ReadLock:"
			' 
			' button3
			' 
			Me.button3.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button3.Location = New System.Drawing.Point(275, 274)
			Me.button3.Name = "button3"
			Me.button3.Size = New System.Drawing.Size(90, 31)
			Me.button3.TabIndex = 30
			Me.button3.Text = "Confirm"
			Me.button3.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button3.Click += new System.EventHandler(this.button3_Click);
			' 
			' txtBlockPermalockPwd
			' 
			Me.txtBlockPermalockPwd.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtBlockPermalockPwd.Location = New System.Drawing.Point(108, 101)
			Me.txtBlockPermalockPwd.Name = "txtBlockPermalockPwd"
			Me.txtBlockPermalockPwd.Size = New System.Drawing.Size(303, 26)
			Me.txtBlockPermalockPwd.TabIndex = 21
			Me.txtBlockPermalockPwd.Text = "00000000"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtBlockPermalockPwd.TextChanged += new System.EventHandler(this.txtBlockPermalockPwd_TextChanged);
			' 
			' label23
			' 
			Me.label23.AutoSize = True
			Me.label23.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label23.Location = New System.Drawing.Point(15, 105)
			Me.label23.Name = "label23"
			Me.label23.Size = New System.Drawing.Size(96, 16)
			Me.label23.TabIndex = 20
			Me.label23.Text = "Access Pwd:"
			' 
			' txtBlockPermalockPtr
			' 
			Me.txtBlockPermalockPtr.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtBlockPermalockPtr.Location = New System.Drawing.Point(108, 58)
			Me.txtBlockPermalockPtr.Name = "txtBlockPermalockPtr"
			Me.txtBlockPermalockPtr.Size = New System.Drawing.Size(303, 26)
			Me.txtBlockPermalockPtr.TabIndex = 18
			Me.txtBlockPermalockPtr.Text = "0"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtBlockPermalockPtr.TextChanged += new System.EventHandler(this.txtBlockPermalockPtr_TextChanged);
			' 
			' label25
			' 
			Me.label25.AutoSize = True
			Me.label25.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label25.Location = New System.Drawing.Point(15, 68)
			Me.label25.Name = "label25"
			Me.label25.Size = New System.Drawing.Size(40, 16)
			Me.label25.TabIndex = 16
			Me.label25.Text = "Ptr:"
			' 
			' cmbBlockPermalockBank
			' 
			Me.cmbBlockPermalockBank.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.cmbBlockPermalockBank.FormattingEnabled = True
			Me.cmbBlockPermalockBank.Items.AddRange(New Object() { "RESERVED", "EPC", "TID", "USER"})
			Me.cmbBlockPermalockBank.Location = New System.Drawing.Point(108, 20)
			Me.cmbBlockPermalockBank.Name = "cmbBlockPermalockBank"
			Me.cmbBlockPermalockBank.Size = New System.Drawing.Size(303, 24)
			Me.cmbBlockPermalockBank.TabIndex = 15
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cmbBlockPermalockBank.TextChanged += new System.EventHandler(this.cmbBlockPermalockBank_SelectedIndexChanged);
			' 
			' label26
			' 
			Me.label26.AutoSize = True
			Me.label26.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label26.Location = New System.Drawing.Point(15, 28)
			Me.label26.Name = "label26"
			Me.label26.Size = New System.Drawing.Size(48, 16)
			Me.label26.TabIndex = 14
			Me.label26.Text = "Bank:"
			' 
			' panel1
			' 
			Me.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.panel1.Controls.Add(Me.groupBox9)
			Me.panel1.Controls.Add(Me.groupBox6)
			Me.panel1.Controls.Add(Me.groupBox7)
			Me.panel1.Controls.Add(Me.groupBox2)
			Me.panel1.Controls.Add(Me.groupBox1)
			Me.panel1.Controls.Add(Me.groupBox4)
			Me.panel1.Location = New System.Drawing.Point(5, -5)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(1291, 646)
			Me.panel1.TabIndex = 29
			' 
			' groupBox9
			' 
			Me.groupBox9.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox9.Controls.Add(Me.label17)
			Me.groupBox9.Controls.Add(Me.label18)
			Me.groupBox9.Controls.Add(Me.txtGBPWD)
			Me.groupBox9.Controls.Add(Me.label19)
			Me.groupBox9.Controls.Add(Me.button4)
			Me.groupBox9.Controls.Add(Me.comboAction)
			Me.groupBox9.Controls.Add(Me.label16)
			Me.groupBox9.Controls.Add(Me.comboConfig)
			Me.groupBox9.Controls.Add(Me.label15)
			Me.groupBox9.Controls.Add(Me.comboBank)
			Me.groupBox9.Controls.Add(Me.label14)
			Me.groupBox9.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox9.Location = New System.Drawing.Point(16, 366)
			Me.groupBox9.Name = "groupBox9"
			Me.groupBox9.Size = New System.Drawing.Size(625, 217)
			Me.groupBox9.TabIndex = 32
			Me.groupBox9.TabStop = False
			Me.groupBox9.Text = "国标标签Lock"
			' 
			' label17
			' 
			Me.label17.AutoSize = True
			Me.label17.Location = New System.Drawing.Point(94, 82)
			Me.label17.Name = "label17"
			Me.label17.Size = New System.Drawing.Size(87, 14)
			Me.label17.TabIndex = 46
			Me.label17.TabStop = True
			Me.label17.Text = "linkLabel1"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.label17.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.label17_LinkClicked);
			' 
			' label18
			' 
			Me.label18.AutoSize = True
			Me.label18.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label18.ForeColor = System.Drawing.Color.Maroon
			Me.label18.Location = New System.Drawing.Point(402, 31)
			Me.label18.Name = "label18"
			Me.label18.Size = New System.Drawing.Size(185, 12)
			Me.label18.TabIndex = 45
			Me.label18.Text = "Can't use the default password"
			' 
			' txtGBPWD
			' 
			Me.txtGBPWD.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtGBPWD.Location = New System.Drawing.Point(96, 21)
			Me.txtGBPWD.Name = "txtGBPWD"
			Me.txtGBPWD.Size = New System.Drawing.Size(303, 26)
			Me.txtGBPWD.TabIndex = 44
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtGBPWD.TextChanged += new System.EventHandler(this.txtGBPWD_TextChanged);
			' 
			' label19
			' 
			Me.label19.AutoSize = True
			Me.label19.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label19.Location = New System.Drawing.Point(3, 25)
			Me.label19.Name = "label19"
			Me.label19.Size = New System.Drawing.Size(96, 16)
			Me.label19.TabIndex = 43
			Me.label19.Text = "Access Pwd:"
			' 
			' button4
			' 
			Me.button4.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button4.Location = New System.Drawing.Point(253, 167)
			Me.button4.Name = "button4"
			Me.button4.Size = New System.Drawing.Size(90, 31)
			Me.button4.TabIndex = 38
			Me.button4.Text = "Confirm"
			Me.button4.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button4.Click += new System.EventHandler(this.button4_Click);
			' 
			' comboAction
			' 
			Me.comboAction.Enabled = False
			Me.comboAction.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.comboAction.FormattingEnabled = True
			Me.comboAction.Items.AddRange(New Object() { "存储区属性", "安全模式"})
			Me.comboAction.Location = New System.Drawing.Point(96, 131)
			Me.comboAction.Name = "comboAction"
			Me.comboAction.Size = New System.Drawing.Size(487, 24)
			Me.comboAction.TabIndex = 20
			' 
			' label16
			' 
			Me.label16.AutoSize = True
			Me.label16.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label16.Location = New System.Drawing.Point(6, 139)
			Me.label16.Name = "label16"
			Me.label16.Size = New System.Drawing.Size(64, 16)
			Me.label16.TabIndex = 19
			Me.label16.Text = "Action:"
			' 
			' comboConfig
			' 
			Me.comboConfig.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.comboConfig.FormattingEnabled = True
			Me.comboConfig.Items.AddRange(New Object() { "存储区属性", "安全模式"})
			Me.comboConfig.Location = New System.Drawing.Point(96, 101)
			Me.comboConfig.Name = "comboConfig"
			Me.comboConfig.Size = New System.Drawing.Size(487, 24)
			Me.comboConfig.TabIndex = 18
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.comboConfig.SelectedIndexChanged += new System.EventHandler(this.comboConfig_SelectedIndexChanged);
			' 
			' label15
			' 
			Me.label15.AutoSize = True
			Me.label15.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label15.Location = New System.Drawing.Point(6, 104)
			Me.label15.Name = "label15"
			Me.label15.Size = New System.Drawing.Size(64, 16)
			Me.label15.TabIndex = 17
			Me.label15.Text = "Config:"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.label15.Click += new System.EventHandler(this.label15_Click);
			' 
			' comboBank
			' 
			Me.comboBank.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.comboBank.FormattingEnabled = True
			Me.comboBank.Items.AddRange(New Object() { "标签信息区", "编码区", "安全区", "用户区"})
			Me.comboBank.Location = New System.Drawing.Point(96, 53)
			Me.comboBank.Name = "comboBank"
			Me.comboBank.Size = New System.Drawing.Size(487, 24)
			Me.comboBank.TabIndex = 16
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.comboBank.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			' 
			' label14
			' 
			Me.label14.AutoSize = True
			Me.label14.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label14.Location = New System.Drawing.Point(3, 56)
			Me.label14.Name = "label14"
			Me.label14.Size = New System.Drawing.Size(48, 16)
			Me.label14.TabIndex = 15
			Me.label14.Text = "Bank:"
			' 
			' groupBox6
			' 
			Me.groupBox6.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox6.Controls.Add(Me.label29)
			Me.groupBox6.Controls.Add(Me.txtLen)
			Me.groupBox6.Controls.Add(Me.label24)
			Me.groupBox6.Controls.Add(Me.label3)
			Me.groupBox6.Controls.Add(Me.txtPtr)
			Me.groupBox6.Controls.Add(Me.groupBox8)
			Me.groupBox6.Controls.Add(Me.label22)
			Me.groupBox6.Controls.Add(Me.label30)
			Me.groupBox6.Controls.Add(Me.txtFilter_EPC)
			Me.groupBox6.Controls.Add(Me.label12)
			Me.groupBox6.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox6.ForeColor = System.Drawing.Color.Black
			Me.groupBox6.Location = New System.Drawing.Point(17, 15)
			Me.groupBox6.Name = "groupBox6"
			Me.groupBox6.Size = New System.Drawing.Size(1250, 72)
			Me.groupBox6.TabIndex = 30
			Me.groupBox6.TabStop = False
			Me.groupBox6.Text = "filter"
			' 
			' label29
			' 
			Me.label29.AutoSize = True
			Me.label29.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label29.ForeColor = System.Drawing.Color.Black
			Me.label29.Location = New System.Drawing.Point(592, 31)
			Me.label29.Name = "label29"
			Me.label29.Size = New System.Drawing.Size(16, 16)
			Me.label29.TabIndex = 37
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
'ORIGINAL LINE: this.txtLen.TextChanged += new System.EventHandler(this.txtLen_TextChanged);
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
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label3.Location = New System.Drawing.Point(1037, 35)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(64, 16)
			Me.label3.TabIndex = 36
			Me.label3.Text = "Length:"
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
			' groupBox8
			' 
			Me.groupBox8.Controls.Add(Me.rbUser)
			Me.groupBox8.Controls.Add(Me.rbEPC)
			Me.groupBox8.Controls.Add(Me.rbTID)
			Me.groupBox8.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox8.Location = New System.Drawing.Point(648, 15)
			Me.groupBox8.Name = "groupBox8"
			Me.groupBox8.Size = New System.Drawing.Size(178, 47)
			Me.groupBox8.TabIndex = 34
			Me.groupBox8.TabStop = False
			Me.groupBox8.Text = "bank"
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
			' groupBox4
			' 
			Me.groupBox4.Controls.Add(Me.label13)
			Me.groupBox4.Controls.Add(Me.label11)
			Me.groupBox4.Controls.Add(Me.textBox2)
			Me.groupBox4.Controls.Add(Me.label10)
			Me.groupBox4.Controls.Add(Me.label1)
			Me.groupBox4.Controls.Add(Me.button1)
			Me.groupBox4.Controls.Add(Me.textBox1)
			Me.groupBox4.Controls.Add(Me.label9)
			Me.groupBox4.Location = New System.Drawing.Point(949, 462)
			Me.groupBox4.Name = "groupBox4"
			Me.groupBox4.Size = New System.Drawing.Size(324, 128)
			Me.groupBox4.TabIndex = 31
			Me.groupBox4.TabStop = False
			Me.groupBox4.Text = "Deactivate/re-activation"
			Me.groupBox4.Visible = False
			' 
			' label13
			' 
			Me.label13.AutoSize = True
			Me.label13.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label13.ForeColor = System.Drawing.Color.FromArgb(CInt(CByte(0)), CInt(CByte(0)), CInt(CByte(192)))
			Me.label13.Location = New System.Drawing.Point(103, 17)
			Me.label13.Name = "label13"
			Me.label13.Size = New System.Drawing.Size(88, 16)
			Me.label13.TabIndex = 51
			Me.label13.Text = "隐藏不使用"
			' 
			' label11
			' 
			Me.label11.AutoSize = True
			Me.label11.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label11.Location = New System.Drawing.Point(413, 45)
			Me.label11.Name = "label11"
			Me.label11.Size = New System.Drawing.Size(32, 16)
			Me.label11.TabIndex = 50
			Me.label11.Text = "hex"
			' 
			' textBox2
			' 
			Me.textBox2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.textBox2.Location = New System.Drawing.Point(108, 42)
			Me.textBox2.MaxLength = 4
			Me.textBox2.Name = "textBox2"
			Me.textBox2.Size = New System.Drawing.Size(303, 26)
			Me.textBox2.TabIndex = 49
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			' 
			' label10
			' 
			Me.label10.AutoSize = True
			Me.label10.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label10.Location = New System.Drawing.Point(15, 42)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(40, 16)
			Me.label10.TabIndex = 48
			Me.label10.Text = "cmd:"
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.ForeColor = System.Drawing.Color.Maroon
			Me.label1.Location = New System.Drawing.Point(417, 102)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(185, 12)
			Me.label1.TabIndex = 47
			Me.label1.Text = "Can't use the default password"
			' 
			' button1
			' 
			Me.button1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.button1.ForeColor = System.Drawing.Color.Black
			Me.button1.Location = New System.Drawing.Point(256, 129)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(90, 31)
			Me.button1.TabIndex = 44
			Me.button1.Text = "Confirm"
			Me.button1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button1.Click += new System.EventHandler(this.button1_Click);
			' 
			' textBox1
			' 
			Me.textBox1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.textBox1.Location = New System.Drawing.Point(108, 95)
			Me.textBox1.Name = "textBox1"
			Me.textBox1.Size = New System.Drawing.Size(303, 26)
			Me.textBox1.TabIndex = 46
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			' 
			' label9
			' 
			Me.label9.AutoSize = True
			Me.label9.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label9.Location = New System.Drawing.Point(15, 99)
			Me.label9.Name = "label9"
			Me.label9.Size = New System.Drawing.Size(96, 16)
			Me.label9.TabIndex = 45
			Me.label9.Text = "Access Pwd:"
			' 
			' Kill_LockForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.ClientSize = New System.Drawing.Size(1290, 631)
			Me.Controls.Add(Me.panel1)
			Me.KeyPreview = True
			Me.Name = "Kill_LockForm"
			Me.Text = "Kill_LockForm"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.Kill_LockForm_Load);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Kill_LockForm_FormClosing);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Kill_LockForm_KeyDown);
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.groupBox5.ResumeLayout(False)
			Me.groupBox5.PerformLayout()
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox3.PerformLayout()
			Me.groupBox7.ResumeLayout(False)
			Me.groupBox7.PerformLayout()
			Me.panel1.ResumeLayout(False)
			Me.groupBox9.ResumeLayout(False)
			Me.groupBox9.PerformLayout()
			Me.groupBox6.ResumeLayout(False)
			Me.groupBox6.PerformLayout()
			Me.groupBox8.ResumeLayout(False)
			Me.groupBox8.PerformLayout()
			Me.groupBox4.ResumeLayout(False)
			Me.groupBox4.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private groupBox1 As System.Windows.Forms.GroupBox
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private WithEvents button2 As System.Windows.Forms.Button
		Private WithEvents txtKill_AccessPwd As System.Windows.Forms.TextBox
		Private label5 As System.Windows.Forms.Label
		Private groupBox7 As System.Windows.Forms.GroupBox
		Private cmbBlockPermalockReadLock As System.Windows.Forms.ComboBox
		Private label20 As System.Windows.Forms.Label
		Private WithEvents button3 As System.Windows.Forms.Button
		Private WithEvents txtBlockPermalockPwd As System.Windows.Forms.TextBox
		Private label23 As System.Windows.Forms.Label
		Private WithEvents txtBlockPermalockPtr As System.Windows.Forms.TextBox
		Private label25 As System.Windows.Forms.Label
		Private WithEvents cmbBlockPermalockBank As System.Windows.Forms.ComboBox
		Private label26 As System.Windows.Forms.Label
		Private WithEvents cbBlock1 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock16 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock15 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock14 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock13 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock12 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock11 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock10 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock9 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock7 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock6 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock5 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock4 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock3 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock2 As System.Windows.Forms.CheckBox
		Private WithEvents cbBlock8 As System.Windows.Forms.CheckBox
		Private WithEvents button5 As System.Windows.Forms.Button
		Private WithEvents txtLockPwd As System.Windows.Forms.TextBox
		Private label2 As System.Windows.Forms.Label
		Private panel1 As System.Windows.Forms.Panel
		Private label4 As System.Windows.Forms.Label
		Private label6 As System.Windows.Forms.Label
		Private label7 As System.Windows.Forms.Label
		Private label8 As System.Windows.Forms.Label
		Private groupBox3 As System.Windows.Forms.GroupBox
		Private WithEvents rbPermanentLock As System.Windows.Forms.RadioButton
		Private WithEvents rbPermanentOpen As System.Windows.Forms.RadioButton
		Private WithEvents rbTemporaryLock As System.Windows.Forms.RadioButton
		Private WithEvents rbTemporaryOpen As System.Windows.Forms.RadioButton
		Private groupBox5 As System.Windows.Forms.GroupBox
		Private WithEvents cbKill As System.Windows.Forms.RadioButton
		Private WithEvents cbUser As System.Windows.Forms.RadioButton
		Private WithEvents cbTID As System.Windows.Forms.RadioButton
		Private WithEvents cbEPC As System.Windows.Forms.RadioButton
		Private WithEvents cbAccess As System.Windows.Forms.RadioButton
		Private groupBox6 As System.Windows.Forms.GroupBox
		Private WithEvents txtLen As System.Windows.Forms.TextBox
		Private label24 As System.Windows.Forms.Label
		Private label3 As System.Windows.Forms.Label
		Private WithEvents txtPtr As System.Windows.Forms.TextBox
		Private groupBox8 As System.Windows.Forms.GroupBox
		Private WithEvents rbUser As System.Windows.Forms.RadioButton
		Private WithEvents rbEPC As System.Windows.Forms.RadioButton
		Private WithEvents rbTID As System.Windows.Forms.RadioButton
		Private label22 As System.Windows.Forms.Label
		Private label30 As System.Windows.Forms.Label
		Private WithEvents txtFilter_EPC As System.Windows.Forms.TextBox
		Private label12 As System.Windows.Forms.Label
		Private groupBox4 As System.Windows.Forms.GroupBox
		Private WithEvents textBox2 As System.Windows.Forms.TextBox
		Private label10 As System.Windows.Forms.Label
		Private label1 As System.Windows.Forms.Label
		Private WithEvents button1 As System.Windows.Forms.Button
		Private WithEvents textBox1 As System.Windows.Forms.TextBox
		Private label9 As System.Windows.Forms.Label
		Private label11 As System.Windows.Forms.Label
		Private label29 As System.Windows.Forms.Label
		Private groupBox9 As System.Windows.Forms.GroupBox
		Private label13 As System.Windows.Forms.Label
		Private label14 As System.Windows.Forms.Label
		Private WithEvents comboBank As System.Windows.Forms.ComboBox
		Private comboAction As System.Windows.Forms.ComboBox
		Private label16 As System.Windows.Forms.Label
		Private WithEvents comboConfig As System.Windows.Forms.ComboBox
		Private WithEvents label15 As System.Windows.Forms.Label
		Private WithEvents button4 As System.Windows.Forms.Button
		Private label18 As System.Windows.Forms.Label
		Private WithEvents txtGBPWD As System.Windows.Forms.TextBox
		Private label19 As System.Windows.Forms.Label
		Private WithEvents label17 As System.Windows.Forms.LinkLabel
	End Class
End Namespace
