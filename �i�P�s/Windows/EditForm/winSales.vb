Imports 進銷存.Database.StructureBase

Public Class winSales

    Enum Form
        Order = 0
        Sales = 1
    End Enum

    Enum Mode
        Create = 0
        Edit = 1
    End Enum

    Dim Work As Mode

    Dim FormKind As Form

    Dim Customer As Database.Customer = Database.Customer.Null()
    Dim Personnel As Database.Personnel = Database.Personnel.Null()

    Structure GoodsInfo
        Dim GoodsLabel As String
        Dim StockLabel As String
        Dim Name As String
        Dim Brand As String
        Dim Kind As String
        Dim Price As Single
        Dim SellingPrice As Single
        Dim Number As Integer

        'Dim Goods As Goods
        'Dim Sales As SalesGoods
    End Structure


    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        cbPayMode.Items.AddRange(TypeOfPaymentsDescribe)
        cbPayMode.SelectedIndex = TypeOfPayment.Commission
    End Sub


    Private Sub winSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btOrder.Text = IIf(Work = Mode.Create, "新增", "更新")
        txtLabel.Enabled = Work = Mode.Create

        FormKind = IIf(txtSalesDate.Text = "", Form.Order, Form.Sales)
        Me.Text = IIf(FormKind = Form.Order, "訂單", "銷貨單")

        btOrder.Text = IIf(Work = Mode.Create, "新增訂單", "修改訂單")
        btOrder.Enabled = FormKind = Form.Order

        btSales.Text = IIf(Work = Mode.Create Or FormKind = Form.Order, "銷貨", "修改銷貨單")


        TabControl1.SelectedTab = IIf(FormKind = Form.Order, tpOrder, tpSales)
        tpOrder.BackColor = ToColor(Config.OrderBackcolor)
        tpSales.BackColor = ToColor(Config.SalesBackColor)
    End Sub

    Public Overloads Sub Create()
        If Not CheckAuthority(2) Then Exit Sub

        Personnel = CurrentUser
        txtPersonnel.Text = Personnel.Name
        Dim sales As Sales = GetNewSales()
        txtLabel.Text = sales.Label
        txtOrderDate.Text = sales.OrderDate.ToString("yyyy/MM/dd HH:mm:ss")
        Work = Mode.Create
        tpOrder.BackColor = ToColor(Config.OrderBackcolor)
        tpSales.BackColor = ToColor(Config.SalesBackColor)
        MyBase.Show()
    End Sub


    Public Sub Open(ByVal Label As String)
        Dim sales As Sales = DB.GetSales(Label)
        Open(sales)
    End Sub


    Public Sub Open(ByVal Sales As Sales)

        txtLabel.Text = Sales.Label
        txtOrderDate.Text = Sales.OrderDate.ToString("yyyy/MM/dd HH:mm:ss")
        txtSalesDate.Text = IIf(Sales.SalesDate = New Date(2001, 1, 1, 0, 0, 0), "", Sales.SalesDate.ToString("yyyy/MM/dd HH:mm:ss"))
        txtNote.Text = Sales.Note
        txtDeposit.Text = Sales.Deposit
        cbPayMode.SelectedIndex = Sales.TypeOfPayment

        Personnel = DB.GetPersonnelByLabel(Sales.PersonnelLabel)
        Customer = DB.GetCustomerByLabel(Sales.CustomerLabel)

        txtPersonnel.Text = Personnel.Name
        txtCustomer.Text = Customer.Name

        ReadSalesList(Sales.Label)
        ReadOrderList(Sales.Label)
        ReadContractList(Sales.Label)
        Work = Mode.Edit
        MyBase.Show()
        CalTotalPrice()
    End Sub

    Private Sub ReadSalesList(ByVal SalesLabel As String)
        Dim dt As Data.DataTable = DB.GetGoodsListBySalesLabelWithHistoryPrice(SalesLabel) 'DB.GetGoodsListBySalesLabel(SalesLabel)

        For Each r As Data.DataRow In dt.Rows
            dgSalesList.Rows.Add(r.ItemArray())
        Next
        'CalSalesTotal()
    End Sub


    Private Sub ReadOrderList(ByVal SalesLabel As String)
        Dim dt As Data.DataTable = DB.GetOrderListBySalesLabel(SalesLabel)

        For Each r As Data.DataRow In dt.Rows
            dgOrderList.Rows.Add(r.ItemArray())
        Next
        'CalOrderTotal()

    End Sub

    Private Sub ReadContractList(ByVal SalesLabel As String)
        Dim dt As Data.DataTable = DB.GetContractListBySalesLabel(SalesLabel)

        For Each r As Data.DataRow In dt.rows
            dgContract.Rows.Add(r.ItemArray())
        Next
    End Sub

    Private Sub btAddGood_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddSalesItem.Click
        Dim row As DataGridViewRow = winStockList.SelectStock()
        If row Is Nothing Then Exit Sub
        For Each tmp As DataGridViewRow In dgSalesList.Rows
            If row.Cells("庫存編號").Value = tmp.Cells(cSLabel.Index).Value Then
                MsgBox("該商品已在清單中...")
                Exit Sub
            End If
        Next

        If row IsNot Nothing Then
            dgSalesList.Rows.Add(New String() {row.Cells("商品編號").Value, row.Cells("庫存編號").Value, row.Cells("種類").Value, row.Cells("廠牌").Value, row.Cells("品名").Value, row.Cells("售價").Value, row.Cells("售價").Value, 1})
        End If
        CalTotalPrice()
    End Sub

    Private Sub btDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDeleteSalesItem.Click
        If dgSalesList.SelectedCells.Count >= 1 Then
            Dim idx As Integer = dgSalesList.SelectedCells.Item(0).RowIndex
            If (idx < dgSalesList.Rows.Count) Then dgSalesList.Rows.RemoveAt(idx)
        End If
        CalTotalPrice()
    End Sub

    Private Function CalSalesTotal() As Single
        Dim total As Integer = 0
        Dim sub_total As Integer = 0
        For Each row As DataGridViewRow In dgSalesList.Rows
            sub_total = row.Cells(cSSellingPrice.Index).Value * row.Cells(cSNumber.Index).Value
            row.Cells(cSSubTotal.Index).Value = sub_total
            total += sub_total
        Next

        Return total '+ GetContractTotal()
        'lbSalesTotal.Text = total
    End Function

    Private Function CalOrderTotal() As Single
        Dim total As Integer = 0
        Dim sub_total As Integer = 0
        For Each row As DataGridViewRow In dgOrderList.Rows
            sub_total = row.Cells(cOSellingPrice.Index).Value * row.Cells(cONumber.Index).Value
            row.Cells(cOSubTotal.Index).Value = sub_total
            total += sub_total
        Next

        Return total '+ GetContractTotal()
        'lbSalesTotal.Text = total
    End Function

    Private Sub dgSalesList_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgSalesList.CellDoubleClick
        Dim GoodsLabel As String = dgSalesList.Rows(dgSalesList.SelectedCells(0).RowIndex).Cells(cSGoods.Index).Value
        Dim row As DataGridViewRow = winStockList.SelectStock(GoodsLabel)
        If row Is Nothing Then Exit Sub

        With dgSalesList.Rows(e.RowIndex)
            .Cells(cSLabel.Index).Value = row.Cells("庫存編號").Value
            .Cells(cSKind.Index).Value = row.Cells("種類").Value
            .Cells(cSName.Index).Value = row.Cells("品名").Value
            .Cells(cSBrand.Index).Value = row.Cells("廠牌").Value
            .DefaultCellStyle.BackColor = dgSalesList.DefaultCellStyle.BackColor
        End With
    End Sub


    Private Sub dgSales_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgSalesList.CellEndEdit
        If e.ColumnIndex = cSSellingPrice.Index Or e.ColumnIndex = cSNumber.Index Then CalTotalPrice()
    End Sub


    '取得銷貨單資訊
    Public Function GetSalesInfo() As Sales
        Dim newSales As Sales
        With newSales
            .Label = txtLabel.Text
            .OrderDate = Date.ParseExact(txtOrderDate.Text, "yyyy/MM/dd HH:mm:ss", Nothing)
            Date.TryParseExact(txtSalesDate.Text, "yyyy/MM/dd HH:mm:ss", Nothing, Globalization.DateTimeStyles.None, .SalesDate)
            .Deposit = Val(txtDeposit.Text)
            .Note = txtNote.Text
            .CustomerLabel = Customer.Label
            .PersonnelLabel = Personnel.Label
            .TypeOfPayment = cbPayMode.SelectedIndex
        End With
        Return newSales
    End Function


    '取得銷貨清單
    Public Function GetSalseList() As SalesGoods()
        Dim lstGoods As New List(Of SalesGoods)
        Dim newGoods As SalesGoods

        For Each r As DataGridViewRow In dgSalesList.Rows
            If r.Cells(cSLabel.Index).Value Is Nothing Then Continue For
            If r.Cells(cSLabel.Index).Value = "" Then Continue For
            With newGoods
                .SalesLabel = txtLabel.Text
                .StockLabel = r.Cells(cSLabel.Index).Value
                .SellingPrice = r.Cells(cSSellingPrice.Index).Value
                .Number = r.Cells(cSNumber.Index).Value
            End With
            lstGoods.Add(newGoods)
        Next
        Return lstGoods.ToArray
    End Function


    '取得銷貨清單
    Public Function GetOrderList() As OrderGoods()
        Dim lstGoods As New List(Of OrderGoods)

        For Each r As DataGridViewRow In dgOrderList.Rows
            If r.Cells(cOLabel.Index).Value Is Nothing Then Continue For
            lstGoods.Add(OrderRow2OrderGoods(r))
        Next
        Return lstGoods.ToArray
    End Function

    Private Function OrderRow2GoodsInfo(ByVal r As DataGridViewRow) As GoodsInfo
        Dim item As New GoodsInfo
        item.GoodsLabel = r.Cells(cOLabel.Index).Value
        item.Kind = r.Cells(cOKind.Index).Value
        item.Brand = r.Cells(cOBrand.Index).Value
        item.Name = r.Cells(cOName.Index).Value
        item.Number = r.Cells(cONumber.Index).Value
        item.SellingPrice = r.Cells(cOSellingPrice.Index).Value
        item.StockLabel = ""
        item.Price = GetSingle(r.Cells(cOPrice.Index).Value)
        Return item

    End Function

    Private Function OrderRow2OrderGoods(ByVal r As DataGridViewRow) As OrderGoods
        Dim newGoods As OrderGoods
        With newGoods
            .SalesLabel = txtLabel.Text
            .GoodsLabel = r.Cells(cOLabel.Index).Value
            .Price = r.Cells(cOSellingPrice.Index).Value
            .Number = r.Cells(cONumber.Index).Value
            .PurchaseLabel = ""
        End With
        Return newGoods
    End Function

    Private Function OrderRow2Goods(ByVal r As DataGridViewRow) As Goods
        Dim item As New Goods
        item.Label = r.Cells(cOLabel.Index).Value
        item.Kind = r.Cells(cOKind.Index).Value
        item.Brand = r.Cells(cOBrand.Index).Value
        item.Name = r.Cells(cOName.Index).Value
        Return item
    End Function

    Public Function GetContractList() As SalesContract()
        Dim lst As New List(Of SalesContract)
        Dim item As SalesContract

        For Each r As DataGridViewRow In dgContract.Rows
            With item
                .SalesLabel = txtLabel.Text
                .ContractLabel = r.Cells(cCLabel.Index).Value
                .Discount = r.Cells(cCDiscount.Index).Value
                .Phone = r.Cells(cCPhone.Index).Value
            End With
            lst.Add(item)
        Next
        Return lst.ToArray()
    End Function


    Private Sub btOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOrder.Click
        Dim sales As Sales = GetSalesInfo()

        If sales.TypeOfPayment <> TypeOfPayment.Commission Then
            MsgBox("訂單的付款方式必須是訂金", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Work = Mode.Create Then
            '新增銷貨單
            DB.CreateSales(sales, GetSalseList(), GetOrderList(), GetContractList())
        Else
            '更新銷貨單
            DB.ChangeSales(sales, GetSalseList(), GetOrderList(), GetContractList())
        End If
        Me.Close()
    End Sub


    Private Sub btSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSales.Click

        Dim lstOrder As New List(Of String)
        For Each r As DataGridViewRow In dgSalesList.Rows
            If r.Cells(cSLabel.Index).Value = "" Then
                lstOrder.Add(Trim(r.Cells(cSName.Index).Value))
            End If
        Next
        If lstOrder.Count > 0 Then
            If MsgBox("以下訂單項目尚未處理，您確定要轉銷貨？" & vbCrLf & Join(lstOrder.ToArray, ",") & "。", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                Exit Sub
            End If
        End If

        Dim sales As Sales = GetSalesInfo()
        If sales.TypeOfPayment = TypeOfPayment.Commission Then
            MsgBox("尚未選擇付款方式", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If txtSalesDate.Text = "" Then txtSalesDate.Text = Now.ToString("yyyy/MM/dd HH:mm:ss")
        sales = GetSalesInfo()


        If Work = Mode.Create Then
            DB.CreateSales(sales, GetSalseList(), GetOrderList(), GetContractList())
        Else
            sales.SalesDate = Now
            DB.ChangeSales(sales, GetSalseList(), GetOrderList(), GetContractList())
        End If
        Me.Close()
    End Sub

    Private Sub txtPersonnel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPersonnel.Click

        'If Not Personnel.IsNull() AndAlso winLogIn.ShowDialog("此銷售人員的帳號密碼", Personnel.ID).State <> LoginState.Success Then
        '    Exit Sub
        'End If

        'If winLogIn.ShowDialog("請登入欲修改的銷售帳號").State = LoginState.Success Then
        '    Personnel = CurrentUser
        '    txtPersonnel.Text = Personnel.Name
        'End If

        Dim per As Database.Personnel = winPersonnelList.SelectDialog()
        If Not per.IsNull() Then
            Personnel = per
            txtPersonnel.Text = per.Name
        End If

    End Sub

    Private Sub txtCustomer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomer.Click
        Dim cur As Database.Customer = winCustomerList.SelectDialog()
        If Not cur.IsNull() Then
            Customer = cur
            txtCustomer.Text = cur.Name
        End If
    End Sub


    Private Sub btResetPersonnel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btResetPersonnel.Click

        If winLogIn.ShowDialog("請重新登入銷售帳號", Personnel.ID).State = LoginState.Success Then
            Personnel = CurrentUser
            txtPersonnel.Text = Personnel.Name
            Personnel = Database.Personnel.Null()
            txtPersonnel.Text = Personnel.Name
        End If


    End Sub

    Private Sub btResetCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btResetCustomer.Click
        Customer = Database.Customer.Null()
        txtCustomer.Text = Customer.Name
    End Sub


    Private Sub cbPayMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPayMode.SelectedIndexChanged
        Me.BackColor = IIf(cbPayMode.SelectedIndex = 2, ToColor(Config.OrderBackcolor), ToColor(Config.SalesBackColor))

    End Sub

    Private Sub btAddOrderItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddOrderItem.Click
        Dim newGoods As Database.Goods = winGoodsList.SelectDialog()
        If newGoods.IsNull() Then Exit Sub


        For Each tmp As DataGridViewRow In dgOrderList.Rows
            If newGoods.Label = tmp.Cells(cOLabel.Index).Value Then
                MsgBox("該商品已在清單中...")
                Exit Sub
            End If
        Next

        Dim dt As Data.DataTable = DB.GetGoodsWithPrice(newGoods.Label)
        Dim row As Data.DataRow = dt.Rows(0)
        If row IsNot Nothing Then
            dgOrderList.Rows.Add(New String() {row("Label"), row("Kind"), row("Brand"), row("Name"), GetSingle(row("Price")), GetSingle(row("Price")), 1})
        End If
        CalTotalPrice()
    End Sub


    Private Function GetSingle(ByVal obj As Object) As Single
        If obj Is DBNull.Value Then Return 0
        Return Val(obj)
    End Function

    Private Sub dgOrderList_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgOrderList.CellEndEdit
        If e.ColumnIndex = cOSellingPrice.Index Or e.ColumnIndex = cONumber.Index Then CalTotalPrice()
    End Sub

    Private Sub btDeleteOrderItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDeleteOrderItem.Click
        If dgOrderList.SelectedCells.Count >= 1 Then
            Dim idx As Integer = dgOrderList.SelectedCells.Item(Me.cOLabel.Index).RowIndex
            If (idx < dgOrderList.Rows.Count) Then dgOrderList.Rows.RemoveAt(idx)
        End If
        CalTotalPrice()
    End Sub

    Private Sub CalTotalPrice()
        Dim GoodsTotal As Single
        If TabControl1.SelectedTab Is tpOrder Then
            GoodsTotal = CalOrderTotal()
        Else
            GoodsTotal = CalSalesTotal()
        End If
        Dim contractTotal As ContractTotal = GetContractTotal()

        Dim msg As String = ""
        If GoodsTotal > 0 Then msg &= GoodsTotal & "(商品)"
        If contractTotal.Prepay > 0 Then msg &= " + " & contractTotal.Prepay & "(預付額)"
        If contractTotal.Discount > 0 Then msg &= " - " & contractTotal.Discount & "(折扣)"

        Dim Deposit As Single = 0

        Try
            Deposit = Val(txtDeposit.Text)
        Catch ex As Exception
        End Try


        'If Deposit > 0 Then msg &= " - " & txtDeposit.Text & "(訂金)"
        Dim total As Single = GoodsTotal + contractTotal.Prepay - contractTotal.Discount
        lbTotal.Text = "金額:  " & msg & " = " & total
        lbRealTotal.Text = "應付金額:  " & total & " - " & Deposit & "(訂金) = " & (total - Deposit)

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab Is Me.tpSales Then TransToSales()
        CalTotalPrice()
    End Sub

    Private Sub TransToSales()
        For Each row As DataGridViewRow In dgOrderList.Rows
            If Not SalesItemExist(row.Cells(cOLabel.Index).Value) Then
                Dim item As GoodsInfo = OrderRow2GoodsInfo(row)
                Dim idx As Integer = dgSalesList.Rows.Add(item.GoodsLabel, "", item.Kind, item.Brand, item.Name, item.Price, item.SellingPrice, item.Number)
                dgSalesList.Rows(idx).DefaultCellStyle.BackColor = Color.Red
            End If
        Next


        'CalTotalPrice()
    End Sub

    Private Function SalesItemExist(ByVal GoodsLabel As String) As Boolean
        For Each row As DataGridViewRow In dgSalesList.Rows
            If row.Cells(cSGoods.Index).Value = GoodsLabel Then Return True
        Next
        Return False
    End Function


    Private Sub btAddContract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddContract.Click
        Dim item As Database.Contract = winContractList.SelectEffectDialog()
        If item.IsNull() Then Exit Sub
        Dim row As New DataGridViewRow
        dgContract.Rows.Add(New String() {item.Label, item.Name, item.Prepay, item.Discount, ""})
        CalTotalPrice()
    End Sub

    Private Sub btDeleteContract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDeleteContract.Click
        If dgContract.SelectedCells.Count = 0 Then Exit Sub
        Dim idx As Integer = dgContract.SelectedCells(0).RowIndex
        dgContract.Rows.RemoveAt(idx)
        CalTotalPrice()
    End Sub


    Structure ContractTotal
        Dim Prepay As Single
        Dim Discount As Single
    End Structure

    Private Function GetContractTotal() As ContractTotal
        Dim subTotal As Single = 0
        Dim result As ContractTotal
        result.Discount = 0
        result.Prepay = 0

        For Each row As DataGridViewRow In dgContract.Rows
            result.Prepay += row.Cells(cCPrepay.Index).Value
            result.Discount += row.Cells(cCDiscount.Index).Value
            subTotal += row.Cells(cCPrepay.Index).Value - row.Cells(cCDiscount.Index).Value
        Next

        Return result
    End Function

    Private Sub dgContract_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgContract.CellEndEdit
        CalTotalPrice()
    End Sub

    Private Sub txtDeposit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDeposit.TextChanged
        CalTotalPrice()
    End Sub
End Class