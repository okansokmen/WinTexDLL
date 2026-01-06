Imports System.Globalization
Imports System.Text

Public Module PurchaseOrderRenderer

    Private Const TEMPLATE_VERSION As String = "2.0.0"

    Private Function Esc(value As Object) As String
        Try
            Dim s = If(value, "").ToString()
            s = s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")
            s = s.Replace("""", "&quot;").Replace("'", "&#39;")
            Return s
        Catch ex As Exception
            ErrDisp(ex.Message, "PurchaseOrderRenderer.Esc", , , ex)
            Return ""
        End Try
    End Function

    Private Function FmtNum(n As Decimal?) As String
        Try
            If Not n.HasValue Then Return "—"
            Return n.Value.ToString("#,##0.##", CultureInfo.InvariantCulture)
        Catch ex As Exception
            ErrDisp(ex.Message, "PurchaseOrderRenderer.FmtNum", , , ex)
            Return "—"
        End Try
    End Function

    Private Function FmtNum(n As Double) As String
        Try
            Return n.ToString("#,##0.##", CultureInfo.InvariantCulture)
        Catch ex As Exception
            ErrDisp(ex.Message, "PurchaseOrderRenderer.FmtNum", , , ex)
            Return "0"
        End Try
    End Function

    Private Function RenderAssortmentTable(a As AssortmentTable) As String
        Try
            Dim sb As New StringBuilder()

            ' Determine if row totals should be rendered (must have at least one non-zero row total)
            Dim hasRowTotals As Boolean = (a.RowTotals IsNot Nothing AndAlso HasMeaningfulRowTotals(a.RowTotals))

            ' Prepare header/column filtering and compute column sums to ensure footer numbers are all non-zero
            Dim headers As String() = a.ColumnHeaders
            Dim qtyCols As Integer = If(a.Quantities IsNot Nothing, a.Quantities.GetLength(1), 0)
            Dim qtyRows As Integer = If(a.Quantities IsNot Nothing, a.Quantities.GetLength(0), 0)
            Dim sizeRows As Integer = If(a.SizeNames IsNot Nothing, a.SizeNames.Length, 0)
            Dim rowCount As Integer = Math.Min(qtyRows, sizeRows)

            ' Compute column sums first
            Dim colSums As New List(Of Double)()
            If qtyCols > 0 AndAlso rowCount > 0 Then
                For j As Integer = 0 To qtyCols - 1
                    Dim s As Double = 0
                    For i As Integer = 0 To rowCount - 1
                        s += a.Quantities(i, j)
                    Next
                    colSums.Add(s)
                Next
            End If

            ' Build list of valid column indexes and labels
            Dim validColIndexes As New List(Of Integer)()
            Dim validHeaderLabels As New List(Of String)()
            For j As Integer = 0 To qtyCols - 1
                Dim label As String = ""
                If headers IsNot Nothing AndAlso j < headers.Length AndAlso headers(j) IsNot Nothing Then
                    label = headers(j).Trim()
                End If
                Dim labelU As String = label.ToUpperInvariant()
                Dim isInputTotal As Boolean = (labelU = "TOTAL" OrElse labelU = "TOPLAM")
                Dim isEmpty As Boolean = String.IsNullOrWhiteSpace(label)

                ' Keep only columns that have a header, are not TOTAL/TOPLAM, and have a positive sum
                If Not isEmpty AndAlso Not isInputTotal Then
                    Dim sumVal As Double = If(j < colSums.Count, colSums(j), 0)
                    If sumVal > 0 Then
                        validColIndexes.Add(j)
                        validHeaderLabels.Add(label)
                    End If
                End If
            Next

            ' Compute grand total from valid column sums
            Dim computedGrandTotal As Double = 0
            For Each idx In validColIndexes
                If idx < colSums.Count Then computedGrandTotal += colSums(idx)
            Next
            Dim showGrandTotal As Boolean = (computedGrandTotal > 0)

            sb.AppendLine("<section class=""po-assortment"">")
            sb.AppendLine("  <header class=""po-assortment__header"">")
            sb.AppendLine("    <div class=""po-grid"">")
            sb.AppendLine($"      <div class=""po-info-item""><span class=""po-label"">Destination:</span> <span class=""po-value"">{Esc(a.Destination_code)} — {Esc(a.Destination_description)}</span></div>")
            sb.AppendLine($"      <div class=""po-info-item""><span class=""po-label"">Delivery date:</span> <span class=""po-value"">{Esc(a.Delivery_date)}</span></div>")
            sb.AppendLine($"      <div class=""po-info-item""><span class=""po-label"">Assortment code:</span> <span class=""po-value"">{Esc(a.assortment_code)}</span></div>")
            sb.AppendLine($"      <div class=""po-info-item""><span class=""po-label"">Payment term:</span> <span class=""po-value"">{Esc(a.payment_term)}</span></div>")
            sb.AppendLine("    </div>")
            sb.AppendLine("  </header>")

            sb.AppendLine("  <div class=""table-container"">")
            sb.AppendLine("    <table class=""po-table"" role=""table"" aria-label=""Size breakdown"">")
            sb.AppendLine("      <thead class=""po-table__thead"">")
            sb.AppendLine("        <tr class=""po-table__tr"">")
            sb.AppendLine("          <th class=""po-table__th po-table__th--header"">Size</th>")
            For Each h In validHeaderLabels
                sb.AppendLine($"          <th class=""po-table__th po-table__th--header"">{Esc(h)}</th>")
            Next
            If hasRowTotals AndAlso showGrandTotal Then
                sb.AppendLine("          <th class=""po-table__th po-table__th--header"">Total</th>")
            End If
            sb.AppendLine("        </tr>")
            sb.AppendLine("      </thead>")

            sb.AppendLine("      <tbody class=""po-table__tbody"">")
            If rowCount > 0 Then
                For i As Integer = 0 To rowCount - 1
                    sb.AppendLine("        <tr class=""po-table__tr"">")
                    sb.AppendLine($"          <td class=""po-table__td po-table__td--size"">{Esc(a.SizeNames(i))}</td>")

                    ' Render only valid quantity columns aligned with filtered headers
                    For Each colIdx In validColIndexes
                        sb.AppendLine($"          <td class=""po-table__td po-table__td--qty"">{FmtNum(a.Quantities(i, colIdx))}</td>")
                    Next

                    ' Row total cell only if we are showing a totals column
                    If hasRowTotals AndAlso showGrandTotal Then
                        Dim rowTotal As Double = If(a.RowTotals IsNot Nothing AndAlso i < a.RowTotals.Length, a.RowTotals(i), 0)
                        sb.AppendLine($"          <td class=""po-table__td po-table__td--total po-table__td--row-total"">{FmtNum(rowTotal)}</td>")
                    End If

                    sb.AppendLine("        </tr>")
                Next
            End If
            sb.AppendLine("      </tbody>")

            sb.AppendLine("      <tfoot class=""po-table__tfoot"">")
            sb.AppendLine("        <tr class=""po-table__tr po-table__tr--totals"">")
            sb.AppendLine($"          <td class=""po-table__td po-table__td--total-label"">{Esc(If(a.TotalLabel, "Total"))}</td>")

            ' Footer totals must be full, numeric, and non-zero: use computed sums for valid columns
            For Each colIdx In validColIndexes
                Dim sumVal As Double = If(colIdx < colSums.Count, colSums(colIdx), 0)
                ' Only render columns we kept (sumVal > 0 by construction)
                sb.AppendLine($"          <td class=""po-table__td po-table__td--column-total"">{FmtNum(sumVal)}</td>")
            Next

            ' Grand total only if computed > 0
            If showGrandTotal Then
                sb.AppendLine($"          <td class=""po-table__td po-table__td--grand-total"">{FmtNum(computedGrandTotal)}</td>")
            End If

            sb.AppendLine("        </tr>")
            sb.AppendLine("      </tfoot>")
            sb.AppendLine("    </table>")
            sb.AppendLine("  </div>")
            sb.AppendLine("</section>")

            Return sb.ToString()

        Catch ex As Exception
            ErrDisp(ex.Message, "PurchaseOrderRenderer.RenderAssortmentTable", , , ex)
            Return "<section class=""po-assortment""><p class=""error-message"">Error rendering assortment table</p></section>"
        End Try
    End Function

    ' Helper function to check if row totals contain meaningful data
    Private Function HasMeaningfulRowTotals(rowTotals As Double()) As Boolean
        Try
            If rowTotals Is Nothing OrElse rowTotals.Length = 0 Then
                Return False
            End If

            ' Check if at least one row total is not zero
            For Each total In rowTotals
                If total <> 0 Then
                    Return True
                End If
            Next

            Return False
        Catch ex As Exception
            ErrDisp(ex.Message, "PurchaseOrderRenderer.HasMeaningfulRowTotals", , , ex)
            Return False
        End Try
    End Function

    Friend Function RenderPurchaseOrderHtml(d As OrderDoc) As String
        Try
            Dim fabricList As New StringBuilder()
            If d.fabric_content IsNot Nothing AndAlso d.fabric_content.Length > 0 Then
                For Each s In d.fabric_content
                    fabricList.AppendLine($"<li class=""po-list__item"">{Esc(s)}</li>")
                Next
            Else
                fabricList.AppendLine("<li class=""po-list__item"">—</li>")
            End If

            Dim assortmentsHtml As New StringBuilder()
            If d.xassortments IsNot Nothing Then
                For Each a In d.xassortments
                    assortmentsHtml.Append(RenderAssortmentTable(a))
                Next
            End If

            Dim css As String = GetEnhancedCSS()

            Dim sb As New StringBuilder()
            sb.AppendLine("<!DOCTYPE html>")
            sb.AppendLine($"<html lang=""en"" data-template=""purchase-order"" data-template-version=""{TEMPLATE_VERSION}"">")
            sb.AppendLine("<head>")
            sb.AppendLine("  <meta charset=""utf-8"" />")
            sb.AppendLine("  <meta name=""viewport"" content=""width=device-width,initial-scale=1"" />")
            sb.AppendLine($"  <title>Purchase Order - {Esc(d.order_no)}</title>")
            sb.AppendLine("  <style>")
            sb.AppendLine(css)
            sb.AppendLine("  </style>")
            sb.AppendLine("</head>")
            sb.AppendLine("<body>")
            sb.AppendLine("  <main class=""po"" id=""po-root"">")

            ' Enhanced header with gradient background
            sb.AppendLine("    <header class=""po-header"">")
            sb.AppendLine("      <div class=""po-header-content"">")
            sb.AppendLine($"        <h1 class=""po-title"">Purchase Order <span class=""po-badge"">{Esc(d.currency_code)}</span></h1>")
            sb.AppendLine($"        <div class=""po-order-number"">Order No: <strong>{Esc(d.order_no)}</strong></div>")
            sb.AppendLine("      </div>")
            sb.AppendLine("    </header>")

            ' Order Information Section
            sb.AppendLine("    <section class=""po-section"">")
            sb.AppendLine("      <h2 class=""po-section-title"">Order Information</h2>")
            sb.AppendLine("      <div class=""po-info-grid"">")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Date:</span> <span class=""po-value"">{Esc(d.date)}</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Season:</span> <span class=""po-value"">{Esc(d.season)}</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Production Group:</span> <span class=""po-value"">{Esc(d.production_group)}</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Main Group:</span> <span class=""po-value"">{Esc(d.production_main_group)}</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Gender:</span> <span class=""po-value"">{Esc(d.gender)}</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Barcode Item #:</span> <span class=""po-value"">{Esc(d.barcode_item_nr)}</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Agent:</span> <span class=""po-value"">{Esc(d.agent_name)} ({Esc(d.agent_code)})</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Preorder PO:</span> <span class=""po-value"">{Esc(d.preorder_po)}</span></div>")
            sb.AppendLine("      </div>")
            sb.AppendLine("    </section>")

            ' Shipping Information Section
            sb.AppendLine("    <section class=""po-section"">")
            sb.AppendLine("      <h2 class=""po-section-title"">Shipping & Delivery</h2>")
            sb.AppendLine("      <div class=""po-info-grid"">")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">From:</span> <span class=""po-value"">{Esc(d.from_)}</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">To:</span> <span class=""po-value"">{Esc(d.to_)}</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Shipment Mode:</span> <span class=""po-value"">{Esc(d.shipment_mode)}</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Terms of Delivery:</span> <span class=""po-value"">{Esc(d.terms_of_del)}</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Terms of Payment:</span> <span class=""po-value"">{Esc(d.terms_of_payment)}</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Style No:</span> <span class=""po-value"">{Esc(d.style_no)}</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Color:</span> <span class=""po-value"">{Esc(If(String.IsNullOrEmpty(d.color_no), d.color, (d.color_no & " " & If(d.color, ""))))}</span></div>")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Delivery Dates:</span> <span class=""po-value"">{Esc(d.delivery_date_1)} · {Esc(d.delivery_date_2)}</span></div>")
            sb.AppendLine("      </div>")
            sb.AppendLine("    </section>")

            ' Supplier Information Section
            sb.AppendLine("    <section class=""po-section"">")
            sb.AppendLine("      <h2 class=""po-section-title"">Supplier Information</h2>")
            sb.AppendLine("      <div class=""po-info-grid"">")
            sb.AppendLine($"        <div class=""po-info-item""><span class=""po-label"">Supplier:</span> <span class=""po-value"">{Esc(d.supplier)} ({Esc(d.supplier_code)})</span></div>")
            sb.AppendLine($"        <div class=""po-info-item po-info-item--full""><span class=""po-label"">Address:</span> <span class=""po-value"">{Esc(d.supplier_address)}</span></div>")
            sb.AppendLine("      </div>")
            sb.AppendLine("    </section>")

            ' Buyer Information Section
            sb.AppendLine("    <section class=""po-section"">")
            sb.AppendLine("      <h2 class=""po-section-title"">Buyer Information</h2>")
            sb.AppendLine("      <div class=""po-info-grid"">")
            sb.AppendLine($"        <div class=""po-info-item po-info-item--full""><span class=""po-label"">Address:</span> <span class=""po-value"">{Esc(d.address)}</span></div>")
            sb.AppendLine("      </div>")
            sb.AppendLine("    </section>")

            ' Financial Information Section
            sb.AppendLine("    <section class=""po-section po-section--financial"">")
            sb.AppendLine("      <h2 class=""po-section-title"">Financial Information</h2>")
            sb.AppendLine("      <div class=""po-amounts-grid"">")
            sb.AppendLine($"        <div class=""po-amount-card""><span class=""po-label"">Unit Price:</span> <span class=""po-amount"">{FmtNum(d.unit_price)} {Esc(d.currency_code)}</span></div>")
            sb.AppendLine($"        <div class=""po-amount-card""><span class=""po-label"">Foreign Amount:</span> <span class=""po-amount"">{FmtNum(d.foreign_amount)} {Esc(d.currency_code)}</span></div>")
            sb.AppendLine($"        <div class=""po-amount-card po-amount-card--highlight""><span class=""po-label"">Domestic Amount:</span> <span class=""po-amount"">{FmtNum(d.domestic_amount)} TRY</span></div>")
            sb.AppendLine("      </div>")
            sb.AppendLine("    </section>")

            ' Fabric Content Section
            sb.AppendLine("    <section class=""po-section"">")
            sb.AppendLine("      <h2 class=""po-section-title"">Fabric Content</h2>")
            sb.AppendLine($"      <ul class=""po-list"">{fabricList}</ul>")
            sb.AppendLine("    </section>")

            ' Assortments Section
            If assortmentsHtml.Length > 0 Then
                sb.AppendLine("    <section class=""po-section po-section--assortments"">")
                sb.AppendLine("      <h2 class=""po-section-title"">Size Assortments</h2>")
                sb.AppendLine(assortmentsHtml.ToString())
                sb.AppendLine("    </section>")
            Else
                sb.AppendLine("    <section class=""po-section"">")
                sb.AppendLine("      <h2 class=""po-section-title"">Size Assortments</h2>")
                sb.AppendLine("      <div class=""po-no-data"">No assortments available</div>")
                sb.AppendLine("    </section>")
            End If

            sb.AppendLine("  </main>")
            sb.AppendLine("</body>")
            sb.AppendLine("</html>")

            Return sb.ToString()

        Catch ex As Exception
            ErrDisp(ex.Message, "PurchaseOrderRenderer.RenderPurchaseOrderHtml", , , ex)
            Return "<html><body><div class=""error-container""><h1>Error Rendering Purchase Order</h1><p>" + ex.Message + "</p></div></body></html>"
        End Try
    End Function

    Private Function GetEnhancedCSS() As String
        Try
            Return "
/* Enhanced Purchase Order Styles - Black Text on White Background */
:root {
  --primary-color: #000000;
  --secondary-color: #000000;
  --accent-color: #000000;
  --success-color: #000000;
  --warning-color: #000000;
  --danger-color: #000000;
  --bg-color: #ffffff;
  --bg-alt: #ffffff;
  --border-color: #000000;
  --text-primary: #000000;
  --text-secondary: #000000;
  --text-muted: #000000;
  --shadow-sm: 0 1px 2px 0 rgba(0, 0, 0, 0.1);
  --shadow-md: 0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06);
  --shadow-lg: 0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05);
  --border-radius: 8px;
  --table-yellow: #ffffff;
  --table-yellow-dark: #000000;
  --red-text: #000000;
  --bold-border: 2px solid #000000;
  --cell-border: 1px solid #000000;
}

* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

html, body {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  line-height: 1.6;
  color: #000000;
  background: #ffffff;
  min-height: 100vh;
}

.po {
  max-width: 1200px;
  margin: 20px auto;
  background: #ffffff;
  border-radius: var(--border-radius);
  box-shadow: var(--shadow-lg);
  overflow: hidden;
  border: var(--bold-border);
}

/* Enhanced Header */
.po-header {
  background: #ffffff;
  color: #000000;
  padding: 30px;
  text-align: center;
  position: relative;
  overflow: hidden;
  border-bottom: var(--bold-border);
}

.po-header-content {
  position: relative;
  z-index: 1;
}

.po-title {
  font-size: 32px;
  font-weight: 800;
  margin-bottom: 10px;
  color: #000000;
}

.po-badge {
  display: inline-block;
  background: #ffffff;
  color: #000000;
  border-radius: 20px;
  padding: 4px 12px;
  font-size: 14px;
  font-weight: 600;
  margin-left: 10px;
  box-shadow: var(--shadow-sm);
  border: 2px solid #000000;
}

.po-order-number {
  font-size: 18px;
  font-weight: 600;
  color: #000000;
}

/* Section Styles */
.po-section {
  padding: 25px 30px;
  border-bottom: var(--bold-border);
  background: #ffffff;
}

.po-section:last-child {
  border-bottom: none;
}

.po-section--financial {
  background: #ffffff;
}

.po-section--assortments {
  background: #ffffff;
}

.po-section-title {
  font-size: 22px;
  font-weight: 700;
  color: #000000;
  margin-bottom: 20px;
  padding-bottom: 10px;
  border-bottom: 3px solid #000000;
  text-transform: uppercase;
  letter-spacing: 1px;
}

/* Info Grid */
.po-info-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 15px;
}

.po-info-item {
  display: flex;
  padding: 12px;
  background: #ffffff;
  border: var(--bold-border);
  border-radius: var(--border-radius);
  box-shadow: var(--shadow-sm);
}

.po-info-item--full {
  grid-column: 1 / -1;
}

.po-label {
  font-weight: 600;
  color: #000000;
  min-width: 140px;
  flex-shrink: 0;
}

.po-value {
  font-weight: 500;
  color: #000000;
  flex: 1;
}

/* Financial Cards */
.po-amounts-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 20px;
}

.po-amount-card {
  background: #ffffff;
  border: var(--bold-border);
  border-radius: var(--border-radius);
  padding: 20px;
  text-align: left;
  box-shadow: var(--shadow-md);
  transition: transform 0.2s ease;
}

.po-amount-card:hover {
  transform: translateY(-2px);
  box-shadow: var(--shadow-lg);
}

.po-amount-card--highlight {
  background: #ffffff;
  color: #000000;
  border-color: #000000;
}

.po-amount {
  display: block;
  font-size: 24px;
  font-weight: 700;
  margin-top: 8px;
  color: #000000;
}

/* List Styles */
.po-list {
  list-style: none;
  padding: 0;
  background: #ffffff;
  border: var(--bold-border);
  border-radius: var(--border-radius);
}

.po-list__item {
  padding: 12px 20px;
  border-bottom: 1px solid #000000;
  font-weight: 500;
  color: #000000;
  background: #ffffff;
}

.po-list__item:last-child {
  border-bottom: none;
}

.po-list__item:nth-child(even) {
  background: #ffffff;
}

/* Enhanced Table Styles with Complete Black Grid Lines */
.table-container {
  margin: 20px 0;
  border-radius: 0; 
  overflow: hidden;
  box-shadow: var(--shadow-md);
  border: var(--bold-border);
}

.po-table {
  width: 100%;
  border-collapse: collapse;
  border-spacing: 0;
  background: #ffffff;
  font-size: 14px;
  border: 2px solid #000000 !important; /* Force black outer border */
  color: #000000 !important;
}

/* Table Headers with Complete Black Borders */
.po-table__thead {
  background: #ffffff;
}

.po-table__th--header {
  padding: 15px 12px;
  text-align: center;
  font-weight: 700;
  font-size: 15px;
  color: #000000 !important;
  text-transform: uppercase;
  letter-spacing: 0.5px;
  background: #ffffff;
  border: 1px solid #000000 !important;
  border-top: 2px solid #000000 !important;
  border-bottom: 2px solid #000000 !important;
}

.po-table__th--header:first-child {
  border-left: 2px solid #000000 !important;
}

.po-table__th--header:last-child {
  border-right: 2px solid #000000 !important;
}

/* Table Body with Complete Black Grid Lines */
.po-table__tbody .po-table__tr {
  transition: background-color 0.2s ease;
}

.po-table__tbody .po-table__tr:hover {
  background: #f8f9fa;
}

.po-table__tbody .po-table__tr:nth-child(even) {
  background: #ffffff;
}

/* All table cells with complete BLACK borders */
.po-table__td {
  padding: 12px;
  text-align: center;
  font-weight: 500;
  vertical-align: middle;
  color: #000000 !important;
  background: #ffffff;
  border: 1px solid #000000 !important;
}

/* First column borders */
.po-table__td:first-child,
.po-table__td--size {
  border-left: 2px solid #000000 !important;
  text-align: left;
  font-weight: 600;
  background: #ffffff;
  color: #000000 !important;
}

/* Last column borders */
.po-table__td:last-child {
  border-right: 2px solid #000000 !important;
}

/* Size column specific styling */
.po-table__td--size {
  text-align: left;
  font-weight: 600;
  background: #ffffff;
  color: #000000 !important;
}

/* Quantity cells */
.po-table__td--qty {
  font-family: 'Courier New', monospace;
  font-weight: 600;
  color: #000000 !important;
  background: #ffffff;
  border: 1px solid #000000 !important;
}

/* Row Totals - Complete borders with emphasis */
.po-table__td--row-total {
  background: #ffffff !important;
  font-weight: 700;
  font-size: 15px;
  color: #000000 !important;
  border: 2px solid #000000 !important;
}

/* Footer - Column Totals with Complete Black Borders */
.po-table__tfoot {
  background: #ffffff;
  border-top: 2px solid #000000 !important;
}

.po-table__tfoot .po-table__td {
  border-top: 2px solid #000000 !important;
  border-bottom: 2px solid #000000 !important;
  border-left: 1px solid #000000 !important;
  border-right: 1px solid #000000 !important;
}

.po-table__td--total-label {
  background: #ffffff !important;
  font-weight: 700;
  font-size: 15px;
  text-align: left;
  color: #000000 !important;
  text-transform: uppercase;
  border: 2px solid #000000 !important;
}

.po-table__td--column-total {
  background: #ffffff !important;
  font-weight: 700;
  font-size: 15px;
  color: #000000 !important;
  font-family: 'Courier New', monospace;
  border-top: 2px solid #000000 !important;
  border-bottom: 2px solid #000000 !important;
  border-left: 1px solid #000000 !important;
  border-right: 1px solid #000000 !important;
}

.po-table__td--grand-total {
  background: #ffffff !important;
  font-weight: 800;
  font-size: 16px;
  color: #000000 !important;
  border: 2px solid #000000 !important;
}

/* Last row bottom border */
.po-table__tbody .po-table__tr:last-child .po-table__td {
  border-bottom: 2px solid #000000 !important;
}

/* Assortment Sections */
.po-assortment {
  margin: 25px 0;
  background: #ffffff;
  border: var(--bold-border);
  border-radius: var(--border-radius);
  overflow: hidden;
}

.po-assortment__header {
  background: #ffffff;
  color: #000000;
  padding: 20px;
  border-bottom: 3px solid #000000;
}

/* Fix for Assortment Header Visibility */
.po-assortment__header .po-info-item {
  background: #ffffff !important;
  border: 2px solid #000000;
  margin-bottom: 10px;
  color: #000000 !important;
}

.po-assortment__header .po-info-item .po-label {
  color: #000000 !important;
  font-weight: 700;
}

.po-assortment__header .po-info-item .po-value {
  color: #000000 !important;
  font-weight: 600;
}

.po-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 15px;
}

/* Error and No Data States */
.error-message, .po-no-data {
  text-align: center;
  padding: 40px;
  background: #ffffff;
  color: #000000;
  border: 2px solid #000000;
  border-radius: var(--border-radius);
  font-weight: 600;
  font-size: 16px;
}

.po-no-data {
  background: #ffffff;
  color: #000000;
  border-color: #000000;
}

/* Responsive Design */
@media (max-width: 768px) {
  .po {
    margin: 10px;
    border-radius: 0;
  }
  
  .po-title {
    font-size: 24px;
  }
  
  .po-info-grid {
    grid-template-columns: 1fr;
  }
  
  .po-amounts-grid {
    grid-template-columns: 1fr;
  }
  
  .po-grid {
    grid-template-columns: 1fr;
  }
  
  .po-table {
    font-size: 12px;
  }
  
  .po-table__th--header,
  .po-table__td {
    padding: 8px 6px;
  }
}

/* Print Styles */
@media print {
  body {
    background: #ffffff;
    color: #000000;
  }
  
  .po {
    box-shadow: none;
    margin: 0;
    background: #ffffff;
  }
  
  .po-header {
    background: #ffffff !important;
    color: #000000 !important;
    -webkit-print-color-adjust: exact;
  }
  
  .po-section {
    break-inside: avoid;
    background: #ffffff;
    color: #000000;
  }
  
  .po-assortment__header .po-info-item {
    background: #ffffff !important;
    color: #000000 !important;
  }
  
  .po-assortment__header .po-info-item .po-label,
  .po-assortment__header .po-info-item .po-value {
    color: #000000 !important;
  }
  
  /* Ensure all borders print correctly */
  .po-table,
  .po-table__th--header,
  .po-table__td {
    border: 1px solid #000000 !important;
  }
  
  .po-table__td--row-total,
  .po-table__td--total-label,
  .po-table__td--grand-total {
    border: 2px solid #000000 !important;
  }
  
  * {
    background: #ffffff !important;
    color: #000000 !important;
  }
}
"
        Catch ex As Exception
            ErrDisp(ex.Message, "GetEnhancedCSS", , , ex)
            Return "/* Error loading CSS */"
        End Try
    End Function
End Module
