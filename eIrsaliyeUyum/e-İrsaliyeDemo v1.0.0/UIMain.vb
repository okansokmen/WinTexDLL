Imports System
Imports System.Net
Imports System.Net.Mail
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography.X509Certificates
Imports eIrsaliyeUyum.DespatchConnect
Imports System.Xml
Imports System.Configuration
Imports System.Globalization
Imports System.Runtime.InteropServices

<ComClass(UIMain.ClassId, UIMain.InterfaceId, UIMain.EventsId)>
Public Class UIMain

    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "63c0ee59-1247-4db1-97a7-5a2d0833bb2f"
    Public Const InterfaceId As String = "d944fdfc-537a-48fe-ad58-139950877a2b"
    Public Const EventsId As String = "00823e4f-5acd-49Fa-b564-bddb5f20ea1b"

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.

    Public Sub New()
        MyBase.New()
    End Sub

    Public Function init(Optional cServer As String = "", Optional cDatabase As String = "", Optional cUser As String = "sa", Optional cPassword As String = "", Optional cConnStr As String = "",
                         Optional cWintexUser As String = "") As Boolean
        ' init database connection
        Dim cSQL As String = ""
        Dim oSQL As eIrsaliyeUyum.SQLServerClass

        init = False

        Try
            eIrsaliyeUyum.UtilRoot.oConnection.cServer = cServer.Trim
            eIrsaliyeUyum.UtilRoot.oConnection.cDatabase = cDatabase.Trim
            eIrsaliyeUyum.UtilRoot.oConnection.cUser = cUser.Trim
            eIrsaliyeUyum.UtilRoot.oConnection.cPassword = cPassword.Trim

            eIrsaliyeUyum.UtilRoot.oConnection.cWinTexUser = cWintexUser.Trim

            If cConnStr.Trim = "" Then
                eIrsaliyeUyum.UtilRoot.oConnection.cConnStr = "Data Source=" + eIrsaliyeUyum.UtilRoot.oConnection.cServer + ";" +
                                                              "Initial Catalog=" + eIrsaliyeUyum.UtilRoot.oConnection.cDatabase + ";" +
                                                              "uid=" + eIrsaliyeUyum.UtilRoot.oConnection.cUser + ";" +
                                                              "pwd=" + eIrsaliyeUyum.UtilRoot.oConnection.cPassword + ""
            Else
                eIrsaliyeUyum.UtilRoot.oConnection.cConnStr = cConnStr.Trim
            End If

            If eIrsaliyeUyum.TestConnection() Then
                init = True

                oSQL = New eIrsaliyeUyum.SQLServerClass()
                oSQL.OpenConn()

                eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumEirsaliyeServiceUrl = oSQL.GetSysPar("UyumEirsaliyeServiceUrl")
                eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumUsername = oSQL.GetSysPar("UyumUsername")
                eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumPassword = oSQL.GetSysPar("UyumPassword")
                eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumPortalUrl = oSQL.GetSysPar("UyumPortalUrl")
                eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumPortalUsername = oSQL.GetSysPar("UyumPortalUsername")
                eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumPortalPassword = oSQL.GetSysPar("UyumPortalPassword")
                eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumirsservicestok = oSQL.GetSysPar("Uyumirsservicestok", "Butun satirlar")
                eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumirsserviceuretim = oSQL.GetSysPar("Uyumirsserviceuretim", "Butun satirlar")
                eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumirsdraft = oSQL.GetSysPar("Uyumirsdraft")
                eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumSaticiFirma = oSQL.GetSysPar("UyumSaticiFirma", "DAHILI")

                If cWintexUser.Trim = "" Then
                    eIrsaliyeUyum.UtilRoot.oConnection.cPersonel = ""
                Else
                    oSQL.cSQLQuery = "select top 1 personel, uyumid, uyumusername, uyumpassword  " +
                                    " from personel with (NOLOCK) " +
                                    " where username = '" + cWintexUser.Trim + "' "

                    oSQL.GetSQLReader()

                    If oSQL.oReader.Read Then
                        eIrsaliyeUyum.UtilRoot.oConnection.cPersonel = oSQL.SQLReadString("personel")
                    End If
                    oSQL.oReader.Close()
                End If

                oSQL.cSQLQuery = "select top 1 firmname " +
                                " from sysinfo with (NOLOCK) " +
                                " where firmname is not null " +
                                " and firmname <> '' "

                oSQL.GetSQLReader()

                If oSQL.oReader.Read Then
                    eIrsaliyeUyum.UtilRoot.oConnection.cOwner = oSQL.SQLReadString("firmname").ToLower
                End If
                oSQL.oReader.Close()

                oSQL.CloseConn()
                oSQL = Nothing
            End If

        Catch ex As Exception
            init = False
            eIrsaliyeUyum.ErrDisp("SQL Connect : " + ex.Message + " " + cServer.Trim + " " + cDatabase.Trim + " " + cUser.Trim + " " + cPassword.Trim, "UtilMain",,, ex)
        End Try
    End Function

    Public Function DllVersion() As String
        DllVersion = ""
        Try
            DllVersion = My.Application.Info.Version.ToString.Trim
        Catch ex As Exception
            DllVersion = ""
        End Try
    End Function

    Public Function DllTest() As String
        DllTest = "eIrsaliyeUyum ÇALIŞMIYOR"
        Try
            Return "eIrsaliyeUyum Çalışıyor"
        Catch ex As Exception
            DllTest = "eIrsaliyeUyum ÇALIŞMIYOR"
        End Try
    End Function

    Public Sub DllTestForm()
        Try
            Dim oForm As New eIrsaliyeUyum.Form1
            oForm.ShowDialog()
        Catch ex As Exception
            eIrsaliyeUyum.ErrDisp("DllTestForm", "UtilMain",,, ex)
        End Try
    End Sub

    Public Sub DllDebug()
        Try
            Dim oForm As New eIrsaliyeUyum.FrmInfo
            oForm.ShowDialog()
        Catch ex As Exception
            eIrsaliyeUyum.ErrDisp("DllDebug", "UtilMain",,, ex)
        End Try
    End Sub

    Public Sub DllViewer()
        Try
            Dim oForm As New eIrsaliyeUyum.FrmViewer
            oForm.ShowDialog()
        Catch ex As Exception
            eIrsaliyeUyum.ErrDisp("DllViewer", "UtilMain",,, ex)
        End Try
    End Sub

    Public Sub DllSettings()
        Try
            Dim oForm As New eIrsaliyeUyum.FrmUyumSettings
            oForm.ShowDialog()
        Catch ex As Exception
            eIrsaliyeUyum.ErrDisp("DllSettings", "UtilMain",,, ex)
        End Try
    End Sub

    ' ************* FUNCTIONS

    Public Function SendEIrsaliye(nCase As Integer, cFisNo As String, Optional ByRef cIrsaliyeID As String = "", Optional ByRef cIrsaliyeNumarasi As String = "",
                                  Optional ByVal lPDF As Boolean = False) As Boolean
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

            Dim oSQL1 As New eIrsaliyeUyum.SQLServerClass
            Dim oSQL2 As New eIrsaliyeUyum.SQLServerClass
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
            Dim lTest As Boolean = False
            Dim cFisTipi As String = ""
            Dim cSFE As String = ""
            Dim cUFE As String = ""
            Dim cMTF As String = ""
            Dim cUTF As String = ""
            Dim cNotlar As String = ""
            Dim cID As String = ""
            Dim cDepartman As String = ""
            Dim cUUID As String = ""
            Dim cF1VD As String = "9000068418"

            oSQL1.OpenConn()
            oSQL2.OpenConn()

            cSFE = eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumirsservicestok
            cUFE = eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumirsserviceuretim

            If eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumEirsaliyeServiceUrl = "https://efatura-test.uyumsoft.com.tr/Services/DespatchIntegration" Then
                lTest = True
            End If

            ' irsaliye kafası
            ublVersion.Value = "2.1"                ' Sabit Değer
            customizationID.Value = "TR1.2.1"       ' Sabit Değer
            copyIndicator.Value = False              ' Taslak
            despatchAdviceTypeCode.Value = "SEVK"   ' çoğu zaman sabit. sadece irsaliye mücbir hallerde önce matbu evraka düzenlenip sornadan e-irsaliyeye dönüştürülürse "MATBUDAN" olması gerekiyor. 
            profileID.Value = "TEMELIRSALIYE" 'Sabit değer
            id.Value = "" 'A012020011111132 İrsaliye Numarası. Boş bırakıldığında sistem üretir ve response'ta döner. 16 hane olmalıdır. Formatı içni irsaliye kılavuzlarına bakınız.  
            uuid.Value = Guid.NewGuid().ToString() 'irsaliyeye ait unique ID. boş bırakıldığında sistem üretir ve responseta geri döner. Boş bırakılması response'un alınamadığı senaryolarda takibi imkansız kıldığı için önerilmez. 

            Select Case nCase
                Case 1
                    ' stok fişi
                    cFisTipi = "stok"
                    cMTF = eIrsaliyeMTF(cFisNo)

                    Select Case cSFE
                        Case "Ana stok grubu + stok tipi gruplu"
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(7, cFisNo)
                        Case "Stok kodu gruplu"
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(11, cFisNo)
                        Case "Stok kodu + renk gruplu"
                            oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(15, cFisNo)
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

                        If Not lTest Then
                            cF1VD = oSQL1.SQLReadString("f1vn")
                        End If

                        If oSQL1.SQLReadDate("belgetarihi") <> #1/1/1950# Then
                            issueDate.Value = oSQL1.SQLReadDate("belgetarihi") 'irsaliye tarihi
                        ElseIf oSQL1.SQLReadDate("fistarihi") <> #1/1/1950# Then
                            issueDate.Value = oSQL1.SQLReadDate("fistarihi") 'irsaliye tarihi
                        Else
                            issueDate.Value = DateTime.Now.Date ' irsaliye tarihi
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
                                .ID = New IDType With {.Value = cF1VD, .schemeID = "VKN"}
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

                    Select Case cSFE
                        Case "Ana stok grubu + stok tipi gruplu"
                            oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(9, cFisNo)
                        Case "Stok kodu gruplu"
                            oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(12, cFisNo)
                        Case "Stok kodu + renk gruplu"
                            oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(16, cFisNo)
                        Case Else
                            oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(5, cFisNo)
                    End Select

                    oSQL2.GetSQLReader()

                    Do While oSQL2.oReader.Read

                        Select Case cSFE
                            Case "Ana stok grubu + stok tipi gruplu"
                                cKodu = oSQL2.SQLReadString("anastokgrubu", 15)
                                cID = oSQL2.SQLReadString("stoktipi")
                                cAdi = oSQL2.SQLReadString("stoktipi")
                            Case "Stok kodu gruplu"
                                cKodu = oSQL2.SQLReadString("anastokgrubu", 15)
                                cID = oSQL2.SQLReadString("stokno")
                                cAdi = oSQL2.SQLReadString("stoktipi")
                            Case "Stok kodu + renk gruplu"
                                cKodu = oSQL2.SQLReadString("anastokgrubu", 15)
                                cID = oSQL2.SQLReadString("stokno")
                                cAdi = oSQL2.SQLReadString("stoktipi") + " " + IIf(oSQL2.SQLReadString("renk") = "" Or oSQL2.SQLReadString("renk") = "HEPSI", "", " " + oSQL2.SQLReadString("renk"))
                            Case Else
                                cKodu = oSQL2.SQLReadString("anastokgrubu") + "-" + oSQL2.SQLReadString("stoktipi")
                                cID = oSQL2.SQLReadString("stokno")
                                cAdi = IIf(oSQL2.SQLReadString("renk") = "" Or oSQL2.SQLReadString("renk") = "HEPSI", "", " " + oSQL2.SQLReadString("renk")).ToString +
                                       IIf(oSQL2.SQLReadString("beden") = "" Or oSQL2.SQLReadString("beden") = "HEPSI", "", " " + oSQL2.SQLReadString("beden")).ToString +
                                       IIf(oSQL2.SQLReadString("malzemetakipkodu") = "", "", " " + oSQL2.SQLReadString("malzemetakipkodu")).ToString
                        End Select

                        nMiktar = oSQL2.SQLReadDouble("netmiktar1")
                        cBirim = oSQL2.SQLReadString("birim1")

                        nLineCount = nLineCount + 1

                        oDespatchLine = New DespatchLineType With {' satır 1
                                    .ID = New IDType With {.Value = CStr(nLineCount + 1)}, 'Sıra No
                                    .Item = New ItemType With {
                                        .Name = New NameType1 With {.Value = cAdi}, 'Mal Adı
                                        .Description = New DescriptionType With {.Value = cID},
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
                    cUTF = eIrsaliyeUTF(cFisNo)

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

                        If Not lTest Then
                            cF1VD = oSQL1.SQLReadString("f1vn")
                        End If

                        cDepartman = oSQL1.SQLReadString("girisdept")

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
                                .ID = New IDType With {.Value = cF1VD, .schemeID = "VKN"}
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

                        Select Case cUFE
                            Case "Ana model tipi + model no gruplu"
                                cID = oSQL2.SQLReadString("anamodeltipi")
                                cKodu = oSQL2.SQLReadString("aciklama")
                                cAdi = oSQL2.SQLReadString("modelno")
                            Case "Siparis gruplu"
                                cID = oSQL2.SQLReadString("uretimtakipno")
                                If cDepartman = "YIKAMA" Then
                                    cID = cID + " / " + oSQL2.SQLReadString("yikama")
                                End If

                                cKodu = oSQL2.SQLReadString("anamodeltipi")
                                cAdi = oSQL2.SQLReadString("musteri") + " " + oSQL2.SQLReadString("aciklama").ToString
                            Case Else
                                cID = oSQL2.SQLReadString("anamodeltipi")
                                cKodu = oSQL2.SQLReadString("aciklama")
                                cAdi = oSQL2.SQLReadString("modelno") +
                                       IIf(oSQL2.SQLReadString("renk") = "", "", " " + oSQL2.SQLReadString("renk")).ToString +
                                       IIf(oSQL2.SQLReadString("beden") = "", "", " " + oSQL2.SQLReadString("beden")).ToString
                        End Select

                        nMiktar = oSQL2.SQLReadDouble("adet")
                        cBirim = "ADET"

                        nLineCount = nLineCount + 1

                        oDespatchLine = New DespatchLineType With {' satır 1
                                    .ID = New IDType With {.Value = CStr(nLineCount + 1)}, ' Sıra No
                                    .Item = New ItemType With {
                                        .Name = New NameType1 With {.Value = cAdi}, ' Mal Adı
                                        .Description = New DescriptionType With {.Value = cID},
                                        .SellersItemIdentification = New ItemIdentificationType With {.ID = New IDType With {.Value = cID}}   ' Satıcı Kodu
                                        },
                                    .DeliveredQuantity = New DeliveredQuantityType With {.unitCode = cBirim, .Value = CDec(nMiktar)},           ' Gönderilen Miktar 
                                    .OrderLineReference = New OrderLineReferenceType With {.LineID = New LineIDType With {.Value = "1"}}        ' Sipariş Referansı 
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

            If eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumirsdraft = "1" Then
                response = client.SaveAsDraft(despatchInfos)
            Else
                response = client.SendDespatch(despatchInfos)
            End If

            If response.IsSucceded Then
                cMessage = String.Format("Belge başarıyla gönderildi. UUID: {0}, ID:{1} ", response.Value(0).Id, response.Value(0).Number)
                MsgBox(cMessage)

                cIrsaliyeID = response.Value(0).Id
                cIrsaliyeNumarasi = response.Value(0).Number
                cUUID = cIrsaliyeID

                nDay = issueDate.Value.Day
                nMonth = issueDate.Value.Month
                nYear = issueDate.Value.Year
                nHour = issueTime.Value.Hour
                nMinute = issueTime.Value.Minute

                cTarihSaat = Strings.Format(nDay, "00") + "-" + Strings.Format(nMonth, "00") + "-" + Strings.Format(nYear, "0000") + " " +
                             Strings.Format(nHour, "00") + ":" + Strings.Format(nMinute, "00") + ":00"

                eIrsaliyeUpdateWinTex(nCase, cFisNo, cIrsaliyeNumarasi, cUUID, cTarihSaat, cIrsaliyeID)

                SendEIrsaliye = True

                If lPDF Then
                    DespatchPDF(cIrsaliyeID, cFisNo, cFisTipi)
                End If
            Else
                cMessage = String.Format("Belge gönderilirken hata oluştu {0} ", response.Message)
                MsgBox(cMessage)
            End If

            oSQL1.CloseConn()
            oSQL1.oReader = Nothing
            oSQL1 = Nothing

            oSQL2.CloseConn()
            oSQL2 = Nothing

        Catch ex As Exception
            eIrsaliyeUyum.ErrDisp("SendEIrsaliye", "eIrsaliye",,, ex)
        End Try
    End Function

    Public Sub eIrsaliyeUpdateWinTex(nCase As Integer, cFisNo As String, cIrsaliyeNumarasi As String, cUUID As String, cTarihSaat As String, Optional cIrsaliyeID As String = "")
        Try
            Dim oSQL As New eIrsaliyeUyum.SQLServerClass

            oSQL.OpenConn()

            Select Case nCase
                Case 1
                    oSQL.cSQLQuery = "set dateformat dmy " +
                                    " update stokfis Set " +
                                    " belgeno = '" + cIrsaliyeNumarasi.Trim + "', " +
                                    " crsuuid = '" + cUUID.Trim + "', " +
                                    " crsid = '" + cIrsaliyeID.Trim + "', " +
                                    " crstarih = '" + cTarihSaat.Trim + "' " +
                                    " where stokfisno = '" + cFisNo.Trim + "' "
                    oSQL.SQLExecute()
                Case 2
                    oSQL.cSQLQuery = "set dateformat dmy " +
                                    " update uretharfis Set " +
                                    " belgeno = '" + cIrsaliyeNumarasi.Trim + "', " +
                                    " crsuuid = '" + cUUID.Trim + "', " +
                                    " crsid = '" + cIrsaliyeID.Trim + "', " +
                                    " crstarih = '" + cTarihSaat.Trim + "' " +
                                    " where uretfisno = '" + cFisNo.Trim + "' "
                    oSQL.SQLExecute()
            End Select

            oSQL.CloseConn()

        Catch ex As Exception
            eIrsaliyeUyum.ErrDisp("eIrsaliyeUpdateWinTex", "UtileIrsaliye",,, ex)
        End Try
    End Sub

    Public Function DespatchStatus(ByVal cIrsaliyeID As String) As String

        DespatchStatus = ""

        Try
            Dim client As DespatchIntegrationClient = GetClient()
            Dim response As DespatchStatusResponse

            response = client.QueryOutboxDespatchStatus(New String() {cIrsaliyeID})

            If response.IsSucceded Then
                DespatchStatus = String.Format("İrsaliye ID:{0}, Durum Kodu :{1}, Durum :{2} ", response.Value(0).DespatchId, response.Value(0).StatusCode, response.Value(0).StatusEnum)
            Else
                DespatchStatus = String.Format("Durum sorgusu alınamadı {0} ", response.Message)
            End If

        Catch ex As Exception
            eIrsaliyeUyum.ErrDisp("DespatchStatus", "eIrsaliye",,, ex)
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

            oEPAddress = New ServiceModel.EndpointAddress(eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumEirsaliyeServiceUrl)

            oClient = New DespatchIntegrationClient(oBinding, oEPAddress)
            oClient.ClientCredentials.UserName.UserName = eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumUsername
            oClient.ClientCredentials.UserName.Password = eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumPassword

            GetClient = oClient

        Catch ex As Exception
            eIrsaliyeUyum.ErrDisp("GetClient", "eIrsaliye",,, ex)
        End Try
    End Function

    Public Function eIrsaliyeMTF(cFisNo As String) As String

        eIrsaliyeMTF = ""

        Try
            If cFisNo.Trim = "" Then Exit Function

            Dim cSonuc As String = ""
            Dim oSQL As New eIrsaliyeUyum.SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select distinct malzemetakipkodu " +
                            " from stokfislines with (NOLOCK) " +
                            " where stokfisno = '" + cFisNo.Trim + "' " +
                            " and malzemetakipkodu is not null " +
                            " and malzemetakipkodu <> '' " +
                            " order by malzemetakipkodu "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                If cSonuc.Trim = "" Then
                    cSonuc = oSQL.SQLReadString("malzemetakipkodu")
                Else
                    cSonuc = cSonuc + "," + oSQL.SQLReadString("malzemetakipkodu")
                End If
            Loop
            oSQL.oReader.Close()
            oSQL.CloseConn()

            eIrsaliyeMTF = cSonuc.Trim

        Catch ex As Exception
            eIrsaliyeUyum.ErrDisp("eIrsaliyeMTF", "UIMain",,, ex)
        End Try
    End Function

    Public Function eIrsaliyeUTF(cFisNo As String) As String

        eIrsaliyeUTF = ""

        Try
            If cFisNo.Trim = "" Then Exit Function

            Dim cSonuc As String = ""
            Dim oSQL As New eIrsaliyeUyum.SQLServerClass

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select distinct uretimtakipno " +
                            " from uretharfislines with (NOLOCK) " +
                            " where uretfisno = '" + cFisNo.Trim + "' " +
                            " and uretimtakipno is not null " +
                            " and uretimtakipno <> '' " +
                            " order by uretimtakipno "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                If cSonuc.Trim = "" Then
                    cSonuc = oSQL.SQLReadString("uretimtakipno")
                Else
                    cSonuc = cSonuc + "," + oSQL.SQLReadString("uretimtakipno")
                End If
            Loop
            oSQL.oReader.Close()
            oSQL.CloseConn()

            eIrsaliyeUTF = cSonuc.Trim

        Catch ex As Exception
            eIrsaliyeUyum.ErrDisp("eIrsaliyeUTF", "UIMain",,, ex)
        End Try
    End Function

    Public Function GetSQLQueryeIrsaliye(nCase As Integer, cFilter As String) As String

        Dim cSQL As String = ""
        Dim cUyumSaticiFirma As String = ""

        GetSQLQueryeIrsaliye = ""

        Try
            cUyumSaticiFirma = eIrsaliyeUyum.UtilRoot.oUyumConnect.cUyumSaticiFirma.Trim

            Select Case nCase
                Case 1
                    ' stok fişi kafası
                    cSQL = "SELECT top 1 a.stokfisno, a.fistarihi, a.belgeno, a.belgetarihi, a.sevkno, " +
                        " a.faturano, a.faturatarihi, a.departman, a.firma, a.stokfistipi, a.notlar, " +
                        " a.tasiyicifirma, a.aracplaka, a.dorseplaka, a.teslimeden, a.soforadi, a.soforsoyadi, a.sofortckn, "

                    ' f1xxx cikisfirm_atl / malı GÖNDEREN firma / DespatchSupplierParty
                    cSQL = cSQL +
                        " f1adi = d.aciklama, " +
                        " f1vd = d.vergidairesi, " +
                        " f1vn = d.vergino, " +
                        " f1adr = rtrim(rtrim(coalesce(d.adres1,'')) + ' ' + rtrim(coalesce(d.adres2,''))), " +
                        " f1smt = d.semt, " +
                        " f1shr = case when d.sehir is null or d.sehir = '' then 'İstanbul' else d.sehir end, " +
                        " f1ulk = case when d.ulke  is null or d.ulke  = '' then 'Türkiye'  else d.ulke end, " +
                        " f1email = d.emailadresi, " +
                        " f1tel1 = d.tel1, " +
                        " f1fax = d.fax, " +
                        " f1pk = case when d.postakodu is null or d.postakodu  = '' then '34000' else d.postakodu end,  " +
                        " f1sahis = d.yetkili1, " +
                        " f1soyad = d.yetkili2, " +
                        " f1tc = d.yetkilitc, "

                    ' f2xxx girisfirm_atl / malı TESLIM ALAN firma / DeliveryCustomerParty
                    cSQL = cSQL +
                        " f2adi = b.aciklama, " +
                        " f2vd = b.vergidairesi, " +
                        " f2vn = b.vergino, " +
                        " f2adr = rtrim(rtrim(coalesce(b.adres1,'')) + ' ' + rtrim(coalesce(b.adres2,''))), " +
                        " f2smt = b.semt, " +
                        " f2shr = case when b.sehir is null or b.sehir = '' then 'İstanbul' else b.sehir end, " +
                        " f2ulk = case when b.ulke  is null or b.ulke  = '' then 'Türkiye'  else b.ulke end, " +
                        " f2email = b.emailadresi, " +
                        " f2tel1 = b.tel1, " +
                        " f2fax = b.fax, " +
                        " f2pk = case when b.postakodu is null or b.postakodu  = '' then '34000' else b.postakodu end,  " +
                        " f2sahis = b.yetkili1, " +
                        " f2soyad = b.yetkili2, " +
                        " f2tc = b.yetkilitc, "

                    ' f3xxx tasiyicifirma / TAŞIYICI firma
                    cSQL = cSQL +
                        " f3adi = c.aciklama, " +
                        " f3vd = c.vergidairesi, " +
                        " f3vn = c.vergino, " +
                        " f3adr = rtrim(rtrim(coalesce(c.adres1,'')) + ' ' + rtrim(coalesce(c.adres2,''))), " +
                        " f3smt = c.semt, " +
                        " f3shr = case when c.sehir is null or c.sehir = '' then 'İstanbul' else c.sehir end, " +
                        " f3ulk = case when c.ulke  is null or c.ulke  = '' then 'Türkiye'  else c.ulke end, " +
                        " f3email = c.emailadresi, " +
                        " f3tel1 = c.tel1, " +
                        " f3fax = c.fax, " +
                        " f3pk = case when c.postakodu is null or c.postakodu  = '' then '34000' else c.postakodu end, " +
                        " f3sahis = c.yetkili1, " +
                        " f3soyad = c.yetkili2, " +
                        " f3tc = c.yetkilitc "

                    cSQL = cSQL +
                        " from stokfis a with (NOLOCK) " +
                        " left outer join firma b with (NOLOCK) on b.firma = a.firma " +
                        " left outer join firma c with (NOLOCK) on c.firma = a.tasiyicifirma " +
                        " left outer join firma d with (NOLOCK) on d.firma = '" + cUyumSaticiFirma + "' " +
                        " where a.stokfisno = '" + cFilter.Trim + "' "

                Case 2
                    ' üretim fişi kafası
                    cSQL = "select top 1 a.uretfisno, a.fistarihi, a.belgeno, a.belgetarihi, a.faturano, a.faturatarihi, " +
                        " a.cikisdept, a.cikisfirm_atl, a.girisdept, a.girisfirm_atl, a.notlar, " +
                        " a.tasiyicifirma, a.aracplaka, a.dorseplaka, a.teslimpersonel, a.soforadi, a.soforsoyadi, a.sofortckn, "

                    ' f1xxx cikisfirm_atl / malı GÖNDEREN firma / DespatchSupplierParty
                    cSQL = cSQL +
                        " f1adi = d.aciklama, " +
                        " f1vd = d.vergidairesi, " +
                        " f1vn = d.vergino, " +
                        " f1adr = rtrim(rtrim(coalesce(d.adres1,'')) + ' ' + rtrim(coalesce(d.adres2,''))), " +
                        " f1smt = d.semt, " +
                        " f1shr = case when d.sehir is null or d.sehir = '' then 'İstanbul' else d.sehir end, " +
                        " f1ulk = case when d.ulke  is null or d.ulke  = '' then 'Türkiye'  else d.ulke end, " +
                        " f1email = d.emailadresi, " +
                        " f1tel1 = d.tel1, " +
                        " f1fax = d.fax, " +
                        " f1pk = case when d.postakodu is null or d.postakodu  = '' then '34000' else d.postakodu end,  " +
                        " f1sahis = d.yetkili1, " +
                        " f1soyad = d.yetkili2, " +
                        " f1tc = d.yetkilitc, "

                    ' f2xxx girisfirm_atl / malı TESLIM ALAN firma / DeliveryCustomerParty
                    cSQL = cSQL +
                        " f2adi = e.aciklama, " +
                        " f2vd = e.vergidairesi, " +
                        " f2vn = e.vergino, " +
                        " f2adr = rtrim(rtrim(coalesce(e.adres1,'')) + ' ' + rtrim(coalesce(e.adres2,''))), " +
                        " f2smt = e.semt, " +
                        " f2shr = case when e.sehir is null or e.sehir = '' then 'İstanbul' else e.sehir end, " +
                        " f2ulk = case when e.ulke  is null or e.ulke  = '' then 'Türkiye'  else e.ulke end, " +
                        " f2email = e.emailadresi, " +
                        " f2tel1 = e.tel1, " +
                        " f2fax = e.fax, " +
                        " f2pk = case when e.postakodu is null or e.postakodu  = '' then '34000' else e.postakodu end,  " +
                        " f2sahis = e.yetkili1, " +
                        " f2soyad = e.yetkili2, " +
                        " f2tc = e.yetkilitc, "

                    ' f3xxx tasiyicifirma / TAŞIYICI firma
                    cSQL = cSQL +
                        " f3adi = f.aciklama, " +
                        " f3vd = f.vergidairesi, " +
                        " f3vn = f.vergino, " +
                        " f3adr = rtrim(rtrim(coalesce(f.adres1,'')) + ' ' + rtrim(coalesce(f.adres2,''))), " +
                        " f3smt = f.semt, " +
                        " f3shr = case when f.sehir is null or f.sehir = '' then 'İstanbul' else f.sehir end, " +
                        " f3ulk = case when f.ulke  is null or f.ulke  = '' then 'Türkiye'  else f.ulke end, " +
                        " f3email = f.emailadresi, " +
                        " f3tel1 = f.tel1, " +
                        " f3fax = f.fax, " +
                        " f3pk = case when f.postakodu is null or f.postakodu  = '' then '34000' else f.postakodu end, " +
                        " f3sahis = f.yetkili1, " +
                        " f3soyad = f.yetkili2, " +
                        " f3tc = f.yetkilitc "

                    cSQL = cSQL +
                        " from uretharfis a with (NOLOCK) " +
                        " left outer join uretharfislines b With (NOLOCK) On b.uretfisno = a.uretfisno " +
                        " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                        " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                        " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                        " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                        " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                        " where a.uretfisno = '" + cFilter.Trim + "' "

                ' detaylı bütün satırlar
                Case 3
                    ' stok fişi satır adedi
                    cSQL = "select count (distinct a.stokhareketno) " +
                        " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                        " where a.stokfisno = '" + cFilter.Trim + "' " +
                        " and a.stokno = b.stokno " +
                        " and a.stokno is not null " +
                        " and a.stokno <> '' " +
                        " and a.netmiktar1 is not null " +
                        " and a.netmiktar1 > 0 "
                Case 4
                    ' üretim fişi satır adedi
                    cSQL = "select count (distinct b.modelno + c.renk + c.beden) " +
                        " from uretharfis a with (NOLOCK) " +
                        " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                        " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                        " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                        " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                        " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                        " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                        " where a.uretfisno = '" + cFilter.Trim + "' "
                Case 5
                    ' stok fişi satırları
                    cSQL = "select a.stokhareketno, b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.renk, a.beden, a.netmiktar1, a.birim1, a.malzemetakipkodu " +
                        " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                        " where a.stokfisno = '" + cFilter.Trim + "' " +
                        " and a.stokno = b.stokno " +
                        " and a.stokno is not null " +
                        " and a.stokno <> '' " +
                        " and a.netmiktar1 is not null " +
                        " and a.netmiktar1 > 0 " +
                        " order by b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.renk, a.beden "
                Case 6
                    ' üretim fişi satırları
                    cSQL = "select g.anamodeltipi, b.modelno, c.renk, c.beden, g.aciklama, " +
                        " adet = sum(coalesce(c.adet,0)) " +
                        " from uretharfis a with (NOLOCK) " +
                        " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                        " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                        " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                        " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                        " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                        " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                        " where a.uretfisno = '" + cFilter.Trim + "' " +
                        " group by g.anamodeltipi, b.modelno, c.renk, c.beden, g.aciklama " +
                        " order by g.anamodeltipi, b.modelno, c.renk, c.beden "

                ' özet satırlar 
                Case 7
                    ' stok fişi özet satır adedi
                    cSQL = "select count (distinct b.anastokgrubu + b.stoktipi + b.birim1) " +
                        " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                        " where a.stokfisno = '" + cFilter.Trim + "' " +
                        " and a.stokno = b.stokno " +
                        " and a.stokno is not null " +
                        " and a.stokno <> '' " +
                        " and a.netmiktar1 is not null " +
                        " and a.netmiktar1 > 0 "
                Case 8
                    ' üretim fişi özet satır adedi
                    cSQL = "select count (distinct g.anamodeltipi + b.modelno) " +
                        " from uretharfis a with (NOLOCK) " +
                        " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                        " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                        " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                        " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                        " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                        " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                        " where a.uretfisno = '" + cFilter.Trim + "' "
                Case 9
                    ' stok fişi özet satırları
                    cSQL = "select b.anastokgrubu, b.stoktipi, b.birim1, " +
                        " netmiktar1 = sum(coalesce(a.netmiktar1,0)) " +
                        " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                        " where a.stokfisno = '" + cFilter.Trim + "' " +
                        " and a.stokno = b.stokno " +
                        " and a.stokno is not null " +
                        " and a.stokno <> '' " +
                        " and a.netmiktar1 is not null " +
                        " and a.netmiktar1 > 0 " +
                        " group by b.anastokgrubu, b.stoktipi, b.birim1 " +
                        " order by b.anastokgrubu, b.stoktipi, b.birim1 "
                Case 10
                    ' üretim fişi özet satırları
                    cSQL = "select g.anamodeltipi, b.modelno, g.aciklama, " +
                        " adet = sum(coalesce(c.adet,0)) " +
                        " from uretharfis a with (NOLOCK) " +
                        " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                        " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                        " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                        " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                        " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                        " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                        " where a.uretfisno = '" + cFilter.Trim + "' " +
                        " group by g.anamodeltipi, b.modelno, g.aciklama  " +
                        " order by g.anamodeltipi, b.modelno  "
                ' stok kodu gruplu
                Case 11
                    ' stok kodu gruplu satır adedi
                    cSQL = "select count (distinct b.stokno) " +
                        " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                        " where a.stokfisno = '" + cFilter.Trim + "' " +
                        " and a.stokno = b.stokno " +
                        " and a.stokno is not null " +
                        " and a.stokno <> '' " +
                        " and a.netmiktar1 is not null " +
                        " and a.netmiktar1 > 0 "
                Case 12
                    ' stok kodu gruplu satırlar
                    cSQL = "select b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.birim1, " +
                        " netmiktar1 = sum(coalesce(a.netmiktar1,0)) " +
                        " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                        " where a.stokfisno = '" + cFilter.Trim + "' " +
                        " and a.stokno = b.stokno " +
                        " and a.stokno is not null " +
                        " and a.stokno <> '' " +
                        " and a.netmiktar1 is not null " +
                        " and a.netmiktar1 > 0 " +
                        " group by b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.birim1 " +
                        " order by a.stokno "
                Case 13
                    ' sipariş gruplu üretim fişi satır adedi
                    cSQL = "select count (distinct b.uretimtakipno + g.anamodeltipi + g.aciklama) " +
                        " from uretharfis a with (NOLOCK) " +
                        " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                        " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                        " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                        " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                        " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                        " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                        " where a.uretfisno = '" + cFilter.Trim + "' "
                Case 14
                    ' sipariş gruplu üretim fişi satırları
                    cSQL = "select b.uretimtakipno, g.anamodeltipi, g.aciklama, " +
                        " adet = sum(coalesce(c.adet,0)), " +
                        " musteri = (select top 1 x.musterino " +
                                    " from siparis x with (NOLOCK) , sipmodel y with (NOLOCK) " +
                                    " where x.kullanicisipno = y.siparisno " +
                                    " and y.uretimtakipno = b.uretimtakipno), " +
                        " yikama = (select top 1 rtrim(convert(char(30),x.parasalnotlar))  " +
                                    " from siparis x with (NOLOCK) , sipmodel y with (NOLOCK) " +
                                    " where x.kullanicisipno = y.siparisno " +
                                    " and y.uretimtakipno = b.uretimtakipno) " +
                        " from uretharfis a with (NOLOCK) " +
                        " left outer join uretharfislines b with (NOLOCK) on b.uretfisno = a.uretfisno " +
                        " left outer join uretharrba c with (NOLOCK) on c.uretfisno = a.uretfisno and c.ulineno = b.ulineno and c.adet is not null and c.adet > 0 " +
                        " left outer join firma d with (NOLOCK) on d.firma = a.cikisfirm_atl " +
                        " left outer join firma e with (NOLOCK) on e.firma = a.girisfirm_atl " +
                        " left outer join firma f with (NOLOCK) on f.firma = a.tasiyicifirma " +
                        " left outer join ymodel g with (NOLOCK) on g.modelno = b.modelno " +
                        " where a.uretfisno = '" + cFilter.Trim + "' " +
                        " group by b.uretimtakipno, g.anamodeltipi, g.aciklama " +
                        " order by b.uretimtakipno, g.anamodeltipi, g.aciklama "

                ' stok kodu + renk gruplu
                Case 15
                    ' stok kodu + renk gruplu satır adedi
                    cSQL = "select count (distinct a.stokno + a.renk) " +
                        " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                        " where a.stokfisno = '" + cFilter.Trim + "' " +
                        " and a.stokno = b.stokno " +
                        " and a.stokno is not null " +
                        " and a.stokno <> '' " +
                        " and a.netmiktar1 is not null " +
                        " and a.netmiktar1 > 0 "
                Case 16
                    ' stok kodu + renk gruplu satırlar
                    cSQL = "select b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.renk, a.birim1, " +
                        " netmiktar1 = sum(coalesce(a.netmiktar1,0)) " +
                        " from stokfislines a with (NOLOCK) , stok b with (NOLOCK) " +
                        " where a.stokfisno = '" + cFilter.Trim + "' " +
                        " and a.stokno = b.stokno " +
                        " and a.stokno is not null " +
                        " and a.stokno <> '' " +
                        " and a.netmiktar1 is not null " +
                        " and a.netmiktar1 > 0 " +
                        " group by b.anastokgrubu, b.stoktipi, b.cinsaciklamasi, a.stokno, a.renk, a.birim1 " +
                        " order by a.stokno, a.renk "
            End Select

            GetSQLQueryeIrsaliye = cSQL

        Catch ex As Exception
            eIrsaliyeUyum.ErrDisp("GetSQLQueryeIrsaliye", "UtileIrsaliye",,, ex)
        End Try
    End Function

    Public Function DespatchPDF(ByVal cIrsaliyeID As String, ByVal Optional cFisNo As String = "", ByVal Optional cFisTipi As String = "") As String

        DespatchPDF = ""

        Try
            Dim client As DespatchIntegrationClient = GetClient()
            Dim response As DespatchesDataResponse
            Dim cMessage As String = ""
            Dim cTodaysDate As String = String.Format("{0:dd_MM_yyyy}", DateTime.Now)
            Dim cPath As String = ""
            Dim oSQL As eIrsaliyeUyum.SQLServerClass
            Dim cFileName As String = ""

            response = client.GetOutboxDespatchPdf(cIrsaliyeID)

            If response.IsSucceded Then
                ' response.Value.Items[0].Data bu alanda pdf dosyası byte[] olarak dönecektir. Bunun kaydedilmesi gerekir
                cMessage = String.Format("PDF alındı {0} ", cIrsaliyeID)

                oSQL = New eIrsaliyeUyum.SQLServerClass

                oSQL.OpenConn()

                cPath = oSQL.GetSysPar("pathofshare", "c:\wintex")

                If Right$(cPath, 1) <> "\" Then
                    cPath = cPath + "\"
                End If
                cPath = cPath + "docs\"

                cFileName = "eirsaliye" +
                            IIf(cFisTipi = "", "", "_" + cFisTipi).ToString +
                            IIf(cFisNo = "", "", "_" + cFisNo).ToString +
                            "_" + cTodaysDate

                cFileName = cPath + cFileName + ".pdf"

                If System.IO.File.Exists(cFileName) Then
                    System.IO.File.Delete(cFileName)
                End If

                System.IO.File.WriteAllBytes(cFileName, response.Value.Items(0).Data)

                oSQL.cSQLQuery = "insert documents (docvalue, doctype, rdocname, vdocname, docpath, " +
                                " type, extension, docsubtype, duzletmetarihi, duzeltmesaati, " +
                                " username) "

                oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " values ('" + cFisNo + "', " +
                                " '" + cFisTipi.Trim + "', " +
                                " '" + cFisTipi.Trim + "', " +
                                " '" + eIrsaliyeUyum.SQLWriteString(cFileName.Trim, 150) + "', " +
                                " '" + eIrsaliyeUyum.SQLWriteString(cFileName.Trim, 255) + "', "

                oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " 'Adobe PDF File', " +
                                " 'pdf', " +
                                " 'eirsaliye', " +
                                " convert(date,getdate()), " +
                                " convert(char(8),getdate(),108), "

                oSQL.cSQLQuery = oSQL.cSQLQuery +
                                " '" + eIrsaliyeUyum.SQLWriteString(eIrsaliyeUyum.Gl_UserName, 30) + "') "

                oSQL.SQLExecute()

                oSQL.CloseConn()

                DespatchPDF = cFileName
            Else
                cMessage = String.Format("PDF alınamadı {0} ", response.Message)
                MsgBox(cMessage)
            End If

        Catch ex As Exception
            eIrsaliyeUyum.ErrDisp("DespatchPDF", "eIrsaliye",,, ex)
        End Try
    End Function

    Public Function CheckValidEIrsaliye(ByVal nCase As Integer, ByVal cFisNo As String) As Boolean
        ' nCase = 1 Stok Fişi
        ' nCase = 2 Üretim Fişi

        CheckValidEIrsaliye = False

        Try
            Dim oSQL As New eIrsaliyeUyum.SQLServerClass
            Dim nLineCount As Integer = 0
            Dim lTest As Boolean = False
            Dim cF1VN As String = ""
            Dim cSQL_FisSatirSayisi As String = ""
            Dim cSQL_Fis As String = ""
            Dim cMessage As String = ""

            Select Case nCase
                Case 1
                    ' stok fişi
                    cSQL_FisSatirSayisi = GetSQLQueryeIrsaliye(3, cFisNo)
                    cSQL_Fis = GetSQLQueryeIrsaliye(1, cFisNo)
                Case 2
                    ' üretim fişi 
                    cSQL_FisSatirSayisi = GetSQLQueryeIrsaliye(4, cFisNo)
                    cSQL_Fis = GetSQLQueryeIrsaliye(2, cFisNo)
            End Select

            oSQL.OpenConn()

            oSQL.cSQLQuery = cSQL_FisSatirSayisi

            nLineCount = oSQL.DBReadInteger()

            If nLineCount = 0 Then
                cMessage = cMessage + "Gönderilebilecek satır yok" + vbCrLf
            End If

            oSQL.cSQLQuery = cSQL_Fis

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                ' satıcı kontrolleri

                If oSQL.SQLReadString("f1vn") = "" Then
                    cMessage = cMessage + "Malları satan firmanın (" + oSQL.SQLReadString("f1adi") + ") vergi numarası yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f1adi") = "" Then
                    cMessage = cMessage + "Malları satan firmanın adı yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f1smt") = "" Then
                    cMessage = cMessage + "Malları satan firmanın (" + oSQL.SQLReadString("f1adi") + ") semt bilgisi yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f1shr") = "" Then
                    cMessage = cMessage + "Malları satan firmanın (" + oSQL.SQLReadString("f1adi") + ") şehir bilgisi yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f1ulk") = "" Then
                    cMessage = cMessage + "Malları satan firmanın (" + oSQL.SQLReadString("f1adi") + ") ülke bilgisi yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f1pk") = "" Then
                    cMessage = cMessage + "Malları satan firmanın (" + oSQL.SQLReadString("f1adi") + ") posta kodu bilgisi yok" + vbCrLf
                End If

                ' alıcı kontrolleri

                If oSQL.SQLReadString("f2vn").Length = 11 Then
                    ' TCKN
                    If oSQL.SQLReadString("f2sahis") = "" Then
                        cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2sahis") + ") yetkili adı yok" + vbCrLf
                    End If
                    If oSQL.SQLReadString("f2soyad") = "" Then
                        cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2soyad") + ") yetkili soyadı yok" + vbCrLf
                    End If
                End If

                If oSQL.SQLReadString("f2vn") = "" Then
                    cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2adi") + ") vergi numarası yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f2adi") = "" Then
                    cMessage = cMessage + "Malları teslim alan firmanın adı yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f2smt") = "" Then
                    cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2adi") + ") semt bilgisi yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f2shr") = "" Then
                    cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2adi") + ") şehir bilgisi yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f2ulk") = "" Then
                    cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2adi") + ") ülke bilgisi yok" + vbCrLf
                End If

                If oSQL.SQLReadString("f2pk") = "" Then
                    cMessage = cMessage + "Malları teslim alan firmanın (" + oSQL.SQLReadString("f2adi") + ") posta kodu bilgisi yok" + vbCrLf
                End If

                ' nakliyeci

                If oSQL.SQLReadString("sofortckn") = "" Then
                    If oSQL.SQLReadString("f3vn") = "" Then
                        cMessage = cMessage + "Nakliyeci firmanın (" + oSQL.SQLReadString("f3adi") + ") vergi numarası yok" + vbCrLf
                    End If

                    If oSQL.SQLReadString("f3adi") = "" Then
                        cMessage = cMessage + "Nakliyeci firmanın adı yok" + vbCrLf
                    End If
                Else
                    If oSQL.SQLReadString("soforadi") = "" Then
                        cMessage = cMessage + "Şoför adı bilgisi yok" + vbCrLf
                    End If

                    If oSQL.SQLReadString("soforsoyadi") = "" Then
                        cMessage = cMessage + "Şoför soyadı bilgisi yok" + vbCrLf
                    End If

                    If oSQL.SQLReadString("aracplaka") = "" Then
                        cMessage = cMessage + "Araç plaka bilgisi yok" + vbCrLf
                    End If
                End If

            End If
            oSQL.oReader.Close()

            oSQL.CloseConn()

            If cMessage.Trim <> "" Then
                cMessage = "Dikkat : " + cFisNo + " fişi eIrsaliye olarak gönderilemez." + cMessage
                MessageBox.Show(cMessage.Trim)
                Exit Function
            End If

            CheckValidEIrsaliye = True

        Catch ex As Exception
            eIrsaliyeUyum.ErrDisp("CheckValidEIrsaliye", "eIrsaliye",,, ex)
        End Try
    End Function

End Class
