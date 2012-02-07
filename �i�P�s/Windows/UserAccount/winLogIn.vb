Imports 進銷存.Database

Public Class winLogIn

    Dim result As LoginResult
    Dim access As Access
    Public Overloads Function ShowDialog(ByVal db As Access, Optional ByVal Title As String = "登入", Optional ByVal ID As String = "") As LoginResult
        Me.access = db
        txtID.Enabled = ID = ""
        Me.Text = Title
        txtID.Text = ID
        txtPassword.Text = ""
        ckAutoLogin.Checked = LoginSetting.AutoLog
        result.State = LoginState.PasswordError

        MyBase.ShowDialog()
        Return result
    End Function

    Private Sub winLogIn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If txtID.Enabled Then txtID.Focus()
        If Not txtID.Enabled Then txtPassword.Focus()

        cbShop.Items.Clear()
        cbShop.Items.AddRange(Client.GetNameList())

        cbShop.Enabled = Config.Mode = Connect.Client
        cbShop.Text = access.Name

    End Sub

    Private Sub winLogIn_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated


    End Sub

    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        'Me.Close()
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub btLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btLogin.Click

        Dim db As Access = Client(cbShop.Text)

        If db Is Nothing Then
            MsgBox("您所選擇的店名不存在")
            Exit Sub
        End If

        result = db.LogIn(txtID.Text, txtPassword.Text)


        If result.State = LoginState.Disconnect Then MsgBox(result.msg, MsgBoxStyle.Information, "錯誤")

        LoginSetting.AutoLog = ckAutoLogin.Checked
        If LoginSetting.AutoLog Then
            LoginSetting.ID = txtID.Text
            LoginSetting.Password = txtPassword.Text
        Else
            LoginSetting.ID = ""
            LoginSetting.Password = ""
        End If
        LoginSetting.Shop = cbShop.Text
        LoginSetting.Save(LoginInfoPath)

        If result.State = LoginState.Success Then

            DialogResult = Windows.Forms.DialogResult.OK
        End If

    End Sub


End Class