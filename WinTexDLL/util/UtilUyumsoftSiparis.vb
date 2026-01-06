Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Module UtilUyumsoftSiparis

    Const cModuleName As String = "UtilUyumsoftSiparis"

    Public Function UyumUretimSiparisEkle(ByVal nCase As Integer, ByVal cSiparisNo As String, Optional ByRef cMessage As String = "") As Boolean
        ' nCase = 1 Fiili üretim firmaya sipariş ekle
        ' nCase = 2 Resmi ihracat firmasına sipariş ekle
        Dim oService As UyumsoftSaveWebService.UyumSaveWebService
        Dim oToken As UyumsoftSaveWebService.UyumToken
        Dim oResult As UyumsoftSaveWebService.ServiceResultOfBoolean
        Dim orderdef As UyumsoftSaveWebService.UyumServiceRequestOfOrderDef
        Dim orderDetailDef As UyumsoftSaveWebService.OrderDetailDef
        Dim orderDetailDefList As List(Of UyumsoftSaveWebService.OrderDetailDef)

        Dim cSiparisNo2 As String = ""
        Dim cSQL As String = ""
        Dim ConnYage As SqlConnection
        Dim dr As SqlDataReader
        Dim lEklendi As Boolean = False
        Dim cStokNo As String = ""
        Dim cAciklama As String = ""
        Dim cUyumBirimID As String = ""
        Dim cUyumStokID As String = ""
        Dim cUyumStokTipiID As String = ""
        Dim cStokTipi As String = ""
        Dim nLineNo As Integer = 0
        Dim nDocTraID As Integer = 2883
        Dim nUyumSiparisID As Double = 0
        Dim nUyumSiparisID2 As Double = 0
        Dim cAnaStokGrubu As String = ""
        Dim cGtipID As String = ""
        Dim cKdvID As String = ""
        Dim cMuhasebeID As String = ""
        Dim cMessage2 As String = ""
        Dim cAciklama2 As String = ""
        Dim cVatCode As String = ""
        Dim nVatID As Double = 0
        Dim cDepo As String = "MERKEZ"
        Dim lGrupFirmasi As Boolean = False
        Dim lYurtDisiMusteri As Boolean = False
        Dim lYurtIci As Boolean = False
        Dim cUyumssyigisatis As String = "2880"
        Dim cUyumssyigdsatis As String = "2881"
        Dim cUyumssydgisatis As String = "2882"
        Dim cUyumssydgdsatis As String = "2883"

        UyumUretimSiparisEkle = False
        cMessage = ""

        Try
            ConnYage = OpenConn()

            ' resmi firma var mı 
            If nCase = 2 And GetSysParConnected("UyumDTFirmaAktif", ConnYage, "0") <> "1" Then
                ConnYage.Close()
                Exit Function
            End If

            cDepo = GetSysParConnected("UyumIhracatDepo", ConnYage, "MERKEZ")
            cUyumssyigisatis = GetSysParConnected("uyumssyigisatis", ConnYage, "2880")       ' YURT İÇİ GRUP İÇİ SATIŞ SİPARİŞİ
            cUyumssyigdsatis = GetSysParConnected("uyumssyigdsatis", ConnYage, "2881")       ' YURT İÇİ GRUP DIŞI SATIŞ SİPARİŞİ
            cUyumssydgisatis = GetSysParConnected("uyumssydgisatis", ConnYage, "2882")       ' YURT DIŞI GRUP İÇİ SATIŞ SİPARİŞİ
            cUyumssydgdsatis = GetSysParConnected("uyumssydgdsatis", ConnYage, "2883")       ' YURT DIŞI GRUP DIŞI SATIŞ SİPARİŞİ


            cSQL = "Select top 1 a.uyumsiparisid, a.uyumsiparisid2, a.yurtici, b.yurtdisimusteri, b.grupfirmasi " +
                    " from siparis a With (NOLOCK) , firma b With (NOLOCK) " +
                    " where a.musterino = b.firma " +
                    " And a.kullanicisipno = '" + cSiparisNo.Trim + "' "

            dr = GetSQLReader(cSQL, ConnYage)

            If dr.Read Then
                nUyumSiparisID = SQLReadDouble(dr, "uyumsiparisid")
                nUyumSiparisID2 = SQLReadDouble(dr, "uyumsiparisid2")
                If SQLReadString(dr, "grupfirmasi") = "E" Then
                    lGrupFirmasi = True
                    lYurtIci = True
                End If
                If SQLReadString(dr, "yurtdisimusteri") = "E" Then
                    lYurtDisiMusteri = True
                End If
                If SQLReadString(dr, "yurtici") = "E" Then
                    lYurtIci = True
                End If
            End If
            dr.Close()

            Select Case nCase
                Case 1 ' Fiili üretim firması siparişi
                    If nUyumSiparisID <> 0 Then
                        cMessage = nUyumSiparisID.ToString
                        UyumUretimSiparisEkle = True
                        Exit Function
                    End If
                    ' fiili üretim firması
                    initUyumServices(1)

                Case 2 ' Resmi firması siparişi
                    If nUyumSiparisID2 <> 0 Then
                        cMessage = nUyumSiparisID2.ToString
                        UyumUretimSiparisEkle = True
                        Exit Function
                    End If

                    If lGrupFirmasi Then
                        ' resmi üretim firması (R firması yurt içi / grup içi satışı)                        
                        initUyumServices(3)
                    Else
                        ' resmi ihracat firması (R firması yurt dışı / grup dışı satışı)
                        initUyumServices(2)
                    End If
            End Select

            If nCase = 1 Then
                ' uyumda açılmamış stok kartı varsa aç
                cSQL = "select distinct a.stokno, a.cinsaciklamasi, a.birim1, a.stoktipi, a.anastokgrubu, "

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
                        " gtipid = (select top 1 uyumid " +
                                    " from gtip with (NOLOCK) " +
                                    " where gtip = a.gtip), " +
                        " kdvid = (select top 1 uyumid " +
                                    " from kdvgroup with (NOLOCK) " +
                                    " where oran = a.kdv ), " +
                        " muhasebeid = c.uyummuhsablonid "

                cSQL = cSQL +
                        " from stok a with (NOLOCK) , " +
                        " birim b with (NOLOCK) , " +
                        " stoktipi c with (NOLOCK) , " +
                        " sipmodel d with (NOLOCK) "

                cSQL = cSQL +
                        " where a.birim1 = b.birim " +
                        " And a.stoktipi = c.kod " +
                        " And a.stokno = d.modelno " +
                        " And a.stokno Is Not null " +
                        " And a.stokno <> '' " +
                        " And (a.uyumid is null or a.uyumid = 0) " +
                        " And b.uyumid is not null " +
                        " And b.uyumid <> 0 " +
                        " And c.uyumid is not null " +
                        " And c.uyumid <> 0 " +
                        " And d.siparisno = '" + cSiparisNo + "' "

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
                    ' sadece fiili firmaya stok kartı ekleniyor
                    ' diğer firmalara otomatik eklenmez
                    cUyumStokID = UyumStokInsert(cStokNo, cAciklama, cUyumBirimID, cUyumStokTipiID, cAnaStokGrubu, cMessage2, cGtipID, cKdvID, cMuhasebeID, cAciklama2, lEklendi)

                    If Not lEklendi Then
                        If cMessage2.Trim = "" Then
                            cMessage = cMessage2
                        Else
                            cMessage = cMessage + " , " + cMessage2
                        End If
                    End If
                Loop
                dr.Close()

                If cMessage.Trim <> "" Then
                    ConnYage.Close()
                    Exit Function
                End If
            End If

            cVatCode = "0"

            If lGrupFirmasi Then
                nDocTraID = CInt(cUyumssyigisatis)
                cVatCode = "8"
            Else
                If lYurtIci Then
                    nDocTraID = CInt(cUyumssyigdsatis)
                Else
                    nDocTraID = CInt(cUyumssydgdsatis)
                End If

                If Not lYurtDisiMusteri Then
                    cVatCode = "8"
                End If

                If lYurtIci Then
                    cVatCode = "8"
                End If
            End If

            cSQL = "select uyumid " +
                    " from kdvgroup with (NOLOCK) " +
                    " where kodu = '" + cVatCode + "' "

            nVatID = ReadSingleDoubleValueConnected(cSQL, ConnYage)

            oService = New UyumsoftSaveWebService.UyumSaveWebService
            oService.Timeout = 300000

            oService.Url = oUyum.cURLUyumSaveWebService

            oToken = New UyumsoftSaveWebService.UyumToken

            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            orderdef = New UyumsoftSaveWebService.UyumServiceRequestOfOrderDef
            orderdef.Token = oToken
            orderdef.Value = New UyumsoftSaveWebService.OrderDef

            cSQL = "select a.kullanicisipno, a.musterino, a.siparistarihi, a.ilksevktarihi, " +
                    " firmaid = b.uyumid, "

            cSQL = cSQL +
                    " dovizid = (SELECT TOP 1 y.uyumid " +
                                " From sipfiyat x with (NOLOCK)  , doviz y with (NOLOCK)  " +
                                " Where x.satisdoviz = y.doviz " +
                                " And x.siparisno = a.kullanicisipno " +
                                " And y.uyumid Is Not null " +
                                " And y.uyumid <> 0), "
            cSQL = cSQL +
                    " kur = (SELECT TOP 1 dbo.getkur(x.satisdoviz,a.siparistarihi)  " +
                                " From sipfiyat x with (NOLOCK)  , doviz y with (NOLOCK)  " +
                                " Where x.satisdoviz = y.doviz " +
                                " And x.siparisno = a.kullanicisipno " +
                                " And y.uyumid is not null " +
                                " And y.uyumid <> 0) "
            cSQL = cSQL +
                    " from siparis a with (NOLOCK) , firma b with (NOLOCK)  " +
                    " where a.musterino = b.firma " +
                    " and a.kullanicisipno = '" + cSiparisNo.Trim + "' "

            dr = GetSQLReader(cSQL, ConnYage)

            If dr.Read Then

                cSiparisNo2 = SQLReadString(dr, "kullanicisipno")
                cSiparisNo2 = Replace(cSiparisNo2, " ", "")
                cSiparisNo2 = Left(cSiparisNo2, 15)
                cSiparisNo2 = cSiparisNo2.Trim

                orderdef.Value.DocTraId = nDocTraID
                orderdef.Value.CoCode = oUyum.cCoCode
                orderdef.Value.BranchCode = oUyum.cBranchCode
                orderdef.Value.DocNo = cSiparisNo2
                orderdef.Value.DocDate = SQLReadDate(dr, "siparistarihi")
                orderdef.Value.EntityId = CInt(SQLReadDouble(dr, "firmaid")) ' 1045297
                orderdef.Value.ShippingDate = SQLReadDate(dr, "ilksevktarihi")
                orderdef.Value.SourceApp = UyumsoftSaveWebService.SourceApplication.SatışSiparişi
                orderdef.Value.CurRateTypeId = 235                             ' 235 mb alış , 234 mb satış
                orderdef.Value.CurId = CInt(SQLReadDouble(dr, "dovizid"))      ' 115 eur , gnld_currency / döviz kodları
                orderdef.Value.CurTra = CDec(SQLReadDouble(dr, "kur"))
                orderdef.Value.CountyId = 103
                orderdef.Value.CurrencyOption = UyumsoftSaveWebService.CurrencyOption.Belge_Kuru

                If nCase = 1 Then
                    ' sadece F firmasında
                    orderdef.Value.CatCode1Id = 1001000001
                End If
            End If
            dr.Close()

            orderDetailDefList = New List(Of UyumsoftSaveWebService.OrderDetailDef)

            cSQL = "select a.siparisno, a.modelno, d.siparistarihi, c.uyumid, "

            cSQL = cSQL +
                    " dovizid = (SELECT TOP 1 y.uyumid " +
                                " From sipfiyat x with (NOLOCK)  , doviz y with (NOLOCK)  " +
                                " Where x.satisdoviz = y.doviz " +
                                " And x.siparisno = a.siparisno " +
                                " And y.uyumid Is Not null " +
                                " And y.uyumid <> 0), "
            cSQL = cSQL +
                    " kur = (SELECT TOP 1 dbo.getkur(x.satisdoviz,d.siparistarihi)  " +
                                " From sipfiyat x with (NOLOCK)  , doviz y with (NOLOCK)  " +
                                " Where x.satisdoviz = y.doviz " +
                                " And x.siparisno = a.siparisno " +
                                " And y.uyumid is not null " +
                                " And y.uyumid <> 0), "
            cSQL = cSQL +
                    " fiyat = (SELECT TOP 1 x.satisfiyati " +
                                " From sipfiyat x with (NOLOCK)  , doviz y with (NOLOCK)  " +
                                " Where x.satisdoviz = y.doviz " +
                                " And x.siparisno = a.siparisno " +
                                " And y.uyumid is not null " +
                                " And y.uyumid <> 0), "
            cSQL = cSQL +
                    " stokid = b.uyumid, " +
                    " birimid = c.uyumid, " +
                    " adet = sum(coalesce(a.adet,0)) "

            cSQL = cSQL +
                    " from sipmodel a with (NOLOCK) , " +
                    " stok b with (NOLOCK) , " +
                    " birim c with (NOLOCK) , " +
                    " siparis d with (NOLOCK) "

            cSQL = cSQL +
                    " where a.modelno = b.stokno " +
                    " And b.birim1 = c.birim " +
                    " And a.siparisno = d.kullanicisipno " +
                    " And a.siparisno = '" + cSiparisNo.Trim + "' " +
                    " group by a.siparisno, a.modelno, d.siparistarihi, c.uyumid, b.uyumid "

            dr = GetSQLReader(cSQL, ConnYage)

            Do While dr.Read
                nLineNo = nLineNo + 10

                orderDetailDef = New UyumsoftSaveWebService.OrderDetailDef

                orderDetailDef.LineNo = nLineNo
                orderDetailDef.LineType = UyumsoftSaveWebService.LineType.S ' H hizmet
                orderDetailDef.WhouseCode = cDepo
                orderDetailDef.DcardId = CInt(SQLReadDouble(dr, "stokid"))
                orderDetailDef.Qty = CDec(SQLReadDouble(dr, "adet"))
                orderDetailDef.UnitId = CInt(SQLReadDouble(dr, "birimid"))
                orderDetailDef.UnitPriceTra = CDec(SQLReadDouble(dr, "fiyat"))

                orderDetailDef.CurRateTypeId = 235 ' 235 mb alış , 234 mb satış
                orderDetailDef.CurTraId = CInt(SQLReadDouble(dr, "dovizid")) ' gnld_currency     / döviz kodları
                orderDetailDef.CurRateTra = CDec(SQLReadDouble(dr, "kur"))

                orderDetailDef.VatStatus = UyumsoftSaveWebService.VatStatus.Hariç
                orderDetailDef.VatId = CInt(nVatID)

                'If cVatCode.Trim <> "" Then
                '    orderDetailDef.VatCode = cVatCode
                'End If

                If nCase = 1 Then
                    ' sadece F firmasında
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

                Select Case nCase
                    Case 1
                        ' F firma
                        cSQL = "update siparis " +
                                " set uyumsiparisid = " + oResult.Message +
                                " where kullanicisipno = '" + cSiparisNo.Trim + "' "

                        ExecuteSQLCommandConnected(cSQL, ConnYage)
                    Case 2, 3
                        ' R firması
                        cSQL = "update siparis " +
                                " set uyumsiparisid2 = " + oResult.Message +
                                " where kullanicisipno = '" + cSiparisNo.Trim + "' "

                        ExecuteSQLCommandConnected(cSQL, ConnYage)
                End Select

                UyumUretimSiparisEkle = True

                CreateLog("UyumSiparis", "Uyum Servis : " + oService.Url + vbCrLf + "Siparis -> Uyum bilgi mesaji : " + cSiparisNo + " -> " + oResult.Message)
            Else
                CreateLog("UyumSiparisHata", "Uyum Servis : " + oService.Url + vbCrLf + "Siparis -> Uyum hata mesaji : " + cSiparisNo + " -> " + oResult.Message)
            End If

            ConnYage.Close()

        Catch ex As Exception
            ErrDisp("UyumUretimSiparisEkle", cModuleName,,, ex)
        End Try
    End Function

End Module
