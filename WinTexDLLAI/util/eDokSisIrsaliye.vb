Option Explicit On

Imports WinTexDLL.net.edoksis.efaturatest

Public Class eDokSisIrsaliye

    Dim cURL As String = ""
    Dim cUsername As String = ""
    Dim cPassword As String = ""
    Dim cToken As String = ""
    Dim oClient As IrsaliyeWebService

    Public Sub New()
        Try
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            cURL = oSQL.GetSysPar("eDokSisService", "https://efaturatest.edoksis.net/IrsaliyeWebService.asmx")

            If oConnection.cOwner = "bolero" Then
                cUsername = oSQL.GetSysPar("eDokSisUsername", "bolerotestws")
                cPassword = oSQL.GetSysPar("eDokSisPassword", "Gg123123")
            Else
                cUsername = oSQL.GetSysPar("eDokSisUsername")
                cPassword = oSQL.GetSysPar("eDokSisPassword")
            End If

            oSQL.CloseConn()
            oSQL = Nothing

            oClient = New IrsaliyeWebService
            oClient.Url = cURL

        Catch ex As Exception
            ErrDisp("initWebService", "eDokSisIrsaliye",,, ex)
        End Try
    End Sub

    Public Function SendEIrsaliye(ByVal nCase As Integer, ByVal cFisNo As String, Optional ByVal lPDF As Boolean = False) As Boolean
        ' nCase = 1 Stok Fişi
        ' nCase = 2 Üretim Fişi

        SendEIrsaliye = False

        Try
            Dim oSQL1 As New SQLServerClass
            Dim oSQL2 As New SQLServerClass
            Dim cFisTipi As String = ""
            Dim nLineCount As Integer = 0
            Dim despatchLines As Array = Nothing
            Dim oDespatchLine As DespatchLine = Nothing
            Dim cKodu As String = ""
            Dim cAdi As String = ""
            Dim nMiktar As Double = 0
            Dim cBirim As String = ""
            Dim cMessage As String = ""
            Dim cIrsaliyeID As String = ""
            Dim cIrsaliyeNumarasi As String = ""
            Dim nIrsaliyeID As Long = 0
            Dim cSFE As String = ""
            Dim cUFE As String = ""
            Dim nDay As Integer = 0
            Dim nMonth As Integer = 0
            Dim nYear As Integer = 0
            Dim nHour As Integer = 0
            Dim nMinute As Integer = 0
            Dim cTarihSaat As String = ""
            Dim cID As String = ""

            oSQL1.OpenConn()
            oSQL2.OpenConn()

            cSFE = oSQL1.GetSysPar("irsservicestok", "Butun satirlar")
            cUFE = oSQL1.GetSysPar("irsserviceuretim", "Butun satirlar")

            Dim oDespatchInvoice As New Despatch

            oDespatchInvoice.UBLVersionID = "2.1"
            oDespatchInvoice.CustomizationID = "TR1.2.1"
            oDespatchInvoice.ID = "" ' buraya irsaliye no dönecek
            oDespatchInvoice.CopyIndicator = False
            oDespatchInvoice.ProfileID = "TEMELIRSALIYE"
            oDespatchInvoice.DespatchAdviceTypeCode = "SEVK"

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
                    despatchLines = Array.CreateInstance(GetType(DespatchLine), nLineCount) 'irsaliye satırları

                    oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(1, cFisNo)

                    oSQL1.GetSQLReader()

                    If oSQL1.oReader.Read Then

                        oDespatchInvoice.Note = {oSQL1.SQLReadString("notlar")}

                        If oSQL1.SQLReadDate("belgetarihi") <> #1/1/1950# Then
                            oDespatchInvoice.IssueDate = oSQL1.SQLReadDate("belgetarihi")   ' irsaliye tarihi
                            oDespatchInvoice.IssueTime = oSQL1.SQLReadDate("belgetarihi")   ' irsaliye saati
                        ElseIf oSQL1.SQLReadDate("fistarihi") <> #1/1/1950# Then
                            oDespatchInvoice.IssueDate = oSQL1.SQLReadDate("fistarihi")     ' irsaliye tarihi
                            oDespatchInvoice.IssueTime = oSQL1.SQLReadDate("fistarihi")     ' irsaliye tarihi
                        Else
                            oDespatchInvoice.IssueDate = DateTime.Now.Date                  ' irsaliye tarihi
                            oDespatchInvoice.IssueTime = DateTime.Now                       ' irsaliye saati
                        End If

                        ' Malları sevk even firmaya ait bilgiler
                        oDespatchInvoice.DespatchSupplierParty = New Party
                        oDespatchInvoice.DespatchSupplierParty.Name = oSQL1.SQLReadString("f1adi")
                        oDespatchInvoice.DespatchSupplierParty.TaxSchemeName = oSQL1.SQLReadString("f1vd")
                        oDespatchInvoice.DespatchSupplierParty.IDScheme = oSQL1.SQLReadString("f1vn")
                        oDespatchInvoice.DespatchSupplierParty.CountryName = oSQL1.SQLReadString("f1ulk")
                        oDespatchInvoice.DespatchSupplierParty.CityName = oSQL1.SQLReadString("f1shr")
                        oDespatchInvoice.DespatchSupplierParty.CitySubdivisionName = oSQL1.SQLReadString("f1smt")
                        oDespatchInvoice.DespatchSupplierParty.StreetName = oSQL1.SQLReadString("f1adr")
                        oDespatchInvoice.DespatchSupplierParty.Telefax = oSQL1.SQLReadString("f1fax")
                        oDespatchInvoice.DespatchSupplierParty.Telephone = oSQL1.SQLReadString("f1tel1")
                        oDespatchInvoice.DespatchSupplierParty.ElectronicMail = oSQL1.SQLReadString("f1email")
                        oDespatchInvoice.DespatchSupplierParty.PostalZone = oSQL1.SQLReadString("f1pk")

                        ' Malları Teslim alan firmaya ait bilgiler
                        oDespatchInvoice.DeliveryCustomerParty = New Party
                        oDespatchInvoice.DeliveryCustomerParty.Name = oSQL1.SQLReadString("f2adi")
                        oDespatchInvoice.DeliveryCustomerParty.TaxSchemeName = oSQL1.SQLReadString("f2vd")
                        oDespatchInvoice.DeliveryCustomerParty.IDScheme = oSQL1.SQLReadString("f2vn")
                        oDespatchInvoice.DeliveryCustomerParty.CountryName = oSQL1.SQLReadString("f2ulk")
                        oDespatchInvoice.DeliveryCustomerParty.CityName = oSQL1.SQLReadString("f2shr")
                        oDespatchInvoice.DeliveryCustomerParty.CitySubdivisionName = oSQL1.SQLReadString("f2smt")
                        oDespatchInvoice.DeliveryCustomerParty.StreetName = oSQL1.SQLReadString("f2adr")
                        oDespatchInvoice.DeliveryCustomerParty.Telefax = oSQL1.SQLReadString("f2fax")
                        oDespatchInvoice.DeliveryCustomerParty.Telephone = oSQL1.SQLReadString("f2tel1")
                        oDespatchInvoice.DeliveryCustomerParty.ElectronicMail = oSQL1.SQLReadString("f2email")
                        oDespatchInvoice.DeliveryCustomerParty.PostalZone = oSQL1.SQLReadString("f2pk")

                        ' Taşıyıcı bilgisi
                        oDespatchInvoice.Shipment = New Shipment
                        oDespatchInvoice.Shipment.ActualDespatchDate = DateTime.Now.Date
                        oDespatchInvoice.Shipment.ActualDespatchTime = DateTime.Now
                        oDespatchInvoice.Shipment.ActualDeliveryDate = DateTime.Now.Date
                        oDespatchInvoice.Shipment.ActualDeliveryTime = DateTime.Now
                        oDespatchInvoice.Shipment.ValueAmount = "0"
                        oDespatchInvoice.Shipment.ValueAmountScheme = "TRY"
                        oDespatchInvoice.Shipment.CountryName = oSQL1.SQLReadString("f3ulk")
                        oDespatchInvoice.Shipment.CityName = oSQL1.SQLReadString("f3shr")
                        oDespatchInvoice.Shipment.CitySubdivisionName = oSQL1.SQLReadString("f3smt")
                        oDespatchInvoice.Shipment.StreetName = oSQL1.SQLReadString("f3adr")
                        oDespatchInvoice.Shipment.PostalZone = oSQL1.SQLReadString("f2pk")
                        oDespatchInvoice.Shipment.LicensePlateID = oSQL1.SQLReadString("aracplaka")

                        Dim oDespatchDriver As New DriverPerson
                        oDespatchDriver.FirstName = oSQL1.SQLReadString("soforadi")
                        oDespatchDriver.FamilyName = oSQL1.SQLReadString("soforsoyadi")
                        oDespatchDriver.NationalityID = oSQL1.SQLReadString("sofortckn")
                        oDespatchInvoice.Shipment.DriverPerson = {oDespatchDriver}
                    End If
                    oSQL1.oReader.Close()
                    oSQL1.oReader = Nothing

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
                                cAdi = oSQL2.SQLReadString("anastokgrubu") + "-" + oSQL2.SQLReadString("stoktipi")
                            Case Else
                                cKodu = oSQL2.SQLReadString("anastokgrubu") + "-" + oSQL2.SQLReadString("stoktipi")
                                cID = oSQL2.SQLReadString("stokno")
                                cAdi = IIf(oSQL2.SQLReadString("renk") = "" Or oSQL2.SQLReadString("renk") = "HEPSI", "", " " + oSQL2.SQLReadString("renk")) +
                                    IIf(oSQL2.SQLReadString("beden") = "" Or oSQL2.SQLReadString("beden") = "HEPSI", "", " " + oSQL2.SQLReadString("beden")) +
                                    IIf(oSQL2.SQLReadString("malzemetakipkodu") = "", "", " " + oSQL2.SQLReadString("malzemetakipkodu"))
                        End Select

                        nMiktar = oSQL2.SQLReadDouble("netmiktar1")

                        cBirim = oSQL2.SQLReadString("birim1")

                        nLineCount = nLineCount + 1

                        oDespatchLine = New DespatchLine
                        oDespatchLine.ID = cID
                        oDespatchLine.LineID = CStr(nLineCount + 1)
                        oDespatchLine.ItemName = cKodu
                        oDespatchLine.Description = cAdi
                        oDespatchLine.DeliveredQuantity = nMiktar.ToString
                        oDespatchLine.DeliveredQuantityUnitCode = cBirim
                        oDespatchLine.PriceAmount = "0"

                        despatchLines(nLineCount) = oDespatchLine
                    Loop
                    oSQL2.oReader.Close()
                    oSQL2.oReader = Nothing

                Case 2
                    ' üretim fişi 
                    cFisTipi = "uretimfisi"

                    If cUFE = "Ana model tipi + model no gruplu" Then
                        oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(8, cFisNo)
                    Else
                        oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(4, cFisNo)
                    End If
                    nLineCount = oSQL1.DBReadInteger()
                    despatchLines = Array.CreateInstance(GetType(DespatchLine), nLineCount) 'irsaliye satırları

                    oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(2, cFisNo)

                    oSQL1.GetSQLReader()

                    If oSQL1.oReader.Read Then

                        oDespatchInvoice.Note = {oSQL1.SQLReadString("notlar")}

                        If oSQL1.SQLReadDate("belgetarihi") <> #1/1/1950# Then
                            oDespatchInvoice.IssueDate = oSQL1.SQLReadDate("belgetarihi")   ' irsaliye tarihi
                            oDespatchInvoice.IssueTime = oSQL1.SQLReadDate("belgetarihi")   ' irsaliye saati
                        ElseIf oSQL1.SQLReadDate("fistarihi") <> #1/1/1950# Then
                            oDespatchInvoice.IssueDate = oSQL1.SQLReadDate("fistarihi")     ' irsaliye tarihi
                            oDespatchInvoice.IssueTime = oSQL1.SQLReadDate("fistarihi")     ' irsaliye tarihi
                        Else
                            oDespatchInvoice.IssueDate = DateTime.Now.Date                  ' irsaliye tarihi
                            oDespatchInvoice.IssueTime = DateTime.Now                       ' irsaliye saati
                        End If

                        ' Malları sevk even firmaya ait bilgiler
                        oDespatchInvoice.DespatchSupplierParty = New Party
                        oDespatchInvoice.DespatchSupplierParty.Name = oSQL1.SQLReadString("f1adi")
                        oDespatchInvoice.DespatchSupplierParty.TaxSchemeName = oSQL1.SQLReadString("f1vd")
                        oDespatchInvoice.DespatchSupplierParty.IDScheme = oSQL1.SQLReadString("f1vn")
                        oDespatchInvoice.DespatchSupplierParty.CountryName = oSQL1.SQLReadString("f1ulk")
                        oDespatchInvoice.DespatchSupplierParty.CityName = oSQL1.SQLReadString("f1shr")
                        oDespatchInvoice.DespatchSupplierParty.CitySubdivisionName = oSQL1.SQLReadString("f1smt")
                        oDespatchInvoice.DespatchSupplierParty.StreetName = oSQL1.SQLReadString("f1adr")
                        oDespatchInvoice.DespatchSupplierParty.Telefax = oSQL1.SQLReadString("f1fax")
                        oDespatchInvoice.DespatchSupplierParty.Telephone = oSQL1.SQLReadString("f1tel1")
                        oDespatchInvoice.DespatchSupplierParty.ElectronicMail = oSQL1.SQLReadString("f1email")
                        oDespatchInvoice.DespatchSupplierParty.PostalZone = oSQL1.SQLReadString("f1pk")

                        ' Malları Teslim alan firmaya ait bilgiler
                        oDespatchInvoice.DeliveryCustomerParty = New Party
                        oDespatchInvoice.DeliveryCustomerParty.Name = oSQL1.SQLReadString("f2adi")
                        oDespatchInvoice.DeliveryCustomerParty.TaxSchemeName = oSQL1.SQLReadString("f2vd")
                        oDespatchInvoice.DeliveryCustomerParty.IDScheme = oSQL1.SQLReadString("f2vn")
                        oDespatchInvoice.DeliveryCustomerParty.CountryName = oSQL1.SQLReadString("f2ulk")
                        oDespatchInvoice.DeliveryCustomerParty.CityName = oSQL1.SQLReadString("f2shr")
                        oDespatchInvoice.DeliveryCustomerParty.CitySubdivisionName = oSQL1.SQLReadString("f2smt")
                        oDespatchInvoice.DeliveryCustomerParty.StreetName = oSQL1.SQLReadString("f2adr")
                        oDespatchInvoice.DeliveryCustomerParty.Telefax = oSQL1.SQLReadString("f2fax")
                        oDespatchInvoice.DeliveryCustomerParty.Telephone = oSQL1.SQLReadString("f2tel1")
                        oDespatchInvoice.DeliveryCustomerParty.ElectronicMail = oSQL1.SQLReadString("f2email")
                        oDespatchInvoice.DeliveryCustomerParty.PostalZone = oSQL1.SQLReadString("f2pk")

                        ' Taşıyıcı bilgisi
                        oDespatchInvoice.Shipment = New Shipment
                        oDespatchInvoice.Shipment.ActualDespatchDate = DateTime.Now.Date
                        oDespatchInvoice.Shipment.ActualDespatchTime = DateTime.Now
                        oDespatchInvoice.Shipment.ActualDeliveryDate = DateTime.Now.Date
                        oDespatchInvoice.Shipment.ActualDeliveryTime = DateTime.Now
                        oDespatchInvoice.Shipment.ValueAmount = "0"
                        oDespatchInvoice.Shipment.ValueAmountScheme = "TRY"
                        oDespatchInvoice.Shipment.CountryName = oSQL1.SQLReadString("f3ulk")
                        oDespatchInvoice.Shipment.CityName = oSQL1.SQLReadString("f3shr")
                        oDespatchInvoice.Shipment.CitySubdivisionName = oSQL1.SQLReadString("f3smt")
                        oDespatchInvoice.Shipment.StreetName = oSQL1.SQLReadString("f3adr")
                        oDespatchInvoice.Shipment.PostalZone = oSQL1.SQLReadString("f2pk")
                        oDespatchInvoice.Shipment.LicensePlateID = oSQL1.SQLReadString("aracplaka")

                        Dim oDespatchDriver As New DriverPerson
                        oDespatchDriver.FirstName = oSQL1.SQLReadString("soforadi")
                        oDespatchDriver.FamilyName = oSQL1.SQLReadString("soforsoyadi")
                        oDespatchDriver.NationalityID = oSQL1.SQLReadString("sofortckn")
                        oDespatchInvoice.Shipment.DriverPerson = {oDespatchDriver}

                        nLineCount = -1

                        If cUFE = "Ana model tipi + model no gruplu" Then
                            oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(10, cFisNo)
                        Else
                            oSQL2.cSQLQuery = GetSQLQueryeIrsaliye(6, cFisNo)
                        End If

                        oSQL2.GetSQLReader()

                        Do While oSQL2.oReader.Read

                            cID = oSQL2.SQLReadString("anamodeltipi")
                            cKodu = oSQL2.SQLReadString("aciklama")

                            If cUFE = "Ana model tipi + model no gruplu" Then
                                cAdi = oSQL2.SQLReadString("modelno")
                            Else
                                cAdi = oSQL2.SQLReadString("modelno") +
                                    IIf(oSQL2.SQLReadString("renk") = "", "", " " + oSQL2.SQLReadString("renk")) +
                                    IIf(oSQL2.SQLReadString("beden") = "", "", " " + oSQL2.SQLReadString("beden"))
                            End If

                            nMiktar = oSQL2.SQLReadDouble("adet")

                            cBirim = "ADET"

                            nLineCount = nLineCount + 1

                            oDespatchLine = New DespatchLine
                            oDespatchLine.ID = cID
                            oDespatchLine.LineID = CStr(nLineCount + 1)
                            oDespatchLine.ItemName = cKodu
                            oDespatchLine.Description = cAdi
                            oDespatchLine.DeliveredQuantity = nMiktar.ToString
                            oDespatchLine.DeliveredQuantityUnitCode = "ADET"
                            oDespatchLine.PriceAmount = "0"

                            despatchLines(nLineCount) = oDespatchLine
                        Loop
                        oSQL2.oReader.Close()
                        oSQL2.oReader = Nothing
                    End If
                    oSQL1.oReader.Close()
                    oSQL1.oReader = Nothing
            End Select

            oDespatchInvoice.DespatchLine = despatchLines

            Dim oIZGYG As New IrsaliyeZarfGonderYapisalGirdi
            Dim oIZGYC As New IrsaliyeZarfGonderYapisalCikti

            Dim oIG As New IrsaliyeGonderim
            oIG.aliciVkn = oDespatchInvoice.DeliveryCustomerParty.IDScheme
            oIG.aliciAlias = oDespatchInvoice.DeliveryCustomerParty.Name
            oIG.despatch = oDespatchInvoice
            oIG.erpNo = cFisNo
            oIG.gonderenVkn = oDespatchInvoice.DespatchSupplierParty.IDScheme
            oIG.gonderenAlias = oDespatchInvoice.DespatchSupplierParty.Name

            Dim oIK As New IrsaliyeKimlik
            oIK.Kullanici = cUsername
            oIK.Sifre = cPassword

            Dim oIZ As New IrsaliyeZarf
            oIZ.ZarfETTN = Guid.NewGuid()

            oIZGYG.Belgeler = {oIG}
            oIZGYG.Kimlik = oIK
            oIZGYG.KimlikNo = ""
            oIZGYG.XSLTRumuzu = ""
            oIZGYG.Zarf = oIZ

            oIZGYC = oClient.IrsaliyeZarfGonderYapisal(oIZGYG)

            If oIZGYC.Sonuc = "1" Then
                ' başarılı
                cMessage = String.Format("Belge başarıyla gönderildi. ID: {0} ", oIZGYC.SistemTarafindanVerilenIrsaliyeNumaralari(0).IRsaliyeNo)
                AddEventLog(cMessage, 1)

                cIrsaliyeID = oIZGYC.SistemTarafindanVerilenIrsaliyeNumaralari(0).IrsaliyeETTN.ToString
                cIrsaliyeNumarasi = oIZGYC.SistemTarafindanVerilenIrsaliyeNumaralari(0).IRsaliyeNo

                nDay = oDespatchInvoice.IssueDate.Day
                nMonth = oDespatchInvoice.IssueDate.Month
                nYear = oDespatchInvoice.IssueDate.Year
                nHour = oDespatchInvoice.IssueDate.Hour
                nMinute = oDespatchInvoice.IssueDate.Minute

                cTarihSaat = Strings.Format(nDay, "00") + "-" + Strings.Format(nMonth, "00") + "-" + Strings.Format(nYear, "0000") + " " +
                                 Strings.Format(nHour, "00") + ":" + Strings.Format(nMinute, "00") + ":00"

                eIrsaliyeUpdateWinTex(nCase, cFisNo, cIrsaliyeNumarasi, cIrsaliyeID, cTarihSaat)

                SendEIrsaliye = True

                If lPDF Then
                    Dim oIIG As New IrsaliyeIndirGirdi
                    oIIG.IrsaliyeETTN = oIZGYC.SistemTarafindanVerilenIrsaliyeNumaralari(0).IrsaliyeETTN
                    oIIG.Kimlik = oIK

                    Dim oIIC As New IrsaliyeIndirCikti

                    oIIC = oClient.IrsaliyeIndir(oIIG)

                    If oIIC.Sonuc = "1" Then
                        Dim oIrsaliye() As Byte = oIIC.Icerik
                        eIrsaliyeStoreDocument(cFisNo, cFisTipi, oIrsaliye)
                    End If
                End If
            Else
                cMessage = String.Format("Belge gönderilirken hata oluştu {0} ", oIZGYC.Mesaj)
                AddEventLog(cMessage, 2)
                SendEIrsaliye = False
            End If

        Catch ex As Exception
            ErrDisp("SendEIrsaliye", "eDokSisIrsaliye",,, ex)
        End Try
    End Function

    Public Function DespatchPDF(ByVal cIrsaliyeID As String, Optional ByVal cFisNo As String = "", Optional ByVal cFisTipi As String = "") As String

        DespatchPDF = ""

        Try
            'Dim cMessage As String = ""
            'Dim oIrsaliye() As Byte = oClient.GidenIrsaliyePdfAl(cToken, CLng(cIrsaliyeID))

            'If oIrsaliye Is Nothing Then
            '    cMessage = String.Format("PDF alınamadı {0} ", cIrsaliyeID)
            '    AddEventLog(cMessage, 2)
            'Else
            '    cMessage = String.Format("PDF alındı {0} ", cIrsaliyeID)
            '    AddEventLog(cMessage, 1)
            '    DespatchPDF = eIrsaliyeStoreDocument(cFisNo, cFisTipi, oIrsaliye)
            'End If

        Catch ex As Exception
            ErrDisp("DespatchPDF", "eIrsaliye",,, ex)
        End Try
    End Function

End Class
