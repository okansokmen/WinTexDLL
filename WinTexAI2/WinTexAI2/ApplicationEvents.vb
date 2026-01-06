Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            ' Process command line arguments
            ProcessCommandLineArgs(e.CommandLine.ToArray())
        End Sub

        Private Sub ProcessCommandLineArgs(args() As String)
            ' Expected format: WinTexAI2.exe server database user password owner wintexuser

            If args.Length >= 6 Then
                utilMain.oConnection.cServer = args(0)
                utilMain.oConnection.cDatabase = args(1)
                utilMain.oConnection.cUser = args(2)
                utilMain.oConnection.cPassword = args(3)
                utilMain.oConnection.cOwner = args(4)
                utilMain.oConnection.cWinTexUser = args(5)

            Else
                ' Use default values from initWinTex if insufficient arguments
                Select Case oConnection.cOwner
                    Case "eroglu"
                        utilMain.oConnection.cOwner = "eroglu"
                        utilMain.oConnection.cServer = "192.168.1.8"
                        utilMain.oConnection.cDatabase = "tes"
                        utilMain.oConnection.cUser = "sa"
                        utilMain.oConnection.cPassword = "er1303*?"
                        utilMain.oConnection.cWinTexUser = "ADMIN"
                    Case "eroglu_deneme"
                        utilMain.oConnection.cOwner = "eroglu"
                        utilMain.oConnection.cServer = "192.168.1.8"
                        utilMain.oConnection.cDatabase = "tesdeneme"
                        utilMain.oConnection.cUser = "sa"
                        utilMain.oConnection.cPassword = "er1303*?"
                        utilMain.oConnection.cWinTexUser = "ADMIN"
                    Case "eroglu_local"
                        utilMain.oConnection.cOwner = "eroglu"
                        utilMain.oConnection.cServer = "monster"
                        utilMain.oConnection.cDatabase = "tes"
                        utilMain.oConnection.cUser = "sa"
                        utilMain.oConnection.cPassword = "Hayabusa1024"
                        utilMain.oConnection.cWinTexUser = "ADMIN"
                    Case "veragroup_local"
                        utilMain.oConnection.cOwner = "vera"
                        utilMain.oConnection.cServer = "monster"
                        utilMain.oConnection.cDatabase = "veragroup"
                        utilMain.oConnection.cUser = "sa"
                        utilMain.oConnection.cPassword = "Hayabusa1024"
                        utilMain.oConnection.cWinTexUser = "ADMIN"
                        'Case Else ' "veragroup"
                        '    utilMain.oConnection.cOwner = "vera"
                        '    utilMain.oConnection.cServer = "192.168.1.216"
                        '    utilMain.oConnection.cDatabase = "veragroup"
                        '    utilMain.oConnection.cUser = "sa"
                        '    utilMain.oConnection.cPassword = "wintex"
                        '    utilMain.oConnection.cWinTexUser = "ADMIN"
                    Case Else
                        ' Default connection parameters
                        utilMain.oConnection.cOwner = "vera"
                        utilMain.oConnection.cServer = "monster"
                        utilMain.oConnection.cDatabase = "veragroup"
                        utilMain.oConnection.cUser = "sa"
                        utilMain.oConnection.cPassword = "Hayabusa1024"
                        utilMain.oConnection.cWinTexUser = "ADMIN"
                End Select
            End If

            ' Update connection string with new parameters
            utilMain.oConnection.cConnStr = "Data Source=" + utilMain.oConnection.cServer + ";" +
                                            "Initial Catalog=" + utilMain.oConnection.cDatabase + ";" +
                                            "uid=" + utilMain.oConnection.cUser + ";" +
                                            "pwd=" + utilMain.oConnection.cPassword + ""

            'MsgBox("Connection Parameters:" & vbCrLf &
            '        "Server: " & utilMain.oConnection.cServer & vbCrLf &
            '        "Database: " & utilMain.oConnection.cDatabase & vbCrLf &
            '        "User: " & utilMain.oConnection.cUser & vbCrLf &
            '        "Owner: " & utilMain.oConnection.cOwner & vbCrLf &
            '        "WinTex User: " & utilMain.oConnection.cWinTexUser, MsgBoxStyle.Information, "Info")

            utilMain.initWinTex()
        End Sub
    End Class
End Namespace
