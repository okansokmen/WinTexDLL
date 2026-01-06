Option Explicit On
Option Strict On

Module utilEliar

    Private Function NewConnectionEliar() As SQLServerClass

        NewConnectionEliar = Nothing

        Try
            Dim oSQL As New SQLServerClass
            Dim cServer As String = ""
            Dim cDataBase As String = ""
            Dim cUsername As String = ""
            Dim cPassword As String = ""

            oSQL.OpenConn()
            cServer = oSQL.GetSysPar("eliarserver", "msi")
            cDataBase = oSQL.GetSysPar("eliardatabase", "DmExchange")
            cUsername = oSQL.GetSysPar("eliarusername", "sa")
            cPassword = oSQL.GetSysPar("eliarpassword", "Hayabusa1024")
            oSQL.CloseConn()

            NewConnectionEliar = New SQLServerClass(False, cServer, cDataBase, cUsername, cPassword)

        Catch ex As Exception
            ErrDisp("NewConnectionEliar", "utilEliar",,, ex)
        End Try
    End Function


End Module
