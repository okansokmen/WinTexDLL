Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Module UtilLogo

    Public oConnectionLogo As oSQLConn

    Public Sub GetConnLogo()

        Dim ConnYage As SqlClient.SqlConnection

        Try
            ConnYage = OpenConn()
            oConnectionLogo.cServer = GetSysParConnected("coralserver", ConnYage)
            oConnectionLogo.cDatabase = GetSysParConnected("coraldb", ConnYage)
            oConnectionLogo.cUser = GetSysParConnected("coraluser", ConnYage)
            oConnectionLogo.cPassword = GetSysParConnected("coralpassword", ConnYage)
            CloseConn(ConnYage)

            oConnectionLogo.cConnStr = "Data Source=" + oConnectionLogo.cServer + ";" +
                                        "Initial Catalog=" + oConnectionLogo.cDatabase + ";" +
                                        "uid=" + oConnectionLogo.cUser + ";" +
                                        "pwd=" + oConnectionLogo.cPassword + ""
        Catch ex As Exception
            ErrDisp("GetConnLogo", "UtilLogo",,, ex)
        End Try
    End Sub

    Public Function LogoKur(Optional cFilter As String = "") As Boolean

        Dim cSQL As String = ""
        Dim oSQLLogo As SQLServerClass

        Dim cDoviz As String = ""
        Dim dTarih As Date = #1/1/1950#
        Dim nDovizAlis As Double = 0
        Dim nDovizSatis As Double = 0
        Dim nEfektifAlis As Double = 0
        Dim nEfektifSatis As Double = 0

        LogoKur = True

        Try
            GetConnLogo()
            oSQLLogo = New SQLServerClass(False, oConnectionLogo.cServer, oConnectionLogo.cDatabase, oConnectionLogo.cUser, oConnectionLogo.cPassword)

            oSQLLogo.OpenConn()

            cSQL = "select distinct b.curcode, b.curname, a.edate, " +
                    " rates1 = Convert(Decimal(18, 6), a.rates1), " +
                    " rates2 = Convert(Decimal(18, 6), a.rates2), " +
                    " rates3 = Convert(Decimal(18, 6), a.rates3), " +
                    " rates4 = Convert(Decimal(18, 6), a.rates4) " +
                    " from L_DAILYEXCHANGES a with (NOLOCK) , L_CURRENCYLIST b with (NOLOCK) " +
                    " where a.CRTYPE = b.CURTYPE " +
                    " and a.rates1 is not null " +
                    " and a.rates1 <> 0 " +
                    cFilter +
                    " order by a.edate desc , b.curcode "

            oSQLLogo.GetSQLReader(cSQL)

            Do While oSQLLogo.oReader.Read
                cDoviz = oSQLLogo.SQLReadString("curcode")
                dTarih = oSQLLogo.SQLReadDate("edate")
                nDovizAlis = oSQLLogo.SQLReadDouble("rates1")
                nDovizSatis = oSQLLogo.SQLReadDouble("rates2")
                nEfektifAlis = oSQLLogo.SQLReadDouble("rates3")
                nEfektifSatis = oSQLLogo.SQLReadDouble("rates4")

                If Not DovizKuruYaz(dTarih, cDoviz.Trim, "Kur", nDovizAlis, "LOGO") Then
                    LogoKur = False
                End If

                If Not DovizKuruYaz(dTarih, cDoviz.Trim, "Satis Kuru", nDovizSatis, "LOGO") Then
                    LogoKur = False
                End If

                If Not DovizKuruYaz(dTarih, cDoviz.Trim, "Efektif Alis Kuru", nEfektifAlis, "LOGO") Then
                    LogoKur = False
                End If

                If Not DovizKuruYaz(dTarih, cDoviz.Trim, "Efektif Satis Kuru", nEfektifSatis, "LOGO") Then
                    LogoKur = False
                End If
            Loop

            oSQLLogo.oReader.Close()
            oSQLLogo.oReader = Nothing

            oSQLLogo.CloseConn()
            oSQLLogo = Nothing

        Catch ex As Exception
            ErrDisp("LogoKur", "UtilLogo",,, ex)
        End Try
    End Function
End Module
