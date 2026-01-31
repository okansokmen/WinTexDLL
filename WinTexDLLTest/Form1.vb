Imports System.Xml
Imports Microsoft.Office.Interop.Outlook
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Imports System.Net.Http
Imports System.Text
Imports System.Net.ServicePointManager
Imports Newtonsoft.Json
Imports System.Exception
Imports System.Threading

Public Class Form1

    Private Const LoginUrl As String = "https://ykkapi.ykk.com.tr/Authentication/Login"
    Private Const GetOrderUrl As String = "https://ykkapi.ykk.com.tr/api/Order/GetOrder"
    Private Const OrderUrl As String = "https://ykkapi.ykk.com.tr/api/Order/PostOrder"

    'Private WithEvents clsDropProcess As DragDropProcess

    'Public Sub DropEvent(ByVal sender As Object, ByVal filename As String, ByVal bytes As Byte())
    '    'Add code to do whatever you want with the file.
    '    'My.Computer.FileSystem.WriteAllBytes(String.Format("c:\Temp\{0}", filename), bytes, False)
    '    ListBox1.Items.Add(filename)
    'End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.AllowDrop = True
        'TextBox1.AllowDrop = True

        'ListBox1.AllowDrop = True
        '' Create a new instance of the class and pass the control that you want to allow files to be dropped on
        'clsDropProcess = New DragDropProcess(ListBox1)
        '' Add a handler to get the files/emails that have been dropped
        'AddHandler clsDropProcess.DroppedByteArrayAvailable, AddressOf DropEvent
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        'Dim oy As New WinTexTerazi2.HTMain
        'Dim nSonuc As Double

        'oy.ComTest()
        'Exit Sub

        'nSonuc = oy.TeraziOku()
        'MsgBox(nSonuc)
        'Exit Sub

        'Button1.Text = WinTexDLL.HTMain.TestWinTexDLL.ToString
        Dim oX As New WinTexDLL.HTMain
        'Dim oY As New eIrsaliyeUyum.UIMain
        'Dim cSQL As String = "select a.UretimTakipNo, a.isEmriNo, a.Tarih, a.Departman, a.Firma,  a.Eleman, b.Baslama_Tar, b.Bitis_Tar, b.Fiyati, b.Doviz,  c.ModelNo, c.BedenSeti, c.Parca,  Tutar = sum(coalesce(b.fiyati,0) * coalesce(c.adet,0)),  Adet = sum(coalesce(c.Adet,0))  from uretimisemri a with (NOLOCK), uretimisdetayi b with (NOLOCK), uretimisrba c with (NOLOCK)  where a.isEmriNo = b.isEmriNo  and b.isEmriNo = c.isEmriNo  and b.ulineno = c.ulineno  and a.UretimTakipNo = c.UretimTakipNo  and a.UretimTakipNo is not null  and a.UretimTakipNo <> ''  group by a.UretimTakipNo, a.isEmriNo, a.Tarih, a.Departman, a.Firma,  a.Eleman, b.Baslama_Tar, b.Bitis_Tar, b.Fiyati, b.Doviz,  c.ModelNo, c.BedenSeti, c.Parca "
        ' server   : smtp.gmail.com
        ' username : wintexokansokmen@gmail.com
        ' password : Okansokmenwintex
        ' username : wintexokansokmen@gmail.com
        ' password : Okansokmenwintex
        ' username : wintexprogram@gmail.com
        ' password : Wintexprogram2016
        ' port     : 587
        ' TLS      : true
        ' SERVER = 192.1.0.34
        ' DATABASE = wintex
        ' USER = sa_wintex
        ' PASSWORD = saWinteX21@OXXO

        ' yndex 07-10-2024
        ' wintexprogram2
        ' Wintex2024
        ' khhlvwwbguaoydnw

        'Dim oX As New WinTexSchedule.HTMain

        'If oX.init("monster", "veragroup", "sa", "Hayabusa1024") Then
        '    oX.Takvim()
        'End If
        'Exit Sub

        'If oX.init("192.168.1.150", "wintexvardar", "wintex", "wintex") Then

        'oY.TestForm()

        'If oY.init("monster", "istwtx", "sa", "Hayabusa1024") Then
        '    oY.DllSettings()
        '    oY.SendEIrsaliye(1, "0000227591")
        '    'oY.SendEIrsaliye(2, "0000124835")

        'End If

        'If oX.init("192.168.1.8", "tes", "sa", "er1303*?") Then

        '    oX.DevxBrowse()

        If oX.init("monster", "oxxo", "sa", "Hayabusa1024") Then

            ' volkan.polatman@eroglugiyim.com
            'oX.SendGMail("okansokmen@gmail.com",
            '                 "subject deneme",
            '                 "body deneme",,,
            '                 "wintex@eroglugiyim.com",
            '                 "wintex@eroglugiyim.com",
            '                 "W456s456+", "mail.eroglugiyim.com", 587,, True, True, True)

            'oX.SendGMail("okansokmen@gmail.com",
            '                 "subject deneme",
            '                 "body deneme",,,
            '                 "wintex@vardarsocks.com",
            '                 "wintex",
            '                 "Wintex@0571.++", "smtp.yandex.com", 465, False, True, True, True)

            'oX.SendGMail("okansokmen@gmail.com",
            '                 "subject deneme",
            '                 "body deneme",,,
            '                 "wintexprogram2@yandex.com",
            '                 "wintexprogram2@yandex.com",
            '                 "khhlvwwbguaoydnw", "smtp.yandex.com", 465, False, True, True, True)
            ' server   : smtp.gmail.com
            ' username : wintexokansokmen@gmail.com
            ' password : Okansokmenwintex
            ' username : wintexprogram@gmail.com
            ' password : Wintexprogram2016
            ' port     : 587
            ' TLS      : true pop

            'oX.SendGMail("okansokmen@gmail.com",
            '"subject deneme",
            '"body deneme",,,
            '"wintex@wintexprogram.com",
            '"wintex@wintexprogram.com",
            '"Wintex2024", "smtp.office365.com", 587,, True, True, True)

            'oX.StiReportsBackup("c:\stibackup")

            oX.SendGMail("okansokmen@gmail.com",
            "subject deneme",
            "body deneme",,,
            "wintex@oxxo.com.tr",
            "wintex@oxxo.com.tr",
            "czkw ekig sqfh hojd", "smtp.gmail.com", 587,, True)

        End If

        MsgBox("***")


        '    oX.StyleShootsSysPar()
        '    oX.StyleShootsCopy()

        'oX.ReportToDevX("uretisemri2", cSQL)

        'If oX.init("192.1.0.34", "wintex", "sa_wintex", "saWinteX21@OXXO") Then
        '    Button1.Text = "OK"
        'oX.SendGMail("okansokmen@gmail.com", "subject deneme", "body deneme",,, "wintex@darindaturkey.com", "wintex@darindaturkey.com", "Drnd0906.", "smtp.yandex.com", 587)
        'oX.SendGMail("okansokmen@gmail.com", "subject deneme", "body deneme",,, "wintexprogram@hotmail.com", "wintexprogram@hotmail.com", "Wintex2022.", "smtp.office365.com", 587,, True)
        'oX.SendGMail("okansokmen@gmail.com", "subject deneme", "body deneme",,, "wintexokansokmen@gmail.com", "wintexokansokmen@gmail.com", "Okansokmenwintex", "smtp.gmail.com", 587,, True, False, False)

        'oX.GunlukDovizKuruGirisi()
        'oX.GetKurlarFromMBXML()

        'oX.SendGMail("okansokmen@gmail.com", "subject deneme", "body deneme",,,
        '             "wintex@indigo-white.com",
        '             "wintex@indigo-white.com",
        '             "Suk47138",
        '             "smtp.office365.com",
        '             587,, True, True, True)

        'Else
        '    Button1.Text = "Error Connect DB  "
        'End If


        'If oX.init("monster", "veragroup", "sa", "Hayabusa1024") Then
        '    cSQL = "select a.uretimtakipno, a.sevkiyattakipno, a.Departman, a.Firma, c.ModelNo, c.Renk, c.Beden,  IsemriAdedi = sum(coalesce(c.Adet,0)),  UretilenAdet = (select sum(coalesce(z.adet,0))  from uretharfis x with (NOLOCK), uretharfislines y with (NOLOCK), uretharrba z with (NOLOCK)  where x.uretfisno = y.uretfisno  and y.uretfisno = z.uretfisno  and y.ulineno = z.ulineno  and y.uretimtakipno = a.uretimtakipno  and z.modelno = c.modelno  and z.renk = c.renk  and z.beden = c.beden  and x.CikisDept = a.departman  and x.CikisFirm_Atl = a.firma),  d.sira, e.siralama   from uretimisemri a with (NOLOCK), uretimisdetayi b with (NOLOCK), uretimisrba c with (NOLOCK) , departman d with (NOLOCK) , beden e with (NOLOCK)  where a.isEmriNo = b.isEmriNo  and b.isEmriNo = c.isEmriNo  and b.ulineno = c.ulineno  and a.departman = d.departman  and c.beden = e.beden  and a.UretimTakipNo = c.UretimTakipNo  and a.UretimTakipNo is not null  and a.UretimTakipNo <> ''  and a.departman not like 'SEVK_YAT'  and a.uretimtakipno = 'M-00851-REC'  group by a.uretimtakipno, a.sevkiyattakipno, a.Departman, a.Firma, c.ModelNo, c.Renk, c.Beden, d.sira, e.siralama   "
        '    oX.ReportToDevX("uretimtakipsk", cSQL)
        'End If

        'Try
        'If oX.init("msi", "tes", "sa", "Hayabusa1024") Then
        'If oX.init("192.168.1.8", "TES", "sa", "er1303*?",, "YILDIRIM") Then
        '    oX.Translate()
        'End If

        'cSQL = "select w.*, " + _
        '        " SiparisTutari = (w.SiparisSatisFiyati * w.SiparisAdedi), " + _
        '        " SevkiyatTutari = (w.SiparisSatisFiyati * w.SevkiyatAdedi) "

        'cSQL = cSQL + _
        '        " from (Select a.kullanicisipno, a.siparistarihi, a.musterino, a.musteridepartman, a.musterisipno, " + _
        '                " a.ilksevktarihi, a.sonsevktarihi, a.sorumlu, a.siparisgrubu, a.dosyakapandi, a.teslimat, " + _
        '                " b.modelno, c.aciklama, c.anamodeltipi, c.yabanciadi, c.kalipno, " + _
        '                " ilksevkyil     = cast(datepart(year ,a.ilksevktarihi) As Decimal(10,0)), " + _
        '                " ilksevkay      = cast(datepart(month,a.ilksevktarihi) As Decimal(10,0)), " + _
        '                " ilksevkhafta   = cast(datepart(week ,a.ilksevktarihi) As Decimal(10,0)),  "
        'cSQL = cSQL + _
        '                " SiparisSatisFiyati = (Select top 1 x.satisfiyati " + _
        '                                " from sipfiyat x " + _
        '                                " where x.siparisno = a.kullanicisipno " + _
        '                                " And x.modelkodu = b.modelno " + _
        '                                " And x.satisfiyati Is Not null " + _
        '                                " And x.satisfiyati > 0), "
        'cSQL = cSQL + _
        '                " SiparisSatisDovizi = (Select top 1  x.satisdoviz " + _
        '                                " from sipfiyat x " + _
        '                                " where x.siparisno = a.kullanicisipno " + _
        '                                " And x.modelkodu = b.modelno " + _
        '                                " And x.satisfiyati Is Not null " + _
        '                                " And x.satisfiyati > 0), "
        'cSQL = cSQL + _
        '                " SiparisAdedi = (Select sum(coalesce(adet,0)) " + _
        '                                " from sipmodel " + _
        '                                " where siparisno = a.kullanicisipno " + _
        '                                " And modelno = b.modelno), "
        'cSQL = cSQL + _
        '                " SevkiyatAdedi = (Select adet = sum((y.koliend - y.kolibeg + 1) * z.adet) " + _
        '                                " from sevkform x, sevkformlines y, sevkformlinesrba z " + _
        '                                " where x.sevkformno = y.sevkformno " + _
        '                                " And y.sevkformno = z.sevkformno " + _
        '                                " And y.ulineno = z.ulineno " + _
        '                                " And y.siparisno = a.kullanicisipno " + _
        '                                " And y.modelno = b.modelno) "
        'cSQL = cSQL + _
        '                " from siparis a, sipmodel b, ymodel c "
        'cSQL = cSQL + _
        '                " where a.kullanicisipno = b.siparisno " + _
        '                " And b.modelno = c.modelno " + _
        '                " And (a.dosyakapandi = 'H' or a.dosyakapandi = '' or a.dosyakapandi is null) "
        'cSQL = cSQL + _
        '                " group by a.kullanicisipno, a.siparistarihi, a.musterino, a.musteridepartman, a.musterisipno, " + _
        '                " a.ilksevktarihi, a.sonsevktarihi, a.sorumlu, a.siparisgrubu, a.dosyakapandi, a.teslimat, " + _
        '                " b.modelno, c.aciklama, c.anamodeltipi, c.yabanciadi, c.kalipno) w "

        'If oX.init("OKANMSI", "AldersWinTex", "sa", "Hayabusa1024") Then
        '    oX.RotatePDF("c:\report.pdf")
        'End If
        'oX.ShowVersionInfo()

        'If oX.init("OKANMSI", "AldersWinTex", "sa", "Hayabusa1024") Then
        '    'If oX.init("alders2012\sqlexpress", "AldersWinTex", "sa", "Wintex12sa") Then
        '    'If oX.init("192.168.0.2", "WINTEX", "sa", "") Then
        '    Button1.Text = "OK"
        '    'oX.DevXReportDesign("sipsvk1", cSQL)
        '    'oX.DevXSpreadSheet(cSQL)
        '    'oX.ReportToDevX("deneme", cSQL)
        '    oX.SendGMail("okansokmen@gmail.com,efesokmen2004@gmail.com", , , "c:\devx.plf,c:\devx.pgf", "okansokmen@gmail.com,efesokmen2004@gmail.com")
        'Else
        '    Button1.Text = "Error Connect DB  "
        'End If

        'Dim cPersonel As String = ""
        'Dim cUserName As String = ""
        'Dim cActivePass As String = ""
        'Dim cPersonelFaceTemplate As String = ""
        'Dim cPersonelResim As String = ""
        'Dim nSimilarity As Double = 0

        'If oX.init("okanmsi", "ttnc", "sa", "Hayabusa1024") Then
        'If oX.init("okanmsi", "ttnc", "sa", "Hayabusa1024") Then

        'If oX.init("192.168.8.13", "istwtx", "sa", "Password1") Then
        'If oX.init("donsa", "donsa", "sa", "121200") Then
        'If oX.init("10.0.0.19", "alderswintex", "sa", "Wintex12sa") Then
        'If oX.init("192.1.0.34", "wintex", "sa", "sapass1") Then
        'oX.DevXScheduler(1)
        'oX.DevXReportDesign("", "onmodel", "select b.yenimodel, b.kritik, b.talepeden, a.tarih, a.musterino, a.modelno,  b.preordernumarasi, b.numuneturu, b.numunebedenadet, b.kumasgelistarihi,  b.numunetermini, b.numune, b.metraj, a.modelist,  a.kumas1, a.kumas2, a.kumas3, a.kumas4, a.kumas5,  a.kumas1mt, a.kumas2mt, a.kumas3mt, a.kumas4mt, a.kumas5mt,  a.kumas1birim, a.kumas2birim, a.kumas3birim, a.kumas4birim, a.kumas5birim,  a.tela, a.tela2, a.tela3,  a.telakullanimyeri, a.tela2kullanimyeri, a.tela3kullanimyeri,  a.telamt, a.tela2mt, a.tela3mt,  a.aksesuar1, a.aksesuar2, a.aksesuar3, a.aksesuar4, a.aksesuar5,  a.aksesuar6, a.aksesuar7, a.aksesuar8, a.aksesuar9, a.aksesuar10,  a.aksesuar11, a.aksesuar12, a.aksesuar13, a.aksesuar14, a.aksesuar15,  a.aksesuar1ad, a.aksesuar2ad, a.aksesuar3ad, a.aksesuar4ad, a.aksesuar5ad,  a.aksesuar6ad, a.aksesuar7ad, a.aksesuar8ad, a.aksesuar9ad, a.aksesuar10ad,  a.aksesuar11ad, a.aksesuar12ad, a.aksesuar13ad, a.aksesuar14ad, a.aksesuar15ad,  a.aksesuar1birim, a.aksesuar2birim, a.aksesuar3birim, a.aksesuar4birim, a.aksesuar5birim,  a.aksesuar6birim, a.aksesuar7birim, a.aksesuar8birim, a.aksesuar9birim, a.aksesuar10birim,  a.aksesuar11birim, a.aksesuar12birim, a.aksesuar13birim, a.aksesuar14birim, a.aksesuar15birim,  a.yikamakodu1, a.yikamakodu2, a.yikamakodu3, a.islem,  b.yikamaci , a.baskinakisisleme, b.kalip, a.Resim, a.notlar  from onmodel a with (NOLOCK), onmodel2 b with (NOLOCK)  where a.onmodelno = b.onmodelno ")
        'If oX.FaceRecognition(2, cPersonel, cUserName, cActivePass, cPersonelFaceTemplate, cPersonelResim, nSimilarity) Then
        '    MsgBox("Personel " + cPersonel + vbCrLf +
        '           "User " + cUserName + vbCrLf +
        '           "Pass " + cActivePass + vbCrLf +
        '           "FT " + Mid(cPersonelFaceTemplate, 1, 10) + vbCrLf +
        '           "PR " + cPersonelResim + vbCrLf +
        '           "Benzerlik " + nSimilarity.ToString)
        'End If
        'oX.DevXDashBoardView("522",  "", "select * from siparis", "MTFKumas")

        'oX.DevXScheduler(1, "tmpt_666")
        'End If

        'Catch ex As Exception
        '    MsgBox("Err : " + ex.Message)
        'End Try
    End Sub


    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim oX As New WinTexDLL.HTMain
        Dim cSQL1 As String = ""
        Dim cSQL2 As String = ""

        If oX.init("192.168.1.8", "MISIR", "sa", "er1303*?",, "YILDIRIM") Then
            oX.ReportDesigner("330", "yikamarecete",,,,,,,,,,,, 2)
            oX.ReportToViewer("330")
        End If

        'cSQL1 = "select v.PlanlananIlkSevkiyatYili, v.PlanlananIlkSevkiyatAyi, v.musterino, v.ulke, v.firma, v.siparisgrubu, v.anamodeltipi,  EURSatis = sum(coalesce(v.SiparisAdedi,0) * coalesce(v.SiparisSatisFiyati,0) * coalesce(v.satiskur,0) / coalesce(v.eurkur,0)),  TLSatis = sum(coalesce(v.SiparisAdedi,0) * coalesce(v.SiparisSatisFiyati,0) * coalesce(v.satiskur,0)),  AdetSatis = sum(coalesce(v.SiparisAdedi,0)),  SipSayisi = count(distinct v.kullanicisipno),  ModelSayisi = count(distinct v.modelno)  from (select w.*,  satiskur = dbo.getkur (w.SiparisSatisDovizi,w.siparistarihi),  eurkur = dbo.getkur ('EUR',w.siparistarihi)  from (select a.kullanicisipno, a.musterino, a.siparistarihi, a.ilksevktarihi, b.modelno, c.anamodeltipi, b.ulke, b.firma, a.siparisgrubu,  b.ilksevktar,  PlanlananIlkSevkiyatYili    = cast(datepart(year ,b.ilksevktar) as decimal(10,0)),  PlanlananIlkSevkiyatAyi     = cast(datepart(month,b.ilksevktar) as decimal(10,0)),  PlanlananIlkSevkiyatHaftasi = cast(datepart(week ,b.ilksevktar) as decimal(10,0)), SiparisSatisFiyati = (select top 1 (100 - (COALESCE(z.indirim,0))) / 100 * COALESCE(y.satisfiyati,0)  from siparis x, sipfiyat y, firma z  where x.kullanicisipno = y.siparisno  AND x.musterino = z.firma  AND y.satisfiyati is not null  AND y.satisfiyati <> 0  AND y.siparisno = a.kullanicisipno  AND (y.modelkodu = b.modelno OR y.modelkodu = 'HEPSI')),  SiparisSatisDovizi = (select top 1 y.satisdoviz  from siparis x, sipfiyat y, firma z  where x.kullanicisipno = y.siparisno  AND x.musterino = z.firma  AND y.satisfiyati is not null  AND y.satisfiyati <> 0  AND y.siparisno = a.kullanicisipno  AND (y.modelkodu = b.modelno OR y.modelkodu = 'HEPSI')),  SiparisAdedi = (select sum(coalesce(adet,0))  from sipmodel  where siparisno = a.kullanicisipno  and modelno = b.modelno  and ilksevktar = b.ilksevktar  and ulke = b.ulke  and firma = b.firma)  from siparis a,  sipmodel  b, ymodel c  where a.kullanicisipno = b.siparisno  and b.modelno = c.modelno  group by a.kullanicisipno, a.musterino, a.siparistarihi, a.ilksevktarihi, b.modelno, c.anamodeltipi, b.ilksevktar, b.ulke, b.firma, a.siparisgrubu) w) v  where v.eurkur is not null  and v.eurkur <> 0  group by v.PlanlananIlkSevkiyatYili, v.PlanlananIlkSevkiyatAyi, v.musterino, v.ulke, v.firma, v.siparisgrubu, v.anamodeltipi  "
        'cSQL2 = "select v.PlanlananIlkSevkiyatYili, v.PlanlananIlkSevkiyatAyi, v.musterino, v.ulke, v.firma, v.siparisgrubu, v.anamodeltipi,  EURSatis = sum(coalesce(v.SiparisAdedi,0) * coalesce(v.SiparisSatisFiyati,0) * coalesce(v.satiskur,0) / coalesce(v.eurkur,0)),  TLSatis = sum(coalesce(v.SiparisAdedi,0) * coalesce(v.SiparisSatisFiyati,0) * coalesce(v.satiskur,0)),  AdetSatis = sum(coalesce(v.SiparisAdedi,0)),  SipSayisi = count(distinct v.kullanicisipno),  ModelSayisi = count(distinct v.modelno)  from (select w.*,  satiskur = dbo.getkur (w.SiparisSatisDovizi,w.siparistarihi),  eurkur = dbo.getkur ('EUR',w.siparistarihi)  from (select a.kullanicisipno, a.musterino, a.siparistarihi, a.ilksevktarihi, b.modelno, c.anamodeltipi, b.ulke, b.firma, a.siparisgrubu,  b.ilksevktar,  PlanlananIlkSevkiyatYili    = cast(datepart(year ,b.ilksevktar) as decimal(10,0)),  PlanlananIlkSevkiyatAyi     = cast(datepart(month,b.ilksevktar) as decimal(10,0)),  PlanlananIlkSevkiyatHaftasi = cast(datepart(week ,b.ilksevktar) as decimal(10,0)), SiparisSatisFiyati = (select top 1 (100 - (COALESCE(z.indirim,0))) / 100 * COALESCE(y.satisfiyati,0)  from siparis x, sipfiyat y, firma z  where x.kullanicisipno = y.siparisno  AND x.musterino = z.firma  AND y.satisfiyati is not null  AND y.satisfiyati <> 0  AND y.siparisno = a.kullanicisipno  AND (y.modelkodu = b.modelno OR y.modelkodu = 'HEPSI')),  SiparisSatisDovizi = (select top 1 y.satisdoviz  from siparis x, sipfiyat y, firma z  where x.kullanicisipno = y.siparisno  AND x.musterino = z.firma  AND y.satisfiyati is not null  AND y.satisfiyati <> 0  AND y.siparisno = a.kullanicisipno  AND (y.modelkodu = b.modelno OR y.modelkodu = 'HEPSI')),  SiparisAdedi = (select sum(coalesce(adet,0))  from sipmodel  where siparisno = a.kullanicisipno  and modelno = b.modelno  and ilksevktar = b.ilksevktar  and ulke = b.ulke  and firma = b.firma)  from siparis a,  sipmodel  b, ymodel c  where a.kullanicisipno = b.siparisno  and b.modelno = c.modelno and (a.kapanissebep not in ('bizim iptalimiz','musteri iptali','acenta iptali','tedarikci iptali') or a.kapanissebep is null or a.kapanissebep = '') group by a.kullanicisipno, a.musterino, a.siparistarihi, a.ilksevktarihi, b.modelno, c.anamodeltipi, b.ilksevktar, b.ulke, b.firma, a.siparisgrubu) w) v  where v.eurkur is not null  and v.eurkur <> 0  group by v.PlanlananIlkSevkiyatYili, v.PlanlananIlkSevkiyatAyi, v.musterino, v.ulke, v.firma, v.siparisgrubu, v.anamodeltipi  "

        'If oX.init("MSI", "gecit", "sa", "Hayabusa1024") Then
        '    oX.ReportToViewer("98",,,,,,,,,,, 3)

        '    ' oX.DevXDashBoardDesign("447", "siparis", cSQL1, "SiparisToplam", cSQL2, "SipModel")
        '    'oX.DevXDashBoardView("466", cSQL1, cSQL2)
        'Else
        '    Button4.Text = "Error connect"
        'End If

        'If oX.init("alders2012\sqlexpress", "AldersWinTex", "sa", "Wintex12sa") Then
        'If oX.init("192.168.1.31", "WINTEX", "sa", "") Then
        'If oX.init("OKANMSI", "oxxo", "sa", "Hayabusa1024") Then
        '    Button1.Text = "OK : " + "OKANMSI, oxxo, sa, Hayabusa1024"
        'Else
        '    Button1.Text = "Error Connect DB : " + "okan3020, oxxo, sa, Hayabusa1024"
        'End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim oX As New WinTexDLL.HTMain
        Dim nAlis As Double
        Dim nSatis As Double

        Dim nAlis2 As Double
        Dim nSatis2 As Double

        'If oX.init("monster", "donsa", "sa", "Hayabusa1024") Then
        If oX.init("192.1.0.34", "wintex", "sa_wintex", "saWinteX21@OXXO") Then

            oX.GetKurFromMBXML2("USD", Now, nAlis, nSatis, nAlis2, nSatis2)
            oX.GetKurlarFromMBXML()
        End If

        'If oX.init("10.0.100.248", "bolero", "sa", "Bolerotex2019@.!") Then
        '    oX.ReportToViewer("271", " and a.kullanicisipno = '21-0443' ")
        'End If

        'oX.DevXReportViewer()

        'oX.ReportToViewer("22", " And a.tasarimno = '0000000016' ")
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Dim oX As New WinTexDLL.HTMain


        If oX.init("msi", "istwtx", "sa", "Hayabusa1024") Then
            oX.ReportDesigner("22", "siparis",,,,,,,,,,,, 2)
            'oX.DevXReportDesign("", "deneme2", "select * from siparis")
        Else
            Button4.Text = "Error connect"
        End If

    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Dim oX As New WinTexDLL.HTMain
        oX.ReportToPrinter("22")
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        Dim oX As New WinTexDLL.HTMain
        Dim cSQL1 As String = ""
        Dim cSQL2 As String = ""

        cSQL1 = "select w.*,  SiparisTutari  = (w.SiparisSatisFiyati * w.SiparisAdedi),  SevkiyatTutari = (w.SiparisSatisFiyati * w.SevkiyatAdedi)  from (select SiparisNo = a.kullanicisipno,  SiparisGelis = a.siparistarihi,  Musteri = a.musterino,  MusteriDepartman = a.musteridepartman,  MusteriSiparisNo = a.musterisipno,  PlanlananIlkSevkiyatTarihi = a.ilksevktarihi,  PlanlananSonSevkiyatTarihi = a.sonsevktarihi,  MusteriTemsilcisi = a.sorumlu,  SiparisGrubu = a.siparisgrubu,  SiparisKapandi = a.dosyakapandi,  TeslimSekli = a.teslimat,  ModelNo = b.modelno,  ModelAciklamasi = c.aciklama,  AnaModelTipi = c.anamodeltipi,  ModelYabanciAdi = c.yabanciadi,  KalipNo = c.kalipno,  PlanlananIlkSevkiyatYili    = cast(datepart(year ,a.ilksevktarihi) as decimal(10,0)),  PlanlananIlkSevkiyatAyi     = cast(datepart(month,a.ilksevktarihi) as decimal(10,0)),  PlanlananIlkSevkiyatHaftasi = cast(datepart(week ,a.ilksevktarihi) as decimal(10,0)),   SiparisSatisFiyati = (select top 1 (100 - (COALESCE(z.indirim,0))) / 100 * COALESCE(y.satisfiyati,0)  from siparis x, sipfiyat y, firma z  where x.kullanicisipno = y.siparisno  AND x.musterino = z.firma  AND y.satisfiyati is not null  AND y.satisfiyati <> 0  AND y.siparisno = a.kullanicisipno  AND (y.modelkodu = b.modelno OR y.modelkodu = 'HEPSI')),  SiparisSatisDovizi = (select top 1 y.satisdoviz  from siparis x, sipfiyat y, firma z  where x.kullanicisipno = y.siparisno  AND x.musterino = z.firma  AND y.satisfiyati is not null  AND y.satisfiyati <> 0  AND y.siparisno = a.kullanicisipno  AND (y.modelkodu = b.modelno OR y.modelkodu = 'HEPSI')),  SiparisAdedi = (select sum(coalesce(adet,0))  from sipmodel  where siparisno = a.kullanicisipno  and modelno = b.modelno),  SevkiyatAdedi = (select adet = sum((y.koliend - y.kolibeg + 1) * z.adet)  from sevkform x, sevkformlines y, sevkformlinesrba z  where x.sevkformno = y.sevkformno  and y.sevkformno = z.sevkformno  and y.ulineno = z.ulineno  and y.siparisno = a.kullanicisipno  and y.modelno = b.modelno  and x.ok = 'E')  from siparis a,  sipmodel  b, ymodel c  where a.kullanicisipno = b.siparisno  and b.modelno = c.modelno  group by a.kullanicisipno, a.siparistarihi, a.musterino, a.musteridepartman, a.musterisipno,  a.ilksevktarihi, a.sonsevktarihi, a.sorumlu, a.siparisgrubu, a.dosyakapandi, a.teslimat,  b.modelno, c.aciklama, c.anamodeltipi, c.yabanciadi, c.kalipno) w "
        cSQL2 = "select * from sipmodel "

        If oX.init("OKANMSI", "AldersWinTex", "sa", "Hayabusa1024") Then
            oX.DevXDashBoardView("447", cSQL1, cSQL1 + " where w.musteri = 'H&M' ", "SiparisToplam", cSQL2, cSQL2, "SipModel")
        Else
            Button6.Text = "Error connect"
        End If

    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        Dim oX As New WinTexDLL.HTMain
        If oX.init("192.1.0.34", "wintex", "sa_wintex", "saWinteX21@OXXO") Then
            oX.StiBulDegistir(3)
        End If

    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        Dim oX As New WinTexDLL.HTMain
        oX.MrtToReport()
    End Sub

    Private Sub Button10_Click(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        Dim oX As New WinTexDLL.HTMain
        oX.ReportToPivot("uretharrba")
    End Sub

    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        'Dim oX As New WinTexDLL.HTMain
        'oX.SuperEditor("")

        Dim oX As New WinTexMNG.HTMain
        Dim cSonuc As String = ""
        Dim cErrorMessage As String = ""
        Dim n1 As Double
        Dim n2 As Double

        'oX.TakipsanGetInfo("18931.b-1/Zara", "erogluaksaray", "121312", n1, n2)
        oX.TakipsanGetInfo("18931.b-1/Zara", "eroglumisir", "121312", n1, n2)

        'oX.TakipsanGetInfo("18931.b-1/Zara", "eroglumisiradmin", "eroglumisir121312", n1, n2)
        'oX.TakipsanGetInfo("18931.b-1/Zara", "erogluaksarayadmin", "erogluaksaray121312", n1, n2)

        'oX.init("monster", "gecit", "sa", "Hayabusa1024")
        'oX.KargoZamaniSysPar()
        'oX.ArasSysPar()
        'oX.PostmanSendOrder("880000618980", cSonuc, cErrorMessage, 3)
        'oX.PostmanQueryOrder("880000593504")
        'oX.eContGetCountries()
        'oX.IESendOrder("880000593504", cSonuc, cErrorMessage)
        'oX.PostmanQueryOrder("880000621936", cSonuc, cErrorMessage, 3)
        MsgBox(cSonuc + " " + cErrorMessage)
        'oX.eContGetCities("BGR")
    End Sub

    Private Sub Button12_Click(sender As System.Object, e As System.EventArgs) Handles Button12.Click
        Dim oX As New WinTexDLL.HTMain
        oX.SuperPicture("f:\9224.jpg", "f:\9225.jpg")
    End Sub

    Private Sub Button15_Click(sender As System.Object, e As System.EventArgs) Handles Button15.Click
        Dim oX As New WinTexDLL.HTMain
        If oX.init("192.168.0.1", "donsa", "sa", "121200") Then
            oX.TakePicture(-1)
        End If

        oX = Nothing
    End Sub

    Private Sub Button16_Click(sender As System.Object, e As System.EventArgs) Handles Button16.Click
        Dim oX As New WinTexDLL.HTMain
        oX.TakePicture(0, "c:\deneme")
        oX = Nothing

    End Sub

    Private Sub Button17_Click(sender As System.Object, e As System.EventArgs) Handles Button17.Click

        Dim oX As New WinTexDLL.HTMain

        'If oX.init("10.0.100.248", "bolero", "sa", "Bolerotex2019@.!") Then
        '    oX.eDokSisSysPar()
        '    oX.eDokSisInit()
        '    oX.eDokSisSend(1, "0000014089")
        '    oX.eDokSisDispose()
        'End If
        'cRPAServer = "192.168.1.8"   ' oSQL.GetSysPar("RPAServer", "192.168.1.8")
        'cRPADatabase = "ROBO"        ' oSQL.GetSysPar("RPADatabase", "ROBO")
        'cRPAUserName = "sa"          ' oSQL.GetSysPar("RPAUsername", Gl_UserName)
        'cRPAPassword = "er1303*?"    ' oSQL.GetSysPar("RPAPassword", Gl_ActivePass)

        'Exit Sub

        If oX.init("MONSTER", "istwtx", "sa", "Hayabusa1024") Then

            'If oX.init("192.168.1.8", "TESDENEME", "sa", "er1303*?") Then
            'oX.RPASiparis()

            'oX.ZaraPDFtoSiparis()

            'If oX.init("192.168.8.13", "istwtx", "sa", "damdandUstU2kurbaGa") Then

            'oX.TurkKEPSysPar()

            'If Not oX.TurkKEPinit() Then
            '    MessageBox.Show("servise bağlanılamadı, kullanıcı adı/şifresi yanlış")
            '    Exit Sub
            'End If

            'stok fişi 0000208110 JNI2022000000226
            'üretim fişi 0000118176 JNI2022000000211
            'If Not oX.CRSCheckValidIrsaliye(1, "0000209872") Then
            '    MessageBox.Show("entegrasyon başarısız")
            '    Exit Sub
            'End If
            'oX.TurkKEPKontor()
            'oX.TurkKEPSend(1, "0000209872", False)

            'Dim cN As String
            'Dim cU As String
            'oX.TurkKEPGetIrsaliyeNoUUID("2647996", cN, cU)

            'Dim cMsg As String
            'cMsg = oX.TurkKEPGetStatus("2647996", cN, cU)

            'oX.TurkKEPDispose()

            'oX.CRSSysPar()

            'oX.CRSinit()

            'If oX.CRSCheckValidIrsaliye(1, "0000237938") Then
            '    oX.CRSSend(1, "0000237938", True)
            'End If

            'If oX.CRSCheckValidIrsaliye(2, "0000000162") Then
            '    oX.CRSSend(2, "0000000162", True)
            'End If

            'oX.CRSDispose()

            oX.ParkSysPar()
            If oX.CRSCheckValidIrsaliye(1, "0000232947") Then
                oX.Parkinit()
                oX.ParkSend(1, "0000232947")
                'oX.ParkStatus()
                oX.ParkDispose()
            End If
        End If


        'oX.initWebService()
        'oX.SendEIrsaliye(1, "0000000156")
        'oX.SendEIrsaliye(2, "0000000162")


        'Dim oX As New WinTexDLL.HTMain

        'Dim nAlis As Double = 0
        'Dim nSatis As Double = 0
        'Dim nEfektifAlis As Double = 0
        'Dim nEfektifSatis As Double = 0
        'Dim cSQL As String = ""

        'Dim cSonuc As String = ""

        'cSQL = "select a.musterino, b.modelno, b.ilksevktar, " +
        '        " adet = sum(coalesce(b.adet,0)) " +
        '        " from siparis a with (NOLOCK) , sipmodel b with (NOLOCK)  " +
        '        " where a.kullanicisipno = b.siparisno " +
        '        " and (a.dosyakapandi is null or a.dosyakapandi = 'H' or a.dosyakapandi = '') " +
        '        " group by a.musterino, b.modelno, b.ilksevktar "

        'cSQL = "select v.PlanlananIlkSevkiyatYili, v.PlanlananIlkSevkiyatAyi,  modelno = v.aciklama,  MusteriNo = (case when v.musterino is null or v.musterino = '' then 'BELIRSIZ' else v.musterino end),  SiparisGrubu = (case when v.siparisgrubu is null or v.siparisgrubu = '' then 'BELIRSIZ' else v.siparisgrubu end),  AnaModelTipi = (case when v.anamodeltipi is null or v.anamodeltipi = '' then 'BELIRSIZ' else v.anamodeltipi end),  AdetSatis = sum(coalesce(v.SiparisAdedi,0)),  AdetSevkiyat = sum(coalesce(v.SevkiyatAdedi,0)),  EURSatis = sum(coalesce(v.SiparisAdedi,0) * coalesce(v.SiparisSatisFiyati,0) * coalesce(v.satiskur,0) / coalesce(v.eurkur,0)),  EURSevkiyat = sum(coalesce(v.SevkiyatAdedi,0) * coalesce(v.SiparisSatisFiyati,0) * coalesce(v.satiskur,0) / coalesce(v.eurkur,0)),  TLSatis = sum(coalesce(v.SiparisAdedi,0) * coalesce(v.SiparisSatisFiyati,0) * coalesce(v.satiskur,0)),  TLSevkiyat = sum(coalesce(v.SevkiyatAdedi,0) * coalesce(v.SiparisSatisFiyati,0) * coalesce(v.satiskur,0))  from (select w.*,  satiskur = dbo.getkur (w.SiparisSatisDovizi,w.siparistarihi),  eurkur = dbo.getkur ('EUR',w.siparistarihi)  from (select a.kullanicisipno, a.musterino, a.siparistarihi, a.ilksevktarihi, b.modelno, c.anamodeltipi, b.ulke, b.firma, a.siparisgrubu, b.sevkiyattakipno, b.tasimasekli, c.aciklama,  b.ilksevktar,  PlanlananIlkSevkiyatYili    = cast(datepart(year ,b.ilksevktar) as decimal(10,0)),  PlanlananIlkSevkiyatAyi     = cast(datepart(month,b.ilksevktar) as decimal(10,0)),  PlanlananIlkSevkiyatHaftasi = cast(datepart(week ,b.ilksevktar) as decimal(10,0)),   SiparisSatisFiyati = (select top 1 (100 - (COALESCE(z.indirim,0))) / 100 * COALESCE(y.satisfiyati,0)  from siparis x with (NOLOCK), sipfiyat y with (NOLOCK), firma z with (NOLOCK)  where x.kullanicisipno = y.siparisno  AND x.musterino = z.firma  AND y.satisfiyati is not null  AND not (y.satisfiyati = 0)  AND y.siparisno = a.kullanicisipno  AND (y.modelkodu = b.modelno OR y.modelkodu = 'HEPSI')),  SiparisSatisDovizi = (select top 1 y.satisdoviz  from siparis x with (NOLOCK), sipfiyat y with (NOLOCK), firma z with (NOLOCK)  where x.kullanicisipno = y.siparisno  AND x.musterino = z.firma  AND y.satisfiyati is not null  AND not (y.satisfiyati = 0)  AND y.siparisno = a.kullanicisipno  AND (y.modelkodu = b.modelno OR y.modelkodu = 'HEPSI')),  SevkiyatAdedi = (select adet = sum((y.koliend - y.kolibeg + 1) * z.adet)  from sevkform x with (NOLOCK), sevkformlines y with (NOLOCK), sevkformlinesrba z with (NOLOCK)  where x.sevkformno = y.sevkformno  and y.sevkformno = z.sevkformno  and y.ulineno = z.ulineno  and y.siparisno = a.kullanicisipno  and y.modelno = b.modelno  and y.sevkiyattakipno = b.sevkiyattakipno  and x.ok = 'E'),  SiparisAdedi = (select sum(coalesce(adet,0))  from sipmodel with (NOLOCK)  where siparisno = a.kullanicisipno  and modelno = b.modelno  and ilksevktar = b.ilksevktar  and sevkiyattakipno = b.sevkiyattakipno  and ulke = b.ulke  and firma = b.firma  and tasimasekli = b.tasimasekli)  from siparis a with (NOLOCK),  sipmodel  b with (NOLOCK), ymodel c with (NOLOCK)  where a.kullanicisipno = b.siparisno  and b.modelno = c.modelno AND  ((a.ilksevktarihi >= '01.01.2019'  AND  a.ilksevktarihi < '01.01.2020') )  and (a.kapanissebep not in ('bizim iptalimiz','musteri iptali','acenta iptali','tedarikci iptali') or a.kapanissebep is null or a.kapanissebep = '') group by a.kullanicisipno, a.musterino, a.siparistarihi, a.ilksevktarihi, b.modelno, c.anamodeltipi, b.ulke, b.firma, a.siparisgrubu, b.sevkiyattakipno, b.tasimasekli, c.aciklama, b.ilksevktar) w) v  where v.eurkur is not null  and not (v.eurkur = 0)  group by v.PlanlananIlkSevkiyatYili, v.PlanlananIlkSevkiyatAyi, v.modelno, v.musterino, v.siparisgrubu, v.anamodeltipi, v.aciklama  order by PlanlananIlkSevkiyatYili, PlanlananIlkSevkiyatAyi, modelno  "


        'If Not oX.init("msi", "alderswintex", "sa", "Hayabusa1024") Then Exit Sub
        'oX.DevXSpreadSheet(cSQL, 2)
        'oX.ReportToPivot(cSQL)
        'oX.ReportToSpread(cSQL)

        'oX.GunlukDovizKuruGirisi()

        'oX.SuperEditor("")
        'oX.GunlukDovizKuruGirisi()

        'oX.GetKurFromMBXML(DateValue(Today))
        'oX.GetKurFromMBXML2("EUR", DateValue(Today), nAlis, nSatis, nEfektifAlis, nEfektifSatis)
        'MsgBox("Sonuc : " + nAlis.ToString + " " + nSatis.ToString + " " + nEfektifAlis.ToString + " " + nEfektifSatis.ToString)

        'oX.GetKurFromLogo()
        'oX.GetKurFromUyum()
        'oX.GetKurlarFromMBXML()

        'cSonuc = oX.GroupDocsShow("c:\deneme.xlsx")

        'MsgBox(cSonuc)
        'oX = Nothing
    End Sub


    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click

        Dim oX As New WinTexDLL.HTMain
        Dim cSonuc As String = "OK"
        Dim lSonuc As Boolean = False
        Dim cFilter As String = ""

        If oX.init("192.168.1.8", "TESDENEME", "sa", "er1303*?",, "CAYMAK") Then


            'oX.DownloadFTP("ftp://78.188.110.130/wintex/", "wintex", "wintex", "WinTexSetup2017.msi", "C:\wintex\")

            'If oX.init("msi", "TES", "sa", "Hayabusa1024") Then

            oX.UyumSysPar()
            'oX.UyumCariWinTexOtoUpdate()

            'oX.UyumStoklarTable()

            'lSonuc = oX.UyumStokAdd(" And a.stokno = 'YENİ TAS'  ", cSonuc)
            'MsgBox(cSonuc)

            'oX.UyumCariWinTexOtoUpdate()

            'cFilter = " AND  ((a.faturatarihi >= '01.01.2019'  AND  a.faturatarihi < '16.01.2019') )  AND  (b.stokhareketkodu in ('02 Tedarikten Giris','07 Satis', '07 Satis Iade') ) "
            'cFilter = " AND  ((a.faturatarihi >= '01.01.2019'  AND  a.faturatarihi < '30.01.2019') ) and a.stokfisno = '0000134524' "

            cFilter = " and a.stokfisno = '0001857863' "
            lSonuc = oX.UyumStokFisiKontrol(cFilter)
            If lSonuc Then
                lSonuc = oX.UyumStokFisiAktar(cFilter, cSonuc)
            End If

            'lSonuc = oX.UyumStokFisiDelete(cFilter)
            'If Not lSonuc Then Exit Sub

            'lSonuc = oX.UyumStokFisiKontrol(cFilter)
            'If lSonuc Then
            '    lSonuc = oX.UyumStokFisiAktar(cFilter)
            'End If
            'MsgBox(lSonuc.ToString)

            'If oX.UyumSatinalmaSiparisAdd("0000225276", cSonuc) Then
            '    MsgBox("satınalma siparişi eklendi")
            'Else
            '    MsgBox("satınalma siparişi eklenemedi")
            'End If

            'If oX.UyumSatinalmaSiparisDelete("0000221284", cSonuc) Then
            '    MsgBox("satınalma siparişi silindi " + cSonuc)
            'Else
            '    MsgBox("satınalma siparişi silinemedi " + cSonuc)
            'End If


            'If oX.UyumUretimSiparisAdd("D366362", cSonuc) Then
            '    MsgBox("üretim siparişi eklendi " + cSonuc)
            'Else
            '    MsgBox("üretim siparişi eklenemedi " + cSonuc)
            'End If

            'If oX.UyumUretimSiparisAdd("D220191", cSonuc) Then
            '    MsgBox("üretim siparişi eklendi " + cSonuc)
            'Else
            '    MsgBox("üretim siparişi eklenemedi " + cSonuc)
            'End If


            'If oX.UyumUretimSiparisDelete("I811363", cSonuc) Then
            '    MsgBox("üretim siparişi silindi " + cSonuc)
            'Else
            '    MsgBox("üretim siparişi silindi " + cSonuc)
            'End If

            'If oX.UyumUretimSiparisDelete("D220191", cSonuc) Then
            '    MsgBox("üretim siparişi silindi " + cSonuc)
            'Else
            '    MsgBox("üretim siparişi silindi " + cSonuc)
            'End If

            'If oX.UyumBOMAdd("MOLLY MATELL", cSonuc) Then
            '    oX.UyumUIEAdd("D220191", cSonuc)
            'Else
            '    MsgBox("BOM eklenemedi")
            'End If
            'oX.UyumUretimAdd2("0001374929", cSonuc)


            'If oX.UyumUretimAdd2("0001426877", cSonuc) Then
            '    If oX.UyumUretimDelete2("0001426877", cSonuc) Then
            '        MsgBox("üretim eklendi")
            '    Else
            '        MsgBox("üretim eklenemedi")
            '    End If
            'End If

            MsgBox(cSonuc)

        Else
            Button19.Text = "Error connect"
        End If

    End Sub

    Private Sub Button14_Click(sender As System.Object, e As System.EventArgs) Handles Button14.Click

        Dim oX As New WinTexDLL.HTMain
        Dim cSonuc As String = ""
        Dim cDoviz As String = "USD"
        Dim dTarih As Date = Now.Date
        Dim nAlis As Double = 0
        Dim nSatis As Double = 0
        Dim nEfektifAlis As Double = 0
        Dim nEfektifSatis As Double = 0

        'If Not oX.init("monster", "wintexkarmen", "sa", "Hayabusa1024") Then Exit Sub
        'oX.ResimSpread()

        'Dim cSQL As String = "select StokKodu = a.stokno, StokAciklama = c.cinsaciklamasi, StokTipi = c.stoktipi, Renk = b.renk, RenkAciklama = b.yabancirenk, " +
        '" Parti = a.partino, Siparis = a.malzemetakipkodu, Tedarikci = b.firma, Depo = a.depo, Giren = a.giris, Cikan = a.cikis, " +
        '" Kalan = coalesce(a.giris,0) - coalesce(a.cikis,0) " +
        '" from envanterrb a , renk b with (NOLOCK) , stok c with (NOLOCK) " +
        '" where a.stokno = c.stokno " +
        '" And a.renk = b.renk " +
        '" And a.stokno Like 'IP%'  "

        If Not oX.init("192.1.0.34", "wintex", "sa_wintex", "saWinteX21@OXXO") Then Exit Sub
        'If Not oX.init("192.168.1.150", "wintexvardar", "wintex", "wintex") Then Exit Sub
        'If Not oX.init("192.168.0.2", "donsa", "sa", "121200") Then Exit Sub
        'oX.GunlukDovizKuruGirisi()
        'oX.GetKurFromMBXML2(cDoviz, dTarih, nAlis, nSatis, nEfektifAlis, nEfektifSatis)
        'oX.GetKurlarFromMBXML()

        'oX.ReportToDevX("genel", cSQL, 3)

        'oX.ReportToDevX("genel",,, "NTS_PROGRESS_6WEEKS_SUBE", "192.1.0.34", "OXXO_DW", "sa_wintex", "saWinteX21@OXXO")
        cSonuc = oX.OLAPDevX("6Weeks", "SiparisNo", "provider=MSOLAP;data source=datasrv2;initial catalog=Oxxo_Report_Cubes;cube name=6Weeks_Stores;Query Timeout=100;")
        MsgBox("sonuç : " + cSonuc)
        'If Not oX.init("10.0.0.19", "alderswintex", "sa", "Wintex12sa") Then Exit Sub
        'oX.DevXDashBoardDesign(533)
        'oX.DevXDashBoardView("533")
        'oX.DevXReportDesign("", "onmodel", "select b.yenimodel, b.kritik, b.talepeden, a.tarih, a.musterino, a.modelno,  b.preordernumarasi, b.numuneturu, b.numunebedenadet, b.kumasgelistarihi,  b.numunetermini, b.numune, b.metraj, a.modelist,  a.kumas1, a.kumas2, a.kumas3, a.kumas4, a.kumas5,  a.kumas1mt, a.kumas2mt, a.kumas3mt, a.kumas4mt, a.kumas5mt,  a.kumas1birim, a.kumas2birim, a.kumas3birim, a.kumas4birim, a.kumas5birim,  a.tela, a.tela2, a.tela3,  a.telakullanimyeri, a.tela2kullanimyeri, a.tela3kullanimyeri,  a.telamt, a.tela2mt, a.tela3mt,  a.aksesuar1, a.aksesuar2, a.aksesuar3, a.aksesuar4, a.aksesuar5,  a.aksesuar6, a.aksesuar7, a.aksesuar8, a.aksesuar9, a.aksesuar10,  a.aksesuar11, a.aksesuar12, a.aksesuar13, a.aksesuar14, a.aksesuar15,  a.aksesuar1ad, a.aksesuar2ad, a.aksesuar3ad, a.aksesuar4ad, a.aksesuar5ad,  a.aksesuar6ad, a.aksesuar7ad, a.aksesuar8ad, a.aksesuar9ad, a.aksesuar10ad,  a.aksesuar11ad, a.aksesuar12ad, a.aksesuar13ad, a.aksesuar14ad, a.aksesuar15ad,  a.aksesuar1birim, a.aksesuar2birim, a.aksesuar3birim, a.aksesuar4birim, a.aksesuar5birim,  a.aksesuar6birim, a.aksesuar7birim, a.aksesuar8birim, a.aksesuar9birim, a.aksesuar10birim,  a.aksesuar11birim, a.aksesuar12birim, a.aksesuar13birim, a.aksesuar14birim, a.aksesuar15birim,  a.yikamakodu1, a.yikamakodu2, a.yikamakodu3, a.islem,  b.yikamaci , a.baskinakisisleme, b.kalip, a.Resim, a.notlar  from onmodel a with (NOLOCK), onmodel2 b with (NOLOCK)  where a.onmodelno = b.onmodelno")
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click

        'Dim oX As WinTexYKK.HTMain
        'Dim oX2 As New WinTexDLL.HTMain
        Dim cSonuc As String = ""

        'If Not oX2.init("msi", "tes", "sa", "Hayabusa1024") Then Exit Sub
        'oX2.EliarSysPar()

        'If Not oX.init("ER-WINTEX", "TES", "sa", "er1303*?") Then Exit Sub

        'If Not oX.init("monster", "tes", "sa", "Hayabusa1024") Then Exit Sub

        'Dim token = GetTokenAsync().Result
        'If Not String.IsNullOrEmpty(token) Then
        '    Console.WriteLine("Token alındı: " & token)

        '    Dim orderData = GetOrderDataAsync(token).Result
        '    If Not String.IsNullOrEmpty(orderData) Then
        '        Dim formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(orderData), Newtonsoft.Json.Formatting.Indented)
        '        Console.WriteLine(vbNewLine & "Sipariş Verileri:")
        '        Console.WriteLine(formattedJson)
        '    End If

        'End If

        'oX.YKKSysPar()
        'oX.GetAddresses()

        'oX = New WinTexYKK.HTMain
        'If oX.init("monster", "tes", "sa", "Hayabusa1024") Then
        '    cSonuc = oX.SendIsemri2("0000310256")
        '    oX = Nothing
        'End If

        'oX = New WinTexYKK.HTMain
        'If oX.init("monster", "tes", "sa", "Hayabusa1024") Then
        '    cSonuc = oX.GetIsemriDurum2("0000310256")
        '    oX = Nothing
        'End If


        MsgBox("sonuç : " + cSonuc)

        'cSonuc = oX.SendIsemri("0000283215")
        'cSonuc = oX.GetIsemriDurum2("0000262888")
        'Button18.Text = "YKK"

        'Dim oX As New WinTexDLL.HTMain

        'If oX.init("10.0.0.19", "AldersWinTex", "sa", "Wintex12sa",, "OKAN", "osalders", "bfy381!") Then
        '    'oX.Abit(Now.AddDays(-10), Now)
        '    oX.WinTexCs()
        'Else
        '    Button18.Text = "Error connect"
        'End If

        'If oX.init("MSI", "alderswintex", "sa", "Hayabusa1024") Then
        '    oX.WinTexCs()
        'Else
        '    Button18.Text = "Error connect"
        'End If

        'Dim oX2 As New WinTexTicimax.HTMain
        'If Not oX2.init("monster", "gecit", "sa", "Hayabusa1024") Then Exit Sub
        'oX2.AnaMenu()

    End Sub

    Private Sub Button13_Click(sender As System.Object, e As System.EventArgs) Handles Button13.Click
        'Dim oX As New WinTexDLL.HTMain
        'oX.StitchPictures("f:\9224.jpg", "f:\9225.jpg", "f:\resimx.jpg")
        'Try

        Dim oX As New WinTexMNG.HTMain
        'Dim oX As New WinTexDLL.HTMain
        Dim cSonuc As String = ""
        Dim cErrorMessage As String = ""
        Dim cSiparis As String = "880000213799;880000213805;880000213723;880000214160;880000214159;880000214158;880000214157" ' ;880000214156;880000214155;880000214154;880000214153;880000214152;880000214149;880000214150;880000214148;880000214146;880000214144;880000214145;880000214143;880000214142;880000214141;880000214131;880000214127;880000214126;880000214123;880000214121;880000214109;880000214119;880000214118;880000214116;880000214115;880000214114;880000214113;880000214112;880000214111;880000214107;880000214108;880000214102;880000214106;880000214104;880000214103;880000214101;880000214100;880000214099;880000214098;880000214097;880000214096;880000214095;880000214093;880000214087"
        'Dim aSonuc() As String

        If oX.init("MONSTER", "gecit", "sa", "Hayabusa1024") Then
            'oX.SipPerakendeSonDurum(1, True)
            'If oX.init("192.168.1.8", "TES", "sa", "er1303*?") Then
            'oX.MNGSysPar()
            'oX.MNGSendOrder("880000000119")
            'oX.MNGQueryOrder("880000087806", cSonuc)
            'oX.MNGCancelOrder("880000000119")

            oX.MNGQueryOrder(cSiparis, cSonuc)
            'oX.MNGQueryOrder("880000213799", cSonuc)
            'oX.SipPerakendeSonDurum(2, True)

            'oX.ByExpressSysPar()
            'oX.ByExpressSendOrder("880000000120")
            'oX.ByExpressQueryOrder("880000000190", "871EB1A5E8EE434")
            'oX.ByExpressCancelOrder("880000000120")

            'oX.PTTSysPar()
            'oX.PTTEkHizmetSorgula()
            'oX.PTTIlceSorgula()
            'oX.PTTSendOrder("880000001407", cSonuc)
            'oX.PTTQueryOrder("880000107351", cSonuc, cErrorMessage)
            'oX.PTTCancelOrder("880000000200", cSonuc)
            'oX.PTTQueryOrder()

            'oX.NacSysPar()
            ''oX.GetNacCredit()
            'oX.SendNacSms("905425343855", "WinTex", "Hello Deneme Okan")
            'oX.SendSmsSipPerakende("880000094136")
            'Dim dDate As Date
            'Dim cUser As String
            'oX.WinTexDragDrop(1, "D600043")
            'MsgBox(dDate.ToString + " " + cUser)

            'oX.YurticiSysPar()

            'oX.YurticiSendOrder("880000181057", cSonuc, cErrorMessage)
            'MsgBox("YurticiSendOrder : " + cSonuc + " " + cErrorMessage)

            'oX.YurticiQueryOrder("880000189570", cSonuc, cErrorMessage)
            'oX.YurticiQueryOrder("880000198856", cSonuc, cErrorMessage)
            'MsgBox("YurticiQueryOrder : " + cSonuc + " " + cErrorMessage)

            'oX.YurticiCancelOrder("880000181057", cErrorMessage)
            'MsgBox("YurticiCancelOrder : " + cErrorMessage)
        End If


        oX = Nothing

        'Catch ex As Exception
        '    MsgBox("Error " + ex.Message)
        'End Try

    End Sub

    'Private Sub TextBox1_DragEnter(sender As Object, e As DragEventArgs) Handles TextBox1.DragEnter

    '    Dim oMailItem As MailItem
    '    Dim cBuffer As String = ""

    '    If Not IsNothing(e.Data) Then

    '        oMailItem = TryCast(e.Data, MailItem)

    '        cBuffer = oMailItem.Body
    '        cBuffer = cBuffer + vbCrLf + oMailItem.Subject
    '        cBuffer = cBuffer + vbCrLf + e.Data.GetData(DataFormats.Text)
    '        TextBox1.Text = cBuffer
    '    End If
    'End Sub

    ' *******************

    Private Sub TextBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TextBox1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Or e.Data.GetDataPresent("FileGroupDescriptor") Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    Private Sub TextBox1_DragDrop(sender As Object, e As DragEventArgs) Handles TextBox1.DragDrop

        Dim strDestinationPath As String = "c:\ProgramData\temp"
        Dim strDestinationFile As String = ""
        Dim strFilename As String = ""
        Dim lFileDragDrop As Boolean = False
        Dim lOutlookDragDrop As Boolean = False
        Dim aFiles() As String
        Dim dCreationDate As Date
        Dim cCreationUser As String

        If e.Data.GetDataPresent("FileGroupDescriptor") Then
            lOutlookDragDrop = True
        ElseIf e.Data.GetDataPresent(DataFormats.FileDrop) Then
            lFileDragDrop = True
        End If

        If Not IO.Directory.Exists("c:\ProgramData\temp") Then IO.Directory.CreateDirectory("c:\ProgramData\temp")

        If lFileDragDrop Then
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                aFiles = e.Data.GetData(DataFormats.FileDrop)
                strDestinationFile = aFiles(0)
                dCreationDate = IO.File.GetCreationTime(strDestinationFile)

                Dim fi As New IO.FileInfo(strDestinationFile)
                Dim fs As System.Security.AccessControl.FileSecurity = fi.GetAccessControl
                Dim owner As System.Security.Principal.NTAccount = CType(fs.GetOwner(GetType(System.Security.Principal.NTAccount)), System.Security.Principal.NTAccount)
                cCreationUser = owner.Value.ToString

                MsgBox("Creation date : " + dCreationDate.ToString + vbCrLf +
                       "Owner : " + cCreationUser)
            End If
        End If

        If lOutlookDragDrop Then
            If e.Data.GetDataPresent("FileGroupDescriptor") Then
                'Get the name of the dragged file/message
                Dim theStream As IO.Stream = DirectCast(e.Data.GetData("FileGroupDescriptor"), IO.Stream)
                Dim fileGroupDescriptor As Byte() = New Byte(511) {}
                theStream.Read(fileGroupDescriptor, 0, 512)

                Dim fileName As New System.Text.StringBuilder("")
                Dim i As Integer = 76
                While fileGroupDescriptor(i) <> 0
                    fileName.Append(Convert.ToChar(fileGroupDescriptor(i)))
                    i += 1
                End While
                theStream.Close()

                strFilename = fileName.ToString
            End If

            'Check if user dragged the Message or Attachment
            If IO.Path.GetExtension(strFilename).ToUpper = ".MSG" Then

                'Message dragged and dropped
                Dim objMI As MailItem
                Dim objOL As New Application

                For Each objMI In objOL.ActiveExplorer.Selection()
                    strFilename = RemoveIllegalChar(objMI.Subject) & ".msg"
                    strDestinationFile = IO.Path.Combine(strDestinationPath, strFilename)

                    If IO.File.Exists(strDestinationFile) Then
                        MsgBox(strFilename & " Is already In this folder" & vbCrLf & "COPY ABORTED", MsgBoxStyle.Exclamation)
                    Else
                        Try
                            TextBox1.Text = strFilename
                            objMI.SaveAs(strDestinationFile, OlSaveAsType.olMSG)
                            'Check file copied OK then add to Attachment Table and File List
                        Catch ex As System.Exception
                            MsgBox("Error copying email", MsgBoxStyle.Exclamation)
                            Exit Sub
                        End Try
                    End If

                    MsgBox("Success, email copied OK", MsgBoxStyle.Exclamation)

                Next

            Else
                'Check if File/Message already exists
                strDestinationFile = IO.Path.Combine(strDestinationPath, strFilename)

                If IO.File.Exists(strDestinationFile) Then
                    MsgBox(strFilename & " Is already In this folder" & vbCrLf & "COPY ABORTED", MsgBoxStyle.Exclamation)
                End If
                Try

                    Dim ms As IO.MemoryStream = DirectCast(e.Data.GetData("FileContents", True), IO.MemoryStream)
                    Dim fileBytes As Byte() = New Byte(CInt(ms.Length - 1)) {}
                    ms.Position = 0
                    ms.Read(fileBytes, 0, CInt(ms.Length))

                    Dim fs As New IO.FileStream(strDestinationFile, IO.FileMode.Create)
                    fs.Write(fileBytes, 0, CInt(fileBytes.Length))

                    fs.Close()
                Catch ex As System.Exception
                    MsgBox("Error copying attachment file", MsgBoxStyle.Exclamation)
                    Exit Sub
                End Try

                MsgBox("Success, attachment file copied OK", MsgBoxStyle.Exclamation)

            End If

            Dim oMailItem As Microsoft.Office.Interop.Outlook.MailItem
            Dim oOutlook As New Microsoft.Office.Interop.Outlook.Application
            oMailItem = oOutlook.CreateItemFromTemplate(strDestinationFile)
            MsgBox("received date : " + oMailItem.ReceivedTime.ToString + vbCrLf +
                "sender : " + oMailItem.SenderEmailAddress.ToString + vbCrLf +
                "sent date : " + oMailItem.SentOn.ToString)
        End If

        TextBox1.Text = strDestinationFile

    End Sub

    Friend Function RemoveIllegalChar(ByVal StringToCheck As String) As String
        '=======================================================================
        'purpose: Eliminate characters that are not allowed in file/folder name
        '=======================================================================
        Dim sIllegal As String = "\,/,:,*,?,<,>,|,~," & Chr(34)
        Dim arIllegal() As String = Split(sIllegal, ",")
        Dim sReturn As String = ""
        'Dim strString2 As String

        'sReturn = StringToCheck
        'Remove all characters above 127 ascii and place result imto sReturn
        For Each c As Char In StringToCheck
            sReturn = sReturn & IIf(Asc(c) > 127, "_", c)
        Next

        For i = 0 To arIllegal.Length - 1
            sReturn = Replace(sReturn, arIllegal(i), "_")
        Next

        'Remove leading spaces and return
        Return sReturn.TrimStart
    End Function

    'Private Async Function GetTokenAsync() As Task(Of String)
    '    Try
    '        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

    '        Using client As New HttpClient()
    '            client.Timeout = TimeSpan.FromSeconds(30)

    '            Dim loginData = New With {
    '                .username = "ykk@eroglugiyim.com",
    '                .password = "€r0Glu2019!"
    '            }

    '            Dim json = JsonConvert.SerializeObject(loginData)
    '            Dim content = New StringContent(json, Encoding.UTF8, "application/json")

    '            Dim response = Await client.PostAsync(LoginUrl, content)
    '            response.EnsureSuccessStatusCode()
    '            Return Await response.Content.ReadAsStringAsync()
    '        End Using

    '    Catch ex As SystemException
    '        Console.WriteLine("Token alınırken hata oluştu: " & ex.Message)
    '        If ex.InnerException IsNot Nothing Then
    '            Console.WriteLine("Detay: " & ex.InnerException.Message)
    '        End If
    '    End Try
    '    Return String.Empty
    'End Function

    'Private Async Function GetOrderDataAsync(token As String) As Task(Of String)
    '    Try
    '        SPMAyar()

    '        Using client As New HttpClient()
    '            client.DefaultRequestHeaders.Add("Authorization", "Bearer " & token)
    '            client.Timeout = TimeSpan.FromSeconds(30)

    '            Dim encodedContract = Uri.EscapeDataString("W25-PS26 PROTO REQUEST EROGLU")
    '            Dim url = GetOrderUrl & "?purchaseContractNos=" & encodedContract

    '            Dim response = Await client.GetAsync(url)
    '            response.EnsureSuccessStatusCode()
    '            Return Await response.Content.ReadAsStringAsync()
    '        End Using

    '    Catch ex As SystemException
    '        Console.WriteLine("Sipariş verileri alınırken hata oluştu: " & ex.Message)
    '        If ex.InnerException IsNot Nothing Then
    '            Console.WriteLine("Detay: " & ex.InnerException.Message)
    '        End If
    '    End Try
    '    Return String.Empty
    'End Function


    'Private Sub SPMAyar()
    '    Try
    '        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3  ' DirectCast(3072, System.Net.SecurityProtocolType)

    '        ServicePointManager.ServerCertificateValidationCallback =
    '        Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors)

    '            If certificate Is Nothing Then
    '                Return False
    '            End If

    '            Dim expectedThumbprint As String = "684CD5721BD21B73D5FE722F870C605B3662583A"

    '            Try
    '                Console.WriteLine("Sertifika doğrulama başladı")
    '                Dim cert As New X509Certificate2(certificate)
    '                Dim receivedThumbprint As String = cert.Thumbprint.Replace(" ", "").ToUpper()
    '                Dim expectedThumbprintClean As String = expectedThumbprint.Replace(" ", "").ToUpper()

    '                Console.WriteLine("Sertifika Detayları:")
    '                Console.WriteLine($"Subject: {certificate.Subject}")
    '                Console.WriteLine($"Issuer: {certificate.Issuer}")
    '                Console.WriteLine($"Alınan Thumbprint: {receivedThumbprint}")
    '                Console.WriteLine($"Beklenen Thumbprint: {expectedThumbprintClean}")

    '                If receivedThumbprint = expectedThumbprintClean Then
    '                    Console.WriteLine("Sertifika doğrulama başarılı")
    '                    Return True
    '                Else
    '                    Console.WriteLine($"Sertifika doğrulama hatası:")
    '                    Console.WriteLine($"Beklenen: {expectedThumbprintClean}")
    '                    Console.WriteLine($"Alınan: {receivedThumbprint}")
    '                    Return False
    '                End If

    '            Catch ex As SystemException
    '                Console.WriteLine("Sertifika doğrulama hatası: " & ex.Message)
    '                Return False
    '            End Try
    '        End Function

    '    Catch ex As SystemException
    '        Console.WriteLine("hatası: " & ex.Message)
    '    End Try
    'End Sub

End Class
