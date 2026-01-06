Namespace eIrsaliyeUyum
	Partial Public Class FrmInfo
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInfo))
            Me.label1 = New System.Windows.Forms.Label()
            Me.lblCalledMethod = New System.Windows.Forms.Label()
            Me.label2 = New System.Windows.Forms.Label()
            Me.lblInvoiceNumber = New System.Windows.Forms.Label()
            Me.label3 = New System.Windows.Forms.Label()
            Me.label4 = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'label1
            '
            Me.label1.AutoSize = True
            Me.label1.Location = New System.Drawing.Point(17, 41)
            Me.label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(96, 13)
            Me.label1.TabIndex = 0
            Me.label1.Text = "Çağrılan Metot Adı:"
            '
            'lblCalledMethod
            '
            Me.lblCalledMethod.AutoSize = True
            Me.lblCalledMethod.Location = New System.Drawing.Point(128, 41)
            Me.lblCalledMethod.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.lblCalledMethod.Name = "lblCalledMethod"
            Me.lblCalledMethod.Size = New System.Drawing.Size(16, 13)
            Me.lblCalledMethod.TabIndex = 1
            Me.lblCalledMethod.Text = "..."
            '
            'label2
            '
            Me.label2.AutoSize = True
            Me.label2.Location = New System.Drawing.Point(28, 71)
            Me.label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(60, 13)
            Me.label2.TabIndex = 0
            Me.label2.Text = "Fatura No: "
            '
            'lblInvoiceNumber
            '
            Me.lblInvoiceNumber.AutoSize = True
            Me.lblInvoiceNumber.Location = New System.Drawing.Point(128, 71)
            Me.lblInvoiceNumber.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.lblInvoiceNumber.Name = "lblInvoiceNumber"
            Me.lblInvoiceNumber.Size = New System.Drawing.Size(16, 13)
            Me.lblInvoiceNumber.TabIndex = 1
            Me.lblInvoiceNumber.Text = "..."
            '
            'label3
            '
            Me.label3.AutoSize = True
            Me.label3.Location = New System.Drawing.Point(28, 95)
            Me.label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(60, 13)
            Me.label3.TabIndex = 0
            Me.label3.Text = "Fatura No: "
            '
            'label4
            '
            Me.label4.AutoSize = True
            Me.label4.Location = New System.Drawing.Point(128, 95)
            Me.label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label4.Name = "label4"
            Me.label4.Size = New System.Drawing.Size(16, 13)
            Me.label4.TabIndex = 1
            Me.label4.Text = "..."
            '
            'FrmInfo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(430, 412)
            Me.Controls.Add(Me.label4)
            Me.Controls.Add(Me.lblInvoiceNumber)
            Me.Controls.Add(Me.lblCalledMethod)
            Me.Controls.Add(Me.label3)
            Me.Controls.Add(Me.label2)
            Me.Controls.Add(Me.label1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Margin = New System.Windows.Forms.Padding(2)
            Me.Name = "FrmInfo"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Uyum eIrsaliye Info Penceresi"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region

        Private label1 As System.Windows.Forms.Label
		Private lblCalledMethod As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private lblInvoiceNumber As System.Windows.Forms.Label
		Private label3 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
	End Class
End Namespace
