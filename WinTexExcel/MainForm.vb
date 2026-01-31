Imports System.IO
Imports Services
Imports Models

Public Class MainForm
    Private _selectedFilePath As String = ""

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Form yükleme işlemleri
        Dim args As String() = Environment.GetCommandLineArgs()

        If args.Length = 1 Then
            oConnection.cServer = "MONSTER\MSSQLSERVER2"
            oConnection.cDatabase = "oxxo"
            oConnection.cUser = "sa"
            oConnection.cPassword = "Hayabusa1024"
        Else
            oConnection.cServer = args(1).ToString.Trim
            oConnection.cDatabase = args(2).ToString.Trim
            oConnection.cUser = args(3).ToString.Trim
            oConnection.cPassword = args(4).ToString.Trim
        End If

        oConnection.cConnStr = "Data Source=" + oConnection.cServer + ";" +
                                   "Initial Catalog=" + oConnection.cDatabase + ";" +
                                   "uid=" + oConnection.cUser + ";" +
                                   "pwd=" + oConnection.cPassword + ""

        txtConnectionString.Text = oConnection.cConnStr
        txtConnectionString.Enabled = False

        AddLog("Uygulama başlatıldı.")
        AddLog("Lütfen Excel dosyasını seçin ve veritabanı bağlantı dizesini girin.")
        AddLog("Desteklenen formatlar: .xlsx ve .csv")
    End Sub

    ''' <summary>
    ''' Dosya seçme dialog açar
    ''' </summary>
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "CSV Dosyaları (*.csv)|*.csv|Excel Dosyaları (*.xlsx)|*.xlsx|Tüm Dosyalar (*.*)|*.*"
            openFileDialog.Title = "Excel/CSV Dosyası Seçin"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                _selectedFilePath = openFileDialog.FileName
                txtFilePath.Text = _selectedFilePath
                AddLog($"Dosya seçildi: {Path.GetFileName(_selectedFilePath)}")
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Veritabanı bağlantısını test eder
    ''' </summary>
    Private Sub btnTestConnection_Click(sender As Object, e As EventArgs) Handles btnTestConnection.Click
        Dim connectionString As String = oConnection.cConnStr

        If String.IsNullOrEmpty(connectionString) Then
            MessageBox.Show("Lütfen bağlantı dizesi girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            AddLog("Veritabanı bağlantısı test ediliyor...")
            Dim dbService As New DatabaseService(connectionString)

            If dbService.TestConnection() Then
                AddLog("✓ Veritabanı bağlantısı başarılı!")
                MessageBox.Show("Veritabanı bağlantısı başarılı!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                AddLog("✗ Veritabanı bağlantısı başarısız!")
                MessageBox.Show("Veritabanı bağlantısı başarısız. Lütfen bağlantı dizesini kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            AddLog($"✗ Bağlantı hatası: {ex.Message}")
            MessageBox.Show($"Hata: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Excel dosyasını okuyup veritabanına aktarır
    ''' </summary>
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        ' Validasyon
        If String.IsNullOrEmpty(_selectedFilePath) Then
            MessageBox.Show("Lütfen bir Excel dosyası seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not File.Exists(_selectedFilePath) Then
            MessageBox.Show("Seçilen dosya bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim connectionString As String = txtConnectionString.Text.Trim()
        If String.IsNullOrEmpty(connectionString) Then
            MessageBox.Show("Lütfen bağlantı dizesi girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            ' Butonları devre dışı bırak
            btnImport.Enabled = False
            btnBrowse.Enabled = False
            btnTestConnection.Enabled = False
            ProgressBar1.Value = 0

            Dim fileName As String = Path.GetFileName(_selectedFilePath)
            AddLog($"")
            AddLog($"========== İçe Aktarma İşlemi Başladı ==========")
            AddLog($"Dosya: {fileName}")
            AddLog($"Tarih: {DateTime.Now:yyyy-MM-dd HH:mm:ss}")

            ' Excel dosyasını oku
            AddLog("Excel dosyası okunuyor...")
            ProgressBar1.Value = 25
            Application.DoEvents()

            Dim orders As OrderRecord() = ExcelReaderService.ReadExcelFile(_selectedFilePath)
            AddLog($"✓ {orders.Length} kayıt okundu.")
            ProgressBar1.Value = 50
            Application.DoEvents()

            If orders.Length = 0 Then
                AddLog("✗ Dosyada kayıt bulunamadı!")
                MessageBox.Show("Dosyada kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Veritabanına ekle
            AddLog("Veritabanına bağlanılıyor...")
            Dim dbService As New DatabaseService(connectionString)
            ProgressBar1.Value = 75
            Application.DoEvents()

            AddLog("Kayıtlar veritabanına ekleniyor...")
            Dim insertedCount As Integer = dbService.InsertOrders(orders, fileName)

            ProgressBar1.Value = 100
            Application.DoEvents()

            AddLog($"✓ {insertedCount} kayıt başarıyla veritabanına eklendi.")
            AddLog($"========== İçe Aktarma İşlemi Tamamlandı ==========")
            AddLog("")

            MessageBox.Show($"{insertedCount} kayıt başarıyla veritabanına eklendi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' AddToWintex()

        Catch ex As Exception
            AddLog($"✗ HATA: {ex.Message}")
            AddLog($"Detay: {ex.InnerException?.Message}")
            MessageBox.Show($"Hata: {ex.Message}", "İçe Aktarma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Butonları etkinleştir
            btnImport.Enabled = True
            btnBrowse.Enabled = True
            btnTestConnection.Enabled = True
        End Try
    End Sub

    ''' <summary>
    ''' Durum listesine log mesajı ekler
    ''' </summary>
    Private Sub AddLog(message As String)
        lstStatus.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}")
        lstStatus.TopIndex = lstStatus.Items.Count - 1
        Application.DoEvents()
    End Sub

    Private Sub AddToWintex()

        Dim cTasarimNo As String = ""
        Dim cKod As String = ""
        Dim cAciklama As String = ""
        Dim cSiparisNo As String = ""
        Dim cUretici As String = ""
        Dim nFiyat As Double = 0
        Dim nBeden(9) As Double
        Dim nCnt As Integer = 0
        Dim cAsorti As String = ""
        Dim nPos As Integer = 0
        Dim cEtiket As String = ""
        Dim cSallantiEtiket As String = ""
        Dim cYikama As String = ""
        Dim cOdemeAciklama As String = ""
        Dim nOdemeVade As Double = 0
        Dim cOKTakibi As String = ""
        Dim cOdemSekli As String = ""
        Dim dKumasTermini As Date = #1/1/1950#

        Dim oSQL1 As New SQLServerClass
        Dim oSQL2 As New SQLServerClass

        oSQL1.OpenConn()
        oSQL2.OpenConn()

        oSQL1.cSQLQuery = "SELECT DISTINCT CustomerNo, description " +
                        " FROM jccorder with (NOLOCK) " +
                        " WHERE SourceFileName = '" + SQLWriteString(_selectedFilePath) + "' " +
                        " ORDER BY CustomerNo, description "

        oSQL1.GetSQLReader()

        Do While oSQL1.oReader.Read

            ' her müşteri ve model için wintex e sipariş ekle    

            oSQL2.cSQLQuery = "select tasarimno, odemeaciklama, kumastermini, odemevade, " +
                            " ok1, uretici1, odemesekli1, fiyat1,  " +
                            " ok2, uretici2, odemesekli2, fiyat2, " +
                            " ok3, uretici3, odemesekli3, fiyat3, " +
                            " ok4, uretici4, odemesekli4, fiyat4, " +
                            " ok5, uretici5, odemesekli5, fiyat5, " +
                            " ok6, uretici6, odemesekli6, fiyat6, " +
                            " ok7, uretici7, odemesekli7, fiyat7, " +
                            " ok8, uretici8, odemesekli8, fiyat8, " +
                            " ok9, uretici9, odemesekli9, fiyat9 " +
                            " from tasarim with (NOLOCK) " +
                            " where kolartikel = '" + SQLWriteString(oSQL1.oReader("description").ToString) + "' " +
                            " ORDER BY sirano DESC "

            oSQL2.GetSQLReader()

            If oSQL2.oReader.Read Then

                cTasarimNo = oSQL2.SQLReadString("tasarimno")

                If oSQL2.SQLReadString("ok1") = "E" Then
                    cUretici = oSQL2.SQLReadString("uretici1")
                    cOdemSekli = oSQL2.SQLReadString("odemesekli1")
                    nFiyat = oSQL2.SQLReadDouble("fiyat1")
                End If
                If oSQL2.SQLReadString("ok2") = "E" Then
                    cUretici = oSQL2.SQLReadString("uretici2")
                    cOdemSekli = oSQL2.SQLReadString("odemesekli2")
                    nFiyat = oSQL2.SQLReadDouble("fiyat2")
                End If
                If oSQL2.SQLReadString("ok3") = "E" Then
                    cUretici = oSQL2.SQLReadString("uretici3")
                    cOdemSekli = oSQL2.SQLReadString("odemesekli3")
                    nFiyat = oSQL2.SQLReadDouble("fiyat3")
                End If
                If oSQL2.SQLReadString("ok4") = "E" Then
                    cUretici = oSQL2.SQLReadString("uretici4")
                    cOdemSekli = oSQL2.SQLReadString("odemesekli4")
                    nFiyat = oSQL2.SQLReadDouble("fiyat4")
                End If
                If oSQL2.SQLReadString("ok5") = "E" Then
                    cUretici = oSQL2.SQLReadString("uretici5")
                    cOdemSekli = oSQL2.SQLReadString("odemesekli5")
                    nFiyat = oSQL2.SQLReadDouble("fiyat5")
                End If
                If oSQL2.SQLReadString("ok6") = "E" Then
                    cUretici = oSQL2.SQLReadString("uretici6")
                    cOdemSekli = oSQL2.SQLReadString("odemesekli6")
                    nFiyat = oSQL2.SQLReadDouble("fiyat6")
                End If
                If oSQL2.SQLReadString("ok7") = "E" Then
                    cUretici = oSQL2.SQLReadString("uretici7")
                    cOdemSekli = oSQL2.SQLReadString("odemesekli7")
                    nFiyat = oSQL2.SQLReadDouble("fiyat7")
                End If
                If oSQL2.SQLReadString("ok8") = "E" Then
                    cUretici = oSQL2.SQLReadString("uretici8")
                    cOdemSekli = oSQL2.SQLReadString("odemesekli8")
                    nFiyat = oSQL2.SQLReadDouble("fiyat8")
                End If
                If oSQL2.SQLReadString("ok9") = "E" Then
                    cUretici = oSQL2.SQLReadString("uretici9")
                    cOdemSekli = oSQL2.SQLReadString("odemesekli9")
                    nFiyat = oSQL2.SQLReadDouble("fiyat9")
                End If

                If cOdemSekli = "" Then
                    cOdemSekli = oSQL2.SQLReadString("odemeaciklama")
                End If

                dKumasTermini = oSQL2.SQLReadDate("kumastermini")
                nOdemeVade = oSQL2.SQLReadDouble("odemevade")

                If nFiyat = 0 Then
                    If oSQL2.SQLReadDouble("fiyat1") <> 0 Then
                        cUretici = oSQL2.SQLReadString("uretici1")
                        nFiyat = oSQL2.SQLReadDouble("fiyat1")
                    End If
                End If

            End If
            oSQL2.oReader.Close()

        Loop
        oSQL1.oReader.Close()

        oSQL1.CloseConn()
        oSQL2.CloseConn()


    End Sub

End Class
