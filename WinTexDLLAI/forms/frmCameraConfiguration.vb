Imports DirectX.Capture
Imports System.Drawing
Imports System.Windows.Forms

Public Class frmCameraConfiguration
    Inherits System.Windows.Forms.Form

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub init()

        Dim Tamano As Size = CaptureInformation.CaptureInfo.FrameSize ' New Size(160, 120) 
        Dim Rate As Integer = CaptureInformation.CaptureInfo.FrameRate * 1000 '  5000
        Dim list As ListBox = Nothing
        Dim i As Integer
        Dim fAux As Filter
        Dim p As PropertyPage

        'Add available capture sizes
        Me.cmbTam.Items.Clear()
        Me.cmbTam.Items.Add("160x120")
        Me.cmbTam.Items.Add("176x144")
        Me.cmbTam.Items.Add("320x240")
        Me.cmbTam.Items.Add("352x288")
        Me.cmbTam.Items.Add("640x480")
        If (Tamano.Equals(New Size(160, 120))) Then
            Me.cmbTam.Text = "160x120"
        End If
        If (Tamano.Equals(New Size(176, 144))) Then
            Me.cmbTam.Text = "176x144"
        End If
        If (Tamano.Equals(New Size(320, 240))) Then
            Me.cmbTam.Text = "320x240"
        End If
        If (Tamano.Equals(New Size(352, 288))) Then
            Me.cmbTam.Text = "352x288"
        End If
        If (Tamano.Equals(New Size(640, 480))) Then
            Me.cmbTam.Text = "640x480"
        End If

        'Add available capture frames per second
        Me.cmbFPS.Items.Clear()
        Me.cmbFPS.Items.Add("5 fps")
        Me.cmbFPS.Items.Add("10 fps")
        Me.cmbFPS.Items.Add("15 fps")
        Me.cmbFPS.Items.Add("20 fps")
        Me.cmbFPS.Items.Add("25 fps (PAL)")
        Me.cmbFPS.Items.Add("30 fps")
        Me.cmbFPS.Items.Add("60 fps")
        If (Rate = 5000) Then
            Me.cmbFPS.Text = "5 fps"
        End If
        If (Rate = 10000) Then
            Me.cmbFPS.Text = "10 fps"
        End If
        If (Rate = 15000) Then
            Me.cmbFPS.Text = "15 fps"
        End If
        If (Rate = 20000) Then
            Me.cmbFPS.Text = "20 fps"
        End If
        If (Rate = 25000) Then
            Me.cmbFPS.Text = "25 fps (PAL)"
        End If
        If (Rate = 30000) Then
            Me.cmbFPS.Text = "30 fps"
        End If
        If (Rate = 60000) Then
            Me.cmbFPS.Text = "60 fps"
        End If

        'Add the possible compressors to use in capturing
        Me.cmbCompress.Items.Clear()
        Me.cmbCompress.Items.Add("Without Compressor")
        If (CaptureInformation.CaptureInfo.VideoCompressor Is Nothing) Then
            Me.cmbCompress.Text = "Without Compressor"
        End If
        For i = 0 To Dispositivos.VideoCompressors.Count - 1
            fAux = Dispositivos.VideoCompressors(i)
            Me.cmbCompress.Items.Add(fAux.Name)
            If (CaptureInformation.CaptureInfo.VideoCompressor Is fAux) Then
                Me.cmbCompress.Text = fAux.Name
            End If
        Next

        'Driver Configuration

        For i = 0 To CaptureInformation.CaptureInfo.PropertyPages.Count - 1
            p = CaptureInformation.CaptureInfo.PropertyPages(i)
            Me.lboxDriver.Items.Add(p.Name + "...")
        Next

        Me.txtPathVideo.Text = oCamera.cFileName

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
    Friend WithEvents lblFPS As System.Windows.Forms.Label
    Friend WithEvents lblTam As System.Windows.Forms.Label
    Friend WithEvents lblCompress As System.Windows.Forms.Label
    Friend WithEvents cmbFPS As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTam As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCompress As System.Windows.Forms.ComboBox
    Public WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents lboxDriver As System.Windows.Forms.ListBox
    Friend WithEvents lblDriver As System.Windows.Forms.Label
    Public WithEvents CanButton As System.Windows.Forms.Button
    Friend WithEvents txtPathVideo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCameraConfiguration))
        Me.lblFPS = New System.Windows.Forms.Label()
        Me.lblTam = New System.Windows.Forms.Label()
        Me.lblCompress = New System.Windows.Forms.Label()
        Me.cmbFPS = New System.Windows.Forms.ComboBox()
        Me.cmbTam = New System.Windows.Forms.ComboBox()
        Me.cmbCompress = New System.Windows.Forms.ComboBox()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.CanButton = New System.Windows.Forms.Button()
        Me.lboxDriver = New System.Windows.Forms.ListBox()
        Me.lblDriver = New System.Windows.Forms.Label()
        Me.txtPathVideo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblFPS
        '
        Me.lblFPS.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFPS.Location = New System.Drawing.Point(28, 16)
        Me.lblFPS.Name = "lblFPS"
        Me.lblFPS.Size = New System.Drawing.Size(240, 16)
        Me.lblFPS.TabIndex = 1
        Me.lblFPS.Text = "- Saniyede yakalanan resim /  Frames per second"
        '
        'lblTam
        '
        Me.lblTam.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTam.Location = New System.Drawing.Point(28, 75)
        Me.lblTam.Name = "lblTam"
        Me.lblTam.Size = New System.Drawing.Size(240, 16)
        Me.lblTam.TabIndex = 2
        Me.lblTam.Text = "- Resim boyutlarý / Image size"
        '
        'lblCompress
        '
        Me.lblCompress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCompress.Location = New System.Drawing.Point(28, 134)
        Me.lblCompress.Name = "lblCompress"
        Me.lblCompress.Size = New System.Drawing.Size(240, 16)
        Me.lblCompress.TabIndex = 3
        Me.lblCompress.Text = "- Sýkýþtýrýcýlar / Compressors"
        '
        'cmbFPS
        '
        Me.cmbFPS.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbFPS.Location = New System.Drawing.Point(56, 43)
        Me.cmbFPS.Name = "cmbFPS"
        Me.cmbFPS.Size = New System.Drawing.Size(212, 21)
        Me.cmbFPS.TabIndex = 4
        '
        'cmbTam
        '
        Me.cmbTam.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbTam.Location = New System.Drawing.Point(56, 102)
        Me.cmbTam.Name = "cmbTam"
        Me.cmbTam.Size = New System.Drawing.Size(212, 21)
        Me.cmbTam.TabIndex = 5
        '
        'cmbCompress
        '
        Me.cmbCompress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCompress.Location = New System.Drawing.Point(56, 161)
        Me.cmbCompress.Name = "cmbCompress"
        Me.cmbCompress.Size = New System.Drawing.Size(212, 21)
        Me.cmbCompress.TabIndex = 6
        '
        'OKButton
        '
        Me.OKButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OKButton.BackColor = System.Drawing.SystemColors.Control
        Me.OKButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.OKButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.OKButton.ForeColor = System.Drawing.Color.Green
        Me.OKButton.Location = New System.Drawing.Point(31, 356)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OKButton.Size = New System.Drawing.Size(116, 25)
        Me.OKButton.TabIndex = 7
        Me.OKButton.Text = "OK"
        Me.OKButton.UseVisualStyleBackColor = False
        '
        'CanButton
        '
        Me.CanButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CanButton.BackColor = System.Drawing.SystemColors.Control
        Me.CanButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.CanButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CanButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.CanButton.ForeColor = System.Drawing.Color.Red
        Me.CanButton.Location = New System.Drawing.Point(152, 356)
        Me.CanButton.Name = "CanButton"
        Me.CanButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CanButton.Size = New System.Drawing.Size(116, 25)
        Me.CanButton.TabIndex = 8
        Me.CanButton.Text = "Çýkýþ"
        Me.CanButton.UseVisualStyleBackColor = False
        '
        'lboxDriver
        '
        Me.lboxDriver.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lboxDriver.Location = New System.Drawing.Point(56, 220)
        Me.lboxDriver.Name = "lboxDriver"
        Me.lboxDriver.Size = New System.Drawing.Size(212, 43)
        Me.lboxDriver.TabIndex = 9
        '
        'lblDriver
        '
        Me.lblDriver.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDriver.Location = New System.Drawing.Point(28, 193)
        Me.lblDriver.Name = "lblDriver"
        Me.lblDriver.Size = New System.Drawing.Size(240, 16)
        Me.lblDriver.TabIndex = 10
        Me.lblDriver.Text = "- Sürücü / Driver configuration"
        '
        'txtPathVideo
        '
        Me.txtPathVideo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPathVideo.Location = New System.Drawing.Point(56, 312)
        Me.txtPathVideo.Name = "txtPathVideo"
        Me.txtPathVideo.Size = New System.Drawing.Size(212, 20)
        Me.txtPathVideo.TabIndex = 12
        Me.txtPathVideo.Text = "c:\Capture.avi"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Location = New System.Drawing.Point(28, 280)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(240, 16)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "- Dosya adý / Captured File Name"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmCameraConfiguration
        '
        Me.AcceptButton = Me.OKButton
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.CancelButton = Me.CanButton
        Me.ClientSize = New System.Drawing.Size(292, 399)
        Me.Controls.Add(Me.txtPathVideo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblDriver)
        Me.Controls.Add(Me.lboxDriver)
        Me.Controls.Add(Me.CanButton)
        Me.Controls.Add(Me.OKButton)
        Me.Controls.Add(Me.cmbCompress)
        Me.Controls.Add(Me.cmbTam)
        Me.Controls.Add(Me.cmbFPS)
        Me.Controls.Add(Me.lblCompress)
        Me.Controls.Add(Me.lblTam)
        Me.Controls.Add(Me.lblFPS)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCameraConfiguration"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kamera Ayarlarý"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub lboxDriver_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lboxDriver.MouseDown
        CaptureInformation.CaptureInfo.PropertyPages(Me.lboxDriver.SelectedIndex).Show(frmCameraConfiguration.ActiveForm)
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OKButton.Click

        Dim s() As String
        Dim z() As String

        ' the image size
        s = Me.cmbTam.Text.Split("x")
        ' Change the number of frames per second
        z = Me.cmbFPS.Text.Split(" ")
        ' load 
        oCamera.nVideoCompressor = CaptureInformation.ConfWindow.cmbCompress.Items.IndexOf(Me.cmbCompress.Text)
        oCamera.nWidth = Val(s(0))
        oCamera.nHeight = Val(s(1))
        oCamera.nFrameRate = Val(z(0))
        oCamera.cFileName = txtPathVideo.Text.Trim

        Me.Close()
    End Sub

    Private Sub CanButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CanButton.Click
        Me.Close()
    End Sub

    Private Sub frmCameraConfiguration_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
