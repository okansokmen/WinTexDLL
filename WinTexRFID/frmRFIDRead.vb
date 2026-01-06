Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Threading
Imports System.Collections
Imports System.Net
Imports System.Net.Sockets

Imports BLEDeviceAPI

Imports UHFAPP.utils
Imports UHFAPP.excel
Imports UHFAPP.RFID
Imports UHFAPP.custom.authenticate
Imports UHFAPP.custom.m775Authenticate
Imports UHFAPP.barcode
Imports UHFAPP.Entity

Imports WinForm_Test

Namespace UHFAPP

    Partial Public Class frmRFIDRead

        Inherits Form

        Public uhf As UHFAPI = Nothing

        Dim lComplete As Boolean = False
        Dim beginTime As Long = 0
        Dim lLoading As Boolean = False
        Dim nKesimTolerans As Double = 0
        Dim nOncekiAdet As Double = 0

        Public Sub New()
            InitializeComponent()
            uhf = UHFAPI.getInstance()
        End Sub

        Public Sub init()
            Try
                Me.Text = "WinTex RFID Okuyucusu"
                Me.ShowDialog()

            Catch ex As Exception
                ErrDisp("init", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub GMsg(cMessage As String)

            Try
                If cMessage.Trim = "" Then Exit Sub

                TextBox1.Text = cMessage.Trim + vbCrLf + TextBox1.Text
                TextBox1.Refresh()

                System.Windows.Forms.Application.DoEvents()
                Thread.Sleep(100)

            Catch ex As Exception
                ErrDisp("GMsg", Me.Name,,, ex)
            End Try
        End Sub

        Private Function openUsbPort() As Boolean

            Dim lResult As Boolean = False

            openUsbPort = False

            Try
                Do While True
                    lResult = uhf.OpenUsb()

                    If lResult Then

                        'GMsg("Hardware version : " + uhf.GetHardwareVersion().Replace(vbNullChar, ""))
                        'GMsg("Firmware version : " + uhf.GetSoftwareVersion().Replace(vbNullChar, ""))
                        'GMsg("Mainboard version : " + uhf.GetSTM32Version().Replace(vbNullChar, ""))
                        'GMsg("API version : " + uhf.GetAPIVersion().Replace(vbNullChar, ""))
                        GMsg("UHF Device ID : " + uhf.GetUHFGetDeviceID().ToString)

                        If uhf.GetUHFGetDeviceID() <> -1 Then
                            openUsbPort = True
                            Exit Do
                        End If
                    End If

                    If lComplete Then
                        Exit Do
                    End If

                    System.Windows.Forms.Application.DoEvents()
                    Thread.Sleep(100)
                Loop

            Catch ex As Exception
                ErrDisp("openUsbPort",,,, ex)
            End Try
        End Function

        Private Sub frmRFIDRead_Load(sender As Object, e As EventArgs) Handles Me.Load
            Try
                lComplete = False
                lLoading = True
                Sifirla()
                LoadCombos()
                lLoading = False
                Me.WindowState = FormWindowState.Maximized

            Catch ex As Exception
                ErrDisp("frmRFIDRead_Load", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub StopEPC()

            Try
                Dim result As Boolean = uhf.StopGet()
                If Not result Then
                    GMsg("Cannot stop EPC")
                End If
                Thread.Sleep(100)

            Catch ex As Exception
                ErrDisp("StopEPC", Me.Name,,, ex)
            End Try
        End Sub

        Private Function ReadEPC() As Boolean

            ReadEPC = False

            Try
                Dim lFound As Boolean = False
                Dim nCnt As Integer = 0
                Dim info As UHFTAGInfo
                Dim nSonOkuma As Integer = 0
                Dim oSQL As New SQLServerClass
                Dim cBarcode As String = ""

                oSQL.OpenConn()

                For nCnt = 0 To 1000

                    GMsg("RFID okuması yapılıyor : " + nCnt.ToString)

                    info = uhf.uhfGetReceived()

                    If info IsNot Nothing Then

                        cBarcode = info.Epc.ToString.Trim

                        oSQL.cSQLQuery = "select top 1 barcode " +
                                        " from stokbarkod with (NOLOCK) " +
                                        " where siparisno = '" + ComboBox1.Text.Trim + "' " +
                                        " and stokno = '" + ComboBox2.Text.Trim + "' " +
                                        " and renk = '" + ComboBox3.Text.Trim + "' " +
                                        " and beden = '" + ComboBox5.Text.Trim + "' " +
                                        " and barcode = '" + cBarcode + "' "

                        If oSQL.CheckExists() Then
                            GMsg("RFID daha önce aynı sip/model/renk/bedene tanımlanmış : " + cBarcode)
                        Else
                            lFound = False
                            For Each oItem In ListBox1.Items
                                If oItem.ToString = cBarcode Then
                                    lFound = True
                                    Exit For
                                End If
                            Next
                            If Not lFound Then
                                GMsg("RFID okundu : " + cBarcode)
                                ListBox1.Items.Add(cBarcode)
                                ListBox1.Refresh()
                                nSonOkuma = nCnt
                                ReadEPC = True
                            End If
                        End If
                    Else
                        GMsg("RFID okunamadı")
                    End If

                    If lComplete Then
                        Exit For
                    End If

                    If nCnt > 20 And Not ReadEPC Then
                        ' ilk 20 turda okuma olmadıysa sorun olabilir
                        Exit For
                    End If

                    If nCnt - nSonOkuma > 20 Then
                        ' son 20 turda yeni bir etiket okutulmadıysa 
                        Exit For
                    End If

                    TextBox2.Text = ListBox1.Items.Count.ToString

                    System.Windows.Forms.Application.DoEvents()
                    Thread.Sleep(100)
                Next

                oSQL.CloseConn()

                If ReadEPC Then
                    TextBox2.Text = ListBox1.Items.Count.ToString
                    Calc()
                End If

            Catch ex As Exception
                ErrDisp("ReadEPC", Me.Name,,, ex)
            End Try
        End Function

        Friend WithEvents Panel1 As Panel
        Friend WithEvents Button1 As Button
        Friend WithEvents Button4 As Button
        Friend WithEvents ListBox1 As ListBox
        Friend WithEvents TextBox1 As TextBox

        Private Sub InitializeComponent()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.Button3 = New System.Windows.Forms.Button()
            Me.Button2 = New System.Windows.Forms.Button()
            Me.Button4 = New System.Windows.Forms.Button()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.ListBox1 = New System.Windows.Forms.ListBox()
            Me.TextBox1 = New System.Windows.Forms.TextBox()
            Me.Panel2 = New System.Windows.Forms.Panel()
            Me.TextBox5 = New System.Windows.Forms.TextBox()
            Me.Label9 = New System.Windows.Forms.Label()
            Me.ComboBox5 = New System.Windows.Forms.ComboBox()
            Me.ComboBox4 = New System.Windows.Forms.ComboBox()
            Me.Label8 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.ComboBox3 = New System.Windows.Forms.ComboBox()
            Me.ComboBox2 = New System.Windows.Forms.ComboBox()
            Me.ComboBox1 = New System.Windows.Forms.ComboBox()
            Me.TextBox4 = New System.Windows.Forms.TextBox()
            Me.TextBox3 = New System.Windows.Forms.TextBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.TextBox2 = New System.Windows.Forms.TextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label10 = New System.Windows.Forms.Label()
            Me.TextBox6 = New System.Windows.Forms.TextBox()
            Me.Button5 = New System.Windows.Forms.Button()
            Me.Label11 = New System.Windows.Forms.Label()
            Me.TextBox7 = New System.Windows.Forms.TextBox()
            Me.Panel1.SuspendLayout()
            Me.Panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.BackColor = System.Drawing.Color.Transparent
            Me.Panel1.Controls.Add(Me.Button5)
            Me.Panel1.Controls.Add(Me.Button3)
            Me.Panel1.Controls.Add(Me.Button2)
            Me.Panel1.Controls.Add(Me.Button4)
            Me.Panel1.Controls.Add(Me.Button1)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel1.Location = New System.Drawing.Point(0, 412)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(799, 46)
            Me.Panel1.TabIndex = 4
            '
            'Button3
            '
            Me.Button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.Button3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            Me.Button3.ForeColor = System.Drawing.Color.MediumBlue
            Me.Button3.Location = New System.Drawing.Point(340, 11)
            Me.Button3.Name = "Button3"
            Me.Button3.Size = New System.Drawing.Size(119, 23)
            Me.Button3.TabIndex = 7
            Me.Button3.Text = "Kaydet"
            Me.Button3.UseVisualStyleBackColor = True
            '
            'Button2
            '
            Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.Button2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            Me.Button2.ForeColor = System.Drawing.Color.Red
            Me.Button2.Location = New System.Drawing.Point(590, 11)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(119, 23)
            Me.Button2.TabIndex = 6
            Me.Button2.Text = "Çıkış"
            Me.Button2.UseVisualStyleBackColor = True
            '
            'Button4
            '
            Me.Button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.Button4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            Me.Button4.ForeColor = System.Drawing.Color.MediumBlue
            Me.Button4.Location = New System.Drawing.Point(215, 11)
            Me.Button4.Name = "Button4"
            Me.Button4.Size = New System.Drawing.Size(119, 23)
            Me.Button4.TabIndex = 5
            Me.Button4.Text = "Okumayı Durdur"
            Me.Button4.UseVisualStyleBackColor = True
            '
            'Button1
            '
            Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.Button1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            Me.Button1.ForeColor = System.Drawing.Color.ForestGreen
            Me.Button1.Location = New System.Drawing.Point(90, 11)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(119, 23)
            Me.Button1.TabIndex = 2
            Me.Button1.Text = "RFID Oku"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'ListBox1
            '
            Me.ListBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ListBox1.FormattingEnabled = True
            Me.ListBox1.Location = New System.Drawing.Point(12, 12)
            Me.ListBox1.Name = "ListBox1"
            Me.ListBox1.Size = New System.Drawing.Size(344, 381)
            Me.ListBox1.TabIndex = 5
            '
            'TextBox1
            '
            Me.TextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TextBox1.Location = New System.Drawing.Point(363, 314)
            Me.TextBox1.Multiline = True
            Me.TextBox1.Name = "TextBox1"
            Me.TextBox1.Size = New System.Drawing.Size(424, 79)
            Me.TextBox1.TabIndex = 6
            '
            'Panel2
            '
            Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Panel2.Controls.Add(Me.TextBox7)
            Me.Panel2.Controls.Add(Me.Label11)
            Me.Panel2.Controls.Add(Me.TextBox6)
            Me.Panel2.Controls.Add(Me.Label10)
            Me.Panel2.Controls.Add(Me.TextBox5)
            Me.Panel2.Controls.Add(Me.Label9)
            Me.Panel2.Controls.Add(Me.ComboBox5)
            Me.Panel2.Controls.Add(Me.ComboBox4)
            Me.Panel2.Controls.Add(Me.Label8)
            Me.Panel2.Controls.Add(Me.Label3)
            Me.Panel2.Controls.Add(Me.ComboBox3)
            Me.Panel2.Controls.Add(Me.ComboBox2)
            Me.Panel2.Controls.Add(Me.ComboBox1)
            Me.Panel2.Controls.Add(Me.TextBox4)
            Me.Panel2.Controls.Add(Me.TextBox3)
            Me.Panel2.Controls.Add(Me.Label7)
            Me.Panel2.Controls.Add(Me.TextBox2)
            Me.Panel2.Controls.Add(Me.Label1)
            Me.Panel2.Controls.Add(Me.Label6)
            Me.Panel2.Controls.Add(Me.Label5)
            Me.Panel2.Controls.Add(Me.Label4)
            Me.Panel2.Controls.Add(Me.Label2)
            Me.Panel2.Location = New System.Drawing.Point(363, 12)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(425, 296)
            Me.Panel2.TabIndex = 9
            '
            'TextBox5
            '
            Me.TextBox5.Location = New System.Drawing.Point(256, 143)
            Me.TextBox5.Name = "TextBox5"
            Me.TextBox5.Size = New System.Drawing.Size(59, 20)
            Me.TextBox5.TabIndex = 26
            '
            'Label9
            '
            Me.Label9.AutoSize = True
            Me.Label9.Location = New System.Drawing.Point(204, 146)
            Me.Label9.Name = "Label9"
            Me.Label9.Size = New System.Drawing.Size(46, 13)
            Me.Label9.TabIndex = 25
            Me.Label9.Text = "Kesim %"
            '
            'ComboBox5
            '
            Me.ComboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboBox5.FormattingEnabled = True
            Me.ComboBox5.Location = New System.Drawing.Point(139, 116)
            Me.ComboBox5.Name = "ComboBox5"
            Me.ComboBox5.Size = New System.Drawing.Size(235, 21)
            Me.ComboBox5.TabIndex = 24
            '
            'ComboBox4
            '
            Me.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboBox4.FormattingEnabled = True
            Me.ComboBox4.Location = New System.Drawing.Point(139, 89)
            Me.ComboBox4.Name = "ComboBox4"
            Me.ComboBox4.Size = New System.Drawing.Size(235, 21)
            Me.ComboBox4.TabIndex = 23
            '
            'Label8
            '
            Me.Label8.AutoSize = True
            Me.Label8.Location = New System.Drawing.Point(3, 119)
            Me.Label8.Name = "Label8"
            Me.Label8.Size = New System.Drawing.Size(38, 13)
            Me.Label8.TabIndex = 22
            Me.Label8.Text = "Beden"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(3, 92)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(59, 13)
            Me.Label3.TabIndex = 21
            Me.Label3.Text = "Beden Seti"
            '
            'ComboBox3
            '
            Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboBox3.FormattingEnabled = True
            Me.ComboBox3.Location = New System.Drawing.Point(139, 62)
            Me.ComboBox3.Name = "ComboBox3"
            Me.ComboBox3.Size = New System.Drawing.Size(235, 21)
            Me.ComboBox3.TabIndex = 20
            '
            'ComboBox2
            '
            Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboBox2.FormattingEnabled = True
            Me.ComboBox2.Location = New System.Drawing.Point(139, 35)
            Me.ComboBox2.Name = "ComboBox2"
            Me.ComboBox2.Size = New System.Drawing.Size(235, 21)
            Me.ComboBox2.TabIndex = 19
            '
            'ComboBox1
            '
            Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboBox1.FormattingEnabled = True
            Me.ComboBox1.Location = New System.Drawing.Point(139, 8)
            Me.ComboBox1.Name = "ComboBox1"
            Me.ComboBox1.Size = New System.Drawing.Size(235, 21)
            Me.ComboBox1.TabIndex = 18
            '
            'TextBox4
            '
            Me.TextBox4.Location = New System.Drawing.Point(139, 143)
            Me.TextBox4.Name = "TextBox4"
            Me.TextBox4.Size = New System.Drawing.Size(59, 20)
            Me.TextBox4.TabIndex = 17
            '
            'TextBox3
            '
            Me.TextBox3.Location = New System.Drawing.Point(139, 254)
            Me.TextBox3.Name = "TextBox3"
            Me.TextBox3.Size = New System.Drawing.Size(235, 20)
            Me.TextBox3.TabIndex = 16
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(3, 257)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(120, 13)
            Me.Label7.TabIndex = 15
            Me.Label7.Text = "Okutulacak RFID Adedi"
            '
            'TextBox2
            '
            Me.TextBox2.Location = New System.Drawing.Point(139, 226)
            Me.TextBox2.Name = "TextBox2"
            Me.TextBox2.Size = New System.Drawing.Size(235, 20)
            Me.TextBox2.TabIndex = 14
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(3, 229)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(103, 13)
            Me.Label1.TabIndex = 13
            Me.Label1.Text = "Okunan RFID Adedi"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(3, 146)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(68, 13)
            Me.Label6.TabIndex = 12
            Me.Label6.Text = "Sipariş Adedi"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(3, 65)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(33, 13)
            Me.Label5.TabIndex = 11
            Me.Label5.Text = "Renk"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(3, 38)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(53, 13)
            Me.Label4.TabIndex = 10
            Me.Label4.Text = "Model No"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(3, 11)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(55, 13)
            Me.Label2.TabIndex = 8
            Me.Label2.Text = "Sipariş No"
            '
            'Label10
            '
            Me.Label10.AutoSize = True
            Me.Label10.Location = New System.Drawing.Point(3, 204)
            Me.Label10.Name = "Label10"
            Me.Label10.Size = New System.Drawing.Size(127, 13)
            Me.Label10.TabIndex = 27
            Me.Label10.Text = "Önceden Okunmuş RFID"
            '
            'TextBox6
            '
            Me.TextBox6.Location = New System.Drawing.Point(139, 201)
            Me.TextBox6.Name = "TextBox6"
            Me.TextBox6.Size = New System.Drawing.Size(235, 20)
            Me.TextBox6.TabIndex = 28
            '
            'Button5
            '
            Me.Button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.Button5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
            Me.Button5.ForeColor = System.Drawing.Color.MediumBlue
            Me.Button5.Location = New System.Drawing.Point(465, 11)
            Me.Button5.Name = "Button5"
            Me.Button5.Size = New System.Drawing.Size(119, 23)
            Me.Button5.TabIndex = 8
            Me.Button5.Text = "Sıfırla"
            Me.Button5.UseVisualStyleBackColor = True
            '
            'Label11
            '
            Me.Label11.AutoSize = True
            Me.Label11.Location = New System.Drawing.Point(3, 174)
            Me.Label11.Name = "Label11"
            Me.Label11.Size = New System.Drawing.Size(92, 13)
            Me.Label11.TabIndex = 29
            Me.Label11.Text = "Fireli Sipariş Adedi"
            '
            'TextBox7
            '
            Me.TextBox7.Location = New System.Drawing.Point(139, 171)
            Me.TextBox7.Name = "TextBox7"
            Me.TextBox7.Size = New System.Drawing.Size(235, 20)
            Me.TextBox7.TabIndex = 30
            '
            'frmRFIDRead
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(799, 458)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.TextBox1)
            Me.Controls.Add(Me.ListBox1)
            Me.Controls.Add(Me.Panel1)
            Me.Name = "frmRFIDRead"
            Me.Text = "frmRFIDRead"
            Me.Panel1.ResumeLayout(False)
            Me.Panel2.ResumeLayout(False)
            Me.Panel2.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            ' open usb
            Try
                GMsg("Okuma başladı")

                lComplete = False
                TextBox1.Text = ""

                ListBox1.Items.Clear()
                ListBox1.Refresh()

                uhf.CloseUsb()
                GMsg("USB Port kapatıldı")

                If openUsbPort() Then

                    If uhf.Inventory() Then
                        GMsg("Cihaz bulundu")
                        uhf.UHFSetBuzzer(2)
                        If Not ReadEPC() Then
                            GMsg("RFID bilgileri okunamadı")
                        End If
                    Else
                        GMsg("Cihaz bulunamadı")
                    End If

                    uhf.StopGet()
                    GMsg("Okuma tamamlandı")

                    uhf.CloseUsb()
                    GMsg("USB Port kapatıldı")
                Else
                    GMsg("USB port açılamadı. Cihazı söküp takınız")
                End If

            Catch ex As Exception
                ErrDisp("Button1_Click", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
            ' stop
            Try
                lComplete = True
                StopEPC()
                uhf.CloseUsb()

            Catch ex As Exception
                ErrDisp("Button4_Click", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
            ' çıkış
            Me.Close()
        End Sub

        Friend WithEvents Panel2 As Panel
        Friend WithEvents TextBox4 As TextBox
        Friend WithEvents TextBox3 As TextBox
        Friend WithEvents Label7 As Label
        Friend WithEvents TextBox2 As TextBox
        Friend WithEvents Label1 As Label
        Friend WithEvents Label6 As Label
        Friend WithEvents Label5 As Label
        Friend WithEvents Label4 As Label
        Friend WithEvents Label2 As Label
        Friend WithEvents ComboBox3 As ComboBox
        Friend WithEvents ComboBox2 As ComboBox
        Friend WithEvents ComboBox1 As ComboBox
        Friend WithEvents Button2 As Button
        Friend WithEvents Button3 As Button

        Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
            ' veritabanına yaz
            Try
                Dim oSQL As New SQLServerClass
                Dim cBarkod As String = ""

                oSQL.OpenConn()

                For Each oItem In ListBox1.Items

                    cBarkod = oItem.ToString.Trim

                    If cBarkod <> "" Then

                        GMsg(cBarkod + " veritabanına yazılıyor")

                        oSQL.cSQLQuery = "delete stokbarkod where barcode = '" + cBarkod + "' "
                        oSQL.SQLExecute()

                        oSQL.cSQLQuery = "insert stokbarkod (siparisno, stokno, renk, bedenseti, beden, barcode, adet) " +
                                        " values ('" + ComboBox1.Text.Trim + "' , " +
                                        " '" + ComboBox2.Text.Trim + "' , " +
                                        " '" + ComboBox3.Text.Trim + "' , " +
                                        " '" + ComboBox4.Text.Trim + "' , " +
                                        " '" + ComboBox5.Text.Trim + "' , " +
                                        " '" + cBarkod.Trim + "' , " +
                                        " 1 ) "
                        oSQL.SQLExecute()
                    End If
                Next

                oSQL.CloseConn()

                GMsg("Barkodlar veritabanına yazıldı")

            Catch ex As Exception
                ErrDisp("Button3_Click", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub LoadCombos()

            Try
                Dim oSQL As SQLServerClass

                ComboBox1.Items.Clear()

                oSQL = New SQLServerClass
                oSQL.OpenConn()

                oSQL.cSQLQuery = "select distinct kullanicisipno " +
                                " from siparis with (NOLOCK) " +
                                " where kullanicisipno Is Not null " +
                                " and kullanicisipno <> '' " +
                                " and (dosyakapandi is null or dosyakapandi = '' or dosyakapandi = 'H') " +
                                " order by kullanicisipno "

                oSQL.GetSQLReader()

                Do While oSQL.oReader.Read
                    ComboBox1.Items.Add(oSQL.SQLReadString("kullanicisipno"))
                Loop
                oSQL.oReader.Close()

                oSQL.CloseConn()

                Calc()

            Catch ex As Exception
                ErrDisp("LoadCombos", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedValueChanged

            Try
                Dim oSQL As SQLServerClass

                If lLoading Then Exit Sub
                If ComboBox1.Text.Trim = "" Then Exit Sub

                ComboBox2.Items.Clear()

                oSQL = New SQLServerClass
                oSQL.OpenConn()

                oSQL.cSQLQuery = "select distinct modelno " +
                                " from sipmodel with (NOLOCK) " +
                                " where siparisno = '" + ComboBox1.Text.Trim + "' " +
                                " and modelno is not null " +
                                " and modelno <> '' " +
                                " and adet is not null " +
                                " and adet <> 0 " +
                                " order by modelno "

                oSQL.GetSQLReader()

                Do While oSQL.oReader.Read
                    ComboBox2.Items.Add(oSQL.SQLReadString("modelno"))
                Loop
                oSQL.oReader.Close()

                oSQL.CloseConn()

                If ComboBox2.Items.Count > 0 Then
                    ComboBox2.SelectedItem = ComboBox2.Items(0)
                End If

                Calc()

            Catch ex As Exception
                ErrDisp("ComboBox1_SelectedValueChanged", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub ComboBox2_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedValueChanged

            Try
                Dim oSQL As SQLServerClass

                If lLoading Then Exit Sub
                If ComboBox1.Text.Trim = "" Then Exit Sub
                If ComboBox2.Text.Trim = "" Then Exit Sub

                ComboBox3.Items.Clear()

                oSQL = New SQLServerClass
                oSQL.OpenConn()

                oSQL.cSQLQuery = "select distinct renk " +
                                " from sipmodel with (NOLOCK) " +
                                " where siparisno = '" + ComboBox1.Text.Trim + "' " +
                                " and modelno = '" + ComboBox2.Text.Trim + "' " +
                                " and renk is not null " +
                                " and renk <> '' " +
                                " and adet is not null " +
                                " and adet <> 0 " +
                                " order by renk "

                oSQL.GetSQLReader()

                Do While oSQL.oReader.Read
                    ComboBox3.Items.Add(oSQL.SQLReadString("renk"))
                Loop
                oSQL.oReader.Close()

                oSQL.CloseConn()

                If ComboBox3.Items.Count > 0 Then
                    ComboBox3.SelectedItem = ComboBox3.Items(0)
                End If

                Calc()

            Catch ex As Exception
                ErrDisp("ComboBox1_SelectedValueChanged", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub ComboBox3_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedValueChanged

            Try
                Dim oSQL As SQLServerClass

                If lLoading Then Exit Sub
                If ComboBox1.Text.Trim = "" Then Exit Sub
                If ComboBox2.Text.Trim = "" Then Exit Sub
                If ComboBox3.Text.Trim = "" Then Exit Sub

                ComboBox4.Items.Clear()

                oSQL = New SQLServerClass
                oSQL.OpenConn()

                oSQL.cSQLQuery = "select distinct bedenseti " +
                                " from sipmodel with (NOLOCK) " +
                                " where siparisno = '" + ComboBox1.Text.Trim + "' " +
                                " and modelno = '" + ComboBox2.Text.Trim + "' " +
                                " and renk = '" + ComboBox3.Text.Trim + "' " +
                                " and bedenseti is not null " +
                                " and bedenseti <> '' " +
                                " and adet is not null " +
                                " and adet <> 0 " +
                                " order by bedenseti "

                oSQL.GetSQLReader()

                Do While oSQL.oReader.Read
                    ComboBox4.Items.Add(oSQL.SQLReadString("bedenseti"))
                Loop
                oSQL.oReader.Close()

                oSQL.CloseConn()

                If ComboBox4.Items.Count > 0 Then
                    ComboBox4.SelectedItem = ComboBox4.Items(0)
                End If

                Calc()

            Catch ex As Exception
                ErrDisp("ComboBox3_SelectedValueChanged", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub ComboBox4_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedValueChanged

            Try
                Dim oSQL As SQLServerClass

                If lLoading Then Exit Sub
                If ComboBox1.Text.Trim = "" Then Exit Sub
                If ComboBox2.Text.Trim = "" Then Exit Sub
                If ComboBox3.Text.Trim = "" Then Exit Sub
                If ComboBox4.Text.Trim = "" Then Exit Sub

                ComboBox5.Items.Clear()

                oSQL = New SQLServerClass
                oSQL.OpenConn()

                oSQL.cSQLQuery = "select distinct beden " +
                                " from sipmodel with (NOLOCK) " +
                                " where siparisno = '" + ComboBox1.Text.Trim + "' " +
                                " and modelno = '" + ComboBox2.Text.Trim + "' " +
                                " and renk = '" + ComboBox3.Text.Trim + "' " +
                                " and bedenseti = '" + ComboBox4.Text.Trim + "' " +
                                " and beden is not null " +
                                " and beden <> '' " +
                                " and adet is not null " +
                                " and adet <> 0 " +
                                " order by beden "

                oSQL.GetSQLReader()

                Do While oSQL.oReader.Read
                    ComboBox5.Items.Add(oSQL.SQLReadString("beden"))
                Loop
                oSQL.oReader.Close()

                If ComboBox5.Items.Count > 0 Then
                    ComboBox5.SelectedItem = ComboBox5.Items(0)
                End If

                oSQL.CloseConn()

                Calc()

            Catch ex As Exception
                ErrDisp("ComboBox4_SelectedValueChanged", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub ComboBox5_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedValueChanged
            Calc()
        End Sub

        Private Sub Calc()

            Try
                Dim oSQL As SQLServerClass
                Dim nAdet As Double = 0
                Dim nToplamOkunmus As Double = 0
                Dim nFireliSiparisAdedi As Double = 0
                Dim nOkunacakAdet As Double = 0

                TextBox3.Text = "0"
                TextBox4.Text = "0"

                If lLoading Then Exit Sub
                If ComboBox1.Text.Trim = "" Then Exit Sub
                If ComboBox2.Text.Trim = "" Then Exit Sub
                If ComboBox3.Text.Trim = "" Then Exit Sub
                If ComboBox4.Text.Trim = "" Then Exit Sub
                If ComboBox5.Text.Trim = "" Then Exit Sub

                GetBarkodlanmisAdet()
                GetKesimToleransi()

                oSQL = New SQLServerClass
                oSQL.OpenConn()

                oSQL.cSQLQuery = "select adet = sum(coalesce(adet,0)) " +
                                " from sipmodel with (NOLOCK) " +
                                " where siparisno = '" + ComboBox1.Text.Trim + "' " +
                                " and modelno = '" + ComboBox2.Text.Trim + "' " +
                                " and renk = '" + ComboBox3.Text.Trim + "' " +
                                " and bedenseti = '" + ComboBox4.Text.Trim + "' " +
                                " and beden = '" + ComboBox5.Text.Trim + "' " +
                                " and adet is not null " +
                                " and adet <> 0 "

                oSQL.GetSQLReader()

                If oSQL.oReader.Read Then
                    nAdet = oSQL.SQLReadDouble("adet")
                End If
                oSQL.oReader.Close()

                nFireliSiparisAdedi = Math.Round(nAdet * (1 + (nKesimTolerans / 100)), 0)
                nToplamOkunmus = nOncekiAdet + CDbl(ListBox1.Items.Count)
                nOkunacakAdet = nFireliSiparisAdedi - nToplamOkunmus

                ' bu turda okunmuş RFID adedi
                TextBox2.Text = ListBox1.Items.Count.ToString
                ' sipariş adedi
                TextBox4.Text = nAdet.ToString
                ' fireli sipariş adedi 
                TextBox7.Text = nFireliSiparisAdedi.ToString
                ' okunacak adet
                TextBox3.Text = nOkunacakAdet.ToString

                oSQL.CloseConn()

            Catch ex As Exception
                ErrDisp("Calc", Me.Name,,, ex)
            End Try
        End Sub

        Private Sub GetKesimToleransi()

            Dim oSQL As SQLServerClass

            nKesimTolerans = 0
            TextBox5.Text = "0"

            If lLoading Then Exit Sub
            If ComboBox2.Text.Trim = "" Then Exit Sub

            oSQL = New SQLServerClass
            oSQL.OpenConn()

            oSQL.cSQLQuery = "select top 1 uretimtoleransi " +
                            " from modeluretim with (NOLOCK) " +
                            " where modelno = '" + ComboBox2.Text.Trim + "' " +
                            " and departman like 'KES_M' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                nKesimTolerans = oSQL.SQLReadDouble("uretimtoleransi")
            End If
            oSQL.oReader.Close()

            TextBox5.Text = nKesimTolerans.ToString("###.##")

            oSQL.CloseConn()

        End Sub

        Private Sub GetBarkodlanmisAdet()

            Dim oSQL As SQLServerClass

            nOncekiAdet = 0
            TextBox6.Text = "0"

            If lLoading Then Exit Sub
            If ComboBox1.Text.Trim = "" Then Exit Sub
            If ComboBox2.Text.Trim = "" Then Exit Sub
            If ComboBox3.Text.Trim = "" Then Exit Sub
            If ComboBox5.Text.Trim = "" Then Exit Sub

            oSQL = New SQLServerClass
            oSQL.OpenConn()

            oSQL.cSQLQuery = "select adet = sum(coalesce(adet,0)) " +
                            " from stokbarkod with (NOLOCK) " +
                            " where siparisno = '" + ComboBox1.Text.Trim + "' " +
                            " and stokno = '" + ComboBox2.Text.Trim + "' " +
                            " and renk = '" + ComboBox3.Text.Trim + "' " +
                            " and beden = '" + ComboBox5.Text.Trim + "' "

            oSQL.GetSQLReader()

            If oSQL.oReader.Read Then
                nOncekiAdet = oSQL.SQLReadDouble("adet")
            End If
            oSQL.oReader.Close()

            TextBox6.Text = nOncekiAdet.ToString("###")

            oSQL.CloseConn()

        End Sub

        Friend WithEvents ComboBox5 As ComboBox
        Friend WithEvents ComboBox4 As ComboBox
        Friend WithEvents Label8 As Label
        Friend WithEvents Label3 As Label
        Friend WithEvents TextBox5 As TextBox
        Friend WithEvents Label9 As Label
        Friend WithEvents TextBox6 As TextBox
        Friend WithEvents Label10 As Label
        Friend WithEvents Button5 As Button

        Private Sub Sifirla()

            On Error Resume Next

            ListBox1.Items.Clear()

            TextBox1.ReadOnly = True
            TextBox2.ReadOnly = True
            TextBox3.ReadOnly = True
            TextBox4.ReadOnly = True
            TextBox5.ReadOnly = True
            TextBox6.ReadOnly = True
            TextBox7.ReadOnly = True

            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            TextBox7.Text = ""

            ComboBox1.Text = ""

            ComboBox2.Items.Clear()
            ComboBox2.Text = ""

            ComboBox3.Items.Clear()
            ComboBox3.Text = ""

            ComboBox4.Items.Clear()
            ComboBox4.Text = ""

            ComboBox5.Items.Clear()
            ComboBox5.Text = ""

        End Sub

        Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
            ' sıfırla
            Sifirla()
        End Sub

        Friend WithEvents TextBox7 As TextBox
        Friend WithEvents Label11 As Label
    End Class

End Namespace



