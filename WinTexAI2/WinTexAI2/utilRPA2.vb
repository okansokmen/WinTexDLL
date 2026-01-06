Option Strict On
Option Explicit On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports Microsoft.SqlServer.Server

Module utilRPA2

    Public Structure oRPAOlcuResimleri
        Dim cResimDosyasi As String
    End Structure

    Public Structure oRPAOlcuDetay
        Dim cModelNo As String
        Dim cOlcuYeriKodu As String
        Dim cIngOlcuYeri As String
        Dim cIspOlcuYeri As String
        Dim cTrkOlcuYeri As String
        Dim cBeden As String
        Dim nOlcu As Double
        Dim nPay As Double
        Dim nSatirNo As Double
    End Structure

    Public Structure oRPAOlcu
        Dim cModelNo As String
        Dim cReferans As String
        Dim dTarih As Date
        Dim cKalipci As String
        Dim cNotlar1 As String
        Dim cNotlar2 As String
        Dim cPatern As String
        Dim cOlcuResim As String
        Dim cOlcuMailDosyasi As String
        Dim cOlcuExcelDosyasi As String
        Dim cWinTexDatabaseName As String
        Dim cSiparisNo As String
        Dim cBeden As String
        Dim cBedenler As String
        Dim cBedenSeti As String

        Dim aDetay() As oRPAOlcuDetay
        Dim aRPAOlcuResimleri() As oRPAOlcuResimleri
    End Structure

    Public Structure oRBA
        Dim cRenk As String
        Dim cBeden As String
        Dim nAdet As Double
        Dim dilkSevkTar As Date
        Dim cAmbalaj As String
        Dim cTasimaSekli As String
    End Structure

    Public Structure oRPA
        Dim cSiparisNo As String
        Dim dSiparisTarihi As Date
        Dim cSezon As String
        Dim cMusteriNo As String
        Dim cMusteriYetkili As String
        Dim cVadeTipi As String
        Dim cModelNo As String
        Dim cModelAciklama As String
        Dim nFiyat As Double
        Dim cDoviz As String
        Dim cJoinLife As String
        Dim cKarisim As String
        Dim cYikamaNotlar As String
        Dim cYabanciAdi As String
        Dim cMusteriModelNo As String
        Dim cCalismaNo As String
        Dim cRota As String
        Dim cUretimYeri As String
        Dim cUretimulkesi As String
        Dim cMusteriSiparisNo As String
        Dim cEskiModelNo As String
        Dim cEskiModel As String
        Dim cModelKopyala As String
        Dim cYeniModel As String
        Dim cAnaModelTipi As String
        Dim cMailDosyasi As String
        Dim cYikamaReceteNo As String
        Dim cOnModelNo As String
        Dim cRepeteSebebi As String
        Dim cKaynakSiparisNo As String
        Dim cRepeteTipi As String
        Dim cSorumlu As String

        Dim cWinTexDatabaseName As String
    End Structure

    Public Structure oRPAModel
        Dim cMusteriModelNo As String
        Dim cModelNo As String
        Dim cAnaModelTipi As String
        Dim cMusteriSiparisNo As String
        Dim cSiparisNo As String
        Dim cMusteriNo As String
        Dim dModelTarihi As Date
        Dim cSezon As String
        Dim cAnaModelNo As String
        Dim cModelAciklama As String
        Dim cTasarimci As String
        Dim dTasarimTarihi As Date
        Dim cAnaKumas As String
        Dim cAstar As String
        Dim cIplik As String
        Dim cDikim As String
        Dim cAciklamalar As String
        Dim cBeden As String
        Dim cBedenler As String
        Dim cYikamaKodu As String
        Dim cYikamaKumas As String
        Dim cYikamaRenk As String
        Dim cYikamaRecme As String
        Dim cYikamaFermuar As String
        Dim cYikamaIplik As String
        Dim cTransfered As String
        Dim cTransferUser As String
        Dim dTransferDate As Date
        Dim cCreateUser As String
        Dim dCreationDate As Date
        Dim cMailBilgileri As String
        Dim cRobotOkudu As String
        Dim cModelResim As String
        Dim cModelDikimResim As String
        Dim cModelMailDosyasi As String
        Dim cYikamaOnResim As String
        Dim cYikamaArkaResim As String
        Dim cUretimUlkesi As String
        Dim cTechpackTipi As String
        Dim cEskiModelNo As String
        Dim cRotaSablonu As String
        Dim cEskiModel As String
        Dim cModelKopyala As String
        Dim cYeniModel As String
        Dim cTechPackPdf As String

        Dim cWinTexDatabaseName As String
    End Structure

    Public Function RPASiparis1() As Integer

        RPASiparis1 = 0

        Try
            Dim aBeden() As String
            Dim aRPA() As oRPA
            Dim aRBA() As oRBA
            Dim aMusteriSiparisNo() As String
            Dim aSiparisNo2() As String
            Dim lOK2 As Boolean = False
            Dim cMessage As String = ""
            Dim nCntSiparisNo2 As Integer = -1
            Dim nCnt5 As Integer = -1
            Dim nCnt4 As Integer = -1
            Dim nCnt3 As Integer = -1
            Dim nCnt2 As Integer = -1
            Dim nCnt As Integer = -1
            Dim cSQL As String = ""
            Dim cSiparisNo As String = ""
            Dim cSiparisNo2 As String = ""
            Dim nSipCnt As Integer = 0
            Dim dSiparisTarihi As Date = Now.Date
            Dim cSezon As String = ""
            Dim cMusteriNo As String = ""
            Dim cMusteriYetkili As String = ""
            Dim cVadeTipi As String = ""
            Dim cModelNo As String = ""
            Dim cModelAciklama As String = ""
            Dim nFiyat As Double = 0
            Dim cDoviz As String = ""
            Dim cSertifikaTipi As String = ""
            Dim cSertifikaTipi2 As String = ""
            Dim cSertifikaTipi3 As String = ""
            Dim cSertifikaTipi4 As String = ""
            Dim cSertifikaTipi5 As String = ""
            Dim cSertifikaTipi6 As String = ""
            Dim cJoinlife As String = ""
            Dim cKarisim As String = ""
            Dim cYikamaNotlar As String = ""
            Dim cYabanciAdi As String = ""
            Dim cMusteriModelno As String = ""
            Dim cCalismaNo As String = ""
            Dim cOnsiparisNo As String = ""
            Dim nOnSiparisUlke2SiraNo As Double = 0
            Dim cRota As String = ""
            Dim cUretimYeri As String = ""
            Dim cUretimUlkesi As String = ""
            Dim cBeden As String = ""
            Dim cRenk As String = ""
            Dim cBedenSeti As String = ""
            Dim lOK As Boolean = False
            Dim nAdet As Double = 0
            Dim dilksevktar As Date = #1/1/1950#
            Dim cAmbalaj As String = ""
            Dim cTasimasekli As String = ""
            Dim cMusteriSiparisNo As String = ""
            Dim dilkSevkTarihiSip As Date = #1/1/1950#
            Dim cEskiModelNo As String = ""
            Dim nSonuc As Integer = 1
            Dim cAnaModelTipi As String = ""
            Dim cBedenler As String = ""
            Dim nSTCnt As Integer = 1
            Dim nFound As Integer = -1

            Dim ConnYage As SqlConnection
            Dim oReader As SqlDataReader

            ReDim aSiparisNo2(0)
            ReDim aRPA(0)

            ConnYage = OpenConn()

            'JustForLog("RPA Start : " + ConnYage.ConnectionString)

            cSQL = "select siparisno, siparistarihi, sezon, musterino, musteriyetkili, " +
                    " vadetipi, modelno, modelaciklama, fiyat, doviz, " +
                    " joinlife, karisim, yikamanotlar, yabanciadi, musterimodelno, " +
                    " calismano, rota, uretimyeri, uretimulkesi, musterisiparisno, " +
                    " eskimodelno, eskimodel, modelkopyala, yenimodel, anamodeltipi,  " +
                    " maildosyasi, yikamareceteno, onmodelno, repetesebebi, kaynaksiparisno, " +
                    " repetetipi, createuser "

            cSQL = cSQL +
                    " from ROBO.dbo.rpa_siparis with (NOLOCK) " +
                    " where robotokudu = 'E' " +
                    " and (transfered is null or transfered = '' or transfered = 'H') " +
                    " and modelno is not null " +
                    " and modelno <> '' " +
                    " and siparisno is not null " +
                    " and siparisno <> '' " +
                    " and uretimulkesi is not null " +
                    " and uretimulkesi <> '' "

            cSQL = cSQL +
                    " and exists (select siparisno " +
                                " from ROBO.dbo.rpa_sipmodel with (NOLOCK) " +
                                " where siparisno = ROBO.dbo.rpa_siparis.siparisno) " +
                    " order by siparisno "

            oReader = GetSQLReader(cSQL, ConnYage)

            Do While oReader.Read
                nCnt = nCnt + 1
                ReDim Preserve aRPA(nCnt)

                aRPA(nCnt).cSiparisNo = SQLReadString(oReader, "siparisno")
                aRPA(nCnt).dSiparisTarihi = SQLReadDate(oReader, "siparistarihi")
                aRPA(nCnt).cSezon = SQLReadString(oReader, "sezon")
                aRPA(nCnt).cMusteriNo = SQLReadString(oReader, "musterino")
                aRPA(nCnt).cMusteriYetkili = SQLReadString(oReader, "musteriyetkili")

                aRPA(nCnt).cVadeTipi = SQLReadString(oReader, "vadetipi")
                aRPA(nCnt).cModelNo = SQLReadString(oReader, "modelno")
                aRPA(nCnt).cModelAciklama = SQLReadString(oReader, "modelaciklama")
                aRPA(nCnt).nFiyat = SQLReadDouble(oReader, "fiyat")
                aRPA(nCnt).cDoviz = SQLReadString(oReader, "doviz")

                aRPA(nCnt).cJoinLife = SQLReadString(oReader, "joinlife")
                aRPA(nCnt).cKarisim = SQLReadString(oReader, "karisim")
                aRPA(nCnt).cYikamaNotlar = SQLReadString(oReader, "yikamanotlar")
                aRPA(nCnt).cYabanciAdi = SQLReadString(oReader, "yabanciadi")
                aRPA(nCnt).cMusteriModelNo = SQLReadString(oReader, "musterimodelno")

                aRPA(nCnt).cCalismaNo = SQLReadString(oReader, "calismano")
                aRPA(nCnt).cRota = SQLReadString(oReader, "rota")
                aRPA(nCnt).cUretimYeri = SQLReadString(oReader, "uretimyeri")
                aRPA(nCnt).cUretimulkesi = SQLReadString(oReader, "uretimulkesi")
                aRPA(nCnt).cMusteriSiparisNo = SQLReadString(oReader, "musterisiparisno")

                aRPA(nCnt).cEskiModelNo = SQLReadString(oReader, "eskimodelno")
                aRPA(nCnt).cEskiModel = SQLReadString(oReader, "eskimodel")
                aRPA(nCnt).cModelKopyala = SQLReadString(oReader, "modelkopyala")
                aRPA(nCnt).cYeniModel = SQLReadString(oReader, "yenimodel")
                aRPA(nCnt).cAnaModelTipi = SQLReadString(oReader, "anamodeltipi")

                aRPA(nCnt).cMailDosyasi = SQLReadString(oReader, "maildosyasi")
                aRPA(nCnt).cYikamaReceteNo = SQLReadString(oReader, "yikamareceteno")
                aRPA(nCnt).cOnModelNo = SQLReadString(oReader, "onmodelno")
                aRPA(nCnt).cRepeteSebebi = SQLReadString(oReader, "repetesebebi")
                aRPA(nCnt).cKaynakSiparisNo = SQLReadString(oReader, "kaynaksiparisno")

                aRPA(nCnt).cRepeteTipi = SQLReadString(oReader, "repetetipi")
                aRPA(nCnt).cSorumlu = SQLReadString(oReader, "createuser")

                If aRPA(nCnt).cEskiModel = "" Then
                    aRPA(nCnt).cEskiModel = "H"
                Else
                    aRPA(nCnt).cEskiModel = aRPA(nCnt).cEskiModel.ToUpper
                End If

                If aRPA(nCnt).cModelKopyala = "" Then
                    aRPA(nCnt).cModelKopyala = "H"
                Else
                    aRPA(nCnt).cModelKopyala = aRPA(nCnt).cModelKopyala.ToUpper
                End If

                If aRPA(nCnt).cYeniModel = "" Then
                    aRPA(nCnt).cYeniModel = "H"
                Else
                    aRPA(nCnt).cYeniModel = aRPA(nCnt).cYeniModel.ToUpper
                End If

                If aRPA(nCnt).cEskiModel = "H" And aRPA(nCnt).cModelKopyala = "H" Then
                    aRPA(nCnt).cYeniModel = "E"
                End If

                If aRPA(nCnt).cUretimulkesi = "MISIR" Then
                    aRPA(nCnt).cWinTexDatabaseName = "MISIR.dbo."
                Else
                    aRPA(nCnt).cWinTexDatabaseName = "TES.dbo."
                End If

                'JustForLog("RPA Sip Oku : " + aRPA(nCnt).cSiparisNo + " " + aRPA(nCnt).cWinTexDatabaseName)

                lOK2 = True
            Loop
            oReader.Close()

            If Not lOK2 Then
                ConnYage.Close()
                RPASiparis1 = 1
                'JustForLog("RPA Exit")

                Exit Function
            End If

            For nCnt = 0 To aRPA.GetUpperBound(0)
                ' process bedenler
                cBedenler = ""

                cSQL = "select beden, sirano " +
                        " from ROBO.dbo.rpa_sipmodel with (NOLOCK) " +
                        " where siparisno = '" + aRPA(nCnt).cSiparisNo + "' " +
                        " and beden is not null " +
                        " and beden <> '' " +
                        " order by sirano "

                oReader = GetSQLReader(cSQL, ConnYage)

                Do While oReader.Read

                    cBeden = SQLReadString(oReader, "beden")

                    If cBedenler.Trim = "" Then
                        cBedenler = cBeden
                    Else
                        cBedenler = cBedenler + "," + cBeden
                    End If
                Loop
                oReader.Close()

                ' model kartı

                cBedenSeti = CheckBedenSeti(ConnYage, cBedenler, aRPA(nCnt).cWinTexDatabaseName.Trim, aRPA(nCnt).cModelNo.Trim, aRPA(nCnt).cSiparisNo.Trim)

                If aRPA(nCnt).cEskiModel = "E" Then
                    ' eski model aynen kullanılacak
                ElseIf aRPA(nCnt).cModelKopyala = "E" Then
                    ' eski modelden yeni model kopyalanarak olusturulacak
                    ModelKopyala(ConnYage, aRPA(nCnt).cWinTexDatabaseName.Trim, aRPA(nCnt).cModelNo.Trim, aRPA(nCnt).cEskiModelNo.Trim)
                Else
                    ' yeni model kartını aç 
                    YeniModel(ConnYage, aRPA(nCnt).cWinTexDatabaseName, aRPA(nCnt).cModelNo, aRPA(nCnt).cModelAciklama, aRPA(nCnt).nFiyat, aRPA(nCnt).cDoviz,
                              aRPA(nCnt).cMusteriNo, aRPA(nCnt).cMusteriModelNo, aRPA(nCnt).cYabanciAdi, aRPA(nCnt).cSezon, cBedenSeti, aRPA(nCnt).cAnaModelTipi,
                              aRPA(nCnt).cRota)
                End If

                ' alt siparişler

                ReDim aMusteriSiparisNo(0)
                nCnt2 = -1

                cSQL = "select distinct musterisiparisno " +
                        " from ROBO.dbo.rpa_sipmodel with (NOLOCK) " +
                        " where siparisno = '" + aRPA(nCnt).cSiparisNo + "' " +
                        " and musterisiparisno is not null " +
                        " and musterisiparisno <> '' " +
                        " order by musterisiparisno "

                oReader = GetSQLReader(cSQL, ConnYage)

                Do While oReader.Read

                    nCnt2 = nCnt2 + 1
                    ReDim Preserve aMusteriSiparisNo(nCnt2)

                    aMusteriSiparisNo(nCnt2) = SQLReadString(oReader, "musterisiparisno")
                Loop
                oReader.Close()

                nSipCnt = 0

                For nCnt3 = 0 To aMusteriSiparisNo.GetUpperBound(0)

                    dilkSevkTarihiSip = #1/1/1950#

                    cMusteriSiparisNo = aMusteriSiparisNo(nCnt3)

                    nSipCnt = nSipCnt + 1
                    cSiparisNo2 = aRPA(nCnt).cSiparisNo ' + "-" + nSipCnt.ToString

                    nCntSiparisNo2 = nCntSiparisNo2 + 1
                    ReDim Preserve aSiparisNo2(nCntSiparisNo2)
                    aSiparisNo2(nCntSiparisNo2) = cSiparisNo2

                    'cSQL = "exec dobeforeadd_rpasiparis " + cSiparisNo2 + ",,,," + cMessage
                    'ExecuteSQLCommandConnected(cSQL, ConnYage)

                    ' alt sipariş kartını aç
                    cSQL = "select top 1 kullanicisipno " +
                            " from " + aRPA(nCnt).cWinTexDatabaseName + "siparis with (NOLOCK) " +
                            " where kullanicisipno = '" + cSiparisNo2 + "' "

                    If Not CheckExistsConnected(cSQL, ConnYage) Then

                        'JustForLog("RPA insert siparis " + cSiparisNo2)

                        cSQL = "insert " + aRPA(nCnt).cWinTexDatabaseName + "siparis " +
                                " (kullanicisipno, createuser, creationdate) " +
                                " values ('" + cSiparisNo2 + "', " +
                                " 'ROBO', " +
                                " getdate() ) "

                        ExecuteSQLCommandConnected(cSQL, ConnYage)
                    End If

                    cSQL = "set dateformat dmy " +
                            " update " + aRPA(nCnt).cWinTexDatabaseName + "siparis set " +
                            " siparistarihi = '" + SQLWriteDate(aRPA(nCnt).dSiparisTarihi) + "', " +
                            " sezon = '" + SQLWriteString(aRPA(nCnt).cSezon, 30) + "', " +
                            " musterino = '" + SQLWriteString(aRPA(nCnt).cMusteriNo, 30) + "', " +
                            " musteriyetkili = '" + SQLWriteString(aRPA(nCnt).cSorumlu, 30) + "', " +
                            " musterisipno = '" + cMusteriSiparisNo + "',  "

                    cSQL = cSQL +
                            " bedenseti1 = '" + SQLWriteString(cBedenSeti, 30) + "', " +
                            " teslimat = '" + SQLWriteString(aRPA(nCnt).cVadeTipi, 30) + "', " +
                            " joinlife = '" + SQLWriteString(aRPA(nCnt).cJoinLife, 1) + "', " +
                            " karisim = '" + SQLWriteString(aRPA(nCnt).cKarisim, 100) + "', " +
                            " imalatci = '" + SQLWriteString(aRPA(nCnt).cUretimYeri, 30) + "', "

                    ' MT ler istememiş
                    'cSQL = cSQL +
                    '        " parasalnotlar = '" + SQLWriteString(aRPA(nCnt).cYikamaNotlar) + "', "

                    cSQL = cSQL +
                            " sorumlu = '" + SQLWriteString(aRPA(nCnt).cSorumlu, 30) + "', " +
                            " onmodelno = '" + SQLWriteString(aRPA(nCnt).cOnModelNo, 30) + "', " +
                            " kaynaksiparisno = '" + SQLWriteString(aRPA(nCnt).cKaynakSiparisNo, 30) + "', " +
                            " repetesebebi = '" + SQLWriteString(aRPA(nCnt).cRepeteSebebi, 30) + "', " +
                            " numunetipi = '" + SQLWriteString(aRPA(nCnt).cRepeteTipi, 30) + "', "

                    cSQL = cSQL +
                            " yikamareceteno = '" + SQLWriteString(aRPA(nCnt).cYikamaReceteNo, 30) + "', " +
                            " username = 'ROBO', " +
                            " modificationdate = getdate() " +
                            " where kullanicisipno = '" + cSiparisNo2 + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    ' orjinal döküman 

                    If SQLWriteString(aRPA(nCnt).cMailDosyasi) <> "" Then

                        AddDocSubType(ConnYage, "ROBO-Siparis", aRPA(nCnt).cWinTexDatabaseName)

                        cSQL = "insert " + aRPA(nCnt).cWinTexDatabaseName + "documents " +
                            " (docvalue, doctype, rdocname, vdocname, docpath, " +
                            " type, extension, duzletmetarihi, duzeltmesaati, username, " +
                            " docsubtype) "

                        cSQL = cSQL +
                            " values ('" + GetDocName(cSiparisNo2) + "', 'Siparis', 'Siparis', 'Orjinal Siparis eMail', '" + SQLWriteString(aRPA(nCnt).cMailDosyasi, 255) + "', " +
                            " 'Outlook Mail', 'msg', convert(date,getdate()), convert(char(8),getdate(),108), 'ROBO', " +
                            " 'ROBO-Siparis' )"

                        ExecuteSQLCommandConnected(cSQL, ConnYage)

                    End If

                    ' sipmodel satırları
                    cSQL = "delete " + aRPA(nCnt).cWinTexDatabaseName + "sipmodel " +
                           " where siparisno = '" + cSiparisNo2 + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    ReDim aRBA(0)
                    nCnt4 = -1

                    cSQL = "select renk, beden, adet, ilksevktar, ambalaj, tasimasekli " +
                            " from ROBO.dbo.rpa_sipmodel with (NOLOCK) " +
                            " where siparisno = '" + aRPA(nCnt).cSiparisNo + "' " +
                            " and musterisiparisno = '" + cMusteriSiparisNo + "' " +
                            " order by renk, beden "

                    oReader = GetSQLReader(cSQL, ConnYage)

                    Do While oReader.Read

                        nCnt4 = nCnt4 + 1
                        ReDim Preserve aRBA(nCnt4)

                        ' renk okunurken başındaki "800 - " silinecek
                        aRBA(nCnt4).cRenk = SQLReadString(oReader, "renk")
                        aRBA(nCnt4).cRenk = Mid(aRBA(nCnt4).cRenk, 5, 30).Trim
                        aRBA(nCnt4).cRenk = Replace(aRBA(nCnt4).cRenk, "-", "").Trim

                        aRBA(nCnt4).cBeden = SQLReadString(oReader, "beden")
                        aRBA(nCnt4).nAdet = SQLReadDouble(oReader, "adet")
                        aRBA(nCnt4).cAmbalaj = SQLReadString(oReader, "ambalaj")
                        aRBA(nCnt4).cTasimaSekli = SQLReadString(oReader, "tasimasekli")

                        aRBA(nCnt4).dilkSevkTar = SQLReadDate(oReader, "ilksevktar")

                        If dilkSevkTarihiSip = #1/1/1950# Then
                            dilkSevkTarihiSip = aRBA(nCnt4).dilkSevkTar
                        End If

                        If aRBA(nCnt4).dilkSevkTar < dilkSevkTarihiSip Then
                            dilkSevkTarihiSip = aRBA(nCnt4).dilkSevkTar
                        End If
                    Loop
                    oReader.Close()

                    For nCnt4 = 0 To aRBA.GetUpperBound(0)

                        ' ambalaj

                        cSQL = "select top 1 kodu " +
                                " from " + aRPA(nCnt).cWinTexDatabaseName + "ambalaj with (NOLOCK) " +
                                " where (yabanciadi = '" + aRBA(nCnt4).cAmbalaj + "' or entegrekodu = '" + aRBA(nCnt4).cAmbalaj + "' ) "

                        oReader = GetSQLReader(cSQL, ConnYage)

                        If oReader.Read Then
                            cAmbalaj = SQLReadString(oReader, "kodu")
                        End If
                        oReader.Close()

                        If cAmbalaj.Trim = "" Then
                            cAmbalaj = aRBA(nCnt4).cAmbalaj
                        End If

                        ' taşıma şekli 

                        cSQL = "select top 1 kodu " +
                                " from " + aRPA(nCnt).cWinTexDatabaseName + "tasimasekli with (NOLOCK) " +
                                " where (yabanciadi = '" + aRBA(nCnt4).cTasimaSekli + "' or entegrekodu = '" + aRBA(nCnt4).cTasimaSekli + "' ) "

                        oReader = GetSQLReader(cSQL, ConnYage)

                        If oReader.Read Then
                            cTasimasekli = SQLReadString(oReader, "kodu")
                        End If
                        oReader.Close()

                        If cTasimasekli.Trim = "" Then
                            cTasimasekli = aRBA(nCnt4).cTasimaSekli
                        End If

                        'JustForLog("RPA insert rba " + cSiparisNo2 + " " + aRBA(nCnt4).cRenk + " " + aRBA(nCnt4).cBeden)

                        cSQL = "set dateformat dmy " +
                                " insert " + aRPA(nCnt).cWinTexDatabaseName + "sipmodel " +
                                " (siparisno, bilgisayarsipno, modelno, renk, beden, " +
                                " adet, ilksevktar, sonsevktar, ambalaj, tasimasekli, " +
                                " musterisiparisno, malzemetakipno, uretimtakipno, sevkiyattakipno, createdate, " +
                                " createuser, degistirmetarihi, degistirmesaati, username, bedenseti) "

                        cSQL = cSQL +
                                " values ('" + SQLWriteString(cSiparisNo2, 30) + "' , " +
                                " 0 , " +
                                " '" + SQLWriteString(aRPA(nCnt).cModelNo, 30) + "' , " +
                                " '" + SQLWriteString(aRBA(nCnt4).cRenk, 30) + "' , " +
                                " '" + SQLWriteString(aRBA(nCnt4).cBeden, 30) + "' , "

                        cSQL = cSQL +
                                SQLWriteDecimal(aRBA(nCnt4).nAdet) + " , " +
                                " '" + SQLWriteDate(aRBA(nCnt4).dilkSevkTar) + "' , " +
                                " '" + SQLWriteDate(aRBA(nCnt4).dilkSevkTar) + "' , " +
                                " '" + SQLWriteString(cAmbalaj, 30) + "' , " +
                                " '" + SQLWriteString(cTasimasekli, 30) + "' , "

                        cSQL = cSQL +
                                " '" + SQLWriteString(cMusteriSiparisNo, 30) + "' , " +
                                " '" + SQLWriteString(cSiparisNo2, 30) + "' , " +
                                " '" + SQLWriteString(cSiparisNo2, 30) + "' , " +
                                " '" + SQLWriteString(cSiparisNo2, 30) + "' , " +
                                " convert(date, getdate()) , "

                        cSQL = cSQL +
                                " 'ROBO' , " +
                                " convert(date, getdate()) , " +
                                " convert(char(8), getdate(), 8) , " +
                                " 'ROBO', " +
                                " '" + cBedenSeti + "' ) "

                        ExecuteSQLCommandConnected(cSQL, ConnYage)

                        ' rba döngü sonu 
                    Next

                    cSQL = "set dateformat dmy " +
                            " update " + aRPA(nCnt).cWinTexDatabaseName + "siparis set " +
                            " ilksevktarihi = '" + SQLWriteDate(dilkSevkTarihiSip) + "', " +
                            " sonsevktarihi = '" + SQLWriteDate(dilkSevkTarihiSip) + "' " +
                            " where kullanicisipno = '" + cSiparisNo2 + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    JustForLog("RPA insert sip sert " + cSiparisNo2)

                    ' sipariş sertifikaları
                    cSQL = "delete " + aRPA(nCnt).cWinTexDatabaseName + "siparissertifika " +
                            " where siparisno = '" + cSiparisNo2 + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    cSQL = "insert " + aRPA(nCnt).cWinTexDatabaseName + "siparissertifika " +
                            " (siparisno, sertifikatipi) " +
                            " select siparisno = '" + cSiparisNo2 + "' , sertifikatipi " +
                            " from ROBO.dbo.rpa_sipsertifika with (NOLOCK) " +
                            " where siparisno = '" + aRPA(nCnt).cSiparisNo + "' " +
                            " and sertifikatipi is not null " +
                            " and sertifikatipi <> '' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    cSQL = "update " + aRPA(nCnt).cWinTexDatabaseName + "siparis " +
                            " set sertifikatipi = (select w.sertifikatipi " +
                                                " from (select ROW_NUMBER() OVER (ORDER BY sirano ASC) AS rownumber , sertifikatipi " +
                                                        " from ROBO.dbo.rpa_sipsertifika With (NOLOCK) " +
                                                        " where siparisno = '" + aRPA(nCnt).cSiparisNo + "') w " +
                                                " where w.rownumber = 1) " +
                            " where kullanicisipno = '" + cSiparisNo2 + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    cSQL = "update " + aRPA(nCnt).cWinTexDatabaseName + "siparis " +
                            " set sertifikatipi2 = (select w.sertifikatipi " +
                                                " from (select ROW_NUMBER() OVER (ORDER BY sirano ASC) AS rownumber , sertifikatipi " +
                                                        " from ROBO.dbo.rpa_sipsertifika With (NOLOCK) " +
                                                        " where siparisno = '" + aRPA(nCnt).cSiparisNo + "') w " +
                                                " where w.rownumber = 2) " +
                            " where kullanicisipno = '" + cSiparisNo2 + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    cSQL = "update " + aRPA(nCnt).cWinTexDatabaseName + "siparis " +
                            " set sertifikatipi3 = (select w.sertifikatipi " +
                                                " from (select ROW_NUMBER() OVER (ORDER BY sirano ASC) AS rownumber , sertifikatipi " +
                                                        " from ROBO.dbo.rpa_sipsertifika With (NOLOCK) " +
                                                        " where siparisno = '" + aRPA(nCnt).cSiparisNo + "') w " +
                                                " where w.rownumber = 3) " +
                            " where kullanicisipno = '" + cSiparisNo2 + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    'JustForLog("RPA insert sip fiyat " + cSiparisNo2)

                    ' sipariş fiyatlarını aç 
                    cSQL = "delete " + aRPA(nCnt).cWinTexDatabaseName + "sipfiyat " +
                            " where siparisno = '" + cSiparisNo2 + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    cSQL = "insert " + aRPA(nCnt).cWinTexDatabaseName + "sipfiyat " +
                            " (siparisno, bilgisayarsipno, onmaliyetmodelno, modelkodu, satisfiyati, " +
                            " satisdoviz, username, degistirmetarihi, degistirmesaati) "

                    cSQL = cSQL +
                            " values ('" + cSiparisNo2 + "', " +
                            " 0, " +
                            " '" + SQLWriteString(aRPA(nCnt).cCalismaNo, 30) + "' , " +
                            " '" + SQLWriteString(aRPA(nCnt).cModelNo, 30) + "' , " +
                            SQLWriteDecimal(aRPA(nCnt).nFiyat) + " , "

                    cSQL = cSQL +
                            " '" + SQLWriteString(aRPA(nCnt).cDoviz, 3) + "' , " +
                            " 'ROBO' , " +
                            " convert(date, getdate()) , " +
                            " convert(char(8), getdate(), 8) ) "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    JustForLog("RPA insert ön sip  " + cSiparisNo2)

                    ' ön sipariş 
                    cSQL = "delete " + aRPA(nCnt).cWinTexDatabaseName + "siparisonsiparis " +
                            " where siparisno = '" + cSiparisNo2 + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    cSQL = "insert " + aRPA(nCnt).cWinTexDatabaseName + "siparisonsiparis " +
                            " (siparisno, onsiparisno, onsiparisulke2sirano) " +
                            " select siparisno = '" + cSiparisNo2 + "' , onsiparisno, onsiparisulke2sirano " +
                            " from ROBO.dbo.rpa_siponsiparis with (NOLOCK) " +
                            " where siparisno = '" + aRPA(nCnt).cSiparisNo + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    ' önsipariş satırını KAPAT
                    cSQL = "update " + aRPA(nCnt).cWinTexDatabaseName + "onsiparisulke2 " +
                            " set kapandi = 'E' " +
                            " where exists (select siparisno " +
                                            " from siparisonsiparis wirh (NOLOCK) " +
                                            " where siparisno = '" + cSiparisNo2 + "' " +
                                            " and onsiparisno = onsiparisulke2.onsiparisno " +
                                            " and onsiparisulke2sirano = onsiparisulke2.sirano ) "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    ' ön siparişi kapat
                    cSQL = "update " + aRPA(nCnt).cWinTexDatabaseName + "onsiparis " +
                            " set kapandi = 'E' " +
                            " where exists (select siparisno " +
                                            " from siparisonsiparis wirh (NOLOCK) " +
                                            " where siparisno = '" + cSiparisNo2 + "' " +
                                            " and onsiparisno = onsiparis.onsiparisno ) " +
                            " and not exists (select onsiparisno " +
                                            " from onsiparisulke2 with (NOLOCK) " +
                                            " where onsiparisno = onsiparis.onsiparisno " +
                                            " and (kapandi is null or kapandi = 'H' or kapandi = '') ) "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    'cSQL = "exec doafteradd_rpasiparis " + cSiparisNo2 + ",,,," + cMessage
                    'ExecuteSQLCommandConnected(cSQL, ConnYage)

                    ' update siparis
                    cSQL = "UPDATE " + aRPA(nCnt).cWinTexDatabaseName + "siparis " +
                            " set utfgen = 'E', " +
                            " stfgen = 'E' " +
                            " where kullanicisipno = '" + cSiparisNo2 + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    ' alt siparişler döngü sonu
                Next

                JustForLog("RPA close robo sip  " + aRPA(nCnt).cSiparisNo)

                cSQL = "update ROBO.dbo.rpa_siparis " +
                        " set transfered = 'E', " +
                        " transferuser = 'ROBO', " +
                        " transferdate = getdate() " +
                        " where siparisno = '" + aRPA(nCnt).cSiparisNo + "' "

                ExecuteSQLCommandConnected(cSQL, ConnYage)

                ' siparişler döngü sonu
            Next

            ConnYage.Close()

            ' UTF / STF üret
            For nCntSiparisNo2 = 0 To aSiparisNo2.GetUpperBound(0)

                JustForLog("RPA UTF / UIE / STF  " + aSiparisNo2(nCntSiparisNo2))

                ' UTF
                UTFGenerate(aSiparisNo2(nCntSiparisNo2))
                ' üretim işemirleri
                UretimisEmriUret(aSiparisNo2(nCntSiparisNo2), "add", "", "", "", "", "", "", "", "")
                ' STF
                STFFastGenerateAll(" and a.kullanicisipno = '" + aSiparisNo2(nCntSiparisNo2) + "' ")
            Next

            RPASiparis1 = 1

            JustForLog("RPA end process")

        Catch ex As Exception
            ErrDisp2(ex, "RPASiparis1")
        End Try
    End Function

    Public Sub RPAModel1()

        Try
            Dim aRPA() As oRPAModel
            Dim cSQL As String = ""
            Dim lOK As Boolean = False
            Dim lOK2 As Boolean = False
            Dim nCnt As Integer = -1
            Dim nCnt2 As Integer = -1
            Dim cBedenSeti As String
            Dim cNotlar As String = ""

            Dim ConnYage As SqlConnection
            Dim oReader As SqlDataReader

            ReDim aRPA(0)

            ConnYage = OpenConn()

            JustForLog("RPAModel Start : " + ConnYage.ConnectionString)

            cSQL = "select musterimodelno, modelno, anamodeltipi, musterisiparisno, siparisno, " +
                    " musterino, modeltarihi, sezon, anamodelno, modelaciklama, " +
                    " tasarimci, tasarimtarihi, anakumas, astar, iplik, " +
                    " dikim, aciklamalar, beden, bedenler, yikamakodu, " +
                    " yikamakumas, yikamarenk, yikamarecme, yikamafermuar, yikamaiplik, " +
                    " transfered, transferuser, transferdate, createuser, creationdate, " +
                    " mailbilgileri, robotokudu, modelresim, modeldikimresim, modelmaildosyasi, " +
                    " yikamaonresim, yikamaarkaresim, uretimulkesi, techpacktipi, eskimodelno, " +
                    " rotasablonu, eskimodel, modelkopyala, yenimodel, techpackpdf "

            cSQL = cSQL +
                    " from ROBO.dbo.rpa_model with (NOLOCK) " +
                    " where robotokudu = 'E' " +
                    " and (transfered is null or transfered = '' or transfered = 'H') " +
                    " and modelno is not null " +
                    " and modelno <> '' " +
                    " and uretimulkesi is not null " +
                    " and uretimulkesi <> '' "

            cSQL = cSQL +
                    " order by modelno "

            oReader = GetSQLReader(cSQL, ConnYage)

            Do While oReader.Read
                nCnt = nCnt + 1
                ReDim Preserve aRPA(nCnt)

                aRPA(nCnt).cMusteriModelNo = SQLReadString(oReader, "musterimodelno")
                aRPA(nCnt).cModelNo = SQLReadString(oReader, "modelno")
                aRPA(nCnt).cAnaModelTipi = SQLReadString(oReader, "anaModeltipi")
                aRPA(nCnt).cMusteriSiparisNo = SQLReadString(oReader, "musterisiparisno")
                aRPA(nCnt).cSiparisNo = SQLReadString(oReader, "siparisno")
                aRPA(nCnt).cMusteriNo = SQLReadString(oReader, "musterino")
                aRPA(nCnt).dModelTarihi = SQLReadDate(oReader, "modeltarihi")
                aRPA(nCnt).cSezon = SQLReadString(oReader, "sezon")
                aRPA(nCnt).cAnaModelNo = SQLReadString(oReader, "anamodelno")
                aRPA(nCnt).cModelAciklama = SQLReadString(oReader, "modelaciklama")
                aRPA(nCnt).cTasarimci = SQLReadString(oReader, "tasarimci")
                aRPA(nCnt).dTasarimTarihi = SQLReadDate(oReader, "tasarimtarihi")
                aRPA(nCnt).cAnaKumas = SQLReadString(oReader, "anakumas")
                aRPA(nCnt).cAstar = SQLReadString(oReader, "astar")
                aRPA(nCnt).cIplik = SQLReadString(oReader, "iplik")
                aRPA(nCnt).cDikim = SQLReadString(oReader, "dikim")
                aRPA(nCnt).cAciklamalar = SQLReadString(oReader, "aciklamalar")
                aRPA(nCnt).cBeden = SQLReadString(oReader, "beden")
                aRPA(nCnt).cBedenler = SQLReadString(oReader, "bedenler")
                aRPA(nCnt).cYikamaKodu = SQLReadString(oReader, "yikamakodu")
                aRPA(nCnt).cYikamaKumas = SQLReadString(oReader, "yikamakumas")
                aRPA(nCnt).cYikamaRenk = SQLReadString(oReader, "yikamarenk")
                aRPA(nCnt).cYikamaRecme = SQLReadString(oReader, "yikamarecme")
                aRPA(nCnt).cYikamaFermuar = SQLReadString(oReader, "yikamafermuar")
                aRPA(nCnt).cYikamaIplik = SQLReadString(oReader, "yikamaiplik")
                aRPA(nCnt).cTransfered = SQLReadString(oReader, "transfered")
                aRPA(nCnt).cTransferUser = SQLReadString(oReader, "transferuser")
                aRPA(nCnt).dTransferDate = SQLReadDate(oReader, "transferdate")
                aRPA(nCnt).cCreateUser = SQLReadString(oReader, "createuser")
                aRPA(nCnt).dCreationDate = SQLReadDate(oReader, "creationdate")
                aRPA(nCnt).cMailBilgileri = SQLReadString(oReader, "mailbilgileri")
                aRPA(nCnt).cRobotOkudu = SQLReadString(oReader, "robotokudu")
                aRPA(nCnt).cModelResim = SQLReadString(oReader, "modelresim")
                aRPA(nCnt).cModelDikimResim = SQLReadString(oReader, "modeldikimresim")
                aRPA(nCnt).cModelMailDosyasi = SQLReadString(oReader, "modelmaildosyasi")
                aRPA(nCnt).cYikamaOnResim = SQLReadString(oReader, "yikamaonresim")
                aRPA(nCnt).cYikamaArkaResim = SQLReadString(oReader, "yikamaarkaresim")
                aRPA(nCnt).cUretimUlkesi = SQLReadString(oReader, "uretimulkesi")
                aRPA(nCnt).cTechpackTipi = SQLReadString(oReader, "techpacktipi")
                aRPA(nCnt).cEskiModelNo = SQLReadString(oReader, "eskimodelno")
                aRPA(nCnt).cRotaSablonu = SQLReadString(oReader, "rotasablonu")
                aRPA(nCnt).cEskiModel = SQLReadString(oReader, "eskimodel")
                aRPA(nCnt).cModelKopyala = SQLReadString(oReader, "modelkopyala")
                aRPA(nCnt).cYeniModel = SQLReadString(oReader, "yenimodel")
                aRPA(nCnt).cTechPackPdf = SQLReadString(oReader, "techpackpdf")

                If aRPA(nCnt).cEskiModel = "" Then
                    aRPA(nCnt).cEskiModel = "H"
                Else
                    aRPA(nCnt).cEskiModel = aRPA(nCnt).cEskiModel.ToUpper
                End If

                If aRPA(nCnt).cModelKopyala = "" Then
                    aRPA(nCnt).cModelKopyala = "H"
                Else
                    aRPA(nCnt).cModelKopyala = aRPA(nCnt).cModelKopyala.ToUpper
                End If

                If aRPA(nCnt).cYeniModel = "" Then
                    aRPA(nCnt).cYeniModel = "H"
                Else
                    aRPA(nCnt).cYeniModel = aRPA(nCnt).cYeniModel.ToUpper
                End If

                If aRPA(nCnt).cEskiModel = "H" And aRPA(nCnt).cModelKopyala = "H" Then
                    aRPA(nCnt).cYeniModel = "E"
                End If

                If aRPA(nCnt).cUretimUlkesi = "MISIR" Then
                    aRPA(nCnt).cWinTexDatabaseName = "MISIR.dbo."
                Else
                    aRPA(nCnt).cWinTexDatabaseName = "TES.dbo."
                End If

                JustForLog("RPA Sip Oku : " + aRPA(nCnt).cSiparisNo + " " + aRPA(nCnt).cWinTexDatabaseName)

                lOK2 = True
            Loop
            oReader.Close()

            If Not lOK2 Then
                ConnYage.Close()
                JustForLog("RPAModel Exit")

                Exit Sub
            End If

            For nCnt = 0 To aRPA.GetUpperBound(0)

                ' model kartı

                cBedenSeti = CheckBedenSeti(ConnYage, aRPA(nCnt).cBedenler.Trim, aRPA(nCnt).cWinTexDatabaseName.Trim, aRPA(nCnt).cEskiModelNo.Trim, aRPA(nCnt).cSiparisNo.Trim)

                If aRPA(nCnt).cEskiModel = "E" Then
                    ' eski model aynen kullanılacak
                ElseIf aRPA(nCnt).cModelKopyala = "E" Then
                    ' eski modelden yeni model kopyalanarak olusturulacak
                    ModelKopyala(ConnYage, aRPA(nCnt).cWinTexDatabaseName.Trim, aRPA(nCnt).cModelNo.Trim, aRPA(nCnt).cEskiModelNo.Trim)
                Else
                    ' yeni model kartını aç 
                    YeniModel(ConnYage, aRPA(nCnt).cWinTexDatabaseName, aRPA(nCnt).cModelNo, aRPA(nCnt).cModelAciklama, 0, "EUR",
                              aRPA(nCnt).cMusteriNo, aRPA(nCnt).cMusteriModelNo, aRPA(nCnt).cModelAciklama, aRPA(nCnt).cSezon, cBedenSeti, aRPA(nCnt).cAnaModelTipi,
                              aRPA(nCnt).cRotaSablonu)
                End If

                ' update model 

                cNotlar = "Açıklamalar : " + aRPA(nCnt).cAciklamalar + vbCrLf + vbCrLf +
                        "Ana Kumaş : " + aRPA(nCnt).cAnaKumas + vbCrLf + vbCrLf +
                        "Astar : " + aRPA(nCnt).cAstar + vbCrLf + vbCrLf +
                        "Iplik : " + aRPA(nCnt).cIplik + vbCrLf + vbCrLf +
                        "Dikim : " + aRPA(nCnt).cDikim + vbCrLf + vbCrLf +
                        "Yıkama Kodu : " + aRPA(nCnt).cYikamaKodu + vbCrLf + vbCrLf +
                        "Yıkama Kumaş : " + aRPA(nCnt).cYikamaKumas + vbCrLf + vbCrLf +
                        "Yıkama Renk : " + aRPA(nCnt).cYikamaRenk + vbCrLf + vbCrLf +
                        "Yıkama Reçme : " + aRPA(nCnt).cYikamaRecme + vbCrLf + vbCrLf +
                        "Yıkama Fermuar : " + aRPA(nCnt).cYikamaFermuar + vbCrLf + vbCrLf +
                        "Yıkama Iplik : " + aRPA(nCnt).cYikamaIplik

                cSQL = "update " + aRPA(nCnt).cWinTexDatabaseName + "ymodel set " +
                        " musterimodelno = '" + SQLWriteString(aRPA(nCnt).cMusteriModelNo, 30) + "' , " +
                        " anamodeltipi = '" + SQLWriteString(aRPA(nCnt).cAnaModelTipi, 30) + "' , " +
                        " musterino = '" + SQLWriteString(aRPA(nCnt).cMusteriNo, 30) + "' , " +
                        " sezon = '" + SQLWriteString(aRPA(nCnt).cSezon, 30) + "' , " +
                        " aciklama = '" + SQLWriteString(aRPA(nCnt).cModelNo, 250) + "', " +
                        " kaynakmodelno = '" + SQLWriteString(aRPA(nCnt).cAnaModelNo, 30) + "' , " +
                        " yabanciadi = '" + SQLWriteString(aRPA(nCnt).cModelAciklama, 250) + "' , " +
                        " modelist = '" + SQLWriteString(aRPA(nCnt).cTasarimci, 30) + "' , " +
                        " bedenseti = '" + SQLWriteString(cBedenSeti, 30) + "' , " +
                        " renk = '" + SQLWriteString(aRPA(nCnt).cYikamaRenk, 30) + "' , " +
                        " notlar = '" + SQLWriteString(cNotlar) + "' , " +
                        " videodosyasi = '" + SQLWriteString(aRPA(nCnt).cModelResim, 255) + "' , " +
                        " resimdosyasi = '" + SQLWriteString(aRPA(nCnt).cModelResim, 255) + "' , " +
                        " arkadanresim = '" + SQLWriteString(aRPA(nCnt).cYikamaArkaResim, 255) + "' , " +
                        " nakis1resim = '" + SQLWriteString(aRPA(nCnt).cYikamaOnResim, 255) + "' , " +
                        " nakis2resim = '" + SQLWriteString(aRPA(nCnt).cYikamaArkaResim, 255) + "' , " +
                        " nakis3resim = '" + SQLWriteString(aRPA(nCnt).cModelDikimResim, 255) + "' , "

                cSQL = cSQL +
                        " username = 'ROBO', " +
                        " modificationdate = getdate() " +
                        " where modelno = '" + aRPA(nCnt).cModelNo + "' "

                ExecuteSQLCommandConnected(cSQL, ConnYage)

                ' orjinal döküman 

                If SQLWriteString(aRPA(nCnt).cModelMailDosyasi) <> "" Then

                    AddDocSubType(ConnYage, "ROBO-Model", aRPA(nCnt).cWinTexDatabaseName)

                    cSQL = "insert " + aRPA(nCnt).cWinTexDatabaseName + "documents " +
                        " (docvalue, doctype, rdocname, vdocname, docpath, " +
                        " type, extension, duzletmetarihi, duzeltmesaati, username, " +
                        " docsubtype) "

                    cSQL = cSQL +
                        " values ('" + GetDocName(aRPA(nCnt).cModelNo) + "', 'Model', 'Model', 'Orjinal Techpack eMail', '" + SQLWriteString(aRPA(nCnt).cModelMailDosyasi, 255) + "', " +
                        " 'Outlook Mail', 'msg', convert(date,getdate()), convert(char(8),getdate(),108), 'ROBO', " +
                        " 'ROBO-Model' )"

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                End If

                ' techpack pdf

                If SQLWriteString(aRPA(nCnt).cTechPackPdf) <> "" Then

                    AddDocSubType(ConnYage, "ROBO-TechPack", aRPA(nCnt).cWinTexDatabaseName)

                    cSQL = "insert " + aRPA(nCnt).cWinTexDatabaseName + "documents " +
                           " (docvalue, doctype, rdocname, vdocname, docpath, " +
                           " type, extension, duzletmetarihi, duzeltmesaati, username, " +
                           " docsubtype) "

                    cSQL = cSQL +
                           " values ('" + GetDocName(aRPA(nCnt).cModelNo) + "', 'Model', 'Model', 'Orjinal Techpack PDF', '" + SQLWriteString(aRPA(nCnt).cTechPackPdf, 255) + "', " +
                           " 'Adobe PDF File', 'pdf', convert(date,getdate()), convert(char(8),getdate(),108), 'ROBO', " +
                           " 'ROBO-TechPack' )"

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    JustForLog(cSQL)

                End If

                ' transfer tamamlandı

                cSQL = "update ROBO.dbo.rpa_model " +
                        " set transfered = 'E', " +
                        " transferuser = 'ROBO', " +
                        " transferdate = getdate() " +
                        " where modelno = '" + aRPA(nCnt).cModelNo + "' "

                ExecuteSQLCommandConnected(cSQL, ConnYage)

            Next

            ConnYage.Close()

        Catch ex As Exception
            ErrDisp2(ex, "RPAModel1")
        End Try
    End Sub

    Public Sub RPAOlcu1()

        Try
            Dim aRPA() As oRPAOlcu
            Dim cSQL As String = ""
            Dim lOK2 As Boolean = False
            Dim nCnt As Integer = -1
            Dim nCnt1 As Integer = -1
            Dim nCnt2 As Integer = -1
            Dim nCnt3 As Integer = -1
            Dim cOlcuTablosuNo As String = ""
            Dim cOlcuTablosuTipi As String = "YIKAMA SONRASI"
            Dim nSatirNo As Double = 0
            Dim cSatir As String = ""
            Dim cOlcuYeri As String = ""

            Dim ConnYage As SqlConnection
            Dim oReader As SqlDataReader

            ReDim aRPA(0)

            ConnYage = OpenConn()

            JustForLog("RPAOlcu Start : " + ConnYage.ConnectionString)

            cSQL = "select modelno, referans, tarih, kalipci, notlar1, " +
                    " notlar2, patern, olcuresim, olcuexceldosyasi, olcumaildosyasi "

            cSQL = cSQL +
                    " from ROBO.dbo.rpa_model_olcu With (NOLOCK) " +
                    " where robotokudu = 'E' " +
                    " and (transfered is null or transfered = '' or transfered = 'H') " +
                    " and modelno is not null " +
                    " and modelno <> '' "

            cSQL = cSQL +
                    " and exists (select modelno " +
                                " from ROBO.dbo.rpa_model with (NOLOCK) " +
                                " where modelno = ROBO.dbo.rpa_model.modelno ) " +
                    " order by modelno "

            oReader = GetSQLReader(cSQL, ConnYage)

            Do While oReader.Read
                nCnt = nCnt + 1
                ReDim Preserve aRPA(nCnt)

                aRPA(nCnt).cModelNo = SQLReadString(oReader, "modelno")
                aRPA(nCnt).cReferans = SQLReadString(oReader, "referans")
                aRPA(nCnt).dTarih = SQLReadDate(oReader, "tarih")
                aRPA(nCnt).cKalipci = SQLReadString(oReader, "kalipci")
                aRPA(nCnt).cNotlar1 = SQLReadString(oReader, "notlar1")
                aRPA(nCnt).cNotlar2 = SQLReadString(oReader, "notlar2")
                aRPA(nCnt).cPatern = SQLReadString(oReader, "patern")
                aRPA(nCnt).cOlcuResim = SQLReadString(oReader, "olcuresim")
                aRPA(nCnt).cOlcuExcelDosyasi = SQLReadString(oReader, "olcuexceldosyasi")
                aRPA(nCnt).cOlcuMailDosyasi = SQLReadString(oReader, "olcumaildosyasi")

                JustForLog("RPAOlcu model no : " + aRPA(nCnt).cModelNo)

                ' ölçüleri sıfırla
                ReDim aRPA(nCnt).aDetay(0)
                aRPA(nCnt).aDetay(0).cOlcuYeriKodu = ""

                ' ölçü resimlerini sıfırla
                ReDim aRPA(nCnt).aRPAOlcuResimleri(0)
                aRPA(nCnt).aRPAOlcuResimleri(0).cResimDosyasi = ""

                JustForLog("RPAOlcu model okundu : " + aRPA(nCnt).cModelNo)

                lOK2 = True
            Loop
            oReader.Close()

            JustForLog("RPAOlcu Buffer OK")

            If Not lOK2 Then
                ConnYage.Close()
                JustForLog("RPAOlcu Exit")

                Exit Sub
            End If

            For nCnt = 0 To aRPA.GetUpperBound(0)

                nCnt3 = -1

                cSQL = "select modelno, resimdosyasi " +
                        " from ROBO.dbo.rpa_model_olcuresimleri with (NOLOCK) " +
                        " where modelno = '" + aRPA(nCnt).cModelNo.Trim + "' " +
                        " and resimdosyasi is not null " +
                        " and resimdosyasi <> '' "

                oReader = GetSQLReader(cSQL, ConnYage)

                Do While oReader.Read
                    nCnt3 = nCnt3 + 1
                    ReDim Preserve aRPA(nCnt).aRPAOlcuResimleri(nCnt3)

                    aRPA(nCnt).aRPAOlcuResimleri(nCnt3).cResimDosyasi = SQLReadString(oReader, "resimdosyasi")
                Loop
                oReader.Close()

                ' model kartı / üretim ülkesi
                cSQL = "select top 1 uretimulkesi, siparisno, beden, bedenler " +
                        " from ROBO.dbo.rpa_model with (NOLOCK) " +
                        " where modelno = '" + aRPA(nCnt).cModelNo.Trim + "' "

                oReader = GetSQLReader(cSQL, ConnYage)

                If oReader.Read Then

                    aRPA(nCnt).cSiparisNo = SQLReadString(oReader, "siparisno")
                    aRPA(nCnt).cBeden = SQLReadString(oReader, "beden")
                    aRPA(nCnt).cBedenler = SQLReadString(oReader, "bedenler")

                    If SQLReadString(oReader, "uretimulkesi") = "MISIR" Then
                        aRPA(nCnt).cWinTexDatabaseName = "MISIR.dbo."
                    Else
                        aRPA(nCnt).cWinTexDatabaseName = "TES.dbo."
                    End If
                End If
                oReader.Close()

                ' model ölçü resimleri

                ' beden seti
                aRPA(nCnt).cBedenSeti = CheckBedenSeti(ConnYage, aRPA(nCnt).cBedenler.Trim, aRPA(nCnt).cWinTexDatabaseName.Trim, aRPA(nCnt).cModelNo.Trim, aRPA(nCnt).cSiparisNo.Trim)

                ' ölçü yerleri
                cSatir = ""
                nSatirNo = 0
                nCnt1 = -1

                cSQL = "select olcuyerikodu, ingolcuyeri, ispolcuyeri, beden, olcu, pay, olcuyeri " +
                        " from ROBO.dbo.rpa_model_olcudetay with (NOLOCK) " +
                        " where modelno = '" + aRPA(nCnt).cModelNo + "' " +
                        " order by olcuyerikodu, olcuyeri, beden "

                oReader = GetSQLReader(cSQL, ConnYage)

                Do While oReader.Read

                    If cSatir.Trim = "" Then
                        cSatir = SQLReadString(oReader, "olcuyerikodu")
                        nSatirNo = nSatirNo + 1
                    Else
                        If cSatir.Trim <> SQLReadString(oReader, "olcuyerikodu") Then
                            cSatir = SQLReadString(oReader, "olcuyerikodu")
                            nSatirNo = nSatirNo + 1
                        End If
                    End If

                    nCnt1 = nCnt1 + 1
                    ReDim Preserve aRPA(nCnt).aDetay(nCnt1)

                    aRPA(nCnt).aDetay(nCnt1).cOlcuYeriKodu = SQLReadString(oReader, "olcuyerikodu")
                    aRPA(nCnt).aDetay(nCnt1).cIngOlcuYeri = SQLReadString(oReader, "ingolcuyeri")
                    aRPA(nCnt).aDetay(nCnt1).cIspOlcuYeri = SQLReadString(oReader, "ispolcuyeri")
                    aRPA(nCnt).aDetay(nCnt1).cTrkOlcuYeri = SQLReadString(oReader, "olcuyeri")
                    aRPA(nCnt).aDetay(nCnt1).cBeden = SQLReadString(oReader, "beden")
                    aRPA(nCnt).aDetay(nCnt1).nOlcu = SQLReadDecimal(oReader, "olcu")
                    aRPA(nCnt).aDetay(nCnt1).nPay = SQLReadDecimal(oReader, "pay")
                    aRPA(nCnt).aDetay(nCnt1).nSatirNo = nSatirNo
                Loop
                oReader.Close()
            Next

            ' wintex olcuyeri tablosu update 
            For nCnt = 0 To aRPA.GetUpperBound(0)

                If aRPA(nCnt).cWinTexDatabaseName.Trim <> "" And aRPA(nCnt).aDetay(0).cOlcuYeriKodu.Trim <> "" Then

                    nCnt2 = 0

                    For nCnt1 = 0 To aRPA(nCnt).aDetay.GetUpperBound(0)

                        nCnt2 = nCnt2 + 1

                        If aRPA(nCnt).aDetay(nCnt1).cOlcuYeriKodu.Trim = "" Then
                            cOlcuYeri = aRPA(nCnt).aDetay(nCnt1).cTrkOlcuYeri.Trim
                        Else
                            cOlcuYeri = aRPA(nCnt).aDetay(nCnt1).cOlcuYeriKodu.Trim + "-" + aRPA(nCnt).aDetay(nCnt1).cTrkOlcuYeri.Trim
                        End If

                        cSQL = "select top 1 olcuyeri " +
                                " from " + aRPA(nCnt).cWinTexDatabaseName + "olcuyeri with (NOLOCK) " +
                                " where olcuyeri = '" + SQLWriteString(cOlcuYeri, 30) + "' "

                        If Not CheckExistsConnected(cSQL, ConnYage) Then

                            cSQL = "insert " + aRPA(nCnt).cWinTexDatabaseName + "olcuyeri " +
                                    " (olcuyeri, aciklama, siralama, ingolcuyeri, ispolcuyeri) " +
                                    " values ('" + SQLWriteString(cOlcuYeri, 30) + "', " +
                                    " '" + SQLWriteString(aRPA(nCnt).aDetay(nCnt1).cIngOlcuYeri, 100) + "', " +
                                    SQLWriteDecimal(nCnt2) + " , " +
                                    " '" + SQLWriteString(aRPA(nCnt).aDetay(nCnt1).cIngOlcuYeri, 200) + "', " +
                                    " '" + SQLWriteString(aRPA(nCnt).aDetay(nCnt1).cIspOlcuYeri, 200) + "' ) "

                            ExecuteSQLCommandConnected(cSQL, ConnYage)
                        End If
                    Next
                End If
            Next

            ' create olcutabloları
            For nCnt = 0 To aRPA.GetUpperBound(0)

                If aRPA(nCnt).cWinTexDatabaseName.Trim <> "" And aRPA(nCnt).aDetay(0).cOlcuYeriKodu.Trim <> "" Then

                    cOlcuTablosuNo = GetSequenceFisNo(ConnYage, "olcutablosuno")

                    For nCnt3 = 0 To aRPA(nCnt).aRPAOlcuResimleri.GetUpperBound(0)

                        If aRPA(nCnt).aRPAOlcuResimleri(nCnt3).cResimDosyasi.Trim <> "" Then

                            AddDocSubType(ConnYage, "ROBO-OlcuResim", aRPA(nCnt).cWinTexDatabaseName)

                            cSQL = "insert " + aRPA(nCnt).cWinTexDatabaseName + "documents " +
                                   " (docvalue, doctype, rdocname, vdocname, docpath, " +
                                   " type, extension, duzletmetarihi, duzeltmesaati, username, " +
                                   " docsubtype) "

                            cSQL = cSQL +
                                   " values ('" + GetDocName(cOlcuTablosuNo) + "', 'olcutablolari2', 'Olcu Resim', 'Diger Olcu Resim', " +
                                   " '" + SQLWriteString(aRPA(nCnt).aRPAOlcuResimleri(nCnt3).cResimDosyasi.Trim, 255) + "', " +
                                   " 'Picture File', 'jpg', convert(date,getdate()), convert(char(8),getdate(),108), 'ROBO', " +
                                   " 'ROBO-OlcuResim' ) "

                            ExecuteSQLCommandConnected(cSQL, ConnYage)

                            JustForLog(cSQL)

                        End If
                    Next

                    ' insert header 
                    cSQL = "set dateformat dmy " +
                           " insert " + aRPA(nCnt).cWinTexDatabaseName + "sipolcuheader " +
                           " (olcutablosuno, grubu, referans, kalipci, " +
                           " patern, tarih, notlar1, notlar2, robotokudu) "

                    cSQL = cSQL +
                           " values ('" + cOlcuTablosuNo + "', " +
                           " 'SERI IMALAT', " +
                           " '" + SQLWriteString(aRPA(nCnt).cReferans, 30) + "', " +
                           " '" + SQLWriteString(aRPA(nCnt).cKalipci, 100) + "', "

                    cSQL = cSQL +
                           " '" + SQLWriteString(aRPA(nCnt).cPatern, 100) + "', " +
                           " '" + SQLWriteDate(aRPA(nCnt).dTarih) + "', " +
                           " '" + SQLWriteString(aRPA(nCnt).cNotlar1) + "', " +
                           " '" + SQLWriteString(aRPA(nCnt).cNotlar2) + "', " +
                           " 'E' ) "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    JustForLog(cSQL)

                    ' orjinal döküman 

                    If SQLWriteString(aRPA(nCnt).cOlcuResim) <> "" Then

                        cSQL = "insert " + aRPA(nCnt).cWinTexDatabaseName + "documents " +
                           " (docvalue, doctype, rdocname, vdocname, docpath, " +
                           " type, extension, duzletmetarihi, duzeltmesaati, username, " +
                           " docsubtype) "

                        cSQL = cSQL +
                           " values ('" + GetDocName(cOlcuTablosuNo) + "', 'olcutablolari2', 'Olcu Resim', 'Orjinal Olcu Resim', '" + SQLWriteString(aRPA(nCnt).cOlcuResim, 255) + "', " +
                           " 'Picture File', 'jpg', convert(date,getdate()), convert(char(8),getdate(),108), 'ROBO', " +
                           " 'ROBO-OlcuResim' ) "

                        ExecuteSQLCommandConnected(cSQL, ConnYage)

                        JustForLog(cSQL)

                    End If

                    If SQLWriteString(aRPA(nCnt).cOlcuExcelDosyasi) <> "" Then

                        AddDocSubType(ConnYage, "ROBO-OlcuExcel", aRPA(nCnt).cWinTexDatabaseName)

                        cSQL = "insert " + aRPA(nCnt).cWinTexDatabaseName + "documents " +
                               " (docvalue, doctype, rdocname, vdocname, docpath, " +
                               " type, extension, duzletmetarihi, duzeltmesaati, username, " +
                               " docsubtype) "

                        cSQL = cSQL +
                               " values ('" + GetDocName(cOlcuTablosuNo) + "', 'olcutablolari2', 'Olcu Excel', 'Orjinal Olcu Excel', '" + SQLWriteString(aRPA(nCnt).cOlcuExcelDosyasi, 255) + "', " +
                               " 'Microsoft Excel', 'xlsx', convert(date,getdate()), convert(char(8),getdate(),108), 'ROBO', " +
                               " 'ROBO-OlcuExcel' ) "

                        ExecuteSQLCommandConnected(cSQL, ConnYage)

                        JustForLog(cSQL)

                    End If

                    If SQLWriteString(aRPA(nCnt).cOlcuMailDosyasi) <> "" Then

                        AddDocSubType(ConnYage, "ROBO-OlcuMail", aRPA(nCnt).cWinTexDatabaseName)

                        cSQL = "insert " + aRPA(nCnt).cWinTexDatabaseName + "documents " +
                               " (docvalue, doctype, rdocname, vdocname, docpath, " +
                               " type, extension, duzletmetarihi, duzeltmesaati, username, " +
                               " docsubtype) "

                        cSQL = cSQL +
                               " values ('" + GetDocName(cOlcuTablosuNo) + "', 'olcutablolari2', 'Olcu Mail', 'Orjinal Olcu Mail', '" + SQLWriteString(aRPA(nCnt).cOlcuMailDosyasi, 255) + "', " +
                               " 'email', 'msg', convert(date,getdate()), convert(char(8),getdate(),108), 'ROBO', " +
                               " 'ROBO-OlcuMail' ) "

                        ExecuteSQLCommandConnected(cSQL, ConnYage)

                        JustForLog(cSQL)

                    End If

                    ' insert detail
                    For nCnt1 = 0 To aRPA(nCnt).aDetay.GetUpperBound(0)

                        If aRPA(nCnt).aDetay(nCnt1).cOlcuYeriKodu.Trim = "" Then
                            cOlcuYeri = aRPA(nCnt).aDetay(nCnt1).cTrkOlcuYeri.Trim
                        Else
                            cOlcuYeri = aRPA(nCnt).aDetay(nCnt1).cOlcuYeriKodu.Trim + "-" + aRPA(nCnt).aDetay(nCnt1).cTrkOlcuYeri.Trim
                        End If

                        cSQL = "insert " + aRPA(nCnt).cWinTexDatabaseName + "sipolcu " +
                               " (siparisno, modelno, renk, beden, bolum, " +
                               " bedenseti, grading, olcutablosuno, satirno, olcutablosutipi, " +
                               " aciklama, notlar, olcumyeriharfi, olcumyeriyabanci ) "

                        cSQL = cSQL +
                                " values ('" + SQLWriteString(aRPA(nCnt).cSiparisNo, 30) + "' , " +
                                " '" + SQLWriteString(aRPA(nCnt).cModelNo, 30) + "' , " +
                                " '" + SQLWriteString(cOlcuYeri, 250) + "' , " +
                                " '" + SQLWriteString(aRPA(nCnt).aDetay(nCnt1).cBeden, 30) + "' , " +
                                " '" + SQLWriteString(cOlcuYeri, 30) + "' , "

                        cSQL = cSQL +
                                " '" + SQLWriteString(aRPA(nCnt).cBedenSeti, 30) + "' , " +
                                SQLWriteDecimal(aRPA(nCnt).aDetay(nCnt1).nPay) + " , " +
                                " '" + SQLWriteString(cOlcuTablosuNo, 30) + "' , " +
                                SQLWriteDecimal(aRPA(nCnt).aDetay(nCnt1).nSatirNo) + " , " +
                                " '" + SQLWriteString(cOlcuTablosuTipi, 30) + "' , "

                        cSQL = cSQL +
                                " '" + SQLWriteString(aRPA(nCnt).aDetay(nCnt1).cIspOlcuYeri, 100) + "' , " +
                                " '" + SQLWriteDecimal(aRPA(nCnt).aDetay(nCnt1).nOlcu) + "' , " +
                                " '" + SQLWriteString(aRPA(nCnt).aDetay(nCnt1).cOlcuYeriKodu, 10) + "' , " +
                                " '" + SQLWriteString(aRPA(nCnt).aDetay(nCnt1).cIngOlcuYeri, 100) + "' ) "

                        ExecuteSQLCommandConnected(cSQL, ConnYage)

                        JustForLog(cSQL)

                    Next

                    ' transfer tamamlandı

                    cSQL = "update ROBO.dbo.rpa_model_olcu " +
                           " set transfered = 'E', " +
                           " transferuser = 'ROBO', " +
                           " transferdate = getdate() " +
                           " where modelno = '" + aRPA(nCnt).cModelNo + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    JustForLog(cSQL)

                End If
            Next

            ConnYage.Close()

        Catch ex As Exception
            ErrDisp2(ex, "RPAOlcu1")
        End Try
    End Sub

    Private Function GetBedenSetiFromModel(ConnYage As SqlConnection, cWinTexDatabaseName As String, cModelNo As String) As String

        GetBedenSetiFromModel = ""

        Try
            Dim cSQL As String = ""
            Dim oReader As SqlDataReader = Nothing

            cSQL = "select top 1 bedenseti " +
                        " from  " + cWinTexDatabaseName + "ymodel with (NOLOCK) " +
                        " where modelno = '" + cModelNo.Trim + "' "

            oReader = GetSQLReader(cSQL, ConnYage)

            If oReader.Read Then
                GetBedenSetiFromModel = SQLReadString(oReader, "bedenseti")
            End If
            oReader.Close()

        Catch ex As Exception
            ErrDisp2(ex, "GetBedenSetiFromModel")
        End Try
    End Function

    Private Function CheckBedenSeti(ConnYage As SqlConnection, cBedenler As String, cWinTexDatabaseName As String, Optional cModelNo As String = "",
                                    Optional cSiparisNo As String = "") As String
        CheckBedenSeti = ""

        Try
            Dim cSQL As String = ""
            Dim nCnt2 As Integer = 0
            Dim aBeden() As String
            Dim cBeden As String = ""
            Dim cBedenSeti As String = ""
            Dim nSiralama As Integer = 0
            Dim cBFilter As String = ""
            Dim lOK As Boolean = False
            Dim oReader As SqlDataReader = Nothing

            If cWinTexDatabaseName.Trim = "" Then Exit Function

            'If cModelNo.Trim <> "" Then

            '    cSQL = "select top 1 bedenseti " +
            '            " from  " + cWinTexDatabaseName + "ymodel with (NOLOCK) " +
            '            " where modelno = '" + cModelNo.Trim + "' "

            '    oReader = GetSQLReader(cSQL, ConnYage)

            '    If oReader.Read Then
            '        cBedenSeti = SQLReadString(oReader, "bedenseti")
            '    End If
            '    oReader.Close()
            'End If

            If cBedenSeti.Trim = "" And cSiparisNo.Trim <> "" Then

                cSQL = "select top 1 bedenseti1 " +
                       " from  " + cWinTexDatabaseName + "siparis with (NOLOCK) " +
                       " where kullanicisipno = '" + cSiparisNo.Trim + "' "

                oReader = GetSQLReader(cSQL, ConnYage)

                If oReader.Read Then
                    cBedenSeti = SQLReadString(oReader, "bedenseti1")
                End If
                oReader.Close()
            End If

            If cBedenSeti.Trim = "" And cBedenler.Trim <> "" Then

                ReDim aBeden(0)
                cBedenSeti = ""
                cBFilter = ""
                nSiralama = 0

                If InStr(cBedenler, ",") > 0 Then

                    aBeden = Split(cBedenler, ",")

                    ' olmayan bedenleri aç
                    For nCnt2 = 0 To aBeden.GetUpperBound(0)

                        nSiralama = nSiralama + 1

                        cSQL = "select top 1 beden " +
                               " from " + cWinTexDatabaseName + "beden with (NOLOCK) " +
                               " where beden = '" + aBeden(nCnt2) + "' "

                        If Not CheckExistsConnected(cSQL, ConnYage) Then

                            cSQL = "insert " + cWinTexDatabaseName + "beden " +
                                   " (beden, siralama) " +
                                   " values ('" + aBeden(nCnt2) + "', " +
                                   nSiralama.ToString + " ) "

                            ExecuteSQLCommandConnected(cSQL, ConnYage)
                        End If

                        If cBFilter = "" Then
                            cBFilter = " b" + nSiralama.ToString("00") + " = '" + aBeden(nCnt2) + "' "
                            cBedenSeti = aBeden(nCnt2)
                        Else
                            cBFilter = cBFilter + " and b" + nSiralama.ToString("00") + " = '" + aBeden(nCnt2) + "' "
                            cBedenSeti = cBedenSeti + "-" + aBeden(nCnt2)
                        End If
                    Next

                    ' son beden boş olacak
                    nSiralama = nSiralama + 1
                    If cBFilter = "" Then
                        cBFilter = " ( b" + nSiralama.ToString("00") + " =  null or b" + nSiralama.ToString("00") + " = '' ) "
                    Else
                        cBFilter = cBFilter + " and ( b" + nSiralama.ToString("00") + " =  null or b" + nSiralama.ToString("00") + " = '' ) "
                    End If

                    cBedenSeti = Mid(cBedenSeti, 1, 30).Trim

                    ' olmayan beden setini aç
                    lOK = False

                    cSQL = "select top 1 bedenseti " +
                           " from " + cWinTexDatabaseName + "bedenseti with (NOLOCK) " +
                           " where " + cBFilter

                    oReader = GetSQLReader(cSQL, ConnYage)

                    If oReader.Read Then
                        lOK = True
                        cBedenSeti = SQLReadString(oReader, "bedenseti")
                    End If
                    oReader.Close()

                    If Not lOK And cBedenSeti <> "" Then

                        cSQL = "select top 1 bedenseti " +
                               " from " + cWinTexDatabaseName + "bedenseti with (NOLOCK) " +
                               " where bedenseti = '" + cBedenSeti + "' "

                        If Not CheckExistsConnected(cSQL, ConnYage) Then

                            JustForLog("Insert bedenseti " + cBedenSeti)

                            cSQL = "insert " + cWinTexDatabaseName + "bedenseti " +
                                   " (bedenseti) " +
                                   " values ('" + cBedenSeti + "') "

                            ExecuteSQLCommandConnected(cSQL, ConnYage)

                            For nCnt2 = 0 To aBeden.GetUpperBound(0)

                                cSQL = "update " + cWinTexDatabaseName + "bedenseti set " +
                                       " b" + (nCnt2 + 1).ToString("00") + " = '" + aBeden(nCnt2) + "' " +
                                       " where bedenseti = '" + cBedenSeti + "' "

                                ExecuteSQLCommandConnected(cSQL, ConnYage)
                            Next
                        End If
                    End If
                Else
                    ' ayraç yok tek beden var
                    cBeden = cBedenler.Trim

                    ' olmayan bedeni aç
                    cSQL = "select top 1 beden " +
                           " from " + cWinTexDatabaseName + "beden with (NOLOCK) " +
                           " where beden = '" + cBeden + "' "

                    If Not CheckExistsConnected(cSQL, ConnYage) Then

                        cSQL = "insert " + cWinTexDatabaseName + "beden " +
                               " (beden) " +
                               " values ('" + cBeden + "' ) "

                        ExecuteSQLCommandConnected(cSQL, ConnYage)
                    End If

                    ' olmayan beden setini aç
                    lOK = False

                    cSQL = "select top 1 bedenseti " +
                            " from " + cWinTexDatabaseName + "bedenseti with (NOLOCK) " +
                            " where bedenseti = '" + cBeden + "' "

                    oReader = GetSQLReader(cSQL, ConnYage)

                    If oReader.Read Then
                        lOK = True
                        cBedenSeti = SQLReadString(oReader, "bedenseti")
                    End If
                    oReader.Close()

                    If Not lOK And cBedenSeti <> "" Then

                        cSQL = "select top 1 bedenseti " +
                               " from " + cWinTexDatabaseName + "bedenseti with (NOLOCK) " +
                               " where bedenseti = '" + cBedenSeti + "' "

                        If Not CheckExistsConnected(cSQL, ConnYage) Then

                            JustForLog("RPAModel insert bedenseti " + cBedenSeti)

                            cSQL = "insert " + cWinTexDatabaseName + "bedenseti " +
                                   " (bedenseti) " +
                                   " values ('" + cBedenSeti + "') "

                            ExecuteSQLCommandConnected(cSQL, ConnYage)

                            cSQL = "update " + cWinTexDatabaseName + "bedenseti set " +
                                   " b0 = '" + cBeden + "' " +
                                   " where bedenseti = '" + cBedenSeti + "' "

                            ExecuteSQLCommandConnected(cSQL, ConnYage)
                        End If
                    End If
                End If
            End If

            CheckBedenSeti = cBedenSeti.Trim

        Catch ex As Exception
            ErrDisp2(ex, "CheckBedenSeti")
        End Try
    End Function

    Private Sub ModelKopyala(ConnYage As SqlConnection, cWinTexDatabaseName As String, cModelNo As String, cEskiModelNo As String)
        ' Model Kopyala
        Try
            Dim cSQL As String = ""

            cSQL = "select top 1 modelno " +
                    " from " + cWinTexDatabaseName + "ymodel with (NOLOCK) " +
                    " where modelno = '" + cModelNo + "' "

            If CheckExistsConnected(cSQL, ConnYage) Then
                Exit Sub
            End If

            cSQL = "insert " + cWinTexDatabaseName + "ymodel " +
                    " (modelno, aciklama, resimdosyasi, videodosyasi, anamodeltipi, " +
                    " yabanciadi, musterimodelno, kalipno, sorumlu, uretici, " +
                    " entegrekodu, musterino, trkaciklama, yabanciaciklama, onmodelno, " +
                    " sex, maliyetcalismano, calismano, kaynakmodelno, kokmodelno, " +
                    " bedenseti, bedenseti2, bedenseti3, bedenseti4, bedenseti5, " +
                    " bedenseti6, bedenseti7, bedenseti8, bedenseti9, bedenseti10, " +
                    " arkadanresim ) "

            cSQL = cSQL +
                    " select top 1 modelno = '" + cModelNo + "' , aciklama, resimdosyasi, videodosyasi, anamodeltipi, " +
                    " yabanciadi, musterimodelno, kalipno, sorumlu, uretici, " +
                    " entegrekodu, musterino, trkaciklama, yabanciaciklama, onmodelno, " +
                    " sex, maliyetcalismano, calismano, kaynakmodelno, kokmodelno, " +
                    " bedenseti, bedenseti2, bedenseti3, bedenseti4, bedenseti5, " +
                    " bedenseti6, bedenseti7, bedenseti8, bedenseti9, bedenseti10, " +
                    " arkadanresim " +
                    " from " + cWinTexDatabaseName + "ymodel with (NOLOCK) " +
                    " where modelno = '" + cEskiModelNo + "' "

            ExecuteSQLCommandConnected(cSQL, ConnYage)

            ' ana reçete
            cSQL = "insert " + cWinTexDatabaseName + "modelhammadde " +
                    " (modelno, modelrenk, modelbeden, hammaddekodu, hammadderenk, " +
                    " hammaddebeden, kullanimmiktari, fire, miktarbirimi, uretimdepartmani, " +
                    " temindept, hesaplama, anakumas, yrdkumas, pastalno, " +
                    " uretimtoleransi, malzemetoleransi, iplikyeri, fiyat, doviz ) "

            cSQL = cSQL +
                    " select modelno = '" + cModelNo + "' , modelrenk, modelbeden, hammaddekodu, hammadderenk, " +
                    " hammaddebeden, kullanimmiktari, fire, miktarbirimi, uretimdepartmani, " +
                    " temindept, hesaplama, anakumas, yrdkumas, pastalno, " +
                    " uretimtoleransi, malzemetoleransi, iplikyeri, fiyat, doviz " +
                    " from " + cWinTexDatabaseName + "modelhammadde with (NOLOCK) " +
                    " where modelno = '" + cEskiModelNo + "' "

            ExecuteSQLCommandConnected(cSQL, ConnYage)

            ' alternatif reçete
            cSQL = "insert " + cWinTexDatabaseName + "modelhammadde2 " +
                    " (modelno, modelrenk, modelbeden, hammaddekodu, hammadderenk, " +
                    " hammaddebeden, kullanimmiktari, fire, miktarbirimi, uretimdepartmani, " +
                    " temindept, hesaplama, anakumas, yrdkumas, pastalno, " +
                    " uretimtoleransi, malzemetoleransi, iplikyeri, fiyat, doviz, receteno ) "

            cSQL = cSQL +
                    " select modelno = '" + cModelNo + "' , modelrenk, modelbeden, hammaddekodu, hammadderenk, " +
                    " hammaddebeden, kullanimmiktari, fire, miktarbirimi, uretimdepartmani, " +
                    " temindept, hesaplama, anakumas, yrdkumas, pastalno, " +
                    " uretimtoleransi, malzemetoleransi, iplikyeri, fiyat, doviz, receteno " +
                    " from " + cWinTexDatabaseName + "modelhammadde2 with (NOLOCK) " +
                    " where modelno = '" + cEskiModelNo + "' "

            ExecuteSQLCommandConnected(cSQL, ConnYage)

            ' alternatif fark reçete
            cSQL = "insert " + cWinTexDatabaseName + "modelhammadde3 " +
                    " (modelno, modelrenk, modelbeden, hammaddekodu, hammadderenk, " +
                    " hammaddebeden, kullanimmiktari, fire, miktarbirimi, uretimdepartmani, " +
                    " temindept, hesaplama, anakumas, yrdkumas, pastalno, " +
                    " uretimtoleransi, malzemetoleransi, iplikyeri, fiyat, doviz, receteno ) "

            cSQL = cSQL +
                    " select modelno = '" + cModelNo + "' , modelrenk, modelbeden, hammaddekodu, hammadderenk, " +
                    " hammaddebeden, kullanimmiktari, fire, miktarbirimi, uretimdepartmani, " +
                    " temindept, hesaplama, anakumas, yrdkumas, pastalno, " +
                    " uretimtoleransi, malzemetoleransi, iplikyeri, fiyat, doviz, receteno " +
                    " from " + cWinTexDatabaseName + "modelhammadde3 with (NOLOCK) " +
                    " where modelno = '" + cEskiModelNo + "' "

            ExecuteSQLCommandConnected(cSQL, ConnYage)

            ' rota
            cSQL = "insert " + cWinTexDatabaseName + "modeluretim " +
                    " (modelno, departman, uretimtoleransi, giristakipesasi, cikistakipesasi, " +
                    " parca, sira, yikamakodu, iscilikfiyat, iscilikdoviz) "

            cSQL = cSQL +
                    " select modelno = '" + cModelNo + "' , departman, uretimtoleransi, giristakipesasi, cikistakipesasi, " +
                    " parca, sira, yikamakodu, iscilikfiyat, iscilikdoviz " +
                    " from " + cWinTexDatabaseName + "modeluretim with (NOLOCK) " +
                    " where modelno = '" + cEskiModelNo + "' "

            ExecuteSQLCommandConnected(cSQL, ConnYage)

        Catch ex As Exception
            ErrDisp2(ex, "ModelKopyala")
        End Try
    End Sub

    Private Sub YeniModel(ConnYage As SqlConnection, cWinTexDatabaseName As String, cModelNo As String, cModelAciklama As String, nFiyat As Double, cDoviz As String,
                          cMusteriNo As String, cMusteriModelNo As String, cYabanciAdi As String, cSezon As String, cBedenSeti As String, cAnaModelTipi As String,
                          cRota As String)
        ' yeni model kartını aç 
        Try
            Dim cSQL As String = ""

            If cModelNo.Trim = "" Then Exit Sub

            cSQL = "select top 1 modelno " +
                    " from " + cWinTexDatabaseName + "ymodel with (NOLOCK) " +
                    " where modelno = '" + cModelNo + "' "

            If Not CheckExistsConnected(cSQL, ConnYage) Then

                JustForLog("RPA insert model " + cModelNo)

                cSQL = "insert " + cWinTexDatabaseName + "ymodel " +
                        " (modelno, createuser, creationdate) " +
                        " values ('" + cModelNo + "', " +
                        " 'ROBO', " +
                        " getdate() ) "

                ExecuteSQLCommandConnected(cSQL, ConnYage)
            End If

            cSQL = "update " + cWinTexDatabaseName + "ymodel set " +
                    " aciklama = '" + SQLWriteString(cModelNo, 250) + "', " +
                    " satisfiyati = " + SQLWriteDecimal(nFiyat) + " , " +
                    " dovizkodu = '" + SQLWriteString(cDoviz, 3) + "' , " +
                    " musterino = '" + SQLWriteString(cMusteriNo, 30) + "' , " +
                    " musterimodelno = '" + SQLWriteString(cMusteriModelNo, 30) + "' , " +
                    " yabanciadi = '" + SQLWriteString(cYabanciAdi, 250) + "' , " +
                    " sezon = '" + SQLWriteString(cSezon, 30) + "' , " +
                    " bedenseti = '" + SQLWriteString(cBedenSeti, 30) + "', " +
                    " anamodeltipi = '" + SQLWriteString(cAnaModelTipi, 30) + "', " +
                    " username = 'ROBO', " +
                    " modificationdate = getdate() " +
                    " where modelno = '" + cModelNo + "' "

            ExecuteSQLCommandConnected(cSQL, ConnYage)

            ' modele rota ekle
            If cRota.Trim <> "" Then

                cSQL = "delete " + cWinTexDatabaseName + "modeluretim " +
                    " where modelno = '" + cModelNo + "' "

                ExecuteSQLCommandConnected(cSQL, ConnYage)

                JustForLog("RPA insert modeluretim " + cModelNo)

                cSQL = "insert " + cWinTexDatabaseName + "modeluretim " +
                    " (modelno, departman, uretimtoleransi, sira) "

                cSQL = cSQL +
                    " select modelno = '" + cModelNo + "' , a.departman , uretimtoleransi = a.tolerans , a.sira  " +
                    " from " + cWinTexDatabaseName + "frmuretim a with (NOLOCK) , " + cWinTexDatabaseName + "departman b with (NOLOCK) " +
                    " where a.departman = b.departman " +
                    " and a.departman is not null " +
                    " and a.departman <> '' " +
                    " and (b.kapandi is null or b.kapandi = 'H' or b.kapandi = '')  " +
                    " and a.formno = '" + cRota + "'  "

                ExecuteSQLCommandConnected(cSQL, ConnYage)
            End If

        Catch ex As Exception
            ErrDisp2(ex, "YeniModel")
        End Try
    End Sub

    Public Function GetDocName(cField As String) As String

        On Error Resume Next

        Dim cFieldValue As String

        cFieldValue = Trim$(cField)
        cFieldValue = Replace(cFieldValue, "/", "", 1)
        cFieldValue = Replace(cFieldValue, "\", "", 1)
        cFieldValue = Replace(cFieldValue, "*", "", 1)

        GetDocName = cFieldValue

    End Function

End Module

