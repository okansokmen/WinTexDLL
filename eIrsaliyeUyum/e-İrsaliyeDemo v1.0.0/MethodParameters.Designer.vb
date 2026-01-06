Namespace eIrsaliyeUyum
	Partial Public Class MethodParameters
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MethodParameters))
            Me.dpBeginDate = New System.Windows.Forms.DateTimePicker()
            Me.dpEndDate = New System.Windows.Forms.DateTimePicker()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.label2 = New System.Windows.Forms.Label()
            Me.chkIsNew = New System.Windows.Forms.CheckBox()
            Me.SuspendLayout()
            '
            'dpBeginDate
            '
            Me.dpBeginDate.CustomFormat = "dd.MM.yyyy HH:mm"
            Me.dpBeginDate.Enabled = False
            Me.dpBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.dpBeginDate.Location = New System.Drawing.Point(120, 71)
            Me.dpBeginDate.Name = "dpBeginDate"
            Me.dpBeginDate.Size = New System.Drawing.Size(177, 20)
            Me.dpBeginDate.TabIndex = 15
            Me.dpBeginDate.Value = New Date(2017, 9, 18, 0, 0, 0, 0)
            '
            'dpEndDate
            '
            Me.dpEndDate.CustomFormat = "dd.MM.yyyy HH:mm"
            Me.dpEndDate.Enabled = False
            Me.dpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.dpEndDate.Location = New System.Drawing.Point(120, 95)
            Me.dpEndDate.Name = "dpEndDate"
            Me.dpEndDate.Size = New System.Drawing.Size(177, 20)
            Me.dpEndDate.TabIndex = 15
            Me.dpEndDate.Value = New Date(2017, 9, 18, 0, 0, 0, 0)
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(36, 71)
            Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(79, 13)
            Me.Label1.TabIndex = 16
            Me.Label1.Text = "BaşlangıçTarihi"
            '
            'label2
            '
            Me.label2.AutoSize = True
            Me.label2.Location = New System.Drawing.Point(36, 95)
            Me.label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(52, 13)
            Me.label2.TabIndex = 16
            Me.label2.Text = "BitişTarihi"
            '
            'chkIsNew
            '
            Me.chkIsNew.AutoSize = True
            Me.chkIsNew.Location = New System.Drawing.Point(120, 126)
            Me.chkIsNew.Margin = New System.Windows.Forms.Padding(2)
            Me.chkIsNew.Name = "chkIsNew"
            Me.chkIsNew.Size = New System.Drawing.Size(91, 17)
            Me.chkIsNew.TabIndex = 17
            Me.chkIsNew.Text = "Yeni Faturalar"
            Me.chkIsNew.TextAlign = System.Drawing.ContentAlignment.TopCenter
            Me.chkIsNew.UseVisualStyleBackColor = True
            '
            'MethodParameters
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(345, 478)
            Me.Controls.Add(Me.chkIsNew)
            Me.Controls.Add(Me.label2)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.dpEndDate)
            Me.Controls.Add(Me.dpBeginDate)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Margin = New System.Windows.Forms.Padding(2)
            Me.Name = "MethodParameters"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Uyum eIrsaliye Method Parameters"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region

        Private dpBeginDate As System.Windows.Forms.DateTimePicker
		Private dpEndDate As System.Windows.Forms.DateTimePicker
		Private Label1 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private chkIsNew As System.Windows.Forms.CheckBox
	End Class
End Namespace
