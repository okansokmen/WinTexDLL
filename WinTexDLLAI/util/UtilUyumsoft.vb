Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Module UtilUyumsoft

    Const cModuleName As String = "UtilUyumsoft"

    Public Structure oUyumServices
        Dim cURLGeneralB2BService As String
        Dim cURLUyumErogluProduction As String
        Dim cURLGeneralSenfoniService As String
        Dim cURLUyumSaveWebService As String
        Dim cURLUyumGetWebService As String
        Dim cURLUyumWebService As String
        Dim cUserName As String
        Dim cPassword As String
        Dim cCoCode As String
        Dim cBranchCode As String
        Dim cWhouseCode As String
        Dim cWorkCenterCode As String
        Dim cWorkOrderType As String
        Dim cIhracatFirmaId As String
        Dim lUyumDTFirmaAktif As Boolean
    End Structure

    Public oUyum As oUyumServices

    Public oConnectionUyum As oSQLConn

    Public Sub GetConnUyum()

        Dim ConnYage As SqlClient.SqlConnection

        Try
            ConnYage = OpenConn()
            oConnectionUyum.cServer = GetSysParConnected("UyumFServer", ConnYage, "192.168.1.3")
            oConnectionUyum.cDatabase = GetSysParConnected("UyumFDatabase", ConnYage, "uyumsoft")
            oConnectionUyum.cUser = GetSysParConnected("UyumFUsername", ConnYage, "uyum")
            oConnectionUyum.cPassword = GetSysParConnected("UyumFPassword", ConnYage, "12345")
            CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("GetConnUyum", "UtilUyumsoft",,, ex)
        End Try
    End Sub

    Public Function UyumKur(Optional cFilter As String = "") As Boolean

        Dim cSQL As String = ""
        Dim oSQLUyum As PostgreClass

        Dim cDoviz As String = ""
        Dim dTarih As Date = #1/1/1950#
        Dim nDovizAlis As Double = 0
        Dim nDovizSatis As Double = 0
        Dim nEfektifAlis As Double = 0
        Dim nEfektifSatis As Double = 0

        UyumKur = True

        Try
            GetConnUyum()
            oSQLUyum = New PostgreClass(oConnectionUyum.cServer, oConnectionUyum.cDatabase, oConnectionUyum.cUser, oConnectionUyum.cPassword)

            oSQLUyum.OpenConn()

            cSQL = "select select cur_from_id, doc_date, cur_rate_tra, cur_rate_type_id " +
                    " from uyumsoft.GNLD_DAILY_CUR_RATE " +
                    " where cur_from_id in (116,115,117) " +
                    " and cur_rate_type_id in (234,235) " +
                    " and a.rates1 is not null " +
                    " and a.rates1 <> 0 " +
                    cFilter +
                    " order by doc_date desc , cur_from_id "

            oSQLUyum.GetSQLReader(cSQL)

            Do While oSQLUyum.oReader.Read

                dTarih = oSQLUyum.SQLReadDate("doc_date")

                Select Case oSQLUyum.SQLReadDouble("cur_from_id")
                    Case 115
                        cDoviz = "EUR"
                    Case 116
                        cDoviz = "USD"
                    Case 117
                        cDoviz = "GBP"
                End Select

                Select Case oSQLUyum.SQLReadDouble("cur_from_id")
                    Case 235
                        nDovizAlis = oSQLUyum.SQLReadDouble("cur_rate_tra")
                        If Not DovizKuruYaz(dTarih, cDoviz.Trim, "Kur", nDovizAlis, "UYUM") Then
                            UyumKur = False
                        End If
                    Case 234
                        nDovizSatis = oSQLUyum.SQLReadDouble("cur_rate_tra")
                        If Not DovizKuruYaz(dTarih, cDoviz.Trim, "Satis Kuru", nDovizSatis, "UYUM") Then
                            UyumKur = False
                        End If
                End Select
            Loop

            oSQLUyum.oReader.Close()
            oSQLUyum.oReader = Nothing

            oSQLUyum.CloseConn()
            oSQLUyum = Nothing

        Catch ex As Exception
            ErrDisp("UyumKur", "UtilLogo",,, ex)
        End Try
    End Function

    ' oluşturma sırası : bom -> işemri -> üretim girişi
    Public Sub initUyumServices(Optional nCase As Integer = 3, Optional lUserWhoHasDeleteRight As Boolean = False)
        ' nCase = 1 fiili üretim firması
        ' nCase = 2 resmi ihracat firması
        ' nCase = 3 resmi üretim firması
        Dim cIPAddress As String = "192.168.1.6"

        Try
            Dim Connyage As SqlConnection

            Connyage = OpenConn()

            Select Case nCase
                Case 1
                    ' F firma
                    ' stokfislines.uyumid
                    ' stokfislines.uyumsipid
                    ' 192.168.1.6
                    cIPAddress = "192.168.1.6"

                    oUyum.cURLGeneralB2BService = GetSysParConnected("URLGeneralB2BService", Connyage, "http://" + cIPAddress + "/WebService/B2B/GeneralB2BService.asmx")
                    oUyum.cURLUyumErogluProduction = GetSysParConnected("URLErogluProduction", Connyage, "http://" + cIPAddress + "/webservice/eroglu/ErogluProduction.asmx")
                    oUyum.cURLGeneralSenfoniService = GetSysParConnected("URLGeneralSenfoniService", Connyage, "http://" + cIPAddress + "/WebService/General/GeneralSenfoniService.asmx")
                    oUyum.cURLUyumSaveWebService = GetSysParConnected("URLUyumSaveWebService", Connyage, "http://" + cIPAddress + "/WebService/ERP/UyumSaveWebService.asmx")
                    oUyum.cURLUyumGetWebService = GetSysParConnected("URLUyumGetWebService", Connyage, "http://" + cIPAddress + "/WebService/ERP/UyumGetWebService.asmx")
                    oUyum.cURLUyumWebService = GetSysParConnected("URLUyumWebService", Connyage, "http://" + cIPAddress + "/WebService/UyumWebService.asmx")

                    If lUserWhoHasDeleteRight Then
                        oUyum.cUserName = GetSysParConnected("UyumKullaniciAdi", Connyage, "webservis")
                        oUyum.cPassword = GetSysParConnected("UyumKullaniciSifresi", Connyage, "uyum123*")
                    Else
                        If oConnection.cUyumUsername.Trim = "" Then
                            oUyum.cUserName = GetSysParConnected("UyumKullaniciAdi", Connyage, "webservis")
                            oUyum.cPassword = GetSysParConnected("UyumKullaniciSifresi", Connyage, "uyum123*")
                        Else
                            oUyum.cUserName = oConnection.cUyumUsername.Trim
                            oUyum.cPassword = oConnection.cUyumPassword.Trim
                        End If
                    End If

                    oUyum.cCoCode = GetSysParConnected("UyumCoCode", Connyage, "10")
                    oUyum.cBranchCode = GetSysParConnected("UyumBranchCode", Connyage, "10")
                    oUyum.cWhouseCode = GetSysParConnected("UyumWhouseCode", Connyage, "10")
                    oUyum.cWorkCenterCode = GetSysParConnected("UyumWorkCenterCode", Connyage, "10")
                    oUyum.cWorkOrderType = GetSysParConnected("UyumIsEmriTipiCode", Connyage, "STANDART")
                Case 2
                    ' resmi ihracat firması (R firması yurt dışı / grup dışı satışı)
                    ' resmi ihracat firması müşteriye satış yapıyor
                    ' stokfislines.uyumid3
                    ' stokfislines.uyumsipid2
                    cIPAddress = "192.168.1.3"

                    oUyum.cURLGeneralB2BService = GetSysParConnected("URLGeneralB2BService2", Connyage, "http://" + cIPAddress + "/WebService/B2B/GeneralB2BService.asmx")
                    oUyum.cURLUyumErogluProduction = GetSysParConnected("URLErogluProduction2", Connyage, "http://" + cIPAddress + "/webservice/eroglu/ErogluProduction.asmx")
                    oUyum.cURLGeneralSenfoniService = GetSysParConnected("URLGeneralSenfoniService2", Connyage, "http://" + cIPAddress + "/WebService/General/GeneralSenfoniService.asmx")
                    oUyum.cURLUyumSaveWebService = GetSysParConnected("URLUyumSaveWebService2", Connyage, "http://" + cIPAddress + "/WebService/ERP/UyumSaveWebService.asmx")
                    oUyum.cURLUyumGetWebService = GetSysParConnected("URLUyumGetWebService2", Connyage, "http://" + cIPAddress + "/WebService/ERP/UyumGetWebService.asmx")
                    oUyum.cURLUyumWebService = GetSysParConnected("URLUyumWebService2", Connyage, "http://" + cIPAddress + "/WebService/UyumWebService.asmx")

                    If lUserWhoHasDeleteRight Then
                        oUyum.cUserName = GetSysParConnected("UyumKullaniciAdi2", Connyage, "webservis")
                        oUyum.cPassword = GetSysParConnected("UyumKullaniciSifresi2", Connyage, "uyum123*")
                    Else
                        If oConnection.cUyumUsername.Trim = "" Then
                            oUyum.cUserName = GetSysParConnected("UyumKullaniciAdi2", Connyage, "webservis")
                            oUyum.cPassword = GetSysParConnected("UyumKullaniciSifresi2", Connyage, "uyum123*")
                        Else
                            oUyum.cUserName = oConnection.cUyumUsername.Trim
                            oUyum.cPassword = oConnection.cUyumPassword.Trim
                        End If
                    End If

                    oUyum.cCoCode = GetSysParConnected("UyumCoCode2", Connyage, "10")
                    oUyum.cBranchCode = GetSysParConnected("UyumBranchCode2", Connyage, "10")
                    oUyum.cWhouseCode = GetSysParConnected("UyumWhouseCode2", Connyage, "10")
                    oUyum.cWorkCenterCode = GetSysParConnected("UyumWorkCenterCode2", Connyage, "10")
                    oUyum.cWorkOrderType = GetSysParConnected("UyumIsEmriTipiCode2", Connyage, "STANDART")
                Case 3
                    ' resmi üretim firması (R firması yurt içi / grup içi satışı)
                    ' resmi üretim firması, resmi ihracat firmasına satış yapıyor
                    ' stokfislines.uyumid2
                    cIPAddress = "192.168.1.3"

                    oUyum.cURLGeneralB2BService = GetSysParConnected("URLGeneralB2BService2", Connyage, "http://" + cIPAddress + "/WebService/B2B/GeneralB2BService.asmx")
                    oUyum.cURLUyumErogluProduction = GetSysParConnected("URLErogluProduction2", Connyage, "http://" + cIPAddress + "/webservice/eroglu/ErogluProduction.asmx")
                    oUyum.cURLGeneralSenfoniService = GetSysParConnected("URLGeneralSenfoniService2", Connyage, "http://" + cIPAddress + "/WebService/General/GeneralSenfoniService.asmx")
                    oUyum.cURLUyumSaveWebService = GetSysParConnected("URLUyumSaveWebService2", Connyage, "http://" + cIPAddress + "/WebService/ERP/UyumSaveWebService.asmx")
                    oUyum.cURLUyumGetWebService = GetSysParConnected("URLUyumGetWebService2", Connyage, "http://" + cIPAddress + "/WebService/ERP/UyumGetWebService.asmx")
                    oUyum.cURLUyumWebService = GetSysParConnected("URLUyumWebService2", Connyage, "http://" + cIPAddress + "/WebService/UyumWebService.asmx")

                    If lUserWhoHasDeleteRight Then
                        oUyum.cUserName = GetSysParConnected("UyumKullaniciAdi2", Connyage, "webservis")
                        oUyum.cPassword = GetSysParConnected("UyumKullaniciSifresi2", Connyage, "uyum123*")
                    Else
                        If oConnection.cUyumUsername.Trim = "" Then
                            oUyum.cUserName = GetSysParConnected("UyumKullaniciAdi2", Connyage, "webservis")
                            oUyum.cPassword = GetSysParConnected("UyumKullaniciSifresi2", Connyage, "uyum123*")
                        Else
                            oUyum.cUserName = oConnection.cUyumUsername.Trim
                            oUyum.cPassword = oConnection.cUyumPassword.Trim
                        End If
                    End If

                    oUyum.cCoCode = GetSysParConnected("UyumCoCode3", Connyage, "10")
                    oUyum.cBranchCode = GetSysParConnected("UyumBranchCode3", Connyage, "10")
                    oUyum.cWhouseCode = GetSysParConnected("UyumWhouseCode2", Connyage, "10")
                    oUyum.cWorkCenterCode = GetSysParConnected("UyumWorkCenterCode2", Connyage, "10")
                    oUyum.cWorkOrderType = GetSysParConnected("UyumIsEmriTipiCode2", Connyage, "STANDART")
            End Select

            oUyum.cIhracatFirmaId = GetSysParConnected("UyumIhracatFirmaId", Connyage, "1045360")

            If GetSysParConnected("UyumDTFirmaAktif", Connyage, "0") = "1" Then
                oUyum.lUyumDTFirmaAktif = True
            Else
                oUyum.lUyumDTFirmaAktif = False
            End If


            Connyage.Close()

        Catch ex As Exception
            ErrDisp("initUyumServices", cModuleName,,, ex)
        End Try
    End Sub

    Public Function UyumGenelSil(cKey As String, nCase As Integer, Optional ByRef cMessage As String = "") As Boolean

        Dim oService As New UyumWebService.UyumWebService
        Dim oToken As New UyumWebService.UyumToken
        Dim oRequest As New UyumWebService.UyumServiceRequestOfObjectDeleteIn
        Dim cResult As String = ""

        Dim nID As Double = 0
        Dim cSQL As String = ""
        Dim cObjectCollectionTypeName As String = ""
        Dim nInitMode As Integer = 3

        UyumGenelSil = False

        Try
            Select Case nCase
                Case 1 ' stok kartı
                    cObjectCollectionTypeName = "INV.ItemCollection,INV"

                    cSQL = "select uyumid " +
                            " from stok with (NOLOCK) " +
                            " where stokno = '" + cKey.Trim + "' "

                    nID = ReadSingleDoubleValue(cSQL)
                Case 2 ' satınalma siparişi fiili üretim firması
                    cObjectCollectionTypeName = "PSM.OrderMCollection,PSM"

                    cSQL = "select uyumid " +
                            " from isemri with (NOLOCK) " +
                            " where isemrino = '" + cKey.Trim + "' "

                    nID = ReadSingleDoubleValue(cSQL)
                Case 3 ' satış siparişi fiili üretim firması
                    cObjectCollectionTypeName = "PSM.OrderMCollection,PSM"

                    cSQL = "select uyumsiparisid " +
                            " from siparis with (NOLOCK) " +
                            " where kullanicisipno = '" + cKey.Trim + "' "

                    nID = ReadSingleDoubleValue(cSQL)

                Case 4 ' satış siparişi resmi üretim firması
                    cSQL = "select top 1 a.uyumsiparisid, a.uyumsiparisid2 " +
                            " from siparis a with (NOLOCK) , firma b with (NOLOCK) " +
                            " where a.musterino = b.firma " +
                            " and a.kullanicisipno = '" + cKey.Trim + "' " +
                            " and b.grupfirmasi = 'E' "

                    If CheckExists(cSQL) Then
                        nInitMode = 3
                    Else

                        nInitMode = 2
                    End If

                    cObjectCollectionTypeName = "PSM.OrderMCollection,PSM"

                    cSQL = "select uyumsiparisid2 " +
                            " from siparis with (NOLOCK) " +
                            " where kullanicisipno = '" + cKey.Trim + "' "

                    nID = ReadSingleDoubleValue(cSQL)
                Case 5 ' satınalma siparişi Resmi üretim firması
                    cObjectCollectionTypeName = "PSM.OrderMCollection,PSM"

                    cSQL = "select uyumid2 " +
                            " from isemri with (NOLOCK) " +
                            " where isemrino = '" + cKey.Trim + "' "

                    nID = ReadSingleDoubleValue(cSQL)

                    nInitMode = 3
            End Select

            If nID = 0 Then
                cMessage = cKey.Trim + " uyum entegrasyon id bulunamadı"
                Exit Function
            End If

            initUyumServices(nInitMode)

            oService.Url = oUyum.cURLUyumWebService

            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            oRequest.Token = oToken
            oRequest.Value = New UyumWebService.ObjectDeleteIn
            oRequest.Value.ObjectCollectionTypeName = cObjectCollectionTypeName
            oRequest.Value.Id = CInt(nID)

            cResult = oService.DeleteObject(oRequest)

            Select Case nCase
                Case 1 ' stok kartı
                    cSQL = "update stok " +
                            " set uyumid = 0 " +
                            " where stokno = '" + cKey.Trim + "' "

                    ExecuteSQLCommand(cSQL)
                Case 2 ' satınalma siparişi F firması
                    cSQL = "update isemri " +
                            " set uyumid = 0 " +
                            " where isemrino = '" + cKey.Trim + "' "

                    ExecuteSQLCommand(cSQL)
                Case 3 ' satış siparişi F firması
                    cSQL = "update siparis " +
                            " set uyumsiparisid = 0 " +
                            " where kullanicisipno = '" + cKey.Trim + "' "

                    ExecuteSQLCommand(cSQL)
                Case 4 ' satış siparişi R firması
                    cSQL = "update siparis " +
                            " set uyumsiparisid2 = 0 " +
                            " where kullanicisipno = '" + cKey.Trim + "' "

                    ExecuteSQLCommand(cSQL)
                Case 5 ' satınalma siparişi R firması
                    cSQL = "update isemri " +
                            " set uyumid2 = 0 " +
                            " where isemrino = '" + cKey.Trim + "' "

                    ExecuteSQLCommand(cSQL)
            End Select

            cMessage = cResult

            UyumGenelSil = True

        Catch ex As Exception
            ErrDisp("UyumGenelSil", cModuleName,,, ex)
        End Try

    End Function

    Public Function UyumIrsaliyeYaz() As Boolean
        ' id ler üzerinden hep gidilecek
        UyumIrsaliyeYaz = False

        Try
            Dim oService As New UyumsoftSaveWebService.UyumSaveWebService
            Dim oToken As New UyumsoftSaveWebService.UyumToken
            Dim oResult As New UyumsoftSaveWebService.ServiceResultOfBoolean
            Dim Inparam As New UyumsoftSaveWebService.UyumServiceRequestOfItemDef
            Dim oFis As New UyumsoftSaveWebService.ItemDef          ' INVT_ITEM_M tablosu
            Dim oSatir As New UyumsoftSaveWebService.ItemDetailDef  ' INVT_ITEM_D tablosu
            Dim oSatirlar As New List(Of UyumsoftSaveWebService.ItemDetailDef)
            Dim nKur As Decimal = 1

            initUyumServices()

            oService.Url = oUyum.cURLUyumSaveWebService

            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            oFis.CoCode = oUyum.cCoCode
            oFis.BranchCode = oUyum.cBranchCode

            oFis.DocNo = SQLWriteString("779", 16)
            oFis.DocDate = DateTime.Now.Date
            oFis.ReceiptDate = DateTime.Now.Date
            oFis.DocTraId = 1292            ' gnld_doc_tra      / döküman tipi
            oFis.EntityCode = "C_1000"      ' find_entity       / cari kodları
            oFis.CurRateTypeId = 235        ' mb alış , 234 mb satış
            oFis.CurCode = "TRY"            ' gnld_currency     / döviz kodları
            oFis.CurTra = nKur
            oFis.IsWaybil = True
            ' satırları yaz
            oSatir.LineNo = 10
            oSatir.WhouseCode = oUyum.cWhouseCode
            oSatir.LineType = UyumsoftSaveWebService.LineType.S
            ' stok kodu
            oSatir.DcardCode = "15201001P01" ' INVD_ITEM        / stok kodu
            ' fiyat
            oSatir.UnitPrice = 1
            oSatir.CurCode = oFis.CurCode
            oSatir.CurRateTypeId = oFis.CurRateTypeId
            oSatir.CurRateTra = oFis.CurTra
            oSatir.VatCode = "18"            ' invd_tax          / 18,8,1 olabilir
            oSatir.VatRate = 18
            ' miktar
            oSatir.Qty = 1
            oSatir.UnitCode = "ADET"         ' INVD_UNIT        / birimler 
            ' satırlara yaz
            oSatirlar.Add(oSatir)
            ' fiş detayını oluştur
            oFis.Details = oSatirlar.ToArray()
            ' fişi oluştur
            Inparam.Token = New UyumsoftSaveWebService.UyumServiceRequestOfItemDef().Token
            Inparam.Token = oToken
            Inparam.Value = oFis
            ' irsaliyeyi yaz
            oResult = oService.SaveWaybill(Inparam)
            ' sonuca bak
            If Not oResult.Result Then
                MsgBox("Irsaliye kaydedildi")
            Else
                MsgBox("Dikkat irsaliye kaydı başarısız")
            End If

            UyumIrsaliyeYaz = oResult.Result

        Catch ex As Exception
            ErrDisp("UyumIrsaliyeYaz", cModuleName,,, ex)
        End Try
    End Function
    Public Function UyumGetDataSet(nCase As Integer, Optional nEntityID As Integer = 0, Optional lShowResults As Boolean = True) As DataSet

        UyumGetDataSet = Nothing

        Try
            Dim oService As New UyumsoftGetWEBService.UyumGetWebService
            Dim oToken As New UyumsoftGetWEBService.UyumToken
            Dim Inparam As New UyumsoftGetWEBService.UyumServiceRequestOfGetServiceDef
            Dim oResult As New UyumsoftGetWEBService.ServiceResultOfDataSet
            Dim oServiceDef As New UyumsoftGetWEBService.GetServiceDef

            initUyumServices()

            oService.Url = oUyum.cURLUyumGetWebService

            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            'oServiceDef.Id = nEntityID
            'oServiceDef.DocNo = "324"
            'oServiceDef.Code = "C_1000"

            oServiceDef.CoCode = oUyum.cCoCode
            oServiceDef.BranchCode = oUyum.cBranchCode
            oServiceDef.StartDate = #1/1/1950#
            oServiceDef.EndDate = #1/1/2099#

            Inparam.Token = New UyumsoftGetWEBService.UyumServiceRequestOfGetServiceDef().Token
            Inparam.Token = oToken
            Inparam.Value = oServiceDef

            Select Case nCase
                Case 1 ' irsaliye
                    oResult = oService.GetWaybill(Inparam)
                Case 2 ' cari
                    oResult = oService.GetEntity(Inparam)
                Case 3 ' stok
                    oResult = oService.GetItem(Inparam)
            End Select

            If oResult.Result Then

                UyumGetDataSet = oResult.Value

                If lShowResults Then
                    If oResult.Value.Tables.Count > 0 Then
                        Dim oSpreadReport1 As New SpreadReport
                        oSpreadReport1.init2(oResult.Value.Tables(0))
                    End If

                    If oResult.Value.Tables.Count > 1 Then
                        Dim oSpreadReport2 As New SpreadReport
                        oSpreadReport2.init2(oResult.Value.Tables(1))
                    End If
                End If
            Else
                UyumGetDataSet = Nothing
            End If

        Catch ex As Exception
            ErrDisp("UyumGetDataSet", cModuleName,,, ex)
        End Try

    End Function

    Public Function UyumGetDataTable(nCase As Integer, Optional nEntityID As Integer = 0, Optional lShowResults As Boolean = True) As DataTable

        UyumGetDataTable = Nothing

        Try
            Dim oService As New GeneralB2B.GeneralB2BService
            Dim oSpreadReport As New SpreadReport
            Dim oResult As DataTable = Nothing

            initUyumServices()

            oService.Url = oUyum.cURLGeneralB2BService

            Select Case nCase
                Case 1 ' stok kartları
                    oResult = oService.GetItemList(0)
                Case 2 ' cari kartları
                    oResult = oService.GetEntity(0)
                Case 3
                    oResult = oService.GetOrders(GeneralB2B.PurchaseSales.Alış, GeneralB2B.OrderStatus.Açık, nEntityID)
                Case 4
                    oResult = oService.GetOrders(GeneralB2B.PurchaseSales.Alış, GeneralB2B.OrderStatus.Kapalı, nEntityID)
                Case 5
                    oResult = oService.GetOrders(GeneralB2B.PurchaseSales.Alış_İade, GeneralB2B.OrderStatus.Açık, nEntityID)
                Case 6
                    oResult = oService.GetOrders(GeneralB2B.PurchaseSales.Alış_İade, GeneralB2B.OrderStatus.Kapalı, nEntityID)
                Case 7
                    oResult = oService.GetOrders(GeneralB2B.PurchaseSales.Satış, GeneralB2B.OrderStatus.Açık, nEntityID)
                Case 8
                    oResult = oService.GetOrders(GeneralB2B.PurchaseSales.Satış, GeneralB2B.OrderStatus.Kapalı, nEntityID)
                Case 9
                    oResult = oService.GetOrders(GeneralB2B.PurchaseSales.Satış_İade, GeneralB2B.OrderStatus.Açık, nEntityID)
                Case 10
                    oResult = oService.GetOrders(GeneralB2B.PurchaseSales.Satış_İade, GeneralB2B.OrderStatus.Kapalı, nEntityID)
            End Select

            If lShowResults Then
                If oResult.Rows.Count > 0 Then
                    oSpreadReport.init2(oResult)
                End If
            End If

            UyumGetDataTable = oResult

        Catch ex As Exception
            ErrDisp("UyumGetDataTable", cModuleName,,, ex)
        End Try
    End Function

    Public Sub UyumCariSorgula()
        Try
            Dim oService As New GeneralB2B.GeneralB2BService
            Dim oResult As DataTable
            Dim oSpreadReport As New SpreadReport

            initUyumServices()

            oService.Url = oUyum.cURLGeneralB2BService

            oResult = oService.GetEntity(0) ' get all
            oSpreadReport.init2(oResult)

            'MsgBox("cari sorgula ok")

        Catch ex As Exception
            ErrDisp("UyumCariSorgula", cModuleName,,, ex)
        End Try
    End Sub

    Public Function UyumGetIDFromCode(nCase As Integer, cCode As String) As String

        Dim cAradigiYer As String = ""
        Dim cSonuc As String = ""

        UyumGetIDFromCode = ""

        Try
            Dim oService As New UyumsoftGetWEBService.UyumGetWebService
            Dim oToken As New UyumsoftGetWEBService.UyumToken
            Dim Inparam As New UyumsoftGetWEBService.UyumServiceRequestOfGetServiceDef
            Dim oDataSet As New UyumsoftGetWEBService.ServiceResultOfDataSet
            Dim oServiceDef As New UyumsoftGetWEBService.GetServiceDef

            Select Case nCase
                Case 1 ' cari id
                    cAradigiYer = "cari"
                Case 2 ' stok id
                    cAradigiYer = "stok"
                Case 3 ' waybill id
                    cAradigiYer = "waybill"
            End Select

            initUyumServices()

            oService.Url = oUyum.cURLUyumGetWebService

            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            oServiceDef.CoCode = oUyum.cCoCode
            oServiceDef.BranchCode = oUyum.cBranchCode
            Select Case nCase
                Case 1, 2
                    oServiceDef.Code = cCode    ' cari / stok kodu
                Case 3
                    oServiceDef.DocNo = cCode   ' irsaliye no, uyumsoft ta unique
            End Select

            Inparam.Token = New UyumsoftGetWEBService.UyumServiceRequestOfGetServiceDef().Token
            Inparam.Token = oToken
            Inparam.Value = oServiceDef

            Select Case nCase
                Case 1 ' cari id
                    oDataSet = oService.GetEntity(Inparam)
                    If oDataSet.Value.Tables.Count > 0 Then
                        cSonuc = oDataSet.Value.Tables(0).Rows(0).ItemArray(2).ToString()
                    End If
                Case 2 ' stok id
                    oDataSet = oService.GetItem(Inparam)
                    If oDataSet.Value.Tables.Count > 0 Then
                        cSonuc = oDataSet.Value.Tables(0).Rows(0).ItemArray(34).ToString()
                    End If
                Case 3 ' waybill id
                    oDataSet = oService.GetWaybill(Inparam)
                    If oDataSet.Value.Tables.Count > 0 Then
                        cSonuc = oDataSet.Value.Tables(0).Rows(0).ItemArray(10).ToString()
                    End If
            End Select

            UyumGetIDFromCode = cSonuc

        Catch ex As Exception
            If ex.Message.Trim = "There is no row at position 0." Then
                ' do nothing
            Else
                ErrDisp("UyumGetIDFromCode : " + cAradigiYer + " / " + cCode, cModuleName,,, ex)
            End If
        End Try
    End Function

    Public Function UyumGetIDFromCodeFast(cSearch As String, cObjectCollectionTypeName As String) As String

        Dim oService As New UyumWebService.UyumWebService
        Dim oToken As New UyumWebService.UyumToken
        Dim Inparam As New UyumWebService.UyumServiceRequestOfObjectInfoIn
        Dim oSearch As New UyumWebService.WherePropertyClause
        Dim oSearchs As New List(Of UyumWebService.WherePropertyClause)
        Dim oDataSet As DataSet

        UyumGetIDFromCodeFast = ""

        Try
            initUyumServices()

            oSearch.PropertyName = "ItemCode"
            oSearch.Value = cSearch.Trim

            oService.Url = oUyum.cURLUyumWebService

            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            Inparam.Token = oToken
            Inparam.Value = New UyumWebService.ObjectInfoIn
            Inparam.Value.ObjectCollectionTypeName = cObjectCollectionTypeName
            Inparam.Value.LoadDetail = False

            oSearchs.Add(oSearch)
            Inparam.Value.WhereClauseList = oSearchs.ToArray

            oDataSet = oService.GetObjectToDataSet(Inparam)

            Select Case cObjectCollectionTypeName
                ' stok kartı
                Case "INV.ItemCollection,INV"
                    If oDataSet.Tables.Count > 0 Then
                        UyumGetIDFromCodeFast = oDataSet.Tables(0).Rows(0).Item("Id").ToString
                    End If
                ' cari
                Case "FIN.EntityCollection,FIN"
                    If oDataSet.Tables.Count > 0 Then
                        UyumGetIDFromCodeFast = oDataSet.Tables(0).Rows(0).Item("Id").ToString
                    End If
            End Select

        Catch ex As Exception
            ' do nothing 
            ' ErrDisp("UyumGetIDFromCode", cModuleName,,, ex)
        End Try
    End Function

    Public Function UyumCarilerTable2() As String

        Dim oDataSet As DataSet = Nothing
        Dim nRow As Integer = 0
        Dim cID As String = ""
        Dim cCode As String = ""
        Dim cHesapNo As String = ""
        Dim cSQL As String = ""
        Dim cTable As String = ""

        UyumCarilerTable2 = ""

        Try
            oDataSet = UyumGetDataSet(2,, False)

            If oDataSet.Tables.Count > 0 Then

                cSQL = " (id char(30) null, code char(100) null, hesapno char(30) null) "
                cTable = CreateTempTable(cSQL)

                For nRow = 0 To oDataSet.Tables(0).Rows.Count - 1
                    cID = oDataSet.Tables(0).Rows(nRow).Item(2).ToString.Trim
                    cCode = oDataSet.Tables(0).Rows(nRow).Item(15).ToString.Trim
                    cHesapNo = oDataSet.Tables(0).Rows(nRow).Item(14).ToString.Trim

                    cSQL = "insert " + cTable + " (id, code, hesapno) " +
                            " values ('" + SQLWriteString(cID, 30) + "', " +
                            " '" + SQLWriteString(cCode, 100) + "', " +
                            " '" + SQLWriteString(cHesapNo, 30) + "' ) "

                    ExecuteSQLCommand(cSQL)
                Next
            End If

            UyumCarilerTable2 = cTable

        Catch ex As Exception
            ErrDisp("UyumCarilerTable2 : " + ex.Message, "HTMain",,, ex)
        End Try

    End Function

    Public Function UyumStoklarTable2() As String

        Dim oDataSet As DataSet = Nothing
        Dim nRow As Integer = 0
        Dim cID As String = ""
        Dim cCode As String = ""
        Dim cAciklama As String = ""
        Dim cSQL As String = ""
        Dim cTable As String = ""

        UyumStoklarTable2 = ""

        Try
            oDataSet = UyumGetDataSet(3,, False)

            If oDataSet.Tables.Count > 0 Then

                cSQL = " (id char(30) null, code char(100) null, aciklama char(100) null) "
                cTable = CreateTempTable(cSQL)

                For nRow = 0 To oDataSet.Tables(0).Rows.Count - 1
                    cID = oDataSet.Tables(0).Rows(nRow).Item(2).ToString.Trim
                    cCode = oDataSet.Tables(0).Rows(nRow).Item(15).ToString.Trim
                    cAciklama = oDataSet.Tables(0).Rows(nRow).Item(14).ToString.Trim

                    cSQL = "insert " + cTable + " (id, code, hesapno) " +
                            " values ('" + SQLWriteString(cID, 30) + "', " +
                            " '" + SQLWriteString(cCode, 100) + "', " +
                            " '" + SQLWriteString(cAciklama, 100) + "' ) "

                    ExecuteSQLCommand(cSQL)
                Next
            End If

            UyumStoklarTable2 = cTable

        Catch ex As Exception
            ErrDisp("UyumStoklarTable2 : " + ex.Message, "HTMain",,, ex)
        End Try

    End Function


    'Dim oContext As New UyumsoftSaveWebService.UyumServiceRequestOfAccDefMM
    'Dim oAccDef As New UyumsoftSaveWebService.AccDefMM
    'Dim oAccDetailDef As New UyumsoftSaveWebService.AccDetailDefMM
    'Dim oAccDetailDefList As New List(Of UyumsoftSaveWebService.AccDetailDefMM)

    'oAccDef.CoCode = "10"
    'oAccDef.AccReceipt = UyumsoftSaveWebService.AccReceipt.Mahsup
    'oAccDef.DocDate = DateTime.Now.Date
    'oAccDef.CatCode1 = "a"
    'oAccDef.CatCode2 = "aa"
    'oAccDef.Note1 = "Andaç Fiş İşlemleri Web Servis Test"
    'oAccDef.SourceMId = 110

    'oAccDetailDef.LineNo = 10
    'oAccDetailDef.TraTypeCode = "FT-01-Alış"
    'oAccDetailDef.AccCode = "320.01"
    'oAccDetailDef.AccDesc = "Satıcı Sevtap Turancı"
    'oAccDetailDef.Title = "Sevtap Turancı"
    'oAccDetailDef.IdentifyNo = "34786148540"
    'oAccDetailDef.TaxNo = String.Empty
    'oAccDetailDef.AmtDebit = 100
    'oAccDetailDef.AmtTraDebit = 100
    'oAccDetailDef.CurCode = "TRY"
    'oAccDetailDef.CurRateTra = 1
    'oAccDetailDef.DocNo = "2547874"
    'oAccDetailDef.DocumentDate = DateTime.Now.Date
    'oAccDetailDef.Address = "Merkez Mahallesi İstanbul"
    'oAccDetailDef.CityName = "ADANA"
    'oAccDetailDef.TownName = "CEYHAN"
    'oAccDetailDef.Qty = 1
    'oAccDetailDef.IsMatchAccCode = True
    'oAccDetailDef.ItemCode = "TEL-1"
    'oAccDetailDef.Email = "uyum@uyumsoft.com.tr"
    'oAccDetailDef.MobileTel = "5355957874"
    'oAccDetailDefList.Add(oAccDetailDef)

    'oAccDetailDef = New UyumsoftSaveWebService.AccDetailDefMM()
    'oAccDetailDef.LineNo = 20
    'oAccDetailDef.TraTypeCode = "FT-02-Satış"
    'oAccDetailDef.AccCode = "108.01.001"
    'oAccDetailDef.AccDesc = "Banka Sevtap Turancı"
    'oAccDetailDef.Title = "Sevtap Turancı"
    'oAccDetailDef.TaxNo = String.Empty
    'oAccDetailDef.IdentifyNo = "34786148540"
    'oAccDetailDef.AmtCredit = 100
    'oAccDetailDef.AmtTraCredit = 100
    'oAccDetailDef.CurCode = "TRY"
    'oAccDetailDef.CurRateTra = 1
    'oAccDetailDef.DocNo = "254787452"
    'oAccDetailDef.DocumentDate = DateTime.Now.Date
    'oAccDetailDef.CityName = "İSTANBUL"
    'oAccDetailDef.TownName = "BEYLİKDÜZÜ"
    'oAccDetailDef.Qty = 1
    'oAccDetailDef.IsMatchAccCode = False
    'oAccDetailDef.ItemCode = "TEL-8"
    'oAccDetailDef.ItemName = "Stok Tel"
    'oAccDetailDef.UnitCode = "ADET"
    'oAccDetailDef.Address = "Merkez Mahallesi İstanbul"
    'oAccDetailDef.Email = "uyum@uyumsoft.com.tr"
    'oAccDetailDef.MobileTel = "5355957874"
    'oAccDetailDefList.Add(oAccDetailDef)

    'oAccDef.Details = oAccDetailDefList.ToArray()

    'oContext.Token = New UyumsoftSaveWebService.UyumServiceRequestOfAccDefMM().Token
    'oContext.Token = oToken
    'oContext.Value = oAccDef

    'lResult = oSaveWS.SaveAccMM(oContext)

    'Public Function UyumFaturaYaz() As Boolean

    '    UyumFaturaYaz = False

    '    Dim oService As New UyumsoftSaveWebService.UyumSaveWebService
    '    Dim oResult As New UyumsoftSaveWebService.ServiceResultOfBoolean
    '    Dim oToken As New UyumsoftSaveWebService.UyumToken
    '    Dim oContext As New UyumsoftSaveWebService.UyumServiceRequestOfInvoiceDef
    '    Dim oinvoiceDef As New UyumsoftSaveWebService.InvoiceDef
    '    Dim oInvoiceDetailDef As New UyumsoftSaveWebService.InvoiceDetailDef
    '    Dim oInvoiceDetailList As New List(Of UyumsoftSaveWebService.InvoiceDetailDef)

    '    oToken.UserName = cUserName
    '    oToken.Password = cPassword

    '    oInvoiceDetailDef.LineNo = 10
    '    'oInvoiceDetailDef.LineType = LineType.S
    '    oInvoiceDetailDef.DcardCode = "APY.AKC.CYP.56"
    '    oInvoiceDetailDef.UnitCode = "ADET"
    '    oInvoiceDetailDef.Qty = 1
    '    oInvoiceDetailDef.UnitPrice = 100
    '    oInvoiceDetailDef.UnitPriceTra = 100
    '    oInvoiceDetailDef.CurCode = "TRL"
    '    oInvoiceDetailDef.CurRateTypeId = 0
    '    oInvoiceDetailDef.CurRateTra = 1
    '    oInvoiceDetailDef.Amt = 100
    '    oInvoiceDetailDef.AmtTra = 100
    '    'oInvoiceDetailDef.VatStatus = VatStatus.Hariç
    '    oInvoiceDetailDef.Note1 = "Uyum Fatura Test"
    '    oInvoiceDetailDef.Note2 = ""
    '    oInvoiceDetailDef.Note3 = ""
    '    oInvoiceDetailDef.VatCode = "KDV"
    '    oInvoiceDetailDef.AnalysisCode = ""
    '    oInvoiceDetailDef.ProjectCode = ""
    '    oInvoiceDetailDef.TaxTemplateName = ""
    '    oInvoiceDetailDef.Disc1Code = ""
    '    oInvoiceDetailDef.Disc2Code = ""
    '    oInvoiceDetailDef.WhouseCode = "2080"
    '    oInvoiceDetailList.Add(oInvoiceDetailDef)

    '    UyumFaturaYaz = True

    'End Function

    'Public Function UyumCariEkle(Optional cFilter As String = "") As Boolean

    '    Dim cSQL As String = ""
    '    Dim Connyage As SqlConnection
    '    Dim dr As SqlDataReader
    '    Dim cFirma As String = ""
    '    Dim cUyumCariID As String = ""

    '    Connyage = OpenConn()

    '    cSQL = "select top 10 firma, aciklama, firmatipi, digeradi, tel1, " +
    '            " tel2, tel3, telex, fax, adres1, " +
    '            " adres2, semt, sehir, ulke, vergidairesi, " +
    '            " vergino " +
    '            " from firma with (NOLOCK) " +
    '            " where firma Is Not null " +
    '            " And firma <> '' " +
    '            cFilter

    '    dr = GetSQLReader(cSQL, Connyage)

    '    Do While dr.Read
    '        cFirma = SQLReadString(dr, "firma")

    '        cUyumCariID = UyumGetIDFromCode(1, cFirma)

    '        If cUyumCariID.Trim = "" Then
    '            cUyumCariID = UyumCariInsert(cFirma, SQLReadString(dr, "adres1"), SQLReadString(dr, "adres2"), SQLReadString(dr, "semt"), SQLReadString(dr, "sehir"),
    '                                         SQLReadString(dr, "ulke"), SQLReadString(dr, "tel1"), SQLReadString(dr, "tel2"), SQLReadString(dr, "vergidairesi"), SQLReadString(dr, "vergino"),
    '                                         SQLReadString(dr, "aciklama"), SQLReadString(dr, "fax"))
    '        End If

    '        UpdateWinTexCari(cFirma, cUyumCariID)
    '    Loop

    '    Connyage.Close()
    '    UyumCariEkle = True

    'End Function

    'Private Function UyumCariInsert(cFirma As String, cAdres1 As String, cAdres2 As String, cSemt As String, cSehir As String,
    '                                cUlke As String, cTel1 As String, cTel2 As String, cVergiDairesi As String, cVergiNo As String,
    '                                cAciklama As String, cFax As String) As String

    '    UyumCariInsert = ""

    '    Try
    '        Dim oService As New GeneralB2B.GeneralB2BService
    '        Dim Inparam As New GeneralB2B.ServiceEntity
    '        Dim oResult As GeneralB2B.ResultInfo

    '        initUyumServices()

    '        oService.Url = oUyum.cURLGeneralB2BService

    '        Inparam.CoCode = oUyum.cCoCode
    '        Inparam.EntityCode = cFirma
    '        If cAciklama.Trim = "" Then
    '            Inparam.EntityName = cFirma
    '        Else
    '            Inparam.EntityName = cAciklama
    '        End If
    '        'Inparam.TaxOfficeCode = cVergiDairesi
    '        Inparam.TaxNo = cVergiNo
    '        Inparam.Tel = cTel1
    '        Inparam.Tel2 = cTel2
    '        Inparam.Fax = cFax
    '        Inparam.Address1 = cAdres1
    '        Inparam.Address2 = cAdres2
    '        Inparam.EntityType = GeneralB2B.EntityType.Müşteri
    '        'Inparam.TownName = cSemt
    '        'Inparam.CityName = cSehir
    '        Inparam.CountryName = "TÜRKİYE" ' cUlke
    '        Inparam.CardIntgGroupCode = ""
    '        Inparam.AccCode = ""
    '        Inparam.OtomaticCreateAccCode = True
    '        Inparam.ContactId = 1

    '        oResult = oService.SaveEntityWithUpdate(Inparam)
    '        UyumCariInsert = oResult.Id.ToString

    '    Catch ex As Exception
    '        ErrDisp("UyumCariInsert", "UtilUyumsoft",,, ex)
    '    End Try
    'End Function

    'Private Sub UpdateWinTexCari(cFirma As String, cUyumCariID As String)

    '    Dim cSQL As String = ""

    '    Try
    '        If cUyumCariID.Trim = "" Then Exit Sub

    '        cSQL = "update firma " +
    '               " set uyumid = " + cUyumCariID +
    '               " where firma = '" + cFirma.Trim + "' "

    '        ExecuteSQLCommand(cSQL)

    '    Catch ex As Exception
    '        ErrDisp("UpdateWinTexCari", "UtilUyumsoft",,, ex)
    '    End Try
    'End Sub

    'Dim oService As New GeneralB2B.GeneralB2BService
    'Dim oToken As New GeneralB2B.Token
    'Dim Inparam As New GeneralB2B.ServiceRequestOfBranchItemInfo
    'Dim oResult As GeneralB2B.ResultInfo

    'Inparam.Value.WhouseCode = oUyum.cWhouseCode

End Module
