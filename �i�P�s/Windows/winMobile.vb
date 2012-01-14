Imports 進銷存.Database.StructureBase

Public Class winMobile



    Public Sub Add(ByVal Data As Mobile)
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub UpdateText(ByVal Data As Mobile)
        txtLabel.Text = Data.Label
        txtName.Text = Data.Name
        txtCommission.Text = Data.Commission
        txtDiscount.Text = Data.Discount
        txtNote.Text = Data.Note
    End Sub

    Public Function GetData() As Mobile
        Dim Data As Mobile
        Data.Label = txtLabel.Text
        Data.Name = txtName.Text
        Data.Discount = txtDiscount.Text
        Data.Commission = txtCommission.Text
        Data.Note = txtNote.Text
        Return Data
    End Function

    Private Sub btAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAdd.Click
        DB.AddMobile(GetData())
        UpdateText(GetNewMobile)
    End Sub


End Class