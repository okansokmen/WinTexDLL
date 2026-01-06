Namespace WinForm_Test
	Partial Public Class frmWaitingBox
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
			Me.components = New System.ComponentModel.Container()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.labMessage = New System.Windows.Forms.Label()
			Me.timer1 = New System.Windows.Forms.Timer(Me.components)
			Me.panel1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' panel1
			' 
			Me.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
			Me.panel1.Controls.Add(Me.labMessage)
			Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel1.Location = New System.Drawing.Point(4, 4)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(284, 127)
			Me.panel1.TabIndex = 0
			' 
			' labMessage
			' 
			Me.labMessage.AutoSize = True
			Me.labMessage.Font = New System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
			Me.labMessage.Location = New System.Drawing.Point(31, 43)
			Me.labMessage.Name = "labMessage"
			Me.labMessage.Size = New System.Drawing.Size(184, 19)
			Me.labMessage.TabIndex = 0
			Me.labMessage.Text = "正在处理数据，请稍后..."
			' 
			' timer1
			' 
			Me.timer1.Interval = 1000
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			' 
			' frmWaitingBox
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.Color.DimGray
			Me.ClientSize = New System.Drawing.Size(292, 135)
			Me.Controls.Add(Me.panel1)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
			Me.Name = "frmWaitingBox"
			Me.Padding = New System.Windows.Forms.Padding(4)
			Me.Text = "frmWaitingBox"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Shown += new System.EventHandler(this.frmWaitingBox_Shown);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmWaitingBox_FormClosing);
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private panel1 As System.Windows.Forms.Panel
		Private labMessage As System.Windows.Forms.Label
		Private WithEvents timer1 As System.Windows.Forms.Timer
	End Class
End Namespace
