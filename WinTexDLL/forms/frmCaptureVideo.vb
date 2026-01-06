Imports DirectX.Capture

Public Class frmCaptureVideo
    Inherits System.Windows.Forms.Form

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub init()
        CaptureInformation.CaptureInfo.PreviewWindow = Me.videoBoard
        'Define RefreshImage as event handler of FrameCaptureComplete
        AddHandler CaptureInformation.CaptureInfo.FrameCaptureComplete, AddressOf RefreshImage

        Me.ShowDialog()
    End Sub

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents videoBoard As System.Windows.Forms.Panel
    Friend WithEvents cmdFrame As System.Windows.Forms.Button
    Friend WithEvents cmdStart As System.Windows.Forms.Button
    Friend WithEvents cmdStop As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents pcbFrame As System.Windows.Forms.PictureBox

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCaptureVideo))
        Me.videoBoard = New System.Windows.Forms.Panel()
        Me.cmdFrame = New System.Windows.Forms.Button()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.pcbFrame = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.pcbFrame, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'videoBoard
        '
        Me.videoBoard.BackColor = System.Drawing.Color.Black
        Me.videoBoard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.videoBoard.Location = New System.Drawing.Point(2, 0)
        Me.videoBoard.Name = "videoBoard"
        Me.videoBoard.Size = New System.Drawing.Size(320, 240)
        Me.videoBoard.TabIndex = 0
        '
        'cmdFrame
        '
        Me.cmdFrame.BackColor = System.Drawing.Color.Silver
        Me.cmdFrame.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.cmdFrame.ForeColor = System.Drawing.Color.Green
        Me.cmdFrame.Image = Global.WinTexDLL.My.Resources.Resources._photo
        Me.cmdFrame.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdFrame.Location = New System.Drawing.Point(334, 246)
        Me.cmdFrame.Name = "cmdFrame"
        Me.cmdFrame.Size = New System.Drawing.Size(160, 56)
        Me.cmdFrame.TabIndex = 1
        Me.cmdFrame.Text = "Resim Çek"
        Me.cmdFrame.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdFrame.UseVisualStyleBackColor = False
        '
        'cmdStart
        '
        Me.cmdStart.BackColor = System.Drawing.Color.Silver
        Me.cmdStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.cmdStart.ForeColor = System.Drawing.Color.Green
        Me.cmdStart.Image = Global.WinTexDLL.My.Resources.Resources._video
        Me.cmdStart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdStart.Location = New System.Drawing.Point(2, 246)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(160, 56)
        Me.cmdStart.TabIndex = 2
        Me.cmdStart.Text = "Video Çekmeye Baþla"
        Me.cmdStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdStart.UseVisualStyleBackColor = False
        '
        'cmdStop
        '
        Me.cmdStop.BackColor = System.Drawing.Color.Silver
        Me.cmdStop.Enabled = False
        Me.cmdStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.cmdStop.ForeColor = System.Drawing.Color.Red
        Me.cmdStop.Image = Global.WinTexDLL.My.Resources.Resources._off
        Me.cmdStop.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdStop.Location = New System.Drawing.Point(162, 246)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(160, 56)
        Me.cmdStop.TabIndex = 3
        Me.cmdStop.Text = "Video Çekmeyi Bitir"
        Me.cmdStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdStop.UseVisualStyleBackColor = False
        '
        'pcbFrame
        '
        Me.pcbFrame.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pcbFrame.BackColor = System.Drawing.Color.Black
        Me.pcbFrame.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pcbFrame.Location = New System.Drawing.Point(334, 0)
        Me.pcbFrame.Name = "pcbFrame"
        Me.pcbFrame.Size = New System.Drawing.Size(320, 240)
        Me.pcbFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pcbFrame.TabIndex = 4
        Me.pcbFrame.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Silver
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Red
        Me.Button1.Image = Global.WinTexDLL.My.Resources.Resources._off
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button1.Location = New System.Drawing.Point(494, 246)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(160, 56)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Çýkýþ"
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button1.UseVisualStyleBackColor = False
        '
        'frmCaptureVideo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(656, 305)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.pcbFrame)
        Me.Controls.Add(Me.cmdStop)
        Me.Controls.Add(Me.cmdStart)
        Me.Controls.Add(Me.cmdFrame)
        Me.Controls.Add(Me.videoBoard)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCaptureVideo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Video/Resim Yakalama Penceresi"
        CType(Me.pcbFrame, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub RefreshImage(ByVal Frame As System.Windows.Forms.PictureBox)

        Dim cFileName As String = oCamera.cFileName + ".jpg"

        CaptureInformation.PathVideo = cFileName
        CaptureInformation.CaptureInfo.Filename = cFileName

        Me.pcbFrame.Image = Frame.Image

        DestroyFile(cFileName)
        Me.pcbFrame.Image.Save(cFileName, System.Drawing.Imaging.ImageFormat.Jpeg)

        Me.pcbFrame.Refresh()
    End Sub

    Private Sub cmdFrame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFrame.Click
        CaptureInformation.CaptureInfo.CaptureFrame()
    End Sub

    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click

        Dim cFileName As String = oCamera.cFileName + ".avi"

        DestroyFile(cFileName)

        CaptureInformation.PathVideo = cFileName
        CaptureInformation.CaptureInfo.Filename =cFileName

        'CaptureInformation.CaptureInfo.RenderPreview()
        CaptureInformation.CaptureInfo.Start()

        cmdStart.Enabled = False
        cmdStop.Enabled = True
    End Sub

    Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        CaptureInformation.CaptureInfo.Stop()
        cmdStart.Enabled = True
        cmdStop.Enabled = False
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub MW_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ' önce bi stopla
        CaptureInformation.CaptureInfo.Stop()
        ' video penceresini baþlat
        CaptureInformation.CaptureInfo.RenderPreview()
        ' resim penceresine ilk pozu at
        CaptureInformation.CaptureInfo.CaptureFrame()
    End Sub
End Class
