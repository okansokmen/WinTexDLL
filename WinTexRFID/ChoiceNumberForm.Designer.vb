Namespace UHFAPP
	Partial Public Class ChoiceNumberForm
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
			Me.button1 = New System.Windows.Forms.Button()
			Me.SuspendLayout()
			' 
			' comboBox1
			' 
			Me.comboBox1.FormattingEnabled = True
			Me.comboBox1.Items.AddRange(New Object() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"})
			Me.comboBox1.Location = New System.Drawing.Point(58, 12)
			Me.comboBox1.Name = "comboBox1"
			Me.comboBox1.Size = New System.Drawing.Size(233, 20)
			Me.comboBox1.TabIndex = 0
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(11, 15)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(41, 12)
			Me.label1.TabIndex = 1
			Me.label1.Text = "编号："
			' 
			' button1
			' 
			Me.button1.Location = New System.Drawing.Point(84, 52)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(122, 36)
			Me.button1.TabIndex = 2
			Me.button1.Text = "确定"
			Me.button1.UseVisualStyleBackColor = True
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.button1.Click += new System.EventHandler(this.button1_Click);
			' 
			' ChoiceNumberForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(310, 118)
			Me.ControlBox = False
			Me.Controls.Add(Me.button1)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.comboBox1)
			Me.MaximizeBox = False
			Me.MinimizeBox = False
			Me.Name = "ChoiceNumberForm"
			Me.Text = "  "
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private WithEvents comboBox1 As System.Windows.Forms.ComboBox
		Private label1 As System.Windows.Forms.Label
		Private WithEvents button1 As System.Windows.Forms.Button
	End Class
End Namespace
