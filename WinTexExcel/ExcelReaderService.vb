Imports System.IO
Imports System.Globalization
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet

''' <summary>
''' Excel/CSV dosyalarını okumak ve OrderRecord dizisine dönüştürmek için servis
''' XLSX dosyalarını doğrudan destekler
''' </summary>
Public Class ExcelReaderService

    ''' <summary>
    ''' Excel dosyasını okuyup OrderRecord dizisine dönüştürür
    ''' XLSX ve CSV dosyalarını destekler
    ''' </summary>
    ''' <param name="filePath">Excel dosyasının tam yolu</param>
    ''' <returns>OrderRecord dizisi</returns>
    Public Shared Function ReadExcelFile(filePath As String) As OrderRecord()
        Dim fileExtension As String = Path.GetExtension(filePath).ToLower()

        If fileExtension = ".xlsx" Then
            Return ReadExcelXlsx(filePath)
        ElseIf fileExtension = ".csv" Then
            Return ReadExcelCsv(filePath)
        Else
            Throw New Exception("Desteklenmeyen dosya formatı. Lütfen .xlsx veya .csv dosyası seçin.")
        End If
    End Function

    ''' <summary>
    ''' XLSX dosyasını okur
    ''' </summary>
    Private Shared Function ReadExcelXlsx(filePath As String) As OrderRecord()
        Dim orders As New List(Of OrderRecord)()

        Try
            ' OpenXML kütüphanesini kullanarak XLSX dosyasını aç
            Using document = SpreadsheetDocument.Open(filePath, False)
                Dim workbookPart = document.WorkbookPart
                Dim worksheetPart = workbookPart.WorksheetParts.First()
                Dim sheetData = worksheetPart.Worksheet.Elements(Of SheetData)().First()

                Dim rows = sheetData.Elements(Of Row)().ToList()

                If rows.Count = 0 Then
                    Throw New Exception("Excel dosyası boş veya geçersiz.")
                End If

                ' Başlık satırını parse et
                Dim headerRow = rows(0)
                Dim headers As New Dictionary(Of String, Integer)()
                Dim cellIndex As Integer = 0

                For Each cell In headerRow.Elements(Of Cell)()
                    Dim cellValue As String = GetCellValue(cell, workbookPart)
                    If Not String.IsNullOrWhiteSpace(cellValue) Then
                        headers(cellValue.Trim().ToLower()) = cellIndex
                    End If
                    cellIndex += 1
                Next

                ' Veri satırlarını oku
                For rowIndex As Integer = 1 To rows.Count - 1
                    Try
                        Dim row = rows(rowIndex)
                        Dim cells = row.Elements(Of Cell)().ToList()

                        Dim record As New OrderRecord With {
                            .OrderNo = GetCellValueByHeader(cells, headers, "order no", workbookPart),
                            .ArticleNo = GetCellValueByHeader(cells, headers, "article no", workbookPart),
                            .Description = GetCellValueByHeader(cells, headers, "description", workbookPart),
                            .ColorNo = GetCellValueByHeader(cells, headers, "color no", workbookPart),
                            .CustomerNo = GetCellValueByHeader(cells, headers, "customer no", workbookPart),
                            .CustomerName = GetCellValueByHeader(cells, headers, "customer name", workbookPart),
                            .Size = GetCellValueByHeader(cells, headers, "size", workbookPart),
                            .Quantity = GetCellIntValueByHeader(cells, headers, "quantity", workbookPart),
                            .Country = GetCellValueByHeader(cells, headers, "country", workbookPart),
                            .Zip = GetCellValueByHeader(cells, headers, "zip", workbookPart),
                            .City = GetCellValueByHeader(cells, headers, "city", workbookPart),
                            .ColorDescription = GetCellValueByHeader(cells, headers, "color description", workbookPart),
                            .DeliveryDateFrom = GetCellDateValueByHeader(cells, headers, "delivery date from", workbookPart),
                            .DeliveryDateTo = GetCellDateValueByHeader(cells, headers, "delivery date to", workbookPart),
                            .EAN = GetCellValueByHeader(cells, headers, "ean", workbookPart),
                            .ShortDescription = GetCellValueByHeader(cells, headers, "short description", workbookPart)
                            }

                        orders.Add(record)
                    Catch ex As Exception
                        System.Diagnostics.Debug.WriteLine($"Satır {rowIndex} parse hatası: {ex.Message}")
                    End Try
                Next
            End Using
        Catch ex As Exception
            Throw New Exception($"Excel dosyası okunurken hata oluştu: {ex.Message}", ex)
        End Try

        Return orders.ToArray()
    End Function

    ''' <summary>
    ''' CSV dosyasını okur
    ''' </summary>
    Private Shared Function ReadExcelCsv(filePath As String) As OrderRecord()
        Dim orders As New List(Of OrderRecord)()

        Try
            Using reader As New StreamReader(filePath, System.Text.Encoding.GetEncoding("ISO-8859-1"))
                Dim headerLine As String = reader.ReadLine()

                If headerLine Is Nothing Then
                    Throw New Exception("Excel dosyası boş veya geçersiz.")
                End If

                ' Başlık satırını parse et
                Dim headers As String() = headerLine.Split(";"c)
                Dim columnIndices As New Dictionary(Of String, Integer)()

                For i As Integer = 0 To headers.Length - 1
                    columnIndices(headers(i).Trim().ToLower()) = i
                Next

                ' Veri satırlarını oku
                Dim line As String
                While (InlineAssignHelper(line, reader.ReadLine())) IsNot Nothing
                    If String.IsNullOrWhiteSpace(line) Then
                        Continue While
                    End If

                    Dim values As String() = line.Split(";"c)

                    Try
                        Dim record As New OrderRecord With {
                            .OrderNo = GetValue(values, columnIndices, "order no"),
                            .ArticleNo = GetValue(values, columnIndices, "article no"),
                            .Description = GetValue(values, columnIndices, "description"),
                            .ColorNo = GetValue(values, columnIndices, "color no"),
                            .CustomerNo = GetValue(values, columnIndices, "customer no"),
                            .CustomerName = GetValue(values, columnIndices, "customer name"),
                            .Size = GetValue(values, columnIndices, "size"),
                            .Quantity = GetIntValue(values, columnIndices, "quantity"),
                            .Country = GetValue(values, columnIndices, "country"),
                            .Zip = GetValue(values, columnIndices, "zip"),
                            .City = GetValue(values, columnIndices, "city"),
                            .ColorDescription = GetValue(values, columnIndices, "color description"),
                            .DeliveryDateFrom = GetDateValue(values, columnIndices, "delivery date from"),
                            .DeliveryDateTo = GetDateValue(values, columnIndices, "delivery date to"),
                            .EAN = GetValue(values, columnIndices, "ean"),
                            .ShortDescription = GetValue(values, columnIndices, "short description")
                            }

                        orders.Add(record)
                    Catch ex As Exception
                        System.Diagnostics.Debug.WriteLine($"Satır parse hatası: {ex.Message}")
                    End Try
                End While
            End Using
        Catch ex As Exception
            Throw New Exception($"CSV dosyası okunurken hata oluştu: {ex.Message}", ex)
        End Try

        Return orders.ToArray()
    End Function

    ''' <summary>
    ''' OpenXML Cell'den değer alır
    ''' </summary>
    Private Shared Function GetCellValue(cell As Cell, workbookPart As WorkbookPart) As String
        If cell Is Nothing Then
            Return ""
        End If

        If cell.DataType IsNot Nothing AndAlso cell.DataType = CellValues.SharedString Then
            Try
                Dim stringTablePart = workbookPart.SharedStringTablePart
                If stringTablePart IsNot Nothing Then
                    Dim stringTable = stringTablePart.SharedStringTable
                    Dim index As Integer = Integer.Parse(cell.CellValue.Text)
                    Return stringTable.ElementAt(index).InnerText
                End If
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine($"SharedString okuma hatası: {ex.Message}")
                Return ""
            End Try
        End If

        Return If(cell.CellValue Is Nothing, "", cell.CellValue.Text)
    End Function

    ''' <summary>
    ''' OpenXML Cell'den tarih değeri alır (OLE Automation formatında)
    ''' </summary>
    Private Shared Function GetCellDateValue(cell As Cell, workbookPart As WorkbookPart) As DateTime
        If cell Is Nothing OrElse cell.CellValue Is Nothing Then
            Return DateTime.MinValue
        End If

        Try
            ' Eğer hücre SharedString ise, string olarak parse et
            If cell.DataType IsNot Nothing AndAlso cell.DataType = CellValues.SharedString Then
                Dim stringValue = GetCellValue(cell, workbookPart)
                Dim result As DateTime = DateTime.MinValue

                ' Türkçe tarih formatlarını dene
                Dim formats As String() = {"dd.MM.yyyy", "d.M.yyyy", "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd"}
                If DateTime.TryParseExact(stringValue, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, result) Then
                    Return result
                End If

                ' Genel parse dene
                If DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.None, result) Then
                    Return result
                End If

                Return DateTime.MinValue
            End If

            ' Excel tarih değeri (OLE Automation Date) olarak saklanmışsa
            Dim cellValue As String = cell.CellValue.Text
            Dim numericValue As Double = 0

            If Double.TryParse(cellValue, NumberStyles.Any, CultureInfo.InvariantCulture, numericValue) Then
                ' Excel tarihleri 1900-01-01'den itibaren gün sayısı olarak saklanır
                ' OLE Automation Date'e dönüştür
                Try
                    ' Excel'in tarih sistemi: 1 = 1900-01-01 (ama Excel'de 1900'ü artık yıl olarak sayar, hata!)
                    ' .NET'in DateTime.FromOADate bu hatayı düzeltir
                    Return DateTime.FromOADate(numericValue)
                Catch
                    Return DateTime.MinValue
                End Try
            End If

            ' String formatında tarih parse et
            Dim dateResult As DateTime = DateTime.MinValue
            Dim formats2 As String() = {"dd.MM.yyyy", "d.M.yyyy", "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd"}
            If DateTime.TryParseExact(cellValue, formats2, CultureInfo.InvariantCulture, DateTimeStyles.None, dateResult) Then
                Return dateResult
            End If

            ' Genel parse dene
            If DateTime.TryParse(cellValue, CultureInfo.InvariantCulture, DateTimeStyles.None, dateResult) Then
                Return dateResult
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Tarih parse hatası: {ex.Message}")
        End Try

        Return DateTime.MinValue
    End Function

    ''' <summary>
    ''' Başlık adına göre hücre değerini alır (XLSX)
    ''' </summary>
    Private Shared Function GetCellValueByHeader(cells As List(Of Cell), headers As Dictionary(Of String, Integer), headerName As String, workbookPart As WorkbookPart) As String
        Dim lowerHeaderName = headerName.ToLower()
        If headers.ContainsKey(lowerHeaderName) Then
            Dim cellIndex = headers(lowerHeaderName)
            If cellIndex < cells.Count Then
                Return GetCellValue(cells(cellIndex), workbookPart)
            End If
        End If
        Return ""
    End Function

    ''' <summary>
    ''' Başlık adına göre integer değerini alır (XLSX)
    ''' </summary>
    Private Shared Function GetCellIntValueByHeader(cells As List(Of Cell), headers As Dictionary(Of String, Integer), headerName As String, workbookPart As WorkbookPart) As Integer
        Dim value = GetCellValueByHeader(cells, headers, headerName, workbookPart)
        Dim result As Integer = 0
        If Integer.TryParse(value, result) Then
            Return result
        End If
        Return 0
    End Function

    ''' <summary>
    ''' Başlık adına göre tarih değerini alır (XLSX) - Güncellenmiş versiyon
    ''' </summary>
    Private Shared Function GetCellDateValueByHeader(cells As List(Of Cell), headers As Dictionary(Of String, Integer), headerName As String, workbookPart As WorkbookPart) As DateTime
        Dim lowerHeaderName = headerName.ToLower()
        If headers.ContainsKey(lowerHeaderName) Then
            Dim cellIndex = headers(lowerHeaderName)
            If cellIndex < cells.Count Then
                Return GetCellDateValue(cells(cellIndex), workbookPart)
            End If
        End If
        Return DateTime.MinValue
    End Function

    ''' <summary>
    ''' CSV dosyasından string değer alır
    ''' </summary>
    Private Shared Function GetValue(values As String(), columnIndices As Dictionary(Of String, Integer), columnName As String) As String
        Dim lowerColumnName = columnName.ToLower()
        If columnIndices.ContainsKey(lowerColumnName) AndAlso columnIndices(lowerColumnName) < values.Length Then
            Dim value As String = values(columnIndices(lowerColumnName)).Trim()
            Return If(String.IsNullOrWhiteSpace(value), "", value)
        End If
        Return ""
    End Function

    ''' <summary>
    ''' CSV dosyasından integer değer alır
    ''' </summary>
    Private Shared Function GetIntValue(values As String(), columnIndices As Dictionary(Of String, Integer), columnName As String) As Integer
        Dim value As String = GetValue(values, columnIndices, columnName)
        Dim result As Integer = 0
        If Integer.TryParse(value, result) Then
            Return result
        End If
        Return 0
    End Function

    ''' <summary>
    ''' CSV dosyasından tarih değeri alır
    ''' </summary>
    Private Shared Function GetDateValue(values As String(), columnIndices As Dictionary(Of String, Integer), columnName As String) As DateTime
        Dim value As String = GetValue(values, columnIndices, columnName)
        Dim result As DateTime = DateTime.MinValue

        ' Türkçe tarih formatlarını dene
        Dim formats As String() = {"dd.MM.yyyy", "d.M.yyyy", "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd"}
        If DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, result) Then
            Return result
        End If

        ' Genel parse dene
        If DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, result) Then
            Return result
        End If

        Return DateTime.MinValue
    End Function

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function
End Class

