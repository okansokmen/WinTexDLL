Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.IO
Imports System.Text

<ComClass(HTMain.ClassId, HTMain.InterfaceId, HTMain.EventsId)>
Public Class HTMain

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "C7531473-42A6-47A4-8BA3-943A816481CC"
    Public Const InterfaceId As String = "DC7F5385-ACA6-4A97-B835-2466E3AD5738"
    Public Const EventsId As String = "7BA5BCDA-2384-4515-BD02-DDA7EE824070"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Shared cWinTexTiciMaxVersion As String = My.Application.Info.Version.ToString.Trim

    Public Sub New()
        MyBase.New()
    End Sub

    Public Function ShowVersionInfo() As String

        ShowVersionInfo = ""

        Try
            ShowVersionInfo = My.Application.Info.Version.ToString.Trim

        Catch ex As Exception
            ErrDisp("ShowVersionInfo : " + ex.Message, "HTMain")
        End Try
    End Function

    Public Shared Function TestWinTexTiciMax() As String

        TestWinTexTiciMax = "TestWinTexTiciMax ÇALIŞMIYOR"

        Try
            Return "TestWinTexTiciMax Çalışıyor"
        Catch ex As Exception
            ErrDisp("TestWinTexTiciMax : " + ex.Message, "HTMain",,, ex)
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
            GetTiciMaxParameters()

            init = True

        Catch ex As Exception
            init = False
            ErrDisp("SQL Connect : " + ex.Message + " " + cServer.Trim + " " + cDatabase.Trim + " " + cUser.Trim + " " + cPassword.Trim,,,, ex)
        End Try
    End Function

    Public Sub TiciMaxSysPar()
        Try
            Dim ofrmTicimaxParameters As New frmTicimaxParameters
            ofrmTicimaxParameters.ShowDialog()

        Catch ex As Exception
            ErrDisp("TiciMaxSysPar : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub GetListe(nCase As Integer)

        Try
            Dim oUrunClass As New UrunClass
            oUrunClass.GetList(nCase)
            oUrunClass.CloseClient()
            oUrunClass = Nothing

        Catch ex As Exception
            ErrDisp("GetListe : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Sub AnaMenu()
        Try
            Dim oAnaMenu As New AnaMenu
            oAnaMenu.init()

        Catch ex As Exception
            ErrDisp("AnaMenu : " + ex.Message, "HTMain",,, ex)
        End Try
    End Sub

    Public Function DownloadSiparisler()

        DownloadSiparisler = False

        Try
            Dim oSiparis As New SiparisClass
            DownloadSiparisler = oSiparis.ReadSiparisFromTiciMax()
            oSiparis.CloseClient()
            oSiparis = Nothing

        Catch ex As Exception
            ErrDisp("DownloadSiparisler : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function UploadSiparisler()

        UploadSiparisler = False

        Try
            Dim oSiparis As New SiparisClass
            UploadSiparisler = oSiparis.WriteSiparislerToTiciMax()
            oSiparis.CloseClient()
            oSiparis = Nothing

        Catch ex As Exception
            ErrDisp("UploadSiparisler : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function UploadSingleSiparis(cSiparisNo As String)

        UploadSingleSiparis = False

        Try
            Dim oSiparis As New SiparisClass
            UploadSingleSiparis = oSiparis.WriteSiparisToTiciMax(cSiparisNo)
            oSiparis.CloseClient()
            oSiparis = Nothing

        Catch ex As Exception
            ErrDisp("UploadSingleSiparis : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function UploadUrunler() As Boolean

        UploadUrunler = False

        Try
            Dim oUrun As New UrunClass
            UploadUrunler = oUrun.SendProducts()
            oUrun.CloseClient()
            oUrun = Nothing

        Catch ex As Exception
            ErrDisp("UploadUrunler : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

    Public Function UploadUrun(cStokNo As String, cRenk As String, cBeden As String) As Boolean

        UploadUrun = False

        Try
            Dim oUrun As New UrunClass
            UploadUrun = oUrun.SendProduct(cStokNo, cRenk, cBeden)
            oUrun.CloseClient()
            oUrun = Nothing

        Catch ex As Exception
            ErrDisp("UploadUrun : " + ex.Message, "HTMain",,, ex)
        End Try
    End Function

End Class
