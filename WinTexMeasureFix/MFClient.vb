Option Explicit On

Imports Dart.Sockets
Imports System.IO

Public Class MFClient

    Public cRemoteEndPoint As String = ""
    Public cSerialNo As String = ""
    Public cUserName As String = ""
    Public cMakineNo As String = ""
    Public cSiparisNo As String = ""
    Public cModelNo As String = ""
    Public cRenk As String = ""
    Public cBeden As String = ""
    Public cOlcuTablosuNo As String = ""
    Public cBarkod As String = ""

    Dim nMaxPoints As Integer = 14

    Private Structure oOlcu
        Dim nID As Double
        Dim nOlcu As Double
    End Structure

    Private Structure oOlcuTablosu
        Dim nId As Double
        Dim cOlcuTabosuNo As String
    End Structure

    Dim aOlcuTablolari() As oOlcuTablosu

    Public Function GetSipModelBeden() As String

        GetSipModelBeden = ""

        Try
            Dim oSQL As New SQLServerClass
            Dim cMSG As String = ""

            If cBarkod.Trim = "" Then Exit Function

            cBarkod = Mid(cBarkod, 1, 12)

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 siparisno, stokno, renk, beden " +
                            " from stokbarkod with (NOLOCK) " +
                            " where barcode = '" + cBarkod.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                cSiparisNo = oSQL.SQLReadString("siparisno")
                cModelNo = oSQL.SQLReadString("stokno")
                cRenk = oSQL.SQLReadString("renk")
                cBeden = oSQL.SQLReadString("beden")
            End If
            oSQL.oReader.Close()

            oSQL.CloseConn()

            If cSiparisNo <> "" Then
                GetSipModelBeden = OlcuTablosuSec()
            End If

        Catch ex As Exception
            ErrDisp(ex.Message, "GetSipModelBeden",,, ex)
        End Try
    End Function

    Public Sub WriteMeasures(cMessage As String)

        Try
            cMessage = Replace(cMessage, vbCr, "")
            cMessage = Replace(cMessage, vbLf, "")
            cMessage = Replace(cMessage, vbNullChar, "")

            Dim aOlcu() As oOlcu
            Dim cFisNo As String = ""
            Dim lOK As Boolean = False
            Dim nCnt As Integer = 0
            Dim nID As Double = 0
            Dim nOlcu As Double = 0
            Dim cSonuc As String = ""
            Dim nSure As Double = 0
            Dim oSQL As New SQLServerClass
            Dim oMeasures As Object = New System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(Of Object)(cMessage)
            Dim oMeasureArrayDetail As New Dictionary(Of String, Object)
            Dim oMeasureArray() As Object
            Dim oDict As New Dictionary(Of String, Object)
            Dim cOlcumYeri As String = ""

            ReDim aOlcu(0)

            oDict = oMeasures
            oMeasureArray = oDict.Item("params").item("Sections")

            For nCnt = 0 To oMeasureArray.GetUpperBound(0)
                lOK = True
                oMeasureArrayDetail = oMeasureArray(nCnt)

                ReDim Preserve aOlcu(nCnt)
                aOlcu(nCnt).nID = CDbl(oMeasureArrayDetail.Item("Id"))
                aOlcu(nCnt).nOlcu = CDbl(oMeasureArrayDetail.Item("Value"))
            Next
            cSonuc = oDict.Item("params").item("Status").ToString.Trim
            nSure = CDbl(oDict.Item("params").item("TimeRequired"))

            If lOK Then
                cFisNo = GetSequenceFisNo("qcfisno")

                oSQL.OpenConn()

                oSQL.cSQLQuery = "insert qcfis2 (fisno, createuser, creationdate) " +
                                " values ('" + cFisNo + "', " +
                                " '" + cUserName + "', " +
                                " getdate() ) "
                oSQL.SQLExecute()

                oSQL.cSQLQuery = "set dateformat dmy " +
                                " update qcfis2 set " +
                                " tarih = getdate(), " +
                                " personel = '" + SQLWriteString(cUserName, 30) + "', " +
                                " departman = 'UTU / PAKET', " +
                                " firma = 'DAHILI', " +
                                " makineno = '" + SQLWriteString(cMakineNo, 30) + "', " +
                                " siparisno = '" + SQLWriteString(cSiparisNo, 30) + "', " +
                                " modelno = '" + SQLWriteString(cModelNo, 30) + "', " +
                                " renk = '" + SQLWriteString(cRenk, 30) + "', " +
                                " beden = '" + SQLWriteString(cBeden, 30) + "', " +
                                " kktipi = '31-UTU PAKET OLCU KONTROL', " +
                                " olcutablosuno = '" + SQLWriteString(cOlcuTablosuNo, 30) + "', " +
                                " sonuc = '" + SQLWriteString(cSonuc, 30) + "', " +
                                " fistipi = 'qcara', " +
                                " username = '" + cUserName + "', " +
                                " modificationdate = getdate() " +
                                " where fisno = '" + cFisNo + "' "
                oSQL.SQLExecute()

                For nCnt = 0 To aOlcu.GetUpperBound(0)

                    oSQL.cSQLQuery = "select top 1 bolum " +
                                    " from sipolcu with (NOLOCK) " +
                                    " where siparisno = '" + cSiparisNo + "' " +
                                    " and olcutablosuno = '" + cOlcuTablosuNo + "' " +
                                    " and beden = '" + cBeden + "' " +
                                    " and satirno = " + aOlcu(nCnt).nID.ToString +
                                    " and bolum is not null " +
                                    " and bolum <> '' "

                    cOlcumYeri = oSQL.DBReadString()

                    oSQL.cSQLQuery = "insert qcfisolculines2 (fisno, olcumyeri, beden, olcu2, aciklama) " +
                                    " values ('" + cFisNo + "', " +
                                    " '" + SQLWriteString(cOlcumYeri, 100) + "', " +
                                    " '" + SQLWriteString(cBeden, 30) + "', " +
                                    " '" + SQLWriteDecimal(aOlcu(nCnt).nOlcu / 10) + "', " +
                                    " '" + SQLWriteDecimal(aOlcu(nCnt).nID) + "' ) "
                    oSQL.SQLExecute()
                Next

                oSQL.cSQLQuery = "update b set " +
                                " b.olcu1 = c.notlar, " +
                                " b.tolerans = c.parca, " +
                                " b.olcu3 = convert(float, coalesce(c.notlar,'0')) - convert(float, coalesce(c.parca,'0')), " +
                                " b.olcu4 = convert(float, coalesce(c.notlar,'0')) + convert(float, coalesce(c.parca,'0')), " +
                                " b.olcuok = case when convert(float, coalesce(b.olcu2,'0')) >= convert(float, coalesce(c.notlar,'0')) - convert(float, coalesce(c.parca,'0')) and " +
                                                     " convert(float, coalesce(b.olcu2,'0')) <= convert(float, coalesce(c.notlar,'0')) + convert(float, coalesce(c.parca,'0')) " +
                                            " then 'OK' " +
                                            " else 'FAIL' " +
                                            " end " +
                                " from qcfis2 a , qcfisolculines2 b , sipolcu c " +
                                " where a.fisno = b.fisno " +
                                " and a.siparisno = c.siparisno " +
                                " and a.olcutablosuno = c.olcutablosuno " +
                                " and b.aciklama = c.satirno " +
                                " and a.beden = c.beden " +
                                " and a.fisno = '" + cFisNo + "' "
                oSQL.SQLExecute()

                oSQL.CloseConn()
            End If

        Catch ex As Exception
            ErrDisp(ex.Message, "WriteMeasures",,, ex)
        End Try
    End Sub

    Private Function OlcuTablosuSec() As String

        OlcuTablosuSec = ""

        Try
            Dim oSQL As New SQLServerClass
            Dim nOlcuTablosuAdedi As Integer = 0
            Dim cMsg As String = ""
            Dim cSections As String = ""
            Dim cSection As String = ""
            Dim nID As Double = 0

            ReDim aOlcuTablolari(0)

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select count(distinct olcutablosuno) " +
                            " from sipolcu with (NOLOCK) " +
                            " where siparisno = '" + cSiparisNo.Trim + "' " +
                            " and notlar is not null " +
                            " and notlar <> '' " +
                            " and olcutablosuno is not null " +
                            " and olcutablosuno <> '' "

            nOlcuTablosuAdedi = oSQL.DBReadInteger()

            If nOlcuTablosuAdedi = 1 Then

                oSQL.cSQLQuery = "select top 1 olcutablosuno " +
                                " from sipolcu with (NOLOCK) " +
                                " where siparisno = '" + cSiparisNo.Trim + "' " +
                                " and notlar is not null " +
                                " and notlar <> '' " +
                                " and olcutablosuno is not null " +
                                " and olcutablosuno <> '' "

                cOlcuTablosuNo = oSQL.DBReadString

                aOlcuTablolari(0).nId = 0
                aOlcuTablolari(0).cOlcuTabosuNo = cOlcuTablosuNo

                OlcuTablosuSec = SendMeasures()

            ElseIf nOlcuTablosuAdedi > 1 Then

                cMsg = "{'jsonrpc':'2.0'," +
                        "'message':'GarmentIdSelect'," +
                        "'params':[*1*]}"

                oSQL.cSQLQuery = "select distinct olcutablosuno, olcutablosutipi " +
                                " from sipolcu with (NOLOCK) " +
                                " where siparisno = '" + cSiparisNo.Trim + "' " +
                                " and notlar is not null " +
                                " and notlar <> '' " +
                                " and olcutablosuno is not null " +
                                " and olcutablosuno <> '' " +
                                " order by olcutablosuno "

                oSQL.GetSQLReader()

                Do While oSQL.oReader.Read

                    nID = nID + 1

                    ReDim Preserve aOlcuTablolari(nID - 1)
                    aOlcuTablolari(nID - 1).nId = nID
                    aOlcuTablolari(nID - 1).cOlcuTabosuNo = oSQL.SQLReadString("olcutablosuno")

                    cSection = "{'ItemSet':" + nID.ToString + ", " +
                                "'ItemId':'" + nID.ToString + "', " +
                                "'SetName':'" + oSQL.SQLReadString("olcutablosutipi") + "', " +
                                "'Size':'" + cBeden + "', " +
                                "'Information':'" + oSQL.SQLReadString("olcutablosuno") + "', " +
                                "'Customer':'EROGLU', " +
                                "'ProductionOrder':'" + cSiparisNo + "'}"

                    If cSections.Trim = "" Then
                        cSections = cSection
                    Else
                        cSections = cSections + "," + cSection
                    End If
                Loop
                oSQL.oReader.Close()

                OlcuTablosuSec = Replace(cMsg, "*1*", cSections)

            End If

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp(ex.Message, "OlcuTablosuSec",,, ex)
        End Try
    End Function

    Public Function SelectOlcuTablosu(nSelectionId As Integer) As String

        SelectOlcuTablosu = ""

        Try
            cOlcuTablosuNo = aOlcuTablolari(nSelectionId - 1).cOlcuTabosuNo
            SelectOlcuTablosu = SendMeasures()

        Catch ex As Exception
            ErrDisp(ex.Message, "SelectOlcuTablosu",,, ex)
        End Try
    End Function

    Public Function SendMeasures() As String

        SendMeasures = ""

        Try
            Dim cBedenSeti As String = ""
            Dim cName As String = ""
            Dim nID As Double = -1
            Dim cOlcu As String = ""
            Dim nOlcu As Double = 0
            Dim nTolerans As Double = 0
            Dim cTolerans As String = ""
            Dim nMax As Double = 0
            Dim nMin As Double = 0
            Dim cSections As String = ""
            Dim cSection As String = ""
            Dim oSQL As New SQLServerClass
            Dim oSQL2 As New SQLServerClass
            Dim cMsg As String = ""
            Dim mf_sensor As String = ""
            Dim mf_laser0 As String = ""
            Dim mf_laser1 As String = ""
            Dim mf_extfunc As String = ""
            Dim mf_user0 As String = ""
            Dim mf_user1 As String = ""
            Dim mf_units As String = ""

            oSQL2.OpenConn()
            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 a.kullanicisipno, a.musterino, b.modelno, b.renk, " +
                            " kumas = (select top 1 coalesce(y.cinsaciklamasi,'') " +
                                    " from modelhammadde x with (NOLOCK) , stok y with (NOLOCK) " +
                                    " where x.hammaddekodu = y.stokno " +
                                    " and x.modelno = b.modelno " +
                                    " and x.anakumas = 'E') " +
                            " From siparis a with (NOLOCK) , sipmodel b with (NOLOCK) , ymodel c with (NOLOCK) " +
                            " Where a.kullanicisipno = b.siparisno " +
                            " And b.modelno = c.modelno " +
                            " And a.kullanicisipno = '" + cSiparisNo + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then

                cModelNo = oSQL.SQLReadString("modelno")
                cRenk = oSQL.SQLReadString("renk")

                cMsg = "{'jsonrpc':'2.0'," +
                        "'message':'ItemId'," +
                        "'params':{'General':{'InternalId':'1'," +
                                            "'ItemId':'" + cSiparisNo + "'," +
                                            "'GarmentInfoName':'" + cSiparisNo + "_" + cBeden + "'," +
                                            "'BundleSize':1," +
                                            "'BundleId':0," +
                                            "'Customer':'" + oSQL.SQLReadString("musterino") + "'," +
                                            "'ProductionOrder':'" + cSiparisNo + "'," +
                                            "'Style':'" + cModelNo + "'," +
                                            "'Fabric':'" + oSQL.SQLReadString("kumas") + "'," +
                                            "'Color':'" + cRenk + "'," +
                                            "'Size':'" + cBeden + "'}," +
                            "'Sections':[*1*]}}"
            End If
            oSQL.oReader.Close()

            If cOlcuTablosuNo.Trim = "" Then

                oSQL.cSQLQuery = "select distinct olcutablosuno " +
                                " from sipolcu with (NOLOCK) " +
                                " where siparisno = '" + cSiparisNo + "' " +
                                " and kullanilan = 'E' " +
                                " and notlar is not null " +
                                " and notlar <> '' " +
                                " and olcutablosuno is not null " +
                                " and olcutablosuno <> '' "

                cOlcuTablosuNo = oSQL.DBReadString()
            End If

            If cSiparisNo.Trim = "" Or cOlcuTablosuNo.Trim = "" Or cBeden.Trim = "" Then
                oSQL.CloseConn()
                Exit Function
            End If

            '  get max 14 measurepoints 
            oSQL.cSQLQuery = "select distinct top " + nMaxPoints.ToString + " satirno, bolum, renk, beden, notlar, parca " +
                            " from sipolcu with (NOLOCK) " +
                            " where siparisno = '" + cSiparisNo + "' " +
                            " and olcutablosuno = '" + cOlcuTablosuNo + "' " +
                            " and beden = '" + cBeden + "' " +
                            " and bolum is not null " +
                            " and notlar <> '' " +
                            " and notlar is not null " +
                            " and notlar <> '' " +
                            " and measurefix = 'E' " +
                            " order by satirno "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                nID = oSQL.SQLReadDouble("satirno") ' nID + 1 ' 
                cName = oSQL.SQLReadString("bolum")

                cOlcu = oSQL.SQLReadString("notlar")
                nOlcu = 0

                cTolerans = oSQL.SQLReadString("parca")
                nTolerans = 0

                If IsNumeric(cTolerans) Then
                    nTolerans = CDbl(cTolerans)
                End If

                If IsNumeric(cOlcu) Then
                    nOlcu = CDbl(cOlcu)
                    nMax = nOlcu + nTolerans
                    nMin = nOlcu - nTolerans

                    mf_sensor = "6"
                    mf_laser0 = "-1"
                    mf_laser1 = "-1"
                    mf_extfunc = "0"
                    mf_user0 = "0"
                    mf_user1 = "0"
                    mf_units = "0"

                    oSQL2.cSQLQuery = "select top 1 mf_sensor, mf_laser0, mf_laser1, mf_extfunc, mf_user0, mf_user1, mf_units " +
                                    " from olcuyeri with (NOLOCK) " +
                                    " where olcuyeri = '" + cName + "' "

                    oSQL2.GetSQLReader()

                    If oSQL2.oReader.Read Then
                        mf_sensor = oSQL2.SQLReadString("mf_sensor")
                        mf_laser0 = oSQL2.SQLReadString("mf_laser0")
                        mf_laser1 = oSQL2.SQLReadString("mf_laser1")
                        mf_extfunc = oSQL2.SQLReadString("mf_extfunc")
                        mf_user0 = oSQL2.SQLReadString("mf_user0")
                        mf_user1 = oSQL2.SQLReadString("mf_user1")
                        mf_units = oSQL2.SQLReadString("mf_units")
                    End If
                    oSQL2.oReader.Close()

                    If mf_sensor.Trim = "" Then mf_sensor = "6"
                    If mf_laser0.Trim = "" Then mf_laser0 = "-1"
                    If mf_laser1.Trim = "" Then mf_laser1 = "-1"
                    If mf_extfunc.Trim = "" Then mf_extfunc = "0"
                    If mf_user0.Trim = "" Then mf_user0 = "0"
                    If mf_user1.Trim = "" Then mf_user1 = "0"
                    If mf_units.Trim = "" Then mf_units = "0"

                    cSection = "{'Id':" + nID.ToString + "," +
                                "'Name':'" + Mid(cName, 1, 10).Trim + "'," +
                                "'Note':'" + cName + "'," +
                                "'Sensor':" + mf_sensor + "," +
                                "'Laser0':" + mf_laser0 + "," +
                                "'Laser1':" + mf_laser1 + "," +
                                "'ExtFunc':" + mf_extfunc + "," +
                                "'User0':" + mf_user0 + "," +
                                "'User1':" + mf_user1 + "," +
                                "'Units':" + mf_units + "," +
                                "'Expected':'" + Format(nOlcu * 10, "##0.0000") + "'," +
                                "'Min':'" + Format(nMin * 10, "##0.0000") + "'," +
                                "'Max':'" + Format(nMax * 10, "##0.0000") + "'}"

                    If cSections.Trim = "" Then
                        cSections = cSection
                    Else
                        cSections = cSections + "," + cSection
                    End If
                End If

            Loop
            oSQL.oReader.Close()

            oSQL.CloseConn()
            oSQL2.CloseConn()

            ' çalışan örnek mesaj 
            'cMsg = "{'jsonrpc':'2.0','message':'ItemId','params':{'General':{'InternalId':'1','ItemId':'D370401','GarmentInfoName':'D370401_32','BundleSize':1,'BundleId':0,'Customer':'PVH','ProductionOrder':'D370401','Style':'DENTON','Fabric':'DNM DENTON CALİ','Color':'Color','Size':'Size(1)'},'Sections':[{'Id':0,'Name':'BEL','Note':'','Sensor':6,'Laser0':-1,'Laser1':-1,'ExtFunc':0,'User0':0,'User1':0,'Units':0,'Expected':'438.0000','Min':'428.0000','Max':'448.0000'},{'Id':1,'Name':'ÜST BASEN','Note':'','Sensor':6,'Laser0':-1,'Laser1':-1,'ExtFunc':0,'User0':0,'User1':0,'Units':0,'Expected':'498.0000','Min':'488.0000','Max':'508.0000'},{'Id':2,'Name':'DİZ','Note':'32 DEN','Sensor':6,'Laser0':-1,'Laser1':-1,'ExtFunc':0,'User0':0,'User1':0,'Units':0,'Expected':'202.0000','Min':'192.0000','Max':'212.0000'},{'Id':3,'Name':'PACA','Note':'','Sensor':6,'Laser0':-1,'Laser1':-1,'ExtFunc':0,'User0':0,'User1':0,'Units':0,'Expected':'170.0000','Min':'160.0000','Max':'180.0000'},{'Id':4,'Name':'İÇ BOY','Note':'DIŞARIDAN','Sensor':6,'Laser0':-1,'Laser1':-1,'ExtFunc':0,'User0':0,'User1':0,'Units':0,'Expected':'850.0000','Min':'840.0000','Max':'860.0000'},{'Id':5,'Name':'BALDIR','Note':'','Sensor':6,'Laser0':-1,'Laser1':-1,'ExtFunc':0,'User0':0,'User1':0,'Units':0,'Expected':'297.0000','Min':'287.0000','Max':'307.0000'},{'Id':6,'Name':'DIŞ BOY','Note':'DIŞTAM','Sensor':6,'Laser0':-1,'Laser1':-1,'ExtFunc':0,'User0':0,'User1':0,'Units':0,'Expected':'1132.0000','Min':'1122.0000','Max':'1142.0000'}]}}" + vbCrLf

            SendMeasures = Replace(cMsg, "*1*", cSections)

        Catch ex As Exception
            ErrDisp(ex.Message, "SendMeasures",,, ex)
        End Try
    End Function

    Public Sub Login()
        ' execute login procedure
    End Sub

    Public Sub Logout()
        ' execute logout procedure
    End Sub

End Class
