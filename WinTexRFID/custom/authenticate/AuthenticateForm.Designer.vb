Namespace UHFAPP.custom.authenticate
	Partial Public Class AuthenticateForm
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
			Me.groupBox4 = New System.Windows.Forms.GroupBox()
			Me.txtFilter_EPCLen = New System.Windows.Forms.Label()
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
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.txtEncryptionData2Len = New System.Windows.Forms.Label()
			Me.groupBox6 = New System.Windows.Forms.GroupBox()
			Me.btnWrite = New System.Windows.Forms.Button()
			Me.btnActivate = New System.Windows.Forms.Button()
			Me.txtKey0DataLen = New System.Windows.Forms.Label()
			Me.btnRead = New System.Windows.Forms.Button()
			Me.txtKey0Data = New System.Windows.Forms.TextBox()
			Me.lblKey0 = New System.Windows.Forms.Label()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.txtDataLen = New System.Windows.Forms.Label()
			Me.txtAuthenticateEncryptionData = New System.Windows.Forms.TextBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.txtAuthenticateData = New System.Windows.Forms.TextBox()
			Me.btnAuthenticate = New System.Windows.Forms.Button()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label5 = New System.Windows.Forms.Label()
			Me.txtAuthenticateKeyID = New System.Windows.Forms.TextBox()
			Me.txtDecodeCiphertext = New System.Windows.Forms.TextBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.groupBox5 = New System.Windows.Forms.GroupBox()
			Me.lblencryptoKeyLen = New System.Windows.Forms.Label()
			Me.txtC = New System.Windows.Forms.TextBox()
			Me.label9 = New System.Windows.Forms.Label()
			Me.txtRnd = New System.Windows.Forms.TextBox()
			Me.label8 = New System.Windows.Forms.Label()
			Me.txtMsg = New System.Windows.Forms.TextBox()
			Me.label7 = New System.Windows.Forms.Label()
			Me.btnDecode = New System.Windows.Forms.Button()
			Me.label6 = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.decodeKey = New System.Windows.Forms.TextBox()
			Me.txtData2Len = New System.Windows.Forms.Label()
			Me.groupBox4.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			Me.groupBox1.SuspendLayout()
			Me.groupBox6.SuspendLayout()
			Me.groupBox2.SuspendLayout()
			Me.groupBox5.SuspendLayout()
			Me.SuspendLayout()
			' 
			' groupBox4
			' 
			Me.groupBox4.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox4.Controls.Add(Me.txtFilter_EPCLen)
			Me.groupBox4.Controls.Add(Me.txtLen)
			Me.groupBox4.Controls.Add(Me.label24)
			Me.groupBox4.Controls.Add(Me.label23)
			Me.groupBox4.Controls.Add(Me.txtPtr)
			Me.groupBox4.Controls.Add(Me.groupBox3)
			Me.groupBox4.Controls.Add(Me.label22)
			Me.groupBox4.Controls.Add(Me.label30)
			Me.groupBox4.Controls.Add(Me.txtFilter_EPC)
			Me.groupBox4.Controls.Add(Me.label12)
			Me.groupBox4.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox4.ForeColor = System.Drawing.Color.Black
			Me.groupBox4.Location = New System.Drawing.Point(13, 20)
			Me.groupBox4.Name = "groupBox4"
			Me.groupBox4.Size = New System.Drawing.Size(531, 147)
			Me.groupBox4.TabIndex = 29
			Me.groupBox4.TabStop = False
			Me.groupBox4.Text = "Filter"
			' 
			' txtFilter_EPCLen
			' 
			Me.txtFilter_EPCLen.AutoSize = True
			Me.txtFilter_EPCLen.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtFilter_EPCLen.Location = New System.Drawing.Point(487, 22)
			Me.txtFilter_EPCLen.Name = "txtFilter_EPCLen"
			Me.txtFilter_EPCLen.Size = New System.Drawing.Size(0, 12)
			Me.txtFilter_EPCLen.TabIndex = 39
			' 
			' txtLen
			' 
			Me.txtLen.Font = New System.Drawing.Font("宋体", 9F)
			Me.txtLen.Location = New System.Drawing.Point(264, 113)
			Me.txtLen.MaxLength = 3
			Me.txtLen.Name = "txtLen"
			Me.txtLen.Size = New System.Drawing.Size(82, 21)
			Me.txtLen.TabIndex = 37
			Me.txtLen.Tag = "Number"
			Me.txtLen.Text = "0"
			' 
			' label24
			' 
			Me.label24.AutoSize = True
			Me.label24.Font = New System.Drawing.Font("宋体", 9F)
			Me.label24.Location = New System.Drawing.Point(350, 118)
			Me.label24.Name = "label24"
			Me.label24.Size = New System.Drawing.Size(35, 12)
			Me.label24.TabIndex = 38
			Me.label24.Text = "(bit)"
			' 
			' label23
			' 
			Me.label23.AutoSize = True
			Me.label23.Font = New System.Drawing.Font("宋体", 9F)
			Me.label23.Location = New System.Drawing.Point(218, 118)
			Me.label23.Name = "label23"
			Me.label23.Size = New System.Drawing.Size(47, 12)
			Me.label23.TabIndex = 36
			Me.label23.Text = "Length:"
			' 
			' txtPtr
			' 
			Me.txtPtr.Font = New System.Drawing.Font("宋体", 9F)
			Me.txtPtr.Location = New System.Drawing.Point(73, 113)
			Me.txtPtr.MaxLength = 3
			Me.txtPtr.Name = "txtPtr"
			Me.txtPtr.Size = New System.Drawing.Size(82, 21)
			Me.txtPtr.TabIndex = 33
			Me.txtPtr.Tag = "Number"
			Me.txtPtr.Text = "32"
			' 
			' groupBox3
			' 
			Me.groupBox3.Controls.Add(Me.rbUser)
			Me.groupBox3.Controls.Add(Me.rbEPC)
			Me.groupBox3.Controls.Add(Me.rbTID)
			Me.groupBox3.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox3.Location = New System.Drawing.Point(72, 58)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(296, 47)
			Me.groupBox3.TabIndex = 34
			Me.groupBox3.TabStop = False
			Me.groupBox3.Text = "bank"
			' 
			' rbUser
			' 
			Me.rbUser.AutoSize = True
			Me.rbUser.Font = New System.Drawing.Font("宋体", 9F)
			Me.rbUser.Location = New System.Drawing.Point(114, 20)
			Me.rbUser.Name = "rbUser"
			Me.rbUser.Size = New System.Drawing.Size(47, 16)
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
			Me.rbEPC.Font = New System.Drawing.Font("宋体", 9F)
			Me.rbEPC.Location = New System.Drawing.Point(11, 19)
			Me.rbEPC.Name = "rbEPC"
			Me.rbEPC.Size = New System.Drawing.Size(41, 16)
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
			Me.rbTID.Font = New System.Drawing.Font("宋体", 9F)
			Me.rbTID.Location = New System.Drawing.Point(67, 20)
			Me.rbTID.Name = "rbTID"
			Me.rbTID.Size = New System.Drawing.Size(41, 16)
			Me.rbTID.TabIndex = 9
			Me.rbTID.Text = "TID"
			Me.rbTID.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.rbTID.Click += new System.EventHandler(this.rbTID_Click);
			' 
			' label22
			' 
			Me.label22.AutoSize = True
			Me.label22.Font = New System.Drawing.Font("宋体", 9F)
			Me.label22.Location = New System.Drawing.Point(155, 119)
			Me.label22.Name = "label22"
			Me.label22.Size = New System.Drawing.Size(35, 12)
			Me.label22.TabIndex = 35
			Me.label22.Text = "(bit)"
			' 
			' label30
			' 
			Me.label30.AutoSize = True
			Me.label30.Font = New System.Drawing.Font("宋体", 9F)
			Me.label30.Location = New System.Drawing.Point(37, 117)
			Me.label30.Name = "label30"
			Me.label30.Size = New System.Drawing.Size(29, 12)
			Me.label30.TabIndex = 32
			Me.label30.Text = "Ptr:"
			' 
			' txtFilter_EPC
			' 
			Me.txtFilter_EPC.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtFilter_EPC.Location = New System.Drawing.Point(73, 14)
			Me.txtFilter_EPC.Multiline = True
			Me.txtFilter_EPC.Name = "txtFilter_EPC"
			Me.txtFilter_EPC.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtFilter_EPC.Size = New System.Drawing.Size(415, 38)
			Me.txtFilter_EPC.TabIndex = 12
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtFilter_EPC.TextChanged += new System.EventHandler(this.txtFilter_EPC_TextChanged);
			' 
			' label12
			' 
			Me.label12.AutoSize = True
			Me.label12.Font = New System.Drawing.Font("宋体", 9F)
			Me.label12.ForeColor = System.Drawing.Color.Black
			Me.label12.Location = New System.Drawing.Point(37, 26)
			Me.label12.Name = "label12"
			Me.label12.Size = New System.Drawing.Size(35, 12)
			Me.label12.TabIndex = 11
			Me.label12.Text = "Data:"
			' 
			' groupBox1
			' 
			Me.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox1.Controls.Add(Me.txtEncryptionData2Len)
			Me.groupBox1.Controls.Add(Me.groupBox6)
			Me.groupBox1.Controls.Add(Me.groupBox2)
			Me.groupBox1.Controls.Add(Me.groupBox4)
			Me.groupBox1.Location = New System.Drawing.Point(12, 12)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(559, 501)
			Me.groupBox1.TabIndex = 30
			Me.groupBox1.TabStop = False
			' 
			' txtEncryptionData2Len
			' 
			Me.txtEncryptionData2Len.AutoSize = True
			Me.txtEncryptionData2Len.Location = New System.Drawing.Point(513, 298)
			Me.txtEncryptionData2Len.Name = "txtEncryptionData2Len"
			Me.txtEncryptionData2Len.Size = New System.Drawing.Size(0, 12)
			Me.txtEncryptionData2Len.TabIndex = 53
			' 
			' groupBox6
			' 
			Me.groupBox6.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox6.Controls.Add(Me.btnWrite)
			Me.groupBox6.Controls.Add(Me.btnActivate)
			Me.groupBox6.Controls.Add(Me.txtKey0DataLen)
			Me.groupBox6.Controls.Add(Me.btnRead)
			Me.groupBox6.Controls.Add(Me.txtKey0Data)
			Me.groupBox6.Controls.Add(Me.lblKey0)
			Me.groupBox6.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox6.Location = New System.Drawing.Point(13, 390)
			Me.groupBox6.Name = "groupBox6"
			Me.groupBox6.Size = New System.Drawing.Size(531, 95)
			Me.groupBox6.TabIndex = 53
			Me.groupBox6.TabStop = False
			Me.groupBox6.Text = "Read/Write/Activate"
			' 
			' btnWrite
			' 
			Me.btnWrite.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnWrite.Location = New System.Drawing.Point(179, 47)
			Me.btnWrite.Name = "btnWrite"
			Me.btnWrite.Size = New System.Drawing.Size(86, 29)
			Me.btnWrite.TabIndex = 54
			Me.btnWrite.Text = "Write"
			Me.btnWrite.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
			' 
			' btnActivate
			' 
			Me.btnActivate.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnActivate.Location = New System.Drawing.Point(310, 47)
			Me.btnActivate.Name = "btnActivate"
			Me.btnActivate.Size = New System.Drawing.Size(86, 29)
			Me.btnActivate.TabIndex = 48
			Me.btnActivate.Text = "Activate"
			Me.btnActivate.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
			' 
			' txtKey0DataLen
			' 
			Me.txtKey0DataLen.AutoSize = True
			Me.txtKey0DataLen.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtKey0DataLen.Location = New System.Drawing.Point(498, 29)
			Me.txtKey0DataLen.Name = "txtKey0DataLen"
			Me.txtKey0DataLen.Size = New System.Drawing.Size(0, 12)
			Me.txtKey0DataLen.TabIndex = 53
			' 
			' btnRead
			' 
			Me.btnRead.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnRead.Location = New System.Drawing.Point(60, 47)
			Me.btnRead.Name = "btnRead"
			Me.btnRead.Size = New System.Drawing.Size(86, 29)
			Me.btnRead.TabIndex = 53
			Me.btnRead.Text = "Read"
			Me.btnRead.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
			' 
			' txtKey0Data
			' 
			Me.txtKey0Data.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtKey0Data.Location = New System.Drawing.Point(46, 20)
			Me.txtKey0Data.MaxLength = 48
			Me.txtKey0Data.Name = "txtKey0Data"
			Me.txtKey0Data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtKey0Data.Size = New System.Drawing.Size(448, 21)
			Me.txtKey0Data.TabIndex = 52
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtKey0Data.TextChanged += new System.EventHandler(this.txtKey0Data_TextChanged);
			' 
			' lblKey0
			' 
			Me.lblKey0.AutoSize = True
			Me.lblKey0.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.lblKey0.Location = New System.Drawing.Point(11, 27)
			Me.lblKey0.Name = "lblKey0"
			Me.lblKey0.Size = New System.Drawing.Size(29, 12)
			Me.lblKey0.TabIndex = 0
			Me.lblKey0.Text = "Key0"
			' 
			' groupBox2
			' 
			Me.groupBox2.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox2.Controls.Add(Me.txtDataLen)
			Me.groupBox2.Controls.Add(Me.txtAuthenticateEncryptionData)
			Me.groupBox2.Controls.Add(Me.label1)
			Me.groupBox2.Controls.Add(Me.txtAuthenticateData)
			Me.groupBox2.Controls.Add(Me.btnAuthenticate)
			Me.groupBox2.Controls.Add(Me.label3)
			Me.groupBox2.Controls.Add(Me.label5)
			Me.groupBox2.Controls.Add(Me.txtAuthenticateKeyID)
			Me.groupBox2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox2.Location = New System.Drawing.Point(13, 183)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(531, 201)
			Me.groupBox2.TabIndex = 52
			Me.groupBox2.TabStop = False
			Me.groupBox2.Text = "AES Authenticate"
			' 
			' txtDataLen
			' 
			Me.txtDataLen.AutoSize = True
			Me.txtDataLen.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtDataLen.Location = New System.Drawing.Point(498, 78)
			Me.txtDataLen.Name = "txtDataLen"
			Me.txtDataLen.Size = New System.Drawing.Size(0, 12)
			Me.txtDataLen.TabIndex = 52
			' 
			' txtAuthenticateEncryptionData
			' 
			Me.txtAuthenticateEncryptionData.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtAuthenticateEncryptionData.Location = New System.Drawing.Point(102, 110)
			Me.txtAuthenticateEncryptionData.Name = "txtAuthenticateEncryptionData"
			Me.txtAuthenticateEncryptionData.ReadOnly = True
			Me.txtAuthenticateEncryptionData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtAuthenticateEncryptionData.Size = New System.Drawing.Size(392, 21)
			Me.txtAuthenticateEncryptionData.TabIndex = 46
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtAuthenticateEncryptionData.TextChanged += new System.EventHandler(this.txtEncryptionData2_TextChanged);
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("宋体", 9F)
			Me.label1.ForeColor = System.Drawing.Color.Black
			Me.label1.Location = New System.Drawing.Point(2, 43)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(47, 12)
			Me.label1.TabIndex = 48
			Me.label1.Text = "Key ID:"
			' 
			' txtAuthenticateData
			' 
			Me.txtAuthenticateData.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtAuthenticateData.Location = New System.Drawing.Point(102, 74)
			Me.txtAuthenticateData.MaxLength = 30
			Me.txtAuthenticateData.Name = "txtAuthenticateData"
			Me.txtAuthenticateData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtAuthenticateData.Size = New System.Drawing.Size(392, 21)
			Me.txtAuthenticateData.TabIndex = 51
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtAuthenticateData.TextChanged += new System.EventHandler(this.txtData_TextChanged);
			' 
			' btnAuthenticate
			' 
			Me.btnAuthenticate.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnAuthenticate.Location = New System.Drawing.Point(199, 152)
			Me.btnAuthenticate.Name = "btnAuthenticate"
			Me.btnAuthenticate.Size = New System.Drawing.Size(147, 36)
			Me.btnAuthenticate.TabIndex = 47
			Me.btnAuthenticate.Text = "Authenticate"
			Me.btnAuthenticate.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnAuthenticate.Click += new System.EventHandler(this.btnAuthenticate_Click);
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label3.ForeColor = System.Drawing.Color.Black
			Me.label3.Location = New System.Drawing.Point(2, 112)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(101, 12)
			Me.label3.TabIndex = 45
			Me.label3.Text = "Enciphered data:"
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Font = New System.Drawing.Font("宋体", 9F)
			Me.label5.ForeColor = System.Drawing.Color.Black
			Me.label5.Location = New System.Drawing.Point(3, 77)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(53, 12)
			Me.label5.TabIndex = 50
			Me.label5.Text = "Message:"
			' 
			' txtAuthenticateKeyID
			' 
			Me.txtAuthenticateKeyID.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtAuthenticateKeyID.Location = New System.Drawing.Point(102, 41)
			Me.txtAuthenticateKeyID.Name = "txtAuthenticateKeyID"
			Me.txtAuthenticateKeyID.ReadOnly = True
			Me.txtAuthenticateKeyID.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtAuthenticateKeyID.Size = New System.Drawing.Size(392, 21)
			Me.txtAuthenticateKeyID.TabIndex = 49
			Me.txtAuthenticateKeyID.Text = "0"
			' 
			' txtDecodeCiphertext
			' 
			Me.txtDecodeCiphertext.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtDecodeCiphertext.Location = New System.Drawing.Point(68, 61)
			Me.txtDecodeCiphertext.Name = "txtDecodeCiphertext"
			Me.txtDecodeCiphertext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtDecodeCiphertext.Size = New System.Drawing.Size(302, 21)
			Me.txtDecodeCiphertext.TabIndex = 44
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtDecodeCiphertext.TextChanged += new System.EventHandler(this.txtData2_TextChanged);
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Font = New System.Drawing.Font("宋体", 9F)
			Me.label2.ForeColor = System.Drawing.Color.Black
			Me.label2.Location = New System.Drawing.Point(6, 70)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(41, 12)
			Me.label2.TabIndex = 43
			Me.label2.Text = "Data："
			' 
			' groupBox5
			' 
			Me.groupBox5.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox5.Controls.Add(Me.lblencryptoKeyLen)
			Me.groupBox5.Controls.Add(Me.txtC)
			Me.groupBox5.Controls.Add(Me.label9)
			Me.groupBox5.Controls.Add(Me.txtRnd)
			Me.groupBox5.Controls.Add(Me.label8)
			Me.groupBox5.Controls.Add(Me.txtMsg)
			Me.groupBox5.Controls.Add(Me.label7)
			Me.groupBox5.Controls.Add(Me.btnDecode)
			Me.groupBox5.Controls.Add(Me.label6)
			Me.groupBox5.Controls.Add(Me.label4)
			Me.groupBox5.Controls.Add(Me.decodeKey)
			Me.groupBox5.Controls.Add(Me.txtDecodeCiphertext)
			Me.groupBox5.Controls.Add(Me.label2)
			Me.groupBox5.Controls.Add(Me.txtData2Len)
			Me.groupBox5.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox5.Location = New System.Drawing.Point(577, 12)
			Me.groupBox5.Name = "groupBox5"
			Me.groupBox5.Size = New System.Drawing.Size(405, 278)
			Me.groupBox5.TabIndex = 31
			Me.groupBox5.TabStop = False
			Me.groupBox5.Text = "AES Decode"
			' 
			' lblencryptoKeyLen
			' 
			Me.lblencryptoKeyLen.AutoSize = True
			Me.lblencryptoKeyLen.Font = New System.Drawing.Font("宋体", 9F)
			Me.lblencryptoKeyLen.ForeColor = System.Drawing.Color.Black
			Me.lblencryptoKeyLen.Location = New System.Drawing.Point(376, 34)
			Me.lblencryptoKeyLen.Name = "lblencryptoKeyLen"
			Me.lblencryptoKeyLen.Size = New System.Drawing.Size(0, 12)
			Me.lblencryptoKeyLen.TabIndex = 60
			' 
			' txtC
			' 
			Me.txtC.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtC.Location = New System.Drawing.Point(68, 93)
			Me.txtC.Name = "txtC"
			Me.txtC.ReadOnly = True
			Me.txtC.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtC.Size = New System.Drawing.Size(302, 21)
			Me.txtC.TabIndex = 59
			' 
			' label9
			' 
			Me.label9.AutoSize = True
			Me.label9.Font = New System.Drawing.Font("宋体", 9F)
			Me.label9.ForeColor = System.Drawing.Color.Black
			Me.label9.Location = New System.Drawing.Point(8, 98)
			Me.label9.Name = "label9"
			Me.label9.Size = New System.Drawing.Size(47, 12)
			Me.label9.TabIndex = 58
			Me.label9.Text = "C_TAM1:"
			' 
			' txtRnd
			' 
			Me.txtRnd.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtRnd.Location = New System.Drawing.Point(68, 128)
			Me.txtRnd.Name = "txtRnd"
			Me.txtRnd.ReadOnly = True
			Me.txtRnd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtRnd.Size = New System.Drawing.Size(302, 21)
			Me.txtRnd.TabIndex = 57
			' 
			' label8
			' 
			Me.label8.AutoSize = True
			Me.label8.Font = New System.Drawing.Font("宋体", 9F)
			Me.label8.ForeColor = System.Drawing.Color.Black
			Me.label8.Location = New System.Drawing.Point(6, 133)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(65, 12)
			Me.label8.TabIndex = 56
			Me.label8.Text = "TRnd_TAM1:"
			' 
			' txtMsg
			' 
			Me.txtMsg.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtMsg.Location = New System.Drawing.Point(68, 160)
			Me.txtMsg.Name = "txtMsg"
			Me.txtMsg.ReadOnly = True
			Me.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtMsg.Size = New System.Drawing.Size(302, 21)
			Me.txtMsg.TabIndex = 55
			' 
			' label7
			' 
			Me.label7.AutoSize = True
			Me.label7.Font = New System.Drawing.Font("宋体", 9F)
			Me.label7.ForeColor = System.Drawing.Color.Black
			Me.label7.Location = New System.Drawing.Point(8, 165)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(53, 12)
			Me.label7.TabIndex = 54
			Me.label7.Text = "Message:"
			' 
			' btnDecode
			' 
			Me.btnDecode.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnDecode.Location = New System.Drawing.Point(134, 209)
			Me.btnDecode.Name = "btnDecode"
			Me.btnDecode.Size = New System.Drawing.Size(133, 36)
			Me.btnDecode.TabIndex = 52
			Me.btnDecode.Text = "decode"
			Me.btnDecode.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
			' 
			' label6
			' 
			Me.label6.AutoSize = True
			Me.label6.Font = New System.Drawing.Font("宋体", 9F)
			Me.label6.ForeColor = System.Drawing.Color.Black
			Me.label6.Location = New System.Drawing.Point(18, 133)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(35, 12)
			Me.label6.TabIndex = 52
			Me.label6.Text = "TRnd:"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Font = New System.Drawing.Font("宋体", 9F)
			Me.label4.ForeColor = System.Drawing.Color.Black
			Me.label4.Location = New System.Drawing.Point(5, 34)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(35, 12)
			Me.label4.TabIndex = 50
			Me.label4.Text = "Key："
			' 
			' decodeKey
			' 
			Me.decodeKey.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.decodeKey.Location = New System.Drawing.Point(68, 31)
			Me.decodeKey.MaxLength = 48
			Me.decodeKey.Name = "decodeKey"
			Me.decodeKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.decodeKey.Size = New System.Drawing.Size(302, 21)
			Me.decodeKey.TabIndex = 51
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.decodeKey.TextChanged += new System.EventHandler(this.decodeKey_TextChanged);
			' 
			' txtData2Len
			' 
			Me.txtData2Len.AutoSize = True
			Me.txtData2Len.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtData2Len.Location = New System.Drawing.Point(369, 64)
			Me.txtData2Len.Name = "txtData2Len"
			Me.txtData2Len.Size = New System.Drawing.Size(0, 12)
			Me.txtData2Len.TabIndex = 61
			' 
			' AuthenticateForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.ClientSize = New System.Drawing.Size(996, 520)
			Me.Controls.Add(Me.groupBox5)
			Me.Controls.Add(Me.groupBox1)
			Me.KeyPreview = True
			Me.Name = "AuthenticateForm"
			Me.Text = "AuthenticateForm"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AuthenticateForm_KeyDown);
			Me.groupBox4.ResumeLayout(False)
			Me.groupBox4.PerformLayout()
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox3.PerformLayout()
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.groupBox6.ResumeLayout(False)
			Me.groupBox6.PerformLayout()
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.groupBox5.ResumeLayout(False)
			Me.groupBox5.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private groupBox4 As System.Windows.Forms.GroupBox
		Private txtLen As System.Windows.Forms.TextBox
		Private label24 As System.Windows.Forms.Label
		Private label23 As System.Windows.Forms.Label
		Private txtPtr As System.Windows.Forms.TextBox
		Private groupBox3 As System.Windows.Forms.GroupBox
		Private WithEvents rbUser As System.Windows.Forms.RadioButton
		Private WithEvents rbEPC As System.Windows.Forms.RadioButton
		Private WithEvents rbTID As System.Windows.Forms.RadioButton
		Private label22 As System.Windows.Forms.Label
		Private label30 As System.Windows.Forms.Label
		Private WithEvents txtFilter_EPC As System.Windows.Forms.TextBox
		Private label12 As System.Windows.Forms.Label
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private WithEvents btnAuthenticate As System.Windows.Forms.Button
		Private WithEvents txtAuthenticateEncryptionData As System.Windows.Forms.TextBox
		Private label3 As System.Windows.Forms.Label
		Private WithEvents txtDecodeCiphertext As System.Windows.Forms.TextBox
		Private label2 As System.Windows.Forms.Label
		Private groupBox5 As System.Windows.Forms.GroupBox
		Private WithEvents txtAuthenticateData As System.Windows.Forms.TextBox
		Private label5 As System.Windows.Forms.Label
		Private label1 As System.Windows.Forms.Label
		Private txtAuthenticateKeyID As System.Windows.Forms.TextBox
		Private label6 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private WithEvents decodeKey As System.Windows.Forms.TextBox
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private WithEvents btnDecode As System.Windows.Forms.Button
		Private groupBox6 As System.Windows.Forms.GroupBox
		Private WithEvents btnActivate As System.Windows.Forms.Button
		Private WithEvents btnRead As System.Windows.Forms.Button
		Private WithEvents txtKey0Data As System.Windows.Forms.TextBox
		Private lblKey0 As System.Windows.Forms.Label
		Private txtMsg As System.Windows.Forms.TextBox
		Private label7 As System.Windows.Forms.Label
		Private txtC As System.Windows.Forms.TextBox
		Private label9 As System.Windows.Forms.Label
		Private txtRnd As System.Windows.Forms.TextBox
		Private label8 As System.Windows.Forms.Label
		Private lblencryptoKeyLen As System.Windows.Forms.Label
		Private txtData2Len As System.Windows.Forms.Label
		Private txtFilter_EPCLen As System.Windows.Forms.Label
		Private txtEncryptionData2Len As System.Windows.Forms.Label
		Private txtDataLen As System.Windows.Forms.Label
		Private txtKey0DataLen As System.Windows.Forms.Label
		Private WithEvents btnWrite As System.Windows.Forms.Button

	End Class
End Namespace
