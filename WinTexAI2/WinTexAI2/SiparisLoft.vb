Imports DevExpress.Office

Public Class SiparisLoft

    Public Property Prompt As String
    Public Property JsonSchema As Object

    Public Sub New()
        ' Initialize with example JSON schema
        JsonSchema = New With {
            .type = "object",
            .properties = New With {
                .order_no = New With {.type = "string"},
                .date = New With {.type = "string"},
                .season = New With {.type = "string"},
                .production_group = New With {.type = "string"},
                .production_main_group = New With {.type = "string"},
                .gender = New With {.type = "string"},
                .barcode_item_nr = New With {.type = "string"},
                .agent_name = New With {.type = "string"},
                .agent_code = New With {.type = "string"},
                .preorder_po = New With {.type = "string"},
                .currency_code = New With {.type = "string"},
                .unit_price = New With {.type = "number"},
                .total_amount = New With {.type = "number"},
                .fabric_content = New With {
                                            .type = "array",
                                            .items = New With {.type = "string"}
                                            },
                .address = New With {.type = "string"},
                .supplier = New With {.type = "string"},
                .supplier_code = New With {.type = "string"},
                .style_no = New With {.type = "string"},
                .delivery_date_1 = New With {.type = "string"},
                .delivery_date_2 = New With {.type = "string"},
                .terms_of_del = New With {.type = "string"},
                .shipment_mode = New With {.type = "string"},
                .from = New With {.type = "string"},
                .to = New With {.type = "string"},
                .terms_of_payment = New With {.type = "string"},
                .foreign_amount = New With {.type = "number"},
                .domestic_amount = New With {.type = "number"},
                .supplier_address = New With {.type = "string"},
                .color_no = New With {.type = "string"},
                .color = New With {.type = "string"},
                .xassortments = New With {
                                .type = "array",
                                .items = New With {
                                    .type = "object",
                                    .required = New String() {"Destination_code", "Destination_description", "Delivery_date",
                                                              "assortment_code", "payment_term",
                                                              "column_headers_row", "data_rows", "column_totals_row"},
                                    .properties = New With {
                                        .Destination_code = New With {.type = "string"},
                                        .Destination_description = New With {.type = "string"},
                                        .Delivery_date = New With {.type = "string"},
                                        .assortment_code = New With {.type = "string"},
                                        .payment_term = New With {.type = "string"},
                                        .column_headers_row = New With {
                                            .type = "array",
                                            .items = New With {.type = "string"}
                                        },
                                        .data_rows = New With {
                                            .type = "array",
                                            .items = New With {
                                                    .type = "object",
                                                    .required = New String() {"size_name", "quantities", "row_total"},
                                                    .properties = New With {
                                                                .size_name = New With {.type = "string"},
                                                                .quantities = New With {
                                                                            .type = "array",
                                                                            .items = New With {.type = "number"}
                                                                },
                                                                .row_total = New With {.type = "number"}
                                                    }
                                            }
                                        },
                                        .column_totals_row = New With {
                                            .type = "object",
                                            .required = New String() {"total_label", "total_quantities", "total_row_total"},
                                            .properties = New With {
                                                        .total_label = New With {.type = "string"},
                                                        .total_quantities = New With {
                                                                    .type = "array",
                                                                    .items = New With {.type = "number"}
                                                        },
                                                        .total_row_total = New With {.type = "number"}
                                            }
                                        }
                                    }
                                }
                }
            },
        .required = New String() {"order_no", "date", "season", "production_group", "production_main_group",
                                    "gender", "barcode_item_nr", "agent_name", "agent_code", "preorder_po",
                                    "currency_code", "unit_price", "total_amount", "fabric_content",
                                    "address", "supplier", "supplier_code", "style_no", "delivery_date_1",
                                    "delivery_date_2", "terms_of_del", "shipment_mode", "from", "to",
                                    "terms_of_payment", "foreign_amount", "domestic_amount", "supplier_address", "xassortments"}
        }

        'Prompt = "Task: Parse the attached Purchase Order PDF and return a single JSON object that exactly matches the schema below. " +
        '        " Do not summarize or omit any details. " +
        '        " Ensure the JSON is valid and complete, with no additional text, explanations, or deviations. " +
        '        " The document contains structured data that must be extracted verbatim where possible. " +
        '        " Follow these rules strictly: " +
        '        " Critical rule about blanks: If a table cell is empty/blank/whitespace, output null (not 0). " +
        '        " Only output numeric 0 if the source explicitly shows a zero (e.g., 0, 0,00, 0.00). " +
        '        " Numbers: " +
        '        " • Accept both comma and dot decimal formats and return as numbers (e.g., 4.460,40 → 4460.40). " +
        '        " • Trim currency/units/symbols; preserve units separately only if present as labels. " +
        '        " Dates: Keep as found (string). " +
        '        " Whitespace & repeats: Normalize whitespace; if a field repeats in the PDF, prefer the most specific/structured occurrence. " +
        '        " Tables: " +
        '        " • For each assortment table, capture the header row, per-row size data, per-row totals, and the final column-totals row. " +
        '        " • In data_rows[i].quantities, align quantities to the header sizes. Use null for truly blank cells; never invent values. " +
        '        " Validation: " +
        '        " • row_total = sum of non-null quantities in that row (treat null as missing, not zero). " +
        '        " • column_totals_row values must equal the column sums of non-null quantities across rows. " +
        '        " • If a published total does not match computed sums, keep the published value but add a _validation object explaining the mismatch (do not fix the source). " +
        '        " Output format: Return JSON only. No prose, no markdown, no explanations outside _validation. " +
        '        “ Treat any whitespace-only cell as null. Never coerce null to zero during any arithmetic; compute sums by ignoring null cells. ” +
        '        “ If the PDF layout makes a cell ambiguous, prefer null. ” +
        '        " Do Not summarize Or omit any details—extract everything verbatim where possible. " +
        '        " Output only the extracted information strictly in the following JSON structure, with no additional text, explanations, Or deviations. " +
        '        " Ensure the JSON Is valid And complete. "

        Prompt = "You are an expert at extracting and structuring information from PDF documents. " +
                " Analyze the entire provided PDF file page by page. " +
                " Extract the full text content from each page, preserving the original layout, formatting, " +
                " And any structured elements (such as tables, headings, Or key-value pairs) as closely as possible in plain text form.  " +
                " Do Not summarize Or omit any details—extract everything verbatim where possible. " +
                " Output only the extracted information strictly in the following JSON structure, with no additional text, explanations, Or deviations. " +
                " Ensure the JSON Is valid And complete. "

        Prompt = Prompt + vbCrLf +
                " the document contains 3 sections. PURCHASE ORDER section, DESTINATION DETAILS section , SIZE ASSORTED section " + vbCrLf +
                " first extract PURCHASE ORDER section " + vbCrLf +
                " in  PURCHASE ORDER section first extract the fields below " + vbCrLf +
                " - order_no : customers order code , example : PO000043479 " + vbCrLf +
                " - date : order arrival date in dd.mm.YYYY format, example : 04.06.2025 " + vbCrLf +
                " - season : season code , example : 2026 Yaz / 2026 nеto " + vbCrLf +
                " - production_group : production group code , example : PNT " + vbCrLf +
                " - production_main_group : production main group code , example : DENIM " + vbCrLf +
                " - gender : W for woman, M for man, may be other strings , example : W " + vbCrLf +
                " - barcode_item_nr : borcode for the product , example : LF2038796 " + vbCrLf +
                " - agent_name : trader company name , example : COLINS " + vbCrLf +
                " - agent_code : trader company code , example : D.MS.B.04.TR.00.01 " + vbCrLf +
                " - preorder_po : preorder order code , example : PO000043470 " + vbCrLf +
                " - currency_code : international currency code , example : USD" + vbCrLf +
                " - unit_price : price , decimal seperator as comma , example : 11,80 " + vbCrLf +
                " - total_amount : amount , decimal seperator as comma , example : 4.460,40 " + vbCrLf +
                " - fabric_content : may be two fabric contents  " + vbCrLf +
                " - address : address , may be multiline , example : LOFT MAĞ.AŞ " + vbCrLf +
                " - supplier : supplier company , example : COLINS MISIR " + vbCrLf +
                " - supplier_code : supplier company code , example : D.MS.B.04.TR.00.01" + vbCrLf +
                " - style_no : model code , example : 2038796 BELLA " + vbCrLf +
                " - delivery_date_1 : a note for delivery date , label is DELIVERY DATE , example : Please check individual destination " + vbCrLf +
                " - delivery_date_2 : a date value for delivery date in dd.mm.YYYY format, label is DELIVERY DATE , example : 18.11.2025 " + vbCrLf +
                " - terms_of_del : international delivery terms code , example DDP or FOB or FOT " + vbCrLf +
                " - shipment_mode : shipment vehicle , example TRUCK or AIR or SHIP " + vbCrLf +
                " - from : exporting country name , example : Egypt " + vbCrLf +
                " - to : importing country name , example : Turkey " + vbCrLf +
                " - terms_of_payment : payment due code , example : 90G for 90 days " + vbCrLf +
                " - foreign_amount : order amount , decimal seperator as comma , example : 4.460,40 " + vbCrLf +
                " - domestic_amount : order amount , decimal seperator as comma , example : 4.460,40 " + vbCrLf +
                " - supplier_address : address of the supplier , may be multiline , example : MISIR " + vbCrLf +
                " do not extract the tables in  PURCHASE ORDER section " + vbCrLf

        Prompt = Prompt & vbCrLf &
                " in DESTINATION DETAILS section extract : " + vbCrLf +
                " color_no : color code , example : DN12387 " + vbCrLf +
                " color : name of the color , example : SALINA ANTRA WASH " + vbCrLf +
                " critical : extract all the xassortments tables in DESTINATION DETAILS section starting with Destination Code " + vbCrLf +
                " ALL xassortments With their size distribution tables. Each assortment MUST include: " + vbCrLf +
                " - assortment_code : The specific assortment code , example : RU_WLEG11" + vbCrLf +
                " - destination_code : Destination/ market code , example : FRT.LF.W  " + vbCrLf +
                " - destination_description : Full Destination / market description , example : FRT Diğer Loft " + vbCrLf +
                " - delivery_date : Delivery Date for this assortment example : 30.01.2026 " + vbCrLf +
                " - payment_term : payment due date code , example : 90G " + vbCrLf +
                " - table Size distribution table structure is like this:  " + vbCrLf

        Prompt = Prompt & vbCrLf &
                " * column_headers_row is the first row of the table, first value of the row is blank or empty, last value is TOTAL, " +
                " in between values are size names, only first value is empty or blank and all other values are never blank Or empty. " + vbCrLf

        Prompt = Prompt & vbCrLf &
                " * data_rows are the array of rows whose column values are quantities per size, size_name Is the first value of the row " +
                " and is the name of the size, last value of the row Is the total of all the values in between row header and total column , " +
                " in data_rows[i].quantities, align quantities to the header sizes. Use 0 for truly empty/blank/whitespace cells; never invent values. " +
                " Critical rule about empty/blank/whitespace: If a table cell is empty/blank/whitespace, output 0. " +
                " Numbers: Accept both comma and dot decimal formats and return as numbers (e.g., 4.460,40 → 4460.40). " + vbCrLf

        Prompt = Prompt & vbCrLf &
                " * column_totals_row is the last row of the table. " +
                " total_label is first value of the row and has a value of 'TOTAL:' always. " +
                " total_row_total is the last value of the row and is the total of all the values in between row header and total column. " +
                " total_quantities are the values between total_label (alwyas have TOTAL: value) and total_row_total. " + vbCrLf

        Prompt = Prompt & vbCrLf &
                " All numeric values in xassortments tables are integer. " + vbCrLf +
                " All rows of a xassortments table always has the same number of columns " + vbCrLf +
                " all columns have a 'TOTAL:' labeled last row , add the values extracted from that column and compare it with TOTAL cell value for that colum ensure it all sums up" +
                " all rows have a 'TOTAL' labeled last column , add the values extracted from that row and compare it with TOTAL cell for that row ensure it all sums up" + vbCrLf +
                " do not extract anything after SIZE ASSORTED label " + vbCrLf +
                " Return ONLY valid JSON without markdown formatting. " + vbCrLf +
                " Do Not summarize Or omit any details—extract everything verbatim where possible. " +
                " Output only the extracted information strictly in the following JSON structure, with no additional text, explanations, Or deviations. " +
                " Ensure the JSON Is valid And complete. " +
                " Trim currency/units/symbols; preserve units separately only if present as labels. " +
                " Dates: Keep as found (string). " +
                " Whitespace & repeats: Normalize whitespace; if a field repeats in the PDF, prefer the most specific/structured occurrence. "
    End Sub

    Public Function GetJsonSchema() As String
        ' Convert the schema to JSON string
        Return Newtonsoft.Json.JsonConvert.SerializeObject(JsonSchema, Newtonsoft.Json.Formatting.Indented)
    End Function

    Public Function GetPrompt() As String
        ' Return the prompt text
        Return Prompt
    End Function

End Class
