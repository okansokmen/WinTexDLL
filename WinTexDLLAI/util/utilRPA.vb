Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Module utilRPA

    Dim cRPAServer As String = ""
    Dim cRPADatabase As String = ""
    Dim cRPAUserName As String = ""
    Dim cRPAPassword As String = ""

    Public Function RPASiparis1(Optional cFilter As String = "", Optional ByRef cMessage As String = "", Optional lSilent As Boolean = False) As Boolean

        RPASiparis1 = False

        Try
            Dim oSQL As SQLServerClass
            Dim oSQL2 As SQLServerClass
            Dim oSQL3 As SQLServerClass
            Dim oSQL4 As SQLServerClass

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
            Dim cCalismaNo As String
            Dim cOnsiparisNo As String
            Dim nOnSiparisUlke2SiraNo As Double
            Dim cRota As String
            Dim cUretimYeri As String
            Dim cUretimUlkesi As String
            Dim cBeden As String = ""
            Dim cRenk As String = ""
            Dim nSiralama As Integer = 0
            Dim cBFilter As String = ""
            Dim cBedenSeti As String = ""
            Dim lOK As Boolean = False
            Dim aBeden() As String
            Dim nCnt As Integer = 0
            Dim nAdet As Double = 0
            Dim dilksevktar As Date = #1/1/1950#
            Dim cAmbalaj As String = ""
            Dim cTasimasekli As String = ""
            Dim cMusteriSiparisNo As String = ""
            Dim dilkSevkTarihiSip As Date = #1/1/1950#
            Dim cEskiModelNo As String = ""
            Dim cEskiModel As String = ""
            Dim cModelKopyala As String = ""
            Dim cYeniModel As String = ""
            Dim nSonuc As Integer = 1
            Dim cAnaModelTipi As String = ""

            cRPAServer = "192.168.1.8"           ' oSQL.GetSysPar("RPAServer", "192.168.1.8")
            cRPADatabase = "ROBO"                ' oSQL.GetSysPar("RPADatabase", "ROBO")
            cRPAUserName = oConnection.cUser     ' oSQL.GetSysPar("RPAUsername", Gl_UserName) / sa
            cRPAPassword = oConnection.cPassword ' oSQL.GetSysPar("RPAPassword", Gl_ActivePass) / "er1303*?

            ' alt sipariş döngüsü
            oSQL4 = New SQLServerClass(False, cRPAServer, cRPADatabase, cRPAUserName, cRPAPassword)
            oSQL4.OpenConn()

            ' ana loop içindeki rpa okumaları
            oSQL3 = New SQLServerClass(False, cRPAServer, cRPADatabase, cRPAUserName, cRPAPassword)
            oSQL3.OpenConn()

            ' ana loop - aktarılacak siparişler
            oSQL2 = New SQLServerClass(False, cRPAServer, cRPADatabase, cRPAUserName, cRPAPassword)
            oSQL2.OpenConn()

            oSQL2.cSQLQuery = "select siparisno, siparistarihi, sezon, musterino, musteriyetkili, " +
                             " vadetipi, modelno, modelaciklama, fiyat, doviz, " +
                             " sertifikatipi, joinlife, karisim, yikamanotlar, yabanciadi, " +
                             " musterimodelno, calismano, onsiparisno, onsiparisulke2sirano, sertifikatipi2, " +
                             " sertifikatipi3, sertifikatipi4, sertifikatipi5, sertifikatipi6, rota, " +
                             " uretimyeri, uretimulkesi, musterisiparisno, eskimodelno, eskimodel, " +
                             " modelkopyala, yenimodel, anamodeltipi  "

            oSQL2.cSQLQuery = oSQL2.cSQLQuery +
                             " from rpa_siparis with (NOLOCK) " +
                             " where robotokudu = 'E' " +
                             " and (transfered is null or transfered = '' or transfered = 'H') " +
                             " and modelno is not null " +
                             " and modelno <> '' " +
                             " and siparisno is not null " +
                             " and siparisno <> '' " +
                             " and exists (select siparisno from rpa_sipmodel with (NOLOCK) where siparisno = rpa_siparis.siparisno) " +
                             " order by siparisno "

            oSQL2.GetSQLReader()

            Do While oSQL2.oReader.Read

                dilkSevkTarihiSip = #1/1/1950#

                cSiparisNo = oSQL2.SQLReadString("siparisno")
                dSiparisTarihi = oSQL2.SQLReadDate("siparistarihi")
                cSezon = oSQL2.SQLReadString("sezon")
                cMusteriNo = oSQL2.SQLReadString("musterino")
                cMusteriYetkili = oSQL2.SQLReadString("musteriyetkili")
                cVadeTipi = oSQL2.SQLReadString("vadetipi")
                cModelNo = oSQL2.SQLReadString("modelno")
                cModelAciklama = oSQL2.SQLReadString("modelaciklama")
                nFiyat = oSQL2.SQLReadDouble("fiyat")
                cDoviz = oSQL2.SQLReadString("doviz")
                cMusteriSiparisNo = oSQL2.SQLReadString("musterisiparisno")
                cEskiModelNo = oSQL2.SQLReadString("eskimodelno")
                cEskiModel = oSQL2.SQLReadString("eskimodel")
                cModelKopyala = oSQL2.SQLReadString("modelkopyala")
                cYeniModel = oSQL2.SQLReadString("yenimodel")
                cAnaModelTipi = oSQL2.SQLReadString("anamodeltipi")

                If cEskiModel = "" Then
                    cEskiModel = "H"
                Else
                    cEskiModel = cEskiModel.ToUpper
                End If

                If cModelKopyala = "" Then
                    cModelKopyala = "H"
                Else
                    cModelKopyala = cModelKopyala.ToUpper
                End If

                If cYeniModel = "" Then
                    cYeniModel = "H"
                Else
                    cYeniModel = cYeniModel.ToUpper
                End If

                If cEskiModel = "H" And cModelKopyala = "H" Then
                    cYeniModel = "E"
                End If

                cSertifikaTipi = oSQL2.SQLReadString("sertifikatipi")
                cSertifikaTipi2 = oSQL2.SQLReadString("sertifikatipi2")
                cSertifikaTipi3 = oSQL2.SQLReadString("sertifikatipi3")
                cSertifikaTipi4 = oSQL2.SQLReadString("sertifikatipi4")
                cSertifikaTipi5 = oSQL2.SQLReadString("sertifikatipi5")
                cSertifikaTipi6 = oSQL2.SQLReadString("sertifikatipi6")

                cJoinlife = oSQL2.SQLReadString("joinlife")
                cKarisim = oSQL2.SQLReadString("karisim")
                cYikamaNotlar = oSQL2.SQLReadString("yikamanotlar")
                cYabanciAdi = oSQL2.SQLReadString("yabanciadi")
                cRota = oSQL2.SQLReadString("rota")
                cUretimYeri = oSQL2.SQLReadString("uretimyeri")

                cUretimUlkesi = oSQL2.SQLReadString("uretimulkesi")
                If cUretimUlkesi.Trim = "" Then
                    cUretimUlkesi = "TURKIYE"
                End If

                cMusteriModelno = oSQL2.SQLReadString("musterimodelno")
                cCalismaNo = oSQL2.SQLReadString("calismano")
                cOnSiparisNo = oSQL2.SQLReadString("onsiparisno")
                nOnSiparisUlke2SiraNo = oSQL2.SQLReadDouble("onsiparisulke2sirano")

                If cUretimUlkesi = "TURKIYE" Then
                    ' TES
                    oSQL = New SQLServerClass(False, cRPAServer, "TES", cRPAUserName, cRPAPassword)
                Else
                    ' MISIR
                    oSQL = New SQLServerClass(False, cRPAServer, "MISIR", cRPAUserName, cRPAPassword)
                End If
                oSQL.OpenConn()

                ' olmayan bedenleri aç
                ReDim aBeden(0)
                nSiralama = 0
                cBFilter = ""
                cBedenSeti = ""

                oSQL3.cSQLQuery = "select distinct beden " +
                                " from rpa_sipmodel with (NOLOCK) " +
                                " where siparisno = '" + cSiparisNo + "' " +
                                " and beden is not null " +
                                " and beden <> '' " +
                                " order by beden "

                oSQL3.GetSQLReader()

                Do While oSQL3.oReader.Read

                    cBeden = oSQL3.SQLReadString("beden")

                    ReDim Preserve aBeden(nSiralama)
                    aBeden(nSiralama) = cBeden

                    nSiralama = nSiralama + 1

                    oSQL.cSQLQuery = "select top 1 beden " +
                                    " from beden with (NOLOCK) " +
                                    " where beden = '" + cBeden + "' "

                    If Not oSQL.CheckExists Then

                        oSQL.cSQLQuery = "insert beden (beden, siralama) " +
                                        " values ('" + cBeden + "', " +
                                        nSiralama.ToString + " ) "

                        oSQL.SQLExecute()
                    End If

                    If cBFilter = "" Then
                        cBFilter = " b" + nSiralama.ToString("00") + " = '" + cBeden + "' "
                        cBedenSeti = cBeden
                    Else
                        cBFilter = cBFilter + " and b" + nSiralama.ToString("00") + " = '" + cBeden + "' "
                        cBedenSeti = cBedenSeti + "-" + cBeden
                    End If
                Loop
                oSQL3.oReader.Close()

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
                oSQL.cSQLQuery = "select top 1 bedenseti " +
                                " from bedenseti with (NOLOCK) " +
                                " where " + cBFilter

                oSQL.GetSQLReader()

                If oSQL.oReader.Read Then
                    lOK = True
                    cBedenSeti = oSQL.SQLReadString("bedenseti")
                End If
                oSQL.oReader.Close()

                If Not lOK And cBedenSeti <> "" Then

                    oSQL.cSQLQuery = "select top 1 bedenseti " +
                                    " from bedenseti with (NOLOCK) " +
                                    " where bedenseti = '" + cBedenSeti + "' "

                    If Not oSQL.CheckExists Then

                        oSQL.cSQLQuery = "insert bedenseti (bedenseti) values ('" + cBedenSeti + "') "
                        oSQL.SQLExecute()

                        For nCnt = 0 To aBeden.GetUpperBound(0)

                            oSQL.cSQLQuery = "update bedenseti set " +
                                        " b" + (nCnt + 1).ToString("00") + " = '" + aBeden(nCnt) + "' " +
                                        " where bedenseti = '" + cBedenSeti + "' "

                            oSQL.SQLExecute()
                        Next
                    End If
                End If

                If cEskiModel = "E" Then
                    ' eski model aynen kullanılacak
                ElseIf cModelKopyala = "E" Then
                    ' eski modelden yeni model kopyalanarak olusturulacak
                    oSQL.cSQLQuery = "insert ymodel (modelno, aciklama, resimdosyasi, videodosyasi, anamodeltipi, " +
                                                    " yabanciadi, musterimodelno, kalipno, sorumlu, uretici, " +
                                                    " entegrekodu, musterino, trkaciklama, yabanciaciklama, onmodelno, " +
                                                    " sex, maliyetcalismano, calismano, kaynakmodelno, kokmodelno, " +
                                                    " bedenseti, bedenseti2, bedenseti3, bedenseti4, bedenseti5, " +
                                                    " bedenseti6, bedenseti7, bedenseti8, bedenseti9, bedenseti10, " +
                                                    " arkadanresim ) " +
                                    " select top 1 modelno = '" + cModelNo + "' , aciklama, resimdosyasi, videodosyasi, anamodeltipi, " +
                                                    " yabanciadi, musterimodelno, kalipno, sorumlu, uretici, " +
                                                    " entegrekodu, musterino, trkaciklama, yabanciaciklama, onmodelno, " +
                                                    " sex, maliyetcalismano, calismano, kaynakmodelno, kokmodelno, " +
                                                    " bedenseti, bedenseti2, bedenseti3, bedenseti4, bedenseti5, " +
                                                    " bedenseti6, bedenseti7, bedenseti8, bedenseti9, bedenseti10, " +
                                                    " arkadanresim " +
                                                    " from ymodel with (NOLOCK) " +
                                                    " where modelno = '" + cEskiModelNo + "' "
                    oSQL.SQLExecute()

                    ' ana reçete
                    oSQL.cSQLQuery = "insert modelhammadde (modelno, modelrenk, modelbeden, hammaddekodu, hammadderenk, " +
                                                        " hammaddebeden, kullanimmiktari, fire, miktarbirimi, uretimdepartmani, " +
                                                        " temindept, hesaplama, anakumas, yrdkumas, pastalno, " +
                                                        " uretimtoleransi, malzemetoleransi, iplikyeri, fiyat, doviz ) " +
                                    " select modelno = '" + cModelNo + "' , modelrenk, modelbeden, hammaddekodu, hammadderenk, " +
                                    " hammaddebeden, kullanimmiktari, fire, miktarbirimi, uretimdepartmani, " +
                                    " temindept, hesaplama, anakumas, yrdkumas, pastalno, " +
                                    " uretimtoleransi, malzemetoleransi, iplikyeri, fiyat, doviz " +
                                    " from modelhammadde with (NOLOCK) " +
                                    " where modelno = '" + cEskiModelNo + "' "
                    oSQL.SQLExecute()

                    ' alternatif reçete
                    oSQL.cSQLQuery = "insert modelhammadde2 (modelno, modelrenk, modelbeden, hammaddekodu, hammadderenk, " +
                                                        " hammaddebeden, kullanimmiktari, fire, miktarbirimi, uretimdepartmani, " +
                                                        " temindept, hesaplama, anakumas, yrdkumas, pastalno, " +
                                                        " uretimtoleransi, malzemetoleransi, iplikyeri, fiyat, doviz, receteno ) " +
                                    " select modelno = '" + cModelNo + "' , modelrenk, modelbeden, hammaddekodu, hammadderenk, " +
                                    " hammaddebeden, kullanimmiktari, fire, miktarbirimi, uretimdepartmani, " +
                                    " temindept, hesaplama, anakumas, yrdkumas, pastalno, " +
                                    " uretimtoleransi, malzemetoleransi, iplikyeri, fiyat, doviz, receteno " +
                                    " from modelhammadde2 with (NOLOCK) " +
                                    " where modelno = '" + cEskiModelNo + "' "
                    oSQL.SQLExecute()

                    ' alternatif fark reçete
                    oSQL.cSQLQuery = "insert modelhammadde3 (modelno, modelrenk, modelbeden, hammaddekodu, hammadderenk, " +
                                                        " hammaddebeden, kullanimmiktari, fire, miktarbirimi, uretimdepartmani, " +
                                                        " temindept, hesaplama, anakumas, yrdkumas, pastalno, " +
                                                        " uretimtoleransi, malzemetoleransi, iplikyeri, fiyat, doviz, receteno ) " +
                                    " select modelno = '" + cModelNo + "' , modelrenk, modelbeden, hammaddekodu, hammadderenk, " +
                                    " hammaddebeden, kullanimmiktari, fire, miktarbirimi, uretimdepartmani, " +
                                    " temindept, hesaplama, anakumas, yrdkumas, pastalno, " +
                                    " uretimtoleransi, malzemetoleransi, iplikyeri, fiyat, doviz, receteno " +
                                    " from modelhammadde3 with (NOLOCK) " +
                                    " where modelno = '" + cEskiModelNo + "' "
                    oSQL.SQLExecute()

                    ' rota
                    oSQL.cSQLQuery = "insert modeluretim (modelno, departman, uretimtoleransi, giristakipesasi, cikistakipesasi, " +
                                                        " parca, sira, yikamakodu, iscilikfiyat, iscilikdoviz) " +
                                    " select modelno = '" + cModelNo + "' , departman, uretimtoleransi, giristakipesasi, cikistakipesasi, " +
                                    " parca, sira, yikamakodu, iscilikfiyat, iscilikdoviz " +
                                    " from modeluretim with (NOLOCK) " +
                                    " where modelno = '" + cEskiModelNo + "' "
                    oSQL.SQLExecute()

                Else
                    ' yeni model kartını aç 
                    oSQL.cSQLQuery = "select top 1 modelno " +
                                " from ymodel with (NOLOCK) " +
                                " where modelno = '" + cModelNo + "' "

                    If Not oSQL.CheckExists Then
                        oSQL.cSQLQuery = "insert ymodel (modelno, createuser, creationdate) " +
                                        " values ('" + cModelNo + "', " +
                                        " 'ROBO', " +
                                        " getdate() ) "
                        oSQL.SQLExecute()
                    End If

                    oSQL.cSQLQuery = "update ymodel set " +
                                    " aciklama = '" + SQLWriteString(cModelAciklama, 250) + "', " +
                                    " satisfiyati = " + SQLWriteDecimal(nFiyat) + " , " +
                                    " dovizkodu = '" + SQLWriteString(cDoviz, 3) + "' , " +
                                    " musterino = '" + SQLWriteString(cMusteriNo, 30) + "' , " +
                                    " musterimodelno = '" + SQLWriteString(cMusteriModelno, 30) + "' , " +
                                    " yabanciadi = '" + SQLWriteString(cYabanciAdi, 250) + "' , " +
                                    " sezon = '" + SQLWriteString(cSezon, 30) + "' , " +
                                    " bedenseti = '" + SQLWriteString(cBedenSeti, 30) + "', " +
                                    " anamodeltipi = '" + SQLWriteString(cAnaModelTipi, 30) + "', " +
                                    " username = 'ROBO', " +
                                    " modificationdate = getdate() " +
                                    " where modelno = '" + cModelNo + "' "
                    oSQL.SQLExecute()

                    ' modele rota ekle
                    oSQL.cSQLQuery = "delete modeluretim " +
                                    " where modelno = '" + cModelNo + "' "
                    oSQL.SQLExecute()

                    oSQL.cSQLQuery = "insert modeluretim (modelno, departman, uretimtoleransi, sira) " +
                                    " select modelno = '" + cModelNo + "' , a.departman , uretimtoleransi = a.tolerans , a.sira  " +
                                    " from frmuretim a with (NOLOCK), departman b with (NOLOCK) " +
                                    " where a.departman = b.departman " +
                                    " and a.departman is not null " +
                                    " and a.departman <> '' " +
                                    " and (b.kapandi is null or b.kapandi = 'H' or b.kapandi = '')  " +
                                    " and a.formno = '" + cRota + "'  "
                    oSQL.SQLExecute()
                End If

                ' alt siparisleri al 
                ' her alt sipariş için ayrı sipariş aç

                nSipCnt = 0
                dilkSevkTarihiSip = #1/1/1950#

                oSQL4.cSQLQuery = "select distinct musterisiparisno " +
                                 " from rpa_sipmodel with (NOLOCK) " +
                                 " where siparisno = '" + cSiparisNo + "' "

                oSQL4.GetSQLReader()

                Do While oSQL4.oReader.Read

                    cMusteriSiparisNo = oSQL4.SQLReadString("musterisiparisno")

                    nSipCnt = nSipCnt + 1
                    cSiparisNo2 = cSiparisNo.Trim + "-" + nSipCnt.ToString

                    If Not oSQL.CLRExecute("dobeforeadd_rpasiparis", cSiparisNo2,,,, cMessage) Then
                        ' Exit Function
                    End If

                    ' alt sipariş kartını aç
                    oSQL.cSQLQuery = "select top 1 kullanicisipno " +
                                    " from siparis with (NOLOCK) " +
                                    " where kullanicisipno = '" + cSiparisNo2 + "' "

                    If Not oSQL.CheckExists Then

                        oSQL.cSQLQuery = "insert siparis (kullanicisipno, createuser, creationdate) " +
                                        " values ('" + cSiparisNo2 + "', " +
                                        " 'ROBO', " +
                                        " getdate() ) "
                        oSQL.SQLExecute()
                    End If

                    oSQL.cSQLQuery = "set dateformat dmy " +
                                    " update siparis set " +
                                    " siparistarihi = '" + SQLWriteDate(dSiparisTarihi) + "', " +
                                    " sezon = '" + SQLWriteString(cSezon, 30) + "', " +
                                    " musterino = '" + SQLWriteString(cMusteriNo, 30) + "', " +
                                    " musteriyetkili = '" + SQLWriteString(cMusteriYetkili, 30) + "', " +
                                    " bedenseti1 = '" + SQLWriteString(cBedenSeti, 30) + "', " +
                                    " teslimat = '" + SQLWriteString(cVadeTipi, 30) + "', " +
                                    " joinlife = '" + SQLWriteString(cJoinlife, 1) + "', " +
                                    " karisim = '" + SQLWriteString(cKarisim, 100) + "', " +
                                    " imalatci = '" + SQLWriteString(cUretimYeri, 30) + "', " +
                                    " parasalnotlar = '" + SQLWriteString(cYikamaNotlar) + "', " +
                                    " username = 'ROBO', " +
                                    " modificationdate = getdate() " +
                                    " where kullanicisipno = '" + cSiparisNo2 + "' "
                    oSQL.SQLExecute()

                    ' sipmodel satırları
                    oSQL.cSQLQuery = "delete sipmodel " +
                                    " where siparisno = '" + cSiparisNo2 + "' "
                    oSQL.SQLExecute()

                    oSQL3.cSQLQuery = "select renk, beden, adet, ilksevktar, ambalaj, tasimasekli " +
                                     " from rpa_sipmodel with (NOLOCK) " +
                                     " where siparisno = '" + cSiparisNo + "' " +
                                     " and musterisiparisno = '" + cMusteriSiparisNo + "' " +
                                     " order by renk, beden "

                    oSQL3.GetSQLReader()

                    Do While oSQL3.oReader.Read

                        cRenk = oSQL3.SQLReadString("renk")
                        cRenk = Mid(cRenk, 7, 30).Trim

                        cBeden = oSQL3.SQLReadString("beden")
                        nAdet = oSQL3.SQLReadDouble("adet")
                        dilksevktar = oSQL3.SQLReadDate("ilksevktar")

                        If dilkSevkTarihiSip = #1/1/1950# Then
                            dilkSevkTarihiSip = dilksevktar
                        End If

                        If dilksevktar < dilkSevkTarihiSip Then
                            dilkSevkTarihiSip = dilksevktar
                        End If

                        cAmbalaj = oSQL3.SQLReadString("ambalaj")
                        cAmbalaj = GetAmbalaj(cAmbalaj)

                        cTasimasekli = oSQL3.SQLReadString("tasimasekli")
                        cTasimasekli = GetTasimaSekli(cTasimasekli)

                        oSQL.cSQLQuery = "set dateformat dmy " +
                                        " insert sipmodel (siparisno, bilgisayarsipno, modelno, renk, beden, " +
                                                        " adet, ilksevktar, sonsevktar, ambalaj, tasimasekli, " +
                                                        " musterisiparisno, malzemetakipno, uretimtakipno, sevkiyattakipno, createdate, " +
                                                        " createuser, degistirmetarihi, degistirmesaati, username, bedenseti) "
                        oSQL.cSQLQuery = oSQL.cSQLQuery +
                                        " values ('" + SQLWriteString(cSiparisNo2, 30) + "' , " +
                                        " 0 , " +
                                        " '" + SQLWriteString(cModelNo, 30) + "' , " +
                                        " '" + SQLWriteString(cRenk, 30) + "' , " +
                                        " '" + SQLWriteString(cBeden, 30) + "' , "

                        oSQL.cSQLQuery = oSQL.cSQLQuery +
                                        SQLWriteDecimal(nAdet) + " , " +
                                        " '" + SQLWriteDate(dilksevktar) + "' , " +
                                        " '" + SQLWriteDate(dilksevktar) + "' , " +
                                        " '" + SQLWriteString(cAmbalaj, 30) + "' , " +
                                        " '" + SQLWriteString(cTasimasekli, 30) + "' , "

                        oSQL.cSQLQuery = oSQL.cSQLQuery +
                                        " '" + SQLWriteString(cMusteriSiparisNo, 30) + "' , " +
                                        " '" + SQLWriteString(cSiparisNo2, 30) + "' , " +
                                        " '" + SQLWriteString(cSiparisNo2, 30) + "' , " +
                                        " '" + SQLWriteString(cSiparisNo2, 30) + "' , " +
                                        " convert(date, getdate()) , "

                        oSQL.cSQLQuery = oSQL.cSQLQuery +
                                        " 'ROBO' , " +
                                        " convert(date, getdate()) , " +
                                        " convert(char(8), getdate(), 8) , " +
                                        " 'ROBO', " +
                                        " '" + cBedenSeti + "' ) "

                        oSQL.SQLExecute()
                    Loop
                    oSQL3.oReader.Close()

                    oSQL.cSQLQuery = "set dateformat dmy " +
                                    " update siparis set " +
                                    " ilksevktarihi = '" + SQLWriteDate(dilkSevkTarihiSip) + "', " +
                                    " sonsevktarihi = '" + SQLWriteDate(dilkSevkTarihiSip) + "' " +
                                    " where kullanicisipno = '" + cSiparisNo2 + "' "
                    oSQL.SQLExecute()

                    ' sipariş sertifikaları
                    oSQL.cSQLQuery = "delete siparissertifika " +
                                    " where siparisno = '" + cSiparisNo2 + "' "
                    oSQL.SQLExecute()

                    oSQL.cSQLQuery = "insert siparissertifika (siparisno, sertifikatipi) " +
                                    " select siparisno = '" + cSiparisNo2 + "' , sertifikatipi " +
                                    " from ROBO.dbo.rpa_sipsertifika with (NOLOCK) " +
                                    " where siparisno = '" + cSiparisNo + "' "

                    oSQL.SQLExecute()

                    ' sipariş fiyatlarını aç 
                    oSQL.cSQLQuery = "delete sipfiyat " +
                                    " where siparisno = '" + cSiparisNo2 + "' "
                    oSQL.SQLExecute()

                    oSQL.cSQLQuery = "insert sipfiyat (siparisno, bilgisayarsipno, onmaliyetmodelno, modelkodu, satisfiyati, " +
                                                     " satisdoviz, username, degistirmetarihi, degistirmesaati) "

                    oSQL.cSQLQuery = oSQL.cSQLQuery +
                                    " values ('" + cSiparisNo2 + "', " +
                                    " 0, " +
                                    " '" + SQLWriteString(cCalismaNo, 30) + "' , " +
                                    " '" + SQLWriteString(cModelNo, 30) + "' , " +
                                    SQLWriteDecimal(nFiyat) + " , "

                    oSQL.cSQLQuery = oSQL.cSQLQuery +
                                    " '" + SQLWriteString(cDoviz, 3) + "' , " +
                                    " 'ROBO' , " +
                                    " convert(date, getdate()) , " +
                                    " convert(char(8), getdate(), 8) ) "
                    oSQL.SQLExecute()

                    ' ön sipariş 
                    oSQL.cSQLQuery = "delete siparisonsiparis " +
                                    " where siparisno = '" + cSiparisNo2 + "' "
                    oSQL.SQLExecute()

                    oSQL.cSQLQuery = "insert siparisonsiparis (siparisno, onsiparisno, onsiparisulke2sirano) " +
                                    " select siparisno = '" + cSiparisNo2 + "' , onsiparisno, onsiparisulke2sirano " +
                                    " from ROBO.dbo.rpa_siponsiparis with (NOLOCK) " +
                                    " where siparisno = '" + cSiparisNo + "' "
                    oSQL.SQLExecute()

                    ' önsipariş satırını KAPAT
                    oSQL.cSQLQuery = "update onsiparisulke2 " +
                                    " set kapandi = 'E' " +
                                    " where exists (select siparisno " +
                                                " from siparisonsiparis wirh (NOLOCK) " +
                                                " where siparisno = '" + cSiparisNo2 + "' " +
                                                " and onsiparisno = onsiparisulke2.onsiparisno " +
                                                " and onsiparisulke2sirano = onsiparisulke2.sirano ) "
                    oSQL.SQLExecute()

                    ' ön siparişi kapat
                    oSQL.cSQLQuery = "update onsiparis " +
                                    " set kapandi = 'E' " +
                                    " where exists (select siparisno " +
                                                    " from siparisonsiparis wirh (NOLOCK) " +
                                                    " where siparisno = '" + cSiparisNo2 + "' " +
                                                    " and onsiparisno = onsiparis.onsiparisno ) " +
                                    " and not exists (select onsiparisno " +
                                                    " from onsiparisulke2 with (NOLOCK) " +
                                                    " where onsiparisno = onsiparis.onsiparisno " +
                                                    " and (kapandi is null or kapandi = 'H' or kapandi = '') ) "
                    oSQL.SQLExecute()

                    If Not oSQL.CLRExecute("doafteradd_rpasiparis", cSiparisNo2,,,, cMessage) Then
                        ' Exit Function
                    End If

                    ' alt sipariş döngüsü
                Loop
                oSQL4.oReader.Close()

                ' UTF
                oSQL.CLRExecute("FastUTFBuild", cSiparisNo2)

                ' üretim işemirleri
                oSQL.CLRExecute("UretimisEmriUretCLR", cSiparisNo2, "add")

                ' STF
                oSQL.CLRExecute("FastSTFBuildAll", " and a.kullanicisipno = '" + cSiparisNo2 + "' ")

                ' update siparis
                oSQL.cSQLQuery = "UPDATE siparis " +
                                 " set utfgen = 'E', " +
                                 " stfgen = 'E' " +
                                 " where kullanicisipno = '" + cSiparisNo2 + "' "

                oSQL.SQLExecute()

                oSQL3.cSQLQuery = "update rpa_siparis " +
                                 " set transfered = 'E', " +
                                 " transferuser = 'ROBO', " +
                                 " transferdate = getdate() " +
                                 " where siparisno = '" + cSiparisNo + "' "

                oSQL3.SQLExecute()

                oSQL.CloseConn()

                ' sipariş döngüsü
            Loop
            oSQL2.oReader.Close()

            oSQL4.CloseConn()
            oSQL3.CloseConn()
            oSQL2.CloseConn()

            RPASiparis1 = True

        Catch ex As Exception
            ErrDisp("RPASiparis1", "utilRPA",,, ex)
        End Try
    End Function

    Private Function GetAmbalaj(cAmbalaj As String) As String

        GetAmbalaj = ""

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 kodu " +
                            " from ambalaj with (NOLOCK) " +
                            " where yabanciadi = '" + cAmbalaj + "' "

            GetAmbalaj = oSQL.DBReadString()

            If GetAmbalaj.Trim = "" Then
                GetAmbalaj = cAmbalaj
            End If

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("GetAmbalaj", "utilRPA",,, ex)
        End Try
    End Function

    Private Function GetTasimaSekli(cTasimaSekli As String) As String

        GetTasimaSekli = ""

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 kodu " +
                            " from tasimasekli with (NOLOCK) " +
                            " where yabanciadi = '" + cTasimaSekli + "' "

            GetTasimaSekli = oSQL.DBReadString()

            If GetTasimaSekli.Trim = "" Then
                GetTasimaSekli = cTasimaSekli
            End If

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("GetTasimaSekli", "utilRPA",,, ex)
        End Try
    End Function

End Module
