Namespace eIrsaliyeUyum
	Partial Public Class FrmViewer
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmViewer))
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.webBrowser1 = New System.Windows.Forms.WebBrowser()
            Me.splitter1 = New System.Windows.Forms.Splitter()
            Me.panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.AutoScroll = True
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Margin = New System.Windows.Forms.Padding(2)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(318, 650)
            Me.panel1.TabIndex = 1
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.webBrowser1)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel2.Location = New System.Drawing.Point(318, 0)
            Me.panel2.Margin = New System.Windows.Forms.Padding(2)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(532, 650)
            Me.panel2.TabIndex = 2
            '
            'webBrowser1
            '
            Me.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.webBrowser1.Location = New System.Drawing.Point(0, 0)
            Me.webBrowser1.Margin = New System.Windows.Forms.Padding(2)
            Me.webBrowser1.MinimumSize = New System.Drawing.Size(15, 16)
            Me.webBrowser1.Name = "webBrowser1"
            Me.webBrowser1.Size = New System.Drawing.Size(532, 650)
            Me.webBrowser1.TabIndex = 0
            '
            'splitter1
            '
            Me.splitter1.Location = New System.Drawing.Point(318, 0)
            Me.splitter1.Margin = New System.Windows.Forms.Padding(2)
            Me.splitter1.Name = "splitter1"
            Me.splitter1.Size = New System.Drawing.Size(14, 650)
            Me.splitter1.TabIndex = 3
            Me.splitter1.TabStop = False
            '
            'FrmViewer
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(850, 650)
            Me.Controls.Add(Me.splitter1)
            Me.Controls.Add(Me.panel2)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Margin = New System.Windows.Forms.Padding(2)
            Me.Name = "FrmViewer"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Uyum eIrsaliye Görüntüleme Penceresi"
            Me.panel2.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private panel1 As System.Windows.Forms.Panel
		Private panel2 As System.Windows.Forms.Panel
		Private splitter1 As System.Windows.Forms.Splitter
		Private webBrowser1 As System.Windows.Forms.WebBrowser
	End Class
End Namespace
