Option Explicit On
Option Strict On

Imports System
Imports System.Configuration
Imports System.IO
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class SQLServerClass

    Public oReader As SqlDataReader
    Public cSQLQuery As String = ""

    Dim cConnectionString As String = ""
    Dim oConnection As SqlConnection

    Public Sub New(Optional lDefault As Boolean = True, Optional cServer As String = "", Optional cDatabase As String = "", Optional cUsername As String = "", Optional cPassword As String = "")

        If lDefault Then
            cConnectionString = "Data Source=" + UtilRoot.oConnection.cServer + ";" +
                                "Initial Catalog=" + UtilRoot.oConnection.cDatabase + ";" +
                                "uid=" + UtilRoot.oConnection.cUser + ";" +
                                "pwd=" + UtilRoot.oConnection.cPassword + ""
        Else
            cConnectionString = "Data Source=" + cServer + ";" +
                                "Initial Catalog=" + cDatabase + ";" +
                                "uid=" + cUsername + ";" +
                                "pwd=" + cPassword + ""
        End If
    End Sub

    Public Function OpenConn() As Boolean

        OpenConn = False

        Try
            oConnection = New SqlConnection(cConnectionString)

            oConnection.Open()
            OpenConn = True

        Catch ex As Exception
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try
    End Function

    Public Sub CloseConn()
        Try
            oConnection.Close()
            oConnection.Dispose()

        Catch ex As Exception
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try
    End Sub

    Public Sub GetSQLReader(Optional ByVal cSQL As String = "")

        Dim oCommand As SqlCommand

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            oCommand = New SqlCommand(cSQL, oConnection)
            oCommand.CommandTimeout = 0
            oReader = oCommand.ExecuteReader()

        Catch ex As Exception
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try
    End Sub

    Public Function CheckExists(Optional ByVal cSQL As String = "") As Boolean

        Dim dr As SqlDataReader
        Dim oCommand As SqlCommand

        CheckExists = False

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            oCommand = New SqlCommand(cSQL, oConnection)
            oCommand.CommandTimeout = 0
            dr = oCommand.ExecuteReader()
            If dr.Read Then
                CheckExists = True
            End If
            dr.Close()

        Catch ex As Exception
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try
    End Function

    Public Function SQLSelectOpenCloseConnection(Optional ByVal cSQL As String = "", Optional ByRef cErrorMessage As String = "") As DataTable

        SQLSelectOpenCloseConnection = Nothing

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            OpenConn()
            SQLSelectOpenCloseConnection = SQLSelect(cSQL, cErrorMessage)
            CloseConn()

        Catch ex As Exception
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try
    End Function

    Public Function SQLSelect(Optional ByVal cSQL As String = "", Optional ByRef cErrorMessage As String = "") As DataTable

        SQLSelect = Nothing

        Try
            Dim oCommand As SqlCommand
            Dim oDataTable As New DataTable

            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            Cursor.Current = Cursors.WaitCursor

            oCommand = New SqlCommand(cSQL, oConnection)
            oDataTable.Load(oCommand.ExecuteReader())

            SQLSelect = oDataTable

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            cErrorMessage = ex.Message.ToString
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try
    End Function

    Public Function SQLExecuteOpenCloseConnection(Optional ByVal cSQL As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        SQLExecuteOpenCloseConnection = False

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            OpenConn()
            SQLExecuteOpenCloseConnection = SQLExecute(cSQL, cErrorMessage)
            CloseConn()

        Catch ex As Exception
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try

    End Function

    Public Function SQLExecute(Optional ByVal cSQL As String = "", Optional ByRef cErrorMessage As String = "") As Boolean

        SQLExecute = False

        Try
            Dim oCommand As SqlCommand

            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            Cursor.Current = Cursors.WaitCursor

            oCommand = New SqlCommand(cSQL, oConnection)
            oCommand.ExecuteNonQuery()

            Cursor.Current = Cursors.Default

            SQLExecute = True

        Catch ex As Exception
            cErrorMessage = ex.Message.ToString
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try
    End Function

    Public Function SQLReadString(Optional ByVal cFieldName As String = "", Optional ByVal nWidth As Integer = 0) As String
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
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
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
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
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
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
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
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
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
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
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
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
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
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
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
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try
    End Function

    Public Function CreateTempView(Optional ByVal cSQL As String = "", Optional ByVal lNoConnection As Boolean = False, Optional cViewName As String = "", Optional cViewNameHeader As String = "tmpv_service", Optional lDeleteIfViewNameExists As Boolean = True) As String

        Dim cSQL2 As String = ""
        CreateTempView = ""

        Try
            If cSQL.Trim = "" Then
                cSQL = cSQLQuery.Trim
            End If

            If cSQL.Trim = "" Then Exit Function

            If lNoConnection Then OpenConn()

            If Trim(cViewName) = "" Then
                CreateTempView = cViewNameHeader + CStr(Int(Rnd() * 10000))
            Else
                CreateTempView = Trim(cViewName)
            End If

            If lDeleteIfViewNameExists Then
                DropView(CreateTempView)
            Else
                Do While True
                    cSQL2 = "select * " +
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
            SQLExecute(cSQL)

            If lNoConnection Then CloseConn()

        Catch ex As Exception
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
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
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try
    End Sub

    Public Function GetTempTableName(Optional cNameSeed As String = "service", Optional ByVal lNoConnection As Boolean = False) As String

        Dim cSQL As String = ""

        GetTempTableName = ""

        Try

            If lNoConnection Then OpenConn()

            GetTempTableName = "tmpt_" + cNameSeed + CStr(Int(Rnd() * 10000))

            Do While True
                cSQL = "select * " +
                    " from sysobjects with (NOLOCK) " +
                    " where id = object_id('" + Trim(GetTempTableName) + "') "

                If CheckExists(cSQL) Then
                    GetTempTableName = "tmpt_" + cNameSeed + CStr(Int(Rnd() * 10000))
                Else
                    Exit Do
                End If
            Loop

            DropTable(GetTempTableName)

            If lNoConnection Then CloseConn()

        Catch ex As Exception
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try
    End Function

    Public Function CreateTempTable(Optional ByVal cSQL As String = "", Optional ByVal lNoConnection As Boolean = False, Optional cNameSeed As String = "service", Optional cTableName As String = "") As String

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
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
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
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try
    End Sub

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
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try
    End Function

    Public Sub ExecuteStoredProcedure(ByVal cProcedureName As String, Optional ByVal cParameter1 As String = "")

        Try
            Dim oCommand As SqlCommand
            Dim oParameter1 As SqlParameter
            Dim cSQL As String = ""
            Dim nRows As Double = 0

            If cProcedureName.Trim = "" Then
                Exit Sub
            End If

            cSQL = "SELECT top 1 object_id " +
                    " From sys.objects with (NOLOCK) " +
                    " WHERE object_id = OBJECT_ID('" + cProcedureName + "') " +
                    " AND type in ('P', 'PC') "

            If Not CheckExists(cSQL) Then
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor

            oCommand = New SqlCommand(cProcedureName, oConnection)
            oCommand.CommandType = CommandType.StoredProcedure

            If cParameter1.Trim <> "" Then
                oParameter1 = New SqlParameter()
                oParameter1.ParameterName = "@cParameter1"
                oParameter1.Value = cParameter1
                oParameter1.SqlDbType = SqlDbType.Char
                oParameter1.Direction = ParameterDirection.Input
                oCommand.Parameters.Add(oParameter1)
            End If

            nRows = CDbl(oCommand.ExecuteNonQuery())

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            ErrDisp(ex.Message, "SqlServerClass",,, ex)
        End Try
    End Sub


    Public Function GetSysPar(ByVal cParameterName As String, Optional cDefaultValue As String = "") As String

        Dim cSQL As String = ""

        GetSysPar = ""

        Try
            Cursor.Current = Cursors.WaitCursor

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

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            ErrDisp("GetSysPar : " + cParameterName.Trim, "SQLServerClass", cSQL,, ex)
        End Try
    End Function

    Public Sub SetSysPar(ByVal cParameterName As String, ByVal cParameterValue As String)

        Dim cSQL As String = ""
        Dim oCommand As SqlCommand

        Try
            Cursor.Current = Cursors.WaitCursor

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

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            ErrDisp("SetSysParConnected : " + cParameterName.Trim, "SQLServerClass", cSQL,, ex)
        End Try
    End Sub

End Class
