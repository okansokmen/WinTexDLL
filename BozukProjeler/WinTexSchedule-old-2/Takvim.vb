Option Explicit On

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports Dbi.WinControl.Schedule

Public Class Takvim

    Dim cFilter As String = ""
    Dim DbiScheduleObject1 As Dbi.WinControl.Schedule.dbiScheduleObject()
    Dim cGruplama As String = ""
    Dim lLoading As Boolean = False

    Public Sub init(Optional cFilter1 As String = "")
        Try
            cFilter = cFilter1
            cGruplama = "Atölye"
            Me.ShowDialog()

        Catch ex As Exception
            ErrDisp("init : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Takvim_Load(sender As Object, e As EventArgs) Handles Me.Load
        On Error Resume Next
        lLoading = True
        SetSchedule()
        GetData()
        lLoading = False
        Me.Text = "WinTex Kaynak Planlama Versiyon " + My.Application.Info.Version.ToString.Trim
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
    End Sub

    Private Sub SetSchedule()

        Try
            Dim DbiScheduleObject1 As New Dbi.WinControl.Schedule.dbiScheduleObject()
            Dim oSQL As New SQLServerClass
            Dim dBasla As DateTime = Now.Date
            Dim dBitis As DateTime = DateAdd(DateInterval.Day, 365, Now)

            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 baslama_tar " +
                        " from uretimisemriplanlama with (NOLOCK) " +
                        " where baslama_tar is not null " +
                        " and baslama_tar <> '01.01.1950' " +
                        " and bitis_tar is not null " +
                        " and bitis_tar <> '01.01.1950' " +
                        " and bitis_tar >= baslama_tar " +
                        cFilter +
                        " order by baslama_tar "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                dBasla = oSQL.SQLReadDate("baslama_tar")
            End If
            oSQL.oReader.Close()

            oSQL.cSQLQuery = "select top 1 bitis_tar " +
                        " from uretimisemriplanlama with (NOLOCK)  " +
                        " where baslama_tar is not null " +
                        " and baslama_tar <> '01.01.1950' " +
                        " and bitis_tar is not null " +
                        " and bitis_tar <> '01.01.1950' " +
                        " and bitis_tar >= baslama_tar " +
                        cFilter +
                        " order by bitis_tar desc "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                dBitis = oSQL.SQLReadDate("bitis_tar")
            End If
            oSQL.oReader.Close()

            oSQL.CloseConn()

            'dBasla = DateAdd(DateInterval.Day, -10, dBasla)
            'dBitis = DateAdd(DateInterval.Day, 10, dBitis)

            DbiSchedule1.AllowDrop = False
            DbiSchedule1.AllowHorzMoves = True
            DbiSchedule1.AllowVertMoves = False
            DbiSchedule1.AllowItemDrag = True
            DbiSchedule1.AllowNewBars = False
            DbiSchedule1.AltColorEven = System.Drawing.Color.LightCyan
            DbiSchedule1.AltColorOdd = System.Drawing.Color.AliceBlue
            DbiSchedule1.BackColor = System.Drawing.Color.LightBlue

            DbiSchedule1.BarSelectBackColor = System.Drawing.Color.Yellow
            DbiSchedule1.BarSelectForeColor = System.Drawing.Color.Red
            DbiSchedule1.BarSelectBorderColor = System.Drawing.Color.Red

            DbiSchedule1.CurrentTimeLine = True
            DbiSchedule1.CurrentTimeColor = System.Drawing.Color.Crimson
            DbiSchedule1.CurrentTimeColorTo = System.Drawing.Color.Yellow
            DbiSchedule1.CurrentTimePosition = enumCurrentTimePosition.Front
            'DbiSchedule1.DragTimerInterval = 1

            DbiSchedule1.DisplayGuideLines = True
            DbiSchedule1.GuideLineColor = System.Drawing.Color.Black
            DbiSchedule1.GuideLineWidth = 2

            DbiSchedule1.HorzGridLines = True
            DbiSchedule1.HorzGridColor = System.Drawing.Color.Black
            DbiSchedule1.HorzGridWidth = 2

            DbiSchedule1.VertGridLines = True
            DbiSchedule1.VertGridColor = System.Drawing.Color.Black

            DbiSchedule1.DisplayTitle = True
            DbiSchedule1.TitleText = "WinTex Üretim İşemirleri"
            DbiSchedule1.TitleAlign = Drawing.ContentAlignment.MiddleCenter

            DbiSchedule1.HeaderAlign = Drawing.ContentAlignment.MiddleCenter

            DbiSchedule1.SnapToGrid = True
            DbiSchedule1.SnapToGuideLines = True
            DbiSchedule1.SplitterBarColor = System.Drawing.Color.Red
            DbiSchedule1.SplitterBarWidth = 2
            DbiSchedule1.TipsBackColor = System.Drawing.Color.Yellow
            DbiSchedule1.HistorySize = 1000

            DbiSchedule1.TipsOnNotes = True
            DbiSchedule1.TipsOnChangeBar = True
            DbiSchedule1.TipsOnCreateBar = True
            DbiSchedule1.TipsOnOverBar = True
            DbiSchedule1.TipsOnVScroll = True

            'DbiSchedule1.WatermarkText = "Vera"
            'DbiSchedule1.WatermarkAlign = Drawing.ContentAlignment.MiddleCenter

            DbiSchedule1.DisplayHeader = True
            DbiSchedule1.LockLineHeight = False
            DbiSchedule1.LockSplitterBar = False

            DbiSchedule1.WeekendDays.Add(DayOfWeek.Saturday)
            DbiSchedule1.WeekendDays.Add(DayOfWeek.Sunday)

            DbiScheduleObject1.DisplayDays = True
            DbiScheduleObject1.DisplayTimeBarDates = True
            DbiScheduleObject1.HeaderColor = System.Drawing.Color.LightSteelBlue

            DbiScheduleObject1.DisplayTimeLines = False
            DbiScheduleObject1.TimeLineWidth = Dbi.WinControl.Schedule.dbiScheduleObject.enumTimeLineWidth.Thick
            DbiScheduleObject1.TimeLineColor = System.Drawing.Color.Red

            'DbiScheduleObject1.AltColorEven = System.Drawing.Color.LightCyan
            'DbiScheduleObject1.AltColorOdd = System.Drawing.Color.AliceBlue

            DbiScheduleObject1.TimeType = Dbi.WinControl.Schedule.enumTimeType.Weeks
            DbiScheduleObject1.TimeDistance = 30
            DbiScheduleObject1.Start = dBasla 'New DateTime(dtDate.Year, dtDate.Month, dtDate.Day, 0, 0, 0)
            DbiScheduleObject1.End = dBitis '  DbiScheduleObject1.Start.AddDays(20)
            DbiScheduleObject1.RulerAlign = Dbi.WinControl.Schedule.dbiScheduleObject.enumRulerAlign.Over
            DbiScheduleObject1.LineMaxSize = 5
            DbiScheduleObject1.WeekendColor = System.Drawing.Color.Yellow
            DbiScheduleObject1.FirstDayOfWeek = Dbi.WinControl.Schedule.dbiScheduleObject.enumFirstDayOfWeek.Monday
            DbiScheduleObject1.DisplayOverlap = True
            DbiScheduleObject1.OverlapColor = System.Drawing.Color.Orange
            'DbiScheduleObject1.DateFormat = "D n/y"

            DbiSchedule1.Schedules.Clear()
            DbiSchedule1.Schedules.Add(DbiScheduleObject1)


            FillCombos()

        Catch ex As Exception
            ErrDisp("SetSchedule : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub FillCombos()

        Try
            Dim oSQL As New SQLServerClass

            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Atölye")
            ComboBox1.Items.Add("Müşteri")
            ComboBox1.Items.Add("Model")
            ComboBox1.Items.Add("İmalatçı")
            ComboBox1.SelectedItem = "Atölye"

            oSQL.OpenConn()

            CheckedComboBoxEdit1.Properties.Items.Clear()

            oSQL.cSQLQuery = "select distinct musteri  " +
                        " from uretimisemriplanlama with (NOLOCK)  " +
                        " where baslama_tar Is Not null " +
                        " and baslama_tar <> '01.01.1950' " +
                        " and bitis_tar is not null " +
                        " and bitis_tar <> '01.01.1950' " +
                        " and bitis_tar >= baslama_tar "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                CheckedComboBoxEdit1.Properties.Items.Add(oSQL.SQLReadString("musteri"))
            Loop
            oSQL.oReader.Close()
            CheckedComboBoxEdit1.CheckAll()

            CheckedComboBoxEdit2.Properties.Items.Clear()

            oSQL.cSQLQuery = "select distinct firma  " +
                        " from uretimisemriplanlama with (NOLOCK)  " +
                        " where baslama_tar Is Not null " +
                        " and baslama_tar <> '01.01.1950' " +
                        " and bitis_tar is not null " +
                        " and bitis_tar <> '01.01.1950' " +
                        " and bitis_tar >= baslama_tar "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                CheckedComboBoxEdit2.Properties.Items.Add(oSQL.SQLReadString("firma"))
            Loop
            oSQL.oReader.Close()
            CheckedComboBoxEdit2.CheckAll()

            CheckedComboBoxEdit3.Properties.Items.Clear()

            oSQL.cSQLQuery = "select distinct imalatci  " +
                        " from uretimisemriplanlama with (NOLOCK)  " +
                        " where baslama_tar Is Not null " +
                        " and baslama_tar <> '01.01.1950' " +
                        " and bitis_tar is not null " +
                        " and bitis_tar <> '01.01.1950' " +
                        " and bitis_tar >= baslama_tar "

            oSQL.GetSQLReader()

            Do While oSQL.oReader.Read
                CheckedComboBoxEdit3.Properties.Items.Add(oSQL.SQLReadString("imalatci"))
            Loop
            oSQL.oReader.Close()
            CheckedComboBoxEdit3.CheckAll()

            oSQL.CloseConn()

        Catch ex As Exception
            ErrDisp("FillCombos : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub GetData()

        Try
            Dim objItem As Dbi.WinControl.Schedule.dbiScheduleItem
            Dim objParent As Dbi.WinControl.Schedule.dbiScheduleItem
            Dim oSQL As New SQLServerClass
            Dim oSQL2 As New SQLServerClass
            Dim dBasla As DateTime = Now.Date
            Dim dBitis As DateTime = DateAdd(DateInterval.Day, 365, Now)
            Dim cMessage As String = ""
            Dim cToolTip As String = ""
            Dim cKeyColumn As String = "firma"

            DbiSchedule1.Columns.Clear()
            DbiSchedule1.Items.Clear()
            DbiSchedule1.SuspendLayout()

            oSQL.OpenConn()
            oSQL2.OpenConn()

            AddColumn("Atölye", 100)
            AddColumn("İmalatçı", 100)
            AddColumn("Model", 100)
            AddColumn("Müşteri", 100)
            AddColumn("Sipariş No", 100)
            AddColumn("İşemri No", 100)
            AddColumn("Sipariş Geliş", 70)
            AddColumn("İstenen", 50)
            AddColumn("Üretilen", 50)
            AddColumn("Sevkiyat Planı", 500)

            Select Case cGruplama
                Case "Müşteri"
                    cKeyColumn = "musteri"
                Case "Model"
                    cKeyColumn = "modelno"
                Case "İmalatçı"
                    cKeyColumn = "imalatci"
                Case Else
                    cKeyColumn = "firma"
            End Select

            oSQL2.cSQLQuery = "select " + cKeyColumn + ", istenenadet = sum(coalesce(istenenadet,0)), uretilenadet = sum(coalesce(uretilenadet,0)) " +
                            " from uretimisemriplanlama with (NOLOCK)  " +
                            " where baslama_tar is not null " +
                            " and baslama_tar <> '01.01.1950' " +
                            " and bitis_tar is not null " +
                            " and bitis_tar <> '01.01.1950' " +
                            " and bitis_tar >= baslama_tar " +
                            " and " + cKeyColumn + " is not null " +
                            " and " + cKeyColumn + " <> '' " +
                            GetCBEdit1() +
                            GetCBEdit2() +
                            GetCBEdit3() +
                            cFilter

            oSQL2.cSQLQuery = oSQL2.cSQLQuery +
                            " group by " + cKeyColumn +
                            " order by " + cKeyColumn

            oSQL2.GetSQLReader()

            Do While oSQL2.oReader.Read

                objParent = New Dbi.WinControl.Schedule.dbiScheduleItem()
                objParent.SetCellText(0, oSQL2.SQLReadString(cKeyColumn))
                objParent.SetCellText(7, oSQL2.SQLReadDouble("istenenadet").ToString(G_NumberFormat))
                objParent.SetCellText(8, oSQL2.SQLReadDouble("uretilenadet").ToString(G_NumberFormat))

                oSQL.cSQLQuery = "select uretimtakipno, isemrino, modelno, departman, firma, " +
                                " baslama_tar, bitis_tar, istenenadet, uretilenadet, siparisgelis, " +
                                " sevkplani, musteri, imalatci " +
                                " from uretimisemriplanlama with (NOLOCK)  " +
                                " where " + cKeyColumn + " = '" + oSQL2.SQLReadString(cKeyColumn) + "' " +
                                " and baslama_tar Is Not null " +
                                " and baslama_tar <> '01.01.1950' " +
                                " and bitis_tar is not null " +
                                " and bitis_tar <> '01.01.1950' " +
                                " and bitis_tar >= baslama_tar " +
                                GetCBEdit1() +
                                GetCBEdit2() +
                                GetCBEdit3() +
                                cFilter

                oSQL.GetSQLReader()

                Do While oSQL.oReader.Read

                    objItem = New Dbi.WinControl.Schedule.dbiScheduleItem()

                    objItem.SetCellText(0, oSQL.SQLReadString("firma"))
                    objItem.SetCellText(1, oSQL.SQLReadString("imalatci"))
                    objItem.SetCellText(2, oSQL.SQLReadString("modelno"))
                    objItem.SetCellText(3, oSQL.SQLReadString("musteri"))
                    objItem.SetCellText(4, oSQL.SQLReadString("uretimtakipno"))
                    objItem.SetCellText(5, oSQL.SQLReadString("isemrino"))
                    objItem.SetCellText(6, oSQL.SQLReadDate("siparisgelis").ToString("dd.MM.yyyy"))
                    objItem.SetCellText(7, oSQL.SQLReadDouble("istenenadet").ToString(G_NumberFormat))
                    objItem.SetCellText(8, oSQL.SQLReadDouble("uretilenadet").ToString(G_NumberFormat))
                    objItem.SetCellText(9, oSQL.SQLReadString("sevkplani"))

                    If oSQL.SQLReadDouble("uretilenadet") = 0 Then
                        cMessage = " Istenen:" + oSQL.SQLReadDouble("istenenadet").ToString(G_NumberFormat)
                    Else
                        cMessage = " Istenen:" + oSQL.SQLReadDouble("istenenadet").ToString(G_NumberFormat) + " Üretilen:" + oSQL.SQLReadDouble("uretilenadet").ToString(G_NumberFormat)
                    End If

                    ' detay gantt
                    Dim itemTimeBar As New Dbi.WinControl.Schedule.dbiTimeBarItem(oSQL.SQLReadDate("baslama_tar"), oSQL.SQLReadDate("bitis_tar"))

                    itemTimeBar.Text = cMessage
                    itemTimeBar.Body = oSQL.SQLReadString("isemrino")
                    itemTimeBar.Font = New System.Drawing.Font("Verdana", 8.25) 'Sets the font for the primary text
                    itemTimeBar.SubTextFont = New System.Drawing.Font("Verdana", 7, System.Drawing.FontStyle.Italic) 'Sets the font for the body text

                    cToolTip = "İşemri No : " + oSQL.SQLReadString("isemrino") + vbCrLf +
                                "İmalatçı : " + oSQL.SQLReadString("imalatci") + vbCrLf +
                                "Müşteri : " + oSQL.SQLReadString("musteri") + vbCrLf +
                                "Model : " + oSQL.SQLReadString("modelno") + vbCrLf +
                                "Atölye : " + oSQL.SQLReadString("firma") + vbCrLf +
                                "Başlama : " + oSQL.SQLReadDate("baslama_tar").ToString("dd.MM.yyyy") + vbCrLf +
                                "Bitirme : " + oSQL.SQLReadDate("bitis_tar").ToString("dd.MM.yyyy") + vbCrLf +
                                "İstenen : " + oSQL.SQLReadDouble("istenenadet").ToString(G_NumberFormat) + vbCrLf +
                                "Üretilen : " + oSQL.SQLReadDouble("uretilenadet").ToString(G_NumberFormat) + vbCrLf +
                                "Sevk.Plan:" + oSQL.SQLReadString("sevkplani")

                    itemTimeBar.Cargo = cToolTip

                    If oSQL.SQLReadDouble("uretilenadet") >= oSQL.SQLReadDouble("istenenadet") Then
                        itemTimeBar.BackColor = System.Drawing.Color.LightGreen
                    ElseIf oSQL.SQLReadDouble("uretilenadet") > 0 Then
                        itemTimeBar.BackColor = System.Drawing.Color.Yellow
                    Else
                        itemTimeBar.BackColor = System.Drawing.Color.LightPink
                    End If

                    objItem.TimeBars.Add(itemTimeBar)

                    ' özet gantt
                    Dim itemTimeBar2 As New Dbi.WinControl.Schedule.dbiTimeBarItem(oSQL.SQLReadDate("baslama_tar"), oSQL.SQLReadDate("bitis_tar"))

                    itemTimeBar2.Text = "Model:" + oSQL.SQLReadString("modelno") + " İstenen:" + oSQL.SQLReadDouble("istenenadet").ToString(G_NumberFormat)
                    itemTimeBar2.Body = oSQL.SQLReadString("isemrino")
                    itemTimeBar2.Font = New System.Drawing.Font("Verdana", 10, System.Drawing.FontStyle.Bold) 'Sets the font for the primary text
                    itemTimeBar2.SubTextFont = New System.Drawing.Font("Verdana", 7, System.Drawing.FontStyle.Italic) 'Sets the font for the body text

                    itemTimeBar2.Cargo = cToolTip
                    itemTimeBar2.BackColor = System.Drawing.Color.AntiqueWhite
                    objParent.TimeBars.Add(itemTimeBar2)

                    objParent.Items.Add(objItem)
                Loop
                oSQL.oReader.Close()

                DbiSchedule1.Items.Add(objParent)

            Loop
            oSQL2.oReader.Close()

            DbiSchedule1.ResumeLayout()

            DbiSchedule1.ScrollToDateTime(Today, 0)

            oSQL.CloseConn()
            oSQL2.CloseConn()

        Catch ex As Exception
            ErrDisp("GetData : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub AddColumn(cColumnName As String, Optional nWidth As Integer = 100)

        Try
            Dim objColumn As Dbi.WinControl.dbiColumnItem

            objColumn = New Dbi.WinControl.dbiColumnItem(cColumnName, nWidth)
            objColumn.Sortable = True
            DbiSchedule1.Columns.Add(objColumn)  'Add 

        Catch ex As Exception
            ErrDisp("AddColumn : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub DbiSchedule1_ValuePoint(ByVal sender As Object, ByVal e As Dbi.WinControl.Schedule.ValuePointEventArgs) Handles DbiSchedule1.ValuePoint

        Try
            If Me.DbiSchedule1.Schedules(0).TimeType = Dbi.WinControl.Schedule.enumTimeType.Weeks Then
                Select Case e.Value
                    Case 1 : e.Text = "Pz " + DateValue(e.CurrentTime).Day.ToString
                    Case 2 : e.Text = "Sa " + DateValue(e.CurrentTime).Day.ToString
                    Case 3 : e.Text = "Ça " + DateValue(e.CurrentTime).Day.ToString
                    Case 4 : e.Text = "Pe " + DateValue(e.CurrentTime).Day.ToString
                    Case 5 : e.Text = "Cu " + DateValue(e.CurrentTime).Day.ToString
                    Case 6 : e.Text = "Ct " + DateValue(e.CurrentTime).Day.ToString
                    Case 7 : e.Text = "Pa " + DateValue(e.CurrentTime).Day.ToString
                End Select
            End If

        Catch ex As Exception
            ErrDisp("DbiSchedule1_ValuePoint : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' undo
        On Error Resume Next
        DbiSchedule1.Undo()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' redo
        On Error Resume Next
        DbiSchedule1.Redo()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ' sola çek
        On Error Resume Next

        Dim oCurrentTimeBar As Dbi.WinControl.Schedule.dbiTimeBarItem

        For Each oCurrentTimeBar In DbiSchedule1.SelectedTimeBars
            DbiSchedule1.BarToBack(oCurrentTimeBar)
        Next
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ' sağa çek
        On Error Resume Next

        Dim oCurrentTimeBar As Dbi.WinControl.Schedule.dbiTimeBarItem

        For Each oCurrentTimeBar In DbiSchedule1.SelectedTimeBars
            DbiSchedule1.BarToFront(oCurrentTimeBar)
        Next
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        ' OK
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        ' çık
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click

        Try
            Dim oResult As System.Windows.Forms.DialogResult

            PageSetupDialog1.PageSettings = New System.Drawing.Printing.PageSettings()
            PageSetupDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
            PageSetupDialog1.ShowNetwork = False
            oResult = PageSetupDialog1.ShowDialog()

            If oResult = System.Windows.Forms.DialogResult.OK Then
                DbiSchedule1.PrintPreview(PageSetupDialog1.PrinterSettings)
            End If

            'oPrintSettings.DefaultPageSettings.Landscape = True
            'oPrintSettings.DefaultPageSettings.PaperSize = New System.Drawing.Printing.PaperSize("Custom", 2000, 10000)


        Catch ex As Exception
            ErrDisp("Button3_Click : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ' refresh
        GetData()
    End Sub

    Private Sub DbiSchedule1_ItemMouseOver(sender As Object, e As ItemMouseOverEventArgs) Handles DbiSchedule1.ItemMouseOver
        'If Not IsNothing(e) Then
        '    If Not IsNothing(e.TimeBarItem) Then
        '        If Not IsNothing(e.TimeBarItem.Cargo) Then
        '            'MsgBox("İşemri No " + e.TimeBarItem.Cargo.ToString())
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub DbiSchedule1_BeforeToolTips(sender As Object, e As BeforeToolTipsEventArgs) Handles DbiSchedule1.BeforeToolTips

        On Error Resume Next

        Dim nBarIndex As Integer = 0
        Dim oTimeBar As Dbi.WinControl.Schedule.dbiTimeBarItem

        If (e.TipsType = Dbi.WinControl.Schedule.enumTipsType.Over) Then
            nBarIndex = e.BarIndex
            oTimeBar = e.ScheduleItem.TimeBars.Item(nBarIndex)

            e.Text = oTimeBar.Cargo
        End If
    End Sub

    Private Sub DbiSchedule1_TimeBarDoubleClick(sender As Object, e As TimeBarDoubleClickEventArgs) Handles DbiSchedule1.TimeBarDoubleClick

        Try
            Dim cIsemriNo As String = ""
            Dim oUretimisemriEdit As UretimisemriEdit

            If Not IsNothing(e) Then
                If Not IsNothing(e.TimeBarItem) Then
                    If Not IsNothing(e.TimeBarItem.Cargo) Then
                        cIsemriNo = e.TimeBarItem.Body.ToString()
                        If cIsemriNo.Trim <> "" Then
                            oUretimisemriEdit = New UretimisemriEdit
                            If oUretimisemriEdit.init(cIsemriNo) Then
                                GetData()
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrDisp("DbiSchedule1_TimeBarDoubleClick : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ' düzelt
        Try
            Dim oCurrentTimeBar As Dbi.WinControl.Schedule.dbiTimeBarItem
            Dim cIsemriNo As String = ""
            Dim oUretimisemriEdit As UretimisemriEdit

            For Each oCurrentTimeBar In DbiSchedule1.SelectedTimeBars
                cIsemriNo = oCurrentTimeBar.Body.ToString()
                If cIsemriNo.Trim <> "" Then
                    oUretimisemriEdit = New UretimisemriEdit
                    If oUretimisemriEdit.init(cIsemriNo) Then
                        GetData()
                    End If
                End If
                Exit For
            Next

            If cIsemriNo.Trim = "" Then
                MsgBox("Dikkat : İşemri (Gantt Bar) seçiniz")
            End If

        Catch ex As Exception
            ErrDisp("Button9_Click : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            lLoading = True
            cGruplama = "Atölye"
            FillCombos()
            GetData()
            lLoading = False

        Catch ex As Exception
            ErrDisp("Button10_Click : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub DbiSchedule1_AfterTimeBarMoved(sender As Object, e As AfterTimeBarMovedEventArgs) Handles DbiSchedule1.AfterTimeBarMoved
        ' after time bar moved
        Try
            Dim oSQL As SQLServerClass
            Dim cIsemriNo As String = ""

            If Not IsNothing(e.TimeBarItem) Then
                cIsemriNo = e.TimeBarItem.Body.ToString()
                If cIsemriNo.Trim <> "" Then

                    oSQL = New SQLServerClass

                    oSQL.OpenConn()

                    oSQL.cSQLQuery = "set dateformat dmy " +
                                    " update uretimisdetayi " +
                                    " set baslama_tar = '" + SQLWriteDate(e.TimeBarItem.Start) + "' , " +
                                    " bitis_tar = '" + SQLWriteDate(e.TimeBarItem.End) + "' " +
                                    " where isemrino = '" + cIsemriNo.Trim + "' "

                    oSQL.SQLExecute()

                    oSQL.CloseConn()

                    GetData()

                End If
            End If

        Catch ex As Exception
            ErrDisp("DbiSchedule1_AfterTimeBarMoved : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedValueChanged
        If lLoading Then Exit Sub
        cGruplama = ComboBox1.SelectedItem.ToString
        GetData()
    End Sub

    Private Sub CheckedComboBoxEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles CheckedComboBoxEdit1.EditValueChanged
        If lLoading Then Exit Sub
        GetData()
    End Sub

    Private Sub CheckedComboBoxEdit2_EditValueChanged(sender As Object, e As EventArgs) Handles CheckedComboBoxEdit2.EditValueChanged
        If lLoading Then Exit Sub
        GetData()
    End Sub

    Private Sub CheckedComboBoxEdit3_EditValueChanged(sender As Object, e As EventArgs) Handles CheckedComboBoxEdit3.EditValueChanged
        If lLoading Then Exit Sub
        GetData()
    End Sub

    Private Function GetCBEdit1() As String

        GetCBEdit1 = ""

        If lLoading Then Exit Function

        Try
            Dim cFilter As String = ""

            If IsNothing(CheckedComboBoxEdit1.EditValue) Then Exit Function
            cFilter = CheckedComboBoxEdit1.EditValue
            If cFilter.Trim = "" Then Exit Function
            cFilter = Replace(cFilter, ", ", "','")

            GetCBEdit1 = " and musteri in ('" + cFilter.Trim + "') "

        Catch ex As Exception
            ErrDisp("GetCBEdit1 : " + ex.Message, Me.Name,,, ex)
        End Try
    End Function

    Private Function GetCBEdit2() As String

        GetCBEdit2 = ""

        If lLoading Then Exit Function

        Try
            Dim cFilter As String = ""

            If IsNothing(CheckedComboBoxEdit2.EditValue) Then Exit Function
            cFilter = CheckedComboBoxEdit2.EditValue
            If cFilter.Trim = "" Then Exit Function
            cFilter = Replace(cFilter, ", ", "','")

            GetCBEdit2 = " and firma in ('" + cFilter.Trim + "') "

        Catch ex As Exception
            ErrDisp("GetCBEdit2 : " + ex.Message, Me.Name,,, ex)
        End Try
    End Function

    Private Function GetCBEdit3() As String

        GetCBEdit3 = ""

        If lLoading Then Exit Function

        Try
            Dim cFilter As String = ""

            If IsNothing(CheckedComboBoxEdit3.EditValue) Then Exit Function
            cFilter = CheckedComboBoxEdit3.EditValue
            If cFilter.Trim = "" Then Exit Function
            cFilter = Replace(cFilter, ", ", "','")

            GetCBEdit3 = " and imalatci in ('" + cFilter.Trim + "') "

        Catch ex As Exception
            ErrDisp("GetCBEdit3 : " + ex.Message, Me.Name,,, ex)
        End Try
    End Function

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        ' to jpg
        Try
            Dim cFileName As String = GetTempFile("jpg",, "c:\wintex")

            Me.DbiSchedule1.ImageType = Dbi.WinControl.Schedule.enumImageType.Jpeg
            Me.DbiSchedule1.CreateImage(cFileName, -1, -1)
            System.Diagnostics.Process.Start(cFileName)

        Catch ex As Exception
            ErrDisp("Button11_Click : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            If DbiSchedule1.MapVisible Then
                DbiSchedule1.MapVisible = False
            Else
                DbiSchedule1.MapVisible = True
            End If
            DbiSchedule1.MapHeight = CInt(Me.Height / 3)

        Catch ex As Exception
            ErrDisp("Button12_Click : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        ' büyüt
        Try
            Dim nTD As Integer = DbiSchedule1.Schedules.Item(0).TimeDistance

            DbiSchedule1.Schedules.Item(0).TimeDistance = nTD + 10
            DbiSchedule1.Schedules.Item(0).Refresh()

        Catch ex As Exception
            ErrDisp("Button13_Click : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        ' küçült
        Try
            Dim nTD As Integer = DbiSchedule1.Schedules.Item(0).TimeDistance

            If nTD > 10 Then
                DbiSchedule1.Schedules.Item(0).TimeDistance = nTD - 10
                DbiSchedule1.Schedules.Item(0).Refresh()
            End If
        Catch ex As Exception
            ErrDisp("Button14_Click : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub
End Class