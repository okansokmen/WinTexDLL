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
' Eroðlu Aksaray Tekstil
' Admin
' username: erogluaksarayadmin
' password: erogluaksaray121312
' Üretici
' username: erogluaksaray
' password: 121312

' Eroðlu Mýsýr Tekstil
' Admin
' username: eroglumisiradmin
' password: eroglumisir121312
' Üretici
' username: eroglumisir
' password: 121312

' Login Service : (GET)
' URL: https://api.takipsanplus.com/read/login?username={{username}}&password={{password}}

' ScannedCount Service : (GET)
' URL: https://api.takipsanplus.com/read/scannedCount?token={{token}}&poNumber={{poNumber}}

Module Program

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

    Public Sub Main()
        Try
            Dim args As String() = Environment.GetCommandLineArgs()
            Dim cDurum As String = ""
            Dim cURL As String = ""
            Dim cToken As String = ""
            Dim cLogin As String = ""
            Dim cErrorMessage As String = ""
            Dim cConsignmentName As String = ""
            Dim nTargetQuantity As Double = 0
            Dim nScannedQuantity As Double = 0
            Dim nTotalTargetQuantity As Double = 0
            Dim nTotalScannedQuantity As Double = 0
            Dim cSiparisNo As String = "18931.b-1/Zara" ' 17068-1/Zara
            Dim cUsername As String = "eroglumisir"
            Dim cPassword As String = "121312"
            Dim oSQL As SQLServerClass

            If args.Length = 1 Then
                oConnection.cServer = "monster"
                oConnection.cDatabase = "tes"
                oConnection.cUser = "sa"
                oConnection.cPassword = "Hayabusa1024"
            Else
                oConnection.cServer = args(1).ToString.Trim
                oConnection.cDatabase = args(2).ToString.Trim
                oConnection.cUser = args(3).ToString.Trim
                oConnection.cPassword = args(4).ToString.Trim
                cSiparisNo = args(5).ToString.Trim
                cUsername = args(6).ToString.Trim
                cPassword = args(7).ToString.Trim
            End If

            oConnection.cConnStr = "Data Source=" + oConnection.cServer + ";" +
                                    "Initial Catalog=" + oConnection.cDatabase + ";" +
                                    "uid=" + oConnection.cUser + ";" +
                                    "pwd=" + oConnection.cPassword + ""

            cURL = "https://api.takipsanplus.com/read/login?username=" + cUsername + "&password=" + cPassword
            cLogin = GetWSData(cURL)

            Dim result1 As RootObject1 = JsonConvert.DeserializeObject(Of RootObject1)(cLogin)

            If IsNothing(result1) Then
                Exit Sub
            End If

            If result1.status.ToString.Trim <> "success" Then
                Exit Sub
            End If


            cToken = result1.data.token.ToString.Trim
            cErrorMessage = result1.errorMessage.ToString.Trim

            cURL = "https://api.takipsanplus.com/read/scannedCount?token=" + cToken.Trim + "&poNumber=" + cSiparisNo.Trim
            cDurum = GetWSData(cURL)

            Dim result2 As RootObject2 = JsonConvert.DeserializeObject(Of RootObject2)(cDurum)

            If IsNothing(result2) Then
                Exit Sub
            End If

            If result2.status.ToString.Trim <> "success" Then
                Exit Sub
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

            If nTotalScannedQuantity > 0 Then
                oSQL = New SQLServerClass
                oSQL.OpenConn()
                oSQL.cSQLQuery = "insert takipsanscannedcount (siparisno, targetquantity, scannedquantity, modificationdate) " +
                                " values ('" + cSiparisNo.Trim + "' , " +
                                SQLWriteDecimal(nTotalTargetQuantity) + " , " +
                                SQLWriteDecimal(nTotalScannedQuantity) + " , " +
                                " getdate() ) "
                oSQL.SQLExecute()
                oSQL.CloseConn()
            End If

            'Console.ReadLine()

        Catch ex As Exception
            ErrDisp("Main: " & ex.Message, "Program",,, ex)
        End Try
    End Sub

    Private Function GetWSData(cURL As String) As String

        GetWSData = ""

        Try
            Dim cSonuc As String = ""
            Dim userAgent As String = "Mozilla/5.0 (Windows; U; Windows NT 5.1; tr; rv:1.9.0.6) Gecko/2009011913 Firefox/3.0.6"

            SPMAyar()

            Dim request As HttpWebRequest = CType(WebRequest.Create(cURL), HttpWebRequest)
            request.UserAgent = userAgent
            request.Method = "GET"

            Console.WriteLine("HTTP Sorgu : " + cURL)

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    cSonuc = reader.ReadToEnd()
                    Console.WriteLine("HTTP Cevap : " + cSonuc)
                    Return cSonuc
                End Using
            End Using

        Catch ex As Exception
            ErrDisp("GetWSData: " & ex.Message, "GetWSData",,, ex)
        End Try
    End Function

    Private Sub SPMAyar()
        Try
            ' ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3
            ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True

        Catch ex As Exception
            ErrDisp("SPMAyar: " & ex.Message, "SPMAyar",,, ex)
        End Try
    End Sub

End Module
