Imports 進銷存.Database

Public Class winLogIn



    Dim result As LoginResult
    Public WithEvents Access As Access
    'Dim access As Access
    Public Overloads Function ShowDialog(ByVal db As Access, Optional ByVal Title As String = "登入", Optional ByVal ID As String = "") As LoginResult
        ' Me.access = db
        txtID.Enabled = ID = ""
        Me.Text = Title
        txtID.Text = ID
        txtPassword.Text = ""
        ckAutoLogin.Checked = LoginSetting.AutoLog
        result.State = LoginState.PasswordError

        MyBase.ShowDialog()
        Return result
    End Function

    Private Sub winLogIn_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Access = Nothing
    End Sub

    Private Sub winLogIn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If txtID.Enabled Then txtID.Focus()
        If Not txtID.Enabled Then txtPassword.Focus()

        cbShop.Items.Clear()
        cbShop.Items.AddRange(ClientManager.GetNameList())

        cbShop.Enabled = Config.Mode = Connect.Client
        'cbShop.Text = access.Name

        If Config.Mode = Connect.Server Then
            cbShop.Text = myDatabase.Name
        Else
            If CurrentAccess IsNot Nothing Then
                cbShop.Text = CurrentAccess.Name
            Else
                cbShop.Text = LoginSetting.Shop ' Access.Name
            End If

        End If
    End Sub

    Private Sub winLogIn_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated


    End Sub

    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        'Me.Close()
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub btLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btLogin.Click

        Access = ClientManager(cbShop.Text)

        If Access Is Nothing Then
            MsgBox("您所選擇的店名不存在")
            Exit Sub
        End If

        Login(txtID.Text, txtPassword.Text)
        'db.LogIn(txtID.Text, txtPassword.Text)
        'result = db.LogIn(txtID.Text, txtPassword.Text)


        'If result.State = LoginState.Disconnect Then MsgBox(result.msg, MsgBoxStyle.Information, "錯誤")

        'LoginSetting.AutoLog = ckAutoLogin.Checked
        'If LoginSetting.AutoLog Then
        '    LoginSetting.ID = txtID.Text
        '    LoginSetting.Password = txtPassword.Text
        'Else
        '    LoginSetting.ID = ""
        '    LoginSetting.Password = ""
        'End If
        'LoginSetting.Shop = cbShop.Text
        'LoginSetting.Save(LoginInfoPath)

        'If result.State = LoginState.Success Then

        '    DialogResult = Windows.Forms.DialogResult.OK
        'End If

    End Sub
    Private Sub Access_Account_LogIn(ByVal sender As Object, ByVal e As Database.LoginResult) Handles Access.Account_LogIn
        Me.result = e
        If result.State = LoginState.Disconnect Then MsgBox(result.msg, MsgBoxStyle.Information, "錯誤")


        LoginSetting.AutoLog = ckAutoLogin.Checked
        If LoginSetting.AutoLog Then
            LoginSetting.ID = txtID.Text
            LoginSetting.Password = txtPassword.Text
        Else
            LoginSetting.ID = ""
            LoginSetting.Password = ""
        End If
        LoginSetting.Shop = CType(sender, Access).Name  'cbShop.Text
        LoginSetting.Save(LoginInfoPath)

        If result.State = LoginState.Success Then

            DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub


    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Me.DefaultTextBoxImeMode()
    End Sub


End Class