Imports System
Imports System.Configuration
Imports System.IO
Imports System.Windows.Forms
Imports System.Data
Imports Npgsql
Imports System.Threading.Tasks.TaskExtensions
Imports Microsoft.VisualBasic

Module Module1
    Const connectionString As String = "Server={0};User Id={2};Password={3};Database={1};ApplicationName=;"


    Public PrmData As String = ""
    Public si As Integer = 0

    Public SLLU As String = ConfigurationSettings.AppSettings("SLLU")       '  Kaynak Veritabanı  Where Şartı
    Public SLLG As String = ConfigurationSettings.AppSettings("SLLG")       '  Kaynak Veritabanı  Where Şartı  0 olanlar
    Public SLLG13 As String = ConfigurationSettings.AppSettings("SLLG13")   '  Kaynak Veritabanı  Where Şartı 0 ve 3 olanlar
    Public NSrg As String = ConfigurationSettings.AppSettings("NSrg")       '  Veritabanı1 deki Log dosyasını Dönen değerde hata yoksa update sorgusu
    Public HSrg As String = ConfigurationSettings.AppSettings("HSrg")       '  Veritabanı1 deki Log dosyasını Dönen değerde hata varsa update sorgusu
    Public DSrg As String = ConfigurationSettings.AppSettings("DSrg")       '  Veritabanı1 deki Log dosyasını xp_op = 9 yapma sorgusu

    Public VTB1 As String = ConfigurationSettings.AppSettings("VTB1")       '  Kaynak Veritabanı için Veritabanı Adı
    Public SRV1 As String = ConfigurationSettings.AppSettings("SRV1")       '  Kaynak Veritabanı için Sunucu Adı-Adresi
    Public VTB2 As String = ConfigurationSettings.AppSettings("VTB2")       '  Hedef Veritabanı için Veritabanı Adı
    Public SRV2 As String = ConfigurationSettings.AppSettings("SRV2")       '  Hedef Veritabanı için Sunucu Adı-Adresi

    Public OD01 As String = ConfigurationSettings.AppSettings("OD01")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 01
    Public OD02 As String = ConfigurationSettings.AppSettings("OD02")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 02
    Public OD03 As String = ConfigurationSettings.AppSettings("OD03")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 03
    Public OD04 As String = ConfigurationSettings.AppSettings("OD04")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 04
    Public OD05 As String = ConfigurationSettings.AppSettings("OD05")       '  UPDATE KOMUT İÇİNDE GELEN VERİLERDE TEMİZLENECEK ÖZEL DURUMLAR 05
    Public nl As Integer = 0

    Public USR1 As String = "uyum"
    Public USR2 As String = "uyum"
    Public PSW1 As String = "12345"
    Public PSW2 As String = "12345"
    Public M As Double = 0
    Public Mn As Double = Convert.ToDouble(ConfigurationSettings.AppSettings("Mn"))
    Public Hr As Double = Convert.ToDouble(ConfigurationSettings.AppSettings("Hr"))
    Public pName As String = ""

    Private Sub UyumMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Function PGSelect(cSQL As String) As DataTable

        Try
            Dim cConnectionString As String = String.Format(connectionString, SRV1, VTB1, USR1, PSW1)
            Dim oConnection As NpgsqlConnection
            Dim oCommand As NpgsqlCommand
            Dim oDataTable As New DataTable

            Cursor.Current = Cursors.WaitCursor

            oConnection = New NpgsqlConnection(cConnectionString)
            oConnection.Open()
            oCommand = New NpgsqlCommand(cSQL, oConnection)
            oDataTable.Load(oCommand.ExecuteReader())
            PGSelect = oDataTable
            oConnection.Close()
            oConnection.Dispose()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Function

    Private Sub PGExecute(cSQL As String)

        Try
            Dim cConnectionString As String = String.Format(connectionString, SRV1, VTB1, USR1, PSW1)
            Dim oConnection As NpgsqlConnection
            Dim oCommand As NpgsqlCommand

            Cursor.Current = Cursors.WaitCursor

            oConnection = New NpgsqlConnection(cConnectionString)
            oConnection.Open()
            oCommand = New NpgsqlCommand(cSQL, oConnection)
            oCommand.ExecuteNonQuery()
            oConnection.Close()
            oConnection.Dispose()

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Private Sub GetData1(cSQL As String)
        Try
            C1TrueDBGrid1.DataSource = Nothing
            C1TrueDBGrid1.Rows.Clear()
            C1TrueDBGrid1.DataSource = PGSelect(cSQL)

        Catch ex As Exception
            ErrDisp(ex, Me.Name)
        End Try
    End Sub

    'Private Sub C1Button1_Click(sender As Object, e As EventArgs) Handles C1Button1.Click
    '    Try

    '        'PGExecute(SLLU)
    '        GetData1(SLLG)

    '    Catch ex As Exception
    '        ErrDisp(ex, Me.Name)
    '    End Try
    'End Sub


    'Private Sub C1Button2_Click(sender As Object, e As EventArgs)
    '    Me.Close()
    'End Sub

End Module
