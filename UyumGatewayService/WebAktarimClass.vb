Imports System
Imports System.Configuration
Imports System.IO
Imports System.Windows.Forms
Imports System.Data
Imports Npgsql
Imports System.Threading.Tasks.TaskExtensions
Imports Microsoft.VisualBasic
Imports UyumGatewayService.UWS

Public Class WebAktarimClass

    Public USR1 As String = "uyum"
    Public USR2 As String = "uyum"
    Public PSW1 As String = "12345"
    Public PSW2 As String = "12345"
    Public SR1U As String = System.Configuration.ConfigurationManager.AppSettings("SRV1URL")     ' Kaynak Veritabanı için Webservis Adresi
    Public SR2U As String = System.Configuration.ConfigurationManager.AppSettings("SRV2URL")     ' Hedef Veritabanı için Webservis Adresi

    Public WSUser As String = System.Configuration.ConfigurationManager.AppSettings("WSUs")      ' Webservis Kullanıcı Adı
    Public WSPass As String = System.Configuration.ConfigurationManager.AppSettings("WSPs")      ' Webservis Kullanıcı Şifresi
    Public SLLU As String = System.Configuration.ConfigurationManager.AppSettings("WSLLU")       ' Kaynak Veritabanı   
    Public SLLG As String = System.Configuration.ConfigurationManager.AppSettings("WSLLG")       ' Kaynak Veritabanı  Where Şartı  0 olanlar
    Public SLLG13 As String = System.Configuration.ConfigurationManager.AppSettings("WSLLG13")   ' Kaynak Veritabanı  Where Şartı 0 ve 3 olanlar
    Public NSrg As String = System.Configuration.ConfigurationManager.AppSettings("WNSrg")       ' Veritabanı1 deki Log dosyasını Dönen değerde hata yoksa update sorgusu
    Public HSrg As String = System.Configuration.ConfigurationManager.AppSettings("WHSrg")       ' Veritabanı1 deki Log dosyasını Dönen değerde hata varsa update sorgusu
    Public DSrg As String = System.Configuration.ConfigurationManager.AppSettings("WDSrg")       ' Veritabanı1 deki Log dosyasını xp_op = 9 yapma sorgusu
    Public ErkanSrgDelete As String = System.Configuration.ConfigurationManager.AppSettings("ErkanSrgDelete")
    Public ErkanSrgInsert As String = System.Configuration.ConfigurationManager.AppSettings("ErkanSrgInsert")

    Public VTB1 As String = System.Configuration.ConfigurationManager.AppSettings("VTB1")        ' Kaynak Veritabanı için Veritabanı Adı
    Public SRV1 As String = System.Configuration.ConfigurationManager.AppSettings("SRV1")        ' Kaynak Veritabanı için Sunucu Adı-Adresi
    Public VTB2 As String = System.Configuration.ConfigurationManager.AppSettings("VTB2")       '  Hedef Veritabanı için Veritabanı Adı
    Public SRV2 As String = System.Configuration.ConfigurationManager.AppSettings("SRV2")       '  Hedef Veritabanı için Sunucu Adı-Adresi

    Public M As Double = 0
    Public TTT As Boolean = False
    Public CKS As Integer = 0

    Public Sub init()

        Dim oKaynak As PostgreClass
        Dim oHedef As PostgreClass

        Dim cErrorMessage As String = ""
        Dim oDataTable As DataTable
        Dim oRow As DataRow
        Dim CiftTirnak As String = ControlChars.Quote
        Dim cSorguInsert As String = ""
        Dim cSorguDelete As String = ""

        Dim Res As String = ""
        Dim IID As String = ""
        Dim MID As String = ""
        Dim LLD As String = ""
        Dim TYP As String = ""
        Dim CL1 As String = ""
        Dim CLL As String = ""
        Dim OBJ As String = ""
        Dim XXX As String = ""
        Dim sorgu As String = ""
        Dim Msj As String = ""
        Dim nRow As Integer = 0
        Dim DS As DataSet
        Dim lOK As Boolean = False

        Dim cSQL As String = ""
        Dim cSQLInsert As String = ""
        Dim cSQLDelete As String = ""

        Try
            CreateLog(cServiceName, "Webservis aktarım BAŞLADI")

            oKaynak = New PostgreClass(SRV1, VTB1, USR1, PSW1)
            oHedef = New PostgreClass(SRV2, VTB2, USR2, PSW2)

            cErrorMessage = ""
            oDataTable = oKaynak.PGSelectOpenCloseConnection(SLLG, cErrorMessage)
            If oDataTable Is Nothing Then
                CreateLog(cServiceName, "Webservis aktarım hata var. SQL : " + SLLG + " Hata : " + cErrorMessage)
                Exit Sub
            End If

            For Each oRow In oDataTable.Rows

                DS = New DataSet
                CL1 = ""
                CLL = ""
                OBJ = oRow.Item("COLLECTION_TYPE").ToString
                XXX = oRow.Item("COLLECTION_TYPE").ToString

                If XXX <> "" Then

                    Res = ""
                    CL1 = XXX.Substring(0, XXX.Length - 4)
                    CLL = CL1.Substring(4, CL1.Length - 4)

                    IID = oRow.Item("ID").ToString
                    MID = oRow.Item("OP_ID").ToString
                    LLD = oRow.Item("LOADDETAIL").ToString
                    TYP = oRow.Item("OP").ToString

                    If LLD = "F" Then
                        DS = VeriOku(OBJ, MID, False)
                    Else
                        DS = VeriOku(OBJ, MID, True)
                    End If

                    If DS.Tables.Count > 0 Then

                        cSQLInsert = ""
                        cSQLDelete = ""
                        cSorguInsert = ""
                        cSorguDelete = ""
                        Msj = "Ok"

                        Select Case TYP
                            Case "I"
                                ' insert
                                Res = VeriIns(DS, OBJ, MID).Trim
                                Res = Res.Replace("'", "")

                                ' Erkan araya giriyor
                                If ErkanSrgInsert.Trim <> "" Then
                                    cSQLInsert = ErkanSrgInsert + " " + MID
                                    oHedef.PGExecuteOpenCloseConnection(cSQLInsert, cErrorMessage)
                                End If

                                If Res.Length = 0 Then
                                    cSorguInsert = NSrg + "xp_error = 'Ok' where id = " + IID
                                Else
                                    cSorguInsert = HSrg + "xp_error = '" + Res + "' where id = " + IID
                                End If
                                oKaynak.PGExecuteOpenCloseConnection(cSorguInsert, cErrorMessage)

                            Case "D"
                                ' delete
                                Res = VeriSil(OBJ, MID).Trim
                                Res = Res.Replace("'", "")

                                ' Erkan araya giriyor
                                If ErkanSrgDelete.Trim <> "" Then
                                    cSQLDelete = ErkanSrgDelete + " " + MID
                                    oHedef.PGExecuteOpenCloseConnection(cSQLDelete, cErrorMessage)
                                End If

                                If Res.Length = 0 Then
                                    cSorguDelete = NSrg + "xp_error = 'Ok' where id = " + IID
                                Else
                                    cSorguDelete = HSrg + "xp_error = '" + Res + "' where id = " + IID
                                End If
                                oKaynak.PGExecuteOpenCloseConnection(cSorguDelete, cErrorMessage)

                            Case "U"
                                lOK = True

                                ' delete
                                Res = VeriSil(OBJ, MID).Trim()
                                Res = Res.Replace("'", "")

                                ' Erkan araya giriyor
                                If ErkanSrgDelete.Trim <> "" Then
                                    cSQLDelete = ErkanSrgDelete + " " + MID
                                    oHedef.PGExecuteOpenCloseConnection(cSQLDelete, cErrorMessage)
                                End If

                                If Res.Length = 0 Then
                                    cSorguDelete = NSrg + "xp_error = 'Ok' where id = " + IID
                                Else
                                    lOK = False
                                    cSorguDelete = HSrg + "xp_error = '" + Res + "' where id = " + IID
                                End If
                                oKaynak.PGExecuteOpenCloseConnection(cSorguDelete, cErrorMessage)

                                If lOK Then
                                    ' insert
                                    Res = VeriIns(DS, OBJ, MID).Trim
                                    Res = Res.Replace("'", "")

                                    ' Erkan araya giriyor
                                    If ErkanSrgInsert.Trim <> "" Then
                                        cSQLInsert = ErkanSrgInsert + " " + MID
                                        oHedef.PGExecuteOpenCloseConnection(cSQLInsert, cErrorMessage)
                                    End If

                                    If Res.Length = 0 Then
                                        cSorguInsert = NSrg + "xp_error = 'Ok' where id = " + IID
                                    Else
                                        cSorguInsert = HSrg + "xp_error = '" + Res + "' where id = " + IID
                                    End If
                                    oKaynak.PGExecuteOpenCloseConnection(cSorguInsert, cErrorMessage)
                                End If
                        End Select

                        CreateLog(cServiceName, "TYP : " + TYP + vbCrLf +
                                                "Erkan Delete : " + cSQLDelete + vbCrLf +
                                                "Delete Sorgusu : " + cSorguDelete + vbCrLf +
                                                "Erkan Insert : " + cSQLInsert + vbCrLf +
                                                "Insert Sorgusu : " + cSorguInsert + vbCrLf +
                                                "Son Error : " + cErrorMessage)
                    Else
                        CreateLog(cServiceName, "Veri gelmedi. MID : " + MID)
                    End If
                End If
            Next

            CreateLog(cServiceName, "Webservis aktarım TAMAMLANDI")

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Private Function VeriSil(cObjectCollectionTypeName As String, cID As String) As String

        Dim oServis As New UWS.UyumWebService
        Dim oObj1 As New UWS.UyumServiceRequestOfObjectDeleteIn

        VeriSil = "YOK"

        Try
            oServis.Url = SR2U

            oObj1.Token = New UWS.UyumToken
            oObj1.Token.UserName = WSUser
            oObj1.Token.Password = WSPass
            oObj1.Value = New UWS.ObjectDeleteIn
            oObj1.Value.ObjectCollectionTypeName = cObjectCollectionTypeName
            oObj1.Value.Id = CInt(cID)

            VeriSil = oServis.DeleteObject(oObj1)

        Catch ex As Exception
            VeriSil = ex.Message.ToString
        End Try
    End Function

    Private Function VeriIns(oDS As DataSet, cObjectCollectionTypeName As String, cID As String) As String

        Dim oServis As New UWS.UyumWebService
        Dim oObj1 As New UWS.UyumServiceRequestOfDataSet
        Dim oObj2 As New UWS.UyumServiceRequestOfObjectsIn
        Dim nSonuc As Integer = 0
        Dim lSonuc As Boolean = False

        VeriIns = ""

        Try
            oServis.Url = SR2U

            oObj1.Token = New UWS.UyumToken
            oObj1.Token.UserName = WSUser
            oObj1.Token.Password = WSPass
            oObj1.Value = New DataSet
            oObj1.Value = oDS

            oObj2.Token = New UWS.UyumToken
            oObj2.Token.UserName = WSUser
            oObj2.Token.Password = WSPass
            oObj2.Value = New UWS.ObjectsIn
            oObj2.Value.ObjectCollectionTypeName = cObjectCollectionTypeName
            oObj2.Value.Id = CInt(cID)

            nSonuc = oServis.InsertDataSetToCollection2(oObj1, True)
            lSonuc = oServis.UpdateObject(oObj2)

        Catch ex As Exception
            VeriIns = ex.Message.ToString
        End Try
    End Function

    Private Function VeriUpd(oDS As DataSet, cObjectCollectionTypeName As String, cID As String) As String

        Dim oServis As New UWS.UyumWebService
        Dim oObj1 As New UWS.UyumServiceRequestOfDataSet
        Dim oObj2 As New UWS.UyumServiceRequestOfObjectsIn
        Dim oDS2 As New DataSet

        VeriUpd = ""

        Try
            oServis.Url = SR2U

            oObj1.Token = New UWS.UyumToken
            oObj1.Token.UserName = WSUser
            oObj1.Token.Password = WSPass
            oObj1.Value = New DataSet
            oObj1.Value = oDS

            oDS2 = oObj1.Value

            oServis.UpdateDataSetToCollection(oObj1, False)
            ' oServis.UpdateObject(oObj2)

        Catch ex As Exception
            VeriUpd = ex.Message.ToString
        End Try
    End Function

    Private Function VeriOku(cObjectCollectionTypeName As String, cID As String, lLoadDetail As Boolean) As DataSet

        Dim oServis As New UWS.UyumWebService
        Dim oObj1 As New UWS.UyumServiceRequestOfObjectInfoIn
        Dim oSearch As UWS.WherePropertyClause
        Dim oSearchs As New List(Of UWS.WherePropertyClause)

        VeriOku = New DataSet

        Try
            oServis.Url = SR1U

            oObj1.Token = New UWS.UyumToken
            oObj1.Token.UserName = WSUser
            oObj1.Token.Password = WSPass
            oObj1.Value = New UWS.ObjectInfoIn
            oObj1.Value.ObjectCollectionTypeName = cObjectCollectionTypeName
            oObj1.Value.LoadDetail = lLoadDetail

            oSearch = New UWS.WherePropertyClause
            oSearch.PropertyName = "Id"
            oSearch.Value = CInt(cID)

            oSearchs.Add(oSearch)

            oObj1.Value.WhereClauseList = oSearchs.ToArray

            VeriOku = oServis.GetObjectToDataSet(oObj1)

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Function

End Class
