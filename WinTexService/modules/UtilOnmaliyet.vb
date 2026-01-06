Option Explicit On
Option Strict On

Imports System.Xml
Imports System.Configuration
Imports System.Net
Imports System
Imports System.IO
Imports System.Globalization

Module UtilOnmaliyet


    Public Function NumKumSatMailTekrar(cDatabase As String) As Boolean

        ' numune kumaş satış onayı hatırlatma eMaili

        NumKumSatMailTekrar = False

        Try

            NumKumSatMailTekrar = True

        Catch ex As Exception
            ErrDisp(ex, "NumKumSatMailTekrar")
        End Try
    End Function

    Public Function NumKumRezMailTekrar(cDatabase As String) As Boolean

        ' numune kumaş rezervasyon onayı hatırlatma eMaili

        NumKumRezMailTekrar = False

        Try

            NumKumRezMailTekrar = True

        Catch ex As Exception
            ErrDisp(ex, "NumKumRezMailTekrar")
        End Try
    End Function

    Public Function OnMaliyet3MailTekrar(cDatabase As String) As Boolean

        ' ön maliyet-3 onayı hatırlatma eMaili

        OnMaliyet3MailTekrar = False

        Try
            Dim oSQL As New SQLServerClass(True,, cDatabase)
            Dim cCalismaNo As String = ""
            Dim cID As String = ""
            Dim cOnmlyt3SurecBasla As String = ""
            Dim cOnmlyYetkiler As String = ""
            Dim cFilter As String = ""

            Dim oCredentials As New NetworkCredential

            oCredentials.Domain = ConfigurationManager.AppSettings("WinTexFileShareDomain").ToString()
            oCredentials.UserName = ConfigurationManager.AppSettings("WinTexFileShareUsername").ToString()
            oCredentials.Password = ConfigurationManager.AppSettings("WinTexFileSharePassword").ToString()

            Dim oNC As New NetworkConnection(ConfigurationManager.AppSettings("WinTexFileSharePath").ToString(), oCredentials)

            Using oNC
                oSQL.OpenConn()

                cOnmlyYetkiler = oSQL.GetSysPar("onmlyyetkiler")
                cOnmlyt3SurecBasla = oSQL.GetSysPar("onmlyt3surecbasla")

                If cOnmlyYetkiler.Trim = "1" Then

                    ' teknik onay istenmiş fakat geri dönüş olmamış
                    oSQL.cSQLQuery = "select distinct calismano, requestid " +
                                " from maliyetonayistek with (NOLOCK) " +
                                " where teknikonayisteyenpersonel is not null " +
                                " and teknikonayisteyenpersonel <> '' " +
                                " and (teknikonay is null or teknikonay = '') " +
                                " and (revizeedildi is null or  revizeedildi = 'H' or revizeedildi = '') " +
                                " and calismano is not null " +
                                " and calismano <> '' " +
                                " order by calismano "

                    oSQL.GetSQLReader()

                    Do While oSQL.oReader.Read
                        cCalismaNo = oSQL.SQLReadString("calismano")
                        cID = oSQL.SQLReadString("requestid")
                        OnayTalepMailYolla(3, cCalismaNo, cID, cDatabase)
                    Loop
                    oSQL.oReader.Close()

                    ' yeni süreç öncesinde ara onay istenmiş fakat geri dönüş olmamış
                    oSQL.cSQLQuery = "select distinct calismano, requestid " +
                                " from maliyetonayistek with (NOLOCK) " +
                                " where onayisteyen is not null " +
                                " and onayisteyen <> '' " +
                                " and (araonay is null or araonay = '') " +
                                " and (revizeedildi is null or  revizeedildi = 'H' or revizeedildi = '') " +
                                " and calismano is not null " +
                                " and calismano <> '' " +
                                " and exists (select calismano " +
                                        " from maliyetheader with (NOLOCK) " +
                                        " where calismano = maliyetonayistek.calismano " +
                                        " and tarih < '" + cOnmlyt3SurecBasla + "' ) " +
                                " order by calismano "

                    oSQL.GetSQLReader()

                    Do While oSQL.oReader.Read
                        cCalismaNo = oSQL.SQLReadString("calismano")
                        cID = oSQL.SQLReadString("requestid")
                        OnayTalepMailYolla(1, cCalismaNo, cID, cDatabase)
                    Loop
                    oSQL.oReader.Close()

                    ' yeni süreç içinde ara onay istenmiş fakat geri dönüş olmamış
                    oSQL.cSQLQuery = "select distinct calismano, requestid " +
                                " from maliyetonayistek with (NOLOCK) " +
                                " where onayisteyen is not null " +
                                " and onayisteyen <> '' " +
                                " and (araonay is null or araonay = '') " +
                                " and (revizeedildi is null or  revizeedildi = 'H' or revizeedildi = '') " +
                                " and calismano is not null " +
                                " and calismano <> '' " +
                                " and teknikonay = 'E' " +
                                " and exists (select calismano " +
                                        " from maliyetheader with (NOLOCK) " +
                                        " where calismano = maliyetonayistek.calismano " +
                                        " and onmlysurecbasla = 'E' " +
                                        " and tarih >= '" + cOnmlyt3SurecBasla + "' ) " +
                                " order by calismano "

                    oSQL.GetSQLReader()

                    Do While oSQL.oReader.Read
                        cCalismaNo = oSQL.SQLReadString("calismano")
                        cID = oSQL.SQLReadString("requestid")
                        OnayTalepMailYolla(1, cCalismaNo, cID, cDatabase)
                    Loop
                    oSQL.oReader.Close()

                    ' yeni süreç öncesinde final onay istenmiş fakat geri dönüş olmamış
                    oSQL.cSQLQuery = "select distinct calismano, requestid  " +
                                " from maliyetonayistek with (NOLOCK) " +
                                " where araonay = 'E' " +
                                " and (finalonay is null or finalonay = '') " +
                                " and (revizeedildi is null or  revizeedildi = 'H' or revizeedildi = '') " +
                                " and calismano is not null " +
                                " and calismano <> '' " +
                                " and exists (select calismano " +
                                        " from maliyetheader with (NOLOCK) " +
                                        " where calismano = maliyetonayistek.calismano " +
                                        " and tarih < '" + cOnmlyt3SurecBasla + "' ) " +
                                " order by calismano "

                    oSQL.GetSQLReader()

                    Do While oSQL.oReader.Read
                        cCalismaNo = oSQL.SQLReadString("calismano")
                        cID = oSQL.SQLReadString("requestid")
                        OnayTalepMailYolla(2, cCalismaNo, cID, cDatabase)
                    Loop
                    oSQL.oReader.Close()

                    ' yeni süreç içinde final onay istenmiş fakat geri dönüş olmamış
                    oSQL.cSQLQuery = "select distinct calismano, requestid  " +
                                " from maliyetonayistek with (NOLOCK) " +
                                " where araonay = 'E' " +
                                " and (finalonay is null or finalonay = '') " +
                                " and (revizeedildi is null or  revizeedildi = 'H' or revizeedildi = '') " +
                                " and calismano is not null " +
                                " and calismano <> '' " +
                                " and teknikonay = 'E' " +
                                " and exists (select calismano " +
                                        " from maliyetheader with (NOLOCK) " +
                                        " where calismano = maliyetonayistek.calismano " +
                                        " and onmlysurecbasla = 'E' " +
                                        " and tarih >= '" + cOnmlyt3SurecBasla + "' ) " +
                                " order by calismano "

                    oSQL.GetSQLReader()

                    Do While oSQL.oReader.Read
                        cCalismaNo = oSQL.SQLReadString("calismano")
                        cID = oSQL.SQLReadString("requestid")
                        OnayTalepMailYolla(2, cCalismaNo, cID, cDatabase)
                    Loop
                    oSQL.oReader.Close()
                Else
                    ' ara onay istenmiş fakat geri dönüş olmamış
                    oSQL.cSQLQuery = "select distinct calismano, requestid " +
                                " from maliyetonayistek with (NOLOCK) " +
                                " where onayisteyen is not null " +
                                " and onayisteyen <> '' " +
                                " and (araonay is null or araonay = '') " +
                                " and (revizeedildi is null or  revizeedildi = 'H' or revizeedildi = '') " +
                                " and calismano is not null " +
                                " and calismano <> '' " +
                                " order by calismano "

                    oSQL.GetSQLReader()

                    Do While oSQL.oReader.Read
                        cCalismaNo = oSQL.SQLReadString("calismano")
                        cID = oSQL.SQLReadString("requestid")
                        OnayTalepMailYolla(1, cCalismaNo, cID, cDatabase)
                    Loop
                    oSQL.oReader.Close()

                    ' final onay istenmiş fakat geri dönüş olmamış
                    oSQL.cSQLQuery = "select distinct calismano, requestid  " +
                                " from maliyetonayistek with (NOLOCK) " +
                                " where araonay = 'E' " +
                                " and (finalonay is null or finalonay = '') " +
                                " and (revizeedildi is null or  revizeedildi = 'H' or revizeedildi = '') " +
                                " and calismano is not null " +
                                " and calismano <> '' " +
                                " order by calismano "

                    oSQL.GetSQLReader()

                    Do While oSQL.oReader.Read
                        cCalismaNo = oSQL.SQLReadString("calismano")
                        cID = oSQL.SQLReadString("requestid")
                        OnayTalepMailYolla(2, cCalismaNo, cID, cDatabase)
                    Loop
                    oSQL.oReader.Close()
                End If

                oSQL.CloseConn()
                oSQL = Nothing

            End Using

            OnMaliyet3MailTekrar = True

        Catch ex As Exception
            ErrDisp(ex, "OnMaliyet3MailTekrar")
        End Try
    End Function

    Private Function OnayTalepMailYolla(nMode As Integer, cCalismaNo As String, cID As String, cDatabase As String) As Boolean
        ' nMode = 1 , ara onay
        ' nMode = 2 , final onay
        ' nMode = 3 , teknik onay
        Dim oSQL As New SQLServerClass(True,, cDatabase)
        Dim cMusteriNo As String = ""
        Dim nReportID As Integer = 0
        Dim nCnt As Integer = 0
        Dim cWEBServisURL As String = ""
        Dim cBaseURL As String = ""
        Dim cDosyaAdiTipi As String = ""
        Dim cMessage As String = ""
        Dim cSubject As String = ""
        Dim cBody As String = ""
        Dim cFileName As String = ""
        Dim cSonuc As String = ""
        Dim lReportOK As Boolean = False
        Dim aMail() As String
        Dim cOnayTipi As String = ""

        OnayTalepMailYolla = False

        Try
            Select Case nMode
                Case 1
                    cOnayTipi = "ARA"
                Case 2
                    cOnayTipi = "FINAL"
                Case 3
                    cOnayTipi = "TEKNIK"
            End Select

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select reportid " +
                            " from stireports with (NOLOCK) " +
                            " where reportclass = 'dokumaonmlyt' " +
                            " and webreport = 'E' "

            nReportID = oSQL.DBReadInteger()

            If nReportID = 0 Then
                oSQL.CloseConn()
                Exit Function
            End If

            cBaseURL = oSQL.GetSysPar("emailokurl") ' https://tablet.eroglugiyim.com
            cWEBServisURL = cBaseURL.Trim

            oSQL.cSQLQuery = "select top 1 musteri " +
                            " from maliyetheader with (NOLOCK) " +
                            " where calismano = '" + cCalismaNo + "' "

            cMusteriNo = oSQL.DBReadString()

            ReDim aMail(0)

            Select Case nMode
                Case 1
                    cDosyaAdiTipi = "AraOnayTekrar"
                    cSubject = "On maliyet no : " + cCalismaNo.Trim + " ara onay TEKRAR bilgilendirmesi"
                    cBody = "On maliyet no : " + cCalismaNo.Trim + " ara onay istegi henuz cevaplanmamistir. "
                    cFileName = "C:\wintex\Temp\" + "OnMaliyet-" + cDosyaAdiTipi + "-" + cCalismaNo.Trim + ".pdf"

                    oSQL.cSQLQuery = "select distinct b.email " +
                                " from onaysureci a with (NOLOCK) , personel b with (NOLOCK) " +
                                " Where a.onaylayacakpersonel = b.personel " +
                                " and b.email is not null " +
                                " and b.email <> '' " +
                                " and a.onaysistemi = 'ONMALIYET' " +
                                " and a.onayrolu = 'ARA ONAY' " +
                                " and a.musterino = '" + cMusteriNo + "' "

                    If oSQL.CheckExists() Then
                        aMail = oSQL.SQLtoStringArray()
                    Else
                        oSQL.cSQLQuery = "select distinct b.email " +
                                " from onaysureci a with (NOLOCK) , personel b with (NOLOCK) " +
                                " Where a.onaylayacakpersonel = b.personel " +
                                " and b.email is not null " +
                                " and b.email <> '' " +
                                " and a.onaysistemi = 'ONMALIYET' " +
                                " and a.onayrolu = 'ARA ONAY' " +
                                " and a.musterino = 'HEPSI' "

                        aMail = oSQL.SQLtoStringArray()
                    End If

                Case 2
                    cDosyaAdiTipi = "FinalOnayTekrar"
                    cSubject = "On maliyet no : " + cCalismaNo.Trim + " final onay TEKRAR bilgilendirmesi"
                    cBody = "On maliyet no : " + cCalismaNo.Trim + " final onay istegi henuz cevaplanmamistir. "
                    cFileName = "C:\wintex\Temp\" + "OnMaliyet-" + cDosyaAdiTipi + "-" + cCalismaNo.Trim + ".pdf"

                    oSQL.cSQLQuery = "select distinct b.email " +
                                " from onaysureci a with (NOLOCK) , personel b with (NOLOCK) " +
                                " Where a.onaylayacakpersonel = b.personel " +
                                " and b.email is not null " +
                                " and b.email <> '' " +
                                " and a.onaysistemi = 'ONMALIYET' " +
                                " and a.onayrolu = 'FINAL ONAY' " +
                                " and a.musterino = '" + cMusteriNo + "' "

                    If oSQL.CheckExists() Then
                        aMail = oSQL.SQLtoStringArray()
                    Else
                        oSQL.cSQLQuery = "select distinct b.email " +
                                " from onaysureci a with (NOLOCK) , personel b with (NOLOCK) " +
                                " Where a.onaylayacakpersonel = b.personel " +
                                " and b.email is not null " +
                                " and b.email <> '' " +
                                " and a.onaysistemi = 'ONMALIYET' " +
                                " and a.onayrolu = 'FINAL ONAY' " +
                                " and a.musterino = 'HEPSI' "

                        aMail = oSQL.SQLtoStringArray()
                    End If

                Case 3
                    cDosyaAdiTipi = "MaliyetlendirmeOnayTekrar"
                    cSubject = "On maliyet no : " + cCalismaNo.Trim + " maliyetlendirme onay TEKRAR bilgilendirmesi"
                    cBody = "On maliyet no : " + cCalismaNo.Trim + " maliyetlendirme onay istegi henuz cevaplanmamistir. "
                    cFileName = "C:\wintex\Temp\" + "OnMaliyet-" + cDosyaAdiTipi + "-" + cCalismaNo.Trim + ".pdf"

                    oSQL.cSQLQuery = "select distinct b.email " +
                                " from onaysureci a with (NOLOCK) , personel b with (NOLOCK) " +
                                " Where a.onaylayacakpersonel = b.personel " +
                                " and b.email is not null " +
                                " and b.email <> '' " +
                                " and a.onaysistemi = 'ONMALIYET' " +
                                " and a.onayrolu = 'TEKNIK ONAY' " +
                                " and a.musterino = '" + cMusteriNo + "' "

                    If oSQL.CheckExists() Then
                        aMail = oSQL.SQLtoStringArray()
                    Else
                        oSQL.cSQLQuery = "select distinct b.email " +
                                " from onaysureci a with (NOLOCK) , personel b with (NOLOCK) " +
                                " Where a.onaylayacakpersonel = b.personel " +
                                " and b.email is not null " +
                                " and b.email <> '' " +
                                " and a.onaysistemi = 'ONMALIYET' " +
                                " and a.onayrolu = 'TEKNIK ONAY' " +
                                " and a.musterino = 'HEPSI' "

                        aMail = oSQL.SQLtoStringArray()
                    End If
            End Select

            cBody += "On maliyet belgesi eMail ekindedir. WinTex Servis Versiyon : " + My.Application.Info.Version.ToString.Trim

            If (My.Computer.FileSystem.DirectoryExists("C:\wintex\Temp") = False) Then
                My.Computer.FileSystem.CreateDirectory("C:\wintex\Temp")
            End If

            If My.Computer.FileSystem.FileExists(cFileName) Then
                My.Computer.FileSystem.DeleteFile(cFileName)
            End If

            If StiReportToPDF(nReportID.ToString, cFileName, cCalismaNo.Trim, cSonuc,,,,,,,,, cDatabase) Then
                If My.Computer.FileSystem.FileExists(cFileName) Then
                    lReportOK = True
                End If
            End If

            If lReportOK Then
                For nCnt = 0 To UBound(aMail)
                    If aMail(nCnt) <> "" Then
                        cBody = GetHTMLOnay(cWEBServisURL, cID, aMail(nCnt), cSubject, cCalismaNo, cDatabase, cOnayTipi)
                        If SendGoogleMail(aMail(nCnt), cSubject, cBody, cFileName, cDatabase) Then
                            OnayTalepMailYolla = True
                            ActivityLog(cDatabase, "Mesaj", "OMY3 eMail Gonderildi", cSubject, aMail(nCnt) + " " + cBody.Trim)
                        Else
                            ActivityLog(cDatabase, "Mesaj", "OMY3 eMail Gonderilemedi", cSubject, aMail(nCnt) + " " + cBody.Trim)
                        End If
                    End If
                Next
            End If

            oSQL.CloseConn()
            oSQL = Nothing

            My.Computer.FileSystem.DeleteFile(cFileName)

        Catch ex As Exception
            ErrDisp(ex, "OnayTalepMailYolla")
        End Try
    End Function

    Private Function GetHTMLOnay(Optional cWEBServisURL As String = "", Optional cID As String = "", Optional cUserID As String = "",
                                 Optional cHeader As String = "", Optional cCalismaNo As String = "", Optional cDatabase As String = "",
                                 Optional cOnayTipi As String = "") As String
        GetHTMLOnay = ""

        Try
            Dim cText As String = ""
            Dim nRow As Long = 0
            Dim nCol As Long = 0
            Dim oSQL As SQLServerClass
            Dim c2 As String = ""
            Dim c3 As String = ""
            Dim c4 As String = ""
            Dim cSiparisler As String = ""
            Dim cMusteriler As String = ""
            Dim cSiparisTable As String = ""
            Dim cOnayTarihcesi As String = ""
            Dim cRevize As String = ""

            cSiparisTable = GetOnMaliyetOnayHTML(cCalismaNo, cDatabase)
            cOnayTarihcesi = GetOnMaliyetTarihceHTML(cCalismaNo, cDatabase)
            cRevize = GetOnMaliyetRevizeHTML(cCalismaNo, cDatabase)

            c2 = "'" + cWEBServisURL +
                "?cID=" + cID +
                "&cUserID=" + cUserID +
                "&cResult=E" +
                "&cCalismaNo=" + cCalismaNo +
                "&cOnayTipi=" + cOnayTipi +
                "&cDB=" + cDatabase + "' "

            c3 = "'" + cWEBServisURL +
                "?cID=" + cID +
                "&cUserID=" + cUserID +
                "&cResult=H" +
                "&cCalismaNo=" + cCalismaNo +
                "&cOnayTipi=" + cOnayTipi +
                "&cDB=" + cDatabase + "' "

            c4 = "'" + cWEBServisURL +
                "?cUserID=" + cUserID + "' "

            oSQL = New SQLServerClass(True,, cDatabase)

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select dbo.getonmlyt3musteri('" + cCalismaNo + "') "
            cMusteriler = oSQL.DBReadString()

            oSQL.cSQLQuery = "select dbo.getonmlyt3siparis('" + cCalismaNo + "') "
            cSiparisler = oSQL.DBReadString()

            oSQL.cSQLQuery = "select top 1 parametertext " +
                             " from syspar with (NOLOCK) " +
                             " where parametername = 'onmlyonaytext' "

            cText = oSQL.DBReadString()

            If cText.Trim = "" Then
                cText = " <table align='center' width='1000' style='border:1px solid #333'> " +
                " <tr> " +
                    " <td align='center' style = 'padding-bottom:40px; font-size: 50px; text-decoration: none' >" + cHeader + "</td> " +
                " </tr> "

                cText = cText +
                " <tr> " +
                " <table align='center' width='1000' border='1' cellspacing='1' cellpadding='1' style='border:1px solid #ccc'> "

                cText = cText + " <tr> " +
                " <td style = 'padding-bottom:40px; font-size: 50px; text-decoration: none' > " +
                " <p>Dikkat : Belgeyi onaylamadan once mail ekine bakiniz lutfen</p> " +
                " </td> " +
                " </tr> "

                ' WEB link 
                cText = cText + " <tr> " +
                " <td style='text-align:center'> " +
                " <a href='" + cWEBServisURL + "?cUserID=" + cUserID +
                                                "' style = 'padding-bottom:40px; font-size: 50px; text-decoration: none' >Onay Sitesi</a>" +
                " </td> " +
                " </tr> "

                ' OK buton HTML
                cText = cText + " <tr> " +
                " <td style='text-align:center'> " +
                " <a href='" + cWEBServisURL + "?cID=" + cID +
                                                "&cUserID=" + cUserID +
                                                "&cResult=E" +
                                                "&cCalismaNo=" + cCalismaNo +
                                                "&cOnayTipi=" + cOnayTipi +
                                                "' style = 'padding-bottom:40px; font-size: 50px; text-decoration: none' >Talebi Onayliyorum</a>" +
                " </td> " +
                " </tr> "

                ' Red buton HTML
                cText = cText + " <tr> " +
                " <td style='text-align:center'> " +
                " <a href='" + cWEBServisURL + "?cID=" + cID +
                                                "&cUserID=" + cUserID +
                                                "&cResult=H" +
                                                "&cCalismaNo=" + cCalismaNo +
                                                "&cOnayTipi=" + cOnayTipi +
                                                "' style = 'padding-bottom:40px; font-size: 50px; text-decoration: none' >Dikkat Talebi Red Ediyorum</a>" +
                " </td> " +
                " </tr> "

                cText = cText +
                " </table> " +
                " </tr> " +
                " </table> "
            Else
                cText = Replace(cText, "||", "'")
                cText = Replace(cText, "{1}", cCalismaNo + "/WinTexService " + My.Application.Info.Version.ToString.Trim)
                cText = Replace(cText, "{2}", c2)
                cText = Replace(cText, "{3}", c3)
                cText = Replace(cText, "{4}", cSiparisler)
                cText = Replace(cText, "{5}", cMusteriler)
                cText = Replace(cText, "{6}", cSiparisTable)
                cText = Replace(cText, "{7}", cOnayTarihcesi)
                cText = Replace(cText, "{8}", cRevize)
                cText = Replace(cText, "{9}", c4)
            End If

            oSQL.CloseConn()
            oSQL = Nothing

            GetHTMLOnay = cText

        Catch ex As Exception
            ErrDisp(ex, "GetHTMLOnay")
        End Try
    End Function

    Private Function GetOnMaliyetOnayHTML(cCalismaNo As String, cDatabase As String) As String

        GetOnMaliyetOnayHTML = ""

        Try
            Dim oSQL As SQLServerClass
            Dim nMaxCols As Integer = 0
            Dim nRow As Integer = 0
            Dim cTarih As String = ""

            If cCalismaNo.Trim = "" Then Exit Function

            oSQL = New SQLServerClass(True,, cDatabase)
            oSQL.OpenConn()

            nRow = 0
            nMaxCols = 5
            ReDim aHTMLRow(nMaxCols, 0)
            aHTMLRow(0, 0).cValue = "Musteri"
            aHTMLRow(1, 0).cValue = "Siparis"
            aHTMLRow(2, 0).cValue = "Planlanan Kesim"
            aHTMLRow(3, 0).cValue = "Ilk Sevk"
            aHTMLRow(4, 0).cValue = "Son Sevk"
            aHTMLRow(5, 0).cValue = "Siparis Adet"

            oSQL.cSQLQuery = "select distinct a.musterino, a.kullanicisipno, a.ilksevktarihi, a.sonsevktarihi, a.eksevktarihi2, "

            oSQL.cSQLQuery += " adet = (select sum(coalesce(adet,0)) " +
                                    " from sipmodel with (NOLOCK) " +
                                    " where siparisno = a.kullanicisipno) "

            oSQL.cSQLQuery += " from siparis a with (NOLOCK), sipfiyat b with (NOLOCK) " +
                            " where a.kullanicisipno = b.siparisno " +
                            " and b.onmaliyetmodelno = '" + cCalismaNo.Trim + "' " +
                            " order by a.musterino, a.kullanicisipno "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                nRow = nRow + 1
                ReDim Preserve aHTMLRow(nMaxCols, nRow)

                aHTMLRow(0, nRow).cValue = oSQL.SQLReadString("musterino")
                aHTMLRow(1, nRow).cValue = oSQL.SQLReadString("kullanicisipno")

                If oSQL.SQLReadDate("eksevktarihi2") = #1/1/1950# Then
                    aHTMLRow(2, nRow).cValue = "Planlanmamis"
                Else
                    aHTMLRow(2, nRow).cValue = oSQL.SQLReadDate("eksevktarihi2").ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                End If

                If oSQL.SQLReadDate("ilksevktarihi") = #1/1/1950# Then
                    aHTMLRow(3, nRow).cValue = "Girilmemis"
                Else
                    aHTMLRow(3, nRow).cValue = oSQL.SQLReadDate("ilksevktarihi").ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                End If

                If oSQL.SQLReadDate("sonsevktarihi") = #1/1/1950# Then
                    aHTMLRow(4, nRow).cValue = "Girilmemis"
                Else
                    aHTMLRow(4, nRow).cValue = oSQL.SQLReadDate("sonsevktarihi").ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                End If

                aHTMLRow(5, nRow).cValue = oSQL.SQLReadDouble("adet").ToString
            Loop

            oSQL.oReader.Close()
            oSQL.CloseConn()

            If nRow > 0 Then
                GetOnMaliyetOnayHTML = GetHTMLTable(nRow, nMaxCols, "On Maliyete Bagli Siparisler")
            End If

        Catch ex As Exception
            ErrDisp(ex, "GetOnMaliyetOnayHTML")
        End Try
    End Function

    Private Function GetOnMaliyetTarihceHTML(cCalismaNo As String, cDataBase As String) As String

        GetOnMaliyetTarihceHTML = ""

        Try
            Dim oSQL As SQLServerClass
            Dim nMaxCols As Integer = 0
            Dim nRow As Integer = 0
            Dim cTarih As String = ""

            If cCalismaNo.Trim = "" Then Exit Function

            oSQL = New SQLServerClass(True,, cDataBase)
            oSQL.OpenConn()

            nRow = 0
            nMaxCols = 14
            ReDim aHTMLRow(nMaxCols, 0)
            aHTMLRow(0, 0).cValue = "Istek ID"
            aHTMLRow(1, 0).cValue = "Istek Tarihi"

            aHTMLRow(2, 0).cValue = "Mly.Onay Isteyen"
            aHTMLRow(3, 0).cValue = "Mly.Onaycı"
            aHTMLRow(4, 0).cValue = "Mly.Onay Tarihi"
            aHTMLRow(5, 0).cValue = "Mly.Onay"

            aHTMLRow(6, 0).cValue = "Ara Onay Isteyen"
            aHTMLRow(7, 0).cValue = "Ara Onayci"
            aHTMLRow(8, 0).cValue = "Ara Onay Tarihi"
            aHTMLRow(9, 0).cValue = "Ara Onay"

            aHTMLRow(10, 0).cValue = "Final Onayci"
            aHTMLRow(11, 0).cValue = "Final Onay Tarihi"
            aHTMLRow(12, 0).cValue = "Final Onay"

            aHTMLRow(13, 0).cValue = "RED Sebebi"
            aHTMLRow(14, 0).cValue = "Revize Edildi"

            oSQL.cSQLQuery = "select requestid, tarih, " +
                            " teknikonayisteyenpersonel, teknikonaypersonel, teknikonaytarih, teknikonay, " +
                            " onayisteyen, araonaypersonel, araonaytarih, araonay, " +
                            " finalonaypersonel , finalonaytarih, finalonay, " +
                            " maliyetredsebebi, revizeedildi " +
                            " from maliyetonayistek with (NOLOCK) " +
                            " where calismano = '" + cCalismaNo.Trim + "' " +
                            " order by tarih "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read

                nRow = nRow + 1
                ReDim Preserve aHTMLRow(nMaxCols, nRow)

                aHTMLRow(0, nRow).cValue = oSQL.SQLReadString("requestid")

                If oSQL.SQLReadDate("tarih") = #1/1/1950# Then
                    aHTMLRow(1, nRow).cValue = "Girilmemis"
                Else
                    aHTMLRow(1, nRow).cValue = oSQL.SQLReadDate("tarih").ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                End If

                ' teknik onay
                aHTMLRow(2, nRow).cValue = oSQL.SQLReadString("teknikonayisteyenpersonel")
                aHTMLRow(3, nRow).cValue = oSQL.SQLReadString("teknikonaypersonel")

                If oSQL.SQLReadDate("teknikonaytarih") = #1/1/1950# Then
                    aHTMLRow(4, nRow).cValue = "Girilmemis"
                Else
                    aHTMLRow(4, nRow).cValue = oSQL.SQLReadDate("teknikonaytarih").ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                End If

                Select Case oSQL.SQLReadString("teknikonay")
                    Case "E"
                        aHTMLRow(5, nRow).cValue = "Onaylandi"
                    Case "H"
                        aHTMLRow(5, nRow).cValue = "RED Edildi"
                    Case Else
                        aHTMLRow(5, nRow).cValue = "Belirsiz"
                End Select

                ' ara onay
                aHTMLRow(6, nRow).cValue = oSQL.SQLReadString("onayisteyen")
                aHTMLRow(7, nRow).cValue = oSQL.SQLReadString("araonaypersonel")

                If oSQL.SQLReadDate("araonaytarih") = #1/1/1950# Then
                    aHTMLRow(8, nRow).cValue = "Girilmemis"
                Else
                    aHTMLRow(8, nRow).cValue = oSQL.SQLReadDate("araonaytarih").ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                End If

                Select Case oSQL.SQLReadString("araonay")
                    Case "E"
                        aHTMLRow(9, nRow).cValue = "Onaylandi"
                    Case "H"
                        aHTMLRow(9, nRow).cValue = "RED Edildi"
                    Case Else
                        aHTMLRow(9, nRow).cValue = "Belirsiz"
                End Select

                ' final onay
                aHTMLRow(10, nRow).cValue = oSQL.SQLReadString("finalonaypersonel")

                If oSQL.SQLReadDate("finalonaytarih") = #1/1/1950# Then
                    aHTMLRow(11, nRow).cValue = "Girilmemis"
                Else
                    aHTMLRow(11, nRow).cValue = oSQL.SQLReadDate("finalonaytarih").ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                End If

                Select Case oSQL.SQLReadString("finalonay")
                    Case "E"
                        aHTMLRow(12, nRow).cValue = "Onaylandi"
                    Case "H"
                        aHTMLRow(12, nRow).cValue = "RED Edildi"
                    Case Else
                        aHTMLRow(12, nRow).cValue = "Belirsiz"
                End Select

                aHTMLRow(13, nRow).cValue = oSQL.SQLReadString("maliyetredsebebi")

                Select Case oSQL.SQLReadString("revizeedildi")
                    Case "E"
                        aHTMLRow(14, nRow).cValue = "Evet"
                    Case Else
                        aHTMLRow(14, nRow).cValue = ""
                End Select
            Loop

            oSQL.oReader.Close()
            oSQL.CloseConn()

            If nRow > 0 Then
                GetOnMaliyetTarihceHTML = GetHTMLTable(nRow, nMaxCols, "Onay Tarihcesi")
            End If

        Catch ex As Exception
            ErrDisp(ex, "GetOnMaliyetTarihceHTML")
        End Try
    End Function

    Public Function GetOnMaliyetRevizeHTML(cCalismaNo As String, cDataBase As String) As String

        GetOnMaliyetRevizeHTML = ""

        Try
            Dim cSQL As String = ""
            Dim nMaxCols As Integer = 0
            Dim nRow As Integer = 0
            Dim oSQL As SQLServerClass
            Dim cDoviz As String = ""
            Dim nKar1 As Double = 0
            Dim nKar2 As Double = 0
            Dim nFotf As Double = 0

            If cCalismaNo.Trim = "" Then Exit Function

            oSQL = New SQLServerClass(True,, cDataBase)
            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 doviz, finalonaytargetfiyat " +
                            " from maliyetheader with (NOLOCK) " +
                            " where calismano = '" + cCalismaNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                nFotf = oSQL.SQLReadDouble("finalonaytargetfiyat")
                cDoviz = oSQL.SQLReadString("doviz")
            End If
            oSQL.oReader.Close()

            If nFotf = 0 Then
                oSQL.CloseConn()
                Exit Function
            End If

            nRow = 0
            nMaxCols = 3
            ReDim aHTMLRow(nMaxCols, 7)
            aHTMLRow(0, 0).cValue = "Tipi"
            aHTMLRow(1, 0).cValue = "Onaylanmis " + cDoviz
            aHTMLRow(2, 0).cValue = "Yeni " + cDoviz
            aHTMLRow(3, 0).cValue = "Degisim %"

            oSQL.cSQLQuery = "select top 1 " +
                            " finalonaytargetfiyat, targetfiyat, " +
                            " finalonaytoplammaliyet, toplammaliyet, " +
                            " finalonaytoplamkumas, toplamkumas, " +
                            " finalonaytoplamaksesuar, toplamaksesuar, " +
                            " finalonaytoplamiscilik, toplamiscilik, " +
                            " finalonaytoplamdiger, toplamdiger " +
                            " from maliyetheader with (NOLOCK) " +
                            " where calismano = '" + cCalismaNo.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then

                aHTMLRow(0, 1).cValue = "Kumas"
                aHTMLRow(1, 1).cValue = Format(oSQL.SQLReadDouble("finalonaytoplamkumas"), G_Number2Format)
                aHTMLRow(2, 1).cValue = Format(oSQL.SQLReadDouble("toplamkumas"), G_Number2Format)
                If oSQL.SQLReadDouble("finalonaytoplamkumas") <> 0 Then
                    aHTMLRow(3, 1).cValue = Format((oSQL.SQLReadDouble("toplamkumas") - oSQL.SQLReadDouble("finalonaytoplamkumas")) / oSQL.SQLReadDouble("finalonaytoplamkumas") * 100, G_Number2Format)
                End If

                aHTMLRow(0, 2).cValue = "Aksesuar"
                aHTMLRow(1, 2).cValue = Format(oSQL.SQLReadDouble("finalonaytoplamaksesuar"), G_Number2Format)
                aHTMLRow(2, 2).cValue = Format(oSQL.SQLReadDouble("toplamaksesuar"), G_Number2Format)
                If oSQL.SQLReadDouble("finalonaytoplamaksesuar") <> 0 Then
                    aHTMLRow(3, 2).cValue = Format((oSQL.SQLReadDouble("toplamaksesuar") - oSQL.SQLReadDouble("finalonaytoplamaksesuar")) / oSQL.SQLReadDouble("finalonaytoplamaksesuar") * 100, G_Number2Format)
                End If

                aHTMLRow(0, 3).cValue = "Iscilik"
                aHTMLRow(1, 3).cValue = Format(oSQL.SQLReadDouble("finalonaytoplamiscilik"), G_Number2Format)
                aHTMLRow(2, 3).cValue = Format(oSQL.SQLReadDouble("toplamiscilik"), G_Number2Format)
                If oSQL.SQLReadDouble("finalonaytoplamiscilik") <> 0 Then
                    aHTMLRow(3, 3).cValue = Format((oSQL.SQLReadDouble("toplamiscilik") - oSQL.SQLReadDouble("finalonaytoplamiscilik")) / oSQL.SQLReadDouble("finalonaytoplamiscilik") * 100, G_Number2Format)
                End If

                aHTMLRow(0, 4).cValue = "Diger"
                aHTMLRow(1, 4).cValue = Format(oSQL.SQLReadDouble("finalonaytoplamdiger"), G_Number2Format)
                aHTMLRow(2, 4).cValue = Format(oSQL.SQLReadDouble("toplamdiger"), G_Number2Format)
                If oSQL.SQLReadDouble("finalonaytoplamdiger") <> 0 Then
                    aHTMLRow(3, 4).cValue = Format((oSQL.SQLReadDouble("toplamdiger") - oSQL.SQLReadDouble("finalonaytoplamdiger")) / oSQL.SQLReadDouble("finalonaytoplamdiger") * 100, G_Number2Format)
                End If

                aHTMLRow(0, 5).cValue = "Toplam Maliyet"
                aHTMLRow(1, 5).cValue = Format(oSQL.SQLReadDouble("finalonaytoplammaliyet"), G_Number2Format)
                aHTMLRow(2, 5).cValue = Format(oSQL.SQLReadDouble("toplammaliyet"), G_Number2Format)
                If oSQL.SQLReadDouble("finalonaytoplammaliyet") <> 0 Then
                    aHTMLRow(3, 5).cValue = Format((oSQL.SQLReadDouble("toplammaliyet") - oSQL.SQLReadDouble("finalonaytoplammaliyet")) / oSQL.SQLReadDouble("finalonaytoplammaliyet") * 100, G_Number2Format)
                End If

                aHTMLRow(0, 6).cValue = "Anlasilan Fiyat"
                aHTMLRow(1, 6).cValue = Format(oSQL.SQLReadDouble("finalonaytargetfiyat"), G_Number2Format)
                aHTMLRow(2, 6).cValue = Format(oSQL.SQLReadDouble("targetfiyat"), G_Number2Format)
                If oSQL.SQLReadDouble("finalonaytargetfiyat") <> 0 Then
                    aHTMLRow(3, 6).cValue = Format((oSQL.SQLReadDouble("targetfiyat") - oSQL.SQLReadDouble("finalonaytargetfiyat")) / oSQL.SQLReadDouble("finalonaytargetfiyat") * 100, G_Number2Format)
                End If

                If oSQL.SQLReadDouble("finalonaytoplammaliyet") <> 0 Then
                    nKar1 = (oSQL.SQLReadDouble("finalonaytargetfiyat") - oSQL.SQLReadDouble("finalonaytoplammaliyet")) / oSQL.SQLReadDouble("finalonaytoplammaliyet") * 100
                End If
                If oSQL.SQLReadDouble("targetfiyat") <> 0 Then
                    nKar2 = (oSQL.SQLReadDouble("targetfiyat") - oSQL.SQLReadDouble("toplammaliyet")) / oSQL.SQLReadDouble("toplammaliyet") * 100
                End If

                aHTMLRow(0, 7).cValue = "Kar %"
                aHTMLRow(1, 7).cValue = Format(nKar1, G_Number2Format)
                aHTMLRow(2, 7).cValue = Format(nKar2, G_Number2Format)
                aHTMLRow(3, 7).cValue = Format(nKar2 - nKar1, G_Number2Format)
            End If

            oSQL.oReader.Close()
            oSQL.CloseConn()

            GetOnMaliyetRevizeHTML = GetHTMLTable(7, nMaxCols, "Revize Final Ozeti")

        Catch ex As Exception
            ErrDisp(ex, "GetOnMaliyetRevizeHTML")
        End Try
    End Function

End Module
