Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Drawing
Imports System.IO
Imports Microsoft.SqlServer.Server
Imports Microsoft.VisualBasic.FileIO.FileSystem

Module UtilRoot

    Public lGlobalDebugMode As Boolean = False

    Public nUyeID As Integer = 0
    Public nSiparisID As Integer = 0
    Public nUrunID As Integer = 0

    Public Const G_NumberFormat = "###,###,###,###,###,##0"
    Public Const G_Number1Format = "###,###,###,###,###,##0.0"
    Public Const G_Number2Format = "###,###,###,###,###,##0.00"
    Public Const G_Number3Format = "###,###,###,###,###,##0.000"
    Public Const G_Number4Format = "###,###,###,###,###,##0.0000"
    Public Const G_Number5Format = "###,###,###,###,###,##0.00000"
    Public Const G_Number6Format = "###,###,###,###,###,##0.000000"

    Public Gl_Personel As String = ""
    Public Gl_UserName As String = ""
    Public Gl_ActivePass As String = ""

    Public Structure oSQLConn
        Dim cOwner As String

        Dim cServer As String
        Dim cDatabase As String
        Dim cUser As String
        Dim cPassword As String
        Dim cConnStr As String

        Dim cPersonel As String

        Dim cTiciMaxApiUrunUrl As String
        Dim cTiciMaxApiSiparisUrl As String
        Dim cTiciMaxApiUyeUrl As String
        Dim cTiciMaxApiCustomUrl As String

        Dim cTiciMaxAdminURL As String
        Dim cTiciMaxAdminUserName As String
        Dim cTiciMaxAdminPassword As String
        Dim cTiciMaxFirma As String
        Dim cTiciMaxYetkiKodu As String

        Dim cTiciMaxAnaKategori As String
        Dim cTiciMaxAnaKategoriID As String
        Dim cTiciMaxMarka As String
        Dim cTiciMaxMarkaID As String
        Dim cTiciMaxTedarikciID As String
        Dim cTiciMaxSatisBirimi As String
        Dim cTiciMaxDesi As String
        Dim cTiciMaxKargoUcreti As String
        Dim cTiciMaxKdvOrani As String

        Dim cTiciMaxSiparisTipi As String
        Dim cTiciMaxKargoFirmasi As String
        Dim cTiciMaxAraciKargo As String
        Dim cTiciMaxOdemeTipi As String
        Dim cTiciMaxTeslimSekli As String
        Dim cTiciMaxKomisyoncuFirma As String
        Dim cTiciMaxUserName As String

    End Structure

    Public oConnection As oSQLConn

    Public Sub GetTiciMaxParameters()

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oConnection.cTiciMaxApiUrunUrl = oSQL.GetSysPar("ticimaxapiurunurl")
            oConnection.cTiciMaxApiSiparisUrl = oSQL.GetSysPar("ticimaxapisiparisurl")
            oConnection.cTiciMaxApiUyeUrl = oSQL.GetSysPar("ticimaxapiuyeurl")
            oConnection.cTiciMaxApiCustomUrl = oSQL.GetSysPar("ticimaxapicustomurl")
            oConnection.cTiciMaxAdminURL = oSQL.GetSysPar("ticimaxadminurl")
            oConnection.cTiciMaxAdminUserName = oSQL.GetSysPar("ticimaxadminusername")
            oConnection.cTiciMaxAdminPassword = oSQL.GetSysPar("ticimaxadminpassword")
            oConnection.cTiciMaxFirma = oSQL.GetSysPar("ticimaxfirma")
            oConnection.cTiciMaxYetkiKodu = oSQL.GetSysPar("ticimaxyetkikodu")

            oConnection.cTiciMaxAnaKategori = oSQL.GetSysPar("ticimaxanakategori")
            oConnection.cTiciMaxAnaKategoriID = oSQL.GetSysPar("ticimaxanakategoriid", "1")
            oConnection.cTiciMaxMarka = oSQL.GetSysPar("ticimaxmarka")
            oConnection.cTiciMaxMarkaID = oSQL.GetSysPar("ticimaxmarkaid", "1")
            oConnection.cTiciMaxTedarikciID = oSQL.GetSysPar("ticimaxtedarikciid", "1")
            oConnection.cTiciMaxSatisBirimi = oSQL.GetSysPar("ticimaxsatisbirimi")
            oConnection.cTiciMaxDesi = oSQL.GetSysPar("ticimaxdesi", "1")
            oConnection.cTiciMaxKargoUcreti = oSQL.GetSysPar("ticimaxkargoucreti", "1")
            oConnection.cTiciMaxKdvOrani = oSQL.GetSysPar("ticimaxkdvorani", "1")

            oConnection.cTiciMaxSiparisTipi = oSQL.GetSysPar("ticimaxsiparistipi")
            oConnection.cTiciMaxKargoFirmasi = oSQL.GetSysPar("ticimaxkargofirmasi")
            oConnection.cTiciMaxAraciKargo = oSQL.GetSysPar("ticimaxaracikargo")
            oConnection.cTiciMaxOdemeTipi = oSQL.GetSysPar("ticimaxodemetipi")
            oConnection.cTiciMaxTeslimSekli = oSQL.GetSysPar("ticimaxteslimsekli")
            oConnection.cTiciMaxKomisyoncuFirma = oSQL.GetSysPar("ticimaxkomisyoncufirma")
            oConnection.cTiciMaxUserName = oSQL.GetSysPar("ticimaxusername")

            oSQL.cSQLQuery = "select top 1 firmname " +
                                " from sysinfo with (NOLOCK) " +
                                " where firmname is not null " +
                                " and firmname <> '' "
            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                oConnection.cOwner = oSQL.SQLReadString("firmname").ToLower
            End If
            oSQL.oReader.Close()

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("GetMNGParameters : " + ex.Message, "utilroot",,, ex)
        End Try
    End Sub

    Public Sub TiciMaxParametersList()
        'List<SiparisOdemeTipleri> siparisOdemeTipleriListe =  SiparisServiceMethods.GetOdemeTipleri();

    End Sub

End Module
