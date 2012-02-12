Imports 進銷存.Database.DatabaseType
Imports 進銷存.Database

Public Class winGoods
    Enum Mode
        Create = 0
        Open = 1
    End Enum

    Dim Work As Mode

    Dim access As Database.Access

    Private Sub winGoods_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btAdd.Text = IIf(Work = Mode.Create, "新增", "修改")
        txtLabel.Enabled = Work = Mode.Create
        txtPrice.Enabled = txtLabel.Enabled
        txtCost.Enabled = txtLabel.Enabled
        cbBrand.Items.Clear()
        cbBrand.Items.AddRange(CurrentAccess.GetBrandList())
        cbKind.Items.Clear()
        cbKind.Items.AddRange(CurrentAccess.GetKindList())
    End Sub

    Public Sub Open(ByVal Data As Goods, ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub
        Work = Mode.Open
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub Create(ByVal Data As Goods, ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub
        Work = Mode.Create
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub UpdateText(ByVal Data As Goods)
        txtLabel.Text = Data.Label
        txtName.Text = Data.Name
        cbKind.Text = Data.Kind
        cbBrand.Text = Data.Brand
        txtNote.Text = Data.Note
    End Sub

    Public Function GetData() As Goods
        Dim Data As Goods
        Data.Label = txtLabel.Text
        Data.Name = txtName.Text
        Data.Kind = cbKind.Text
        Data.Brand = cbBrand.Text
        Data.Note = txtNote.Text
        Data.Modify = Now
        Return Data
    End Function

    Public Function GetHistoryPrice() As HistoryPrice
        Dim data As HistoryPrice
        data.Cost = Val(txtCost.Text)
        data.Price = Val(txtPrice.Text)
        data.GoodsLabel = txtLabel.Text
        data.Time = Now
        Return data
    End Function

    Private Sub btAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAdd.Click

        If Work = Mode.Create Then
            access.AddGoods(GetData())
            access.AddHistoryPrice(GetHistoryPrice())
        Else
            access.ChangeGoods(GetData())
        End If

        Me.Close()
        'UpdateText(GetNewGoods)


    End Sub


End Class