Imports System.ComponentModel
Imports Luxand
Imports System.Data.SqlClient

Public Class frmLuxand

    Const cLicence As String = "KbRaUIpYUA72Dqa/fbaF2WyGDBObaJ7QT8UephQ1nMa1qgoz/s21Iu40OLumjCEy3M/KrXRWTkOWtn1tIzFM9H+Wf8FqvEBLqj+g8/2rYUUtksGpX70a8dx23yo8dsq4FZ7RQuAiVnlwvPCj5fJ8g43I8rpFyZkeRxtfPyF8teo="

    Structure oPersonel
        Dim cPersonel As String
        Dim cUserName As String
        Dim cPassword As String
        Dim FaceTemplate() As Byte
    End Structure

    Dim aPersonel() As oPersonel

    Dim cameraHandle As Integer = 0
    Dim frameImage As Image
    Dim ImageHandle As Integer = 0
    Dim FacialFeatures(FSDK.FSDK_FACIAL_FEATURE_COUNT - 1) As FSDK.TPoint
    Dim nMatchingThreshold As Single = 0
    Dim lNeedToClose As Boolean = False
    Dim nMode As Double = 2
    Dim nFAR As Single = 0.8

    ' WinAPI procedure to release HBITMAP handles returned by FSDKCam.GrabFrame
    Declare Auto Function DeleteObject Lib "gdi32.dll" (ByVal hObject As IntPtr) As Boolean

    Public Sub init(Optional ByVal nCase As Integer = 1)
        nMode = nCase
        Me.Text = "WinTex Yüz Tanıma Penceresi / DLL Versiyon : " + HTMain.cWinTexDLLVersion
        Me.Top = 1
        Me.Left = 1
        Me.ShowDialog()
    End Sub

    Private Sub frmLuxand_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim cameralist() As String
            Dim count As Integer = 0
            Dim formatList() As FSDKCam.VideoFormatInfo
            Dim cameraName As String = ""
            Dim cFAR As String = "80"
            Dim ConnYage As SqlConnection

            ConnYage = OpenConn()
            cFAR = GetSysParConnected("yuztanimaorani", ConnYage)
            CloseConn(ConnYage)

            TextBox1.Enabled = False
            TextBox1.Text = ""

            Gl_Personel = ""
            Gl_UserName = ""
            Gl_ActivePass = ""
            GL_PersonelFaceTemplate = ""
            GL_PersonelResim = ""
            GL_Similarity = 0

            If nMode = 1 Then
                Button3.Enabled = True
            Else
                Button3.Enabled = False
            End If

            If (FSDK.ActivateLibrary(cLicence) <> FSDK.FSDKE_OK) Then
                MessageBox.Show("Please run the License Key Wizard", "Error activating FaceSDK")
                Close()
            End If
            FSDK.InitializeLibrary()
            FSDKCam.InitializeCapturing()
            cameralist = Nothing
            FSDKCam.GetCameraList(cameralist, count)

            If (0 = count) Then
                MessageBox.Show("Lütfen bir kamera bağlayınız", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
            End If

            formatList = Nothing
            FSDKCam.GetVideoFormatList(cameralist(0), formatList, count)
            PictureBox1.Width = formatList(0).Width
            PictureBox1.Height = formatList(0).Height
            sender.Width = formatList(0).Width + 36
            sender.Height = formatList(0).Height + 126

            cameraName = cameralist(0)

            TextBox1.Text = cameraName
            TextBox1.Refresh()

            If (FSDKCam.OpenVideoCamera(cameraName, cameraHandle) <> FSDK.FSDKE_OK) Then
                MessageBox.Show("Ilk kameraya (" + cameraName.ToString.Trim + ") bağlanılamıyor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
            End If

            formatList = Nothing
            cameralist = Nothing

            If cFAR.Trim <> "" Then
                If IsNumeric(cFAR) Then
                    nFAR = CSng(cFAR) / 100
                End If
            End If

            FSDK.SetFaceDetectionParameters(False, False, 100)
            'FSDK.GetMatchingThresholdAtFAR(nFAR, nMatchingThreshold)
            nMatchingThreshold = nFAR

            Timer1.Enabled = True

            If nMode = 2 Then
                LoadPersonel()
            End If

        Catch ex As Exception
            ErrDisp("frmLuxand_Load : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub LoadPersonel()
        Try
            Dim cSQL As String = ""
            Dim ConnYage As SqlConnection
            Dim oDR As SqlDataReader
            Dim aFaceTemplate() As String
            Dim nCnt As Integer = 0
            Dim nCnt2 As Integer = 0
            Dim cFaceTemplate As String = ""

            nCnt = -1
            ReDim aPersonel(0)

            ConnYage = OpenConn()

            cSQL = "select a.personel, a.username, b.password, a.facetemplate " +
                    " from personel a with (NOLOCK), users b with (NOLOCK) " +
                    " where a.username = b.username " +
                    " and a.personel is not null " +
                    " and a.personel <> '' " +
                    " and a.username is not null " +
                    " and a.username <> '' "

            oDR = GetSQLReader(cSQL, ConnYage)

            Do While oDR.Read
                cFaceTemplate = SQLReadString(oDR, "facetemplate")
                If cFaceTemplate.Trim <> "" Then
                    aFaceTemplate = Split(cFaceTemplate, ",")
                    If IsArray(aFaceTemplate) Then
                        nCnt = nCnt + 1
                        ReDim Preserve aPersonel(nCnt)
                        aPersonel(nCnt).cPersonel = SQLReadString(oDR, "personel")
                        aPersonel(nCnt).cUserName = SQLReadString(oDR, "username")
                        aPersonel(nCnt).cPassword = Decypher(SQLReadString(oDR, "password"))
                        ReDim aPersonel(nCnt).FaceTemplate(13342)
                        For nCnt2 = 0 To UBound(aFaceTemplate)
                            aPersonel(nCnt).FaceTemplate(nCnt2) = CByte(aFaceTemplate(nCnt2))
                        Next
                    End If
                End If
            Loop
            oDR.Close()
            oDR = Nothing

            CloseConn(ConnYage)

        Catch ex As Exception
            ErrDisp("LoadPersonel : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Timer1.Enabled = False
            Gl_Personel = ""
            Gl_UserName = ""
            Gl_ActivePass = ""
            GL_PersonelFaceTemplate = ""
            GL_PersonelResim = ""
            Me.Close()
        Catch ex As Exception
            ErrDisp("Button2_Click : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub frmLuxand_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            Timer1.Enabled = False

            FSDKCam.CloseVideoCamera(cameraHandle)
            FSDKCam.FinalizeCapturing()
            FSDK.FinalizeLibrary()
        Catch ex As Exception
            ErrDisp("frmLuxand_Closing : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim FacePosition As FSDK.TFacePosition
        Dim image As FSDK.CImage
        Dim gr As Graphics
        Dim i As Integer
        Dim p As FSDK.TPoint
        Dim lOK As Boolean = False
        Dim nQuality As Double = 0
        Dim FaceTemplate(13342) As Byte
        Dim nCnt As Integer
        Dim nSimilarity As Single = 0

        Static lProcessing As Boolean = False

        Try
            If lProcessing Then Exit Sub
            lProcessing = True

            TextBox1.Text = "Kamera kontrol " + nFAR.ToString
            TextBox1.Refresh()

            If (FSDKCam.GrabFrame(cameraHandle, ImageHandle) = FSDK.FSDKE_OK) Then
                image = New FSDK.CImage(ImageHandle)
                frameImage = image.ToCLRImage()
                PictureBox1.Image = frameImage
                PictureBox1.Refresh()
            Else
                TextBox1.Text = "Kameradan görüntü alınamıyor " + nFAR.ToString
                TextBox1.Refresh()
                lProcessing = False
                Exit Sub
            End If

            If Not (FSDK.DetectFace(ImageHandle, FacePosition) = FSDK.FSDKE_OK) Then
                TextBox1.Text = "Kameraya doğru bakınız lütfen " + nFAR.ToString
                TextBox1.Refresh()
                image.Dispose()
                frameImage.Dispose()
                lProcessing = False
                Exit Sub
            End If

            If Not (FSDK.DetectFacialFeatures(ImageHandle, FacialFeatures) = FSDK.FSDKE_OK) Then
                TextBox1.Text = "Yüz hatlarınız okunamıyor " + nFAR.ToString
                TextBox1.Refresh()
                image.Dispose()
                frameImage.Dispose()
                lProcessing = False
                Exit Sub
            End If

            If Not (FSDK.GetFaceTemplateUsingFeatures(ImageHandle, FacialFeatures, FaceTemplate) = FSDK.FSDKE_OK) Then
                TextBox1.Text = "Yüz şablonunuz hesaplanamıyor " + nFAR.ToString
                TextBox1.Refresh()
                image.Dispose()
                frameImage.Dispose()
                lProcessing = False
                Exit Sub
            End If

            gr = PictureBox1.CreateGraphics()
            gr.DrawRectangle(Pens.LightGreen, CType(FacePosition.xc - FacePosition.w * 0.6, Integer), CType(FacePosition.yc - FacePosition.w * 0.5, Integer), CType(FacePosition.w * 1.2, Integer), CType(FacePosition.w * 1.2, Integer))

            i = 1
            For Each p In FacialFeatures
                If (i > 2) Then
                    gr.DrawEllipse(Pens.LightGreen, p.x, p.y, 3, 3)
                Else
                    gr.DrawEllipse(Pens.Blue, p.x, p.y, 3, 3)
                End If
                i = i + 1
            Next

            PictureBox1.Refresh()

            If nMode = 1 And lNeedToClose Then
                GL_PersonelFaceTemplate = ""
                For i = 0 To UBound(FaceTemplate)
                    If GL_PersonelFaceTemplate.Trim = "" Then
                        GL_PersonelFaceTemplate = FaceTemplate(i).ToString
                    Else
                        GL_PersonelFaceTemplate = GL_PersonelFaceTemplate + "," + FaceTemplate(i).ToString
                    End If
                    If CDbl(FaceTemplate(i)) > 0 Then
                        nQuality = nQuality + 1
                    End If
                Next
                GL_PersonelResim = GetTempFile("jpg", "Personel")
                FSDK.SaveImageToFile(ImageHandle, GL_PersonelResim)
                TextBox1.Text = GL_PersonelResim + " kaydedildi"
                TextBox1.Refresh()
                lOK = True
            End If

            If nMode = 2 Then
                For nCnt = 0 To UBound(aPersonel)
                    If (FSDK.MatchFaces(FaceTemplate, aPersonel(nCnt).FaceTemplate, nSimilarity) = FSDK.FSDKE_OK) Then
                        If (nSimilarity > nMatchingThreshold) Then
                            Gl_Personel = aPersonel(nCnt).cPersonel
                            Gl_UserName = aPersonel(nCnt).cUserName
                            Gl_ActivePass = aPersonel(nCnt).cPassword
                            GL_Similarity = CDbl(nSimilarity * 100)

                            TextBox1.Text = Gl_Personel + " % " + CStr(Math.Round(nSimilarity * 100, 0))
                            TextBox1.Refresh()

                            lOK = True
                            Exit For
                        End If
                    End If
                Next
            End If

            FSDK.FreeImage(ImageHandle)
            image.Dispose()
            frameImage.Dispose()

            If lOK Then
                Timer1.Enabled = False
                lProcessing = False
                Me.Close()
            Else
                lProcessing = False
            End If

        Catch ex As Exception
            lProcessing = False
            ErrDisp("frmLuxand_Closing : " + ex.Message, Me.Name,,, ex)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        lNeedToClose = True
    End Sub
End Class