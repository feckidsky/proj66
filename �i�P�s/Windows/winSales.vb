Imports 進銷存.Database.StructureBase

Public Class winSales

    Private Sub winSales_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cbPayMode.Items.AddRange(TypeOfPaymentsDescribe)
        cbPayMode.SelectedIndex = TypeOfPayment.Unpaid
    End Sub

    'Public Overloads Sub Show(ByVal Data As Sales)

    '    MyBase.Show()

    '    txtLabel.Text = Data.Label
    '    txtDate.Text = Data.Date.ToString("yyyy/MM/dd")

    'End Sub

    Public Overloads Sub Show()
        MyBase.Show()
        Dim sales As Sales = NewSales()
        txtLabel.Text = sales.Label
        txtDate.Text = sales.Date.ToString("yyyy/MM/dd")
    End Sub

    Private Sub btAddGood_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddGood.Click
        Dim row As DataGridViewRow = winStock.SelectGood()

        If row IsNot Nothing Then
            dgList.Rows.Add(New String() {row.Cells("庫存編號").Value, row.Cells("種類").Value, row.Cells("廠牌").Value, row.Cells("品名").Value, row.Cells("售價").Value, row.Cells("售價").Value, 1})
        End If
        CalTotal()
    End Sub

    Private Sub btDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDelete.Click
        If dgList.SelectedCells.Count >= 1 Then
            Dim idx As Integer = dgList.SelectedCells.Item(0).RowIndex
            If (idx < dgList.Rows.Count - 1) Then dgList.Rows.RemoveAt(idx)
        End If
        CalTotal()
    End Sub

    Private Sub CalTotal()
        Dim total As Integer = 0
        Dim sub_total As Integer = 0
        For Each row As DataGridViewRow In dgList.Rows
            sub_total = row.Cells(cSPrice.Index).Value * row.Cells(cNumber.Index).Value
            row.Cells(cSubTotal.Index).Value = sub_total
            total += sub_total
        Next
        lbTotal.Text = total
    End Sub


    Private Sub dgList_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgList.CellValueChanged
        If e.ColumnIndex = cSPrice.Index Or e.ColumnIndex = cNumber.Index Then CalTotal()
    End Sub


    Private Sub btCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCheck.Click
        Dim newSales As Sales

        With newSales
            .Label = txtLabel.Text
            .Date = Now
            .Note = txtNote.Text
            .CustomerLabel = ""
            .PersonnelLabel = ""
            .TypeOfPayment = cbPayMode.SelectedIndex
        End With

        Database.Access.File.AddBase(newSales)

        Dim newGoods As SalesGoods

        For Each r As DataGridViewRow In dgList.Rows

            If r.Cells(cLabel.Index).Value Is Nothing Then Continue For
            With newGoods
                .SalesLabel = newSales.Label
                .StockLabel = r.Cells(cLabel.Index).Value
                .SellingPrice = r.Cells(cSPrice.Index).Value
                .Number = r.Cells(cNumber.Index).Value
            End With

            Database.Access.File.AddBase(newGoods)
        Next
    End Sub


End Class