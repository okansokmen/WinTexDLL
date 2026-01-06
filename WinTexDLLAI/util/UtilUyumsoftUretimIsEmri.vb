Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Module UtilUyumsoftUretimIsEmri

    Const cModuleName As String = "UtilUyumsoftUretimIsEmri"

    Public Function UyumUIEEkle(cSiparisNo As String, Optional cMessage As String = "") As Boolean
        ' sadece F (fiili) firmaya gönderilecek
        ' R (resmi) firma üretim yapmayacak
        UyumUIEEkle = False

        Try
            Dim oService As UyumErogluProduction.ErogluProduction
            Dim oToken As UyumErogluProduction.UyumToken
            Dim InParam As UyumErogluProduction.UyumServiceRequestOfPrdWorderMInParam
            Dim oResult As UyumErogluProduction.ServiceResultOfInt32
            Dim pWorderOpDParam As UyumErogluProduction.WorderOpDParam
            Dim WorderOpDParamList As List(Of UyumErogluProduction.WorderOpDParam)

            Dim cUretimIsEmriID As String = ""
            Dim cSQL As String = ""
            Dim ConnYage As SqlConnection
            Dim dr As SqlDataReader
            Dim nWorkStationID As Integer = 0
            Dim nOperationID As Integer = 0
            Dim cDepartman As String = ""
            Dim nItemID As Integer = 0
            Dim nBirimID As Integer = 0
            Dim nBOMID As Integer = 0
            Dim nAdet As Decimal = 0
            Dim nIsEmriID As Double = 0
            Dim dSiparisTarihi As Date = #1/1/1950#

            initUyumServices()

            oService = New UyumErogluProduction.ErogluProduction
            oService.Timeout = 300000

            oService.Url = oUyum.cURLUyumErogluProduction

            oToken = New UyumErogluProduction.UyumToken
            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            ConnYage = OpenConn()

            cSQL = "select sum(coalesce(adet,0)) " +
                    " from sipmodel with (NOLOCK) " +
                    " where siparisno = '" + cSiparisNo.Trim + "' "

            nAdet = CDec(ReadSingleDoubleValueConnected(cSQL, ConnYage))

            cSQL = "select bilgisayarsipno, siparistarihi " +
                    " from siparis with (NOLOCK) " +
                    " where kullanicisipno = '" + cSiparisNo.Trim + "' "

            dr = GetSQLReader(cSQL, ConnYage)

            If dr.Read Then
                dSiparisTarihi = SQLReadDate(dr, "siparistarihi")
                nIsEmriID = SQLReadDouble(dr, "bilgisayarsipno")
            End If
            dr.Close()

            WorderOpDParamList = New List(Of UyumErogluProduction.WorderOpDParam)

            cSQL = "select distinct d.departman , d.sira , " +
                    " workstationid = d.uyumwsid , " +
                    " operationid = d.uyumid , " +
                    " bomid = e.uyumbomid , " +
                    " itemid = f.uyumid , " +
                    " birimid = g.uyumid "

            cSQL = cSQL +
                    " from siparis a with (NOLOCK) , " +
                    " sipmodel b with (NOLOCK) , " +
                    " modeluretim c with (NOLOCK) , " +
                    " departman d with (NOLOCK) , " +
                    " ymodel e with (NOLOCK) , " +
                    " stok f with (NOLOCK) , " +
                    " birim g with (NOLOCK) "

            cSQL = cSQL +
                    " where a.kullanicisipno = b.siparisno " +
                    " And b.modelno = c.modelno " +
                    " And c.departman = d.departman " +
                    " And b.modelno = e.modelno " +
                    " And b.modelno = f.stokno " +
                    " and f.birim1 = g.birim " +
                    " And a.kullanicisipno = '" + cSiparisNo.Trim + "' " +
                    " order by d.sira "

            dr = GetSQLReader(cSQL, ConnYage)

            Do While dr.Read

                cDepartman = SQLReadString(dr, "departman")
                nWorkStationID = CInt(SQLReadDouble(dr, "workstationid"))
                nOperationID = CInt(SQLReadDouble(dr, "operationid"))
                nItemID = CInt(SQLReadDouble(dr, "itemid"))
                nBirimID = CInt(SQLReadDouble(dr, "birimid"))
                nBOMID = CInt(SQLReadDouble(dr, "bomid"))

                If nWorkStationID = 0 Then
                    cMessage = cDepartman + " workstation id bulunamadı"
                    'Exit Function
                End If

                If nOperationID = 0 Then
                    cMessage = cDepartman + " operation id bulunamadı"
                    'Exit Function
                End If

                If nWorkStationID <> 0 And nOperationID <> 0 Then
                    pWorderOpDParam = New UyumErogluProduction.WorderOpDParam
                    pWorderOpDParam.WcenterCode = oUyum.cWorkCenterCode
                    pWorderOpDParam.WstationId = nWorkStationID
                    pWorderOpDParam.OperationId = nOperationID
                    pWorderOpDParam.OperationNo = CInt(SQLReadDouble(dr, "sira"))
                    pWorderOpDParam.IsNotActualcost = False
                    WorderOpDParamList.Add(pWorderOpDParam)
                End If
            Loop
            dr.Close()

            InParam = New UyumErogluProduction.UyumServiceRequestOfPrdWorderMInParam
            InParam.Token = oToken

            InParam.Value = New UyumErogluProduction.PrdWorderMInParam
            InParam.Value.CoCode = oUyum.cCoCode
            InParam.Value.BranchCode = oUyum.cBranchCode
            InParam.Value.WoTypeCode = oUyum.cWorkOrderType
            InParam.Value.WorderNo = cSiparisNo.Trim

            InParam.Value.ItemId = CInt(nItemID)
            InParam.Value.UnitId = CInt(nBirimID)
            InParam.Value.BomMId = CInt(nBOMID)
            InParam.Value.Qty = nAdet
            InParam.Value.WoOpenDate = dSiparisTarihi
            InParam.Value.StartDate = dSiparisTarihi
            InParam.Value.EndDate = CDate("01.01.2099")
            InParam.Value.SourceMId = CInt(nIsEmriID)
            InParam.Value.SourceDId = 0
            InParam.Value.WorderControl = True  ' var ise yeniden oluşturmuyor

            InParam.Value.WorderOpDList = WorderOpDParamList.ToArray()

            oResult = New UyumErogluProduction.ServiceResultOfInt32

            oResult = oService.SavePrdWorderM(InParam)

            If oResult.Result Then
                UyumUIEEkle = True
                cUretimIsEmriID = oResult.Message

                If IsNumeric(cUretimIsEmriID) Then

                    cSQL = "update siparis " +
                            " set uyumisemriid = " + cUretimIsEmriID +
                            " where kullanicisipno = '" + cSiparisNo.Trim + "' "

                    ExecuteSQLCommandConnected(cSQL, ConnYage)

                    CreateLog("UyumUretimIsemri", "Uyum Servis : " + oService.Url + vbCrLf +
                                                  "Siparis -> Uyum bilgi mesaji : " + cSiparisNo + " -> " + oResult.Message)
                End If
            Else
                UyumUIEEkle = False
                cMessage = oResult.Message
                CreateLog("UyumUretimIsemriHata", "Uyum Servis : " + oService.Url + vbCrLf +
                                                  "Siparis -> Uyum hata mesaji : " + cSiparisNo + " -> " + oResult.Message)
            End If

            ConnYage.Close()

        Catch ex As Exception
            ErrDisp("UyumUIEEkle", cModuleName,,, ex)
        End Try
    End Function

    Public Function UyumUIESil(cSiparisNo As String, Optional cMessage As String = "") As Boolean

        UyumUIESil = False

        Try
            Dim oService As New UyumErogluProduction.ErogluProduction
            Dim oToken As New UyumErogluProduction.UyumToken
            Dim InParam As New UyumErogluProduction.UyumServiceRequestOfPrdWorderMInDelete
            Dim oResult As New UyumErogluProduction.ServiceResultOfBoolean

            Dim nID As Double = 0
            Dim cSQL As String = ""

            cSQL = "select uyumisemriid " +
                    " from siparis with (NOLOCK) " +
                    " where kullanicisipno = '" + cSiparisNo.Trim + "' "

            nID = ReadSingleDoubleValue(cSQL)

            If nID = 0 Then Exit Function

            initUyumServices()

            oService.Url = oUyum.cURLUyumErogluProduction

            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            InParam.Token = oToken
            InParam.Value = New UyumErogluProduction.PrdWorderMInDelete

            InParam.Value.WorderMId = CInt(nID)

            oResult = oService.DeletePrdWorderM(InParam)

            If oResult.Result Then

                cSQL = "update siparis " +
                        " set uyumisemriid = 0 " +
                        " where kullanicisipno = '" + cSiparisNo.Trim + "' "

                ExecuteSQLCommand(cSQL)

                UyumUIESil = True
            Else
                cMessage = oResult.Message
            End If

        Catch ex As Exception
            ErrDisp("UyumUIESil", cModuleName,,, ex)
        End Try
    End Function

End Module
