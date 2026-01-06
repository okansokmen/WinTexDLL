Namespace UHFAPP
	Partial Public Class UHFUpgradeFormEx
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
			Me.textBox1 = New System.Windows.Forms.TextBox()
			Me.progressBar1 = New System.Windows.Forms.ProgressBar()
			Me.btnStart = New System.Windows.Forms.Button()
			Me.btnPath = New System.Windows.Forms.Button()
			Me.txtPath = New System.Windows.Forms.TextBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.rbUHFModule = New System.Windows.Forms.RadioButton()
			Me.rbReaderBootloader = New System.Windows.Forms.RadioButton()
			Me.rbEx10SDKFirmware = New System.Windows.Forms.RadioButton()
			Me.rbReaderApplication = New System.Windows.Forms.RadioButton()
			Me.SuspendLayout()
			' 
			' textBox1
			' 
			Me.textBox1.Location = New System.Drawing.Point(37, 170)
			Me.textBox1.Multiline = True
			Me.textBox1.Name = "textBox1"
			Me.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.textBox1.Size = New System.Drawing.Size(368, 254)
			Me.textBox1.TabIndex = 12
			' 
			' progressBar1
			' 
			Me.progressBar1.Location = New System.Drawing.Point(38, 146)
			Me.progressBar1.Name = "progressBar1"
			Me.progressBar1.Size = New System.Drawing.Size(367, 18)
			Me.progressBar1.TabIndex = 13
			' 
			' btnStart
			' 
			Me.btnStart.Location = New System.Drawing.Point(212, 102)
			Me.btnStart.Name = "btnStart"
			Me.btnStart.Size = New System.Drawing.Size(105, 33)
			Me.btnStart.TabIndex = 11
			Me.btnStart.Text = "Start"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			' 
			' btnPath
			' 
			Me.btnPath.Location = New System.Drawing.Point(323, 100)
			Me.btnPath.Name = "btnPath"
			Me.btnPath.Size = New System.Drawing.Size(82, 35)
			Me.btnPath.TabIndex = 10
			Me.btnPath.Text = "Select file"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
			' 
			' txtPath
			' 
			Me.txtPath.Location = New System.Drawing.Point(41, 9)
			Me.txtPath.Multiline = True
			Me.txtPath.Name = "txtPath"
			Me.txtPath.ReadOnly = True
			Me.txtPath.Size = New System.Drawing.Size(364, 52)
			Me.txtPath.TabIndex = 9
			' 
			' label1
			' 
			Me.label1.Location = New System.Drawing.Point(-1, 23)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(100, 20)
			Me.label1.TabIndex = 14
			Me.label1.Text = "path:"
			' 
			' label2
			' 
			Me.label2.Location = New System.Drawing.Point(54, 76)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(354, 20)
			Me.label2.TabIndex = 15
			Me.label2.Text = "version:"
			Me.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
			' 
			' rbUHFModule
			' 
			Me.rbUHFModule.AutoSize = True
			Me.rbUHFModule.Checked = True
			Me.rbUHFModule.Location = New System.Drawing.Point(41, 82)
			Me.rbUHFModule.Name = "rbUHFModule"
			Me.rbUHFModule.Size = New System.Drawing.Size(83, 16)
			Me.rbUHFModule.TabIndex = 16
			Me.rbUHFModule.TabStop = True
			Me.rbUHFModule.Text = "UHF module"
			Me.rbUHFModule.UseVisualStyleBackColor = True
			' 
			' rbReaderBootloader
			' 
			Me.rbReaderBootloader.AutoSize = True
			Me.rbReaderBootloader.Location = New System.Drawing.Point(41, 102)
			Me.rbReaderBootloader.Name = "rbReaderBootloader"
			Me.rbReaderBootloader.Size = New System.Drawing.Size(131, 16)
			Me.rbReaderBootloader.TabIndex = 17
			Me.rbReaderBootloader.Text = "Reader bootloader "
			Me.rbReaderBootloader.UseVisualStyleBackColor = True
			' 
			' rbEx10SDKFirmware
			' 
			Me.rbEx10SDKFirmware.AutoSize = True
			Me.rbEx10SDKFirmware.Location = New System.Drawing.Point(41, 124)
			Me.rbEx10SDKFirmware.Name = "rbEx10SDKFirmware"
			Me.rbEx10SDKFirmware.Size = New System.Drawing.Size(125, 16)
			Me.rbEx10SDKFirmware.TabIndex = 18
			Me.rbEx10SDKFirmware.Text = "Ex10 SDK firmware"
			Me.rbEx10SDKFirmware.UseVisualStyleBackColor = True
			' 
			' rbReaderApplication
			' 
			Me.rbReaderApplication.AutoSize = True
			Me.rbReaderApplication.Location = New System.Drawing.Point(41, 63)
			Me.rbReaderApplication.Name = "rbReaderApplication"
			Me.rbReaderApplication.Size = New System.Drawing.Size(131, 16)
			Me.rbReaderApplication.TabIndex = 19
			Me.rbReaderApplication.Text = "Reader application"
			Me.rbReaderApplication.UseVisualStyleBackColor = True
			' 
			' UHFUpgradeFormEx
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.ActiveCaption
			Me.ClientSize = New System.Drawing.Size(434, 431)
			Me.Controls.Add(Me.rbReaderApplication)
			Me.Controls.Add(Me.rbEx10SDKFirmware)
			Me.Controls.Add(Me.rbReaderBootloader)
			Me.Controls.Add(Me.rbUHFModule)
			Me.Controls.Add(Me.textBox1)
			Me.Controls.Add(Me.progressBar1)
			Me.Controls.Add(Me.btnStart)
			Me.Controls.Add(Me.btnPath)
			Me.Controls.Add(Me.txtPath)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.label2)
			Me.MaximizeBox = False
			Me.Name = "UHFUpgradeFormEx"
			Me.Text = "UHFUpgradeForm"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.UHFUpgradeForm_Load);
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private textBox1 As System.Windows.Forms.TextBox
		Private progressBar1 As System.Windows.Forms.ProgressBar
		Private WithEvents btnStart As System.Windows.Forms.Button
		Private WithEvents btnPath As System.Windows.Forms.Button
		Private txtPath As System.Windows.Forms.TextBox
		Private label1 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private rbUHFModule As System.Windows.Forms.RadioButton
		Private rbReaderBootloader As System.Windows.Forms.RadioButton
		Private rbEx10SDKFirmware As System.Windows.Forms.RadioButton
		Private rbReaderApplication As System.Windows.Forms.RadioButton
	End Class
End Namespace
