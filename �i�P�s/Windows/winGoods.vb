Imports 進銷存.Database.StructureBase

Public Class winGoods
    Enum Mode
        Create = 0
        Open = 1
    End Enum

    Dim Work As Mode

    Private Sub winGoods_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btAdd.Text = IIf(Work = Mode.Create, "新增", "修改")
    End Sub

    Public Sub Open(ByVal Data As Goods)
        Work = Mode.Open
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub Create(ByVal Data As Goods)
        Work = Mode.Create
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub UpdateText(ByVal Data As Goods)
        txtLabel.Text = Data.Label
        txtName.Text = Data.Name
        txtKind.Text = Data.Kind
        txtBrand.Text = Data.Brand
        txtNote.Text = Data.Note
    End Sub

    Public Function GetData() As Goods
        Dim Data As Goods
        Data.Label = txtLabel.Text
        Data.Name = txtName.Text
        Data.Kind = txtKind.Text
        Data.Brand = txtBrand.Text
        Data.Note = txtNote.Text
        Return Data
    End Function

    Private Sub btAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAdd.Click

        If Work = Mode.Create Then
            DB.AddGoods(GetData())
        Else
            DB.ChangeGoods(GetData())
        End If

        Me.Close()
        'UpdateText(GetNewGoods)


    End Sub


End Class