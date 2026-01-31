Option Explicit On
Option Strict On

Imports System.Xml

Module UtilKur
    Public Function MBKur(Optional dTarih As Date = #1/1/1950#) As Boolean

        MBKur = False

        Dim cSQL As String = ""

        Try
            Dim oSQL1 As New SQLServerClass

            Dim cUrl As String = ""
            Dim myxml As New XmlDocument
            Dim i As Integer = 0

            Dim tarih As XmlNode
            Dim mylist, adi, kod As XmlNodeList
            Dim doviz_alis, doviz_satis As XmlNodeList
            Dim efektif_alis, efektif_satis As XmlNodeList

            Dim cAdi As String = ""
            Dim cKodu As String = ""
            Dim cAlis As String = ""
            Dim cSatis As String = ""
            Dim cEfektifAlis As String = ""
            Dim cEfektifSatis As String = ""

            Dim nAlis As Double = 0
            Dim nSatis As Double = 0
            Dim nEfektifAlis As Double = 0
            Dim nEfektifSatis As Double = 0

            If dTarih = #1/1/1950# Then
                dTarih = Now.Date
            End If

            If dTarih > Now.Date Then
                Exit Function
            ElseIf dTarih = Now.Date Then
                If TimeOfDay.ToString("hh:mm:ss") > "15:30:00" Then
                    ' yarinin kuru yayinlandi, dünü oku
                    dTarih = DateAdd(DateInterval.Day, -1, dTarih)
                Else
                    ' bugunun kuru
                End If
            ElseIf dTarih < Now.Date Then
                dTarih = DateAdd(DateInterval.Day, -1, dTarih)
            End If

            If dTarih = Now.Date Then
                cUrl = "http://www.tcmb.gov.tr/kurlar/today.xml"
            Else
                Do While True
                    ' http://www.tcmb.gov.tr/kurlar/YYYYAA/GGAAYYYY.xml
                    cUrl = "http://www.tcmb.gov.tr/kurlar/" +
                            CStr(Format(Year(dTarih), "0000")) + CStr(Format(Month(dTarih), "00")) + "/" +
                            CStr(Format(Day(dTarih), "00")) + CStr(Format(Month(dTarih), "00")) + CStr(Format(Year(dTarih), "0000")) + ".xml"

                    If CheckURL(cUrl) Then
                        Exit Do
                    Else
                        dTarih = DateAdd(DateInterval.Day, -1, dTarih)
                    End If
                Loop
            End If

            myxml.Load(cUrl)

            tarih = myxml.SelectSingleNode("/Tarih_Date/Tarih")
            mylist = myxml.SelectNodes("/Tarih_Date/Currency")
            adi = myxml.SelectNodes("/Tarih_Date/Currency/Isim")
            kod = myxml.SelectNodes("/Tarih_Date/Currency/@Kod")
            doviz_alis = myxml.SelectNodes("/Tarih_Date/Currency/ForexBuying")
            doviz_satis = myxml.SelectNodes("/Tarih_Date/Currency/ForexSelling")
            efektif_alis = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteBuying")
            efektif_satis = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteSelling")

            MBKur = True

            oSQL1.OpenConn()

            For i = 0 To 18
                cAdi = adi.Item(i).InnerText.ToString().Replace(".", ",")
                cKodu = kod.Item(i).InnerText.ToString().Replace(".", ",")
                cAlis = doviz_alis.Item(i).InnerText.ToString().Replace(".", ",")
                cSatis = doviz_satis.Item(i).InnerText.ToString().Replace(".", ",")
                cEfektifAlis = efektif_alis.Item(i).InnerText.ToString().Replace(".", ",")
                cEfektifSatis = efektif_satis.Item(i).InnerText.ToString().Replace(".", ",")

                nAlis = 0
                nSatis = 0
                nEfektifAlis = 0
                nEfektifSatis = 0

                If IsNumeric(cAlis) Then nAlis = CDbl(cAlis)
                If IsNumeric(cSatis) Then nSatis = CDbl(cSatis)
                If IsNumeric(cEfektifAlis) Then nEfektifAlis = CDbl(cEfektifAlis)
                If IsNumeric(cEfektifSatis) Then nEfektifSatis = CDbl(cEfektifSatis)

                cSQL = "select distinct doviz " +
                        " from doviz with (NOLOCK)  " +
                        " where doviz is not null " +
                        " and doviz not in ('','YTL','TL','TRY') " +
                        " and doviz = '" + cKodu.Trim + "' "

                If oSQL1.CheckExists(cSQL) Then

                    If Not DovizKuruYaz(dTarih, cKodu.Trim, "Kur", nAlis) Then
                        'oWinTexServiceMain.CreateLog("Döviz alış kuru hatası " + cKodu.Trim + " " + dTarih.ToString + " " + cAlis.ToString, 2)
                        MBKur = False
                    End If

                    If Not DovizKuruYaz(dTarih, cKodu.Trim, "Satis Kuru", nSatis) Then
                        'oWinTexServiceMain.CreateLog("Döviz Satis kuru hatası " + cKodu.Trim + " " + dTarih.ToString + " " + cSatis.ToString, 2)
                        MBKur = False
                    End If

                    If Not DovizKuruYaz(dTarih, cKodu.Trim, "Efektif Alis Kuru", nEfektifAlis) Then
                        'oWinTexServiceMain.CreateLog("Döviz Efektif Alis kuru hatası " + cKodu.Trim + " " + dTarih.ToString + " " + cEfektifAlis.ToString, 2)
                        MBKur = False
                    End If

                    If Not DovizKuruYaz(dTarih, cKodu.Trim, "Efektif Satis Kuru", nEfektifSatis) Then
                        'oWinTexServiceMain.CreateLog("Döviz Efektif Satis kuru hatası " + cKodu.Trim + " " + dTarih.ToString + " " + cEfektifSatis.ToString, 2)
                        MBKur = False
                    End If
                End If
            Next

            oSQL1.CloseConn()
            oSQL1 = Nothing

        Catch ex As Exception
            MBKur = False
            ErrDisp(ex, "MBKur", cSQL)
        End Try
    End Function

    Private Function DovizKuruYaz(dTarih As Date, cDoviz As String, cKurCinsi As String, nKur As Double) As Boolean

        DovizKuruYaz = False

        Dim cSQL As String = ""
        Dim oSQL1 As New SQLServerClass
        Dim oSQL2 As New SQLServerClass
        Dim cTarih As String = ""

        Try

            If dTarih = #1/1/1950# Or cDoviz.Trim = "" Or cKurCinsi.Trim = "" Or nKur = 0 Then Exit Function

            If Not oSQL1.OpenConn() Then Exit Function
            If Not oSQL2.OpenConn() Then Exit Function

            cTarih = CStr(Format(Day(dTarih), "00")) + "." + CStr(Format(Month(dTarih), "00")) + "." + CStr(Format(Year(dTarih), "0000"))

            cSQL = "set dateformat dmy " +
                    " select doviz " +
                    " from dovkur with (NOLOCK) " +
                    " where tarih = '" + cTarih + "' " +
                    " and doviz = '" + cDoviz.Trim + "' " +
                    " and kurcinsi = '" + cKurCinsi.Trim + "' "

            If oSQL1.CheckExists(cSQL) Then

                cSQL = "set dateformat dmy " +
                        " update dovkur " +
                        " set kur = " + SQLWriteDecimal(nKur) +
                        " where tarih = '" + cTarih + "' " +
                        " and doviz = '" + cDoviz.Trim + "' " +
                        " and kurcinsi = '" + cKurCinsi.Trim + "' "

                oSQL2.SQLExecute(cSQL)
            Else
                cSQL = "set dateformat dmy " +
                        " insert dovkur (tarih, doviz, kurcinsi, kur) " +
                        " values ('" + cTarih + "', " +
                        " '" + SQLWriteString(cDoviz) + "', " +
                        " '" + SQLWriteString(cKurCinsi) + "', " +
                         SQLWriteDecimal(nKur) + " ) "

                oSQL2.SQLExecute(cSQL)
            End If

            oSQL2.CloseConn()
            oSQL1.CloseConn()

            oSQL1 = Nothing
            oSQL2 = Nothing

            DovizKuruYaz = True

        Catch ex As Exception
            ErrDisp(ex, "DovizKuruYaz", cSQL)
        End Try
    End Function

End Module
