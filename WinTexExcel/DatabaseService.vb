Imports System.Data
Imports System.Data.SqlClient
Imports DocumentFormat.OpenXml.Office2010.Excel
Imports Models

Public Class DatabaseService

    Private _connectionString As String
    Private oSQL As SQLServerClass
    ''' <summary>
    ''' DatabaseService yapıcısı
    ''' </summary>
    ''' <param name="connectionString">SQL Server bağlantı dizesi</param>
    Public Sub New(connectionString As String)
        _connectionString = connectionString
    End Sub

    ''' <summary>
    ''' Veritabanı bağlantısını test eder
    ''' </summary>
    ''' <returns>Bağlantı başarılı ise True, aksi halde False</returns>
    Public Function TestConnection() As Boolean
        Try
            Using connection As New SqlConnection(_connectionString)
                connection.Open()
                Return True
            End Using
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Veritabanı bağlantı hatası: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' OrderRecord dizisini veritabanına ekler
    ''' </summary>
    ''' <param name="orders">Eklenecek OrderRecord dizisi</param>
    ''' <param name="sourceFileName">Excel dosyasının adı</param>
    ''' <returns>Eklenen kayıt sayısı</returns>
    Public Function InsertOrders(orders As OrderRecord(), sourceFileName As String) As Integer
        Dim insertedCount As Integer = 0

        Try
            oSQL = New SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "delete from jccorder where SourceFileName = '" & SQLWriteString(sourceFileName) & "'"
            oSQL.SQLExecute()

            Using oSQL.oConnection

                For Each order In orders
                    Try
                        Dim command As New SqlCommand(GetInsertQuery(), oSQL.oConnection)

                        ' Parametreleri ekle
                        command.Parameters.AddWithValue("@SourceFileName", SQLWriteString(sourceFileName))
                        command.Parameters.AddWithValue("@OrderNo", If(String.IsNullOrEmpty(order.OrderNo), DBNull.Value, order.OrderNo))
                        command.Parameters.AddWithValue("@ArticleNo", If(String.IsNullOrEmpty(order.ArticleNo), DBNull.Value, order.ArticleNo))
                        command.Parameters.AddWithValue("@Description", If(String.IsNullOrEmpty(order.Description), DBNull.Value, order.Description))
                        command.Parameters.AddWithValue("@ColorNo", If(String.IsNullOrEmpty(order.ColorNo), DBNull.Value, order.ColorNo))
                        command.Parameters.AddWithValue("@CustomerNo", If(String.IsNullOrEmpty(order.CustomerNo), DBNull.Value, order.CustomerNo))
                        command.Parameters.AddWithValue("@CustomerName", If(String.IsNullOrEmpty(order.CustomerName), DBNull.Value, order.CustomerName))
                        command.Parameters.AddWithValue("@Size", If(String.IsNullOrEmpty(order.Size), DBNull.Value, order.Size))
                        command.Parameters.AddWithValue("@Quantity", order.Quantity)
                        command.Parameters.AddWithValue("@Country", If(String.IsNullOrEmpty(order.Country), DBNull.Value, order.Country))
                        command.Parameters.AddWithValue("@Zip", If(String.IsNullOrEmpty(order.Zip), DBNull.Value, order.Zip))
                        command.Parameters.AddWithValue("@City", If(String.IsNullOrEmpty(order.City), DBNull.Value, order.City))
                        command.Parameters.AddWithValue("@ColorDescription", If(String.IsNullOrEmpty(order.ColorDescription), DBNull.Value, order.ColorDescription))
                        command.Parameters.AddWithValue("@DeliveryDateFrom", If(order.DeliveryDateFrom = DateTime.MinValue, DBNull.Value, order.DeliveryDateFrom))
                        command.Parameters.AddWithValue("@DeliveryDateTo", If(order.DeliveryDateTo = DateTime.MinValue, DBNull.Value, order.DeliveryDateTo))
                        command.Parameters.AddWithValue("@EAN", If(String.IsNullOrEmpty(order.EAN), DBNull.Value, order.EAN))
                        command.Parameters.AddWithValue("@ShortDescription", If(String.IsNullOrEmpty(order.ShortDescription), DBNull.Value, order.ShortDescription))

                        command.ExecuteNonQuery()
                        insertedCount += 1
                    Catch ex As Exception
                        System.Diagnostics.Debug.WriteLine($"Kayıt eklenirken hata: {ex.Message}")
                        ' Hatalı kaydı atla ve devam et
                    End Try
                Next
            End Using

            oSQL.CloseConn()

        Catch ex As Exception
            Throw New Exception($"Veritabanına veri eklenirken hata oluştu: {ex.Message}", ex)
        End Try

        Return insertedCount
    End Function

    ''' <summary>
    ''' INSERT SQL sorgusunu oluşturur
    ''' </summary>
    Private Function GetInsertQuery() As String
        Return "INSERT INTO [dbo].[jccorder] (" &
                   "[SourceFileName], [OrderNo], [ArticleNo], [Description], [ColorNo], " &
                   "[CustomerNo], [CustomerName], [Size], [Quantity], [Country], " &
                   "[Zip], [City], [ColorDescription], [DeliveryDateFrom], [DeliveryDateTo], " &
                   "[EAN], [ShortDescription]) " &
                   "VALUES (" &
                   "@SourceFileName, @OrderNo, @ArticleNo, @Description, @ColorNo, " &
                   "@CustomerNo, @CustomerName, @Size, @Quantity, @Country, " &
                   "@Zip, @City, @ColorDescription, @DeliveryDateFrom, @DeliveryDateTo, " &
                   "@EAN, @ShortDescription)"
    End Function

End Class
