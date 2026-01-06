Option Explicit On

Imports RestSharp
Imports System.Globalization
Imports System.ServiceModel
Imports System.ServiceModel.Channels

Module utilPTT

    Private Function GetPTTBarcode(cSiparisNo As String) As String

        GetPTTBarcode = ""

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 kargotakipno " +
                            " from sipperakende with (NOLOCK) " +
                            " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "

            GetPTTBarcode = oSQL.DBReadString

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp("GetPTTBarcode",,,, ex)
        End Try
    End Function

    Public Function PTTSendOrder1(Optional cSiparisNo As String = "", Optional ByRef cSonuc As String = "") As Boolean

        ' Ek hizmet kodları

        ' PI - PTT ISYERINE TESLIM
        ' UA - ÜCRETİ ALICIDAN TAHSİL
        ' OS - ÖDEME ŞARTLI

        ' AA - ADRESTEN ALMA
        ' ST - ŞEHİR İÇİ TESLİM
        ' DK - DEĞER KONULMUŞ
        ' AH - ALMA HABERLİ
        ' AK - ALICININ KENDİNE TESLİM
        ' TA - TELEFONLA BİLGİLENDİRME
        ' KT - KONTROLLU TESLIM
        ' OU - ÖZEL ULAK
        ' GD - GİDİŞ - DÖNÜŞ
        ' SV - SERVİS
        ' RP - RESMİ PUL
        ' UO - ÜCRET ÖDEME MAKİNESİ
        ' VI - KREDİ KARTI
        ' PC - POSTA ÇEKİ HESABI
        ' DN - BARKOD DÖNÜŞLÜ
        ' AT - ADLI TIP
        ' PR - POSTRESTANT
        ' SB - SMS ILE BILGILENDIRME

        PTTSendOrder1 = False

        Try
            If CheckFirmaCalisilmasin("PTT") Then Exit Function

            Dim oSQL As New SQLServerClass
            Dim oPTT As New PTTService.SorguPortTypeClient("SorguHttpSoap12Endpoint")
            Dim oInput As New PTTService.Input2
            Dim oOutput As New PTTService.Output2
            Dim oDonguItem As New PTTService.InputDongu2
            Dim lOK As Boolean = False
            Dim cEkHizmet As String = "OS/DK"
            Dim cAraciKargo As String = ""
            Dim cKargocuTahsilatYapmaz As String = "H"
            Dim cOdemeTipi As String = ""
            Dim cBarkod As String = ""

            If Not GetServiceConnectionParameters(cSiparisNo, oConnection.cPTTApiUserName, oConnection.cPTTApiPassword, oConnection.cPTTMusteri, "PTT") Then Exit Function

            oSQL.OpenConnTransaction()

            oInput.musteriId = CInt(oConnection.cPTTMusteri)
            oInput.musteriIdSpecified = True
            oInput.dosyaAdi = cSiparisNo
            oInput.gonderiTur = "KARGO"
            oInput.gonderiTip = "NORMAL"
            oInput.kullanici = oConnection.cPTTApiUserName
            oInput.sifre = oConnection.cPTTApiPassword

            oSQL.cSQLQuery = "select top 1 tarih, adi, soyadi, ili, " +
                            " ilcesi, telefon, adres, kargofirmasi, odemetipi, " +
                            " teslimsekli, finaltutar, kargotakipno, " +
                            " kargocutahsilatyapmaz = (select top 1 kargocutahsilatyapmaz " +
                                                        " from odemetipi with (NOLOCK)  " +
                                                        " where kod = sipperakende.odemetipi), " +
                            " ilid = (select top 1 pttid " +
                                    " from turkiyesehirler with (NOLOCK) " +
                                    " where sehir = sipperakende.ili), " +
                            " ilceid = (select top 1 pttid " +
                                    " from turkiyeilceler with (NOLOCK) " +
                                    " where sehir = sipperakende.ili " +
                                    " and ilce = sipperakende.ilcesi), " +
                            " pttbarkod = (select top 1 barkod " +
                                    " from pttbarkod with (NOLOCK) " +
                                    " where not exists (select siparisno " +
                                                    " from sipperakende with (NOLOCK) " +
                                                    " where kargotakipno = pttbarkod.barkod) " +
                                     " order by barkod desc) " +
                            " from sipperakende with (NOLOCK) " +
                            " where siparisno = '" + cSiparisNo.Trim + "' "

            oSQL.GetSQLReaderTransaction()

            If oSQL.oReader.Read Then

                If oSQL.SQLReadString("kargotakipno") = "" Then
                    cBarkod = oSQL.SQLReadString("pttbarkod")
                Else
                    cBarkod = oSQL.SQLReadString("kargotakipno")
                End If

                cOdemeTipi = oSQL.SQLReadString("odemetipi")
                cKargocuTahsilatYapmaz = oSQL.SQLReadString("kargocutahsilatyapmaz")

                oDonguItem.aliciAdi = oSQL.SQLReadString("adi") + " " + oSQL.SQLReadString("soyadi")
                oDonguItem.aAdres = oSQL.SQLReadString("adres") + " " + oSQL.SQLReadString("ili") + " " + oSQL.SQLReadString("ilcesi")
                oDonguItem.aIlKodu = oSQL.SQLReadString("ilid")
                oDonguItem.aIlceKodu = oSQL.SQLReadString("ilceid")
                oDonguItem.agirlik = 1
                oDonguItem.aliciAdi = oSQL.SQLReadString("adi") + " " + oSQL.SQLReadString("soyadi")
                oDonguItem.aliciIlAdi = oSQL.SQLReadString("ili")
                oDonguItem.aliciIlceAdi = oSQL.SQLReadString("ilcesi")
                oDonguItem.aliciSms = oSQL.SQLReadString("telefon")
                oDonguItem.barkodNo = cBarkod
                oDonguItem.aliciTel = oSQL.SQLReadString("telefon")
                oDonguItem.musteriReferansNo = cSiparisNo

                oDonguItem.boy = 35
                oDonguItem.boySpecified = True
                oDonguItem.en = 20
                oDonguItem.enSpecified = True
                oDonguItem.yukseklik = 12
                oDonguItem.yukseklikSpecified = True
                oDonguItem.agirlik = 1000
                oDonguItem.agirlikSpecified = True
                oDonguItem.desi = 20 * 35 * 12 / 3000
                oDonguItem.desiSpecified = True

                ' İşte PTT kargo ödeme tipleri;
                ' N:  Kargo ücretinin nakit ödendiği/ödeneceği anlamına gelir.
                ' MH: Kargonun ücretinin gönderici tarafından ödendiğini belirtir.
                ' PO: Kargo ücreti peşin ödendi demektir.
                ' UA: Kargo ücretinin teslim edilecek kişiden alınacağını belirtir.

                Select Case oSQL.SQLReadString("odemetipi")
                    Case "PESIN - UCRET ALICI"
                        oDonguItem.odemesekli = "UA"
                    Case "PESIN - UCRET GONDERICI"
                        oDonguItem.odemesekli = "MH"
                    Case "KAPIDA KREDI KARTI"
                    Case "KAPIDA NAKIT"
                End Select

                Select Case oSQL.SQLReadString("teslimsekli")
                    Case "KAPIDA TESLIM"
                    Case "KARGO ACENTESINDE TESLIM"
                        cEkHizmet = cEkHizmet + "/PI"
                End Select

                If cKargocuTahsilatYapmaz = "E" Or cOdemeTipi = "PESIN - UCRET ALICI" Or cOdemeTipi = "PESIN - UCRET GONDERICI" Then
                    oDonguItem.odeme_sart_ucreti = 0
                    oDonguItem.odeme_sart_ucretiSpecified = True

                    oDonguItem.deger_ucreti = 0
                    oDonguItem.deger_ucretiSpecified = True

                    'oDonguItem.ucret = 0
                    'oDonguItem.ucretSpecified = True
                Else
                    oDonguItem.odeme_sart_ucreti = oSQL.SQLReadDouble("finaltutar")
                    oDonguItem.odeme_sart_ucretiSpecified = True

                    oDonguItem.deger_ucreti = oSQL.SQLReadDouble("finaltutar")
                    oDonguItem.deger_ucretiSpecified = True

                    'oDonguItem.ucret = oSQL.SQLReadDouble("finaltutar")
                    'oDonguItem.ucretSpecified = True
                End If

                oDonguItem.ekhizmet = cEkHizmet + "/--"

                lOK = True

            End If
            oSQL.oReader.Close()

            If lOK Then

                oInput.dongu = {oDonguItem}

                oOutput = oPTT.kabulEkle2(oInput)

                cSonuc = oOutput.aciklama

                If oOutput.hataKodu = 1 Then

                    PTTSendOrder1 = True

                    oSQL.cSQLQuery = "update sipperakende " +
                                    " set kargotakipno = '" + cBarkod + "', " +
                                    " kargoyakayityollandi = 'E', " +
                                    " kargoyakayityollanmatarihi = getdate() " +
                                    " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "

                    oSQL.SQLExecuteTransaction()

                    oSQL.cSQLQuery = "select top 1 aracikargo " +
                                    " from firma with (NOLOCK) " +
                                    " where firma = 'PTT' "

                    cAraciKargo = oSQL.DBReadStringTransaction()

                    If cAraciKargo.Trim <> "" Then

                        oSQL.cSQLQuery = "update sipperakende " +
                                    " set aracikargo = '" + cAraciKargo + "' " +
                                    " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') " +
                                    " and (aracikargo is null or aracikargo = '') "

                        oSQL.SQLExecuteTransaction()
                    End If
                End If
            End If

            oSQL.CloseConnTransaction()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp("PTTSendOrder2",,,, ex)
        End Try
    End Function

    Public Function PTTIlceSorgula1() As Boolean

        PTTIlceSorgula1 = False

        Try
            If CheckFirmaCalisilmasin("PTT") Then Exit Function

            Dim oSQL As New SQLServerClass
            Dim oPTT As New PTTBilgi.SorguPortTypeClient("SorguHttpSoap12Endpoint1")
            Dim oSonuc As PTTBilgi.OutputDonguIlce

            Dim oInput As New PTTBilgi.Input
            oInput.kullanici = oConnection.cPTTBilgiUserName
            oInput.sifre = oConnection.cPTTBilgiPassword

            Dim oOutput As New PTTBilgi.OutputIlce
            oOutput = oPTT.ilceSorgula(oInput)

            If oOutput.sonucKodu = 1 Then

                oSQL.OpenConn()

                For Each oSonuc In oOutput.dongu

                    oSQL.cSQLQuery = "update turkiyesehirler " +
                                     " set pttid = '" + SQLWriteString(oSonuc.il_id.ToString, 10) + "' " +
                                     " where sehir = '" + oSonuc.il_ad.ToString.Trim + "' "
                    oSQL.SQLExecute()

                    oSQL.cSQLQuery = "update turkiyeilceler " +
                                     " set pttid = '" + SQLWriteString(oSonuc.ilce_id.ToString, 10) + "' " +
                                     " where sehir = '" + oSonuc.il_ad.ToString.Trim + "' " +
                                     " and ilce = '" + oSonuc.ilce_ad.ToString.Trim + "' "
                    oSQL.SQLExecute()

                    oSQL.cSQLQuery = "update turkiyeilceler " +
                                     " set pttid = '" + SQLWriteString(oSonuc.ilce_id.ToString, 10) + "' " +
                                     " where sehir like '" + SearchFit(oSonuc.il_ad.ToString.Trim) + "' " +
                                     " and ilce like '" + SearchFit(oSonuc.ilce_ad.ToString.Trim) + "' "
                    oSQL.SQLExecute()
                Next

                oSQL.CloseConn()

                PTTIlceSorgula1 = True
            End If

        Catch ex As Exception
            ErrDisp("PTTIlceSorgula1",,,, ex)
        End Try
    End Function

    Public Function PTTEkHizmetSorgula1() As Boolean
        ' kullanılmıyor
        PTTEkHizmetSorgula1 = False

        Try
            If CheckFirmaCalisilmasin("PTT") Then Exit Function

            Dim cSonuc As String = ""
            Dim oPTT As New PTTBilgi.SorguPortTypeClient("SorguHttpSoap12Endpoint1")
            Dim oSonuc As PTTBilgi.OutputDonguEkHizmet

            Dim oInput As New PTTBilgi.Input
            oInput.kullanici = oConnection.cPTTBilgiUserName
            oInput.sifre = oConnection.cPTTBilgiPassword

            Dim oOutput As New PTTBilgi.OutputEkHizmet
            oOutput = oPTT.ekHizmetSorgula(oInput)

            If oOutput.sonucKodu = 1 Then

                For Each oSonuc In oOutput.dongu
                    If cSonuc.Trim = "" Then
                        cSonuc = oSonuc.ek_hizmet_ad + " / " + oSonuc.ek_hizmet_id.ToString
                    Else
                        cSonuc = cSonuc + vbCrLf + oSonuc.ek_hizmet_ad + " / " + oSonuc.ek_hizmet_id.ToString
                    End If
                Next

                PTTEkHizmetSorgula1 = True

            End If

        Catch ex As Exception
            ErrDisp("PTTEkHizmetSorgula1",,,, ex)
        End Try
    End Function

    Public Function PTTCancelOrder1(ByVal cSiparisNo As String, Optional ByRef cErrorMessage As String = "") As Boolean

        cErrorMessage = ""
        PTTCancelOrder1 = False

        Try
            If CheckFirmaCalisilmasin("PTT") Then Exit Function
            If Not GetServiceConnectionParameters(cSiparisNo, oConnection.cPTTApiUserName, oConnection.cPTTApiPassword, oConnection.cPTTMusteri, "PTT") Then Exit Function

            Dim oSQL As New SQLServerClass
            Dim oPTT As New PTTService.SorguPortTypeClient("SorguHttpSoap12Endpoint")
            Dim oInput As New PTTService.InputDelete
            Dim oOutput As New PTTService.OutputDelete
            Dim cBarkod As String = GetPTTBarcode(cSiparisNo)

            oInput.musteriId = CInt(oConnection.cPTTMusteri)
            oInput.musteriIdSpecified = True
            oInput.sifre = oConnection.cPTTApiPassword
            oInput.dosyaAdi = cSiparisNo
            oInput.barcode = cBarkod

            oOutput = oPTT.barkodVeriSil(oInput)

            cErrorMessage = oOutput.aciklama.ToString.Trim

            If oOutput.hataKodu = 1 Then
                oSQL.OpenConn()
                oSQL.cSQLQuery = "update sipperakende " +
                                " set iptal = 'E', " +
                                " iptaltarih = getdate() " +
                                " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "
                oSQL.SQLExecute()
                oSQL.CloseConn()

                CreateLog("WinTexPTTLog", "Sipariş Iptal Edildi / PTTCancelOrder1 : " + cSiparisNo)

                PTTCancelOrder1 = True
            End If

        Catch ex As Exception
            ErrDisp("PTTCancelOrder1",,,, ex)
        End Try
    End Function

    Public Function PTTQueryOrder1(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        Dim cDebugPoint As String = "1"

        cSonuc = ""
        cErrorMessage = ""
        PTTQueryOrder1 = False

        Try
            If CheckFirmaCalisilmasin("PTT") Then Exit Function
            If Not GetServiceConnectionParameters(cSiparisNo, oConnection.cPTTApiUserName, oConnection.cPTTApiPassword, oConnection.cPTTMusteri, "PTT") Then Exit Function

            Dim oSQL As New SQLServerClass
            Dim oPTT As New PTTGonderiTakipV2.SorguPortTypeClient("SorguHttpSoap12Endpoint2")
            Dim oInput As New PTTGonderiTakipV2.Input
            Dim oOutput As New PTTGonderiTakipV2.OutputTum
            Dim cBarkod As String = GetPTTBarcode(cSiparisNo)

            Dim cCikisSube As String = ""
            Dim cTeslimSube As String = ""
            Dim cKargoStatu As String = ""
            Dim cOrjinalGonderiCikisTarihi As String = ""
            Dim cGonderiCikisTarihi As String = ""
            Dim dGonderiCikisTarihi As Date = #1/1/1950#
            Dim cDurum As String = "SIPARIS ALINDI"
            Dim cKargoTutari As String = ""
            Dim nKargoTutari As Double = 0
            Dim dKargoStatuTarihi As Date = #1/1/1950#
            Dim cKargoStatuTarihi As String = ""
            Dim nKargoKgDesi As Double = 0
            Dim cGrField As String = ""
            Dim oGrField() As String
            Dim nKargoUrunBedeli As Double = 0
            Dim cKargoUrunBedeli As String = ""
            Dim cTarih As String = ""
            Dim cSaat As String = ""
            Dim nCnt As Integer = 0
            Dim cSube As String = ""
            Dim nSonucKodu As Integer = 0
            Dim cAciklama As String = ""
            Dim cIKodu As String = ""
            Dim cIslem As String = ""
            Dim cSiraNo As String = ""
            Dim cKargoMesaj As String = ""

            If IsNothing(oPTT) Then
                CreateLog("WinTexPTTLog", "oPTT obj nothing " + cSiparisNo)
                Exit Function
            End If

            If IsNothing(oInput) Then
                CreateLog("WinTexPTTLog", "oInput obj nothing " + cSiparisNo)
                Exit Function
            End If

            'cBarkod = "2752938860765"
            oInput.kullanici = oConnection.cPTTMusteri ' 204512996
            oInput.sifre = oConnection.cPTTApiPassword ' jPocWIy40ATZzEDZUZIZZg
            oInput.barkod = cBarkod

            oOutput = oPTT.gonderiSorgu(oInput)

            If IsNothing(oOutput) Then
                CreateLog("WinTexPTTLog", "oOutput obj nothing " + oConnection.cPTTMusteri + vbCrLf +
                          oConnection.cPTTApiPassword + vbCrLf +
                          cBarkod + vbCrLf +
                          cSiparisNo)
                Exit Function
            End If

            If oOutput.sonucKodu = -15 Then
                ' barkod bulunamadı
                CreateLog("WinTexPTTLog", oOutput.sonucAciklama + vbCrLf +
                          "Sipariş No : " + cSiparisNo + vbCrLf +
                          "Barkod : " + cBarkod)
                Exit Function
            End If

            cKargoUrunBedeli = WebReadString(oOutput.DEGKONUCR)
            If cKargoUrunBedeli.Trim = "" Then
                cKargoUrunBedeli = "0"
            Else
                cKargoUrunBedeli = Replace(cKargoUrunBedeli, "TL", "").Trim
                If IsNumeric(cKargoUrunBedeli) Then
                    nKargoUrunBedeli = CDbl(cKargoUrunBedeli)
                End If
            End If

            cGrField = WebReadString(oOutput.GR)
            If InStr(cGrField, "/") > 0 Then
                oGrField = Split(cGrField, "/")
                cGrField = oGrField(1)
                cGrField = Replace(cGrField, "D.", "").Trim
                If IsNumeric(cGrField) Then
                    nKargoKgDesi = CDbl(cGrField)
                    If nKargoKgDesi >= 100 Then
                        nKargoKgDesi = nKargoKgDesi / 100
                    End If
                End If
            End If

            cKargoTutari = WebReadString(oOutput.GONUCR)
            If cKargoTutari.Trim = "" Then
                cKargoTutari = "0"
            Else
                cKargoTutari = Replace(cKargoTutari, "TL", "").Trim
                If IsNumeric(cKargoTutari) Then
                    nKargoTutari = CDbl(cKargoTutari)
                End If
            End If

            cKargoStatu = WebReadString(oOutput.sonucKodu)
            cCikisSube = WebReadString(oOutput.IMERK)

            cOrjinalGonderiCikisTarihi = WebReadString(oOutput.ITARIH)
            cGonderiCikisTarihi = Mid(cOrjinalGonderiCikisTarihi, 7, 2) + "." + Mid(cOrjinalGonderiCikisTarihi, 5, 2) + "." + Mid(cOrjinalGonderiCikisTarihi, 1, 4)
            If IsDate(cGonderiCikisTarihi) Then
                dGonderiCikisTarihi = CDate(cGonderiCikisTarihi)
            Else
                dGonderiCikisTarihi = #1/1/1950#
            End If

            cTeslimSube = WebReadString(oOutput.VMERK)
            cErrorMessage = WebReadString(oOutput.sonucAciklama)

            cSonuc = ""
            If Not IsNothing(oOutput.dongu) Then
                For Each oOD In oOutput.dongu
                    ' siraNo    // Gönderinin geçtiği işlem sırası 
                    ' ISLEM     // Gönderinin geçtiği işlem 
                    ' IMERK     // İşlemin meydana geldiği merkez 
                    ' ITARIH    // İşlemin meydana geldiği tarih
                    If Not IsNothing(oOD) Then
                        cSiraNo = WebReadString(oOD.siraNo)
                        cIslem = WebReadString(oOD.ISLEM)
                        cIKodu = WebReadString(oOD.IKODU)
                        cSube = WebReadString(oOD.IMERK)
                        cSaat = WebReadString(oOD.ISAAT)
                        If IsDate(cGonderiCikisTarihi) Then
                            cKargoStatuTarihi = cGonderiCikisTarihi + " " + cSaat
                            dKargoStatuTarihi = DateTime.ParseExact(cKargoStatuTarihi, "dd.MM.yyyy HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                        Else
                            cKargoStatuTarihi = "01.01.1950"
                            dKargoStatuTarihi = #1/1/1950#
                        End If
                        If cSonuc.Trim = "" Then
                            cSonuc = cSiraNo + " " + cIKodu + " " + cSube + " " + cIslem + " " + cGonderiCikisTarihi + " " + cSaat
                        Else
                            cSonuc = cSonuc + vbCrLf + cSiraNo + " " + cIKodu + " " + cSube + " " + cIslem + " " + cGonderiCikisTarihi + " " + cSaat
                        End If

                        cKargoMesaj = WebReadString(oOD.ISLEM)
                        nSonucKodu = WebReadInt(oOD.IKODU)
                        PTTSorguSonuc(nSonucKodu, cDurum, cAciklama)

                        nCnt = nCnt + 1
                        If nCnt = 1 Then
                            cDurum = "CIKIS SUBEDE"
                        End If
                    End If
                Next
            End If

            oSQL.OpenConn()

            Select Case cDurum

                Case "TESLIM EDILDI"

                    oSQL.cSQLQuery = "update sipperakende " +
                                    " set kapandi = 'E', " +
                                    " kapanmatarihi = '" + SQLWriteDateTime(dKargoStatuTarihi) + "', " +
                                    " iade = null, " +
                                    " iadetarihi = null, " +
                                    " iptal = null, " +
                                    " iptaltarih = null " +
                                    " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "
                    oSQL.SQLExecute()

                Case "IADE"

                    oSQL.cSQLQuery = "update sipperakende " +
                                    " set iade = 'E', " +
                                    " iadetarihi = '" + SQLWriteDateTime(dKargoStatuTarihi) + "', " +
                                    " kapandi = null, " +
                                    " kapanmatarihi = null, " +
                                    " iptal = null, " +
                                    " iptaltarih = null " +
                                    " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "
                    oSQL.SQLExecute()

            End Select

            oSQL.cSQLQuery = "update sipperakende " +
                                " set kargotakipno = '" + SQLWriteString(cBarkod, 30) + "', " +
                                " kargotutari = " + SQLWriteDecimal(nKargoTutari) + ", " +
                                " kargotakipurl = 'https://gonderitakip.ptt.gov.tr/', " +
                                " kargosondurumkodu = '" + SQLWriteString(cKargoStatu, 3) + "', " +
                                " kargosondurumtarihi = getdate(), " +
                                " kargokgdesi = " + SQLWriteDecimal(nKargoKgDesi) + ", " +
                                " kargotahsilattutari = " + SQLWriteDecimal(nKargoUrunBedeli) + ", " +
                                " kargostatutarihi = '" + SQLWriteDateTime(dKargoStatuTarihi) + "', " +
                                " kargostatutarihiorjinal = '" + SQLWriteString(cKargoStatuTarihi, 30) + "', " +
                                " gondericikistarihi = '" + SQLWriteDateTime(dGonderiCikisTarihi) + "', " +
                                " gondericikistarihiorjinal = '" + SQLWriteString(cOrjinalGonderiCikisTarihi, 30) + "', " +
                                " kargomesaj = '" + SQLWriteString(cKargoMesaj, 100) + "', " +
                                " kargostatu = '" + SQLWriteString(cDurum, 100) + "', " +
                                " cikis_sube = '" + SQLWriteString(cCikisSube, 30) + "', " +
                                " teslim_sube = '" + SQLWriteString(cTeslimSube, 30) + "' " +
                                " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "
            oSQL.SQLExecute()

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("PTTQueryOrder1 " + cDebugPoint, ,,, ex)
        End Try
    End Function

    Private Sub PTTSorguSonuc(ByVal nSonucKodu As Integer, ByRef cDurum As String, ByRef cAciklama As String)
        ' Standart Durum Kodları
        ' SIPARIS ALINDI
        ' SIPARIS HAZIRLANIYOR
        ' CIKIS SUBEDE
        ' YOLDA
        ' VARIS SUBEDE
        ' DAGITIMDA
        ' TESLIM EDILDI
        ' TESLIM EDILEMEDI
        ' IADE

        cDurum = ""
        cAciklama = ""

        Try
            Select Case nSonucKodu
                Case 1, 701
                    cDurum = "CIKIS SUBEDE" ' KABUL

                Case 159, 471, 470, 472
                    cDurum = "YOLDA" ' GUNCELLEME

                Case 831, 192, 191
                    cDurum = "SORUNLU" ' TASFIYE

                Case 77, 8, 9, 3, 6, 4, 78, 303, 5, 91, 302, 999, 320, 14, 149, 11, 13, 92, 148
                    cDurum = "YOLDA"

                Case 7, 12
                    cDurum = "DAGITIMDA"

                Case 565, 583
                    ' el terminali
                    cDurum = "DAGITIMDA"

                Case 100, 252, 157, 807, 818, 812, 202, 813, 221, 222, 223, 824, 220, 225, 226, 227, 224, 815, 232, 814, 229, 230, 231, 228, 826, 829, 827, 825, 816,
                     828, 106, 121, 138, 399
                    cDurum = "TESLIM EDILDI"

                Case 15, 109, 118, 127, 147, 105, 150, 140, 101, 145, 125, 146, 380, 107, 113, 124, 126, 112, 131, 144, 381, 111, 143, 117, 388, 389, 142, 110, 139,
                     132, 114, 115, 116, 716, 194, 712, 108, 128, 713, 129, 711, 709, 714, 717, 718, 211, 710, 130, 715, 719, 196, 195, 199, 197, 720, 262, 650, 653, 657,
                     699, 651, 656, 654, 655
                    cDurum = "SORUNLU"

                Case 600, 601, 532, 568, 531, 557, 567, 569, 530, 566, 585, 578
                    ' el terminali
                    cDurum = "SORUNLU"

                Case 156, 99, 151, 163, 161, 164, 830, 133, 154, 155, 152, 160, 162, 134, 135, 136, 153, 166, 158, 167, 137, 168, 198, 169, 120, 141, 849, 854, 855,
                     856, 260, 261, 832
                    cDurum = "IADE"

                Case 527, 560, 528, 545, 547, 556, 529, 546
                    ' el terminali
                    cDurum = "IADE"

                Case 165
                    cDurum = "SORUNLU" ' IADE IPTAL

                Case 501
                    cDurum = "SORUNLU" ' IMHA IPTAL

                Case 2
                    cDurum = "SORUNLU" ' KABUL IPTAL

                Case 176, 177, 174, 175, 172, 178, 170, 171, 173
                    cDurum = "SORUNLU" ' GERI ZIMMET

                Case 21
                    cDurum = "SORUNLU" ' SISTEMDEN CIKARTILDI

                Case Else
                    cDurum = "SIPARIS HAZIRLANIYOR" ' KABUL
            End Select

            Select Case nSonucKodu
                Case 1 : cAciklama = "Kabul Edildi, 0, KABUL, 1000"
                Case 2 : cAciklama = "İptal Edildi, 0, KABUL İPTAL, 7000"
                Case 3 : cAciklama = "Torbaya Eklendi, 0, SEVK, 2000"
                Case 4 : cAciklama = "Torbadan Çıkarıldı, 0, SEVK, 2000"
                Case 5 : cAciklama = "Torbası İptal Edildi, 0, SEVK, 2000"
                Case 6 : cAciklama = "Torbadan Alìndì, 0, SEVK, 2000"
                Case 7 : cAciklama = "Dağıtıcıya Verildi, 1, DAĞITIM, 3000"
                Case 8 : cAciklama = "Zimmet Alındı, 0, SEVK, 2000"
                Case 9 : cAciklama = "Zimmet Edildi, 0, SEVK, 2000"
                Case 10 : cAciklama = "Problemli Gönderi, 0, DİĞER, 9999"
                Case 11 : cAciklama = "Gönderinin Geliş Kaydı Yapıldı, 0, SEVK, 2000"
                Case 12 : cAciklama = "Cihetten Gönderi Çıkarıldı, -1, DAĞITIM, 3000"
                Case 13 : cAciklama = "Merkez Kayıt Defterine Eklendi, 0, SEVK, 2000"
                Case 14 : cAciklama = "Kayıt Defterinden Silindi, 0, SEVK, 2000"
                Case 15 : cAciklama = "Teslim İptal Edildi, 0, TESLİM İPTAL, 5600"
                Case 16 : cAciklama = "Yurtdışı Kabul Geliş Kaydı Yapıldı, 0, 1500"
                Case 831 : cAciklama = "Tasfiye Edildi, 0, TASFİYE, 999"
                Case 701 : cAciklama = "Kayıt Edildi, 0, KABUL, 1000"
                Case 159 : cAciklama = "Güncelleme Yapıldı, 0, GUNCELLEME, 1106"
                Case 303 : cAciklama = "Gönderi Gümrükte Tutuluyor, -1, SEVK, 2000"
                Case 77 : cAciklama = "Sevk Edildi, 0, SEVK, 2000"
                Case 78 : cAciklama = "PTB'den Çıkarıldı, 0, SEVK, 2000"
                Case 91 : cAciklama = "Torbası Sevk Edildi, 0, SEVK, 2000"
                Case 92 : cAciklama = "Torbasının Geliş Kaydı Yapıldı, 0, SEVK, 2000"
                Case 99 : cAciklama = "İade Edilecek, -1, İADE, 4000"
                Case 302 : cAciklama = "Gümrüğe Sevk Edildi, -1, SEVK, 2000"
                Case 999 : cAciklama = "Zimmet İptal Edildi, 0, SEVK, 2000"
                Case 320 : cAciklama = "Yurtdışına Sevk Edildi -1,SEVK, 2000"
                Case 149 : cAciklama = "Ptt Tarafından Zimmet Alındı, 0, SEVK, 2000"
                Case 148 : cAciklama = "Firma Tarafından Zimmet Alındı, 0, SEVK, 2000"
                Case 156 : cAciklama = "İADE-Diğer, -1, İADE, 4000"
                Case 151 : cAciklama = "İADE-Tanınmıyor, -1, İADE, 4000"
                Case 163 : cAciklama = "İADE-Aranılmadı, -1, İADE, 4000"
                Case 161 : cAciklama = "İADE-Adreste Yok, -1, İADE, 4000"
                Case 164 : cAciklama = "İADE-İthali Yasak, -1, İADE, 4000"
                Case 830 : cAciklama = "İADE-Geri İstendi, -1, İADE, 4000"
                Case 133 : cAciklama = "İADE-Kayıp Gönderi, -1, İADE, 4000"
                Case 154 : cAciklama = "İADE-Kabul Edilmedi, -1, İADE, 4000"
                Case 155 : cAciklama = "İADE-Adres Yetersiz, -1, İADE, 4000"
                Case 152 : cAciklama = "İADE-Adresden Ayrılmış, -1, İADE, 4000"
                Case 160 : cAciklama = "İADE-Binaya Girilemedi, -1, İADE, 4000"
                Case 162 : cAciklama = "İADE-Alıcı Vefat Etmiş, -1, İADE, 4000"
                Case 134 : cAciklama = "İADE-Alıcısı Vefat Etmiş, -1, İADE, 4000"
                Case 135 : cAciklama = "İADE-Geri İstendi (Banka), -1, İADE, 4000"
                Case 136 : cAciklama = "İADE-İade İstendi (Banka), -1, İADE, 4000"
                Case 153 : cAciklama = "İADE-Bekleme Süresi Bitti, -1, İADE, 4000"
                Case 166 : cAciklama = "İADE:Kimlik İbraz Edilmedi, -1, İADE, 4000"
                Case 158 : cAciklama = "İADE-Adres Hatalı/Okunmuyor, -1, İADE, 4000"
                Case 167 : cAciklama = "İADE:Tutanaklı/Talimatlı İşlem, -1, İADE, 4000"
                Case 137 : cAciklama = "İADE-Dağıtım Sahası Dışı Sevk (Banka), -1, İADE, 4000"
                Case 168 : cAciklama = "İADE:Muhatap Adresi Değişmiş/Yeni Adres Bulunumadı, -1, İADE, 4000"
                Case 198 : cAciklama = "İADE:Vergi Tebligatı 2. Dağıtımda Tebliğ Edilemedi, -1, İADE, 4000"
                Case 169 : cAciklama = "İADE:Adresteki Bina Yıkılmış veya Bina Özelliğini Tamamen Yitirmiştir, -1, İADE, 4000"
                Case 165 : cAciklama = "İade İptal Edildi, 0, İADE İPTAL, 4500"
                Case 100 : cAciklama = "Teslim Edildi, -1, TESLİM, 5000"
                Case 252 : cAciklama = "MAZBATA TESLİM, -1, TESLİM, 5000"
                Case 157 : cAciklama = "Evrak Memuruna Teslim, -1, TESLİM, 5000"
                Case 807 : cAciklama = "MUHATABA BiZZAT TESLiM, -1, TESLİM, 5000"
                Case 818 : cAciklama = "iSYERiNDE AMiRiNE TESLiM, -1, TESLİM, 5000"
                Case 812 : cAciklama = "AYNI KONUTTA YAKINA TESLiM, -1, TESLİM, 5000"
                Case 202 : cAciklama = "Kargomatikten Teslim Alındı, -1, TESLİM, 5000"
                Case 813 : cAciklama = "21.MAD. GORE MUHTARA TESLiM, -1, TESLİM, 5000"
                Case 221 : cAciklama = "İkinci Adreste Kendisine Teslim, -1, TESLİM, 5000"
                Case 222 : cAciklama = "Üçüncü Adreste Kendisine Teslim, -1, TESLİM, 5000"
                Case 223 : cAciklama = "Farklı Adreste Kendisine Teslim, -1, TESLİM, 5000"
                Case 824 : cAciklama = "iSYERiNDE DAiMi CALISANA TESLiM, -1, TESLİM, 5000"
                Case 220 : cAciklama = "Birinci Adreste Kendisine Teslim, -1, TESLİM, 5000"
                Case 225 : cAciklama = "İkinci Adreste Aile Bireyine Teslim, -1, TESLİM, 5000"
                Case 226 : cAciklama = "Üçüncü Adreste Aile Bireyine Teslim, -1, TESLİM, 5000"
                Case 227 : cAciklama = "Farklı Adreste Aile Bireyine Teslim, -1, TESLİM, 5000"
                Case 224 : cAciklama = "Birinci Adreste Aile Bireyine Teslim, -1, TESLİM, 5000"
                Case 815 : cAciklama = "21.MAD. GORE ZABITA AMiRi/MEMURA TESLiM, -1, TESLİM, 5000"
                Case 232 : cAciklama = "Teslim Edildi(Tutanaklı/Talimatlı İşlem), -1, TESLİM, 5000"
                Case 814 : cAciklama = "21.MAD. GORE iHTiYAR HEYETi/AZAYA TESLiM, -1, TESLİM, 5000"
                Case 229 : cAciklama = "İkinci Adreste İş Yeri Yetkilisine Teslim, -1, TESLİM, 5000"
                Case 230 : cAciklama = "Üçüncü Adreste İş Yeri Yetkilisine Teslim, -1, TESLİM, 5000"
                Case 231 : cAciklama = "Farklı Adreste İş Yeri Yetkilisine Teslim, -1, TESLİM, 5000"
                Case 228 : cAciklama = "Birinci Adreste İş Yeri Yetkilisine Teslim, -1, TESLİM, 5000"
                Case 826 : cAciklama = "BAĞIMSIZ BÖLÜMDE OTURAN YOK, PANOYA ASILDI  -1, TESLİM, 5000"
                Case 829 : cAciklama = "21/2. Maddeye Göre Muhtara Teslim (MERNİS)  -1, TESLİM, 5000"
                Case 827 : cAciklama = "21.MADDE VE EK.1 E GÖRE TEBLİĞ, PANOYA ASILDI, -1, TESLİM, 5000"
                Case 825 : cAciklama = "BAĞIMSIZ BÖLÜMDE OTURANA TEBLİĞ, PANOYA ASILDI, -1, TESLİM, 5000"
                Case 816 : cAciklama = "35.MAD. GORE MUHATAP ADRESi KAPISINA YAPISTIRMA, -1, TESLİM, 5000"
                Case 828 : cAciklama = "Muhatap imtinaEttigindenSahitleÖnüneBirakilmistir, -1, TESLİM, 5000"
                Case 120 : cAciklama = "Göndericisine Teslim Edildi, -1, GÖNDERİCİSİNE TESLİM, 5500"
                Case 141 : cAciklama = "İadeten Banka Şubesine Teslim, -1, GÖNDERİCİSİNE TESLİM, 5500"
                Case 106 : cAciklama = "Banka Şubesine Teslim, -1, TESLİM, 5501"
                Case 121 : cAciklama = "Alma Haber Kartı Teslimi, -1, TESLİM, 5502"
                Case 138 : cAciklama = "Sözleşme Teslim (Banka), -1, TESLİM, 5503"
                Case 109 : cAciklama = "İmha, -1, TESLİM EDİLEMEDİ, 6000"
                Case 118 : cAciklama = "Diğer, -1, TESLİM EDİLEMEDİ, 6000"
                Case 127 : cAciklama = "Askerde, -1, TESLİM EDİLEMEDİ, 6000"
                Case 147 : cAciklama = "El Koyma, -1, TESLİM EDİLEMEDİ, 6000"
                Case 105 : cAciklama = "Geri İstendi, -1, TESLİM EDİLEMEDİ, 6000"
                Case 150 : cAciklama = "İade İstendi, -1, TESLİM EDİLEMEDİ, 6000"
                Case 140 : cAciklama = "Kabul Edilmedi, -1, TESLİM EDİLEMEDİ, 6000"
                Case 101 : cAciklama = "Teslim Edilemedi, -1, TESLİM EDİLEMEDİ, 6000"
                Case 145 : cAciklama = "Imha-Vefat Etmiş, -1, TESLİM EDİLEMEDİ, 6000"
                Case 125 : cAciklama = "İsim/Soyad Hatalı, -1, TESLİM EDİLEMEDİ, 6000"
                Case 146 : cAciklama = "Binaya Girilemedi, -1, TESLİM EDİLEMEDİ, 6000"
                Case 380 : cAciklama = "Diğer Adrese Sevk, -1, TESLİM EDİLEMEDİ, 6000"
                Case 107 : cAciklama = "Üçüncü Adrese Sevk, -1, TESLİM EDİLEMEDİ, 6000"
                Case 113 : cAciklama = "İkinci Adrese Sevk, -1, TESLİM EDİLEMEDİ, 6000"
                Case 124 : cAciklama = "Adreste Yok/Kapalı, -1, TESLİM EDİLEMEDİ, 6000"
                Case 126 : cAciklama = "Adreste Tanınmıyor, -1, TESLİM EDİLEMEDİ, 6000"
                Case 112 : cAciklama = "PTT İşyerine Teslim, -1, TESLİM EDİLEMEDİ, 6000"
                Case 131 : cAciklama = "Sorunlu Alıcı/Diğer, -1, TESLİM EDİLEMEDİ, 6000"
                Case 144 : cAciklama = "Imha-Kabul Edilmedi, -1, TESLİM EDİLEMEDİ, 6000"
                Case 381 : cAciklama = "Alıcısı Vefat Etmiş, -1, TESLİM EDİLEMEDİ, 6000"
                Case 111 : cAciklama = "Haber Kağıdı Bırakıldı, -1, TESLİM EDİLEMEDİ, 6000"
                Case 143 : cAciklama = "Imha-Banka Geri İstedi, -1, TESLİM EDİLEMEDİ, 6000"
                Case 117 : cAciklama = "Yargıtay Teslimli Kargo, -1, TESLİM EDİLEMEDİ, 6000"
                Case 388 : cAciklama = "İliçi Diğer Adrese Sevk, 0, TESLİM EDİLEMEDİ, 6000"
                Case 389 : cAciklama = "İldışı Diğer Adrese Sevk, 0, TESLİM EDİLEMEDİ, 6000"
                Case 142 : cAciklama = "Imha-Bekleme Süresi Bitti, -1, TESLİM EDİLEMEDİ, 6000"
                Case 110 : cAciklama = "PTT İşyerine Teslim Edildi, -1, TESLİM EDİLEMEDİ, 6000"
                Case 139 : cAciklama = "Adresten Ayrılmış/Taşınmış, -1, TESLİM EDİLEMEDİ, 6000"
                Case 501 : cAciklama = "İmha İptal Edildi, 0, İMHA İPTAL, 5700"
                Case 399 : cAciklama = "Teslim Edilen Gönderi Ücreti Yatırıldı, 0, ODEME, 3500"
                Case 132 : cAciklama = "Adres Hatalı/Adres Yetersiz, -1, TESLİM EDİLEMEDİ, 6000"
                Case 114 : cAciklama = "Mutemetin Gelmesi Bekleniyor, -1, TESLİM EDİLEMEDİ, 6000"
                Case 115 : cAciklama = "Köy Dağıtımına Tabi Bekliyor, -1, TESLİM EDİLEMEDİ, 6000"
                Case 116 : cAciklama = "2.Kez Haber Kağıdı Bırakıldı, -1, TESLİM EDİLEMEDİ, 6000"
                Case 716 : cAciklama = "Gönderi Akıbeti Araştırılıyor, -1, TESLİM EDİLEMEDİ, 6000"
                Case 194 : cAciklama = "Hava Şartları/Terör/Doğal Afet, 0, TESLİM EDİLEMEDİ, 6000"
                Case 712 : cAciklama = "E-posta Gönderilemedi Bekliyor, -1, TESLİM EDİLEMEDİ, 6000"
                Case 108 : cAciklama = "Geçici Olarak Adresten Ayrılmış, -1, TESLİM EDİLEMEDİ, 6000"
                Case 128 : cAciklama = "Sorunlu Alıcı-Kimlik Göstermedi, -1, TESLİM EDİLEMEDİ, 6000"
                Case 713 : cAciklama = "Alıcı Talimatı ile Bekletiliyor, -1, TESLİM EDİLEMEDİ, 6000"
                Case 129 : cAciklama = "Sorunlu Alıcı-TC No İsim Tutmuyor, -1, TESLİM EDİLEMEDİ, 6000"
                Case 711 : cAciklama = "SMS ile Mesaj Gönderildi Bekliyor, -1, TESLİM EDİLEMEDİ, 6000"
                Case 709 : cAciklama = "PTT İşyeri Teslim Cihetine Eklendi, -1, TESLİM EDİLEMEDİ, 6000"
                Case 714 : cAciklama = "Gönderici Talimatı ile Bekletiliyor, -1, TESLİM EDİLEMEDİ, 6000"
                Case 717 : cAciklama = "Gönderi Akıbeti Belirlenemedi/Kayıp, -1, TESLİM EDİLEMEDİ ,6000"
                Case 718 : cAciklama = "Mazbata Akıbeti Belirlenemedi/Kayıp, -1, TESLİM EDİLEMEDİ, 6000"
                Case 211 : cAciklama = "Telefon ile Haber Verilmedi Bekliyor, -1, TESLİM EDİLEMEDİ, 6000"
                Case 710 : cAciklama = "SMS ile Mesaj Gönderilemedi Bekliyor, -1, TESLİM EDİLEMEDİ, 6000"
                Case 130 : cAciklama = "Sorunlu Alıcı/Sözleşmeyi İmzalamıyor/Şerh, -1, TESLİM EDİLEMEDİ, 6000"
                Case 715 : cAciklama = "Teslim Edilemedi(Tutanaklı/Talimatlı İşlem), -1, TESLİM EDİLEMEDİ, 6000"
                Case 719 : cAciklama = "Vergi Tebligatı 1. Dağıtımda Tebliğ Edilemedi, -1, TESLİM EDİLEMEDİ, 6000"
                Case 196 : cAciklama = "Dağıtım Binaya Girilemedi-Haber Kağıdı Bırakıldı, -1, TESLİM EDİLEMEDİ, 6000"
                Case 195 : cAciklama = "Dağıtım Adreste Yok/Kapalı-Haber Kağıdı Bırakıldı, -1, TESLİM EDİLEMEDİ, 6000"
                Case 199 : cAciklama = "PTT ye Teslim Binaya Girilemedi-Haber Kağıdı Bırakıldı, -1, TESLİM EDİLEMEDİ, 6000"
                Case 197 : cAciklama = "PTT ye Teslim Adreste Yok/Kapalı-Haber Kağıdı Bırakıldı, -1, TESLİM EDİLEMEDİ, 6000"
                Case 176 : cAciklama = "EL KOYMA, 1, GERİ ZİMMET, 7500"
                Case 177 : cAciklama = "SÖZLEŞME, 1, GERİ ZİMMET, 7500"
                Case 174 : cAciklama = "2. ADRESE SEVK, 1, GERİ ZİMMET, 7500"
                Case 175 : cAciklama = "3. ADRESE SEVK, 1, GERİ ZİMMET, 7500"
                Case 172 : cAciklama = "SERVİS GÖNDERİSİ, 1, GERİ ZİMMET, 7500"
                Case 178 : cAciklama = "ALMA HABER KARTI, 1, GERİ ZİMMET, 7500"
                Case 170 : cAciklama = "PTT ADRESLİ GÖNDERİ, 1, GERİ ZİMMET, 7500"
                Case 171 : cAciklama = "KÖY DAĞITIMINA TABİ GÖNDERİ, 1, GERİ ZİMMET, 7500"
                Case 173 : cAciklama = "DAĞITIM SAHASI DIŞI GÖNDERİ, 1, GERİ ZİMMET, 7500"
                Case 600 : cAciklama = "Tanınmıyor, -1, EL TERMİNALİ, 8000"
                Case 601 : cAciklama = "Tanınmıyor, -1, EL TERMİNALİ, 8000"
                Case 532 : cAciklama = "Yerinde Yok, -1, EL TERMİNALİ, 8000"
                Case 568 : cAciklama = "Yerinde Yok, -1, EL TERMİNALİ, 8000"
                Case 531 : cAciklama = "Adres Hatalı, -1, EL TERMİNALİ, 8000"
                Case 557 : cAciklama = "Yanlış Cihet, -1, EL TERMİNALİ, 8000"
                Case 567 : cAciklama = "Adres Hatalı, -1, EL TERMİNALİ, 8000"
                Case 569 : cAciklama = "Yanlış Cihet, -1, EL TERMİNALİ, 8000"
                Case 530 : cAciklama = "Adres Okunmuyor, -1, EL TERMİNALİ, 8000"
                Case 566 : cAciklama = "Adres Okunmuyor, -1, EL TERMİNALİ, 8000"
                Case 527 : cAciklama = "İade : Alıcısı Ölmüş, -1, EL TERMİNALİ, 8000"
                Case 560 : cAciklama = "İade : Alıcısı Ölmüş, -1, EL TERMİNALİ, 8000"
                Case 528 : cAciklama = "İade : Adres Yetersiz, -1, EL TERMİNALİ, 8000"
                Case 545 : cAciklama = "İade : Adres Yetersiz, -1, EL TERMİNALİ, 8000"
                Case 565 : cAciklama = "Haber Kağıdı Bırakıldı, -1, EL TERMİNALİ, 8000"
                Case 583 : cAciklama = "PTT İşyerinde İhbarlandı, -1, EL TERMİNALİ, 8000"
                Case 547 : cAciklama = "İade : Alıcısı Kabul Etmiyor, -1, EL TERMİNALİ, 8000"
                Case 556 : cAciklama = "İade : Alıcısı Kabul Etmiyor, -1, EL TERMİNALİ, 8000"
                Case 529 : cAciklama = "İade : Adresten Ayrılmış(Yeni Adresi Yok), -1, EL TERMİNALİ, 8000"
                Case 546 : cAciklama = "İade : Adresten Ayrılmış(Yeni Adresi Yok), -1, EL TERMİNALİ, 8000"
                Case 585 : cAciklama = "Teslim Edilemedi : PTT İşyerinde İhbarlandı, -1EL TERMİNALİ, 8000"
                Case 578 : cAciklama = "Teslim Edilemedi : Adresten Ayrılmış(Yeni Adrese Sevk), -1, EL TERMİNALİ, 8000"
                Case 652 : cAciklama = "Talimat Bekleniyor, -1, DEPO, 8500"
                Case 650 : cAciklama = "PTT İşyerinde Bekliyor, -1, DEPO, 8500"
                Case 653 : cAciklama = "PTT İşyerinde Bekliyor, -1, DEPO, 8500"
                Case 657 : cAciklama = "PTT İşyerinde Bekliyor, -1, DEPO, 8500"
                Case 699 : cAciklama = "Depodan Gönderi Çıkartıldı, 0, DEPO, 8500"
                Case 651 : cAciklama = "Köy Dağıtımına Tabi Bekliyor, -1, DEPO, 8500"
                Case 656 : cAciklama = "(MEVKUF) Elde Kalmış Gönderi, -1, DEPO, 8500"
                Case 654 : cAciklama = "(İADE) PTT İşyerinde Bekliyor, -1, DEPO, 8500"
                Case 655 : cAciklama = "(BANKA İADE) PTT İşyerinde Bekliyor, -1, DEPO, 8500"
                Case 21 : cAciklama = "Sistemden Çıkartıldı, -1, SİSTEMDEN ÇIKARTILDI, 9000"
                Case 307 : cAciklama = "TEST, -1, DİĞER, 9999"
                Case 180 : cAciklama = "Kayıp, -1, DİĞER, 9999"
                Case 181 : cAciklama = "Hasar, -1, DİĞER, 9999"
                Case 240 : cAciklama = "Satıldı, 0, DİĞER, 9999"
                Case 185 : cAciklama = "El Koyma, -1, DİĞER, 9999"
                Case 301 : cAciklama = "Onaylandı, 0, DİĞER, 9999"
                Case 955 : cAciklama = "Sözleşme Kayıp, 0, DİĞER, 9999"
                Case 183 : cAciklama = "Tazminat Ödendi, -1, DİĞER, 9999"
                Case 300 : cAciklama = "Siparis Verildi, 0, DİĞER, 9999"
                Case 304 : cAciklama = "Gümrükten Çıktı, 0, DİĞER, 9999"
                Case 192 : cAciklama = "Kayıp (Tasfiye), 0, TASFİYE, 9999"
                Case 305 : cAciklama = "Onay İptal Edildi, 0, DİĞER, 9999"
                Case 103 : cAciklama = "Elde Kalmış Gönderi, -1, DİĞER, 9999"
                Case 954 : cAciklama = "Sözleşme Tamamlandı,  0, DİĞER, 9999"
                Case 182 : cAciklama = "Araştırma Başlatıldı, -1, DİĞER, 9999"
                Case 184 : cAciklama = "Süresinde Aranılmadı, -1, DİĞER, 9999"
                Case 306 : cAciklama = "E-Telgraf Gönderildi, -1, DİĞER, 9999"
                Case 201 : cAciklama = "İhb-Kargomatiğe Bırakıldı, -1, DİĞER, 9999"
                Case 450 : cAciklama = "Talimat İptal Edildi, 0, TALİMAT, 9999"
                Case 210 : cAciklama = "Telefonla Haber Verildi, 0, DİĞER, 9999"
                Case 700 : cAciklama = "Talimat İçin Bekleniyor, -1, DİĞER, 9999"
                Case 950 : cAciklama = "Sözleşme Kontrol Edildi, 0, DİĞER, 9999"
                Case 952 : cAciklama = "Sözleşme İmza Sürecinde, 0, DİĞER, 9999"
                Case 186 : cAciklama = "Tutanaklı/Talimatlı İşlem, -1, DİĞER, 9999"
                Case 191 : cAciklama = "PTT (Tasfiye) Geri Aldı, 0, TASFİYE, 9999"
                Case 953 : cAciklama = "Sözleşme Tamamlatılamıyor, 0, DİĞER, 9999"
                Case 102 : cAciklama = "PTT Terk/Hesabıma Satılsın, -1, DİĞER, 9999"
                Case 241 : cAciklama = "Elde Kalmış Gönderi İmhası, 0, DİĞER, 9999"
                Case 471 : cAciklama = "Posta Çeki Güncellendi, 0, GUNCELLEME, 9999"
                Case 119 : cAciklama = "Banka İsteğiyle Geri Çekildi, 0, DİĞER, 9999"
                Case 188 : cAciklama = "Kayıp Tebligat Suret İstendi, -1, DİĞER, 9999"
                Case 403 : cAciklama = "Talimat Verildi(Geri Alma), 0, TALİMAT, 9999"
                Case 470 : cAciklama = "Ödeme Bedeli Güncellendi, 0, GUNCELLEME, 9999"
                Case 951 : cAciklama = "Sözleşme Kontrol İptal Edildi, 0, DİĞER, 9999"
                Case 190 : cAciklama = "Gönderici Talebi İle Geri Alındı, -1, DİĞER, 9999"
                Case 410 : cAciklama = "Talimat Verildi(II.Adrese Git), 0, TALİMAT, 9999"
                Case 413 : cAciklama = "Talimat Verildi(3. Adrese Git), 0, TALİMAT, 9999"
                Case 44 : cAciklama = "Cihet Hazırlama Listesine Eklendi, -1, DIGER, 9999"
                Case 472 : cAciklama = "Ödeme Şart Türü Güncellendi, 0, GUNCELLEME, 9999"
                Case 411 : cAciklama = "Talimat Verildi(İade Adresine Git), 0, TALİMAT, 9999"
                Case 720 : cAciklama = "Kargomatta Bekleme Süresi Bitti, -1, TESLİM EDİLEMEDİ, 6000"
                Case 262 : cAciklama = "Kimlik Bilgisi Alınamadı, -1, TESLİM EDİLEMEDİ, 6000"
                Case 45 : cAciklama = "Cihet Hazırlama Listesinden Çıkarıldı, -1, DIGER, 9999"
                Case 423 : cAciklama = "Talimat Etiketi Üretildi(Geri Alma), 0, TALİMAT, 9999"
                Case 187 : cAciklama = "Hatalı Tebliğ Nedeni İle Suret İstendi, -1, DİĞER, 9999"
                Case 203 : cAciklama = "İade Edilmek Üzere Kargomatikten Alındı, -1, DİĞER, 9999"
                Case 412 : cAciklama = "Talimat Verildi(Banka Şubesine Teslim), 0, TALİMAT, 9999"
                Case 308 : cAciklama = "Mazbata Datası Çıkaran Merciye Gönderildi, 0, DİĞER, 9999"
                Case 400 : cAciklama = "Talimat Verildi(Alıcı Adını Değiştirme), 0, TALİMAT, 9999"
                Case 430 : cAciklama = "Talimat Etiketi Üretildi(II.Adrese Git), 0, TALİMAT, 9999"
                Case 433 : cAciklama = "Talimat Yerine Getirildi(3. Adrese Git), 0, TALİMAT, 9999"
                Case 405 : cAciklama = "Talimat Verildi(Teslim Gününü Belirleme), 0, TALİMAT, 9999"
                Case 104 : cAciklama = "Alıcının Talebi ile PTT İşyerine Teslim, -1, PTT ISYERI, 9999"
                Case 401 : cAciklama = "Talimat Verildi(Alıcı Adresini Değiştirme), 0, TALİMAT, 9999"
                Case 189 : cAciklama = "Kayıp Mazbata Teslim Belgesi Suret Gönderildi, -1, DİĞER, 9999"
                Case 431 : cAciklama = "Talimat Etiketi Üretildi(İade Adresine Git), 0, TALİMAT, 9999"
                Case 402 : cAciklama = "Talimat Verildi(Bekleme Suresini Değiştirme), 0, TALİMAT, 9999"
                Case 440 : cAciklama = "Talimat Verildi(Alma Haber Ek Hizmeti Ekleme), 0, TALİMAT, 9999"
                Case 417 : cAciklama = "Talimat Verildi(Gönderici Adresini Değiştirme), 0, TALİMAT, 9999"
                Case 432 : cAciklama = "Talimat Etiketi Üretildi(Banka Şubesine Teslim), 0, TALİMAT, 9999"
                Case 441 : cAciklama = "Talimat Verildi(Alma Haber Ek Hizmeti Kaldırma), 0, TALİMAT, 9999"
                Case 416 : cAciklama = "Talimat Verildi(Kontrollü Teslim Hizmeti Ekleme), 0, TALİMAT, 9999"
                Case 420 : cAciklama = "Talimat Etiketi Üretildi(Alıcı Adını Değiştirme), 0, TALİMAT, 9999"
                Case 409 : cAciklama = "Talimat Verildi(Gönderiyi 3 kez Dağıtıma Çıkarma), 0, TALİMAT, 9999"
                Case 425 : cAciklama = "Talimat Etiketi Üretildi(Teslim Gününü Belirleme), 0, TALİMAT, 9999"
                Case 404 : cAciklama = "Talimat Verildi(Gonderiyi Tekrar Dağıtıma Çıkarma), 0, TALİMAT, 9999"
                Case 406 : cAciklama = "Talimat Verildi(Teslim Edilememe Bilgisi Gönderme), 0, TALİMAT, 9999"
                Case 414 : cAciklama = "Talimat Verildi(Barkod Bilgisi Güncelle (Ödemeli)), 0, TALİMAT, 9999"
                Case 418 : cAciklama = "Talimat Verildi(Ücreti Alıcıdan Ek Hizmeti Ekleme), 0, TALİMAT, 9999"
                Case 407 : cAciklama = "Talimat Verildi(Teslim Edilemez ise İdareye Kalsın), 0, TALİMAT, 9999"
                Case 421 : cAciklama = "Talimat Etiketi Üretildi(Alıcı Adresini Değiştirme), 0, TALİMAT, 9999"
                Case 415 : cAciklama = "Talimat Verildi(Ücreti Alıcıdan Ek Hizmeti Kaldırma), 0, TALİMAT, 9999"
                Case 422 : cAciklama = "Talimat Etiketi Üretildi(Bekleme Suresini Değiştirme), 0, TALİMAT, 9999"
                Case 408 : cAciklama = "Talimat Verildi(Teslim Edilemez ise Hesabıma Satılsın), 0, TALİMAT, 9999"
                Case 460 : cAciklama = "Talimat Etiketi Üretildi(Alma Haber Ek Hizmeti Ekleme), 0, TALİMAT, 9999"
                Case 437 : cAciklama = "Talimat Etiketi Üretildi(Gönderici Adresini Değiştirme), 0, TALİMAT, 9999"
                Case 461 : cAciklama = "Talimat Etiketi Üretildi(Alma Haber Ek Hizmeti Kaldırma), 0, TALİMAT, 9999"
                Case 436 : cAciklama = "Talimat Yerine Getirildi(Kontrollü Teslim Hizmeti Ekleme), 0, TALİMAT, 9999"
                Case 429 : cAciklama = "Talimat Etiketi Üretildi(Gönderiyi 3 kez Dağıtıma Çıkarma), 0, TALİMAT, 9999"
                Case 424 : cAciklama = "Talimat Etiketi Üretildi(Gonderiyi Tekrar Dağıtıma Çıkarma), 0, TALİMAT, 9999"
                Case 426 : cAciklama = "Talimat Etiketi Üretildi(Teslim Edilememe Bilgisi Gönderme), 0, TALİMAT, 9999"
                Case 434 : cAciklama = "Talimat Yerine Getirildi(Barkod Bilgisi Güncelle (Ödemeli)), 0, TALİMAT, 9999"
                Case 438 : cAciklama = "Talimat Etiketi Üretildi(Ücreti Alıcıdan Ek Hizmeti Ekleme), 0, TALİMAT, 9999"
                Case 427 : cAciklama = "Talimat Etiketi Üretildi(Teslim Edilemez ise İdareye Kalsın), 0, TALİMAT, 9999"
                Case 435 : cAciklama = "Talimat Yerine Getirildi(Ücreti Alıcıdan Ek Hizmeti Kaldırma), 0, TALİMAT, 9999"
                Case 428 : cAciklama = "Talimat Etiketi Üretildi(Teslim Edilemez ise Hesabıma Satılsın), 0 TALİMAT, 9999"
                Case 310 : cAciklama = "Gönderinin İthaline Gümrükçe İzin Verilmedi, 0, DİĞER, 9999"
                Case 849 : cAciklama = "İADE-Elçilik Adresi-Tebligat Yapılamadı, -1, İADE, 4000"
                Case 442 : cAciklama = "Talimat Verildi(Alıcı Alıcı Telefonu Değiştirme (Kargomat)), 0, TALİMAT 9999"
                Case 462 : cAciklama = "Talimat Yerine Getirildi(Alıcı Telefonu Değiştirme (Kargomat)), 0, TALİMAT 9999"
                Case 854 : cAciklama = "Vergi Tebligatı-Muhatap Adına Almaya Yetkili Kişiler İmtina Etti İade, -1,İADE4000"
                Case 204 : cAciklama = "Islem Iptali Sebebiyle Kargomatikten Alindi, -1, DİĞER, 9999"
                Case 473 : cAciklama = "Pal Ebay Ücret Ödemesi Yapıldı, 0, GUNCELLEME, 9999"
                Case 855 : cAciklama = "İADE:Cezaevi Adresi-Tebligat Yapılamadı, -1, İADE, 4000"
                Case 856 : cAciklama = "İADE:Kargomatta Bekleme Süresi Bitti, -1, İADE, 4000"
                Case 474 : cAciklama = "Gümrük Ücret Kaydı Yapıldı, 0, GUNCELLEME, 9999"
                Case 260 : cAciklama = "Sözleşme Gereği İhbarlanmayan Gönderi, -1, İADE, 4000"
                Case 261 : cAciklama = "Kimlik Bilgisi Alınamadı, -1, İADE, 4000"
                Case 290 : cAciklama = "Kargomat İşlem İptal (Sistem Hatası/Zaman Aşımı), -1, DİĞER, 9999"
                Case 291 : cAciklama = "Kargomattan Alınmak Üzere İptal Onayı Verildi, -1, DİĞER, 9999"
                Case 832 : cAciklama = "İADE:Sevk Edildi, -1, İADE, 4000"
                Case 443 : cAciklama = "Talimat Verildi(Elektronik Alma Haber Ek Hizmeti Ekleme), 0, TALİMAT, 9999"
                Case 444 : cAciklama = "Talimat Verildi(Alıcı Telefonu Değiştirme/Ekleme), 0, TALİMAT, 9999"
                Case 463 : cAciklama = "Talimat Etiketi Üretildi(Elektronik Alma Haber Ek Hizmeti Ekleme), 0, TALİMAT, 9999"
                Case 464 : cAciklama = "Talimat Etiketi Üretildi(Alıcı Telefonu Değiştirme/Ekleme), 0, TALİMAT, 9999"
                Case 997 : cAciklama = "Gönderi Arşivlenebilir, 0, DİĞER, 9999"
                Case 998 : cAciklama = "Gönderi Arşivlenemez, 0, DİĞER, 9999"
                Case 309 : cAciklama = "Suret Baskısı Alındı, 0, DİĞER, 9999"
            End Select

        Catch ex As Exception
            ErrDisp("PTTSorguSonuc",,,, ex)
        End Try
    End Sub

End Module

