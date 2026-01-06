Namespace UHFAPP.barcode
	Partial Public Class HidInputForm
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
			Me.cmbFormat = New System.Windows.Forms.ComboBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.btnOpen = New System.Windows.Forms.Button()
			Me.textBox1 = New System.Windows.Forms.TextBox()
			Me.SuspendLayout()
			' 
			' cmbFormat
			' 
			Me.cmbFormat.FormattingEnabled = True
			Me.cmbFormat.Items.AddRange(New Object() { "ANSI", "UTF8"})
			Me.cmbFormat.Location = New System.Drawing.Point(192, 44)
			Me.cmbFormat.Name = "cmbFormat"
			Me.cmbFormat.Size = New System.Drawing.Size(121, 20)
			Me.cmbFormat.TabIndex = 0
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.cmbFormat.SelectedIndexChanged += new System.EventHandler(this.cmbFormat_SelectedIndexChanged);
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(127, 47)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(59, 12)
			Me.label1.TabIndex = 1
			Me.label1.Text = "Encoding:"
			' 
			' btnOpen
			' 
			Me.btnOpen.Location = New System.Drawing.Point(129, 108)
			Me.btnOpen.Name = "btnOpen"
			Me.btnOpen.Size = New System.Drawing.Size(210, 49)
			Me.btnOpen.TabIndex = 2
			Me.btnOpen.Text = "openUSB"
			Me.btnOpen.UseVisualStyleBackColor = True
			Me.btnOpen.Visible = False

			' 
			' textBox1
			' 
			Me.textBox1.Location = New System.Drawing.Point(12, 70)
			Me.textBox1.Multiline = True
			Me.textBox1.Name = "textBox1"
			Me.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.textBox1.Size = New System.Drawing.Size(973, 435)
			Me.textBox1.TabIndex = 3
			' 
			' HidInputForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 12F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.InactiveCaption
			Me.ClientSize = New System.Drawing.Size(997, 526)
			Me.Controls.Add(Me.textBox1)
			Me.Controls.Add(Me.btnOpen)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.cmbFormat)
			Me.Name = "HidInputForm"
			Me.Text = "BarcodeForm"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.VisibleChanged += new System.EventHandler(this.HidInputForm_VisibleChanged);
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private WithEvents cmbFormat As System.Windows.Forms.ComboBox
		Private label1 As System.Windows.Forms.Label
		Private btnOpen As System.Windows.Forms.Button
		Private textBox1 As System.Windows.Forms.TextBox
	End Class
End Namespace
