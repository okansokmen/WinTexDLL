Option Explicit On

Imports System.Xml
Imports System.Configuration
Imports System.Net
Imports System
Imports System.IO
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

' Portal ve API 
' https://takipsan.takipsanplus.com/login                            
' username : eroglumisir        
' password : 121312

' Login Service : (GET)
' URL: https://api.takipsanplus.com/read/login?username={{username}}&password={{password}}

' ScannedCount Service : (GET)
' URL: https://api.takipsanplus.com/read/scannedCount?token={{token}}&poNumber={{poNumber}}

Module utilTakipsan

    ' Login
    Public Class RootObject1
        Public Property status As String
        Public Property data As DataObject1
        Public Property errorMessage As String
    End Class

    Public Class DataObject1
        Public Property token As String
    End Class

    ' Siparis
    Public Class RootObject2
        Public Property status As String
        Public Property data As List(Of Consignment)
        Public Property errorMessage As String
    End Class

    Public Class Consignment
        Public Property consignmentName As String
        Public Property targetQuantity As Integer
        Public Property scannedQuantity As Integer
    End Class

    Public Function TakipsanGetInfo2(cSiparisNo As String, Optional cUsername As String = "eroglumisir", Optional cPassword As String = "121312",
                                     Optional ByRef nTotalTargetQuantity As Double = 0, Optional ByRef nTotalScannedQuantity As Double = 0,
                                     Optional ByRef cErrorMessage As String = "") As Boolean
        TakipsanGetInfo2 = False

        Try
            Dim cDurum As String = ""
            Dim cURL As String = ""
            Dim cToken As String = ""
            Dim cLogin As String = ""
            Dim cConsignmentName As String = ""
            Dim nTargetQuantity As Double = 0
            Dim nScannedQuantity As Double = 0

            cURL = "https://api.takipsanplus.com/read/login?username=" + cUsername.Trim + "&password=" + cPassword.Trim
            cLogin = GetWSData(cURL, cErrorMessage)

            If cErrorMessage.Trim <> "" Then
                cErrorMessage = "Dikkat : " + cURL + vbCrLf + cErrorMessage
                Exit Function
            End If

            Dim result1 As RootObject1 = JsonConvert.DeserializeObject(Of RootObject1)(cLogin)

            If IsNothing(result1) Then
                cErrorMessage = "Hatali URL " + cURL + vbCrLf + "Servis cevabi : " + cLogin
                Exit Function
            End If

            If result1.status.ToString.Trim <> "success" Then
                cErrorMessage = result1.errorMessage.ToString.Trim
                Exit Function
            End If

            cToken = result1.data.token.ToString.Trim

            cURL = "https://api.takipsanplus.com/read/scannedCount?token=" + cToken.Trim + "&poNumber=" + cSiparisNo.Trim
            cDurum = GetWSData(cURL, cErrorMessage)

            If cErrorMessage.Trim <> "" Then
                cErrorMessage = "Dikkat : " + cURL + vbCrLf + cErrorMessage
                Exit Function
            End If

            Dim result2 As RootObject2 = JsonConvert.DeserializeObject(Of RootObject2)(cDurum)

            If IsNothing(result2) Then
                cErrorMessage = "Hatali URL " + cURL
                Exit Function
            End If

            If result2.status.ToString.Trim <> "success" Then
                cErrorMessage = result2.errorMessage.ToString.Trim
                Exit Function
            End If

            For Each consignment As Consignment In result2.data

                nTargetQuantity = 0
                nScannedQuantity = 0

                cConsignmentName = consignment.consignmentName.ToString.Trim

                If IsNumeric(consignment.targetQuantity) Then
                    nTargetQuantity = CDbl(consignment.targetQuantity)
                End If

                If IsNumeric(consignment.scannedQuantity) Then
                    nScannedQuantity = CDbl(consignment.scannedQuantity)
                End If

                nTotalTargetQuantity = nTotalTargetQuantity + nTargetQuantity
                nTotalScannedQuantity = nTotalScannedQuantity + nScannedQuantity
            Next

            TakipsanGetInfo2 = True

        Catch ex As Exception
            cErrorMessage = ex.Message.ToString.Trim
            ErrDisp("TakipsanGetInfo2: " & ex.Message, "utilTakipsan",,, ex)
        End Try
    End Function

    Private Function GetWSData(cURL As String, Optional ByRef cErrorMessage As String = "") As String

        GetWSData = ""

        Try
            'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType) ' Tls12

            Dim userAgent As String = "Mozilla/5.0 (Windows; U; Windows NT 5.1; tr; rv:1.9.0.6) Gecko/2009011913 Firefox/3.0.6"
            Dim request As HttpWebRequest = CType(WebRequest.Create(cURL), HttpWebRequest)
            request.UserAgent = userAgent
            request.Method = "GET"

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Return reader.ReadToEnd()
                End Using
            End Using

        Catch ex As Exception
            cErrorMessage = ex.Message
            ErrDisp("GetWSData: " & ex.Message, "utilTakipsan",,, ex)
        End Try
    End Function

End Module
