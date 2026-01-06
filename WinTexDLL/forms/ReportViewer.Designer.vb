<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportViewer
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportViewer))
        Me.StiRibbonViewerControl1 = New Stimulsoft.Report.Viewer.StiRibbonViewerControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'StiRibbonViewerControl1
        '
        Me.StiRibbonViewerControl1.AllowDrop = True
        Me.StiRibbonViewerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StiRibbonViewerControl1.Location = New System.Drawing.Point(0, 0)
        Me.StiRibbonViewerControl1.Name = "StiRibbonViewerControl1"
        Me.StiRibbonViewerControl1.PageViewMode = Stimulsoft.Report.Viewer.StiPageViewMode.Continuous
        Me.StiRibbonViewerControl1.Report = Nothing
        Me.StiRibbonViewerControl1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StiRibbonViewerControl1.ShowToolbar = False
        Me.StiRibbonViewerControl1.ShowZoom = True
        Me.StiRibbonViewerControl1.Size = New System.Drawing.Size(888, 320)
        Me.StiRibbonViewerControl1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.StiRibbonViewerControl1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(888, 320)
        Me.Panel2.TabIndex = 3
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'ReportViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(888, 320)
        Me.Controls.Add(Me.Panel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ReportViewer"
        Me.Text = "Stimulus Raporlama Aracı"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StiRibbonViewerControl1 As Stimulsoft.Report.Viewer.StiRibbonViewerControl
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Timer1 As Timer
End Class
