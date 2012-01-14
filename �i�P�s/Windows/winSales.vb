Imports 進銷存.Database.StructureBase

Public Class winSales

    Enum Mode
        Create = 0
        Edit = 1
    End Enum

    Dim Work As Mode


    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        cbPayMode.Items.AddRange(TypeOfPaymentsDescribe)
        cbPayMode.SelectedIndex = TypeOfPayment.Unpaid
    End Sub


    Private Sub winSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btCheck.Text = IIf(Work = Mode.Create, "新增", "更新")

    End Sub

    Public Overloads Sub Create()

        Dim sales As Sales = GetNewSales()
        txtLabel.Text = sales.Label
        txtDate.Text = sales.Date.ToString("yyyy/MM/dd HH:mm:ss")
        Work = Mode.Create
        MyBase.Show()
    End Sub


    Public Sub Open(ByVal Label As String)
        Dim sales As Sales = DB.GetSales(Label)
        Open(sales)
    End Sub


    Public Sub Open(ByVal Sales As Sales)

        txtLabel.Text = Sales.Label
        txtDate.Text = Sales.Date.ToString("yyyy/MM/dd HH:mm:ss")
        txtNote.Text = Sales.Note
        cbPayMode.SelectedIndex = Sales.TypeOfPayment

        Dim dt As Data.DataTable = DB.GetGoodsListBySalesLabel(Sales.Label)

        For Each r As Data.DataRow In dt.Rows
            dgList.Rows.Add(r.ItemArray())
        Next
        CalTotal()
        Work = Mode.Edit
        MyBase.Show()
    End Sub

    Private Sub btAddGood_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddGood.Click
        Dim row As DataGridViewRow = winStockList.SelectGood()
        If row Is Nothing Then Exit Sub
        For Each tmp As DataGridViewRow In dgList.Rows
            If row.Cells("庫存編號").Value = tmp.Cells(cLabel.Index).Value Then
                MsgBox("該商品已在清單中...")
                Exit Sub
            End If
        Next

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


    '取得銷貨單資訊
    Public Function GetFormSales() As Sales
        Dim newSales As Sales
        With newSales
            .Label = txtLabel.Text
            .Date = Date.ParseExact(txtDate.Text, "yyyy/MM/dd HH:mm:ss", Nothing)
            .Note = txtNote.Text
            .CustomerLabel = ""
            .PersonnelLabel = ""
            .TypeOfPayment = cbPayMode.SelectedIndex
        End With
        Return newSales
    End Function


    '取得銷貨清單
    Public Function GetFormGoodsList() As SalesGoods()
        Dim lstGoods As New List(Of SalesGoods)
        Dim newGoods As SalesGoods

        For Each r As DataGridViewRow In dgList.Rows
            If r.Cells(cLabel.Index).Value Is Nothing Then Continue For
            With newGoods
                .SalesLabel = txtLabel.Text
                .StockLabel = r.Cells(cLabel.Index).Value
                .SellingPrice = r.Cells(cSPrice.Index).Value
                .Number = r.Cells(cNumber.Index).Value
            End With
            lstGoods.Add(newGoods)
        Next
        Return lstGoods.ToArray
    End Function


    Private Sub btCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCheck.Click
        If Work = Mode.Create Then
            '新增銷貨單
            DB.CreateSales(GetFormSales(), GetFormGoodsList())
        Else
            '更新銷貨單
            DB.ChangeSales(GetFormSales(), GetFormGoodsList())
        End If


        Me.Close()

    End Sub



End Class