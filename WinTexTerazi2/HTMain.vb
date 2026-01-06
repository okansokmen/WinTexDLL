<ComClass(HTMain.ClassId, HTMain.InterfaceId, HTMain.EventsId)>
Public Class HTMain

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "BC21B5ED-C2EB-4527-88A8-25A8BFD13FED"
    Public Const InterfaceId As String = "E1F5B157-E0E1-4762-B508-42A1189EAA5B"
    Public Const EventsId As String = "887BD7AE-6775-4CA9-B96B-24BB4B1E7634"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public Shared cWinTexTeraziVersion As String = My.Application.Info.Version.ToString.Trim
    Public Shared cLogDir As String = "C:\wintex\WinTexTeraziLogs"
    Public Shared cLogFile As String = "C:\wintex\WinTexTeraziLogs\WinTexTeraziLog"
    Public Shared cErrorDir As String = "C:\wintex\WinTexTeraziLogs"
    Public Shared cErrorFile As String = "C:\wintex\WinTexTeraziErrors\WinTexTeraziError"

    Public Shared Function TestWinTexTarti() As String

        TestWinTexTarti = "TestWinTexTarti ÇALIŞMIYOR"

        Try
            Return "TestWinTexTarti Çalışıyor"
        Catch ex As Exception
            ErrDisp("TestWinTexTarti  " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function ShowVersionInfo() As String

        ShowVersionInfo = ""

        Try
            ShowVersionInfo = My.Application.Info.Version.ToString.Trim

        Catch ex As Exception
            ErrDisp("ShowVersionInfo  " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function init(Optional cServer As String = "", Optional cDatabase As String = "", Optional cUser As String = "sa", Optional cPassword As String = "") As Boolean
        ' init database connection
        Dim oSQL As SQLServerClass

        init = False

        Try
            oConnection.cServer = cServer.Trim
            oConnection.cDatabase = cDatabase.Trim
            oConnection.cUser = cUser.Trim
            oConnection.cPassword = cPassword.Trim

            oConnection.cConnStr = "Data Source=" + oConnection.cServer + ";" +
                                    "Initial Catalog=" + oConnection.cDatabase + ";" +
                                    "uid=" + oConnection.cUser + ";" +
                                    "pwd=" + oConnection.cPassword + ""

            oSQL = New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 firmname " +
                                " from sysinfo with (NOLOCK) " +
                                " where firmname Is Not null " +
                                " And firmname <> '' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                oConnection.cOwner = oSQL.SQLReadString("firmname").ToLower
            End If
            oSQL.oReader.Close()

            oSQL.CloseConn()
            oSQL = Nothing

            init = True

        Catch ex As Exception
            init = False
            ErrDisp("SQL Connect : " + ex.Message + " " + cServer.Trim + " " + cDatabase.Trim + " " + cUser.Trim + " " + cPassword.Trim, "HTMain",,, ex)
        End Try
    End Function

    Public Sub ComTest()

        Try
            Dim ofmMain As New fmMain
            ofmMain.Show()

        Catch ex As Exception
            ErrDisp("ComTest  " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function TeraziOku(Optional cComPort As String = "COM4", Optional nBaudRate As Integer = 9600) As Double

        TeraziOku = 0

        Try
            Dim oTerazi As New Terazi

            oTerazi.init(cComPort, nBaudRate)
            oTerazi.EnumComPorts()
            If oTerazi.Connect() Then
                oTerazi.SetConfig()
                TeraziOku = oTerazi.GetData
            End If
            oTerazi.Disconnect()
            oTerazi = Nothing

        Catch ex As Exception
            ErrDisp("TeraziOku  " + ex.Message, "HTMain",,, ex)
        End Try
    End Function
End Class
