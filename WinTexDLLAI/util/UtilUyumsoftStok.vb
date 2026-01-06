Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Module UtilUyumsoftStok

    Const cModuleName As String = "UtilUyumsoftStok"

    Public Sub UyumStoklar()

        Try
            Dim oService As New GeneralB2B.GeneralB2BService
            Dim oResult As DataTable
            Dim oSpreadReport As New SpreadReport

            initUyumServices()

            oService.Url = oUyum.cURLGeneralB2BService

            oResult = oService.GetItemList(0)
            oSpreadReport.init2(oResult)

        Catch ex As Exception
            ErrDisp("UyumTest : " + ex.Message, cModuleName,,, ex)
        End Try
    End Sub

    Public Function UyumStokSorgula(Optional nCase As Integer = 1) As Boolean

        UyumStokSorgula = False

        Try
            Dim oService As New GeneralB2B.GeneralB2BService
            Dim oDataset As DataSet
            Dim oSpreadReport As New SpreadReport

            initUyumServices(nCase)

            oService.Url = oUyum.cURLGeneralB2BService

            oDataset = oService.GetItemWithDetail(0)
            oSpreadReport.init2(oDataset.Tables(0))

            'MsgBox("stok sorgula ok")

        Catch ex As Exception
            ErrDisp("UyumStokSorgula", cModuleName,,, ex)
        End Try
    End Function

    Public Function UyumStokEkle(Optional cFilter As String = "", Optional ByRef cMessage As String = "", Optional lSilent As Boolean = False) As Boolean

        Dim cSQL As String = ""
        Dim Connyage As SqlConnection
        Dim dr As SqlDataReader
        Dim cStokNo As String = ""
        Dim cAciklama As String = ""
        Dim cUyumBirimID As String = ""
        Dim cUyumStokID As String = ""
        Dim cUyumStokTipiID As String = ""
        Dim cStokTipi As String = ""
        Dim cMessage2 As String = ""
        Dim ofrmstatus As New frmStatus
        Dim cAnaStokGrubu As String = ""
        Dim cGtipID As String = ""
        Dim cKdvID As String = ""
        Dim cMuhasebeID As String = ""
        Dim cAciklama2 As String = ""

        UyumStokEkle = False

        Try
            If Not lSilent Then
                ofrmstatus.init()
            End If

            Connyage = OpenConn()

            cSQL = "select a.stokno, a.cinsaciklamasi, a.birim1, a.stoktipi, a.anastokgrubu, "

            cSQL = cSQL +
                    " aciklama2 = LTrim(case when a.anastokgrubu = 'MAMUL'  " +
                                " then rtrim(coalesce((select top 1 coalesce(k.karisim,'') + ' ' + coalesce(m.sex,'')  " +
                                        " From siparis k with (NOLOCK) , sipmodel l with (NOLOCK) , ymodel m with (NOLOCK)  " +
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
                    " stoktipi c with (NOLOCK)  "

            cSQL = cSQL +
                    " where a.birim1 = b.birim " +
                    " and a.stoktipi = c.kod " +
                    " and a.stokno Is Not null " +
                    " and a.stokno <> '' " +
                    " and (a.uyumid Is null or a.uyumid = 0) " +
                    " and b.uyumid is not null " +
                    " and b.uyumid <> 0 " +
                    " and c.uyumid is not null " +
                    " and c.uyumid <> 0 " +
                    cFilter +
                    " order by a.stokno "

            dr = GetSQLReader(cSQL, Connyage)

            Do While dr.Read
                cStokNo = SQLReadString(dr, "stokno")
                cAciklama = SQLReadString(dr, "cinsaciklamasi")
                cUyumBirimID = CStr(SQLReadDouble(dr, "birimid"))
                cUyumStokTipiID = CStr(SQLReadDouble(dr, "stoktipiid"))
                cUyumStokID = CStr(SQLReadDouble(dr, "stokid"))
                cAnaStokGrubu = SQLReadString(dr, "anastokgrubu")
                cGtipID = CStr(SQLReadDouble(dr, "gtipid"))
                cKdvID = CStr(SQLReadDouble(dr, "kdvid"))
                cMuhasebeID = CStr(SQLReadDouble(dr, "muhasebeid"))
                cAciklama2 = SQLReadString(dr, "aciklama2")

                If IsNumeric(cUyumStokID) And cUyumStokID <> "0" Then
                    If Not lSilent Then
                        ofrmstatus.ShowMessage("Stok kodu : " + cStokNo + " daha önce eklenmiş. ID : " + cUyumStokID)
                    End If
                Else
                    cUyumStokID = UyumStokInsert(cStokNo, cAciklama, cUyumBirimID, cUyumStokTipiID, cAnaStokGrubu, cMessage2, cGtipID, cKdvID, cMuhasebeID, cAciklama2)
                    cMessage = cMessage + " " + cMessage2
                    If IsNumeric(cUyumStokID) And cUyumStokID <> "0" Then
                        If Not lSilent Then
                            ofrmstatus.ShowMessage("Stok kodu : " + cStokNo + " eklendi. ID : " + cUyumStokID)
                        End If
                    Else
                        If Not lSilent Then
                            ofrmstatus.ShowMessage("Stok kodu : " + cStokNo + " başarısız. " + cMessage2)
                        End If
                    End If
                End If
            Loop
            dr.Close()

            Connyage.Close()
            UyumStokEkle = True
            If Not lSilent Then
                ofrmstatus.WindowState = FormWindowState.Minimized
            End If

        Catch ex As Exception
            ErrDisp("UyumStokEkle", cModuleName,,, ex)
        End Try
    End Function

    Public Function UyumStokInsert(cStokNo As String, cAciklama As String, Optional cUnitID As String = "", Optional cUyumStokTipiID As String = "",
                                   Optional cAnaStokGrubu As String = "HAMMADDE", Optional ByRef cMessage As String = "", Optional cGTIPID As String = "",
                                   Optional cKDVID As String = "", Optional cMuhasebeID As String = "", Optional cItemName2 As String = "",
                                   Optional ByRef lEklendi As Boolean = False) As String
        ' F ve R firmalarında stok id aynı olacak
        UyumStokInsert = ""
        lEklendi = False

        Try
            Dim oService As UyumErogluProduction.ErogluProduction
            Dim oToken As UyumErogluProduction.UyumToken
            Dim Inparam As UyumErogluProduction.UyumServiceRequestOfItemCardDef
            Dim oResult As UyumErogluProduction.ServiceResultOfBoolean

            Dim cSQL As String = ""
            Dim cUyumID As String = ""

            initUyumServices()

            oService = New UyumErogluProduction.ErogluProduction
            oService.Timeout = 300000

            oService.Url = oUyum.cURLUyumErogluProduction

            oToken = New UyumErogluProduction.UyumToken
            oToken.UserName = oUyum.cUserName
            oToken.Password = oUyum.cPassword

            Inparam = New UyumErogluProduction.UyumServiceRequestOfItemCardDef
            Inparam.Token = oToken
            Inparam.Value = New UyumErogluProduction.ItemCardDef()

            'Inparam.Value.CoCode = oUyum.cCoCode
            'Inparam.Value.BranchCode = oUyum.cBranchCode

            Inparam.Value.ItemCode = SQLWriteString(cStokNo, 30)
            Inparam.Value.ItemName = SQLWriteString(cAciklama, 100)
            Inparam.Value.UnitId = CInt(cUnitID)
            Inparam.Value.BrandId = CInt(cUyumStokTipiID)
            Inparam.Value.BomUnitId = CInt(cUnitID)
            Inparam.Value.ConsumptionUnitId = CInt(cUnitID)
            Inparam.Value.ItemName2 = SQLWriteString(cItemName2, 100) ' ana kumas karışımı + cinsiyet + stoktipi , %100 pamuk erkek pantolon
            If cGTIPID.Trim <> "" Then
                Inparam.Value.CccnId = CInt(cGTIPID)
            End If
            If cKDVID.Trim <> "" Then
                Inparam.Value.DefaultTaxId = CInt(cKDVID)
            End If
            If cMuhasebeID.Trim <> "" Then
                Inparam.Value.IaccIntgTypeCodeId = CInt(cMuhasebeID)
            End If

            Select Case cAnaStokGrubu
                Case "MAMUL"
                    'Inparam.Value.ItemClassCode = "Mamül"
                    Inparam.Value.ItemClassId = 129
                Case Else
                    'Inparam.Value.ItemClassCode = "Hammadde"
                    Inparam.Value.ItemClassId = 131
            End Select
            'Inparam.Value.ItemClassCode = "Yarı Mamül"
            'Inparam.Value.ItemClassId = 130

            oResult = New UyumErogluProduction.ServiceResultOfBoolean

            oResult = oService.SaveItemNotCompany(Inparam)

            If oResult.Result Then
                UyumStokInsert = oResult.Message.Trim

                If IsNumeric(oResult.Message.Trim) Then
                    cSQL = "update stok " +
                            " set uyumid = " + oResult.Message.Trim +
                            " where stokno = '" + cStokNo.Trim + "' "

                    ExecuteSQLCommand(cSQL)
                End If

                lEklendi = True
                cMessage = cStokNo + " eklendi,ID: " + oResult.Message.Trim
                CreateLog("UyumStok", cStokNo + " -> " + oResult.Message.Trim)
            Else
                cUyumID = UyumGetIDFromCodeFast(cStokNo, "INV.ItemCollection,INV")
                If IsNumeric(cUyumID) Then

                    UyumStokInsert = cUyumID

                    cSQL = "update stok " +
                            " set uyumid = " + cUyumID +
                            " where stokno = '" + cStokNo.Trim + "' "

                    ExecuteSQLCommand(cSQL)

                    cMessage = cStokNo + " daha önce eklenmiş,ID: " + oResult.Message.Trim
                    CreateLog("UyumStok", "Uyum Servis : " + oService.Url + vbCrLf +
                                          "Stok no -> Uyum bilgi mesaji : " + cStokNo + " -> " + oResult.Message.Trim)
                Else
                    UyumStokInsert = ""
                    cMessage = cStokNo + " başarısız,ID: " + oResult.Message.Trim
                    CreateLog("UyumStokHata", "Uyum Servis : " + oService.Url + vbCrLf +
                                            "Stok no -> Uyum hata mesaji : " + cStokNo + " -> " + oResult.Message.Trim)
                End If
            End If

        Catch ex As Exception
            ErrDisp("UyumStokInsert", cModuleName,,, ex)
        End Try
    End Function

End Module
