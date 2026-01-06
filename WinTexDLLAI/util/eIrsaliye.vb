Option Explicit On

Imports WinTexDLL.DespatchConnect

Public Class eIrsaliye
    ' kullanıcı adı: umut@mothouse.com
    ' şifre: Mothouse123
    ' portal link: https://edunya.crssoft.com/Giris 

    Dim cURL As String = ""
    Dim cUsername As String = ""
    Dim cPassword As String = ""

    Public Sub init()

        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            cURL = oSQL.GetSysPar("UrlCrsEirsaliyeService", "https://connect-test.crssoft.com/Services/DespatchIntegration")

            If oConnection.cOwner = "mothouse" Then
                cUsername = oSQL.GetSysPar("CrsUsername", "CrsDemo85")
                cPassword = oSQL.GetSysPar("CrsPassword", "11223385")
            Else
                cUsername = oSQL.GetSysPar("CrsUsername")
                cPassword = oSQL.GetSysPar("CrsPassword")
            End If

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp("initWebService", "eIrsaliye",,, ex)
        End Try
    End Sub

    Public Function SendEIrsaliye(ByVal nCase As Integer, ByVal cFisNo As String, Optional ByVal lPDF As Boolean = False) As Boolean
        ' nCase = 1 Stok Fişi
        ' nCase = 2 Üretim Fişi

        SendEIrsaliye = False

        Try
            Dim client As DespatchIntegrationClient = GetClient()
            Dim response As DespatchIdentitiesResponse = New DespatchIdentitiesResponse()
            Dim despatchInfos() As DespatchInfo = {New DespatchInfo}
            Dim despatchAdvice As DespatchAdviceType = New DespatchAdviceType() 'irsaliye objesi

            Dim despatchSupplierParty = New SupplierPartyType()   ' Malların Sevkiyatını Sağlayan Firma / gönderici firma
            Dim deliveryCustomerParty = New CustomerPartyType()   ' Malları Teslim Alan Firma / alıcı firma 
            Dim SellerSupplierParty = New SupplierPartyType()     ' Malları Satan Firma
            Dim BuyerCustomerParty = New CustomerPartyType()      ' Malların Satın Alımını Sağlayan firma
            Dim OriginatorCustomerParty = New CustomerPartyType() ' Tüm sürecin başlamasını Sağlayan Alıcı (örnekte var ama kullanılmamıştı)

            Dim copyIndicator As CopyIndicatorType = New CopyIndicatorType()
            Dim despatchAdviceTypeCode As DespatchAdviceTypeCodeType = New DespatchAdviceTypeCodeType()
            Dim ublVersion As UBLVersionIDType = New UBLVersionIDType()
            Dim customizationID As CustomizationIDType = New CustomizationIDType()
            Dim profileID As ProfileIDType = New ProfileIDType()
            Dim id As IDType = New IDType()
            Dim uuid As UUIDType = New UUIDType()
            Dim issueDate As IssueDateType = New IssueDateType()
            Dim issueTime As IssueTimeType = New IssueTimeType()
            Dim linecount As LineCountNumericType = New LineCountNumericType() 'irsaliyedeki kalem/satir sayısı
            Dim shipment As ShipmentType = New ShipmentType()
            'Dim despatchLines As Array = Array.CreateInstance(GetType(DespatchLineType), CInt(nLineCount))  'irsaliye satırları

            Dim notSayisi As Integer = 2
            Dim notes As Array = Array.CreateInstance(GetType(NoteType), notSayisi)
            Dim note1 As NoteType = New NoteType()
            Dim note2 As NoteType = New NoteType()

            Dim oSQL1 As New SQLServerClass
            Dim oSQL2 As New SQLServerClass
            Dim nLineNr As Integer = -1
            Dim nLineCount As Integer = 0
            Dim despatchLines As Array = Nothing
            Dim oDespatchLine As DespatchLineType = Nothing
            Dim cKodu As String = ""
            Dim cAdi As String = ""
            Dim nMiktar As Double = 0
            Dim cBirim As String = ""
            Dim cSQL As String = ""
            Dim cTarihSaat As String = ""
            Dim nDay As Integer = 0
            Dim nMonth As Integer = 0
            Dim nYear As Integer = 0
            Dim nHour As Integer = 0
            Dim nMinute As Integer = 0
            Dim cMessage As String = ""
            Dim cF1VN As String = ""
            Dim cF2VN As String = ""
            Dim cF3VN As String = ""
            Dim cF1TC As String = ""
            Dim cF2TC As String = ""
            Dim cF3TC As String = ""
            Dim cSchemeID1 As String = "VKN"
            Dim cSchemeID2 As String = "VKN"
            Dim cSchemeID3 As String = "VKN"
            Dim lTest As Boolean = False
            Dim cFisTipi As String = ""
            Dim cIrsaliyeID As String = ""
            Dim cIrsaliyeNumarasi As String = ""
            Dim cMTF As String = ""
            Dim cSFE As String = ""
            Dim cUFE As String = ""
            Dim cID As String = ""
            Dim lirsdraft As Boolean = False
            Dim cSahis1 As String = ""
            Dim cSoyad1 As String = ""
            Dim cSahis2 As String = ""
            Dim cSoyad2 As String = ""
            Dim cSahis3 As String = ""
            Dim cSoyad3 As String = ""
            Dim cVD1 As String = ""
            Dim cVD2 As String = ""
            Dim cVD3 As String = ""

            oSQL1.OpenConn()
            oSQL2.OpenConn()

            cSFE = oSQL1.GetSysPar("irsservicestok", "Butun satirlar")
            cUFE = oSQL1.GetSysPar("irsserviceuretim", "Butun satirlar")
            If oSQL1.GetSysPar("irsdraft", "0") = "1" Then
                lirsdraft = True
            End If

            If oSQL1.GetSysPar("UrlCrsEirsaliyeService") = "https://connect-test.crssoft.com/Services/DespatchIntegration" Then
                lTest = True
            End If

            ' irsaliye kafası
            ublVersion.Value = "2.1" 'Sabit Değer
            customizationID.Value = "TR1.2.1" 'Sabit Değer
            copyIndicator.Value = False 'Sabit Değer
            despatchAdviceTypeCode.Value = "SEVK" ' çoğu zaman sabit. sadece irsaliye mücbir hallerde önce matbu evraka düzenlenip sornadan e-irsaliyeye dönüştürülürse "MATBUDAN" olması gerekiyor. 
            profileID.Value = "TEMELIRSALIYE" ' TEMELSEVKIRSALIYESI 'Sabit değer
            id.Value = "" 'A012020011111132 İrsaliye Numarası. Boş bırakıldığında sistem üretir ve response'ta döner. 16 hane olmalıdır. Formatı için irsaliye kılavuzlarına bakınız.  
            uuid.Value = Guid.NewGuid().ToString() 'irsaliyeye ait unique ID. boş bırakıldığında sistem üretir ve responseta geri döner. Boş bırakılması response'un alınamadığı senaryolarda takibi imkansız kıldığı için önerilmez. 

            Select Case nCase
                Case 1
                    ' stok fişi
                    cFisTipi = "stokfis"

                    Select Case cSFE
                        Case "Ana stok grubu + stok tipi gruplu"
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(7, cFisNo)
                        Case "Stok kodu gruplu"
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(11, cFisNo)
                        Case Else
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(3, cFisNo)
                    End Select

                    nLineCount = oSQL1.DBReadInteger()

                    despatchLines = Array.CreateInstance(GetType(DespatchLineType), nLineCount) 'irsaliye satırları

                    ' irsaliye satır sayısı
                    linecount.Value = CDec(nLineCount)

                    oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(1, cFisNo)

                    oSQL1.GetSQLReader()

                    If oSQL1.oReader.Read Then

                        'id.Value = oSQL1.SQLReadString("stokfisno")

                        cF1VN = oSQL1.SQLReadString("f1vn")
                        cF1TC = oSQL1.SQLReadString("f1tc")
                        cSahis1 = oSQL1.SQLReadString("f1sahis")
                        cSoyad1 = oSQL1.SQLReadString("f1soyad")

                        cF2VN = oSQL1.SQLReadString("f2vn")
                        cF2TC = oSQL1.SQLReadString("f2tc")
                        cSahis2 = oSQL1.SQLReadString("f2sahis")
                        cSoyad2 = oSQL1.SQLReadString("f2soyad")

                        cF3VN = oSQL1.SQLReadString("f3vn")
                        cF3TC = oSQL1.SQLReadString("f3tc")
                        cSahis3 = oSQL1.SQLReadString("f3sahis")
                        cSoyad3 = oSQL1.SQLReadString("f3soyad")

                        ' vergi dairesi
                        cVD1 = oSQL1.SQLReadString("f1vd")
                        cVD2 = oSQL1.SQLReadString("f2vd")
                        cVD3 = oSQL1.SQLReadString("f3vd")

                        ' TC ler varsa VN yerini alsınlar
                        If cF1TC <> "" Then cF1VN = cF1TC
                        If cF2TC <> "" Then cF2VN = cF2TC
                        If cF3TC <> "" Then cF3VN = cF3TC

                        ' vergi numarasına TC no yazılmış durumdamı ???
                        If cF1VN.Length = 11 Then cSchemeID1 = "TCKN"
                        If cF2VN.Length = 11 Then cSchemeID2 = "TCKN"
                        If cF3VN.Length = 11 Then cSchemeID3 = "TCKN"

                        If lTest Then
                            cF1VN = "2150240232"
                        End If

                        If oSQL1.SQLReadDate("belgetarihi") <> #1/1/1950# Then
                            issueDate.Value = oSQL1.SQLReadDate("belgetarihi") 'irsaliye tarihi
                        ElseIf oSQL1.SQLReadDate("fistarihi") <> #1/1/1950# Then
                            issueDate.Value = oSQL1.SQLReadDate("fistarihi") 'irsaliye tarihi
                        Else
                            issueDate.Value = DateTime.Now() 'irsaliye saati
                        End If

                        issueTime.Value = DateTime.Now() 'irsaliye saati

                        note1.Value = oSQL1.SQLReadString("departman") + " işlemi yapılmak üzere gönderilmiştir. Fatura Edilmeyecektir."
                        note2.Value = oSQL1.SQLReadString("notlar", 100)

                        notes.SetValue(note1, 0)
                        notes.SetValue(note2, 1)

                        '.PartyTaxScheme = New PartyTaxSchemeType With {.TaxScheme = New TaxSchemeType With {.Name = New NameType With {.Value = cVD1}}}

#Region "DespatchSupplierParty"
                        ' Malları sevk even firmaya ait bilgiler
                        despatchSupplierParty =
                        New SupplierPartyType With {
                            .Party = New PartyType With {
                                .PartyIdentification = New PartyIdentificationType() {
                                    New PartyIdentificationType With {
                                    .ID = New IDType With {.Value = cF1VN, .schemeID = cSchemeID1}
                                                                     }
                                    },
                                .PartyName = New PartyNameType With {
                                .Name = New NameType1 With {.Value = oSQL1.SQLReadString("f1adi")}},
                                .PostalAddress = New AddressType With {
                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f1shr")},
                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f1smt")},
                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f1ulk")}},
                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f1adr")},
                                    .PostalZone = New PostalZoneType With {.Value = oSQL1.SQLReadString("f1pk")}
                                    }
                                }
                            }

#End Region

#Region "DeliveryCustomerParty"
                        ' Malları Teslim alan firmaya ait bilgiler
                        deliveryCustomerParty =
                        New CustomerPartyType With {
                            .Party = New PartyType With {
                                .PartyIdentification = New PartyIdentificationType() {
                                    New PartyIdentificationType With {
                                    .ID = New IDType With {.Value = cF2VN, .schemeID = cSchemeID2}
                                                                     }
                                    },
                                .PartyName = New PartyNameType With {
                                .Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2adi")}},
                                .PostalAddress = New AddressType With {
                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f2shr")},
                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f2smt")},
                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2ulk")}},
                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f2adr")},
                                    .PostalZone = New PostalZoneType With {.Value = oSQL1.SQLReadString("f2pk")}
                                    }
                                }
                            }
#End Region

#Region "SellerSupplierParty"
                        SellerSupplierParty =
                        New SupplierPartyType With {
                            .Party = New PartyType With {
                                .PartyIdentification = New PartyIdentificationType() {
                                    New PartyIdentificationType With {
                                    .ID = New IDType With {.Value = cF1VN, .schemeID = cSchemeID1}
                                                                     }
                                    },
                                .PartyName = New PartyNameType With {
                                .Name = New NameType1 With {.Value = oSQL1.SQLReadString("f1adi")}},
                                .PostalAddress = New AddressType With {
                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f1shr")},
                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f1smt")},
                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f1ulk")}},
                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f1adr")},
                                    .PostalZone = New PostalZoneType With {.Value = oSQL1.SQLReadString("f1pk")}
                                    }
                                }
                            }
#End Region

#Region "BuyerCustomerParty"
                        BuyerCustomerParty =
                        New CustomerPartyType With {
                            .Party = New PartyType With {
                                .PartyIdentification = New PartyIdentificationType() {
                                    New PartyIdentificationType With {
                                    .ID = New IDType With {.Value = cF2VN, .schemeID = cSchemeID2}
                                                                     }
                                    },
                                .PartyName = New PartyNameType With {
                                .Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2adi")}},
                                .PostalAddress = New AddressType With {
                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f2shr")},
                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f2smt")},
                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2ulk")}},
                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f2adr")},
                                    .PostalZone = New PostalZoneType With {.Value = oSQL1.SQLReadString("f2pk")}
                                    }
                                }
                            }
#End Region

#Region "Shipment"
                        ' Taşımaya ait bilgiler
                        shipment = New ShipmentType With {
                            .ID = New IDType With {.Value = 1}, 'Schema gereği zorunlu alan. 1 yazılabilir
                            .Delivery = New DeliveryType With {
                                .DeliveryAddress = New AddressType With {.CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f2shr")},
                                            .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f2smt")},
                                            .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2ulk")}},
                                            .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f2adr")},
                                            .PostalZone = New PostalZoneType With {.Value = oSQL1.SQLReadString("f2pk")}},
                                .Despatch = New DespatchType With {
                                            .ActualDespatchDate = New ActualDespatchDateType With {.Value = issueDate.Value}, 'Fiili Sevk Tarihi
                                            .ActualDespatchTime = New ActualDespatchTimeType With {.Value = issueTime.Value}  'Fiili Sevk Saati
                                                                  }
                                },
                            .ShipmentStage = New ShipmentStageType() {
                                New ShipmentStageType With {
                                .TransportMeans = New TransportMeansType With {
                                    .RoadTransport = New RoadTransportType With {
                                        .LicensePlateID = New LicensePlateIDType With {.schemeID = "PLAKA", .Value = oSQL1.SQLReadString("aracplaka")}
                                                                     }}}}}

                        If cF3VN <> "" Then
                            shipment.Delivery.CarrierParty = New PartyType With { 'Taşıyıcı/Kargo veya lojistik firması bilgileri
                                                            .PartyIdentification = New PartyIdentificationType() {
                                                                            New PartyIdentificationType With {
                                                                            .ID = New IDType With {.Value = cF3VN, .schemeID = cSchemeID3}
                                                                                                             }
                                                                            },
                                                            .PartyName = New PartyNameType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f3adi")}},
                                                            .PostalAddress = New AddressType With {
                                                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f3shr")},
                                                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f3smt")},
                                                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f3ulk")}},
                                                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f3adr")},
                                                                    .PostalZone = New PostalZoneType With {.Value = oSQL1.SQLReadString("f3pk")}
                                                                    }}
                        End If

                        If oSQL1.SQLReadString("sofortckn") <> "" Then
                            shipment.ShipmentStage(0).DriverPerson = New PersonType() {
                                                            New PersonType With {.FirstName = New FirstNameType With {.Value = oSQL1.SQLReadString("soforadi")}, 'Şoför Adı
                                                                                 .FamilyName = New FamilyNameType With {.Value = oSQL1.SQLReadString("soforsoyadi")}, 'Şoför Soyadı
                                                                                 .NationalityID = New NationalityIDType With {.Value = oSQL1.SQLReadString("sofortckn")} 'Şoför TC
                                                                                }
                                                                }
                        End If

                        If oSQL1.SQLReadString("dorseplaka") <> "" Then
                            shipment.TransportHandlingUnit = New TransportHandlingUnitType() {
                                                            New TransportHandlingUnitType With {
                                                            .TransportEquipment = New TransportEquipmentType() {
                                                                New TransportEquipmentType With {
                                                                    .ID = New IDType With {.schemeID = "DORSEPLAKA", .Value = oSQL1.SQLReadString("dorseplaka")}}}}
                                                            }
                        End If
#End Region

                    End If
                    oSQL1.oReader.Close()
                    oSQL1.oReader = Nothing

#Region "Satırlar"
                    nLineCount = -1

                    Select Case cSFE
                        Case "Ana stok grubu + stok tipi gruplu"
                            oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(9, cFisNo)
                        Case "Stok kodu gruplu"
                            oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(12, cFisNo)
                        Case Else
                            oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(5, cFisNo)
                    End Select

                    oSQL2.GetSQLReader()

                    Do While oSQL2.oReader.Read

                        Select Case cSFE
                            Case "Ana stok grubu + stok tipi gruplu"
                                cKodu = oSQL2.SQLReadString("anastokgrubu")
                                cID = oSQL2.SQLReadString("stoktipi")
                                cAdi = oSQL2.SQLReadString("stoktipi")
                            Case "Stok kodu gruplu"
                                cKodu = oSQL2.SQLReadString("cinsaciklamasi")
                                cID = oSQL2.SQLReadString("stokno")
                                cAdi = oSQL2.SQLReadString("stokno") ' oSQL2.SQLReadString("anastokgrubu") + "-" + oSQL2.SQLReadString("stoktipi")
                            Case Else
                                cKodu = oSQL2.SQLReadString("anastokgrubu") + "-" + oSQL2.SQLReadString("stoktipi")
                                cID = oSQL2.SQLReadString("stokno")
                                cAdi = oSQL2.SQLReadString("stokno") + " " +
                                    IIf(oSQL2.SQLReadString("renk") = "" Or oSQL2.SQLReadString("renk") = "HEPSI", "", " " + oSQL2.SQLReadString("renk")) +
                                    IIf(oSQL2.SQLReadString("beden") = "" Or oSQL2.SQLReadString("beden") = "HEPSI", "", " " + oSQL2.SQLReadString("beden")) +
                                    IIf(oSQL2.SQLReadString("malzemetakipkodu") = "", "", " " + oSQL2.SQLReadString("malzemetakipkodu"))
                        End Select

                        nMiktar = oSQL2.SQLReadDouble("netmiktar1")

                        cBirim = oSQL2.SQLReadString("birim1")
                        If cBirim.ToLower = "adet" Or cBirim.ToLower = "ad" Then cBirim = "C62"

                        nLineCount = nLineCount + 1

                        oDespatchLine = New DespatchLineType With {' satır 1
                                        .ID = New IDType With {.Value = CStr(nLineCount + 1)}, 'Sıra No
                                        .Item = New ItemType With {
                                            .Name = New NameType1 With {.Value = cAdi}, 'Mal Adı
                                            .SellersItemIdentification = New ItemIdentificationType With {.ID = New IDType With {.Value = cKodu}} 'Satıcı Kodu
                                            },
                                        .DeliveredQuantity = New DeliveredQuantityType With {.unitCode = cBirim, .Value = CDec(nMiktar)}, 'Gönderilen Miktar 
                                        .OrderLineReference = New OrderLineReferenceType With {.LineID = New LineIDType With {.Value = "1"}} 'Sipariş Referansı. 
                                        }

                        despatchLines(nLineCount) = oDespatchLine
                    Loop
                    oSQL2.oReader.Close()
                    oSQL2.oReader = Nothing
#End Region

                Case 2
                    cFisTipi = "uretimfisi"

                    Select Case cUFE
                        Case "Ana model tipi + model no gruplu"
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(8, cFisNo)
                        Case "Siparis gruplu"
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(13, cFisNo)
                        Case Else
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(4, cFisNo)
                    End Select

                    nLineCount = oSQL1.DBReadInteger()

                    despatchLines = Array.CreateInstance(GetType(DespatchLineType), nLineCount)  'irsaliye satırları

                    ' irsaliye satır sayısı
                    linecount.Value = CDec(nLineCount)

                    ' üretim fişi
                    oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(2, cFisNo)

                    oSQL1.GetSQLReader()

                    If oSQL1.oReader.Read Then

                        'id.Value = oSQL1.SQLReadString("uretfisno")

                        cF1VN = oSQL1.SQLReadString("f1vn")
                        cF1TC = oSQL1.SQLReadString("f1tc")
                        cSahis1 = oSQL1.SQLReadString("f1sahis")
                        cSoyad1 = oSQL1.SQLReadString("f1soyad")

                        cF2VN = oSQL1.SQLReadString("f2vn")
                        cF2TC = oSQL1.SQLReadString("f2tc")
                        cSahis2 = oSQL1.SQLReadString("f2sahis")
                        cSoyad2 = oSQL1.SQLReadString("f2soyad")

                        cF3VN = oSQL1.SQLReadString("f3vn")
                        cF3TC = oSQL1.SQLReadString("f3tc")
                        cSahis3 = oSQL1.SQLReadString("f3sahis")
                        cSoyad3 = oSQL1.SQLReadString("f3soyad")

                        ' vergi dairesi
                        cVD1 = oSQL1.SQLReadString("f1vd")
                        cVD2 = oSQL1.SQLReadString("f2vd")
                        cVD3 = oSQL1.SQLReadString("f3vd")

                        ' TC ler varsa VN yerini alsınlar
                        If cF1TC <> "" Then cF1VN = cF1TC
                        If cF2TC <> "" Then cF2VN = cF2TC
                        If cF3TC <> "" Then cF3VN = cF3TC

                        ' vergi numarasına TC no yazılmış durumdamı ???
                        If cF1VN.Length = 11 Then cSchemeID1 = "TCKN"
                        If cF2VN.Length = 11 Then cSchemeID2 = "TCKN"
                        If cF3VN.Length = 11 Then cSchemeID3 = "TCKN"

                        If lTest Then
                            cF1VN = "2150240232"
                        End If

                        If oSQL1.SQLReadDate("belgetarihi") <> #1/1/1950# Then
                            issueDate.Value = oSQL1.SQLReadDate("belgetarihi") 'irsaliye tarihi
                        ElseIf oSQL1.SQLReadDate("fistarihi") <> #1/1/1950# Then
                            issueDate.Value = oSQL1.SQLReadDate("fistarihi") 'irsaliye tarihi
                        Else
                            issueDate.Value = DateTime.Now() 'irsaliye saati
                        End If

                        issueTime.Value = DateTime.Now() 'irsaliye saati

                        note1.Value = oSQL1.SQLReadString("girisdept") + " işlemi yapılmak üzere gönderilmiştir. Fatura Edilmeyecektir."
                        note2.Value = oSQL1.SQLReadString("notlar", 100)

                        notes.SetValue(note1, 0)
                        notes.SetValue(note2, 1)

#Region "DespatchSupplierParty"
                        'Malları sevk even firmaya ait bilgiler
                        despatchSupplierParty =
                        New SupplierPartyType With {
                            .Party = New PartyType With {
                                .PartyIdentification = New PartyIdentificationType() {
                                    New PartyIdentificationType With {
                                    .ID = New IDType With {.Value = cF1VN, .schemeID = cSchemeID1}
                                                                        }
                                    },
                                .PartyName = New PartyNameType With {
                                .Name = New NameType1 With {.Value = oSQL1.SQLReadString("f1adi")}},
                                .PostalAddress = New AddressType With {
                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f1shr")},
                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f1smt")},
                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f1ulk")}},
                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f1adr")},
                                    .PostalZone = New PostalZoneType With {.Value = oSQL1.SQLReadString("f1pk")}
                                    }
                                }
                            }
#End Region

#Region "DeliveryCustomerParty"
                        'Malları Teslim alan firmaya ait bilgiler
                        deliveryCustomerParty =
                        New CustomerPartyType With {
                            .Party = New PartyType With {
                                .PartyIdentification = New PartyIdentificationType() {
                                    New PartyIdentificationType With {
                                    .ID = New IDType With {.Value = cF2VN, .schemeID = cSchemeID2}
                                                                        }
                                    },
                                .PartyName = New PartyNameType With {
                                .Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2adi")}},
                                .PostalAddress = New AddressType With {
                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f2shr")},
                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f2smt")},
                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2ulk")}},
                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f2adr")},
                                    .PostalZone = New PostalZoneType With {.Value = oSQL1.SQLReadString("f2pk")}
                                    }
                                }
                            }
#End Region

#Region "SellerSupplierParty"
                        SellerSupplierParty =
                        New SupplierPartyType With {
                            .Party = New PartyType With {
                                .PartyIdentification = New PartyIdentificationType() {
                                    New PartyIdentificationType With {
                                    .ID = New IDType With {.Value = cF1VN, .schemeID = cSchemeID1}
                                                                     }
                                    },
                                .PartyName = New PartyNameType With {
                                .Name = New NameType1 With {.Value = oSQL1.SQLReadString("f1adi")}},
                                .PostalAddress = New AddressType With {
                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f1shr")},
                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f1smt")},
                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f1ulk")}},
                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f1adr")},
                                    .PostalZone = New PostalZoneType With {.Value = oSQL1.SQLReadString("f1pk")}
                                    }
                                }
                            }
#End Region

#Region "BuyerCustomerParty"
                        BuyerCustomerParty =
                        New CustomerPartyType With {
                            .Party = New PartyType With {
                                .PartyIdentification = New PartyIdentificationType() {
                                    New PartyIdentificationType With {
                                    .ID = New IDType With {.Value = cF2VN, .schemeID = cSchemeID2}
                                                                     }
                                    },
                                .PartyName = New PartyNameType With {
                                .Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2adi")}},
                                .PostalAddress = New AddressType With {
                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f2shr")},
                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f2smt")},
                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2ulk")}},
                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f2adr")},
                                    .PostalZone = New PostalZoneType With {.Value = oSQL1.SQLReadString("f2pk")}
                                    }
                                }
                            }
#End Region

#Region "Shipment"
                        ' Taşımaya ait bilgiler
                        shipment = New ShipmentType With {
                            .ID = New IDType With {.Value = 1}, 'Schema gereği zorunlu alan. 1 yazılabilir
                            .Delivery = New DeliveryType With {
                                .DeliveryAddress = New AddressType With {.CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f2shr")},
                                            .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f2smt")},
                                            .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2ulk")}},
                                            .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f2adr")},
                                            .PostalZone = New PostalZoneType With {.Value = oSQL1.SQLReadString("f2pk")}},
                                .Despatch = New DespatchType With {
                                            .ActualDespatchDate = New ActualDespatchDateType With {.Value = issueDate.Value}, 'Fiili Sevk Tarihi
                                            .ActualDespatchTime = New ActualDespatchTimeType With {.Value = issueTime.Value}  'Fiili Sevk Saati
                                                                  }
                                },
                            .ShipmentStage = New ShipmentStageType() {
                                New ShipmentStageType With {
                                .TransportMeans = New TransportMeansType With {
                                    .RoadTransport = New RoadTransportType With {
                                        .LicensePlateID = New LicensePlateIDType With {.schemeID = "PLAKA", .Value = oSQL1.SQLReadString("aracplaka")}
                                                                     }}}}}

                        If cF3VN <> "" Then
                            shipment.Delivery.CarrierParty = New PartyType With { 'Taşıyıcı/Kargo veya lojistik firması bilgileri
                                                            .PartyIdentification = New PartyIdentificationType() {
                                                                            New PartyIdentificationType With {
                                                                            .ID = New IDType With {.Value = cF3VN, .schemeID = cSchemeID3}
                                                                                                             }
                                                                            },
                                                            .PartyName = New PartyNameType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f3adi")}},
                                                            .PostalAddress = New AddressType With {
                                                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f3shr")},
                                                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f3smt")},
                                                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f3ulk")}},
                                                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f3adr")},
                                                                    .PostalZone = New PostalZoneType With {.Value = oSQL1.SQLReadString("f3pk")}
                                                                    }}
                        End If

                        If oSQL1.SQLReadString("sofortckn") <> "" Then
                            shipment.ShipmentStage(0).DriverPerson = New PersonType() {
                                                            New PersonType With {.FirstName = New FirstNameType With {.Value = oSQL1.SQLReadString("soforadi")}, 'Şoför Adı
                                                                                 .FamilyName = New FamilyNameType With {.Value = oSQL1.SQLReadString("soforsoyadi")}, 'Şoför Soyadı
                                                                                 .NationalityID = New NationalityIDType With {.Value = oSQL1.SQLReadString("sofortckn")} 'Şoför TC
                                                                                }
                                                                }
                        End If

                        If oSQL1.SQLReadString("dorseplaka") <> "" Then
                            shipment.TransportHandlingUnit = New TransportHandlingUnitType() {
                                                            New TransportHandlingUnitType With {
                                                            .TransportEquipment = New TransportEquipmentType() {
                                                                New TransportEquipmentType With {
                                                                    .ID = New IDType With {.schemeID = "DORSEPLAKA", .Value = oSQL1.SQLReadString("dorseplaka")}}}}
                                                            }
                        End If
#End Region

                    End If
                    oSQL1.oReader.Close()
                    oSQL1.oReader = Nothing

#Region "Satırlar"
                    nLineCount = -1

                    Select Case cUFE
                        Case "Ana model tipi + model no gruplu"
                            oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(10, cFisNo)
                        Case "Siparis gruplu"
                            oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(14, cFisNo)
                        Case Else
                            oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(6, cFisNo)
                    End Select

                    oSQL2.GetSQLReader()

                    Do While oSQL2.oReader.Read

                        cID = oSQL2.SQLReadString("anamodeltipi")
                        cKodu = oSQL2.SQLReadString("aciklama")

                        Select Case cUFE
                            Case "Ana model tipi + model no gruplu"
                                cID = oSQL2.SQLReadString("anamodeltipi")
                                cKodu = oSQL2.SQLReadString("aciklama")
                                cAdi = oSQL2.SQLReadString("modelno")
                            Case "Siparis gruplu"
                                cID = oSQL2.SQLReadString("uretimtakipno")
                                cKodu = oSQL2.SQLReadString("anamodeltipi")
                                cAdi = oSQL2.SQLReadString("musteri") + " " + oSQL2.SQLReadString("aciklama")
                            Case Else
                                cID = oSQL2.SQLReadString("anamodeltipi")
                                cKodu = oSQL2.SQLReadString("aciklama")
                                cAdi = oSQL2.SQLReadString("modelno") +
                                        IIf(oSQL2.SQLReadString("renk") = "", "", " " + oSQL2.SQLReadString("renk")) +
                                        IIf(oSQL2.SQLReadString("beden") = "", "", " " + oSQL2.SQLReadString("beden"))
                        End Select

                        nMiktar = oSQL2.SQLReadDouble("adet")

                        cBirim = "C62"

                        nLineCount = nLineCount + 1

                        oDespatchLine = New DespatchLineType With {' satır 1
                                        .ID = New IDType With {.Value = CStr(nLineCount + 1)}, 'Sıra No
                                        .Item = New ItemType With {
                                            .Name = New NameType1 With {.Value = cAdi}, 'Mal Adı
                                            .SellersItemIdentification = New ItemIdentificationType With {.ID = New IDType With {.Value = cKodu}} 'Satıcı Kodu
                                            },
                                        .DeliveredQuantity = New DeliveredQuantityType With {.unitCode = cBirim, .Value = nMiktar},         'Gönderilen Miktar 
                                        .OrderLineReference = New OrderLineReferenceType With {.LineID = New LineIDType With {.Value = "1"}}      'Sipariş Referansı
                                        }

                        despatchLines(nLineCount) = oDespatchLine
                    Loop
                    oSQL2.oReader.Close()
                    oSQL2.oReader = Nothing
#End Region

            End Select

            If cSahis1 <> "" And cSoyad1 <> "" Then
                despatchSupplierParty.Party.Person = New PersonType With {.FirstName = New FirstNameType With {.Value = cSahis1},
                                                                          .FamilyName = New FamilyNameType With {.Value = cSoyad1}}
            End If

            If cSahis2 <> "" And cSoyad2 <> "" Then
                deliveryCustomerParty.Party.Person = New PersonType With {.FirstName = New FirstNameType With {.Value = cSahis2},
                                                                          .FamilyName = New FamilyNameType With {.Value = cSoyad2}}
            End If

            If cSahis3 <> "" And cSoyad3 <> "" Then
                shipment.Delivery.CarrierParty.Person = New PersonType With {.FirstName = New FirstNameType With {.Value = cSahis3},
                                                                             .FamilyName = New FamilyNameType With {.Value = cSoyad3}}
            End If

            despatchAdvice.UBLVersionID = ublVersion
            despatchAdvice.CustomizationID = customizationID
            despatchAdvice.CopyIndicator = copyIndicator
            despatchAdvice.DespatchAdviceTypeCode = despatchAdviceTypeCode
            despatchAdvice.ProfileID = profileID
            despatchAdvice.ID = id
            despatchAdvice.UUID = uuid
            despatchAdvice.IssueDate = issueDate
            despatchAdvice.IssueTime = issueTime
            despatchAdvice.Note = notes
            despatchAdvice.LineCountNumeric = linecount
            despatchAdvice.DespatchSupplierParty = despatchSupplierParty
            despatchAdvice.DeliveryCustomerParty = deliveryCustomerParty
            despatchAdvice.BuyerCustomerParty = BuyerCustomerParty
            despatchAdvice.SellerSupplierParty = SellerSupplierParty
            despatchAdvice.DespatchLine = despatchLines
            despatchAdvice.Shipment = shipment

            despatchInfos(0).DespatchAdvice = despatchAdvice

            If lirsdraft Then
                response = client.SaveAsDraft(despatchInfos)
            Else
                response = client.SendDespatch(despatchInfos)
            End If

            If response.IsSucceded Then
                cMessage = String.Format("Belge başarıyla gönderildi. UUID: {0}, ID:{1} ", response.Value(0).Id, response.Value(0).Number)
                CreateLog("WinTex_CRS_SuccessLog", cMessage)

                cIrsaliyeID = response.Value(0).Id
                cIrsaliyeNumarasi = response.Value(0).Number

                SendEIrsaliye = True

                If lPDF Then
                    DespatchPDF(cIrsaliyeID, cFisNo, cFisTipi)
                End If

                nDay = issueDate.Value.Day
                nMonth = issueDate.Value.Month
                nYear = issueDate.Value.Year
                nHour = issueTime.Value.Hour
                nMinute = issueTime.Value.Minute

                cTarihSaat = Strings.Format(nDay, "00") + "-" + Strings.Format(nMonth, "00") + "-" + Strings.Format(nYear, "0000") + " " +
                             Strings.Format(nHour, "00") + ":" + Strings.Format(nMinute, "00") + ":00"

                eIrsaliyeUpdateWinTex(nCase, cFisNo, cIrsaliyeNumarasi, cIrsaliyeID, cTarihSaat, cIrsaliyeID)
            Else
                cMessage = String.Format("Belge gönderilirken hata oluştu {0} ", response.Message)
                CreateLog("WinTex_CRS_FailLog", cMessage)
                MsgBox(cMessage)
                SendEIrsaliye = False
            End If

            oSQL1.oReader = Nothing
            oSQL1.CloseConn()
            oSQL1 = Nothing

            oSQL2.CloseConn()
            oSQL2 = Nothing


        Catch ex As Exception
            ErrDisp("SendEIrsaliye", "eIrsaliye",,, ex)
        End Try
    End Function

    Public Function DespatchStatus(ByVal cIrsaliyeID As String) As String

        DespatchStatus = ""

        Try
            Dim client As DespatchIntegrationClient = GetClient()
            Dim response As DespatchStatusResponse

            response = client.QueryOutboxDespatchStatus(New String() {cIrsaliyeID})

            If response.IsSucceded Then
                DespatchStatus = String.Format("İrsaliye ID:{0}, Durum Kodu :{1}, Durum :{2} ", response.Value(0).DespatchId, response.Value(0).StatusCode, response.Value(0).StatusEnum)
                AddEventLog(DespatchStatus, 1)
            Else
                DespatchStatus = String.Format("Durum sorgusu alınamadı {0} ", response.Message)
                AddEventLog(DespatchStatus, 2)
            End If

        Catch ex As Exception
            ErrDisp("DespatchStatus", "eIrsaliye",,, ex)
        End Try
    End Function

    Public Function DespatchPDF(ByVal cIrsaliyeID As String, ByVal Optional cFisNo As String = "", ByVal Optional cFisTipi As String = "") As String

        DespatchPDF = ""

        Try
            Dim client As DespatchIntegrationClient = GetClient()
            Dim response As DespatchesDataResponse
            Dim cMessage As String = ""

            response = client.GetOutboxDespatchPdf(cIrsaliyeID)

            If response.IsSucceded Then
                ' response.Value.Items[0].Data bu alanda pdf dosyası byte[] olarak dönecektir. Bunun kaydedilmesi gerekir
                cMessage = String.Format("PDF alındı {0} ", cIrsaliyeID)
                AddEventLog(cMessage, 1)
                DespatchPDF = eIrsaliyeStoreDocument(cFisNo, cFisTipi, response.Value.Items(0).Data)
            Else
                cMessage = String.Format("PDF alınamadı {0} ", response.Message)
                AddEventLog(cMessage, 2)
            End If

        Catch ex As Exception
            ErrDisp("DespatchPDF", "eIrsaliye",,, ex)
        End Try
    End Function

    Private Function GetClient() As DespatchIntegrationClient

        GetClient = Nothing

        Try
            Dim oBinding As System.ServiceModel.BasicHttpBinding
            Dim oEPAddress As System.ServiceModel.EndpointAddress
            Dim oClient As DespatchIntegrationClient

            oBinding = New ServiceModel.BasicHttpBinding(securityMode:=ServiceModel.BasicHttpSecurityMode.TransportWithMessageCredential)
            oBinding.Name = "BasicHttpBinding_IDespatchIntegration"
            oBinding.MaxBufferSize = 2147483647
            oBinding.MaxReceivedMessageSize = 2147483647
            oBinding.ReaderQuotas.MaxStringContentLength = 2147483647
            oBinding.CloseTimeout = TimeSpan.FromMinutes(10)
            oBinding.OpenTimeout = TimeSpan.FromMinutes(10)
            oBinding.ReceiveTimeout = TimeSpan.FromMinutes(10)
            oBinding.SendTimeout = TimeSpan.FromMinutes(10)

            oEPAddress = New ServiceModel.EndpointAddress(cURL)

            oClient = New DespatchIntegrationClient(oBinding, oEPAddress)
            oClient.ClientCredentials.UserName.UserName = cUsername
            oClient.ClientCredentials.UserName.Password = cPassword

            GetClient = oClient

        Catch ex As Exception
            ErrDisp("GetClient", "eIrsaliye",,, ex)
        End Try
    End Function

End Class
