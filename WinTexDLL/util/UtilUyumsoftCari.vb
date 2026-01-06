Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports Devart.Data.PostgreSql

Module UtilUyumsoftCari

    Const cModuleName As String = "UtilUyumsoftCari"

    Public Sub UyumCariWinTexUpdate()
        ' F ve R firmalarında cari id si aynı olacak
        Dim oSQL As SQLServerClass
        Dim cSQL As String = ""
        Dim nCol As Integer = 0
        Dim nRow As Integer = 0

        Dim oUyumPG As PostgreClass
        Dim cErrorMessage As String = ""
        'Dim oPGReader As PgSqlDataReader
        Dim cServer As String = "192.168.1.3"
        Dim cDatabase As String = "uyumsoft"
        Dim cUsername As String = "uyum"
        Dim cPassword As String = "12345"

        Dim oService As New GeneralB2B.GeneralB2BService
        Dim oDataTable As DataTable
        'Dim ofrmstatus As New frmStatus

        Dim cFirmaTipi As String = ""
        Dim cId As String = ""
        Dim cEntityCode As String = ""
        Dim cEntityName As String = ""
        Dim cAddress1 As String = ""
        Dim cAddress2 As String = ""
        Dim cAddress3 As String = ""
        Dim cCityName As String = ""
        Dim cCountryName As String = ""
        Dim cZipCode As String = ""
        Dim cTel1 As String = ""
        Dim cTel2 As String = ""
        Dim cFax As String = ""
        Dim cEmail As String = ""
        Dim cTaxOfficeName As String = ""
        Dim cTaxNo As String = ""
        Dim dCreateDate As Date = #1/1/1950#
        Dim cYabanci As String = ""
        Dim cTownName As String = ""
        Dim cIdentifyNo As String = ""
        Dim nDueDay As Double = 0
        Dim nDueDay2 As Double = 0
        Dim cTownId As String = ""
        Dim cTaxOfficeId As String = ""

        Try
            'ofrmstatus.init()

            initUyumServices(3) ' resmi üretim firmasından al
            oService.Url = oUyum.cURLGeneralB2BService
            oDataTable = oService.GetEntity(0)

            If oDataTable Is Nothing Then Exit Sub

            oSQL = New SQLServerClass
            oSQL.OpenConn()

            cSQL = "delete uyumfirma "
            oSQL.SQLExecute(cSQL)

            For nRow = 0 To oDataTable.Rows.Count - 1

                cFirmaTipi = ""
                cId = ""
                cEntityCode = ""
                cEntityName = ""
                cAddress1 = ""
                cAddress2 = ""
                cAddress3 = ""
                cCityName = ""
                cCountryName = ""
                cZipCode = ""
                cTel1 = ""
                cTel2 = ""
                cFax = ""
                cEmail = ""
                cTaxOfficeName = ""
                cTaxNo = ""
                dCreateDate = #1/1/1950#
                cYabanci = ""
                cTownName = ""
                cIdentifyNo = ""

                For nCol = 0 To oDataTable.Columns.Count - 1

                    Select Case oDataTable.Columns(nCol).ColumnName
                        Case "Id"
                            cId = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "TownName"
                            cTownName = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "IdentifyNo"
                            cIdentifyNo = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "EntityCode"
                            cEntityCode = oDataTable.Rows(nRow).ItemArray(nCol).ToString

                            Select Case Mid(cEntityCode, 1, 3)
                                Case "120"
                                    cFirmaTipi = "MUSTERi"
                                Case "320"
                                    cFirmaTipi = "TEDARiKCi"
                            End Select

                            If Mid(cEntityCode, 1, 6) = "320 03" Then
                                cYabanci = "E"
                            Else
                                cYabanci = "H"
                            End If
                        Case "EntityName"
                            cEntityName = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "Address1"
                            cAddress1 = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "Address2"
                            cAddress2 = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "Address3"
                            cAddress3 = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "CityName"
                            cCityName = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "CountryName"
                            cCountryName = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "ZipCode"
                            cZipCode = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "Tel1"
                            cTel1 = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "Tel2"
                            cTel2 = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "Fax"
                            cFax = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "Email"
                            cEmail = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "TaxOfficeName"
                            cTaxOfficeName = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "TaxNo"
                            cTaxNo = oDataTable.Rows(nRow).ItemArray(nCol).ToString
                        Case "CreateDate"
                            dCreateDate = CDate(oDataTable.Rows(nRow).ItemArray(nCol).ToString)
                        Case "Categories1ID"
                            cFirmaTipi = oDataTable.Rows(nRow).ItemArray(nCol).ToString.Trim
                            Select Case cFirmaTipi
                                Case "2143", "2144", "2148"
                                    cFirmaTipi = "FASONCU"
                                Case "2141", "2142", "2145", "2146"
                                    cFirmaTipi = "TEDARiKCi"
                            End Select
                    End Select
                Next

                cSQL = "insert uyumfirma (firma, aciklama, firmatipi, tel1, tel2, " +
                                            " fax, adres1, adres2, semt, sehir, " +
                                            " ulke, vergidairesi, vergino, email, postakodu, " +
                                            " yabanci, uyumid, createuser, username, creationdate, " +
                                            " modificationdate ) "
                cSQL = cSQL +
                            " values ('" + SQLWriteString(cEntityCode, 30) + "', " +
                            " '" + SQLWriteString(cEntityName, 100) + "', " +
                            " '" + SQLWriteString(cFirmaTipi, 30) + "', " +
                            " '" + SQLWriteString(cTel1, 30) + "', " +
                            " '" + SQLWriteString(cTel2, 30) + "', "

                cSQL = cSQL +
                                " '" + SQLWriteString(cFax, 30) + "', " +
                                " '" + SQLWriteString(cAddress1, 500) + "', " +
                                " '" + SQLWriteString(cAddress2, 299) + " " + SQLWriteString(cAddress3, 200) + "', " +
                                " '" + SQLWriteString(cTownName, 30) + "', " +
                                " '" + SQLWriteString(cCityName, 30) + "', "

                cSQL = cSQL +
                                " '" + SQLWriteString(cCountryName, 30) + "', " +
                                " '" + SQLWriteString(cTaxOfficeName, 30) + "', " +
                                " '" + IIf(cTaxNo.Trim = "", SQLWriteString(cIdentifyNo, 30), SQLWriteString(cTaxNo, 30)).ToString + "', " +
                                " '" + SQLWriteString(cEmail, 100) + "', " +
                                " '" + SQLWriteString(cZipCode, 30) + "', "

                cSQL = cSQL +
                                " '" + SQLWriteString(cYabanci, 1) + "', " +
                                cId + ", " +
                                " 'UYUM', " +
                                " 'UYUM', " +
                                " getdate(), "

                cSQL = cSQL +
                                " getdate() ) "

                oSQL.SQLExecute(cSQL)

                'cSQL = "select top 1 firma " +
                '        " from uyumfirma with (NOLOCK) " +
                '        " where uyumid = " + cId

                'If oSQL.CheckExists(cSQL) Then

                '    cSQL = "update uyumfirma set " +
                '            " tel1 = '" + SQLWriteString(cTel1, 30) + "', " +
                '            " tel2 = '" + SQLWriteString(cTel2, 30) + "', " +
                '            " fax = '" + SQLWriteString(cFax, 30) + "', " +
                '            " adres1 = '" + SQLWriteString(cAddress1, 500) + "', " +
                '            " adres2 = '" + SQLWriteString(cAddress2, 299) + " " + SQLWriteString(cAddress3, 200) + "', " +
                '            " semt = '" + SQLWriteString(cTownName, 30) + "', " +
                '            " sehir = '" + SQLWriteString(cCityName, 30) + "', " +
                '            " ulke = '" + SQLWriteString(cCountryName, 30) + "', " +
                '            " vergidairesi = '" + SQLWriteString(cTaxOfficeName, 30) + "', " +
                '            " vergino = '" + IIf(cTaxNo.Trim = "", SQLWriteString(cIdentifyNo, 30), SQLWriteString(cTaxNo, 30)).ToString + "', " +
                '            " email = '" + SQLWriteString(cEmail, 100) + "', " +
                '            " postakodu = '" + SQLWriteString(cZipCode, 30) + "', " +
                '            " yabanci = '" + SQLWriteString(cYabanci, 1) + "', " +
                '            " username = 'UYUM', " +
                '            " modificationdate = getdate() " +
                '            " where uyumid = " + cId

                '    oSQL.SQLExecute(cSQL)

                '    'ofrmstatus.ShowMessage("Firma : " + cEntityCode + " / " + cEntityName + " wintex firma tablosunda GUNCELLENDI. Uyumid : " + cId)

                '    CreateLog("UyumCariGuncelleme", cEntityCode + " / " + cEntityName + " -> " + cId)
                'Else
                '    cSQL = "select top 1 firma " +
                '            " from uyumfirma with (NOLOCK) " +
                '            " where firma = '" + SQLWriteString(cEntityCode, 30) + "' "

                '    If oSQL.CheckExists(cSQL) Then
                '        'cSQL = "update firma " +
                '        '        " set uyumid = " + cId +
                '        '        " where firma = '" + SQLWriteString(cEntityCode, 30) + "' "

                '        'ExecuteSQLCommandConnected(cSQL, Connyage)

                '        'ofrmstatus.ShowMessage("Firma : " + cEntityCode + " / " + cEntityName + " wintex firma tablosunda CAKISMA. Uyum uyumid : " + cId)

                '        CreateLog("UyumCariCakisma", cEntityCode + " / " + cEntityName + " -> " + cId)
                '    Else
                '        cSQL = "insert uyumfirma (firma, aciklama, firmatipi, tel1, tel2, " +
                '                            " fax, adres1, adres2, semt, sehir, " +
                '                            " ulke, vergidairesi, vergino, email, postakodu, " +
                '                            " yabanci, uyumid, createuser, username, creationdate, " +
                '                            " modificationdate ) "
                '        cSQL = cSQL +
                '            " values ('" + SQLWriteString(cEntityCode, 30) + "', " +
                '            " '" + SQLWriteString(cEntityName, 100) + "', " +
                '            " '" + SQLWriteString(cFirmaTipi, 30) + "', " +
                '            " '" + SQLWriteString(cTel1, 30) + "', " +
                '            " '" + SQLWriteString(cTel2, 30) + "', "

                '        cSQL = cSQL +
                '                " '" + SQLWriteString(cFax, 30) + "', " +
                '                " '" + SQLWriteString(cAddress1, 500) + "', " +
                '                " '" + SQLWriteString(cAddress2, 299) + " " + SQLWriteString(cAddress3, 200) + "', " +
                '                " '" + SQLWriteString(cTownName, 30) + "', " +
                '                " '" + SQLWriteString(cCityName, 30) + "', "

                '        cSQL = cSQL +
                '                " '" + SQLWriteString(cCountryName, 30) + "', " +
                '                " '" + SQLWriteString(cTaxOfficeName, 30) + "', " +
                '                " '" + IIf(cTaxNo.Trim = "", SQLWriteString(cIdentifyNo, 30), SQLWriteString(cTaxNo, 30)).ToString + "', " +
                '                " '" + SQLWriteString(cEmail, 100) + "', " +
                '                " '" + SQLWriteString(cZipCode, 30) + "', "

                '        cSQL = cSQL +
                '                " '" + SQLWriteString(cYabanci, 1) + "', " +
                '                cId + ", " +
                '                " 'UYUM', " +
                '                " 'UYUM', " +
                '                " getdate(), "

                '        cSQL = cSQL +
                '                " getdate() ) "

                '        oSQL.SQLExecute(cSQL)

                '        'ofrmstatus.ShowMessage("Firma : " + cEntityCode + " / " + cEntityName + " wintex uyumfirma tablosuna EKLENDI. Yeni uyumid : " + cId)

                '        CreateLog("UyumCariEkle", cEntityCode + " / " + cEntityName + " -> " + cId)
                '    End If
                'End If
            Next

            'ofrmstatus.ShowMessage("Firma aktarım işlemi tamamlandı")
            'ofrmstatus.WindowState = FormWindowState.Minimized

            cSQL = "select a.entity_id, " +
                    " b.entity_code, " +
                    " a.due_day_purchase, " +
                    " a.due_day, " +
                    " c.description, " +
                    " d.town_name " +
                    " from uyumsoft.find_co_entity a,  uyumsoft.find_entity b, uyumsoft.find_tax_office c, uyumsoft.gnld_town d " +
                    " where a.entity_id = b.entity_id " +
                    " And b.tax_office_id = c.tax_office_id " +
                    " And b.town_id = d.town_id " +
                    " order by b.entity_code "

            oUyumPG = New PostgreClass(cServer, cDatabase, cUsername, cPassword)
            oUyumPG.OpenConn()
            oUyumPG.GetSQLReader(cSQL)
            Do While oUyumPG.oReader.Read

                cId = oUyumPG.SQLReadDouble("entity_id").ToString
                cEntityCode = oUyumPG.SQLReadString("entity_code")
                cTownName = oUyumPG.SQLReadString("town_name")
                cTaxOfficeName = oUyumPG.SQLReadString("description")
                nDueDay = oUyumPG.SQLReadDouble("due_day_purchase")
                nDueDay2 = oUyumPG.SQLReadDouble("due_day")

                cSQL = "update uyumfirma set "

                If nDueDay <> 0 Then
                    cSQL = cSQL +
                        " duedatepurchase = " + SQLWriteDecimal(nDueDay) + ", "
                End If

                If nDueDay2 <> 0 Then
                    cSQL = cSQL +
                        " duedate = " + SQLWriteDecimal(nDueDay2) + ", "
                End If

                If cTownName.Trim <> "" Then
                    cSQL = cSQL +
                        " semt = '" + SQLWriteString(cTownName, 30) + "', "
                End If

                cSQL = cSQL +
                        " vergidairesi = '" + SQLWriteString(cTaxOfficeName, 30) + "' " +
                        " where uyumid = " + cId

                oSQL.SQLExecute(cSQL)
            Loop
            oUyumPG.oReader.Close()
            oUyumPG.CloseConn()

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("UyumCariWinTexUpdate", cModuleName,,, ex)
            'ofrmstatus.ShowMessage("Firma aktarım hatası : " + ex.Message.Trim)
        End Try
    End Sub

End Module
