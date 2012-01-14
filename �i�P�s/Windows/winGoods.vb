Imports 進銷存.Database.StructureBase

Public Class winGoods


    Public Sub Add(ByVal Data As Goods)
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
        DB.AddGoods(GetData())
        UpdateText(GetNewGoods)
    End Sub

    Private Sub winGoods_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class