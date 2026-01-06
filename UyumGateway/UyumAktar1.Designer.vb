<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UyumAktar1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UyumAktar1))
        Me.C1TrueDBGrid1 = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.C1SplitContainer1 = New C1.Win.C1SplitContainer.C1SplitContainer()
        Me.C1SplitterPanel1 = New C1.Win.C1SplitContainer.C1SplitterPanel()
        Me.C1SplitterPanel2 = New C1.Win.C1SplitContainer.C1SplitterPanel()
        Me.C1TrueDBGrid2 = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.C1Button5 = New C1.Win.C1Input.C1Button()
        Me.C1Button4 = New C1.Win.C1Input.C1Button()
        Me.C1Button3 = New C1.Win.C1Input.C1Button()
        Me.C1Button2 = New C1.Win.C1Input.C1Button()
        Me.C1Button1 = New C1.Win.C1Input.C1Button()
        Me.C1SuperLabel2 = New C1.Win.C1SuperTooltip.C1SuperLabel()
        Me.C1SuperLabel1 = New C1.Win.C1SuperTooltip.C1SuperLabel()
        CType(Me.C1TrueDBGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1SplitContainer1.SuspendLayout()
        Me.C1SplitterPanel1.SuspendLayout()
        Me.C1SplitterPanel2.SuspendLayout()
        CType(Me.C1TrueDBGrid2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.C1Button5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Button4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Button3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Button2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Button1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'C1TrueDBGrid1
        '
        Me.C1TrueDBGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1TrueDBGrid1.GroupByCaption = "Drag a column header here to group by that column"
        Me.C1TrueDBGrid1.Images.Add(CType(resources.GetObject("C1TrueDBGrid1.Images"), System.Drawing.Image))
        Me.C1TrueDBGrid1.Location = New System.Drawing.Point(0, 0)
        Me.C1TrueDBGrid1.Name = "C1TrueDBGrid1"
        Me.C1TrueDBGrid1.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.C1TrueDBGrid1.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.C1TrueDBGrid1.PreviewInfo.ZoomFactor = 75.0R
        Me.C1TrueDBGrid1.PrintInfo.PageSettings = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.C1TrueDBGrid1.Size = New System.Drawing.Size(776, 160)
        Me.C1TrueDBGrid1.TabIndex = 3
        Me.C1TrueDBGrid1.UseCompatibleTextRendering = False
        Me.C1TrueDBGrid1.PropBag = resources.GetString("C1TrueDBGrid1.PropBag")
        '
        'C1SplitContainer1
        '
        Me.C1SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C1SplitContainer1.AutoSizeElement = C1.Framework.AutoSizeElement.Both
        Me.C1SplitContainer1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.C1SplitContainer1.CollapsingCueColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.C1SplitContainer1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.C1SplitContainer1.Location = New System.Drawing.Point(12, 12)
        Me.C1SplitContainer1.Name = "C1SplitContainer1"
        Me.C1SplitContainer1.Panels.Add(Me.C1SplitterPanel1)
        Me.C1SplitContainer1.Panels.Add(Me.C1SplitterPanel2)
        Me.C1SplitContainer1.Size = New System.Drawing.Size(776, 366)
        Me.C1SplitContainer1.TabIndex = 6
        '
        'C1SplitterPanel1
        '
        Me.C1SplitterPanel1.Controls.Add(Me.C1TrueDBGrid1)
        Me.C1SplitterPanel1.Height = 181
        Me.C1SplitterPanel1.Location = New System.Drawing.Point(0, 21)
        Me.C1SplitterPanel1.Name = "C1SplitterPanel1"
        Me.C1SplitterPanel1.Size = New System.Drawing.Size(776, 160)
        Me.C1SplitterPanel1.TabIndex = 0
        Me.C1SplitterPanel1.Text = "Okunan Kayıtlar"
        '
        'C1SplitterPanel2
        '
        Me.C1SplitterPanel2.Controls.Add(Me.C1TrueDBGrid2)
        Me.C1SplitterPanel2.Height = 181
        Me.C1SplitterPanel2.Location = New System.Drawing.Point(0, 206)
        Me.C1SplitterPanel2.Name = "C1SplitterPanel2"
        Me.C1SplitterPanel2.Size = New System.Drawing.Size(776, 160)
        Me.C1SplitterPanel2.TabIndex = 1
        Me.C1SplitterPanel2.Text = "Yazılan Kayıtlar"
        '
        'C1TrueDBGrid2
        '
        Me.C1TrueDBGrid2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1TrueDBGrid2.GroupByCaption = "Drag a column header here to group by that column"
        Me.C1TrueDBGrid2.Images.Add(CType(resources.GetObject("C1TrueDBGrid2.Images"), System.Drawing.Image))
        Me.C1TrueDBGrid2.Location = New System.Drawing.Point(0, 0)
        Me.C1TrueDBGrid2.Name = "C1TrueDBGrid2"
        Me.C1TrueDBGrid2.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.C1TrueDBGrid2.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.C1TrueDBGrid2.PreviewInfo.ZoomFactor = 75.0R
        Me.C1TrueDBGrid2.PrintInfo.PageSettings = CType(resources.GetObject("C1TrueDBGrid2.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.C1TrueDBGrid2.Size = New System.Drawing.Size(776, 160)
        Me.C1TrueDBGrid2.TabIndex = 4
        Me.C1TrueDBGrid2.UseCompatibleTextRendering = False
        Me.C1TrueDBGrid2.PropBag = resources.GetString("C1TrueDBGrid2.PropBag")
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.GroupBox1.Controls.Add(Me.C1SuperLabel2)
        Me.GroupBox1.Controls.Add(Me.C1SuperLabel1)
        Me.GroupBox1.Controls.Add(Me.C1Button5)
        Me.GroupBox1.Controls.Add(Me.C1Button4)
        Me.GroupBox1.Controls.Add(Me.C1Button3)
        Me.GroupBox1.Controls.Add(Me.C1Button2)
        Me.GroupBox1.Controls.Add(Me.C1Button1)
        Me.GroupBox1.Location = New System.Drawing.Point(102, 384)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(597, 62)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        '
        'C1Button5
        '
        Me.C1Button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.C1Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.C1Button5.ForeColor = System.Drawing.Color.Green
        Me.C1Button5.Location = New System.Drawing.Point(6, 7)
        Me.C1Button5.Name = "C1Button5"
        Me.C1Button5.Size = New System.Drawing.Size(75, 49)
        Me.C1Button5.TabIndex = 14
        Me.C1Button5.Text = "Temizle"
        Me.C1Button5.UseVisualStyleBackColor = True
        '
        'C1Button4
        '
        Me.C1Button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.C1Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.C1Button4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.C1Button4.Location = New System.Drawing.Point(249, 7)
        Me.C1Button4.Name = "C1Button4"
        Me.C1Button4.Size = New System.Drawing.Size(75, 49)
        Me.C1Button4.TabIndex = 13
        Me.C1Button4.Text = "Yaz"
        Me.C1Button4.UseVisualStyleBackColor = True
        '
        'C1Button3
        '
        Me.C1Button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.C1Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.C1Button3.ForeColor = System.Drawing.Color.Blue
        Me.C1Button3.Location = New System.Drawing.Point(168, 7)
        Me.C1Button3.Name = "C1Button3"
        Me.C1Button3.Size = New System.Drawing.Size(75, 49)
        Me.C1Button3.TabIndex = 12
        Me.C1Button3.Text = "Oku 0,3"
        Me.C1Button3.UseVisualStyleBackColor = True
        '
        'C1Button2
        '
        Me.C1Button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.C1Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.C1Button2.ForeColor = System.Drawing.Color.Red
        Me.C1Button2.Location = New System.Drawing.Point(330, 7)
        Me.C1Button2.Name = "C1Button2"
        Me.C1Button2.Size = New System.Drawing.Size(75, 49)
        Me.C1Button2.TabIndex = 11
        Me.C1Button2.Text = "Çıkış"
        Me.C1Button2.UseVisualStyleBackColor = True
        '
        'C1Button1
        '
        Me.C1Button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.C1Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.C1Button1.ForeColor = System.Drawing.Color.Blue
        Me.C1Button1.Location = New System.Drawing.Point(87, 7)
        Me.C1Button1.Name = "C1Button1"
        Me.C1Button1.Size = New System.Drawing.Size(75, 49)
        Me.C1Button1.TabIndex = 10
        Me.C1Button1.Text = "Oku 0"
        Me.C1Button1.UseVisualStyleBackColor = True
        '
        'C1SuperLabel2
        '
        Me.C1SuperLabel2.Location = New System.Drawing.Point(411, 37)
        Me.C1SuperLabel2.Name = "C1SuperLabel2"
        Me.C1SuperLabel2.Size = New System.Drawing.Size(180, 17)
        Me.C1SuperLabel2.TabIndex = 18
        Me.C1SuperLabel2.Text = "Aktarılan Satır"
        Me.C1SuperLabel2.UseMnemonic = True
        '
        'C1SuperLabel1
        '
        Me.C1SuperLabel1.Location = New System.Drawing.Point(411, 8)
        Me.C1SuperLabel1.Name = "C1SuperLabel1"
        Me.C1SuperLabel1.Size = New System.Drawing.Size(180, 17)
        Me.C1SuperLabel1.TabIndex = 17
        Me.C1SuperLabel1.Text = "Toplam Satır"
        Me.C1SuperLabel1.UseMnemonic = True
        '
        'UyumAktar1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.C1SplitContainer1)
        Me.Name = "UyumAktar1"
        Me.Text = "Uyum Log Dosyasıyla Aktarma"
        CType(Me.C1TrueDBGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1SplitContainer1.ResumeLayout(False)
        Me.C1SplitterPanel1.ResumeLayout(False)
        Me.C1SplitterPanel2.ResumeLayout(False)
        CType(Me.C1TrueDBGrid2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.C1Button5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Button4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Button3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Button2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Button1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents C1TrueDBGrid1 As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents C1SplitContainer1 As C1.Win.C1SplitContainer.C1SplitContainer
    Friend WithEvents C1SplitterPanel1 As C1.Win.C1SplitContainer.C1SplitterPanel
    Friend WithEvents C1SplitterPanel2 As C1.Win.C1SplitContainer.C1SplitterPanel
    Friend WithEvents C1TrueDBGrid2 As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents C1Button5 As C1.Win.C1Input.C1Button
    Friend WithEvents C1Button4 As C1.Win.C1Input.C1Button
    Friend WithEvents C1Button3 As C1.Win.C1Input.C1Button
    Friend WithEvents C1Button2 As C1.Win.C1Input.C1Button
    Friend WithEvents C1Button1 As C1.Win.C1Input.C1Button
    Friend WithEvents C1SuperLabel2 As C1.Win.C1SuperTooltip.C1SuperLabel
    Friend WithEvents C1SuperLabel1 As C1.Win.C1SuperTooltip.C1SuperLabel
End Class
