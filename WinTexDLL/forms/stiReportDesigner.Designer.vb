<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class stiReportDesigner
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
        Me.StiDesignerControl1 = New Stimulsoft.Report.Design.StiDesignerControl()
        Me.SuspendLayout()
        '
        'StiDesignerControl1
        '
        Me.StiDesignerControl1.AllowDrop = True
        Me.StiDesignerControl1.BackColor = System.Drawing.Color.White
        Me.StiDesignerControl1.Location = New System.Drawing.Point(133, 12)
        Me.StiDesignerControl1.Name = "StiDesignerControl1"
        Me.StiDesignerControl1.Size = New System.Drawing.Size(552, 405)
        Me.StiDesignerControl1.TabIndex = 0
        '
        'stiReportDesigner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1335, 406)
        Me.Controls.Add(Me.StiDesignerControl1)
        Me.Name = "stiReportDesigner"
        Me.Text = "stiReportDesigner"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents StiDesignerControl1 As Stimulsoft.Report.Design.StiDesignerControl
End Class
