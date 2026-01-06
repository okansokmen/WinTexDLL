Option Explicit On
Option Strict On

Imports System.Xml
Imports System.Configuration

Module UtilGenel

    Public Const G_NumberFormat = "###,###,###,###,###,##0"
    Public Const G_Number1Format = "###,###,###,###,###,##0.0"
    Public Const G_Number2Format = "###,###,###,###,###,##0.00"
    Public Const G_Number3Format = "###,###,###,###,###,##0.000"
    Public Const G_Number4Format = "###,###,###,###,###,##0.0000"
    Public Const G_Number5Format = "###,###,###,###,###,##0.00000"
    Public Const G_Number6Format = "###,###,###,###,###,##0.000000"

    Public oWinTexServiceMain As WinTexServiceMain
    Public cSrv As String = ""
    Public cDB As String = ""

    Public Function UyumEntegrasyon(cEntegrasyon As String, Optional cFilter As String = "", Optional cServer1 As String = "", Optional cDatabase1 As String = "", Optional cUsername1 As String = "", Optional cPassword1 As String = "") As Boolean

        Dim cSQL As String = ""

        UyumEntegrasyon = False

        Try
            Dim cServer As String = cServer1
            Dim cDatabase As String = cDatabase1
            Dim cUsername As String = cUsername1
            Dim cPassword As String = cPassword1

            Dim oSQL As New SQLServerClass(True,, cDatabase1)
            Dim oSQL2 As New SQLServerClass(True,, cDatabase1)
            Dim oWinTexDLL As WinTexDLL.HTMain
            Dim cMessage As String = ""
            Dim cServerInfo As String = ""
            Dim cSuccessMessage As String = ""
            Dim cFailMessage As String = ""
            Dim cControlMessage As String = ""
            Dim cStokFisNo As String = ""
            Dim cStokNo As String = ""
            Dim cSiparisNo As String = ""
            Dim cIsEmriNo As String = ""
            Dim cUretFisNo As String = ""
            Dim cModelNo As String = ""
            Dim cWinTexVersion As String = ""

            cServerInfo = "Server / Database / Username : " + cServer + " / " + cDatabase + " / " + cUsername
            cSuccessMessage = cEntegrasyon + vbCrLf + cServerInfo + vbCrLf + "Başarıyla tamamlandı : "
            cFailMessage = cEntegrasyon + vbCrLf + cServerInfo + vbCrLf + "Aktarım hatası : "
            cControlMessage = cEntegrasyon + vbCrLf + cServerInfo + vbCrLf + "Kontrol hatası : "

            oWinTexDLL = New WinTexDLL.HTMain

            If IsNothing(oWinTexDLL) Then
                Exit Function
            End If

            If oWinTexDLL.init(cServer, cDatabase, cUsername, cPassword) Then

                Select Case cEntegrasyon
                    Case "Uyumsoft - Stok Kartlarini Aktar"

                        oSQL.OpenConn()

                        cSQL = "select distinct a.stokno " +
                                " from stok a with (NOLOCK) , birim b with (NOLOCK) , stoktipi c with (NOLOCK)  " +
                                " where a.birim1 = b.birim " +
                                " And a.stoktipi = c.kod " +
                                " And a.stokno Is Not null " +
                                " And a.stokno <> '' " +
                                " and (a.uyumid Is null or a.uyumid = 0) " +
                                " and b.uyumid is not null " +
                                " and b.uyumid <> 0 " +
                                " and c.uyumid is not null " +
                                " and c.uyumid <> 0 " +
                                cFilter.Trim() +
                                " order by a.stokno "

                        oSQL.GetSQLReader(cSQL)

                        Do While oSQL.oReader.Read

                            cStokNo = oSQL.SQLReadString("stokno")
                            cFilter = " and a.stokno = '" + cStokNo + "' "

                            If oWinTexDLL.UyumStokAdd(cFilter, cMessage, True) Then
                                oWinTexServiceMain.CreateLog(cSuccessMessage + vbCrLf +
                                                             "Stok No : " + cStokNo)
                            Else
                                oWinTexServiceMain.CreateLog(cFailMessage + vbCrLf +
                                                             "Hata mesajı : " + cMessage + vbCrLf +
                                                             "Stok No : " + cStokNo)
                                CreateLogFile("Aktarılamayan stok no : " + cStokNo + vbCrLf +
                                                             "Hata mesajı : " + cMessage)
                            End If
                        Loop

                        oSQL.oReader.Close()
                        oSQL.CloseConn()

                    Case "Uyumsoft - Stok Hareketlerini Aktar"

                        oSQL.OpenConn()

                        cSQL = "select distinct a.stokfisno " +
                                " from stokfis a with (NOLOCK) , stokfislines b with (NOLOCK) , stok c with (NOLOCK) " +
                                " where a.stokfisno = b.stokfisno " +
                                " and b.stokno = c.stokno " +
                                " and a.stokfisno is not null " +
                                " and a.stokfisno <> '' " +
                                " and b.stokno is not null " +
                                " and b.stokno <> '' " +
                                " and (b.uyumid2 is null or b.uyumid2 = 0) " +
                                " and (a.donottransfer is null or a.donottransfer = 'H' or a.donottransfer = '') " +
                                cFilter.Trim() +
                                " order by a.stokfisno "

                        oSQL.GetSQLReader(cSQL)

                        Do While oSQL.oReader.Read

                            cStokFisNo = oSQL.SQLReadString("stokfisno")
                            cFilter = " and a.stokfisno = '" + cStokFisNo + "' "

                            If oWinTexDLL.UyumStokFisiKontrol(cFilter, cMessage) Then

                                cSQL = "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'uyumstokfisiaktar') EXECUTE uyumstokfisiaktar '" + cStokFisNo + "' "
                                oSQL2.SQLExecuteOpenCloseConnection(cSQL)

                                If oWinTexDLL.UyumStokFisiAktar(cFilter, cMessage) Then
                                    oWinTexServiceMain.CreateLog(cSuccessMessage + vbCrLf +
                                                                 "Fiş No : " + cStokFisNo)
                                Else
                                    oWinTexServiceMain.CreateLog(cFailMessage + vbCrLf +
                                                                 "Hata mesajı : " + cMessage + vbCrLf +
                                                                 "Fiş No : " + cStokFisNo)
                                    CreateLogFile("Aktarılamayan stok fiş no : " + cStokFisNo + vbCrLf +
                                                                 "Hata mesajı : " + cMessage)
                                End If
                            Else
                                oWinTexServiceMain.CreateLog(cControlMessage + vbCrLf +
                                                             "Hata mesajı : " + cMessage + vbCrLf +
                                                             "Fiş No : " + cStokFisNo)
                                CreateLogFile("Aktarılamayan stok fiş no : " + cStokFisNo + vbCrLf +
                                                             "Hata mesajı : " + cMessage)
                            End If
                        Loop

                        oSQL.oReader.Close()
                        oSQL.CloseConn()

                    Case "Uyumsoft - Model Recetelerini Aktar"

                        oSQL.OpenConn()

                        cSQL = "select distinct b.modelno " +
                                " from siparis a with (NOLOCK), sipmodel b with (NOLOCK), ymodel c with (NOLOCK) " +
                                " where a.kullanicisipno = b.siparisno " +
                                " and a.kullanicisipno is not null " +
                                " and a.kullanicisipno <> '' " +
                                " and b.modelno = c.modelno " +
                                " and b.modelno is not null " +
                                " and b.modelno <> '' " +
                                " and (c.uyumbomid is null or c.uyumbomid = 0) " +
                                cFilter.Trim() +
                                " order by b.modelno "

                        oSQL.GetSQLReader(cSQL)

                        Do While oSQL.oReader.Read

                            cModelNo = oSQL.SQLReadString("modelno")

                            If oWinTexDLL.UyumBOMAdd(cFilter, cMessage) Then
                                oWinTexServiceMain.CreateLog(cSuccessMessage + vbCrLf + "Model No : " + cModelNo)
                            Else
                                oWinTexServiceMain.CreateLog(cFailMessage + vbCrLf +
                                                             "Hata mesajı : " + cMessage + vbCrLf +
                                                             "Model No : " + cModelNo)
                                CreateLogFile("Aktarılamayan model no : " + cModelNo + vbCrLf +
                                                             "Hata mesajı : " + cMessage)
                            End If
                        Loop

                        oSQL.oReader.Close()
                        oSQL.CloseConn()

                    Case "Uyumsoft - Alinan Ihracat Siparislerini Aktar"

                        oSQL.OpenConn()

                        cSQL = "select distinct a.kullanicisipno " +
                                " from siparis a with (NOLOCK), sipmodel b with (NOLOCK), ymodel c with (NOLOCK) " +
                                " where a.kullanicisipno = b.siparisno " +
                                " and a.kullanicisipno is not null " +
                                " and a.kullanicisipno <> '' " +
                                " and b.modelno = c.modelno " +
                                " and b.modelno is not null " +
                                " and b.modelno <> '' " +
                                " and (a.uyumsiparisid is null or a.uyumsiparisid = 0) " +
                                cFilter.Trim() +
                                " order by a.kullanicisipno "

                        oSQL.GetSQLReader(cSQL)

                        Do While oSQL.oReader.Read

                            cSiparisNo = oSQL.SQLReadString("kullanicisipno")

                            If oWinTexDLL.UyumUretimSiparisAdd(cSiparisNo, cMessage) Then
                                oWinTexServiceMain.CreateLog(cSuccessMessage + vbCrLf +
                                                                            "Sipariş No : " + cSiparisNo)
                            Else
                                oWinTexServiceMain.CreateLog(cFailMessage + vbCrLf + cMessage + vbCrLf + "Sipariş No : " + cSiparisNo)
                                CreateLogFile("Aktarılamayan sipariş no : " + cSiparisNo + vbCrLf +
                                                                            "Hata mesajı : " + cMessage)
                            End If
                        Loop

                        oSQL.oReader.Close()
                        oSQL.CloseConn()

                    Case "Uyumsoft - Uretim Emirlerini Aktar"

                        oSQL.OpenConn()

                        cSQL = "select distinct a.kullanicisipno " +
                                " from siparis a with (NOLOCK), sipmodel b with (NOLOCK), ymodel c with (NOLOCK) " +
                                " where a.kullanicisipno = b.siparisno " +
                                " and a.kullanicisipno is not null " +
                                " and a.kullanicisipno <> '' " +
                                " and b.modelno = c.modelno " +
                                " and b.modelno is not null " +
                                " and b.modelno <> '' " +
                                " and (a.uyumisemriid is null or a.uyumisemriid = 0) " +
                                cFilter.Trim() +
                                " order by a.kullanicisipno "

                        oSQL.GetSQLReader(cSQL)

                        Do While oSQL.oReader.Read

                            cSiparisNo = oSQL.SQLReadString("kullanicisipno")

                            If oWinTexDLL.UyumUIEAdd(cSiparisNo, cMessage) Then
                                oWinTexServiceMain.CreateLog(cSuccessMessage + vbCrLf +
                                                             "Sipariş No : " + cSiparisNo)
                            Else
                                oWinTexServiceMain.CreateLog(cFailMessage + vbCrLf +
                                                             "Hata mesajı : " + cMessage + vbCrLf +
                                                             "Sipariş No : " + cSiparisNo)
                                CreateLogFile("Aktarılamayan sipariş no : " + cSiparisNo + vbCrLf +
                                                            "Hata mesajı : " + cMessage)
                            End If
                        Loop

                        oSQL.oReader.Close()
                        oSQL.CloseConn()

                    Case "Uyumsoft - Verilen (Satinalma) Siparisler Aktar"

                        oSQL.OpenConn()

                        cSQL = "select distinct a.isEmriNo " +
                                " from isemri a with (NOLOCK), isemrilines b with (NOLOCK) " +
                                " where a.isEmriNo = b.isEmriNo " +
                                " and a.isEmriNo is not null " +
                                " and a.isEmriNo <> '' " +
                                " and (a.uyumid is null or a.uyumid = 0) " +
                                cFilter.Trim() +
                                " order by a.isemrino "

                        oSQL.GetSQLReader(cSQL)

                        Do While oSQL.oReader.Read

                            cIsEmriNo = oSQL.SQLReadString("isEmriNo")

                            If oWinTexDLL.UyumSatinalmaSiparisAdd(cIsEmriNo, cMessage) Then
                                oWinTexServiceMain.CreateLog(cSuccessMessage + vbCrLf +
                                                             "İşemri No : " + cIsEmriNo)
                            Else
                                oWinTexServiceMain.CreateLog(cFailMessage + vbCrLf +
                                                             "Hata mesajı : " + cMessage + vbCrLf +
                                                             "İşemri No : " + cIsEmriNo)
                                CreateLogFile("Aktarılamayan işemri no : " + cIsEmriNo + vbCrLf +
                                                            "Hata mesajı : " + cMessage)
                            End If
                        Loop

                        oSQL.oReader.Close()
                        oSQL.CloseConn()

                    Case "Uyumsoft - Uretim (Hareket Fisleri) Aktar"

                        oSQL.OpenConn()

                        cSQL = "select distinct a.UretFisNo " +
                               " a.BelgeNo, a.FaturaTarihi, " +
                               " a.FaturaNo, a.CikisDept, a.CikisFirm_Atl, a.GirisDept, a.GirisFirm_Atl " +
                               " from uretharfis a with (NOLOCK), uretharfislines b with (NOLOCK), uretharrba c with (NOLOCK) " +
                               " where a.uretfisno = b.uretfisno " +
                               " and b.uretfisno = c.uretfisno " +
                               " and b.ulineno = c.ulineno " +
                               " and a.uretfisno is not null " +
                               " and a.uretfisno <> '' " +
                               " and (b.uyumid is null or b.uyumid = 0) " +
                                cFilter.Trim() +
                               " order by a.UretFisNo "

                        oSQL.GetSQLReader(cSQL)

                        Do While oSQL.oReader.Read

                            cUretFisNo = oSQL.SQLReadString("UretFisNo")

                            If oWinTexDLL.UyumUretimAdd(cUretFisNo, cMessage) Then
                                oWinTexServiceMain.CreateLog(cSuccessMessage + vbCrLf +
                                                             "Üretim Fiş No : " + cUretFisNo)
                            Else
                                oWinTexServiceMain.CreateLog(cFailMessage + vbCrLf +
                                                            "Hata mesajı : " + cMessage + vbCrLf +
                                                            "Üretim Fiş No : " + cUretFisNo)
                                CreateLogFile("Aktarılamayan üretim fiş no : " + cUretFisNo + vbCrLf +
                                                            "Hata mesajı : " + cMessage)
                            End If
                        Loop

                        oSQL.oReader.Close()
                        oSQL.CloseConn()
                End Select
            End If
            oWinTexDLL = Nothing

            oSQL = Nothing
            oSQL2 = Nothing

            UyumEntegrasyon = True

        Catch ex As Exception
            ErrDisp(ex, "UyumEntegrasyon", cSQL)
        End Try
    End Function

    Public Sub CorapMlzIsEmriBakim(cDatabase As String)

        Dim oSQL As New SQLServerClass(True,, cDatabase)

        Try
            oSQL.cSQLQuery = "select isemrino " +
                            " from isemri with (NOLOCK) " +
                            " where isemrino is not null " +
                            " and isemrino <> '' " +
                            " and (isemriok Is null or isemriok = 'H' or isemriok = '') " +
                            " and exists (select isemrino " +
                                        " from isemrilines with (NOLOCK)  " +
                                        " where isemrino = isemri.isemrino ) "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                CorapMlzIsEmriValidate(oSQL.SQLReadString("isemrino"), cDatabase)
            Loop
            oSQL.oReader.Close()
            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex, "CorapMlzIsEmriBakim")
        End Try
    End Sub

    Private Function CorapMlzIsEmriValidate(cisEmriNo As String, cDatabase As String) As Boolean

        Dim cSQL As String = ""
        Dim cSHK As String = ""
        Dim cMTF As String = ""
        Dim cStokNo As String = ""
        Dim cRenk As String = ""
        Dim cBeden As String = ""
        Dim nKalan As Double = 0
        Dim nIhtiyac As Double = 0
        Dim nSiraNo As Double = 0
        Dim nKullanilan As Double = 0
        Dim nKullanilan2 As Double = 0
        Dim nTamireCikan As Double = 0
        Dim nTamirdenGelen As Double = 0
        Dim nTedarikIade As Double = 0
        Dim nUretimIade As Double = 0
        Dim nTedarikGelen As Double = 0
        Dim nUretimGelen As Double = 0

        Dim oSQL1 As New SQLServerClass(True,, cDatabase)
        Dim oSQL2 As New SQLServerClass(True,, cDatabase)
        Dim oSQL3 As New SQLServerClass(True,, cDatabase)

        CorapMlzIsEmriValidate = False

        Try
            oSQL1.OpenConn()
            oSQL2.OpenConn()
            oSQL3.OpenConn()

            If Trim$(cisEmriNo) = "" Then Exit Function

            cSQL = "update isemrilines " +
                    " set tedarikgelen = 0, " +
                    " tedarikiade = 0, " +
                    " uretimgelen = 0, " +
                    " uretimiade = 0, " +
                    " tamirgelen = 0, " +
                    " tamirgiden = 0, " +
                    " toplamsatis = 0," +
                    " toplamsatisiade = 0" +
                    " where isemrino = '" + cisEmriNo.Trim + "' "

            oSQL1.SQLExecute(cSQL)

            cSQL = "update isemrilines " +
                    " set malzemetakipno = '' " +
                    " where isemrino = '" + cisEmriNo.Trim + "' " +
                    " and malzemetakipno is null "

            oSQL1.SQLExecute(cSQL)

            ' depoya / isemrine girisler

            cSQL = "select b.stokhareketkodu, b.malzemetakipkodu, b.stokno, b.renk, b.beden, " +
                    " netmiktar1 = sum(coalesce(b.netmiktar1,0)) " +
                    " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK) " +
                    " where a.stokfisno = b.stokfisno " +
                    " and b.isemrino = '" + cisEmriNo.Trim + "' " +
                    " and b.isemrino is not null " +
                    " and b.isemrino <> '' " +
                    " and b.netmiktar1 is not null " +
                    " and b.netmiktar1 <> 0 " +
                    " and a.stokfistipi = 'Giris' " +
                    " group by b.stokhareketkodu, b.malzemetakipkodu, b.stokno, b.renk, b.beden " +
                    " order by b.stokhareketkodu, b.malzemetakipkodu, b.stokno, b.renk, b.beden "

            oSQL1.GetSQLReader(cSQL)

            Do While oSQL1.oReader.Read

                cSHK = oSQL1.SQLReadString("stokhareketkodu")
                cMTF = oSQL1.SQLReadString("malzemetakipkodu")
                cStokNo = oSQL1.SQLReadString("stokno")
                cRenk = oSQL1.SQLReadString("renk")
                cBeden = oSQL1.SQLReadString("beden")
                nKalan = oSQL1.SQLReadDouble("netmiktar1")
                nTamirdenGelen = 0
                nTedarikGelen = 0
                nUretimGelen = 0
                nSiraNo = 0

                Select Case cSHK
                    Case "02 Tedarikten Giris", "05 Diger Giris"
                        nTedarikGelen = oSQL1.SQLReadDouble("netmiktar1")
                    Case "04 Mlz Uretimden Giris"
                        nUretimGelen = oSQL1.SQLReadDouble("netmiktar1")
                    Case "06 Tamirden Giris"
                        nTamirdenGelen = oSQL1.SQLReadDouble("netmiktar1")
                End Select

                ' gelen miktari paylastir

                cSQL = "select w.* " +
                    " from (select sirano, malzemetakipno, miktar1, uretimgelen, tedarikgelen, " +
                            " tamirgelen, toplamsatis, toplamsatisiade, termintarihi, "
                cSQL = cSQL +
                            " siparistermin = COALESCE((select top 1 a.ilksevktarihi " +
                                                        " from siparis a with (NOLOCK), sipmodel b with (NOLOCK) " +
                                                        " Where a.kullanicisipno = b.siparisno " +
                                                        " and b.malzemetakipno = isemrilines.malzemetakipno " +
                                                        " order by a.ilksevktarihi),CONVERT(DATETIME,'01.01.2099')) "
                cSQL = cSQL +
                            " from isemrilines with (NOLOCK) " +
                            " where isemrino = '" + cisEmriNo + "' " +
                            " and malzemetakipno = '" + cMTF + "' " +
                            " and stokno = '" + cStokNo + "' " +
                            " and renk = '" + cRenk + "' " +
                            " and beden = '" + cBeden + "') w " +
                   " order by w.termintarihi, w.siparistermin  "

                oSQL2.GetSQLReader(cSQL)

                Do While oSQL2.oReader.Read

                    nSiraNo = oSQL2.SQLReadDouble("SiraNo")
                    '        nIhtiyac = NumNull(oRDO2!miktar1) - NumNull(oRDO2!TedarikGelen) - NumNull(oRDO2!UretimGelen) - NumNull(oRDO2!TamirGelen)

                    Select Case cSHK
                        Case "02 Tedarikten Giris", "05 Diger Giris"
                            If nTedarikGelen > oSQL2.SQLReadDouble("miktar1") Then
                                nKullanilan = oSQL2.SQLReadDouble("miktar1")
                                nTedarikGelen = nTedarikGelen - oSQL2.SQLReadDouble("miktar1")
                            Else
                                nKullanilan = nTedarikGelen
                                nTedarikGelen = 0
                            End If

                            If nKullanilan > 0 Then

                                cSQL = "update isemrilines " +
                                " set tedarikgelen = coalesce(tedarikgelen,0) + " + CStr(nKullanilan) +
                                " where sirano = " + CStr(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If

                        Case "04 Mlz Uretimden Giris"
                            If nUretimGelen > oSQL2.SQLReadDouble("miktar1") Then
                                nKullanilan = oSQL2.SQLReadDouble("miktar1")
                                nUretimGelen = nUretimGelen - oSQL2.SQLReadDouble("miktar1")
                            Else
                                nKullanilan = nUretimGelen
                                nUretimGelen = 0
                            End If

                            If nKullanilan > 0 Then

                                cSQL = "update isemrilines " +
                                    " set uretimgelen = coalesce(uretimgelen,0) + " + CStr(nKullanilan) +
                                    " where sirano = " + CStr(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If

                        Case "06 Tamirden Giris"
                            If nTamirdenGelen > oSQL2.SQLReadDouble("miktar1") Then
                                nKullanilan = oSQL2.SQLReadDouble("miktar1")
                                nTamirdenGelen = nTamirdenGelen - oSQL2.SQLReadDouble("miktar1")
                            Else
                                nKullanilan = nTamirdenGelen
                                nTamirdenGelen = 0
                            End If

                            If nKullanilan > 0 Then

                                cSQL = "update isemrilines " +
                                    " set tamirgelen = coalesce(tamirgelen,0) + " + CStr(nKullanilan) +
                                    " where sirano = " + CStr(nSiraNo)

                                oSQL3.SQLExecute(cSQL)

                                cSQL = "update isemrilines " +
                                    " set tedarikgelen = coalesce(tedarikgelen,0) + " + CStr(nKullanilan) +
                                    " where sirano = " + CStr(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If

                        Case "07 Satis Iade"
                            If nKalan > 0 Then

                                cSQL = "update isemrilines " +
                                    " set toplamsatisiade = coalesce(toplamsatisiade,0) + " + CStr(nKalan) +
                                    " where sirano = " + CStr(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If
                    End Select
                Loop
                oSQL2.oReader.Close()

                ' gelen miktar paylastirildiktan sonra elde kalan varsa son satira at
                If nSiraNo <> 0 Then
                    Select Case cSHK
                        Case "02 Tedarikten Giris", "05 Diger Giris"
                            If nTedarikGelen > 0 Then
                                cSQL = "update isemrilines " +
                                    " set tedarikgelen = coalesce(tedarikgelen,0) + " + SQLWriteDecimal(nTedarikGelen) +
                                    " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If
                        Case "04 Mlz Uretimden Giris"
                            If nUretimGelen > 0 Then
                                cSQL = "update isemrilines " +
                                    " set uretimgelen = coalesce(uretimgelen,0) + " + SQLWriteDecimal(nUretimGelen) +
                                    " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If
                        Case "06 Tamirden Giris"
                            If nTamirdenGelen > 0 Then
                                cSQL = "update isemrilines " +
                                    " set tamirgelen = coalesce(tamirgelen,0) + " + SQLWriteDecimal(nTamirdenGelen) +
                                    " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)

                                cSQL = "update isemrilines " +
                                    " set tedarikgelen = coalesce(tedarikgelen,0) + " + SQLWriteDecimal(nTamirdenGelen) +
                                    " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If
                    End Select
                End If

            Loop
            oSQL1.oReader.Close()

            ' depodan / isemrinden cikislar

            cSQL = "select b.stokhareketkodu, b.malzemetakipkodu, b.stokno, b.renk, b.beden, " +
                    " netmiktar1 = sum(coalesce(b.netmiktar1,0)) " +
                    " from stokfis a with (NOLOCK), stokfislines b with (NOLOCK) " +
                    " where a.stokfisno = b.stokfisno " +
                    " and b.isemrino = '" + cisEmriNo.Trim + "' " +
                    " and b.isemrino is not null " +
                    " and b.isemrino <> '' " +
                    " and b.netmiktar1 is not null " +
                    " and b.netmiktar1 <> 0 " +
                    " and a.stokfistipi = 'Cikis' " +
                    " group by b.stokhareketkodu, b.malzemetakipkodu, b.stokno, b.renk, b.beden " +
                    " order by b.stokhareketkodu, b.malzemetakipkodu, b.stokno, b.renk, b.beden "

            oSQL1.GetSQLReader(cSQL)

            Do While oSQL1.oReader.Read

                cSHK = oSQL1.SQLReadString("stokhareketkodu")
                cMTF = oSQL1.SQLReadString("malzemetakipkodu")
                cStokNo = oSQL1.SQLReadString("stokno")
                cRenk = oSQL1.SQLReadString("renk")
                cBeden = oSQL1.SQLReadString("beden")
                nKalan = oSQL1.SQLReadDouble("netmiktar1")
                nTamireCikan = 0
                nTedarikIade = 0
                nUretimIade = 0
                nSiraNo = 0

                Select Case cSHK
                    Case "02 Tedarikten iade", "05 Diger Cikis"
                        nTedarikIade = oSQL1.SQLReadDouble("netmiktar1")
                    Case "04 Mlz Uretime iade"
                        nUretimIade = oSQL1.SQLReadDouble("netmiktar1")
                    Case "06 Tamire Cikis"
                        nTamireCikan = oSQL1.SQLReadDouble("netmiktar1")
                End Select

                ' cikan miktari paylastir

                cSQL = "select w.* " +
                    " from (select sirano, malzemetakipno, miktar1, uretimgelen, tedarikgelen, " +
                            " tamirgelen, toplamsatis, toplamsatisiade, termintarihi, "
                cSQL = cSQL +
                            " siparistermin = COALESCE((select top 1 a.ilksevktarihi " +
                                                        " from siparis a with (NOLOCK), sipmodel b with (NOLOCK) " +
                                                        " Where a.kullanicisipno = b.siparisno " +
                                                        " and b.malzemetakipno = isemrilines.malzemetakipno " +
                                                        " order by a.ilksevktarihi),CONVERT(DATETIME,'01.01.2099')) "
                cSQL = cSQL +
                            " from isemrilines with (NOLOCK) " +
                            " where isemrino = '" + cisEmriNo + "' " +
                            " and malzemetakipno = '" + cMTF + "' " +
                            " and stokno = '" + cStokNo + "' " +
                            " and renk = '" + cRenk + "' " +
                            " and beden = '" + cBeden + "') w " +
                   " order by w.termintarihi , w.siparistermin  "

                oSQL2.GetSQLReader(cSQL)

                Do While oSQL2.oReader.Read

                    nSiraNo = oSQL2.SQLReadDouble("SiraNo")

                    Select Case cSHK
                    ' depodan cikislar
                        Case "02 Tedarikten iade", "05 Diger Cikis"
                            If nTedarikIade > oSQL2.SQLReadDouble("miktar1") Then
                                nKullanilan = oSQL2.SQLReadDouble("miktar1")
                                nTedarikIade = nTedarikIade - oSQL2.SQLReadDouble("miktar1")
                            Else
                                nKullanilan = nTedarikIade
                                nTedarikIade = 0
                            End If

                            If nKullanilan > 0 Then

                                cSQL = "update isemrilines " +
                                    " set tedarikiade = coalesce(tedarikiade,0) + " + SQLWriteDecimal(nKullanilan) +
                                    " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)

                                cSQL = "update isemrilines " +
                                    " set tedarikgelen = coalesce(tedarikgelen,0) - " + SQLWriteDecimal(nKullanilan) +
                                    " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If

                        Case "06 Tamire Cikis"
                            If nTamireCikan > oSQL2.SQLReadDouble("miktar1") Then
                                nKullanilan = oSQL2.SQLReadDouble("miktar1")
                                nTamireCikan = nTamireCikan - oSQL2.SQLReadDouble("miktar1")
                            Else
                                nKullanilan = nTamireCikan
                                nTamireCikan = 0
                            End If

                            If nKullanilan > 0 Then

                                cSQL = "update isemrilines " +
                                        " set tamirgiden = coalesce(tamirgiden,0) + " + SQLWriteDecimal(nKullanilan) +
                                        " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)

                                cSQL = "update isemrilines " +
                                        " set tedarikgelen = coalesce(tedarikgelen,0) - " + SQLWriteDecimal(nKullanilan) +
                                        " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If

                        Case "04 Mlz Uretime iade"
                            If nUretimIade > oSQL2.SQLReadDouble("miktar1") Then
                                nKullanilan = oSQL2.SQLReadDouble("miktar1")
                                nUretimIade = nUretimIade - oSQL2.SQLReadDouble("miktar1")
                            Else
                                nKullanilan = nUretimIade
                                nUretimIade = 0
                            End If

                            If nKullanilan > 0 Then

                                cSQL = "update isemrilines " +
                                        " set uretimiade = coalesce(uretimiade,0) + " + SQLWriteDecimal(nKullanilan) +
                                        " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)

                                cSQL = "update isemrilines " +
                                        " set uretimgelen = coalesce(uretimgelen,0) - " + SQLWriteDecimal(nKullanilan) +
                                        " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If

                        Case "07 Satis"
                            If nKalan > 0 Then

                                cSQL = "update isemrilines " +
                                        " set toplamsatis = coalesce(toplamsatis,0) + " + SQLWriteDecimal(nKalan) +
                                        " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If
                    End Select
                Loop
                oSQL2.oReader.Close()

                ' cikan miktar paylastirildiktan sonra elde kalan varsa son satira at
                If nSiraNo <> 0 Then
                    Select Case cSHK
                        Case "02 Tedarikten iade", "05 Diger Cikis"
                            If nTedarikIade > 0 Then

                                cSQL = "update isemrilines " +
                                        " set tedarikiade = coalesce(tedarikiade,0) + " + SQLWriteDecimal(nTedarikIade) +
                                        " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)

                                cSQL = "update isemrilines " +
                                        " set tedarikgelen = coalesce(tedarikgelen,0) - " + SQLWriteDecimal(nTedarikIade) +
                                        " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If

                        Case "06 Tamire Cikis"
                            If nTamireCikan > 0 Then

                                cSQL = "update isemrilines " +
                                        " set tamirgiden = coalesce(tamirgiden,0) + " + SQLWriteDecimal(nTamireCikan) +
                                        " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)

                                cSQL = "update isemrilines " +
                                        " set tedarikgelen = coalesce(tedarikgelen,0) - " + SQLWriteDecimal(nTamireCikan) +
                                        " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If

                        Case "04 Mlz Uretime iade"
                            If nUretimIade > 0 Then

                                cSQL = "update isemrilines " +
                                        " set uretimiade = coalesce(uretimiade,0) + " + SQLWriteDecimal(nUretimIade) +
                                        " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)

                                cSQL = "update isemrilines " +
                                        " set uretimgelen = coalesce(uretimgelen,0) - " + SQLWriteDecimal(nUretimIade) +
                                        " where sirano = " + SQLWriteDecimal(nSiraNo)

                                oSQL3.SQLExecute(cSQL)
                            End If
                    End Select
                End If
            Loop
            oSQL1.oReader.Close()

            oSQL1.CloseConn()
            oSQL2.CloseConn()
            oSQL3.CloseConn()

            oSQL1 = Nothing
            oSQL2 = Nothing
            oSQL3 = Nothing

            CorapMlzIsEmriValidate = True

        Catch ex As Exception
            ErrDisp(ex, "MlzIsEmriValidate", cSQL)
        End Try
    End Function

    Public Sub UretimIsemriBakimi(Optional cFilter As String = "", Optional cDatabase As String = "")

        Dim cSQL As String = ""
        Dim oSQL As New SQLServerClass(True,, cDatabase)

        Try
            oSQL.OpenConn()

            oSQL.cSQLQuery = "update uretimisdetayi " +
                               " set toplamadet = (select sum(coalesce(adet,0)) " +
                                                " from uretimisrba with (NOLOCK) " +
                                                " where ulineno = uretimisdetayi.ulineno " +
                                                " and uretimtakipno = uretimisdetayi.uretimtakipno " +
                                                " and isemrino = uretimisdetayi.isemrino) " +
                                " where isemrino is not null " +
                                " and isemrino <> '' " +
                                cFilter

            oSQL.SQLExecute()

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex, "UretimIsemriBakimi")
        End Try
    End Sub

    Public Sub CorapStokBakimi(cDatabase As String)

        Try
            Dim oSQL As SQLServerClass

            oSQL = New SQLServerClass(True,, cDatabase)
            oSQL.OpenConn()
            oSQL.CLRExecute("stokbakim")
            oSQL.CloseConn()
            oSQL = Nothing

            StokParti2Bakim(cDatabase)

            'HizliStokRBBakimi(cDatabase)
            'HizliStokTopRBBakimi(cDatabase)
            'HizliStokAksesuarRBBakimi(cDatabase)

        Catch ex As Exception
            ErrDisp(ex, "CorapStokBakimi")
        End Try
    End Sub

    Private Sub StokParti2Bakim(cDatabase As String)

        Dim cSQL As String = ""
        Dim cSQLStok As String = ""
        Dim oSQL As New SQLServerClass(True,, cDatabase)

        Try
            oSQL.OpenConn()

            cSQL = "UPDATE stokfislines " +
                    " SET partino2 = (SELECT TOP 1 partino2 " +
                                    " FROM topongirislines with (NOLOCK) " +
                                    " WHERE topno = stokfislines.topno) " +
                    " WHERE (partino2 Is NULL Or partino2 = '') " +
                    " AND topno IS NOT NULL " +
                    " AND topno <> '' " +
                    " AND EXISTS (SELECT partino2 " +
                                    " FROM topongirislines with (NOLOCK) " +
                                    " WHERE topno = stokfislines.topno " +
                                    " And partino2 Is Not NULL " +
                                    " And partino2 <> '') "
            oSQL.SQLExecute(cSQL)

            cSQL = "update stoktransfer " +
                    " SET hedefpartino2 = (SELECT TOP 1 partino2 " +
                                    " FROM topongirislines with (NOLOCK) " +
                                    " WHERE topno = stoktransfer.topno) " +
                    " WHERE (hedefpartino2 Is NULL Or hedefpartino2 = '') " +
                    " AND topno IS NOT NULL " +
                    " AND topno <> '' " +
                    " AND EXISTS (SELECT partino2 " +
                                    " FROM topongirislines with (NOLOCK) " +
                                    " WHERE topno = stoktransfer.topno " +
                                    " And partino2 Is Not NULL " +
                                    " And partino2 <> '') "
            oSQL.SQLExecute(cSQL)

            cSQL = "update stoktransfer " +
                    " SET kaynakpartino2 = (SELECT TOP 1 partino2 " +
                                    " FROM topongirislines with (NOLOCK) " +
                                    " WHERE topno = stoktransfer.topno) " +
                    " WHERE (kaynakpartino2 Is NULL Or kaynakpartino2 = '') " +
                    " AND topno IS NOT NULL " +
                    " AND topno <> '' " +
                    " AND EXISTS (SELECT partino2 " +
                                    " FROM topongirislines with (NOLOCK) " +
                                    " WHERE topno = stoktransfer.topno " +
                                    " And partino2 Is Not NULL " +
                                    " And partino2 <> '') "
            oSQL.SQLExecute(cSQL)

            cSQL = "truncate table stokparti2"

            oSQL.SQLExecute(cSQL)

            cSQLStok = GetSQLStokDurumu()

            cSQL = "insert stokparti2 (stokno, renk, beden, depo, partino2, malzemetakipno, giren, cikan) " + cSQLStok

            oSQL.SQLExecute(cSQL)

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex, "StokParti2Bakim", cSQL)
        End Try
    End Sub

    Private Function GetSQLStokDurumu(Optional cHareketFilter As String = "", Optional cStokDurumuFilter As String = "", Optional lTransferlerDahil As Boolean = True) As String

        Dim cSQL As String = ""

        GetSQLStokDurumu = ""

        Try
            cSQL = "select stokno = coalesce(b.stokno,''), " +
                    " renk = coalesce(b.renk,''), " +
                    " beden = coalesce(b.beden,''), " +
                    " depo = coalesce(b.depo,''), " +
                    " partino2 = coalesce(b.partino2,''), " +
                    " malzemetakipno = coalesce(b.malzemetakipkodu,''), " +
                    " giris = sum(coalesce(b.netmiktar1,0)), " +
                    " cikis = 0 " +
                    " from stokfis a with (NOLOCK) , stokfislines b with (NOLOCK) , stok c with (NOLOCK) " +
                    " where a.stokfisno = b.stokfisno " +
                    " and a.stokfistipi in ('Giris','02 Satis Iade','03 Defolu iade') " +
                    " and (a.iptal = 'H' OR a.iptal = '' or a.iptal is null) " +
                    " and b.stokno = c.stokno " +
                    " and (c.kapandi = 'H' OR c.kapandi = '' OR c.kapandi is null) "

            If cHareketFilter.Trim <> "" Then
                cSQL = cSQL + cHareketFilter
            End If

            cSQL = cSQL +
                    " group by b.stokno, b.renk, b.beden, b.depo, b.partino2, b.malzemetakipkodu " +
                    " Union All "

            cSQL = cSQL +
                    " select stokno = coalesce(b.stokno,''), " +
                    " renk = coalesce(b.renk,''), " +
                    " beden = coalesce(b.beden,''), " +
                    " depo = coalesce(b.depo,''), " +
                    " partino2 = coalesce(b.partino2,''), " +
                    " malzemetakipno = coalesce(b.malzemetakipkodu,''), " +
                    " giris = 0, " +
                    " cikis = sum(coalesce(b.netmiktar1,0)) " +
                    " from stokfis a with (NOLOCK) , stokfislines b with (NOLOCK) , stok c with (NOLOCK)  " +
                    " where a.stokfisno = b.stokfisno  " +
                    " and a.stokfistipi in ('Cikis','01 Satis') " +
                    " and (a.iptal = 'H' OR a.iptal = '' or a.iptal is null) " +
                    " and b.stokno = c.stokno " +
                    " and (c.kapandi = 'H' OR c.kapandi = '' OR c.kapandi is null) "

            If cHareketFilter.Trim <> "" Then
                cSQL = cSQL + cHareketFilter
            End If

            cSQL = cSQL +
                    " group by b.stokno, b.renk, b.beden, b.depo, b.partino2, b.malzemetakipkodu "

            If lTransferlerDahil Then
                cSQL = cSQL +
                    " Union All "

                cSQL = cSQL +
                    "select stokno = coalesce(a.stokno,''), " +
                    " renk = coalesce(a.renk,''), " +
                    " beden = coalesce(a.beden,''), " +
                    " depo = coalesce(a.hedefdepo,''), " +
                    " partino2 = coalesce(a.hedefpartino2,''), " +
                    " malzemetakipno = coalesce(a.hedefmalzemetakipno,''), " +
                    " giris = sum(coalesce(a.netmiktar1,0)), " +
                    " cikis = 0 " +
                    " from StokTransfer a with (NOLOCK) , stok b with (NOLOCK)  " +
                    " where a.stokno = b.stokno " +
                    " and (b.kapandi is null or b.kapandi = 'H') "

                If cHareketFilter.Trim <> "" Then
                    cSQL = cSQL + cHareketFilter
                End If

                cSQL = cSQL +
                    " group by a.stokno, a.renk, a.beden, a.hedefdepo, a.hedefpartino2, a.hedefmalzemetakipno " +
                    " Union All "

                cSQL = cSQL +
                    " select stokno = coalesce(a.stokno,''), " +
                    " renk = coalesce(a.renk,''), " +
                    " beden = coalesce(a.beden,''), " +
                    " depo = coalesce(a.kaynakdepo,''), " +
                    " partino2 = coalesce(a.kaynakpartino2,''), " +
                    " malzemetakipno = coalesce(a.kaynakmalzemetakipno,''), " +
                    " giris = 0, " +
                    " cikis = sum(coalesce(a.netmiktar1,0)) " +
                    " from StokTransfer a with (NOLOCK) , stok b with (NOLOCK)  " +
                    " where a.stokno = b.stokno " +
                    " and (b.kapandi is null or b.kapandi = 'H') "

                If cHareketFilter.Trim <> "" Then
                    cSQL = cSQL + cHareketFilter
                End If

                cSQL = cSQL +
                    " group by a.stokno, a.renk, a.beden, a.kaynakdepo, a.kaynakpartino2, a.kaynakmalzemetakipno "
            End If

            GetSQLStokDurumu = "select w.stokno, w.renk, w.beden, w.depo, w.partino2, w.malzemetakipno, " +
                                " giren = sum(coalesce(w.giris,0)), " +
                                " cikan = sum(coalesce(w.cikis,0)) " +
                                " from (" + cSQL + ") w " +
                                " where w.stokno is not null " +
                                " and w.stokno <> '' " +
                                cStokDurumuFilter +
                                " group by w.stokno, w.renk, w.beden, w.depo, w.partino2, w.malzemetakipno "

        Catch ex As Exception
            ErrDisp(ex, "GetSQLStokDurumu", cSQL)
        End Try
    End Function

    Private Function GetViewStokDurumu(ByVal nCase As Integer, Optional ByVal cFilterFis As String = "", Optional ByVal cFilterTransfer1 As String = "",
                                      Optional ByVal cFilterTransfer2 As String = "", Optional ByVal dTarih As Date = #1/1/1950#,
                                      Optional cDatabase As String = "") As String
        ' nCase = 1 stokRB
        ' nCase = 2 stokTopRB
        ' nCase = 3 stokAksesuarRB
        Dim cSQL As String = ""
        Dim cStkHarMTFFilter As String = ""
        Dim cTranMTFFilter As String = ""
        Dim cStokFisTarihFilter As String = ""
        Dim cTransferTarihFilter As String = ""
        Dim oSQL As New SQLServerClass(True,, cDatabase)

        GetViewStokDurumu = ""

        Try
            oSQL.OpenConn()

            If dTarih <> CDate("01.01.1950") Then
                cStokFisTarihFilter = " and a.fistarihi <= '" + SQLWriteDate(dTarih) + "' "
                cTransferTarihFilter = " and a.tarih <= '" + SQLWriteDate(dTarih) + "' "
            End If

            Select Case nCase
                Case 1
                    cStkHarMTFFilter = " malzemetakipkodu = coalesce(b.malzemetakipkodu,''), "
                Case 2
                    cStkHarMTFFilter = " malzemetakipkodu = '', "
                Case 3
                    cStkHarMTFFilter = " malzemetakipkodu = '', "
            End Select

            cSQL = "select stokno = coalesce(b.stokno,''), " +
                    IIf(nCase = 1, "", " topno = coalesce(b.topno,''), ").ToString +
                    " renk = coalesce(b.renk,''), " +
                    " beden = coalesce(b.beden,''), " +
                    cStkHarMTFFilter +
                    " partino = coalesce(b.partino,''), " +
                    " depo = coalesce(b.depo,''), " +
                    " c.anastokgrubu, " +
                    " c.stoktipi, " +
                    " c.cinsaciklamasi, " +
                    " giris = sum(coalesce(b.netmiktar1,0)), " +
                    " cikis = 0 " +
                    " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK), stok c WITH (NOLOCK) " +
                    " where a.stokfisno = b.stokfisno " +
                    " and a.stokfistipi in ('Giris','02 Satis Iade','03 Defolu iade') " +
                    " and (a.iptal <> 'E' or a.iptal is null) " +
                    " and b.stokno = c.stokno " +
                    " and (c.kapandi is null or c.kapandi = 'H') " +
                    cFilterFis + cStokFisTarihFilter

            Select Case nCase
                Case 1
                    cSQL = cSQL +
                        " group by b.stokno, b.renk, b.beden, b.malzemetakipkodu, b.partino, b.depo, c.anastokgrubu, c.stoktipi, c.cinsaciklamasi " +
                        " Union All "
                Case 2
                    cSQL = cSQL +
                        " and c.toptakibi = 'E' " +
                        " and b.topno is not null " +
                        " and b.topno <> '' " +
                        " group by b.topno, b.stokno, b.renk, b.beden, b.malzemetakipkodu, b.partino, b.depo, c.anastokgrubu, c.stoktipi, c.cinsaciklamasi " +
                        " Union All "
                Case 3
                    cSQL = cSQL +
                        " and c.aksesuartakibi = 'E' " +
                        " and b.topno is not null " +
                        " and b.topno <> '' " +
                        " group by b.topno, b.stokno, b.renk, b.beden, b.malzemetakipkodu, b.partino, b.depo, c.anastokgrubu, c.stoktipi, c.cinsaciklamasi " +
                        " Union All "
                Case 4
                    cSQL = cSQL +
                        " and (c.toptakibi = 'E' or c.aksesuartakibi = 'E') " +
                        " and b.topno is not null " +
                        " and b.topno <> '' " +
                        " group by b.topno, b.stokno, b.renk, b.beden, b.malzemetakipkodu, b.partino, b.depo, c.anastokgrubu, c.stoktipi, c.cinsaciklamasi " +
                        " Union All "
            End Select

            cSQL = cSQL +
                    " select stokno = coalesce(b.stokno,''), " +
                    IIf(nCase = 1, "", " topno = coalesce(b.topno,''), ").ToString +
                    " renk = coalesce(b.renk,''), " +
                    " beden = coalesce(b.beden,''), " +
                    cStkHarMTFFilter +
                    " partino = coalesce(b.partino,''), " +
                    " depo = coalesce(b.depo,''), " +
                    " c.anastokgrubu, " +
                    " c.stoktipi, " +
                    " c.cinsaciklamasi, " +
                    " giris = 0, " +
                    " cikis = sum(coalesce(b.netmiktar1,0)) " +
                    " from stokfis a WITH (NOLOCK) , stokfislines b WITH (NOLOCK), stok c WITH (NOLOCK) " +
                    " where a.stokfisno = b.stokfisno  " +
                    " and a.stokfistipi in ('Cikis','01 Satis') " +
                    " and (a.iptal <> 'E' or a.iptal is null) " +
                    " and b.stokno = c.stokno " +
                    " and (c.kapandi is null or c.kapandi = 'H') " +
                    cFilterFis + cStokFisTarihFilter

            Select Case nCase
                Case 1
                    cSQL = cSQL +
                        " group by b.stokno, b.renk, b.beden, b.malzemetakipkodu, b.partino, b.depo, c.anastokgrubu, c.stoktipi, c.cinsaciklamasi " +
                        " Union All "
                Case 2
                    cSQL = cSQL +
                        " and c.toptakibi = 'E' " +
                        " and b.topno is not null " +
                        " and b.topno <> '' " +
                        " group by b.topno, b.stokno, b.renk, b.beden, b.malzemetakipkodu, b.partino, b.depo, c.anastokgrubu, c.stoktipi, c.cinsaciklamasi " +
                        " Union All "
                Case 3
                    cSQL = cSQL +
                        " and c.aksesuartakibi = 'E' " +
                        " and b.topno is not null " +
                        " and b.topno <> '' " +
                        " group by b.topno, b.stokno, b.renk, b.beden, b.malzemetakipkodu, b.partino, b.depo, c.anastokgrubu, c.stoktipi, c.cinsaciklamasi " +
                        " Union All "
                Case 4
                    cSQL = cSQL +
                        " and (c.toptakibi = 'E' or c.aksesuartakibi = 'E') " +
                        " and b.topno is not null " +
                        " and b.topno <> '' " +
                        " group by b.topno, b.stokno, b.renk, b.beden, b.malzemetakipkodu, b.partino, b.depo, c.anastokgrubu, c.stoktipi, c.cinsaciklamasi " +
                        " Union All "
            End Select

            Select Case nCase
                Case 1
                    cTranMTFFilter = " malzemetakipkodu = coalesce(a.hedefmalzemetakipno ,''), "
                Case 2
                    cTranMTFFilter = " malzemetakipkodu = '', "
                Case 3
                    cTranMTFFilter = " malzemetakipkodu = '', "
            End Select

            cSQL = cSQL +
                    "select stokno = coalesce(a.stokno,''), " +
                    IIf(nCase = 1, "", " topno = coalesce(a.topno,''), ").ToString +
                    " renk = coalesce(a.renk ,''), " +
                    " beden = coalesce(a.beden ,''), " +
                    cTranMTFFilter +
                    " partino = coalesce(a.hedefpartino,''), " +
                    " depo = coalesce(a.hedefdepo,''), " +
                    " b.anastokgrubu, " +
                    " b.stoktipi, " +
                    " b.cinsaciklamasi, " +
                    " giris = sum(coalesce(a.netmiktar1,0)), " +
                    " cikis = 0 " +
                    " from StokTransfer a WITH (NOLOCK), stok b WITH (NOLOCK) " +
                    " where a.stokno = b.stokno " +
                    " and (b.kapandi is null or b.kapandi = 'H') " +
                    cFilterTransfer1 + cTransferTarihFilter

            Select Case nCase
                Case 1
                    cSQL = cSQL +
                        " group by a.stokno, a.renk, a.beden, a.hedefmalzemetakipno, a.hedefpartino, a.hedefdepo, b.anastokgrubu, b.stoktipi, b.cinsaciklamasi  " +
                        " Union All "
                Case 2
                    cSQL = cSQL +
                        " and b.toptakibi = 'E' " +
                        " and a.topno is not null " +
                        " and a.topno <> '' " +
                        " group by a.topno, a.stokno, a.renk, a.beden, a.hedefmalzemetakipno, a.hedefpartino, a.hedefdepo, b.anastokgrubu, b.stoktipi, b.cinsaciklamasi   " +
                        " Union All "
                Case 3
                    cSQL = cSQL +
                        " and b.aksesuartakibi = 'E' " +
                        " and a.topno is not null " +
                        " and a.topno <> '' " +
                        " group by a.topno, a.stokno, a.renk, a.beden, a.hedefmalzemetakipno, a.hedefpartino, a.hedefdepo, b.anastokgrubu, b.stoktipi, b.cinsaciklamasi   " +
                        " Union All "
                Case 4
                    cSQL = cSQL +
                        " and (b.toptakibi = 'E' or b.aksesuartakibi = 'E') " +
                        " and a.topno is not null " +
                        " and a.topno <> '' " +
                        " group by a.topno, a.stokno, a.renk, a.beden, a.hedefmalzemetakipno, a.hedefpartino, a.hedefdepo, b.anastokgrubu, b.stoktipi, b.cinsaciklamasi   " +
                        " Union All "
            End Select

            Select Case nCase
                Case 1
                    cTranMTFFilter = " malzemetakipkodu = coalesce(a.kaynakmalzemetakipno ,''), "
                Case 2
                    cTranMTFFilter = " malzemetakipkodu = '', "
                Case 3
                    cTranMTFFilter = " malzemetakipkodu = '', "
            End Select

            cSQL = cSQL +
                    " select stokno = coalesce(a.stokno,''), " +
                    IIf(nCase = 1, "", " topno = coalesce(a.topno,''), ").ToString +
                    " renk = coalesce(a.renk ,''), " +
                    " beden = coalesce(a.beden,''), " +
                    cTranMTFFilter +
                    " partino = coalesce(a.kaynakpartino,''), " +
                    " depo = coalesce(a.kaynakdepo,''), " +
                    " b.anastokgrubu, " +
                    " b.stoktipi, " +
                    " b.cinsaciklamasi, " +
                    " giris = 0, " +
                    " cikis = sum(coalesce(a.netmiktar1,0)) " +
                    " from StokTransfer a WITH (NOLOCK), stok b WITH (NOLOCK) " +
                    " where a.stokno = b.stokno " +
                    " and (b.kapandi is null or b.kapandi = 'H') " +
                    cFilterTransfer2 + cTransferTarihFilter

            Select Case nCase
                Case 1
                    cSQL = cSQL +
                        " group by a.stokno, a.renk, a.beden, a.kaynakmalzemetakipno, a.kaynakpartino, a.kaynakdepo, b.anastokgrubu, b.stoktipi, b.cinsaciklamasi  "
                Case 2
                    cSQL = cSQL +
                        " and b.toptakibi = 'E' " +
                        " and a.topno is not null " +
                        " and a.topno <> '' " +
                        " group by a.topno, a.stokno, a.renk, a.beden, a.kaynakmalzemetakipno, a.kaynakpartino, a.kaynakdepo, b.anastokgrubu, b.stoktipi, b.cinsaciklamasi   "
                Case 3
                    cSQL = cSQL +
                        " and b.aksesuartakibi = 'E' " +
                        " and a.topno is not null " +
                        " and a.topno <> '' " +
                        " group by a.topno, a.stokno, a.renk, a.beden, a.kaynakmalzemetakipno, a.kaynakpartino, a.kaynakdepo, b.anastokgrubu, b.stoktipi, b.cinsaciklamasi   "
                Case 4
                    cSQL = cSQL +
                        " and (b.toptakibi = 'E' or b.aksesuartakibi = 'E') " +
                        " and a.topno is not null " +
                        " and a.topno <> '' " +
                        " group by a.topno, a.stokno, a.renk, a.beden, a.kaynakmalzemetakipno, a.kaynakpartino, a.kaynakdepo, b.anastokgrubu, b.stoktipi, b.cinsaciklamasi  "
            End Select

            GetViewStokDurumu = oSQL.CreateTempView(cSQL)

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex, "GetViewStokDurumu", cSQL)
        End Try
    End Function

    Private Sub HizliStokRBBakimi(cDatabase As String)

        Dim cView As String = ""
        Dim oSQL As New SQLServerClass(True,, cDatabase)

        Try
            cView = GetViewStokDurumu(1,,,,, cDatabase)

            oSQL.OpenConn()

            oSQL.cSQLQuery = "delete stokrb"
            oSQL.SQLExecute()

            oSQL.cSQLQuery = "insert stokrb (StokNo, renk, beden, depo, partino, " +
                            " malzemetakipkodu, donemgiris1, donemcikis1, devirgiris1, devircikis1, " +
                            " alismiktari1, alistutari1) " +
                            " select stokno, renk, beden, depo, partino, malzemetakipkodu, " +
                            " donemgiris1 = sum(coalesce(giris,0)), " +
                            " donemcikis1 = sum(coalesce(cikis,0)), " +
                            " devirgiris1 = 0, " +
                            " devircikis1 = 0, " +
                            " alismiktari1 = 0, " +
                            " alistutari1 = 0 " +
                            " from " + cView +
                            " group by stokno, renk, beden, depo, partino, malzemetakipkodu "

            oSQL.SQLExecute()

            oSQL.DropView(cView)

            oSQL.cSQLQuery = "update stok set " +
                            " donemgiris1 = coalesce((select sum(coalesce(donemgiris1,0)) from stokrb WITH (NOLOCK) where stokno = stok.stokno),0), " +
                            " donemcikis1 = coalesce((select sum(coalesce(donemcikis1,0)) from stokrb WITH (NOLOCK) where stokno = stok.stokno),0), " +
                            " devirgiris1 = 0, " +
                            " devircikis1 = 0 "

            oSQL.SQLExecute()

            ' fiyat son girişten okunsun
            oSQL.cSQLQuery = "update stokrb " +
                                " set songiristarihi = (select top 1 a.fistarihi " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " And stokrb.stokno = b.stokno " +
                                            " And stokrb.renk = b.renk " +
                                            " And stokrb.beden = b.beden " +
                                            " And stokrb.malzemetakipkodu = b.malzemetakipkodu " +
                                            " And stokrb.depo = b.depo " +
                                            " And stokrb.partino = b.partino " +
                                            " And a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc), "
            oSQL.cSQLQuery += " songirisfiyati = (select top 1 b.maliyetbirimfiyati " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and stokrb.stokno = b.stokno " +
                                            " and stokrb.renk = b.renk " +
                                            " and stokrb.beden = b.beden " +
                                            " and stokrb.malzemetakipkodu = b.malzemetakipkodu " +
                                            " and stokrb.depo = b.depo " +
                                            " and stokrb.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc), "
            oSQL.cSQLQuery += " songirisdovizi = (select top 1 b.maliyetdovizi " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and stokrb.stokno = b.stokno " +
                                            " and stokrb.renk = b.renk " +
                                            " and stokrb.beden = b.beden " +
                                            " and stokrb.malzemetakipkodu = b.malzemetakipkodu " +
                                            " and stokrb.depo = b.depo " +
                                            " and stokrb.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc), "
            oSQL.cSQLQuery += " songirisdept = (select top 1 a.departman " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and stokrb.stokno = b.stokno " +
                                            " and stokrb.renk = b.renk " +
                                            " and stokrb.beden = b.beden " +
                                            " and stokrb.malzemetakipkodu = b.malzemetakipkodu " +
                                            " and stokrb.depo = b.depo " +
                                            " and stokrb.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc), "
            oSQL.cSQLQuery += " songirisfirmasi = (select top 1 a.firma " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and stokrb.stokno = b.stokno " +
                                            " and stokrb.renk = b.renk " +
                                            " and stokrb.beden = b.beden " +
                                            " and stokrb.malzemetakipkodu = b.malzemetakipkodu " +
                                            " and stokrb.depo = b.depo " +
                                            " and stokrb.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc) "

            oSQL.SQLExecute()

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex, "HizliStokRBBakimi")
        End Try
    End Sub

    Private Sub HizliStokTopRBBakimi(cDatabase As String)

        Dim cView As String = ""
        Dim cTableName As String = ""
        Dim oSQL As New SQLServerClass(True,, cDatabase)

        Try
            oSQL.OpenConn()

            cView = GetViewStokDurumu(2,,,,, cDatabase)

            oSQL.cSQLQuery = "(topno char(30) null, " +
                            " stokno char(30) null, " +
                            " renk char(30) null, " +
                            " beden char(30) null, " +
                            " depo char(30) null, " +
                            " partino char(30) null, " +
                            " malzemetakipkodu char(30) null, " +
                            " donemgiris1 decimal(18,3) null, " +
                            " donemcikis1 decimal(18,3) null)"

            cTableName = oSQL.CreateTempTable()

            ' hız ve hafıza için stok durumunu tablolaştır
            oSQL.cSQLQuery = "insert " + cTableName + " (topno, stokno, renk, beden, depo, partino, malzemetakipkodu, donemgiris1, donemcikis1) " +
                            " select topno, stokno, renk, beden, depo, partino, " +
                            " malzemetakipkodu = '', " +
                            " donemgiris1 = sum(coalesce(giris,0)), " +
                            " donemcikis1 = sum(coalesce(cikis,0)) " +
                            " from " + cView +
                            " group by topno, stokno, renk, beden, depo, partino "

            oSQL.SQLExecute()

            ' delete all
            oSQL.cSQLQuery = "delete  stoktoprb"

            oSQL.SQLExecute()

            ' rebuild all
            oSQL.cSQLQuery = "insert stoktoprb (topno, stokno, renk, beden, depo, partino, malzemetakipkodu, donemgiris1, donemcikis1) " +
                            " select topno, stokno, renk, beden, depo, partino, malzemetakipkodu, donemgiris1, donemcikis1 " +
                            " from " + cTableName

            oSQL.SQLExecute()

            oSQL.cSQLQuery = "update stoktoprb " +
                                " set songiristarihi = (select top 1 a.fistarihi " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and stoktoprb.topno = b.topno " +
                                            " and stoktoprb.stokno = b.stokno " +
                                            " and stoktoprb.renk = b.renk " +
                                            " and stoktoprb.beden = b.beden " +
                                            " and stoktoprb.depo = b.depo " +
                                            " and stoktoprb.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc), "
            oSQL.cSQLQuery += " songirisfiyati = (select top 1 b.maliyetbirimfiyati " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and stoktoprb.topno = b.topno " +
                                            " and stoktoprb.stokno = b.stokno " +
                                            " and stoktoprb.renk = b.renk " +
                                            " and stoktoprb.beden = b.beden " +
                                            " and stoktoprb.depo = b.depo " +
                                            " and stoktoprb.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc), "
            oSQL.cSQLQuery += " songirisdovizi = (select top 1 b.maliyetdovizi " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and stoktoprb.topno = b.topno " +
                                            " and stoktoprb.stokno = b.stokno " +
                                            " and stoktoprb.renk = b.renk " +
                                            " and stoktoprb.beden = b.beden " +
                                            " and stoktoprb.depo = b.depo " +
                                            " and stoktoprb.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc), "
            oSQL.cSQLQuery += " songirisdept = (select top 1 a.departman " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and stoktoprb.topno = b.topno " +
                                            " and stoktoprb.stokno = b.stokno " +
                                            " and stoktoprb.renk = b.renk " +
                                            " and stoktoprb.beden = b.beden " +
                                            " and stoktoprb.depo = b.depo " +
                                            " and stoktoprb.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc), "
            oSQL.cSQLQuery += " songirisfirmasi = (select top 1 a.firma " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and stoktoprb.topno = b.topno " +
                                            " and stoktoprb.stokno = b.stokno " +
                                            " and stoktoprb.renk = b.renk " +
                                            " and stoktoprb.beden = b.beden " +
                                            " and stoktoprb.depo = b.depo " +
                                            " and stoktoprb.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc) "

            oSQL.SQLExecute()

            oSQL.cSQLQuery = "delete stoktoprb " +
                            " where coalesce(donemgiris1, 0) - coalesce(donemcikis1, 0) = 0 " +
                            " and not exists (select * from stokfislines WITH (NOLOCK) where topno = stoktoprb.topno) " +
                            " and not exists (select * from stoktransfer WITH (NOLOCK) where topno = stoktoprb.topno) "

            oSQL.SQLExecute()

            oSQL.DropView(cView)
            oSQL.DropTable(cTableName)

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex, "HizliStokTopRBBakimi")
        End Try
    End Sub

    Private Sub HizliStokAksesuarRBBakimi(cDatabase As String)

        Dim cView As String = ""
        Dim cTableName As String = ""
        Dim oSQL As New SQLServerClass(True,, cDatabase)

        Try
            oSQL.OpenConn()

            cView = GetViewStokDurumu(3,,,,, cDatabase)

            oSQL.cSQLQuery = "(topno char(30) null, " +
                            " stokno char(30) null, " +
                            " renk char(30) null, " +
                            " beden char(30) null, " +
                            " depo char(30) null, " +
                            " partino char(30) null, " +
                            " malzemetakipkodu char(30) null, " +
                            " donemgiris1 decimal(18,3) null, " +
                            " donemcikis1 decimal(18,3) null)"

            cTableName = oSQL.CreateTempTable()

            ' hız ve hafıza için stok durumunu tablolaştır

            oSQL.cSQLQuery = "insert " + cTableName + " (topno, stokno, renk, beden, depo, partino, malzemetakipkodu, donemgiris1, donemcikis1) " +
                            " select topno, stokno, renk, beden, depo, partino, " +
                            " malzemetakipkodu = '', " +
                            " donemgiris1 = sum(coalesce(giris,0)), " +
                            " donemcikis1 = sum(coalesce(cikis,0)) " +
                            " from " + cView +
                            " group by topno, stokno, renk, beden, depo, partino "

            oSQL.SQLExecute()

            ' delete all
            oSQL.cSQLQuery = "delete stokaksesuarrb "

            oSQL.SQLExecute()

            ' rebuild all
            oSQL.cSQLQuery = "insert stokaksesuarrb (topno, stokno, renk, beden, depo, partino, malzemetakipkodu, donemgiris1, donemcikis1) " +
                            " select topno, stokno, renk, beden, depo, partino, malzemetakipkodu, donemgiris1, donemcikis1 " +
                            " from " + cTableName

            oSQL.SQLExecute()

            oSQL.cSQLQuery = "update StokAksesuarRB " +
                                " set songiristarihi = (select top 1 a.fistarihi " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and StokAksesuarRB.topno = b.topno " +
                                            " and StokAksesuarRB.stokno = b.stokno " +
                                            " and StokAksesuarRB.renk = b.renk " +
                                            " and StokAksesuarRB.beden = b.beden " +
                                            " and StokAksesuarRB.depo = b.depo " +
                                            " and StokAksesuarRB.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc), "
            oSQL.cSQLQuery += " songirisfiyati = (select top 1 b.maliyetbirimfiyati " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and StokAksesuarRB.topno = b.topno " +
                                            " and StokAksesuarRB.stokno = b.stokno " +
                                            " and StokAksesuarRB.renk = b.renk " +
                                            " and StokAksesuarRB.beden = b.beden " +
                                            " and StokAksesuarRB.depo = b.depo " +
                                            " and StokAksesuarRB.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc), "
            oSQL.cSQLQuery += " songirisdovizi = (select top 1 b.maliyetdovizi " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and StokAksesuarRB.topno = b.topno " +
                                            " and StokAksesuarRB.stokno = b.stokno " +
                                            " and StokAksesuarRB.renk = b.renk " +
                                            " and StokAksesuarRB.beden = b.beden " +
                                            " and StokAksesuarRB.depo = b.depo " +
                                            " and StokAksesuarRB.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc), "
            oSQL.cSQLQuery += " songirisdept = (select top 1 a.departman " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and StokAksesuarRB.topno = b.topno " +
                                            " and StokAksesuarRB.stokno = b.stokno " +
                                            " and StokAksesuarRB.renk = b.renk " +
                                            " and StokAksesuarRB.beden = b.beden " +
                                            " and StokAksesuarRB.depo = b.depo " +
                                            " and StokAksesuarRB.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc), "
            oSQL.cSQLQuery += " songirisfirmasi = (select top 1 a.firma " +
                                            " from stokfis a WITH (NOLOCK), stokfislines b WITH (NOLOCK) " +
                                            " where a.stokfisno = b.stokfisno " +
                                            " and StokAksesuarRB.topno = b.topno " +
                                            " and StokAksesuarRB.stokno = b.stokno " +
                                            " and StokAksesuarRB.renk = b.renk " +
                                            " and StokAksesuarRB.beden = b.beden " +
                                            " and StokAksesuarRB.depo = b.depo " +
                                            " and StokAksesuarRB.partino = b.partino " +
                                            " and a.stokfistipi = 'Giris' " +
                                            " and b.maliyetbirimfiyati is not null " +
                                            " and b.maliyetbirimfiyati <> 0 " +
                                            " order by a.fistarihi desc) "
            oSQL.SQLExecute()

            oSQL.cSQLQuery = "delete stokaksesuarrb " +
                            " where coalesce(donemgiris1, 0) - coalesce(donemcikis1, 0) = 0 " +
                            " and not exists (select * from stokfislines WITH (NOLOCK) where topno = stokaksesuarrb.topno) " +
                            " and not exists (select * from stoktransfer WITH (NOLOCK) where topno = stokaksesuarrb.topno) "

            oSQL.SQLExecute()

            oSQL.DropView(cView)
            oSQL.DropTable(cTableName)

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp(ex, "HizliStokAksesuarRBBakimi")
        End Try
    End Sub

    Public Function CheckFirmaCalisilmasin(cFirma As String, cDatabase As String) As Boolean

        CheckFirmaCalisilmasin = False

        Try
            Dim oSQL As New SQLServerClass(True,, cDatabase)

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 firma " +
                            " from firma with (NOLOCK) " +
                            " where firma = '" + cFirma.Trim + "' " +
                            " and calisilmasin = 'E' "

            CheckFirmaCalisilmasin = oSQL.CheckExists()

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp(ex, "CheckFirmaCalisilmasin")
        End Try
    End Function

    Public Function ActivityLog(ByVal cDatabase As String, ByVal modulename As String, ByVal action As String, Optional ByVal explanation As String = "",
                                Optional ByVal cNotes As String = "") As Boolean

        ActivityLog = False

        Try
            Dim oSQL As New SQLServerClass(True,, cDatabase)

            cNotes = cVersion + "/" + Trim$(cNotes)

            oSQL.OpenConn()

            oSQL.cSQLQuery = "set dateformat dmy " +
                        " insert into glog  (Username, prgname, actdate, acttime, prgmodule, " +
                        " action, explanation, makinaadi, notes) "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                        "  VALUES ('Service', " +
                        " 'WINTEX', " +
                        " GETDATE(), " +
                        " CONVERT(CHAR(8),CONVERT(TIME,GETDATE())), " +
                        " '" + SQLWriteString(modulename, 30) + "', "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                        " '" + SQLWriteString(action, 30) + "', " +
                        " '" + SQLWriteString(explanation, 250) + "', " +
                        " 'Server', " +
                        " '" + SQLWriteString(cNotes) + "' ) "

            oSQL.SQLExecute()

            oSQL.CloseConn()

            ActivityLog = True

        Catch ex As Exception
            ErrDisp(ex, "ActivityLog")
        End Try
    End Function

End Module
