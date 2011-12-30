Imports 進銷存.Database.StructureBase

Public Class winStock

    Public Sub Config()
        cbGoods.Items.Clear()
        cbGoods.Items.AddRange(Array.ConvertAll(Goodsies, Function(G As Goods) G.Name))
        If cbGoods.Items.Count > 0 Then cbGoods.SelectedIndex = cbGoods.Items.Count - 1

        cbSupplier.Items.Clear()
        cbSupplier.Items.AddRange(Array.ConvertAll(Supplies, Function(S As Supplier) S.Name))
        If cbSupplier.Items.Count > 0 Then cbSupplier.SelectedIndex = cbSupplier.Items.Count - 1

    End Sub


    Public Sub Add(ByVal Data As Stock)
        Config()
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub UpdateText(ByVal Data As Stock)
        txtLabel.Text = Data.Label
        txtCost.Text = Data.Cost
        txtIMEI.Text = Data.IMEI
        txtPrice.Text = Data.Price
        txtDate.Text = Data.Date
        txtNote.Text = Data.Note
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
        If cbGoods.SelectedIndex >= 0 Then Data.GoodsLabel = Goodsies(cbGoods.SelectedIndex).Label
        If cbSupplier.SelectedIndex >= 0 Then Data.SupplierLabel = Supplies(cbSupplier.SelectedIndex).Label
        Return Data
    End Function

    Private Sub btAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAdd.Click
        DB.AddStock(GetData())
        InitialProgram()
    End Sub

    Private Sub btAddSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddSupplier.Click
        winPeople.AddSupplier(NewSupplier)
        InitialProgram()
        Config()
    End Sub

    Private Sub btAddGoods_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddGoods.Click
        winGoods.Add(NewGoods)
        InitialProgram()
        Config()

    End Sub
End Class