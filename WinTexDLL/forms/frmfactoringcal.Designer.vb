<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmfactoringcal
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.tutar = New DevExpress.XtraEditors.TextEdit()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.toplam = New DevExpress.XtraEditors.TextEdit()
        Me.komtutar = New DevExpress.XtraEditors.TextEdit()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.kesinti = New DevExpress.XtraEditors.TextEdit()
        Me.komisyon = New DevExpress.XtraEditors.TextEdit()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.oran = New DevExpress.XtraEditors.TextEdit()
        Me.vade = New DevExpress.XtraEditors.TextEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.tutar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.toplam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.komtutar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.kesinti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.komisyon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oran.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.vade.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.tutar)
        Me.GroupControl1.Controls.Add(Me.Label5)
        Me.GroupControl1.Controls.Add(Me.toplam)
        Me.GroupControl1.Controls.Add(Me.komtutar)
        Me.GroupControl1.Controls.Add(Me.Label7)
        Me.GroupControl1.Controls.Add(Me.Label8)
        Me.GroupControl1.Controls.Add(Me.kesinti)
        Me.GroupControl1.Controls.Add(Me.komisyon)
        Me.GroupControl1.Controls.Add(Me.Label3)
        Me.GroupControl1.Controls.Add(Me.Label4)
        Me.GroupControl1.Controls.Add(Me.oran)
        Me.GroupControl1.Controls.Add(Me.vade)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Location = New System.Drawing.Point(1, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(389, 365)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Factoring Calculate"
        '
        'tutar
        '
        Me.tutar.EditValue = "1"
        Me.tutar.Location = New System.Drawing.Point(165, 23)
        Me.tutar.Name = "tutar"
        Me.tutar.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tutar.Size = New System.Drawing.Size(100, 20)
        Me.tutar.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(69, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 15)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "tutar"
        '
        'toplam
        '
        Me.toplam.EditValue = "10"
        Me.toplam.Location = New System.Drawing.Point(165, 205)
        Me.toplam.Name = "toplam"
        Me.toplam.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.toplam.Size = New System.Drawing.Size(100, 20)
        Me.toplam.TabIndex = 11
        '
        'komtutar
        '
        Me.komtutar.EditValue = "20"
        Me.komtutar.Location = New System.Drawing.Point(165, 177)
        Me.komtutar.Name = "komtutar"
        Me.komtutar.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.komtutar.Size = New System.Drawing.Size(100, 20)
        Me.komtutar.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(69, 208)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 15)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Toplam Tutar"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(69, 180)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(89, 15)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Komisyon Tutarı"
        '
        'kesinti
        '
        Me.kesinti.EditValue = "50"
        Me.kesinti.Location = New System.Drawing.Point(165, 120)
        Me.kesinti.Name = "kesinti"
        Me.kesinti.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.kesinti.Size = New System.Drawing.Size(100, 20)
        Me.kesinti.TabIndex = 7
        '
        'komisyon
        '
        Me.komisyon.EditValue = "1"
        Me.komisyon.Location = New System.Drawing.Point(165, 151)
        Me.komisyon.Name = "komisyon"
        Me.komisyon.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.komisyon.Size = New System.Drawing.Size(100, 20)
        Me.komisyon.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(69, 123)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Kesinti"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(69, 154)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 15)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Komisyon"
        '
        'oran
        '
        Me.oran.EditValue = "1,20"
        Me.oran.Location = New System.Drawing.Point(165, 94)
        Me.oran.Name = "oran"
        Me.oran.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.oran.Size = New System.Drawing.Size(100, 20)
        Me.oran.TabIndex = 3
        '
        'vade
        '
        Me.vade.EditValue = "1"
        Me.vade.Location = New System.Drawing.Point(165, 66)
        Me.vade.Name = "vade"
        Me.vade.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.vade.Size = New System.Drawing.Size(100, 20)
        Me.vade.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(69, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Oran"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(69, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Vade gün"
        '
        'frmfactoringcal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(392, 367)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "frmfactoringcal"
        Me.Text = "frmfactoringcal"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.tutar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.toplam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.komtutar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.kesinti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.komisyon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oran.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.vade.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents toplam As DevExpress.XtraEditors.TextEdit
    Friend WithEvents komtutar As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents kesinti As DevExpress.XtraEditors.TextEdit
    Friend WithEvents komisyon As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents oran As DevExpress.XtraEditors.TextEdit
    Friend WithEvents vade As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tutar As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label5 As Label
End Class
