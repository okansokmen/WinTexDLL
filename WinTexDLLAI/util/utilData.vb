Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.IO

Imports Microsoft.SqlServer.Server

Module utilData
    Public Function GetData_DovKur(ByVal dTarih As Date) As BindingList(Of Record_DovKur)

        GetData_DovKur = Nothing

        Try
            Dim oRecord As Record_DovKur
            Dim oRecords As New BindingList(Of Record_DovKur)()
            Dim oSQL As New SQLServerClass
            Dim cSQL As String = ""
            Dim nCnt As Integer = 0

            cSQL = "set dateformat dmy " +
                    " select sirano, doviz, " +
                    " kurtipi = (case when kurcinsi = 'Kur' then 'Alis Kuru' else kurcinsi end),  " +
                    " kur " +
                    " from dovkur with (NOLOCK) " +
                    " where doviz Is Not null " +
                    " And doviz <> '' " +
                    " and kurcinsi is not null " +
                    " and kurcinsi <> '' " +
                    " and kur is not null " +
                    " and kur <> 0 " +
                    " and tarih = '" + dTarih.Date.ToShortDateString + "' " +
                    " order by doviz, kurcinsi "

            oSQL.OpenConn()
            oSQL.GetSQLReader(cSQL)
            Do While oSQL.oReader.Read

                oRecord = New Record_DovKur

                oRecord.ID = oSQL.SQLReadDouble("sirano")
                oRecord.Doviz = oSQL.SQLReadString("doviz")
                oRecord.KurCinsi = oSQL.SQLReadString("kurtipi")
                oRecord.Kur = oSQL.SQLReadDouble("kur")

                oRecords.Add(oRecord)
            Loop
            oSQL.oReader.Close()
            oSQL.CloseConn()

            Return oRecords

        Catch ex As Exception
            ErrDisp(ex.Message, "GetData_DovKur",  ,, ex)
        End Try
    End Function

    Public Class Record_DovKur
        Implements INotifyPropertyChanged

        Public Sub New()
        End Sub

        Private nID As Double
        Public Property ID() As Double
            Get
                Return nID
            End Get
            Set(ByVal value As Double)
                If nID <> value Then
                    nID = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private cDoviz As String
        <DisplayName("Doviz")>
        Public Property Doviz() As String
            Get
                Return cDoviz
            End Get
            Set(ByVal value As String)
                If cDoviz <> value Then
                    If String.IsNullOrEmpty(value) Then
                        Throw New Exception()
                    End If
                    cDoviz = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private cKurCinsi As String
        <DisplayName("KurCinsi")>
        Public Property KurCinsi() As String
            Get
                Return cKurCinsi
            End Get
            Set(ByVal value As String)
                If cKurCinsi <> value Then
                    If String.IsNullOrEmpty(value) Then
                        Throw New Exception()
                    End If
                    cKurCinsi = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private nKur As Double
        <DisplayName("Kur")>
        Public Property Kur() As Double
            Get
                Return nKur
            End Get
            Set(ByVal value As Double)
                If Not nKur.Equals(value) Then
                    nKur = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("ID = {0}, Doviz = {1}, KurCinsi = {2}, Kur = {3}", ID, Doviz, KurCinsi, Kur)
        End Function

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = "")
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class

    Public Function GetData_IslemGrubu() As BindingList(Of Record_IslemGrubu)

        GetData_IslemGrubu = Nothing

        Try
            Dim oRecord As Record_IslemGrubu
            Dim oRecords As New BindingList(Of Record_IslemGrubu)()
            Dim oSQL As New SQLServerClass
            Dim cSQL As String = ""
            Dim nCnt As Integer = 0

            cSQL = "select sirano, islemgrubu, arabic " +
                    " from islemgrubu with (NOLOCK) " +
                    " where islemgrubu Is Not null " +
                    " and islemgrubu <> '' " +
                    " order by islemgrubu  "

            oSQL.OpenConn()
            oSQL.GetSQLReader(cSQL)
            Do While oSQL.oReader.Read

                oRecord = New Record_IslemGrubu

                oRecord.ID = oSQL.SQLReadDouble("sirano")
                oRecord.IslemGrubu = oSQL.SQLReadString("islemgrubu")
                oRecord.Arabic = oSQL.SQLReadString("arabic")

                oRecords.Add(oRecord)
            Loop
            oSQL.oReader.Close()
            oSQL.CloseConn()

            Return oRecords

        Catch ex As Exception
            ErrDisp(ex.Message, "GetData_IslemGrubu",  ,, ex)
        End Try
    End Function

    Public Class Record_IslemGrubu
        Implements INotifyPropertyChanged

        Public Sub New()
        End Sub

        Private nID As Double
        Public Property ID() As Double
            Get
                Return nID
            End Get
            Set(ByVal value As Double)
                If nID <> value Then
                    nID = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private cIslemGrubu As String
        <DisplayName("IslemGrubu")>
        Public Property IslemGrubu() As String
            Get
                Return cIslemGrubu
            End Get
            Set(ByVal value As String)
                If cIslemGrubu <> value Then
                    If String.IsNullOrEmpty(value) Then
                        Throw New Exception()
                    End If
                    cIslemGrubu = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Private cArabic As String
        <DisplayName("Arabic")>
        Public Property Arabic() As String
            Get
                Return cArabic
            End Get
            Set(ByVal value As String)
                If cArabic <> value Then
                    If String.IsNullOrEmpty(value) Then
                        Throw New Exception()
                    End If
                    cArabic = value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("ID = {0}, IslemGrubu = {1}, Arabic = {2} ", ID, cIslemGrubu, cArabic)
        End Function

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = "")
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class

End Module
