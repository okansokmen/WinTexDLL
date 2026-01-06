' Install-Package Newtonsoft.Json
Imports System.Globalization
Imports System.Net.Http
Imports System.Text
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module utilJsonToHtml
    Public Class PurchaseOrder
        <JsonProperty("order_no")> Public Property OrderNo As String
        <JsonProperty("date")> Public Property [Date] As String
        <JsonProperty("season")> Public Property Season As String
        <JsonProperty("production_group")> Public Property ProductionGroup As String
        <JsonProperty("production_main_group")> Public Property ProductionMainGroup As String
        <JsonProperty("gender")> Public Property Gender As String
        <JsonProperty("barcode_item_nr")> Public Property BarcodeItemNr As String
        <JsonProperty("agent_name")> Public Property AgentName As String
        <JsonProperty("agent_code")> Public Property AgentCode As String
        <JsonProperty("preorder_po")> Public Property PreorderPo As String
        <JsonProperty("currency_code")> Public Property CurrencyCode As String
        <JsonProperty("unit_price")> Public Property UnitPrice As Decimal?
        <JsonProperty("total_amount")> Public Property TotalAmount As Decimal?
        <JsonProperty("fabric_content")> Public Property FabricContent As List(Of String)
        <JsonProperty("address")> Public Property Address As String
        <JsonProperty("supplier")> Public Property Supplier As String
        <JsonProperty("supplier_code")> Public Property SupplierCode As String
        <JsonProperty("style_no")> Public Property StyleNo As String
        <JsonProperty("delivery_date_1")> Public Property DeliveryDate1 As String
        <JsonProperty("delivery_date_2")> Public Property DeliveryDate2 As String
        <JsonProperty("terms_of_del")> Public Property TermsOfDel As String
        <JsonProperty("shipment_mode")> Public Property ShipmentMode As String
        <JsonProperty("from")> Public Property FromCountry As String
        <JsonProperty("to")> Public Property ToCountry As String
        <JsonProperty("terms_of_payment")> Public Property TermsOfPayment As String
        <JsonProperty("foreign_amount")> Public Property ForeignAmount As Decimal?
        <JsonProperty("domestic_amount")> Public Property DomesticAmount As Decimal?
        <JsonProperty("supplier_address")> Public Property SupplierAddress As String
        <JsonProperty("color_no")> Public Property ColorNo As String
        <JsonProperty("color")> Public Property Color As String
        <JsonProperty("xassortments")> Public Property XAssortments As List(Of XAssortment)
    End Class

    Public Class XAssortment
        <JsonProperty("Destination_code")> Public Property DestinationCode As String
        <JsonProperty("Destination_description")> Public Property DestinationDescription As String
        <JsonProperty("Delivery_date")> Public Property DeliveryDate As String
        <JsonProperty("assortment_code")> Public Property AssortmentCode As String
        <JsonProperty("payment_term")> Public Property PaymentTerm As String
        <JsonProperty("column_headers_row")> Public Property ColumnHeadersRow As List(Of String)
        <JsonProperty("data_rows")> Public Property DataRows As List(Of AssortmentRow)
        <JsonProperty("column_totals_row")> Public Property ColumnTotalsRow As ColumnTotals
    End Class

    Public Class AssortmentRow
        <JsonProperty("size_name")> Public Property SizeName As String
        <JsonProperty("quantities")> Public Property Quantities As List(Of Decimal)
        <JsonProperty("row_total")> Public Property RowTotal As Decimal
    End Class

    Public Class ColumnTotals
        <JsonProperty("total_label")> Public Property TotalLabel As String
        <JsonProperty("total_quantities")> Public Property TotalQuantities As List(Of Decimal)
        <JsonProperty("total_row_total")> Public Property TotalRowTotal As Decimal
    End Class

    Private Const TEMPLATE_VERSION As String = "1.0.0"

    Private Function Esc(value As Object) As String
        Dim s = If(value, "").ToString()
        s = s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")
        s = s.Replace("""", "&quot;").Replace("'", "&#39;")
        Return s
    End Function


    Private Function FmtNum(n As Decimal?) As String
        If Not n.HasValue Then Return "—"
        Return n.Value.ToString("#,##0.##", CultureInfo.InvariantCulture)
    End Function


    Private Function RenderAssortmentTable(a As XAssortment) As String
        Dim sb As New StringBuilder()

        sb.AppendLine("<section class=""po-assortment"">")
        sb.AppendLine("  <header class=""po-assortment__header"">")
        sb.AppendLine("    <div class=""po-grid"">")
        sb.AppendLine($"      <div><span class=""po-label"">Destination:</span> {Esc(a.DestinationCode)} — {Esc(a.DestinationDescription)}</div>")
        sb.AppendLine($"      <div><span class=""po-label"">Delivery date:</span> {Esc(a.DeliveryDate)}</div>")
        sb.AppendLine($"      <div><span class=""po-label"">Assortment code:</span> {Esc(a.AssortmentCode)}</div>")
        sb.AppendLine($"      <div><span class=""po-label"">Payment term:</span> {Esc(a.PaymentTerm)}</div>")
        sb.AppendLine("    </div>")
        sb.AppendLine("  </header>")

        sb.AppendLine("  <table class=""po-table"" role=""table"" aria-label=""Size breakdown"">")
        sb.AppendLine("    <thead class=""po-table__thead"">")
        sb.AppendLine("      <tr class=""po-table__tr"">")
        sb.AppendLine("        <th class=""po-table__th po-table__th--size"">Size</th>")
        If a.ColumnHeadersRow IsNot Nothing Then
            For Each h In a.ColumnHeadersRow
                sb.AppendLine($"        <th class=""po-table__th"">{Esc(h)}</th>")
            Next
        End If
        sb.AppendLine("        <th class=""po-table__th po-table__th--total"">Total</th>")
        sb.AppendLine("      </tr>")
        sb.AppendLine("    </thead>")

        sb.AppendLine("    <tbody class=""po-table__tbody"">")
        If a.DataRows IsNot Nothing Then
            For Each r In a.DataRows
                sb.AppendLine("      <tr class=""po-table__tr"">")
                sb.AppendLine($"        <td class=""po-table__td po-table__td--size"">{Esc(r.SizeName)}</td>")
                If r.Quantities IsNot Nothing Then
                    For Each q In r.Quantities
                        sb.AppendLine($"        <td class=""po-table__td po-table__td--qty"">{FmtNum(q)}</td>")
                    Next
                End If
                sb.AppendLine($"        <td class=""po-table__td po-table__td--total"">{FmtNum(r.RowTotal)}</td>")
                sb.AppendLine("      </tr>")
            Next
        End If
        sb.AppendLine("    </tbody>")

        Dim totals = If(a.ColumnTotalsRow, New ColumnTotals With {.TotalLabel = "Total", .TotalQuantities = New List(Of Decimal), .TotalRowTotal = 0D})
        sb.AppendLine("    <tfoot class=""po-table__tfoot"">")
        sb.AppendLine("      <tr class=""po-table__tr po-table__tr--totals"">")
        sb.AppendLine($"        <td class=""po-table__td po-table__td--size po-table__td--sum"">{Esc(If(totals.TotalLabel, "Total"))}</td>")
        If totals.TotalQuantities IsNot Nothing Then
            For Each tq In totals.TotalQuantities
                sb.AppendLine($"        <td class=""po-table__td po-table__td--qty po-table__td--sum"">{FmtNum(tq)}</td>")
            Next
        End If
        sb.AppendLine($"        <td class=""po-table__td po-table__td--total po-table__td--sum"">{FmtNum(totals.TotalRowTotal)}</td>")
        sb.AppendLine("      </tr>")
        sb.AppendLine("    </tfoot>")
        sb.AppendLine("  </table>")
        sb.AppendLine("</section>")

        Return sb.ToString()
    End Function

    Public Function RenderPurchaseOrderHtml(d As PurchaseOrder) As String
        Dim fabricList As New StringBuilder()
        If d.FabricContent IsNot Nothing AndAlso d.FabricContent.Count > 0 Then
            For Each s In d.FabricContent
                fabricList.AppendLine($"<li class=""po-list__item"">{Esc(s)}</li>")
            Next
        Else
            fabricList.AppendLine("<li class=""po-list__item"">—</li>")
        End If

        Dim assortmentsHtml As New StringBuilder()
        If d.XAssortments IsNot Nothing Then
            For Each a In d.XAssortments
                assortmentsHtml.Append(RenderAssortmentTable(a))
            Next
        End If

        Dim css As String =
"  :root{--fg:#0b1220;--muted:#5b6474;--line:#e6e8ef;--bg:#ffffff;--card:#fafbff;--acc:#0f62fe}
  *{box-sizing:border-box}
  html,body{margin:0;padding:0;background:var(--bg);color:var(--fg);font:14px/1.6 ui-sans-serif,system-ui,-apple-system,Segoe UI,Roboto,Arial}
  .po{max-width:1040px;margin:32px auto;padding:24px}
  .po-header{display:flex;justify-content:space-between;align-items:flex-start;margin-bottom:16px}
  .po-title{font-size:20px;font-weight:700;margin:0}
  .po-meta{display:grid;grid-template-columns:repeat(4,minmax(0,1fr));gap:8px 16px;margin:16px 0 24px}
  .po-card{background:var(--card);border:1px solid var(--line);border-radius:12px;padding:16px;margin-bottom:16px}
  .po-grid{display:grid;grid-template-columns:repeat(2,minmax(0,1fr));gap:8px 16px}
  .po-label{color:var(--muted)}
  .po-amounts{display:grid;grid-template-columns:repeat(3,minmax(0,1fr));gap:8px 16px}
  .po-list{margin:8px 0 0 16px}
  .po-table{width:100%;border-collapse:separate;border-spacing:0;margin-top:8px;border:1px solid var(--line);border-radius:12px;overflow:hidden}
  .po-table__th,.po-table__td{padding:8px 10px;border-bottom:1px solid var(--line);text-align:right;white-space:nowrap}
  .po-table__th--size,.po-table__td--size{text-align:left}
  .po-table__th{background:#f3f5fb;font-weight:600}
  .po-table__tr--totals .po-table__td--sum{font-weight:700;border-top:2px solid var(--line)}
  .po-badge{display:inline-block;background:var(--acc);color:white;border-radius:16px;padding:2px 8px;font-size:12px}
  @media (max-width:800px){.po-meta{grid-template-columns:repeat(2,minmax(0,1fr))}.po-grid{grid-template-columns:1fr}.po-amounts{grid-template-columns:1fr}}"

        Dim sb As New StringBuilder()
        sb.AppendLine("<!DOCTYPE html>")
        sb.AppendLine($"<html lang=""en"" data-template=""purchase-order"" data-template-version=""{TEMPLATE_VERSION}"">")
        sb.AppendLine("<head>")
        sb.AppendLine("  <meta charset=""utf-8"" />")
        sb.AppendLine("  <meta name=""viewport"" content=""width=device-width,initial-scale=1"" />")
        sb.AppendLine($"  <title>PO {Esc(d.OrderNo)}</title>")
        sb.AppendLine("  <style>")
        sb.AppendLine(css)
        sb.AppendLine("  </style>")
        sb.AppendLine("</head>")
        sb.AppendLine("<body>")
        sb.AppendLine("  <main class=""po"" id=""po-root"">")
        sb.AppendLine("    <header class=""po-header"">")
        sb.AppendLine($"      <h1 class=""po-title"">Purchase Order <span class=""po-badge"">{Esc(d.CurrencyCode)}</span></h1>")
        sb.AppendLine($"      <div class=""po-label"">Order No: <strong>{Esc(d.OrderNo)}</strong></div>")
        sb.AppendLine("    </header>")

        sb.AppendLine("    <section class=""po-meta po-card"">")
        sb.AppendLine($"      <div><span class=""po-label"">Date:</span> {Esc(d.Date)}</div>")
        sb.AppendLine($"      <div><span class=""po-label"">Season:</span> {Esc(d.Season)}</div>")
        sb.AppendLine($"      <div><span class=""po-label"">Group:</span> {Esc(d.ProductionGroup)}</div>")
        sb.AppendLine($"      <div><span class=""po-label"">Main group:</span> {Esc(d.ProductionMainGroup)}</div>")
        sb.AppendLine($"      <div><span class=""po-label"">Gender:</span> {Esc(d.Gender)}</div>")
        sb.AppendLine($"      <div><span class=""po-label"">Barcode item #:</span> {Esc(d.BarcodeItemNr)}</div>")
        sb.AppendLine($"      <div><span class=""po-label"">Agent:</span> {Esc(d.AgentName)} ({Esc(d.AgentCode)})</div>")
        sb.AppendLine($"      <div><span class=""po-label"">Preorder PO:</span> {Esc(d.PreorderPo)}</div>")
        sb.AppendLine("    </section>")

        sb.AppendLine("    <section class=""po-card"">")
        sb.AppendLine("      <div class=""po-grid"">")
        sb.AppendLine($"        <div><span class=""po-label"">From:</span> {Esc(d.FromCountry)}</div>")
        sb.AppendLine($"        <div><span class=""po-label"">To:</span> {Esc(d.ToCountry)}</div>")
        sb.AppendLine($"        <div><span class=""po-label"">Shipment mode:</span> {Esc(d.ShipmentMode)}</div>")
        sb.AppendLine($"        <div><span class=""po-label"">Terms of delivery:</span> {Esc(d.TermsOfDel)}</div>")
        sb.AppendLine($"        <div><span class=""po-label"">Terms of payment:</span> {Esc(d.TermsOfPayment)}</div>")
        sb.AppendLine($"        <div><span class=""po-label"">Style no:</span> {Esc(d.StyleNo)}</div>")
        sb.AppendLine($"        <div><span class=""po-label"">Color:</span> {Esc(If(String.IsNullOrEmpty(d.ColorNo), d.Color, (d.ColorNo & " " & If(d.Color, ""))))}</div>")
        sb.AppendLine($"        <div><span class=""po-label"">Delivery:</span> {Esc(d.DeliveryDate1)} · {Esc(d.DeliveryDate2)}</div>")
        sb.AppendLine("      </div>")
        sb.AppendLine("    </section>")

        sb.AppendLine("    <section class=""po-card"">")
        sb.AppendLine("      <h2 class=""po-section-title"">Supplier</h2>")
        sb.AppendLine("      <div class=""po-grid"">")
        sb.AppendLine($"        <div><span class=""po-label"">Supplier:</span> {Esc(d.Supplier)} ({Esc(d.SupplierCode)})</div>")
        sb.AppendLine($"        <div><span class=""po-label"">Address:</span> {Esc(d.SupplierAddress)}</div>")
        sb.AppendLine("      </div>")
        sb.AppendLine("    </section>")

        sb.AppendLine("    <section class=""po-card"">")
        sb.AppendLine("      <h2 class=""po-section-title"">Buyer</h2>")
        sb.AppendLine("      <div class=""po-grid"">")
        sb.AppendLine($"        <div><span class=""po-label"">Address:</span> {Esc(d.Address)}</div>")
        sb.AppendLine("      </div>")
        sb.AppendLine("    </section>")

        sb.AppendLine("    <section class=""po-card"">")
        sb.AppendLine("      <h2 class=""po-section-title"">Amounts</h2>")
        sb.AppendLine("      <div class=""po-amounts"">")
        sb.AppendLine($"        <div><span class=""po-label"">Unit price:</span> {FmtNum(d.UnitPrice)} {Esc(d.CurrencyCode)}</div>")
        sb.AppendLine($"        <div><span class=""po-label"">Foreign amount:</span> {FmtNum(d.ForeignAmount)} {Esc(d.CurrencyCode)}</div>")
        sb.AppendLine($"        <div><span class=""po-label"">Domestic amount:</span> {FmtNum(d.DomesticAmount)} TRY</div>")
        sb.AppendLine("      </div>")
        sb.AppendLine("    </section>")

        sb.AppendLine("    <section class=""po-card"">")
        sb.AppendLine("      <h2 class=""po-section-title"">Fabric content</h2>")
        sb.AppendLine($"      <ul class=""po-list"">{fabricList}</ul>")
        sb.AppendLine("    </section>")

        sb.AppendLine(If(assortmentsHtml.Length > 0, assortmentsHtml.ToString(), "<section class=""po-card""><em>No assortments</em></section>"))

        sb.AppendLine("  </main>")
        sb.AppendLine("</body>")
        sb.AppendLine("</html>")

        Return sb.ToString()
    End Function

    Public Class GeminiClient
        Private ReadOnly _http As HttpClient
        Private ReadOnly _apiKey As String
        Private ReadOnly _modelUrl As String   ' e.g. https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=YOUR_KEY

        Public Sub New(apiKey As String, modelUrl As String)
            _apiKey = apiKey
            _modelUrl = modelUrl
            _http = New HttpClient()
        End Sub

        Public Async Function GetPurchaseOrderAsync(inputText As String, jsonSchema As String) As Task(Of PurchaseOrder)
            Dim payload As New JObject From {
            {"contents", New JArray(New JObject From {
                {"parts", New JArray(New JObject From {{"text", inputText}})}
            })},
            {"generationConfig", New JObject From {
                {"temperature", 0},
                {"topK", 1},
                {"responseMimeType", "application/json"},
                {"responseSchema", JObject.Parse(jsonSchema)}
            }}
        }

            Dim req As New HttpRequestMessage(HttpMethod.Post, _modelUrl)
            req.Content = New StringContent(payload.ToString(), Encoding.UTF8, "application/json")

            Dim rsp = Await _http.SendAsync(req)
            rsp.EnsureSuccessStatusCode()
            Dim json As String = Await rsp.Content.ReadAsStringAsync()

            ' Gemini returns a wrapper; locate the JSON in candidates[0].content/parts[x].text if needed.
            Dim root = JObject.Parse(json)
            Dim poJson As String = Nothing

            ' Try: candidates[0].content.parts[*].text contains the JSON
            Dim candidates = root("candidates")
            If candidates IsNot Nothing AndAlso candidates.Any() Then
                Dim parts = candidates(0)("content")?("parts")
                If parts IsNot Nothing Then
                    For Each p In parts
                        Dim t = p("text")?.ToString()
                        If Not String.IsNullOrWhiteSpace(t) AndAlso t.TrimStart().StartsWith("{") Then
                            poJson = t
                            Exit For
                        End If
                    Next
                End If
            End If

            If String.IsNullOrEmpty(poJson) Then
                Throw New InvalidOperationException("Model did not return JSON content.")
            End If

            Dim po As PurchaseOrder = JsonConvert.DeserializeObject(Of PurchaseOrder)(poJson)
            Return po
        End Function
    End Class

End Module
