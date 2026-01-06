<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRFIDRead
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lvEPC = New System.Windows.Forms.ListView()
        Me.columnHeaderID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeaderEPC = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeaderTID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeaderUser = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeaderRssi = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeaderCount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeaderANT = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvEPC
        '
        Me.lvEPC.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvEPC.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lvEPC.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeaderID, Me.columnHeaderEPC, Me.columnHeaderTID, Me.columnHeaderUser, Me.columnHeaderRssi, Me.columnHeaderCount, Me.columnHeaderANT})
        Me.lvEPC.Font = New System.Drawing.Font("SimSun", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lvEPC.FullRowSelect = True
        Me.lvEPC.HideSelection = False
        Me.lvEPC.Location = New System.Drawing.Point(12, 12)
        Me.lvEPC.Name = "lvEPC"
        Me.lvEPC.Size = New System.Drawing.Size(776, 386)
        Me.lvEPC.TabIndex = 3
        Me.lvEPC.UseCompatibleStateImageBehavior = False
        Me.lvEPC.View = System.Windows.Forms.View.Details
        '
        'columnHeaderID
        '
        Me.columnHeaderID.Text = "ID"
        Me.columnHeaderID.Width = 0
        '
        'columnHeaderEPC
        '
        Me.columnHeaderEPC.Text = "EPC"
        Me.columnHeaderEPC.Width = 420
        '
        'columnHeaderTID
        '
        Me.columnHeaderTID.Text = "TID"
        Me.columnHeaderTID.Width = 270
        '
        'columnHeaderUser
        '
        Me.columnHeaderUser.Text = "USER"
        Me.columnHeaderUser.Width = 270
        '
        'columnHeaderRssi
        '
        Me.columnHeaderRssi.Text = "Rssi"
        Me.columnHeaderRssi.Width = 90
        '
        'columnHeaderCount
        '
        Me.columnHeaderCount.Text = "Count"
        Me.columnHeaderCount.Width = 80
        '
        'columnHeaderANT
        '
        Me.columnHeaderANT.Text = "ANT"
        Me.columnHeaderANT.Width = 50
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 412)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(799, 46)
        Me.Panel1.TabIndex = 4
        '
        'Button2
        '
        Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button2.Location = New System.Drawing.Point(402, 12)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Close USB"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button1.Location = New System.Drawing.Point(321, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Open USB"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmRFIDRead
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(799, 458)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lvEPC)
        Me.Name = "frmRFIDRead"
        Me.Text = "frmRFIDRead"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents lvEPC As ListView
    Private WithEvents columnHeaderID As ColumnHeader
    Private WithEvents columnHeaderEPC As ColumnHeader
    Private WithEvents columnHeaderTID As ColumnHeader
    Private WithEvents columnHeaderUser As ColumnHeader
    Private WithEvents columnHeaderRssi As ColumnHeader
    Private WithEvents columnHeaderCount As ColumnHeader
    Private WithEvents columnHeaderANT As ColumnHeader
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
End Class
