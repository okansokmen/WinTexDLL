Option Explicit On
Option Strict On

Imports System.Data
Imports System.Data.SqlClient

Module UtilWUA

    Public lTatillerLoaded As Boolean = False
    Public aTatil() As Date

    Private Structure oVardiya
        Dim dTarih As Date
        Dim cVardiya As String
        Dim nDuzine As Integer
    End Structure

    Public Function WUAKonteynerAktar(cDatabase As String) As Boolean

        Dim cSQL As String = ""

        WUAKonteynerAktar = False

        Try
            Dim oSQL3 As New SQLServerClass(False, "192.168.2.4", "wintex", "sa", "NM121200.")
            Dim oSQL2 As New SQLServerClass(False, "192.168.2.4", "wintex", "sa", "NM121200.")
            Dim oSQL As New SQLServerClass(True,, cDatabase)

            Dim cPartiRefno As String = ""
            Dim nPartiNo As Double = 0
            Dim cWUAUretFisNo As String = ""
            Dim nWUAUretFisNo As Double = 0
            Dim nWuaUretLinesNo As Double = 0
            Dim nOperasyonBarcodeNo As Double = 0
            Dim cLineRefNo As String = ""
            Dim cislemAciklama As String = "ORME"
            Dim nStandartSure As Double = 0
            Dim dTarih As Date = #1/1/1950#
            Dim dSimdi As Date
            Dim dWUAFisTarihi As Date
            Dim cOrmeIsEmriNo As String = ""
            Dim nOrmeYagePartiRefNo As Double = 0
            Dim nMakineSayisi As Double = 0
            Dim cModelNo As String = ""
            Dim cRenk As String = ""
            Dim cBeden As String = ""
            Dim aVardiya() As oVardiya
            Dim nBaslangicVardiya As Integer = 0
            Dim nBitisVardiya As Integer = 0
            Dim cBarkod As String = ""
            Dim aBarkod() As String
            Dim cYagePartiRef As String
            Dim nDuzine As Double = 0
            Dim nSiraNo As Double = 0
            Dim cVardiya As String = ""
            Dim cUTF As String = ""
            Dim nTartiKG As Double = 0

            LoadTatiller(cDatabase)

            dSimdi = Now

            ReDim aVardiya(3)

            If Not oSQL.OpenConn() Then Exit Function
            If Not oSQL2.OpenConn() Then Exit Function
            If Not oSQL3.OpenConn() Then Exit Function

            cSQL = "select barkod, baslangicvardiya, bitisvardiya, vardiya1duzine, vardiya2duzine, " +
                    " vardiya3duzine, vardiya4duzine, sirano, durum, tarih, " +
                    " wintexaktarim " +
                    " from konveyor with (NOLOCK) " +
                    " where durum = 1 " +
                    " and wintexaktarim Is null " +
                    " order by sirano "

            oSQL2.GetSQLReader(cSQL)

            Do While oSQL2.oReader.Read

                cBarkod = oSQL2.SQLReadString("barkod")
                nBaslangicVardiya = oSQL2.SQLReadInteger("baslangicvardiya")
                nBitisVardiya = oSQL2.SQLReadInteger("bitisvardiya")
                dTarih = oSQL2.SQLReadDate("tarih")
                nSiraNo = oSQL2.SQLReadDouble("sirano")

                aVardiya(0).nDuzine = oSQL2.SQLReadInteger("vardiya1duzine")
                aVardiya(1).nDuzine = oSQL2.SQLReadInteger("vardiya2duzine")
                aVardiya(2).nDuzine = oSQL2.SQLReadInteger("vardiya3duzine")
                aVardiya(3).nDuzine = oSQL2.SQLReadInteger("vardiya4duzine")
                nDuzine = CDbl(aVardiya(0).nDuzine + aVardiya(1).nDuzine + aVardiya(2).nDuzine + aVardiya(3).nDuzine)

                If nBaslangicVardiya = 1 Then
                    aVardiya(0).cVardiya = "SABAH"
                    aVardiya(1).cVardiya = "AKSAM"
                    aVardiya(2).cVardiya = "GECE"
                    aVardiya(3).cVardiya = "SABAH"
                ElseIf nBaslangicVardiya = 2 Then
                    aVardiya(0).cVardiya = "AKSAM"
                    aVardiya(1).cVardiya = "GECE"
                    aVardiya(2).cVardiya = "SABAH"
                    aVardiya(3).cVardiya = "AKSAM"
                ElseIf nBaslangicVardiya = 3 Then
                    aVardiya(0).cVardiya = "GECE"
                    aVardiya(1).cVardiya = "SABAH"
                    aVardiya(2).cVardiya = "AKSAM"
                    aVardiya(3).cVardiya = "GECE"
                End If

                If aVardiya(3).nDuzine > 0 Then
                    ' 4 vardiya sürmüş
                    If nBitisVardiya = 1 Then
                        aVardiya(3).dTarih = dTarih
                        aVardiya(2).dTarih = GetPrevBusinessDay(dTarih)
                        aVardiya(1).dTarih = GetPrevBusinessDay(dTarih)
                        aVardiya(0).dTarih = GetPrevBusinessDay(dTarih)
                    ElseIf nBitisVardiya = 2 Then
                        aVardiya(3).dTarih = dTarih
                        aVardiya(2).dTarih = dTarih
                        aVardiya(1).dTarih = GetPrevBusinessDay(dTarih)
                        aVardiya(0).dTarih = GetPrevBusinessDay(dTarih)
                    ElseIf nBitisVardiya = 3 Then
                        aVardiya(3).dTarih = dTarih
                        aVardiya(2).dTarih = dTarih
                        aVardiya(1).dTarih = dTarih
                        aVardiya(0).dTarih = GetPrevBusinessDay(dTarih)
                    End If
                ElseIf aVardiya(2).nDuzine > 0 Then
                    ' 3 vardiya sürmüş
                    If nBitisVardiya = 1 Then
                        aVardiya(2).dTarih = dTarih
                        aVardiya(1).dTarih = GetPrevBusinessDay(dTarih)
                        aVardiya(0).dTarih = GetPrevBusinessDay(dTarih)
                    ElseIf nBitisVardiya = 2 Then
                        aVardiya(2).dTarih = dTarih
                        aVardiya(1).dTarih = dTarih
                        aVardiya(0).dTarih = GetPrevBusinessDay(dTarih)
                    ElseIf nBitisVardiya = 3 Then
                        aVardiya(2).dTarih = dTarih
                        aVardiya(1).dTarih = dTarih
                        aVardiya(0).dTarih = dTarih
                    End If
                ElseIf aVardiya(1).nDuzine > 0 Then
                    ' 2 vardiya sürmüş
                    If nBitisVardiya = 1 Then
                        aVardiya(1).dTarih = dTarih
                        aVardiya(0).dTarih = GetPrevBusinessDay(dTarih)
                    ElseIf nBitisVardiya = 2 Then
                        aVardiya(1).dTarih = dTarih
                        aVardiya(0).dTarih = dTarih
                    ElseIf nBitisVardiya = 3 Then
                        aVardiya(1).dTarih = dTarih
                        aVardiya(0).dTarih = dTarih
                    End If
                ElseIf aVardiya(0).nDuzine > 0 Then
                    ' 1 vardiya sürmüş
                    If nBitisVardiya = 1 Then
                        aVardiya(0).dTarih = dTarih
                    ElseIf nBitisVardiya = 2 Then
                        aVardiya(0).dTarih = dTarih
                    ElseIf nBitisVardiya = 3 Then
                        aVardiya(0).dTarih = dTarih
                    End If
                End If

                dWUAFisTarihi = aVardiya(0).dTarih
                cVardiya = aVardiya(0).cVardiya
                aBarkod = Split(cBarkod, ";")
                cYagePartiRef = aBarkod(0)

                ' Örme parti numaralarını bul (ilk orme partisinden)

                cSQL = "select top 1 a.uretimtakipno, a.uretimisemrino, a.modelno, a.renk, b.beden, a.partirefno, b.partino, b.yagepartiref " +
                        " from wuaparti a with (NOLOCK) , wuapartilines b with (NOLOCK)  " +
                        " where a.partirefno = b.partirefno " +
                        " And a.departman = 'ORME' " +
                        " and a.firma = 'DAHILI' " +
                        " and b.yagepartiref = " + cYagePartiRef +
                        " order by a.partirefno "

                oSQL.GetSQLReader(cSQL)

                If oSQL.oReader.Read Then
                    cUTF = oSQL.SQLReadString("uretimtakipno")
                    cOrmeIsEmriNo = oSQL.SQLReadString("uretimisemrino")
                    cModelNo = oSQL.SQLReadString("modelno")
                    cRenk = oSQL.SQLReadString("renk")
                    cBeden = oSQL.SQLReadString("beden")
                    cPartiRefno = oSQL.SQLReadString("partirefno")
                    nPartiNo = CDbl(oSQL.SQLReadInteger("partino"))
                    nOrmeYagePartiRefNo = oSQL.SQLReadDouble("yagepartiref")
                End If
                oSQL.oReader.Close()

                ' örme operasyon barkodunu bul

                cSQL = "select top 1 linerefno, aciklama, departman, standartsure, notlar " +
                        " from wuamodeloperasyon with (NOLOCK)  " +
                        " where modelno =  '" + cModelNo.Trim + "' " +
                        " and departman = 'ORME' " +
                        " and islemkodu = 'ORME' "

                oSQL.GetSQLReader(cSQL)

                If oSQL.oReader.Read Then
                    cLineRefNo = oSQL.SQLReadDouble("linerefno").ToString
                    cislemAciklama = oSQL.SQLReadString("aciklama")
                    nStandartSure = oSQL.SQLReadDouble("standartsure")
                End If
                oSQL.oReader.Close()

                If cLineRefNo.Trim <> "" Then
                    ' örme operasyon barkod numarasını bul
                    cSQL = "select top 1 sirano  " +
                            " from wuabarcodelist with (NOLOCK)  " +
                            " where mkplsirano = " + cLineRefNo + " " +
                            " and yagepartirefno = " + nOrmeYagePartiRefNo.ToString

                    nOperasyonBarcodeNo = oSQL.DBReadDouble(cSQL)
                End If

                ' yeni fiş aç
                cSQL = "select top 1 wuauretfisno from sysinfo "

                oSQL.GetSQLReader(cSQL)

                If oSQL.oReader.Read Then
                    nWUAUretFisNo = oSQL.SQLReadDouble("wuauretfisno") + 1
                End If
                oSQL.oReader.Close()

                cWUAUretFisNo = Format(nWUAUretFisNo, "0000000000")

                cSQL = "select top 1 wuauretfisno " +
                        " from wuauretharfis with (NOLOCK)  " +
                        " where wuauretfisno  = '" + cWUAUretFisNo + "' "

                If oSQL.CheckExists(cSQL) Then
                    ' fiş no daha önceden kullanılmış
                    cSQL = "select maxwuauretfisno = max(wuauretfisno) " +
                            " from wuauretharfis "

                    oSQL.GetSQLReader(cSQL)

                    If oSQL.oReader.Read Then
                        nWUAUretFisNo = oSQL.SQLReadDouble("maxwuauretfisno") + 1
                        cWUAUretFisNo = Format(nWUAUretFisNo, "0000000000")
                    End If
                    oSQL.oReader.Close()
                End If

                cSQL = "set dateformat dmy " +
                        " insert into wuauretharfis " +
                        " (wuauretfisno, personel, tarih, bolum, Vardiya, " +
                        " Makine, Statu, wintexentegrasyon, createdbywuaonline, creationdate, " +
                        " modificationdate, username) "

                cSQL = cSQL +
                        " values ('" + cWUAUretFisNo + "', " +
                        " '', " +
                        " '" + Format(dWUAFisTarihi.Day, "00") + "/" + Format(dWUAFisTarihi.Month, "00") + "/" + Format(dWUAFisTarihi.Year, "0000") + "', " +
                        " '', " +
                        " '" + cVardiya.Trim + "', "

                cSQL = cSQL +
                        " '', " +
                        " 'b','H','H',getdate(), "

                cSQL = cSQL +
                        " getdate(), " +
                        " 'KONVEYOR' ) "

                oSQL.SQLExecute(cSQL)

                If nWUAUretFisNo <> 0 Then
                    cSQL = "update sysinfo set wuauretfisno = " + nWUAUretFisNo.ToString
                    oSQL.SQLExecute(cSQL)
                End If

                ' her harekette yeni satır açıyoruz

                cSQL = "select top 1 parametervalue " +
                        " from syspar with (NOLOCK)  " +
                        " where parametername = 'wuauretlinesno' "

                oSQL.GetSQLReader(cSQL)

                If oSQL.oReader.Read Then
                    nWuaUretLinesNo = Val(oSQL.SQLReadString("parametervalue"))
                End If
                oSQL.oReader.Close()

                cSQL = "update syspar " +
                        " set parametervalue = coalesce(parametervalue,0) + 1 " +
                        " where parametername = 'wuauretlinesno' "

                oSQL.SQLExecute(cSQL)

                nMakineSayisi = 0

                'cSQL = "SELECT top 1 makinesayisi " +
                '        " FROM wuamakineparkuru with (NOLOCK)  " +
                '        " WHERE makineno = '" + cMakina.Trim + "' "

                'dr = GetSQLReaderTransaction(cSQL, ConnYage, oTransaction)

                'If dr.Read() Then
                '    nMakineSayisi = Val(dr.GetValue(dr.GetOrdinal("makinesayisi")))
                'End If
                'dr.Close()
                'dr = Nothing

                cSQL = "set dateformat dmy " +
                        " insert into wuauretharfislines " +
                        " (wuauretfisno, barcode, satirno, partirefno, uretimtakipno, " +
                        " uretimisemrino, partino, modelno, renk, beden, " +
                        " islemkodu, aciklama, standartsure, departman, adet, " +
                        " statu, begdate, begtime, enddate, endtime, " +
                        " agirlik, yagepartiref, bayagepartiref, makinesayisi) "

                cSQL = cSQL +
                        " values ('" + cWUAUretFisNo + "', " +
                        " '" + Format(nOperasyonBarcodeNo, "000000000000") + "', " +
                        "  " + nWuaUretLinesNo.ToString + ", " +
                        "  " + CStr(Val(cPartiRefno)) + ", " +
                        " '" + cUTF.Trim + "', "

                cSQL = cSQL +
                        " '" + cOrmeIsEmriNo.Trim + "', " +
                        "  " + nPartiNo.ToString + ", " +
                        " '" + cModelNo.Trim + "', " +
                        " '" + cRenk.Trim + "', " +
                        " '" + cBeden.Trim + "', "

                cSQL = cSQL +
                        " 'ORME', " +
                        " '" + cislemAciklama.Trim + "', " +
                        "  " + nStandartSure.ToString + ", " +
                        " 'ORME', " +
                        nDuzine.ToString + ", "

                cSQL = cSQL +
                        " 'b', " +
                        " getdate(), " +
                        " '" + Format(dSimdi.Hour, "00") + ":" + Format(dSimdi.Minute, "00") + ":" + Format(dSimdi.Second, "00") + "', " +
                        " getdate(), " +
                        " '" + Format(dSimdi.Hour, "00") + ":" + Format(dSimdi.Minute, "00") + ":" + Format(dSimdi.Second, "00") + "', "

                cSQL = cSQL +
                        "  " + nTartiKG.ToString + ", " +
                        "  " + nOrmeYagePartiRefNo.ToString + ", " +
                        "  " + cYagePartiRef.Trim + ", " +
                        "  " + nMakineSayisi.ToString + " ) "

                oSQL.SQLExecute(cSQL)

                ' parti fişini ateşle

                cSQL = "update wuapartilines " +
                        " set adet = " + SQLWriteDecimal(nDuzine) +
                        " where yagepartiref = " + aBarkod(1)

                oSQL.SQLExecute(cSQL)

                ' okundu 

                cSQL = "update konveyor " +
                        " set wintexaktarim = getdate() " +
                        " where sirano = " + nSiraNo.ToString

                oSQL3.SQLExecute(cSQL)
            Loop
            oSQL2.oReader.Close()

            oSQL2.CloseConn()
            oSQL3.CloseConn()
            oSQL.CloseConn()

            oSQL = Nothing
            oSQL2 = Nothing
            oSQL3 = Nothing

            WUAKonteynerAktar = True

        Catch ex As Exception
            ErrDisp(ex, "WUAKonteynerAktar", cSQL)
        End Try
    End Function

    Private Sub LoadTatiller(cDatabase As String)

        Dim oSQL As New SQLServerClass(True,, cDatabase)
        Dim cSQL As String = ""
        Dim nCnt As Integer = -1
        Dim dTarih As Date = #1/1/1950#
        Dim dBaslangic As Date = #1/1/1950#
        Dim dBitis As Date = #1/1/1950#

        Try
            If lTatillerLoaded Then Exit Sub

            ReDim aTatil(0)

            oSQL.OpenConn()

            cSQL = "select tatilgunu, baslangic, bitis " +
                   " from tatilgunleri with (NOLOCK)  " +
                   " where tatilgunu is not null " +
                   " and tatilgunu <> '01.01.1950' " +
                   " order by tatilgunu "

            oSQL.GetSQLReader(cSQL)

            Do While oSQL.oReader.Read

                dBaslangic = DateValue(oSQL.SQLReadDate("baslangic").ToString)
                dBitis = TimeValue(oSQL.SQLReadDate("bitis").ToString)

                If dBitis > CDate("08:00:00") Then
                    dBitis = DateValue(oSQL.SQLReadDate("bitis").ToString)
                Else
                    dBitis = DateAdd(DateInterval.Day, -1, DateValue(oSQL.SQLReadDate("bitis").ToString))
                End If

                dTarih = dBaslangic
                Do While dTarih <= dBitis
                    nCnt = nCnt + 1
                    ReDim Preserve aTatil(nCnt)
                    aTatil(nCnt) = dTarih
                    dTarih = DateAdd(DateInterval.Day, 1, dTarih)
                Loop
            Loop

            oSQL.CloseConn()
            oSQL = Nothing

            lTatillerLoaded = True

        Catch ex As Exception
            ErrDisp(ex, "LoadTatiller", cSQL)
        End Try
    End Sub

    Private Function BusinessDay(ByVal dTarih As Date) As Boolean

        Dim nCnt As Integer = 0
        Dim lTatil As Boolean = False

        BusinessDay = False

        Try
            For nCnt = 0 To UBound(aTatil)
                If dTarih = aTatil(nCnt) Then
                    lTatil = True
                    Exit For
                ElseIf dTarih < aTatil(nCnt) Then
                    Exit For
                End If
            Next

            BusinessDay = Not lTatil

        Catch ex As Exception
            ErrDisp(ex, "BusinessDay")
        End Try
    End Function

    Private Function GetPrevBusinessDay(ByVal dTarih As Date) As Date

        Dim nCnt As Integer = 0
        Dim lTatil As Boolean = False

        GetPrevBusinessDay = #1/1/1950#

        Try
            For nCnt = 0 To UBound(aTatil)
                If dTarih = aTatil(nCnt) Then
                    lTatil = True
                    Exit For
                ElseIf dTarih < aTatil(nCnt) Then
                    Exit For
                End If
            Next

            If lTatil Then
                dTarih = DateAdd(DateInterval.Day, -1, dTarih)
                dTarih = GetPrevBusinessDay(dTarih)
            End If

            GetPrevBusinessDay = dTarih

        Catch ex As Exception
            ErrDisp(ex, "GetPrevBusinessDay")
        End Try
    End Function

    Public Function GetFBD(ByVal dTarih As Date, ByVal nDays As Integer) As Date
        ' get first available business day, BACKWARD
        Dim nCnt As Integer = 0

        GetFBD = #1/1/1950#

        Try
            GetFBD = dTarih
            If dTarih = CDate("01.01.1950") Then Exit Function

            If Not BusinessDay(dTarih) Then
                dTarih = GetPrevBusinessDay(dTarih)
            End If

            If nDays > 0 Then
                For nCnt = 1 To nDays
                    dTarih = DateAdd(DateInterval.Day, -1, dTarih)
                    dTarih = GetPrevBusinessDay(dTarih)
                Next
            End If

            GetFBD = dTarih

        Catch ex As Exception
            ErrDisp(ex, "GetFBD")
        End Try
    End Function

    Private Function GetNextBusinessDay(ByVal dTarih As Date) As Date

        Dim nCnt As Integer = 0
        Dim lTatil As Boolean = False

        GetNextBusinessDay = #1/1/1950#

        Try
            For nCnt = 0 To UBound(aTatil)
                If dTarih = aTatil(nCnt) Then
                    lTatil = True
                    Exit For
                ElseIf dTarih < aTatil(nCnt) Then
                    Exit For
                End If
            Next

            If lTatil Then
                dTarih = DateAdd(DateInterval.Day, 1, dTarih)
                dTarih = GetNextBusinessDay(dTarih)
            End If

            GetNextBusinessDay = dTarih

        Catch ex As Exception
            ErrDisp(ex, "GetNextBusinessDay")
        End Try
    End Function

    Public Function GetNBD(ByVal dTarih As Date, ByVal nDays As Integer) As Date
        ' get next available business day, FORWARD
        Dim nCnt As Integer = 0

        GetNBD = #1/1/1950#

        Try
            GetNBD = dTarih
            If dTarih = CDate("01.01.1950") Then Exit Function

            If Not BusinessDay(dTarih) Then
                dTarih = GetNextBusinessDay(dTarih)
            End If

            If nDays > 0 Then
                For nCnt = 1 To nDays
                    dTarih = DateAdd(DateInterval.Day, 1, dTarih)
                    dTarih = GetNextBusinessDay(dTarih)
                Next
            End If

            GetNBD = dTarih

        Catch ex As Exception
            ErrDisp(ex, "GetNBD")
        End Try
    End Function


End Module
