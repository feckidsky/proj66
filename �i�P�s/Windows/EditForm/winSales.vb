Imports 進銷存.Database.DatabaseType
Imports 進銷存.Database

Public Class winSales

    Enum Form
        None = -1
        Order = 0
        Sales = 1
        [Return] = 2
    End Enum

    Enum Mode
        Create = 0
        Edit = 1
    End Enum

    Dim Work As Mode

    Dim FormKind As Form

    Dim Customer As Database.Customer = Database.Customer.Null()
    Dim Personnel As Database.Personnel = Database.Personnel.Null()

    Dim access As Database.Access

    Structure GoodsInfo
        Dim GoodsLabel As String
        Dim StockLabel As String
        Dim Name As String
        Dim Brand As String
        Dim Kind As String
        Dim Cost As Single
        Dim Price As Single
        Dim SellingPrice As Single
        Dim Number As Integer

        'Dim Goods As Goods
        'Dim Sales As SalesGoods
    End Structure


    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()
        Me.DefaultTextBoxImeMode()
        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        cbPayMode.Items.AddRange(Sales.PaymentDescribe)
        cbPayMode.SelectedIndex = Payment.Deposit
    End Sub


    Private Sub winSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btOrder.Text = IIf(Work = Mode.Create, "新增", "更新")
        txtLabel.Enabled = Work = Mode.Create


        'If FormKind = Form.None Then
        '    FormKind = IIf(txtSalesDate.Text = "", Form.Order, Form.Sales)
        'End If

        If FormKind = Form.None Then
            If dgReturnList.RowCount > 0 Then
                FormKind = Form.Return
            ElseIf dgSalesList.RowCount > 0 Then
                FormKind = Form.Sales
            Else
                FormKind = Form.Order
            End If
        End If


        Me.Text = IIf(FormKind = Form.Order, "訂單", "銷貨單")

        btOrder.Text = IIf(Work = Mode.Create, "新增訂單", "儲存訂單")
        'btOrder.Enabled = FormKind = Form.Order

        btSales.Text = IIf(Work = Mode.Create Or FormKind = Form.Order, "銷貨", "儲存銷貨單")

        Select Case FormKind
            Case Form.Order : TabControl1.SelectedTab = tpOrder
            Case Form.Sales : TabControl1.SelectedTab = tpSales
            Case Form.Return : TabControl1.SelectedTab = tpReturn
        End Select

        'TabControl1.SelectedTab = IIf(FormKind = Form.Order, tpOrder, tpSales)
        tpOrder.BackColor = ToColor(Config.OrderBackcolor)
        tpSales.BackColor = ToColor(Config.SalesBackColor)
    End Sub

    Public Overloads Sub Create(ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub

        Personnel = CurrentUser
        txtPersonnel.Text = Personnel.Name
        Dim sales As Sales = GetNewSales()
        txtLabel.Text = sales.Label
        txtOrderDate.Text = sales.OrderDate.ToString("yyyy/MM/dd HH:mm:ss")
        txtDepositCharge.Text = 2
        txtPayCharge.Text = 2
        Work = Mode.Create
        tpOrder.BackColor = ToColor(Config.OrderBackcolor)
        tpSales.BackColor = ToColor(Config.SalesBackColor)
        MyBase.Show()
    End Sub

    ReadOnly Property SalesLabel() As String
        Get
            Return Strings.Trim(txtLabel.Text)
        End Get
    End Property

    Public Shared Sub Open(ByVal Label As String, ByVal DB As Database.Access, Optional Kind As Form=Form.None)
        'access = DB
        'Dim sales As Sales = access.GetSales(Label)

        For Each f As Windows.Forms.Form In My.Application.OpenForms
            If f.GetType Is GetType(winSales) Then
                Dim w As winSales = f
                If w.SalesLabel = Strings.Trim(Label) And w.access Is DB Then
                    f.Show()
                    f.BringToFront()
                    Exit Sub
                End If
            End If
        Next

        Dim sales As Sales = DB.GetSales(Label)
        Dim win As New winSales
        win.Open(sales, DB, Kind)
    End Sub


    Public Sub Open(ByVal Sales As Sales, ByVal DB As Database.Access, Optional ByVal Kind As Form = Form.None)
        access = DB
        txtLabel.Text = Sales.Label
        txtOrderDate.Text = Sales.OrderDate.ToString("yyyy/MM/dd HH:mm:ss")
        txtSalesDate.Text = IIf(Sales.SalesDate = New Date(2001, 1, 1, 0, 0, 0) Or Sales.SalesDate = Nothing, "", Sales.SalesDate.ToString("yyyy/MM/dd HH:mm:ss"))
        txtNote.Text = Sales.Note
        txtDeposit.Text = Sales.DepositByCash
        txtDepositByCard.Text = Sales.DepositByCard
        txtPayByCard.Text = Sales.PayByCard
        txtPayByCash.Text = Sales.PayByCash
        txtDepositCharge.Text = Sales.DepositCardCharge * 100
        txtPayCharge.Text = Sales.PayCardCharge * 100
        cbPayMode.SelectedIndex = Sales.TypeOfPayment

        Personnel = DB.GetPersonnelByLabel(Sales.PersonnelLabel)
        Customer = DB.GetCustomerByLabel(Sales.CustomerLabel)

        txtPersonnel.Text = Personnel.Name
        txtCustomer.Text = Customer.Name

        ReadSalesList(Sales.Label)
        ReadOrderList(Sales.Label)
        ReadReturnList(Sales.Label)
        ReadContractList(Sales.Label)
        Work = Mode.Edit
        FormKind = Kind

        MyBase.Show()
        CalTotalPrice()
    End Sub

    Private Sub ReadSalesList(ByVal SalesLabel As String)
        Dim dt As Data.DataTable = access.GetGoodsListBySalesLabelWithHistoryPrice(SalesLabel) 'DB.GetGoodsListBySalesLabel(SalesLabel)

        For Each r As Data.DataRow In dt.Rows

            dgSalesList.Rows.Add(New Object() {r("Label"), r("StockLabel"), r("Kind"), r("Brand"), r("Name"), r("Cost"), r("Price"), r("SellingPrice"), r("Number"), Nothing}) ', r("SalesDate")})  'r.ItemArray())
        Next
        'CalSalesTotal()
    End Sub

    Private Sub ReadReturnList(ByVal SalesLabel As String)
        Dim dt As DataTable = access.GetReturnListBySalesLabel(SalesLabel)
        For Each r As Data.DataRow In dt.Rows
            dgReturnList.Rows.Add(r.ItemArray)
        Next
    End Sub


    Private Sub ReadOrderList(ByVal SalesLabel As String)
        Dim dt As Data.DataTable = access.GetOrderListBySalesLabel(SalesLabel)

        For Each r As Data.DataRow In dt.Rows
            dgOrderList.Rows.Add(r.ItemArray())
        Next
        'CalOrderTotal()

    End Sub

    Private Sub ReadContractList(ByVal SalesLabel As String)
        Dim dt As Data.DataTable = access.GetContractListBySalesLabel(SalesLabel)

        For Each r As Data.DataRow In dt.rows
            dgContract.Rows.Add(r.ItemArray())
        Next
    End Sub

    Private Sub btAddGood_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddSalesItem.Click
        Dim row As DataGridViewRow = winStockList.SelectStock(access)
        If row Is Nothing Then Exit Sub
        For Each tmp As DataGridViewRow In dgSalesList.Rows
            If row.Cells("庫存編號").Value = tmp.Cells(cSLabel.Index).Value Then
                MsgBox("該商品已在清單中...")
                Exit Sub
            End If
        Next

        If row IsNot Nothing Then
            dgSalesList.Rows.Add(New Object() {row.Cells("商品編號").Value, row.Cells("庫存編號").Value, row.Cells("種類").Value, row.Cells("廠牌").Value, row.Cells("品名").Value, GetSingle(row.Cells("進價").Value), GetSingle(row.Cells("售價").Value), GetSingle(row.Cells("售價").Value), 1, Nothing, Now})
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

    Private Function CalReturnTotal() As Single
        Dim total As Integer = 0
        Dim sub_total As Integer = 0
        For Each row As DataGridViewRow In dgReturnList.Rows
            sub_total = row.Cells(cRReturnPrice.Index).Value * row.Cells(cRNumber.Index).Value
            row.Cells(cRSubTotal.Index).Value = sub_total
            total += sub_total
        Next
        Return total
    End Function



    Private Sub dgSalesList_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgSalesList.CellDoubleClick
        'Dim GoodsLabel As String = dgSalesList.Rows(dgSalesList.SelectedCells(0).RowIndex).Cells(cSGoods.Index).Value
        'Dim row As DataGridViewRow = winStockList.SelectStock(GoodsLabel, access)
        'If row Is Nothing Then Exit Sub

        'With dgSalesList.Rows(e.RowIndex)
        '    .Cells(cSLabel.Index).Value = row.Cells("庫存編號").Value
        '    .Cells(cSKind.Index).Value = row.Cells("種類").Value
        '    .Cells(cSName.Index).Value = row.Cells("品名").Value
        '    .Cells(cSBrand.Index).Value = row.Cells("廠牌").Value
        '    .DefaultCellStyle.BackColor = dgSalesList.DefaultCellStyle.BackColor
        'End With


        SelectSalesItem(e.RowIndex)


    End Sub

    Private Sub dgSalesList_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgSalesList.CellClick
        If e.RowIndex < 0 Or e.ColumnIndex <> cSSalesDate.Index Then Exit Sub
        DialogTime.Value = dgSalesList.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
        If DialogTime.ShowDialog = Windows.Forms.DialogResult.OK Then
            dgSalesList.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = DialogTime.Value
        End If
    End Sub

    Private Sub SelectSalesItem(ByVal RowIndex As Integer)
        If RowIndex = -1 Or RowIndex >= dgSalesList.Rows.Count Then Exit Sub
        Dim GoodsLabel As String = dgSalesList.Rows(RowIndex).Cells(cSGoods.Index).Value
        Dim row As DataGridViewRow = winStockList.SelectStock(GoodsLabel, access)
        If row Is Nothing Then Exit Sub

        With row ' dgSalesList.Rows(e.RowIndex)
            .Cells(cSLabel.Index).Value = row.Cells("庫存編號").Value
            .Cells(cSKind.Index).Value = row.Cells("種類").Value
            .Cells(cSName.Index).Value = row.Cells("品名").Value
            .Cells(cSBrand.Index).Value = row.Cells("廠牌").Value
            .DefaultCellStyle.BackColor = dgSalesList.DefaultCellStyle.BackColor
        End With
    End Sub


    Private Sub dgSales_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgSalesList.CellEndEdit
        Dim cell As DataGridViewCell = dgSalesList.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If e.ColumnIndex = cSSellingPrice.Index Or e.ColumnIndex = cSNumber.Index Then
            Dim tmpV As Single
            If Not Single.TryParse(cell.Value, tmpV) Then
                cell.Value = 0
            End If
        End If

        If e.ColumnIndex = cSNumber.Index Then
            CheckStockNumber(dgSalesList.Rows(e.RowIndex).Cells(cSLabel.Index).Value, dgSalesList.Rows(e.RowIndex))
        End If
        If e.ColumnIndex = cSSellingPrice.Index Or e.ColumnIndex = cSNumber.Index Then
            CalTotalPrice()
        End If

    End Sub

    Private Sub CheckStockNumber(ByVal StockLabel As String, ByVal row As DataGridViewRow)
        Dim dt As DataTable = access.GetStockListWithHistoryPrice(StockLabel, txtLabel.Text)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            MsgBox("查無庫存資料")
            Exit Sub
        End If

        Dim goods As SalesGoods = GetSalesItem(row)
        If goods.IsNull Then Exit Sub

        Dim StockNumber As Integer = dt.Rows(0).Item("數量")
        If goods.Number > StockNumber Then
            MsgBox("[" & row.Cells(cSName.Index).Value & "] 庫存不足 " & vbCrLf & " 庫存量:" & StockNumber, MsgBoxStyle.Exclamation)
            row.Cells(cSNumber.Index).Value = StockNumber
        End If

    End Sub


    '取得銷貨單資訊
    Public Function GetSalesInfo() As Sales
        Dim newSales As Sales
        With newSales
            .Label = txtLabel.Text
            .TypeOfPayment = cbPayMode.SelectedIndex
            .OrderDate = Date.ParseExact(txtOrderDate.Text, "yyyy/MM/dd HH:mm:ss", Nothing)
            Date.TryParseExact(txtSalesDate.Text, "yyyy/MM/dd HH:mm:ss", Nothing, Globalization.DateTimeStyles.None, .SalesDate)
            If .TypeOfPayment = Payment.Deposit Then .SalesDate = Nothing
            .DepositByCash = Val(txtDeposit.Text)
            .DepositByCard = Val(txtDepositByCard.Text)
            .PayByCash = Val(txtPayByCash.Text)
            .PayByCard = Val(txtPayByCard.Text)
            .PayCardCharge = Val(txtPayCharge.Text) / 100
            .DepositCardCharge = Val(txtDepositCharge.Text) / 100
            .Note = txtNote.Text
            .CustomerLabel = Customer.Label
            .PersonnelLabel = Personnel.Label

        End With
        Return newSales
    End Function


    '取得銷貨清單
    Public Function GetSalseList() As SalesGoods()
        Dim lstGoods As New List(Of SalesGoods)
        Dim newGoods As SalesGoods

        For Each r As DataGridViewRow In dgSalesList.Rows
            newGoods = GetSalesItem(r)
            If Not newGoods.IsNull Then lstGoods.Add(newGoods)
        Next
        Return lstGoods.ToArray
    End Function

    Public Function GetSalesItem(ByVal r As DataGridViewRow) As SalesGoods
        If r.Cells(cSLabel.Index).Value Is Nothing OrElse r.Cells(cSLabel.Index).Value = "" Then Return SalesGoods.Null
        Dim newGoods As SalesGoods
        With newGoods
            .SalesLabel = txtLabel.Text
            .StockLabel = r.Cells(cSLabel.Index).Value
            .SellingPrice = r.Cells(cSSellingPrice.Index).Value
            .Number = r.Cells(cSNumber.Index).Value
            '.SalesDate = r.Cells(cSSalesDate.Index).Value
        End With
        Return newGoods
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
        item.SellingPrice = GetSingle(r.Cells(cOSellingPrice.Index).Value)
        item.Cost = GetSingle(r.Cells(cOCost.Index).Value)
        item.StockLabel = ""
        item.Price = GetSingle(r.Cells(cOPrice.Index).Value)
        Return item

    End Function

    Private Function OrderRow2OrderGoods(ByVal r As DataGridViewRow) As OrderGoods
        Dim newGoods As OrderGoods
        With newGoods
            .SalesLabel = txtLabel.Text
            .GoodsLabel = r.Cells(cOLabel.Index).Value
            .Price = GetSingle(r.Cells(cOSellingPrice.Index).Value)
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

    Public Function GetReturnList() As ReturnGoods()
        Dim lst As New List(Of ReturnGoods)
        Dim item As ReturnGoods
        For Each r As DataGridViewRow In dgReturnList.Rows
            'item.ReturnDate = txtSalesDate.Text
            item.ReturnLabel = txtLabel.Text
            item.SalesLabel = r.Cells(cRSalesLabel.Index).Value
            item.ReturnPrice = r.Cells(cRReturnPrice.Index).Value
            item.StockLabel = r.Cells(cRStockLabel.Index).Value
            item.Number = r.Cells(cRNumber.Index).Value
            lst.Add(item)
        Next
        Return lst.ToArray()
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
                .Commission = r.Cells(cCCommission.Index).Value
                .ReturnDate = GetDate(r.Cells(cCReturnDate.Index).Value)
            End With
            lst.Add(item)
        Next
        Return lst.ToArray()
    End Function

    Private Function GetDate(ByVal obj As Object) As Date
        'If obj Is DBNull.Value Then Return Nothing
        'Return obj
        Try
            Return obj
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Private Sub btOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOrder.Click
        Dim sales As Sales = GetSalesInfo()

        If sales.TypeOfPayment <> Payment.Deposit Then
            MsgBox("訂單的付款方式必須是訂金", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If dgSalesList.RowCount > 0 Then
            If MsgBox("此單已有銷貨項目，儲存此訂單將會刪除這些項目，您確定要儲存？", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                Exit Sub
            Else
                dgSalesList.Rows.Clear()
            End If
        End If

        If Work = Mode.Create Then
            '新增銷貨單
            access.CreateSales(sales, GetSalseList(), GetOrderList(), GetReturnList(), GetContractList())
        Else
            '更新銷貨單
            access.ChangeSales(sales, GetSalseList(), GetOrderList(), GetReturnList(), GetContractList())
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
        'If sales.TypeOfPayment = Payment.Deposit Then
        '    MsgBox("尚未選擇付款方式", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If

        If sales.TypeOfPayment = Payment.Cancel And dgSalesList.RowCount > 0 Then
            If MsgBox("此單已有銷貨項目，退訂將會刪除這些項目，您確定要儲存？", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                Exit Sub
            Else
                dgSalesList.Rows.Clear()
            End If
        End If

        If txtSalesDate.Text = "" Then txtSalesDate.Text = Now.ToString("yyyy/MM/dd HH:mm:ss")
        sales = GetSalesInfo()


        If Work = Mode.Create Then
            access.CreateSales(sales, GetSalseList(), GetOrderList(), GetReturnList(), GetContractList())
        Else
            'sales.SalesDate = Now
            access.ChangeSales(sales, GetSalseList(), GetOrderList(), GetReturnList(), GetContractList())
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

        Dim per As Database.Personnel = winPersonnelList.SelectDialog(access)
        If Not per.IsNull() Then
            Personnel = per
            txtPersonnel.Text = per.Name
        End If

    End Sub

    Private Sub txtCustomer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomer.Click
        Dim cur As Database.Customer = winCustomerList.SelectDialog(access)
        If Not cur.IsNull() Then
            Customer = cur
            txtCustomer.Text = cur.Name
        End If
    End Sub


    Private Sub btResetPersonnel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btResetPersonnel.Click

        'If winLogIn.ShowDialog("請重新登入銷售帳號", Personnel.ID).State = LoginState.Success Then
        'Personnel = CurrentUser
        'txtPersonnel.Text = Personnel.Name
        Personnel = Database.Personnel.Null()
        txtPersonnel.Text = Personnel.Name
        'End If


    End Sub

    Private Sub btResetCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btResetCustomer.Click
        Customer = Database.Customer.Null()
        txtCustomer.Text = Customer.Name
    End Sub


    Private Sub cbPayMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPayMode.SelectedIndexChanged
        Me.BackColor = IIf(cbPayMode.SelectedIndex = Payment.Cancel, Color.Yellow, IIf(cbPayMode.SelectedIndex = 2, ToColor(Config.OrderBackcolor), ToColor(Config.SalesBackColor)))
        btOrder.Enabled = cbPayMode.SelectedIndex = Payment.Deposit
        btSales.Enabled = cbPayMode.SelectedIndex = Payment.Finish Or cbPayMode.SelectedIndex = Payment.Cancel
    End Sub

    Private Sub btAddOrderItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddOrderItem.Click
        Dim newGoods As Database.Goods = winGoodsList.SelectDialog(access)
        If newGoods.IsNull() Then Exit Sub


        For Each tmp As DataGridViewRow In dgOrderList.Rows
            If newGoods.Label = tmp.Cells(cOLabel.Index).Value Then
                MsgBox("該商品已在清單中...")
                Exit Sub
            End If
        Next

        Dim dt As Data.DataTable = access.GetGoodsWithPrice(newGoods.Label)
        Dim row As Data.DataRow = dt.Rows(0)
        If row IsNot Nothing Then
            dgOrderList.Rows.Add(New String() {row("Label"), row("Kind"), row("Brand"), row("Name"), GetSingle(row("Cost")), GetSingle(row("Price")), GetSingle(row("Price")), 1, Now})
        End If
        CalTotalPrice()
    End Sub


    Private Function GetSingle(ByVal obj As Object) As Single
        If obj Is DBNull.Value Then Return 0
        Return Val(obj)
    End Function

    Private Sub dgOrderList_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgOrderList.CellEndEdit
        Dim cell As DataGridViewCell = dgOrderList.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If e.ColumnIndex = cOSellingPrice.Index Or e.ColumnIndex = cONumber.Index Then
            Dim tmpV As Single
            If Not Single.TryParse(cell.Value, tmpV) Then
                cell.Value = 0
            End If
        End If
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
        Dim returnTotal As Single = CalReturnTotal()

        Dim msg As String = ""
        If GoodsTotal > 0 Then msg &= GoodsTotal & "(商品)"
        If returnTotal > 0 Then msg &= " - " & returnTotal & "(退貨)"
        If contractTotal.Prepay > 0 Then msg &= " + " & contractTotal.Prepay & "(預付額)"
        If contractTotal.Discount > 0 Then msg &= " - " & contractTotal.Discount & "(折扣)"

        Dim Deposit As Single = 0

        Try
            Deposit = Val(txtDeposit.Text) + Val(txtDepositByCard.Text)
        Catch ex As Exception
        End Try


        'If Deposit > 0 Then msg &= " - " & txtDeposit.Text & "(訂金)"
        Dim total As Single = GoodsTotal - returnTotal + contractTotal.Prepay - contractTotal.Discount
        lbTotal.Text = "金額:  " & msg & " = " & total
        lbRealTotal.Text = "應付金額:  " & total & " - " & Deposit & "(訂金) = " & (total - Deposit)

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        CalTotalPrice()
    End Sub

    Private Sub TransToSales()

        If MsgBox("要將訂單內容匯到銷貨單嗎？", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub


        For Each row As DataGridViewRow In dgOrderList.Rows
            Dim GoodsLabel As String = row.Cells(cOLabel.Index).Value


            If Not SalesItemExist(GoodsLabel) Then
                Dim dt As DataTable
ReadStockList:
                dt = access.GetStockListByGoodsLabel(GoodsLabel)

                Dim item As GoodsInfo = OrderRow2GoodsInfo(row)

                If dt Is Nothing OrElse dt.Rows.Count = 0 Then '沒有庫存
                    '新增庫存
                    If MsgBox(item.Name & "目前沒有庫存，現在要新增嗎？", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        If winStockIn.Create(access, item.GoodsLabel) = Windows.Forms.DialogResult.OK Then GoTo ReadStockList
                    End If

                    Dim idx As Integer = dgSalesList.Rows.Add(item.GoodsLabel, "", item.Kind, item.Brand, item.Name, item.Cost, item.Price, item.SellingPrice, item.Number)
                    dgSalesList.Rows(idx).DefaultCellStyle.BackColor = Color.Red
                ElseIf dt.Rows.Count = 1 Then '有一筆庫存
                    Dim r As DataRow = dt.Rows(0)
                    Dim num As Integer = IIf(item.Number > r("數量"), r("數量"), item.Number)
                    Dim idx As Integer = dgSalesList.Rows.Add(item.GoodsLabel, r("庫存編號"), item.Kind, item.Brand, item.Name, r("進價"), r("售價"), item.SellingPrice, num)
                    If item.Number > r("數量") Then dgSalesList.Rows(idx).DefaultCellStyle.BackColor = Color.Red
                Else '有兩筆以上的庫存
                    MsgBox(item.Name & "有" & dt.Rows.Count & "筆庫存，請選擇其中一筆!", MsgBoxStyle.Information, "銷貨提示")
                    Dim r As DataGridViewRow = winStockList.SelectStock(GoodsLabel, access)
                    Dim idx As Integer
                    If r Is Nothing Then
                        idx = dgSalesList.Rows.Add(item.GoodsLabel, "", item.Kind, item.Brand, item.Name, item.Cost, item.Price, item.SellingPrice, item.Number)
                        dgSalesList.Rows(idx).DefaultCellStyle.BackColor = Color.Red
                    Else
                        Dim num As Integer = IIf(item.Number > r.Cells("數量").Value, r.Cells("數量").Value, item.Number)
                        idx = dgSalesList.Rows.Add(item.GoodsLabel, r.Cells("庫存編號").Value, item.Kind, item.Brand, item.Name, r.Cells("進價").Value, r.Cells("售價").Value, item.SellingPrice, num)
                        If item.Number > r.Cells("數量").Value Then dgSalesList.Rows(idx).DefaultCellStyle.BackColor = Color.Red
                    End If


                End If
            End If
        Next


        CalTotalPrice()
    End Sub

    Private Function SalesItemExist(ByVal GoodsLabel As String) As Boolean
        For Each row As DataGridViewRow In dgSalesList.Rows
            If row.Cells(cSGoods.Index).Value = GoodsLabel Then Return True
        Next
        Return False
    End Function


    Private Sub btAddContract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddContract.Click
        Dim item As Database.Contract = winContractList.SelectEffectDialog(access)
        If item.IsNull() Then Exit Sub
        Dim row As New DataGridViewRow
        dgContract.Rows.Add(New String() {item.Label, item.Name, item.Prepay, item.Commission, item.Discount, "", Nothing})
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

    Private Function GetDbDate(ByVal t As Date) As Object
        If t = Nothing Then Return DBNull.Value
        Return t
    End Function

    '修改退佣日期
    Private Sub dgContract_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgContract.CellClick
        If e.RowIndex < 0 Or e.ColumnIndex <> cCReturnDate.Index Then Exit Sub
        DialogTime.Value = GetDate(dgContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
        If DialogTime.ShowDialog = Windows.Forms.DialogResult.OK Then
            dgContract.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = GetDbDate(DialogTime.Value)
        End If
    End Sub

    Private Sub dgContract_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgContract.CellDoubleClick

        If e.RowIndex < 0 Then Exit Sub
        Dim row As DataGridViewRow = dgContract.Rows(e.RowIndex)

        Dim item As Database.Contract = winContractList.SelectEffectDialog(access)
        If item.IsNull() Then Exit Sub

        'Dim row As New DataGridViewRow
        'dgContract.Rows.Add(New String() {item.Label, item.Name, item.Prepay, item.Commission, item.Discount, "", Now})

        row.Cells(cCLabel.Index).Value = item.Label
        row.Cells(cCName.Index).Value = item.Name
        row.Cells(cCPrepay.Index).Value = item.Prepay
        row.Cells(cCCommission.Index).Value = item.Commission
        row.Cells(cCDiscount.Index).Value = item.Discount

        CalTotalPrice()
    End Sub

    '編輯過合約
    Private Sub dgContract_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgContract.CellEndEdit
        CalTotalPrice()
    End Sub

    '修改訂金
    Private Sub txtDeposit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDeposit.TextChanged, txtDepositByCard.TextChanged, txtPayByCash.TextChanged, txtPayByCard.TextChanged, txtPayCharge.TextChanged, txtDepositCharge.TextChanged
        CalTotalPrice()
    End Sub

    '訂單轉出銷貨單
    Private Sub btOrder2Sales_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOrder2Sales.Click
        TransToSales()
    End Sub

    '訂單日期/銷貨日期修改
    Private Sub txtDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrderDate.Click, txtSalesDate.Click
        DialogTime.Value = GetDate(sender.text) 'CType(sender.Text, Date)
        If DialogTime.ShowDialog = Windows.Forms.DialogResult.OK Then
            sender.Text = DialogTime.Value.ToString("yyyy/MM/dd HH:mm:ss")
        End If
    End Sub

    Private Function GetDate(ByVal text As String) As Date
        Try
            Return text
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    Private Sub btAddReturnGoods_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddReturnGoods.Click
        Dim result As DataGridViewRow = winSalesGoodsList.ShowDialog(access)
        If result Is Nothing Then Exit Sub

        For Each row As DataGridViewRow In dgReturnList.Rows
            If row.Cells(cRStockLabel.Index).Value = result.Cells("庫存編號").Value Then
                MsgBox("所選擇的項目已在退貨清單中")
                Exit Sub
            End If
        Next

        With result
            dgReturnList.Rows.Add(.Cells("商品編號").Value, .Cells("銷貨編號").Value, .Cells("庫存編號").Value, .Cells("種類").Value, .Cells("廠牌").Value, .Cells("品名").Value, .Cells("成本").Value, .Cells("賣價").Value, .Cells("賣價").Value, .Cells("數量").Value)
        End With

        CalTotalPrice()
    End Sub

    Private Sub btDelReturnGoods_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDelReturnGoods.Click
        If dgReturnList.SelectedCells.Count = 0 Then
            MsgBox("你必須選擇一個項目")
            Exit Sub
        End If

        If MsgBox("確定要刪除這個退貨項目？", MsgBoxStyle.Question + MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then Exit Sub


        Dim idx As Integer = dgReturnList.SelectedCells.Item(0).RowIndex
        If (idx < dgReturnList.Rows.Count) Then dgReturnList.Rows.RemoveAt(idx)

        'Dim delSales As New List(Of DataGridViewRow)
        'For i As Integer = 0 To dgReturnList.SelectedRows.Count - 1
        '    delSales.Add(dgReturnList.SelectedRows(i))
        'Next

        'For i As Integer = 0 To delSales.Count - 1
        '    dgReturnList.Rows.Remove(delSales(i))
        'Next
        CalTotalPrice()
    End Sub

    Public Function GetReturnGoods(ByVal row As DataGridViewRow) As Database.ReturnGoods
        Dim item As Database.ReturnGoods
        item.ReturnLabel = txtLabel.Text
        item.Number = row.Cells(cRNumber.Index).Value
        item.ReturnPrice = row.Cells(cRReturnPrice.Index).Value
        item.SalesLabel = row.Cells(cRSalesLabel.Index).Value
        item.StockLabel = row.Cells(cRStockLabel.Index).Value
        Return item
    End Function

    Private Sub dgReturnList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgReturnList.CellContentClick

    End Sub

    Private Sub dgReturnList_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgReturnList.CellEndEdit
        If e.ColumnIndex = cRNumber.Index Or e.ColumnIndex = cRReturnPrice.Index Then CalTotalPrice()
    End Sub


    Private Sub dgSalesList_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgSalesList.RowsAdded
        UpdateTableText(tpSales, "銷貨單", dgSalesList)
    End Sub

    Private Sub dgSalesList_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgSalesList.RowsRemoved
        UpdateTableText(tpSales, "銷貨單", dgSalesList)
    End Sub


    Private Sub dgOrderList_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgOrderList.RowsAdded
        UpdateTableText(tpOrder, "訂單", dgOrderList)
    End Sub

    Private Sub dgOrderList_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgOrderList.RowsRemoved
        UpdateTableText(tpOrder, "訂單", dgOrderList)
    End Sub

    Private Sub dgReturnList_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgReturnList.RowsAdded
        UpdateTableText(tpReturn, "退貨單", dgReturnList)
    End Sub

    Private Sub dgReturnList_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgReturnList.RowsRemoved
        UpdateTableText(tpReturn, "退貨單", dgReturnList)
    End Sub

    Private Sub UpdateTableText(ByVal tp As TabPage, ByVal Name As String, ByVal dgList As DataGridView)
        tp.Text = Name & GetCountText(dgList.RowCount)
    End Sub


    Private Function GetCountText(ByVal count As Integer) As String
        If count = 0 Then Return ""
        Return "(" & count & ")"
    End Function

End Class