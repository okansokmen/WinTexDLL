Namespace UHFAPP.RFID
	Partial Public Class RFIDMainForm
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
			Me.cmbM1KeyType = New System.Windows.Forms.ComboBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.cmbM1TagType = New System.Windows.Forms.ComboBox()
			Me.lblM1TagType = New System.Windows.Forms.Label()
			Me.txtM1Data = New System.Windows.Forms.TextBox()
			Me.cmbM1Block = New System.Windows.Forms.ComboBox()
			Me.lblM1Block = New System.Windows.Forms.Label()
			Me.cmbM1Sector = New System.Windows.Forms.ComboBox()
			Me.lblM1Sector = New System.Windows.Forms.Label()
			Me.lblM1Key = New System.Windows.Forms.Label()
			Me.txtM1Key = New System.Windows.Forms.TextBox()
			Me.btnWrite = New System.Windows.Forms.Button()
			Me.btnRead = New System.Windows.Forms.Button()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.btnDsfidLock = New System.Windows.Forms.Button()
			Me.btnAFILock = New System.Windows.Forms.Button()
			Me.txtDsfid = New System.Windows.Forms.TextBox()
			Me.btn15693Write = New System.Windows.Forms.Button()
			Me.btn15693Read = New System.Windows.Forms.Button()
			Me.btnDsfidWrite = New System.Windows.Forms.Button()
			Me.btnAFIWrite = New System.Windows.Forms.Button()
			Me.label3 = New System.Windows.Forms.Label()
			Me.txtAFI = New System.Windows.Forms.TextBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.txt15693Data = New System.Windows.Forms.TextBox()
			Me.cmb15693Block = New System.Windows.Forms.ComboBox()
			Me.label5 = New System.Windows.Forms.Label()
			Me.label6 = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.lbl15693Block = New System.Windows.Forms.Label()
			Me.groupBox3 = New System.Windows.Forms.GroupBox()
			Me.txtPsamReceive = New System.Windows.Forms.TextBox()
			Me.label10 = New System.Windows.Forms.Label()
			Me.btnPsamSend = New System.Windows.Forms.Button()
			Me.btnFree = New System.Windows.Forms.Button()
			Me.btnInit = New System.Windows.Forms.Button()
			Me.txtPsamData = New System.Windows.Forms.TextBox()
			Me.label9 = New System.Windows.Forms.Label()
			Me.rbCard2 = New System.Windows.Forms.RadioButton()
			Me.rbCard1 = New System.Windows.Forms.RadioButton()
			Me.label8 = New System.Windows.Forms.Label()
			Me.groupBox4 = New System.Windows.Forms.GroupBox()
			Me.txt14443BSendData = New System.Windows.Forms.TextBox()
			Me.label13 = New System.Windows.Forms.Label()
			Me.label11 = New System.Windows.Forms.Label()
			Me.btn14443B = New System.Windows.Forms.Button()
			Me.btnGetUID = New System.Windows.Forms.Button()
			Me.txt14443BReceive = New System.Windows.Forms.TextBox()
			Me.groupBox5 = New System.Windows.Forms.GroupBox()
			Me.txtCPUReceive = New System.Windows.Forms.TextBox()
			Me.lblMSG = New System.Windows.Forms.Label()
			Me.btn14443ACPUInit = New System.Windows.Forms.Button()
			Me.label7 = New System.Windows.Forms.Label()
			Me.txt14443ACPUData = New System.Windows.Forms.TextBox()
			Me.groupBox6 = New System.Windows.Forms.GroupBox()
			Me.txtUID = New System.Windows.Forms.TextBox()
			Me.groupBox1.SuspendLayout()
			Me.groupBox2.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			Me.groupBox4.SuspendLayout()
			Me.groupBox5.SuspendLayout()
			Me.groupBox6.SuspendLayout()
			Me.SuspendLayout()
			' 
			' groupBox1
			' 
			Me.groupBox1.Controls.Add(Me.cmbM1KeyType)
			Me.groupBox1.Controls.Add(Me.label1)
			Me.groupBox1.Controls.Add(Me.cmbM1TagType)
			Me.groupBox1.Controls.Add(Me.lblM1TagType)
			Me.groupBox1.Controls.Add(Me.txtM1Data)
			Me.groupBox1.Controls.Add(Me.cmbM1Block)
			Me.groupBox1.Controls.Add(Me.lblM1Block)
			Me.groupBox1.Controls.Add(Me.cmbM1Sector)
			Me.groupBox1.Controls.Add(Me.lblM1Sector)
			Me.groupBox1.Controls.Add(Me.lblM1Key)
			Me.groupBox1.Controls.Add(Me.txtM1Key)
			Me.groupBox1.Controls.Add(Me.btnWrite)
			Me.groupBox1.Controls.Add(Me.btnRead)
			Me.groupBox1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox1.Location = New System.Drawing.Point(6, 12)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(350, 285)
			Me.groupBox1.TabIndex = 0
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "14443A"
			' 
			' cmbM1KeyType
			' 
			Me.cmbM1KeyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cmbM1KeyType.FormattingEnabled = True
			Me.cmbM1KeyType.Items.AddRange(New Object() { "A", "B"})
			Me.cmbM1KeyType.Location = New System.Drawing.Point(245, 31)
			Me.cmbM1KeyType.Name = "cmbM1KeyType"
			Me.cmbM1KeyType.Size = New System.Drawing.Size(64, 24)
			Me.cmbM1KeyType.TabIndex = 12
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(160, 34)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(80, 16)
			Me.label1.TabIndex = 11
			Me.label1.Text = "Key Type:"
			' 
			' cmbM1TagType
			' 
			Me.cmbM1TagType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cmbM1TagType.FormattingEnabled = True
			Me.cmbM1TagType.Items.AddRange(New Object() { "S50", "S70"})
			Me.cmbM1TagType.Location = New System.Drawing.Point(88, 31)
			Me.cmbM1TagType.Name = "cmbM1TagType"
			Me.cmbM1TagType.Size = New System.Drawing.Size(64, 24)
			Me.cmbM1TagType.TabIndex = 10
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cmbM1TagType.SelectedIndexChanged += new System.EventHandler(this.cmbM1TagType_SelectedIndexChanged);
			' 
			' lblM1TagType
			' 
			Me.lblM1TagType.AutoSize = True
			Me.lblM1TagType.Location = New System.Drawing.Point(6, 34)
			Me.lblM1TagType.Name = "lblM1TagType"
			Me.lblM1TagType.Size = New System.Drawing.Size(80, 16)
			Me.lblM1TagType.TabIndex = 9
			Me.lblM1TagType.Text = "Tag Type:"
			' 
			' txtM1Data
			' 
			Me.txtM1Data.Location = New System.Drawing.Point(9, 129)
			Me.txtM1Data.Multiline = True
			Me.txtM1Data.Name = "txtM1Data"
			Me.txtM1Data.Size = New System.Drawing.Size(317, 92)
			Me.txtM1Data.TabIndex = 8
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtM1Data.TextChanged += new System.EventHandler(this.txtM1Data_TextChanged);
			' 
			' cmbM1Block
			' 
			Me.cmbM1Block.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cmbM1Block.FormattingEnabled = True
			Me.cmbM1Block.Items.AddRange(New Object() { "0", "1", "2", "3"})
			Me.cmbM1Block.Location = New System.Drawing.Point(245, 60)
			Me.cmbM1Block.Name = "cmbM1Block"
			Me.cmbM1Block.Size = New System.Drawing.Size(64, 24)
			Me.cmbM1Block.TabIndex = 7
			' 
			' lblM1Block
			' 
			Me.lblM1Block.AutoSize = True
			Me.lblM1Block.Location = New System.Drawing.Point(178, 63)
			Me.lblM1Block.Name = "lblM1Block"
			Me.lblM1Block.Size = New System.Drawing.Size(56, 16)
			Me.lblM1Block.TabIndex = 6
			Me.lblM1Block.Text = "Block:"
			' 
			' cmbM1Sector
			' 
			Me.cmbM1Sector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cmbM1Sector.FormattingEnabled = True
			Me.cmbM1Sector.Items.AddRange(New Object() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"})
			Me.cmbM1Sector.Location = New System.Drawing.Point(88, 60)
			Me.cmbM1Sector.Name = "cmbM1Sector"
			Me.cmbM1Sector.Size = New System.Drawing.Size(64, 24)
			Me.cmbM1Sector.TabIndex = 5
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cmbM1Sector.SelectedIndexChanged += new System.EventHandler(this.cmbM1Sector_SelectedIndexChanged);
			' 
			' lblM1Sector
			' 
			Me.lblM1Sector.AutoSize = True
			Me.lblM1Sector.Location = New System.Drawing.Point(6, 63)
			Me.lblM1Sector.Name = "lblM1Sector"
			Me.lblM1Sector.Size = New System.Drawing.Size(64, 16)
			Me.lblM1Sector.TabIndex = 4
			Me.lblM1Sector.Text = "Sector:"
			' 
			' lblM1Key
			' 
			Me.lblM1Key.AutoSize = True
			Me.lblM1Key.Location = New System.Drawing.Point(6, 99)
			Me.lblM1Key.Name = "lblM1Key"
			Me.lblM1Key.Size = New System.Drawing.Size(40, 16)
			Me.lblM1Key.TabIndex = 3
			Me.lblM1Key.Text = "Key:"
			' 
			' txtM1Key
			' 
			Me.txtM1Key.Location = New System.Drawing.Point(88, 96)
			Me.txtM1Key.Name = "txtM1Key"
			Me.txtM1Key.Size = New System.Drawing.Size(221, 26)
			Me.txtM1Key.TabIndex = 2
			Me.txtM1Key.Text = "FFFFFFFFFFFF"
			' 
			' btnWrite
			' 
			Me.btnWrite.Location = New System.Drawing.Point(191, 227)
			Me.btnWrite.Name = "btnWrite"
			Me.btnWrite.Size = New System.Drawing.Size(83, 32)
			Me.btnWrite.TabIndex = 1
			Me.btnWrite.Text = "Write"
			Me.btnWrite.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
			' 
			' btnRead
			' 
			Me.btnRead.Location = New System.Drawing.Point(31, 227)
			Me.btnRead.Name = "btnRead"
			Me.btnRead.Size = New System.Drawing.Size(83, 32)
			Me.btnRead.TabIndex = 0
			Me.btnRead.Text = "Read"
			Me.btnRead.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
			' 
			' groupBox2
			' 
			Me.groupBox2.Controls.Add(Me.btnDsfidLock)
			Me.groupBox2.Controls.Add(Me.btnAFILock)
			Me.groupBox2.Controls.Add(Me.txtDsfid)
			Me.groupBox2.Controls.Add(Me.btn15693Write)
			Me.groupBox2.Controls.Add(Me.btn15693Read)
			Me.groupBox2.Controls.Add(Me.btnDsfidWrite)
			Me.groupBox2.Controls.Add(Me.btnAFIWrite)
			Me.groupBox2.Controls.Add(Me.label3)
			Me.groupBox2.Controls.Add(Me.txtAFI)
			Me.groupBox2.Controls.Add(Me.label2)
			Me.groupBox2.Controls.Add(Me.txt15693Data)
			Me.groupBox2.Controls.Add(Me.cmb15693Block)
			Me.groupBox2.Controls.Add(Me.label5)
			Me.groupBox2.Controls.Add(Me.label6)
			Me.groupBox2.Controls.Add(Me.label4)
			Me.groupBox2.Controls.Add(Me.lbl15693Block)
			Me.groupBox2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox2.Location = New System.Drawing.Point(393, 14)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(403, 283)
			Me.groupBox2.TabIndex = 1
			Me.groupBox2.TabStop = False
			Me.groupBox2.Text = "15693"
			' 
			' btnDsfidLock
			' 
			Me.btnDsfidLock.Location = New System.Drawing.Point(268, 197)
			Me.btnDsfidLock.Name = "btnDsfidLock"
			Me.btnDsfidLock.Size = New System.Drawing.Size(67, 28)
			Me.btnDsfidLock.TabIndex = 19
			Me.btnDsfidLock.Text = "Lock"
			Me.btnDsfidLock.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnDsfidLock.Click += new System.EventHandler(this.btnDsfidLock_Click);
			' 
			' btnAFILock
			' 
			Me.btnAFILock.Location = New System.Drawing.Point(268, 156)
			Me.btnAFILock.Name = "btnAFILock"
			Me.btnAFILock.Size = New System.Drawing.Size(67, 28)
			Me.btnAFILock.TabIndex = 18
			Me.btnAFILock.Text = "Lock"
			Me.btnAFILock.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnAFILock.Click += new System.EventHandler(this.btnAFILock_Click);
			' 
			' txtDsfid
			' 
			Me.txtDsfid.Location = New System.Drawing.Point(66, 196)
			Me.txtDsfid.Name = "txtDsfid"
			Me.txtDsfid.Size = New System.Drawing.Size(53, 26)
			Me.txtDsfid.TabIndex = 17
			' 
			' btn15693Write
			' 
			Me.btn15693Write.Location = New System.Drawing.Point(165, 104)
			Me.btn15693Write.Name = "btn15693Write"
			Me.btn15693Write.Size = New System.Drawing.Size(67, 32)
			Me.btn15693Write.TabIndex = 11
			Me.btn15693Write.Text = "Write"
			Me.btn15693Write.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btn15693Write.Click += new System.EventHandler(this.btn15693Write_Click);
			' 
			' btn15693Read
			' 
			Me.btn15693Read.Location = New System.Drawing.Point(81, 104)
			Me.btn15693Read.Name = "btn15693Read"
			Me.btn15693Read.Size = New System.Drawing.Size(67, 33)
			Me.btn15693Read.TabIndex = 10
			Me.btn15693Read.Text = "Read"
			Me.btn15693Read.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btn15693Read.Click += new System.EventHandler(this.btn15693Read_Click);
			' 
			' btnDsfidWrite
			' 
			Me.btnDsfidWrite.Location = New System.Drawing.Point(195, 196)
			Me.btnDsfidWrite.Name = "btnDsfidWrite"
			Me.btnDsfidWrite.Size = New System.Drawing.Size(67, 29)
			Me.btnDsfidWrite.TabIndex = 16
			Me.btnDsfidWrite.Text = "Write"
			Me.btnDsfidWrite.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnDsfidWrite.Click += new System.EventHandler(this.btnDsfidWrite_Click);
			' 
			' btnAFIWrite
			' 
			Me.btnAFIWrite.Location = New System.Drawing.Point(195, 156)
			Me.btnAFIWrite.Name = "btnAFIWrite"
			Me.btnAFIWrite.Size = New System.Drawing.Size(67, 28)
			Me.btnAFIWrite.TabIndex = 15
			Me.btnAFIWrite.Text = "Write"
			Me.btnAFIWrite.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnAFIWrite.Click += new System.EventHandler(this.btnAFIWrite_Click);
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(7, 202)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(56, 16)
			Me.label3.TabIndex = 14
			Me.label3.Text = "Dsfid:"
			' 
			' txtAFI
			' 
			Me.txtAFI.Location = New System.Drawing.Point(66, 160)
			Me.txtAFI.Name = "txtAFI"
			Me.txtAFI.Size = New System.Drawing.Size(53, 26)
			Me.txtAFI.TabIndex = 13
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(9, 163)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(40, 16)
			Me.label2.TabIndex = 13
			Me.label2.Text = "AFI:"
			' 
			' txt15693Data
			' 
			Me.txt15693Data.Location = New System.Drawing.Point(66, 53)
			Me.txt15693Data.Multiline = True
			Me.txt15693Data.Name = "txt15693Data"
			Me.txt15693Data.Size = New System.Drawing.Size(269, 45)
			Me.txt15693Data.TabIndex = 12
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txt15693Data.TextChanged += new System.EventHandler(this.txt15693Data_TextChanged);
			' 
			' cmb15693Block
			' 
			Me.cmb15693Block.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cmb15693Block.FormattingEnabled = True
			Me.cmb15693Block.Items.AddRange(New Object() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27"})
			Me.cmb15693Block.Location = New System.Drawing.Point(66, 26)
			Me.cmb15693Block.Name = "cmb15693Block"
			Me.cmb15693Block.Size = New System.Drawing.Size(64, 24)
			Me.cmb15693Block.TabIndex = 9
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label5.Location = New System.Drawing.Point(115, 166)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(71, 12)
			Me.label5.TabIndex = 21
			Me.label5.Text = "(1byte hex)"
			' 
			' label6
			' 
			Me.label6.AutoSize = True
			Me.label6.Font = New System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label6.Location = New System.Drawing.Point(114, 202)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(84, 14)
			Me.label6.TabIndex = 22
			Me.label6.Text = "(1byte hex)"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(20, 65)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(48, 16)
			Me.label4.TabIndex = 20
			Me.label4.Text = "Data:"
			' 
			' lbl15693Block
			' 
			Me.lbl15693Block.AutoSize = True
			Me.lbl15693Block.Location = New System.Drawing.Point(13, 29)
			Me.lbl15693Block.Name = "lbl15693Block"
			Me.lbl15693Block.Size = New System.Drawing.Size(56, 16)
			Me.lbl15693Block.TabIndex = 8
			Me.lbl15693Block.Text = "Block:"
			' 
			' groupBox3
			' 
			Me.groupBox3.Controls.Add(Me.txtPsamReceive)
			Me.groupBox3.Controls.Add(Me.label10)
			Me.groupBox3.Controls.Add(Me.btnPsamSend)
			Me.groupBox3.Controls.Add(Me.btnFree)
			Me.groupBox3.Controls.Add(Me.btnInit)
			Me.groupBox3.Controls.Add(Me.txtPsamData)
			Me.groupBox3.Controls.Add(Me.label9)
			Me.groupBox3.Controls.Add(Me.rbCard2)
			Me.groupBox3.Controls.Add(Me.rbCard1)
			Me.groupBox3.Controls.Add(Me.label8)
			Me.groupBox3.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox3.Location = New System.Drawing.Point(828, 14)
			Me.groupBox3.Name = "groupBox3"
			Me.groupBox3.Size = New System.Drawing.Size(388, 296)
			Me.groupBox3.TabIndex = 2
			Me.groupBox3.TabStop = False
			Me.groupBox3.Text = "PSAM"
			' 
			' txtPsamReceive
			' 
			Me.txtPsamReceive.Location = New System.Drawing.Point(60, 217)
			Me.txtPsamReceive.Multiline = True
			Me.txtPsamReceive.Name = "txtPsamReceive"
			Me.txtPsamReceive.ReadOnly = True
			Me.txtPsamReceive.Size = New System.Drawing.Size(312, 66)
			Me.txtPsamReceive.TabIndex = 32
			' 
			' label10
			' 
			Me.label10.AutoSize = True
			Me.label10.Location = New System.Drawing.Point(21, 191)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(72, 16)
			Me.label10.TabIndex = 31
			Me.label10.Text = "Receive:"
			' 
			' btnPsamSend
			' 
			Me.btnPsamSend.Enabled = False
			Me.btnPsamSend.Location = New System.Drawing.Point(318, 73)
			Me.btnPsamSend.Name = "btnPsamSend"
			Me.btnPsamSend.Size = New System.Drawing.Size(54, 42)
			Me.btnPsamSend.TabIndex = 30
			Me.btnPsamSend.Text = "Send"
			Me.btnPsamSend.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnPsamSend.Click += new System.EventHandler(this.button3_Click);
			' 
			' btnFree
			' 
			Me.btnFree.Enabled = False
			Me.btnFree.Location = New System.Drawing.Point(183, 139)
			Me.btnFree.Name = "btnFree"
			Me.btnFree.Size = New System.Drawing.Size(97, 35)
			Me.btnFree.TabIndex = 29
			Me.btnFree.Text = "free"
			Me.btnFree.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnFree.Click += new System.EventHandler(this.btnFree_Click);
			' 
			' btnInit
			' 
			Me.btnInit.Location = New System.Drawing.Point(60, 142)
			Me.btnInit.Name = "btnInit"
			Me.btnInit.Size = New System.Drawing.Size(96, 34)
			Me.btnInit.TabIndex = 28
			Me.btnInit.Text = "init"
			Me.btnInit.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
			' 
			' txtPsamData
			' 
			Me.txtPsamData.Location = New System.Drawing.Point(60, 70)
			Me.txtPsamData.Multiline = True
			Me.txtPsamData.Name = "txtPsamData"
			Me.txtPsamData.Size = New System.Drawing.Size(252, 45)
			Me.txtPsamData.TabIndex = 26
			Me.txtPsamData.Text = "0084000004"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtPsamData.TextChanged += new System.EventHandler(this.txtPsamData_TextChanged);
			' 
			' label9
			' 
			Me.label9.AutoSize = True
			Me.label9.Location = New System.Drawing.Point(14, 82)
			Me.label9.Name = "label9"
			Me.label9.Size = New System.Drawing.Size(48, 16)
			Me.label9.TabIndex = 27
			Me.label9.Text = "Data:"
			' 
			' rbCard2
			' 
			Me.rbCard2.AutoSize = True
			Me.rbCard2.Location = New System.Drawing.Point(120, 34)
			Me.rbCard2.Name = "rbCard2"
			Me.rbCard2.Size = New System.Drawing.Size(66, 20)
			Me.rbCard2.TabIndex = 25
			Me.rbCard2.Text = "Card2"
			Me.rbCard2.UseVisualStyleBackColor = True
			' 
			' rbCard1
			' 
			Me.rbCard1.AutoSize = True
			Me.rbCard1.Checked = True
			Me.rbCard1.Location = New System.Drawing.Point(24, 32)
			Me.rbCard1.Name = "rbCard1"
			Me.rbCard1.Size = New System.Drawing.Size(66, 20)
			Me.rbCard1.TabIndex = 24
			Me.rbCard1.TabStop = True
			Me.rbCard1.Text = "Card1"
			Me.rbCard1.UseVisualStyleBackColor = True
			' 
			' label8
			' 
			Me.label8.AutoSize = True
			Me.label8.Location = New System.Drawing.Point(18, 57)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(0, 16)
			Me.label8.TabIndex = 9
			' 
			' groupBox4
			' 
			Me.groupBox4.Controls.Add(Me.txt14443BReceive)
			Me.groupBox4.Controls.Add(Me.groupBox6)
			Me.groupBox4.Controls.Add(Me.txt14443BSendData)
			Me.groupBox4.Controls.Add(Me.label13)
			Me.groupBox4.Controls.Add(Me.label11)
			Me.groupBox4.Controls.Add(Me.btn14443B)
			Me.groupBox4.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox4.Location = New System.Drawing.Point(393, 307)
			Me.groupBox4.Name = "groupBox4"
			Me.groupBox4.Size = New System.Drawing.Size(403, 289)
			Me.groupBox4.TabIndex = 2
			Me.groupBox4.TabStop = False
			Me.groupBox4.Text = "14443B"
			' 
			' txt14443BSendData
			' 
			Me.txt14443BSendData.Location = New System.Drawing.Point(74, 34)
			Me.txt14443BSendData.Multiline = True
			Me.txt14443BSendData.Name = "txt14443BSendData"
			Me.txt14443BSendData.Size = New System.Drawing.Size(243, 45)
			Me.txt14443BSendData.TabIndex = 28
			Me.txt14443BSendData.Text = "0084000004"
			' 
			' label13
			' 
			Me.label13.AutoSize = True
			Me.label13.Location = New System.Drawing.Point(20, 46)
			Me.label13.Name = "label13"
			Me.label13.Size = New System.Drawing.Size(48, 16)
			Me.label13.TabIndex = 26
			Me.label13.Text = "Data:"
			' 
			' label11
			' 
			Me.label11.AutoSize = True
			Me.label11.Location = New System.Drawing.Point(7, 98)
			Me.label11.Name = "label11"
			Me.label11.Size = New System.Drawing.Size(72, 16)
			Me.label11.TabIndex = 26
			Me.label11.Text = "Receive:"
			' 
			' btn14443B
			' 
			Me.btn14443B.Location = New System.Drawing.Point(321, 38)
			Me.btn14443B.Name = "btn14443B"
			Me.btn14443B.Size = New System.Drawing.Size(74, 32)
			Me.btn14443B.TabIndex = 29
			Me.btn14443B.Text = "Send"
			Me.btn14443B.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btn14443B.Click += new System.EventHandler(this.btn14443B_Click);
			' 
			' btnGetUID
			' 
			Me.btnGetUID.Location = New System.Drawing.Point(287, 42)
			Me.btnGetUID.Name = "btnGetUID"
			Me.btnGetUID.Size = New System.Drawing.Size(78, 32)
			Me.btnGetUID.TabIndex = 27
			Me.btnGetUID.Text = "GetUID"
			Me.btnGetUID.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnGetUID.Click += new System.EventHandler(this.btnGetUID_Click);
			' 
			' txt14443BReceive
			' 
			Me.txt14443BReceive.Location = New System.Drawing.Point(77, 85)
			Me.txt14443BReceive.Multiline = True
			Me.txt14443BReceive.Name = "txt14443BReceive"
			Me.txt14443BReceive.ReadOnly = True
			Me.txt14443BReceive.Size = New System.Drawing.Size(240, 45)
			Me.txt14443BReceive.TabIndex = 26
			' 
			' groupBox5
			' 
			Me.groupBox5.Controls.Add(Me.txtCPUReceive)
			Me.groupBox5.Controls.Add(Me.lblMSG)
			Me.groupBox5.Controls.Add(Me.btn14443ACPUInit)
			Me.groupBox5.Controls.Add(Me.label7)
			Me.groupBox5.Controls.Add(Me.txt14443ACPUData)
			Me.groupBox5.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox5.Location = New System.Drawing.Point(6, 303)
			Me.groupBox5.Name = "groupBox5"
			Me.groupBox5.Size = New System.Drawing.Size(350, 293)
			Me.groupBox5.TabIndex = 3
			Me.groupBox5.TabStop = False
			Me.groupBox5.Text = "14443A-CPU"
			' 
			' txtCPUReceive
			' 
			Me.txtCPUReceive.Location = New System.Drawing.Point(57, 185)
			Me.txtCPUReceive.Multiline = True
			Me.txtCPUReceive.Name = "txtCPUReceive"
			Me.txtCPUReceive.ReadOnly = True
			Me.txtCPUReceive.Size = New System.Drawing.Size(269, 45)
			Me.txtCPUReceive.TabIndex = 25
			' 
			' lblMSG
			' 
			Me.lblMSG.AutoSize = True
			Me.lblMSG.Location = New System.Drawing.Point(18, 159)
			Me.lblMSG.Name = "lblMSG"
			Me.lblMSG.Size = New System.Drawing.Size(72, 16)
			Me.lblMSG.TabIndex = 24
			Me.lblMSG.Text = "Receive:"
			' 
			' btn14443ACPUInit
			' 
			Me.btn14443ACPUInit.Location = New System.Drawing.Point(102, 104)
			Me.btn14443ACPUInit.Name = "btn14443ACPUInit"
			Me.btn14443ACPUInit.Size = New System.Drawing.Size(83, 32)
			Me.btn14443ACPUInit.TabIndex = 23
			Me.btn14443ACPUInit.Text = "Send"
			Me.btn14443ACPUInit.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btn14443ACPUInit.Click += new System.EventHandler(this.btn14443ACPUInit_Click);
			' 
			' label7
			' 
			Me.label7.AutoSize = True
			Me.label7.Location = New System.Drawing.Point(11, 50)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(48, 16)
			Me.label7.TabIndex = 22
			Me.label7.Text = "Data:"
			' 
			' txt14443ACPUData
			' 
			Me.txt14443ACPUData.Location = New System.Drawing.Point(60, 38)
			Me.txt14443ACPUData.Multiline = True
			Me.txt14443ACPUData.Name = "txt14443ACPUData"
			Me.txt14443ACPUData.Size = New System.Drawing.Size(269, 45)
			Me.txt14443ACPUData.TabIndex = 21
			Me.txt14443ACPUData.Text = "0084000004"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txt14443ACPUData.TextChanged += new System.EventHandler(this.txt14443ACPUData_TextChanged);
			' 
			' groupBox6
			' 
			Me.groupBox6.Controls.Add(Me.txtUID)
			Me.groupBox6.Controls.Add(Me.btnGetUID)
			Me.groupBox6.Location = New System.Drawing.Point(10, 152)
			Me.groupBox6.Name = "groupBox6"
			Me.groupBox6.Size = New System.Drawing.Size(385, 118)
			Me.groupBox6.TabIndex = 30
			Me.groupBox6.TabStop = False
			Me.groupBox6.Text = "Identity Card"
			' 
			' txtUID
			' 
			Me.txtUID.Location = New System.Drawing.Point(41, 38)
			Me.txtUID.Multiline = True
			Me.txtUID.Name = "txtUID"
			Me.txtUID.ReadOnly = True
			Me.txtUID.Size = New System.Drawing.Size(240, 45)
			Me.txtUID.TabIndex = 31
			' 
			' RFIDMainForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.ClientSize = New System.Drawing.Size(1290, 625)
			Me.Controls.Add(Me.groupBox5)
			Me.Controls.Add(Me.groupBox4)
			Me.Controls.Add(Me.groupBox3)
			Me.Controls.Add(Me.groupBox2)
			Me.Controls.Add(Me.groupBox1)
			Me.Name = "RFIDMainForm"
			Me.Text = "RFIDMainForm"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RFIDMainForm_FormClosing);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RFIDMainForm_KeyDown);
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox3.PerformLayout()
			Me.groupBox4.ResumeLayout(False)
			Me.groupBox4.PerformLayout()
			Me.groupBox5.ResumeLayout(False)
			Me.groupBox5.PerformLayout()
			Me.groupBox6.ResumeLayout(False)
			Me.groupBox6.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private groupBox1 As System.Windows.Forms.GroupBox
		Private groupBox2 As System.Windows.Forms.GroupBox
		Private groupBox3 As System.Windows.Forms.GroupBox
		Private groupBox4 As System.Windows.Forms.GroupBox
		Private WithEvents btnWrite As System.Windows.Forms.Button
		Private WithEvents btnRead As System.Windows.Forms.Button
		Private txtM1Key As System.Windows.Forms.TextBox
		Private lblM1Key As System.Windows.Forms.Label
		Private WithEvents cmbM1Sector As System.Windows.Forms.ComboBox
		Private lblM1Sector As System.Windows.Forms.Label
		Private cmbM1Block As System.Windows.Forms.ComboBox
		Private lblM1Block As System.Windows.Forms.Label
		Private WithEvents txtM1Data As System.Windows.Forms.TextBox
		Private lblM1TagType As System.Windows.Forms.Label
		Private cmbM1KeyType As System.Windows.Forms.ComboBox
		Private label1 As System.Windows.Forms.Label
		Private WithEvents cmbM1TagType As System.Windows.Forms.ComboBox
		Private WithEvents btn15693Write As System.Windows.Forms.Button
		Private WithEvents btn15693Read As System.Windows.Forms.Button
		Private cmb15693Block As System.Windows.Forms.ComboBox
		Private lbl15693Block As System.Windows.Forms.Label
		Private WithEvents txt15693Data As System.Windows.Forms.TextBox
		Private txtAFI As System.Windows.Forms.TextBox
		Private label2 As System.Windows.Forms.Label
		Private WithEvents btnAFILock As System.Windows.Forms.Button
		Private txtDsfid As System.Windows.Forms.TextBox
		Private WithEvents btnDsfidWrite As System.Windows.Forms.Button
		Private WithEvents btnAFIWrite As System.Windows.Forms.Button
		Private label3 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private WithEvents btnDsfidLock As System.Windows.Forms.Button
		Private label5 As System.Windows.Forms.Label
		Private label6 As System.Windows.Forms.Label
		Private groupBox5 As System.Windows.Forms.GroupBox
		Private label7 As System.Windows.Forms.Label
		Private WithEvents txt14443ACPUData As System.Windows.Forms.TextBox
		Private WithEvents btn14443ACPUInit As System.Windows.Forms.Button
		Private lblMSG As System.Windows.Forms.Label
		Private txtCPUReceive As System.Windows.Forms.TextBox
		Private WithEvents btnGetUID As System.Windows.Forms.Button
		Private txt14443BReceive As System.Windows.Forms.TextBox
		Private rbCard2 As System.Windows.Forms.RadioButton
		Private rbCard1 As System.Windows.Forms.RadioButton
		Private label8 As System.Windows.Forms.Label
		Private WithEvents txtPsamData As System.Windows.Forms.TextBox
		Private label9 As System.Windows.Forms.Label
		Private WithEvents btnPsamSend As System.Windows.Forms.Button
		Private WithEvents btnFree As System.Windows.Forms.Button
		Private WithEvents btnInit As System.Windows.Forms.Button
		Private txtPsamReceive As System.Windows.Forms.TextBox
		Private label10 As System.Windows.Forms.Label
		Private WithEvents btn14443B As System.Windows.Forms.Button
		Private txt14443BSendData As System.Windows.Forms.TextBox
		Private label13 As System.Windows.Forms.Label
		Private label11 As System.Windows.Forms.Label
		Private groupBox6 As System.Windows.Forms.GroupBox
		Private txtUID As System.Windows.Forms.TextBox
	End Class
End Namespace
