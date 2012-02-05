Imports System.Runtime.InteropServices
Imports 進銷存.Database


Public Class winSupplier


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

    'Public Overloads Sub ShowDialog(Of T)(ByVal data As T)

    'End Sub

    Public Sub Open(ByVal data As Supplier, ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub
        work = Mode.Open
        UpdateText(data)
        MyBase.ShowDialog()
    End Sub

    Public Sub Create(ByVal Data As Supplier, ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub
        work = Mode.Create
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub


    Public Sub UpdateText(ByVal supplier As Supplier)

        txtLabel.Text = Supplier.Label
        txtName.Text = Supplier.Name
        txtAddr.Text = Supplier.Addr
        txtTel1.Text = Supplier.Tel1
        txtTel2.Text = Supplier.Tel2
        txtNote.Text = Supplier.Note
        btAdd.Text = IIf(work = Mode.Create, "新增", "修改")
    End Sub

    Public Function GetData() As Supplier
        Dim data As New Supplier
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
        Dim myData As Supplier = GetData()

        If work = Mode.Create Then
            Access.AddSupplier(myData)
        Else
            Access.ChangeSupplier(myData)
        End If
        Me.Close()
    End Sub



    'Private Sub btAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAccount.Click
    '    winAccount.ShowDialog(myData.Personnel)
    '    UpdateAccountButton()
    'End Sub
End Class
