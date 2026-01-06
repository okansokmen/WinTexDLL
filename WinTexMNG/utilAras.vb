Option Explicit On

Imports System.Globalization
'Imports WinTexMNG.tr.com.araskargo.customerservices

'Dim cUserName As String = "mares"
'Dim cPassword As String = "my343827"

'Dim cUserName As String = "neodyum"
'Dim cPassword As String = "nd2580"

'Dim cUserName As String = "favorite"
'Dim cPassword As String = "favorite06"

'Dim cUserName As String = "muhasebebenimpapucum@gmail.com"
'Dim cPassword As String = "Chn1901966"

'Dim cUserName As String = "bulutkrg2018@gmail.com"
'Dim cPassword As String = "Be20061201"

'Dim cUserName As String = "muhasebebenimpapucum@gmail.com"
'Dim cPassword As String = "Chn1901966"

'Dim cUserName As String = "melihyuksel01@hotmail.com"
'Dim cPassword As String = "my343827"

'Dim cUserName As String = "mares"
'Dim cPassword As String = "x3b96n8iok"


' Saha Destek Uzmanı : hasanbatak@araskargo.com.tr
' Satış Yöneticisi   : ugurliman@araskargo.com.tr

' Aras Kargo Kurumsal Giriş
' https://esasweb.araskargo.com.tr/ 
' mail-geçit : melihyuksel01@hotmail.com
' pass-geçit : Gecit202024-
' mail-1 : bulutkrg2018@gmail.com
' pass-1 : Be20061201
' mail-2 : muhasebebenimpapucum@gmail.com
' pass-2 : Chn1901966

' sorgulama için
' Production Ortamı Linki (GetQueryDS)
' https://customerservices.araskargo.com.tr/ArasCargoCustomerIntegrationService/ArasCargoIntegrationService.svc
' geçit şifreleri
' müşteri kodu : 2329754550911
' kullanıcı adı : mares
' şifre : my343827
' favorinin şifreleri
' müşteri kodu:  2211254551651
' kullanıcı adı: favorite
' şifre:         favorite06
' eMail:         info@favorite.com

' Test Ortamı Linki 
' http://customerservicestest.araskargo.com.tr/ArasCargoIntegrationService.svc
' Test Ortamı Bilgileri
' Username: neodyum
' Password: nd2580
' CustomerCode = 1932448851342

' gönderi kaydı yapmak için
' https://appls-srv.araskargo.com.tr/arascargoservice/arascargoservice.asmx

' 1)Aras Kargo'nun Üretmiş Olduğu “Kargo Takip Numarası” İle Takip Linki
' http://kargotakip.araskargo.com.tr/mainpage.aspx?code=%203513773163316
' Code: Aras Kargo 'nun gönderi kaydı oluştururken üretmiş olduğu 13 haneli kargo takip kodudur. 

' 2) “Sipariş Numarası” İle Gönderi Takip Linki
' http://kargotakip.araskargo.com.tr/mainpage.aspx?accountid=CBAD417894B73048BBC058C09771CDB8&sifre = nd1234&alici_kod=6140307
' https://kargotakip.araskargo.com.tr/mainpage.aspx?accountid=9B5AE4062CB6164D9D5A04D5687601BB&alici_kod=XXXX
' https://kargotakip.araskargo.com.tr/mainpage.aspx?accountid=34FDFAB04B158B4592FBFCC6505B9506&alici_kod=111111
' Alıcı Kod: Siparişleriniz için üretmiş olduğunuz unique ( benzersiz) kodu temsil eder.
' müşteri kodu:  2211254551651
' kullanıcı:     favirite344
' şifre:         favirite344785*-
' eMail:         yakamozkargo@gmail.com

' 3) “Kargo Barkod Kodu” İle Gönderi Takip Linki
' http://kargotakip.araskargo.com.tr/yurticigonbil.aspx?Cargo_Code=0805513773163332313
' Kargo Code: Aras Kargo 'nun gönderi kaydı oluştururken üretmiş olduğu 20 haneli barkod koddur

Module utilAras

    Public Function ArasSendOrder1(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        cSonuc = ""
        ArasSendOrder1 = False

        Try
            Dim cSiparisNo2 As String = ""
            Dim cUserName As String = oConnection.cArasApiUserName2
            Dim cPassword As String = oConnection.cArasApiPassword2
            Dim cMusteriKodu As String = oConnection.cArasMusteriKodu

            If CheckFirmaCalisilmasin("ARAS") Then
                cErrorMessage = "Aras ile çalışılmasın olarak işaretlenmiş"
                Exit Function
            End If

            If Not GetServiceConnectionParameters(cSiparisNo, cUserName, cPassword, cMusteriKodu, "ARAS",, cSiparisNo2, True) Then
                cErrorMessage = cUserName + " / " + cPassword + " / " + cMusteriKodu + " ile Aras servisine bağlantı yapılamıyor "
                Exit Function
            End If

            Dim oSQL As New SQLServerClass
            Dim cAraciKargo As String = ""

            Dim oService As New WinTexMNG.tr.com.araskargo.customerws.Service
            Dim aOrder(0) As WinTexMNG.tr.com.araskargo.customerws.Order
            Dim oOrder As New WinTexMNG.tr.com.araskargo.customerws.Order
            Dim aOrderResultInfo() As WinTexMNG.tr.com.araskargo.customerws.OrderResultInfo
            Dim aPieceDetails() As WinTexMNG.tr.com.araskargo.customerws.PieceDetail
            Dim oPieceDetail As New WinTexMNG.tr.com.araskargo.customerws.PieceDetail
            Dim cKargocuTahsilatYapmaz As String = ""
            Dim cOdemeTipi As String = ""

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 tarih, adi, soyadi, ili, " +
                             " ilcesi, telefon, adres, kargofirmasi, odemetipi, " +
                             " teslimsekli, finaltutar, " +
                             " kargocutahsilatyapmaz = (select top 1 kargocutahsilatyapmaz " +
                                                      " from odemetipi with (NOLOCK)  " +
                                                      " where kod = sipperakende.odemetipi) " +
                             " from sipperakende with (NOLOCK) " +
                             " where siparisno = '" + cSiparisNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then

                oOrder.UserName = cUserName
                oOrder.Password = cPassword

                oOrder.InvoiceNumber = cSiparisNo.Trim
                oOrder.TradingWaybillNumber = cSiparisNo.Trim
                oOrder.IntegrationCode = cSiparisNo.Trim

                oOrder.ReceiverName = oSQL.SQLReadString("adi") + " " + oSQL.SQLReadString("soyadi")
                oOrder.ReceiverAddress = oSQL.SQLReadString("adres")
                oOrder.ReceiverPhone1 = oSQL.SQLReadString("telefon")
                oOrder.ReceiverTownName = oSQL.SQLReadString("ilcesi")
                oOrder.ReceiverCityName = oSQL.SQLReadString("ili")

                oOrder.VolumetricWeight = 3
                oOrder.Weight = 1
                oOrder.PieceCount = 1
                oOrder.Description = "AYAKKABI"

                oPieceDetail.Description = "AYAKKABI"
                oPieceDetail.VolumetricWeight = 3
                oPieceDetail.Weight = 1
                oPieceDetail.BarcodeNumber = cSiparisNo.Trim

                ReDim aPieceDetails(0)
                aPieceDetails(0) = oPieceDetail

                oOrder.PieceDetails = aPieceDetails

                ' IsWorldWide -> Yurtiçi için 0 Yurtdışı için 1
                oOrder.IsWorldWide = "0"

                cKargocuTahsilatYapmaz = oSQL.SQLReadString("kargocutahsilatyapmaz")
                cOdemeTipi = oSQL.SQLReadString("odemetipi")

                ' IsCod -> Tahsilatlı Kargo gönderisi (0=Hayır, 1=Evet)
                If cKargocuTahsilatYapmaz = "E" Or cOdemeTipi = "PESIN - UCRET ALICI" Or cOdemeTipi = "PESIN - UCRET GONDERICI" Then
                    oOrder.IsCod = "0"
                    oOrder.CodAmount = "0"
                Else
                    oOrder.IsCod = "1"
                    oOrder.CodAmount = Replace(CStr(oSQL.SQLReadDouble("finaltutar")), ".", ",")
                End If

                ' PayorTypeCode     -> Gönderinin ödemesini kimin yapacağını belirler. (1=Gönderici Öder , 2=Alıcı Öder)
                ' CodCollectionType -> teslimat ürünü ödeme tipi (0=Nakit , 1=Kredi Kartı)
                Select Case cOdemeTipi
                    Case "PESIN - UCRET ALICI"
                        oOrder.PayorTypeCode = 2
                    Case "SIRKETE PESIN-UCRET ALICI"
                        oOrder.PayorTypeCode = 2
                    Case "PESIN - UCRET GONDERICI"
                        oOrder.PayorTypeCode = 1
                    Case "SIRKETE PESIN-UCRET GONDERICI"
                        oOrder.PayorTypeCode = 1
                    Case "KAPIDA KREDI KARTI"
                        oOrder.PayorTypeCode = 1
                        oOrder.CodCollectionType = "1" ' KREDI_KARTI
                    Case "KAPIDA NAKIT"
                        oOrder.PayorTypeCode = 1
                        oOrder.CodCollectionType = "0" ' NAKIT
                End Select

                aOrder(0) = oOrder
                aOrderResultInfo = oService.SetOrder(aOrder, cUserName, cPassword)

                If aOrderResultInfo(0).ResultCode = 0 Then
                    ' başarılı
                    cSonuc = aOrderResultInfo(0).ResultMessage

                    oSQL.cSQLQuery = "update sipperakende " +
                                    " set kargotakipno = '" + SQLWriteString(aOrderResultInfo(0).InvoiceKey, 30) + "', " +
                                    " kargoyakayityollandi = 'E', " +
                                    " kargoyakayityollanmatarihi = getdate() " +
                                    " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') "

                    oSQL.SQLExecute()

                    oSQL.cSQLQuery = "select top 1 aracikargo " +
                                    " from firma with (NOLOCK) " +
                                    " where firma = 'ARAS' "

                    cAraciKargo = oSQL.DBReadString()

                    ' update wintex 
                    If cAraciKargo.Trim <> "" Then

                        oSQL.cSQLQuery = "update sipperakende " +
                                    " set aracikargo = '" + cAraciKargo + "' " +
                                    " where (siparisno = '" + cSiparisNo.Trim + "' or siparisno2 = '" + cSiparisNo.Trim + "') " +
                                    " and (aracikargo is null or aracikargo = '') "

                        oSQL.SQLExecute()
                    End If

                    ArasSendOrder1 = True
                Else
                    cErrorMessage = aOrderResultInfo(0).ResultMessage
                End If
            End If
            oSQL.oReader.Close()

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("ArasQueryOrder1",,,, ex)
        End Try
    End Function

    Public Function ArasQueryOrder1(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        ArasQueryOrder1 = False
        cSonuc = ""
        cErrorMessage = ""

        Try
            Dim cSiparisNo2 As String = ""
            Dim cUserName As String = oConnection.cArasApiUserName     ' mares / neodyum / favorite
            Dim cPassword As String = oConnection.cArasApiPassword     ' my343827 / nd2580 / favorite06 
            Dim cCustomerCode As String = oConnection.cArasMusteriKodu ' 2329754550911 / 1932448851342 / 2211254551651

            If CheckFirmaCalisilmasin("ARAS") Then
                cErrorMessage = "Aras ile çalışılmasın olarak işaretlenmiş"
                Exit Function
            End If

            If Not GetServiceConnectionParameters(cSiparisNo, cUserName, cPassword, cCustomerCode, "ARAS",, cSiparisNo2) Then
                cErrorMessage = cUserName + " / " + cPassword + " / " + cCustomerCode + " ile Aras servisine bağlantı yapılamıyor "
                Exit Function
            End If

            Dim cMusteriOzelKodu As String = ""
            Dim cIrsaliyeNumara As String = ""
            Dim cGonderici As String = ""
            Dim cAlici As String = ""
            Dim cKargoTakipNo As String = ""
            Dim cCikisSube As String = ""
            Dim cVarisSube As String = ""
            Dim cCikisTarih As String = "01.01.1950"
            Dim cAdet As String = ""
            Dim cDesi As String = ""
            Dim cOdemeTipi As String = ""
            Dim cTutar As String = ""
            Dim cReferans As String = ""
            Dim cTeslimAlan As String = ""
            Dim cTeslimTarihi As String = "01.01.1950"
            Dim cTeslimSaati As String = ""
            Dim cVarisKodu As String = ""
            Dim cTipKodu As String = ""
            Dim cDurumKodu As String = ""
            Dim cDurumu As String = ""
            Dim cIadeSebebi As String = ""
            Dim cKargoLinkNo As String = ""
            Dim cWorldwide As String = ""
            Dim cKargoKodu As String = ""
            Dim cDurumEn As String = ""
            Dim cDevirKodu As String = ""
            Dim cIslemTarihi As String = "01.01.1950"
            Dim cHacimselAgirlik As String = ""
            Dim cAgirlik As String = ""

            Dim cSonIslemTarihi As String = "01.01.1950"
            Dim cSonBirim As String = ""
            Dim cSonIslem As String = ""
            Dim cSonAciklama As String = ""

            Dim cKargoTakipUrl As String = ""
            Dim cDurum As String = ""
            Dim dKargoStatuTarihi As DateTime = #1/1/1950#
            Dim dCikisTarih As DateTime = #1/1/1950#

            Dim XmlResult As String = ""
            Dim JsonResult As String = ""
            Dim oDataSetResult As DataSet = Nothing

            Dim XmlResult2 As String = ""
            Dim JsonResult2 As String = ""
            Dim oDataSetResult2 As DataSet = Nothing

            Dim oRow As DataRow = Nothing
            Dim nCol As Integer = 0
            Dim oService As New WinTexMNG.tr.com.araskargo.customerservices.ArasCargoIntegrationService
            Dim oService2 As New WinTexMNG.tr.com.araskargo.customerservices.ArasCargoIntegrationService
            Dim oSQL As SQLServerClass

            Dim cLoginInfo As String = "<LoginInfo>" +
                                    "<UserName>" + cUserName.Trim + "</UserName>" +
                                    "<Password>" + cPassword.Trim + "</Password>" +
                                    "<CustomerCode>" + cCustomerCode.Trim + "</CustomerCode>" +
                                    "</LoginInfo>"

            Dim cQueryInfo As String = "<QueryInfo>" +
                                    "<QueryType>1</QueryType>" +
                                    "<IntegrationCode>" + cSiparisNo.Trim + "</IntegrationCode>" +
                                    "</QueryInfo>"

            Dim cQueryInfo2 As String = "<QueryInfo>" +
                                    "<QueryType>9</QueryType>" +
                                    "<IntegrationCode>" + cSiparisNo.Trim + "</IntegrationCode>" +
                                    "</QueryInfo>"

            oSQL = New SQLServerClass
            oSQL.OpenConn()

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12

            ' XML ve json kullanılmıyorlar
            oDataSetResult2 = oService2.GetQueryDS(cLoginInfo, cQueryInfo2)
            XmlResult2 = oService2.GetQueryXML(cLoginInfo, cQueryInfo2)
            JsonResult2 = oService2.GetQueryJSON(cLoginInfo, cQueryInfo2)

            For Each oRow In oDataSetResult2.Tables.Item(0).Rows
                cSonIslemTarihi = oRow.Item(0).ToString.Trim
                cSonBirim = oRow.Item(1).ToString.Trim
                cSonIslem = oRow.Item(2).ToString.Trim
                cSonAciklama = oRow.Item(3).ToString.Trim

                If cSonuc.Trim = "" Then
                    cSonuc = cSonIslemTarihi + " " + cSonBirim + " " + cSonIslem + " " + cSonAciklama
                Else
                    cSonuc = cSonuc + vbCrLf + cSonIslemTarihi + " " + cSonBirim + " " + cSonIslem + " " + cSonAciklama
                End If
            Next

            If cSonIslemTarihi.Trim <> "" Then
                dKargoStatuTarihi = CDate(cSonIslemTarihi)
            End If

            ' XML ve json kullanılmıyorlar
            oDataSetResult = oService.GetQueryDS(cLoginInfo, cQueryInfo)
            XmlResult = oService.GetQueryXML(cLoginInfo, cQueryInfo)
            JsonResult = oService.GetQueryJSON(cLoginInfo, cQueryInfo)

            For Each oRow In oDataSetResult.Tables.Item(0).Rows
                For nCol = 0 To oRow.ItemArray.Length - 1
                    Select Case nCol
                        Case 0 ' MUSTERI_OZEL_KODU
                            cMusteriOzelKodu = oRow.Item(nCol).ToString.Trim
                        Case 1 ' IRSALIYE_NUMARA
                            cIrsaliyeNumara = oRow.Item(nCol).ToString.Trim
                        Case 2 ' GONDERICI
                            cGonderici = oRow.Item(nCol).ToString.Trim
                        Case 3 ' ALICI
                            cAlici = oRow.Item(nCol).ToString.Trim
                        Case 4 ' KARGO_TAKIP_NO
                            cKargoTakipNo = oRow.Item(nCol).ToString.Trim
                        Case 5 ' CIKIS_SUBE
                            cCikisSube = oRow.Item(nCol).ToString.Trim
                        Case 6 ' VARIS_SUBE
                            cVarisSube = oRow.Item(nCol).ToString.Trim
                        Case 7 ' CIKIS_TARIH
                            cCikisTarih = oRow.Item(nCol).ToString.Trim
                            dCikisTarih = CDate(cCikisTarih)
                        Case 8 ' ADET
                            cAdet = oRow.Item(nCol).ToString.Trim
                        Case 9 ' DESI
                            cDesi = oRow.Item(nCol).ToString.Trim
                        Case 10 ' ODEME_TIPI
                            ' ÜG = Ücreti göndericiden
                            ' ÜA = Ücreti alıcıdan
                            cOdemeTipi = oRow.Item(nCol).ToString.Trim
                        Case 11 ' KDV hariç tutar
                            cTutar = oRow.Item(nCol).ToString.Trim
                        Case 12 ' REFERANS
                            cReferans = oRow.Item(nCol).ToString.Trim
                        Case 13
                            cTeslimAlan = oRow.Item(nCol).ToString.Trim
                        Case 14
                            cTeslimTarihi = oRow.Item(nCol).ToString.Trim
                        Case 15
                            cTeslimSaati = oRow.Item(nCol).ToString.Trim
                        Case 16 ' VARIS_KODU
                            cVarisKodu = oRow.Item(nCol).ToString.Trim
                        Case 17 ' TIP_KODU
                            ' 1 Normal
                            ' 2 Yönlendirildi
                            ' 3 İade edildi
                            cTipKodu = oRow.Item(nCol).ToString.Trim
                        Case 18 ' DURUM_KODU
                            ' 1 Çıkış Şubesinde
                            ' 2 Yolda
                            ' 3 Teslimat Şubesinde
                            ' 4 Teslimatta
                            ' 5 Parçalı Teslimat
                            ' 6 Teslim Edildi
                            ' 7 Yönlendirildi
                            cDurumKodu = oRow.Item(nCol).ToString.Trim
                        Case 19 ' Durum açıklaması
                            cDurumu = oRow.Item(nCol).ToString.Trim
                        Case 20
                            cIadeSebebi = oRow.Item(nCol).ToString.Trim
                        Case 21 ' KARGO_LINK_NO
                            cKargoLinkNo = oRow.Item(nCol).ToString.Trim
                        Case 22 ' WORLDWIDE
                            ' 1 yurtdışı
                            ' 0 yurtiçi
                            cWorldwide = oRow.Item(nCol).ToString.Trim
                        Case 23 ' KARGO_KODU
                            cKargoKodu = oRow.Item(nCol).ToString.Trim
                        Case 24 ' Kargonun İngilizce son durum açıklamasıdır
                            cDurumEn = oRow.Item(nCol).ToString.Trim
                        Case 25
                            cDevirKodu = oRow.Item(nCol).ToString.Trim
                        Case 26 ' ISLEM_TARIHI
                            cIslemTarihi = oRow.Item(nCol).ToString.Trim
                        Case 27 ' HACIMSEL_AGIRLIK
                            cHacimselAgirlik = oRow.Item(nCol).ToString.Trim
                        Case 28 ' AGIRLIK
                            cAgirlik = oRow.Item(nCol).ToString.Trim
                    End Select
                Next

                Select Case cDurumKodu.Trim
                    Case "1"
                        cDurum = "CIKIS SUBEDE"
                    Case "2"
                        cDurum = "YOLDA"
                    Case "3"
                        cDurum = "VARIS SUBEDE"
                    Case "4"
                        cDurum = "DAGITIMDA"
                    Case "5", "6"
                        cDurum = "TESLIM EDILDI"
                    Case Else
                        cDurum = "SIPARIS HAZIRLANIYOR"
                End Select

                Select Case cTipKodu
                    Case "3"
                        cDurum = "IADE"
                End Select

                cKargoTakipUrl = "http://kargotakip.araskargo.com.tr/mainpage.aspx?code=" + cKargoLinkNo.Trim

                oSQL.cSQLQuery = "set dateformat dmy " +
                                " update sipperakende " +
                                " set kargotakipno = '" + SQLWriteString(cKargoTakipNo, 30) + "', " +
                                " kargotakipurl = '" + SQLWriteString(cKargoTakipUrl, 150) + "', " +
                                " kargosondurumkodu = '" + SQLWriteString(cDurumKodu, 3) + "', " +
                                " kargosondurumtarihi = getdate(), " +
                                " kargokgdesi = " + cDesi + ", " +
                                " kargotahsilattutari = " + cTutar + ", " +
                                " kargostatutarihi = '" + SQLWriteDateTime(dKargoStatuTarihi) + "', " +
                                " kargostatutarihiorjinal = '" + SQLWriteString(cSonIslemTarihi, 30) + "', " +
                                " gondericikistarihi = '" + SQLWriteDateTime(dCikisTarih) + "', " +
                                " gondericikistarihiorjinal = '" + SQLWriteString(cCikisTarih, 30) + "', " +
                                " kargomesaj = '" + SQLWriteString(cSonAciklama, 100) + "', " +
                                " kargostatu = '" + SQLWriteString(cDurum, 100) + "', " +
                                " cikis_sube = '" + SQLWriteString(cCikisSube, 30) + "', " +
                                " teslim_sube = '" + SQLWriteString(cVarisSube, 30) + "' " +
                                " where siparisno = '" + cSiparisNo.Trim + "' "
                oSQL.SQLExecute()

                Select Case cDurumKodu.Trim
                    Case "5", "6"
                        ' teslim edildi
                        oSQL.cSQLQuery = "update sipperakende " +
                                        " set kapandi = 'E', " +
                                        " kapanmatarihi = '" + SQLWriteDateTime(dKargoStatuTarihi) + "', " +
                                        " iade = null, " +
                                        " iadetarihi = null " +
                                        " where siparisno = '" + cSiparisNo.Trim + "' "
                        oSQL.SQLExecute()
                End Select

                Select Case cTipKodu
                    Case "3"
                        ' iade
                        oSQL.cSQLQuery = "update sipperakende " +
                                        " set iade = 'E', " +
                                        " iadetarihi = '" + SQLWriteDateTime(dKargoStatuTarihi) + "', " +
                                        " kapandi = null, " +
                                        " kapanmatarihi = null " +
                                        " where siparisno = '" + cSiparisNo.Trim + "' "
                        oSQL.SQLExecute()
                End Select
            Next

            oSQL.CloseConn()

            oService = Nothing
            oService2 = Nothing

            ArasQueryOrder1 = True

        Catch ex As Exception
            cErrorMessage = ex.Message.Trim
            ErrDisp("ArasQueryOrder1",,,, ex)
        End Try
    End Function

End Module
