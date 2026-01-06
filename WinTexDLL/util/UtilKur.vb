Option Explicit On
Option Strict On

Imports System.Xml
Imports System.Threading
Imports System.Net

Module UtilKur

    Dim myxml As XmlDocument

    Public Function CheckURL(ByVal HostAddress As String) As Boolean

        CheckURL = False

        Dim url As New System.Uri(HostAddress)
        Dim wRequest As System.Net.WebRequest
        Dim wResponse As System.Net.WebResponse
        Dim cSayfa As String = ""

        Try
            'ByPass SSL Certificate Validation Checking
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            'System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(se As Object,
            '                                                                      cert As System.Security.Cryptography.X509Certificates.X509Certificate,
            '                                                                      chain As System.Security.Cryptography.X509Certificates.X509Chain,
            '                                                                      sslerror As System.Net.Security.SslPolicyErrors) True


            wRequest = System.Net.WebRequest.Create(url)
            wResponse = wRequest.GetResponse()

            cSayfa = wResponse.ResponseUri.AbsoluteUri().ToString

            'Restore SSL Certificate Validation Checking
            System.Net.ServicePointManager.ServerCertificateValidationCallback = Nothing

            cSayfa = LCase(cSayfa)
            cSayfa = Replace(cSayfa, "https://", "")
            cSayfa = Replace(cSayfa, "http://", "")

            HostAddress = LCase(HostAddress)
            HostAddress = Replace(HostAddress, "https://", "")
            HostAddress = Replace(HostAddress, "http://", "")

            'Is the responding address the same as HostAddress to avoid false positive from an automatic redirect.
            If cSayfa = HostAddress Then 'include query strings
                CheckURL = True
            End If

            wResponse.Close()
            wRequest = Nothing

        Catch ex As Exception
            wRequest = Nothing
            'ErrDisp("CheckURL : " + ex.Message, "UtilKur",,, ex)
        End Try
    End Function

    Public Function MBKurlar() As Boolean

        Dim nYear As Integer = 0
        Dim nMonth As Integer = 0
        Dim nDay As Integer = 0
        Dim cTarih As String = ""
        Dim dTarih As Date = #1/1/1950#
        Dim dBugun As Date = Now.Date
        'Dim oSQL As New SQLServerClass
        Dim cDoviz As String = ""
        Dim nAlis As Double = 0
        Dim nSatis As Double = 0
        Dim nEfektifAlis As Double = 0
        Dim nEfektifSatis As Double = 0

        MBKurlar = True

        Try
            'oSQL.OpenConn()

            For nYear = dBugun.Year To dBugun.Year - 1 Step -1
                For nMonth = 12 To 1 Step -1
                    For nDay = 31 To 1 Step -1
                        cTarih = Trim$(CStr(nDay)) + "." + Trim$(CStr(nMonth)) + "." + Trim$(CStr(nYear))
                        If IsDate(cTarih) Then
                            dTarih = CDate(cTarih)
                            If dTarih <= dBugun Then

                                MBKur(dTarih, False)

                                'oSQL.cSQLQuery = "select distinct doviz " +
                                '                " from doviz with (NOLOCK) " +
                                '                " where doviz is not null " +
                                '                " and doviz not in ('','YTL','TL') "

                                'oSQL.GetSQLReader()

                                'Do While oSQL.oReader.Read

                                '    cDoviz = oSQL.SQLReadString("doviz")

                                '    If MBKurOku2(cDoviz, dTarih, nAlis, nSatis, nEfektifAlis, nEfektifSatis) Then
                                '        DovizKuruYaz(dTarih, cDoviz, "Kur", nAlis, "TCMBSITE")
                                '        DovizKuruYaz(dTarih, cDoviz, "Satis Kuru", nAlis, "TCMBSITE")
                                '        DovizKuruYaz(dTarih, cDoviz, "Efektif Alis Kuru", nAlis, "TCMBSITE")
                                '        DovizKuruYaz(dTarih, cDoviz, "Efektif Satis Kuru", nAlis, "TCMBSITE")
                                '    End If
                                'Loop

                                'oSQL.oReader.Close()

                                'If Not MBKur(dTarih, False) Then
                                '    MBKurlar = False
                                'End If
                            End If
                        End If
                    Next
                Next
            Next
            'oSQL.CloseConn()

        Catch ex As Exception
            MBKurlar = False
            ErrDisp("MBKurlar : " + ex.Message, "UtilKur",,, ex)
        End Try
    End Function

    Private Function KurSayfasiniYukle(cURL As String) As Boolean

        KurSayfasiniYukle = False

        Try
            myxml.Load(cURL)
            KurSayfasiniYukle = True

        Catch ex As XmlException
            ErrDisp("KurSayfasiniYukle : " + ex.Message, "UtilKur")

            'If ex.Message = WebExceptionStatus.ProtocolError Then
            '    Dim oResp As WebResponse = ex.Response
            '    Dim oSr As System.IO.Stream
            '    Dim oBuffer() As Byte
            '    oSr = oResp.GetResponseStream()
            '    Debug.Write(oSr.Read(oBuffer, 0, CInt(oSr.Length)))

            'End If

            KurSayfasiniYukle = False
        End Try
    End Function

    Public Function MBKurOku(cDoviz As String,
                             Optional dTarih As Date = #1/1/1950#,
                             Optional ByRef nAlis As Double = 0,
                             Optional ByRef nSatis As Double = 0,
                             Optional ByRef nEfektifAlis As Double = 0,
                             Optional ByRef nEfektifSatis As Double = 0) As Boolean

        ' eski rutin çalışmıyor

        Dim nMaxSearchBack As Integer = 30
        Dim nSearchBack As Integer = 1
        Dim cUrl As String = ""
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
        Dim lFound As Boolean = False

        Dim dSayfaTarihi As Date = #1/1/1950#

        MBKurOku = False

        Try
            nAlis = 0
            nSatis = 0
            nEfektifAlis = 0
            nEfektifSatis = 0

            If dTarih = #1/1/1950# Then
                dTarih = Now.Date
            End If

            myxml = New XmlDocument

            ' saat 15:30 da yayınlanan kur sayfası aslında yarının kuru
            ' http://www.tcmb.gov.tr/kurlar/today.xml

            ' istenilen günün kuru aslında 1 önceki gün yayınlanıyor
            ' bugünden önce yayınlanan bir kur sayfasına aşağıdaki şekilde ulaşıyoruz
            ' http://www.tcmb.gov.tr/kurlar/YYYYAA/GGAAYYYY.xml

            Do While True
                ' bir gün önce yayınlanan kura bak
                dSayfaTarihi = DateAdd(DateInterval.Day, -nSearchBack, dTarih)
                ' yayın sayfası adresi
                cUrl = "http://www.tcmb.gov.tr/kurlar/" +
                        CStr(Format(Microsoft.VisualBasic.DateAndTime.Year(dSayfaTarihi), "0000")) + CStr(Format(Microsoft.VisualBasic.DateAndTime.Month(dSayfaTarihi), "00")) + "/" +
                        CStr(Format(Microsoft.VisualBasic.DateAndTime.Day(dSayfaTarihi), "00")) + CStr(Format(Microsoft.VisualBasic.DateAndTime.Month(dSayfaTarihi), "00")) + CStr(Format(Microsoft.VisualBasic.DateAndTime.Year(dSayfaTarihi), "0000")) + ".xml"
                ' sayfa bulunca çık
                If CheckURL(cUrl) Then
                    lFound = True
                    Exit Do
                End If
                ' en fazla nMaxSearchBack gün geriye ara
                nSearchBack = nSearchBack + 1
                If nSearchBack > nMaxSearchBack Then
                    Exit Function
                End If
            Loop

            If Not lFound Then
                Exit Function
            End If

            If Not KurSayfasiniYukle(cUrl) Then
                Exit Function
            End If

            tarih = myxml.SelectSingleNode("/Tarih_Date/Tarih")
            mylist = myxml.SelectNodes("/Tarih_Date/Currency")
            adi = myxml.SelectNodes("/Tarih_Date/Currency/Isim")
            kod = myxml.SelectNodes("/Tarih_Date/Currency/@Kod")
            doviz_alis = myxml.SelectNodes("/Tarih_Date/Currency/ForexBuying")
            doviz_satis = myxml.SelectNodes("/Tarih_Date/Currency/ForexSelling")
            efektif_alis = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteBuying")
            efektif_satis = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteSelling")

            ' TCMB sitesinde 20 adet döviz cinsi var
            For i = 0 To 19

                cAdi = adi.Item(i).InnerText.ToString()
                cKodu = kod.Item(i).InnerText.ToString()
                cAlis = doviz_alis.Item(i).InnerText.ToString()
                cSatis = doviz_satis.Item(i).InnerText.ToString()
                cEfektifAlis = efektif_alis.Item(i).InnerText.ToString()
                cEfektifSatis = efektif_satis.Item(i).InnerText.ToString()

                If cDoviz.Trim = cKodu.Trim Then

                    nAlis = GetDouble(cAlis)
                    nSatis = GetDouble(cSatis)
                    nEfektifAlis = GetDouble(cEfektifAlis)
                    nEfektifSatis = GetDouble(cEfektifSatis)

                    MBKurOku = True

                    Exit For
                End If
            Next

        Catch ex As Exception
            MBKurOku = False
            ErrDisp("MBKurOku : " + ex.Message, "UtilKur",,, ex)
        End Try
    End Function

    Public Function MBKur(Optional dTarih As Date = #1/1/1950#, Optional lUpdate As Boolean = True) As Boolean

        MBKur = False

        Dim cSQL As String = ""

        Try
            Dim oSQL As New SQLServerClass
            Dim nMaxSearchBack As Integer = 30
            Dim nSearchBack As Integer = 1
            Dim cUrl As String = ""
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
            Dim dSayfaTarihi As Date = #1/1/1950#

            If dTarih = #1/1/1950# Then
                dTarih = Now.Date
            End If

            myxml = New XmlDocument

            ' saat 15:30 da yayınlanan kur sayfası aslında yarının kuru
            ' https://www.tcmb.gov.tr/kurlar/today.xml

            ' istenilen günün kuru aslında 1 önceki gün yayınlanıyor
            ' bugünden önce yayınlanan bir kur sayfasına aşağıdaki şekilde ulaşıyoruz
            ' https://www.tcmb.gov.tr/kurlar/YYYYAA/GGAAYYYY.xml

            Do While True
                ' bir gün önce yayınlanan kura bak
                dSayfaTarihi = DateAdd(DateInterval.Day, -nSearchBack, dTarih)
                ' yayın sayfası adresi
                cUrl = "https://www.tcmb.gov.tr/kurlar/" +
                        CStr(Format(Microsoft.VisualBasic.DateAndTime.Year(dSayfaTarihi), "0000")) + CStr(Format(Microsoft.VisualBasic.DateAndTime.Month(dSayfaTarihi), "00")) + "/" +
                        CStr(Format(Microsoft.VisualBasic.DateAndTime.Day(dSayfaTarihi), "00")) + CStr(Format(Microsoft.VisualBasic.DateAndTime.Month(dSayfaTarihi), "00")) + CStr(Format(Microsoft.VisualBasic.DateAndTime.Year(dSayfaTarihi), "0000")) + ".xml"
                ' sayfa bulunca çık
                If CheckURL(cUrl) Then
                    Exit Do
                End If
                ' en fazla nMaxSearchBack gün geriye ara
                nSearchBack = nSearchBack + 1
                If nSearchBack > nMaxSearchBack Then
                    Exit Function
                End If
            Loop

            If Not KurSayfasiniYukle(cUrl) Then
                Exit Function
            End If

            tarih = myxml.SelectSingleNode("/Tarih_Date/Tarih")
            mylist = myxml.SelectNodes("/Tarih_Date/Currency")
            adi = myxml.SelectNodes("/Tarih_Date/Currency/Isim")
            kod = myxml.SelectNodes("/Tarih_Date/Currency/@Kod")
            doviz_alis = myxml.SelectNodes("/Tarih_Date/Currency/ForexBuying")
            doviz_satis = myxml.SelectNodes("/Tarih_Date/Currency/ForexSelling")
            efektif_alis = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteBuying")
            efektif_satis = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteSelling")

            MBKur = True

            oSQL.OpenConn()

            ' TCMB sitesinde 19 adet döviz cinsi var
            For i = 0 To mylist.Count - 1
                cAdi = adi.Item(i).InnerText.ToString()
                cKodu = kod.Item(i).InnerText.ToString()
                cAlis = doviz_alis.Item(i).InnerText.ToString()
                cSatis = doviz_satis.Item(i).InnerText.ToString()
                cEfektifAlis = efektif_alis.Item(i).InnerText.ToString()
                cEfektifSatis = efektif_satis.Item(i).InnerText.ToString()

                nAlis = GetDouble(cAlis)
                nSatis = GetDouble(cSatis)
                nEfektifAlis = GetDouble(cEfektifAlis)
                nEfektifSatis = GetDouble(cEfektifSatis)

                cSQL = "select top 1 doviz " +
                        " from doviz with (NOLOCK) " +
                        " where doviz = '" + cKodu.Trim + "' " +
                        " and doviz is not null " +
                        " and doviz not in ('','YTL','TL') "

                If oSQL.CheckExists(cSQL) Then

                    If Not DovizKuruYaz(dTarih, cKodu.Trim, "Kur", nAlis, "TCMBSITE", lUpdate) Then
                        MBKur = False
                    End If

                    If Not DovizKuruYaz(dTarih, cKodu.Trim, "Satis Kuru", nSatis, "TCMBSITE", lUpdate) Then
                        MBKur = False
                    End If

                    If Not DovizKuruYaz(dTarih, cKodu.Trim, "Efektif Alis Kuru", nEfektifAlis, "TCMBSITE", lUpdate) Then
                        MBKur = False
                    End If

                    If Not DovizKuruYaz(dTarih, cKodu.Trim, "Efektif Satis Kuru", nEfektifSatis, "TCMBSITE", lUpdate) Then
                        MBKur = False
                    End If
                End If
            Next

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            MBKur = False
            ErrDisp("MBKur : " + ex.Message, "UtilKur", cSQL,, ex)
        End Try
    End Function

    Public Function DovizKuruYaz(dTarih As Date, cDoviz As String, cKurCinsi As String, nKur As Double, Optional cKaynak As String = "TCMBSITE", Optional lUpdate As Boolean = True) As Boolean

        DovizKuruYaz = False

        Dim cSQL As String = ""
        Dim oSQL1 As New SQLServerClass
        Dim oSQL2 As New SQLServerClass
        Dim cTarih As String = ""

        Try

            If dTarih = #1/1/1950# Or cDoviz.Trim = "" Or cKurCinsi.Trim = "" Or nKur = 0 Then Exit Function

            If Not oSQL1.OpenConn() Then Exit Function
            If Not oSQL2.OpenConn() Then Exit Function

            cTarih = CStr(Format(Microsoft.VisualBasic.DateAndTime.Day(dTarih), "00")) + "." + CStr(Format(Microsoft.VisualBasic.DateAndTime.Month(dTarih), "00")) + "." + CStr(Format(Microsoft.VisualBasic.DateAndTime.Year(dTarih), "0000"))

            cSQL = "set dateformat dmy " +
                    " select doviz " +
                    " from dovkur with (NOLOCK) " +
                    " where tarih = '" + cTarih + "' " +
                    " and doviz = '" + cDoviz.Trim + "' " +
                    " and kurcinsi = '" + cKurCinsi.Trim + "' "

            If oSQL1.CheckExists(cSQL) Then
                If lUpdate Then
                    cSQL = "set dateformat dmy " +
                        " update dovkur " +
                        " set kur = " + SQLWriteDecimal(nKur) + " , " +
                        " kaynak = '" + cKaynak.Trim + "', " +
                        " modificationdate = getdate() " +
                        " where tarih = '" + cTarih + "' " +
                        " and doviz = '" + cDoviz.Trim + "' " +
                        " and kurcinsi = '" + cKurCinsi.Trim + "' "

                    oSQL2.SQLExecute(cSQL)
                End If
            Else
                cSQL = "set dateformat dmy " +
                        " insert dovkur (tarih, doviz, kurcinsi, kur, modificationdate, kaynak) " +
                        " values ('" + cTarih + "', " +
                        " '" + SQLWriteString(cDoviz) + "', " +
                        " '" + SQLWriteString(cKurCinsi) + "', " +
                         SQLWriteDecimal(nKur) + ", " +
                        " getdate(), " +
                        " '" + cKaynak.Trim + "' ) "

                oSQL2.SQLExecute(cSQL)
            End If

            oSQL2.CloseConn()
            oSQL1.CloseConn()

            oSQL1 = Nothing
            oSQL2 = Nothing

            DovizKuruYaz = True

        Catch ex As Exception
            ErrDisp("DovizKuruYaz : " + ex.Message, "UtilKur", cSQL,, ex)
        End Try
    End Function

    Public Function MBKurOku2(cDoviz As String,
                             Optional dTarih As Date = #1/1/1950#,
                             Optional ByRef nAlis As Double = 0,
                             Optional ByRef nSatis As Double = 0,
                             Optional ByRef nEfektifAlis As Double = 0,
                             Optional ByRef nEfektifSatis As Double = 0) As Boolean

        MBKurOku2 = False

        Try
            Dim x As Integer = 0
            Dim oTable As DataTable
            Dim cAlis As String = ""
            Dim cSatis As String = ""
            Dim cEfektifAlis As String = ""
            Dim cEfektifSatis As String = ""

            If dTarih = #1/1/1950# Then
                dTarih = Now.Date
            End If

            Dim cURL As String = GetMBURL(dTarih)

            If cURL.Trim = "" Then
                Exit Function
            End If

            Dim rdr As XmlTextReader = New XmlTextReader(cURL)
            Dim ds As DataSet = New DataSet()

            ds.ReadXml(rdr)

            oTable = ds.Tables("Currency")

            Thread.Sleep(2000) 'Bu kısım verilerin internet üzerinden gecikmesi durumuna karsi 2 saniye bekletilmistir

            For x = 0 To oTable.Rows.Count - 1
                If Not (oTable.Rows(x).ItemArray(10) Is Nothing) Then
                    If oTable.Rows(x).ItemArray(10).ToString.Contains(cDoviz) Then
                        cAlis = oTable.Rows(x).ItemArray(3).ToString()
                        cSatis = oTable.Rows(x).ItemArray(4).ToString()
                        cEfektifAlis = oTable.Rows(x).ItemArray(5).ToString()
                        cEfektifSatis = oTable.Rows(x).ItemArray(6).ToString()

                        nAlis = GetDouble(cAlis)
                        nSatis = GetDouble(cSatis)
                        nEfektifAlis = GetDouble(cEfektifAlis)
                        nEfektifSatis = GetDouble(cEfektifSatis)

                        MBKurOku2 = True

                        Exit For
                    End If
                End If
            Next

        Catch ex As Exception
            ErrDisp("MBKurOku2 : " + ex.Message, "UtilKur",,, ex)
        End Try
    End Function

    Private Function GetMBURL(Optional dTarih As Date = #1/1/1950#) As String

        Dim dSayfaTarihi As Date = #1/1/1950#
        Dim nMaxSearchBack As Integer = 30
        Dim nSearchBack As Integer = 1
        Dim cUrl As String = ""

        GetMBURL = ""

        Try
            ' saat 15:30 da yayınlanan kur sayfası aslında yarının kuru
            ' http://www.tcmb.gov.tr/kurlar/today.xml

            ' istenilen günün kuru aslında 1 önceki gün yayınlanıyor
            ' bugünden önce yayınlanan bir kur sayfasına aşağıdaki şekilde ulaşıyoruz
            ' http://www.tcmb.gov.tr/kurlar/YYYYAA/GGAAYYYY.xml

            Do While True
                ' bir gün önce yayınlanan kura bak
                dSayfaTarihi = DateAdd(DateInterval.Day, -nSearchBack, dTarih)
                ' yayın sayfası adresi
                cUrl = "http://www.tcmb.gov.tr/kurlar/" +
                        CStr(Format(Microsoft.VisualBasic.DateAndTime.Year(dSayfaTarihi), "0000")) + CStr(Format(Microsoft.VisualBasic.DateAndTime.Month(dSayfaTarihi), "00")) + "/" +
                        CStr(Format(Microsoft.VisualBasic.DateAndTime.Day(dSayfaTarihi), "00")) + CStr(Format(Microsoft.VisualBasic.DateAndTime.Month(dSayfaTarihi), "00")) + CStr(Format(Microsoft.VisualBasic.DateAndTime.Year(dSayfaTarihi), "0000")) + ".xml"
                ' sayfa bulunca çık
                If CheckURL(cUrl) Then
                    GetMBURL = cUrl.Trim
                    Exit Function
                End If
                ' en fazla nMaxSearchBack gün geriye ara
                nSearchBack = nSearchBack + 1
                If nSearchBack > nMaxSearchBack Then
                    Exit Function
                End If
            Loop

        Catch ex As Exception
            ErrDisp("GetMBURL : " + ex.Message, "UtilKur",,, ex)
        End Try
    End Function

    Public Function GetDouble(ByVal doublestring As String) As Double
        On Error Resume Next
        Dim retval As Double = 0
        Dim sep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator

        Double.TryParse(Replace(Replace(doublestring, ".", sep), ",", sep), retval)
        Return retval
    End Function

    Public Function GetDoubleNullable(ByVal doublestring As String) As Double
        On Error Resume Next
        Dim retval As Double = 0
        Dim sep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator

        If Double.TryParse(Replace(Replace(doublestring, ".", sep), ",", sep), retval) Then
            Return retval
        Else
            Return Nothing
        End If
    End Function

End Module
