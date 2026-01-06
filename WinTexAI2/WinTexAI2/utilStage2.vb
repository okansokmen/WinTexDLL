Option Explicit On
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module utilStage2

    Private Const PR_SENT_REPRESENTING_ENTRYID As String = "http://schemas.microsoft.com/mapi/proptag/0x00410102"

    Dim cCustomer As String = ""
    Dim nDatabase As Integer = 0

    Public Function Stage2(cFileName As String, pdfPath As String, ByRef jsonOutput As String, ByVal cCustomer1 As String, nDatabase1 As Integer) As String

        Stage2 = ""

        Try
            Dim cMessage As String = ""

            cCustomer = cCustomer1.Trim
            nDatabase = nDatabase1

            If String.IsNullOrEmpty(cCustomer) Then
                cMessage &= vbCrLf & "Error: Customer name is empty."
                Stage2 = cMessage
                Exit Function
            End If

            ' Test API connectivity
            If Not TestApiConnectivity(cMessage) Then
                cMessage &= vbCrLf & "Error API connectivity test failed. Check API key and endpoint."
                Stage2 = cMessage
                Exit Function
            End If

            Cursor.Current = Cursors.WaitCursor

            ' Upload PDF to Gemini File API
            cMessage &= vbCrLf & "Uploading PDF " + cFileName
            Dim fileUri As String = UploadPdf(pdfPath, cFileName, cMessage)
            If String.IsNullOrEmpty(fileUri) Then
                cMessage &= vbCrLf & "Error uploading PDF."
                Stage2 = cMessage
                Exit Function
            End If
            cMessage &= vbCrLf & "PDF uploaded successfully. File URI: " & fileUri

            ' Generate JSON from PDF using Gemini API
            cMessage &= vbCrLf & "Generating JSON..."
            jsonOutput = GenerateJsonFromPdf(fileUri, cMessage)
            If Not String.IsNullOrEmpty(jsonOutput) Then
                ' Format JSON for display
                Dim parsedJson As JObject = JObject.Parse(jsonOutput)
                cMessage &= vbCrLf & parsedJson.ToString(Formatting.Indented)
            Else
                cMessage &= vbCrLf & "Error generating JSON."
            End If

            Stage2 = cMessage

            Cursor.Current = Cursors.Default

        Catch ex As System.Exception
            ErrDisp(ex.Message, "Stage2", , , ex)
        End Try
    End Function

    Private Function UploadPdf(pdfPath As String, displayName As String, Optional ByRef cMessage As String = "") As String
        Try
            ' Read PDF file bytes
            Dim pdfBytes As Byte() = File.ReadAllBytes(pdfPath)
            Dim numBytes As Long = pdfBytes.Length

            ' Step 1: Initiate resumable upload
            Dim uploadUrl As String = $"https://generativelanguage.googleapis.com/upload/v1beta/files?key={cGoogleApiKey}"
            Dim request As HttpWebRequest = CType(WebRequest.Create(uploadUrl), HttpWebRequest)
            request.Method = "POST"
            request.Headers.Add("X-Goog-Upload-Protocol", "resumable")
            request.Headers.Add("X-Goog-Upload-Command", "start")
            request.Headers.Add("X-Goog-Upload-Header-Content-Length", numBytes.ToString())
            request.Headers.Add("X-Goog-Upload-Header-Content-Type", "application/pdf")
            request.ContentType = "application/json"

            Dim requestBody As String = JsonConvert.SerializeObject(New With {
                .file = New With {
                    .displayName = displayName
                }
            })
            Using streamWriter As New StreamWriter(request.GetRequestStream())
                streamWriter.Write(requestBody)
            End Using

            Dim response As HttpWebResponse
            Try
                response = CType(request.GetResponse(), HttpWebResponse)
            Catch ex As WebException
                Dim errorResponse As HttpWebResponse = TryCast(ex.Response, HttpWebResponse)
                If errorResponse IsNot Nothing Then
                    Using reader As New StreamReader(errorResponse.GetResponseStream())
                        cMessage &= vbCrLf & "Upload error response: " & reader.ReadToEnd()
                    End Using
                End If
                Throw
            End Try

            Dim uploadLocation As String = response.Headers("X-Goog-Upload-URL")
            response.Close()

            If String.IsNullOrEmpty(uploadLocation) Then
                cMessage &= vbCrLf & "Error: No upload URL returned."
                Return Nothing
            End If

            ' Step 2: Upload the PDF bytes
            request = CType(WebRequest.Create(uploadLocation), HttpWebRequest)
            request.Method = "POST"
            request.Headers.Add("X-Goog-Upload-Command", "upload, finalize")
            request.Headers.Add("X-Goog-Upload-Offset", "0")
            request.ContentLength = numBytes

            Using stream As Stream = request.GetRequestStream()
                stream.Write(pdfBytes, 0, pdfBytes.Length)
            End Using

            Try
                response = CType(request.GetResponse(), HttpWebResponse)
            Catch ex As WebException
                Dim errorResponse As HttpWebResponse = TryCast(ex.Response, HttpWebResponse)
                If errorResponse IsNot Nothing Then
                    Using reader As New StreamReader(errorResponse.GetResponseStream())
                        cMessage &= vbCrLf & "Upload finalize error response: " & reader.ReadToEnd()
                    End Using
                End If
                Throw
            End Try

            Dim responseStream As Stream = response.GetResponseStream()
            Dim responseText As String
            Using reader As New StreamReader(responseStream)
                responseText = reader.ReadToEnd()
            End Using
            response.Close()

            ' Parse the file URI from response
            Dim jsonResponse As JObject = JObject.Parse(responseText)
            Return jsonResponse("file")("uri").ToString()

        Catch ex As System.Exception
            cMessage &= vbCrLf & "Upload error: " & ex.Message
            Return Nothing
        End Try
    End Function

    Private Function GenerateJsonFromPdf(fileUri As String, Optional ByRef cMessage As String = "") As String
        Try
            Dim oSQL As New SQLServerClass
            Dim cPrompt As String = "Please generate a JSON representation of the PDF content."
            Dim cJsonSchema As String = GetOrderJsonSchema()
            Dim jsonSchema As Object = Nothing ' Define your JSON schema here if needed

            Dim requestUrl As String = $"{cGoogleBaseUrl}/models/{cGoogleModel}:generateContent?key={cGoogleApiKey}"
            Dim request As HttpWebRequest = CType(WebRequest.Create(requestUrl), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"

            oSQL.init(nDatabase)
            oSQL.OpenConn()
            oSQL.cSQLQuery = "SELECT top 1 aiprompt, aijson " +
                            " FROM firma with (NOLOCK) " +
                            " WHERE firma = '" + cCustomer + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cPrompt = oSQL.SQLReadString("aiprompt")
                If oSQL.SQLReadString("aijson") <> "" Then
                    cJsonSchema = oSQL.SQLReadString("aijson")
                End If
            End If
            oSQL.oReader.Close()
            oSQL.CloseConn()

            jsonSchema = JsonConvert.DeserializeObject(Of Object)(cJsonSchema)

            'Dim oSiparisLoft As New SiparisLoft
            'Dim cPrompt As String = oSiparisLoft.Prompt
            'Dim jsonSchema As Object = oSiparisLoft.JsonSchema

            ' Create request body
            Dim requestBody As Object = New With {
                .contents = New Object() {New With {
                    .parts = New Object() {
                        New With {
                            .fileData = New With {
                                .mimeType = "application/pdf",
                                .fileUri = fileUri
                            }
                        },
                        New With {
                            .text = cPrompt
                        }
                    }
                }},
                .generationConfig = New With {
                    .responseMimeType = "application/json",
                    .responseSchema = jsonSchema
                }
            }

            'Dim requestBody As Object = New With {
            '    .contents = New Object() {
            '        New With {
            '            .parts = New Object() {
            '                New With {
            '                    .text = cPrompt & vbCrLf & cJson
            '                }
            '            }
            '        }
            '    },
            '    .generation_config = New With {
            '        .response_mime_type = "application/json",
            '        .response_schema = jsonSchema
            '    }
            '}

            Dim jsonRequest As String = JsonConvert.SerializeObject(requestBody)
            Using streamWriter As New StreamWriter(request.GetRequestStream())
                streamWriter.Write(jsonRequest)
            End Using

            Dim response As HttpWebResponse
            Try
                response = CType(request.GetResponse(), HttpWebResponse)
            Catch ex As WebException
                Dim errorResponse As HttpWebResponse = TryCast(ex.Response, HttpWebResponse)
                If errorResponse IsNot Nothing Then
                    Using reader As New StreamReader(errorResponse.GetResponseStream())
                        cMessage &= vbCrLf & "Generate content error response: " & reader.ReadToEnd()
                    End Using
                End If
                Throw
            End Try

            Dim responseStream As Stream = response.GetResponseStream()
            Dim responseText As String
            Using reader As New StreamReader(responseStream)
                responseText = reader.ReadToEnd()
            End Using
            response.Close()

            ' Extract JSON from response
            Dim jsonResponse As JObject = JObject.Parse(responseText)
            Return jsonResponse("candidates")(0)("content")("parts")(0)("text").ToString()

        Catch ex As System.Exception
            cMessage &= vbCrLf & "Generation error: " & ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetOrderJsonSchema() As String
        Try
            Return "{" & vbCrLf &
                   "  ""type"": ""object""," & vbCrLf &
                   "  ""properties"": {" & vbCrLf &
                   "    ""order_no"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""date"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""season"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""production_group"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""production_main_group"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""gender"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""barcode_item_nr"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""agent_name"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""agent_code"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""preorder_po"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""currency_code"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""unit_price"": {" & vbCrLf &
                   "      ""type"": ""number""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""total_amount"": {" & vbCrLf &
                   "      ""type"": ""number""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""fabric_content"": {" & vbCrLf &
                   "      ""type"": ""array""," & vbCrLf &
                   "      ""items"": {" & vbCrLf &
                   "        ""type"": ""string""" & vbCrLf &
                   "      }" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""address"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""supplier"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""supplier_code"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""style_no"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""delivery_date_1"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""delivery_date_2"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""terms_of_del"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""shipment_mode"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""from"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""to"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""terms_of_payment"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""foreign_amount"": {" & vbCrLf &
                   "      ""type"": ""number""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""domestic_amount"": {" & vbCrLf &
                   "      ""type"": ""number""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""supplier_address"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""color_no"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""color"": {" & vbCrLf &
                   "      ""type"": ""string""" & vbCrLf &
                   "    }," & vbCrLf &
                   "    ""xassortments"": {" & vbCrLf &
                   "      ""type"": ""array""," & vbCrLf &
                   "      ""items"": {" & vbCrLf &
                   "        ""type"": ""object""," & vbCrLf &
                   "        ""required"": [" & vbCrLf &
                   "          ""Destination_code""," & vbCrLf &
                   "          ""Destination_description""," & vbCrLf &
                   "          ""Delivery_date""," & vbCrLf &
                   "          ""assortment_code""," & vbCrLf &
                   "          ""payment_term""," & vbCrLf &
                   "          ""column_headers_row""," & vbCrLf &
                   "          ""data_rows""," & vbCrLf &
                   "          ""column_totals_row""" & vbCrLf &
                   "        ]," & vbCrLf &
                   "        ""properties"": {" & vbCrLf &
                   "          ""Destination_code"": {" & vbCrLf &
                   "            ""type"": ""string""" & vbCrLf &
                   "          }," & vbCrLf &
                   "          ""Destination_description"": {" & vbCrLf &
                   "            ""type"": ""string""" & vbCrLf &
                   "          }," & vbCrLf &
                   "          ""Delivery_date"": {" & vbCrLf &
                   "            ""type"": ""string""" & vbCrLf &
                   "          }," & vbCrLf &
                   "          ""assortment_code"": {" & vbCrLf &
                   "            ""type"": ""string""" & vbCrLf &
                   "          }," & vbCrLf &
                   "          ""payment_term"": {" & vbCrLf &
                   "            ""type"": ""string""" & vbCrLf &
                   "          }," & vbCrLf &
                   "          ""column_headers_row"": {" & vbCrLf &
                   "            ""type"": ""array""," & vbCrLf &
                   "            ""items"": {" & vbCrLf &
                   "              ""type"": ""string""" & vbCrLf &
                   "            }" & vbCrLf &
                   "          }," & vbCrLf &
                   "          ""data_rows"": {" & vbCrLf &
                   "            ""type"": ""array""," & vbCrLf &
                   "            ""items"": {" & vbCrLf &
                   "              ""type"": ""object""," & vbCrLf &
                   "              ""required"": [" & vbCrLf &
                   "                ""size_name""," & vbCrLf &
                   "                ""quantities""," & vbCrLf &
                   "                ""row_total""" & vbCrLf &
                   "              ]," & vbCrLf &
                   "              ""properties"": {" & vbCrLf &
                   "                ""size_name"": {" & vbCrLf &
                   "                  ""type"": ""string""" & vbCrLf &
                   "                }," & vbCrLf &
                   "                ""quantities"": {" & vbCrLf &
                   "                  ""type"": ""array""," & vbCrLf &
                   "                  ""items"": {" & vbCrLf &
                   "                    ""type"": ""number""" & vbCrLf &
                   "                  }" & vbCrLf &
                   "                }," & vbCrLf &
                   "                ""row_total"": {" & vbCrLf &
                   "                  ""type"": ""number""" & vbCrLf &
                   "                }" & vbCrLf &
                   "              }" & vbCrLf &
                   "            }" & vbCrLf &
                   "          }," & vbCrLf &
                   "          ""column_totals_row"": {" & vbCrLf &
                   "            ""type"": ""object""," & vbCrLf &
                   "            ""required"": [" & vbCrLf &
                   "              ""total_label""," & vbCrLf &
                   "              ""total_quantities""," & vbCrLf &
                   "              ""total_row_total""" & vbCrLf &
                   "            ]," & vbCrLf &
                   "            ""properties"": {" & vbCrLf &
                   "              ""total_label"": {" & vbCrLf &
                   "                ""type"": ""string""" & vbCrLf &
                   "              }," & vbCrLf &
                   "              ""total_quantities"": {" & vbCrLf &
                   "                ""type"": ""array""," & vbCrLf &
                   "                ""items"": {" & vbCrLf &
                   "                  ""type"": ""number""" & vbCrLf &
                   "                }" & vbCrLf &
                   "              }," & vbCrLf &
                   "              ""total_row_total"": {" & vbCrLf &
                   "                ""type"": ""number""" & vbCrLf &
                   "              }" & vbCrLf &
                   "            }" & vbCrLf &
                   "          }" & vbCrLf &
                   "        }" & vbCrLf &
                   "      }" & vbCrLf &
                   "    }" & vbCrLf &
                   "  }," & vbCrLf &
                   "  ""required"": [" & vbCrLf &
                   "    ""order_no""," & vbCrLf &
                   "    ""date""," & vbCrLf &
                   "    ""season""," & vbCrLf &
                   "    ""production_group""," & vbCrLf &
                   "    ""production_main_group""," & vbCrLf &
                   "    ""gender""," & vbCrLf &
                   "    ""barcode_item_nr""," & vbCrLf &
                   "    ""agent_name""," & vbCrLf &
                   "    ""agent_code""," & vbCrLf &
                   "    ""preorder_po""," & vbCrLf &
                   "    ""currency_code""," & vbCrLf &
                   "    ""unit_price""," & vbCrLf &
                   "    ""total_amount""," & vbCrLf &
                   "    ""fabric_content""," & vbCrLf &
                   "    ""address""," & vbCrLf &
                   "    ""supplier""," & vbCrLf &
                   "    ""supplier_code""," & vbCrLf &
                   "    ""style_no""," & vbCrLf &
                   "    ""delivery_date_1""," & vbCrLf &
                   "    ""delivery_date_2""," & vbCrLf &
                   "    ""terms_of_del""," & vbCrLf &
                   "    ""shipment_mode""," & vbCrLf &
                   "    ""from""," & vbCrLf &
                   "    ""to""," & vbCrLf &
                   "    ""terms_of_payment""," & vbCrLf &
                   "    ""foreign_amount""," & vbCrLf &
                   "    ""domestic_amount""," & vbCrLf &
                   "    ""supplier_address""," & vbCrLf &
                   "    ""xassortments""" & vbCrLf &
                   "  ]" & vbCrLf &
                   "}"
        Catch ex As System.Exception
            ErrDisp(ex.Message, "GetOrderJsonSchema", , , ex)
            Return ""
        End Try
    End Function

End Module
