<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
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
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblFilePath = New System.Windows.Forms.Label()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.lblConnectionString = New System.Windows.Forms.Label()
        Me.txtConnectionString = New System.Windows.Forms.TextBox()
        Me.btnTestConnection = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lstStatus = New System.Windows.Forms.ListBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(338, 24)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Excel Dosyası Veritabanına Aktarma"
        '
        'lblFilePath
        '
        Me.lblFilePath.AutoSize = True
        Me.lblFilePath.Location = New System.Drawing.Point(12, 50)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(107, 13)
        Me.lblFilePath.TabIndex = 1
        Me.lblFilePath.Text = "Excel Dosyası Seçin:"
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(12, 66)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(500, 20)
        Me.txtFilePath.TabIndex = 2
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(518, 66)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowse.TabIndex = 3
        Me.btnBrowse.Text = "Gözat..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lblConnectionString
        '
        Me.lblConnectionString.AutoSize = True
        Me.lblConnectionString.Location = New System.Drawing.Point(12, 105)
        Me.lblConnectionString.Name = "lblConnectionString"
        Me.lblConnectionString.Size = New System.Drawing.Size(156, 13)
        Me.lblConnectionString.TabIndex = 4
        Me.lblConnectionString.Text = "Veritabanı Bağlantı Dizesi:"
        '
        'txtConnectionString
        '
        Me.txtConnectionString.Location = New System.Drawing.Point(12, 121)
        Me.txtConnectionString.Multiline = True
        Me.txtConnectionString.Name = "txtConnectionString"
        Me.txtConnectionString.Size = New System.Drawing.Size(500, 60)
        Me.txtConnectionString.TabIndex = 5
        Me.txtConnectionString.Text = oConnection.cConnStr
        '
        'btnTestConnection
        '
        Me.btnTestConnection.Location = New System.Drawing.Point(518, 121)
        Me.btnTestConnection.Name = "btnTestConnection"
        Me.btnTestConnection.Size = New System.Drawing.Size(75, 60)
        Me.btnTestConnection.TabIndex = 6
        Me.btnTestConnection.Text = "Bağlantıyı Test Et"
        Me.btnTestConnection.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(12, 195)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(38, 13)
        Me.lblStatus.TabIndex = 7
        Me.lblStatus.Text = "Durum:"
        '
        'lstStatus
        '
        Me.lstStatus.FormattingEnabled = True
        Me.lstStatus.Location = New System.Drawing.Point(12, 211)
        Me.lstStatus.Name = "lstStatus"
        Me.lstStatus.Size = New System.Drawing.Size(581, 199)
        Me.lstStatus.TabIndex = 8
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 416)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(581, 23)
        Me.ProgressBar1.TabIndex = 9
        '
        'btnImport
        '
        Me.btnImport.BackColor = System.Drawing.Color.Green
        Me.btnImport.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnImport.ForeColor = System.Drawing.Color.White
        Me.btnImport.Location = New System.Drawing.Point(12, 445)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(581, 35)
        Me.btnImport.TabIndex = 10
        Me.btnImport.Text = "Veritabanına Aktar"
        Me.btnImport.UseVisualStyleBackColor = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(605, 492)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.lstStatus)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnTestConnection)
        Me.Controls.Add(Me.txtConnectionString)
        Me.Controls.Add(Me.lblConnectionString)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.txtFilePath)
        Me.Controls.Add(Me.lblFilePath)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "MainForm"
        Me.Text = "Excel Veritabanı Aktarma Aracı"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents lblFilePath As Label
    Friend WithEvents txtFilePath As TextBox
    Friend WithEvents btnBrowse As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents lblConnectionString As Label
    Friend WithEvents txtConnectionString As TextBox
    Friend WithEvents btnTestConnection As Button
    Friend WithEvents lblStatus As Label
    Friend WithEvents lstStatus As ListBox
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
