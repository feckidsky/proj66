Imports System.Runtime.InteropServices
Imports 進銷存.Database


Public Class winCustomer


    Enum Mode
        Create = 0
        Open = 1
    End Enum

    Dim work As Mode

    Dim access As Database.Access

    Private Sub winPeople_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtLabel.Enabled = work = Mode.Create
        btAccount.Visible = False
    End Sub

    Public Sub Open(ByVal data As Customer, ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub
        work = Mode.Open
        UpdateText(data)
        MyBase.ShowDialog()
    End Sub

    Public Sub Create(ByVal Data As Customer, ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub
        work = Mode.Create
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub


    Public Sub UpdateText(ByVal supplier As Customer)

        txtLabel.Text = supplier.Label
        txtName.Text = supplier.Name
        txtAddr.Text = supplier.Addr
        txtTel1.Text = supplier.Tel1
        txtTel2.Text = supplier.Tel2
        txtNote.Text = supplier.Note
        btAdd.Text = IIf(work = Mode.Create, "新增", "修改")
    End Sub

    Public Function GetData() As Customer
        Dim data As New Customer
        data.Label = txtLabel.Text
        data.Name = txtName.Text
        data.Addr = txtAddr.Text
        data.Tel1 = txtTel1.Text
        data.Tel2 = txtTel2.Text
        data.Note = txtNote.Text
        data.Modify = Now

        'If DataMode = StructKind.Personnel Then
        '    data.Personnel.ID = myData.Personnel.ID
        '    data.Personnel.Password = myData.Personnel.Password
        '    data.Personnel.Authority = myData.Personnel.Authority
        '    data.Personnel.Modify = Now

        'End If

        Return data
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAdd.Click
        Dim myData As Customer = GetData()

        If work = Mode.Create Then
            Access.AddCustomer(myData)
        Else
            Access.ChangeCustomer(myData)
        End If
        Me.Close()
    End Sub



    'Private Sub btAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAccount.Click
    '    winAccount.ShowDialog(myData.Personnel)
    '    UpdateAccountButton()
    'End Sub
End Class
