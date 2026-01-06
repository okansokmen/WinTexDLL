Option Explicit On

Imports System.Globalization
Imports WinTexMNG.com.yurticikargo.testwebservices

Module utilYurtici

    Public Function YurticiSendOrder1(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        Dim cUserName As String = ""
        Dim cPassword As String = ""
        Dim oYurtici As KOPSWebServices = Nothing
        Dim oCS As createShipment = Nothing
        Dim oSO As ShippingOrderVO = Nothing

        Dim oResponse As createShipmentResponse = Nothing
        Dim oSQL As New SQLServerClass
        Dim lKapidaKrediKarti As Boolean = False
        Dim lKapidaNakit As Boolean = False
        Dim lPesinUcretAlici As Boolean = False
        Dim lPesinUcretGonderici As Boolean = False
        Dim cTipi As String = "GOT"
        Dim cAraciKargo As String = ""
        Dim oSODVO As shippingOrderDetailVO = Nothing

        YurticiSendOrder1 = False
        cSonuc = ""
        cErrorMessage = ""

        Try
            If CheckFirmaCalisilmasin("YURTICI") Then Exit Function

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 odemetipi " +
                            " from sipperakende with (NOLOCK) " +
                            " where siparisno = '" + cSiparisNo + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                Select Case oSQL.SQLReadString("odemetipi")
                    Case "KAPIDA KREDI KARTI"
                        cTipi = "GOT"
                        lKapidaKrediKarti = True
                    Case "KAPIDA NAKIT"
                        cTipi = "GOT"
                        lKapidaNakit = True
                    Case "PESIN - UCRET ALICI"
                        cTipi = "AO"
                        lPesinUcretAlici = True
                    Case "PESIN - UCRET GONDERICI"
                        cTipi = "GO"
                        lPesinUcretGonderici = True
                End Select
            End If
            oSQL.oReader.Close()

            If Not GetServiceConnectionParameters(cSiparisNo, cUserName, cPassword,, "YURTICI", cTipi) Then
                cErrorMessage = "Bağlantı parametreleri bulunamadı"
                oSQL.CloseConn()
                Exit Function
            End If

            oSO = New ShippingOrderVO

            oSQL.cSQLQuery = "select top 1 tarih, adi, soyadi, ili, " +
                            " ilcesi, telefon, adres, kargofirmasi, odemetipi, " +
                            " teslimsekli, finaltutar " +
                            " from sipperakende with (NOLOCK) " +
                            " where siparisno = '" + cSiparisNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                oSO.receiverCustName = oSQL.SQLReadString("adi") + " " + oSQL.SQLReadString("soyadi")
                oSO.receiverAddress = oSQL.SQLReadString("adres")
                oSO.cityName = oSQL.SQLReadString("ili")
                oSO.townName = oSQL.SQLReadString("ilcesi")
                oSO.receiverPhone1 = oSQL.SQLReadString("telefon")

                ' $cargoKey= kargo anahtarı  = YK-Şube bu bilgiyi gönderi veya kargo üzerinde text/barkodlu olarak görmelidir.
                oSO.cargoKey = cSiparisNo
                ' $invoiceKey= kargo anahtarı = Her gönderi için tekil bilgi olmalıdır.
                oSO.invoiceKey = cSiparisNo

                If lKapidaKrediKarti Then
                    ' Kapıda kredi kartı ile ödeme
                    ' $waybillNo= Sevk İrsaliye No = (Ticari gönderilerde zorunludur)
                    ' $ttDocumentSaveType=Tahsilâtlı teslimat ürünü hizmet bedeli gönderi içerisinde mi? Ayrı mı faturalandırılacak? (0 – Aynı fatura,1 – farklı fatura)
                    ' ttDocumentId = Tahsilâtlı Teslimat Fatura No
                    ' dcCreditRule = Taksit Uygulama Kriteri 0: Müşteri Seçimi Zorunlu, 1: Tek Çekime izin ver
                    ' dcSelectedCredit = Taksit Sayısı

                    'oSO.waybillNo = cSiparisNo
                    oSO.ttInvoiceAmount = oSQL.SQLReadDouble("finaltutar")
                    oSO.ttInvoiceAmountSpecified = True
                    oSO.ttDocumentSaveType = 0
                    oSO.ttDocumentId = CDbl(cSiparisNo)
                    oSO.dcCreditRule = 1
                    oSO.dcSelectedCredit = 1
                    oSO.ttCollectionType = 1
                End If

                If lKapidaNakit Then
                    ' Kapıda nakit ödeme
                    ' $waybillNo= Sevk İrsaliye No = (Ticari gönderilerde zorunludur)
                    ' $ttDocumentSaveType=Tahsilâtlı teslimat ürünü hizmet bedeli gönderi içerisinde mi? Ayrı mı faturalandırılacak? (0 – Aynı fatura,1 – farklı fatura)
                    ' ttDocumentId = Tahsilâtlı Teslimat Fatura No
                    ' 
                    'oSO.waybillNo = cSiparisNo
                    oSO.ttInvoiceAmount = oSQL.SQLReadDouble("finaltutar")
                    oSO.ttInvoiceAmountSpecified = True
                    oSO.ttDocumentSaveType = 0
                    oSO.ttDocumentId = CDbl(cSiparisNo)
                    oSO.ttCollectionType = 0
                End If

                oSO.description = "AYAKKABI"
                oSO.taxNumber = "11111111111"
                oSO.taxOfficeName = "SAHIS"
                oSO.cargoCount = 1
                oSO.emailAddress = ""
                oSO.desi = 1
                oSO.desiSpecified = True
                oSO.kg = 1
                oSO.kgSpecified = True
            End If
            oSQL.oReader.Close()

            oCS = New createShipment
            oCS.wsUserName = cUserName
            oCS.wsPassword = cPassword
            oCS.userLanguage = "TR"
            oCS.ShippingOrderVO = {oSO}

            oYurtici = New KOPSWebServices
            oYurtici.Url = oConnection.cYurticiUrl
            'oYurtici.UseDefaultCredentials = False
            'oYurtici.Credentials = New System.Net.NetworkCredential(cUserName.Trim, cPassword.Trim)
            oYurtici.Timeout = 1000000

            oResponse = New createShipmentResponse
            oResponse = oYurtici.createShipment(oCS)

            If oResponse.ShippingOrderResultVO.outResult = "Başarılı" Then
                ' başarılı
                cSonuc = oResponse.ShippingOrderResultVO.jobId.ToString
                cErrorMessage = ""
                YurticiSendOrder1 = True

                oSQL.cSQLQuery = "update sipperakende " +
                                " set kargotakipno = '" + cSonuc + "', " +
                                " kargoyakayityollandi = 'E', " +
                                " kargoyakayityollanmatarihi = getdate() " +
                                " where siparisno = '" + cSiparisNo.Trim + "' "

                oSQL.SQLExecute()

                oSQL.cSQLQuery = "select top 1 aracikargo " +
                                " from firma with (NOLOCK) " +
                                " where firma = 'YURTICI' "

                cAraciKargo = oSQL.DBReadString()

                If cAraciKargo.Trim <> "" Then

                    oSQL.cSQLQuery = "update sipperakende " +
                                " set aracikargo = '" + cAraciKargo + "' " +
                                " where siparisno = '" + cSiparisNo.Trim + "' " +
                                " and (aracikargo is null or aracikargo = '') "

                    oSQL.SQLExecute()
                End If
            Else
                cSonuc = "0"
                cErrorMessage = oResponse.ShippingOrderResultVO.outResult
                For Each oSODVO In oResponse.ShippingOrderResultVO.shippingOrderDetailVO
                    cErrorMessage = cErrorMessage + ";" + oSODVO.errCode.ToString + "/" + oSODVO.errMessage
                Next
                YurticiSendOrder1 = False
            End If

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("YurticiSendOrder1",,,, ex)
        End Try
    End Function

    Public Function YurticiQueryOrder1(cSiparisNo As String, Optional ByRef cSonuc As String = "", Optional ByRef cErrorMessage As String = "") As Boolean
        ' Yurtiçikargo sistemine web servis entegrasyonu ile gönderilmiş olan  gönderi bilgisi hakkında bilgi almaya ve durumunu raporlamak için kullanılacak servistir.
        ' keyType = Keys parametresinde belirtilen anahtarların tipini belirler. 0 – Kargo Anahtarı 1 – Fatura Anahtarı
        ' addHistoricalData = Gönderiye ait taşıma hareketlerinin raporlanması için belirtilmelidir. True / False  Default : false
        ' onlyTracking = Sadece takip linkinin raporlanmasını sağlar. True / False Default : false

        Dim cUserName As String = ""
        Dim cPassword As String = ""
        Dim oYurtici As KOPSWebServices = Nothing
        'Dim oQS As queryShipment = Nothing
        'Dim oQSR As queryShipmentResponse = Nothing
        Dim oQSD As queryShipmentDetail = Nothing
        Dim oSDRVO2 As shipmentDeliveryResultVO = Nothing
        Dim oSDDVO3 As shipmentDeliveryDetailVO = Nothing
        Dim oSDIDVO3 As ShipmentDeliveryItemDetailVO = Nothing
        Dim oQSDR As queryShipmentDetailResponse = Nothing
        Dim oIDCV As InvDocCargoVO = Nothing

        'Dim oSDRVO As shippingDeliveryResultVO = Nothing
        'Dim oSDDVO As shippingDeliveryDetailVO = Nothing
        'Dim oSDIDVO As ShippingDeliveryItemDetailVO = Nothing
        Dim cSiparisNo2 As String = ""
        Dim oSQL As SQLServerClass

        Dim cKargoTakipNo As String = ""
        Dim cKargoTakipUrl As String = ""
        Dim cKargoStatu As String = ""
        Dim nKargoKgDesi As Double = 0
        Dim nKargoUrunBedeli As Double = 0
        Dim dKargoStatuTarihi As Date = #1/1/1950#
        Dim cKargoStatuTarihi As String = ""
        Dim dGonderiCikisTarihi As Date = #1/1/1950#
        Dim cGonderiCikisTarihi As String = ""
        Dim cDurum As String = ""
        Dim cCikisSube As String = ""
        Dim cTeslimSube As String = ""
        Dim cKargoMesaj As String = ""

        YurticiQueryOrder1 = False
        cSonuc = ""
        cErrorMessage = ""

        Try
            If CheckFirmaCalisilmasin("YURTICI") Then Exit Function

            cSiparisNo2 = GetSiparisNo2(cSiparisNo)

            If cSiparisNo2.Trim = "" Then
                cSiparisNo2 = cSiparisNo.Trim
            End If

            If Not GetServiceConnectionParameters(cSiparisNo, cUserName, cPassword,, "YURTICI") Then Exit Function

            oYurtici = New KOPSWebServices
            oYurtici.Url = oConnection.cYurticiUrl
            oYurtici.Timeout = 1000000

            oQSD = New queryShipmentDetail
            oQSD.wsUserName = cUserName
            oQSD.wsPassword = cPassword
            oQSD.wsLanguage = "TR"
            oQSD.keys = {cSiparisNo2}
            oQSD.keyType = 0
            oQSD.addHistoricalData = True
            oQSD.onlyTracking = False

            oQSDR = New queryShipmentDetailResponse
            oQSDR = oYurtici.queryShipmentDetail(oQSD)

            oSDRVO2 = New shipmentDeliveryResultVO
            oSDRVO2 = oQSDR.ShipmentDeliveryResultVO

            If oSDRVO2.outResult = "Başarılı" Then

                oSQL = New SQLServerClass

                oSQL.OpenConn()

                oSQL.cSQLQuery = "select top 1 kargostatu " +
                                " from sipperakende with (NOLOCK) " +
                                " where siparisno = '" + cSiparisNo.Trim + "' "

                oSQL.GetSQLReader()

                If oSQL.oReader.Read Then
                    cDurum = oSQL.SQLReadString("kargostatu")
                End If
                oSQL.oReader.Close()

                For Each oSDDVO3 In oSDRVO2.shipmentDeliveryDetailVO
                    ' operationStatus, operationCode, operationMesasage Değerleri
                    ' NOP 0	Kargo İşlem Görmemiş.
                    ' IND 1	Kargo Teslimattadır.
                    ' IND 1	İade isteği Alınmıştır (cancelShipment metodu ile gönderisi düzenlenmiş kayıt için iptal bilgisi gönderilmesi durumunda ilgili statü dönmektedir)
                    ' ISR 2	Kargo işlem görmüş, faturası henüz düzenlenmemiştir.
                    ' CNL 3	Kargo Çıkışı Engellendi.
                    ' ISC 4	Kargo daha önceden iptal edilmiştir.
                    ' DLV 5	Kargo teslim edilmiştir. 
                    ' BI  6	Fatura şube tarafından iptal edilmiştir.

                    cKargoStatu = oSDDVO3.operationStatus

                    If IsNothing(oSDDVO3.shipmentDeliveryItemDetailVO) Then
                        ' henüz işlem görmemiş
                    Else
                        oSDIDVO3 = New ShipmentDeliveryItemDetailVO
                        oSDIDVO3 = oSDDVO3.shipmentDeliveryItemDetailVO

                        For Each oIDCV In oSDIDVO3.invDocCargoVOArray
                            If WebReadString(oIDCV.unitId) = WebReadString(oSDIDVO3.departureTrCenterUnitId) Then
                                cDurum = "CIKIS SUBEDE"
                                dGonderiCikisTarihi = WebReadDate2(oIDCV.eventDate + " " + oIDCV.eventTime)
                                cGonderiCikisTarihi = WebReadString(oIDCV.eventDate) + " " + WebReadString(oIDCV.eventTime)
                            End If
                            If WebReadString(oIDCV.unitId) = WebReadString(oSDIDVO3.arrivalTrCenterUnitId) Then
                                cDurum = "VARIS SUBEDE"
                            End If
                            dKargoStatuTarihi = WebReadDate2(oIDCV.eventDate + " " + oIDCV.eventTime)
                            cKargoStatuTarihi = WebReadString(oIDCV.eventDate) + " " + WebReadString(oIDCV.eventTime)
                        Next

                        If Not IsNothing(oSDIDVO3.rejectStatus) Then
                            ' İade Durumu
                            ' rejectStatus değerleri iade işlemlerinde onay, iade ve teslim süreçleri için oluşan farklı durumların takibinin yapılmasını sağlamaktadır.
                            Select Case oSDIDVO3.rejectStatus
                                Case 0, 1, 2, 3, 9, 10
                                    ' kargo iade sürecindedir
                                    ' 0   “İade isteği yapıldı”; iade talebinin onaylanması
                                    ' 1   “Çıkış Şubesi Onayladı”, kargonun çıkışını yapan şubenin iade yönlendirimini kabul etmesi
                                    ' 2   “İade Bölge Onayladı”; kargonun iade talebini alakalı Bölge Müdürlüğü'nün onaylaması,
                                    ' 3   “Müşteri Onayladı”, iadenin müşteri tarafından da kabul edilmesi,
                                    ' 9   “İade Yapıldı”, iadenin yapılmış olması,
                                    ' 10  “İade Sonlandırıldı”, gönderimin iadesinin yapılması,
                                    cDurum = "IADE"
                                Case 4, 7, 8, 11
                                    ' iade süreci başlatılmış fakat iptal edilerek normal sürece geçilmiş
                                    ' 4   “İade Bekletiliyor”, iade talebi olan gönderimin şubede bekletilmesi,
                                    ' 7   “Teslim İptal”, iade talebinin iptal edilmesi,
                                    ' 8   “Borçlandırma İptal”, farklı bir şubeye yönlendiriminin iptal edilmesi,
                                    ' 11  “İade Onaylanmadı”, iade talebinin kabul edilmemesi,
                            End Select
                        End If

                        If Not IsNothing(oSDIDVO3.returnStatus) Then
                            Select Case oSDIDVO3.returnStatus
                                Case 0, 1, 2, 3
                                    ' Geri Dönüş Durumu
                                    ' 0   Teslim Edilmedi ve Geri Dönüş Faturası Kesilmedi.
                                    ' 1   Teslim Edildi ve Geri Dönüş Faturası Kesilmedi.
                                    ' 2   Teslim Edildi ve Geri Dönüş Faturası Kesildi.
                                    ' 3   Gönderici Müşteriye İade Edildi.
                                    cDurum = "IADE"
                            End Select
                        End If

                        If Not IsNothing(oSDIDVO3.cargoReasonId) Then
                            If oSDIDVO3.cargoReasonId.Trim = "AAS" Then
                                cDurum = "SORUNLU"
                            End If
                        End If

                        If Not IsNothing(oSDIDVO3.cargoEventId) Then
                            Select Case oSDIDVO3.cargoEventId.Trim
                                Case "TX"
                                    ' Devir (Alıcıya teslim edilemedi, varış şubesinde bekletiliyor)
                                    ' cargoEventId = TX, cargoReasontId = * (Ekteki listede TX'e karşılık gelen tanımlı reasonId’lerden biri olabilir)
                                    cDurum = "SORUNLU"
                                Case "YK"
                                    Select Case oSDIDVO3.cargoReasonId.Trim
                                        Case "GOK"
                                            ' Dağıtıma Çıkarıldı(Kuryeye zimmetlendi)
                                            cDurum = "DAGITIMDA"
                                        Case "KTN"
                                            ' Dağıtıma Çıkarıldı(KTN'ye bırakılmak üzere, Kuryeye zimmetlendi)
                                            cDurum = "DAGITIMDA"
                                    End Select
                                Case "OK"
                                    If oSDIDVO3.cargoReasonId.Trim = "OK" Then
                                        If Not (cDurum = "IADE") Then
                                            cDurum = "TESLIM EDILDI"
                                        End If
                                    End If
                            End Select
                        End If

                        If Not (cDurum = "IADE") Then
                            If Not IsNothing(oSDIDVO3.deliveryDate) Then
                                cDurum = "TESLIM EDILDI"
                            End If
                        End If

                        cKargoMesaj = WebReadString(oSDIDVO3.cargoEventExplanation)
                        cKargoTakipNo = WebReadString(oSDIDVO3.docId)
                        cKargoTakipUrl = WebReadString(oSDIDVO3.trackingUrl)
                        nKargoKgDesi = WebReadDouble(oSDIDVO3.totalDesi)
                        nKargoUrunBedeli = WebReadDouble(oSDIDVO3.totalPrice)
                        cCikisSube = WebReadString(oSDIDVO3.departureUnitName)
                        cTeslimSube = WebReadString(oSDIDVO3.deliveryUnitName)
                    End If

                    If Not (cDurum = "IADE" Or cDurum = "SORUNLU") Then
                        Select Case oSDDVO3.operationCode
                            Case 0 ' NOP
                                ' Kargo İşlem Görmemiş
                                If Not (cDurum = "TESLIM EDILDI" Or cDurum = "CIKIS SUBEDE" Or cDurum = "VARIS SUBEDE") Then
                                    cDurum = "YOLDA"
                                End If
                            Case 1 ' IND
                                ' Kargo Teslimattadır
                                If Not (cDurum = "TESLIM EDILDI") Then
                                    cDurum = "DAGITIMDA"
                                End If
                            Case 2 ' ISR
                                ' Kargo işlem görmüş, faturası henüz düzenlenmemiştir
                                If Not (cDurum = "TESLIM EDILDI" Or cDurum = "CIKIS SUBEDE" Or cDurum = "VARIS SUBEDE") Then
                                    cDurum = "YOLDA"
                                End If
                            Case 3 ' CNL
                                ' Kargo Çıkışı Engellendi
                                cDurum = "SORUNLU"
                            Case 4 ' ISC
                                ' Kargo daha önceden iptal edilmiştir
                                cDurum = "SORUNLU"
                            Case 5 ' DLV
                                ' Kargo Teslim edilmiştir
                                cDurum = "TESLIM EDILDI"
                        End Select
                    End If
                Next

                oSQL.cSQLQuery = "set dateformat dmy " +
                                " update sipperakende " +
                                " set kargotakipno = '" + SQLWriteString(cKargoTakipNo, 30) + "', " +
                                " kargotakipurl = '" + SQLWriteString(cKargoTakipUrl, 150) + "', " +
                                " kargosondurumkodu = '" + SQLWriteString(cKargoStatu, 3) + "', " +
                                " kargosondurumtarihi = getdate(), " +
                                " kargokgdesi = " + SQLWriteDecimal(nKargoKgDesi) + ", " +
                                " kargotahsilattutari = " + SQLWriteDecimal(nKargoUrunBedeli) + ", " +
                                " kargostatutarihi = '" + SQLWriteDateTime(dKargoStatuTarihi) + "', " +
                                " kargostatutarihiorjinal = '" + SQLWriteString(cKargoStatuTarihi, 30) + "', " +
                                " gondericikistarihi = '" + SQLWriteDateTime(dGonderiCikisTarihi) + "', " +
                                " gondericikistarihiorjinal = '" + SQLWriteString(cGonderiCikisTarihi, 30) + "', " +
                                " kargomesaj = '" + SQLWriteString(cKargoMesaj, 100) + "', " +
                                " kargostatu = '" + SQLWriteString(cDurum, 100) + "', " +
                                " cikis_sube = '" + SQLWriteString(cCikisSube, 30) + "', " +
                                " teslim_sube = '" + SQLWriteString(cTeslimSube, 30) + "' " +
                                " where siparisno = '" + cSiparisNo.Trim + "' "
                oSQL.SQLExecute()

                oSQL.CloseConn()

                cSonuc = cDurum
                cErrorMessage = ""
                YurticiQueryOrder1 = True
            Else
                cSonuc = ""
                cErrorMessage = oSDRVO2.outResult
                For Each oSDDVO3 In oSDRVO2.shipmentDeliveryDetailVO
                    cErrorMessage = cErrorMessage + ";" + oSDDVO3.errCode.ToString + "/" + oSDDVO3.errMessage
                Next

                YurticiQueryOrder1 = False
            End If

        Catch ex As Exception
            ErrDisp("YurticiQueryOrder1",,,, ex)
        End Try
    End Function

    Public Function YurticiCancelOrder1(ByVal cSiparisNo As String, Optional ByRef cErrorMessage As String = "") As Boolean

        Dim cUserName As String = ""
        Dim cPassword As String = ""
        Dim oYurtici As KOPSWebServices = Nothing
        Dim oCO As cancelShipment
        Dim oCSR As cancelShipmentResponse
        Dim oSCDVO As shippingCancelDetailVO = Nothing
        Dim cSiparisNo2 As String = ""
        Dim oSQL As New SQLServerClass
        Dim cKargoTakipNo As String = ""

        YurticiCancelOrder1 = False
        cErrorMessage = ""

        Try
            If CheckFirmaCalisilmasin("YURTICI") Then Exit Function

            cSiparisNo2 = GetSiparisNo2(cSiparisNo)

            If cSiparisNo2.Trim = "" Then
                cSiparisNo2 = cSiparisNo.Trim
            End If

            If Not GetServiceConnectionParameters(cSiparisNo, cUserName, cPassword,, "YURTICI") Then Exit Function

            oCO = New cancelShipment
            oCO.wsUserName = cUserName
            oCO.wsPassword = cPassword
            oCO.userLanguage = "TR"
            oCO.cargoKeys = {cSiparisNo2}

            oYurtici = New KOPSWebServices
            oYurtici.Url = oConnection.cYurticiUrl
            oYurtici.Timeout = 1000000

            oCSR = New cancelShipmentResponse
            oCSR = oYurtici.cancelShipment(oCO)

            If oCSR.ShippingOrderResultVO.outResult = "Başarılı" Then
                cErrorMessage = "Başarılı"
                For Each oSCDVO In oCSR.ShippingOrderResultVO.shippingCancelDetailVO
                    cKargoTakipNo = oSCDVO.docId.ToString
                    cErrorMessage = cErrorMessage + ";" + oSCDVO.operationCode.ToString + "/" + oSCDVO.operationMessage
                Next
                YurticiCancelOrder1 = True

                oSQL.OpenConn()

                oSQL.cSQLQuery = "update sipperakende " +
                                " set iptal = 'E', "

                If cKargoTakipNo.Trim <> "" Then
                    oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " kargotakipno = '" + SQLWriteString(cKargoTakipNo, 30) + "', "
                End If

                oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " iptaltarih = getdate() " +
                                " where siparisno = '" + cSiparisNo.Trim + "' "

                oSQL.SQLExecute()

                oSQL.CloseConn()

                CreateLog("WinTexYurticiLog", "Sipariş Iptal Edildi / YurticiCancelOrder1 : " + cSiparisNo)
            Else
                cErrorMessage = oCSR.ShippingOrderResultVO.outResult
                For Each oSCDVO In oCSR.ShippingOrderResultVO.shippingCancelDetailVO
                    cErrorMessage = cErrorMessage + ";" + oSCDVO.errCode.ToString + "/" + oSCDVO.errMessage
                Next
                YurticiCancelOrder1 = False
            End If

        Catch ex As Exception
            ErrDisp("YurticiCancelOrder1",,,, ex)
        End Try
    End Function

End Module
