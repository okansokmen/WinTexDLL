Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Module UtilUyumsoftStokFisindenSatisSiparisi

    Const cModuleName As String = "UtilUyumsoftStokFisindenSatisSiparisi"

    Public Function UtilUyumsoftStokFisindenSatisSiparisiEkle(nCase As Integer, cStokFisNo As String, cFilter As String, Optional ByRef cMessage As String = "") As Boolean

        ' nCase = 1  ihrac edilen malzeme için ihracat siparişi fiili üretim firması
        ' nCase = 2  ihrac edilen malzeme için ihracat siparişi resmi ihracat firması

        ' stok hareketinde 07 Satış , mamül OLMAYAN, firma kartında yurt dışı sipariş üret işaretli ler için Uyum da sipariş yaratılır

        Dim oService As UyumsoftSaveWebService.UyumSaveWebService
        Dim oToken As UyumsoftSaveWebService.UyumToken
        Dim oResult As UyumsoftSaveWebService.ServiceResultOfBoolean
        Dim orderdef As UyumsoftSaveWebService.UyumServiceRequestOfOrderDef
        Dim orderDetailDef As UyumsoftSaveWebService.OrderDetailDef
        Dim orderDetailDefList As List(Of UyumsoftSaveWebService.OrderDetailDef)

        Dim cSQL As String = ""
        Dim ConnYage As SqlConnection
        Dim ConnYage2 As SqlConnection
        Dim dr As SqlDataReader
        Dim dr2 As SqlDataReader

        Dim cDepo As String = ""
        Dim nDocTraID As Integer = 0
        Dim lOK As Boolean = False
        Dim cuyumssydgisatis As String = ""
        Dim cuyumssydgdsatis As String = ""
        Dim nSiparisCnt As Integer = 0
        Dim nLineNo As Integer = 0
        Dim cVatCode As String = ""
        Dim cUyumSipIdFieldName As String = ""

        UtilUyumsoftStokFisindenSatisSiparisiEkle = False

        Try

            If cStokFisNo.Trim = "" Then Exit Function

            initUyumServices(nCase)

            Select Case nCase
                Case 1 ' fiili üretim firması
                    cUyumSipIdFieldName = "uyumsipid"
                Case 2 ' resmi ihracat firması
                    cUyumSipIdFieldName = "uyumsipid2"
                    If Not oUyum.lUyumDTFirmaAktif Then
                        Exit Function
                    End If
            End Select

            ConnYage = OpenConn()
            ConnYage2 = OpenConn()

            ' resmi firma var mı 
            If nCase = 2 And GetSysParConnected("UyumDTFirmaAktif", ConnYage, "0") <> "1" Then
                ConnYage.Close()
                ConnYage2.Close()
                Exit Function
            End If

            cuyumssydgisatis = GetSysParConnected("uyumssydgisatis", ConnYage, "2882")       ' YURT DIŞI GRUP İÇİ SATIŞ SİPARİŞİ
            cuyumssydgdsatis = GetSysParConnected("uyumssydgdsatis", ConnYage, "2883")       ' YURT DIŞI GRUP DIŞI SATIŞ SİPARİŞİ
            cDepo = GetSysParConnected("UyumIhracatDepo", ConnYage, "MERKEZ")

            ' yurtdışı satış siparişleri açılacak mı
            cSQL = "SELECT TOP 1 d.grupfirmasi "

            cSQL = cSQL +
                    " from stokfis a WITH (NOLOCK) , " +
                    " stokfislines b WITH (NOLOCK) , " +
                    " stok c WITH (NOLOCK) , " +
                    " firma d WITH (NOLOCK) "

            cSQL = cSQL +
                    " WHERE a.stokfisno = b.stokfisno " +
                    " And b.stokno = c.stokno " +
                    " And a.firma = d.firma " +
                    " And b.stokhareketkodu = '07 Satis' " +
                    " AND c.anastokgrubu <> 'MAMUL' " +
                    " AND d.uyumyurtdisisiparis = 'E' " +
                    " AND a.stokfisno = '" + cStokFisNo.Trim + "' " +
                    cFilter

            dr = GetSQLReader(cSQL, ConnYage)

            If dr.Read Then
                If SQLReadString(dr, "grupfirmasi") = "E" Then
                    nDocTraID = CInt(cuyumssydgisatis) ' YURT DIŞI GRUP İÇİ SATIŞ SİPARİŞİ
                Else
                    nDocTraID = CInt(cuyumssydgdsatis) ' YURT DIŞI GRUP DIŞI SATIŞ SİPARİŞİ
                End If
            Else
                dr.Close()
                ConnYage.Close()
                'cMessage = "Stok fişi alınan siparişi açmaya uygun değil"
                Exit Function
            End If
            dr.Close()

            oService = New UyumsoftSaveWebService.UyumSaveWebService
            oService.Timeout = 300000

            oService.Url = oUyum.cURLUyumSaveWebService


            cSQL = "SELECT a.stokfisno, a.fistarihi, a.firma, b.aktardoviz, " +
                    " kur = max(b.aktarkur), "

            cSQL = cSQL +
                    " firmaid = d.uyumid, " +
                    " dovizid = g.uyumid "

            cSQL = cSQL +
                    " from stokfis a WITH (NOLOCK) , " +
                    " stokfislines b WITH (NOLOCK) , " +
                    " stok c WITH (NOLOCK) , " +
                    " firma d WITH (NOLOCK) , " +
                    " doviz g with (NOLOCK) "

            cSQL = cSQL +
                    " WHERE a.stokfisno = b.stokfisno " +
                    " And a.firma = d.firma " +
                    " And b.stokno = c.stokno " +
                    " And b.aktardoviz = g.doviz " +
                    " And b.stokhareketkodu = '07 Satis' " +
                    " AND c.anastokgrubu <> 'MAMUL' " +
                    " AND d.uyumyurtdisisiparis = 'E' " +
                    " AND a.stokfisno = '" + cStokFisNo.Trim + "' " +
                    cFilter

            cSQL = cSQL +
                    " group by a.stokfisno, a.fistarihi, a.firma, b.aktardoviz, d.uyumid, g.uyumid " +
                    " order by a.stokfisno, a.fistarihi, a.firma, b.aktardoviz "

            dr = GetSQLReader(cSQL, ConnYage)

            Do While dr.Read

                nSiparisCnt = nSiparisCnt + 1

                oToken = New UyumsoftSaveWebService.UyumToken

                oToken.UserName = oUyum.cUserName
                oToken.Password = oUyum.cPassword

                orderdef = New UyumsoftSaveWebService.UyumServiceRequestOfOrderDef

                orderdef.Token = oToken

                orderdef.Value = New UyumsoftSaveWebService.OrderDef

                orderdef.Value.DocTraId = nDocTraID
                orderdef.Value.CoCode = oUyum.cCoCode
                orderdef.Value.BranchCode = oUyum.cBranchCode
                orderdef.Value.DocNo = "SPR-" + SQLReadString(dr, "stokfisno") + "-" + nSiparisCnt.ToString
                orderdef.Value.DocDate = SQLReadDate(dr, "fistarihi")
                orderdef.Value.EntityId = CInt(SQLReadDouble(dr, "firmaid")) ' 1045297
                orderdef.Value.ShippingDate = SQLReadDate(dr, "fistarihi")
                orderdef.Value.SourceApp = UyumsoftSaveWebService.SourceApplication.SatışSiparişi
                orderdef.Value.CurRateTypeId = 235                             ' 235 mb alış , 234 mb satış
                orderdef.Value.CurId = CInt(SQLReadDouble(dr, "dovizid"))      ' 115 eur , gnld_currency / döviz kodları
                orderdef.Value.CurTra = CDec(SQLReadDouble(dr, "kur"))
                orderdef.Value.CatCode1Id = 1001000001
                orderdef.Value.CountyId = 103
                orderdef.Value.CurrencyOption = UyumsoftSaveWebService.CurrencyOption.Belge_Kuru

                nLineNo = 0
                orderDetailDefList = New List(Of UyumsoftSaveWebService.OrderDetailDef)

                cSQL = "SELECT a.stokfisno, a.firma, d.grupfirmasi, c.kompozisyon, c.cinsaciklamasi, c.anastokgrubu, c.stoktipi, " +
                        " b.aktarfiyat, b.aktardoviz, "

                cSQL = cSQL +
                        " miktar = SUM(COALESCE(b.netmiktar1,0)), " +
                        " kur = max(b.aktarkur), "

                cSQL = cSQL +
                        " stokid = c.uyumid, " +
                        " firmaid = d.uyumid, " +
                        " birimid = e.uyumid, " +
                        " stoktipiid = f.uyumid, " +
                        " dovizid = g.uyumid, " +
                        " gtipid = (SELECT TOP 1 uyumid FROM gtip with (NOLOCK) WHERE gtip = c.gtip), " +
                        " kdvid = (select top 1 uyumid from kdvgroup with (NOLOCK) where oran = c.kdv), " +
                        " muhasebeid = f.uyummuhsablonid  "

                cSQL = cSQL +
                        " from stokfis a WITH (NOLOCK) , " +
                        " stokfislines b WITH (NOLOCK) , " +
                        " stok c WITH (NOLOCK) , " +
                        " firma d WITH (NOLOCK) , " +
                        " birim e WITH (NOLOCK) , " +
                        " stoktipi f WITH (NOLOCK) , " +
                        " doviz g with (NOLOCK) "


                cSQL = cSQL +
                        " WHERE a.stokfisno = b.stokfisno " +
                        " And b.stokno = c.stokno " +
                        " And a.firma = d.firma " +
                        " And c.birim1 = e.birim " +
                        " And c.stoktipi = f.kod  " +
                        " And b.aktardoviz = g.doviz " +
                        " And b.stokhareketkodu = '07 Satis' " +
                        " And c.anastokgrubu <> 'MAMUL' " +
                        " And d.uyumyurtdisisiparis = 'E' " +
                        " And a.stokfisno = '" + cStokFisNo.Trim + "' " +
                        " And b.aktardoviz = '" + SQLReadString(dr, "aktardoviz") + "' " +
                        cFilter

                cSQL = cSQL +
                        " GROUP BY a.stokfisno, a.firma, d.grupfirmasi, c.kompozisyon, c.cinsaciklamasi, c.anastokgrubu, c.stoktipi, " +
                        " b.aktarfiyat, b.aktardoviz, " +
                        " c.uyumid, d.uyumid, e.uyumid, f.uyumid, c.gtip, c.kdv, f.uyummuhsablonid, g.uyumid "

                dr2 = GetSQLReader(cSQL, ConnYage2)

                Do While dr2.Read
                    nLineNo = nLineNo + 10

                    orderDetailDef = New UyumsoftSaveWebService.OrderDetailDef

                    orderDetailDef.LineNo = nLineNo
                    orderDetailDef.LineType = UyumsoftSaveWebService.LineType.S ' H hizmet
                    orderDetailDef.WhouseCode = cDepo
                    orderDetailDef.DcardId = CInt(SQLReadDouble(dr2, "stokid"))
                    orderDetailDef.Qty = CDec(SQLReadDouble(dr2, "miktar"))
                    orderDetailDef.UnitId = CInt(SQLReadDouble(dr2, "birimid"))
                    orderDetailDef.UnitPriceTra = CDec(SQLReadDouble(dr2, "aktarfiyat"))

                    orderDetailDef.CurRateTypeId = 235      ' 235 mb alış , 234 mb satış
                    orderDetailDef.CurTraId = CInt(SQLReadDouble(dr2, "dovizid")) ' gnld_currency     / döviz kodları
                    orderDetailDef.CurRateTra = CDec(SQLReadDouble(dr2, "kur"))
                    orderDetailDef.CatCode1Id = 1001000001  ' kopyalanarak aktarılmayacak
                    orderDetailDef.VatStatus = UyumsoftSaveWebService.VatStatus.Hariç

                    orderDetailDefList.Add(orderDetailDef)
                Loop
                dr2.Close()

                orderdef.Value.Details = orderDetailDefList.ToArray

                oResult = New UyumsoftSaveWebService.ServiceResultOfBoolean

                oResult = oService.SaveOrderM(orderdef)

                cMessage = oResult.Message

                If IsNumeric(oResult.Message) Then

                    cSQL = "set dateformat dmy " +
                            " UPDATE stokfislines " +
                            " set " + cUyumSipIdFieldName + " = " + oResult.Message +
                            " where stokfisno = '" + cStokFisNo + "' " +
                            " and stokhareketno in (SELECT b.stokhareketno  " +
                                                " from stokfis a WITH (NOLOCK) , " +
                                                " stokfislines b WITH (NOLOCK) , " +
                                                " stok c WITH (NOLOCK) , " +
                                                " firma d WITH (NOLOCK) , " +
                                                " birim e WITH (NOLOCK) , " +
                                                " stoktipi f WITH (NOLOCK) , " +
                                                " doviz g with (NOLOCK) " +
                                                " WHERE a.stokfisno = b.stokfisno " +
                                                " And b.stokno = c.stokno " +
                                                " And a.firma = d.firma " +
                                                " And c.birim1 = e.birim " +
                                                " And c.stoktipi = f.kod  " +
                                                " And b.aktardoviz = g.doviz " +
                                                " And b.stokhareketkodu = '07 Satis'  " +
                                                " And c.anastokgrubu <> 'MAMUL' " +
                                                " And d.uyumyurtdisisiparis = 'E' " +
                                                " And a.stokfisno = '" + cStokFisNo + "' " +
                                                " And b.aktardoviz = '" + SQLReadString(dr, "aktardoviz") + "' " +
                                                cFilter + " ) "

                    ExecuteSQLCommandConnected(cSQL, ConnYage2)

                    CreateLog("UyumStokFisindenSiparis", "Uyum Servis : " + oService.Url + vbCrLf +
                                                        cUyumSipIdFieldName + " " + cStokFisNo + " -> " + oResult.Message)
                Else
                    CreateLog("UyumStokFisindenSiparisHata", "Uyum Servis : " + oService.Url + vbCrLf +
                                                        cUyumSipIdFieldName + " " + cStokFisNo + " -> " + oResult.Message)
                End If
            Loop
            dr.Close()

            ConnYage2.Close()
            ConnYage.Close()

            UtilUyumsoftStokFisindenSatisSiparisiEkle = True

        Catch ex As Exception
            ErrDisp("UtilUyumsoftStokFisindenSatisSiparisiEkle", cModuleName,,, ex)
        End Try

    End Function

End Module
