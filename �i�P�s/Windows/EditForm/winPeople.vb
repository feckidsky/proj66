Imports System.Runtime.InteropServices
Imports 進銷存.Database


Public Class winPeople


    Enum Mode
        Create = 0
        Open = 1
    End Enum

    Dim work As Mode

    Dim myData As Personnel

    Dim access As Database.Access

    Private Sub winPeople_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtLabel.Enabled = work = Mode.Create
        btAccount.Visible = True
    End Sub

    Public Sub Open(ByVal data As Personnel, ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(3, WithAdmin:=True) Then Exit Sub
        If winLogIn.ShowDialog(access, "請輸入使用者密碼", CurrentUser.ID).State <> LoginState.Success Then Exit Sub
        work = Mode.Open
        UpdateText(data)
        MyBase.ShowDialog()
    End Sub


    Public Sub Create(ByVal Data As Personnel, ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(3, WithAdmin:=True) Then Exit Sub
        If winLogIn.ShowDialog(access, "請輸入使用者密碼", CurrentUser.ID).State <> LoginState.Success Then Exit Sub
        work = Mode.Create
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub UpdateAccountButton()
        btAccount.Text = IIf(myData.ID = "", "此員工尚未設定帳號(按此設定)", myData.ID)
    End Sub

    Public Sub UpdateText(ByVal obj As Personnel)

        Me.Text = "員工"
        myData = obj
        UpdateAccountButton()

        txtLabel.Text = myData.Label
        txtName.Text = myData.Name
        txtAddr.Text = myData.Addr
        txtTel1.Text = myData.Tel1
        txtTel2.Text = myData.Tel2
        txtNote.Text = myData.Note
        btAdd.Text = IIf(work = Mode.Create, "新增", "修改")
    End Sub

    Public Function GetData() As Personnel
        Dim data As Personnel = Nothing
        data.Label = txtLabel.Text
        data.Name = txtName.Text
        data.Addr = txtAddr.Text
        data.Tel1 = txtTel1.Text
        data.Tel2 = txtTel2.Text
        data.Note = txtNote.Text

        data.ID = myData.ID
        data.Password = myData.Password
        data.Authority = myData.Authority
        data.Modify = Now

        Return data
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAdd.Click
        myData = GetData()

        If work = Mode.Create Then
            Access.AddPersonnel(myData)
        Else
            Access.ChangePersonnel(myData)
        End If
        Me.Close()
    End Sub



    Private Sub btAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAccount.Click
        winAccount.ShowDialog(myData, access)
        UpdateAccountButton()
    End Sub

    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Me.DefaultTextBoxImeMode()
    End Sub
End Class
