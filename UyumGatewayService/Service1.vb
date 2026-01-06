Imports System
Imports System.Data
Imports System.ServiceProcess
Imports System.Configuration
Imports System.Timers
Imports System.Diagnostics
Imports System.IO
Imports System.Threading
Imports Microsoft.VisualBasic

Public Class Service1

    Dim lProcessing As Boolean = False

    Public oTimer As System.Timers.Timer
    Public cServisTipi As String = ConfigurationManager.AppSettings("SERVISTIPI")       ' Servis Tipi (NORMAL / WEB)

    Public Mn As Double = Convert.ToDouble(ConfigurationManager.AppSettings("Mn"))
    Public Hr As Double = Convert.ToDouble(ConfigurationManager.AppSettings("Hr"))

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.CanHandlePowerEvent = True
        Me.CanHandleSessionChangeEvent = True
        Me.CanPauseAndContinue = True
        Me.CanShutdown = True
        Me.CanStop = True

        oEventLog = New System.Diagnostics.EventLog

        If Not System.Diagnostics.EventLog.SourceExists("WintexGatewayService") Then
            System.Diagnostics.EventLog.CreateEventSource("WintexGatewayService", "WintexGatewayLog")
        End If
        oEventLog.Source = "WintexGatewayService"
        oEventLog.Log = "WintexGatewayLog"
    End Sub

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.

        Try
            Dim nMinutes As Integer = 0

#If DEBUG Then
            System.Diagnostics.Debugger.Launch()
            Do While Not Debugger.IsAttached
                Threading.Thread.Sleep(1000)
            Loop
#End If

            cSrvTipi = cServisTipi
            cServiceName = ServiceName
            nMinutes = CInt((Hr * 3600000) + (Mn * 60000))

            oTimer = New System.Timers.Timer
            oTimer.Interval = nMinutes
            AddHandler oTimer.Elapsed, New ElapsedEventHandler(AddressOf TimerTicking)
            oTimer.AutoReset = True
            oTimer.Enabled = True
            oTimer.Start()

            CreateLog(ServiceName, "Service Start" + vbCrLf +
                                   "Timer interval " + nMinutes.ToString + " mili seconds")
        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        Try
            CreateLog(ServiceName, "Service End")
            oTimer.Stop()
            oTimer.Dispose()

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Private Sub RunNormalAktarim()
        Try
            Dim oThread As Threading.Thread

            oThread = New Threading.Thread(AddressOf NormalAktarim)
            oThread.IsBackground = True
            oThread.Start()

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Public Sub NormalAktarim()
        Try
            Dim oNormalAktarimJob As New NormalAktarimClass
            oNormalAktarimJob.init()

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Private Sub RunWebAktarim()
        Try
            Dim oThread As Threading.Thread

            oThread = New Threading.Thread(AddressOf WebAktarim)
            oThread.IsBackground = True
            oThread.Start()

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Public Sub WebAktarim()
        Try
            Dim oWebAktarimJob As New WebAktarimClass
            oWebAktarimJob.init()

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Private Sub TimerTicking(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        Try
            'CreateLog(ServiceName, "Timer Ticking")

            If lProcessing Then Exit Sub
            lProcessing = True

            If cServisTipi = "NORMAL" Then
                'RunNormalAktarim()
                NormalAktarim()
            Else
                'RunWebAktarim()
                WebAktarim()
            End If

            lProcessing = False

        Catch ex As Exception
            lProcessing = False
            ErrDisp(ex)
        End Try
    End Sub

End Class
