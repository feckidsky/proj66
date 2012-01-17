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
    End Sub

    Public Overloads Sub Create()
        If Not CheckAuthority(2) Then Exit Sub

        Personnel = CurrentUser
        txtPersonnel.Text = Personnel.Name
        Dim sales As Sales = GetNewSales()
        txtLabel.Text = sales.Label
        txtOrderDate.Text = sales.OrderDate.ToString("yyyy/MM/dd HH:mm:ss")
        Work = Mode.Create
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


    Private Sub btCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOrder.Click
        Dim sales As Sales = GetFormSales()

        If sales.TypeOfPayment <> TypeOfPayment.Commission Then
            MsgBox("訂單的付款方式必須是訂金", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Work = Mode.Create Then
            '新增銷貨單
            DB.CreateSales(sales, GetFormGoodsList())
        Else
            '更新銷貨單
            DB.ChangeSales(sales, GetFormGoodsList())
        End If
        Me.Close()
    End Sub


    Private Sub btSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSales.Click
        Dim sales As Sales = GetFormSales()
        If sales.TypeOfPayment = TypeOfPayment.Commission Then
            MsgBox("尚未選擇付款方式", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If txtSalesDate.Text = "" Then txtSalesDate.Text = Now.ToString("yyyy/MM/dd HH:mm:ss")
        sales = GetFormSales()


        If Work = Mode.Create Then
            DB.CreateSales(sales, GetFormGoodsList)
        Else
            DB.ChangeSales(sales, GetFormGoodsList())
        End If
        Me.Close()
    End Sub

    Private Sub txtPersonnel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPersonnel.Click

        If Not Personnel.IsNull() AndAlso winLogIn.ShowDialog("此銷售人員的帳號密碼", Personnel.ID).State <> LoginState.Success Then
            Exit Sub
        End If

        If winLogIn.ShowDialog("請登入欲修改的銷售帳號").State = LoginState.Success Then
            Personnel = CurrentUser
            txtPersonnel.Text = Personnel.Name
        End If

        'Dim per As Database.Personnel = winPersonnelList.SelectDialog()
        'If Not per.IsNull() Then
        '    Personnel = per
        '    txtPersonnel.Text = per.Name
        'End If

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
End Class