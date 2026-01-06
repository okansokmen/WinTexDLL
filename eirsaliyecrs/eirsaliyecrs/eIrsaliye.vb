Option Explicit On

Imports eirsaliyecrs.DespatchConnect
Imports System.Runtime.InteropServices

<ComClass(eIrsaliye.ClassId, eIrsaliye.InterfaceId, eIrsaliye.EventsId)> Public Class eIrsaliye

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "fcb3bea1-c7c9-4fc9-9db6-5fbb0d668ab9"
    Public Const InterfaceId As String = "e15da67f-38f9-4566-ac81-6786d305d33e"
    Public Const EventsId As String = "f1a5f242-14ca-4c26-afda-59c72da5f895"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public Shared cVersion As String = My.Application.Info.Version.ToString.Trim

    Dim cURL As String = ""
    Dim cUsername As String = ""
    Dim cPassword As String = ""

    Public Shared Function TestCRS() As String

        TestCRS = "CRS ÇALIŞMIYOR"

        Try
            Return "CRS Çalışıyor"
        Catch ex As Exception
            ErrDisp("TestCRS", "eIrsaliye",,, ex)
        End Try
    End Function

    Public Function initDBConnection(Optional cServer As String = "", Optional cDatabase As String = "", Optional cUser As String = "sa", Optional cPassword As String = "", Optional cConnStr As String = "",
                                     Optional cWintexUser As String = "") As Boolean
        ' init database connection
        initDBConnection = False

        Try
            oConnection.cServer = cServer.Trim
            oConnection.cDatabase = cDatabase.Trim
            oConnection.cUser = cUser.Trim
            oConnection.cPassword = cPassword.Trim

            oConnection.cWinTexUser = cWintexUser.Trim

            If cConnStr.Trim = "" Then
                oConnection.cConnStr = "Data Source=" + oConnection.cServer + ";" +
                                        "Initial Catalog=" + oConnection.cDatabase + ";" +
                                        "uid=" + oConnection.cUser + ";" +
                                        "pwd=" + oConnection.cPassword + ""
            Else
                oConnection.cConnStr = cConnStr.Trim
            End If

            initDBConnection = True

        Catch ex As Exception
            ErrDisp("initDBConnection", "eIrsaliye",,, ex)
        End Try
    End Function

    Public Function initWebService()
        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            cURL = oSQL.GetSysPar("UrlCrsEirsaliyeService", "https://connect-test.crssoft.com/Services/DespatchIntegration")
            cUsername = oSQL.GetSysPar("CrsUsername", "CrsDemo85")
            cPassword = oSQL.GetSysPar("CrsPassword", "11223385")

            oSQL.CloseConn()
            oSQL = Nothing

        Catch ex As Exception
            ErrDisp("initWebService", "eIrsaliye",,, ex)
        End Try
    End Function


    Public Sub EditParameters()
        Try
            Dim ofrmCrsSoft As New frmCrsSoft
            ofrmCrsSoft.ShowDialog()

        Catch ex As Exception
            ErrDisp("EditParameters", "eIrsaliye",,, ex)
        End Try
    End Sub

    Public Function SendEIrsaliye(nCase As Integer, cFisNo As String, Optional ByRef cIrsaliyeID As String = "", Optional ByRef cIrsaliyeNumarasi As String = "") As Boolean
        ' nCase = 1 Stok Fişi
        ' nCase = 2 Üretim Fişi

        SendEIrsaliye = False

        Try
            Dim client As DespatchIntegrationClient = GetClient()
            Dim response As DespatchIdentitiesResponse = New DespatchIdentitiesResponse()
            Dim despatchInfos() As DespatchInfo = {New DespatchInfo}
            Dim despatchAdvice As DespatchAdviceType = New DespatchAdviceType() 'irsaliye objesi
            Dim despatchSupplierParty = New SupplierPartyType() 'gönderici firma
            Dim deliveryCustomerParty = New CustomerPartyType() 'alıcı firma 
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
            Dim despatchLines As Array
            Dim oDespatchLine As DespatchLineType
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
            Dim lTest As Boolean = False
            Dim cFisTipi As String = ""

            oSQL1.OpenConn()
            oSQL2.OpenConn()

            If oSQL1.GetSysPar("UrlCrsEirsaliyeService") = "https://connect-test.crssoft.com/Services/DespatchIntegration" Then
                lTest = True
            End If

            ' irsaliye kafası
            ublVersion.Value = "2.1" 'Sabit Değer
            customizationID.Value = "TR1.2.1" 'Sabit Değer
            copyIndicator.Value = False 'Sabit Değer
            despatchAdviceTypeCode.Value = "SEVK" ' çoğu zaman sabit. sadece irsaliye mücbir hallerde önce matbu evraka düzenlenip sornadan e-irsaliyeye dönüştürülürse "MATBUDAN" olması gerekiyor. 
            profileID.Value = "TEMELIRSALIYE" 'Sabit değer
            id.Value = "" 'A012020011111132 İrsaliye Numarası. Boş bırakıldığında sistem üretir ve response'ta döner. 16 hane olmalıdır. Formatı içni irsaliye kılavuzlarına bakınız.  
            uuid.Value = Guid.NewGuid().ToString() 'irsaliyeye ait unique ID. boş bırakıldığında sistem üretir ve responseta geri döner. Boş bırakılması response'un alınamadığı senaryolarda takibi imkansız kıldığı için önerilmez. 

            Select Case nCase
                Case 1
                    ' stok fişi
                    cFisTipi = "stok"

                    oSQL1.cSQLQuery = "select count (distinct a.stokhareketno) " +
                                        " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                                        " where a.stokfisno = '" + cFisNo.Trim + "' " +
                                        " and a.stokno = b.stokno " +
                                        " and a.stokno is not null " +
                                        " and a.stokno <> '' " +
                                        " and a.netmiktar1 is not null " +
                                        " and a.netmiktar1 > 0 "

                    nLineCount = oSQL1.DBReadInteger()

                    despatchLines = Array.CreateInstance(GetType(DespatchLineType), nLineCount) 'irsaliye satırları

                    ' irsaliye satır sayısı
                    linecount.Value = CDec(nLineCount)

                    oSQL1.cSQLQuery = "SELECT top 1 a.stokfisno, a.fistarihi, a.belgeno, a.belgetarihi, " +
                                        " a.faturano, a.faturatarihi, a.departman, a.firma, a.stokfistipi, a.notlar, " +
                                        " a.tasiyicifirma, a.aracplaka, a.dorseplaka, a.teslimeden, a.soforadi, a.soforsoyadi, a.sofortckn, " +
                                        " f1adi = d.aciklama, f1vd = d.vergidairesi, f1vn = d.vergino, f1adr = rtrim(rtrim(coalesce(d.adres1,'')) + ' ' + rtrim(coalesce(d.adres2,''))), f1smt = d.semt, f1shr = d.sehir, f1ulk = d.ulke, " +
                                        " f2adi = b.aciklama, f2vd = b.vergidairesi, f2vn = b.vergino, f2adr = rtrim(rtrim(coalesce(b.adres1,'')) + ' ' + rtrim(coalesce(b.adres2,''))), f2smt = b.semt, f2shr = b.sehir, f2ulk = b.ulke, " +
                                        " f3adi = c.aciklama, f3vd = c.vergidairesi, f3vn = c.vergino, f3adr = rtrim(rtrim(coalesce(c.adres1,'')) + ' ' + rtrim(coalesce(c.adres2,''))), f3smt = c.semt, f3shr = c.sehir, f3ulk = c.ulke " +
                                        " from stokfis a with (NOLOCK) " +
                                        " left outer join firma b with (NOLOCK) on b.firma = a.firma " +
                                        " left outer join firma c with (NOLOCK) on c.firma = a.tasiyicifirma " +
                                        " left outer join firma d with (NOLOCK) on d.firma like 'DAH%' and d.dahilifirma = 'E' " +
                                        " where a.stokfisno = '" + cFisNo.Trim + "' "
                    oSQL1.GetSQLReader()

                    If oSQL1.oReader.Read Then

                        If lTest Then
                            cF1VN = "2150240232"
                        Else
                            cF1VN = oSQL1.SQLReadString("f1vn")
                        End If

                        If oSQL1.SQLReadDate("belgetarihi") <> #1/1/1950# Then
                            issueDate.Value = oSQL1.SQLReadDate("belgetarihi") 'irsaliye tarihi
                        ElseIf oSQL1.SQLReadDate("fistarihi") <> #1/1/1950# Then
                            issueDate.Value = oSQL1.SQLReadDate("fistarihi") 'irsaliye tarihi
                        Else
                            issueDate.Value = DateTime.Now() 'irsaliye saati
                        End If

                        issueTime.Value = DateTime.Now() 'irsaliye saati

                        note1.Value = oSQL1.SQLReadString("departman") + " işlemi yapılmak üzere"
                        note2.Value = oSQL1.SQLReadString("notlar", 100)

                        notes.SetValue(note1, 0)
                        notes.SetValue(note2, 1)

                        'Malları sevk even firmaya ait bilgiler
                        despatchSupplierParty =
                        New SupplierPartyType With {
                            .Party = New PartyType With {
                                .PartyIdentification = New PartyIdentificationType() {
                                    New PartyIdentificationType With {
                                    .ID = New IDType With {.Value = cF1VN, .schemeID = "VKN"}
                                                                        }
                                    },
                                .PartyName = New PartyNameType With {
                                .Name = New NameType1 With {.Value = oSQL1.SQLReadString("f1adi")}},
                                .PostalAddress = New AddressType With {
                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f1shr")},
                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f1smt")},
                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f1ulk")}},
                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f1adr")}
                                    }
                                }
                            }

                        'Malları Teslim alan firmaya ait bilgiler
                        deliveryCustomerParty =
                        New CustomerPartyType With {
                            .Party = New PartyType With {
                                .PartyIdentification = New PartyIdentificationType() {
                                    New PartyIdentificationType With {
                                    .ID = New IDType With {.Value = oSQL1.SQLReadString("f2vn"), .schemeID = "VKN"}
                                                                        }
                                    },
                                .PartyName = New PartyNameType With {
                                .Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2adi")}},
                                .PostalAddress = New AddressType With {
                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f2shr")},
                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f2smt")},
                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2ulk")}},
                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f2adr")}
                                    }
                                }
                            }

                        'Taşımaya ait bilgiler
                        shipment = New ShipmentType With {
                            .ID = New IDType With {.Value = 1}, 'Schema gereği zorunlu alan. 1 yazılabilir
                            .Delivery = New DeliveryType With {
                                .CarrierParty = New PartyType With { 'Taşıyıcı/Kargo veya lojistik firması bilgileri
                                                            .PartyIdentification = New PartyIdentificationType() {
                                                                            New PartyIdentificationType With {
                                                                            .ID = New IDType With {.Value = oSQL1.SQLReadString("f3vn"), .schemeID = "VKN"}
                                                                                                                }
                                                                            },
                                                            .PartyName = New PartyNameType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f3adi")}},
                                                            .PostalAddress = New AddressType With {
                                                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f3shr")},
                                                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f3smt")},
                                                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f3ulk")}},
                                                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f3adr")}
                                                                    }},
                                .Despatch =
                                New DespatchType With {
                                .ActualDespatchDate = New ActualDespatchDateType With {.Value = issueDate.Value}, 'Fiili Sevk Tarihi
                                .ActualDespatchTime = New ActualDespatchTimeType With {.Value = issueTime.Value} 'Fiili Sevk Saati
                                                        }
                                },
                            .ShipmentStage = New ShipmentStageType() {
                                New ShipmentStageType With {
                                .DriverPerson = New PersonType() {
                                    New PersonType With {.FirstName = New FirstNameType With {.Value = oSQL1.SQLReadString("soforadi")}, 'Şoför Adı
                                                         .FamilyName = New FamilyNameType With {.Value = oSQL1.SQLReadString("soforsoyadi")}, 'Şoför Soyadı
                                                         .NationalityID = New NationalityIDType With {.Value = oSQL1.SQLReadString("sofortckn")} 'Şoför TC
                                                        }
                                        }
                                    }
                                }
                            }

                    End If
                    oSQL1.oReader.Close()
                    oSQL1.oReader = Nothing

                    nLineCount = -1

                    oSQL2.cSQLQuery = "select a.stokhareketno, b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.renk, a.beden, a.netmiktar1, a.birim1 " +
                                        " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                                        " where a.stokfisno = '" + cFisNo.Trim + "' " +
                                        " and a.stokno = b.stokno " +
                                        " and a.stokno is not null " +
                                        " and a.stokno <> '' " +
                                        " and a.netmiktar1 is not null " +
                                        " and a.netmiktar1 > 0 " +
                                        " order by b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.renk, a.beden "

                    oSQL2.GetSQLReader()

                    Do While oSQL2.oReader.Read

                        cKodu = oSQL2.SQLReadString("anastokgrubu") + "-" + oSQL2.SQLReadString("stoktipi")
                        cAdi = oSQL2.SQLReadString("stokno") +
                                    IIf(oSQL2.SQLReadString("renk") = "", "", " " + oSQL2.SQLReadString("renk")) +
                                    IIf(oSQL2.SQLReadString("beden") = "", "", " " + oSQL2.SQLReadString("beden"))
                        nMiktar = oSQL2.SQLReadDouble("netmiktar1")
                        cBirim = oSQL2.SQLReadString("birim1")

                        nLineCount = nLineCount + 1

                        oDespatchLine = New DespatchLineType With {' satır 1
                                        .ID = New IDType With {.Value = CStr(nLineCount + 1)}, 'Sıra No
                                        .Item = New ItemType With {
                                            .Name = New NameType1 With {.Value = cAdi}, 'Mal Adı
                                            .SellersItemIdentification = New ItemIdentificationType With {.ID = New IDType With {.Value = cKodu}} 'SAtıcı Kodu
                                            },
                                        .DeliveredQuantity = New DeliveredQuantityType With {.unitCode = cBirim, .Value = CDec(nMiktar)}, 'Gönderilen Miktar 
                                        .OrderLineReference = New OrderLineReferenceType With {.LineID = New LineIDType With {.Value = "1"}} 'Sipariş Referansı. 
                                        }


                        despatchLines(nLineCount) = oDespatchLine
                    Loop
                    oSQL2.oReader.Close()
                    oSQL2.oReader = Nothing

                Case 2
                    cFisTipi = "uretim"

                    oSQL1.cSQLQuery = "select count (distinct b.modelno + c.renk + c.beden) " +
                                        " from uretharfis a with (NOLOCK) " +
                                        " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                                        " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                                        " where a.uretfisno = '" + cFisNo.Trim + "' "

                    nLineCount = oSQL1.DBReadInteger()

                    despatchLines = Array.CreateInstance(GetType(DespatchLineType), nLineCount)  'irsaliye satırları

                    ' irsaliye satır sayısı
                    linecount.Value = CDec(nLineCount)

                    ' üretim fişi
                    cSQL = "select top 1 a.uretfisno, a.fistarihi, a.belgeno, a.belgetarihi, a.faturano, a.faturatarihi, " +
                            " a.cikisdept, a.cikisfirm_atl, a.girisdept, a.girisfirm_atl, a.notlar, " +
                            " a.tasiyicifirma, a.aracplaka, a.dorseplaka, a.teslimpersonel, a.soforadi, a.soforsoyadi, a.sofortckn, "

                    ' f1xxx cikisfirm_atl / malı GÖNDEREN firma
                    cSQL = cSQL +
                            " f1adi = d.aciklama, " +
                            " f1vd = d.vergidairesi, " +
                            " f1vn = d.vergino, " +
                            " f1adr = rtrim(rtrim(coalesce(d.adres1,'')) + ' ' + rtrim(coalesce(d.adres2,''))), " +
                            " f1smt = d.semt, " +
                            " f1shr = d.sehir, " +
                            " f1ulk = d.ulke, "

                    ' f2xxx girisfirm_atl / malı TESLIM ALAN firma
                    cSQL = cSQL +
                            " f2adi = e.aciklama, " +
                            " f2vd = e.vergidairesi, " +
                            " f2vn = e.vergino, " +
                            " f2adr = rtrim(rtrim(coalesce(e.adres1,'')) + ' ' + rtrim(coalesce(e.adres2,''))), " +
                            " f2smt = e.semt, " +
                            " f2shr = e.sehir, " +
                            " f2ulk = e.ulke, "

                    ' f3xxx tasiyicifirma / TAŞIYICI firma
                    cSQL = cSQL +
                            " f3adi = f.aciklama, " +
                            " f3vd = f.vergidairesi, " +
                            " f3vn = f.vergino, " +
                            " f3adr = rtrim(rtrim(coalesce(f.adres1,'')) + ' ' + rtrim(coalesce(f.adres2,''))), " +
                            " f3smt = f.semt, " +
                            " f3shr = f.sehir, " +
                            " f3ulk = f.ulke "

                    cSQL = cSQL +
                            " from uretharfis a with (NOLOCK) " +
                            " left outer join uretharfislines b With (NOLOCK) On b.uretfisno = a.uretfisno " +
                            " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                            " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                            " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                            " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                            " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                            " where a.uretfisno = '" + cFisNo.Trim + "' "

                    oSQL1.cSQLQuery = cSQL

                    oSQL1.GetSQLReader()

                    If oSQL1.oReader.Read Then

                        If lTest Then
                            cF1VN = "2150240232"
                        Else
                            cF1VN = oSQL1.SQLReadString("f1vn")
                        End If

                        If oSQL1.SQLReadDate("belgetarihi") <> #1/1/1950# Then
                            issueDate.Value = oSQL1.SQLReadDate("belgetarihi") 'irsaliye tarihi
                        ElseIf oSQL1.SQLReadDate("fistarihi") <> #1/1/1950# Then
                            issueDate.Value = oSQL1.SQLReadDate("fistarihi") 'irsaliye tarihi
                        Else
                            issueDate.Value = DateTime.Now() 'irsaliye saati
                        End If

                        issueTime.Value = DateTime.Now() 'irsaliye saati

                        note1.Value = oSQL1.SQLReadString("girisdept") + " işlemi yapılmak üzere"
                        note2.Value = oSQL1.SQLReadString("notlar", 100)

                        notes.SetValue(note1, 0)
                        notes.SetValue(note2, 1)

                        'Malları sevk even firmaya ait bilgiler
                        despatchSupplierParty =
                        New SupplierPartyType With {
                            .Party = New PartyType With {
                                .PartyIdentification = New PartyIdentificationType() {
                                    New PartyIdentificationType With {
                                    .ID = New IDType With {.Value = cF1VN, .schemeID = "VKN"}
                                                                        }
                                    },
                                .PartyName = New PartyNameType With {
                                .Name = New NameType1 With {.Value = oSQL1.SQLReadString("f1adi")}},
                                .PostalAddress = New AddressType With {
                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f1shr")},
                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f1smt")},
                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f1ulk")}},
                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f1adr")}
                                    }
                                }
                            }

                        'Malları Teslim alan firmaya ait bilgiler
                        deliveryCustomerParty =
                        New CustomerPartyType With {
                            .Party = New PartyType With {
                                .PartyIdentification = New PartyIdentificationType() {
                                    New PartyIdentificationType With {
                                    .ID = New IDType With {.Value = oSQL1.SQLReadString("f2vn"), .schemeID = "VKN"}
                                                                        }
                                    },
                                .PartyName = New PartyNameType With {
                                .Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2adi")}},
                                .PostalAddress = New AddressType With {
                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f2shr")},
                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f2smt")},
                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f2ulk")}},
                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f2adr")}
                                    }
                                }
                            }

                        'Taşımaya ait bilgiler
                        shipment = New ShipmentType With {
                            .ID = New IDType With {.Value = 1}, 'Schema gereği zorunlu alan. 1 yazılabilir
                            .Delivery = New DeliveryType With {
                                .CarrierParty = New PartyType With { 'Taşıyıcı/Kargo veya lojistik firması bilgileri
                                                            .PartyIdentification = New PartyIdentificationType() {
                                                                            New PartyIdentificationType With {
                                                                            .ID = New IDType With {.Value = oSQL1.SQLReadString("f3vn"), .schemeID = "VKN"}
                                                                                                                }
                                                                            },
                                                            .PartyName = New PartyNameType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f3adi")}},
                                                            .PostalAddress = New AddressType With {
                                                                    .CityName = New CityNameType With {.Value = oSQL1.SQLReadString("f3shr")},
                                                                    .CitySubdivisionName = New CitySubdivisionNameType With {.Value = oSQL1.SQLReadString("f3smt")},
                                                                    .Country = New CountryType With {.Name = New NameType1 With {.Value = oSQL1.SQLReadString("f3ulk")}},
                                                                    .StreetName = New StreetNameType With {.Value = oSQL1.SQLReadString("f3adr")}
                                                                    }},
                                .Despatch =
                                New DespatchType With {
                                .ActualDespatchDate = New ActualDespatchDateType With {.Value = issueDate.Value}, 'Fiili Sevk Tarihi
                                .ActualDespatchTime = New ActualDespatchTimeType With {.Value = issueTime.Value} 'Fiili Sevk Saati
                                                        }
                                },
                            .ShipmentStage = New ShipmentStageType() {
                                New ShipmentStageType With {
                                .DriverPerson = New PersonType() {
                                    New PersonType With {.FirstName = New FirstNameType With {.Value = oSQL1.SQLReadString("soforadi")}, 'Şoför Adı
                                                         .FamilyName = New FamilyNameType With {.Value = oSQL1.SQLReadString("soforsoyadi")}, 'Şoför Soyadı
                                                         .NationalityID = New NationalityIDType With {.Value = oSQL1.SQLReadString("sofortckn")} 'Şoför TC
                                                        }
                                        }
                                    }
                                }
                            }

                    End If
                    oSQL1.oReader.Close()
                    oSQL1.oReader = Nothing

                    nLineCount = -1

                    oSQL2.cSQLQuery = "select g.anamodeltipi, b.modelno, c.renk, c.beden, " +
                                        " adet = sum(coalesce(c.adet,0)) " +
                                        " from uretharfis a with (NOLOCK) " +
                                        " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                                        " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                                        " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                                        " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                                        " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                                        " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                                        " where a.uretfisno = '" + cFisNo.Trim + "' " +
                                        " group by g.anamodeltipi, b.modelno, c.renk, c.beden "

                    oSQL2.GetSQLReader()

                    Do While oSQL2.oReader.Read

                        cKodu = oSQL2.SQLReadString("anamodeltipi")
                        cAdi = oSQL2.SQLReadString("modelno") +
                                    IIf(oSQL2.SQLReadString("renk") = "", "", " " + oSQL2.SQLReadString("renk")) +
                                    IIf(oSQL2.SQLReadString("beden") = "", "", " " + oSQL2.SQLReadString("beden"))
                        nMiktar = oSQL2.SQLReadDouble("adet")
                        cBirim = "ADET"

                        nLineCount = nLineCount + 1

                        oDespatchLine = New DespatchLineType With {' satır 1
                                        .ID = New IDType With {.Value = CStr(nLineCount + 1)}, 'Sıra No
                                        .Item = New ItemType With {
                                            .Name = New NameType1 With {.Value = cAdi}, 'Mal Adı
                                            .SellersItemIdentification = New ItemIdentificationType With {.ID = New IDType With {.Value = cKodu}} 'SAtıcı Kodu
                                            },
                                        .DeliveredQuantity = New DeliveredQuantityType With {.unitCode = cBirim, .Value = CDec(nMiktar)}, 'Gönderilen Miktar 
                                        .OrderLineReference = New OrderLineReferenceType With {.LineID = New LineIDType With {.Value = "1"}} 'Sipariş Referansı. 
                                        }


                        despatchLines(nLineCount) = oDespatchLine
                    Loop
                    oSQL2.oReader.Close()
                    oSQL2.oReader = Nothing
            End Select

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
            despatchAdvice.DespatchLine = despatchLines
            despatchAdvice.Shipment = shipment

            despatchInfos(0).DespatchAdvice = despatchAdvice

            response = client.SendDespatch(despatchInfos)

            If response.IsSucceded Then
                cMessage = String.Format("Belge başarıyla gönderildi. UUID: {0}, ID:{1} ", response.Value(0).Id, response.Value(0).Number)
                AddEventLog(cMessage, 1)
                MsgBox(cMessage)

                cIrsaliyeID = response.Value(0).Id
                cIrsaliyeNumarasi = response.Value(0).Number

                SendEIrsaliye = True

                'DespatchPDF(cIrsaliyeID, cFisNo, cFisTipi)

                nDay = issueDate.Value.Day
                nMonth = issueDate.Value.Month
                nYear = issueDate.Value.Year
                nHour = issueTime.Value.Hour
                nMinute = issueTime.Value.Minute

                cTarihSaat = Strings.Format(nDay, "00") + "-" + Strings.Format(nMonth, "00") + "-" + Strings.Format(nYear, "0000") + " " +
                             Strings.Format(nHour, "00") + ":" + Strings.Format(nMinute, "00") + ":00"


                Select Case nCase
                    Case 1
                        oSQL1.cSQLQuery = "set dateformat dmy " +
                                            " update stokfis Set " +
                                            " belgeno = '" + cIrsaliyeNumarasi + "', " +
                                            " crsuuid = '" + cIrsaliyeID + "', " +
                                            " crstarih = '" + cTarihSaat + "' " +
                                            " where stokfisno = '" + cFisNo.Trim + "' "
                        oSQL1.SQLExecute()
                    Case 2
                        oSQL1.cSQLQuery = "set dateformat dmy " +
                                            " update uretharfis Set " +
                                            " belgeno = '" + cIrsaliyeNumarasi + "', " +
                                            " crsuuid = '" + cIrsaliyeID + "', " +
                                            " crstarih = '" + cTarihSaat + "' " +
                                            " where uretfisno = '" + cFisNo.Trim + "' "
                        oSQL1.SQLExecute()
                End Select
            Else
                cMessage = String.Format("Belge gönderilirken hata oluştu {0} ", response.Message)
                AddEventLog(cMessage, 2)
                MsgBox(cMessage)
            End If

            oSQL1.CloseConn()
            oSQL1.oReader = Nothing
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

    Public Function DespatchPDF(ByVal cIrsaliyeID As String, ByVal Optional cFisNo As String = "", ByVal Optional cFisTipi As String = "", Optional ByVal cFileName As String = "") As String

        DespatchPDF = ""

        Try
            Dim client As DespatchIntegrationClient = GetClient()
            Dim response As DespatchesDataResponse
            Dim cMessage As String = ""
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)

            response = client.GetOutboxDespatchPdf(cIrsaliyeID)

            If response.IsSucceded Then
                ' response.Value.Items[0].Data bu alanda pdf dosyası byte[] olarak dönecektir. Bunun kaydedilmesi gerekir
                cMessage = String.Format("PDF alındı {0} ", cIrsaliyeID)
                AddEventLog(cMessage, 1)

                If cFileName.Trim = "" Then
                    cFileName = "c:\wintex\eirsaliye" +
                                IIf(cFisTipi = "", "", "_" + cFisTipi).ToString +
                                IIf(cFisNo = "", "", "_" + cFisNo).ToString +
                                "_" + cTodaysDate +
                                ".pdf"
                End If

                If System.IO.File.Exists(cFileName) Then
                    System.IO.File.Delete(cFileName)
                End If
                System.IO.File.WriteAllBytes(cFileName, response.Value.Items(0).Data)
                DespatchPDF = cFileName
            Else
                cMessage = String.Format("PDF alınamadı {0} ", response.Message)
                AddEventLog(cMessage, 2)
                MsgBox(cMessage)
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
