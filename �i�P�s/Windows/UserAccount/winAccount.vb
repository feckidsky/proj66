Public Class winAccount
    Dim personnel As Database.Personnel

    Dim access As Database.Access

    Public Overloads Sub ShowDialog(ByRef per As Database.Personnel, ByVal DB As Database.Access)
        access = DB
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

        If personnel.ID <> txtID.Text AndAlso Not access.GetPersonnelByID(txtID.Text).IsNull() Then
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

    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Me.DefaultTextBoxImeMode()
    End Sub
End Class