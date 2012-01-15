Public Class winAccount
    Dim personnel As Database.Personnel

    Public Overloads Sub ShowDialog(ByRef per As Database.Personnel)
        personnel = per
        txtID.Text = per.ID
        txtPassword1.Text = per.Password
        txtPassword2.Text = per.Password
        txtAuthority.Text = per.Authority
        txtID.Focus()

        If MyBase.ShowDialog() = Windows.Forms.DialogResult.OK Then


            per.ID = txtID.Text
            per.Password = txtPassword1.Text
            per.Authority = Integer.Parse(txtAuthority.Text)

        End If

    End Sub

    Private Sub btOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOK.Click

        Dim ErrorMsg As String = ""

        Dim cnv As Integer

        If personnel.ID <> txtID.Text AndAlso Not DB.GetPersonnelByID(txtID.Text).IsNull() Then
            ErrorMsg = "此帳號已經被使用，請使用其他名稱"
        ElseIf txtPassword1.Text <> txtPassword2.Text Then
            ErrorMsg = "密碼確認錯誤!"
        ElseIf Not Integer.TryParse(txtAuthority.Text, cnv) Then
            ErrorMsg = "錯誤的權限設定"
        End If


        If ErrorMsg <> "" Then
            MsgBox(ErrorMsg, MsgBoxStyle.Exclamation, "錯誤提示")
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class