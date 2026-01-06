Imports DevExpress.DashboardCommon
Imports DevExpress.XtraBars.Ribbon
Imports Microsoft.InteropFormTools
Imports DevExpress.XtraEditors
Imports DevExpress.DashboardWin
Imports DevExpress.DataAccess.Sql
Imports DevExpress.DataAccess.ConnectionParameters
Imports System.Data.SqlClient

<InteropForm()> Public Class frmDashboardDesigner
    Dim aTempTables() As String
    Public Sub New()
        Try
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.

        Catch ex As Exception
            ErrDisp("frmDashboardDesigner New : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub LoadXMLFile()
        Try
            Dim cTempFile As String = ""
            Dim oDashboard As New Dashboard

            If oReportDevX.cReport.Trim = "" Then
                LoadDashboardData(oDashboard)
            Else
                ' Loads a dashboard from an XML file.
                cTempFile = GetTempFile("xml")
                My.Computer.FileSystem.WriteAllText(cTempFile, oReportDevX.cReport, False)
                oDashboard.LoadFromXml(cTempFile)
                DestroyFile(cTempFile)
            End If
            oDashboard.EnableAutomaticUpdates = False

            DashboardDesigner1.Dashboard = oDashboard
            DashboardDesigner1.ReloadData()

        Catch ex As Exception
            ErrDisp("LoadXMLFile : " + ex.Message, Me.Name)
        End Try
    End Sub

    Public Sub init()
        Try
            Dim ribbon As RibbonControl = TryCast(DashboardDesigner1.MenuManager, RibbonControl)

            ReDim aTempTables(0)
            LoadXMLFile()
            Me.Show()

        Catch ex As Exception
            ErrDisp("init : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub frmDashboardDesigner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DashboardDesigner1.DataSourceWizard.SqlWizardSettings.EnableCustomSql = True
            DashboardDesigner1.DataSourceWizard.SqlWizardSettings.QueryBuilderDiagramView = True
            DashboardDesigner1.DataSourceWizard.SqlWizardSettings.QueryBuilderLight = True
            DashboardDesigner1.DataSourceWizard.EFWizardSettings.ShowBrowseButton = True
            DashboardDesigner1.DataSourceWizard.ShowDataSourceNamePage = True

            Me.WindowState = FormWindowState.Maximized

        Catch ex As Exception
            ErrDisp("frmDashboardDesigner_Load : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub Save()
        Try
            Dim cSQL As String = ""
            Dim cReportName As String = ""
            Dim cReportClass As String = ""
            Dim cReport As String
            Dim cReportID As String
            Dim cTempFile As String

            If oReportDevX.cReportName.Trim = "" Then
                cReportName = InputBox("Rapor Adı", "Kaydetmeyi iptal etmek için BOŞ bırakınız", oReportDevX.cReportName)
            Else
                cReportName = oReportDevX.cReportName
            End If
            If cReportName.Trim = "" Then Exit Sub

            If oReportDevX.cReportClass.Trim = "" Then
                cReportClass = InputBox("Rapor Sınıfı", "Kaydetmeyi iptal etmek için BOŞ bırakınız", oReportDevX.cReportClass)
            Else
                cReportClass = oReportDevX.cReportClass
            End If
            If cReportClass.Trim = "" Then Exit Sub

            If oReportDevX.cReportID.Trim = "" Then
                cReportID = GetFisNo("reportid")
            Else
                cReportID = oReportDevX.cReportID
            End If

            cTempFile = GetTempFile("xml")
            DashboardDesigner1.Dashboard.SaveToXml(cTempFile)
            cReport = My.Computer.FileSystem.ReadAllText(cTempFile)
            cReport = cReport.Replace("'", "||")

            oReportDevX.cReportID = cReportID
            oReportDevX.cReportName = cReportName
            oReportDevX.cReportClass = cReportClass
            oReportDevX.cReport = cReport

            cSQL = "select top 1 reportid " +
                   " from devxdashboards with (NOLOCK) " +
                   " where reportid = " + cReportID

            If CheckExists(cSQL) Then
                cSQL = "update devxdashboards " +
                        " set reportname = '" + SQLWriteString(cReportName, 100) + "', " +
                        " report = '" + SQLWriteString(cReport) + "', " +
                        " reportclass = '" + SQLWriteString(cReportClass, 30) + "' " +
                        " where reportid = " + cReportID

                ExecuteSQLCommand(cSQL)
            Else
                cSQL = "insert into devxdashboards (reportid, reportname, report, reportclass)  " +
                        " values (" + oReportDevX.cReportID + ", " +
                        " '" + SQLWriteString(cReportName) + "', " +
                        " '" + SQLWriteString(cReport) + "', " +
                        " '" + SQLWriteString(cReportClass) + "') "

                ExecuteSQLCommand(cSQL)
            End If

            MsgBox(oReportDevX.cReportName + " " + oReportDevX.cReportClass + vbCrLf + "Rapor kaydedildi")

            DestroyFile(cTempFile)

        Catch ex As Exception
            ErrDisp("Save : " + ex.Message, Me.Name)
        End Try
    End Sub

    ' Handles the DashboardSaving event.
    Private Sub dashboardDesigner1_DashboardSaving(ByVal sender As Object,
                                                    ByVal e As DevExpress.DashboardWin.DashboardSavingEventArgs) _
                                                    Handles DashboardDesigner1.DashboardSaving
        Try
            ' Determines whether the user has called the Save command.
            If e.Command = DevExpress.DashboardWin.DashboardSaveCommand.Save Then
                ' Saves the dashboard to the specified XML file.
                Save()
                ' Specifies that the dashboard has been saved and no default actions are required.
                e.Handled = True
                ' Specifies that the dashboard has been saved.
                e.Saved = True
            End If

        Catch ex As Exception
            ErrDisp("DashboardSaving : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub DashboardDesigner1_ConfigureDataConnection(sender As Object, e As DashboardConfigureDataConnectionEventArgs) Handles DashboardDesigner1.ConfigureDataConnection
        Try
            ' Checks the name of the connection for which the event has been raised.
            If e.DataSourceName = "WinTexDataSource" Then
                Dim oSqlParams As MsSqlConnectionParameters = CType(e.ConnectionParameters, MsSqlConnectionParameters)
                oSqlParams.AuthorizationType = MsSqlAuthorizationType.SqlServer
                oSqlParams.ServerName = oConnection.cServer
                oSqlParams.DatabaseName = oConnection.cDatabase
                oSqlParams.UserName = oConnection.cUser
                oSqlParams.Password = oConnection.cPassword
            End If

        Catch ex As Exception
            ErrDisp("ConfigureDataConnection : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub DashboardDesigner1_ValidateCustomSqlQuery(sender As Object, e As ValidateDashboardCustomSqlQueryEventArgs) Handles DashboardDesigner1.ValidateCustomSqlQuery
        Try
            Dim customQuery As CustomSqlQuery = e.CustomSqlQuery
            Dim validationResult As Boolean = True
            ' Insert your custom validation logic here. 
            e.Valid = validationResult

        Catch ex As Exception
            ErrDisp("reportDesigner1_ValidateCustomSql : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub DashboardDesigner1_DashboardItemDoubleClick(sender As Object, e As DashboardItemMouseActionEventArgs) Handles DashboardDesigner1.DashboardItemDoubleClick
        Try

            Dim underlyingData As DashboardUnderlyingDataSet = e.GetUnderlyingData()

            If underlyingData IsNot Nothing Then
                Dim form As New XtraForm()
                form.Text = "Detay Kayıtlar"
                Dim grid As New DataGrid()
                grid.Parent = form
                grid.Dock = DockStyle.Fill
                grid.DataSource = underlyingData
                form.ShowDialog()
                form.Dispose()
            End If

        Catch ex As Exception
            ErrDisp("DashboardItemDoubleClick : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub DashboardDesigner1_DashboardLoaded(sender As Object, e As DashboardLoadedEventArgs) Handles DashboardDesigner1.DashboardLoaded

        Dim cSQL As String = ""
        Dim cTempTable As String = ""
        Dim cTempView As String = ""
        Dim nCnt As Integer = -1

        Try
            Dim oDS As DashboardSqlDataSource = Nothing
            Dim oCustomQuery As CustomSqlQuery = Nothing

            Me.Cursor = Cursors.WaitCursor

            For Each oDS In e.Dashboard.DataSources
                For Each oCustomQuery In oDS.Queries
                    Select Case oCustomQuery.Name
                        Case oReportDevX.cReportQueryName1 : oCustomQuery.Sql = oReportDevX.cReportSQL1
                        Case oReportDevX.cReportQueryName2 : oCustomQuery.Sql = oReportDevX.cReportSQL2
                        Case oReportDevX.cReportQueryName3 : oCustomQuery.Sql = oReportDevX.cReportSQL3
                        Case oReportDevX.cReportQueryName4 : oCustomQuery.Sql = oReportDevX.cReportSQL4
                        Case oReportDevX.cReportQueryName5 : oCustomQuery.Sql = oReportDevX.cReportSQL5
                        Case oReportDevX.cReportQueryName6 : oCustomQuery.Sql = oReportDevX.cReportSQL6
                        Case oReportDevX.cReportQueryName7 : oCustomQuery.Sql = oReportDevX.cReportSQL7
                        Case oReportDevX.cReportQueryName8 : oCustomQuery.Sql = oReportDevX.cReportSQL8
                        Case oReportDevX.cReportQueryName9 : oCustomQuery.Sql = oReportDevX.cReportSQL9
                        Case oReportDevX.cReportQueryName10 : oCustomQuery.Sql = oReportDevX.cReportSQL10
                    End Select
                Next
            Next

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            ErrDisp("DashboardViewer1_DashboardLoaded : " + ex.Message, Me.Name, cSQL)
        End Try
    End Sub

    Private Sub frmDashboardDesigner_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        Dim nCnt As Integer
        Dim ConnYage As SqlConnection = Nothing

        Try
            ConnYage = OpenConn()
            For nCnt = 0 To UBound(aTempTables)
                DropTable(aTempTables(nCnt), ConnYage)
            Next
            ConnYage.Close()

        Catch ex As Exception
            ErrDisp("frmDashboardDesigner_Closed : " + ex.Message, Me.Name)
        End Try
    End Sub

End Class