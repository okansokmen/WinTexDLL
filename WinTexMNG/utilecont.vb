Option Explicit On

Imports System.Net
Imports RestSharp

' Bulgaristan EKOHT Kargo Firması Entegrasyonu
' https://www.econt.com/developers/

' test sistemi
' http://demo.econt.com/ee/services/
' username: iasp-dev
' password: iasp-dev 

' gerçek sistem
' http://ee.econt.com/services/
' username: leylabgood@gmail.com
' password: 12345654321Aq.

Module utilecont

    Private Const URL_Nomenclatures As String = "http://ee.econt.com/services/Nomenclatures"

    Public Sub eCont_GetCities(cCountryCode3 As String)

        Try
            Dim oCities As New eCont_Cities
            Dim oParameter As Parameter

            Dim oClient = New RestClient(URL_Nomenclatures)

            oClient.Authenticator = New RestSharp.Authenticators.HttpBasicAuthenticator(oConnection.cEContUserName, oConnection.cEContPassword)
            oClient.Timeout = -1

            Dim oRequest = New RestRequest("NomenclaturesService.getCities.json", Method.GET)

            oRequest.AddHeader("cache-control", "no-cache")
            oRequest.AddHeader("content-type", "application/json")

            oParameter = New Parameter("countryCode", cCountryCode3, ParameterType.QueryString)
            oParameter.DataFormat = DataFormat.Json
            oRequest.AddParameter(oParameter)

            Dim oResponse As IRestResponse = oClient.Execute(oRequest)

            If oResponse.StatusCode = HttpStatusCode.OK Then
                oCities = New System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(Of eCont_Cities)(oResponse.Content)
            End If

        Catch ex As Exception
            ErrDisp("eCont_GetCities : " + ex.Message, "utilecont",,, ex)
        End Try
    End Sub

    Public Sub eCont_GetCountries()

        Try

            Dim oCountries As New eCont_Countries

            Dim oClient = New RestClient(URL_Nomenclatures)

            oClient.Authenticator = New RestSharp.Authenticators.HttpBasicAuthenticator(oConnection.cEContUserName, oConnection.cEContPassword)
            oClient.Timeout = -1

            Dim oRequest = New RestRequest("NomenclaturesService.getCountries.json", Method.GET)

            oRequest.AddHeader("cache-control", "no-cache")
            oRequest.AddHeader("content-type", "application/json")

            Dim oResponse As IRestResponse = oClient.Execute(oRequest)

            If oResponse.StatusCode = HttpStatusCode.OK Then
                oCountries = New System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(Of eCont_Countries)(oResponse.Content)
            End If

        Catch ex As Exception
            ErrDisp("eCont_GetCountries : " + ex.Message, "utilecont",,, ex)
        End Try
    End Sub

    Public Class eCont_Country
        Public Property id As Object
        Public Property code2 As String
        Public Property code3 As String
        Public Property name As String
        Public Property nameEn As String
        Public Property isEU As Boolean
    End Class

    Public Class eCont_Countries
        Public Property countries As List(Of eCont_Country)
    End Class

    Public Class eCont_GeoLocation
        Public Property latitude As Double
        Public Property longitude As Double
        Public Property confidence As Integer
    End Class

    Public Class eCont_Street
        Public Property id As Integer
        Public Property cityID As Integer
        Public Property name As String
        Public Property nameEn As String
    End Class

    Public Class eCont_Quarter
        Public Property id As Integer
        Public Property cityID As Integer
        Public Property name As String
        Public Property nameEn As String
    End Class

    Public Class eCont_Cities
        Public Property cities As List(Of eCont_City)
    End Class

    Public Class eCont_City
        Public Property id As Integer
        Public Property country As eCont_Country
        Public Property postCode As String
        Public Property name As String
        Public Property nameEn As String
        Public Property regionName As String
        Public Property regionNameEn As String
        Public Property phoneCode As String
        Public Property location As eCont_GeoLocation
        Public Property expressCityDeliveries As Boolean
    End Class

    Public Class eCont_Office
        Public Property id As Integer
        Public Property code As String
        Public Property isMPS As Boolean
        Public Property isAPS As Boolean
        Public Property name As String
        Public Property nameEn As String
        Public Property phones As List(Of String)
        Public Property emails As List(Of String)
        Public Property address As eCont_Address
        Public Property info As String
        Public Property currency As String
        Public Property language As String
        Public Property normalBusinessHoursFrom As DateTime
        Public Property normalBusinessHoursTo As DateTime
        Public Property halfDayBusinessHoursFrom As DateTime
        Public Property halfDayBusinessHoursTo As DateTime
        Public Property shipmentTypes As List(Of eCont_ShipmentType)
        Public Property partnerCode As String
        Public Property hubCode As String
        Public Property hubName As String
        Public Property hubNameEn As String
    End Class

    Public Enum eCont_ShipmentType
        document ' Documents (up To 0.5kg)
        pack ' Parcel (up To 50kg)
        post_pack ' Post parcel (up To 20kg, 60x60x60cm And subcode = office-office)
        pallet ' Pallet (80x120x180cm And up To 1000kg)
        cargo '  Cargo express (palletized shipment over 80x120x180cm up To 200x200x180 And up To 500kg)
        documentpallet ' Pallet + documents
        big_letter ' Letter (big)
        small_letter ' Letter (small)
        money_transfer ' Money transfer
        pp ' Post transfer
    End Enum

    Public Class eCont_Address
        Public Property id As Integer
        Public Property city As eCont_City
        Public Property fullAddress As String
        Public Property quarter As String
        Public Property street As String
        Public Property num As String
        Public Property other As String
        Public Property location As eCont_GeoLocation
        Public Property zip As String
    End Class

End Module
