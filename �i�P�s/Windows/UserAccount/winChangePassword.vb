﻿Public Class winChangePassword

    Dim result As Database.LoginResult

    Dim access As Database.Access



    Public Overloads Sub ShowDialog(ByVal DB As Database.Access, Optional ByVal Title As String = "登入", Optional ByVal ID As String = "")
        access = DB
        If CurrentUser.IsGuest() Then
            MsgBox("尚未登入")
            Exit Sub
        End If

        txtPassword.Clear()
        txtNewPassword1.Clear()
        txtNewPassword2.Clear()

        MyBase.ShowDialog()

    End Sub

    Private Sub winLogIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        Me.Close()
    End Sub

    Private Sub btOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOK.Click

        Dim ErrorMsg As String = ""
        If txtPassword.Text <> CurrentUser.Password Then
            ErrorMsg = "密碼錯誤!"
        ElseIf txtNewPassword1.Text <> txtNewPassword2.Text Then
            txtNewPassword1.Clear()
            txtNewPassword2.Clear()
            ErrorMsg = "密碼確認錯誤!，請重新輸入密碼"
        End If

        If ErrorMsg = "" Then
            CurrentUser.Password = txtNewPassword1.Text
            CurrentUser.Modify = Now
            access.ChangePersonnel(CurrentUser)
            MsgBox("密碼修改成功!", MsgBoxStyle.Information)
            Me.Close()
        Else
            MsgBox(ErrorMsg, MsgBoxStyle.Exclamation)
        End If

    End Sub

    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Me.DefaultTextBoxImeMode()
    End Sub
End Class