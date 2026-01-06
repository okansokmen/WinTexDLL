Namespace UHFAPP.multidevice
	Partial Public Class DeviceListForm
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
			Me.comboBox1 = New System.Windows.Forms.ComboBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.SuspendLayout()
			' 
			' comboBox1
			' 
			Me.comboBox1.FormattingEnabled = True
			Me.comboBox1.Location = New System.Drawing.Point(67, 42)
			Me.comboBox1.Name = "comboBox1"
			Me.comboBox1.Size = New System.Drawing.Size(177, 20)
			Me.comboBox1.TabIndex = 0
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(28, 45)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(23, 12)
			Me.label1.TabIndex = 1
			Me.label1.Text = "IP:"
			' 
			' DeviceListForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(284, 132)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.comboBox1)
			Me.Name = "DeviceListForm"
			Me.Text = "DeviceListForm"
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private WithEvents comboBox1 As System.Windows.Forms.ComboBox
		Private label1 As System.Windows.Forms.Label
	End Class
End Namespace
