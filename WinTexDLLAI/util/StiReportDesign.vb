Option Explicit On
Option Strict On

Imports Stimulsoft.Report
Imports Stimulsoft.Report.Dictionary
Imports Stimulsoft.Report.Design
Imports System.Data.SqlClient

Public Class StiReportDesign

    Private Structure oStimulusReport
        Dim cReportID As String
        Dim cReportName As String
        Dim cReportClass As String
        Dim cReportSQL As String
        Dim cReportVariable1 As String
        Dim cReportVariable2 As String
        Dim cReportVariable3 As String
        Dim cReportVariable4 As String
        Dim cReportVariable5 As String
        Dim cReportVariable6 As String
        Dim cReportVariable7 As String
        Dim cReportVariable8 As String
        Dim cReportVariable9 As String
        Dim cReportVariable10 As String
        Dim cReport As String
        Dim cPrinterName As String
        Dim nCopies As Double
        Dim oReportStimulus As Stimulsoft.Report.StiReport
        Dim cDigerDiller As String
    End Structure

    Dim oReport As oStimulusReport
    Dim lUserClose As Boolean = False

    Public Sub init(Optional cReportID As String = "", Optional cReportClass As String = "", Optional cReportVariable1 As String = "", Optional cReportSQL As String = "",
                    Optional cReportVariable2 As String = "", Optional cReportVariable3 As String = "", Optional cReportVariable4 As String = "", Optional cReportVariable5 As String = "",
                    Optional cReportVariable6 As String = "", Optional cReportVariable7 As String = "", Optional cReportVariable8 As String = "", Optional cReportVariable9 As String = "",
                    Optional cReportVariable10 As String = "")
        Try
            oReport.cReportName = ""
            oReport.cReportID = cReportID.Trim
            oReport.cReportClass = cReportClass.Trim
            oReport.cReportSQL = cReportSQL.Trim
            oReport.cReportVariable1 = cReportVariable1.Trim
            oReport.cReportVariable2 = cReportVariable2.Trim
            oReport.cReportVariable3 = cReportVariable3.Trim
            oReport.cReportVariable4 = cReportVariable4.Trim
            oReport.cReportVariable5 = cReportVariable5.Trim
            oReport.cReportVariable6 = cReportVariable6.Trim
            oReport.cReportVariable7 = cReportVariable7.Trim
            oReport.cReportVariable8 = cReportVariable8.Trim
            oReport.cReportVariable9 = cReportVariable9.Trim
            oReport.cReportVariable10 = cReportVariable10.Trim
            oReport.cReport = ""

            If Not StiLoadReport() Then
                Exit Sub
            End If

            ShowDesigner()

        Catch ex As Exception
            ErrDisp("init : " + ex.Message,,,, ex)
        End Try
    End Sub

    Public Sub initNewReport(Optional cReportID As String = "", Optional cReportClass As String = "", Optional cReportVariable1 As String = "", Optional cReportSQL As String = "",
                             Optional cReportVariable2 As String = "", Optional cReportVariable3 As String = "", Optional cReportVariable4 As String = "", Optional cReportVariable5 As String = "",
                             Optional cReportVariable6 As String = "", Optional cReportVariable7 As String = "", Optional cReportVariable8 As String = "", Optional cReportVariable9 As String = "",
                             Optional cReportVariable10 As String = "")
        Try
            oReport.oReportStimulus = New Stimulsoft.Report.StiReport
            oReport.cReportName = ""
            oReport.cReportID = GetFisNo("reportid")
            oReport.cReportClass = cReportClass.Trim
            oReport.cReportSQL = cReportSQL.Trim
            oReport.cReportVariable1 = cReportVariable1.Trim
            oReport.cReportVariable2 = cReportVariable2.Trim
            oReport.cReportVariable3 = cReportVariable3.Trim
            oReport.cReportVariable4 = cReportVariable4.Trim
            oReport.cReportVariable5 = cReportVariable5.Trim
            oReport.cReportVariable6 = cReportVariable6.Trim
            oReport.cReportVariable7 = cReportVariable7.Trim
            oReport.cReportVariable8 = cReportVariable8.Trim
            oReport.cReportVariable9 = cReportVariable9.Trim
            oReport.cReportVariable10 = cReportVariable10.Trim
            oReport.cReport = ""

            LoadReportSystem()

            ShowDesigner()

        Catch ex As Exception
            ErrDisp("ReportDesigner InitNewReport : " + ex.Message,,,, ex)
        End Try
    End Sub

    Private Sub ShowDesigner()
        Try

            lUserClose = False

            ConfigReportDesigner()
            oReport.oReportStimulus.ScriptLanguage = StiReportLanguageType.VB
            oReport.oReportStimulus.Design()

        Catch ex As Exception
            ErrDisp("ReportDesigner FormLoad : " + ex.Message,,,, ex)
        End Try
    End Sub

    Public Sub New()
        AddHandler Stimulsoft.Report.StiOptions.Engine.GlobalEvents.SavingReportInDesigner, AddressOf GlobalEvents_SavingReportInDesigner
        'AddHandler Stimulsoft.Report.StiOptions.Engine.GlobalEvents.LoadingReportInDesigner, AddressOf GlobalEvents_LoadingReportInDesigner
        'AddHandler Stimulsoft.Report.StiOptions.Engine.GlobalEvents.CreatingReportInDesigner, AddressOf GlobalEvents_CreatingReportInDesigner
    End Sub

    'Private Sub GlobalEvents_CreatingReportInDesigner(ByVal sender As Object, ByVal e As Stimulsoft.Report.Design.StiCreatingObjectEventArgs)
    'End Sub

    'Private Sub GlobalEvents_LoadingReportInDesigner(ByVal sender As Object, ByVal e As Stimulsoft.Report.Design.StiLoadingObjectEventArgs)
    '    e.Processed = True

    '    Dim report As New Stimulsoft.Report.StiReport()
    '    report.Load("..\..\Reports\SimpleList.mrt")
    '    StiRibbonDesignerControl1.Report = report
    'End Sub

    Private Sub GlobalEvents_SavingReportInDesigner(ByVal sender As Object, ByVal e As Stimulsoft.Report.Design.StiSavingObjectEventArgs)
        If oReport.oReportStimulus Is Nothing Then
            Return
        End If
        e.Processed = True

        StiSaveReport()
        oReport.oReportStimulus.Save(oReport.cReportName)
    End Sub

    Private Sub LoadReportSystem()
        Try
            oReport.oReportStimulus.Dictionary.Databases.Clear()
            AddDataBase()

            oReport.oReportStimulus.Dictionary.Variables.Clear()
            AddVariableToDatabase("cVariable1")
            If oReport.cReportVariable2 <> "" Then AddVariableToDatabase("cVariable2")
            If oReport.cReportVariable3 <> "" Then AddVariableToDatabase("cVariable3")
            If oReport.cReportVariable4 <> "" Then AddVariableToDatabase("cVariable4")
            If oReport.cReportVariable5 <> "" Then AddVariableToDatabase("cVariable5")
            If oReport.cReportVariable6 <> "" Then AddVariableToDatabase("cVariable6")
            If oReport.cReportVariable7 <> "" Then AddVariableToDatabase("cVariable7")
            If oReport.cReportVariable8 <> "" Then AddVariableToDatabase("cVariable8")
            If oReport.cReportVariable9 <> "" Then AddVariableToDatabase("cVariable9")
            If oReport.cReportVariable10 <> "" Then AddVariableToDatabase("cVariable10")

            If Not IsNothing(oReport.cReportSQL) Then
                oReport.oReportStimulus.Dictionary.DataSources.Clear()
                AddDataSource()
                oReport.oReportStimulus.Dictionary.Synchronize()
                oReport.oReportStimulus.Dictionary.Connect()
                oReport.oReportStimulus.DataSources(0).SynchronizeColumns()
            End If

        Catch ex As Exception
            ErrDisp("LoadReportSystem : " + ex.Message,,,, ex)
        End Try
    End Sub

    Private Sub GetSaveConfirmation()
        Try
            If Confirmed2("Report : " + oReport.cReportName + vbCrLf + "Rapor veritabanına kaydedilsin mi") Then
                StiSaveReport()
            End If

        Catch ex As Exception
            ErrDisp("GetSaveConfirmation : " + ex.Message,,,, ex)
        End Try
    End Sub

    Private Sub StiSaveReport()

        Dim cSQL As String = ""
        Dim cReportName As String = ""
        Dim cReportClass As String = ""
        Dim cReport As String
        Dim cReportID As String

        Try
            If oReport.cReportName.Trim = "" Then
                cReportName = InputBox("Rapor Adı", "Kaydetmeyi iptal etmek için BOŞ bırakınız", oReport.cReportName)
            Else
                cReportName = oReport.cReportName
            End If
            If cReportName.Trim = "" Then Exit Sub

            If oReport.cReportClass.Trim = "" Then
                cReportClass = InputBox("Rapor Sınıfı", "Kaydetmeyi iptal etmek için BOŞ bırakınız", oReport.cReportClass)
            Else
                cReportClass = oReport.cReportClass
            End If
            If cReportClass.Trim = "" Then Exit Sub

            If oReport.cReportID.Trim = "" Then
                cReportID = GetFisNo("reportid")
            Else
                cReportID = oReport.cReportID
            End If

            cReport = oReport.oReportStimulus.SaveToString ' Save("Report.mrt")
            cReport = cReport.Replace("'", "||")

            cSQL = "select top 1 reportid " +
                   " from stireports with (NOLOCK) " +
                   " where reportid = " + cReportID

            If CheckExists(cSQL) Then
                cSQL = "update stireports " +
                        " set reportname = '" + SQLWriteString(cReportName, 100) + "', " +
                        " report = '" + SQLWriteString(cReport) + "', " +
                        " nvreport = N'" + SQLWriteString(cReport) + "', " +
                        " reportclass = '" + SQLWriteString(cReportClass, 30) + "' " +
                        " where reportid = " + cReportID

                ExecuteSQLCommand(cSQL)
            Else
                cSQL = "insert into stireports (reportid, reportname, report, nvreport, reportclass)  " +
                        " values (" + cReportID + ", " +
                        " '" + SQLWriteString(cReportName, 100) + "', " +
                        " '" + SQLWriteString(cReport) + "', " +
                        " N'" + SQLWriteString(cReport) + "', " +
                        " '" + SQLWriteString(cReportClass, 30) + "' ) "

                ExecuteSQLCommand(cSQL)
            End If

            oReport.cReportID = cReportID
            oReport.cReportName = cReportName
            oReport.cReportClass = cReportClass
            oReport.cReport = cReport

            MsgBox(cReportName.Trim + " " + cReportClass.Trim + vbCrLf + "Report saved")

        Catch ex As Exception
            ErrDisp("SaveReport", , cSQL,, ex)
        End Try
    End Sub

    Private Function StiLoadReport() As Boolean

        Dim cSQL As String = ""
        Dim ConnYage As SqlConnection
        Dim dr As SqlDataReader

        StiLoadReport = False

        Try
            If oReport.cReportID = "" Then
                Exit Function
            End If

            ConnYage = OpenConn()

            oReport.cReportName = ""
            oReport.cReportClass = ""
            oReport.cReport = ""

            cSQL = "select top 1 reportname, report, nvreport, reportclass, digerdiller " +
                    " from stireports with (NOLOCK) " +
                    " where reportid = " + oReport.cReportID

            dr = New SqlCommand(cSQL, ConnYage).ExecuteReader(CommandBehavior.SingleRow)

            If dr.Read Then
                oReport.cReportName = SQLReadString(dr, "reportname")
                oReport.cReportClass = SQLReadString(dr, "reportclass")
                oReport.cDigerDiller = SQLReadString(dr, "digerdiller")
                If oReport.cDigerDiller = "E" Then
                    If SQLReadString(dr, "nvreport") = "" Then
                        oReport.cReport = SQLReadString(dr, "report")
                    Else
                        oReport.cReport = SQLReadString(dr, "nvreport")
                    End If
                Else
                    oReport.cReport = SQLReadString(dr, "report")
                End If
                oReport.cReport = oReport.cReport.Replace("||", "'")
            End If
            dr.Close()

            CloseConn(ConnYage)

            If oReport.cReportName = "" Then
                Exit Function
            End If

            oReport.oReportStimulus = New Stimulsoft.Report.StiReport
            oReport.oReportStimulus.LoadFromString(oReport.cReport) ' Load("..\..\Reports\SimpleList.mrt")
            oReport.oReportStimulus.Dictionary.Databases.Clear()
            AddDataBase()
            StiLoadReport = True

        Catch ex As Exception
            ErrDisp("StiLoadReport : " + ex.Message.Trim, , cSQL,, ex)
        End Try
    End Function

    Private Sub AddDataSource()
        Try
            Dim oDataSource As StiSqlSource = Nothing

            oDataSource = New StiSqlSource()
            oDataSource.NameInSource = "WinTex"
            oDataSource.Name = "WinTexDataSource"
            oDataSource.Alias = "WinTexDataSource"
            oDataSource.ConnectOnStart = False
            oDataSource.ReconnectOnEachRow = False
            oDataSource.SqlCommand = oReport.cReportSQL
            oReport.oReportStimulus.Dictionary.DataSources.Add(oDataSource)

        Catch ex As Exception
            ErrDisp("AddDataSource : " + ex.Message.Trim,,,, ex)
        End Try
    End Sub
    Private Sub AddDataBase()
        Try
            Dim oDataBase As StiSqlDatabase = Nothing

            oDataBase = New StiSqlDatabase()
            oDataBase.Name = "WinTex" ' oConnection.cDatabase
            oDataBase.Alias = "WinTex" ' oConnection.cDatabase
            oDataBase.ConnectionString = oConnection.cConnStr
            oReport.oReportStimulus.Dictionary.Databases.Add(oDataBase)

        Catch ex As Exception
            ErrDisp("AddDataBase : " + ex.Message.Trim,,,, ex)
        End Try
    End Sub

    Private Sub AddVariableToDatabase(ByVal cVariableName As String)
        Try
            Dim oVar As StiVariable = Nothing

            oVar = New StiVariable
            oVar.Name = cVariableName
            oVar.Alias = cVariableName
            oVar.Description = "Filter expression"
            oVar.ReadOnly = False
            oVar.Value = " "
            oReport.oReportStimulus.Dictionary.Variables.Add(oVar)

        Catch ex As Exception
            ErrDisp("AddVariableToDatabase : " + ex.Message.Trim,,,, ex)
        End Try
    End Sub

    Private Sub ConfigReportDesigner()
        Try
            Dim MainMenuService As StiMainMenuService = CType(StiConfig.Services.GetService(GetType(StiMainMenuService)), StiMainMenuService)

            If (Not MainMenuService Is Nothing) Then
                MainMenuService.ShowViewShowHeaders = False
            End If

        Catch ex As Exception
            ErrDisp("ConfigReportDesigner : " + ex.Message,,,, ex)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        Try
            GetSaveConfirmation()
            MyBase.Finalize()

        Catch ex As Exception
            ErrDisp("Finalize : " + ex.Message,,,, ex)
        End Try
    End Sub
End Class
