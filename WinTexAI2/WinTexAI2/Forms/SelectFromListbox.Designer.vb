<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SelectFromListbox
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
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.TextEdit1 = New System.Windows.Forms.TextBox()
        Me.SimpleButton1 = New System.Windows.Forms.Button()
        Me.SimpleButton2 = New System.Windows.Forms.Button()
        Me.SimpleButton3 = New System.Windows.Forms.Button()
        Me.lblPageInfo = New System.Windows.Forms.Label()
        Me.btnFirst = New System.Windows.Forms.Button()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.txtPageNumber = New System.Windows.Forms.TextBox()
        Me.lblGoToPage = New System.Windows.Forms.Label()
        Me.pnlPagination = New System.Windows.Forms.Panel()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        
        ' ListView1
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(12, 45)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(960, 400)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        
        ' TextEdit1 (Filter textbox)
        Me.TextEdit1.Location = New System.Drawing.Point(65, 12)
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Size = New System.Drawing.Size(200, 20)
        Me.TextEdit1.TabIndex = 1
        
        ' lblFilter
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Location = New System.Drawing.Point(12, 15)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(47, 13)
        Me.lblFilter.TabIndex = 8
        Me.lblFilter.Text = "Filtre:"
        
        ' SimpleButton1 (Filter button)
        Me.SimpleButton1.Location = New System.Drawing.Point(271, 10)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(75, 23)
        Me.SimpleButton1.TabIndex = 2
        Me.SimpleButton1.Text = "Filtrele"
        Me.SimpleButton1.UseVisualStyleBackColor = True
        
        ' pnlPagination (Pagination panel)
        Me.pnlPagination.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPagination.Location = New System.Drawing.Point(12, 451)
        Me.pnlPagination.Name = "pnlPagination"
        Me.pnlPagination.Size = New System.Drawing.Size(960, 40)
        Me.pnlPagination.TabIndex = 9
        
        ' btnFirst
        Me.btnFirst.Location = New System.Drawing.Point(0, 8)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(60, 23)
        Me.btnFirst.TabIndex = 3
        Me.btnFirst.Text = "<<"
        Me.btnFirst.UseVisualStyleBackColor = True
        AddHandler Me.btnFirst.Click, AddressOf Me.btnFirst_Click
        
        ' btnPrevious
        Me.btnPrevious.Location = New System.Drawing.Point(66, 8)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(60, 23)
        Me.btnPrevious.TabIndex = 4
        Me.btnPrevious.Text = "<"
        Me.btnPrevious.UseVisualStyleBackColor = True
        AddHandler Me.btnPrevious.Click, AddressOf Me.btnPrevious_Click
        
        ' lblPageInfo
        Me.lblPageInfo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPageInfo.Location = New System.Drawing.Point(200, 8)
        Me.lblPageInfo.Name = "lblPageInfo"
        Me.lblPageInfo.Size = New System.Drawing.Size(300, 23)
        Me.lblPageInfo.TabIndex = 7
        Me.lblPageInfo.Text = "Sayfa 1 / 1 (Toplam: 0 kayıt)"
        Me.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        
        ' btnNext
        Me.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnNext.Location = New System.Drawing.Point(834, 8)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(60, 23)
        Me.btnNext.TabIndex = 5
        Me.btnNext.Text = ">"
        Me.btnNext.UseVisualStyleBackColor = True
        AddHandler Me.btnNext.Click, AddressOf Me.btnNext_Click
        
        ' btnLast
        Me.btnLast.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnLast.Location = New System.Drawing.Point(900, 8)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(60, 23)
        Me.btnLast.TabIndex = 6
        Me.btnLast.Text = ">>"
        Me.btnLast.UseVisualStyleBackColor = True
        AddHandler Me.btnLast.Click, AddressOf Me.btnLast_Click
        
        ' lblGoToPage
        Me.lblGoToPage.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblGoToPage.AutoSize = True
        Me.lblGoToPage.Location = New System.Drawing.Point(650, 13)
        Me.lblGoToPage.Name = "lblGoToPage"
        Me.lblGoToPage.Size = New System.Drawing.Size(67, 13)
        Me.lblGoToPage.TabIndex = 10
        Me.lblGoToPage.Text = "Sayfaya git:"
        
        ' txtPageNumber
        Me.txtPageNumber.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.txtPageNumber.Location = New System.Drawing.Point(723, 10)
        Me.txtPageNumber.Name = "txtPageNumber"
        Me.txtPageNumber.Size = New System.Drawing.Size(50, 20)
        Me.txtPageNumber.TabIndex = 11
        Me.txtPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        AddHandler Me.txtPageNumber.KeyPress, AddressOf Me.txtPageNumber_KeyPress
        AddHandler Me.txtPageNumber.KeyDown, AddressOf Me.txtPageNumber_KeyDown
        
        ' Add controls to pagination panel
        Me.pnlPagination.Controls.Add(Me.btnFirst)
        Me.pnlPagination.Controls.Add(Me.btnPrevious)
        Me.pnlPagination.Controls.Add(Me.lblPageInfo)
        Me.pnlPagination.Controls.Add(Me.btnNext)
        Me.pnlPagination.Controls.Add(Me.btnLast)
        Me.pnlPagination.Controls.Add(Me.lblGoToPage)
        Me.pnlPagination.Controls.Add(Me.txtPageNumber)
        
        ' SimpleButton2 (Select button)
        Me.SimpleButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton2.Location = New System.Drawing.Point(816, 497)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(75, 23)
        Me.SimpleButton2.TabIndex = 12
        Me.SimpleButton2.Text = "Seç"
        Me.SimpleButton2.UseVisualStyleBackColor = True
        
        ' SimpleButton3 (Cancel button)
        Me.SimpleButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton3.Location = New System.Drawing.Point(897, 497)
        Me.SimpleButton3.Name = "SimpleButton3"
        Me.SimpleButton3.Size = New System.Drawing.Size(75, 23)
        Me.SimpleButton3.TabIndex = 13
        Me.SimpleButton3.Text = "İptal"
        Me.SimpleButton3.UseVisualStyleBackColor = True
        
        ' SelectFromListbox Form
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 532)
        Me.Controls.Add(Me.SimpleButton3)
        Me.Controls.Add(Me.SimpleButton2)
        Me.Controls.Add(Me.pnlPagination)
        Me.Controls.Add(Me.lblFilter)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.TextEdit1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.MinimizeBox = True
        Me.MinimumSize = New System.Drawing.Size(800, 500)
        Me.Name = "SelectFromListbox"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Kayıt Seçimi"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListView1 As ListView
    Friend WithEvents TextEdit1 As TextBox
    Friend WithEvents SimpleButton1 As Button
    Friend WithEvents SimpleButton2 As Button
    Friend WithEvents SimpleButton3 As Button
    Friend WithEvents lblPageInfo As Label
    Friend WithEvents btnFirst As Button
    Friend WithEvents btnPrevious As Button
    Friend WithEvents btnNext As Button
    Friend WithEvents btnLast As Button
    Friend WithEvents txtPageNumber As TextBox
    Friend WithEvents lblGoToPage As Label
    Friend WithEvents pnlPagination As Panel
    Friend WithEvents lblFilter As Label
End Class
