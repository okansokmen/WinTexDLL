Imports System
Imports System.Configuration
Imports System.IO
Imports System.Data
Imports Npgsql
Imports System.Threading.Tasks.TaskExtensions
Imports Microsoft.VisualBasic

Public Class NormalAktarimClass

    Public SLLU As String = ConfigurationManager.AppSettings("SLLU")       '  Kaynak Veritabanı  Where Şartı
    Public SLLG As String = ConfigurationManager.AppSettings("SLLG")       '  Kaynak Veritabanı  Where Şartı  0 olanlar
    Public SLLG13 As String = ConfigurationManager.AppSettings("SLLG13")   '  Kaynak Veritabanı  Where Şartı 0 ve 3 olanlar
    Public NSrg As String = ConfigurationManager.AppSettings("NSrg")       '  Veritabanı1 deki Log dosyasını Dönen değerde hata yoksa update sorgusu
    Public HSrg As String = ConfigurationManager.AppSettings("HSrg")       '  Veritabanı1 deki Log dosyasını Dönen değerde hata varsa update sorgusu
    Public DSrg As String = ConfigurationManager.AppSettings("DSrg")       '  Veritabanı1 deki Log dosyasını xp_op = 9 yapma sorgusu

    Public VTB1 As String = ConfigurationManager.AppSettings("VTB1")       '  Kaynak Veritabanı için Veritabanı Adı
    Public SRV1 As String = ConfigurationManager.AppSettings("SRV1")       '  Kaynak Veritabanı için Sunucu Adı-Adresi
    Public VTB2 As String = ConfigurationManager.AppSettings("VTB2")       '  Hedef Veritabanı için Veritabanı Adı
    Public SRV2 As String = ConfigurationManager.AppSettings("SRV2")       '  Hedef Veritabanı için Sunucu Adı-Adresi

    Public OD01 As String = ConfigurationManager.AppSettings("OD01")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 01
    Public OD02 As String = ConfigurationManager.AppSettings("OD02")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 02
    Public OD03 As String = ConfigurationManager.AppSettings("OD03")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 03
    Public OD04 As String = ConfigurationManager.AppSettings("OD04")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 04
    Public OD05 As String = ConfigurationManager.AppSettings("OD05")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 05
    Public nl As Integer = 0

    Public cAlanIcerik1 As String = ConfigurationManager.AppSettings("AlanIcerik1")      '  Null düzeltme
    Public cAlanDeger1 As String = ConfigurationManager.AppSettings("AlanDeger1")        '  Null düzeltme
    Public cAlanIcerik2 As String = ConfigurationManager.AppSettings("AlanIcerik2")      '  Null düzeltme
    Public cAlanDeger2 As String = ConfigurationManager.AppSettings("AlanDeger2")        '  Null düzeltme
    Public cAlanIcerik3 As String = ConfigurationManager.AppSettings("AlanIcerik3")      '  Null düzeltme
    Public cAlanDeger3 As String = ConfigurationManager.AppSettings("AlanDeger3")        '  Null düzeltme
    Public cAlanIcerik4 As String = ConfigurationManager.AppSettings("AlanIcerik4")      '  Null düzeltme
    Public cAlanDeger4 As String = ConfigurationManager.AppSettings("AlanDeger4")        '  Null düzeltme
    Public cAlanIcerik5 As String = ConfigurationManager.AppSettings("AlanIcerik5")      '  Null düzeltme
    Public cAlanDeger5 As String = ConfigurationManager.AppSettings("AlanDeger5")        '  Null düzeltme
    Public cAlanIcerik6 As String = ConfigurationManager.AppSettings("AlanIcerik6")      '  Null düzeltme
    Public cAlanDeger6 As String = ConfigurationManager.AppSettings("AlanDeger6")        '  Null düzeltme
    Public cAlanIcerik7 As String = ConfigurationManager.AppSettings("AlanIcerik7")      '  Null düzeltme
    Public cAlanDeger7 As String = ConfigurationManager.AppSettings("AlanDeger7")        '  Null düzeltme
    Public cAlanIcerik8 As String = ConfigurationManager.AppSettings("AlanIcerik8")      '  Null düzeltme
    Public cAlanDeger8 As String = ConfigurationManager.AppSettings("AlanDeger8")        '  Null düzeltme
    Public cAlanIcerik9 As String = ConfigurationManager.AppSettings("AlanIcerik9")      '  Null düzeltme
    Public cAlanDeger9 As String = ConfigurationManager.AppSettings("AlanDeger9")        '  Null düzeltme
    Public cAlanIcerik10 As String = ConfigurationManager.AppSettings("AlanIcerik10")    '  Null düzeltme
    Public cAlanDeger10 As String = ConfigurationManager.AppSettings("AlanDeger10")      '  Null düzeltme

    Public USR1 As String = "uyum"
    Public USR2 As String = "uyum"
    Public PSW1 As String = "12345"
    Public PSW2 As String = "12345"
    Public M As Double = 0

    Public Sub init()

        Dim oKaynak As PostgreClass
        Dim oHedef As PostgreClass

        Dim cErrorMessage As String = ""
        Dim oDataTable As DataTable
        Dim oRow As DataRow
        Dim CiftTirnak As String = ControlChars.Quote
        Dim cMsj As String = ""
        Dim cSorgu As String = ""

        Dim aDelimiterChars() As Char = {System.Convert.ToChar(";"), ControlChars.Tab}
        Dim nRow As Integer = 0

        Dim Kmt As String = ""
        Dim Typ As String = ""
        Dim Id As String = ""
        Dim tbln As String = ""
        Dim Fldn As String = ""
        Dim text As String = ""
        Dim opId As String = ""
        Dim Oprs As String = ""
        Dim words() As String
        Dim word As String = ""

        Dim nm As Integer = 0
        Dim c As Integer = 0
        Dim kk0 As String = ""
        Dim kk1 As String = ""
        Dim kk2 As String = ""
        Dim kk3 As String = ""

        Dim w2 As String = ""
        Dim uz1 As Integer = 0
        Dim uz2 As Integer = 0

        Try
            CreateLog(cServiceName, "Normal aktarım BAŞLADI")

            oKaynak = New PostgreClass(SRV1, VTB1, USR1, PSW1)
            oHedef = New PostgreClass(SRV2, VTB2, USR2, PSW2)

            cErrorMessage = ""
            If Not oKaynak.PGExecuteOpenCloseConnection(SLLU, cErrorMessage) Then
                CreateLog(cServiceName, "Normal aktarım hata var. SQL : " + SLLU + vbCrLf +
                                        "Hata : " + cErrorMessage)
                Exit Sub
            End If

            cErrorMessage = ""
            oDataTable = oKaynak.PGSelectOpenCloseConnection(SLLG, cErrorMessage)
            If oDataTable Is Nothing Then
                CreateLog(cServiceName, "Normal aktarım hata var. SQL : " + SLLG + vbCrLf +
                                        "Hata : " + cErrorMessage)
                Exit Sub
            End If

            For Each oRow In oDataTable.Rows

                Kmt = ""
                Typ = ""

                Id = oRow.Item("ID").ToString
                tbln = oRow.Item("TABLE_NAME").ToString
                Fldn = oRow.Item("TABLE_FIED_M").ToString
                text = oRow.Item("OP_DATA").ToString
                opId = oRow.Item("OP_ID").ToString
                Oprs = oRow.Item("OP").ToString

                If OD01.Trim <> "" Then text = text.Replace(OD01, "")
                If OD02.Trim <> "" Then text = text.Replace(OD02, "")
                If OD03.Trim <> "" Then text = text.Replace(OD03, "")
                If OD04.Trim <> "" Then text = text.Replace(OD04, "")
                If OD05.Trim <> "" Then text = text.Replace(OD05, "")

                text = text.Replace(": " + CiftTirnak + CiftTirnak, ": " + CiftTirnak + "_" + CiftTirnak)
                text = text.Replace(CiftTirnak + ":", ";")
                text = text.Replace("{" + CiftTirnak, ";")
                text = text.Replace(", " + CiftTirnak, ";")
                text = text.Replace(" " + CiftTirnak, ";")
                text = text.Replace("}", "")
                text = text.Replace("'", "")
                text = text.Replace(CiftTirnak, "")

                words = text.Split(aDelimiterChars)

                Select Case Oprs
                    Case "D"
                        Kmt = "Delete from " + tbln + " where " + Fldn + "=" + opId + ";"
                        Typ = "Delete Command"

                    Case "I"
                        Typ = "Insert Command"
                        nm = 0
                        c = 0
                        kk1 = ""
                        kk2 = ""
                        kk3 = "INSERT INTO " + tbln + " ("

                        For Each word In words
                            If word <> "" And word <> "." And word <> ":" Then
                                w2 = word
                                If w2.Substring(0, 1) = ":" Then w2 = word.Substring(1, (word.Length - 1))

                                c = c + 1
                                Select Case c
                                    Case 1
                                        If (w2 = "amt_net" And tbln = "hrmt_payroll") Then
                                            nm = 1
                                            kk1 = kk1 + w2 + ", zz_rodenen, "
                                        Else
                                            kk1 = kk1 + w2 + ","
                                        End If
                                    Case 2
                                        w2 = w2.Replace(" ", "")
                                        If (tbln = "hrmt_payroll" And nm = 1) Then
                                            kk2 = kk2 + "'" + w2 + "','" + w2 + "',"
                                            nm = 0
                                        ElseIf (w2 = "null") Then
                                            kk2 = kk2 + w2 + ","
                                        Else
                                            kk2 = kk2 + "'" + w2 + "',"
                                        End If
                                        c = 0
                                End Select
                            End If
                        Next
                        If (kk1.Length > 2 And kk2.Length > 2) Then
                            uz1 = kk1.Length - 1
                            uz2 = kk2.Length - 1
                            Kmt = kk3 + kk1.Substring(0, uz1) + ")  VALUES (" + kk2.Substring(0, uz2) + ");"
                        End If

                    Case "U"
                        Typ = "Update Command"
                        c = 0
                        kk0 = ""
                        kk1 = ""
                        kk2 = ""
                        kk3 = "UPDATE " + tbln + " set "
                        Kmt = ""
                        nl = 0

                        For Each word In words
                            If (word <> "" And word <> "." And word <> ":") Then
                                w2 = word
                                If (w2.Substring((word.Length - 1), 1) = ".") Then w2 = word.Substring(0, (word.Length - 1))

                                c = c + 1
                                Select Case c
                                    Case 1
                                        kk1 = w2
                                    Case 2
                                        w2 = w2.Replace(" ", "")
                                        kk2 = w2

                                        If (w2 = "null" Or w2 = "") Then
                                            kk2 = ""
                                            If (kk1.IndexOf("_date") > -1) Then kk2 = "1.01.0001"
                                            If (kk1.IndexOf("_id") > -1) Then kk2 = "0"
                                            If (kk1.IndexOf("record") > -1) Then kk2 = "0"
                                            If (kk1.IndexOf("source") > -1) Then kk2 = "0"
                                            If (kk1.IndexOf("serial") > -1) Then kk2 = "0"
                                            If (kk1.IndexOf("zz_expense_") > -1) Then kk2 = "0"
                                            If (kk1.IndexOf("voucher_no") > -1) Then kk2 = "0"

                                            If cAlanIcerik1.Trim <> "" And cAlanDeger1.Trim <> "" Then
                                                If (kk1.IndexOf(cAlanIcerik1.Trim) > -1) Then kk2 = cAlanDeger1.Trim
                                            End If
                                            If cAlanIcerik2.Trim <> "" And cAlanDeger2.Trim <> "" Then
                                                If (kk1.IndexOf(cAlanIcerik2.Trim) > -1) Then kk2 = cAlanDeger2.Trim
                                            End If
                                            If cAlanIcerik3.Trim <> "" And cAlanDeger3.Trim <> "" Then
                                                If (kk1.IndexOf(cAlanIcerik3.Trim) > -1) Then kk2 = cAlanDeger3.Trim
                                            End If
                                            If cAlanIcerik4.Trim <> "" And cAlanDeger4.Trim <> "" Then
                                                If (kk1.IndexOf(cAlanIcerik4.Trim) > -1) Then kk2 = cAlanDeger4.Trim
                                            End If
                                            If cAlanIcerik5.Trim <> "" And cAlanDeger5.Trim <> "" Then
                                                If (kk1.IndexOf(cAlanIcerik5.Trim) > -1) Then kk2 = cAlanDeger5.Trim
                                            End If
                                            If cAlanIcerik6.Trim <> "" And cAlanDeger6.Trim <> "" Then
                                                If (kk1.IndexOf(cAlanIcerik6.Trim) > -1) Then kk2 = cAlanDeger6.Trim
                                            End If
                                            If cAlanIcerik7.Trim <> "" And cAlanDeger7.Trim <> "" Then
                                                If (kk1.IndexOf(cAlanIcerik7.Trim) > -1) Then kk2 = cAlanDeger7.Trim
                                            End If
                                            If cAlanIcerik8.Trim <> "" And cAlanDeger8.Trim <> "" Then
                                                If (kk1.IndexOf(cAlanIcerik8.Trim) > -1) Then kk2 = cAlanDeger8.Trim
                                            End If
                                            If cAlanIcerik9.Trim <> "" And cAlanDeger9.Trim <> "" Then
                                                If (kk1.IndexOf(cAlanIcerik9.Trim) > -1) Then kk2 = cAlanDeger9.Trim
                                            End If
                                            If cAlanIcerik10.Trim <> "" And cAlanDeger10.Trim <> "" Then
                                                If (kk1.IndexOf(cAlanIcerik10.Trim) > -1) Then kk2 = cAlanDeger10.Trim
                                            End If
                                        ElseIf (w2 = "") Then
                                            kk2 = ""
                                        End If

                                        If kk0 = "" Then
                                            If (kk1 = "note_large") Then
                                                kk2 = "-"
                                            ElseIf (kk1 = "amt_net" And tbln = "hrmt_payroll") Then
                                                kk0 = kk1 + "='" + kk2 + "', zz_rodenen='" + kk2 + "'"
                                            Else
                                                kk0 = kk1 + "='" + kk2 + "'"
                                            End If
                                        Else
                                            If (kk1 = "note_large") Then
                                                kk2 = "-"
                                            ElseIf (kk1 = "amt_net" And tbln = "hrmt_payroll") Then
                                                kk0 = kk0 + "," + kk1 + "='" + kk2 + "', zz_rodenen ='" + kk2 + "'"
                                            Else
                                                kk0 = kk0 + "," + kk1 + "='" + kk2 + "'"
                                            End If
                                        End If

                                        c = 0
                                End Select
                            End If
                        Next
                        Kmt = kk3 + kk0 + " where " + Fldn + "=" + opId + ";"
                End Select

                cErrorMessage = ""
                If oHedef.PGExecuteOpenCloseConnection(Kmt, cErrorMessage) Then
                    cMsj = "OK"
                    cSorgu = NSrg + "xp_error = '" + cMsj + "' where id = " + Id
                Else
                    cMsj = cErrorMessage
                    If cMsj = "" Then cMsj = "Bilinmeyen Hata"
                    cSorgu = HSrg + "xp_error = '" + Mid(cMsj, 1, 100).Trim + "' where id = " + Id
                End If

                cErrorMessage = ""
                oKaynak.PGExecuteOpenCloseConnection(cSorgu, cErrorMessage)
                If cErrorMessage.Trim <> "" Then
                    CreateLog(cServiceName, "Normal aktarım hata var. SQL : " + cSorgu + vbCrLf +
                                            "Hata : " + cErrorMessage)
                Else
                    'CreateLog(cServiceName, cSorgu)
                End If
            Next

            CreateLog(cServiceName, "Normal aktarım TAMAMLANDI")

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub
End Class
