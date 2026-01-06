Namespace UHFAPP.custom
	Partial Public Class CryptoTransmitForm
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
			Me.txtSendData = New System.Windows.Forms.TextBox()
			Me.btnSend = New System.Windows.Forms.Button()
			Me.textBox2 = New System.Windows.Forms.TextBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.label3 = New System.Windows.Forms.Label()
			Me.button1 = New System.Windows.Forms.Button()
			Me.SuspendLayout()
			' 
			' txtSendData
			' 
			Me.txtSendData.Location = New System.Drawing.Point(12, 34)
			Me.txtSendData.Multiline = True
			Me.txtSendData.Name = "txtSendData"
			Me.txtSendData.Size = New System.Drawing.Size(814, 225)
			Me.txtSendData.TabIndex = 0
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.txtSendData.TextChanged += new System.EventHandler(this.txtSendData_TextChanged);
			' 
			' btnSend
			' 
			Me.btnSend.Location = New System.Drawing.Point(731, 265)
			Me.btnSend.Name = "btnSend"
			Me.btnSend.Size = New System.Drawing.Size(95, 39)
			Me.btnSend.TabIndex = 1
			Me.btnSend.Text = "发送"
			Me.btnSend.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			' 
			' textBox2
			' 
			Me.textBox2.Location = New System.Drawing.Point(12, 322)
			Me.textBox2.Multiline = True
			Me.textBox2.Name = "textBox2"
			Me.textBox2.Size = New System.Drawing.Size(814, 189)
			Me.textBox2.TabIndex = 2
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(12, 19)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(71, 12)
			Me.label1.TabIndex = 3
			Me.label1.Text = "发送的数据:"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(12, 307)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(71, 12)
			Me.label2.TabIndex = 4
			Me.label2.Text = "接收的数据:"
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(12, 262)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(11, 12)
			Me.label3.TabIndex = 5
			Me.label3.Text = "0"
			' 
			' button1
			' 
			Me.button1.Location = New System.Drawing.Point(731, 517)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(95, 39)
			Me.button1.TabIndex = 6
			Me.button1.Text = "清空"
			Me.button1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button1.Click += new System.EventHandler(this.button1_Click);
			' 
			' CryptoTransmitForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(838, 562)
			Me.Controls.Add(Me.button1)
			Me.Controls.Add(Me.label3)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.textBox2)
			Me.Controls.Add(Me.btnSend)
			Me.Controls.Add(Me.txtSendData)
			Me.KeyPreview = True
			Me.Name = "CryptoTransmitForm"
			Me.Text = "CryptoTransmitForm"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReadWriteTagForm_KeyDown);
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private WithEvents txtSendData As System.Windows.Forms.TextBox
		Private WithEvents btnSend As System.Windows.Forms.Button
		Private textBox2 As System.Windows.Forms.TextBox
		Private label1 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private label3 As System.Windows.Forms.Label
		Private WithEvents button1 As System.Windows.Forms.Button
	End Class
End Namespace
