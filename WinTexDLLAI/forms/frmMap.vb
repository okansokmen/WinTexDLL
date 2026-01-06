Option Explicit On
Option Strict On

Imports System.Text
Imports DevExpress.XtraMap
Imports System.Collections.Generic
Imports System.Drawing
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Microsoft.SqlServer.Server
Imports Microsoft.InteropFormTools

<InteropForm()> Public Class frmMap
    Private Const cBingKey As String = "Aj8cFwZVTzW2oMr23OUtMXfzg9LjozZC_hOEGZV8nOS9toKyJMsp7IHTBGHkabTM"

    Dim oGeocodeProvider As BingGeocodeDataProvider
    Dim oinfoLayer As InformationLayer
    Dim oGeoCodeLayer As InformationLayer
    Dim oitemsLayer As VectorItemsLayer
    Dim ostorage As MapItemStorage
    Dim oLocations() As MapItem
    Dim cSQLLocations As String
    Dim routeProvider As BingRouteDataProvider
    Dim searchProvider As BingSearchDataProvider

    Private Const msgMinMaxErrorFormatString As String = "The {0} must be less than or equal to {2} and greater than or equal to {1}. Correct the input value."
    Private Const latitudeName As String = "Latitude"
    Private Const minLatitude As Double = -90
    Private Const maxLatitude As Double = 90
    Private Const longitudeName As String = "Longitude"
    Private Const minLongitude As Double = -180
    Private Const maxLongitude As Double = 180

    Private Structure oPersonel
        Dim cPersonel As String
        Dim cImei As String
        Dim dTarih As Date
        Dim cLatitude As String
        Dim cLongitude As String
        Dim cAltitude As String
        Dim cUserName As String
    End Structure

    Public Sub New()
        Try
            ' This call is required by the designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.
            oGeocodeProvider = New BingGeocodeDataProvider With {.BingKey = cBingKey, .MaxVisibleResultCount = 1}
            AddHandler oGeocodeProvider.LocationInformationReceived, AddressOf OnLocationInformationReceived
            AddHandler oGeocodeProvider.LayerItemsGenerating, AddressOf OnLayerItemsGenerating

            searchProvider = New BingSearchDataProvider With {.BingKey = cBingKey}
            AddHandler searchProvider.SearchCompleted, AddressOf OnSearchCompleted

        Catch ex As Exception
            ErrDisp("frmMap New : " + ex.Message, Me.Name)
        End Try
    End Sub

    Public Sub init(Optional cSQL As String = "")
        Try
            cSQLLocations = cSQL.Trim
            Me.Show()
        Catch ex As Exception
            ErrDisp("init : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub frmMap_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            RestoreLayoutFromRegistry()

            oGeoCodeLayer = New InformationLayer
            MapControl1.Layers.Add(oGeoCodeLayer)
            oGeoCodeLayer.DataProvider = oGeocodeProvider
            oGeocodeProvider.BingKey = cBingKey

            oinfoLayer = New InformationLayer
            MapControl1.Layers.Add(oinfoLayer)

            oinfoLayer.DataProvider = searchProvider
            searchProvider.BingKey = cBingKey
            searchProvider.SearchOptions.ResultsCount = 5

            oitemsLayer = New VectorItemsLayer
            MapControl1.Layers.Add(oitemsLayer)

            ostorage = New MapItemStorage

            GetData()
            Me.WindowState = FormWindowState.Maximized

        Catch ex As Exception
            ErrDisp("frmMap_Load : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Me.Close()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        GetData
    End Sub

    Private Sub GetData()

        Dim cText As String = ""

        Try
            Dim cSQL As String = ""
            Dim ConnYage As SqlConnection = Nothing
            Dim oDr As SqlDataReader = Nothing
            Dim aPersonel() As oPersonel = Nothing
            Dim nCnt As Integer = -1
            Dim nCnt2 As Integer = -1

            ostorage.Items.Clear()
            oitemsLayer.Data = ostorage

            ConnYage = OpenConn()

            cSQL = "delete mobilelocations " +
                    " where latitude is null or latitude = '' or longitude is null or longitude = '' "

            ExecuteSQLCommandConnected(cSQL, ConnYage)

            cSQL = "delete mobilelocations " +
                    " where Not (latitude Like '%.%' or longitude like '%.%') "

            ExecuteSQLCommandConnected(cSQL, ConnYage)

            If cSQLLocations = "" Then
                cSQL = "SELECT DISTINCT b.personel, b.imei, b.username " +
                        " FROM mobilelocations a with (NOLOCK) , personel b with (NOLOCK) " +
                        " WHERE (a.imei = b.imei or a.username = b.username) " +
                        " and b.personel is not null " +
                        " and b.personel <> '' " +
                        " and a.latitude is not null " +
                        " and a.latitude <> '' " +
                        " and a.longitude is not null " +
                        " and a.longitude <> '' "

                oDr = GetSQLReader(cSQL, ConnYage)

                Do While oDr.Read
                    nCnt = nCnt + 1
                    ReDim Preserve aPersonel(nCnt)
                    aPersonel(nCnt).cPersonel = SQLReadString(oDr, "personel")
                    aPersonel(nCnt).cImei = SQLReadString(oDr, "imei")
                    aPersonel(nCnt).cUserName = SQLReadString(oDr, "username")
                Loop
                oDr.Close()

                If nCnt > -1 Then
                    For nCnt = 0 To UBound(aPersonel)
                        cSQL = "SELECT TOP 1 latitude, longitude, altitude, tarih, username " +
                                " FROM mobilelocations with (NOLOCK)  " +
                                " where (imei = '" + aPersonel(nCnt).cImei + "' or username = '" + aPersonel(nCnt).cUserName + "') " +
                                " and latitude is not null " +
                                " and latitude <> '' " +
                                " and longitude is not null " +
                                " and longitude <> '' " +
                                " ORDER BY tarih desc "

                        oDr = GetSQLReader(cSQL, ConnYage)

                        If oDr.Read Then
                            aPersonel(nCnt).cLatitude = SQLReadString(oDr, "latitude")
                            aPersonel(nCnt).cLongitude = SQLReadString(oDr, "longitude")
                            aPersonel(nCnt).cAltitude = SQLReadString(oDr, "altitude")
                            aPersonel(nCnt).dTarih = SQLReadDate(oDr, "tarih")
                        End If
                        oDr.Close()
                    Next

                    ReDim oLocations(0)
                    For nCnt = 0 To UBound(aPersonel)
                        cText = aPersonel(nCnt).cPersonel + vbCrLf + aPersonel(nCnt).dTarih.ToString

                        nCnt2 = nCnt2 + 1
                        ReDim Preserve oLocations(nCnt2)
                        oLocations(nCnt2) = New MapCallout() With {.Text = cText, .Location = New GeoPoint(CDbl(aPersonel(nCnt).cLatitude), CDbl(aPersonel(nCnt).cLongitude))}
                        oLocations(nCnt2).Fill = Color.Yellow
                    Next
                End If
            Else
                oDr = GetSQLReader(cSQLLocations, ConnYage)

                Do While oDr.Read

                    cText = SQLReadString(oDr, "aciklama1") +
                            IIf(SQLReadString(oDr, "aciklama2") = "", "", vbCrLf + SQLReadString(oDr, "aciklama2")).ToString +
                            IIf(SQLReadString(oDr, "aciklama3") = "", "", vbCrLf + SQLReadString(oDr, "aciklama3")).ToString

                    nCnt2 = nCnt2 + 1
                    ReDim Preserve oLocations(nCnt2)
                    oLocations(nCnt2) = New MapCallout() With {.Text = cText, .Location = New GeoPoint(CDbl(SQLReadString(oDr, "latitude")), CDbl(SQLReadString(oDr, "longitude")))}
                    oLocations(nCnt2).Fill = Color.Green
                Loop
                oDr.Close()
            End If

            cSQL = "select firma, latitude, longitude " +
                    " from firma with (NOLOCK)  " +
                    " where firma is not null " +
                    " and firma <> '' " +
                    " and latitude is not null " +
                    " and latitude <> '' " +
                    " and longitude is not null " +
                    " and longitude <> '' "

            oDr = GetSQLReader(cSQL, ConnYage)

            Do While oDr.Read
                nCnt2 = nCnt2 + 1
                ReDim Preserve oLocations(nCnt2)
                oLocations(nCnt2) = New MapCallout() With {.Text = SQLReadString(oDr, "firma"), .Location = New GeoPoint(CDbl(SQLReadString(oDr, "latitude")), CDbl(SQLReadString(oDr, "longitude")))}
                oLocations(nCnt2).Fill = Color.Green
            Loop
            oDr.Close()

            If nCnt2 > -1 Then
                ostorage.Items.AddRange(oLocations)
                oitemsLayer.Data = ostorage

                MapControl1.Refresh()
            End If

            ConnYage.Close()

        Catch ex As Exception
            ErrDisp("GetData : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Try
            MapControl1.ShowRibbonPrintPreview()
        Catch ex As Exception
            ErrDisp("BarButtonItem3_ItemClick : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private ReadOnly Property SearchLayer() As InformationLayer
        Get
            Return CType(MapControl1.Layers("SearchLayer"), InformationLayer)
        End Get
    End Property

    Private ReadOnly Property GeocodeLayer() As InformationLayer
        Get
            Return CType(MapControl1.Layers("GeocodeLayer"), InformationLayer)
        End Get
    End Property

    Private Sub OnLocationInformationReceived(ByVal sender As Object, ByVal e As LocationInformationReceivedEventArgs)
        If e.Cancelled = True Then
            Return
        End If
        If e.Result.ResultCode <> RequestResultCode.Success Then
            MemoEdit1.Text = "The Bing Geocode service does not work for this location."
            Return
        End If
        Dim resultList As New StringBuilder("")
        Dim resCounter As Integer = 1
        For Each locations As LocationInformation In e.Result.Locations
            resultList.Append(String.Format("Bilinen Adres {0}:" & ControlChars.CrLf, resCounter))
            resultList.Append(String.Format(locations.EntityType & ControlChars.CrLf))
            resultList.Append(String.Format(locations.Address.FormattedAddress & ControlChars.CrLf))
            resultList.Append(String.Format("Koordinatlar: {0}" & ControlChars.CrLf, locations.Location))
            resultList.Append(String.Format("___" & ControlChars.CrLf))
            resCounter += 1
        Next locations
        MemoEdit1.Text = resultList.ToString()
    End Sub

    Private Sub OnLayerItemsGenerating(ByVal sender As Object, ByVal e As LayerItemsGeneratingEventArgs)
        MapControl1.ZoomToFit(e.Items, 0.4)
    End Sub


    Private Function TryGetLocationArguments(ByRef point As GeoPoint) As Boolean
        Dim latitude As Double = Nothing
        Dim longitude As Double = Nothing
        If TryConvertLocationCoordinate(TextEdit1.Text, minLatitude, maxLatitude, latitudeName, latitude) AndAlso TryConvertLocationCoordinate(TextEdit2.Text, minLongitude, maxLongitude, longitudeName, longitude) Then
            point = New GeoPoint(latitude, longitude)
            Return True
        End If
        point = Nothing
        Return False
    End Function

    Private Function TryConvertLocationCoordinate(ByVal str As String, ByVal minValue As Double, ByVal maxValue As Double, ByVal valueName As String, ByRef value As Double) As Boolean
        Dim convertedValue As Double = If(String.IsNullOrEmpty(str), 0, Double.Parse(str))
        If (convertedValue > maxValue) OrElse (convertedValue < minValue) Then
            MessageBox.Show(String.Format(msgMinMaxErrorFormatString, valueName, minValue, maxValue))
            value = 0
            Return False
        End If
        value = convertedValue
        Return True

    End Function

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        Dim searchPoint As GeoPoint = Nothing
        If TryGetLocationArguments(searchPoint) Then
            oGeocodeProvider.RequestLocationInformation(searchPoint, 0)
        End If
    End Sub

    Private Sub frmMap_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Try
            SaveLayoutToRegistry()
        Catch ex As Exception
            ErrDisp("frmMap_Closed : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub RestoreLayoutFromRegistry()
        Try
            LayoutControl1.RestoreLayoutFromRegistry("WinTex\Settings\MapLayout")
        Catch ex As Exception
            ErrDisp("LoadLayoutFromRegistry : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub SaveLayoutToRegistry()
        Try
            LayoutControl1.SaveLayoutToRegistry("WinTex\Settings\MapLayout")
        Catch ex As Exception
            ErrDisp("SaveLayoutToRegistry : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub OnSearchCompleted(ByVal sender As Object, ByVal e As BingSearchCompletedEventArgs)
        If e.Cancelled Then
            Return
        End If
        If e.RequestResult.ResultCode <> RequestResultCode.Success Then
            MemoEdit1.Text = "The Bing Search service does not work for this location."
            Return
        End If

        Dim resultList As New StringBuilder("")
        Dim resCounter As Integer = 1
        For Each resultInfo As BingLocationInformation In e.RequestResult.SearchResults
            resultList.Append(String.Format("Ara/Bul Sonuç {0}:  " & ControlChars.CrLf, resCounter))
            resultList.Append(String.Format("Ad: {0}" & ControlChars.CrLf, resultInfo.DisplayName))
            resultList.Append(String.Format("Adres: {0}" & ControlChars.CrLf, resultInfo.Address.FormattedAddress))
            resultList.Append(String.Format("Güvenilirlik: {0}" & ControlChars.CrLf, resultInfo.Confidence))
            resultList.Append(String.Format("Koordinatlar:  {0}" & ControlChars.CrLf, resultInfo.Location))
            resultList.Append(String.Format("Uygun kod: {0}" & ControlChars.CrLf, resultInfo.MatchCode))
            resultList.Append(String.Format("___" & ControlChars.CrLf))
            resCounter += 1
        Next resultInfo
        MemoEdit1.Text = resultList.ToString()
    End Sub
End Class