Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Module UtilUyumsoftSatinalmaSiparis

    Const cModuleName As String = "UtilUyumsoftSatinalmaSiparis"

    Public Function UyumSatinalmaSiparisEkle(nCase As Integer, cIsemriNo As String, Optional ByRef cMessage As String = "") As Boolean
        ' Satınalma Sipariş nDocTraID
        ' Yurtiçi Satınalma Siparişi: DocTraCode(Of String) : Hareket Kodu: SA-101 – Hareket Kodu ID :  1319
        ' İhraç Kayıtlı Satınalma Siparişi: DocTraCode(Of String) : Hareket Kodu: SA-103 – Hareket Kodu ID :  1321
        ' İthalat  Siparişi: DocTraCode(Of String) : Hareket Kodu: SA-201 – Hareket Kodu ID :  1322        

        ' nCase = 1 -> F Firması
        ' nCase = 2 -> R Firması
        Dim oService As UyumsoftSaveWebService.UyumSaveWebService
        Dim oToken As UyumsoftSaveWebService.UyumToken
        Dim oResult As UyumsoftSaveWebService.ServiceResultOfBoolean
        Dim orderdef As UyumsoftSaveWebService.UyumServiceRequestOfOrderDef
        Dim orderDetailDef As UyumsoftSaveWebService.OrderDetailDef
        Dim orderDetailDefList As List(Of UyumsoftSaveWebService.OrderDetailDef)

        Dim cSQL As String = ""
        Dim ConnYage As SqlConnection
        Dim dr As SqlDataReader

        Dim cStokNo As String = ""
        Dim cAciklama As String = ""
        Dim cUyumBirimID As String = ""
        Dim cUyumStokID As String = ""
        Dim cUyumStokTipiID As String = ""
        Dim cStokTipi As String = ""
        Dim nLineNo As Integer = 0
        Dim nDocTraID As Integer = 0
        Dim nUyumID As Double = 0
        Dim cAnaStokGrubu As String = ""
        Dim cGtipID As String = ""
        Dim cKdvID As String = ""
        Dim cMuhasebeID As String = ""
        Dim cMessage2 As String = ""
        Dim cAciklama2 As String = ""
        Dim nKur As Double = 0
        Dim cuyumsassatinalma As String = ""
        Dim cuyumsasithalat As String = ""
        Dim nMiktar As Double = 0

        UyumSatinalmaSiparisEkle = False

        Try
            ConnYage = OpenConn()

            ' resmi firma var mı 
            If nCase = 2 And GetSysParConnected("UyumDTFirmaAktif", ConnYage, "0") <> "1" Then
                ConnYage.Close()
                Exit Function
            End If

            cuyumsassatinalma = GetSysParConnected("uyumsassatinalma", ConnYage, "1319")      ' SATINALMA SİPARİŞİ
            cuyumsasithalat = GetSysParConnected("uyumsasithalat", ConnYage, "1322")          ' İTHALAT SİPARİŞİ
            nDocTraID = CInt(cuyumsassatinalma)

            cMessage = ""

            ' hepsi SIFIR miktarlı satırlardan oluşan işemri aktarılamaz
            cSQL = "select sum(coalesce(miktar1,0)) " +
                    " from isemrilines with (NOLOCK) " +
                    " where isemrino = '" + cIsemriNo + "' "

            nMiktar = ReadSingleDoubleValueConnected(cSQL, ConnYage)

            If nMiktar = 0 Then
                cMessage = cMessage + "isemri toplam miktari SIFIR olamaz"
                ConnYage.Close()
                Exit Function
            End If

            ' uyumda açılmamış stok kartı varsa aç

            cSQL = "select distinct a.stokno, a.cinsaciklamasi, a.birim1, a.stoktipi, a.anastokgrubu,  "

            cSQL = cSQL +
                    " aciklama2 = LTrim(case when a.anastokgrubu = 'MAMUL'  " +
                                        " then rtrim(coalesce((select top 1 coalesce(k.karisim,'') + ' ' + coalesce(m.sex,'')  " +
                                                " From siparis k with (NOLOCK) , sipmodel l with (NOLOCK) , ymodel m with (NoLOCK)  " +
                                                " Where k.kullanicisipno = l.siparisno " +
                                                " And l.modelno = m.modelno " +
                                                " And l.modelno = a.stokno " +
                                                " order by k.bilgisayarsipno desc),'')) + ' ' + a.stoktipi " +
                                        " Else a.kompozisyon " +
                                        " End), "

            cSQL = cSQL +
                    " stokid = a.uyumid, " +
                    " birimid = b.uyumid, " +
                    " stoktipiid = c.uyumid, " +
                    " gtipid = (select top 1 uyumid from gtip with (NOLOCK) where gtip = a.gtip), " +
                    " kdvid = (select top 1 uyumid from kdvgroup with (NOLOCK) where oran = a.kdv ), " +
                    " muhasebeid = c.uyummuhsablonid "


            cSQL = cSQL +
                    " from stok a with (NOLOCK) , " +
                    " birim b with (NOLOCK) , " +
                    " stoktipi c with (NOLOCK) , " +
                    " isemrilines d with (NOLOCK) "

            cSQL = cSQL +
                    " where a.birim1 = b.birim " +
                    " And a.stoktipi = c.kod " +
                    " And a.stokno = d.stokno " +
                    " And a.stokno is not null " +
                    " And a.stokno <> '' " +
                    " And (a.uyumid is null or a.uyumid = 0) " +
                    " And b.uyumid is not null " +
                    " And b.uyumid <> 0 " +
                    " And c.uyumid is not null " +
                    " And c.uyumid <> 0 " +
                    " And d.isemrino = '" + cIsemriNo + "' "

            dr = GetSQLReader(cSQL, ConnYage)

            Do While dr.Read
                cStokNo = SQLReadString(dr, "stokno")
                cStokTipi = SQLReadString(dr, "stoktipi")
                cAciklama = SQLReadString(dr, "cinsaciklamasi")
                cUyumBirimID = CStr(SQLReadDouble(dr, "birimid"))
                cUyumStokTipiID = CStr(SQLReadDouble(dr, "stoktipiid"))
                cAnaStokGrubu = SQLReadString(dr, "anastokgrubu")
                cGtipID = CStr(SQLReadDouble(dr, "gtipid"))
                cKdvID = CStr(SQLReadDouble(dr, "kdvid"))
                cMuhasebeID = CStr(SQLReadDouble(dr, "muhasebeid"))
                cAciklama2 = SQLReadString(dr, "aciklama2")

                cUyumStokID = UyumStokInsert(cStokNo, cAciklama, cUyumBirimID, cUyumStokTipiID, cAnaStokGrubu, cMessage2, cGtipID, cKdvID, cMuhasebeID, cAciklama2)

                If cUyumStokID.Trim = "" Then
                    If cMessage.Trim = "" Then
                        cMessage = cStokNo + ","
                    Else
                        cMessage = cStokNo + "," + cMessage
                    End If
                End If
            Loop
            dr.Close()

            If cMessage.Trim <> "" Then
                cMessage = cMessage + " stok kartlarının id leri bulunamadı"
                ConnYage.Close()
                Exit Function
            End If

            cSQL = "select top 1 a.uyumid, a.uyumid2, a.ithalat " +
                    " from isemri a with (NOLOCK) , firma b with (NOLOCK) " +
                    " where a.firma = b.firma " +
                    " and a.isemrino = '" + cIsemriNo + "' "
            '" and b.yabanci = 'E'  "

            dr = GetSQLReader(cSQL, ConnYage)

            If dr.Read Then
                Select Case nCase
                    Case 1
                        nUyumID = SQLReadDouble(dr, "uyumid")
                    Case 2
                        nUyumID = SQLReadDouble(dr, "uyumid2")
                End Select
                If nUyumID = 0 Then
                    nDocTraID = CInt(cuyumsasithalat)
                Else
                    cMessage = nUyumID.ToString
                    dr.Close()
                    UyumSatinalmaSiparisEkle = True
                    Exit Function
                End If
            Else
                cMessage = "Sadece firma kartında yabancı işareretli olan ithalat siparişleri Uyum programına transfer edilebilir"
                dr.Close()
                UyumSatinalmaSiparisEkle = False
                Exit Function
            End If
            dr.Close()

            Select Case nCase
                Case 1 ' Fiili Üretim Firması
                    initUyumServices(1)
                Case 2 ' Resmi Üretim Firması
                    initUyumServices(3)
            End Select

            oService = New UyumsoftSaveWebService.UyumSaveWebService
            oService.Timeout = 300000

            oService.Url = oUyum.cURLUyumSaveWebService

            oToken = New UyumsoftSaveWebService.UyumToken

            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            orderdef = New UyumsoftSaveWebService.UyumServiceRequestOfOrderDef

            orderdef.Token = oToken
            orderdef.Value = New UyumsoftSaveWebService.OrderDef

            cSQL = "select a.isemrino, a.firma, a.tarih, " +
                    " firmaid = b.uyumid, "

            cSQL = cSQL +
                    " dovizid = (SELECT TOP 1 y.uyumid " +
                                " From isemrilines x with (NOLOCK)  , doviz y with (NOLOCK)  " +
                                " Where x.doviz = y.doviz " +
                                " And x.isemrino = a.isemrino " +
                                " And y.uyumid Is Not null " +
                                " And y.uyumid <> 0), "
            cSQL = cSQL +
                    " kur = (SELECT TOP 1 dbo.getkur(x.doviz,a.tarih)  " +
                                " From isemrilines x with (NOLOCK)  , doviz y with (NOLOCK)  " +
                                " Where x.doviz = y.doviz " +
                                " And x.isemrino = a.isemrino " +
                                " And y.uyumid is not null " +
                                " And y.uyumid <> 0) "
            cSQL = cSQL +
                    " from isemri a with (NOLOCK) , firma b with (NOLOCK)  " +
                    " where a.firma = b.firma " +
                    " and a.isemrino = '" + cIsemriNo.Trim + "' "

            dr = GetSQLReader(cSQL, ConnYage)

            If dr.Read Then
                orderdef.Value.DocTraId = nDocTraID
                orderdef.Value.CoCode = oUyum.cCoCode
                orderdef.Value.BranchCode = oUyum.cBranchCode
                orderdef.Value.DocNo = SQLReadString(dr, "isemrino")
                orderdef.Value.DocDate = SQLReadDate(dr, "tarih")
                orderdef.Value.EntityId = CInt(SQLReadDouble(dr, "firmaid")) ' 1045297
                orderdef.Value.ShippingDate = SQLReadDate(dr, "tarih")
                orderdef.Value.SourceApp = UyumsoftSaveWebService.SourceApplication.SatınalmaSiparişi
                orderdef.Value.CurRateTypeId = 235                             ' 235 mb alış , 234 mb satış
                orderdef.Value.CurId = CInt(SQLReadDouble(dr, "dovizid"))      ' 115 eur , gnld_currency / döviz kodları
                orderdef.Value.CurTra = CDec(SQLReadDouble(dr, "kur"))
                orderdef.Value.CountyId = 103 ' Türkiye
                orderdef.Value.CurrencyOption = UyumsoftSaveWebService.CurrencyOption.Belge_Kuru

                If nCase = 1 Then
                    ' sadece F firması için
                    orderdef.Value.CatCode1Id = 1001000001
                End If
            End If
            dr.Close()

            orderDetailDefList = New List(Of UyumsoftSaveWebService.OrderDetailDef)

            cSQL = "select a.isemrino, a.stokno, d.tarih, a.fiyat, a.doviz, b.kdv, a.kur, "

            cSQL = cSQL +
                    " dovizid = e.uyumid, " +
                    " stokid = b.uyumid, " +
                    " birimid = c.uyumid, " +
                    " kur2 = dbo.getkur(a.doviz,d.tarih),  " +
                    " miktar = sum(coalesce(a.miktar1,0)) "

            cSQL = cSQL +
                    " from isemrilines a with (NOLOCK) , " +
                    " stok b with (NOLOCK) , " +
                    " birim c with (NOLOCK) , " +
                    " isemri d with (NOLOCK) , " +
                    " doviz e with (NOLOCK) "

            cSQL = cSQL +
                    " where a.stokno = b.stokno " +
                    " and b.birim1 = c.birim " +
                    " and a.doviz = e.doviz " +
                    " and a.isemrino = d.isemrino " +
                    " and a.isemrino = '" + cIsemriNo + "' " +
                    " and a.miktar1 is not null " +
                    " and a.miktar1 <> 0 " +
                    " group by a.isemrino, a.stokno, d.tarih, a.fiyat, a.doviz, b.uyumid, c.uyumid, e.uyumid, b.kdv, a.kur "

            dr = GetSQLReader(cSQL, ConnYage)

            Do While dr.Read
                nLineNo = nLineNo + 10

                orderDetailDef = New UyumsoftSaveWebService.OrderDetailDef
                orderDetailDef.LineNo = nLineNo
                orderDetailDef.LineType = UyumsoftSaveWebService.LineType.S ' H hizmet
                orderDetailDef.WhouseCode = oUyum.cWhouseCode
                orderDetailDef.DcardId = CInt(SQLReadDouble(dr, "stokid"))
                orderDetailDef.Qty = CDec(SQLReadDouble(dr, "miktar"))
                orderDetailDef.UnitId = CInt(SQLReadDouble(dr, "birimid"))
                orderDetailDef.UnitPriceTra = CDec(SQLReadDouble(dr, "fiyat"))
                If nDocTraID = 1322 Then
                    ' ithalat siparişi KDV siz olur
                Else
                    orderDetailDef.VatCode = CStr(CInt(SQLReadDouble(dr, "kdv")))
                End If

                orderDetailDef.VatStatus = UyumsoftSaveWebService.VatStatus.Hariç
                orderDetailDef.CurRateTypeId = 235      ' 235 mb alış , 234 mb satış
                orderDetailDef.CurTraId = CInt(SQLReadDouble(dr, "dovizid")) ' gnld_currency     / döviz kodları

                If SQLReadString(dr, "doviz") = "TL" Then
                    orderDetailDef.CurRateTra = 1
                ElseIf SQLReadString(dr, "doviz") = "" Then
                    orderDetailDef.CurRateTra = 0
                Else
                    nKur = SQLReadDouble(dr, "kur")
                    If nKur = 0 Then
                        nKur = SQLReadDouble(dr, "kur2")
                    End If
                    orderDetailDef.CurRateTra = CDec(nKur)
                End If

                If nCase = 1 Then
                    ' sadece F firması için
                    orderDetailDef.CatCode1Id = 1001000001
                End If

                orderDetailDefList.Add(orderDetailDef)
            Loop
            dr.Close()

            orderdef.Value.Details = orderDetailDefList.ToArray

            oResult = New UyumsoftSaveWebService.ServiceResultOfBoolean

            oResult = oService.SaveOrderM(orderdef)

            cMessage = oResult.Message

            If IsNumeric(oResult.Message) Then
                ' işemirlerini kilitlemiyoruz - Erkan Bey
                Select Case nCase
                    Case 1 ' F Firması
                        cSQL = "update isemri " +
                                " set uyumid = " + oResult.Message +
                                " where isemrino = '" + cIsemriNo + "' "

                        ExecuteSQLCommandConnected(cSQL, ConnYage)
                    Case 2 ' R Firması
                        cSQL = "update isemri " +
                                " set uyumid2 = " + oResult.Message +
                                " where isemrino = '" + cIsemriNo + "' "

                        ExecuteSQLCommandConnected(cSQL, ConnYage)
                End Select

                UyumSatinalmaSiparisEkle = True

                CreateLog("UyumSatinalmaSiparis", "Uyum Servis : " + oService.Url + vbCrLf + "Isemri -> Uyum bilgi mesaji : " + cIsemriNo + " -> " + oResult.Message)
            Else
                CreateLog("UyumSatinalmaSiparisHata", "Uyum Servis : " + oService.Url + vbCrLf + "Isemri -> Uyum hata mesaji : " + cIsemriNo + " -> " + oResult.Message)
            End If

            ConnYage.Close()

        Catch ex As Exception
            ErrDisp("UyumSatinalmaSiparisEkle", cModuleName,,, ex)
        End Try
    End Function
End Module
