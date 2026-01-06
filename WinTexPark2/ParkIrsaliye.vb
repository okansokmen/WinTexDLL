Option Explicit On

Imports WinTexPark2.tr.com.parkentegrasyon.wstest
Imports System.Xml
Imports System.Configuration
Imports System.Net
Imports System
Imports System.IO
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net.Http
Imports System.Text
Imports System.Net.Security
Imports System.Net.ServicePointManager
Imports System.Security.Cryptography.X509Certificates

Public Class ParkIrsaliye
    ' Test Portal adresi:               https://efptest.parkentegrasyon.com.tr
    ' e-Fatura & E-Arşiv Fatura WSDL:   https://wstest.parkentegrasyon.com.tr/EFaturaIntegration.asmx
    ' Örnek Proje:                      http://efpconnector.parkentegrasyon.com.tr/PARKConnectorProd/CMCParkEntegrasyonOrnekCalisma.zip
    ' Kullanıcı Adı:                    park-entegrasyon
    ' Şifre:                            cmc2017*!Park
    ' Test VKN:                         7210452742

    ' production için
    ' wsdl adresi:             https://ws.parkentegrasyon.com.tr/EFaturaIntegration.asmx
    ' kullanıcı adı:           burakelikara  /  4842274171
    ' şifre:                   Park2019.     /  4842274171G

    ' Park programcı
    ' Yunus Karaal +90 534 745 70 91
    ' Yunus Karaal <yunus.karaal@parkentegrasyon.com.tr>

    Dim cURL As String = ""
    Dim cUsername As String = ""
    Dim cPassword As String = ""
    Dim cSeri As String = ""
    Dim cToken As String = ""
    Dim oClient As EFaturaIntegration
    Dim oSession As LoginRes

    Public Function init() As Boolean

        init = True

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            ' gerçek URL https://ws.parkentegrasyon.com.tr/EFaturaIntegration.asmx
            ' test   URL https://wstest.parkentegrasyon.com.tr/EFaturaIntegration.asmx

            cURL = oSQL.GetSysPar("ParkService", "https://wstest.parkentegrasyon.com.tr/EFaturaIntegration.asmx")
            cSeri = oSQL.GetSysPar("ParkSeri", "JNC")

            If oConnection.cOwner = "jeansco" Then
                cUsername = oSQL.GetSysPar("ParkUsername", "park-entegrasyon")
                cPassword = oSQL.GetSysPar("ParkPassword", "cmc2017*!Park")
            Else
                cUsername = oSQL.GetSysPar("ParkUsername")
                cPassword = oSQL.GetSysPar("ParkPassword")
            End If

            ' jeansco hard coded
            cURL = "https://ws.parkentegrasyon.com.tr/EFaturaIntegration.asmx"
            cUsername = "4842274171"
            cPassword = "4842274171G."

            oSQL.CloseConn()
            oSQL = Nothing

            oClient = New EFaturaIntegration
            oClient.Url = cURL

            SPMAyar()

            oSession = oClient.OturumAc(cUsername, cPassword)
            If oSession.IsSuccessLogin Then
                cToken = oSession.SessionId
            Else
                CreateLog("WinTex_Park_FailLog", "Login basarisiz oldu. Username / password : " + cUsername + " / " + cPassword)
                init = False
            End If

        Catch ex As Exception
            ErrDisp("init", "ParkIrsaliye",,, ex)
        End Try
    End Function

    Public Function SendEIrsaliye(ByVal nCase As Integer, ByVal cFisNo As String, Optional ByVal lPDF As Boolean = False) As Boolean
        ' nCase = 1 Stok Fişi
        ' nCase = 2 Üretim Fişi

        SendEIrsaliye = False

        Try
            Dim oReturn As InvoiceResponse
            Dim oSQL1 As New SQLServerClass
            Dim oSQL2 As New SQLServerClass
            Dim cFisTipi As String = ""
            Dim nLineCount As Integer = 0
            Dim cKodu As String = ""
            Dim cAdi As String = ""
            Dim nMiktar As Double = 0
            Dim cBirim As String = ""
            Dim cMessage As String = ""
            Dim cIrsaliyeID As String = ""
            Dim cIrsaliyeNumarasi As String = ""
            Dim cSFE As String = ""
            Dim cUFE As String = ""
            Dim nDay As Integer = 0
            Dim nMonth As Integer = 0
            Dim nYear As Integer = 0
            Dim nHour As Integer = 0
            Dim nMinute As Integer = 0
            Dim cTarihSaat As String = ""
            Dim cNotlar As String = ""
            Dim cID As String = ""
            Dim cUUID As String = ""
            Dim cMsg As String = ""
            Dim cMTF As String = ""
            Dim cUTF As String = ""
            Dim cDepartman As String = ""
            Dim cirsdraft As String = ""
            Dim oDespatchLine As List(Of IrsaliyeSatirlari) = New List(Of IrsaliyeSatirlari)()
            Dim cBelgeNo As String = ""

            oSQL1.OpenConn()
            oSQL2.OpenConn()

            cSFE = oSQL1.GetSysPar("irsservicestok", "Butun satirlar")
            cUFE = oSQL1.GetSysPar("irsserviceuretim", "Butun satirlar")
            cirsdraft = oSQL1.GetSysPar("irsdraft", "1")

            Dim irsaliyetasarimList = oClient.FaturaGorseliListesi(cToken)
            If irsaliyetasarimList.Length = 0 Then
                CreateLog("Yüklü tasarım yoktur. Lütfen destekle iletişime geç")
                Exit Function
            End If

            Dim xsltName = Enumerable.FirstOrDefault(Enumerable.Where(irsaliyetasarimList, Function(f) f.Active)).StyleFilesName

            Dim oDespatchInvoice As PARKDespatchType = New PARKDespatchType()

            oDespatchInvoice.XsltName = xsltName
            oDespatchInvoice.Uuid = Guid.NewGuid().ToString()
            oDespatchInvoice.Tipi = "SEVK"
            oDespatchInvoice.TaslaklaraGonderilsin = True
            oDespatchInvoice.IrsaliyeTarihi = Date.Now
            oDespatchInvoice.IrsaliyeZamani = Date.Now
            oDespatchInvoice.IrsaliyeMukellefiMi = True
            'oDespatchInvoice.IrsaliyeGibNumarasi = "" '  GİB STANDART NUMARASI. EĞER SİZ VERMEK İSTEMİYORSANIZ NULL GEÇİN.
            oDespatchInvoice.MalHizmetToplamTutari = 0 ' tutar zorunlu değil 0 geçebilirsiniz.
            oDespatchInvoice.ParaBirimi = "TRY"
            oDespatchInvoice.Profil = "TEMELIRSALIYE"
            oDespatchInvoice.SevkTarihi = Date.Now
            oDespatchInvoice.SevkZamani = Date.Now
            oDespatchInvoice.SiparisNumarasi = 1
            oDespatchInvoice.SiparisTarihi = Date.Now.AddDays(-1)

            'If cirsdraft = "1" Then
            'Else
            '    oDespatchInvoice.TaslaklaraGonderilsin = False ' false yaparsanız direk resmileşir.
            'End If

            Select Case nCase
                Case 1
                    ' stok fişi
                    cFisTipi = "stokfis"
                    cMTF = eIrsaliyeMTF(cFisNo)

                    Select Case cSFE
                        Case "Ana stok grubu + stok tipi gruplu"
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(7, cFisNo)
                        Case "Stok kodu gruplu"
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(11, cFisNo)
                        Case "Stok kodu + renk gruplu"
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(15, cFisNo)
                        Case Else
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(3, cFisNo)
                    End Select

                    nLineCount = oSQL1.DBReadInteger()

                    oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(1, cFisNo)

                    oSQL1.GetSQLReader()

                    If oSQL1.oReader.Read Then

                        cNotlar = oSQL1.SQLReadString("notlar", 100)

                        If oSQL1.SQLReadString("belgeno") = "" Then

                            'oDespatchInvoice.IrsaliyeGibNumarasi = ""
                            cBelgeNo = ""

                            'If InStr(cURL, "wstest") > 0 Then
                            '    ' test portaline gidiyorsa irsaliye numarası zorunlu
                            '    ' production ortamında boş gönderilirse otomatik numara alınır
                            '    cBelgeNo = oSQL1.SQLReadString("stokfisno")
                            '    cBelgeNo = cSeri + Year(Now).ToString + cBelgeNo.Substring(1, 9).Trim
                            '    oDespatchInvoice.IrsaliyeGibNumarasi = cBelgeNo
                            'End If
                        Else
                            cBelgeNo = oSQL1.SQLReadString("belgeno")
                            If Len(cBelgeNo) > 16 Then
                                cBelgeNo = Mid(cBelgeNo, 1, 16)
                            End If
                            'oDespatchInvoice.IrsaliyeGibNumarasi = cBelgeNo
                        End If

                        If oSQL1.SQLReadDate("belgetarihi") <> #1/1/1950# Then
                            oDespatchInvoice.IrsaliyeTarihi = oSQL1.SQLReadDate("belgetarihi")   ' irsaliye tarihi
                            oDespatchInvoice.IrsaliyeZamani = oSQL1.SQLReadDate("belgetarihi")   ' irsaliye saati
                        ElseIf oSQL1.SQLReadDate("fistarihi") <> #1/1/1950# Then
                            oDespatchInvoice.IrsaliyeTarihi = oSQL1.SQLReadDate("fistarihi")     ' irsaliye tarihi
                            oDespatchInvoice.IrsaliyeZamani = oSQL1.SQLReadDate("fistarihi")     ' irsaliye tarihi
                        Else
                            oDespatchInvoice.IrsaliyeTarihi = DateTime.Now.Date                  ' irsaliye tarihi
                            oDespatchInvoice.IrsaliyeZamani = DateTime.Now                       ' irsaliye saati
                        End If

                        ' Malları Teslim alan firmaya ait bilgiler
                        oDespatchInvoice.AliciFirmaBilgileri = New FirmaBilgileri
                        oDespatchInvoice.AliciFirmaBilgileri.FirmaAdi = oSQL1.SQLReadString("f2adi")
                        oDespatchInvoice.AliciFirmaBilgileri.VergiNumarasi = oSQL1.SQLReadString("f2vn")
                        oDespatchInvoice.AliciFirmaBilgileri.VergiDairesi = oSQL1.SQLReadString("f2vd")
                        oDespatchInvoice.AliciFirmaBilgileri.Ulke = oSQL1.SQLReadString("f2ulk")
                        oDespatchInvoice.AliciFirmaBilgileri.Il = oSQL1.SQLReadString("f2shr")
                        oDespatchInvoice.AliciFirmaBilgileri.Ilce = oSQL1.SQLReadString("f2smt")
                        oDespatchInvoice.AliciFirmaBilgileri.Sokak = oSQL1.SQLReadString("f2adr")
                        oDespatchInvoice.AliciFirmaBilgileri.Fax = oSQL1.SQLReadString("f2fax")
                        oDespatchInvoice.AliciFirmaBilgileri.Telefon = oSQL1.SQLReadString("f2tel1")
                        oDespatchInvoice.AliciFirmaBilgileri.Eposta = oSQL1.SQLReadString("f2email")
                        oDespatchInvoice.AliciFirmaBilgileri.PostaKodu = oSQL1.SQLReadString("f2pk")
                        oDespatchInvoice.AliciFirmaBilgileri.Adi = oSQL1.SQLReadString("f2sahis")
                        oDespatchInvoice.AliciFirmaBilgileri.Soyadi = oSQL1.SQLReadString("f2soyad")
                        oDespatchInvoice.AliciFirmaBilgileri.WebSitesi = "WWW."

                        ' teslimat adresi
                        oDespatchInvoice.TeslimatUlkeKodu = oSQL1.SQLReadString("f2ulk")
                        oDespatchInvoice.TeslimatSehir = oSQL1.SQLReadString("f2shr")
                        oDespatchInvoice.TeslimatIlce = oSQL1.SQLReadString("f2smt")
                        oDespatchInvoice.TeslimatAdresi = oSQL1.SQLReadString("f2adr")
                        oDespatchInvoice.TeslimatPostaKodu = oSQL1.SQLReadString("f2pk")

                        oDespatchInvoice.TasiyiciUnvan = oSQL1.SQLReadString("f3adi")
                        oDespatchInvoice.TasiyiciVknTckn = oSQL1.SQLReadString("f3vn")
                        oDespatchInvoice.TasiyiciPlaka = oSQL1.SQLReadString("aracplaka")

                        If oSQL1.SQLReadString("sofortckn") <> "" Then
                            ' Sürücü bilgisi
                            oDespatchInvoice.SoforBilgileri = New SoforBilgileriObj() {New SoforBilgileriObj With {
                                                                                        .SoforAdi = oSQL1.SQLReadString("soforadi"),
                                                                                        .SoforSoyadi = oSQL1.SQLReadString("soforsoyadi"),
                                                                                        .Tckn = oSQL1.SQLReadString("sofortckn")
                                                                                        }}
                        End If

                        nLineCount = -1

                        Select Case cSFE
                            Case "Ana stok grubu + stok tipi gruplu"
                                oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(9, cFisNo)
                            Case "Stok kodu gruplu"
                                oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(12, cFisNo)
                            Case "Stok kodu + renk gruplu"
                                oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(16, cFisNo)
                            Case Else
                                oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(5, cFisNo)
                        End Select

                        oSQL2.GetSQLReader()

                        Do While oSQL2.oReader.Read

                            Select Case cSFE
                                Case "Ana stok grubu + stok tipi gruplu"
                                    cKodu = oSQL2.SQLReadString("anastokgrubu", 15)
                                    cID = oSQL2.SQLReadString("stoktipi")
                                    cAdi = oSQL2.SQLReadString("stoktipi")
                                Case "Stok kodu gruplu"
                                    cKodu = oSQL2.SQLReadString("anastokgrubu", 15)
                                    cID = oSQL2.SQLReadString("stokno")
                                    cAdi = oSQL2.SQLReadString("stoktipi")
                                Case "Stok kodu + renk gruplu"
                                    cKodu = oSQL2.SQLReadString("stokno")
                                    cID = oSQL2.SQLReadString("anastokgrubu", 15)
                                    cAdi = oSQL2.SQLReadString("stoktipi") + " " + IIf(oSQL2.SQLReadString("renk") = "" Or oSQL2.SQLReadString("renk") = "HEPSI", "", " " + oSQL2.SQLReadString("renk"))
                                Case Else
                                    cKodu = oSQL2.SQLReadString("anastokgrubu") + "-" + oSQL2.SQLReadString("stoktipi")
                                    cID = oSQL2.SQLReadString("stokno")
                                    cAdi = IIf(oSQL2.SQLReadString("renk") = "" Or oSQL2.SQLReadString("renk") = "HEPSI", "", " " + oSQL2.SQLReadString("renk")) +
                                           IIf(oSQL2.SQLReadString("beden") = "" Or oSQL2.SQLReadString("beden") = "HEPSI", "", " " + oSQL2.SQLReadString("beden")) +
                                           IIf(oSQL2.SQLReadString("malzemetakipkodu") = "", "", " " + oSQL2.SQLReadString("malzemetakipkodu"))
                            End Select

                            nMiktar = oSQL2.SQLReadDouble("netmiktar1")
                            cBirim = oSQL2.SQLReadString("birim1")

                            nLineCount = nLineCount + 1

                            Select Case cBirim.ToLower.Trim
                                Case "m2"
                                    cBirim = "MTK"
                                Case "m3"
                                    cBirim = "MTQ"
                                Case "lt", "litre"
                                    cBirim = "LTR"
                                Case "mt", "metre"
                                    cBirim = "MTR"
                                Case "kg"
                                    cBirim = "KGM"
                                Case Else
                                    cBirim = "C62"
                            End Select

                            oDespatchLine.Add(New IrsaliyeSatirlari With {
                                                .AliciStokKodu = cID,
                                                .Birim = cBirim,
                                                .BirimFiyat = 0,
                                                .ParaBirimi = "TRY",
                                                .SatirNo = nLineCount + 1,
                                                .SonraTeslimEdilecekMiktar = 0,
                                                .StokAciklamasi = cAdi,
                                                .StokKodu = cKodu,
                                                .TeslimedilecekMiktar = CDec(nMiktar),
                                                .UrunAdi = cAdi,
                                                .Tutar = 0})
                        Loop
                        oSQL2.oReader.Close()
                        oSQL2.oReader = Nothing

                    End If
                    oSQL1.oReader.Close()
                    oSQL1.oReader = Nothing

                Case 2

                    ' üretim fişi 
                    cFisTipi = "uretimfisi"
                    cUTF = eIrsaliyeUTF(cFisNo)

                    Select Case cUFE
                        Case "Ana model tipi + model no gruplu"
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(8, cFisNo)
                        Case "Siparis gruplu"
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(13, cFisNo)
                        Case Else
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(4, cFisNo)
                    End Select

                    nLineCount = oSQL1.DBReadInteger()

                    oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(2, cFisNo)

                    oSQL1.GetSQLReader()

                    If oSQL1.oReader.Read Then

                        cDepartman = oSQL1.SQLReadString("girisdept")

                        If oSQL1.SQLReadString("belgeno") = "" Then

                            ' oDespatchInvoice.IrsaliyeGibNumarasi = null
                            cBelgeNo = ""

                            'If InStr(cURL, "wstest") > 0 Then
                            '    cBelgeNo = oSQL1.SQLReadString("uretfisno")
                            '    cBelgeNo = cSeri + Year(Now).ToString + cBelgeNo.Substring(1, 9).Trim
                            '    oDespatchInvoice.IrsaliyeGibNumarasi = cBelgeNo
                            'End If
                        Else
                            cBelgeNo = oSQL1.SQLReadString("belgeno")
                            If Len(cBelgeNo) > 16 Then
                                cBelgeNo = Mid(cBelgeNo, 1, 16)
                            End If
                            'oDespatchInvoice.IrsaliyeGibNumarasi = cBelgeNo
                        End If

                        If oSQL1.SQLReadDate("belgetarihi") <> #1/1/1950# Then
                            oDespatchInvoice.IrsaliyeTarihi = oSQL1.SQLReadDate("belgetarihi")   ' irsaliye tarihi
                            oDespatchInvoice.IrsaliyeZamani = oSQL1.SQLReadDate("belgetarihi")   ' irsaliye saati
                        ElseIf oSQL1.SQLReadDate("fistarihi") <> #1/1/1950# Then
                            oDespatchInvoice.IrsaliyeTarihi = oSQL1.SQLReadDate("fistarihi")     ' irsaliye tarihi
                            oDespatchInvoice.IrsaliyeZamani = oSQL1.SQLReadDate("fistarihi")     ' irsaliye tarihi
                        Else
                            oDespatchInvoice.IrsaliyeTarihi = DateTime.Now.Date                  ' irsaliye tarihi
                            oDespatchInvoice.IrsaliyeZamani = DateTime.Now                       ' irsaliye saati
                        End If

                        ' Malları Teslim alan firmaya ait bilgiler
                        oDespatchInvoice.AliciFirmaBilgileri = New FirmaBilgileri
                        oDespatchInvoice.AliciFirmaBilgileri.FirmaAdi = oSQL1.SQLReadString("f2adi")
                        oDespatchInvoice.AliciFirmaBilgileri.VergiNumarasi = oSQL1.SQLReadString("f2vn")
                        oDespatchInvoice.AliciFirmaBilgileri.VergiDairesi = oSQL1.SQLReadString("f2vd")
                        oDespatchInvoice.AliciFirmaBilgileri.Ulke = oSQL1.SQLReadString("f2ulk")
                        oDespatchInvoice.AliciFirmaBilgileri.Il = oSQL1.SQLReadString("f2shr")
                        oDespatchInvoice.AliciFirmaBilgileri.Ilce = oSQL1.SQLReadString("f2smt")
                        oDespatchInvoice.AliciFirmaBilgileri.Sokak = oSQL1.SQLReadString("f2adr")
                        oDespatchInvoice.AliciFirmaBilgileri.Fax = oSQL1.SQLReadString("f2fax")
                        oDespatchInvoice.AliciFirmaBilgileri.Telefon = oSQL1.SQLReadString("f2tel1")
                        oDespatchInvoice.AliciFirmaBilgileri.Eposta = oSQL1.SQLReadString("f2email")
                        oDespatchInvoice.AliciFirmaBilgileri.PostaKodu = oSQL1.SQLReadString("f2pk")
                        oDespatchInvoice.AliciFirmaBilgileri.Adi = oSQL1.SQLReadString("f2sahis")
                        oDespatchInvoice.AliciFirmaBilgileri.Soyadi = oSQL1.SQLReadString("f2soyad")
                        oDespatchInvoice.AliciFirmaBilgileri.WebSitesi = "WWW."

                        ' teslimat adresi
                        oDespatchInvoice.TeslimatUlkeKodu = oSQL1.SQLReadString("f2ulk")
                        oDespatchInvoice.TeslimatSehir = oSQL1.SQLReadString("f2shr")
                        oDespatchInvoice.TeslimatIlce = oSQL1.SQLReadString("f2smt")
                        oDespatchInvoice.TeslimatAdresi = oSQL1.SQLReadString("f2adr")
                        oDespatchInvoice.TeslimatPostaKodu = oSQL1.SQLReadString("f2pk")

                        oDespatchInvoice.TasiyiciUnvan = oSQL1.SQLReadString("f3adi")
                        oDespatchInvoice.TasiyiciVknTckn = oSQL1.SQLReadString("f3vn")
                        oDespatchInvoice.TasiyiciPlaka = oSQL1.SQLReadString("aracplaka")

                        If oSQL1.SQLReadString("sofortckn") <> "" Then
                            ' Sürücü bilgisi
                            oDespatchInvoice.SoforBilgileri = New SoforBilgileriObj() {New SoforBilgileriObj With {
                                                                                        .SoforAdi = oSQL1.SQLReadString("soforadi"),
                                                                                        .SoforSoyadi = oSQL1.SQLReadString("soforsoyadi"),
                                                                                        .Tckn = oSQL1.SQLReadString("sofortckn")
                                                                                        }}
                        End If

                        nLineCount = -1

                        Select Case cUFE
                            Case "Ana model tipi + model no gruplu"
                                oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(10, cFisNo)
                            Case "Siparis gruplu"
                                oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(14, cFisNo)
                            Case Else
                                oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(6, cFisNo)
                        End Select

                        oSQL2.GetSQLReader()

                        Do While oSQL2.oReader.Read

                            Select Case cUFE
                                Case "Ana model tipi + model no gruplu"
                                    cID = oSQL2.SQLReadString("anamodeltipi")
                                    cKodu = oSQL2.SQLReadString("aciklama")
                                    cAdi = oSQL2.SQLReadString("modelno")
                                Case "Siparis gruplu"

                                    cID = oSQL2.SQLReadString("uretimtakipno")

                                    If cDepartman = "YIKAMA" Then
                                        cID = cID + " / " + oSQL2.SQLReadString("yikama")
                                    End If

                                    cKodu = oSQL2.SQLReadString("anamodeltipi")

                                    cAdi = oSQL2.SQLReadString("musteri") + " " + oSQL2.SQLReadString("aciklama")

                                    cAdi = oSQL2.SQLReadString("uretimtakipno")
                                Case Else
                                    cID = oSQL2.SQLReadString("anamodeltipi")
                                    cKodu = oSQL2.SQLReadString("aciklama")
                                    cAdi = oSQL2.SQLReadString("modelno") +
                                           IIf(oSQL2.SQLReadString("renk") = "", "", " " + oSQL2.SQLReadString("renk")) +
                                           IIf(oSQL2.SQLReadString("beden") = "", "", " " + oSQL2.SQLReadString("beden"))
                            End Select

                            nMiktar = oSQL2.SQLReadDouble("adet")

                            cBirim = "ADET"

                            nLineCount = nLineCount + 1

                            oDespatchLine.Add(New IrsaliyeSatirlari With {
                                                .AliciStokKodu = cID,
                                                .Birim = "C62",
                                                .BirimFiyat = 0,
                                                .ParaBirimi = "TRY",
                                                .SatirNo = nLineCount + 1,
                                                .SonraTeslimEdilecekMiktar = 0,
                                                .StokAciklamasi = cAdi,
                                                .StokKodu = cKodu,
                                                .TeslimedilecekMiktar = CDec(nMiktar),
                                                .UrunAdi = cAdi,
                                                .Tutar = 0})
                        Loop
                        oSQL2.oReader.Close()
                        oSQL2.oReader = Nothing

                    End If
                    oSQL1.oReader.Close()
                    oSQL1.oReader = Nothing
            End Select

            oDespatchInvoice.IrsaliyeSatirlari = oDespatchLine.ToArray

            oReturn = oClient.IrsaliyeGonder(oDespatchInvoice, cSeri, oDespatchInvoice.Uuid, cToken)

            If oReturn.isSuccess Then

                cMessage = "Belge başarıyla gönderildi " +
                            oReturn.DetailsDesc.ToString.Trim +
                           " ID: " + oReturn.InvoiceRef.ToString.Trim +
                           " UUID: " + oReturn.Uuid.ToString.Trim

                CreateLog("WinTex_Park_SuccessLog", cMessage)

                cIrsaliyeID = oReturn.InvoiceRef
                cIrsaliyeNumarasi = cIrsaliyeID
                cUUID = oDespatchInvoice.Uuid

                'GetIrsaliyeNoUUID(cIrsaliyeID, cIrsaliyeNumarasi, cUUID)

                nDay = oDespatchInvoice.IrsaliyeTarihi.Day
                nMonth = oDespatchInvoice.IrsaliyeTarihi.Month
                nYear = oDespatchInvoice.IrsaliyeTarihi.Year
                nHour = oDespatchInvoice.IrsaliyeTarihi.Hour
                nMinute = oDespatchInvoice.IrsaliyeTarihi.Minute

                cTarihSaat = Strings.Format(nDay, "00") + "-" + Strings.Format(nMonth, "00") + "-" + Strings.Format(nYear, "0000") + " " +
                             Strings.Format(nHour, "00") + ":" + Strings.Format(nMinute, "00") + ":00"

                eIrsaliyeUpdateWinTex(nCase, cFisNo, cIrsaliyeNumarasi, cUUID, cTarihSaat, cIrsaliyeID)

                SendEIrsaliye = True

                'If lPDF Then
                '    DespatchPDF(cUUID, cFisNo, cFisTipi)
                'End If

            Else
                cMessage = String.Format("Belge gönderilirken hata oluştu {0} ", oReturn.DetailsDesc)

                cMessage = cMessage + vbCrLf +
                           "URL: " + cURL + vbCrLf +
                           "Kullanıcı: " + cUsername + vbCrLf +
                           "Şifre: " + cPassword + vbCrLf

                CreateLog("WinTex_Park_FailLog", cMessage)
                'MsgBox(cMessage)
                SendEIrsaliye = False
            End If

            oClient.OturumKapat(cToken)

            oSQL1.CloseConn()
            oSQL1 = Nothing

            oSQL2.CloseConn()
            oSQL2 = Nothing

        Catch ex As Exception
            ErrDisp("SendEIrsaliye", "TurkKEPeIrsaliye",,, ex)
        End Try

    End Function

    Public Function GetIrsaliyeNoUUID(ByVal cIrsaliyeID As String, ByRef cIrsaliyeNumarasi As String, ByVal cUUID As String) As Boolean

        GetIrsaliyeNoUUID = False

        'Try
        '    If cToken.Trim = "" Then Exit Function

        '    Dim oDIRT As PARKDespatchType = oClient.GidenFaturaOku(cIrsaliyeID, cUUID, cToken)
        '    If Not (oDIRT Is Nothing) Then
        '        Dim oIrsaliye As DespatchInvoice = oDIRT.DespatchInvoice
        '        If Not (oIrsaliye Is Nothing) Then
        '            If Not (oIrsaliye.DispatchReference Is Nothing) Then
        '                cIrsaliyeNumarasi = oIrsaliye.DispatchReference.ToString.Trim
        '                cUUID = oIrsaliye.UUID.ToString.Trim
        '                GetIrsaliyeNoUUID = True
        '            End If
        '        End If
        '    End If

        'Catch ex As Exception
        '    ErrDisp("GetIrsaliyeNoUUID", "TurkKEPeIrsaliye",,, ex)
        'End Try
    End Function

    Public Function DespatchPDF(ByVal cUID As String, Optional ByVal cFisNo As String = "", Optional ByVal cFisTipi As String = "") As String
        ' cIrsaliyeID burada UID oluyor
        DespatchPDF = ""

        Try
            If cToken.Trim = "" Then Exit Function

            Dim cMessage As String = ""
            Dim oIrsaliye() As Byte = oClient.GidenIrsaliyePdfAl(cUID, cToken)

            If oIrsaliye Is Nothing Then
                cMessage = String.Format("PDF alınamadı {0} ", cUID)
            Else
                cMessage = String.Format("PDF alındı {0} ", cUID)
                DespatchPDF = eIrsaliyeStoreDocument(cFisNo, cFisTipi, oIrsaliye)
            End If

        Catch ex As Exception
            ErrDisp("DespatchPDF", "ParkIrsaliye",,, ex)
        End Try
    End Function

    Public Function GetStatus(ByVal cUID As String, ByRef cIrsaliyeNumarasi As String) As String
        ' cIrsaliyeID burada UID oluyor
        GetStatus = ""

        Try
            If cToken.Trim = "" Then Exit Function

            Dim cMessage As String = ""
            Dim oIrsaliye As ParkInvoiceStatusRes = oClient.GidenIrsaliyeDurumSorgula(cUID, cToken)

            cIrsaliyeNumarasi = oIrsaliye.GibCode

            GetStatus = oIrsaliye.StatusDesc.ToString.Trim

        Catch ex As Exception
            ErrDisp("GetStatus", "ParkIrsaliye",,, ex)
        End Try
    End Function

    Private Sub SPMAyar()
        Try
            ' ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3
            ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True

        Catch ex As Exception
            Console.WriteLine("SPMAyar : " & ex.Message)
        End Try
    End Sub

End Class
