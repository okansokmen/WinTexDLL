Imports Dart.Sockets
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Configuration

<ComClass(HTMain.ClassId, HTMain.InterfaceId, HTMain.EventsId)>
Public Class HTMain

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "A90FBF2C-8462-437E-B9BC-B3267CE38F81"
    Public Const InterfaceId As String = "27316209-2627-4535-8CBE-0360BF6A65AF"
    Public Const EventsId As String = "051B5F81-67B2-412A-9594-7E46A427B3C7"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public Shared cWinTexMeasureFixVersion As String = My.Application.Info.Version.ToString.Trim

    Public Shared Function TestDLL() As String

        TestDLL = "WinTexMeasureFix ÇALIŞMIYOR"

        Try
            Return "WinTexMeasureFix Çalışıyor"
        Catch ex As Exception
            ErrDisp("TestDLL : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function ShowVersionInfo() As String

        ShowVersionInfo = ""

        Try
            ShowVersionInfo = My.Application.Info.Version.ToString.Trim

        Catch ex As Exception
            ErrDisp("ShowVersionInfo : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function init(Optional cServer As String = "", Optional cDatabase As String = "", Optional cUser As String = "sa", Optional cPassword As String = "") As Boolean
        ' init database connection
        Dim cSQL As String = ""

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
            init = True

        Catch ex As Exception
            init = False
            ErrDisp("SQL Connect : " + ex.Message + " " + cServer.Trim + " " + cDatabase.Trim + " " + cUser.Trim + " " + cPassword.Trim,,,, ex)
        End Try
    End Function


End Class
