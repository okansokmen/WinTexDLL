Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Module UtilAbit
    Private Structure oBeyanname
        Dim cIslemNo As String
        Dim cSatirNo As String
        Dim cMdKodu As String
        Dim cEvrakTuru As String
        Dim cUnspedDosyaNo As String
        Dim cMusteriRefNo As String
        Dim cBeyannameNo As String
        Dim cBeyannameTarihi As String
        Dim cGumrukAdi As String
        Dim cGumrukKodu As String
        Dim cMalinKiymeti As String
        Dim cDovizCinsi As String
        Dim cKonsimentoNo As String
        Dim cHouseNo As String
        Dim cYurtDisiMusteriNo As String
        Dim cYurtDisiMusteriAdi As String
        Dim cKapanizIntacTarihi As String
        Dim cUlke As String
        Dim cMalinFaturaNo As String
        Dim cVasitaAdi As String
        Dim cAbitCounter As String
        Dim dUGMGiris As Date

        Dim aFaturaItem() As oFaturaItem
        Dim aDekontItem() As oDekontItem
    End Structure

    Dim oBeyannameler() As oBeyanname

    Private Structure oFaturaItem
        Dim cIslemNo As String
        Dim cSatirNo As String
        Dim cMDKodu As String
        Dim cEvrakTuru As String
        Dim cAciklamaKod As String
        Dim cAciklama As String
        Dim cEvrakNo As String
        Dim cEvrakTarihi As String
        Dim cTutar As String
        Dim cKDV As String
        Dim cToplam As String
        Dim cDovizCinsi As String
        Dim cKur As String
        Dim cTLTutar As String
        Dim cVergiNo As String
        Dim cValorTarihi As String
        Dim cABITCounter As String
        Dim dUGMGiris As Date

        Dim aFaturaKalemItem() As oFaturaKalemItem
    End Structure

    Private Structure oFaturaKalemItem
        Dim cIslemNo As String
        Dim cSatirNo As String
        Dim cMDKodu As String
        Dim cEvrakTuru As String
        Dim cAciklamaKod As String
        Dim cAciklama As String
        Dim cEvrakNo As String
        Dim cEvrakTarihi As String
        Dim cTutar As String
        Dim cKDV As String
        Dim cToplam As String
        Dim cDovizCinsi As String
        Dim cKur As String
        Dim cTLTutar As String
        Dim cVergiNo As String
        Dim cValorTarihi As String
        Dim cABITCounter As String
        Dim dUGMGiris As Date
    End Structure

    Private Structure oDekontItem
        Dim cIslemNo As String
        Dim cSatirNo As String
        Dim cMDKodu As String
        Dim cEvrakTuru As String
        Dim cAciklamaKod As String
        Dim cAciklama As String
        Dim cEvrakNo As String
        Dim cEvrakTarihi As String
        Dim cTutar As String
        Dim cKDV As String
        Dim cToplam As String
        Dim cDovizCinsi As String
        Dim cKur As String
        Dim cTLTutar As String
        Dim cVergiNo As String
        Dim cValorTarihi As String
        Dim cABITCounter As String
        Dim dUGMGiris As Date

        Dim aDekontKalemItem() As oDekontKalemItem
    End Structure

    Private Structure oDekontKalemItem
        Dim cIslemNo As String
        Dim cSatirNo As String
        Dim cMDKodu As String
        Dim cEvrakTuru As String
        Dim cAciklamaKod As String
        Dim cAciklama As String
        Dim cEvrakNo As String
        Dim cEvrakTarihi As String
        Dim cTutar As String
        Dim cKDV As String
        Dim cToplam As String
        Dim cDovizCinsi As String
        Dim cKur As String
        Dim cTLTutar As String
        Dim cVergiNo As String
        Dim cValorTarihi As String
        Dim cABITCounter As String
        Dim dUGMGiris As Date

        Dim aDekontKalemDetayItem() As oDekontKalemDetayItem
    End Structure

    Private Structure oDekontKalemDetayItem
        Dim cIslemNo As String
        Dim cSatirNo As String
        Dim cMDKodu As String
        Dim cEvrakTuru As String
        Dim cAciklamaKod As String
        Dim cAciklama As String
        Dim cEvrakNo As String
        Dim cEvrakTarihi As String
        Dim cTutar As String
        Dim cKDV As String
        Dim cToplam As String
        Dim cDovizCinsi As String
        Dim cKur As String
        Dim cTLTutar As String
        Dim cVergiNo As String
        Dim cValorTarihi As String
        Dim cABITCounter As String
        Dim dUGMGiris As Date
    End Structure

    Dim ofrmstatus As frmStatus
    Dim dBasla As Date = #1/1/1950#
    Dim dBitis As Date = #1/1/1950#

    Public Sub AbitReadWrite(Optional dBaslangicTarihi As Date = #1/1/1950#, Optional dBitisTarihi As Date = #1/1/1950#)
        Try
            dBasla = dBaslangicTarihi
            dBitis = dBitisTarihi

            ofrmstatus = New frmStatus
            ofrmstatus.init()
            ' ithalat
            ReadBeyannameler("T")
            WriteBeyannameler("T")
            ' ihracat
            ReadBeyannameler("H")
            WriteBeyannameler("H")
            ' son
            ofrmstatus.Close()
        Catch ex As Exception
            ErrDisp("AbitReadWrite : " + ex.Message)
        End Try
    End Sub
    Private Function WriteBeyannameler(cEvrakTipi As String) As Boolean

        Dim cSQL As String = ""
        Dim ConnYage As SqlConnection = Nothing
        Dim nCnt As Integer = 0
        Dim nCnt1 As Integer = 0
        Dim nCnt2 As Integer = 0
        Dim nCntYazilan As Integer = 0
        Dim oDR As SqlDataReader = Nothing
        Dim lExists As Boolean = False
        Dim lUpdate As Boolean = False

        ofrmstatus.ShowMessage("Yazma işlemi başladı. evraktipi : " + cEvrakTipi)

        WriteBeyannameler = False

        Try
            ConnYage = OpenConn()

            For nCnt = 0 To UBound(oBeyannameler)

                If AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) <> "" Then

                    lExists = False
                    lUpdate = False

                    cSQL = "select top 1 * " +
                           " from abitbeyanname with (NOLOCK) " +
                           " where UnspedDosyaNo = '" + AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + "' "

                    oDR = GetSQLReader(cSQL, ConnYage)

                    If oDR.Read Then
                        lExists = True

                        lUpdate = Not (SQLReadString(oDR, "MusteriRefNo", 50) = AbitReadString(oBeyannameler(nCnt).cMusteriRefNo, 50) And
                                       SQLReadString(oDR, "BeyannameNo", 50) = AbitReadString(oBeyannameler(nCnt).cBeyannameNo, 50) And
                                       SQLReadString(oDR, "BeyannameTarihi", 50) = AbitReadString(oBeyannameler(nCnt).cBeyannameTarihi, 50) And
                                       SQLReadString(oDR, "GumrukAdi", 50) = AbitReadString(oBeyannameler(nCnt).cGumrukAdi, 50) And
                                       SQLReadString(oDR, "GumrukKodu", 50) = AbitReadString(oBeyannameler(nCnt).cGumrukKodu, 50) And
                                       SQLReadString(oDR, "MalinKiymeti", 50) = AbitReadString(oBeyannameler(nCnt).cMalinKiymeti, 50) And
                                       SQLReadString(oDR, "DovizCinsi", 50) = AbitReadString(oBeyannameler(nCnt).cDovizCinsi, 50) And
                                       SQLReadString(oDR, "KonsimentoNo", 50) = AbitReadString(oBeyannameler(nCnt).cKonsimentoNo, 50) And
                                       SQLReadString(oDR, "HouseNo", 50) = AbitReadString(oBeyannameler(nCnt).cHouseNo, 50) And
                                       SQLReadString(oDR, "YurtDisiMusteriNo", 50) = AbitReadString(oBeyannameler(nCnt).cYurtDisiMusteriNo, 50) And
                                       SQLReadString(oDR, "YurtDisiMusteriAdi", 50) = AbitReadString(oBeyannameler(nCnt).cYurtDisiMusteriAdi, 50) And
                                       SQLReadString(oDR, "KapanizIntacTarihi", 50) = AbitReadString(oBeyannameler(nCnt).cKapanizIntacTarihi, 50) And
                                       SQLReadString(oDR, "Ulke", 50) = AbitReadString(oBeyannameler(nCnt).cUlke, 50) And
                                       SQLReadString(oDR, "MalinFaturaNo", 50) = AbitReadString(oBeyannameler(nCnt).cMalinFaturaNo, 50) And
                                       SQLReadString(oDR, "VasitaAdi", 50) = AbitReadString(oBeyannameler(nCnt).cVasitaAdi, 50))
                    End If
                    oDR.Close()

                    If lExists Then
                        If lUpdate Then
                            cSQL = "update abitbeyanname set " +
                                " MusteriRefNo = '" + SQLWriteString(oBeyannameler(nCnt).cMusteriRefNo, 50) + "', " +
                                " BeyannameNo = '" + SQLWriteString(oBeyannameler(nCnt).cBeyannameNo, 50) + "', " +
                                " BeyannameTarihi = '" + SQLWriteString(oBeyannameler(nCnt).cBeyannameTarihi, 50) + "', " +
                                " GumrukAdi = '" + SQLWriteString(oBeyannameler(nCnt).cGumrukAdi, 50) + "', " +
                                " GumrukKodu = '" + SQLWriteString(oBeyannameler(nCnt).cGumrukKodu, 50) + "', " +
                                " MalinKiymeti = '" + SQLWriteString(oBeyannameler(nCnt).cMalinKiymeti, 50) + "', " +
                                " DovizCinsi = '" + SQLWriteString(oBeyannameler(nCnt).cDovizCinsi, 50) + "', " +
                                " KonsimentoNo = '" + SQLWriteString(oBeyannameler(nCnt).cKonsimentoNo, 50) + "', " +
                                " HouseNo = '" + SQLWriteString(oBeyannameler(nCnt).cHouseNo, 50) + "', " +
                                " YurtDisiMusteriNo = '" + SQLWriteString(oBeyannameler(nCnt).cYurtDisiMusteriNo, 50) + "', " +
                                " YurtDisiMusteriAdi = '" + SQLWriteString(oBeyannameler(nCnt).cYurtDisiMusteriAdi, 50) + "', " +
                                " KapanizIntacTarihi = '" + SQLWriteString(oBeyannameler(nCnt).cKapanizIntacTarihi, 50) + "', " +
                                " Ulke = '" + SQLWriteString(oBeyannameler(nCnt).cUlke, 50) + "', " +
                                " MalinFaturaNo = '" + SQLWriteString(oBeyannameler(nCnt).cMalinFaturaNo, 50) + "', " +
                                " VasitaAdi = '" + SQLWriteString(oBeyannameler(nCnt).cVasitaAdi, 50) + "', " +
                                " modificationdate = getdate() " +
                                " where UnspedDosyaNo = '" + oBeyannameler(nCnt).cUnspedDosyaNo.Trim + "' "

                            ExecuteSQLCommandConnected(cSQL, ConnYage)

                            ofrmstatus.ShowMessage("Beyanname update : " + oBeyannameler(nCnt).cUnspedDosyaNo)
                        End If
                        nCntYazilan = nCntYazilan + 1
                    Else
                        cSQL = "insert abitbeyanname (IslemNo, SatirNo, MdKodu, EvrakTuru, UnspedDosyaNo, " +
                                                    " MusteriRefNo, BeyannameNo, BeyannameTarihi, GumrukAdi, GumrukKodu, " +
                                                    " MalinKiymeti, DovizCinsi, KonsimentoNo, HouseNo, YurtDisiMusteriNo, " +
                                                    " YurtDisiMusteriAdi, KapanizIntacTarihi, Ulke, MalinFaturaNo, VasitaAdi, " +
                                                    " AbitCounter, evraktipi, modificationdate, creationdate, ugmgiris) "
                        cSQL = cSQL +
                               " values ('" + SQLWriteString(oBeyannameler(nCnt).cIslemNo, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cSatirNo, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cMdKodu, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cEvrakTuru, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cUnspedDosyaNo, 50) + "', "

                        cSQL = cSQL +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cMusteriRefNo, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cBeyannameNo, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cBeyannameTarihi, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cGumrukAdi, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cGumrukKodu, 50) + "', "

                        cSQL = cSQL +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cMalinKiymeti, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cDovizCinsi, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cKonsimentoNo, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cHouseNo, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cYurtDisiMusteriNo, 50) + "', "

                        cSQL = cSQL +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cYurtDisiMusteriAdi, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cKapanizIntacTarihi, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cUlke, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cMalinFaturaNo, 50) + "', " +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cVasitaAdi, 50) + "', "

                        cSQL = cSQL +
                               " '" + SQLWriteString(oBeyannameler(nCnt).cAbitCounter, 50) + "', " +
                               " '" + cEvrakTipi + "', " +
                               " getdate(), " +
                               " getdate(), " +
                               " '" + SQLWriteDate(oBeyannameler(nCnt).dUGMGiris) + "') "

                        ExecuteSQLCommandConnected(cSQL, ConnYage)

                        ofrmstatus.ShowMessage("Beyanname insert : " + oBeyannameler(nCnt).cUnspedDosyaNo)
                    End If

                    For nCnt1 = 0 To UBound(oBeyannameler(nCnt).aFaturaItem)

                        If AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cEvrakNo) <> "" Then

                            lExists = False
                            lUpdate = False

                            cSQL = "select * " +
                                   " from abitdetay with (NOLOCK) " +
                                   " where UnspedDosyaNo = '" + AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + "' " +
                                   " and EvrakNo = '" + AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cEvrakNo) + "' " +
                                   " and AciklamaKod = '" + AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cAciklamaKod) + "' " +
                                   " and evraktipi = 'FATURA' "

                            oDR = GetSQLReader(cSQL, ConnYage)

                            If oDR.Read Then
                                lExists = True

                                lUpdate = Not (SQLReadString(oDR, "Aciklama", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cAciklama, 50) And
                                               SQLReadString(oDR, "EvrakTarihi", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cEvrakTarihi, 50) And
                                               SQLReadString(oDR, "Tutar", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cTutar, 50) And
                                               SQLReadString(oDR, "KDV", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cKDV, 50) And
                                               SQLReadString(oDR, "Toplam", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cToplam, 50) And
                                               SQLReadString(oDR, "DovizCinsi", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cDovizCinsi, 50) And
                                               SQLReadString(oDR, "Kur", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cKur, 50) And
                                               SQLReadString(oDR, "TLTutar", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cTLTutar, 50) And
                                               SQLReadString(oDR, "VergiNo", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cVergiNo, 50) And
                                               SQLReadString(oDR, "ValorTarihi", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cValorTarihi, 50))
                            End If
                            oDR.Close()

                            If lExists Then
                                If lUpdate Then
                                    cSQL = "update abitdetay set " +
                                            " Aciklama = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cAciklama, 50) + "', " +
                                            " EvrakNo = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cEvrakNo, 50) + "', " +
                                            " EvrakTarihi = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cEvrakTarihi, 50) + "', " +
                                            " Tutar = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cTutar, 50) + "', " +
                                            " KDV = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cKDV, 50) + "', " +
                                            " Toplam = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cToplam, 50) + "', " +
                                            " DovizCinsi = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cDovizCinsi, 50) + "', " +
                                            " Kur = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cKur, 50) + "', " +
                                            " TLTutar = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cTLTutar, 50) + "', " +
                                            " VergiNo = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cVergiNo, 50) + "', " +
                                            " ValorTarihi = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cValorTarihi, 50) + "', " +
                                            " modificationdate = getdate() "

                                    cSQL = cSQL +
                                            " where UnspedDosyaNo = '" + AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + "' " +
                                            " and EvrakNo = '" + AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cEvrakNo) + "' " +
                                            " and AciklamaKod = '" + AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cAciklamaKod) + "' " +
                                            " and evraktipi = 'FATURA' "

                                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                                    ofrmstatus.ShowMessage("Fatura update : " +
                                                           AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + " / " +
                                                           AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cEvrakNo))
                                End If
                            Else
                                cSQL = "insert abitdetay (UnspedDosyaNo, MDKodu, evraktipi, IslemNo, SatirNo, " +
                                                        " EvrakTuru, AciklamaKod, Aciklama, EvrakNo, EvrakTarihi, " +
                                                        " Tutar, KDV, Toplam, DovizCinsi, Kur, " +
                                                        " TLTutar, VergiNo, ValorTarihi, ABITCounter, modificationdate, " +
                                                        " creationdate, ugmgiris) "
                                cSQL = cSQL +
                                        " values ('" + SQLWriteString(oBeyannameler(nCnt).cUnspedDosyaNo, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cMDKodu, 50) + "', " +
                                        " 'FATURA', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cIslemNo, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cSatirNo, 50) + "', "

                                cSQL = cSQL +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cEvrakTuru, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cAciklamaKod, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cAciklama, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cEvrakNo, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cEvrakTarihi, 50) + "', "

                                cSQL = cSQL +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cTutar, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cKDV, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cToplam, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cDovizCinsi, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cKur, 50) + "', "

                                cSQL = cSQL +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cTLTutar, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cVergiNo, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cValorTarihi, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cABITCounter, 50) + "', " +
                                        " getdate(), "

                                cSQL = cSQL +
                                        " getdate(), " +
                                        " '" + SQLWriteDate(oBeyannameler(nCnt).aFaturaItem(nCnt1).dUGMGiris) + "') "


                                ExecuteSQLCommandConnected(cSQL, ConnYage)

                                ofrmstatus.ShowMessage("Fatura insert : " +
                                                       AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + " / " +
                                                       AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cEvrakNo))
                            End If

                            For nCnt2 = 0 To UBound(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem)

                                If AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cEvrakNo) <> "" Then

                                    lExists = False
                                    lUpdate = False

                                    cSQL = "select * " +
                                           " from abitdetay with (NOLOCK) " +
                                           " where UnspedDosyaNo = '" + AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + "' " +
                                           " and EvrakNo = '" + AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cEvrakNo) + "' " +
                                           " and AciklamaKod = '" + AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cAciklamaKod) + "' " +
                                           " and evraktipi = 'FATURAKALEM' "

                                    oDR = GetSQLReader(cSQL, ConnYage)

                                    If oDR.Read Then
                                        lExists = True

                                        lUpdate = Not (SQLReadString(oDR, "Aciklama", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cAciklama, 50) And
                                                       SQLReadString(oDR, "EvrakTarihi", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cEvrakTarihi, 50) And
                                                       SQLReadString(oDR, "Tutar", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cTutar, 50) And
                                                       SQLReadString(oDR, "KDV", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cKDV, 50) And
                                                       SQLReadString(oDR, "Toplam", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cToplam, 50) And
                                                       SQLReadString(oDR, "DovizCinsi", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cDovizCinsi, 50) And
                                                       SQLReadString(oDR, "Kur", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cKur, 50) And
                                                       SQLReadString(oDR, "TLTutar", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cTLTutar, 50) And
                                                       SQLReadString(oDR, "VergiNo", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cVergiNo, 50) And
                                                       SQLReadString(oDR, "ValorTarihi", 50) = AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cValorTarihi, 50))
                                    End If
                                    oDR.Close()

                                    If lExists Then
                                        If lUpdate Then
                                            cSQL = "update abitdetay set " +
                                                    " Aciklama = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cAciklama, 50) + "', " +
                                                    " EvrakNo = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cEvrakNo, 50) + "', " +
                                                    " EvrakTarihi = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cEvrakTarihi, 50) + "', " +
                                                    " Tutar = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cTutar, 50) + "', " +
                                                    " KDV = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cKDV, 50) + "', " +
                                                    " Toplam = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cToplam, 50) + "', " +
                                                    " DovizCinsi = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cDovizCinsi, 50) + "', " +
                                                    " Kur = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cKur, 50) + "', " +
                                                    " TLTutar = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cTLTutar, 50) + "', " +
                                                    " VergiNo = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cVergiNo, 50) + "', " +
                                                    " ValorTarihi = '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cValorTarihi, 50) + "', " +
                                                    " modificationdate = getdate() "

                                            cSQL = cSQL +
                                                   " where UnspedDosyaNo = '" + AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + "' " +
                                                   " and EvrakNo = '" + AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cEvrakNo) + "' " +
                                                   " and AciklamaKod = '" + AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cAciklamaKod) + "' " +
                                                   " and evraktipi = 'FATURAKALEM' "

                                            ExecuteSQLCommandConnected(cSQL, ConnYage)

                                            ofrmstatus.ShowMessage("Fatura Kalem Update : " +
                                                                   AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + " / " +
                                                                   AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cEvrakNo) + " / " +
                                                                   AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cAciklamaKod))
                                        End If
                                    Else
                                        cSQL = "insert abitdetay (UnspedDosyaNo, MDKodu, evraktipi, IslemNo, SatirNo, " +
                                                                " EvrakTuru, AciklamaKod, Aciklama, EvrakNo, EvrakTarihi, " +
                                                                " Tutar, KDV, Toplam, DovizCinsi, Kur, " +
                                                                " TLTutar, VergiNo, ValorTarihi, ABITCounter, modificationdate, " +
                                                                " creationdate, ugmgiris ) "

                                        cSQL = cSQL +
                                                " values ('" + SQLWriteString(oBeyannameler(nCnt).cUnspedDosyaNo, 50) + "', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cMDKodu, 50) + "', " +
                                                " 'FATURAKALEM', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cIslemNo, 50) + "', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cSatirNo, 50) + "', "

                                        cSQL = cSQL +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cEvrakTuru, 50) + "', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cAciklamaKod, 50) + "', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cAciklama, 50) + "', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cEvrakNo, 50) + "', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cEvrakTarihi, 50) + "', "

                                        cSQL = cSQL +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cTutar, 50) + "', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cKDV, 50) + "', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cToplam, 50) + "', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cDovizCinsi, 50) + "', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cKur, 50) + "', "

                                        cSQL = cSQL +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cTLTutar, 50) + "', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cVergiNo, 50) + "', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cValorTarihi, 50) + "', " +
                                                " '" + SQLWriteString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cABITCounter, 50) + "', " +
                                                " getdate(), "

                                        cSQL = cSQL +
                                                " getdate(), " +
                                                " '" + SQLWriteDate(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).dUGMGiris) + "') "

                                        ExecuteSQLCommandConnected(cSQL, ConnYage)

                                        ofrmstatus.ShowMessage("Fatura Kalem Insert : " +
                                                                   AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + " / " +
                                                                   AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).cEvrakNo) + " / " +
                                                                   AbitReadString(oBeyannameler(nCnt).aFaturaItem(nCnt1).aFaturaKalemItem(nCnt2).cAciklamaKod))
                                    End If
                                End If
                            Next
                        End If
                    Next

                    For nCnt1 = 0 To UBound(oBeyannameler(nCnt).aDekontItem)

                        If AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakNo) <> "" Then

                            lExists = False
                            lUpdate = False

                            cSQL = "select * " +
                                   " from abitdetay with (NOLOCK) " +
                                   " where UnspedDosyaNo = '" + AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + "' " +
                                   " and EvrakNo = '" + AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakNo) + "' " +
                                   " and AciklamaKod = '" + AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cAciklamaKod) + "' " +
                                   " and evraktipi = 'DEKONT' "

                            oDR = GetSQLReader(cSQL, ConnYage)

                            If oDR.Read Then
                                lExists = True

                                lUpdate = Not (SQLReadString(oDR, "Aciklama", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cAciklama, 50) And
                                               SQLReadString(oDR, "EvrakTarihi", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakTarihi, 50) And
                                               SQLReadString(oDR, "Tutar", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cTutar, 50) And
                                               SQLReadString(oDR, "KDV", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cKDV, 50) And
                                               SQLReadString(oDR, "Toplam", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cToplam, 50) And
                                               SQLReadString(oDR, "DovizCinsi", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cDovizCinsi, 50) And
                                               SQLReadString(oDR, "Kur", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cKur, 50) And
                                               SQLReadString(oDR, "TLTutar", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cTLTutar, 50) And
                                               SQLReadString(oDR, "VergiNo", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cVergiNo, 50) And
                                               SQLReadString(oDR, "ValorTarihi", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cValorTarihi, 50))
                            End If
                            oDR.Close()

                            If lExists Then
                                If lUpdate Then
                                    cSQL = "update abitdetay set " +
                                            " Aciklama = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cAciklama, 50) + "', " +
                                            " EvrakNo = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakNo, 50) + "', " +
                                            " EvrakTarihi = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakTarihi, 50) + "', " +
                                            " Tutar = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cTutar, 50) + "', " +
                                            " KDV = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cKDV, 50) + "', " +
                                            " Toplam = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cToplam, 50) + "', " +
                                            " DovizCinsi = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cDovizCinsi, 50) + "', " +
                                            " Kur = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cKur, 50) + "', " +
                                            " TLTutar = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cTLTutar, 50) + "', " +
                                            " VergiNo = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cVergiNo, 50) + "', " +
                                            " ValorTarihi = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cValorTarihi, 50) + "', " +
                                            " modificationdate = getdate() "

                                    cSQL = cSQL +
                                           " where UnspedDosyaNo = '" + AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + "' " +
                                           " and EvrakNo = '" + AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakNo) + "' " +
                                           " and AciklamaKod = '" + AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cAciklamaKod) + "' " +
                                           " and evraktipi = 'DEKONT' "

                                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                                    ofrmstatus.ShowMessage("Dekont Update : " +
                                                           AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + " / " +
                                                           AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakNo))
                                End If
                            Else
                                cSQL = "insert abitdetay (UnspedDosyaNo, MDKodu, evraktipi, IslemNo, SatirNo, " +
                                                        " EvrakTuru, AciklamaKod, Aciklama, EvrakNo, EvrakTarihi, " +
                                                        " Tutar, KDV, Toplam, DovizCinsi, Kur, " +
                                                        " TLTutar, VergiNo, ValorTarihi, ABITCounter, modificationdate, " +
                                                        " creationdate, ugmgiris) "
                                cSQL = cSQL +
                                        " values ('" + SQLWriteString(oBeyannameler(nCnt).cUnspedDosyaNo, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cMDKodu, 50) + "', " +
                                        " 'DEKONT', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cIslemNo, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cSatirNo, 50) + "', "

                                cSQL = cSQL +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakTuru, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cAciklamaKod, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cAciklama, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakNo, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakTarihi, 50) + "', "

                                cSQL = cSQL +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cTutar, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cKDV, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cToplam, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cDovizCinsi, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cKur, 50) + "', "

                                cSQL = cSQL +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cTLTutar, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cVergiNo, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cValorTarihi, 50) + "', " +
                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).cABITCounter, 50) + "', " +
                                        " getdate(), "

                                cSQL = cSQL +
                                        " getdate(), " +
                                        " '" + SQLWriteDate(oBeyannameler(nCnt).aDekontItem(nCnt1).dUGMGiris) + "') "

                                ExecuteSQLCommandConnected(cSQL, ConnYage)

                                ofrmstatus.ShowMessage("Dekont insert : " +
                                                           AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + " / " +
                                                           AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakNo))
                            End If

                            For nCnt2 = 0 To UBound(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem)

                                If AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cEvrakNo) <> "" Then

                                    lExists = False
                                    lUpdate = False

                                    cSQL = "select * " +
                                           " from abitdetay with (NOLOCK) " +
                                           " where UnspedDosyaNo = '" + oBeyannameler(nCnt).cUnspedDosyaNo.Trim + "' " +
                                           " and EvrakNo = '" + oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cEvrakNo.Trim + "' " +
                                           " and AciklamaKod = '" + oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cAciklamaKod.Trim + "' " +
                                           " and evraktipi = 'DEKONTKALEM' "

                                    oDR = GetSQLReader(cSQL, ConnYage)

                                    If oDR.Read Then
                                        lExists = True

                                        lUpdate = Not (SQLReadString(oDR, "Aciklama", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cAciklama, 50) And
                                                       SQLReadString(oDR, "EvrakTarihi", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cEvrakTarihi, 50) And
                                                       SQLReadString(oDR, "Tutar", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cTutar, 50) And
                                                       SQLReadString(oDR, "KDV", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cKDV, 50) And
                                                       SQLReadString(oDR, "Toplam", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cToplam, 50) And
                                                       SQLReadString(oDR, "DovizCinsi", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cDovizCinsi, 50) And
                                                       SQLReadString(oDR, "Kur", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cKur, 50) And
                                                       SQLReadString(oDR, "TLTutar", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cTLTutar, 50) And
                                                       SQLReadString(oDR, "VergiNo", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cVergiNo, 50) And
                                                       SQLReadString(oDR, "ValorTarihi", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cValorTarihi, 50))
                                    End If
                                    oDR.Close()

                                    If lExists Then
                                        If lUpdate Then
                                            cSQL = "update abitdetay set " +
                                                    " Aciklama = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cAciklama, 50) + "', " +
                                                    " EvrakNo = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cEvrakNo, 50) + "', " +
                                                    " EvrakTarihi = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cEvrakTarihi, 50) + "', " +
                                                    " Tutar = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cTutar, 50) + "', " +
                                                    " KDV = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cKDV, 50) + "', " +
                                                    " Toplam = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cToplam, 50) + "', " +
                                                    " DovizCinsi = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cDovizCinsi, 50) + "', " +
                                                    " Kur = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cKur, 50) + "', " +
                                                    " TLTutar = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cTLTutar, 50) + "', " +
                                                    " VergiNo = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cVergiNo, 50) + "', " +
                                                    " ValorTarihi = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cValorTarihi, 50) + "', " +
                                                    " modificationdate = getdate() "

                                            cSQL = cSQL +
                                                   " where UnspedDosyaNo = '" + AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + "' " +
                                                   " and EvrakNo = '" + AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cEvrakNo) + "' " +
                                                   " and AciklamaKod = '" + AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cAciklamaKod) + "' " +
                                                   " and evraktipi = 'DEKONTKALEM' "

                                            ExecuteSQLCommandConnected(cSQL, ConnYage)

                                            ofrmstatus.ShowMessage("Dekont kalem update : " +
                                                               AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + " / " +
                                                               AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakNo) + " / " +
                                                               AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cAciklamaKod))
                                        End If
                                    Else
                                        cSQL = "insert abitdetay (UnspedDosyaNo, MDKodu, evraktipi, IslemNo, SatirNo, " +
                                                                " EvrakTuru, AciklamaKod, Aciklama, EvrakNo, EvrakTarihi, " +
                                                                " Tutar, KDV, Toplam, DovizCinsi, Kur, " +
                                                                " TLTutar, VergiNo, ValorTarihi, ABITCounter, modificationdate, " +
                                                                " creationdate, ugmgiris) "

                                        cSQL = cSQL +
                                            " values ('" + SQLWriteString(oBeyannameler(nCnt).cUnspedDosyaNo, 50) + "', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cMDKodu, 50) + "', " +
                                            " 'DEKONTKALEM', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cIslemNo, 50) + "', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cSatirNo, 50) + "', "

                                        cSQL = cSQL +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cEvrakTuru, 50) + "', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cAciklamaKod, 50) + "', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cAciklama, 50) + "', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cEvrakNo, 50) + "', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cEvrakTarihi, 50) + "', "

                                        cSQL = cSQL +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cTutar, 50) + "', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cKDV, 50) + "', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cToplam, 50) + "', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cDovizCinsi, 50) + "', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cKur, 50) + "', "

                                        cSQL = cSQL +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cTLTutar, 50) + "', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cVergiNo, 50) + "', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cValorTarihi, 50) + "', " +
                                            " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cABITCounter, 50) + "', " +
                                            " getdate(), "

                                        cSQL = cSQL +
                                            " getdate(), " +
                                            " '" + SQLWriteDate(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).dUGMGiris) + "') "

                                        ExecuteSQLCommandConnected(cSQL, ConnYage)

                                        ofrmstatus.ShowMessage("Dekont kalem insert : " +
                                                               AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + " / " +
                                                               AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakNo) + " / " +
                                                               AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cAciklamaKod))
                                    End If

                                    For nCnt3 = 0 To UBound(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem)

                                        If AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cEvrakNo) <> "" Then

                                            lExists = False
                                            lUpdate = False

                                            cSQL = "select * " +
                                                   " from abitdetay with (NOLOCK) " +
                                                   " where UnspedDosyaNo = '" + AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + "' " +
                                                   " and EvrakNo = '" + AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cEvrakNo) + "' " +
                                                   " and AciklamaKod = '" + AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cAciklamaKod) + "' " +
                                                   " and evraktipi = 'DEKONTKALEMDETAY' "

                                            oDR = GetSQLReader(cSQL, ConnYage)

                                            If oDR.Read Then
                                                lExists = True

                                                lUpdate = Not (SQLReadString(oDR, "Aciklama", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cAciklama, 50) And
                                                               SQLReadString(oDR, "EvrakTarihi", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cEvrakTarihi, 50) And
                                                               SQLReadString(oDR, "Tutar", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cTutar, 50) And
                                                               SQLReadString(oDR, "KDV", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cKDV, 50) And
                                                               SQLReadString(oDR, "Toplam", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cToplam, 50) And
                                                               SQLReadString(oDR, "DovizCinsi", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cDovizCinsi, 50) And
                                                               SQLReadString(oDR, "Kur", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cKur, 50) And
                                                               SQLReadString(oDR, "TLTutar", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cTLTutar, 50) And
                                                               SQLReadString(oDR, "VergiNo", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cVergiNo, 50) And
                                                               SQLReadString(oDR, "ValorTarihi", 50) = AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cValorTarihi, 50))
                                            End If
                                            oDR.Close()

                                            If lExists Then
                                                If lUpdate Then
                                                    cSQL = "update abitdetay set " +
                                                    " Aciklama = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cAciklama, 50) + "', " +
                                                    " EvrakNo = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cEvrakNo, 50) + "', " +
                                                    " EvrakTarihi = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cEvrakTarihi, 50) + "', " +
                                                    " Tutar = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cTutar, 50) + "', " +
                                                    " KDV = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cKDV, 50) + "', " +
                                                    " Toplam = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cToplam, 50) + "', " +
                                                    " DovizCinsi = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cDovizCinsi, 50) + "', " +
                                                    " Kur = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cKur, 50) + "', " +
                                                    " TLTutar = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cTLTutar, 50) + "', " +
                                                    " VergiNo = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cVergiNo, 50) + "', " +
                                                    " ValorTarihi = '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cValorTarihi, 50) + "', " +
                                                    " modificationdate = getdate() "

                                                    cSQL = cSQL +
                                                   " where UnspedDosyaNo = '" + AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + "' " +
                                                   " and EvrakNo = '" + AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cEvrakNo) + "' " +
                                                   " and AciklamaKod = '" + AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cAciklamaKod) + "' " +
                                                   " and evraktipi = 'DEKONTKALEMDETAY' "

                                                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                                                    ofrmstatus.ShowMessage("Dekont kalem detay update : " +
                                                          AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + " / " +
                                                          AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakNo) + " / " +
                                                          AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cAciklamaKod) + " / " +
                                                          AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cAciklamaKod))
                                                End If
                                            Else
                                                cSQL = "insert abitdetay (UnspedDosyaNo, MDKodu, evraktipi, IslemNo, SatirNo, " +
                                                                        " EvrakTuru, AciklamaKod, Aciklama, EvrakNo, EvrakTarihi, " +
                                                                        " Tutar, KDV, Toplam, DovizCinsi, Kur, " +
                                                                        " TLTutar, VergiNo, ValorTarihi, ABITCounter, modificationdate, " +
                                                                        " creationdate, ugmgiris) "

                                                cSQL = cSQL +
                                                        " values ('" + SQLWriteString(oBeyannameler(nCnt).cUnspedDosyaNo, 50) + "', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cMDKodu, 50) + "', " +
                                                        " 'DEKONTKALEMDETAY', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cIslemNo, 50) + "', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cSatirNo, 50) + "', "

                                                cSQL = cSQL +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cEvrakTuru, 50) + "', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cAciklamaKod, 50) + "', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cAciklama, 50) + "', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cEvrakNo, 50) + "', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cEvrakTarihi, 50) + "', "

                                                cSQL = cSQL +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cTutar, 50) + "', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cKDV, 50) + "', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cToplam, 50) + "', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cDovizCinsi, 50) + "', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cKur, 50) + "', "

                                                cSQL = cSQL +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cTLTutar, 50) + "', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cVergiNo, 50) + "', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cValorTarihi, 50) + "', " +
                                                        " '" + SQLWriteString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cABITCounter, 50) + "', " +
                                                        " getdate(), "

                                                cSQL = cSQL +
                                                        " getdate(), " +
                                                        " '" + SQLWriteDate(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).dUGMGiris) + "') "

                                                ExecuteSQLCommandConnected(cSQL, ConnYage)

                                                ofrmstatus.ShowMessage("Dekont kalem detay insert : " +
                                                          AbitReadString(oBeyannameler(nCnt).cUnspedDosyaNo) + " / " +
                                                          AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).cEvrakNo) + " / " +
                                                          AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).cAciklamaKod) + " / " +
                                                          AbitReadString(oBeyannameler(nCnt).aDekontItem(nCnt1).aDekontKalemItem(nCnt2).aDekontKalemDetayItem(nCnt3).cAciklamaKod))
                                            End If
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next

            ConnYage.Close()

            If cEvrakTipi = "H" Then
                ofrmstatus.ShowMessage("Ihracat Yazma işlemi tamamlandı")
                ofrmstatus.ShowMessage("Ihracat Okunan beyanname adedi : " + UBound(oBeyannameler).ToString)
                ofrmstatus.ShowMessage("Ihracat Eklenen beyanname adedi : " + nCntYazilan.ToString)
            Else
                ofrmstatus.ShowMessage("Ithalat Yazma işlemi tamamlandı")
                ofrmstatus.ShowMessage("Ithalat Okunan beyanname adedi : " + UBound(oBeyannameler).ToString)
                ofrmstatus.ShowMessage("Ithalat Eklenen beyanname adedi : " + nCntYazilan.ToString)
            End If

            WriteBeyannameler = True

        Catch ex As Exception
            ErrDisp("WriteBeyannameler", "UtilAbit", cSQL,, ex)
        End Try
    End Function
    Private Function ReadBeyannameler(cEvrakTipi As String) As Boolean

        Dim nCnt As Integer = 0
        Dim nCnt1 As Integer = 0
        Dim nCnt2 As Integer = 0
        Dim nCnt3 As Integer = 0
        Dim nCntBeyanname As Integer = -1
        Dim dFirstDate As Date = #1/1/1950#
        Dim dEndDate As Date = #1/1/1950#
        Dim dEndDate2 As Date = #1/1/1950#
        Dim cFirstDate As String = ""
        Dim cEndDate As String = ""
        Dim cEndDate2 As String = ""

        Dim oClient As ServiceRefAbit.AbitWsdlSoapClient
        Dim oUser As ServiceRefAbit.PrmUserWithDate
        Dim oAbitListBeyanname As ServiceRefAbit.ListBeyanname = Nothing
        Dim oAbitBeyanname As ServiceRefAbit.BEYANNAME = Nothing
        Dim oBinding As System.ServiceModel.BasicHttpBinding
        Dim oEPAddress As System.ServiceModel.EndpointAddress

        Dim aFaturaItem() As oFaturaItem
        Dim aFaturaKalemItem() As oFaturaKalemItem
        Dim aDekontItem() As oDekontItem
        Dim aDekontKalemItem() As oDekontKalemItem
        Dim aDekontKalemDetayItem() As oDekontKalemDetayItem

        ReadBeyannameler = False

        Try
            ReDim oBeyannameler(0)

            If dBasla = #1/1/1950# Then
                dFirstDate = Now.AddDays(-365)
            Else
                dFirstDate = dBasla
            End If

            If dBitis = #1/1/1950# Then
                dEndDate = Now
            Else
                dEndDate = dBitis ' now
            End If

            oBinding = New ServiceModel.BasicHttpBinding(securityMode:=ServiceModel.BasicHttpSecurityMode.None)
            oBinding.Name = "AbitWsdlSoap"
            oBinding.MaxBufferSize = 2147483647
            oBinding.MaxReceivedMessageSize = 2147483647
            oBinding.ReaderQuotas.MaxStringContentLength = 2147483647

            oEPAddress = New ServiceModel.EndpointAddress("http://ws.ugm.com.tr/UgmGenelWebServis/AbitWsdl.asmx")

            oUser = New ServiceRefAbit.PrmUserWithDate
            oUser.KullaniciAdi = oConnection.cAbitUser
            oUser.Sifre = oConnection.cAbitPassword
            oUser.Tip = cEvrakTipi
            oUser.MusteriRefNo = ""

            oClient = New ServiceRefAbit.AbitWsdlSoapClient(oBinding, oEPAddress)
            oClient.Open()
            oClient.InnerChannel.OperationTimeout = New TimeSpan(0, 10, 0)

            Do While dFirstDate <= dEndDate

                If cEvrakTipi = "H" Then
                    ofrmstatus.ShowMessage("Ihracat aranan tarih " + DateValue(dFirstDate.ToString).ToString)
                Else
                    ofrmstatus.ShowMessage("Ithalat aranan tarih " + DateValue(dFirstDate.ToString).ToString)
                End If

                cFirstDate = Format(Day(dFirstDate), "00") + "." + Format(Month(dFirstDate), "00") + "." + Format(Year(dFirstDate), "0000")
                oUser.FirstDate = cFirstDate

                dEndDate2 = dFirstDate.AddDays(1)
                cEndDate = Format(Day(dEndDate2), "00") + "." + Format(Month(dEndDate2), "00") + "." + Format(Year(dEndDate2), "0000")
                oUser.EndDate = cEndDate

                oAbitListBeyanname = oClient.GetAllAbitData(oUser).objData

                For nCnt = 0 To UBound(oAbitListBeyanname.Beyan)

                    nCntBeyanname = nCntBeyanname + 1
                    ReDim Preserve oBeyannameler(nCntBeyanname)
                    oBeyannameler(nCntBeyanname).cIslemNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).ISLEMNO)
                    oBeyannameler(nCntBeyanname).cSatirNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).SATIRNO)
                    oBeyannameler(nCntBeyanname).cMdKodu = AbitReadString(oAbitListBeyanname.Beyan(nCnt).MDKODU)
                    oBeyannameler(nCntBeyanname).cEvrakTuru = AbitReadString(oAbitListBeyanname.Beyan(nCnt).EVRAKTURU)
                    oBeyannameler(nCntBeyanname).cUnspedDosyaNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).UNSPEDDOSYANO)
                    oBeyannameler(nCntBeyanname).cMusteriRefNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).MUSTERIREFNO)
                    oBeyannameler(nCntBeyanname).cBeyannameNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).BEYANNAMENO)
                    oBeyannameler(nCntBeyanname).cBeyannameTarihi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).BEYANNAMETARIHI)
                    oBeyannameler(nCntBeyanname).cGumrukAdi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).GUMRUKADI)
                    oBeyannameler(nCntBeyanname).cGumrukKodu = AbitReadString(oAbitListBeyanname.Beyan(nCnt).GUMRUKKODU)
                    oBeyannameler(nCntBeyanname).cMalinKiymeti = AbitReadString(oAbitListBeyanname.Beyan(nCnt).MALINKIYMETI)
                    oBeyannameler(nCntBeyanname).cDovizCinsi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DOVIZCINSI)
                    oBeyannameler(nCntBeyanname).cKonsimentoNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).KONSIMENTONO)
                    oBeyannameler(nCntBeyanname).cHouseNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).HOUSENO)
                    oBeyannameler(nCntBeyanname).cYurtDisiMusteriNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).YURTDISIMUSTERINO)
                    oBeyannameler(nCntBeyanname).cYurtDisiMusteriAdi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).YURTDISIMUSTERIADI)
                    oBeyannameler(nCntBeyanname).cKapanizIntacTarihi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).KAPANISINTACTARIHI)
                    oBeyannameler(nCntBeyanname).cUlke = AbitReadString(oAbitListBeyanname.Beyan(nCnt).ULKE)
                    oBeyannameler(nCntBeyanname).cMalinFaturaNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).MALINFATURANO)
                    oBeyannameler(nCntBeyanname).cVasitaAdi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).VASITAADI)
                    oBeyannameler(nCntBeyanname).cAbitCounter = AbitReadString(oAbitListBeyanname.Beyan(nCnt).ABITCOUNTER)
                    oBeyannameler(nCntBeyanname).dUGMGiris = dFirstDate

                    If Not (oAbitListBeyanname.Beyan(nCnt).FATURA Is Nothing) Then

                        ReDim aFaturaItem(0)
                        For nCnt1 = 0 To UBound(oAbitListBeyanname.Beyan(nCnt).FATURA)

                            ReDim Preserve aFaturaItem(nCnt1)
                            aFaturaItem(nCnt1).cIslemNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).ISLEMNO)
                            aFaturaItem(nCnt1).cSatirNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).SATIRNO)
                            aFaturaItem(nCnt1).cMDKodu = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).MDKODU)
                            aFaturaItem(nCnt1).cEvrakTuru = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).EVRAKTURU)
                            aFaturaItem(nCnt1).cAciklamaKod = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).ACIKLAMAKOD)
                            aFaturaItem(nCnt1).cAciklama = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).ACIKLAMA)
                            aFaturaItem(nCnt1).cEvrakNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).EVRAKNO)
                            aFaturaItem(nCnt1).cEvrakTarihi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).EVRAKTARIHI)
                            aFaturaItem(nCnt1).cTutar = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).TUTAR)
                            aFaturaItem(nCnt1).cKDV = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).KDV)
                            aFaturaItem(nCnt1).cToplam = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).TOPLAM)
                            aFaturaItem(nCnt1).cDovizCinsi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).DOVIZCINSI)
                            aFaturaItem(nCnt1).cKur = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).KUR)
                            aFaturaItem(nCnt1).cTLTutar = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).TLTUTAR)
                            aFaturaItem(nCnt1).cVergiNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).VERGINO)
                            aFaturaItem(nCnt1).cValorTarihi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).VALORTARIHI)
                            aFaturaItem(nCnt1).cABITCounter = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).ABITCOUNTER)
                            aFaturaItem(nCnt1).dUGMGiris = dFirstDate

                            If Not (oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM Is Nothing) Then

                                ReDim aFaturaKalemItem(0)
                                For nCnt2 = 0 To UBound(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM)

                                    ReDim Preserve aFaturaKalemItem(nCnt2)
                                    aFaturaKalemItem(nCnt2).cIslemNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).ISLEMNO)
                                    aFaturaKalemItem(nCnt2).cSatirNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).SATIRNO)
                                    aFaturaKalemItem(nCnt2).cMDKodu = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).MDKODU)
                                    aFaturaKalemItem(nCnt2).cEvrakTuru = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).EVRAKTURU)
                                    aFaturaKalemItem(nCnt2).cAciklamaKod = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).ACIKLAMAKOD)
                                    aFaturaKalemItem(nCnt2).cAciklama = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).ACIKLAMA)
                                    aFaturaKalemItem(nCnt2).cEvrakNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).EVRAKNO)
                                    aFaturaKalemItem(nCnt2).cEvrakTarihi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).EVRAKTARIHI)
                                    aFaturaKalemItem(nCnt2).cTutar = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).TUTAR)
                                    aFaturaKalemItem(nCnt2).cKDV = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).KDV)
                                    aFaturaKalemItem(nCnt2).cToplam = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).TOPLAM)
                                    aFaturaKalemItem(nCnt2).cDovizCinsi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).DOVIZCINSI)
                                    aFaturaKalemItem(nCnt2).cKur = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).KUR)
                                    aFaturaKalemItem(nCnt2).cTLTutar = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).TLTUTAR)
                                    aFaturaKalemItem(nCnt2).cVergiNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).VERGINO)
                                    aFaturaKalemItem(nCnt2).cValorTarihi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).VALORTARIHI)
                                    aFaturaKalemItem(nCnt2).cABITCounter = AbitReadString(oAbitListBeyanname.Beyan(nCnt).FATURA(nCnt1).FATURAKALEM(nCnt2).ABITCOUNTER)
                                    aFaturaKalemItem(nCnt2).dUGMGiris = dFirstDate
                                Next
                                aFaturaItem(nCnt1).aFaturaKalemItem = aFaturaKalemItem
                            End If
                        Next
                        oBeyannameler(nCntBeyanname).aFaturaItem = aFaturaItem
                    End If

                    If Not (oAbitListBeyanname.Beyan(nCnt).DEKONT Is Nothing) Then

                        ReDim aDekontItem(0)
                        For nCnt1 = 0 To UBound(oAbitListBeyanname.Beyan(nCnt).DEKONT)

                            ReDim Preserve aDekontItem(nCnt1)
                            aDekontItem(nCnt1).cIslemNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).ISLEMNO)
                            aDekontItem(nCnt1).cSatirNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).SATIRNO)
                            aDekontItem(nCnt1).cMDKodu = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).MDKODU)
                            aDekontItem(nCnt1).cEvrakTuru = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).EVRAKTURU)
                            aDekontItem(nCnt1).cAciklamaKod = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).ACIKLAMAKOD)
                            aDekontItem(nCnt1).cAciklama = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).ACIKLAMA)
                            aDekontItem(nCnt1).cEvrakNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).EVRAKNO)
                            aDekontItem(nCnt1).cEvrakTarihi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).EVRAKTARIHI)
                            aDekontItem(nCnt1).cTutar = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).TUTAR)
                            aDekontItem(nCnt1).cKDV = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).KDV)
                            aDekontItem(nCnt1).cToplam = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).TOPLAM)
                            aDekontItem(nCnt1).cDovizCinsi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DOVIZCINSI)
                            aDekontItem(nCnt1).cKur = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).KUR)
                            aDekontItem(nCnt1).cTLTutar = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).TLTUTAR)
                            aDekontItem(nCnt1).cVergiNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).VERGINO)
                            aDekontItem(nCnt1).cValorTarihi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).VALORTARIHI)
                            aDekontItem(nCnt1).cABITCounter = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).ABITCOUNTER)
                            aDekontItem(nCnt1).dUGMGiris = dFirstDate

                            If Not (oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM Is Nothing) Then

                                ReDim aDekontKalemItem(0)
                                For nCnt2 = 0 To UBound(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM)

                                    ReDim Preserve aDekontKalemItem(nCnt2)
                                    aDekontKalemItem(nCnt2).cIslemNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).ISLEMNO)
                                    aDekontKalemItem(nCnt2).cSatirNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).SATIRNO)
                                    aDekontKalemItem(nCnt2).cMDKodu = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).MDKODU)
                                    aDekontKalemItem(nCnt2).cEvrakTuru = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).EVRAKTURU)
                                    aDekontKalemItem(nCnt2).cAciklamaKod = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).ACIKLAMAKOD)
                                    aDekontKalemItem(nCnt2).cAciklama = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).ACIKLAMA)
                                    aDekontKalemItem(nCnt2).cEvrakNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).EVRAKNO)
                                    aDekontKalemItem(nCnt2).cEvrakTarihi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).EVRAKTARIHI)
                                    aDekontKalemItem(nCnt2).cTutar = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).TUTAR)
                                    aDekontKalemItem(nCnt2).cKDV = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).KDV)
                                    aDekontKalemItem(nCnt2).cToplam = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).TOPLAM)
                                    aDekontKalemItem(nCnt2).cDovizCinsi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DOVIZCINSI)
                                    aDekontKalemItem(nCnt2).cKur = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).KUR)
                                    aDekontKalemItem(nCnt2).cTLTutar = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).TLTUTAR)
                                    aDekontKalemItem(nCnt2).cVergiNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).VERGINO)
                                    aDekontKalemItem(nCnt2).cValorTarihi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).VALORTARIHI)
                                    aDekontKalemItem(nCnt2).cABITCounter = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).ABITCOUNTER)
                                    aDekontKalemItem(nCnt2).dUGMGiris = dFirstDate

                                    If Not (oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY Is Nothing) Then

                                        ReDim aDekontKalemDetayItem(0)
                                        For nCnt3 = 0 To UBound(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY)

                                            ReDim Preserve aDekontKalemDetayItem(nCnt3)
                                            aDekontKalemDetayItem(nCnt3).cIslemNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).ISLEMNO)
                                            aDekontKalemDetayItem(nCnt3).cSatirNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).SATIRNO)
                                            aDekontKalemDetayItem(nCnt3).cMDKodu = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).MDKODU)
                                            aDekontKalemDetayItem(nCnt3).cEvrakTuru = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).EVRAKTURU)
                                            aDekontKalemDetayItem(nCnt3).cAciklamaKod = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).ACIKLAMAKOD)
                                            aDekontKalemDetayItem(nCnt3).cAciklama = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).ACIKLAMA)
                                            aDekontKalemDetayItem(nCnt3).cEvrakNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).EVRAKNO)
                                            aDekontKalemDetayItem(nCnt3).cEvrakTarihi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).EVRAKTARIHI)
                                            aDekontKalemDetayItem(nCnt3).cTutar = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).TUTAR)
                                            aDekontKalemDetayItem(nCnt3).cKDV = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).KDV)
                                            aDekontKalemDetayItem(nCnt3).cToplam = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).TOPLAM)
                                            aDekontKalemDetayItem(nCnt3).cDovizCinsi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).DOVIZCINSI)
                                            aDekontKalemDetayItem(nCnt3).cKur = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).KUR)
                                            aDekontKalemDetayItem(nCnt3).cTLTutar = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).TLTUTAR)
                                            aDekontKalemDetayItem(nCnt3).cVergiNo = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).VERGINO)
                                            aDekontKalemDetayItem(nCnt3).cValorTarihi = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).VALORTARIHI)
                                            aDekontKalemDetayItem(nCnt3).cABITCounter = AbitReadString(oAbitListBeyanname.Beyan(nCnt).DEKONT(nCnt1).DEKONTKALEM(nCnt2).DEKONTKALEMDETAY(nCnt3).ABITCOUNTER)
                                            aDekontKalemDetayItem(nCnt3).dUGMGiris = dFirstDate
                                        Next
                                        aDekontKalemItem(nCnt2).aDekontKalemDetayItem = aDekontKalemDetayItem
                                    End If
                                Next
                                aDekontItem(nCnt1).aDekontKalemItem = aDekontKalemItem
                            End If
                        Next
                        oBeyannameler(nCntBeyanname).aDekontItem = aDekontItem
                    End If

                    ofrmstatus.ShowMessage(oBeyannameler(nCntBeyanname).cBeyannameNo)
                Next
                dFirstDate = dFirstDate.AddDays(1)
            Loop

            oClient.Close()

            ofrmstatus.ShowMessage("finiş")

            ReadBeyannameler = True

        Catch ex As Exception
            ErrDisp("ReadBeyannameler", "UtilAbit",,, ex)
        End Try
    End Function

End Module
