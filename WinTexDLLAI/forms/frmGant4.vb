Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Microsoft.SqlServer.Server
Imports DevExpress.XtraScheduler
Imports Microsoft.InteropFormTools
Imports Microsoft.VisualBasic

<InteropForm()> Public Class frmGant4

    Private Structure oSiparis
        Dim cResourceName As String
        Dim nID As Double
    End Structure

    Dim nOpMode As Integer = 0

    Dim DXSchedulerDataset As DataSet = Nothing

    Dim AppointmentDataAdapter As SqlDataAdapter = Nothing
    Dim ResourceDataAdapter As SqlDataAdapter = Nothing
    Dim AppointmentDependenciesDataAdapter As SqlDataAdapter = Nothing

    Dim DXSchedulerConn As SqlConnection = Nothing

    Dim cScheduleTable As String = ""
    Dim cAppointmentsTable As String = ""
    Dim cResourcesTable As String = ""
    Dim cTaskDependenciesTable As String = ""

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler SchedulerStorage1.AppointmentsInserted, AddressOf schedulerStorage1_AppointmentsInserted
        AddHandler SchedulerStorage1.AppointmentsChanged, AddressOf schedulerStorage1_AppointmentsChanged
        AddHandler SchedulerStorage1.AppointmentsDeleted, AddressOf schedulerStorage1_AppointmentsDeleted

        AddHandler SchedulerStorage1.AppointmentDependenciesInserted, AddressOf schedulerStorage1_AppointmentDependenciesInserted
        AddHandler SchedulerStorage1.AppointmentDependenciesChanged, AddressOf schedulerStorage1_AppointmentDependenciesChanged
        AddHandler SchedulerStorage1.AppointmentDependenciesDeleted, AddressOf schedulerStorage1_AppointmentDependenciesDeleted

        AddHandler SchedulerStorage1.ResourcesInserted, AddressOf schedulerStorage1_ResourcesInserted
        AddHandler SchedulerStorage1.ResourcesChanged, AddressOf schedulerStorage1_ResourcesChanged
        AddHandler SchedulerStorage1.ResourcesDeleted, AddressOf schedulerStorage1_ResourcesDeleted
    End Sub

    Public Sub init(Optional nMode As Integer = 0, Optional cTableName As String = "", Optional lModal As Boolean = True)
        Try
            nOpMode = nMode
            cScheduleTable = cTableName.Trim

            If lModal Then
                Me.ShowDialog()
            Else
                Me.Show()
            End If

        Catch ex As Exception
            ErrDisp("init : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub frmGant4_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            CreateTables
            initData()
            initDB()
            initControl()
            Me.WindowState = FormWindowState.Maximized

        Catch ex As Exception
            ErrDisp("frmGant4_Load : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub initControl()
        Try
            SchedulerControl1.ActiveViewType = SchedulerViewType.Gantt
            SchedulerControl1.GroupType = SchedulerGroupType.Resource
            SchedulerControl1.GanttView.CellsAutoHeightOptions.Enabled = True
            ' Hide unnecessary visual elements.
            SchedulerControl1.GanttView.ShowResourceHeaders = False
            SchedulerControl1.GanttView.NavigationButtonVisibility = NavigationButtonVisibility.Never

            SchedulerControl1.Start = Date.Today
            SchedulerControl1.DayView.TopRowTime = New TimeSpan(10, 0, 0)
            SchedulerControl1.DayView.ResourcesPerPage = 2
            SchedulerControl1.DayView.TimeIndicatorDisplayOptions.ShowOverAppointment = True

            SchedulerControl1.OptionsCustomization.AllowAppointmentConflicts = AppointmentConflictsMode.Allowed
            SchedulerControl1.OptionsCustomization.AllowAppointmentCopy = UsedAppointmentType.All
            SchedulerControl1.OptionsCustomization.AllowAppointmentCreate = UsedAppointmentType.All
            SchedulerControl1.OptionsCustomization.AllowAppointmentDelete = UsedAppointmentType.All
            SchedulerControl1.OptionsCustomization.AllowAppointmentDrag = UsedAppointmentType.All
            SchedulerControl1.OptionsCustomization.AllowAppointmentDragBetweenResources = UsedAppointmentType.All
            SchedulerControl1.OptionsCustomization.AllowAppointmentEdit = UsedAppointmentType.All
            SchedulerControl1.OptionsCustomization.AllowAppointmentMultiSelect = False
            SchedulerControl1.OptionsCustomization.AllowAppointmentResize = UsedAppointmentType.All
            SchedulerControl1.OptionsCustomization.AllowInplaceEditor = UsedAppointmentType.All

            SplitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel1
            SplitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2


        Catch ex As Exception
            ErrDisp("initControl : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub initDB()
        Try
            Dim selectAppointments As String = ""
            Dim selectResources As String = ""
            Dim selectAppointmentDependencies As String = ""

            SchedulerStorage1.Appointments.ResourceSharing = True
            SchedulerStorage1.Appointments.CommitIdToDataSource = False

            DXSchedulerDataset = New DataSet()

            selectAppointments = "select * " +
                                " from " + cAppointmentsTable + " with (NOLOCK) " +
                                " where uniqueid Is Not null "

            selectResources = "select * " +
                                " from " + cResourcesTable + " with (NOLOCK) " +
                                " where id Is Not null "

            selectAppointmentDependencies = "select * " +
                                " from " + cTaskDependenciesTable + " with (NOLOCK) "

            DXSchedulerConn = OpenConn()

            AppointmentDataAdapter = New SqlDataAdapter(selectAppointments, DXSchedulerConn)
            AddHandler AppointmentDataAdapter.RowUpdated, AddressOf AppointmentDataAdapter_RowUpdated
            AppointmentDataAdapter.Fill(DXSchedulerDataset, cAppointmentsTable)

            ResourceDataAdapter = New SqlDataAdapter(selectResources, DXSchedulerConn)
            ResourceDataAdapter.Fill(DXSchedulerDataset, cResourcesTable)

            AppointmentDependenciesDataAdapter = New SqlDataAdapter(selectAppointmentDependencies, DXSchedulerConn)
            AppointmentDependenciesDataAdapter.Fill(DXSchedulerDataset, cTaskDependenciesTable)

            ' Specify mappings.
            MapAppointmentData()
            MapResourceData()
            MapAppointmentDependenciesData()

            ' Generate commands using CommandBuilder.  
            Dim cmdBuilder As New SqlCommandBuilder(AppointmentDataAdapter)
            AppointmentDataAdapter.InsertCommand = cmdBuilder.GetInsertCommand()
            AppointmentDataAdapter.DeleteCommand = cmdBuilder.GetDeleteCommand()
            AppointmentDataAdapter.UpdateCommand = cmdBuilder.GetUpdateCommand()

            DXSchedulerConn.Close()

            Me.SchedulerStorage1.Appointments.DataSource = DXSchedulerDataset
            Me.SchedulerStorage1.Appointments.DataMember = cAppointmentsTable
            Me.SchedulerStorage1.Resources.DataSource = DXSchedulerDataset
            Me.SchedulerStorage1.Resources.DataMember = cResourcesTable
            Me.SchedulerStorage1.AppointmentDependencies.DataSource = DXSchedulerDataset
            Me.SchedulerStorage1.AppointmentDependencies.DataMember = cTaskDependenciesTable

        Catch ex As Exception
            ErrDisp("initData : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub MapAppointmentData()
        Try
            Me.SchedulerStorage1.Appointments.Mappings.AllDay = "allday"
            Me.SchedulerStorage1.Appointments.Mappings.Description = "description"
            ' Required mapping.
            Me.SchedulerStorage1.Appointments.Mappings.End = "enddate"
            Me.SchedulerStorage1.Appointments.Mappings.Label = "label"
            Me.SchedulerStorage1.Appointments.Mappings.Location = "location"
            Me.SchedulerStorage1.Appointments.Mappings.RecurrenceInfo = "recurrenceinfo"
            Me.SchedulerStorage1.Appointments.Mappings.ReminderInfo = "reminderinfo"
            ' Required mapping.
            Me.SchedulerStorage1.Appointments.Mappings.Start = "startdate"
            Me.SchedulerStorage1.Appointments.Mappings.Status = "status"
            Me.SchedulerStorage1.Appointments.Mappings.Subject = "subject"
            Me.SchedulerStorage1.Appointments.Mappings.Type = "type"
            Me.SchedulerStorage1.Appointments.Mappings.ResourceId = "resourceids" ' SchedulerStorage1.Appointments.ResourceSharing = True
            'Me.SchedulerStorage1.Appointments.Mappings.ResourceId = "resourceid" ' SchedulerStorage1.Appointments.ResourceSharing = False
            Me.SchedulerStorage1.Appointments.CustomFieldMappings.Add(New AppointmentCustomFieldMapping("kod", "kod"))

        Catch ex As Exception
            ErrDisp("MapAppointmentData : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub MapResourceData()
        Try
            Me.SchedulerStorage1.Resources.Mappings.Id = "id"
            Me.SchedulerStorage1.Resources.Mappings.Caption = "resourcename"
            Me.SchedulerStorage1.Resources.Mappings.Color = "color"
            Me.SchedulerStorage1.Resources.Mappings.Image = "image"
            Me.SchedulerStorage1.Resources.Mappings.ParentId = "parentid"

        Catch ex As Exception
            ErrDisp("MapResourceData : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub MapAppointmentDependenciesData()
        Try
            Me.SchedulerStorage1.AppointmentDependencies.Mappings.DependentId = "dependentid"
            Me.SchedulerStorage1.AppointmentDependencies.Mappings.ParentId = "parentid"
            Me.SchedulerStorage1.AppointmentDependencies.Mappings.Type = "type"

        Catch ex As Exception
            ErrDisp("MapAppointmentDependenciesData : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub schedulerStorage1_AppointmentsChanged(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles SchedulerStorage1.AppointmentsChanged
        Try
            CommitTask()
        Catch ex As Exception
            ErrDisp("schedulerStorage1_AppointmentsChanged : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub schedulerStorage1_AppointmentsDeleted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles SchedulerStorage1.AppointmentsDeleted
        Try
            CommitTask()
        Catch ex As Exception
            ErrDisp("schedulerStorage1_AppointmentsDeleted : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub schedulerStorage1_AppointmentsInserted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles SchedulerStorage1.AppointmentsInserted
        Try
            CommitTask()
            'SchedulerStorage1.SetAppointmentId((CType(e.Objects(0), Appointment)), e)
        Catch ex As Exception
            ErrDisp("schedulerStorage1_AppointmentsInserted : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub CommitTask()
        Try
            AppointmentDataAdapter.Update(DXSchedulerDataset.Tables(cAppointmentsTable))
            Me.DXSchedulerDataset.AcceptChanges()
        Catch ex As Exception
            ErrDisp("CommitTask : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub schedulerStorage1_AppointmentDependenciesChanged(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles SchedulerStorage1.AppointmentDependenciesChanged
        Try
            CommitTaskDependency()
        Catch ex As Exception
            ErrDisp("schedulerStorage1_AppointmentDependenciesChanged : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub schedulerStorage1_AppointmentDependenciesDeleted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles SchedulerStorage1.AppointmentDependenciesDeleted
        Try
            CommitTaskDependency()
        Catch ex As Exception
            ErrDisp("schedulerStorage1_AppointmentDependenciesDeleted : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub schedulerStorage1_AppointmentDependenciesInserted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles SchedulerStorage1.AppointmentDependenciesInserted
        Try
            CommitTaskDependency()
        Catch ex As Exception
            ErrDisp("schedulerStorage1_AppointmentDependenciesInserted : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub CommitTaskDependency()
        Try
            AppointmentDependenciesDataAdapter.Update(Me.DXSchedulerDataset.Tables(cTaskDependenciesTable))
            Me.DXSchedulerDataset.AcceptChanges()
        Catch ex As Exception
            ErrDisp("CommitTaskDependency : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub schedulerStorage1_ResourcesChanged(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles SchedulerStorage1.ResourcesChanged
        Try
            CommitResources()
        Catch ex As Exception
            ErrDisp("schedulerStorage1_ResourcesChanged : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub schedulerStorage1_ResourcesDeleted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles SchedulerStorage1.ResourcesDeleted
        Try
            CommitResources()
        Catch ex As Exception
            ErrDisp("schedulerStorage1_ResourcesDeleted : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub schedulerStorage1_ResourcesInserted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles SchedulerStorage1.ResourcesInserted
        Try
            CommitResources()
        Catch ex As Exception
            ErrDisp("schedulerStorage1_ResourcesInserted : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub CommitResources()
        Try
            Select Case nOpMode
                Case 1
                Case Else
                    ResourceDataAdapter.Update(Me.DXSchedulerDataset.Tables(cResourcesTable))
                    Me.DXSchedulerDataset.AcceptChanges()
            End Select
        Catch ex As Exception
            ErrDisp("CommitResources : " + ex.Message, Me.Name)
        End Try
    End Sub

    ' Retrieve identity value for an inserted appointment.
    Private Sub AppointmentDataAdapter_RowUpdated(ByVal sender As Object, ByVal e As SqlRowUpdatedEventArgs)
        Try
            If e.Status = UpdateStatus.Continue AndAlso e.StatementType = StatementType.Insert Then
                Dim id As Integer = 0
                Using cmd As New SqlCommand("SELECT IDENT_CURRENT('" + cAppointmentsTable + "')", DXSchedulerConn)
                    id = Convert.ToInt32(cmd.ExecuteScalar())
                End Using
                e.Row("uniqueid") = id
            End If

        Catch ex As Exception
            ErrDisp("AppointmentDataAdapter_RowUpdated : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub schedulerControl1_InitNewAppointment(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.AppointmentEventArgs) Handles SchedulerControl1.InitNewAppointment
        Try
            e.Appointment.Description &= "Created at runtime at " & String.Format("{0:g}", Date.Now)
            e.Appointment.CustomFields("Amount") = 0.01R
            e.Appointment.CustomFields("ContactInfo") = "saban@donsa.com.tr"

        Catch ex As Exception
            ErrDisp("schedulerControl1_InitNewAppointment : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub schedulerControl1_AllowAppointmentConflicts(ByVal sender As Object, ByVal e As AppointmentConflictEventArgs)
        Try
            e.Conflicts.Clear()

            Dim depCollectionDep As AppointmentDependencyBaseCollection = SchedulerStorage1.AppointmentDependencies.Items.GetDependenciesByDependentId(e.Appointment.Id)
            If depCollectionDep.Count > 0 Then
                If CheckForInvalidDependenciesAsDependent(depCollectionDep, e.AppointmentClone) Then
                    e.Conflicts.Add(e.AppointmentClone)
                End If
            End If

            Dim depCollectionPar As AppointmentDependencyBaseCollection = SchedulerStorage1.AppointmentDependencies.Items.GetDependenciesByParentId(e.Appointment.Id)
            If depCollectionPar.Count > 0 Then
                If CheckForInvalidDependenciesAsParent(depCollectionPar, e.AppointmentClone) Then
                    e.Conflicts.Add(e.AppointmentClone)
                End If
            End If

        Catch ex As Exception
            ErrDisp("schedulerControl1_AllowAppointmentConflicts : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Function CheckForInvalidDependenciesAsDependent(ByVal depCollection As AppointmentDependencyBaseCollection, ByVal apt As Appointment) As Boolean

        CheckForInvalidDependenciesAsDependent = False

        Try
            For Each dep As AppointmentDependency In depCollection
                If dep.Type = AppointmentDependencyType.FinishToStart Then
                    Dim checkTime As DateTime = SchedulerStorage1.Appointments.Items.GetAppointmentById(dep.ParentId).End
                    If apt.Start < checkTime Then
                        Return True
                    End If
                End If
            Next dep
            Return False

        Catch ex As Exception
            ErrDisp("CheckForInvalidDependenciesAsDependent : " + ex.Message, Me.Name)
        End Try
    End Function

    Private Function CheckForInvalidDependenciesAsParent(ByVal depCollection As AppointmentDependencyBaseCollection, ByVal apt As Appointment) As Boolean

        CheckForInvalidDependenciesAsParent = False

        Try
            For Each dep As AppointmentDependency In depCollection
                If dep.Type = AppointmentDependencyType.FinishToStart Then
                    Dim checkTime As DateTime = SchedulerStorage1.Appointments.Items.GetAppointmentById(dep.DependentId).Start
                    If apt.End > checkTime Then
                        Return True
                    End If
                End If
            Next dep
            Return False

        Catch ex As Exception
            ErrDisp("CheckForInvalidDependenciesAsParent : " + ex.Message, Me.Name)
        End Try
    End Function

    Private Sub initData()
        Try
            Dim cSQL As String = ""
            Dim ConnYage As SqlConnection
            Dim oReader As SqlDataReader
            Dim nResourceID As Integer = 0
            Dim oResource As Resource = Nothing
            Dim nCnt As Integer = 0
            Dim aResources() As oSiparis
            Dim nSipCnt As Integer = -1
            Dim aModel() As String = Nothing
            Dim cModelNo As String = ""


            Select Case nOpMode
                Case 1
                    ' resources tree
                    'ResourcesTree1.OptionsView.ShowAutoFilterRow = True
                    'ResourcesTree1.OptionsView.ShowFilterPanelMode = DevExpress.XtraTreeList.ShowFilterPanelMode.ShowAlways
                    'ResourcesTree1.OptionsView.ShowCheckBoxes = True
                    'ResourcesTree1.OptionsCustomization.AllowChangeBandParent = False

                    ConnYage = OpenConn()

                    nResourceID = 0
                    ReDim aResources(0)

                    cSQL = "select distinct masterresourcename, masterresourceexplanation, masterresourcebegin, masterresourceend " +
                            " from " + cScheduleTable + " with (NOLOCK) " +
                            " where masterresourcename is not null " +
                            " and masterresourcename <> '' " +
                            " order by masterresourcename  "

                    oReader = GetSQLReader(cSQL, ConnYage)

                    Do While oReader.Read
                        cSQL = "insert " + cResourcesTable + " ([resourcename], [description] ) " +
                                " values ('" + SQLReadString(oReader, "masterresourcename") + "', " +
                                        " '" + SQLReadString(oReader, "masterresourcename") + "') "

                        ExecuteSQLCommand(cSQL)

                        cSQL = "select id " +
                                " from " + cResourcesTable + " with (NOLOCK) " +
                                " where description = '" + SQLReadString(oReader, "masterresourcename") + "' "

                        nResourceID = ReadSingleIntegerValue(cSQL)

                        cSQL = "insert " + cAppointmentsTable + " ([startdate], [enddate], [resourceid] ) " +
                               " values ('" + SQLReadDate(oReader, "masterresourcebegin").ToString + "', " +
                                          " '" + SQLReadDate(oReader, "masterresourceend").ToString + "', " +
                                          nResourceID.ToString + " ) "

                        ExecuteSQLCommand(cSQL, True)

                        nSipCnt = nSipCnt + 1
                        ReDim Preserve aResources(nSipCnt)
                        aResources(nSipCnt).cResourceName = SQLReadString(oReader, "masterresourcename")
                        aResources(nSipCnt).nID = nResourceID
                    Loop
                    oReader.Close()

                    For nCnt = 0 To UBound(aResources)

                        cSQL = "select distinct minorresourcename, minorresourceexplanation, minorresourcesirano, minorresourcebegin, minorresourceend " +
                                " from " + cScheduleTable + " with (NOLOCK) " +
                                " where masterresourcename = '" + aResources(nCnt).cResourceName + "' " +
                                " order by minorresourcesirano  "

                        oReader = GetSQLReader(cSQL, ConnYage)

                        Do While oReader.Read

                            cSQL = "insert " + cResourcesTable + " ([resourcename], [description], [parentid] ) " +
                                    " values ('" + SQLReadString(oReader, "minorresourcename") + "', " +
                                            " '" + aResources(nCnt).cResourceName + "-" + SQLReadString(oReader, "minorresourcename") + "', " +
                                            aResources(nCnt).nID.ToString + " ) "

                            ExecuteSQLCommand(cSQL)

                            cSQL = "select id " +
                                " from " + cResourcesTable + " with (NOLOCK) " +
                                " where description = '" + aResources(nCnt).cResourceName + "-" + SQLReadString(oReader, "minorresourcename") + "' "

                            nResourceID = ReadSingleIntegerValue(cSQL)

                            cSQL = "set dateformat dmy " +
                                    " insert " + cAppointmentsTable + " ([startdate], [enddate], [resourceid] ) " +
                                    " values ('" + SQLReadDate(oReader, "minorresourcebegin").ToString + "', " +
                                            " '" + SQLReadDate(oReader, "minorresourceend").ToString + "', " +
                                            nResourceID.ToString + " ) "

                            ExecuteSQLCommand(cSQL)
                        Loop
                        oReader.Close()

                    Next

                    ConnYage.Close()
            End Select

        Catch ex As Exception
            ErrDisp("initData : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub CreateTables()
        Try
            Dim ConnYage As SqlConnection = Nothing
            Dim cSQL As String = ""

            ConnYage = OpenConn()

            cAppointmentsTable = GetTmpTableName("appointments", ConnYage)

            cSQL = "CREATE TABLE " + cAppointmentsTable + " ( " +
                " uniqueid INT IDENTITY(1,1) NOT NULL, " +
                " type INT NULL, " +
                " startdate SMALLDATETIME NULL, " +
                " enddate SMALLDATETIME NULL, " +
                " allday BIT NULL, " +
                " subject NVARCHAR(MAX) NULL, " +
                " location NVARCHAR(MAX) NULL, " +
                " description NVARCHAR(MAX) NULL, " +
                " status INT NULL, " +
                " label INT NULL, " +
                " resourceid INT NULL, " +
                " resourceids NVARCHAR(MAX) NULL, " +
                " reminderinfo NVARCHAR(MAX) NULL, " +
                " recurrenceinfo NVARCHAR(MAX) NULL, " +
                " percentcomplete INT NULL, " +
                " timezoneid NVARCHAR(MAX) NULL, " +
                " kod CHAR(30) NULL, " +
                " username CHAR(30) NULL, " +
                " CONSTRAINT pk_" + cAppointmentsTable + " PRIMARY KEY CLUSTERED (uniqueid ASC)) "

            ExecuteSQLCommandConnected(cSQL, ConnYage)

            cResourcesTable = GetTmpTableName("resources", ConnYage)

            cSQL = "CREATE TABLE " + cResourcesTable + " ( " +
                " id INT IDENTITY(1,1) NOT NULL, " +
                " idsort INT NULL, " +
                " parentid INT NULL, " +
                " description NVARCHAR(MAX) NULL, " +
                " color INT NULL, " +
                " image IMAGE NULL, " +
                " kod CHAR(30) NULL, " +
                " resourcename CHAR(30) NULL, " +
                " username CHAR(30) NULL, " +
                " CONSTRAINT pk_" + cResourcesTable + " PRIMARY KEY CLUSTERED (id ASC)) "

            ExecuteSQLCommandConnected(cSQL, ConnYage)

            cTaskDependenciesTable = GetTmpTableName("taskdependencies", ConnYage)

            cSQL = "CREATE TABLE " + cTaskDependenciesTable + " ( " +
                " id INT IDENTITY(1,1) NOT NULL, " +
                " parentid INT NULL, " +
                " dependentid INT NULL, " +
                " type INT NOT NULL, " +
                " CONSTRAINT pk_" + cTaskDependenciesTable + " PRIMARY KEY CLUSTERED (id ASC)) "

            ExecuteSQLCommandConnected(cSQL, ConnYage)

            ConnYage.Close()

        Catch ex As Exception
            ErrDisp("CreateTables : " + ex.Message, Me.Name)
        End Try
    End Sub

    Private Sub frmGant4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            Dim ConnYage As SqlConnection = Nothing

            ConnYage = OpenConn()

            DropTable(cAppointmentsTable, ConnYage)
            DropTable(cResourcesTable, ConnYage)
            DropTable(cTaskDependenciesTable, ConnYage)

            ConnYage.Close()

        Catch ex As Exception
            ErrDisp("frmGant4_FormClosing : " + ex.Message, Me.Name)
        End Try
    End Sub


End Class