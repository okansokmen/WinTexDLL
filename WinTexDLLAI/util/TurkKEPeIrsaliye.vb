Option Explicit On

Imports WinTexDLL.tr.com.turkkep.efinttestws

Public Class TurkKEPeIrsaliye

    Public cToken As String = ""
    Dim cUsername As String = ""
    Dim cPassword As String = ""

    Dim oClient As EFaturaEntegrasyon2

    Public Sub New()
        Try
            ' token alan kod buradadır
            Dim oSQL As New SQLServerClass

            oSQL.OpenConn()

            Dim cURL As String = oSQL.GetSysPar("TurkKepService", "http://efinttestws.turkkep.com.tr/EFaturaEntegrasyon2.asmx")

            If oConnection.cOwner = "jeanci" Then
                cUsername = oSQL.GetSysPar("TurkKepUsername", "efatura1")
                cPassword = oSQL.GetSysPar("TurkKepPassword", "Efatura123!")
            Else
                cUsername = oSQL.GetSysPar("TurkKepUsername")
                cPassword = oSQL.GetSysPar("TurkKepPassword")
            End If

            oSQL.CloseConn()
            oSQL = Nothing

            oClient = New EFaturaEntegrasyon2()
            oClient.Url = cURL
            cToken = oClient.OturumAc(cUsername, cPassword)

        Catch ex As Exception
            ErrDisp("initWebService", "TurkKEPeIrsaliye",,, ex)
        End Try
    End Sub

    Protected Overrides Sub Finalize()

        On Error Resume Next

        oClient.Dispose()

        MyBase.Finalize()
    End Sub

    Public Function EArsivKalanKontorSorgula() As Long

        EArsivKalanKontorSorgula = 0

        Try
            Dim nKontor As Long = 0

            If cToken.Trim = "" Then Exit Function
            nKontor = oClient.EArsivKalanKontorSorgula(cToken)
            EArsivKalanKontorSorgula = nKontor

        Catch ex As Exception
            ErrDisp("EArsivKalanKontorSorgula", "TurkKEPeIrsaliye",,, ex)
        End Try

    End Function

    Public Function SendEIrsaliye(ByVal nCase As Integer, ByVal cFisNo As String, Optional ByVal lPDF As Boolean = False) As Boolean
        ' nCase = 1 Stok Fişi
        ' nCase = 2 Üretim Fişi

        SendEIrsaliye = False

        Try
            Dim oSQL1 As New SQLServerClass
            Dim oSQL2 As New SQLServerClass
            Dim oReturn As New SendDespatchInvoiceReturnType
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
            Dim cSFE As String = ""
            Dim cUFE As String = ""
            Dim nDay As Integer = 0
            Dim nMonth As Integer = 0
            Dim nYear As Integer = 0
            Dim nHour As Integer = 0
            Dim nMinute As Integer = 0
            Dim cTarihSaat As String = ""
            Dim cNotlar As String = ""
            Dim cID As String = ""
            Dim cUUID As String = ""
            Dim cMsg As String = ""
            Dim cMTF As String = ""
            Dim cUTF As String = ""
            Dim cDepartman As String = ""

            Dim notSayisi As Integer = 2
            Dim notes As Array = Array.CreateInstance(GetType(NoteType), notSayisi)
            Dim note1 As NoteType = New NoteType()
            Dim note2 As NoteType = New NoteType()

            oSQL1.OpenConn()
            oSQL2.OpenConn()

            cSFE = oSQL1.GetSysPar("irsservicestok", "Butun satirlar")
            cUFE = oSQL1.GetSysPar("irsserviceuretim", "Butun satirlar")

            Dim oDespatchInvoice As New DespatchInvoice()

            'oDespatchInvoice.DespatchInvoiceId = CLng(cFisNo)  UUID nin Gelir İdaresi Başkanlığında tekil olması gerekiyor doldurulmayacak
            'oDespatchInvoice.UUID = Guid.NewGuid().ToString()  TürkKep te tekil olması gerekiyor doldurulmayacak
            'oDespatchInvoice.DispatchReference = "JNI1234567890123"  Otomatik geri gelecek
            oDespatchInvoice.CopyIndicator = False
            oDespatchInvoice.Profile = Profile.TEMELIRSALIYE
            oDespatchInvoice.DespatchType = InvoiceType.SEVK ' InvoiceType.MATBUDAN
            oDespatchInvoice.DocumentCurrency = CurrencyType.TurkishLira
            oDespatchInvoice.PricingExchange = 1 ' Döviz Kuru
            oDespatchInvoice.TotalAmount = Convert.ToDecimal("0")
            oDespatchInvoice.SendDate = DateTime.Now.Date   ' gönderim tarihi
            oDespatchInvoice.SendTime = DateTime.Now        ' gönderim saati

            Select Case nCase
                Case 1
                    ' stok fişi
                    cFisTipi = "stokfis"
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
                    despatchLines = Array.CreateInstance(GetType(DespatchLine), nLineCount) 'irsaliye satırları

                    oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(1, cFisNo)

                    oSQL1.GetSQLReader()

                    If oSQL1.oReader.Read Then

                        cNotlar = oSQL1.SQLReadString("notlar", 100)

                        If oSQL1.SQLReadString("sevkno") = "07 Satis" Then
                            oDespatchInvoice.NoteList = {"WinTex Stok Fis No : " + cFisNo.Trim,
                                                        "Siparisler:" + cMTF,
                                                        cNotlar,
                                                        "** SATIŞ **"}
                        Else
                            oDespatchInvoice.NoteList = {"WinTex Stok Fis No : " + cFisNo.Trim,
                                                        "Siparisler:" + cMTF,
                                                        cNotlar}
                        End If

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

                        ' Malları sevk eden firmaya ait bilgiler
                        oDespatchInvoice.SupplierParty = New Party
                        oDespatchInvoice.SupplierParty.CompanyName = oSQL1.SQLReadString("f1adi")
                        oDespatchInvoice.SupplierParty.TaxOrIdNo = oSQL1.SQLReadString("f1vn")
                        oDespatchInvoice.SupplierParty.TaxOffice = oSQL1.SQLReadString("f1vd")
                        oDespatchInvoice.SupplierParty.Country = oSQL1.SQLReadString("f1ulk")
                        oDespatchInvoice.SupplierParty.City = oSQL1.SQLReadString("f1shr")
                        oDespatchInvoice.SupplierParty.CitySubDivision = oSQL1.SQLReadString("f1smt")
                        oDespatchInvoice.SupplierParty.Street = oSQL1.SQLReadString("f1adr")
                        oDespatchInvoice.SupplierParty.FaxNumber = oSQL1.SQLReadString("f1fax")
                        oDespatchInvoice.SupplierParty.PhoneNumber = oSQL1.SQLReadString("f1tel1")
                        oDespatchInvoice.SupplierParty.Email = oSQL1.SQLReadString("f1email")
                        oDespatchInvoice.SupplierParty.ZipCode = oSQL1.SQLReadString("f1pk")
                        oDespatchInvoice.SupplierParty.PersonName = oSQL1.SQLReadString("f1sahis")
                        oDespatchInvoice.SupplierParty.PersonSurname = oSQL1.SQLReadString("f1soyad")

                        ' Malları Teslim alan firmaya ait bilgiler
                        oDespatchInvoice.CustomerParty = New Party
                        oDespatchInvoice.CustomerParty.CompanyName = oSQL1.SQLReadString("f2adi")
                        oDespatchInvoice.CustomerParty.TaxOrIdNo = oSQL1.SQLReadString("f2vn")
                        oDespatchInvoice.CustomerParty.TaxOffice = oSQL1.SQLReadString("f2vd")
                        oDespatchInvoice.CustomerParty.Country = oSQL1.SQLReadString("f2ulk")
                        oDespatchInvoice.CustomerParty.City = oSQL1.SQLReadString("f2shr")
                        oDespatchInvoice.CustomerParty.CitySubDivision = oSQL1.SQLReadString("f2smt")
                        oDespatchInvoice.CustomerParty.Street = oSQL1.SQLReadString("f2adr")
                        oDespatchInvoice.CustomerParty.FaxNumber = oSQL1.SQLReadString("f2fax")
                        oDespatchInvoice.CustomerParty.PhoneNumber = oSQL1.SQLReadString("f2tel1")
                        oDespatchInvoice.CustomerParty.Email = oSQL1.SQLReadString("f2email")
                        oDespatchInvoice.CustomerParty.ZipCode = oSQL1.SQLReadString("f2pk")
                        oDespatchInvoice.CustomerParty.PersonName = oSQL1.SQLReadString("f2sahis")
                        oDespatchInvoice.CustomerParty.PersonSurname = oSQL1.SQLReadString("f2soyad")

                        ' teslimat adresi
                        oDespatchInvoice.DispatchDeliveryParty = New DispatchDeliveryParty
                        oDespatchInvoice.DispatchDeliveryParty.Country = oSQL1.SQLReadString("f2ulk")
                        oDespatchInvoice.DispatchDeliveryParty.City = oSQL1.SQLReadString("f2shr")
                        oDespatchInvoice.DispatchDeliveryParty.CitySubDivision = oSQL1.SQLReadString("f2smt")
                        oDespatchInvoice.DispatchDeliveryParty.Street = oSQL1.SQLReadString("f2adr")
                        oDespatchInvoice.DispatchDeliveryParty.PostalCode = oSQL1.SQLReadString("f2pk")

                        oDespatchInvoice.CarrierTitle = oSQL1.SQLReadString("f3adi")
                        oDespatchInvoice.CarrierTradeRegNo = oSQL1.SQLReadString("f3vn")

                        If oSQL1.SQLReadString("sofortckn") <> "" Then
                            ' Sürücü bilgisi
                            Dim oDespatchDriver = New DespatchDriver
                            oDespatchDriver.FirstName = oSQL1.SQLReadString("soforadi")
                            oDespatchDriver.LastName = oSQL1.SQLReadString("soforsoyadi")
                            oDespatchDriver.NATIONALIDNO = oSQL1.SQLReadString("sofortckn")
                            oDespatchInvoice.DespatchDrivers = {oDespatchDriver}
                        End If

                        oDespatchInvoice.LicensePlate = oSQL1.SQLReadString("aracplaka")
                        oDespatchInvoice.TrailerPlate = oSQL1.SQLReadString("dorseplaka")

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
                                    cAdi = IIf(oSQL2.SQLReadString("renk") = "" Or oSQL2.SQLReadString("renk") = "HEPSI", "", " " + oSQL2.SQLReadString("renk")) +
                                           IIf(oSQL2.SQLReadString("beden") = "" Or oSQL2.SQLReadString("beden") = "HEPSI", "", " " + oSQL2.SQLReadString("beden")) +
                                           IIf(oSQL2.SQLReadString("malzemetakipkodu") = "", "", " " + oSQL2.SQLReadString("malzemetakipkodu"))
                            End Select

                            nMiktar = oSQL2.SQLReadDouble("netmiktar1")
                            cBirim = oSQL2.SQLReadString("birim1")

                            nLineCount = nLineCount + 1

                            oDespatchLine = New DespatchLine
                            oDespatchLine.DispatchLineNo = nLineCount + 1
                            oDespatchLine.ItemID = cID
                            oDespatchLine.ItemName = cAdi
                            oDespatchLine.RemarkNote = cKodu
                            oDespatchLine.Quantity = CDec(nMiktar)
                            oDespatchLine.PriceAmount = CDec(0)

                            Select Case cBirim.ToLower.Trim
                                Case "m2"
                                    oDespatchLine.QuantityType = QuantityType.M2
                                Case "m3"
                                    oDespatchLine.QuantityType = QuantityType.M3
                                Case "lt", "litre"
                                    oDespatchLine.QuantityType = QuantityType.Litre
                                Case "mt", "metre"
                                    oDespatchLine.QuantityType = QuantityType.M
                                Case "kg"
                                    oDespatchLine.QuantityType = QuantityType.KG
                                Case Else
                                    oDespatchLine.QuantityType = QuantityType.Adet
                            End Select

                            despatchLines(nLineCount) = oDespatchLine
                        Loop
                        oSQL2.oReader.Close()
                        oSQL2.oReader = Nothing

                    End If
                    oSQL1.oReader.Close()
                    oSQL1.oReader = Nothing
                Case 2
                    ' üretim fişi 
                    cFisTipi = "uretimfisi"
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
                    despatchLines = Array.CreateInstance(GetType(DespatchLine), nLineCount) 'irsaliye satırları

                    oSQL1.cSQLQuery = GetSQLQueryeIrsaliye(2, cFisNo)

                    oSQL1.GetSQLReader()

                    If oSQL1.oReader.Read Then

                        cDepartman = oSQL1.SQLReadString("girisdept")

                        oDespatchInvoice.NoteList = {"WinTex Uretim Fis No : " + cFisNo.Trim,
                                                         cDepartman + " işlemi yapılmak üzere",
                                                         "Siparisler:" + cUTF,
                                                         oSQL1.SQLReadString("notlar", 100)}

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
                        oDespatchInvoice.SupplierParty = New Party
                        oDespatchInvoice.SupplierParty.CompanyName = oSQL1.SQLReadString("f1adi")
                        oDespatchInvoice.SupplierParty.TaxOrIdNo = oSQL1.SQLReadString("f1vn")
                        oDespatchInvoice.SupplierParty.TaxOffice = oSQL1.SQLReadString("f1vd")
                        oDespatchInvoice.SupplierParty.Country = oSQL1.SQLReadString("f1ulk")
                        oDespatchInvoice.SupplierParty.City = oSQL1.SQLReadString("f1shr")
                        oDespatchInvoice.SupplierParty.CitySubDivision = oSQL1.SQLReadString("f1smt")
                        oDespatchInvoice.SupplierParty.Street = oSQL1.SQLReadString("f1adr")
                        oDespatchInvoice.SupplierParty.FaxNumber = oSQL1.SQLReadString("f1fax")
                        oDespatchInvoice.SupplierParty.PhoneNumber = oSQL1.SQLReadString("f1tel1")
                        oDespatchInvoice.SupplierParty.Email = oSQL1.SQLReadString("f1email")
                        oDespatchInvoice.SupplierParty.ZipCode = oSQL1.SQLReadString("f1pk")
                        oDespatchInvoice.SupplierParty.PersonName = oSQL1.SQLReadString("f1sahis")
                        oDespatchInvoice.SupplierParty.PersonSurname = oSQL1.SQLReadString("f1soyad")

                        ' Malları Teslim alan firmaya ait bilgiler
                        oDespatchInvoice.CustomerParty = New Party
                        oDespatchInvoice.CustomerParty.CompanyName = oSQL1.SQLReadString("f2adi")
                        oDespatchInvoice.CustomerParty.TaxOrIdNo = oSQL1.SQLReadString("f2vn")
                        oDespatchInvoice.CustomerParty.TaxOffice = oSQL1.SQLReadString("f2vd")
                        oDespatchInvoice.CustomerParty.Country = oSQL1.SQLReadString("f2ulk")
                        oDespatchInvoice.CustomerParty.City = oSQL1.SQLReadString("f2shr")
                        oDespatchInvoice.CustomerParty.CitySubDivision = oSQL1.SQLReadString("f2smt")
                        oDespatchInvoice.CustomerParty.Street = oSQL1.SQLReadString("f2adr")
                        oDespatchInvoice.CustomerParty.FaxNumber = oSQL1.SQLReadString("f2fax")
                        oDespatchInvoice.CustomerParty.PhoneNumber = oSQL1.SQLReadString("f2tel1")
                        oDespatchInvoice.CustomerParty.Email = oSQL1.SQLReadString("f2email")
                        oDespatchInvoice.CustomerParty.ZipCode = oSQL1.SQLReadString("f2pk")
                        oDespatchInvoice.CustomerParty.PersonName = oSQL1.SQLReadString("f2sahis")
                        oDespatchInvoice.CustomerParty.PersonSurname = oSQL1.SQLReadString("f2soyad")

                        ' teslimat adresi
                        oDespatchInvoice.DispatchDeliveryParty = New DispatchDeliveryParty
                        oDespatchInvoice.DispatchDeliveryParty.Country = oSQL1.SQLReadString("f2ulk")
                        oDespatchInvoice.DispatchDeliveryParty.City = oSQL1.SQLReadString("f2shr")
                        oDespatchInvoice.DispatchDeliveryParty.CitySubDivision = oSQL1.SQLReadString("f2smt")
                        oDespatchInvoice.DispatchDeliveryParty.Street = oSQL1.SQLReadString("f2adr")
                        oDespatchInvoice.DispatchDeliveryParty.PostalCode = oSQL1.SQLReadString("f2pk")

                        oDespatchInvoice.CarrierTitle = oSQL1.SQLReadString("f3adi")
                        oDespatchInvoice.CarrierTradeRegNo = oSQL1.SQLReadString("f3vn")

                        If oSQL1.SQLReadString("sofortckn") <> "" Then
                            Dim oDespatchDriver = New DespatchDriver
                            oDespatchDriver.FirstName = oSQL1.SQLReadString("soforadi")
                            oDespatchDriver.LastName = oSQL1.SQLReadString("soforsoyadi")
                            oDespatchDriver.NATIONALIDNO = oSQL1.SQLReadString("sofortckn")
                            oDespatchInvoice.DespatchDrivers = {oDespatchDriver}
                        End If

                        oDespatchInvoice.LicensePlate = oSQL1.SQLReadString("aracplaka")
                        oDespatchInvoice.TrailerPlate = oSQL1.SQLReadString("dorseplaka")

                        ' Sürücü bilgisi

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
                                    cAdi = oSQL2.SQLReadString("musteri") + " " + oSQL2.SQLReadString("aciklama")
                                Case Else
                                    cID = oSQL2.SQLReadString("anamodeltipi")
                                    cKodu = oSQL2.SQLReadString("aciklama")
                                    cAdi = oSQL2.SQLReadString("modelno") +
                                           IIf(oSQL2.SQLReadString("renk") = "", "", " " + oSQL2.SQLReadString("renk")) +
                                           IIf(oSQL2.SQLReadString("beden") = "", "", " " + oSQL2.SQLReadString("beden"))
                            End Select

                            nMiktar = oSQL2.SQLReadDouble("adet")

                            cBirim = "ADET"

                            nLineCount = nLineCount + 1

                            oDespatchLine = New DespatchLine
                            oDespatchLine.DispatchLineNo = nLineCount + 1
                            oDespatchLine.ItemID = cID
                            oDespatchLine.ItemName = cAdi
                            oDespatchLine.RemarkNote = cKodu
                            oDespatchLine.Quantity = CDec(nMiktar)
                            oDespatchLine.PriceAmount = CDec(0)
                            oDespatchLine.QuantityType = QuantityType.Adet

                            despatchLines(nLineCount) = oDespatchLine
                        Loop
                        oSQL2.oReader.Close()
                        oSQL2.oReader = Nothing

                    End If
                    oSQL1.oReader.Close()
                    oSQL1.oReader = Nothing
            End Select

            oDespatchInvoice.Lines = despatchLines

            oReturn = oClient.IrsaliyeFaturaGonder(cToken, oDespatchInvoice, True)

            Select Case oReturn.ServiceResult
                Case ServiceResult.SUCCESFULL
                    cMessage = String.Format("Belge başarıyla gönderildi. ID: {0} ", oReturn.InvoiceId.ToString.Trim)
                    CreateLog("WinTex_TurkKEP_SuccessLog", cMessage)

                    cIrsaliyeID = oReturn.InvoiceId.ToString.Trim
                    cIrsaliyeNumarasi = cIrsaliyeID

                    GetIrsaliyeNoUUID(cIrsaliyeID, cIrsaliyeNumarasi, cUUID)

                    nDay = oDespatchInvoice.IssueDate.Day
                    nMonth = oDespatchInvoice.IssueDate.Month
                    nYear = oDespatchInvoice.IssueDate.Year
                    nHour = oDespatchInvoice.IssueDate.Hour
                    nMinute = oDespatchInvoice.IssueDate.Minute

                    cTarihSaat = Strings.Format(nDay, "00") + "-" + Strings.Format(nMonth, "00") + "-" + Strings.Format(nYear, "0000") + " " +
                                 Strings.Format(nHour, "00") + ":" + Strings.Format(nMinute, "00") + ":00"

                    eIrsaliyeUpdateWinTex(nCase, cFisNo, cIrsaliyeNumarasi, cUUID, cTarihSaat, cIrsaliyeID)

                    SendEIrsaliye = True

                    If lPDF Then
                        DespatchPDF(cIrsaliyeID, cFisNo, cFisTipi)
                    End If

                Case Else
                    cMessage = String.Format("Belge gönderilirken hata oluştu {0} ", oReturn.StatusMessage)
                    CreateLog("WinTex_TurkKEP_FailLog", cMessage)
                    MsgBox(cMessage)
                    SendEIrsaliye = False
            End Select

            oSQL1.CloseConn()
            oSQL1 = Nothing

            oSQL2.CloseConn()
            oSQL2 = Nothing

        Catch ex As Exception
            ErrDisp("SendEIrsaliye", "TurkKEPeIrsaliye",,, ex)
        End Try
    End Function

    Public Function GetIrsaliyeNoUUID(ByVal cIrsaliyeID As String, ByRef cIrsaliyeNumarasi As String, ByRef cUUID As String) As Boolean

        GetIrsaliyeNoUUID = False

        Try
            If cToken.Trim = "" Then Exit Function

            Dim oDIRT As DespatchInvoiceReturnType = oClient.GidenIrsaliyeFaturaOku(cToken, InvoiceQueryType.INVOICEID, cIrsaliyeID)
            If Not (oDIRT Is Nothing) Then
                Dim oIrsaliye As DespatchInvoice = oDIRT.DespatchInvoice
                If Not (oIrsaliye Is Nothing) Then
                    If Not (oIrsaliye.DispatchReference Is Nothing) Then
                        cIrsaliyeNumarasi = oIrsaliye.DispatchReference.ToString.Trim
                        cUUID = oIrsaliye.UUID.ToString.Trim
                        GetIrsaliyeNoUUID = True
                    End If
                End If
            End If

        Catch ex As Exception
            ErrDisp("GetIrsaliyeNoUUID", "TurkKEPeIrsaliye",,, ex)
        End Try
    End Function

    Public Function DespatchPDF(ByVal cIrsaliyeID As String, Optional ByVal cFisNo As String = "", Optional ByVal cFisTipi As String = "") As String

        DespatchPDF = ""

        Try
            If cToken.Trim = "" Then Exit Function

            Dim cMessage As String = ""
            Dim oIrsaliye() As Byte = oClient.GidenIrsaliyePdfAl(cToken, CLng(cIrsaliyeID))

            If oIrsaliye Is Nothing Then
                cMessage = String.Format("PDF alınamadı {0} ", cIrsaliyeID)
                AddEventLog(cMessage, 2)
            Else
                cMessage = String.Format("PDF alındı {0} ", cIrsaliyeID)
                AddEventLog(cMessage, 1)
                DespatchPDF = eIrsaliyeStoreDocument(cFisNo, cFisTipi, oIrsaliye)
            End If

        Catch ex As Exception
            AddEventLog(String.Format("PDF hatasi {0} ", cIrsaliyeID) + " " + ex.Message, 1)
        End Try
    End Function

    Public Function GetStatus(ByVal cIrsaliyeID As String, ByRef cIrsaliyeNumarasi As String, ByRef cUUID As String) As String

        GetStatus = ""

        Try
            If cToken.Trim = "" Then Exit Function

            Dim cMessage As String = ""
            Dim oIrsaliye As DespatchInvoice = oClient.GidenIrsaliyeDurumSorgula(cToken, cIrsaliyeID)

            cIrsaliyeNumarasi = oIrsaliye.DispatchReference
            cUUID = oIrsaliye.UUID

            GetStatus = oIrsaliye.ResponseDescription.ToString.Trim

        Catch ex As Exception
            ErrDisp("GetStatus", "TurkKEPeIrsaliye",,, ex)
        End Try
    End Function

End Class
