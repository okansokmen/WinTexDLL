Imports System
Imports System.Configuration
Imports System.IO
Imports System.Windows.Forms
Imports System.Data
Imports Npgsql
Imports System.Threading.Tasks.TaskExtensions
Imports Microsoft.VisualBasic
Imports C1.Win.C1TrueDBGrid
Imports UyumGateway.UyumAktarWebServis

Public Class UyumAktarWebServis

    Dim oKaynak As PostgreClass

    Public USR1 As String = "uyum"                                           ' WEB servis kullanıcı adı
    Public PSW1 As String = "12345"                                          ' WEB servis kullanıcı şifresi
    Public SR1U As String = ConfigurationManager.AppSettings("SRV1URL")     ' Kaynak Veritabanı için Webservis Adresi
    Public SR2U As String = ConfigurationManager.AppSettings("SRV2URL")     ' Hedef Veritabanı için Webservis Adresi

    Public WSUser As String = ConfigurationManager.AppSettings("WSUs")      ' Webservis Kullanıcı Adı
    Public WSPass As String = ConfigurationManager.AppSettings("WSPs")      ' Webservis Kullanıcı Şifresi
    Public SLLU As String = ConfigurationManager.AppSettings("WSLLU")       ' Kaynak Veritabanı  Where Şartı
    Public SLLG As String = ConfigurationManager.AppSettings("WSLLG")       ' Kaynak Veritabanı  Where Şartı  0 olanlar
    Public SLLG13 As String = ConfigurationManager.AppSettings("WSLLG13")   ' Kaynak Veritabanı  Where Şartı 0 ve 3 olanlar
    Public NSrg As String = ConfigurationManager.AppSettings("WNSrg")       ' Veritabanı1 deki Log dosyasını Dönen değerde hata yoksa update sorgusu
    Public HSrg As String = ConfigurationManager.AppSettings("WHSrg")       ' Veritabanı1 deki Log dosyasını Dönen değerde hata varsa update sorgusu
    Public DSrg As String = ConfigurationManager.AppSettings("WDSrg")       ' Veritabanı1 deki Log dosyasını xp_op = 9 yapma sorgusu

    Public VTB1 As String = ConfigurationManager.AppSettings("VTB1")        ' Kaynak Veritabanı için Veritabanı Adı
    Public SRV1 As String = ConfigurationManager.AppSettings("SRV1")        ' Kaynak Veritabanı için Sunucu Adı-Adresi

    Public Mn As Double = Convert.ToDouble(ConfigurationManager.AppSettings("WMn"))
    Public Hr As Double = Convert.ToDouble(ConfigurationManager.AppSettings("WHr"))

    Public M As Double = 0
    Public TTT As Boolean = False
    Public CKS As Integer = 0


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = My.Resources.wintex
    End Sub

    Private Sub UyumAktarWebServis_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            oKaynak = New PostgreClass(SRV1, VTB1, USR1, PSW1)

            Me.MdiParent = HTMain

            Me.C1TrueDBGrid1.FilterBar = True
            Me.C1TrueDBGrid1.AllowSort = False
            Me.C1TrueDBGrid1.FetchRowStyles = True

            Me.C1TrueDBGrid2.FilterBar = True
            Me.C1TrueDBGrid2.AllowSort = False

            Me.Text = "WSUser : " + WSUser + " Server : " + SRV1 + "/" + VTB1 + " Url : " + SR1U + " " + SR2U

            Me.WindowState = FormWindowState.Maximized

        Catch ex As Exception
            ErrDisp(ex, Me.Name)
        End Try
    End Sub

    Private Sub EkranTemizle()
        Try
            C1SuperLabel1.Text = "Toplam Satır"
            C1SuperLabel2.Text = "Aktarılan Satır"

            C1TrueDBGrid1.DataSource = Nothing
            C1TrueDBGrid1.Rows.Clear()
            C1TrueDBGrid1.Refresh()

            C1TrueDBGrid2.DataSource = Nothing
            C1TrueDBGrid2.Rows.Clear()
            C1TrueDBGrid2.Refresh()

            C1Button4.Enabled = True

        Catch ex As Exception
            ErrDisp(ex, Me.Name)
        End Try
    End Sub

    Private Sub C1Button5_Click(sender As Object, e As EventArgs) Handles C1Button5.Click
        GroupBox1.Enabled = False
        EkranTemizle()
        GroupBox1.Enabled = True
    End Sub

    Private Sub RunTransfer(nCase As Integer)

        Dim cErrorMessage As String = ""
        Dim oDataTable As DataTable
        Dim cSQL As String = ""

        Try
            Select Case nCase
                Case 1
                    cSQL = SLLG13
                Case 2
                    cSQL = SLLG
            End Select

            EkranTemizle()
            'If oKaynak.PGExecuteOpenCloseConnection(SLLU, cErrorMessage) Then

            oDataTable = oKaynak.PGSelectOpenCloseConnection(cSQL, cErrorMessage)
            If oDataTable Is Nothing Then
                MessageBox.Show("Hata : " + cErrorMessage)
            Else
                C1TrueDBGrid1.DataSource = oDataTable
                C1TrueDBGrid1.Refresh()
                C1SuperLabel1.Text = "Toplam Satır " + Me.C1TrueDBGrid1.RowCount.ToString
                C1SuperLabel1.Refresh()

                MessageBox.Show("Kayıtlar okundu" + vbCrLf + cSQL)
            End If

            'Else
            '    MessageBox.Show("Hata : " + cErrorMessage)
            'End If


        Catch ex As Exception
            ErrDisp(ex, Me.Name)
        End Try
    End Sub

    Private Sub C1Button2_Click(sender As Object, e As EventArgs) Handles C1Button2.Click
        Me.Close()
    End Sub

    Private Sub C1Button1_Click(sender As Object, e As EventArgs) Handles C1Button1.Click
        GroupBox1.Enabled = False
        RunTransfer(2) ' SLLG
        GroupBox1.Enabled = True
    End Sub

    Private Sub C1Button3_Click(sender As Object, e As EventArgs) Handles C1Button3.Click
        GroupBox1.Enabled = False
        RunTransfer(1) ' SLLG13
        GroupBox1.Enabled = True
    End Sub

    Private Sub C1Button4_Click(sender As Object, e As EventArgs) Handles C1Button4.Click
        GroupBox1.Enabled = False
        IslemYap
        GroupBox1.Enabled = True
    End Sub

    Public Sub IslemYap()

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
        Dim cErrorMessage As String = ""
        Dim lOK As Boolean = False

        Try
            If Me.C1TrueDBGrid1.RowCount = 0 Then Exit Sub

            For nRow = 0 To Me.C1TrueDBGrid1.RowCount - 1

                DS = New DataSet
                CL1 = ""
                CLL = ""
                OBJ = Me.C1TrueDBGrid1.Columns("COLLECTION_TYPE").CellValue(nRow).ToString
                XXX = Me.C1TrueDBGrid1.Columns("COLLECTION_TYPE").CellValue(nRow).ToString

                If XXX <> "" Then

                    Res = ""
                    CL1 = XXX.Substring(0, XXX.Length - 4)
                    CLL = CL1.Substring(4, CL1.Length - 4)

                    IID = Me.C1TrueDBGrid1.Columns("ID").CellValue(nRow).ToString
                    MID = Me.C1TrueDBGrid1.Columns("OP_ID").CellValue(nRow).ToString
                    LLD = Me.C1TrueDBGrid1.Columns("LOADDETAIL").CellValue(nRow).ToString
                    TYP = Me.C1TrueDBGrid1.Columns("OP").CellValue(nRow).ToString

                    C1TrueDBGrid2.BackColor = System.Drawing.Color.CadetBlue

                    If LLD = "F" Then
                        DS = VeriOku(OBJ, MID, False)
                    Else
                        DS = VeriOku(OBJ, MID, True)
                    End If

                    If DS.Tables.Count > 0 Then

                        C1TrueDBGrid2.DataSource = DS.Tables(CLL)
                        C1TrueDBGrid2.Refresh()

                        Select Case TYP
                            Case "I"
                                ' insert
                                Res = VeriIns(DS, OBJ, MID).Trim
                                Res = Res.Replace("'", "")

                                If Res = "" Then
                                    Msj = "Ok"
                                    sorgu = NSrg + "xp_error = '" + Msj + "' where id = " + IID
                                Else
                                    Msj = Res
                                    sorgu = HSrg + "xp_error = '" + Msj + "' where id = " + IID
                                End If

                                oKaynak.PGExecuteOpenCloseConnection(sorgu, cErrorMessage)

                            Case "D"
                                ' delete
                                Res = VeriSil(OBJ, MID).Trim
                                Res = Res.Replace("'", "")

                                If Res.Length = 0 Then
                                    Msj = "Ok"
                                    sorgu = NSrg + "xp_error = '" + Msj + "' where id = " + IID
                                Else
                                    Msj = Res
                                    sorgu = HSrg + "xp_error = '" + Msj + "' where id = " + IID
                                End If

                                oKaynak.PGExecuteOpenCloseConnection(sorgu, cErrorMessage)

                            Case "U"
                                lOK = True

                                ' delete
                                Res = VeriSil(OBJ, MID).Trim()
                                Res = Res.Replace("'", "")

                                If Res.Length = 0 Then
                                    Msj = "Ok"
                                    sorgu = NSrg + "xp_error = '" + Msj + "' where id = " + IID
                                Else
                                    Msj = Res
                                    sorgu = HSrg + "xp_error = '" + Msj + "' where id = " + IID
                                    lOK = False
                                End If

                                oKaynak.PGExecuteOpenCloseConnection(sorgu, cErrorMessage)

                                If lOK Then
                                    ' insert
                                    Res = VeriIns(DS, OBJ, MID).Trim
                                    Res = Res.Replace("'", "")

                                    If Res = "" Then
                                        Msj = "Ok"
                                        sorgu = NSrg + "xp_error = '" + Msj + "' where id = " + IID
                                    Else
                                        Msj = Res
                                        sorgu = HSrg + "xp_error = '" + Msj + "' where id = " + IID
                                    End If

                                    oKaynak.PGExecuteOpenCloseConnection(sorgu, cErrorMessage)
                                End If
                        End Select
                    Else
                        'MessageBox.Show(CLL + " Veri Gelmedi")
                    End If
                End If

                C1SuperLabel2.Text = "Aktarılan Satır " + nRow.ToString
                C1SuperLabel2.Refresh()

            Next

            C1Button4.Enabled = False
            MessageBox.Show("İşlem Tamamlandı")

        Catch ex As Exception
            ErrDisp(ex, Me.Name)
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
            ErrDisp(ex, Me.Name)
        End Try
    End Function
End Class