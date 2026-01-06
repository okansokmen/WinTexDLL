Option Explicit On

Imports System.Net
Imports RestSharp

'Panel Bilgileri
'https://smslogin.nac.com.tr/
'k.adı : benimpabucum
'şifre: y6nEA9mk
'sms başlığı :  02129459535
'Api Dökümantasyonu : https://smslogin.nac.com.tr/api-docs/?id=eyJwcm9kdWN0IjoiTkFDIFRlbGVrb20iLCJkb21haW4iOiJzbXNsb2dpbi5uYWMuY29tLnRyIn0=

Module utilNacSMS

    Public Function NacGetCredit() As Double

        NacGetCredit = -2

        Try
            Dim oDict1 As New Dictionary(Of String, Object)
            Dim oDict2 As New Dictionary(Of String, Object)

            Dim oClient = New RestClient("http://smslogin.nac.com.tr:9587/user/credit")
            oClient.Authenticator = New RestSharp.Authenticators.HttpBasicAuthenticator(oConnection.cNacUserName, oConnection.cNacPassword)

            Dim oRequest = New RestRequest(Method.GET)

            oRequest.AddHeader("cache-control", "no-cache")
            oRequest.AddHeader("content-type", "application/json")

            Dim oResponse As IRestResponse = oClient.Execute(oRequest)

            If oResponse.StatusCode = HttpStatusCode.OK Then
                oDict1 = New System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(Of Object)(oResponse.Content)
                oDict2 = oDict1.Item("data")
                NacGetCredit = CDbl(oDict2.Item("credit"))
            Else
                NacGetCredit = -1
            End If

        Catch ex As Exception
            ErrDisp("NacGetCredit",,,, ex)
        End Try
    End Function

    Public Function NacSendSms(cTelNo As String, Optional cTitle As String = "", Optional cMessage As String = "", Optional ByRef nPkgID As Double = 0) As Boolean

        NacSendSms = False
        nPkgID = 0

        Try
            Dim oDict1 As New Dictionary(Of String, Object)
            Dim oDict2 As New Dictionary(Of String, Object)
            Dim oParameter As Parameter
            Dim oClient = New RestClient("http://smslogin.nac.com.tr:9587/sms/create")
            oClient.Authenticator = New RestSharp.Authenticators.HttpBasicAuthenticator(oConnection.cNacUserName, oConnection.cNacPassword)

            Dim oRequest = New RestRequest(Method.POST)

            oRequest.AddHeader("cache-control", "no-cache")
            oRequest.AddHeader("content-type", "application/json")

            oParameter = New Parameter("type", 1, ParameterType.QueryString)
            oParameter.DataFormat = DataFormat.Json
            oRequest.AddParameter(oParameter)

            oParameter = New Parameter("sendingType", 0, ParameterType.QueryString)
            oParameter.DataFormat = DataFormat.Json
            oRequest.AddParameter(oParameter)

            oParameter = New Parameter("title", cTitle, ParameterType.QueryString)
            oParameter.DataFormat = DataFormat.Json
            oRequest.AddParameter(oParameter)

            oParameter = New Parameter("content", cMessage, ParameterType.QueryString)
            oParameter.DataFormat = DataFormat.Json
            oRequest.AddParameter(oParameter)

            oParameter = New Parameter("validity", 60, ParameterType.QueryString)
            oParameter.DataFormat = DataFormat.Json
            oRequest.AddParameter(oParameter)

            oParameter = New Parameter("encoding", 0, ParameterType.QueryString)
            oParameter.DataFormat = DataFormat.Json
            oRequest.AddParameter(oParameter)

            oParameter = New Parameter("number", cTelNo, ParameterType.QueryString)
            oParameter.DataFormat = DataFormat.Json
            oRequest.AddParameter(oParameter)

            oParameter = New Parameter("sender", oConnection.cNacSmsBasligi, ParameterType.QueryString)
            oParameter.DataFormat = DataFormat.Json
            oRequest.AddParameter(oParameter)

            Dim oResponse As IRestResponse = oClient.Execute(oRequest)

            If oResponse.StatusCode = HttpStatusCode.OK Then
                If Not IsNothing(oResponse.Content) Then
                    oDict1 = New System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(Of Object)(oResponse.Content)
                    If Not IsNothing(oDict1.Item("data")) Then
                        oDict2 = oDict1.Item("data")
                        If Not IsNothing(oDict2.Item("pkgID")) Then
                            nPkgID = CDbl(oDict2.Item("pkgID"))
                            NacSendSms = True
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrDisp("NacSendSms",,,, ex)
        End Try

    End Function

End Module
