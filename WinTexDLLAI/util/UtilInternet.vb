Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml

Module UtilInternet

    Public Function GetExchangeRates(dRateDate As Date) As oExRate()

        Dim myxml As New XmlDocument
        Dim tarih As XmlNode
        Dim mylist, adi, kod As XmlNodeList
        Dim doviz_alis, doviz_satis As XmlNodeList
        Dim efektif_alis, efektif_satis As XmlNodeList
        Dim myUrl As String = ""
        Dim aExRate() As oExRate
        Dim nCnt As Integer
        Dim nDay As Integer = 0
        Dim nMonth As Integer = 0
        Dim nYear As Integer = 0

        GetExchangeRates = Nothing

        Try

            nDay = dRateDate.Day
            nMonth = dRateDate.Month
            nYear = dRateDate.Year

            myUrl = "http://www.tcmb.gov.tr/kurlar/" + _
                            Microsoft.VisualBasic.Format(nYear, "00") + Microsoft.VisualBasic.Format(nMonth, "00") + "/" + _
                            Microsoft.VisualBasic.Format(nDay, "00") + Microsoft.VisualBasic.Format(nMonth, "00") + Microsoft.VisualBasic.Format(nYear, "00") + ".xml"

            If myUrl.Trim = "" Then
                myUrl = "http://www.tcmb.gov.tr/kurlar/today.xml"
            End If

            ' Create a request for the URL. 		
            Dim request As WebRequest = WebRequest.Create(myUrl)
            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            ' proxy
            request.Proxy = New WebProxy("http://10.254.254.2:8080")
            ' Get the response.
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            ' Get the stream containing content returned by the server.
            Dim dataStream As Stream = response.GetResponseStream()
            ' load stream
            myxml.Load(dataStream)
            ' Cleanup the streams and the response.
            dataStream.Close()
            response.Close()

            nCnt = 0
            ReDim aExRate(0)

            tarih = myxml.SelectSingleNode("/Tarih_Date/@Tarih")
            mylist = myxml.SelectNodes("/Tarih_Date/Currency")
            adi = myxml.SelectNodes("/Tarih_Date/Currency/Isim")
            kod = myxml.SelectNodes("/Tarih_Date/Currency/@Kod")
            doviz_alis = myxml.SelectNodes("/Tarih_Date/Currency/ForexBuying")
            doviz_satis = myxml.SelectNodes("/Tarih_Date/Currency/ForexSelling")
            efektif_alis = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteBuying")
            efektif_satis = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteSelling")

            For i As Integer = 0 To doviz_alis.Count - 1
                ReDim Preserve aExRate(nCnt)
                aExRate(nCnt).cCinsi = adi.Item(i).InnerText.ToString()
                aExRate(nCnt).cKisaltma = kod.Item(i).InnerText.ToString()
                If IsNumeric(doviz_alis.Item(i).InnerText.ToString()) Then
                    aExRate(nCnt).nAlis = CDbl(Replace(doviz_alis.Item(i).InnerText.ToString(), ".", ","))
                Else
                    aExRate(nCnt).nAlis = 0
                End If
                If IsNumeric(doviz_satis.Item(i).InnerText.ToString()) Then
                    aExRate(nCnt).nSatis = CDbl(Replace(doviz_satis.Item(i).InnerText.ToString(), ".", ","))
                Else
                    aExRate(nCnt).nSatis = 0
                End If
                If IsNumeric(efektif_alis.Item(i).InnerText.ToString()) Then
                    aExRate(nCnt).nEfektifAlis = CDbl(Replace(efektif_alis.Item(i).InnerText.ToString(), ".", ","))
                Else
                    aExRate(nCnt).nEfektifAlis = 0
                End If
                If IsNumeric(efektif_satis.Item(i).InnerText.ToString()) Then
                    aExRate(nCnt).nEfektifSatis = CDbl(Replace(efektif_satis.Item(i).InnerText.ToString(), ".", ","))
                Else
                    aExRate(nCnt).nEfektifSatis = 0
                End If
                nCnt = nCnt + 1
            Next

            Return aExRate

        Catch ex As Exception
            ' DO NOTHİNG
            ' ErrDisp("GetExchangeRates : " + ex.Message, myUrl)
        End Try
    End Function

    Public Function GetWEBPage(cWEBPageAddress As String) As String
        Try
            GetWEBPage = ""
            ' Create a request for the URL. 		
            Dim request As WebRequest = WebRequest.Create(cWEBPageAddress)
            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            ' Get the response.
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            ' Display the status.
            'Console.WriteLine(response.StatusDescription)
            ' Get the stream containing content returned by the server.
            Dim dataStream As Stream = response.GetResponseStream()
            ' Open the stream using a StreamReader for easy access.
            Dim reader As New StreamReader(dataStream)
            ' Read the content.
            GetWEBPage = reader.ReadToEnd()
            ' Display the content.
            'Console.WriteLine(responseFromServer)
            ' Cleanup the streams and the response.
            reader.Close()
            dataStream.Close()
            response.Close()
        Catch ex As Exception
            GetWEBPage = ""
            'ErrDisp("GetWEBPage : " + ex.Message, "UtilInternet", cWEBPageAddress)
        End Try
    End Function
End Module
