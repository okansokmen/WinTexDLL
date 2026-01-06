Imports System
Imports System.Diagnostics
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DirectShowLib
Imports System.Runtime.InteropServices.ComTypes
Imports System.Drawing.Imaging

Namespace WinTexCam
    Public Class WTCam
        Inherits System.Windows.Forms.Form

        Enum PlayState
            Stopped
            Paused
            Running
            Init
        End Enum

        Dim CurrentState As PlayState = PlayState.Stopped

        Dim D As Integer = Convert.ToInt32("0X8000", 16)
        Public WM_GRAPHNOTIFY As Integer = D + 1

        Dim VideoWindow As IVideoWindow = Nothing
        Dim MediaControl As IMediaControl = Nothing
        Dim MediaEventEx As IMediaEventEx = Nothing
        Dim GraphBuilder As IGraphBuilder = Nothing
        Dim CaptureGraphBuilder As ICaptureGraphBuilder2 = Nothing

        Dim rot As DsROTEntry = Nothing
        Dim cFileName As String = ""

        Public Sub init(cFile As String)
            cFileName = cFile
            Me.ShowDialog()
        End Sub

        Private Sub WTCam_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            InitializeComponent()
            Me.AutoScaleBaseSize = New System.Drawing.Size(4, 3)
            CaptureVideo()
        End Sub

        Private Sub InitializeComponent()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Button2 = New System.Windows.Forms.Button()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.Controls.Add(Me.Button2)
            Me.Panel1.Controls.Add(Me.Button1)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel1.Location = New System.Drawing.Point(0, 277)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(346, 43)
            Me.Panel1.TabIndex = 0
            '
            'Button2
            '
            Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            Me.Button2.ForeColor = System.Drawing.Color.Red
            Me.Button2.Location = New System.Drawing.Point(176, 10)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(75, 23)
            Me.Button2.TabIndex = 1
            Me.Button2.Text = "Çýkýþ"
            Me.Button2.UseVisualStyleBackColor = True
            '
            'Button1
            '
            Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            Me.Button1.ForeColor = System.Drawing.Color.Green
            Me.Button1.Location = New System.Drawing.Point(95, 10)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(75, 23)
            Me.Button1.TabIndex = 0
            Me.Button1.Text = "OK"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'Panel2
            '
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel2.Location = New System.Drawing.Point(0, 0)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(346, 277)
            Me.Panel2.TabIndex = 1
            '
            'WTCam
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(346, 320)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel1)
            Me.Name = "WTCam"
            Me.Text = "WinTex Kamera"
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Private Sub CaptureVideo()
            Dim hr As Integer = 0
            Dim sourceFilter As IBaseFilter = Nothing
            Try
                GetInterfaces()
                ' Specifies filter graph "graphbuilder" for the capture graph builder "captureGraphBuilder" to use.
                hr = Me.CaptureGraphBuilder.SetFiltergraph(Me.GraphBuilder)
                Debug.WriteLine("Attach the filter graph to the capture graph : " & DsError.GetErrorText(hr))
                DsError.ThrowExceptionForHR(hr)

                sourceFilter = FindCaptureDevice()

                hr = Me.GraphBuilder.AddFilter(sourceFilter, "Video Capture")
                Debug.WriteLine("Add capture filter to our graph : " & DsError.GetErrorText(hr))
                DsError.ThrowExceptionForHR(hr)

                hr = Me.CaptureGraphBuilder.RenderStream(PinCategory.Preview, MediaType.Video, sourceFilter, Nothing, Nothing)
                Debug.WriteLine("Render the preview pin on the video capture filter : " & DsError.GetErrorText(hr))
                DsError.ThrowExceptionForHR(hr)

                Marshal.ReleaseComObject(sourceFilter)

                SetupVideoWindow()

                rot = New DsROTEntry(Me.GraphBuilder)

                hr = Me.MediaControl.Run()
                Debug.WriteLine("Start previewing video data : " & DsError.GetErrorText(hr))
                DsError.ThrowExceptionForHR(hr)

                Me.CurrentState = PlayState.Running
                Debug.WriteLine("The currentstate : " & Me.CurrentState.ToString)

            Catch ex As Exception
                MessageBox.Show("An unrecoverable error has occurred.With error : " & ex.ToString)
            End Try
        End Sub

        Private Sub GetInterfaces()
            Dim hr As Integer = 0
            Me.GraphBuilder = CType(New FilterGraph, IGraphBuilder)
            Me.CaptureGraphBuilder = CType(New CaptureGraphBuilder2, ICaptureGraphBuilder2)
            Me.MediaControl = CType(Me.GraphBuilder, IMediaControl)
            Me.VideoWindow = CType(Me.GraphBuilder, IVideoWindow)
            Me.MediaEventEx = CType(Me.GraphBuilder, IMediaEventEx)
            ' This method designates a window as the recipient of messages generated by or sent to the current DirectShow object
            hr = Me.MediaEventEx.SetNotifyWindow(Me.Handle, WM_GRAPHNOTIFY, IntPtr.Zero)
            ' ThrowExceptionForHR is a wrapper for Marshal.ThrowExceptionForHR, 
            ' but additionally provides descriptions for any DirectShow specific error messages.
            ' If the hr value is not a fatal error, no exception will be thrown:
            DsError.ThrowExceptionForHR(hr)
            Debug.WriteLine("I started Sub Get interfaces , the result is : " & DsError.GetErrorText(hr))
        End Sub

        Public Function FindCaptureDevice() As IBaseFilter
            Debug.WriteLine("Start the Sub FindCaptureDevice")
            Dim hr As Integer = 0
            Dim classEnum As IEnumMoniker = Nothing
            Dim moniker As IMoniker() = New IMoniker(0) {}
            Dim source As Object = Nothing
            Dim devEnum As ICreateDevEnum = CType(New CreateDevEnum, ICreateDevEnum)
            hr = devEnum.CreateClassEnumerator(FilterCategory.VideoInputDevice, classEnum, 0)
            Debug.WriteLine("Create an enumerator for the video capture devices : " & DsError.GetErrorText(hr))
            DsError.ThrowExceptionForHR(hr)
            Marshal.ReleaseComObject(devEnum)
            If classEnum Is Nothing Then
                Throw New ApplicationException("No video capture device was detected.\r\n\r\n" & _
                               "This sample requires a video capture device, such as a USB WebCam,\r\n" & _
                               "to be installed and working properly.  The sample will now close.")
            End If
            If classEnum.Next(moniker.Length, moniker, IntPtr.Zero) = 0 Then
                Dim iid As Guid = GetType(IBaseFilter).GUID
                moniker(0).BindToObject(Nothing, Nothing, iid, source)
            Else
                Throw New ApplicationException("Unable to access video capture device!")
            End If
            Marshal.ReleaseComObject(moniker(0))
            Marshal.ReleaseComObject(classEnum)
            Return CType(source, IBaseFilter)
        End Function

        Public Sub SetupVideoWindow()
            Dim hr As Integer = 0
            'set the video window to be a child of the main window
            'putowner : Sets the owning parent window for the video playback window. 
            'hr = Me.VideoWindow.put_Owner(Me.Handle)
            hr = Me.VideoWindow.put_Owner(Me.Panel2.Handle)
            DsError.ThrowExceptionForHR(hr)

            hr = Me.VideoWindow.put_WindowStyle(WindowStyle.Child Or WindowStyle.ClipChildren)
            DsError.ThrowExceptionForHR(hr)
            'Use helper function to position video window in client rect of main application window
            ResizeVideoWindow()
            'Make the video window visible, now that it is properly positioned
            'put_visible : This method changes the visibility of the video window. 
            hr = Me.VideoWindow.put_Visible(OABool.True)
            DsError.ThrowExceptionForHR(hr)
        End Sub

        Protected Overloads Sub WndProc(ByRef m As Message)
            Select Case m.Msg
                Case WM_GRAPHNOTIFY
                    HandleGraphEvent()
            End Select
            If Not (Me.VideoWindow Is Nothing) Then
                Me.VideoWindow.NotifyOwnerMessage(m.HWnd, m.Msg, m.WParam.ToInt32, m.LParam.ToInt32)
            End If
            MyBase.WndProc(m)
        End Sub

        Public Sub HandleGraphEvent()
            Dim hr As Integer = 0
            Dim evCode As EventCode
            Dim evParam1 As Integer
            Dim evParam2 As Integer
            If Me.MediaEventEx Is Nothing Then
                Return
            End If
            While Me.MediaEventEx.GetEvent(evCode, evParam1, evParam2, 0) = 0
                '// Free event parameters to prevent memory leaks associated with
                '// event parameter data.  While this application is not interested
                '// in the received events, applications should always process them.
                hr = Me.MediaEventEx.FreeEventParams(evCode, evParam1, evParam2)
                DsError.ThrowExceptionForHR(hr)

                '// Insert event processing code here, if desired
            End While
        End Sub

        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                '// Stop capturing and release interfaces
                closeinterfaces()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Public Sub closeinterfaces()
            '//stop previewing data
            If Not (Me.MediaControl Is Nothing) Then
                Me.MediaControl.StopWhenReady()
            End If

            Me.CurrentState = PlayState.Stopped
            '//stop recieving events
            If Not (Me.MediaEventEx Is Nothing) Then
                Me.MediaEventEx.SetNotifyWindow(IntPtr.Zero, WM_GRAPHNOTIFY, IntPtr.Zero)
            End If
            '// Relinquish ownership (IMPORTANT!) of the video window.
            '// Failing to call put_Owner can lead to assert failures within
            '// the video renderer, as it still assumes that it has a valid
            '// parent window.
            If Not (Me.VideoWindow Is Nothing) Then
                Me.VideoWindow.put_Visible(OABool.False)
                Me.VideoWindow.put_Owner(IntPtr.Zero)
            End If
            ' // Remove filter graph from the running object table
            If Not (rot Is Nothing) Then
                rot.Dispose()
                rot = Nothing
            End If
            '// Release DirectShow interfaces
            Marshal.ReleaseComObject(Me.MediaControl) : Me.MediaControl = Nothing
            Marshal.ReleaseComObject(Me.MediaEventEx) : Me.MediaEventEx = Nothing
            Marshal.ReleaseComObject(Me.VideoWindow) : Me.VideoWindow = Nothing
            Marshal.ReleaseComObject(Me.GraphBuilder) : Me.GraphBuilder = Nothing
            Marshal.ReleaseComObject(Me.CaptureGraphBuilder) : Me.CaptureGraphBuilder = Nothing
        End Sub

        Private Sub WTCam_Resize1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
            If Me.WindowState = FormWindowState.Minimized Then
                ChangePreviewState(False)
            End If
            If Me.WindowState = FormWindowState.Normal Then
                ChangePreviewState(True)
            End If
            ResizeVideoWindow()
        End Sub

        Public Sub ChangePreviewState(ByVal showVideo As Boolean)
            Dim hr As Integer = 0
            '// If the media control interface isn't ready, don't call it
            If Me.MediaControl Is Nothing Then
                Debug.WriteLine("MediaControl is nothing")
                Return
            End If
            If showVideo = True Then
                If Not (Me.CurrentState = PlayState.Running) Then
                    Debug.WriteLine("Start previewing video data")
                    hr = Me.MediaControl.Run
                    Me.CurrentState = PlayState.Running
                End If
            Else
                Debug.WriteLine("Stop previewing video data")
                hr = Me.MediaControl.StopWhenReady
                Me.CurrentState = PlayState.Stopped
            End If
        End Sub

        Public Sub ResizeVideoWindow()
            'Resize the video preview window to match owner window size
            'left , top , width , height
            If Not (Me.VideoWindow Is Nothing) Then 'if the videopreview is not nothing
                'Me.VideoWindow.SetWindowPosition(0, 0, Me.Width, Me.ClientSize.Height)
                Me.VideoWindow.SetWindowPosition(0, 0, Me.Panel2.ClientSize.Width, Me.Panel2.ClientSize.Height)
            End If
        End Sub

        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Friend WithEvents Button2 As System.Windows.Forms.Button

        Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
            ' OK
            GetImageShot()
            'Me.Close()
        End Sub

        Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
            ' çýkýþ
            Me.Close()
        End Sub

        Private Sub WTCam_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
            closeinterfaces()
        End Sub

        Private Sub GetImageShot()
            Try
                MediaControl.Pause()

                Dim oGraphics As Graphics
                Dim bmp As Bitmap
                Dim pt As Point = Panel2.PointToScreen(New Point(0, 0))

                bmp = New Bitmap(Panel2.Bounds.Width, Panel2.Bounds.Height, PixelFormat.Format32bppArgb)
                oGraphics = Graphics.FromImage(bmp)
                oGraphics.CopyFromScreen(pt.X, pt.Y, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy)

                'bmp = New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb)
                'oGraphics = Graphics.FromImage(bmp)
                'oGraphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy)

                If cFileName.Trim <> "" Then
                    DestroyFile(cFileName)
                    bmp.Save(cFileName, Imaging.ImageFormat.Jpeg)
                End If

            Catch ex As Exception
                ErrDisp("GetImageShot : " + ex.Message, Me.Name)
            End Try
        End Sub
    End Class
End Namespace
