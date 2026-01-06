Imports Newtonsoft.Json

Module utilSiparis

    ' ---------- 1) RAW DTOs (match the incoming JSON exactly) ----------
    Public Class RawRoot
        Public Property order_no As String
        Public Property [date] As String
        Public Property season As String
        Public Property production_group As String
        Public Property production_main_group As String
        Public Property gender As String
        Public Property barcode_item_nr As String
        Public Property agent_name As String
        Public Property agent_code As String
        Public Property preorder_po As String
        Public Property currency_code As String
        Public Property unit_price As Double?
        Public Property total_amount As Double?
        Public Property fabric_content As List(Of String)
        Public Property address As String
        Public Property supplier As String
        Public Property supplier_code As String
        Public Property style_no As String
        Public Property delivery_date_1 As String
        Public Property delivery_date_2 As String
        Public Property terms_of_del As String
        Public Property shipment_mode As String
        Public Property from As String
        Public Property [to] As String
        Public Property terms_of_payment As String
        Public Property foreign_amount As Double?
        Public Property domestic_amount As Double?
        Public Property supplier_address As String
        Public Property color_no As String
        Public Property color As String
        Public Property xassortments As List(Of RawAssortment)
    End Class

    Public Class RawAssortment
        Public Property Destination_code As String
        Public Property Destination_description As String
        Public Property Delivery_date As String
        Public Property assortment_code As String
        Public Property payment_term As String
        Public Property column_headers_row As List(Of String)
        Public Property data_rows As List(Of RawRow)
        Public Property column_totals_row As RawTotals
    End Class

    Public Class RawRow
        Public Property size_name As String
        Public Property quantities As List(Of Double)
        Public Property row_total As Double
    End Class

    Public Class RawTotals
        Public Property total_label As String
        Public Property total_quantities As List(Of Double)
        Public Property total_row_total As Double
    End Class

    ' ---------- 2) TARGET STRUCTURES (tables become multi-d arrays) ----------
    Public Class OrderDoc
        Public Property order_no As String
        Public Property [date] As String
        Public Property season As String
        Public Property production_group As String
        Public Property production_main_group As String
        Public Property gender As String
        Public Property barcode_item_nr As String
        Public Property agent_name As String
        Public Property agent_code As String
        Public Property preorder_po As String
        Public Property currency_code As String
        Public Property unit_price As Double
        Public Property total_amount As Double
        Public Property fabric_content As String()
        Public Property address As String
        Public Property supplier As String
        Public Property supplier_code As String
        Public Property style_no As String
        Public Property delivery_date_1 As String
        Public Property delivery_date_2 As String
        Public Property terms_of_del As String
        Public Property shipment_mode As String
        Public Property from_ As String
        Public Property to_ As String
        Public Property terms_of_payment As String
        Public Property foreign_amount As Double
        Public Property domestic_amount As Double
        Public Property supplier_address As String
        Public Property color_no As String
        Public Property color As String

        ' Each assortment's table is flattened into multi-d arrays + companion vectors
        Public Property xassortments As AssortmentTable()
    End Class

    Public Class AssortmentTable
        Public Property Destination_code As String
        Public Property Destination_description As String
        Public Property Delivery_date As String
        Public Property assortment_code As String
        Public Property payment_term As String

        ' Table parts
        Public Property ColumnHeaders As String()             ' 1-D header vector
        Public Property SizeNames As String()                 ' 1-D row labels
        Public Property Quantities As Double(,)               ' 2-D numeric matrix [row, col]
        Public Property RowTotals As Double()                 ' 1-D row totals

        ' Totals row
        Public Property TotalLabel As String
        Public Property ColumnTotals As Double()              ' 1-D column totals
        Public Property TotalRowTotal As Double
    End Class

    ' ---------- 3) CONVERTER ----------
    Public Function OrderDeserializeAndConvert(json As String) As OrderDoc

        Dim raw = JsonConvert.DeserializeObject(Of RawRoot)(json)

        If IsNothing(raw) Then
            MsgBox("Invalid JSON input")
            Return Nothing
        End If

        Dim result As New OrderDoc With {
        .order_no = raw.order_no,
        .date = raw.date,
        .season = raw.season,
        .production_group = raw.production_group,
        .production_main_group = raw.production_main_group,
        .gender = raw.gender,
        .barcode_item_nr = raw.barcode_item_nr,
        .agent_name = raw.agent_name,
        .agent_code = raw.agent_code,
        .preorder_po = raw.preorder_po,
        .currency_code = raw.currency_code,
        .unit_price = If(raw.unit_price, 0),
        .total_amount = If(raw.total_amount, 0),
        .address = raw.address,
        .supplier = raw.supplier,
        .supplier_code = raw.supplier_code,
        .style_no = raw.style_no,
        .delivery_date_1 = raw.delivery_date_1,
        .delivery_date_2 = raw.delivery_date_2,
        .terms_of_del = raw.terms_of_del,
        .shipment_mode = raw.shipment_mode,
        .from_ = raw.from,
        .to_ = raw.to,
        .terms_of_payment = raw.terms_of_payment,
        .foreign_amount = If(raw.foreign_amount, 0),
        .domestic_amount = If(raw.domestic_amount, 0),
        .supplier_address = raw.supplier_address,
        .color_no = raw.color_no,
        .color = raw.color
    }
        ' Handle fabric content
        If raw.fabric_content IsNot Nothing Then
            result.fabric_content = raw.fabric_content.ToArray()
        Else
            result.fabric_content = New String() {}
        End If

        Dim assList As New List(Of AssortmentTable)
        If raw.xassortments IsNot Nothing Then
            For Each a In raw.xassortments
                assList.Add(ConvertAssortment(a))
            Next
        End If
        result.xassortments = assList.ToArray()

        Return result
    End Function

    Private Function ConvertAssortment(a As RawAssortment) As AssortmentTable

        Dim headers = If(a.column_headers_row, New List(Of String))
        Dim rows = If(a.data_rows, New List(Of RawRow))

        ' Determine rectangular dimensions safely
        Dim rowCount = rows.Count
        Dim colCount = 0
        If headers IsNot Nothing AndAlso headers.Count > 0 Then
            colCount = headers.Count
        ElseIf rowCount > 0 Then
            colCount = rows.Max(Function(r) If(r.quantities IsNot Nothing, r.quantities.Count, 0))
        End If

        ' Allocate arrays
        Dim q As Double(,) = If(colCount > 0 AndAlso rowCount > 0, New Double(rowCount - 1, colCount - 1) {}, New Double(-1, -1) {})
        Dim sizeNames As String() = If(rowCount > 0, New String(rowCount - 1) {}, Array.Empty(Of String)())
        Dim rowTotals As Double() = If(rowCount > 0, New Double(rowCount - 1) {}, Array.Empty(Of Double)())

        ' Fill arrays, padding ragged rows with 0
        For i = 0 To rowCount - 1
            Dim r = rows(i)
            sizeNames(i) = r.size_name
            rowTotals(i) = r.row_total
            Dim cells = If(r.quantities, New List(Of Double))
            For j = 0 To colCount - 1
                q(i, j) = If(j < cells.Count, cells(j), 0R)
            Next
        Next

        ' Totals
        Dim totals = a.column_totals_row
        Dim colTotals As Double() = If(totals IsNot Nothing AndAlso totals.total_quantities IsNot Nothing,
                                   totals.total_quantities.ToArray(),
                                   If(colCount > 0, Enumerable.Repeat(0R, colCount).ToArray(), Array.Empty(Of Double)()))

        Dim result As New AssortmentTable With {
        .Destination_code = a.Destination_code,
        .Destination_description = a.Destination_description,
        .Delivery_date = a.Delivery_date,
        .assortment_code = a.assortment_code,
        .payment_term = a.payment_term,
        .Quantities = q,
        .SizeNames = sizeNames,
        .RowTotals = rowTotals,
        .TotalLabel = If(totals IsNot Nothing, totals.total_label, Nothing),
        .ColumnTotals = colTotals,
        .TotalRowTotal = If(totals IsNot Nothing, totals.total_row_total, 0R)
    }
        ' Handle column headers
        If headers IsNot Nothing Then
            result.ColumnHeaders = headers.ToArray()
        Else
            result.ColumnHeaders = New String() {}
        End If

        Return result
    End Function

    ' ---------- 4) USAGE EXAMPLE ----------
    ' Dim json As String = File.ReadAllText("input.json")
    ' Dim doc As OrderDoc = OrderConverter.DeserializeAndConvert(json)
    ' Now: doc.xassortments(k).Quantities is a 2-D Double(,) matrix ready for numeric work.

End Module
