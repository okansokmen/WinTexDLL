Option Explicit On
Option Strict On

Imports System
Imports System.Configuration
Imports System.IO
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class SQLServerClass

    Public oReader As SqlDataReader
    Public cSQLQuery As String = ""
    Public cConnectionString As String = ""

    Dim oConnection As SqlConnection

    Public Sub New(Optional lDefault As Boolean = True, Optional cServer As String = "", Optional cDatabase As String = "", Optional cUsername As String = "", Optional cPassword As String = "", Optional cConStr As String = "")

        If lDefault Then
            Select Case cDatabase
                Case ConfigurationManager.AppSettings("DATABASE1")
                    cConnectionString = "Data Source=" + ConfigurationManager.AppSettings("SERVER1") + ";" +
                                        "Initial Catalog=" + ConfigurationManager.AppSettings("DATABASE1") + ";" +
                                        "uid=" + ConfigurationManager.AppSettings("USERNAME1") + ";" +
                                        "pwd=" + ConfigurationManager.AppSettings("PASSWORD1") + ";" +
                                        "Connection Timeout = 6000"
                Case ConfigurationManager.AppSettings("DATABASE2")
                    cConnectionString = "Data Source=" + ConfigurationManager.AppSettings("SERVER2") + ";" +
                                        "Initial Catalog=" + ConfigurationManager.AppSettings("DATABASE2") + ";" +
                                        "uid=" + ConfigurationManager.AppSettings("USERNAME2") + ";" +
                                        "pwd=" + ConfigurationManager.AppSettings("PASSWORD2") + ";" +
                                        "Connection Timeout = 6000"
                Case ConfigurationManager.AppSettings("DATABASE3")
                    cConnectionString = "Data Source=" + ConfigurationManager.AppSettings("SERVER3") + ";" +
                                        "Initial Catalog=" + ConfigurationManager.AppSettings("DATABASE3") + ";" +
                                        "uid=" + ConfigurationManager.AppSettings("USERNAME3") + ";" +
                                        "pwd=" + ConfigurationManager.AppSettings("PASSWORD3") + ";" +
                                        "Connection Timeout = 6000"
                Case ConfigurationManager.AppSettings("DATABASE4")
                    cConnectionString = "Data Source=" + ConfigurationManager.AppSettings("SERVER4") + ";" +
                                        "Initial Catalog=" + ConfigurationManager.AppSettings("DATABASE4") + ";" +
                                        "uid=" + ConfigurationManager.AppSettings("USERNAME4") + ";" +
                                        "pwd=" + ConfigurationManager.AppSettings("PASSWORD4") + ";" +
                                        "Connection Timeout = 6000"
                Case ConfigurationManager.AppSettings("DATABASE5")
                    cConnectionString = "Data Source=" + ConfigurationManager.AppSettings("SERVER5") + ";" +
                                        "Initial Catalog=" + ConfigurationManager.AppSettings("DATABASE5") + ";" +
                                        "uid=" + ConfigurationManager.AppSettings("USERNAME5") + ";" +
                                        "pwd=" + ConfigurationManager.AppSettings("PASSWORD5") + ";" +
                                        "Connection Timeout = 6000"
                Case ConfigurationManager.AppSettings("DATABASE6")
                    cConnectionString = "Data Source=" + ConfigurationManager.AppSettings("SERVER6") + ";" +
                                        "Initial Catalog=" + ConfigurationManager.AppSettings("DATABASE6") + ";" +
                                        "uid=" + ConfigurationManager.AppSettings("USERNAME6") + ";" +
                                        "pwd=" + ConfigurationManager.AppSettings("PASSWORD6") + ";" +
                                        "Connection Timeout = 6000"
                Case ConfigurationManager.AppSettings("DATABASE7")
                    cConnectionString = "Data Source=" + ConfigurationManager.AppSettings("SERVER7") + ";" +
                                        "Initial Catalog=" + ConfigurationManager.AppSettings("DATABASE7") + ";" +
                                        "uid=" + ConfigurationManager.AppSettings("USERNAME7") + ";" +
                                        "pwd=" + ConfigurationManager.AppSettings("PASSWORD7") + ";" +
                                        "Connection Timeout = 6000"
                Case ConfigurationManager.AppSettings("DATABASE8")
                    cConnectionString = "Data Source=" + ConfigurationManager.AppSettings("SERVER8") + ";" +
                                        "Initial Catalog=" + ConfigurationManager.AppSettings("DATABASE8") + ";" +
                                        "uid=" + ConfigurationManager.AppSettings("USERNAME8") + ";" +
                                        "pwd=" + ConfigurationManager.AppSettings("PASSWORD8") + ";" +
                                        "Connection Timeout = 6000"
                Case ConfigurationManager.AppSettings("DATABASE9")
                    cConnectionString = "Data Source=" + ConfigurationManager.AppSettings("SERVER9") + ";" +
                                        "Initial Catalog=" + ConfigurationManager.AppSettings("DATABASE9") + ";" +
                                        "uid=" + ConfigurationManager.AppSettings("USERNAME9") + ";" +
                                        "pwd=" + ConfigurationManager.AppSettings("PASSWORD9") + ";" +
                                        "Connection Timeout = 6000"
                Case ConfigurationManager.AppSettings("DATABASE10")
                    cConnectionString = "Data Source=" + ConfigurationManager.AppSettings("SERVER10") + ";" +
                                        "Initial Catalog=" + ConfigurationManager.AppSettings("DATABASE10") + ";" +
                                        "uid=" + ConfigurationManager.AppSettings("USERNAME10") + ";" +
                                        "pwd=" + ConfigurationManager.AppSettings("PASSWORD10") + ";" +
                                        "Connection Timeout = 6000"
                Case Else
                    cConnectionString = "Data Source=" + ConfigurationManager.AppSettings("SERVER") + ";" +
                                        "Initial Catalog=" + ConfigurationManager.AppSettings("DATABASE") + ";" +
                                        "uid=" + ConfigurationManager.AppSettings("USERNAME") + ";" +
                                        "pwd=" + ConfigurationManager.AppSettings("PASSWORD") + ";" +
                                        "Connection Timeout = 6000"
            End Select
        Else
            If cConStr.Trim = "" Then
                cConnectionString = "Data Source=" + cServer + ";" +
                                    "Initial Catalog=" + cDatabase + ";" +
                                    "uid=" + cUsername + ";" +
                                    "pwd=" + cPassword + ";" +
                                    "Connection Timeout = 6000"
            Else
                cConnectionString = cConStr.Trim
            End If
        End If
    End Sub

    Public Function OpenConn() As Boolean

        OpenConn = False

        Try
            oConnection = New SqlConnection(cConnectionString)

            oConnection.Open()

            OpenConn = True

        Catch ex As Exception
            ErrDisp(ex, "OpenConn : " + cConnectionString)
        End Try
    End Function

    Public Sub CloseConn()
        Try
            oConnection.Close()
            oConnection.Dispose()

        Catch ex As Exception
            ErrDisp(ex, "CloseConn : " + cConnectionString)
        End Try
    End Sub

    Public Sub GetSQLReader(Optional ByVal cSQL As String = "")

        Dim oCommand As SqlCommand

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Sub

            oCommand = New SqlCommand(cSQL, oConnection)
            oCommand.CommandTimeout = 0
            oReader = oCommand.ExecuteReader()

        Catch ex As Exception
            ErrDisp(ex, "GetSQLReader", cSQL)
        End Try
    End Sub

    Public Function CheckExists(Optional ByVal cSQL As String = "") As Boolean

        Dim oCommand As SqlCommand

        CheckExists = False

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            oCommand = New SqlCommand(cSQL, oConnection)
            oCommand.CommandTimeout = 0
            oReader = oCommand.ExecuteReader()
            If oReader.Read Then
                CheckExists = True
            End If
            oReader.Close()

        Catch ex As Exception
            ErrDisp(ex, "CheckExists", cSQL)
        End Try
    End Function

    Public Function SQLSelectOpenCloseConnection(Optional cSQL As String = "", Optional ByRef cErrorMessage As String = "") As DataTable

        SQLSelectOpenCloseConnection = Nothing

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            OpenConn()
            SQLSelectOpenCloseConnection = SQLSelect(cSQL, cErrorMessage)
            CloseConn()

        Catch ex As Exception
            ErrDisp(ex, "SQLSelectOpenCloseConnection", cSQL)
        End Try
    End Function

    Public Function SQLSelect(Optional cSQL As String = "", Optional ByRef cErrorMessage As String = "") As DataTable

        SQLSelect = Nothing

        Try
            Dim oCommand As SqlCommand
            Dim oDataTable As New DataTable

            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            oCommand = New SqlCommand(cSQL, oConnection)
            oDataTable.Load(oCommand.ExecuteReader())

            SQLSelect = oDataTable

        Catch ex As Exception
            cErrorMessage = ex.Message.ToString
            ErrDisp(ex, "SQLSelect", cSQL)
        End Try
    End Function

    Public Function GetSingleStringResult(Optional cSQL As String = "") As String

        Dim cSQLCount As String = ""
        Dim nRowCount As Integer = 0

        GetSingleStringResult = ""

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            OpenConn()

            cSQLCount = "select QueryRowCount = count(*) from (" + cSQL + ") TableToCount "
            nRowCount = DBReadInteger(cSQLCount)

            If nRowCount = 1 Then
                GetSingleStringResult = DBReadString(cSQL)
            End If

            CloseConn()

        Catch ex As Exception
            ErrDisp(ex, "GetSingleStringResult")
        End Try
    End Function

    Public Function SQLSelectToList(Optional cSQL As String = "") As List(Of String)

        SQLSelectToList = Nothing

        Dim oList As New List(Of String)

        Try
            oList.Clear()

            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            GetSQLReader(cSQL)

            Do While oReader.Read
                oList.Add(SQLReadString())
            Loop
            oReader.Close()

            SQLSelectToList = oList

        Catch ex As Exception
            ErrDisp(ex, "SQLSelectToList")
        End Try
    End Function

    Public Function SQLExecuteOpenCloseConnection(Optional cSQL As String = "", Optional ByRef cErrorMessage As String = "", Optional ByVal nTimeOut As Integer = -1) As Boolean

        SQLExecuteOpenCloseConnection = False

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            OpenConn()
            SQLExecuteOpenCloseConnection = SQLExecute(cSQL, cErrorMessage, nTimeOut)
            CloseConn()

        Catch ex As Exception
            ErrDisp(ex, "SQLExecuteOpenCloseConnection", cSQL)
        End Try
    End Function

    Public Function SQLExecute(Optional cSQL As String = "", Optional ByRef cErrorMessage As String = "", Optional ByVal nTimeOut As Integer = -1) As Boolean

        SQLExecute = False

        Try
            Dim oCommand As SqlCommand

            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            oCommand = New SqlCommand(cSQL, oConnection)
            If nTimeOut <> -1 Then
                oCommand.CommandTimeout = nTimeOut
            End If
            oCommand.ExecuteNonQuery()

            SQLExecute = True

        Catch ex As Exception
            cErrorMessage = ex.Message.ToString
            ErrDisp(ex, "SQLExecute", cSQL)
        End Try
    End Function

    Public Function SQLReadString(Optional ByVal cFieldName As String = "", Optional ByVal nWidth As Integer = 0, Optional lProcessOutput As Boolean = True) As String
        SQLReadString = ""
        Try
            If cFieldName = "" Then
                If IsDBNull(oReader.GetValue(0)) Then
                    SQLReadString = ""
                ElseIf IsNothing(oReader.GetValue(0)) Then
                    SQLReadString = ""
                Else
                    If lProcessOutput Then
                        SQLReadString = oReader.GetString(0).Trim()
                    Else
                        SQLReadString = oReader.GetString(0)
                        Exit Function
                    End If
                End If
            Else
                If IsDBNull(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadString = ""
                ElseIf IsNothing(oReader.GetValue(oReader.GetOrdinal(cFieldName))) Then
                    SQLReadString = ""
                Else
                    If lProcessOutput Then
                        SQLReadString = oReader.GetString(oReader.GetOrdinal(cFieldName)).Trim()
                    Else
                        SQLReadString = oReader.GetString(oReader.GetOrdinal(cFieldName))
                        Exit Function
                    End If
                End If
            End If
            SQLReadString = Replace(SQLReadString, "'", "")
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
            ErrDisp(ex, "SQLReadString")
        End Try
    End Function

    Public Function SQLReadDouble(Optional ByVal cFieldName As String = "") As Double
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
            ErrDisp(ex, "SQLReadDouble")
        End Try
    End Function

    Public Function SQLReadInteger(Optional ByVal cFieldName As String = "") As Integer
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
            ErrDisp(ex, "SQLReadInteger")
        End Try
    End Function

    Public Function SQLReadDate(Optional ByVal cFieldName As String = "") As Date
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
            ErrDisp(ex, "SQLReadDate")
        End Try
    End Function

    Public Function DBReadDouble(Optional ByVal cSQL As String = "") As Double

        DBReadDouble = 0

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            GetSQLReader(cSQL)
            If oReader.Read() Then
                DBReadDouble = SQLReadDouble()
            End If
            oReader.Close()

        Catch ex As Exception
            ErrDisp(ex, "DBReadDouble", cSQL)
        End Try
    End Function

    Public Function DBReadInteger(Optional ByVal cSQL As String = "") As Integer

        DBReadInteger = 0

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            GetSQLReader(cSQL)
            If oReader.Read() Then
                DBReadInteger = SQLReadInteger()
            End If
            oReader.Close()

        Catch ex As Exception
            ErrDisp(ex, "DBReadInteger", cSQL)
        End Try
    End Function

    Public Function DBReadString(Optional ByVal cSQL As String = "", Optional ByVal nWidth As Integer = 0) As String

        DBReadString = ""

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            GetSQLReader(cSQL)
            If oReader.Read() Then
                DBReadString = SQLReadString("", nWidth)
            End If
            oReader.Close()

        Catch ex As Exception
            ErrDisp(ex, "DBReadString", cSQL)
        End Try
    End Function

    Public Function DBReadDate(Optional ByVal cSQL As String = "") As Date

        DBReadDate = #1/1/1950#

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            GetSQLReader(cSQL)
            If oReader.Read() Then
                DBReadDate = SQLReadDate()
            End If
            oReader.Close()

        Catch ex As Exception
            ErrDisp(ex, "DBReadDate", cSQL)
        End Try
    End Function

    Public Function CreateTempView(Optional cSQL As String = "", Optional ByVal lNoConnection As Boolean = False, Optional cViewName As String = "", Optional cViewNameHeader As String = "tmpv_web") As String

        Dim cSQL2 As String = ""

        CreateTempView = ""

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            If lNoConnection Then OpenConn()

            If Trim(cViewName) = "" Then
                Randomize()
                CreateTempView = cViewNameHeader + Format(Rnd() * 10000000000, "0000000000")
            Else
                CreateTempView = Trim(cViewName)
            End If

            Do While True

                cSQL2 = "select * " +
                        " from sysobjects with (NOLOCK) " +
                        " where id = object_id('" + Trim(CreateTempView) + "') "

                If CheckExists(cSQL2) Then
                    Randomize()
                    CreateTempView = cViewNameHeader + Format(Rnd() * 10000000000, "0000000000")
                Else
                    Exit Do
                End If
            Loop

            DropView(CreateTempView)

            cSQL = "create view " + CreateTempView + " as " + cSQL
            SQLExecute(cSQL)

            If lNoConnection Then CloseConn()

        Catch ex As Exception
            ErrDisp(ex, "CreateTempView", cSQL)
        End Try
    End Function

    Public Sub DropView(cViewName As String, Optional ByVal lNoConnection As Boolean = False)

        Dim cSQL As String = ""

        Try
            If cViewName.Trim = "" Then Exit Sub

            If lNoConnection Then OpenConn()

            cSQL = "select * " +
                    " from sysobjects with (NOLOCK) " +
                    " where id = object_id('" + cViewName.Trim + "') "

            If CheckExists(cSQL) Then
                cSQL = "drop view " + cViewName.Trim
                SQLExecute(cSQL)
            End If

            If lNoConnection Then CloseConn()

        Catch ex As Exception
            ErrDisp(ex, "DropView", cSQL)
        End Try
    End Sub

    Public Function GetTempTableName(Optional cNameSeed As String = "web", Optional ByVal lNoConnection As Boolean = False) As String

        Dim cSQL As String = ""

        GetTempTableName = ""

        Try
            If lNoConnection Then OpenConn()

            Randomize()
            GetTempTableName = "tmpt_" + cNameSeed + Format(Rnd() * 10000000000, "0000000000")

            Do While True
                cSQL = "select * " +
                    " from sysobjects with (NOLOCK) " +
                    " where id = object_id('" + Trim(GetTempTableName) + "') "

                If CheckExists(cSQL) Then
                    Randomize()
                    GetTempTableName = "tmpt_" + cNameSeed + Format(Rnd() * 10000000000, "0000000000")
                Else
                    Exit Do
                End If
            Loop

            DropTable(GetTempTableName)

            If lNoConnection Then CloseConn()

        Catch ex As Exception
            ErrDisp(ex, "GetTempTableName", cSQL)
        End Try
    End Function

    Public Function CreateTempTable(Optional cSQL As String = "", Optional ByVal lNoConnection As Boolean = False, Optional cNameSeed As String = "service", Optional cTableName As String = "") As String

        CreateTempTable = ""

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            If lNoConnection Then OpenConn()

            If cTableName.Trim = "" Then
                cTableName = GetTempTableName(cNameSeed)
            Else
                cTableName = cTableName.Trim
            End If

            DropTable(cTableName.Trim)

            cSQL = "create table " + cTableName.Trim + " " + cSQL
            SQLExecute(cSQL)

            If lNoConnection Then CloseConn()

            CreateTempTable = cTableName

        Catch ex As Exception
            ErrDisp(ex, "CreateTempTable", cSQL)
        End Try
    End Function

    Public Sub DropTable(ByVal cTableName As String, Optional ByVal lNoConnection As Boolean = False)

        Dim cSQL As String = ""

        Try
            If IsNothing(cTableName) Then Exit Sub
            If cTableName.Trim = "" Then Exit Sub

            If lNoConnection Then OpenConn()

            cSQL = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + cTableName + "]') AND type in (N'U')) " +
                    " DROP TABLE [dbo].[" + cTableName + "] "

            SQLExecute(cSQL)

            If lNoConnection Then CloseConn()

        Catch ex As Exception
            ErrDisp(ex, "DropTable", cSQL)
        End Try
    End Sub

    Public Function BuildFilterString(Optional ByVal cSQL As String = "") As String

        BuildFilterString = ""

        Try
            Dim cResult As String = ""
            Dim cBuffer As String = ""

            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            GetSQLReader(cSQL)

            Do While oReader.Read

                cBuffer = SQLReadString()

                If cBuffer.Trim <> "" Then
                    If cResult.Trim = "" Then
                        cResult = "'" + cBuffer + "'"
                    Else
                        If InStr(cResult, cBuffer) = 0 Then
                            cResult = cResult + ",'" + cBuffer + "'"
                        End If
                    End If
                End If
            Loop
            oReader.Close()

            BuildFilterString = cResult.Trim

        Catch ex As Exception
            ErrDisp(ex, "BuildFilterString", cSQL)
        End Try
    End Function

    Public Function SQLtoStringArray(Optional ByVal cSQL As String = "", Optional ByVal cDefault As String = "", Optional cVarType As String = "string", Optional ByVal lNoConnection As Boolean = False) As String()

        Dim aResult() As String
        Dim nCnt As Integer = 0

        ReDim aResult(0)
        SQLtoStringArray = aResult

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            If lNoConnection Then OpenConn()

            nCnt = 0
            If cDefault.Trim <> "" Then
                ReDim Preserve aResult(nCnt)
                aResult(nCnt) = cDefault.Trim
                nCnt = nCnt + 1
            End If

            GetSQLReader(cSQL)

            Do While oReader.Read

                If cVarType = "string" Then
                    If SQLReadString() <> "" Then
                        ReDim Preserve aResult(nCnt)
                        aResult(nCnt) = SQLReadString()
                        nCnt = nCnt + 1
                    End If
                ElseIf cVarType = "integer" Then
                    If SQLReadInteger() <> 0 Then
                        ReDim Preserve aResult(nCnt)
                        aResult(nCnt) = SQLReadInteger().ToString
                        nCnt = nCnt + 1
                    End If
                End If

            Loop
            oReader.Close()

            SQLtoStringArray = aResult

            If lNoConnection Then CloseConn()

        Catch ex As Exception
            ErrDisp(ex, "SQLtoStringArray", cSQL)
        End Try
    End Function

    Public Function GetSysPar(ByVal cParameterName As String, Optional cDefaultValue As String = "") As String

        Dim cSQL As String = ""

        GetSysPar = ""

        Try
            cSQL = "select top 1 parametervalue " +
                    " from syspar with (NOLOCK) " +
                    " where parametername = '" + cParameterName.Trim + "' "

            GetSQLReader(cSQL)

            If oReader.Read() Then
                GetSysPar = SQLReadString("parametervalue", 200)
            End If
            oReader.Close()

            If GetSysPar.Trim = "" And cDefaultValue.Trim <> "" Then
                GetSysPar = cDefaultValue.Trim
            End If

        Catch ex As Exception
            ErrDisp(ex, "GetSysPar", cSQL)
        End Try
    End Function

    Public Sub SetSysPar(ByVal cParameterName As String, ByVal cParameterValue As String)

        Dim cSQL As String = ""
        Dim oCommand As SqlCommand

        Try
            cSQL = "delete syspar where parametername = '" + cParameterName.Trim + "' "

            oCommand = New SqlCommand(cSQL, oConnection)
            oCommand.ExecuteNonQuery()
            oCommand = Nothing

            cSQL = "insert into syspar (parametername,parametervalue) " +
                                " values ('" + cParameterName.Trim + "', " +
                                        " '" + cParameterValue.Trim + "') "

            oCommand = New SqlCommand(cSQL, oConnection)
            oCommand.ExecuteNonQuery()
            oCommand = Nothing

        Catch ex As Exception
            ErrDisp(ex, "SetSysPar", cSQL)
        End Try
    End Sub

    Public Function GetSequenceFisNo(cFieldName As String) As String

        Dim cSQL As String = ""
        Dim nFisNo As Double = 0

        GetSequenceFisNo = ""

        If cFieldName.Trim = "" Then Exit Function

        Try
            cSQL = "Select top 1 name " +
                    " from sys.sequences " +
                    " where name = '" + cFieldName.Trim + "' "

            If CheckExists(cSQL) Then
                cSQL = "select fisno = convert(decimal(18,0), next value for " + cFieldName.Trim + ")"
                nFisNo = DBReadDouble(cSQL)
            Else
                cSQL = "create sequence " + cFieldName.Trim + " start with 1 increment by 1 "
                SQLExecute(cSQL)
                nFisNo = 1
            End If

            GetSequenceFisNo = Strings.Format(nFisNo, "0000000000")

        Catch ex As Exception
            ErrDisp(ex, "GetSequenceFisNo")
        End Try
    End Function

    Public Function CLRExecute(ByVal cProcName As String, Optional ByVal cFilter As String = "", Optional cFilter2 As String = "", Optional cFilter3 As String = "", Optional cFilter4 As String = "") As Boolean

        CLRExecute = False

        Try
            Dim CmdAdo As SqlCommand
            Dim oParam As SqlParameter
            Dim nPCnt As Long = 0
            Dim cBasla As String = ""

            cFilter = cFilter.Replace("'", "||").Trim
            cFilter2 = cFilter2.Replace("'", "||").Trim
            cFilter3 = cFilter3.Replace("'", "||").Trim
            cFilter4 = cFilter4.Replace("'", "||").Trim

            CmdAdo = New SqlCommand With {
                .Connection = oConnection,
                .CommandTimeout = 0,
                .CommandType = CommandType.StoredProcedure
            }
            CmdAdo.Parameters.Clear()

            Select Case cProcName
                Case "FastUTFBuildAll", "FastMTFBuildAll", "HizliStokBakimi", "stokbakim"
                ' no parameters

                Case "FastSTFBuildAll"
                    oParam = New SqlParameter With {
                        .ParameterName = "cFilter",
                        .SqlDbType = SqlDbType.Char,
                        .Direction = ParameterDirection.Input,
                        .Size = Len(cFilter) + 1,
                        .Value = cFilter
                    }

                    CmdAdo.Parameters.Add(oParam)

                    oParam = New SqlParameter With {
                        .ParameterName = "nSonuc",
                        .SqlDbType = SqlDbType.Int,
                        .Direction = ParameterDirection.Output,
                        .Size = 10,
                        .Value = 0
                    }

                    CmdAdo.Parameters.Add(oParam)

            End Select

            CmdAdo.CommandText = cProcName
            CmdAdo.CommandTimeout = 0
            CmdAdo.ExecuteNonQuery()

            Select Case cProcName
                Case "FastSTFBuildAll"
                    If CInt(CmdAdo.Parameters.Item(1).Value) = 1 Then
                        CLRExecute = True
                    End If
                Case Else
                    CLRExecute = True
            End Select

        Catch ex As Exception
            ErrDisp(ex, "CLRExecute")
        End Try
    End Function

End Class
