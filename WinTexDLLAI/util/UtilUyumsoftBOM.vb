Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Module UtilUyumsoftBOM

    Const cModuleName As String = "UtilUyumsoftBOM"

    Public Function UyumBOMEkle(ByVal cModelNo As String, Optional ByRef cMessage As String = "") As Boolean
        ' BOM sadece F (fiili) firmaya gönderilecek
        ' R (resmi) 
        UyumBOMEkle = False
        cMessage = ""

        Try
            Dim oService As UyumErogluProduction.ErogluProduction
            Dim oToken As UyumErogluProduction.UyumToken
            Dim Inparam As UyumErogluProduction.UyumServiceRequestOfPrdBomMInParam
            Dim oResult As UyumErogluProduction.ServiceResultOfInt32
            Dim pPrdBomDParam As UyumErogluProduction.PrdBomDParam
            Dim PrdBomDList As List(Of UyumErogluProduction.PrdBomDParam)

            Dim ConnYage As SqlConnection
            Dim dr As SqlDataReader
            Dim cSQL As String = ""
            Dim cStokNo As String = ""
            Dim nNet As Double = 0
            Dim nFire As Double = 0
            Dim nBrut As Double = 0
            Dim cBirim As String = ""
            Dim nStokID As Double = 0
            Dim nBirimID As Double = 0
            Dim nDepartmanID As Double = 0
            Dim nStokTipiID As Double = 0
            Dim cAciklama As String = ""
            Dim cUyumBirimID As String = ""
            Dim cUyumStokTipiID As String = ""
            Dim cUyumStokID As String = ""
            Dim cUyumDepartmanID As String = ""
            Dim cDepartman As String = ""
            Dim cStokTipi As String = ""
            Dim nOperationNo As Double = 0
            Dim nUrunStokID As Double = 0
            Dim cUyumUrunStokID As String = ""
            Dim cBOMId As String = ""
            Dim nSiraNo As Double = 0
            Dim cAnaStokGrubu As String = ""
            Dim cGtipID As String = ""
            Dim cKdvID As String = ""
            Dim cMuhasebeID As String = ""
            Dim cMessage2 As String = ""
            Dim cAciklama2 As String = ""

            If cModelNo.Trim = "" Then
                cMessage = "Model no boş olamaz"
                Exit Function
            End If

            ConnYage = OpenConn()

            initUyumServices()

            oService = New UyumErogluProduction.ErogluProduction
            oService.Timeout = 300000

            oService.Url = oUyum.cURLUyumErogluProduction

            oToken = New UyumErogluProduction.UyumToken

            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            cSQL = "select sirano " +
                    " from ymodel with (NOLOCK) " +
                    " where modelno = '" + cModelNo.Trim + "' "

            nSiraNo = ReadSingleDoubleValueConnected(cSQL, ConnYage)

            cSQL = "select a.stokno, a.cinsaciklamasi, a.birim1, a.stoktipi, a.anastokgrubu, "

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
                    " stoktipi c with (NOLOCK) "

            cSQL = cSQL +
                    " where a.birim1 = b.birim " +
                    " and a.stoktipi = c.kod " +
                    " and a.stokno = '" + cModelNo.Trim + "' "

            dr = GetSQLReader(cSQL, ConnYage)

            If dr.Read Then

                cStokNo = SQLReadString(dr, "stokno")
                cAciklama = SQLReadString(dr, "cinsaciklamasi")
                cStokTipi = SQLReadString(dr, "stoktipi")
                nStokID = SQLReadDouble(dr, "stokid")
                cAnaStokGrubu = SQLReadString(dr, "anastokgrubu")
                cGtipID = CStr(SQLReadDouble(dr, "gtipid"))
                cKdvID = CStr(SQLReadDouble(dr, "kdvid"))
                cMuhasebeID = CStr(SQLReadDouble(dr, "muhasebeid"))
                cAciklama2 = SQLReadString(dr, "aciklama2")

                nBirimID = SQLReadDouble(dr, "birimid")
                If nBirimID = 0 Then
                    cMessage = cBirim + " birim uyumid boş olamaz"
                    dr.Close()
                    ConnYage.Close()
                    Exit Function
                End If
                cUyumBirimID = nBirimID.ToString

                nStokTipiID = SQLReadDouble(dr, "stoktipiid")
                If nStokTipiID = 0 Then
                    cMessage = cStokTipi + " stok tipi uyumid boş olamaz"
                    dr.Close()
                    ConnYage.Close()
                    Exit Function
                End If
                cUyumStokTipiID = nStokTipiID.ToString

                If nStokID = 0 Then
                    cMessage = cStokNo + " stokno uyumid boş olamaz"
                    dr.Close()
                    ConnYage.Close()
                    Exit Function
                    'cUyumUrunStokID = UyumStokInsert(cStokNo, cAciklama, cUyumBirimID, cUyumStokTipiID, "MAMUL", cMessage2, cGtipID, cKdvID, cMuhasebeID, cAciklama2)
                    'If Not IsNumeric(cUyumUrunStokID) Then
                    '    MsgBox(cUyumUrunStokID)
                    '    dr.Close()
                    '    ConnYage.Close()
                    '    Exit Function
                    'End If
                Else
                    cUyumUrunStokID = nStokID.ToString
                End If

            End If
            dr.Close()

            PrdBomDList = New List(Of UyumErogluProduction.PrdBomDParam)

            ' ana reçete
            cSQL = "SELECT DISTINCT w.* " +
                    " FROM (SELECT a.hammaddekodu, a.kullanimmiktari, a.fire, b.birim1, b.cinsaciklamasi, b.stoktipi, a.uretimdepartmani, e.sira, b.anastokgrubu, " +
                            " stokid = b.uyumid, " +
                            " birimid = c.uyumid, " +
                            " stoktipiid = d.uyumid, " +
                            " departmanid = e.uyumid, " +
                            " gtipid = (select top 1 uyumid from gtip with (NOLOCK) where gtip = b.gtip), " +
                            " kdvid = (select top 1 uyumid from kdvgroup with (NOLOCK) where oran = b.kdv ), " +
                            " muhasebeid = d.uyummuhsablonid, "
            cSQL = cSQL +
                            " aciklama2 = LTrim(case when b.anastokgrubu = 'MAMUL'  " +
                                " then rtrim(coalesce((select top 1 coalesce(k.karisim,'') + ' ' + coalesce(m.sex,'')  " +
                                        " From siparis k with (NOLOCK) , sipmodel l with (NOLOCK) , ymodel m with (NoLOCK)  " +
                                        " Where k.kullanicisipno = l.siparisno " +
                                        " And l.modelno = m.modelno " +
                                        " And l.modelno = b.stokno " +
                                        " order by k.bilgisayarsipno desc),'')) + ' ' + b.stoktipi " +
                                " Else a.kompozisyon " +
                                " End) "
            cSQL = cSQL +
                            " From modelhammadde a WITH (NOLOCK) , " +
                            " stok b WITH (NOLOCK) , " +
                            " birim c with (NOLOCK) , " +
                            " stoktipi d with (NOLOCK) , " +
                            " departman e with (NOLOCK) "
            cSQL = cSQL +
                            " WHERE a.hammaddekodu = b.stokno " +
                            " And b.birim1 = c.birim " +
                            " And b.stoktipi = d.kod " +
                            " And a.uretimdepartmani = e.departman " +
                            " And a.modelno = '" + cModelNo.Trim + "' " +
                            " UNION "

            ' alternatif reçeteler
            cSQL = cSQL +
                            " SELECT a.hammaddekodu, a.kullanimmiktari, a.fire, b.birim1, b.cinsaciklamasi, b.stoktipi, a.uretimdepartmani, e.sira, b.anastokgrubu, " +
                            " stokid = b.uyumid, " +
                            " birimid = c.uyumid, " +
                            " stoktipiid = d.uyumid, " +
                            " departmanid = e.uyumid, " +
                            " gtipid = (select top 1 uyumid from gtip with (NOLOCK) where gtip = b.gtip), " +
                            " kdvid = (select top 1 uyumid from kdvgroup with (NOLOCK) where oran = b.kdv ), " +
                            " muhasebeid = d.uyummuhsablonid, "
            cSQL = cSQL +
                            " aciklama2 = LTrim(case when b.anastokgrubu = 'MAMUL'  " +
                                " then rtrim(coalesce((select top 1 coalesce(k.karisim,'') + ' ' + coalesce(m.sex,'')  " +
                                        " From siparis k with (NOLOCK) , sipmodel l with (NOLOCK) , ymodel m with (NoLOCK)  " +
                                        " Where k.kullanicisipno = l.siparisno " +
                                        " And l.modelno = m.modelno " +
                                        " And l.modelno = b.stokno " +
                                        " order by k.bilgisayarsipno desc),'')) + ' ' + b.stoktipi " +
                                " Else a.kompozisyon " +
                                " End) "
            cSQL = cSQL +
                            " From modelhammadde2 a WITH (NOLOCK) , " +
                            " stok b WITH (NOLOCK) , " +
                            " birim c with (NOLOCK) , " +
                            " stoktipi d with (NOLOCK) , " +
                            " departman e with (NOLOCK) "
            cSQL = cSQL +
                            " WHERE a.hammaddekodu = b.stokno " +
                            " and b.birim1 = c.birim " +
                            " and b.stoktipi = d.kod " +
                            " and a.uretimdepartmani = e.departman  " +
                            " and a.modelno = '" + cModelNo.Trim + "' ) w "

            dr = GetSQLReader(cSQL, ConnYage)

            Do While dr.Read

                cStokNo = SQLReadString(dr, "hammaddekodu")
                cAciklama = SQLReadString(dr, "cinsaciklamasi")
                cStokTipi = SQLReadString(dr, "stoktipi")
                cDepartman = SQLReadString(dr, "uretimdepartmani")
                nOperationNo = SQLReadDouble(dr, "sira")
                nNet = SQLReadDouble(dr, "kullanimmiktari")
                nFire = SQLReadDouble(dr, "fire")
                nBrut = nNet * (1 + (nFire / 100))
                cBirim = SQLReadString(dr, "birim1")
                cAnaStokGrubu = SQLReadString(dr, "anastokgrubu")
                cGtipID = CStr(SQLReadDouble(dr, "gtipid"))
                cKdvID = CStr(SQLReadDouble(dr, "kdvid"))
                cMuhasebeID = CStr(SQLReadDouble(dr, "muhasebeid"))
                cAciklama2 = SQLReadString(dr, "aciklama2")

                nBirimID = SQLReadDouble(dr, "birimid")
                If nBirimID = 0 Then
                    cMessage = cBirim + " birim uyumid boş olamaz"
                    dr.Close()
                    ConnYage.Close()
                    Exit Function
                End If

                nStokTipiID = SQLReadDouble(dr, "stoktipiid")
                If nStokTipiID = 0 Then
                    cMessage = cStokTipi + " stok tipi uyumid boş olamaz"
                    dr.Close()
                    ConnYage.Close()
                    Exit Function
                End If

                nDepartmanID = SQLReadDouble(dr, "departmanid")
                If nDepartmanID = 0 Then
                    cMessage = cDepartman + " departmanı uyumid boş olamaz"
                    dr.Close()
                    ConnYage.Close()
                    Exit Function
                End If

                nStokID = SQLReadDouble(dr, "stokid")
                If nStokID = 0 Then
                    cMessage = cStokNo + " stokno uyumid boş olamaz"
                    dr.Close()
                    ConnYage.Close()
                    Exit Function
                    'cUyumStokID = UyumStokInsert(cStokNo, cAciklama, cUyumBirimID, cUyumStokTipiID, cAnaStokGrubu, cMessage2, cGtipID, cKdvID, cMuhasebeID, cAciklama2)
                    'If Not IsNumeric(cUyumStokID) Then
                    '    MsgBox(cUyumStokID)
                    '    dr.Close()
                    '    ConnYage.Close()
                    '    Exit Function
                    'End If
                End If

                ' hammadde
                pPrdBomDParam = New UyumErogluProduction.PrdBomDParam
                pPrdBomDParam.ItemId = CInt(nStokID)
                pPrdBomDParam.UnitId = CInt(nBirimID)
                pPrdBomDParam.Qty = CDec(nBrut)
                pPrdBomDParam.QtyNet = CDec(nNet)
                pPrdBomDParam.OperationId = CInt(nDepartmanID)
                pPrdBomDParam.OperationNo = CInt(nOperationNo)
                pPrdBomDParam.GrossNetType = UyumErogluProduction.GrossNetType.Net

                PrdBomDList.Add(pPrdBomDParam)
            Loop
            dr.Close()

            ' ürün 
            Inparam = New UyumErogluProduction.UyumServiceRequestOfPrdBomMInParam

            Inparam.Token = oToken

            Inparam.Value = New UyumErogluProduction.PrdBomMInParam
            Inparam.Value.CoCode = oUyum.cCoCode
            Inparam.Value.BranchCode = oUyum.cBranchCode
            Inparam.Value.ItemId = CInt(cUyumUrunStokID)
            Inparam.Value.BomTypeCode = "STANDART"
            Inparam.Value.StartDate = DateTime.Now
            Inparam.Value.EndDate = Inparam.Value.StartDate.AddMonths(12)
            Inparam.Value.ScrapQtyType = UyumErogluProduction.MainScrapQtyType.Yuzde
            Inparam.Value.WoChangeType = UyumErogluProduction.WoChangeType.DegisiklikYapilabilir
            Inparam.Value.BatchQtyEnd = 999999
            Inparam.Value.BomControl = True ' var ise yeniden oluşturmuyor
            Inparam.Value.SourceMId = CInt(nSiraNo)

            ' hammadde ağacını toparla
            Inparam.Value.PrdBomDList = PrdBomDList.ToArray()

            oResult = New UyumErogluProduction.ServiceResultOfInt32

            oResult = oService.SavePrdBomM(Inparam)

            If oResult.Result Then
                UyumBOMEkle = True
                cBOMId = oResult.Message
                If IsNumeric(cBOMId) Then

                    cSQL = "update ymodel " +
                            " set kilitlemalzeme = 'E' , " +
                            " kilitle = 'E' " +
                            " uyumbomid = " + cBOMId +
                            " where modelno = '" + cModelNo.Trim + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    CreateLog("UyumBOM", cModelNo.Trim + " -> " + oResult.Message)
                End If
            Else
                UyumBOMEkle = False
                cMessage = oResult.Message
                CreateLog("UyumBOMHata", cModelNo.Trim + " -> " + oResult.Message)
            End If

            ConnYage.Close()

        Catch ex As Exception
            ErrDisp("UyumBOMEkle", cModuleName,,, ex)
        End Try
    End Function

    Public Function UyumBOMSil(cModelNo As String, Optional ByRef cMessage As String = "") As Boolean

        UyumBOMSil = False

        Try
            Dim oService As New UyumErogluProduction.ErogluProduction
            Dim oToken As New UyumErogluProduction.UyumToken
            Dim InParam As New UyumErogluProduction.UyumServiceRequestOfPrdBomMInDelete
            Dim oResult As New UyumErogluProduction.ServiceResultOfBoolean

            Dim nID As Double = 0
            Dim cSQL As String = ""

            cSQL = "select uyumbomid " +
                    " from ymodel with (NOLOCK) " +
                    " where modelno = '" + cModelNo.Trim + "' "

            nID = ReadSingleDoubleValue(cSQL)

            If nID = 0 Then Exit Function

            initUyumServices()

            oService.Url = oUyum.cURLUyumErogluProduction

            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            InParam.Token = oToken
            InParam.Value = New UyumErogluProduction.PrdBomMInDelete

            InParam.Value.BomMId = CInt(nID)

            oResult = oService.DeleteBomM(InParam)

            If oResult.Result Then

                cSQL = "update ymodel " +
                        " set uyumbomid = 0 " +
                        " where modelno = '" + cModelNo.Trim + "' "

                ExecuteSQLCommand(cSQL)

                cMessage = oResult.Message

                UyumBOMSil = True
            End If

        Catch ex As Exception
            ErrDisp("UyumBOMSil", cModuleName,,, ex)
        End Try
    End Function

End Module
