Namespace UHFAPP.custom
	Partial Public Class WriteEPCSimpleDemo
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
			Me.txtDataW = New System.Windows.Forms.TextBox()
			Me.txtDataR = New System.Windows.Forms.TextBox()
			Me.btnRead = New System.Windows.Forms.Button()
			Me.btnWrite = New System.Windows.Forms.Button()
			Me.label3 = New System.Windows.Forms.Label()
			Me.SuspendLayout()
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label1.Location = New System.Drawing.Point(24, 118)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(80, 16)
			Me.label1.TabIndex = 0
			Me.label1.Text = "数据录入:"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label2.Location = New System.Drawing.Point(24, 42)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(80, 16)
			Me.label2.TabIndex = 1
			Me.label2.Text = "数据内容:"
			' 
			' txtDataW
			' 
			Me.txtDataW.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtDataW.Location = New System.Drawing.Point(26, 137)
			Me.txtDataW.Name = "txtDataW"
			Me.txtDataW.Size = New System.Drawing.Size(416, 26)
			Me.txtDataW.TabIndex = 2
			' 
			' txtDataR
			' 
			Me.txtDataR.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.txtDataR.Location = New System.Drawing.Point(26, 61)
			Me.txtDataR.Name = "txtDataR"
			Me.txtDataR.Size = New System.Drawing.Size(416, 26)
			Me.txtDataR.TabIndex = 3
			' 
			' btnRead
			' 
			Me.btnRead.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnRead.Location = New System.Drawing.Point(465, 52)
			Me.btnRead.Name = "btnRead"
			Me.btnRead.Size = New System.Drawing.Size(84, 40)
			Me.btnRead.TabIndex = 4
			Me.btnRead.Text = "读取"
			Me.btnRead.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
			' 
			' btnWrite
			' 
			Me.btnWrite.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.btnWrite.Location = New System.Drawing.Point(465, 128)
			Me.btnWrite.Name = "btnWrite"
			Me.btnWrite.Size = New System.Drawing.Size(84, 40)
			Me.btnWrite.TabIndex = 5
			Me.btnWrite.Text = "写入"
			Me.btnWrite.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Font = New System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.label3.Location = New System.Drawing.Point(88, 187)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(0, 16)
			Me.label3.TabIndex = 6
			' 
			' WriteEPCSimpleDemo
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(618, 235)
			Me.Controls.Add(Me.label3)
			Me.Controls.Add(Me.btnWrite)
			Me.Controls.Add(Me.btnRead)
			Me.Controls.Add(Me.txtDataR)
			Me.Controls.Add(Me.txtDataW)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.label1)
			Me.Name = "WriteEPCSimpleDemo"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.WriteEPCSimpleDemo_Load);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WriteEPCSimpleDemo_FormClosing);
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private label1 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private txtDataW As System.Windows.Forms.TextBox
		Private txtDataR As System.Windows.Forms.TextBox
		Private WithEvents btnRead As System.Windows.Forms.Button
		Private WithEvents btnWrite As System.Windows.Forms.Button
		Private label3 As System.Windows.Forms.Label
	End Class
End Namespace
