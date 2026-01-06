Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Xml.Serialization

Public Module SQLQuery

    <XmlRoot("Queries")>
    Public Class Queries
        <XmlElement("Query")>
        Public Property Queries As List(Of Query)
        <XmlIgnore>
        Default Public ReadOnly Property Index(CommandID As String) As String
            Get
                Return Queries.SingleOrDefault(Function(q) q.CommandID = CommandID)?.CommandText
            End Get
        End Property
    End Class

    Public Class Query
        <XmlAttribute>
        Public Property CommandID As String
        <XmlAttribute>
        Public Property CommandText As String
    End Class

    Public Function GetSQLQuery(cID As String, Optional ByVal cFilter As String = "", Optional ByVal cTopRecordCount As String = "") As String

        GetSQLQuery = ""

        Dim cSQL As String = ""

        Try
            Dim mySerializer As New XmlSerializer(GetType(Queries))
            Dim myQueries As Queries ' will hold your query objects

            Using sr As New IO.StreamReader("SQLQueries.xml")
                myQueries = DirectCast(mySerializer.Deserialize(sr), Queries)
            End Using

            'For Each myQuery In myQueries.Queries ' iterate over query objects
            '    Console.WriteLine($"Query ID={myQuery.CommandID }, CommandText='{myQuery.CommandText}'")
            'Next

            'Dim myCommandText = myQueries("XLSP") ' get the query with ID XLSP
            'Console.WriteLine($"XLSP CommandText='{myCommandText}'")

            'myCommandText = myQueries("ABCD") ' get a query which doesn't exist (returns "")
            'Console.WriteLine($"ABCD CommandText='{myCommandText}'")

            cSQL = myQueries.Index(cID)
            cSQL = String.Format(cSQL, cFilter, cTopRecordCount)

            GetSQLQuery = cSQL.Trim

        Catch ex As Exception
            ' ErrDisp("GetSQLQuery", "SQLQuery", cSQL,, ex)
        End Try
    End Function


End Module
