Option Explicit On
Option Strict On

Imports System
Imports System.Configuration
Imports System.IO
Imports System.Windows.Forms
Imports System.Data
Imports Devart.Data.PostgreSql
Imports Microsoft.VisualBasic

Public Class PostgreClass

    Const cConnStr As String = "Server={0};User Id={2};Password={3};Database={1};ApplicationName=;"

    Public oReader As PgSqlDataReader

    Dim cConnectionString As String = ""
    Dim oConnection As PgSqlConnection

    Public Sub New(cServer As String, cDatabase As String, cUsername As String, cPassword As String)
        cConnectionString = String.Format(cConnStr, cServer, cDatabase, cUsername, cPassword)
    End Sub

    Public Function OpenConn() As Boolean

        OpenConn = False

        Try
            oConnection = New PgSqlConnection(cConnectionString)

            oConnection.Open()
            OpenConn = True

        Catch ex As Exception
            ErrDisp(ex.Message, "PostgreClass",,, ex)
        End Try
    End Function

    Public Sub CloseConn()
        Try
            oConnection.Close()
            oConnection.Dispose()

        Catch ex As Exception
            ErrDisp(ex.Message, "PostgreClass",,, ex)
        End Try
    End Sub

    Public Sub GetSQLReader(ByVal cSQL As String)

        Dim oCommand As PgSqlCommand

        Try
            oCommand = New PgSqlCommand(cSQL, oConnection)
            oCommand.CommandTimeout = 0
            oReader = oCommand.ExecuteReader()

        Catch ex As Exception
            ErrDisp(ex.Message, "PostgreClass",,, ex)
        End Try
    End Sub

    Public Function CheckExists(ByVal cSQL As String) As Boolean

        Dim dr As PgSqlDataReader
        Dim oCommand As PgSqlCommand

        CheckExists = False

        Try
            If cSQL.Trim = "" Then Exit Function

            oCommand = New PgSqlCommand(cSQL, oConnection)
            oCommand.CommandTimeout = 0
            dr = oCommand.ExecuteReader()
            If dr.Read Then
                CheckExists = True
            End If
            dr.Close()

        Catch ex As Exception
            ErrDisp(ex.Message, "PostgreClass",,, ex)
        End Try
    End Function

    Public Function SQLSelectOpenCloseConnection(cSQL As String, Optional ByRef cErrorMessage As String = "") As DataTable

        SQLSelectOpenCloseConnection = Nothing

        Try
            OpenConn()
            SQLSelectOpenCloseConnection = SQLSelect(cSQL, cErrorMessage)
            CloseConn()

        Catch ex As Exception
            ErrDisp(ex.Message, "PostgreClass",,, ex)
        End Try
    End Function

    Public Function SQLSelect(cSQL As String, Optional ByRef cErrorMessage As String = "") As DataTable

        SQLSelect = Nothing

        Try
            Dim oCommand As PgSqlCommand
            Dim oDataTable As New DataTable

            If cSQL.Trim = "" Then Exit Function

            Cursor.Current = Cursors.WaitCursor

            oCommand = New PgSqlCommand(cSQL, oConnection)
            oDataTable.Load(oCommand.ExecuteReader())

            SQLSelect = oDataTable

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            cErrorMessage = ex.Message.ToString
            ErrDisp(ex.Message, "PostgreClass",,, ex)
        End Try
    End Function

    Public Function SQLExecuteOpenCloseConnection(cSQL As String, Optional ByRef cErrorMessage As String = "") As Boolean

        SQLExecuteOpenCloseConnection = False

        Try
            OpenConn()
            SQLExecuteOpenCloseConnection = SQLExecute(cSQL, cErrorMessage)
            CloseConn()

        Catch ex As Exception
            ErrDisp(ex.Message, "PostgreClass",,, ex)
        End Try

    End Function

    Public Function SQLExecute(cSQL As String, Optional ByRef cErrorMessage As String = "") As Boolean

        SQLExecute = False

        Try
            Dim oCommand As PgSqlCommand

            If cSQL.Trim = "" Then Exit Function

            Cursor.Current = Cursors.WaitCursor

            oCommand = New PgSqlCommand(cSQL, oConnection)
            oCommand.ExecuteNonQuery()

            Cursor.Current = Cursors.Default

            SQLExecute = True

        Catch ex As Exception
            cErrorMessage = ex.Message.ToString
            ErrDisp(ex.Message, "PostgreClass",,, ex)
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
            ErrDisp("SQLReadString : " + cFieldName,,,, ex)
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
            ErrDisp("SQLReadInteger : " + cFieldName,,,, ex)
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
            ErrDisp("SQLReadDate : " + cFieldName,,,, ex)
        End Try
    End Function

End Class
