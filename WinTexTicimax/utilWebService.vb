Option Explicit On
Option Strict On

Imports System
Imports System.Globalization
Imports System.Net
Imports System.ServiceModel

Module utilWebService

    Public Function GetEndpointAddress(cURL As String) As EndpointAddress

        GetEndpointAddress = Nothing

        Try
            Dim oEndPointAddress = New System.ServiceModel.EndpointAddress(cURL.Trim)
            GetEndpointAddress = oEndPointAddress

        Catch ex As Exception
            ErrDisp(ex.Message, "GetEndpointAddress",,, ex)
        End Try
    End Function

    Public Function GetBinding(cBindingName As String) As BasicHttpBinding

        GetBinding = Nothing

        Try
            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim oBinding = New System.ServiceModel.BasicHttpBinding()
            oBinding.Name = cBindingName.Trim
            oBinding.Security.Mode = ServiceModel.BasicHttpSecurityMode.Transport
            oBinding.SendTimeout = New TimeSpan(0, 4, 50, 0, 0)
            oBinding.MaxReceivedMessageSize = 640000000
            oBinding.Security.Transport.ClientCredentialType = ServiceModel.HttpClientCredentialType.None

            GetBinding = oBinding

        Catch ex As Exception
            ErrDisp(ex.Message, "GetBinding",,, ex)
        End Try
    End Function

    Public Function WebReadString(oValue As Object) As String

        WebReadString = ""

        On Error Resume Next

        If IsNothing(oValue) Then Exit Function
        WebReadString = oValue.ToString.Trim

    End Function

    Public Function WebReadInt(oValue As Object) As Integer

        WebReadInt = 0

        On Error Resume Next

        If IsNothing(oValue) Then Exit Function
        If Not IsNumeric(oValue) Then Exit Function
        WebReadInt = CInt(oValue)

    End Function

    Public Function WebReadDouble(oValue As Object) As Double

        WebReadDouble = 0

        On Error Resume Next

        If IsNothing(oValue) Then Exit Function
        If Not IsNumeric(oValue) Then Exit Function
        WebReadDouble = CDbl(oValue)

    End Function

    Public Function WebReadDate(oValue As Object, nCase As Integer) As Date

        Dim cTarih As String = ""
        Dim oProvider As CultureInfo = CultureInfo.InvariantCulture

        WebReadDate = #1/1/1950#

        On Error Resume Next

        If IsNothing(oValue) Then Exit Function

        cTarih = oValue.ToString
        cTarih = Replace(cTarih, "T", " ")
        cTarih = Mid(cTarih, 1, 19)

        Select Case nCase
            Case 1
                ' mng
                WebReadDate = DateTime.ParseExact(cTarih, "dd-MM-yyyy HH:mm:ss", oProvider)
            Case 2
                ' byexpress
                WebReadDate = DateTime.ParseExact(cTarih, "yyyy-MM-dd HH:mm:ss", oProvider)
            Case 3
                ' ticimax
                WebReadDate = DateTime.ParseExact(cTarih, "dd.MM.yyyy HH:mm:ss", oProvider)
        End Select

    End Function

End Module
