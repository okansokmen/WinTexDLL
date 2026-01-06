Namespace UHFAPP
	Partial Public Class WaitForm
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
			Me.labMessage = New System.Windows.Forms.Label()
			Me.timer1 = New System.Windows.Forms.Timer(Me.components)
			Me.SuspendLayout()
			' 
			' labMessage
			' 
			Me.labMessage.AutoSize = True
			Me.labMessage.Font = New System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(134))
			Me.labMessage.Location = New System.Drawing.Point(69, 52)
			Me.labMessage.Name = "labMessage"
			Me.labMessage.Size = New System.Drawing.Size(79, 19)
			Me.labMessage.TabIndex = 0
			Me.labMessage.Text = "wait..."
			' 
			' WaitForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(229, 130)
			Me.Controls.Add(Me.labMessage)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
			Me.Name = "WaitForm"
			Me.Text = "WaitForm"
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private labMessage As System.Windows.Forms.Label
		Private timer1 As System.Windows.Forms.Timer
	End Class
End Namespace
