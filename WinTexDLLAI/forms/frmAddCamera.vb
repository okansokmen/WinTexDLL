Imports DirectX.Capture

Public Class frmAddCamera
    Inherits System.Windows.Forms.Form

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub init()
        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Dim j As Short
        Dim f As Filter

        'Add to cboCamaras available cameras
        cboCamaras.Items.Clear()
        For j = 0 To Dispositivos.VideoInputDevices.Count - 1
            f = Dispositivos.VideoInputDevices(j)
            cboCamaras.Items.Add(f.Name)
        Next

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
    Friend WithEvents cboCamaras As System.Windows.Forms.ComboBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddCamera))
        Me.cboCamaras = New System.Windows.Forms.ComboBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cboCamaras
        '
        Me.cboCamaras.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboCamaras.Location = New System.Drawing.Point(12, 16)
        Me.cboCamaras.Name = "cboCamaras"
        Me.cboCamaras.Size = New System.Drawing.Size(240, 21)
        Me.cboCamaras.TabIndex = 4
        Me.cboCamaras.Text = "Lütfen bir kamera seçiniz"
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.Color.Red
        Me.cmdCancel.Location = New System.Drawing.Point(131, 56)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(118, 25)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Çýkýþ"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.cmdOK.ForeColor = System.Drawing.Color.Green
        Me.cmdOK.Location = New System.Drawing.Point(12, 56)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(118, 25)
        Me.cmdOK.TabIndex = 3
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'frmAddCamera
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(264, 93)
        Me.Controls.Add(Me.cboCamaras)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddCamera"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kamera Seçimi"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
        Dim IdVentana As String = ""

        If cboCamaras.SelectedItem = Nothing Then
            MsgBox("Select an available camera.", MsgBoxStyle.Exclamation, "Error")
            oCamera.nCameraNo = -1
            Exit Sub
        End If

        CaptureInformation.Camera = Dispositivos.VideoInputDevices(cboCamaras.SelectedIndex)
        CaptureInformation.CaptureInfo = New Capture(CaptureInformation.Camera, Nothing)

        oCamera.nCameraNo = cboCamaras.SelectedIndex

        Me.Dispose()
    End Sub

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Me.Dispose()
    End Sub
End Class
