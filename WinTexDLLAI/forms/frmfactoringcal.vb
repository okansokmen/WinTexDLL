Public Class frmfactoringcal

    Dim sVade As Double = 0
    Dim sOran As Double = 0
    Dim sKomisyon As Double = 0
    Dim sKesinti As Double = 0
    Dim sKomtutar As Double = 0
    Dim sToplam As Double = 0
    Dim sTutar As Double = 0


    Private Sub frmfactoringcal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        toplamhesapla()
    End Sub


    Private Sub vade_EditValueChanged(sender As Object, e As EventArgs) Handles vade.EditValueChanged
        toplamhesapla()
    End Sub


    Sub toplamhesapla()
        If Not (vade.Text.Trim = "") Then
            '  sVade = CDec(vade.Text)
            sVade = vade.Text
        End If

        If Not (oran.Text.Trim = "") Then
            '   sOran = CDec(oran.Text)
            sOran = oran.Text
        End If

        If Not (tutar.Text.Trim = "") Then
            '  sTutar = CDec(tutar.Text)
            sTutar = tutar.Text
        End If


        sKesinti = (sTutar * sVade * sOran) / 36000
        'kesinti.Text = Chr(sKesinti)
        kesinti.Text = sKesinti
        If Not (komisyon.Text.Trim = "") Then
            ' sKomisyon = CDec(komisyon.Text)
            sKomisyon = komisyon.Text
        End If


        sKomtutar = (sKomisyon * sTutar)
        komtutar.Text = sKomtutar

        sToplam = sKomtutar + sKesinti

        toplam.Text = sToplam





    End Sub



    Private Sub tutar_EditValueChanged(sender As Object, e As EventArgs) Handles tutar.EditValueChanged
        toplamhesapla()
    End Sub

    Private Sub oran_EditValueChanged(sender As Object, e As EventArgs) Handles oran.EditValueChanged
        toplamhesapla()
    End Sub

    Private Sub komisyon_EditValueChanged(sender As Object, e As EventArgs) Handles komisyon.EditValueChanged
        toplamhesapla()
    End Sub

    Private Sub GroupControl1_Paint(sender As Object, e As PaintEventArgs) Handles GroupControl1.Paint

    End Sub
End Class