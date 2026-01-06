Namespace UHFAPP.custom
	Partial Public Class R5ModifyParametersForm
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
			Me.label1 = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.txtName = New System.Windows.Forms.TextBox()
			Me.txtValue = New System.Windows.Forms.TextBox()
			Me.textBox3 = New System.Windows.Forms.TextBox()
			Me.button1 = New System.Windows.Forms.Button()
			Me.SuspendLayout()
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label1.Location = New System.Drawing.Point(45, 27)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(48, 16)
			Me.label1.TabIndex = 0
			Me.label1.Text = "Name:"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label2.Location = New System.Drawing.Point(39, 77)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(56, 16)
			Me.label2.TabIndex = 1
			Me.label2.Text = "Value:"
			' 
			' txtName
			' 
			Me.txtName.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtName.Location = New System.Drawing.Point(108, 24)
			Me.txtName.Name = "txtName"
			Me.txtName.ReadOnly = True
			Me.txtName.Size = New System.Drawing.Size(221, 26)
			Me.txtName.TabIndex = 2
			' 
			' txtValue
			' 
			Me.txtValue.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtValue.Location = New System.Drawing.Point(108, 74)
			Me.txtValue.Name = "txtValue"
			Me.txtValue.Size = New System.Drawing.Size(221, 26)
			Me.txtValue.TabIndex = 3
			' 
			' textBox3
			' 
			Me.textBox3.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.textBox3.Location = New System.Drawing.Point(12, 106)
			Me.textBox3.Multiline = True
			Me.textBox3.Name = "textBox3"
			Me.textBox3.ReadOnly = True
			Me.textBox3.Size = New System.Drawing.Size(381, 151)
			Me.textBox3.TabIndex = 4
			' 
			' button1
			' 
			Me.button1.Location = New System.Drawing.Point(155, 274)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(100, 43)
			Me.button1.TabIndex = 5
			Me.button1.Text = "Set"
			Me.button1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button1.Click += new System.EventHandler(this.button1_Click);
			' 
			' R5ModifyParametersForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(405, 329)
			Me.Controls.Add(Me.button1)
			Me.Controls.Add(Me.textBox3)
			Me.Controls.Add(Me.txtValue)
			Me.Controls.Add(Me.txtName)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.label1)
			Me.Name = "R5ModifyParametersForm"
			Me.Text = "R5ModifyParametersForm"
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private label1 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private txtName As System.Windows.Forms.TextBox
		Private txtValue As System.Windows.Forms.TextBox
		Private textBox3 As System.Windows.Forms.TextBox
		Private WithEvents button1 As System.Windows.Forms.Button
	End Class
End Namespace
