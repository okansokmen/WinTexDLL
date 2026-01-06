Module utilMeasureFix

    Public oService1 As Service1

    Public Class MeasureFixReceivedMessage
        Public MFMessage As MeasureFixReceivedMessageSub
    End Class

    Public Class MeasureFixReceivedMessageSub
        Public jsonrpc As String
        Public message As String
        Public params As String
    End Class

    Public Structure oSQLConn
        Dim cServer As String
        Dim cDatabase As String
        Dim cUser As String
        Dim cPassword As String
        Dim cConnStr As String
    End Structure

    Public oConnection As oSQLConn

    Public Function GetSequenceFisNo(cFieldName As String) As String

        Dim oSQL As New SQLServerClass
        Dim cSQL As String = ""
        Dim nFisNo As Double = 0

        GetSequenceFisNo = ""

        If cFieldName.Trim = "" Then Exit Function

        Try
            oSQL.OpenConn()

            cSQL = "Select top 1 name " +
                    " from sys.sequences " +
                    " where name = '" + cFieldName.Trim + "' "

            If oSQL.CheckExists(cSQL) Then
                cSQL = "select fisno = convert(decimal(18,0), next value for " + cFieldName.Trim + ")"
                nFisNo = oSQL.DBReadDouble(cSQL)
            Else
                cSQL = "create sequence " + cFieldName.Trim + " start with 1 increment by 1 "
                oSQL.SQLExecute(cSQL)
                nFisNo = 1
            End If

            oSQL.CloseConn()

            GetSequenceFisNo = Strings.Format(nFisNo, "0000000000")

        Catch ex As Exception
            ErrDisp(ex.Message, "GetSequenceFisNo",,, ex)
        End Try
    End Function

End Module
