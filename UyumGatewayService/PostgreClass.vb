Imports System
Imports System.Configuration
Imports System.IO
Imports System.Data
Imports Npgsql
Imports System.Threading.Tasks.TaskExtensions
Imports Microsoft.VisualBasic

Public Class PostgreClass

    Const cConnStr As String = "Server={0};User Id={2};Password={3};Database={1};ApplicationName=;"

    Dim cConnectionString As String = ""
    Dim oConnection As NpgsqlConnection

    Public Sub New(cServer As String, cDatabase As String, cUsername As String, cPassword As String)
        cConnectionString = String.Format(cConnStr, cServer, cDatabase, cUsername, cPassword)
    End Sub

    Public Function OpenConn() As Boolean

        OpenConn = False

        Try
            oConnection = New NpgsqlConnection(cConnectionString)
            oConnection.Open()
            OpenConn = True

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Function

    Public Sub CloseConn()
        Try
            oConnection.Close()
            oConnection.Dispose()

        Catch ex As Exception
            ErrDisp(ex)
        End Try
    End Sub

    Public Function GetSQLReader(ByVal cSQL As String) As NpgsqlDataReader

        Dim oCommand As NpgsqlCommand

        Try
            oCommand = New NpgsqlCommand(cSQL, oConnection)
            oCommand.CommandTimeout = 0
            GetSQLReader = oCommand.ExecuteReader()

        Catch ex As Exception
            GetSQLReader = Nothing
            ErrDisp(ex,, cSQL)
        End Try
    End Function

    Public Function CheckExists(ByVal cSQL As String) As Boolean

        Dim dr As NpgsqlDataReader

        CheckExists = False

        Try
            If cSQL.Trim = "" Then Exit Function

            dr = GetSQLReader(cSQL)
            If dr.Read Then
                CheckExists = True
            End If
            dr.Close()

        Catch ex As Exception
            ErrDisp(ex,, cSQL)
        End Try
    End Function

    Public Function PGSelectOpenCloseConnection(cSQL As String, Optional ByRef cErrorMessage As String = "") As DataTable

        PGSelectOpenCloseConnection = Nothing

        Try
            OpenConn()
            PGSelectOpenCloseConnection = PGSelect(cSQL, cErrorMessage)
            CloseConn()

        Catch ex As Exception
            ErrDisp(ex,, cSQL)
        End Try
    End Function

    Public Function PGSelect(cSQL As String, Optional ByRef cErrorMessage As String = "") As DataTable

        PGSelect = Nothing

        Try
            Dim oCommand As NpgsqlCommand
            Dim oDataTable As New DataTable

            If cSQL.Trim = "" Then Exit Function

            oCommand = New NpgsqlCommand(cSQL, oConnection)
            oDataTable.Load(oCommand.ExecuteReader())

            PGSelect = oDataTable

        Catch ex As Exception
            cErrorMessage = ex.Message.ToString
            ErrDisp(ex,, cSQL)
        End Try
    End Function

    Public Function PGExecuteOpenCloseConnection(cSQL As String, Optional ByRef cErrorMessage As String = "") As Boolean

        PGExecuteOpenCloseConnection = False

        Try
            OpenConn()
            PGExecuteOpenCloseConnection = PGExecute(cSQL, cErrorMessage)
            CloseConn()

        Catch ex As Exception
            ErrDisp(ex,, cSQL)
        End Try

    End Function

    Public Function PGExecute(cSQL As String, Optional ByRef cErrorMessage As String = "") As Boolean

        PGExecute = False

        Try
            Dim oCommand As NpgsqlCommand

            If cSQL.Trim = "" Then Exit Function

            oCommand = New NpgsqlCommand(cSQL, oConnection)
            oCommand.ExecuteNonQuery()

            PGExecute = True

        Catch ex As Exception
            cErrorMessage = ex.Message.ToString
            ErrDisp(ex,, cSQL)
        End Try
    End Function

End Class
