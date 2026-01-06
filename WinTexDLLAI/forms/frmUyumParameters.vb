Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class frmUyumParameters
    Private Sub frmUyumParameters_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Dim Connyage As SqlConnection

            Connyage = OpenConn()

            ' F firması
            TextEdit1.Text = GetSysParConnected("URLGeneralB2BService", Connyage, "http://192.168.1.3:8060/WebService/B2B/GeneralB2BService.asmx")
            TextEdit2.Text = GetSysParConnected("URLErogluProduction", Connyage, "http://192.168.1.3:8060/webservice/eroglu/ErogluProduction.asmx")
            TextEdit3.Text = GetSysParConnected("URLGeneralSenfoniService", Connyage, "http://192.168.1.3:8060/WebService/General/GeneralSenfoniService.asmx")
            TextEdit4.Text = GetSysParConnected("URLUyumSaveWebService", Connyage, "http://192.168.1.3:8060/WebService/ERP/UyumSaveWebService.asmx")
            TextEdit5.Text = GetSysParConnected("URLUyumGetWebService", Connyage, "http://192.168.1.3:8060/WebService/ERP/UyumGetWebService.asmx")
            TextEdit13.Text = GetSysParConnected("URLUyumWebService", Connyage, "http://192.168.1.3:8060/WebService/UyumWebService.asmx")

            TextEdit6.Text = GetSysParConnected("UyumKullaniciAdi", Connyage, "webservis")
            TextEdit7.Text = GetSysParConnected("UyumKullaniciSifresi", Connyage, "Uyum123*")

            TextEdit8.Text = GetSysParConnected("UyumCoCode", Connyage, "10")
            TextEdit9.Text = GetSysParConnected("UyumBranchCode", Connyage, "10")
            TextEdit10.Text = GetSysParConnected("UyumWhouseCode", Connyage, "10")
            TextEdit11.Text = GetSysParConnected("UyumWorkCenterCode", Connyage, "10")
            TextEdit12.Text = GetSysParConnected("UyumIsEmriTipiCode", Connyage, "STANDART")
            TextEdit14.Text = GetSysParConnected("UyumIhracatDepo", Connyage, "MERKEZ")
            TextEdit54.Text = GetSysParConnected("UyumYerelDoviz", Connyage, "TL")

            TextEdit55.Text = GetSysParConnected("UyumFServer", Connyage, "192.168.1.3")
            TextEdit56.Text = GetSysParConnected("UyumFDatabase", Connyage, "uyumsoft")
            TextEdit57.Text = GetSysParConnected("UyumFUsername", Connyage, "uyum")
            TextEdit58.Text = GetSysParConnected("UyumFPassword", Connyage, "12345")

            If GetSysParConnected("UyumStokKartOnline", Connyage, "0") = "1" Then
                CheckEdit1.Checked = True
            Else
                CheckEdit1.Checked = False
            End If

            ' R firması 
            TextEdit26.Text = GetSysParConnected("URLGeneralB2BService2", Connyage, "http://192.168.1.3:8060/WebService/B2B/GeneralB2BService.asmx")
            TextEdit25.Text = GetSysParConnected("URLErogluProduction2", Connyage, "http://192.168.1.3:8060/webservice/eroglu/ErogluProduction.asmx")
            TextEdit24.Text = GetSysParConnected("URLGeneralSenfoniService2", Connyage, "http://192.168.1.3:8060/WebService/General/GeneralSenfoniService.asmx")
            TextEdit23.Text = GetSysParConnected("URLUyumSaveWebService2", Connyage, "http://192.168.1.3:8060/WebService/ERP/UyumSaveWebService.asmx")
            TextEdit22.Text = GetSysParConnected("URLUyumGetWebService2", Connyage, "http://192.168.1.3:8060/WebService/ERP/UyumGetWebService.asmx")
            TextEdit15.Text = GetSysParConnected("URLUyumWebService2", Connyage, "http://192.168.1.3:8060/WebService/UyumWebService.asmx")

            TextEdit21.Text = GetSysParConnected("UyumKullaniciAdi2", Connyage, "webservis")
            TextEdit20.Text = GetSysParConnected("UyumKullaniciSifresi2", Connyage, "Uyum123*")

            TextEdit19.Text = GetSysParConnected("UyumCoCode2", Connyage, "10")
            TextEdit18.Text = GetSysParConnected("UyumBranchCode2", Connyage, "10")
            TextEdit17.Text = GetSysParConnected("UyumWhouseCode2", Connyage, "10")
            TextEdit16.Text = GetSysParConnected("UyumWorkCenterCode2", Connyage, "10")
            TextEdit28.Text = GetSysParConnected("UyumIsEmriTipiCode2", Connyage, "STANDART")
            TextEdit27.Text = GetSysParConnected("UyumIhracatDepo2", Connyage, "MERKEZ")

            TextEdit29.Text = GetSysParConnected("UyumCoCode3", Connyage, "10")
            TextEdit30.Text = GetSysParConnected("UyumBranchCode3", Connyage, "10")
            TextEdit53.Text = GetSysParConnected("UyumIhracatFirmaId", Connyage, "1045360")

            ' genel 
            If GetSysParConnected("WinTexDllLog", Connyage, "0") = "1" Then
                CheckEdit2.Checked = True
            Else
                CheckEdit2.Checked = False
            End If

            If GetSysParConnected("WinTexDllError", Connyage, "0") = "1" Then
                CheckEdit3.Checked = True
            Else
                CheckEdit3.Checked = False
            End If

            If GetSysParConnected("UyumDTFirmaAktif", Connyage, "0") = "1" Then
                CheckEdit4.Checked = True
            Else
                CheckEdit4.Checked = False
            End If

            ' stok hareket kodları
            ' giriş
            TextEdit31.Text = GetSysParConnected("uyumgithalat", Connyage, "1296")          ' İTHALAT İRSALİYESİ
            TextEdit32.Text = GetSysParConnected("uyumgalis", Connyage, "1292")             ' ALIŞ İRSALİYESİ
            TextEdit33.Text = GetSysParConnected("uyumgsatisiade", Connyage, "1302")        ' SATIŞ İADE İRSALİYESİ
            TextEdit34.Text = GetSysParConnected("uyumgyigdsatisiade", Connyage, "2879")    ' YURT İÇİ GRUP DIŞI SATIŞ İADE İRSALİYESİ
            TextEdit35.Text = GetSysParConnected("uyumgyigisatisiade", Connyage, "2878")    ' YURT İÇİ GRUP İÇİ SATIŞ İADE İRSALİYESİ
            TextEdit36.Text = GetSysParConnected("uyumgstok", Connyage, "1330")             ' STOK GİRİŞİ
            ' çıkış
            TextEdit37.Text = GetSysParConnected("uyumcydgdsatis", Connyage, "2866")        ' YURT DIŞI GRUP DIŞI SATIŞ İRSALİYESİ
            TextEdit38.Text = GetSysParConnected("uyumcydgisatis", Connyage, "2865")        ' YURT DIŞI GRUP İÇİ SATIŞ İRSALİYESİ
            TextEdit39.Text = GetSysParConnected("uyumcyigdsatis", Connyage, "2864")        ' YURT İÇİ GRUP DIŞI SATIŞ İRSALİYESİ
            TextEdit40.Text = GetSysParConnected("uyumcyigisatis", Connyage, "2863")        ' YURT İÇİ GRUP İÇİ SATIŞ İRSALİYESİ
            TextEdit41.Text = GetSysParConnected("uyumcihracat", Connyage, "1303")          ' İHRACAT İRSALİYESİ
            TextEdit42.Text = GetSysParConnected("uyumcsatis", Connyage, "1297")            ' SATIŞ İRSALİYESİ
            TextEdit43.Text = GetSysParConnected("uyumcyialisiade", Connyage, "1308")       ' YURT İÇİ ALIŞ İADE İRSALİYESİ
            TextEdit44.Text = GetSysParConnected("uyumcstok", Connyage, "1331")             ' STOK ÇIKIŞI

            TextEdit59.Text = GetSysParConnected("UyumTransDepo", Connyage)                 ' ihracat satış çıkışında mamülün giriş yapacağı transfer deposu
            TextEdit60.Text = GetSysParConnected("UyumTransFirma", Connyage)                ' ihracat satış çıkışında mamülün giriş yapacağı firma
            TextEdit63.Text = GetSysParConnected("UyumiSevkiyatTransfer", Connyage)         ' istanbul transfer hareket kodu
            TextEdit64.Text = GetSysParConnected("UyumaSevkiyatTransfer", Connyage)         ' aksaray transfer hareket kodu

            ' sipariş kodları
            ' satınalma siparişleri
            TextEdit45.Text = GetSysParConnected("uyumsassatinalma", Connyage, "1319")      ' SATINALMA SİPARİŞİ
            TextEdit46.Text = GetSysParConnected("uyumsasithalat", Connyage, "1322")        ' İTHALAT SİPARİŞİ

            ' satış siparişleri
            TextEdit47.Text = GetSysParConnected("uyumsssatis", Connyage, "1313")           ' SATIŞ SİPARİŞİ
            TextEdit48.Text = GetSysParConnected("uyumssihracat", Connyage, "1315")         ' İHRACAT SATIŞ SİPARİŞİ
            TextEdit49.Text = GetSysParConnected("uyumssyigisatis", Connyage, "2880")       ' YURT İÇİ GRUP İÇİ SATIŞ SİPARİŞİ
            TextEdit50.Text = GetSysParConnected("uyumssyigdsatis", Connyage, "2881")       ' YURT İÇİ GRUP DIŞI SATIŞ SİPARİŞİ
            TextEdit51.Text = GetSysParConnected("uyumssydgisatis", Connyage, "2882")       ' YURT DIŞI GRUP İÇİ SATIŞ SİPARİŞİ
            TextEdit52.Text = GetSysParConnected("uyumssydgdsatis", Connyage, "2883")       ' YURT DIŞI GRUP DIŞI SATIŞ SİPARİŞİ

            ' üretim e-irsaliyesi
            TextEdit61.Text = GetSysParConnected("uyumufisdepogiris", Connyage)             ' Genel giriş deposu
            TextEdit62.Text = GetSysParConnected("uyumufisdepocikis", Connyage)             ' Genel çıkış deposu

            Connyage.Close()

        Catch ex As Exception
            ErrDisp("frmUyumParameters_Load", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Try
            Me.Close()

        Catch ex As Exception
            ErrDisp("SimpleButton2_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click

        Try
            Dim Connyage As SqlConnection = Nothing

            Connyage = OpenConn()

            ' F (fiili) firma
            SetSysParConnected("URLGeneralB2BService", SQLWriteString(TextEdit1.Text, 200), Connyage)
            SetSysParConnected("URLErogluProduction", SQLWriteString(TextEdit2.Text, 200), Connyage)
            SetSysParConnected("URLGeneralSenfoniService", SQLWriteString(TextEdit3.Text, 200), Connyage)
            SetSysParConnected("URLUyumSaveWebService", SQLWriteString(TextEdit4.Text, 200), Connyage)
            SetSysParConnected("URLUyumGetWebService", SQLWriteString(TextEdit5.Text, 200), Connyage)
            SetSysParConnected("URLUyumWebService", SQLWriteString(TextEdit13.Text, 200), Connyage)

            SetSysParConnected("UyumKullaniciAdi", SQLWriteString(TextEdit6.Text, 200), Connyage)
            SetSysParConnected("UyumKullaniciSifresi", SQLWriteString(TextEdit7.Text, 200), Connyage)

            SetSysParConnected("UyumCoCode", SQLWriteString(TextEdit8.Text, 200), Connyage)
            SetSysParConnected("UyumBranchCode", SQLWriteString(TextEdit9.Text, 200), Connyage)
            SetSysParConnected("UyumWhouseCode", SQLWriteString(TextEdit10.Text, 200), Connyage)
            SetSysParConnected("UyumWorkCenterCode", SQLWriteString(TextEdit11.Text, 200), Connyage)
            SetSysParConnected("UyumIsEmriTipiCode", SQLWriteString(TextEdit12.Text, 200), Connyage)
            SetSysParConnected("UyumIhracatDepo", SQLWriteString(TextEdit14.Text, 200), Connyage)
            SetSysParConnected("UyumYerelDoviz", SQLWriteString(TextEdit54.Text, 3), Connyage)

            SetSysParConnected("UyumFServer", SQLWriteString(TextEdit55.Text, 200), Connyage)
            SetSysParConnected("UyumFDatabase", SQLWriteString(TextEdit56.Text, 200), Connyage)
            SetSysParConnected("UyumFUsername", SQLWriteString(TextEdit57.Text, 200), Connyage)
            SetSysParConnected("UyumFPassword", SQLWriteString(TextEdit58.Text, 200), Connyage)

            SetSysParConnected("UyumTransDepo", SQLWriteString(TextEdit59.Text, 200), Connyage)
            SetSysParConnected("UyumTransFirma", SQLWriteString(TextEdit60.Text, 200), Connyage)
            SetSysParConnected("UyumiSevkiyatTransfer", SQLWriteString(TextEdit63.Text, 200), Connyage)
            SetSysParConnected("UyumaSevkiyatTransfer", SQLWriteString(TextEdit64.Text, 200), Connyage)

            If CheckEdit1.Checked Then
                SetSysParConnected("UyumStokKartOnline", "1", Connyage)
            Else
                SetSysParConnected("UyumStokKartOnline", "0", Connyage)
            End If

            ' R (resmi) firma
            SetSysParConnected("URLGeneralB2BService2", SQLWriteString(TextEdit26.Text, 200), Connyage)
            SetSysParConnected("URLErogluProduction2", SQLWriteString(TextEdit25.Text, 200), Connyage)
            SetSysParConnected("URLGeneralSenfoniService2", SQLWriteString(TextEdit24.Text, 200), Connyage)
            SetSysParConnected("URLUyumSaveWebService2", SQLWriteString(TextEdit23.Text, 200), Connyage)
            SetSysParConnected("URLUyumGetWebService2", SQLWriteString(TextEdit22.Text, 200), Connyage)
            SetSysParConnected("URLUyumWebService2", SQLWriteString(TextEdit15.Text, 200), Connyage)

            SetSysParConnected("UyumKullaniciAdi2", SQLWriteString(TextEdit21.Text, 200), Connyage)
            SetSysParConnected("UyumKullaniciSifresi2", SQLWriteString(TextEdit20.Text, 200), Connyage)

            SetSysParConnected("UyumCoCode2", SQLWriteString(TextEdit19.Text, 200), Connyage)
            SetSysParConnected("UyumBranchCode2", SQLWriteString(TextEdit18.Text, 200), Connyage)
            SetSysParConnected("UyumWhouseCode2", SQLWriteString(TextEdit17.Text, 200), Connyage)
            SetSysParConnected("UyumWorkCenterCode2", SQLWriteString(TextEdit16.Text, 200), Connyage)
            SetSysParConnected("UyumIsEmriTipiCode2", SQLWriteString(TextEdit28.Text, 200), Connyage)
            SetSysParConnected("UyumIhracatDepo2", SQLWriteString(TextEdit27.Text, 200), Connyage)

            SetSysParConnected("UyumCoCode3", SQLWriteString(TextEdit29.Text, 200), Connyage)
            SetSysParConnected("UyumBranchCode3", SQLWriteString(TextEdit30.Text, 200), Connyage)
            SetSysParConnected("UyumIhracatFirmaId", SQLWriteString(TextEdit53.Text, 200), Connyage)

            ' genel
            If CheckEdit2.Checked Then
                SetSysParConnected("WinTexDllLog", "1", Connyage)
            Else
                SetSysParConnected("WinTexDllLog", "0", Connyage)
            End If

            If CheckEdit3.Checked Then
                SetSysParConnected("WinTexDllError", "1", Connyage)
            Else
                SetSysParConnected("WinTexDllError", "0", Connyage)
            End If

            If CheckEdit4.Checked Then
                SetSysParConnected("UyumDTFirmaAktif", "1", Connyage)
            Else
                SetSysParConnected("UyumDTFirmaAktif", "0", Connyage)
            End If

            ' stok hareket kodları
            ' giriş
            SetSysParConnected("uyumgithalat", SQLWriteString(TextEdit31.Text, 200), Connyage)
            SetSysParConnected("uyumgalis", SQLWriteString(TextEdit32.Text, 200), Connyage)
            SetSysParConnected("uyumgsatisiade", SQLWriteString(TextEdit33.Text, 200), Connyage)
            SetSysParConnected("uyumgyigdsatisiade", SQLWriteString(TextEdit34.Text, 200), Connyage)
            SetSysParConnected("uyumgyigisatisiade", SQLWriteString(TextEdit35.Text, 200), Connyage)
            SetSysParConnected("uyumgstok", SQLWriteString(TextEdit36.Text, 200), Connyage)
            ' çıkış
            SetSysParConnected("uyumcydgdsatis", SQLWriteString(TextEdit37.Text, 200), Connyage)
            SetSysParConnected("uyumcydgisatis", SQLWriteString(TextEdit38.Text, 200), Connyage)
            SetSysParConnected("uyumcyigdsatis", SQLWriteString(TextEdit39.Text, 200), Connyage)
            SetSysParConnected("uyumcyigisatis", SQLWriteString(TextEdit40.Text, 200), Connyage)
            SetSysParConnected("uyumcihracat", SQLWriteString(TextEdit41.Text, 200), Connyage)
            SetSysParConnected("uyumcsatis", SQLWriteString(TextEdit42.Text, 200), Connyage)
            SetSysParConnected("uyumcyialisiade", SQLWriteString(TextEdit43.Text, 200), Connyage)
            SetSysParConnected("uyumcstok", SQLWriteString(TextEdit44.Text, 200), Connyage)

            ' sipariş kodları
            ' satınalma siparişleri
            SetSysParConnected("uyumsassatinalma", SQLWriteString(TextEdit45.Text, 200), Connyage)
            SetSysParConnected("uyumsasithalat", SQLWriteString(TextEdit46.Text, 200), Connyage)

            ' satış siparişleri
            SetSysParConnected("uyumsssatis", SQLWriteString(TextEdit47.Text, 200), Connyage)
            SetSysParConnected("uyumssihracat", SQLWriteString(TextEdit48.Text, 200), Connyage)
            SetSysParConnected("uyumssyigisatis", SQLWriteString(TextEdit49.Text, 200), Connyage)
            SetSysParConnected("uyumssyigdsatis", SQLWriteString(TextEdit50.Text, 200), Connyage)
            SetSysParConnected("uyumssydgisatis", SQLWriteString(TextEdit51.Text, 200), Connyage)
            SetSysParConnected("uyumssydgdsatis", SQLWriteString(TextEdit52.Text, 200), Connyage)

            ' üretim e-irsaliyesi
            SetSysParConnected("uyumufisdepogiris", SQLWriteString(TextEdit61.Text, 200), Connyage) ' Genel giriş deposu
            SetSysParConnected("uyumufisdepocikis", SQLWriteString(TextEdit62.Text, 200), Connyage) ' Genel çıkış deposu

            Connyage.Close()

            Me.Close()

        Catch ex As Exception
            ErrDisp("SimpleButton1_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Try
            Process.Start("iexplore.exe", TextEdit1.Text)
        Catch ex As Exception
            ErrDisp("SimpleButton_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        Try
            Process.Start("iexplore.exe", TextEdit2.Text)
        Catch ex As Exception
            ErrDisp("SimpleButton_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Try
            Process.Start("iexplore.exe", TextEdit3.Text)
        Catch ex As Exception
            ErrDisp("SimpleButton_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        Try
            Process.Start("iexplore.exe", TextEdit4.Text)
        Catch ex As Exception
            ErrDisp("SimpleButton_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        Try
            Process.Start("iexplore.exe", TextEdit5.Text)
        Catch ex As Exception
            ErrDisp("SimpleButton_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        Try
            Process.Start("iexplore.exe", TextEdit13.Text)
        Catch ex As Exception
            ErrDisp("SimpleButton_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton14_Click(sender As Object, e As EventArgs) Handles SimpleButton14.Click
        Try
            Process.Start("iexplore.exe", TextEdit26.Text)
        Catch ex As Exception
            ErrDisp("SimpleButton_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton13_Click(sender As Object, e As EventArgs) Handles SimpleButton13.Click
        Try
            Process.Start("iexplore.exe", TextEdit25.Text)
        Catch ex As Exception
            ErrDisp("SimpleButton_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton12_Click(sender As Object, e As EventArgs) Handles SimpleButton12.Click
        Try
            Process.Start("iexplore.exe", TextEdit24.Text)
        Catch ex As Exception
            ErrDisp("SimpleButton_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton11_Click(sender As Object, e As EventArgs) Handles SimpleButton11.Click
        Try
            Process.Start("iexplore.exe", TextEdit23.Text)
        Catch ex As Exception
            ErrDisp("SimpleButton_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton10_Click(sender As Object, e As EventArgs) Handles SimpleButton10.Click
        Try
            Process.Start("iexplore.exe", TextEdit22.Text)
        Catch ex As Exception
            ErrDisp("SimpleButton_Click", Me.Name,,, ex)
        End Try
    End Sub

    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        Try
            Process.Start("iexplore.exe", TextEdit15.Text)
        Catch ex As Exception
            ErrDisp("SimpleButton_Click", Me.Name,,, ex)
        End Try
    End Sub
End Class