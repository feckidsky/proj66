Imports System.Runtime.InteropServices
Imports 進銷存.Database


Public Class winPeople

    Public Enum StructKind
        Supplier = 0
        Customer = 1
        Personnel
    End Enum

    Enum Mode
        Create = 0
        Open = 1
    End Enum

    Dim work As Mode

    Public DataMode As StructKind

    <StructLayout(LayoutKind.Explicit)> _
    Structure Data
        <FieldOffset(0)> Dim Supplier As Supplier
        <FieldOffset(0)> Dim Customer As Customer
        <FieldOffset(0)> Dim Personnel As Personnel
    End Structure

    Dim myData As Data

    Private Sub winPeople_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtLabel.Enabled = work = Mode.Create
        btAccount.Visible = DataMode = StructKind.Personnel
    End Sub
    Public Overloads Sub ShowDialog(Of T)(ByVal data As T)
        UpdateText(data)
        MyBase.ShowDialog()
    End Sub

    Public Sub OpenSupplier(ByVal data As Supplier)
        If Not CheckAuthority(2) Then Exit Sub
        work = Mode.Open
        DataMode = StructKind.Supplier
        ShowDialog(data)
    End Sub

    Public Sub OpenCustomer(ByVal data As Customer)
        If Not CheckAuthority(2) Then Exit Sub
        work = Mode.Open
        DataMode = StructKind.Customer
        ShowDialog(data)
    End Sub

    Public Sub OpenPersonnel(ByVal data As Personnel)
        If Not CheckAuthority(3, WithAdmin:=True) Then Exit Sub
        If winLogIn.ShowDialog("請輸入使用者密碼", CurrentUser.ID).State <> LoginState.Success Then Exit Sub
        work = Mode.Open
        DataMode = StructKind.Personnel
        ShowDialog(data)
    End Sub

    Public Sub CreateSupplier(ByVal Data As Supplier)
        If Not CheckAuthority(2) Then Exit Sub
        work = Mode.Create
        DataMode = StructKind.Supplier
        ShowDialog(Data)
    End Sub

    Public Sub CreateCustomer(ByVal Data As Customer)
        If Not CheckAuthority(2) Then Exit Sub

        work = Mode.Create
        DataMode = StructKind.Customer
        ShowDialog(Data)
    End Sub

    Public Sub CreatePersonnel(ByVal Data As Personnel)
        If Not CheckAuthority(3, WithAdmin:=True) Then Exit Sub
        If winLogIn.ShowDialog("請輸入使用者密碼", CurrentUser.ID).State <> LoginState.Success Then Exit Sub
        work = Mode.Create
        DataMode = StructKind.Personnel
        ShowDialog(Data)
    End Sub

    Public Sub UpdateAccountButton()
        btAccount.Text = IIf(myData.Personnel.ID = "", "此員工尚未設定帳號(按此設定)", myData.Personnel.ID)
    End Sub
    Public Sub UpdateText(ByVal obj As Object)
        Select Case DataMode
            Case StructKind.Supplier : Me.Text = "供應商" : myData.Supplier = obj
            Case StructKind.Customer : Me.Text = "客戶" : myData.Customer = obj
            Case StructKind.Personnel : Me.Text = "員工"
                myData.Personnel = obj
                UpdateAccountButton()
        End Select
        txtLabel.Text = myData.Supplier.Label
        txtName.Text = myData.Supplier.Name
        txtAddr.Text = myData.Supplier.Addr
        txtTel1.Text = myData.Supplier.Tel1
        txtTel2.Text = myData.Supplier.Tel2
        txtNote.Text = myData.Supplier.Note
        btAdd.Text = IIf(work = Mode.Create, "新增", "修改")
    End Sub

    Public Function GetData() As Data
        Dim data As Data = Nothing
        data.Supplier.Label = txtLabel.Text
        data.Supplier.Name = txtName.Text
        data.Supplier.Addr = txtAddr.Text
        data.Supplier.Tel1 = txtTel1.Text
        data.Supplier.Tel2 = txtTel2.Text
        data.Supplier.Note = txtNote.Text

        If DataMode = StructKind.Personnel Then
            data.Personnel.ID = myData.Personnel.ID
            data.Personnel.Password = myData.Personnel.Password
            data.Personnel.Authority = myData.Personnel.Authority
        End If

        Return data
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAdd.Click
        myData = GetData()

        If work = Mode.Create Then
            Select Case DataMode
                Case StructKind.Customer
                    DB.AddCustomer(myData.Customer)
                Case StructKind.Personnel
                    DB.AddPersonnel(myData.Personnel)
                Case StructKind.Supplier
                    DB.AddSupplier(myData.Supplier)
            End Select
        Else
            Select Case DataMode
                Case StructKind.Supplier
                    DB.ChangeSupplier(myData.Supplier)
                Case StructKind.Personnel
                    DB.ChangePersonnel(myData.Personnel)
                Case StructKind.Customer
                    DB.ChangeCustomer(myData.Customer)
            End Select
        End If
        Me.Close()
    End Sub



    Private Sub btAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAccount.Click
        winAccount.ShowDialog(myData.Personnel)
        UpdateAccountButton()
    End Sub
End Class
