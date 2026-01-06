Imports System.Net

<ComClass(HTMain.ClassId, HTMain.InterfaceId, HTMain.EventsId)>
Public Class HTMain

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "7f283376-7b6f-4d60-a80b-27319b9009ce"
    Public Const InterfaceId As String = "43ee9731-5526-4e47-9063-4126809B9f69"
    Public Const EventsId As String = "0f28a587-e2bd-47d2-9B7c-5679ba887cf6"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public Shared cWinTexScheduleVersion As String = My.Application.Info.Version.ToString.Trim

    Public Function DllVersion() As String
        DllVersion = ""
        Try
            DllVersion = My.Application.Info.Version.ToString.Trim
        Catch ex As Exception
            DllVersion = ""
        End Try
    End Function

    Public Function DllTest() As String

        DllTest = "WinTexSchedule ÇALIŞMIYOR"

        Try
            Return "WinTexSchedule Çalışıyor"
        Catch ex As Exception
            ErrDisp("DllTest : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function init(Optional cServer As String = "", Optional cDatabase As String = "", Optional cUser As String = "sa", Optional cPassword As String = "",
                         Optional cWinTexUser As String = "") As Boolean
        ' init database connection
        Dim cSQL As String = ""

        init = False

        Try
            oConnection.cServer = cServer.Trim
            oConnection.cDatabase = cDatabase.Trim
            oConnection.cUser = cUser.Trim
            oConnection.cPassword = cPassword.Trim
            oConnection.cWinTexUser = cWinTexUser

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

    Public Sub Takvim(Optional cFilter As String = "")
        Try
            Dim oTakvim As New Takvim
            oTakvim.init(cFilter)

        Catch ex As Exception
            ErrDisp("Takvim : " + ex.Message,,,, ex)
        End Try
    End Sub

End Class
