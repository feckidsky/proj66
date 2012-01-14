Imports 進銷存.Database.StructureBase

Public Class winStockIn

    Enum Mode
        Create = 0
        Open = 1
    End Enum
    Dim Work As Mode


    Private Sub winStockIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btOK.Text = IIf(Work = Mode.Create, "入庫", "修改")
        txtLabel.Enabled = Work = Mode.Create
    End Sub

    Public Sub Config()
        cbGoods.Items.Clear()
        cbGoods.Items.AddRange(Array.ConvertAll(Goodsies, Function(G As Goods) G.Name))
        If cbGoods.Items.Count > 0 Then cbGoods.SelectedIndex = cbGoods.Items.Count - 1

        cbSupplier.Items.Clear()
        cbSupplier.Items.AddRange(Array.ConvertAll(Supplies, Function(S As Supplier) S.Name))
        If cbSupplier.Items.Count > 0 Then cbSupplier.SelectedIndex = cbSupplier.Items.Count - 1

    End Sub


    Public Sub Create()
        Dim data As Stock = GetNewStock()
        Config()
        UpdateText(data)
        Work = Mode.Create
        MyBase.ShowDialog()
    End Sub

    Public Sub Open(ByVal stock As Stock)
        Config()
        UpdateText(stock)
        Work = Mode.Open
        MyBase.ShowDialog()
    End Sub

    Public Sub UpdateText(ByVal Data As Stock)
        txtLabel.Text = Data.Label
        txtCost.Text = Data.Cost
        txtIMEI.Text = Data.IMEI
        txtPrice.Text = Data.Price
        txtDate.Text = Data.Date
        txtNote.Text = Data.Note
        txtNumber.Text = Data.Number
        cbGoods.Text = Array.Find(Goodsies, Function(G As Goods) G.Label = Data.GoodsLabel).Name
        cbSupplier.Text = Array.Find(Supplies, Function(S As Supplier) S.Label = Data.SupplierLabel).Name
    End Sub

    Public Function GetData() As Stock
        Dim Data As Stock = Nothing
        Data.Label = txtLabel.Text
        Data.Cost = txtCost.Text
        Data.IMEI = txtIMEI.Text
        Data.Price = txtPrice.Text
        Data.Note = txtNote.Text
        Data.Date = txtDate.Text
        Data.Number = Val(txtNumber.Text)
        If cbGoods.SelectedIndex >= 0 Then
            Data.GoodsLabel = Goodsies(cbGoods.SelectedIndex).Label
        Else
            Data.GoodsLabel = ""
        End If

        If cbSupplier.SelectedIndex >= 0 Then Data.SupplierLabel = Supplies(cbSupplier.SelectedIndex).Label
        Return Data
    End Function

    Private Sub btAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOK.Click
        Dim newStock As Stock = GetData()
        If newStock.GoodsLabel = "" Then
            MsgBox("尚未指定商品")
            Exit Sub
        End If

        If Work = Mode.Create Then
            DB.AddStock(newStock)
        Else
            DB.ChangeStock(newStock)

        End If
        InitialProgram()
        Me.Close()
    End Sub

    Private Sub btAddSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddSupplier.Click
        winPeople.AddSupplier(GetNewSupplier)
        InitialProgram()
        Config()
    End Sub

    Private Sub btAddGoods_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddGoods.Click
        'winGoods.Add(GetNewGoods)
        'InitialProgram()

        winGoodsList.Show()
        Config()

    End Sub



End Class