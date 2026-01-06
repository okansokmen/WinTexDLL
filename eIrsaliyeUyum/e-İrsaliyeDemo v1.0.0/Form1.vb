Imports eIrsaliyeUyum.DespatchConnect
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Configuration

Namespace eIrsaliyeUyum
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
			GetConnectionInfo()

		End Sub

		Private Sub GetConnectionInfo()
			txtConnectionTestUri.Text = UtilRoot.oUyumConnect.cUyumEirsaliyeServiceUrl
			txtConnectionTestUserName.Text = UtilRoot.oUyumConnect.cUyumUsername
			txtConnectionTestPassword.Text = UtilRoot.oUyumConnect.cUyumPassword

			'txtConnectionTestUri.Text = If(My.Settings.Default.WebServiceTestUri = "", txtConnectionTestUri.Text, My.Settings.Default.WebServiceTestUri)
			'txtConnectionTestUserName.Text = If(My.Settings.Default.WebServiceTestUsername = "", txtConnectionTestUserName.Text, My.Settings.Default.WebServiceTestUsername)
			'txtConnectionTestPassword.Text = If(My.Settings.Default.WebServiceTestPassword = "", txtConnectionTestPassword.Text, My.Settings.Default.WebServiceTestPassword)

			'txtWebServiceTestPassword.Text = Settings1.Default.WebServiceTestPassword == "" ? txtWebServiceTestPassword.Text : Settings1.Default.WebServiceTestPassword;
			'txtWebServiceTestUrl.Text = Settings1.Default.WebServiceTestUri == "" ? txtWebServiceTestUrl.Text : Settings1.Default.WebServiceTestUri;
			'txtWebServiceLiveUsername.Text = Settings1.Default.WebServiceLiveUsername;
			'txtWebServiceLivePassword.Text = Settings1.Default.WebServiceLivePassword;
			'txtWebServiceLiveUrl.Text = Settings1.Default.WebServiceLiveUri;
		End Sub

		Private Sub btnSaveConnectionTestInfo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveConnectionTestInfo.Click
			If Not Me.btnSaveConnectionTestInfo.IsHandleCreated Then Return

			'AppSettings.Default.TestPassword = txtConnectionTestPassword.Text;
			'AppSettings.Default.TestUserName = txtConnectionTestUserName.Text;
			'Properties.Settings.Default.WebServiceTestUri = txtConnectionTestUri.Text;

			'AppSettings.Default.Save();
		End Sub

		'    public DespatchIntegrationClient CreateClient()
		'    {
		'        var username = "";
		'        var password = "";
		'        var serviceuri = "";
		'        //if (rbtnTestSystem.Checked)
		'        //{
		'            username = txtConnectionTestUserName.Text;
		'            password = txtConnectionTestPassword.Text;
		'            serviceuri = txtConnectionTestUri.Text;
		'        //}
		'        //if (rbtnLiveSystem.Checked)
		'        //{
		'        //    username = txtWebServiceLiveUsername.Text;
		'        //    password = txtWebServiceLivePassword.Text;
		'        //    serviceuri = txtWebServiceLiveUrl.Text;
		'        //}

		'        var client = new DespatchIntegrationClient();
		'        client.Endpoint.Address = new System.ServiceModel.EndpointAddress(serviceuri);
		'        client.ClientCredentials.UserName.UserName = username;
		'        client.ClientCredentials.UserName.Password = password;

		'        return client;
		'    }

		Private Sub btnSendDespatch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSendDespatch.Click
			If Not Me.btnSendDespatch.IsHandleCreated Then Return

			Try
				Dim client = DespatchTasks.Instance.CreateClient()

				Dim despInfo = CreateDespatchInfo()
				Dim despInfos = New DespatchInfo() {despInfo}
				Dim response = client.SendDespatch(despInfos)

				If response.IsSucceded Then
					MessageBox.Show(String.Format("İrsaliye No: {0} İrsaliye UUID: {1} ", response.Value(0).Number, response.Value(0).Id))
					txtDespatchUUID.Text = response.Value(0).Id
					txtDespatchID.Text = response.Value(0).Number
				Else
					MessageBox.Show(response.Message)
				End If
			Catch ex As Exception
				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Public Function CreateDespatchInfo() As DespatchInfo
			Dim despInfo As New DespatchInfo()
			Dim despatch As New DespatchAdviceType()

			'			#Region "İrsaliye Genel Bilgileri"
			'İrsaliye Numarası
			If txtDespatchID.Text <> "" Then
				despatch.ID = New IDType With {.Value = txtDespatchID.Text}
			End If
			'İrsaliye UUID(Ettn)
			If txtDespatchUUID.Text <> "" Then
				despatch.UUID = New UUIDType With {.Value = txtDespatchUUID.Text}
			Else
				Dim uuid = Guid.NewGuid().ToString()
				despatch.UUID = New UUIDType With {.Value = uuid}
			End If

			despatch.IssueDate = New IssueDateType With {.Value = DateTime.Now}
			despatch.IssueTime = New IssueTimeType With {.Value = DateTime.Now}
			despatch.CopyIndicator = New CopyIndicatorType With {.Value = False}
			despatch.ProfileID = New ProfileIDType With {.Value = "TEMELIRSALIYE"}
			despatch.DespatchAdviceTypeCode = New DespatchAdviceTypeCodeType With {.Value = "SEVK"}
			despatch.Note = New NoteType() {
				New NoteType With {.Value = "İş bu sevk irsaliyesi muhteviyatına 7 gün içerisinde itiraz edilmediği taktirde aynen kabul edilmiş sayılır."}
			}
			despatch.OrderReference = New OrderReferenceType() {
				New OrderReferenceType With {
					.ID = New IDType With {.Value = "SIP2018012984"},
					.IssueDate = New IssueDateType With {.Value = DateTime.Now}
				}
			}
			despatch.LineCountNumeric = New LineCountNumericType With {.Value = 2}
			'			#End Region

			'			#Region "Shipment"
			despatch.Shipment = New ShipmentType With {
				.GoodsItem = New GoodsItemType() {
					New GoodsItemType With {
						.Description = New DescriptionType() {
							New DescriptionType With {.Value = "Taşıma özenli olmalı"}
						},
						.Item = New ItemType() {
							New ItemType With {
								.Name = New NameType1 With {.Value = "asdasd"}
							}
						},
						.InvoiceLine = New InvoiceLineType() {
							New InvoiceLineType With {
								.ID = New IDType With {.Value = "1"},
								.LineExtensionAmount = New LineExtensionAmountType With {
									.currencyID = "TRY",
									.Value = 0
								},
								.InvoicedQuantity = New InvoicedQuantityType With {
									.Value = 0,
									.unitCode = "NIU"
								},
								.Item = New ItemType With {
									.Name = New NameType1 With {.Value = "test"}
								},
								.Price = New PriceType With {
									.PriceAmount = New PriceAmountType With {
										.Value = 0,
										.currencyID = "TRY"
									}
								}
							}
						}
					}
				},
				.ShipmentStage = New ShipmentStageType() {
					New ShipmentStageType With {
						.DriverPerson = New PersonType() {
							New PersonType With {
								.FirstName = New FirstNameType With {.Value = "Yunus"},
								.FamilyName = New FamilyNameType With {.Value = "Şimşek"},
								.NationalityID = New NationalityIDType With {.Value = "11111111111"}
							}
						}
					}
				},
				.TransportHandlingUnit = New TransportHandlingUnitType() {
					New TransportHandlingUnitType With {
						.TransportEquipment = New TransportEquipmentType() {
							New TransportEquipmentType With {
								.ID = New IDType With {
									.Value = "66 RM 7737",
									.schemeID = "DORSEPLAKA"
								}
							}
						}
					}
				},
				.ID = New IDType With {.Value = "1"},
				.Delivery = New DeliveryType With {
					.ID = New IDType With {.Value = "1"},
					.Despatch = New DespatchType With {
						.ActualDespatchDate = New ActualDespatchDateType With {.Value = DateTime.Now},
						.ActualDespatchTime = New ActualDespatchTimeType With {.Value = DateTime.Now}
					},
					.CarrierParty = New PartyType With {
						.PartyName = New PartyNameType With {
							.Name = New NameType1 With {.Value = "Deneme"}
						},
						.PartyIdentification = New PartyIdentificationType() {
							New PartyIdentificationType With {
								.ID = New IDType With {
									.schemeID = "VKN",
									.Value = "1234567890"
								}
							}
						},
						.PostalAddress = New AddressType With {
							.Country = New CountryType With {
								.Name = New NameType1 With {.Value = "TÜRKİYE"},
								.IdentificationCode = New IdentificationCodeType With {.Value = "TR"}
							},
							.CityName = New CityNameType With {.Value = "İSTANBUL"},
							.StreetName = New StreetNameType With {.Value = "YILDIZ TEKNİK ÜNİ. DAVUTPAŞA KAMP. TEKNOPARK"},
							.CitySubdivisionName = New CitySubdivisionNameType With {.Value = "ESENLER"},
							.BuildingName = New BuildingNameType With {.Value = "B1"},
							.Room = New RoomType With {.Value = "401"}
						}
					}
				}
			}
			'			#End Region

			'			#Region "DespatchSupplierParty"
			despatch.DespatchSupplierParty = New SupplierPartyType With {
				.Party = New PartyType With {
					.PartyName = New PartyNameType With {
						.Name = New NameType1 With {.Value = "Malların Sevkiyatını Sağlayan Firma"}
					},
					.PartyIdentification = New PartyIdentificationType() {
						New PartyIdentificationType With {
							.ID = New IDType With {
								.schemeID = "VKN",
								.Value = txtGondericiVKN.Text
							}
						}
					},
					.PostalAddress = New AddressType With {
						.Country = New CountryType With {
							.Name = New NameType1 With {.Value = "TÜRKİYE"},
							.IdentificationCode = New IdentificationCodeType With {.Value = "TR"}
						},
						.CityName = New CityNameType With {.Value = "İSTANBUL"},
						.StreetName = New StreetNameType With {.Value = "YILDIZ TEKNİK ÜNİ. DAVUTPAŞA KAMP. TEKNOPARK"},
						.CitySubdivisionName = New CitySubdivisionNameType With {.Value = "ESENLER"},
						.BuildingName = New BuildingNameType With {.Value = "B1"},
						.Room = New RoomType With {.Value = "401"}
					}
				}
			}
			'			#End Region

			'			#Region "DeliveryCustomerParty"
			despatch.DeliveryCustomerParty = New CustomerPartyType With {
				.Party = New PartyType With {
					.PartyName = New PartyNameType With {
						.Name = New NameType1 With {.Value = "Malları Teslim Alan Firma"}
					},
					.PartyIdentification = New PartyIdentificationType() {
						New PartyIdentificationType With {
							.ID = New IDType With {
								.schemeID = "VKN",
								.Value = txtVKNAlici.Text
							}
						}
					},
					.PostalAddress = New AddressType With {
						.Country = New CountryType With {
							.Name = New NameType1 With {.Value = "TÜRKİYE"},
							.IdentificationCode = New IdentificationCodeType With {.Value = "TR"}
						},
						.CityName = New CityNameType With {.Value = "İSTANBUL"},
						.StreetName = New StreetNameType With {.Value = "YILDIZ TEKNİK ÜNİ. DAVUTPAŞA KAMP. TEKNOPARK"},
						.CitySubdivisionName = New CitySubdivisionNameType With {.Value = "ESENLER"},
						.BuildingName = New BuildingNameType With {.Value = "B1"},
						.Room = New RoomType With {.Value = "401"}
					}
				}
			}
			'			#End Region

			'			#Region "BuyerCustomerParty"
			despatch.BuyerCustomerParty = New CustomerPartyType With {
				.Party = New PartyType With {
					.PartyName = New PartyNameType With {
						.Name = New NameType1 With {.Value = "Malların Satın Alımını Sağlayan firma"}
					},
					.PartyIdentification = New PartyIdentificationType() {
						New PartyIdentificationType With {
							.ID = New IDType With {
								.schemeID = "VKN",
								.Value = txtVKNAlici.Text
							}
						}
					},
					.PostalAddress = New AddressType With {
						.Country = New CountryType With {
							.Name = New NameType1 With {.Value = "TÜRKİYE"},
							.IdentificationCode = New IdentificationCodeType With {.Value = "TR"}
						},
						.CityName = New CityNameType With {.Value = "İSTANBUL"},
						.StreetName = New StreetNameType With {.Value = "YILDIZ TEKNİK ÜNİ. DAVUTPAŞA KAMP. TEKNOPARK"},
						.CitySubdivisionName = New CitySubdivisionNameType With {.Value = "ESENLER"},
						.BuildingName = New BuildingNameType With {.Value = "B1"},
						.Room = New RoomType With {.Value = "401"}
					}
				}
			}
			'			#End Region

			'			#Region "SellerSupplierParty"
			despatch.SellerSupplierParty = New SupplierPartyType With {
				.Party = New PartyType With {
					.PartyName = New PartyNameType With {
						.Name = New NameType1 With {.Value = "Malları Satan Firma"}
					},
					.PartyIdentification = New PartyIdentificationType() {
						New PartyIdentificationType With {
							.ID = New IDType With {
								.schemeID = "VKN",
								.Value = txtVKNAlici.Text
							}
						}
					},
					.PostalAddress = New AddressType With {
						.Country = New CountryType With {
							.Name = New NameType1 With {.Value = "TÜRKİYE"},
							.IdentificationCode = New IdentificationCodeType With {.Value = "TR"}
						},
						.CityName = New CityNameType With {.Value = "İSTANBUL"},
						.StreetName = New StreetNameType With {.Value = "YILDIZ TEKNİK ÜNİ. DAVUTPAŞA KAMP. TEKNOPARK"},
						.CitySubdivisionName = New CitySubdivisionNameType With {.Value = "ESENLER"},
						.BuildingName = New BuildingNameType With {.Value = "B1"},
						.Room = New RoomType With {.Value = "401"}
					}
				}
			}
			'			#End Region

			'            
			'            #region OriginatorCustomerParty
			'            despatch.OriginatorCustomerParty = new CustomerPartyType
			'            {
			'                Party = new PartyType
			'                {
			'                    PartyName = new PartyNameType { Name = new NameType1 { Value = "Tüm sürecin başlamasını Sağlayan Alıcı " } },
			'                    PartyIdentification = new PartyIdentificationType[] { new PartyIdentificationType { ID = new IDType { schemeID = "VKN", Value = txtVKNAlici.Text } } },
			'                    PostalAddress = new AddressType
			'                    {
			'                        Country = new CountryType
			'                        {
			'                            Name = new NameType1 { Value = "TÜRKİYE" },
			'                            IdentificationCode = new IdentificationCodeType
			'                            {
			'                                Value = "TR"
			'
			'                            }
			'                        },
			'                        CityName = new CityNameType
			'                        {
			'                            Value = "İSTANBUL"
			'                        },
			'                        StreetName = new StreetNameType { Value = "YILDIZ TEKNİK ÜNİ. DAVUTPAŞA KAMP. TEKNOPARK" },
			'                        CitySubdivisionName = new CitySubdivisionNameType { Value = "ESENLER" },
			'                        BuildingName = new BuildingNameType { Value = "B1" },
			'                        Room = new RoomType { Value = "401" }
			'                    }
			'                }
			'            };
			'            #endregion
			'            
			'			#Region "DespatchLine"
			despatch.DespatchLine = New DespatchLineType() {
				New DespatchLineType With {
					.OrderLineReference = New OrderLineReferenceType With {
						.LineID = New LineIDType With {.Value = "1"}
					},
					.ID = New IDType With {.Value = "1"},
					.Shipment = New ShipmentType() {
						New ShipmentType With {
							.ID = New IDType With {.Value = "1"},
							.Delivery = New DeliveryType With {
								.ID = New IDType With {.Value = "1"}
							}
						}
					},
					.Item = New ItemType With {
						.Name = New NameType1 With {.Value = "1 LT Kola"}
					},
					.DeliveredQuantity = New DeliveredQuantityType With {
						.Value = 10,
						.unitCode = "NIU"
					},
					.OversupplyQuantity = New OversupplyQuantityType With {
						.Value = 0,
						.unitCode = "NIU"
					},
					.OutstandingQuantity = New OutstandingQuantityType With {
						.Value = 0,
						.unitCode = "NIU"
					},
					.OutstandingReason = New OutstandingReasonType() {
						New OutstandingReasonType With {.Value = "Stok Yok"}
					},
					.Note = New NoteType() {
						New NoteType With {.Value = "1 koli 2 gün sonra gelecek"}
					}
				}
			}
			'			#End Region
			despInfo.DespatchAdvice = despatch

			Return despInfo
		End Function

		Public Sub SaveDespatchXml(ByVal despatch As DespatchAdviceType)
			Dim xml As String = CreateXmlFromDespatch(despatch)
			Using sf As New FolderBrowserDialog()
				sf.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
				If sf.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
					Return
				End If

				Dim path As String = sf.SelectedPath

				Dim fileName As String = String.Format("{0}.xml", despatch.ID.Value.ToString())

				Dim fullName As String = System.IO.Path.Combine(path, fileName)

				File.WriteAllText(fullName, xml)
			End Using
		End Sub

		Public Function CreateXmlFromDespatch(ByVal despatch As DespatchAdviceType) As String
			Dim rootAttribute = New XmlRootAttribute("DespatchAdvice") With {
				.Namespace = "urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2",
				.IsNullable = False
			}

			Dim serializer As New XmlSerializer(GetType(DespatchAdviceType), rootAttribute)
			Using mstr As New MemoryStream()
				serializer.Serialize(mstr, despatch, DespatchNamespaces)
				serializer.Serialize(mstr, despatch)
				Return Encoding.UTF8.GetString(mstr.ToArray())
			End Using
		End Function

		'XML serialization sırasında namespace'lerin doğru yapılabilmesi için namespace tanımlamaları
		Private Shared _DespatchNamespaces As XmlSerializerNamespaces

		Public Shared ReadOnly Property DespatchNamespaces As XmlSerializerNamespaces
			Get
				If _DespatchNamespaces Is Nothing Then
					_DespatchNamespaces = New XmlSerializerNamespaces()
					_DespatchNamespaces.Add("", "urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2")
					_DespatchNamespaces.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
					_DespatchNamespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")
					_DespatchNamespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
					_DespatchNamespaces.Add("cctc", "urn:un:unece:uncefact:documentation:2")
					_DespatchNamespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#")
					_DespatchNamespaces.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2")
					_DespatchNamespaces.Add("ubltr", "urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents")
					_DespatchNamespaces.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2")
					_DespatchNamespaces.Add("xades", "http://uri.etsi.org/01903/v1.3.2#")
					_DespatchNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
					_DespatchNamespaces.Add("ns10", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2")
					_DespatchNamespaces.Add("ns11", "urn:oasis:names:specification:ubl:schema:xsd:ReceiptAdvice-2")
					_DespatchNamespaces.Add("ns8", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")
					_DespatchNamespaces.Add("ns9", "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2")
				End If
				Return _DespatchNamespaces

			End Get

		End Property


		Private Sub btnSaveDespatchXml_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveDespatchXml.Click
			If Not Me.btnSaveDespatchXml.IsHandleCreated Then Return

			Dim despInfo As DespatchInfo = CreateDespatchInfo()
			Dim despatch As DespatchAdviceType = despInfo.DespatchAdvice 'Metottan dönen irsaliye diye düşünebilrisiniz bunu. Aynı tipte veri dönüyor size
			SaveXml(despatch)

		End Sub

		Public Sub SaveXml(ByVal despatch As DespatchAdviceType)
			Dim xml As String = CreateXmlFromDespatch(despatch)
			Using sf As New FolderBrowserDialog()
				sf.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
				If sf.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
					Return
				End If

				Dim path As String = sf.SelectedPath

				Dim fileName As String = String.Format("{0}.xml", despatch.UUID.Value.ToString())

				Dim fullName As String = System.IO.Path.Combine(path, fileName)

				File.WriteAllText(fullName, xml)
			End Using
		End Sub

		Private Sub btnCreateDespatchFromDespatchXML_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreateDespatchFromDespatchXML.Click
			If Not Me.btnCreateDespatchFromDespatchXML.IsHandleCreated Then Return

			CreateDespatchFromXml()
		End Sub

		Public Sub CreateDespatchFromXml()
			Dim array() As Byte
			Dim filename As String
			Dim rootAttribute = New XmlRootAttribute("DespatchAdvice") With {
				.Namespace = "urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2",
				.IsNullable = False
			}

			Dim client = DespatchTasks.Instance.CreateClient()
			Using openFileDialog1 As New OpenFileDialog()
				If openFileDialog1.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
					Return
				End If
				filename = openFileDialog1.FileName
				array = File.ReadAllBytes(filename)

				Dim serializer As New XmlSerializer(GetType(DespatchAdviceType), rootAttribute)
				Dim ms As New MemoryStream(array)

				Dim read = New XmlTextReader(ms)
				Dim obj = serializer.Deserialize(read)
				Dim despatch = TryCast(obj, DespatchAdviceType)
				Dim despatcInfos() As DespatchInfo = {
					New DespatchInfo With {.DespatchAdvice = despatch}
				}

				Dim response = client.SendDespatch(despatcInfos)
				If response.IsSucceded Then
					MessageBox.Show(String.Format("İrsaliye Gönderildi" & vbLf & " UUID:{0} " & vbLf & " ID:{1} ", response.Value(0).Id.ToString(), response.Value(0).Number.ToString()))
					'txtSampleOutboxGuid.Text = response.Value[0].Id.ToString();
					'txtSampleGuid.Text = response.Value[0].Id.ToString();
				Else
					MessageBox.Show(response.Message)
				End If
			End Using

		End Sub

		Private Sub btnPreviewDespatch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPreviewDespatch.Click
			If Not Me.btnPreviewDespatch.IsHandleCreated Then Return

			'var invoiceInfo = CreateInvoice();
			'var invoice = new InvoiceType[1];
			'invoice[0] = invoiceInfo.Invoice;
			'frmInvoiceViewer frm = new frmInvoiceViewer(invoice);
			'frm.Show();
		End Sub

		Private Sub btnCreateDespatchFromDespatchInfoXml_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreateDespatchFromDespatchInfoXml.Click
			If Not Me.btnCreateDespatchFromDespatchInfoXml.IsHandleCreated Then Return

			Dim array() As Byte
			Dim filename As String
			Try
				Using openFileDialog1 As New OpenFileDialog()
					If openFileDialog1.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
						Return
					End If
					filename = openFileDialog1.FileName
					array = File.ReadAllBytes(filename)

					Dim serializer As New XmlSerializer(GetType(DespatchInfo))
					Dim ms As New MemoryStream(array)

					Dim read = New XmlTextReader(ms)
					Dim obj = serializer.Deserialize(read)
					Dim despatch = TryCast(obj, DespatchAdviceType)
					Dim despatcInfos() As DespatchInfo = {
						New DespatchInfo With {.DespatchAdvice = despatch}
					}
					Dim client = DespatchTasks.Instance.CreateClient()
					Dim response = client.SendDespatch(despatcInfos)
					If response.IsSucceded Then
						MessageBox.Show(String.Format("İrsaliye Gönderildi" & vbLf & " UUID:{0} " & vbLf & " ID:{1} ", response.Value(0).Id.ToString(), response.Value(0).Number.ToString()))
						'txtSampleOutboxGuid.Text = response.Value[0].Id.ToString();
						'txtSampleGuid.Text = response.Value[0].Id.ToString();
					Else
						MessageBox.Show(response.Message)
					End If
				End Using
			Catch ex As Exception
				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btn_QueryDespatchStatus_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnQueryOutboxDespatchStatus.Click
			If Not Me.btnQueryOutboxDespatchStatus.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()
			Try

				If txtDespatchUUID.Text.Length = 36 Then
					Dim response = client.QueryOutboxDespatchStatus(New String() {txtDespatchUUID.Text})
					If response.IsSucceded Then
						MessageBox.Show(response.Value(0).StatusCode.ToString())

					End If

				Else
					MessageBox.Show("Id girmediniz ya da girdiğiniz ID GUID formatına uygun değil ")
				End If
			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btn_GetInboxDespacthList_Click(ByVal sender As Object, ByVal e As EventArgs)
			Dim client = DespatchTasks.Instance.CreateClient()
			Dim response = client.GetInboxDespatchList(New InboxDespatchListQueryModel With {
				.PageIndex = 0,
				.PageSize = 10
			})

			Dim a = response.Value.Items(0).DespatchId
		End Sub

		Private Sub btn_GetOutboxDespatchStatusWithLogs_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetOutboxDespatchStatusWithLogs.Click
			If Not Me.btnGetOutboxDespatchStatusWithLogs.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()

			Try
				If txtDespatchUUID.Text.Length = 36 Then
					Dim response = client.GetOutboxDespatchStatusWithLogs(New String() {txtDespatchUUID.Text})
					If response.IsSucceded Then

						Dim sb As New StringBuilder()
						sb.AppendLine(String.Format("StatusCode : {0}", response.Value(0).StatusCode.ToString()))
						sb.AppendLine(String.Format("Status : {0}", response.Value(0).StatusEnum.ToString()))
						sb.AppendLine(String.Format("UUID : {0}", response.Value(0).DespatchId.ToString()))
						If response.Value IsNot Nothing AndAlso response.Value(0).Logs IsNot Nothing Then
							Dim i As Integer = 0
							Do While i < response.Value(0).Logs.Length
								sb.AppendLine(String.Format("Log {0} : {1}", i, response.Value(0).Logs(i).Message))
								i += 1
							Loop

						Else
							sb.AppendLine("Henüz bir Log yok. Bir süre sonra tekrar kontrol edebilirsiniz.")
						End If
						MessageBox.Show(sb.ToString())
					Else
						MessageBox.Show(response.Message)
					End If


				Else
					MessageBox.Show("Id girmediniz ya da girdiğiniz ID GUID formatına uygun değil ")
				End If
			Catch ex As Exception
				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btn_SaveDraftDespatch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveDraftDespatch.Click
			If Not Me.btnSaveDraftDespatch.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()
			Dim despInfo = CreateDespatchInfo()
			Dim despInfos = New DespatchInfo() {despInfo}
			Try
				Dim response = client.SaveAsDraft(despInfos)

				If response.IsSucceded Then
					MessageBox.Show(String.Format("İrsaliye No: {0} İrsaliye UUID: {1} ", response.Value(0).Number, response.Value(0).Id))
					txtDespatchUUID.Text = response.Value(0).Id
					txtDespatchID.Text = response.Value(0).Number
				Else
					MessageBox.Show(response.Message)
				End If
			Catch ex As Exception
				MessageBox.Show(ex.Message)
			End Try

		End Sub

		Private Sub btnSendDraft_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSendDraft.Click
			If Not Me.btnSendDraft.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()
			Try
				Dim response = client.SendDraft(New String() {txtDespatchUUID.Text})
				If response.IsSucceded Then
					MessageBox.Show("Taslak gönderilmek üzere kuyruğa alındı")
				Else
					MessageBox.Show(response.Message)
				End If

			Catch ex As Exception
				MessageBox.Show(ex.Message)
			End Try

		End Sub

		Private Sub btnCancelDraft_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelDraft.Click
			If Not Me.btnCancelDraft.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()
			Try
				Dim response = client.CancelDraft(New String() {txtDespatchUUID.Text})
				If response.IsSucceded Then
					MessageBox.Show("Taslak İptal edildi")
				Else
					MessageBox.Show(response.Message)
				End If

			Catch ex As Exception
				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btnQueryInboxDespatchStatus_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnQueryInboxDespatchStatus.Click
			If Not Me.btnQueryInboxDespatchStatus.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()
			Try

				If txtDespatchUUID.Text.Length = 36 Then
					Dim response = client.QueryInboxDespatchStatus(New String() {txtDespatchUUID.Text})
					If response.IsSucceded Then
						MessageBox.Show(response.Value(0).StatusCode.ToString())
					End If

				Else
					MessageBox.Show("Id girmediniz ya da girdiğiniz ID GUID formatına uygun değil ")
				End If
			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btnGetInboxDespatchStatusWithLogs_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetInboxDespatchStatusWithLogs.Click
			If Not Me.btnGetInboxDespatchStatusWithLogs.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()

			Try
				If txtDespatchUUID.Text.Length = 36 Then
					Dim response = client.GetInboxDespatchStatusWithLogs(New String() {txtDespatchUUID.Text})
					If response.IsSucceded Then

						Dim sb As New StringBuilder()
						sb.AppendLine(String.Format("StatusCode : {0}", response.Value(0).StatusCode.ToString()))
						sb.AppendLine(String.Format("Status : {0}", response.Value(0).StatusEnum.ToString()))
						sb.AppendLine(String.Format("UUID : {0}", response.Value(0).DespatchId.ToString()))
						If response.Value IsNot Nothing AndAlso response.Value(0).Logs IsNot Nothing Then
							Dim i As Integer = 0
							Do While i < response.Value(0).Logs.Length
								sb.AppendLine(String.Format("Log {0} : {1}", i, response.Value(0).Logs(i).Message))
								i += 1
							Loop

						Else
							sb.AppendLine("Henüz bir Log yok. Bir süre sonra tekrar kontrol edebilirsiniz.")
						End If
						MessageBox.Show(sb.ToString())
					Else
						MessageBox.Show(response.Message)
					End If


				Else
					MessageBox.Show("Id girmediniz ya da girdiğiniz ID GUID formatına uygun değil ")
				End If
			Catch ex As Exception
				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btnCompressedSendDespatch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCompressedSendDespatch.Click
			If Not Me.btnCompressedSendDespatch.IsHandleCreated Then Return

			Dim file() As Byte = Nothing
			Dim client = DespatchTasks.Instance.CreateClient()
			Try
				Dim response = client.CompressedSendDespatch(New BinaryRequestData With {
					.Data = file,
					.Hash = ""
				})

			Catch ex As Exception
				MessageBox.Show(ex.Message)

			End Try
		End Sub

		Private Sub btnGetInboxDespatch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetInboxDespatch.Click
			If Not Me.btnGetInboxDespatch.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()
			Dim sb As New StringBuilder()
			Try
				Dim response = client.GetInboxDespatch(txtDespatchUUID.Text)
				If response.IsSucceded Then
					sb.AppendLine("UUID: " & response.Value.DespatchAdvice.UUID.Value)
					sb.AppendLine("ID: " & response.Value.DespatchAdvice.ID.Value)
					sb.AppendLine("Gönderici: " & response.Value.DespatchAdvice.DespatchSupplierParty.Party.PartyName.Name.Value)
				End If
			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
			MessageBox.Show(sb.ToString())

			'FrmInfo  frm = new FrmInfo(9);
			'frm.Show();
		End Sub

		Private Sub btnGetOutboxDespatch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetOutboxDespatch.Click
			If Not Me.btnGetOutboxDespatch.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()
			Dim sb As New StringBuilder()
			Try
				Dim response = client.GetOutboxDespatch(txtDespatchUUID.Text)
				If response.IsSucceded Then

					sb.AppendLine("UUID: " & response.Value.DespatchAdvice.UUID.Value)
					sb.AppendLine("ID: " & response.Value.DespatchAdvice.ID.Value)
					sb.AppendLine("Alıcı: " & response.Value.DespatchAdvice.BuyerCustomerParty.Party.PartyName.Name.Value)
				Else
					MessageBox.Show(response.Message)
				End If
			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
			MessageBox.Show(sb.ToString())
		End Sub

		Private Sub btnGetInboxDespatches_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetInboxDespatches.Click
			If Not Me.btnGetInboxDespatches.IsHandleCreated Then Return


			Dim client = DespatchTasks.Instance.CreateClient()
			Try
				Dim response = client.GetInboxDespatches(New InboxDespatchQueryModel With {.OnlyNewestDespatches = True})
				If response.IsSucceded Then
					MessageBox.Show(String.Format("Metot başarıyla çağrıldı. {0} kayıt döndü.", response.Value.TotalCount))
				Else
					MessageBox.Show(response.Message)
				End If
			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btnGetInboxDespatchList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetInboxDespatchList.Click
			If Not Me.btnGetInboxDespatchList.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()
			Try
				Dim response = client.GetInboxDespatchList(New InboxDespatchListQueryModel With {.OnlyNewestDespatches = True})
				If response.IsSucceded Then
					MessageBox.Show(String.Format("Metot başarıyla çağrıldı. {0} kayıt döndü.", response.Value.TotalCount))
				Else
					MessageBox.Show(response.Message)
				End If
			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btnGetOutboxDespatchList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetOutboxDespatchList.Click
			If Not Me.btnGetOutboxDespatchList.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()
			Try
				Dim response = client.GetOutboxDespatchList(New OutboxDespatchListQueryModel With {.PageIndex = 0})
				If response.IsSucceded Then
					MessageBox.Show(String.Format("Metot başarıyla çağrıldı. {0} kayıt döndü.", response.Value.TotalCount))
				Else
					MessageBox.Show(response.Message)
				End If
			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btnGetOutboxDespatches_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetOutboxDespatches.Click
			If Not Me.btnGetOutboxDespatches.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()
			Try
				Dim response = client.GetOutboxDespatches(New OutboxDespatchQueryModel With {.PageIndex = 0})
				If response.IsSucceded Then
					MessageBox.Show(String.Format("Metot başarıyla çağrıldı. {0} kayıt döndü.", response.Value.TotalCount))
				Else
					MessageBox.Show(response.Message)
				End If
			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btnSendDespatchResponse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSendDespatchResponse.Click
			If Not Me.btnSendDespatchResponse.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()
			Try
				Dim response = client.SendReceiptAdvice(New ReceiptAdviceInfo() {
					New ReceiptAdviceInfo With {
						.ReceiptAdviceLineInfos = New ReceiptAdviceLineInfo() {
							New ReceiptAdviceLineInfo With {
								.LineId = "1",
								.ReceivedQuantity = 9,
								.OversupplyQuantity = 0,
								.RejectReason = "Delik",
								.RejectedQuantity = 1,
								.RejectReasonCode = "",
								.ShortQuantity = 1
							}
						},
						.ActualDeliveryDate = New DateTime(2018, 1, 1), .DeliveryContactName = "asdasdas", .DespatchContactName = "asdasdasd", .InboxDespatchId = txtDespatchUUID.Text, .Note = "1 Adet Eksik 1 Adet de kusurlu olduğu tespit edilmiştir"
					}
				})
				If response.IsSucceded Then
					MessageBox.Show("response gönderimi başarılı")
				Else
					MessageBox.Show(response.Message)
				End If

			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btnGetInboxDespatchPdf_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetInboxDespatchPdf.Click
			If Not Me.btnGetInboxDespatchPdf.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()

			Try
				If txtDespatchUUID.Text = "" Then
					MessageBox.Show("Örnek UUID girmediniz")
				Else
					Dim response = client.GetInboxDespatchPdf(txtDespatchUUID.Text)
					If response.IsSucceded Then

						Using stream = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & txtDespatchUUID.Text & ".pdf")
							stream.Write(response.Value.Items(0).Data, 0, response.Value.Items(0).Data.Length)
							stream.Flush()
							MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & txtDespatchUUID.Text & ".pdf adresine kaydedildi")
						End Using
					Else
						MessageBox.Show(response.Message)
					End If
				End If

			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try

		End Sub

		Private Sub GetInboxDespatchView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles GetInboxDespatchView.Click
			If Not Me.GetInboxDespatchView.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()

			Try
				If txtDespatchUUID.Text = "" Then
					MessageBox.Show("Örnek UUID girmediniz")
				Else
					Dim response = client.GetInboxDespatchView(txtDespatchUUID.Text)
					If response.IsSucceded Then

						System.IO.File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & txtDespatchUUID.Text & ".html", response.Value.Html)
						MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & txtDespatchUUID.Text & ".html adresine kaydedildi")
					Else
						MessageBox.Show(response.Message)
					End If
				End If

			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btnGetInboxDespatchData_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetInboxDespatchData.Click
			If Not Me.btnGetInboxDespatchData.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()

			Try
				If txtDespatchUUID.Text = "" Then
					MessageBox.Show("Örnek UUID girmediniz")
				Else
					Dim response = client.GetInboxDespatchesData(New InboxDespatchQueryModel With {
						.DespatchIds = New String() {txtDespatchUUID.Text}
					})
					If response.IsSucceded Then
						Using stream = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & txtDespatchUUID.Text & ".xml")
							stream.Write(response.Value.Items(0).Data, 0, response.Value.Items(0).Data.Length)
							stream.Flush()
							MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & txtDespatchUUID.Text & ".xml adresine kaydedildi")
						End Using
					Else
						MessageBox.Show(response.Message)
					End If
				End If

			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btnGetOutboxDespatchPdf_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetOutboxDespatchPdf.Click
			If Not Me.btnGetOutboxDespatchPdf.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()

			Try
				If txtDespatchUUID.Text = "" Then
					MessageBox.Show("Örnek UUID girmediniz")
				Else
					Dim response = client.GetOutboxDespatchPdf(txtDespatchUUID.Text)
					If response.IsSucceded Then
						Using stream = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & txtDespatchUUID.Text & ".pdf")
							stream.Write(response.Value.Items(0).Data, 0, response.Value.Items(0).Data.Length)
							stream.Flush()
							MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & txtDespatchUUID.Text & ".pdf adresine kaydedildi")
						End Using
					Else
						MessageBox.Show(response.Message)
					End If
				End If

			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try

		End Sub

		Private Sub btnGetOutboxDespatchData_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetOutboxDespatchData.Click
			If Not Me.btnGetOutboxDespatchData.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()

			Try
				If txtDespatchUUID.Text = "" Then
					MessageBox.Show("Örnek UUID girmediniz")
				Else
					Dim response = client.GetOutboxDespatchesData(New OutboxDespatchQueryModel With {
						.DespatchIds = New String() {txtDespatchUUID.Text}
					})
					If response.IsSucceded Then
						Using stream = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & txtDespatchUUID.Text & ".xml")
							stream.Write(response.Value.Items(0).Data, 0, response.Value.Items(0).Data.Length)
							stream.Flush()
							MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & txtDespatchUUID.Text & ".xml adresine kaydedildi")
						End Using
					Else
						MessageBox.Show(response.Message)
					End If
				End If

			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btnGetOutboxDespatchView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetOutboxDespatchView.Click
			If Not Me.btnGetOutboxDespatchView.IsHandleCreated Then Return

			Dim client = DespatchTasks.Instance.CreateClient()

			Try
				If txtDespatchUUID.Text = "" Then
					MessageBox.Show("Örnek UUID girmediniz")
				Else
					Dim response = client.GetOutboxDespatchView(txtDespatchUUID.Text)
					If response.IsSucceded Then
						System.IO.File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & txtDespatchUUID.Text & ".html", response.Value.Html)
						MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & txtDespatchUUID.Text & ".html adresine kaydedildi")
					Else
						MessageBox.Show(response.Message)
					End If
				End If

			Catch ex As Exception
				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub GetOutboxDespatchesList_Click(ByVal sender As Object, ByVal e As EventArgs)
			Dim client = DespatchTasks.Instance.CreateClient()
			Dim response = client.GetOutboxDespatchList(New OutboxDespatchListQueryModel With {.IsOnlyNewReceiptAdvice = True})
		End Sub

		Private Sub btnInboxReponseStatus_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInboxReponseStatus.Click
			If Not Me.btnInboxReponseStatus.IsHandleCreated Then Return

			Try
				Dim client = DespatchTasks.Instance.CreateClient()
				Dim response = client.QueryReceiptAdviceStatus(New String() {txtDespatchUUID.Text})
				If response.IsSucceded Then
					MessageBox.Show(String.Format("Durum Kodu : {0} Durum : {1}", response.Value(0).StatusCode, response.Value(0).Status.ToString()))
				End If
			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub btnGetInboxReceiptAdvices_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetInboxReceiptAdvices.Click
			If Not Me.btnGetInboxReceiptAdvices.IsHandleCreated Then Return

			Try
				Dim client = DespatchTasks.Instance.CreateClient()
				Dim response = client.GetOutboxDespatchList(New OutboxDespatchListQueryModel With {
					.IsOnlyNewReceiptAdvice = True,
					.PageSize = 10,
					.PageIndex = 0
				})
				If response.IsSucceded Then
					If response.Value.Items.Length > 0 Then
						Dim sb As New StringBuilder()

						For i As Integer = 0 To response.Value.Items.Length - 1
							Dim j As Integer = 0
							Do While j < response.Value.Items(i).Lines.Length
								Dim line = response.Value.Items(i).Lines(j)
								sb.Append("Satir" & j & ":")
								sb.Append("DespatchLineId : ")
								sb.Append(line.DespatchLineId)
								sb.Append(" | DespatchId : ")
								sb.Append(line.DespatchId)
								sb.Append(" | DeliveredQuantity : ")
								sb.Append(line.DeliveredQuantity)
								sb.Append(" | ReceivedQuantity : ")
								sb.Append(line.ReceivedQuantity)
								sb.Append(" | OversupplyQuantity : ")
								sb.Append(line.OversupplyQuantity)
								sb.Append(" | RejectedQuantity : ")
								'sb.Append(line.RecejtedQuantity);
								sb.Append(" | RejectReasonCode : ")
								sb.Append(line.DeliveredQuantity)
								sb.Append(" | DeliveredQuantity : ")
								sb.Append(line.DeliveredQuantity)
								sb.Append(" | ItemName : ")
								sb.Append(line.ItemName)
								sb.AppendLine()

								j += 1
							Loop


						Next i
						MessageBox.Show(sb.ToString())
					End If

				Else
					MessageBox.Show(response.Message)
				End If


			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
		End Sub

		Private Sub SetDespatchReceiptAdvicesTaken_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SetDespatchReceiptAdvicesTaken.Click
			If Not Me.SetDespatchReceiptAdvicesTaken.IsHandleCreated Then Return

			Try
				Dim client = DespatchTasks.Instance.CreateClient()
				Dim response = client.SetDespatchReceiptAdvicesTaken(New String() {txtDespatchUUID.Text})

				If response.IsSucceded Then
					MessageBox.Show("İrsaliye ynaıtını aldığınıza dair sisteme bilgi gönderdiniz. Bu irsaliye yanıtı Yeni durumdaki irsaliye yanıtlarını çağırdığınızda artık gelmeyecek")
				Else
					MessageBox.Show(response.Message)
				End If
			Catch ex As Exception

				MessageBox.Show(ex.Message)
			End Try
		End Sub
	End Class

End Namespace
