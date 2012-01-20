Public Class winLogIn

    Dim result As LoginResult

    Public Overloads Function ShowDialog(Optional ByVal Title As String = "登入", Optional ByVal ID As String = "") As LoginResult
        txtID.Enabled = ID = ""

        Me.Text = Title
        txtID.Text = ID
        txtPassword.Text = ""

        MyBase.ShowDialog()
        Return result
    End Function

    Private Sub winLogIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If txtID.Enabled Then txtID.Focus()
        If Not txtID.Enabled Then txtPassword.Focus()
    End Sub

    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        Me.Close()
    End Sub

    Private Sub btLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btLogin.Click
        result = LogIn(txtID.Text, txtPassword.Text)
        If result.State = LoginState.Success Then Me.Close()
    End Sub
End Class