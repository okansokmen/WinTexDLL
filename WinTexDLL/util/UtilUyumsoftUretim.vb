Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Module UtilUyumsoftUretim

    Const cModuleName As String = "UtilUyumsoftUretim"

    Dim cGUID As String = ""

    Private Structure oMlz
        Dim cStokNo As String
        Dim nStokid As Double
        Dim nBirimid As Double
        Dim nMiktar As Double
        Dim nStokTipiid As Double
    End Structure

    Public Function UyumUretimEkle(cUretFisNo As String, Optional ByRef cMessage As String = "") As Boolean
        ' sadece F (fiili) firmaya gönderilecek
        ' R (resmi) firma üretim yapmayacak
        UyumUretimEkle = False

        Try
            Dim oService As UyumErogluProduction.ErogluProduction
            Dim oToken As UyumErogluProduction.UyumToken
            Dim InParam As UyumErogluProduction.UyumServiceRequestOfPrdWorderAcOpInParam
            Dim oResult As UyumErogluProduction.ServiceResultOfInt32
            Dim PrdBomDList As List(Of UyumErogluProduction.WorderAcBomDMaterialPrm)
            Dim pPrdBomDParam As UyumErogluProduction.WorderAcBomDMaterialPrm

            Dim cUTF As String = ""
            Dim dTarih As Date = #1/1/1950#
            Dim nWOrderMid As Double = 0
            Dim nOperationid As Double = 0
            Dim nWorkstationid As Double = 0
            Dim nStokid As Double = 0
            Dim nBirimid As Double = 0
            Dim cReceteNo As String = ""
            Dim cModelNo As String = ""
            Dim cRenk As String = ""
            Dim cBeden As String = ""
            Dim cDept As String = ""
            Dim nAdet As Double = 0
            Dim nMiktar As Double = 0
            Dim nCnt As Integer = 0
            Dim nUretimAdedi As Double = 0
            Dim nFound As Integer = 0
            Dim aMlz() As oMlz

            Dim cSQL As String = ""
            Dim ConnYage As SqlConnection
            Dim ConnYage2 As SqlConnection
            Dim ConnYage3 As SqlConnection
            Dim dr As SqlDataReader
            Dim dr2 As SqlDataReader
            Dim dr3 As SqlDataReader

            initUyumServices()

            oService = New UyumErogluProduction.ErogluProduction
            oService.Timeout = 300000

            oService.Url = oUyum.cURLUyumErogluProduction

            ConnYage = OpenConn()

            cSQL = "select distinct a.fistarihi, a.cikisdept, a.cikisfirm_atl, b.uretimtakipno, "

            cSQL = cSQL +
                    " wordermid = (select top 1 uyumisemriid " +
                                " from siparis x with (NOLOCK) , sipmodel y with (NOLOCK) " +
                                " where x.kullanicisipno = y.siparisno " +
                                " and y.uretimtakipno = b.uretimtakipno), "

            cSQL = cSQL +
                    " operationid = c.uyumid, " +
                    " workstationid = c.uyumwsid "

            cSQL = cSQL +
                    " from uretharfis a with (NOLOCK) , " +
                    " uretharfislines b with (NOLOCK) , " +
                    " departman c with (NOLOCK) "

            cSQL = cSQL +
                    " where a.uretfisno = b.uretfisno " +
                    " and a.uretfisno = '" + cUretFisNo.Trim + "'  " +
                    " And a.cikisdept = c.departman " +
                    " and b.uretimtakipno is not null " +
                    " and b.uretimtakipno <> '' "

            dr = GetSQLReader(cSQL, ConnYage)

            Do While dr.Read

                dTarih = SQLReadDate(dr, "fistarihi")
                cUTF = SQLReadString(dr, "uretimtakipno")
                cDept = SQLReadString(dr, "cikisdept")
                nWOrderMid = SQLReadDouble(dr, "wordermid")
                nOperationid = SQLReadDouble(dr, "operationid")
                nWorkstationid = SQLReadDouble(dr, "workstationid")
                nUretimAdedi = 0

                nCnt = -1
                nFound = -1
                ReDim aMlz(0)

                PrdBomDList = New List(Of UyumErogluProduction.WorderAcBomDMaterialPrm)

                ConnYage2 = OpenConn()

                cSQL = "select b.uretimtakipno, c.modelno, c.renk, c.beden, " +
                        " adet = sum(coalesce(c.adet,0)), "

                cSQL = cSQL +
                        " receteno = (select top 1 receteno " +
                                    " from sipmodel with (NOLOCK) " +
                                    " where uretimtakipno = b.uretimtakipno " +
                                    " and modelno = c.modelno " +
                                    " and renk = c.renk " +
                                    " and beden = c.beden) "

                cSQL = cSQL +
                        " from uretharfis a with (NOLOCK) , " +
                        " uretharfislines b with (NOLOCK) , " +
                        " uretharrba c with (NOLOCK)  "

                cSQL = cSQL +
                        " where a.uretfisno = b.uretfisno " +
                        " And b.ulineno = c.ulineno " +
                        " And c.adet Is Not null " +
                        " And c.adet <> 0 " +
                        " And a.uretfisno = '" + cUretFisNo.Trim + "'  " +
                        " And b.uretimtakipno = '" + cUTF.Trim + "' " +
                        " group by b.uretimtakipno, c.modelno, c.renk, c.beden " +
                        " order by b.uretimtakipno, c.modelno, c.renk, c.beden "

                dr2 = GetSQLReader(cSQL, ConnYage2)

                Do While dr2.Read

                    cReceteNo = SQLReadString(dr2, "receteno")
                    cModelNo = SQLReadString(dr2, "modelno")
                    cRenk = SQLReadString(dr2, "renk")
                    cBeden = SQLReadString(dr2, "beden")
                    nAdet = SQLReadDouble(dr2, "adet")
                    nUretimAdedi = nUretimAdedi + nAdet

                    ConnYage3 = OpenConn()

                    If cReceteNo = "" Then
                        cSQL = "select a.hammaddekodu, a.hammadderenk, a.hammaddebeden, a.kullanimmiktari, a.fire, a.hesaplama, a.stokkadarcik, b.maltakipesasi, " +
                                " stokid = b.uyumid, " +
                                " birimid = c.uyumid, " +
                                " stoktipiid = d.uyumid "

                        cSQL = cSQL +
                                " from modelhammadde a with (NOLOCK) , " +
                                " stok b with (NOLOCK) , " +
                                " birim c with (NOLOCK) , " +
                                " stoktipi d with (NoLOCK) "

                        cSQL = cSQL +
                                " where a.hammaddekodu = b.stokno " +
                                " And b.birim1 = c.birim " +
                                " ANd b.stoktipi = d.kod " +
                                " And a.modelno = '" + cModelNo + "' " +
                                " and a.uretimdepartmani = '" + cDept + "' " +
                                " and (a.modelrenk = 'HEPSI' or a.modelrenk = '" + cRenk + "') " +
                                " and (a.modelbeden = 'HEPSI' or a.modelbeden = '" + cBeden + "') "
                    Else
                        cSQL = "select a.hammaddekodu, a.hammadderenk, a.hammaddebeden, a.kullanimmiktari, a.fire, a.hesaplama, a.stokkadarcik, b.maltakipesasi, " +
                                " stokid = b.uyumid, " +
                                " birimid = c.uyumid, " +
                                " stoktipiid = d.uyumid "

                        cSQL = cSQL +
                                " from modelhammadde a with (NOLOCK) , " +
                                " stok b with (NOLOCK) , " +
                                " birim c with (NOLOCK) , " +
                                " stoktipi d with (NoLOCK) "

                        cSQL = cSQL +
                                " where a.hammaddekodu = b.stokno " +
                                " And b.birim1 = c.birim " +
                                " ANd b.stoktipi = d.kod " +
                                " and a.receteno = '" + cReceteNo + "' " +
                                " And a.modelno = '" + cModelNo + "' " +
                                " and a.uretimdepartmani = '" + cDept + "' " +
                                " and (a.modelrenk = 'HEPSI' or a.modelrenk = '" + cRenk + "') " +
                                " and (a.modelbeden = 'HEPSI' or a.modelbeden = '" + cBeden + "') "
                    End If

                    dr3 = GetSQLReader(cSQL, ConnYage3)

                    Do While dr3.Read

                        nMiktar = nAdet * SQLReadDouble(dr3, "kullanimmiktari")

                        If aMlz(0).cStokNo = "" Then
                            aMlz(0).cStokNo = SQLReadString(dr3, "hammaddekodu")
                            aMlz(0).nMiktar = nMiktar
                            aMlz(0).nStokid = SQLReadDouble(dr3, "stokid")
                            aMlz(0).nBirimid = SQLReadDouble(dr3, "birimid")
                            aMlz(0).nStokTipiid = SQLReadDouble(dr3, "stoktipiid")
                        Else
                            nFound = -1
                            For nCnt = 0 To UBound(aMlz)
                                If aMlz(nCnt).cStokNo = SQLReadString(dr3, "hammaddekodu") Then
                                    nFound = nCnt
                                    Exit For
                                End If
                            Next
                            If nFound = -1 Then
                                nCnt = UBound(aMlz) + 1
                                ReDim Preserve aMlz(nCnt)

                                aMlz(nCnt).cStokNo = SQLReadString(dr3, "hammaddekodu")
                                aMlz(nCnt).nMiktar = nMiktar
                                aMlz(nCnt).nStokid = SQLReadDouble(dr3, "stokid")
                                aMlz(nCnt).nBirimid = SQLReadDouble(dr3, "birimid")
                                aMlz(nCnt).nStokTipiid = SQLReadDouble(dr3, "stoktipiid")
                            Else
                                aMlz(nCnt).nMiktar = aMlz(nCnt).nMiktar + nMiktar
                            End If
                        End If
                    Loop
                    dr3.Close()
                    ConnYage3.Close()

                    For nCnt = 0 To UBound(aMlz)
                        pPrdBomDParam = New UyumErogluProduction.WorderAcBomDMaterialPrm
                        pPrdBomDParam.ItemId = CInt(aMlz(nCnt).nStokid)
                        pPrdBomDParam.UnitId = CInt(aMlz(nCnt).nBirimid)
                        pPrdBomDParam.Qty = CDec(aMlz(nCnt).nMiktar)
                        PrdBomDList.Add(pPrdBomDParam)
                    Next
                Loop
                dr2.Close()
                ConnYage2.Close()

                oToken = New UyumErogluProduction.UyumToken
                oToken.UserName = oUyum.cUserName
                oToken.Password = oUyum.cPassword

                InParam = New UyumErogluProduction.UyumServiceRequestOfPrdWorderAcOpInParam
                InParam.Token = oToken

                InParam.Value = New UyumErogluProduction.PrdWorderAcOpInParam
                InParam.Value.CoCode = oUyum.cCoCode
                InParam.Value.BranchCode = oUyum.cBranchCode
                InParam.Value.WorderMId = CInt(nWOrderMid)
                InParam.Value.OperationId = CInt(nOperationid)
                InParam.Value.AWstationId = CInt(nWorkstationid)
                InParam.Value.Qty = CDec(nUretimAdedi)
                InParam.Value.StartDate = dTarih
                InParam.Value.EndDate = dTarih    ' üretim tarihi , malın depoya giris tarihi

                InParam.Value.WorderAcBomDMaterialList = PrdBomDList.ToArray

                oResult = New UyumErogluProduction.ServiceResultOfInt32

                oResult = oService.SavePrdWorderAcOp(InParam)

                If oResult.Result Then

                    cSQL = "update uretharfis " +
                            " set kilitle = 'E' " +
                            " where uretfisno = '" + cUretFisNo.Trim + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    cSQL = "update uretharfislines " +
                            " set uyumid = " + oResult.Message +
                            " where uretfisno = '" + cUretFisNo.Trim + "'  " +
                            " And uretimtakipno = '" + cUTF.Trim + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    CreateLog("UyumUretim", "Uyum Servis : " + oService.Url + vbCrLf +
                                            cUretFisNo + " / " + cUTF + " -> " + oResult.Message.ToString)
                Else
                    cMessage = cMessage + vbCrLf + oResult.Message

                    CreateLog("UyumUretimHata", "Uyum Servis : " + oService.Url + vbCrLf +
                                            cUretFisNo + " / " + cUTF + " -> " + oResult.Message.ToString)
                End If
                UyumUretimEkle = oResult.Result
            Loop
            dr.Close()
            ConnYage.Close()

        Catch ex As Exception
            ErrDisp("UyumUretimEkle", cModuleName,,, ex)
        End Try
    End Function

    Public Function UyumUretimSil(cUretFisNo As String, Optional ByRef cMessage As String = "") As Boolean

        UyumUretimSil = True

        Try
            Dim oService As New UyumErogluProduction.ErogluProduction
            Dim oToken As New UyumErogluProduction.UyumToken
            Dim InParam As New UyumErogluProduction.UyumServiceRequestOfPrdWorderAcOpInDelete
            Dim oResult As New UyumErogluProduction.ServiceResultOfBoolean

            Dim nID As Integer = 0
            Dim Connyage As SqlConnection
            Dim dr As SqlDataReader
            Dim cSQL As String = ""

            Connyage = OpenConn()

            oService.Url = oUyum.cURLUyumErogluProduction

            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            InParam.Token = oToken
            InParam.Value = New UyumErogluProduction.PrdWorderAcOpInDelete

            cSQL = "select distinct b.uyumid " +
                    " from uretharfis a with (NOLOCK), uretharfislines b with (NOLOCK), uretharrba c with (NOLOCK) " +
                    " where a.uretfisno = b.uretfisno " +
                    " and b.uretfisno = c.uretfisno " +
                    " and b.ulineno = c.ulineno " +
                    " and b.uyumid is not null " +
                    " and b.uyumid <> 0 " +
                    " and a.uretfisno = '" + cUretFisNo.Trim + "' "

            dr = GetSQLReader(cSQL, Connyage)

            Do While dr.Read
                nID = CInt(SQLReadDouble(dr, "uyumid"))

                InParam.Value.WorderAcOpId = nID

                oResult = oService.DeletePrdWorderAcOp(InParam)

                If oResult.Result Then
                    CreateLog("UyumUretimSil", "Uyum Servis : " + oService.Url + vbCrLf +
                                                cUretFisNo + " " + CStr(nID) + " -> " + oResult.Message.ToString)
                Else
                    UyumUretimSil = False
                    cMessage = cMessage + vbCrLf + oResult.Message
                    CreateLog("UyumUretimSilHata", "Uyum Servis : " + oService.Url + vbCrLf +
                                                cUretFisNo + " " + CStr(nID) + " -> " + oResult.Message.ToString)
                End If
            Loop
            dr.Close()

            Connyage.Close()

        Catch ex As Exception
            UyumUretimSil = False
            ErrDisp("UyumUretimSil", cModuleName,,, ex)
        End Try
    End Function

    Public Function UyumUretimStokFisi(cUretFisNo As String, Optional ByRef cMessage As String = "") As Boolean
        ' resmi üretim firmasına ekliyor
        UyumUretimStokFisi = False

        Dim oService As UyumsoftSaveWebService.UyumSaveWebService
        Dim oToken As UyumsoftSaveWebService.UyumToken
        Dim oResult As UyumsoftSaveWebService.ServiceResultOfBoolean
        Dim Inparam As UyumsoftSaveWebService.UyumServiceRequestOfItemDef
        Dim oFis As UyumsoftSaveWebService.ItemDef          ' INVT_ITEM_M tablosu
        Dim oSatir As UyumsoftSaveWebService.ItemDetailDef  ' INVT_ITEM_D tablosu
        Dim oSatirlar As List(Of UyumsoftSaveWebService.ItemDetailDef)
        Dim nDocTraID As Integer = 0
        Dim oSQL As New SQLServerClass
        Dim nLineNo As Integer = 0
        Dim cUyumUsername As String = ""
        Dim cUyumPassword As String = ""
        Dim cSQL As String = ""
        Dim cNotlar As String = ""
        Dim cMusteriNo As String = ""

        Try
            GetUyumUserFromUretimFis(cUretFisNo, cUyumUsername, cUyumPassword)

            oSQL.OpenConn()
            oSQL.ExecuteStoredProcedure("uyum_uretim_fisi_aktarim_oncesi", cUretFisNo)

            initUyumServices(3)

            oService = New UyumsoftSaveWebService.UyumSaveWebService
            oService.Timeout = 300000

            oService.Url = oUyum.cURLUyumSaveWebService

            oFis = New UyumsoftSaveWebService.ItemDef ' INVT_ITEM_M tablosu

            oSQL.cSQLQuery = "select top 1 notlar " +
                            " from uretharfis with (NOLOCK) " +
                            " where uretfisno = '" + cUretFisNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cNotlar = oSQL.SQLReadString("notlar")
            End If
            oSQL.oReader.Close()

            cSQL = "Select w.*, " +
                    " cinsaciklamasi = (select top 1 aciklama " +
                                       " From ymodel with (NOLOCK) " +
                                       " Where modelno = w.modelno), " +
                    " firmaid = case " +
                                " when firma2id Is Not null then firma2id " +
                                " Else firma1id " +
                                " End, " +
                    " cikistipi = case " +
                                " when firma2id Is Not null And Not (firma2dahilifirma = 'E') then 'CIKIS' " +
                                " when firma2id Is Not null And firma2dahilifirma = 'E' then 'TRANSFER' " +
                                " when firma2id Is null And firma1dahilifirma = 'E' then 'TRANSFER' " +
                                " Else 'CIKIS' " +
                                " End, " +
                    " doctraid2 = (select top 1 doctraid " +
                                " From urethardoctraid with (NOLOCK) " +
                                " Where (firma = 'HEPSI' or firma = w.cikisfirm_atl) " +
                                " And (cikistipi = 'HEPSI' or cikistipi = case " +
                                                                          " when firma2id Is Not null And Not (firma2dahilifirma = 'E') then 'CIKIS' " +
                                                                          " when firma2id Is Not null And firma2dahilifirma = 'E' then 'TRANSFER' " +
                                                                          " when firma2id Is null And firma1dahilifirma = 'E' then 'TRANSFER' " +
                                                                          " Else 'CIKIS' " +
                                                                          " End ) " +
                                 " And (personel = 'HEPSI' or personel = w.personel)), "
            cSQL = cSQL +
                    " karisim1 = (select top 1 karisim " +
                                 " from siparis with (NOLOCK) " +
                                 " where kullanicisipno = w.uretimtakipno), " +
                    " karisim2 = (Select top 1 b.icerik " +
                                 " from modelhammadde  a with (NOLOCK) , stokdokuma b with (NOLOCK) " +
                                 " where a.hammaddekodu = b.stokno " +
                                 " And a.anakumas = 'E' " +
                                 " And a.modelno = w.uretimtakipno) "
            cSQL = cSQL +
                   " from (select a.uretfisno, a.belgeno, a.belgetarihi, a.girisdept, a.cikisdept, a.cikisfirm_atl, b.modelno, a.createuser, a.doctraid, b.uretimtakipno, " +
                           " stokid = d.uyumid, " +
                           " adet = sum(coalesce(c.adet, 0)), " +
                           " a.girisfirm_atl, " +
                           " firma1id = f.uyumid, " +
                           " firma1dahilifirma = f.dahilifirma, " +
                           " a.girisfirm2, "
            cSQL = cSQL +
                           " birimid = (select top 1 uyumid " +
                                        " From birim with (NOLOCK) " +
                                        " Where birim = 'AD'), " +
                           " girisdepoid = (Select top 1 uyumdepoid " +
                                        " From firma with (NOLOCK) " +
                                        " Where firma = a.girisfirm_atl), " +
                           " cikisdepoid = (select top 1 uyumdepoid " +
                                        " From firma with (NOLOCK) " +
                                        " Where firma = a.cikisfirm_atl), " +
                           " firma2id = (select top 1 uyumid " +
                                        " From firma with (NOLOCK) " +
                                        " Where firma = a.girisfirm2), " +
                           " firma2dahilifirma = (select top 1 dahilifirma " +
                                        " From firma with (NOLOCK) " +
                                        " Where firma = a.girisfirm2), " +
                           " personel = (select top 1 personel " +
                                        " From personel with (NOLOCK) " +
                                        " Where username = a.createuser), " +
                           " musterino = (select top 1 musterino " +
                                        " From siparis with (NOLOCK) " +
                                        " Where kullanicisipno = b.uretimtakipno), " +
                           " musterisipno = (select top 1 musterisipno " +
                                        " From siparis with (NOLOCK) " +
                                        " Where kullanicisipno = b.uretimtakipno) "
            cSQL = cSQL +
                            " From uretharfis a with (NOLOCK), " +
                                  " uretharfislines b with (NOLOCK), " +
                                  " uretharrba c with (NOLOCK), " +
                                  " stok d with (NOLOCK), " +
                                  " firma e with (NOLOCK), " +
                                  " firma f with (NOLOCK) "
            cSQL = cSQL +
                            " Where a.uretfisno = b.uretfisno " +
                                  " And b.uretfisno = c.uretfisno " +
                                  " And b.ulineno = c.ulineno " +
                                  " And b.modelno = d.stokno " +
                                  " And a.cikisfirm_atl = e.firma " +
                                  " And a.girisfirm_atl = f.firma " +
                                  " And e.dahilifirma = 'E' " +
                                  " And a.uretfisno = '" + cUretFisNo.Trim + "' "
            cSQL = cSQL +
                             " group by a.uretfisno, a.belgeno, a.belgetarihi, a.girisdept, a.cikisdept, a.cikisfirm_atl, " +
                                  " a.girisfirm_atl, a.girisfirm2, b.modelno, a.createuser, a.doctraid, " +
                                  " f.dahilifirma, d.uyumid, f.uyumid, b.uretimtakipno ) w "

            oSQL.cSQLQuery = cSQL.Trim

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                oFis.CoCode = oUyum.cCoCode
                oFis.BranchCode = oUyum.cBranchCode
                oFis.WhouseCode = oUyum.cWhouseCode
                If oSQL.SQLReadString("doctraid") = "" Then
                    oFis.DocTraId = CInt(oSQL.SQLReadString("doctraid2"))
                Else
                    oFis.DocTraId = CInt(oSQL.SQLReadString("doctraid"))
                End If
                oFis.DocDate = oSQL.SQLReadDate("belgetarihi")
                oFis.ReceiptDate = oSQL.SQLReadDate("belgetarihi")
                oFis.EntityId = CInt(oSQL.SQLReadDouble("firmaid"))
                oFis.DocNo = cUretFisNo
                oFis.SourceApp = UyumsoftSaveWebService.SourceApplication.İrsaliye
                oFis.SourceApp2 = UyumsoftSaveWebService.SourceApplication.İrsaliye
                oFis.SourceApp3 = UyumsoftSaveWebService.SourceApplication.İrsaliye
                oFis.CountyId = 103 ' Türkiye
                oFis.IsWaybil = True
                oFis.CurrencyOption = UyumsoftSaveWebService.CurrencyOption.Belge_Kuru
                oFis.CurRateTypeId = 235    ' 235 mb alış , 234 mb satış
                oFis.CurId = 114            ' gnld_currency / döviz kodları
                oFis.CurTra = 1
                oFis.WhouseId = CInt(oSQL.SQLReadDouble("cikisdepoid"))

                oFis.Note1 = cUretFisNo
                oFis.Note2 = SQLWriteString(cNotlar, 100)
                oFis.Note3 = oSQL.SQLReadString("belgeno")

                oFis.GnlNote6 = oSQL.SQLReadString("girisdept") + " işlemi yaptırılmak üzere"
                oFis.GnlNote9 = "URETIM FISI"

                If oSQL.SQLReadString("cikistipi") = "TRANSFER" Then
                    oFis.WhouseId2 = CInt(oSQL.SQLReadDouble("girisdepoid"))
                End If
            End If
            oSQL.oReader.Close()

            oSatirlar = New List(Of UyumsoftSaveWebService.ItemDetailDef)

            oSQL.cSQLQuery = cSQL.Trim

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                cMusteriNo = oSQL.SQLReadString("musterino")
                nLineNo = nLineNo + 10

                oSatir = New UyumsoftSaveWebService.ItemDetailDef

                oSatir.LineNo = nLineNo
                oSatir.WhouseId = CInt(oSQL.SQLReadDouble("cikisdepoid"))
                oSatir.LineType = UyumsoftSaveWebService.LineType.S
                oSatir.DcardId = CInt(oSQL.SQLReadDouble("stokid"))
                oSatir.ItemNameManual = oSQL.SQLReadString("cinsaciklamasi", 50)
                oSatir.Qty = CDec(oSQL.SQLReadDouble("adet"))
                oSatir.UnitId = CInt(oSQL.SQLReadDouble("birimid"))
                oSatir.Note1 = oSQL.SQLReadString("cikisdept")

                If InStr(cMusteriNo, "ZARA") > 0 Then
                    If oSQL.SQLReadString("karisim1") <> "" Then
                        oSatir.Note2 = oSQL.SQLReadString("karisim1")
                    ElseIf oSQL.SQLReadString("karisim2") <> "" Then
                        oSatir.Note2 = oSQL.SQLReadString("karisim2")
                    Else
                        oSatir.Note2 = ""
                    End If

                    oSatir.Note3 = oSQL.SQLReadString("uretimtakipno") + " / " + oSQL.SQLReadString("musterisipno")
                Else
                    oSatir.Note2 = oSQL.SQLReadString("cikisfirm_atl")
                    oSatir.Note3 = oSQL.SQLReadString("modelno")
                End If

                ' fiyat
                oSatir.UnitPriceTra = 0
                ' kur
                oSatir.CurRateTypeId = 235 ' 235 mb alış , 234 mb satış
                oSatir.CurTraId = 114      ' gnld_currency / döviz kodları
                oSatir.CurRateTra = 1
                ' kdv
                'oSatir.VatId = CInt(nVatId)
                ' kdv hariç
                oSatir.VatStatus = UyumsoftSaveWebService.VatStatus.Hariç

                ' satırlara yaz
                oSatirlar.Add(oSatir)
            Loop
            oSQL.oReader.Close()

            ' fiş detayını oluştur
            oFis.Details = oSatirlar.ToArray()

            ' fişi oluştur
            cGUID = Guid.NewGuid.ToString
            WEBServisPerformans(cGUID, "UyumsoftSaveWebService", "SaveWaybill", "uretharfis", cUretFisNo)

            oToken = New UyumsoftSaveWebService.UyumToken

            oToken.UserName = cUyumUsername ' oUyum.cUserName
            oToken.Password = cUyumPassword ' oUyum.cPassword

            Inparam = New UyumsoftSaveWebService.UyumServiceRequestOfItemDef

            Inparam.Token = New UyumsoftSaveWebService.UyumServiceRequestOfItemDef().Token
            Inparam.Token = oToken
            Inparam.Value = oFis

            ' irsaliyeyi yaz
            oResult = New UyumsoftSaveWebService.ServiceResultOfBoolean

            oResult = oService.SaveWaybill(Inparam)

            WEBServisPerformans(cGUID)

            If IsNumeric(oResult.Message) Then
                UyumUretimStokFisi = True

                oSQL.cSQLQuery = "update uretharfis set " +
                                " uyumid = " + oResult.Message.ToString + ", " +
                                " transfered = 'E' " +
                                " where uretfisno = '" + cUretFisNo + "' "
                oSQL.SQLExecute()

                CreateLog("UyumUretimFisi", "Uyum Servis : " + oService.Url + vbCrLf +
                                            "UretFisNo : " + cUretFisNo + " -> " + oResult.Message.ToString)

            Else
                UyumUretimStokFisi = False

                cMessage = oResult.Message.ToString + vbCrLf +
                            "Uyum username : " + cUyumUsername + vbCrLf +
                            "Uyum password : " + cUyumPassword + vbCrLf

                CreateLog("UyumUretimFisiHata", "Uyum Servis : " + oService.Url + vbCrLf +
                                            "UretFisNo : " + cUretFisNo + " -> " + oResult.Message.ToString)

            End If


            oSQL.ExecuteStoredProcedure("uyum_uretim_fisi_aktarim_sonrasi", cUretFisNo)

            oSQL.CloseConn()
            oService.Dispose()

        Catch ex As Exception
            ErrDisp("UyumUretimStokFisi : " + ex.Message, "UtilUyumsoftUretim",,, ex)
        End Try
    End Function

    Public Function UyumUretimSil2(cUretFisNo As String, Optional ByRef cMessage As String = "") As Boolean
        ' resmi üretim firmasından siliyor 
        Dim oService As UyumWebService.UyumWebService
        Dim oToken As UyumWebService.UyumToken
        Dim oRequest As UyumWebService.UyumServiceRequestOfObjectDeleteIn
        Dim cResult As String = ""
        Dim oSQL As New SQLServerClass
        Dim nUyumID As Double = 0

        UyumUretimSil2 = False

        Try
            If cUretFisNo.Trim = "" Then Exit Function

            cGUID = Guid.NewGuid.ToString
            WEBServisPerformans(cGUID, "UyumWebService", "ObjectDeleteIn", "uretharfis", cUretFisNo)

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 uyumid " +
                             " from uretharfis with (NOLOCK) " +
                             " where uretfisno = '" + cUretFisNo.Trim + "' "

            nUyumID = oSQL.DBReadDouble()

            If nUyumID = 0 Then
                oSQL.CloseConn()
                Exit Function
            End If

            initUyumServices(3, True)

            oService = New UyumWebService.UyumWebService
            oService.Url = oUyum.cURLUyumWebService

            oToken = New UyumWebService.UyumToken
            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            oRequest = New UyumWebService.UyumServiceRequestOfObjectDeleteIn
            oRequest.Token = oToken
            oRequest.Value = New UyumWebService.ObjectDeleteIn
            oRequest.Value.ObjectCollectionTypeName = "INV.ItemMCollection,INV"

            oRequest.Value.Id = CInt(nUyumID)
            cResult = oService.DeleteObject(oRequest)
            cMessage = cMessage.Trim + " " + cResult.Trim

            oSQL.cSQLQuery = "update uretharfis set " +
                             " uyumid = 0,  " +
                             " transfered = 'H' " +
                             " where uretfisno = '" + cUretFisNo.Trim + "' "
            oSQL.SQLExecute()

            oSQL.CloseConn()

            CreateLog("UyumUretimFisiSil", "Uyum Servis : " + oService.Url + vbCrLf +
                                           "UretFisNo : " + cUretFisNo + " -> " + cResult)

            UyumUretimSil2 = True

            WEBServisPerformans(cGUID)

        Catch ex As Exception
            ErrDisp("UyumUretimSil2 : " + ex.Message, "UtilUyumsoftUretim",,, ex)
        End Try
    End Function

    Private Sub GetUyumUserFromUretimFis(ByVal cUretFisNo As String, ByRef cUserName As String, ByRef cPassword As String)

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select b.uyumusername, b.uyumpassword " +
                            " from uretharfis a with (NOLOCK) , personel b with (NOLOCK) " +
                            " where a.createuser = b.username " +
                            " and a.uretfisno = '" + cUretFisNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cUserName = oSQL.SQLReadString("uyumusername")
                cPassword = oSQL.SQLReadString("uyumpassword")
            End If

            oSQL.oReader.Close()
            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("GetUyumUserFromUretimFis", cModuleName,,, ex)
        End Try
    End Sub

End Module