Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data.SqlClient

Module UtilSQL

    Public Function OpenConn() As SqlConnection

        OpenConn = Nothing

        Try

            OpenConn = New SqlConnection(oConnection.cConnStr)
            OpenConn.Open()

        Catch ex As Exception
            ErrDisp("OpenConn", "UtilSQL",,, ex)
        End Try
    End Function

    Public Function TestConnection() As Boolean

        Dim oConnectionTest As SqlConnection

        TestConnection = False

        Try

            oConnectionTest = New SqlConnection(oConnection.cConnStr)
            oConnectionTest.Open()
            oConnectionTest.Close()
            TestConnection = True

        Catch ex As Exception
            TestConnection = False
        End Try
    End Function

    Public Sub CloseConn(ByVal oMyConnection As SqlConnection)
        Try

            oMyConnection.Close()
            oMyConnection = Nothing

        Catch ex As Exception
            ErrDisp("CloseConn", "UtilSQL",,, ex)
        End Try
    End Sub

    Public Function GetSQLReader(ByVal cSQL As String, ByVal ConnYage As SqlConnection) As SqlDataReader

        Dim oCommand As SqlCommand

        Try
            oCommand = New SqlCommand
            oCommand.CommandText = cSQL
            oCommand.Connection = ConnYage
            oCommand.CommandTimeout = 0
            GetSQLReader = oCommand.ExecuteReader

        Catch ex As Exception
            GetSQLReader = Nothing
            ErrDisp("GetSQLReader", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function SQLWriteDecimal(ByVal nValue As Object, Optional ByVal lFullClean As Boolean = False) As String
        SQLWriteDecimal = "0"
        Try
            If IsNumeric(nValue) Then
                nValue = CDbl(nValue)
            Else
                nValue = 0
            End If
            SQLWriteDecimal = nValue.ToString
            If LCase(SQLWriteDecimal) = "nan" Then SQLWriteDecimal = "0"
            If SQLWriteDecimal = "Infinity" Then
                SQLWriteDecimal = "0"
            End If
            If lFullClean Then
                SQLWriteDecimal = SQLWriteDecimal.Replace(",", "")
                SQLWriteDecimal = SQLWriteDecimal.Replace(".", "")
            Else
                SQLWriteDecimal = SQLWriteDecimal.Replace(",", ".")
            End If
        Catch ex As Exception
            ErrDisp("SQLWriteDecimal", "UtilSQL",,, ex)
        End Try
    End Function

    Public Function SQLWriteStringDecimal(ByVal cValue As String, Optional ByVal lFullClean As Boolean = False) As String
        SQLWriteStringDecimal = "0"
        Try
            If cValue = "" Then cValue = "0"
            If Not IsNumeric(cValue) Then cValue = "0"
            SQLWriteStringDecimal = cValue
            If lFullClean Then
                SQLWriteStringDecimal = SQLWriteStringDecimal.Replace(",", "")
                SQLWriteStringDecimal = SQLWriteStringDecimal.Replace(".", "")
            Else
                SQLWriteStringDecimal = SQLWriteStringDecimal.Replace(",", "")
            End If
        Catch ex As Exception
            ErrDisp("SQLWriteStringDecimal", "UtilSQL",,, ex)
        End Try
    End Function

    Public Function SQLWriteInteger(ByVal nValue As Integer) As String
        SQLWriteInteger = "0"
        Try
            SQLWriteInteger = nValue.ToString
            SQLWriteInteger = SQLWriteInteger.Replace(",", ".")
        Catch ex As Exception
            ErrDisp("SQLWriteInteger", "UtilSQL",,, ex)
        End Try
    End Function

    Public Function SQLWriteDate(ByVal dValue As Date) As String
        SQLWriteDate = "01.01.1950"
        Try
            If IsDate(dValue) Then
                SQLWriteDate = Mid(dValue.Date.ToString, 1, 10)
            End If
        Catch ex As Exception
            ErrDisp("SQLWriteDate", "UtilSQL",,, ex)
        End Try
    End Function

    Public Function SQLWriteDateTime(ByVal dValue As Date) As String
        SQLWriteDateTime = "01.01.1950"
        Try
            If IsDate(dValue) Then
                SQLWriteDateTime = dValue.ToString("dd.MM.yyyy HH:mm:ss")
            End If
        Catch ex As Exception
            ErrDisp("SQLWriteDate", "UtilSQL",,, ex)
        End Try
    End Function

    Public Function SQLWriteString(ByVal cValue As String, Optional ByVal nLength As Integer = 0) As String
        SQLWriteString = ""
        Try
            If cValue.Trim = "" Then Exit Function

            SQLWriteString = cValue.Replace("'", " ")

            If nLength <> 0 Then
                SQLWriteString = Mid(SQLWriteString, 1, nLength).Trim
            End If
        Catch ex As Exception
            ErrDisp("SQLWriteString : " + cValue,,,, ex)
        End Try
    End Function

    Public Function SQLReadString(ByVal oReader As SqlDataReader, Optional ByVal cFieldName As String = "", Optional ByVal nWidth As Integer = 0) As String
        SQLReadString = ""
        Try
            If cFieldName = "" Then
                If IsDBNull(oReader.GetValue(0)) Then
                    SQLReadString = ""
                ElseIf IsNothing(oReader.GetValue(0)) Then
                    SQLReadString = ""
                Else
                    SQLReadString = oReader.GetString(0).Trim()
                End If
            Else
                If IsDBNull(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadString = ""
                ElseIf IsNothing(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadString = ""
                Else
                    SQLReadString = oReader.GetString(oReader.GetOrdinal(cFieldName)).Trim()
                End If
            End If
            SQLReadString = SQLReadString.Replace("'", "")
            If nWidth > 0 Then
                SQLReadString = Mid(SQLReadString, 1, nWidth).Trim
            End If
            If IsNothing(SQLReadString) Then
                SQLReadString = ""
            End If
            If IsDBNull(SQLReadString) Then
                SQLReadString = ""
            End If

        Catch ex As Exception
            ErrDisp("SQLReadString : " + cFieldName,,,, ex)
        End Try
    End Function

    Public Function SQLReadDouble(ByVal oReader As SqlDataReader, Optional ByVal cFieldName As String = "") As Double
        SQLReadDouble = 0
        Try
            If cFieldName = "" Then
                If IsDBNull(oReader.GetValue(0)) Then
                    SQLReadDouble = 0
                ElseIf IsNothing(oReader.GetValue(0)) Then
                    SQLReadDouble = 0
                Else
                    SQLReadDouble = oReader.GetDecimal(0)
                End If
            Else
                If IsDBNull(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadDouble = 0
                ElseIf IsNothing(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadDouble = 0
                Else
                    SQLReadDouble = oReader.GetDecimal(oReader.GetOrdinal(cFieldName))
                End If
            End If

        Catch ex As Exception
            ErrDisp("SQLReadString : " + cFieldName,,,, ex)
        End Try
    End Function

    Public Function SQLReadInteger(ByVal oReader As SqlDataReader, Optional ByVal cFieldName As String = "") As Integer
        SQLReadInteger = 0
        Try
            If cFieldName = "" Then
                If IsDBNull(oReader.GetValue(0)) Then
                    SQLReadInteger = 0
                ElseIf IsNothing(oReader.GetValue(0)) Then
                    SQLReadInteger = 0
                Else
                    SQLReadInteger = oReader.GetInt32(0)
                End If
            Else
                If IsDBNull(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadInteger = 0
                ElseIf IsNothing(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadInteger = 0
                Else
                    SQLReadInteger = oReader.GetInt32(oReader.GetOrdinal(cFieldName))
                End If
            End If

        Catch ex As Exception
            ErrDisp("SQLReadInteger : " + cFieldName,,,, ex)
        End Try
    End Function

    Public Function SQLReadDate(ByVal oReader As SqlDataReader, Optional ByVal cFieldName As String = "") As Date
        SQLReadDate = #1/1/1950#
        Try
            If cFieldName = "" Then
                If IsDBNull(oReader.GetValue(0)) Then
                    SQLReadDate = #1/1/1950#
                ElseIf IsNothing(oReader.GetValue(0)) Then
                    SQLReadDate = #1/1/1950#
                ElseIf IsDate(oReader.GetDateTime(0)) Then
                    SQLReadDate = oReader.GetDateTime(0)
                End If
            Else
                If IsDBNull(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadDate = #1/1/1950#
                ElseIf IsNothing(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadDate = #1/1/1950#
                ElseIf IsDate(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadDate = oReader.GetDateTime(oReader.GetOrdinal(cFieldName))
                End If
            End If

        Catch ex As Exception
            ErrDisp("SQLReadDate : " + cFieldName,,,, ex)
        End Try
    End Function

    Public Function CheckNullString(ByVal oValue As Object) As String
        CheckNullString = ""
        Try
            If IsDBNull(oValue) Then
                CheckNullString = ""
            ElseIf IsNothing(oValue) Then
                CheckNullString = ""
            Else
                CheckNullString = oValue.ToString.Trim()
            End If
        Catch ex As Exception
            ErrDisp("CheckNullString",,,, ex)
        End Try
    End Function

    Public Function ExecuteSQLCommand(ByVal cSQL As String, Optional ByVal lDateFormat As Boolean = False) As Boolean

        Dim ConnYage As SqlConnection

        ExecuteSQLCommand = False

        Try
            If cSQL.Trim = "" Then Exit Function

            ConnYage = OpenConn()
            ExecuteSQLCommand = ExecuteSQLCommandConnected(cSQL, ConnYage, lDateFormat)
            CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("ExecuteSQLCommand", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function ExecuteSQLCommandConnected(ByVal cSQL As String, ByVal ConnYage As SqlConnection, Optional ByVal lDateFormat As Boolean = False) As Boolean

        Dim oCommand As SqlCommand
        Dim returnValue As Integer = 0

        ExecuteSQLCommandConnected = False

        Try
            If cSQL.Trim = "" Then Exit Function

            If lDateFormat Then cSQL = "Set dateformat 'dmy'  " + cSQL

            oCommand = New SqlCommand
            oCommand.CommandText = cSQL
            oCommand.Connection = ConnYage
            oCommand.CommandTimeout = 0
            returnValue = oCommand.ExecuteNonQuery()
            oCommand = Nothing
            ExecuteSQLCommandConnected = True

        Catch ex As Exception
            ErrDisp("ExecuteSQLCommandConnected", "UtilSQL", cSQL,, ex)
            returnValue = 0
        End Try
    End Function

    Public Function CheckExists(ByVal cSQL As String) As Boolean

        Dim ConnYage As SqlClient.SqlConnection

        CheckExists = False

        Try
            If cSQL.Trim = "" Then Exit Function

            ConnYage = OpenConn()
            CheckExists = CheckExistsConnected(cSQL, ConnYage)
            Call CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("CheckExists", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function CheckExistsConnected(ByVal cSQL As String, ByVal ConnYage As SqlConnection) As Boolean

        Dim dr As SqlDataReader
        Dim cm As SqlCommand

        CheckExistsConnected = False

        Try
            If cSQL.Trim = "" Then Exit Function

            cm = New SqlCommand(cSQL, ConnYage)
            dr = cm.ExecuteReader(CommandBehavior.SingleRow)
            If dr.Read() Then
                CheckExistsConnected = True
            End If
            dr.Close()
            dr = Nothing
            cm = Nothing

        Catch ex As Exception
            ErrDisp("CheckExistsConnected", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function ReadSingleIntegerValue(ByVal cSQL As String) As Integer

        Dim ConnYage As SqlClient.SqlConnection

        ReadSingleIntegerValue = 0

        Try
            If cSQL.Trim = "" Then Exit Function

            ConnYage = OpenConn()
            ReadSingleIntegerValue = ReadSingleIntegerValueConnected(cSQL, ConnYage)
            Call CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("ReadSingleIntegerValue", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function ReadSingleDoubleValue(ByVal cSQL As String, Optional ByVal cFormat As String = "") As Double

        Dim ConnYage As SqlClient.SqlConnection

        ReadSingleDoubleValue = 0

        Try
            If cSQL.Trim = "" Then Exit Function

            ConnYage = OpenConn()
            ReadSingleDoubleValue = ReadSingleDoubleValueConnected(cSQL, ConnYage)
            Call CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("ReadSingleDoubleValue", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function ReadSingleDoubleValueConnected(ByVal cSQL As String, ByVal ConnYage As SqlConnection) As Double

        Dim dr As SqlDataReader
        Dim cm As SqlCommand

        ReadSingleDoubleValueConnected = 0

        Try
            If cSQL.Trim = "" Then Exit Function

            cm = New SqlCommand(cSQL, ConnYage)
            dr = cm.ExecuteReader(CommandBehavior.SingleRow)
            If dr.Read() Then
                If IsDBNull(dr.GetValue(0)) Then
                    ReadSingleDoubleValueConnected = 0
                ElseIf IsNumeric(dr.GetValue(0)) Then
                    ReadSingleDoubleValueConnected = dr.GetDecimal(0)
                Else
                    ReadSingleDoubleValueConnected = 0
                End If
            End If
            dr.Close()
            dr = Nothing
            cm = Nothing

        Catch ex As Exception
            ErrDisp("ReadSingleDoubleValueConnected", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function ReadSingleIntegerValueConnected(ByVal cSQL As String, ByVal ConnYage As SqlConnection) As Integer

        Dim dr As SqlDataReader
        Dim cm As SqlCommand

        ReadSingleIntegerValueConnected = 0

        Try
            If cSQL.Trim = "" Then Exit Function

            cm = New SqlCommand(cSQL, ConnYage)
            dr = cm.ExecuteReader(CommandBehavior.SingleRow)
            If dr.Read() Then
                If IsDBNull(dr.GetValue(0)) Then
                    ReadSingleIntegerValueConnected = 0
                Else
                    ReadSingleIntegerValueConnected = dr.GetInt32(0)
                End If
            End If
            dr.Close()
            dr = Nothing
            cm = Nothing

        Catch ex As Exception
            ErrDisp("ReadSingleIntegerValueConnected", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function ReadSingleValue(ByVal cSQL As String, Optional ByVal cFormat As String = "", Optional ByVal nWidth As Integer = 0) As String

        Dim ConnYage As SqlClient.SqlConnection

        ReadSingleValue = ""

        Try
            If cSQL.Trim = "" Then Exit Function

            ConnYage = OpenConn()
            ReadSingleValue = ReadSingleValueConnected(cSQL, ConnYage, nWidth)
            Call CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("ReadSingleValue", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function ReadSingleValueConnected(ByVal cSQL As String, ByVal ConnYage As SqlConnection, Optional ByVal nWidth As Integer = 0) As String

        Dim dr As SqlDataReader
        Dim cm As SqlCommand

        ReadSingleValueConnected = ""

        If cSQL.Trim = "" Then Exit Function

        Try
            cm = New SqlCommand(cSQL, ConnYage)
            dr = cm.ExecuteReader(CommandBehavior.SingleRow)
            If dr.Read() Then
                If IsDBNull(dr.GetValue(0)) Then
                    ReadSingleValueConnected = ""
                ElseIf IsNothing(dr.GetValue(0)) Then
                    ReadSingleValueConnected = ""
                Else
                    ReadSingleValueConnected = dr.GetString(0).Trim()
                End If
            End If
            dr.Close()
            dr = Nothing
            cm = Nothing
            If nWidth > 0 Then
                ReadSingleValueConnected = Mid(ReadSingleValueConnected, 1, nWidth).Trim
            End If

        Catch ex As Exception
            ErrDisp("ReadSingleValueConnected", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function GetNowFromServer(ByVal ConnYage As SqlConnection) As Date

        Dim oReader As SqlDataReader
        Dim cSQL As String = ""

        GetNowFromServer = CDate("01.01.50 00:00:00")

        Try
            cSQL = "select getdate() "
            oReader = GetSQLReader(cSQL, ConnYage)
            If oReader.Read() Then
                GetNowFromServer = oReader.GetDateTime(0)
            End If
            oReader.Close()
            oReader = Nothing

        Catch ex As Exception
            ErrDisp("GetNowFromServer", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function GetKur(ByVal cDoviz As String, ByVal dTarih As Date, ByVal ConnYage As SqlConnection) As Double

        Dim cSQL As String = ""

        GetKur = 0

        Try
            If cDoviz = "" Then
                GetKur = 0
            ElseIf cDoviz = "TL" Or cDoviz = "YTL" Then
                GetKur = 1
            Else
                cSQL = "set dateformat dmy " +
                         " select top 1 kur = coalesce(kur,0)  " +
                         " from dovkur with (NOLOCK) " +
                         " where doviz = '" + cDoviz + "' " +
                         " and kurcinsi = 'Kur' " +
                         " and tarih = '" + SQLWriteDate(dTarih) + "' "

                GetKur = ReadSingleDoubleValueConnected(cSQL, ConnYage)
            End If

        Catch ex As Exception
            ErrDisp("GetKur : " + cDoviz, "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function CreateTempView(cSQL As String, Optional cViewName As String = "", Optional cViewNameHeader As String = "tmpv_", Optional lDeleteIfViewNameExists As Boolean = True) As String

        Dim cSQL2 As String = ""
        CreateTempView = ""

        Try
            If Trim(cViewName) = "" Then
                CreateTempView = cViewNameHeader + CStr(Int(Rnd() * 10000))
            Else
                CreateTempView = Trim(cViewName)
            End If

            If lDeleteIfViewNameExists Then
                DropView(CreateTempView)
            Else
                Do While True
                    cSQL2 = "select top 1 * " +
                            " from sysobjects with (NOLOCK) " +
                            " where id = object_id('" + Trim(CreateTempView) + "') "
                    If CheckExists(cSQL2) Then
                        CreateTempView = cViewNameHeader + CStr(Int(Rnd() * 10000))
                    Else
                        Exit Do
                    End If
                Loop
            End If

            cSQL = "create view " + CreateTempView + " as " + cSQL
            ExecuteSQLCommand(cSQL)

        Catch ex As Exception
            ErrDisp("CreateTempView", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Sub DropView(cViewName As String)

        Dim cSQL As String = ""

        Try
            If cViewName.Trim = "" Then Exit Sub

            cSQL = "select top 1 * " +
                    " from sysobjects with (NOLOCK) " +
                    " where id = object_id('" + cViewName.Trim + "') "

            If CheckExists(cSQL) Then
                cSQL = "drop view " + cViewName.Trim
                ExecuteSQLCommand(cSQL)
            End If

        Catch ex As Exception
            ErrDisp("DropView", "UtilSQL", cSQL,, ex)
        End Try
    End Sub

    'Public Function CreateTempView(ByVal ConnYage As SqlConnection) As String
    '    CreateTempView = "tmp_" + CStr(Int(Rnd() * 10000))
    '    DropView(CreateTempView, ConnYage)
    'End Function

    'Public Sub DropView(ByVal cViewName As String, ByVal ConnYage As SqlConnection)

    '    Dim cSQL As String

    '    cSQL = "if exists (select * from sysobjects where id = object_id('dbo." + cViewName + "')) " + _
    '            " drop view " + cViewName

    '    ExecuteSQLCommandConnected(cSQL, ConnYage)
    'End Sub

    Public Function GetTempTableName(Optional cNameSeed As String = "") As String

        Dim cSQL As String = ""

        GetTempTableName = ""

        Try
            GetTempTableName = "tmpt_" + cNameSeed + CStr(Int(Rnd() * 10000))

            Do While True
                cSQL = "select top 1 * " +
                        " from sysobjects with (NOLOCK) " +
                        " where id = object_id('" + Trim(GetTempTableName) + "') "

                If CheckExists(cSQL) Then
                    GetTempTableName = "tmpt_" + cNameSeed + CStr(Int(Rnd() * 10000))
                Else
                    Exit Do
                End If
            Loop

        Catch ex As Exception
            ErrDisp("GetTempTableName", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function GetTmpTableName(Optional cNameSeed As String = "", Optional ConnYage As SqlConnection = Nothing) As String

        Dim cSQL As String = ""

        GetTmpTableName = ""

        Try

            GetTmpTableName = "tmpt_" + cNameSeed + CStr(Int(Rnd() * 10000))

            Do While True
                cSQL = "select top 1 * " +
                        " from sysobjects with (NOLOCK) " +
                        " where id = object_id('" + Trim(GetTmpTableName) + "') "

                If CheckExists(cSQL) Then
                    GetTmpTableName = "tmpt_" + cNameSeed + CStr(Int(Rnd() * 10000))
                Else
                    Exit Do
                End If
            Loop

            DropTable(GetTmpTableName, ConnYage)

        Catch ex As Exception
            ErrDisp("GetTmpTableName", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function CreateTMPTable(cSQL As String, Optional cNameSeed As String = "", Optional cTableName As String = "", Optional ConnYage As SqlConnection = Nothing) As String

        CreateTMPTable = ""

        Try
            If cSQL.Trim = "" Then Exit Function

            If cTableName.Trim = "" Then
                cTableName = GetTmpTableName(cNameSeed, ConnYage)
            Else
                cTableName = cTableName.Trim
            End If

            DropTable(cTableName.Trim, ConnYage)

            cSQL = "create table " + cTableName.Trim + " " + cSQL
            ExecuteSQLCommandConnected(cSQL, ConnYage)

            CreateTMPTable = cTableName

        Catch ex As Exception
            ErrDisp("CreateTMPTable", "UtilSQL", cSQL,, ex)
        End Try

    End Function

    Public Function CreateTempTable(cSQL As String, Optional cNameSeed As String = "", Optional cTableName As String = "") As String

        CreateTempTable = ""

        Try
            Dim ConnYage As SqlConnection = Nothing

            ConnYage = OpenConn()

            If cSQL.Trim = "" Then Exit Function

            If cTableName.Trim = "" Then
                cTableName = GetTmpTableName(cNameSeed, ConnYage)
            Else
                cTableName = cTableName.Trim
            End If

            DropTable(cTableName.Trim, ConnYage)

            cSQL = "create table " + cTableName.Trim + " " + cSQL
            ExecuteSQLCommandConnected(cSQL, ConnYage)

            ConnYage.Close()

            CreateTempTable = cTableName

        Catch ex As Exception
            ErrDisp("CreateTempTable", "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Sub DropTable(ByVal cTableName As String, ByVal ConnYage As SqlConnection)

        Dim cSQL As String = ""

        Try
            If IsNothing(cTableName) Then Exit Sub
            If cTableName.Trim = "" Then Exit Sub

            cSQL = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + cTableName + "]') AND type in (N'U')) " +
                    " DROP TABLE [dbo].[" + cTableName + "] "

            ExecuteSQLCommandConnected(cSQL, ConnYage)

        Catch ex As Exception
            ErrDisp("DropTable", "UtilSQL", cSQL,, ex)
        End Try
    End Sub

    Public Function G_CBool(ByVal cParam As String) As Boolean
        If cParam = "1" Or cParam = "2" Then
            G_CBool = True
        Else
            G_CBool = False
        End If
    End Function

    Public Function YNto10(ByVal cEH As String) As Integer
        If IsDBNull(cEH) Then
            YNto10 = 0
        Else
            If cEH = "Y" Then
                YNto10 = 1
            Else
                YNto10 = 0
            End If
        End If
    End Function

    Public Function YNtoTF(ByVal cEH As String) As Boolean
        If cEH = "Y" Then
            YNtoTF = True
        Else
            YNtoTF = False
        End If
    End Function

    Public Function TFtoYN(ByVal lTF As Boolean) As String
        If lTF Then
            TFtoYN = "Y"
        Else
            TFtoYN = "N"
        End If
    End Function

    Public Function OneZeroToYN(ByVal nValue As Integer) As String
        If IsDBNull(nValue) Then
            OneZeroToYN = "N"
        Else
            If nValue = 1 Then
                OneZeroToYN = "Y"
            Else
                OneZeroToYN = "N"
            End If
        End If
    End Function

    Public Function GetFisNo(ByVal cKeyField As String, Optional ByVal cFormat As String = "") As String

        Dim cSQL As String = ""
        Dim nFisNo As Double = 0
        Dim ConnYage As SqlClient.SqlConnection

        GetFisNo = ""

        Try
            ConnYage = OpenConn()

            cSQL = "select top 1 parametervalue " +
                    " from syspar with (NOLOCK) " +
                    " where parametername = '" + cKeyField + "' "

            If CheckExistsConnected(cSQL, ConnYage) Then
                nFisNo = CDbl(ReadSingleValueConnected(cSQL, ConnYage))
                nFisNo = nFisNo + 1
                If cFormat = "" Then
                    GetFisNo = SQLWriteDecimal(nFisNo, True)
                Else
                    GetFisNo = Microsoft.VisualBasic.Format(nFisNo, cFormat)
                End If
                GetFisNo = GetFisNo.Trim

                cSQL = "update syspar " +
                        " set parametervalue = " + nFisNo.ToString +
                        " where parametername = '" + cKeyField + "' "

                Call ExecuteSQLCommandConnected(cSQL, ConnYage)
            Else
                cSQL = "insert into syspar (parametername, parametervalue) " +
                        " values ('" + cKeyField.Trim + "','1') "

                ExecuteSQLCommandConnected(cSQL, ConnYage)

                nFisNo = 1
                If cFormat = "" Then
                    GetFisNo = "1"
                Else
                    GetFisNo = Microsoft.VisualBasic.Format(nFisNo, cFormat)
                End If
                GetFisNo = GetFisNo.Trim
            End If

            Call CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("GetFisNo : " + cKeyField.Trim, "UtilSQL", cSQL,, ex)
        End Try
    End Function

    'Public Sub SQLLoadCombo(ByRef oCombo As ComboBox, ByVal cSQL As String, Optional ByVal lLoadAciklama As Boolean = True)

    '    Dim dr As SqlDataReader
    '    Dim cBuffer As String = ""
    '    Dim ConnYage As SqlClient.SqlConnection

    '    Try
    '        ConnYage = OpenConn()

    '        oCombo.Text = ""
    '        oCombo.Items.Clear()

    '        dr = GetSQLReader(cSQL, ConnYage)
    '        Do While dr.Read()
    '            If Not IsDBNull(dr.GetValue(0)) Then
    '                If Not IsNothing(dr.GetValue(0)) Then
    '                    If dr.GetString(0).Trim() <> "" Then
    '                        If lLoadAciklama Then
    '                            cBuffer = dr.GetString(0).Trim() + " > " + dr.GetString(1).Trim()
    '                        Else
    '                            cBuffer = dr.GetString(0).Trim()
    '                        End If
    '                        oCombo.Items.Add(cBuffer)
    '                    End If
    '                End If
    '            End If
    '        Loop
    '        dr.Close()
    '        dr = Nothing

    '        Call CloseConn(ConnYage)

    '    Catch ex As Exception
    '        ErrDisp("SQLLoadCombo", "UtilSQL",,, ex)
    '    End Try

    'End Sub

    Public Function SQLtoStringArray(ByVal cSQL As String, Optional ByVal cDefault As String = "", Optional cVarType As String = "string") As String()

        Dim dr As SqlDataReader
        Dim ConnYage As SqlClient.SqlConnection
        Dim aResult() As String
        Dim nCnt As Integer = 0

        ReDim aResult(0)
        SQLtoStringArray = aResult

        Try
            ConnYage = OpenConn()

            nCnt = 0
            If cDefault.Trim <> "" Then
                ReDim Preserve aResult(nCnt)
                aResult(nCnt) = cDefault.Trim
                nCnt = nCnt + 1
            End If

            dr = New SqlCommand(cSQL, ConnYage).ExecuteReader

            Do While dr.Read

                If cVarType = "string" Then
                    If SQLReadString(dr) <> "" Then
                        ReDim Preserve aResult(nCnt)
                        aResult(nCnt) = SQLReadString(dr)
                        nCnt = nCnt + 1
                    End If
                ElseIf cVarType = "integer" Then
                    If SQLReadInteger(dr) <> 0 Then
                        ReDim Preserve aResult(nCnt)
                        aResult(nCnt) = SQLReadInteger(dr).ToString
                        nCnt = nCnt + 1
                    End If
                End If

            Loop
            dr.Close()
            Call CloseConn(ConnYage)
            SQLtoStringArray = aResult

        Catch ex As Exception
            ErrDisp("FillArrayOfString", "UtilSQL",,, ex)
        End Try
    End Function

    Public Function GetSysParConnected(ByVal cParameterName As String, ByVal ConnYage As SqlConnection, Optional cDefaultValue As String = "") As String

        Dim cSQL As String = ""

        GetSysParConnected = ""

        Try
            cSQL = "select top 1 parametervalue " +
                    " from syspar with (NOLOCK) " +
                    " where parametername = '" + cParameterName.Trim + "' "

            GetSysParConnected = ReadSingleValueConnected(cSQL, ConnYage)

            If GetSysParConnected.Trim = "" And cDefaultValue.Trim <> "" Then
                GetSysParConnected = cDefaultValue.Trim
            End If

        Catch ex As Exception
            ErrDisp("GetSysParConnected : " + cParameterName.Trim, "UtilSQL", cSQL,, ex)
        End Try
    End Function

    Public Function GetSysPar(ByVal cParameterName As String) As String

        Dim ConnYage As SqlClient.SqlConnection

        ConnYage = OpenConn()
        GetSysPar = GetSysParConnected(cParameterName, ConnYage)
        CloseConn(ConnYage)
    End Function

    Public Sub SetSysParConnected(ByVal cParameterName As String, ByVal cParameterValue As String, ByVal ConnYage As SqlConnection)

        Dim cSQL As String = ""

        Try

            cSQL = "delete syspar where parametername = '" + cParameterName.Trim + "' "
            ExecuteSQLCommandConnected(cSQL, ConnYage)

            cSQL = "insert into syspar (parametername,parametervalue) " +
                                " values ('" + cParameterName.Trim + "', " +
                                        " '" + cParameterValue.Trim + "') "
            ExecuteSQLCommandConnected(cSQL, ConnYage)

        Catch ex As Exception
            ErrDisp("SetSysParConnected : " + cParameterName.Trim, "UtilSQL", cSQL,, ex)
        End Try
    End Sub

    Public Sub SetSysPar(ByVal cParameterName As String, ByVal cParameterValue As String)

        Dim ConnYage As SqlClient.SqlConnection

        ConnYage = OpenConn()
        SetSysParConnected(cParameterName, cParameterValue, ConnYage)
        CloseConn(ConnYage)
    End Sub

    Public Function NormalizeString(ByVal cNotes As String) As String
        NormalizeString = cNotes.Replace("'", "")
    End Function

    Public Function PadSTR(ByVal cValue As String, Optional ByVal nLen As Integer = 0, Optional ByVal lPadLeft As Boolean = False) As String
        PadSTR = ""

        PadSTR = cValue.Trim
        If nLen > 0 Then PadSTR = Mid(PadSTR, 1, nLen).Trim
        If Len(PadSTR) < nLen Then
            If lPadLeft Then
                PadSTR = Space(nLen - Len(PadSTR)) + PadSTR
            Else
                PadSTR = PadSTR + Space(nLen - Len(PadSTR))
            End If
        End If
    End Function

    Public Sub AddToArray(ByRef aValues() As String, ByVal cValue As String)

        Dim nCnt As Integer = 0
        Dim nFound As Integer = -1

        Try
            If aValues(0).Trim = "" Then
                aValues(0) = cValue.Trim
            Else
                For nCnt = 0 To UBound(aValues)
                    If aValues(nCnt).Trim = cValue Then
                        nFound = nCnt
                        Exit For
                    End If
                Next
                If nFound = -1 Then
                    ReDim Preserve aValues(UBound(aValues) + 1)
                    aValues(UBound(aValues)) = cValue
                End If
            End If

        Catch ex As Exception
            ErrDisp("AddToArray : " + cValue.Trim, "UtilSQL",,, ex)
        End Try
    End Sub

    Public Function ConvertImageFiletoBytes(ByVal ImageFilePath As String) As Byte()
        Dim _tempByte() As Byte = Nothing
        If String.IsNullOrEmpty(ImageFilePath) = True Then
            Throw New ArgumentNullException("Image File Name Cannot be Null or Empty", "ImageFilePath")
            Return Nothing
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(ImageFilePath)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(ImageFilePath, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            _fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return _tempByte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConvertBytesToImageFile(ByVal ImageData As Byte(), ByVal FilePath As String) As Boolean
        If IsNothing(ImageData) = True Then
            ConvertBytesToImageFile = False
            Exit Function
            'Throw New ArgumentNullException("Image Binary Data Cannot be Null or Empty", "ImageData")
        End If
        Try
            Dim fs As IO.FileStream = New IO.FileStream(FilePath, IO.FileMode.OpenOrCreate, IO.FileAccess.Write)
            Dim bw As IO.BinaryWriter = New IO.BinaryWriter(fs)
            bw.Write(ImageData)
            bw.Flush()
            bw.Close()
            fs.Close()
            bw = Nothing
            fs.Dispose()
            ConvertBytesToImageFile = True
            Exit Function
        Catch ex As Exception
            ConvertBytesToImageFile = False
        End Try
    End Function

    Public Function GetStreamAsByteArray(ByVal stream As System.IO.Stream) As Byte()

        Dim streamLength As Integer = Convert.ToInt32(stream.Length)

        Dim fileData As Byte() = New Byte(streamLength) {}

        ' Read the file into a byte array
        stream.Read(fileData, 0, streamLength)
        stream.Close()

        Return fileData

    End Function

    Public Function UnicodeBytesToString(ByVal bytes() As Byte) As String
        Return System.Text.ASCIIEncoding.ASCII.GetString(bytes)
        'Return System.Text.Encoding.Unicode.GetString(bytes)
    End Function

    Public Function StringToUnicodeBytes(str As String) As Byte()
        Return System.Text.ASCIIEncoding.ASCII.GetBytes(str)
        'Return System.Text.Encoding.Unicode.GetBytes(str)
    End Function

    Public Function SQLDevXDash(cSQL As String) As String

        SQLDevXDash = ""

        Try
            SQLDevXDash = cSQL.Replace("&", "&amp;")
            SQLDevXDash = SQLDevXDash.Replace("<", "&lt;")
            SQLDevXDash = SQLDevXDash.Replace(">", "&gt;")
        Catch ex As Exception
            ErrDisp("SQLDevXDash", "utilSQL",,, ex)
        End Try
    End Function

End Module
