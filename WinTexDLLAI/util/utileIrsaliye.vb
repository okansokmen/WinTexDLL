Option Explicit On

Imports System.Xml
Imports System.Configuration
Imports System.Net
Imports System
Imports System.IO
Imports System.Globalization

Module utileIrsaliye

    Public Function CheckValidEIrsaliye(ByVal nCase As Integer, ByVal cFisNo As String) As Boolean
        ' nCase = 1 Stok Fişi
        ' nCase = 2 Üretim Fişi

        CheckValidEIrsaliye = False

        Try
            Dim oSQL As New SQLServerClass
            Dim nLineCount As Integer = 0
            Dim lTest As Boolean = False
            Dim cF1VN As String = ""
            Dim cSQL_FisSatirSayisi As String = ""
            Dim cSQL_Fis As String = ""
            Dim cMessage As String = ""

            Select Case nCase
                Case 1
                    ' stok fişi
                    cSQL_FisSatirSayisi = GetSQLQueryeIrsaliye(3, cFisNo)
                    cSQL_Fis = GetSQLQueryeIrsaliye(1, cFisNo)
                Case 2
                    ' üretim fişi 
                    cSQL_FisSatirSayisi = GetSQLQueryeIrsaliye(4, cFisNo)
                    cSQL_Fis = GetSQLQueryeIrsaliye(2, cFisNo)
            End Select

            oSQL.OpenConn()

            Select Case oSQL.GetSysPar("irsservicesaglayici")
                Case "crs"
                    lTest = (oSQL.GetSysPar("UrlCrsEirsaliyeService") = "https://connect-test.crssoft.com/Services/DespatchIntegration")
                Case "turkkep"
                    lTest = (oSQL.GetSysPar("TurkKepService") = "http://efinttestws.turkkep.com.tr/EFaturaEntegrasyon2.asmx")
                Case "edoksis"
                    lTest = (oSQL.GetSysPar("eDokSisService") = "https://efaturatest.edoksis.net/IrsaliyeWebService.asmx")
                Case "park"
                    lTest = (oSQL.GetSysPar("ParkService") = "https://wstest.parkentegrasyon.com.tr/EFaturaIntegration.asmx")
            End Select

            If nCase = 1 Then
                ' stok fişinde gönderen firma dahili
                oSQL.cSQLQuery = "select top 1 firma " +
                                " from firma with (NOLOCK) " +
                                " where firma like 'DAH_L%' " +
                                " and dahilifirma = 'E' "

                If Not oSQL.CheckExists() Then
                    cMessage = cMessage + "DAHILI firma işaretlenmemiş" + vbCrLf
                End If
            End If

            oSQL.cSQLQuery = cSQL_FisSatirSayisi

            nLineCount = oSQL.DBReadInteger()

            If nLineCount = 0 Then
                cMessage = cMessage + "Gönderilebilecek satır yok" + vbCrLf
            End If

            oSQL.cSQLQuery = cSQL_Fis

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                'If lTest Then
                '    cF1VN = "2150240232"
                'Else
                '    cF1VN = oSQL.SQLReadString("f1vn")
                'End If

                ' satıcı kontrolleri

                If oSQL.SQLReadString("f1vn") = "" Then
                    cMessage = cMessage + "Malları satan firmanın (" + oSQL.SQLReadString("f1adi") + ") vergi numarası yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f1adi") = "" Then
                    cMessage = cMessage + "Malları satan firmanın adı yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f1smt") = "" Then
                    cMessage = cMessage + "Malları satan firmanın (" + oSQL.SQLReadString("f1adi") + ") semt bilgisi yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f1shr") = "" Then
                    cMessage = cMessage + "Malları satan firmanın (" + oSQL.SQLReadString("f1adi") + ") şehir bilgisi yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f1ulk") = "" Then
                    cMessage = cMessage + "Malları satan firmanın (" + oSQL.SQLReadString("f1adi") + ") ülke bilgisi yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f1pk") = "" Then
                    cMessage = cMessage + "Malları satan firmanın (" + oSQL.SQLReadString("f1adi") + ") posta kodu bilgisi yok" + vbCrLf
                End If

                ' alıcı kontrolleri

                If oSQL.SQLReadString("f2vn").Length = 11 Then
                    ' TCKN
                    If oSQL.SQLReadString("f2sahis") = "" Then
                        cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2sahis") + ") yetkili adı yok" + vbCrLf
                    End If
                    If oSQL.SQLReadString("f2soyad") = "" Then
                        cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2soyad") + ") yetkili soyadı yok" + vbCrLf
                    End If
                End If

                If oSQL.SQLReadString("f2vn") = "" Then
                    cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2adi") + ") vergi numarası yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f2adi") = "" Then
                    cMessage = cMessage + "Malları teslim alan firmanın adı yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f2smt") = "" Then
                    cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2adi") + ") semt bilgisi yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f2shr") = "" Then
                    cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2adi") + ") şehir bilgisi yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f2ulk") = "" Then
                    cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2adi") + ") ülke bilgisi yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f2pk") = "" Then
                    cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2adi") + ") posta kodu bilgisi yok" + vbCrLf
                End If

                ' nakliyeci

                If oSQL.SQLReadString("sofortckn") = "" Then
                    If oSQL.SQLReadString("f3vn") = "" Then
                        cMessage = cMessage + "Nakliyeci firmanın (" + oSQL.SQLReadString("f3adi") + ") vergi numarası yok" + vbCrLf
                    End If

                    If oSQL.SQLReadString("f3adi") = "" Then
                        cMessage = cMessage + "Nakliyeci firmanın adı yok" + vbCrLf
                    End If
                Else
                    If oSQL.SQLReadString("soforadi") = "" Then
                        cMessage = cMessage + "Şoför adı bilgisi yok" + vbCrLf
                    End If

                    If oSQL.SQLReadString("soforsoyadi") = "" Then
                        cMessage = cMessage + "Şoför soyadı bilgisi yok" + vbCrLf
                    End If

                    If oSQL.SQLReadString("aracplaka") = "" Then
                        cMessage = cMessage + "Araç plaka bilgisi yok" + vbCrLf
                    End If
                End If

            End If
            oSQL.oReader.Close()

            oSQL.CloseConn()

            If cMessage.Trim <> "" Then
                cMessage = "Dikkat : " + cFisNo + " fişi eIrsaliye olarak gönderilemez." + cMessage
                MessageBox.Show(cMessage.Trim)
                Exit Function
            End If

            CheckValidEIrsaliye = True

        Catch ex As Exception
            ErrDisp("CheckValidEIrsaliye", "eIrsaliye",,, ex)
        End Try
    End Function

    Public Function GetSQLQueryeIrsaliye(nCase As Integer, cFilter As String) As String

        Dim cSQL As String = ""

        GetSQLQueryeIrsaliye = ""

        Try
            Select Case nCase
                Case 1
                    ' stok fişi kafası
                    cSQL = "SELECT top 1 a.stokfisno, a.fistarihi, a.belgeno, a.belgetarihi, a.sevkno, " +
                            " a.faturano, a.faturatarihi, a.departman, a.firma, a.stokfistipi, a.notlar, " +
                            " a.tasiyicifirma, a.aracplaka, a.dorseplaka, a.teslimeden, a.soforadi, a.soforsoyadi, a.sofortckn, "

                    ' f1xxx cikisfirm_atl / malı GÖNDEREN firma / DespatchSupplierParty
                    cSQL = cSQL +
                            " f1adi = d.aciklama, " +
                            " f1vd = d.vergidairesi, " +
                            " f1vn = d.vergino, " +
                            " f1adr = rtrim(rtrim(coalesce(d.adres1,'')) + ' ' + rtrim(coalesce(d.adres2,''))), " +
                            " f1smt = d.semt, " +
                            " f1shr = case when d.sehir is null or d.sehir = '' then 'İstanbul' else d.sehir end, " +
                            " f1ulk = case when d.ulke  is null or d.ulke  = '' then 'Türkiye'  else d.ulke end, " +
                            " f1email = d.emailadresi, " +
                            " f1tel1 = d.tel1, " +
                            " f1fax = d.fax, " +
                            " f1pk = case when d.postakodu is null or d.postakodu  = '' then '34000' else d.postakodu end,  " +
                            " f1sahis = d.yetkili1, " +
                            " f1soyad = d.yetkili2, " +
                            " f1tc = d.yetkilitc, "

                    ' f2xxx girisfirm_atl / malı TESLIM ALAN firma / DeliveryCustomerParty
                    cSQL = cSQL +
                            " f2adi = b.aciklama, " +
                            " f2vd = b.vergidairesi, " +
                            " f2vn = b.vergino, " +
                            " f2adr = rtrim(rtrim(coalesce(b.adres1,'')) + ' ' + rtrim(coalesce(b.adres2,''))), " +
                            " f2smt = b.semt, " +
                            " f2shr = case when b.sehir is null or b.sehir = '' then 'İstanbul' else b.sehir end, " +
                            " f2ulk = case when b.ulke  is null or b.ulke  = '' then 'Türkiye'  else b.ulke end, " +
                            " f2email = b.emailadresi, " +
                            " f2tel1 = b.tel1, " +
                            " f2fax = b.fax, " +
                            " f2pk = case when b.postakodu is null or b.postakodu  = '' then '34000' else b.postakodu end,  " +
                            " f2sahis = b.yetkili1, " +
                            " f2soyad = b.yetkili2, " +
                            " f2tc = b.yetkilitc, "

                    ' f3xxx tasiyicifirma / TAŞIYICI firma
                    cSQL = cSQL +
                            " f3adi = c.aciklama, " +
                            " f3vd = c.vergidairesi, " +
                            " f3vn = c.vergino, " +
                            " f3adr = rtrim(rtrim(coalesce(c.adres1,'')) + ' ' + rtrim(coalesce(c.adres2,''))), " +
                            " f3smt = c.semt, " +
                            " f3shr = case when c.sehir is null or c.sehir = '' then 'İstanbul' else c.sehir end, " +
                            " f3ulk = case when c.ulke  is null or c.ulke  = '' then 'Türkiye'  else c.ulke end, " +
                            " f3email = c.emailadresi, " +
                            " f3tel1 = c.tel1, " +
                            " f3fax = c.fax, " +
                            " f3pk = case when c.postakodu is null or c.postakodu  = '' then '34000' else c.postakodu end, " +
                            " f3sahis = c.yetkili1, " +
                            " f3soyad = c.yetkili2, " +
                            " f3tc = c.yetkilitc "

                    cSQL = cSQL +
                            " from stokfis a with (NOLOCK) " +
                            " left outer join firma b with (NOLOCK) on b.firma = a.firma " +
                            " left outer join firma c with (NOLOCK) on c.firma = a.tasiyicifirma " +
                            " left outer join firma d with (NOLOCK) on d.firma like 'DAH_L%' and d.dahilifirma = 'E' " +
                            " where a.stokfisno = '" + cFilter.Trim + "' "

                Case 2
                    ' üretim fişi kafası
                    cSQL = "select top 1 a.uretfisno, a.fistarihi, a.belgeno, a.belgetarihi, a.faturano, a.faturatarihi, " +
                            " a.cikisdept, a.cikisfirm_atl, a.girisdept, a.girisfirm_atl, a.notlar, " +
                            " a.tasiyicifirma, a.aracplaka, a.dorseplaka, a.teslimpersonel, a.soforadi, a.soforsoyadi, a.sofortckn, "

                    ' f1xxx cikisfirm_atl / malı GÖNDEREN firma / DespatchSupplierParty
                    cSQL = cSQL +
                            " f1adi = d.aciklama, " +
                            " f1vd = d.vergidairesi, " +
                            " f1vn = d.vergino, " +
                            " f1adr = rtrim(rtrim(coalesce(d.adres1,'')) + ' ' + rtrim(coalesce(d.adres2,''))), " +
                            " f1smt = d.semt, " +
                            " f1shr = case when d.sehir is null or d.sehir = '' then 'İstanbul' else d.sehir end, " +
                            " f1ulk = case when d.ulke  is null or d.ulke  = '' then 'Türkiye'  else d.ulke end, " +
                            " f1email = d.emailadresi, " +
                            " f1tel1 = d.tel1, " +
                            " f1fax = d.fax, " +
                            " f1pk = case when d.postakodu is null or d.postakodu  = '' then '34000' else d.postakodu end,  " +
                            " f1sahis = d.yetkili1, " +
                            " f1soyad = d.yetkili2, " +
                            " f1tc = d.yetkilitc, "

                    ' f2xxx girisfirm_atl / malı TESLIM ALAN firma / DeliveryCustomerParty
                    cSQL = cSQL +
                            " f2adi = e.aciklama, " +
                            " f2vd = e.vergidairesi, " +
                            " f2vn = e.vergino, " +
                            " f2adr = rtrim(rtrim(coalesce(e.adres1,'')) + ' ' + rtrim(coalesce(e.adres2,''))), " +
                            " f2smt = e.semt, " +
                            " f2shr = case when e.sehir is null or e.sehir = '' then 'İstanbul' else e.sehir end, " +
                            " f2ulk = case when e.ulke  is null or e.ulke  = '' then 'Türkiye'  else e.ulke end, " +
                            " f2email = e.emailadresi, " +
                            " f2tel1 = e.tel1, " +
                            " f2fax = e.fax, " +
                            " f2pk = case when e.postakodu is null or e.postakodu  = '' then '34000' else e.postakodu end,  " +
                            " f2sahis = e.yetkili1, " +
                            " f2soyad = e.yetkili2, " +
                            " f2tc = e.yetkilitc, "

                    ' f3xxx tasiyicifirma / TAŞIYICI firma
                    cSQL = cSQL +
                            " f3adi = f.aciklama, " +
                            " f3vd = f.vergidairesi, " +
                            " f3vn = f.vergino, " +
                            " f3adr = rtrim(rtrim(coalesce(f.adres1,'')) + ' ' + rtrim(coalesce(f.adres2,''))), " +
                            " f3smt = f.semt, " +
                            " f3shr = case when f.sehir is null or f.sehir = '' then 'İstanbul' else f.sehir end, " +
                            " f3ulk = case when f.ulke  is null or f.ulke  = '' then 'Türkiye'  else f.ulke end, " +
                            " f3email = f.emailadresi, " +
                            " f3tel1 = f.tel1, " +
                            " f3fax = f.fax, " +
                            " f3pk = case when f.postakodu is null or f.postakodu  = '' then '34000' else f.postakodu end, " +
                            " f3sahis = f.yetkili1, " +
                            " f3soyad = f.yetkili2, " +
                            " f3tc = f.yetkilitc "

                    cSQL = cSQL +
                            " from uretharfis a with (NOLOCK) " +
                            " left outer join uretharfislines b With (NOLOCK) On b.uretfisno = a.uretfisno " +
                            " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                            " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                            " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                            " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                            " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                            " where a.uretfisno = '" + cFilter.Trim + "' "

                ' detaylı bütün satırlar
                Case 3
                    ' stok fişi satır adedi
                    cSQL = "select count (distinct a.stokhareketno) " +
                            " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                            " where a.stokfisno = '" + cFilter.Trim + "' " +
                            " and a.stokno = b.stokno " +
                            " and a.stokno is not null " +
                            " and a.stokno <> '' " +
                            " and a.netmiktar1 is not null " +
                            " and a.netmiktar1 > 0 "
                Case 4
                    ' üretim fişi satır adedi
                    cSQL = "select count (distinct b.modelno + c.renk + c.beden) " +
                            " from uretharfis a with (NOLOCK) " +
                            " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                            " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                            " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                            " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                            " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                            " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                            " where a.uretfisno = '" + cFilter.Trim + "' "
                Case 5
                    ' stok fişi satırları
                    cSQL = "select a.stokhareketno, b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.renk, a.beden, a.netmiktar1, a.birim1, a.malzemetakipkodu " +
                            " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                            " where a.stokfisno = '" + cFilter.Trim + "' " +
                            " and a.stokno = b.stokno " +
                            " and a.stokno is not null " +
                            " and a.stokno <> '' " +
                            " and a.netmiktar1 is not null " +
                            " and a.netmiktar1 > 0 " +
                            " order by b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.renk, a.beden "
                Case 6
                    ' üretim fişi satırları
                    cSQL = "select g.anamodeltipi, b.modelno, c.renk, c.beden, g.aciklama, " +
                            " adet = sum(coalesce(c.adet,0)) " +
                            " from uretharfis a with (NOLOCK) " +
                            " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                            " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                            " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                            " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                            " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                            " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                            " where a.uretfisno = '" + cFilter.Trim + "' " +
                            " group by g.anamodeltipi, b.modelno, c.renk, c.beden, g.aciklama " +
                            " order by g.anamodeltipi, b.modelno, c.renk, c.beden "

                ' özet satırlar 
                Case 7
                    ' stok fişi özet satır adedi
                    cSQL = "select count (distinct b.anastokgrubu + b.stoktipi + b.birim1) " +
                            " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                            " where a.stokfisno = '" + cFilter.Trim + "' " +
                            " and a.stokno = b.stokno " +
                            " and a.stokno is not null " +
                            " and a.stokno <> '' " +
                            " and a.netmiktar1 is not null " +
                            " and a.netmiktar1 > 0 "
                Case 8
                    ' üretim fişi özet satır adedi
                    cSQL = "select count (distinct g.anamodeltipi + b.modelno) " +
                            " from uretharfis a with (NOLOCK) " +
                            " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                            " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                            " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                            " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                            " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                            " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                            " where a.uretfisno = '" + cFilter.Trim + "' "
                Case 9
                    ' stok fişi özet satırları
                    cSQL = "select b.anastokgrubu, b.stoktipi, b.birim1, " +
                            " netmiktar1 = sum(coalesce(a.netmiktar1,0)) " +
                            " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                            " where a.stokfisno = '" + cFilter.Trim + "' " +
                            " and a.stokno = b.stokno " +
                            " and a.stokno is not null " +
                            " and a.stokno <> '' " +
                            " and a.netmiktar1 is not null " +
                            " and a.netmiktar1 > 0 " +
                            " group by b.anastokgrubu, b.stoktipi, b.birim1 " +
                            " order by b.anastokgrubu, b.stoktipi, b.birim1 "
                Case 10
                    ' üretim fişi özet satırları
                    cSQL = "select g.anamodeltipi, b.modelno, g.aciklama, " +
                            " adet = sum(coalesce(c.adet,0)) " +
                            " from uretharfis a with (NOLOCK) " +
                            " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                            " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                            " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                            " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                            " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                            " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                            " where a.uretfisno = '" + cFilter.Trim + "' " +
                            " group by g.anamodeltipi, b.modelno, g.aciklama  " +
                            " order by g.anamodeltipi, b.modelno  "
                ' stok kodu gruplu
                Case 11
                    ' stok kodu gruplu satır adedi
                    cSQL = "select count (distinct b.stokno) " +
                            " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                            " where a.stokfisno = '" + cFilter.Trim + "' " +
                            " and a.stokno = b.stokno " +
                            " and a.stokno is not null " +
                            " and a.stokno <> '' " +
                            " and a.netmiktar1 is not null " +
                            " and a.netmiktar1 > 0 "
                Case 12
                    ' stok kodu gruplu satırlar
                    cSQL = "select b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.birim1, " +
                            " netmiktar1 = sum(coalesce(a.netmiktar1,0)) " +
                            " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                            " where a.stokfisno = '" + cFilter.Trim + "' " +
                            " and a.stokno = b.stokno " +
                            " and a.stokno is not null " +
                            " and a.stokno <> '' " +
                            " and a.netmiktar1 is not null " +
                            " and a.netmiktar1 > 0 " +
                            " group by b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.birim1 " +
                            " order by a.stokno "
                Case 13
                    ' sipariş gruplu üretim fişi satır adedi
                    cSQL = "select count (distinct b.uretimtakipno + g.anamodeltipi + g.aciklama) " +
                            " from uretharfis a with (NOLOCK) " +
                            " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                            " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                            " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                            " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                            " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                            " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                            " where a.uretfisno = '" + cFilter.Trim + "' "
                Case 14
                    ' sipariş gruplu üretim fişi satırları
                    cSQL = "select b.uretimtakipno, g.anamodeltipi, g.aciklama, " +
                            " adet = sum(coalesce(c.adet,0)), " +
                            " musteri = (select top 1 x.musterino " +
                                        " from siparis x with (NOLOCK) , sipmodel y with (NOLOCK) " +
                                        " where x.kullanicisipno = y.siparisno " +
                                        " and y.uretimtakipno = b.uretimtakipno), " +
                            " yikama = (select top 1 rtrim(convert(char(30),x.parasalnotlar))  " +
                                        " from siparis x with (NOLOCK) , sipmodel y with (NOLOCK) " +
                                        " where x.kullanicisipno = y.siparisno " +
                                        " and y.uretimtakipno = b.uretimtakipno) " +
                            " from uretharfis a with (NOLOCK) " +
                            " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                            " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                            " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                            " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                            " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                            " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                            " where a.uretfisno = '" + cFilter.Trim + "' " +
                            " group by b.uretimtakipno, g.anamodeltipi, g.aciklama " +
                            " order by b.uretimtakipno, g.anamodeltipi, g.aciklama "

                ' stok kodu + renk gruplu
                Case 15
                    ' stok kodu + renk gruplu satır adedi
                    cSQL = "select count (distinct a.stokno + a.renk) " +
                            " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                            " where a.stokfisno = '" + cFilter.Trim + "' " +
                            " and a.stokno = b.stokno " +
                            " and a.stokno is not null " +
                            " and a.stokno <> '' " +
                            " and a.netmiktar1 is not null " +
                            " and a.netmiktar1 > 0 "
                Case 16
                    ' stok kodu + renk gruplu satırlar
                    cSQL = "select b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.renk, a.birim1, " +
                            " netmiktar1 = sum(coalesce(a.netmiktar1,0)) " +
                            " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                            " where a.stokfisno = '" + cFilter.Trim + "' " +
                            " and a.stokno = b.stokno " +
                            " and a.stokno is not null " +
                            " and a.stokno <> '' " +
                            " and a.netmiktar1 is not null " +
                            " and a.netmiktar1 > 0 " +
                            " group by b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.renk, a.birim1 " +
                            " order by a.stokno, a.renk "
            End Select

            GetSQLQueryeIrsaliye = cSQL

        Catch ex As Exception
            ErrDisp("GetSQLQueryeIrsaliye", "UtileIrsaliye",,, ex)
        End Try
    End Function

    Public Sub eIrsaliyeUpdateWinTex(nCase As Integer, cFisNo As String, cIrsaliyeNumarasi As String, cUUID As String, cTarihSaat As String, Optional cIrsaliyeID As String = "")
        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            Select Case nCase
                Case 1
                    oSQL.cSQLQuery = "set dateformat dmy " +
                                " update stokfis Set " +
                                " belgeno = '" + cIrsaliyeNumarasi.Trim + "', " +
                                " crsuuid = '" + cUUID.Trim + "', " +
                                " crsid = '" + cIrsaliyeID.Trim + "', " +
                                " crstarih = '" + cTarihSaat.Trim + "' " +
                                " where stokfisno = '" + cFisNo.Trim + "' "
                    oSQL.SQLExecute()
                Case 2
                    oSQL.cSQLQuery = "set dateformat dmy " +
                                " update uretharfis Set " +
                                " belgeno = '" + cIrsaliyeNumarasi.Trim + "', " +
                                " crsuuid = '" + cUUID.Trim + "', " +
                                " crsid = '" + cIrsaliyeID.Trim + "', " +
                                " crstarih = '" + cTarihSaat.Trim + "' " +
                                " where uretfisno = '" + cFisNo.Trim + "' "
                    oSQL.SQLExecute()
            End Select

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("eIrsaliyeUpdateWinTex", "UtileIrsaliye",,, ex)
        End Try
    End Sub

    Public Function eIrsaliyeStoreDocument(cFisNo As String, cFisTipi As String, oDocument As Byte()) As String

        eIrsaliyeStoreDocument = ""

        Try
            Dim oSQL As New SQLServerClass
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cFileName As String = ""
            Dim cFileTitle As String = ""
            Dim cPath As String = ""

            oSQL.OpenConn()

            cPath = oSQL.GetSysPar("pathofshare", "c:\wintex")

            If Right$(cPath, 1) <> "\" Then
                cPath = cPath + "\"
            End If
            cPath = cPath + "docs\"

            cFileTitle = "eirsaliye" +
                        IIf(cFisTipi = "", "", "_" + cFisTipi).ToString +
                        IIf(cFisNo = "", "", "_" + cFisNo).ToString +
                        "_" + cTodaysDate

            cFileName = cPath + cFileTitle + ".pdf"

            Dim oCredentials As New NetworkCredential

            oCredentials.Domain = oSQL.GetSysPar("WinTexFileShareDomain")
            oCredentials.UserName = oSQL.GetSysPar("WinTexFileShareUsername")
            oCredentials.Password = oSQL.GetSysPar("WinTexFileSharePassword")

            Dim oNC As New NetworkConnection(oSQL.GetSysPar("WinTexFileSharePath"), oCredentials)

            Using oNC

                If System.IO.File.Exists(cFileName) Then
                    System.IO.File.Delete(cFileName)
                End If

                System.IO.File.WriteAllBytes(cFileName, oDocument)

            End Using

            oSQL.cSQLQuery = "insert documents (docvalue, doctype, rdocname, vdocname, docpath, " +
                                " type, extension, docsubtype, duzletmetarihi, duzeltmesaati, " +
                                " username) "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " values ('" + cFisNo + "', " +
                                " '" + cFisTipi.Trim + "', " +
                                " '" + cFisTipi.Trim + "', " +
                                " '" + SQLWriteString(cFileTitle.Trim, 150) + "', " +
                                " '" + SQLWriteString(cFileName.Trim, 255) + "', "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " 'Adobe PDF File', " +
                                " 'pdf', " +
                                " 'eirsaliye', " +
                                " convert(date,getdate()), " +
                                " convert(char(8),getdate(),108), "

            oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " '" + SQLWriteString(Gl_UserName, 30) + "') "
            oSQL.SQLExecute()
            oSQL.CloseConn()

            eIrsaliyeStoreDocument = cFileName.Trim

        Catch ex As Exception
            ErrDisp("eIrsaliyeStoreDocument", "UtileIrsaliye",,, ex)
        End Try
    End Function

    Public Function eIrsaliyeMTF(cFisNo As String) As String

        eIrsaliyeMTF = ""

        Try
            If cFisNo.Trim = "" Then Exit Function

            Dim cSonuc As String = ""
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select distinct malzemetakipkodu " +
                            " from stokfislines with (NOLOCK) " +
                            " where stokfisno = '" + cFisNo.Trim + "' " +
                            " and malzemetakipkodu is not null " +
                            " and malzemetakipkodu <> '' " +
                            " order by malzemetakipkodu "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                If cSonuc.Trim = "" Then
                    cSonuc = oSQL.SQLReadString("malzemetakipkodu")
                Else
                    cSonuc = cSonuc + "," + oSQL.SQLReadString("malzemetakipkodu")
                End If
            Loop
            oSQL.oReader.Close()
            oSQL.CloseConn()

            eIrsaliyeMTF = cSonuc.Trim

        Catch ex As Exception
            ErrDisp("eIrsaliyeMTF", "UtileIrsaliye",,, ex)
        End Try
    End Function

    Public Function eIrsaliyeUTF(cFisNo As String) As String

        eIrsaliyeUTF = ""

        Try
            If cFisNo.Trim = "" Then Exit Function

            Dim cSonuc As String = ""
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select distinct uretimtakipno " +
                            " from uretharfislines with (NOLOCK) " +
                            " where uretfisno = '" + cFisNo.Trim + "' " +
                            " and uretimtakipno is not null " +
                            " and uretimtakipno <> '' " +
                            " order by uretimtakipno "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                If cSonuc.Trim = "" Then
                    cSonuc = oSQL.SQLReadString("uretimtakipno")
                Else
                    cSonuc = cSonuc + "," + oSQL.SQLReadString("uretimtakipno")
                End If
            Loop
            oSQL.oReader.Close()
            oSQL.CloseConn()

            eIrsaliyeUTF = cSonuc.Trim

        Catch ex As Exception
            ErrDisp("eIrsaliyeUTF", "UtileIrsaliye",,, ex)
        End Try
    End Function

End Module
