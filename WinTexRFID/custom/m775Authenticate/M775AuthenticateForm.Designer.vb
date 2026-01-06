Namespace UHFAPP.custom.m775Authenticate
	Partial Public Class M775AuthenticateForm
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
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.txtResponse = New System.Windows.Forms.TextBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.txtDataLen = New System.Windows.Forms.Label()
			Me.txtTid = New System.Windows.Forms.TextBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.txtAuthenticateData = New System.Windows.Forms.TextBox()
			Me.btnAuthenticate = New System.Windows.Forms.Button()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label5 = New System.Windows.Forms.Label()
			Me.txtChallenge = New System.Windows.Forms.TextBox()
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
			Me.groupBox2.SuspendLayout()
			Me.groupBox4.SuspendLayout()
			Me.groupBox3.SuspendLayout()
			Me.SuspendLayout()
			' 
			' groupBox2
			' 
			Me.groupBox2.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.groupBox2.Controls.Add(Me.txtResponse)
			Me.groupBox2.Controls.Add(Me.label2)
			Me.groupBox2.Controls.Add(Me.txtDataLen)
			Me.groupBox2.Controls.Add(Me.txtTid)
			Me.groupBox2.Controls.Add(Me.label1)
			Me.groupBox2.Controls.Add(Me.txtAuthenticateData)
			Me.groupBox2.Controls.Add(Me.btnAuthenticate)
			Me.groupBox2.Controls.Add(Me.label3)
			Me.groupBox2.Controls.Add(Me.label5)
			Me.groupBox2.Controls.Add(Me.txtChallenge)
			Me.groupBox2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.groupBox2.Location = New System.Drawing.Point(12, 175)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(531, 239)
			Me.groupBox2.TabIndex = 54
			Me.groupBox2.TabStop = False
			Me.groupBox2.Text = "M775 Authenticate"
			' 
			' txtResponse
			' 
			Me.txtResponse.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtResponse.Location = New System.Drawing.Point(137, 129)
			Me.txtResponse.Name = "txtResponse"
			Me.txtResponse.ReadOnly = True
			Me.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtResponse.Size = New System.Drawing.Size(367, 21)
			Me.txtResponse.TabIndex = 54
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label2.ForeColor = System.Drawing.Color.Black
			Me.label2.Location = New System.Drawing.Point(12, 131)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(83, 12)
			Me.label2.TabIndex = 53
			Me.label2.Text = "Tag Response:"
			' 
			' txtDataLen
			' 
			Me.txtDataLen.AutoSize = True
			Me.txtDataLen.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtDataLen.Location = New System.Drawing.Point(510, 45)
			Me.txtDataLen.Name = "txtDataLen"
			Me.txtDataLen.Size = New System.Drawing.Size(11, 12)
			Me.txtDataLen.TabIndex = 52
			Me.txtDataLen.Text = "0"
			Me.txtDataLen.Visible = False
			' 
			' txtTid
			' 
			Me.txtTid.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtTid.Location = New System.Drawing.Point(137, 98)
			Me.txtTid.Name = "txtTid"
			Me.txtTid.ReadOnly = True
			Me.txtTid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtTid.Size = New System.Drawing.Size(367, 21)
			Me.txtTid.TabIndex = 46
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("宋体", 9F)
			Me.label1.ForeColor = System.Drawing.Color.Black
			Me.label1.Location = New System.Drawing.Point(12, 69)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(65, 12)
			Me.label1.TabIndex = 48
			Me.label1.Text = "Challenge:"
			' 
			' txtAuthenticateData
			' 
			Me.txtAuthenticateData.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtAuthenticateData.Location = New System.Drawing.Point(139, 39)
			Me.txtAuthenticateData.MaxLength = 30
			Me.txtAuthenticateData.Name = "txtAuthenticateData"
			Me.txtAuthenticateData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtAuthenticateData.Size = New System.Drawing.Size(367, 21)
			Me.txtAuthenticateData.TabIndex = 51
			Me.txtAuthenticateData.Visible = False
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtAuthenticateData.TextChanged += new System.EventHandler(this.txtAuthenticateData_TextChanged);
			' 
			' btnAuthenticate
			' 
			Me.btnAuthenticate.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnAuthenticate.Location = New System.Drawing.Point(157, 168)
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
			Me.label3.Location = New System.Drawing.Point(12, 100)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(119, 12)
			Me.label3.TabIndex = 45
			Me.label3.Text = "Tags Shortened TID:"
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Font = New System.Drawing.Font("宋体", 9F)
			Me.label5.ForeColor = System.Drawing.Color.Black
			Me.label5.Location = New System.Drawing.Point(12, 42)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(53, 12)
			Me.label5.TabIndex = 50
			Me.label5.Text = "Message:"
			Me.label5.Visible = False
			' 
			' txtChallenge
			' 
			Me.txtChallenge.Font = New System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtChallenge.Location = New System.Drawing.Point(137, 67)
			Me.txtChallenge.Name = "txtChallenge"
			Me.txtChallenge.ReadOnly = True
			Me.txtChallenge.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.txtChallenge.Size = New System.Drawing.Size(367, 21)
			Me.txtChallenge.TabIndex = 49
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
			Me.groupBox4.Location = New System.Drawing.Point(12, 12)
			Me.groupBox4.Name = "groupBox4"
			Me.groupBox4.Size = New System.Drawing.Size(531, 147)
			Me.groupBox4.TabIndex = 53
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
			' M775AuthenticateForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.ClientSize = New System.Drawing.Size(559, 414)
			Me.Controls.Add(Me.groupBox2)
			Me.Controls.Add(Me.groupBox4)
			Me.KeyPreview = True
			Me.Name = "M775AuthenticateForm"
			Me.Text = "M775AuthenticateForm"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.M775AuthenticateForm_KeyDown);
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.groupBox4.ResumeLayout(False)
			Me.groupBox4.PerformLayout()
			Me.groupBox3.ResumeLayout(False)
			Me.groupBox3.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private groupBox2 As System.Windows.Forms.GroupBox
		Private txtDataLen As System.Windows.Forms.Label
		Private txtTid As System.Windows.Forms.TextBox
		Private label1 As System.Windows.Forms.Label
		Private WithEvents txtAuthenticateData As System.Windows.Forms.TextBox
		Private WithEvents btnAuthenticate As System.Windows.Forms.Button
		Private label3 As System.Windows.Forms.Label
		Private label5 As System.Windows.Forms.Label
		Private txtChallenge As System.Windows.Forms.TextBox
		Private groupBox4 As System.Windows.Forms.GroupBox
		Private txtFilter_EPCLen As System.Windows.Forms.Label
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
		Private txtResponse As System.Windows.Forms.TextBox
		Private label2 As System.Windows.Forms.Label
	End Class
End Namespace
