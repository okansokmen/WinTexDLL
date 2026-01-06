<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSTIDashboardViewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSTIDashboardViewer))
        Me.StiDashboardViewerControl1 = New Stimulsoft.Dashboard.Viewer.StiDashboardViewerControl()
        Me.SuspendLayout()
        '
        'StiDashboardViewerControl1
        '
        Me.StiDashboardViewerControl1.BackColor = System.Drawing.Color.White
        Me.StiDashboardViewerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StiDashboardViewerControl1.ElementFullScreen.IsActivated = False
        Me.StiDashboardViewerControl1.Location = New System.Drawing.Point(0, 0)
        Me.StiDashboardViewerControl1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.StiDashboardViewerControl1.Name = "StiDashboardViewerControl1"
        Me.StiDashboardViewerControl1.Size = New System.Drawing.Size(520, 337)
        Me.StiDashboardViewerControl1.TabIndex = 0
        Me.StiDashboardViewerControl1.ViewerFullScreen.IsActivated = False
        '
        'frmSTIDashboardViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 337)
        Me.Controls.Add(Me.StiDashboardViewerControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSTIDashboardViewer"
        Me.Text = "WinTex Stimulsoft Dashboard Viewer"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents StiDashboardViewerControl1 As Stimulsoft.Dashboard.Viewer.StiDashboardViewerControl
End Class
