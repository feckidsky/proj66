Namespace Database

    Public Structure Column
        Dim Name As String
        Dim Type As String
        Dim HaveIndex As Boolean
        Sub New(ByVal Name As String, ByVal Type As String, Optional ByVal HaveIndex As Boolean = False)
            Me.Name = Name : Me.Type = Type : Me.HaveIndex = HaveIndex
        End Sub
    End Structure

    Public Structure SalesArgs
        Dim Sales As Sales
        Dim GoodsList() As SalesGoods
        Dim OrderList() As OrderGoods
        Dim SalesContracts() As SalesContract
        Sub New(ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal SalesContracts() As SalesContract)
            Me.Sales = sales
            Me.GoodsList = GoodsList
            Me.OrderList = OrderList
            Me.SalesContracts = SalesContracts
        End Sub

    End Structure


    Public Structure LoginResult
        Dim User As Personnel
        Dim State As LoginState
        Dim msg As String
        Dim Client As Access

        Sub New(ByVal state As LoginState, ByVal msg As String, ByVal user As Personnel, ByVal Client As Access)
            Me.State = state : Me.msg = msg : Me.User = user : Me.Client = Client
        End Sub
    End Structure

    Public Enum LoginState
        IdError = 0
        PasswordError = 1
        Success = 2
        Disconnect = 3
    End Enum

#Region "Access"
    Public Class Access
        Inherits TCPTool.Client
        Public Name As String = "DefaultName"
        Private Shared Lock As String = "Lock"

        Public User As Personnel = Personnel.Guest

        Event Account_Logout(ByVal sender As Object, ByVal result As LoginResult)
        Event Account_LogIn(ByVal sender As Object, ByVal result As LoginResult)

        Event CreatedContract(ByVal sender As Object, ByVal con As Contract)
        Event ChangedContract(ByVal sender As Object, ByVal con As Contract)
        Event DeletedContract(ByVal sender As Object, ByVal con As Contract)

        Event CreatedGoods(ByVal sender As Object, ByVal goods As Goods)
        Event ChangedGoods(ByVal sender As Object, ByVal goods As Goods)
        Event DeletedGoods(ByVal sender As Object, ByVal goods As Goods)

        Event CreatedSupplier(ByVal sender As Object, ByVal sup As Supplier)
        Event ChangedSupplier(ByVal sender As Object, ByVal sup As Supplier)
        Event DeletedSupplier(ByVal sender As Object, ByVal sup As Supplier)

        Event CreatedCustomer(ByVal sender As Object, ByVal cus As Customer)
        Event ChangedCustomer(ByVal sender As Object, ByVal cus As Customer)
        Event DeletedCustomer(ByVal sender As Object, ByVal cus As Customer)

        Event CreatedPersonnel(ByVal sender As Object, ByVal per As Personnel)
        Event ChangedPersonnel(ByVal sender As Object, ByVal per As Personnel)
        Event DeletedPersonnel(ByVal sender As Object, ByVal per As Personnel)

        Event CreatedStock(ByVal sender As Object, ByVal stock As Stock)
        Event ChangedStock(ByVal sender As Object, ByVal stock As Stock)
        Event DeletedStock(ByVal sender As Object, ByVal stock As Stock)

        Event CreatedSales(ByVal sender As Object, ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal SalesContracts() As SalesContract)
        Event ChangedSales(ByVal sender As Object, ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal SalesContracts() As SalesContract)
        Event DeletedSales(ByVal sender As Object, ByVal sales As Sales)

        Event CreatedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice)
        Event ChangedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice)
        Event DeletedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice)
        Event DeletedHistoryPriceList(ByVal sender As Object, ByVal hp As HistoryPrice)

        Public Sub New(ByVal Name As String)
            Me.Name = Name
            Dir = My.Application.Info.DirectoryPath & "\data"
            BasePath = Dir & "\base.mdb"
            SalesPath = Dir & "\sales.mdb"
        End Sub

        Public Overloads Function Connected() As Boolean
            If Me.GetType Is GetType(Access) Then Return True
            Return MyBase.Connected()
        End Function

        'Public Sub New()
        '    Dir = My.Application.Info.DirectoryPath & "\data"
        '    BasePath = Dir & "\base.mdb"
        '    SalesPath = Dir & "\sales.mdb"
        'End Sub

        Public Function LogIn(ByVal ID As String, ByVal Password As String, Optional ByVal TriggerEvent As Boolean = True) As LoginResult

            Dim result As LoginResult

            Dim r_user As Personnel = GetPersonnelByID(ID)
            If r_user.IsNull() Then
                result = New LoginResult(LoginState.IdError, "帳號不存在!", Personnel.Guest, Me)
            ElseIf r_user.Password <> Password Then
                result = New LoginResult(LoginState.PasswordError, "密碼錯誤!", Personnel.Guest, Me)
            Else
                result = New LoginResult(LoginState.Success, "登入成功!", r_user, Me)
                'CurrentUser = user
            End If

            User = result.User
            If TriggerEvent Then OnLogin(result)
            Return result
        End Function

        Friend Overridable Sub OnLogin(ByVal result As LoginResult)
            RaiseEvent Account_LogIn(Me, result)
        End Sub

        Public Overridable Sub LogOut(Optional ByVal TriggerEvent As Boolean = True)
            Dim result As New LoginResult(LoginState.Success, "已經登出!", Personnel.Guest, Me)
            If TriggerEvent Then RaiseEvent Account_Logout(Me, result)
        End Sub

        Public Function GetKindList() As String()
            Dim SqlCommand As String = "SELECT Kind FROM goods Group By Kind;"
            Dim dt As DataTable = Read("table", BasePath, SqlCommand)
            Dim lst As New List(Of String)
            For Each r As DataRow In dt.Rows
                lst.Add(Strings.Trim(r("Kind").ToString))
            Next
            Return lst.ToArray
        End Function

        Public Function GetBrandList() As String()
            Dim SqlCommand As String = "SELECT Brand FROM goods Group By Brand;"
            Dim dt As DataTable = Read("table", BasePath, SqlCommand)
            Dim lst As New List(Of String)
            For Each r As DataRow In dt.Rows
                lst.Add(Strings.Trim(r("Brand").ToString))
            Next
            Return lst.ToArray
        End Function

        Public Function GetLogList(ByVal StartTime As Date, ByVal EndTime As Date, ByVal Personnel As String) As DataTable
            Dim between As String = "(Log.Date BETWEEN " & StartTime.ToString("#yyyy/MM/dd HH:mm:ss#") & " AND " & EndTime.ToString("#yyyy/MM/dd HH:mm:ss#") & ")"
            Dim perCondition As String = IIf(Personnel = "", "", "AND Log.Personnel='" & Personnel & "'")
            Dim SqlCommand As String = "SELECT Log.Date as 日期,Log.Personnel as 員工編號, Personnel.Name as 員工 ,Log.Message as 內容 FROM Log Left Join Personnel ON Log.personnel=Personnel.Label WHERE" & between & perCondition & ";"
            Return Read("table", BasePath, SqlCommand)
        End Function

        Public Function GetLogRow(ByVal Time As Date, ByVal Personnel As String) As DataRow
            Dim between As String = "(Log.Date =" & Time.ToString("#yyyy/MM/dd HH:mm:ss#") & ")"
            Dim perCondition As String = IIf(Personnel = "", "", "AND Log.Personnel='" & Personnel & "'")
            Dim SqlCommand As String = "SELECT Log.Date as 日期,Log.Personnel as 員工編號, Personnel.Name as 員工 ,Log.Message as 內容 FROM Log Left Join Personnel ON Log.personnel=Personnel.Label WHERE" & between & perCondition & ";"
            Dim dt As DataTable = Read("table", BasePath, SqlCommand)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return Nothing
            Return dt.Rows(0)
        End Function

        Public Function GetHistoryPriceList(ByVal Label As String) As Data.DataTable
            Dim SqlCommand As String = "SELECT * FROM " & HistoryPrice.Table & " WHERE GoodsLabel='" & Label & "';"
            Return Read("table", BasePath, SqlCommand)
        End Function

        Public Function GetHistoryPriceList() As DataTable
            Dim SqlCommand As String = "SELECT * FROM " & HistoryPrice.Table & ";"
            Return Read("table", BasePath, SqlCommand)
        End Function

        Public Function GetListHistoryPrice(ByVal Label As String) As HistoryPrice
            Dim SqlCommand As String = "SELECT HistoryPrice.GoodsLabel, HistoryPrice.Time, HistoryPrice.Cost, HistoryPrice.Price " & _
            " FROM (SELECT HistoryPrice.GoodsLabel, Max(HistoryPrice.Time) AS [Time] FROM HistoryPrice GROUP BY HistoryPrice.GoodsLabel HAVING (HistoryPrice.GoodsLabel='" & Label & "'))  AS tmp LEFT JOIN HistoryPrice ON (tmp.Time=HistoryPrice.Time) AND (tmp.GoodsLabel=HistoryPrice.GoodsLabel)" & _
            " GROUP BY HistoryPrice.GoodsLabel, HistoryPrice.Time, HistoryPrice.Cost, HistoryPrice.Price;"
            Dim dt As DataTable = Read("table", BasePath, SqlCommand)
            If dt.Rows.Count = 0 Then Return HistoryPrice.Null()
            Return HistoryPrice.GetFrom(dt.Rows(0))
        End Function

        Public Function GetGoodsList() As Data.DataTable
            Dim SQLCommand As String = "SELECT * FROM " & Goods.Table & ";"
            Return Read("table", BasePath, SQLCommand)
        End Function

        Public Function GetGoods(ByVal Label As String) As Goods
            Dim SQLCommand As String = "SELECT * FROM " & Goods.Table & " WHERE Label='" & Label & "';"
            Dim dt As DataTable = Read("table", BasePath, SQLCommand)
            If dt.Rows.Count = 0 Then Return Goods.Null()
            Return Goods.GetFrom(dt.Rows(0))
        End Function

        Public Function GetGoodsWithPrice(ByVal Label As String) As Data.DataTable
            Dim SqlCommand As String = "SELECT Goods.Label, Goods.Kind, Goods.Brand, Goods.Name, history.Price " & _
            " FROM (SELECT HistoryPrice.GoodsLabel, HistoryPrice.Price FROM (SELECT HistoryPrice.GoodsLabel, Max(HistoryPrice.Time) AS Time1 FROM HistoryPrice GROUP BY HistoryPrice.GoodsLabel)  AS tmp LEFT JOIN HistoryPrice ON (tmp.Time1=HistoryPrice.Time) AND (tmp.GoodsLabel=HistoryPrice.GoodsLabel))  AS history RIGHT JOIN Goods ON history.GoodsLabel = Goods.Label " & _
            " WHERE(Goods.Label = '" & Label & "') " & _
            " GROUP BY Goods.Label, Goods.Kind, Goods.Brand, Goods.Name, history.Price; "
            Return Read("table", BasePath, SqlCommand)
        End Function

        Public Function GetCustomerList() As Data.DataTable
            Dim name() As String = Array.ConvertAll(Change(Customer.ToColumns(), "Addr", "Note"), Function(c As Column) c.Name)
            Dim SQLCommand As String = "SELECT " & Join(name, ",") & " FROM " & Customer.Table & ";"
            Return Read("table", BasePath, SQLCommand)
        End Function

        Public Function GetCustomerByLabel(ByVal Label As String) As Customer
            Dim SQLCommand As String = "SELECT * FROM " & Customer.Table & " WHERE Label='" & Label & "';"
            Dim dt As DataTable = Read("table", BasePath, SQLCommand)

            If dt.Rows.Count = 0 Then Return Customer.Null()
            Return Customer.GetFrom(dt.Rows(0))
        End Function

        Public Function Change(ByVal columns() As Column, ByVal Name1 As String, ByVal Name2 As String) As Column()
            Dim c1 As Integer = Array.FindIndex(columns, Function(c As Column) c.Name = Name1)
            Dim c2 As Integer = Array.FindIndex(columns, Function(c As Column) c.Name = Name2)
            Dim tmp As Column = columns(c1)
            columns(c1) = columns(c2)
            columns(c2) = tmp
            Return columns
        End Function

        Public Function GetSupplierList() As Data.DataTable
            Dim name() As String = Array.ConvertAll(Change(Supplier.ToColumns(), "Addr", "Note"), Function(c As Column) c.Name)
            Dim SQLCommand As String = "SELECT " & Join(name, ",") & " FROM " & Supplier.Table & ";"
            Return Read("table", BasePath, SQLCommand)
        End Function

        Public Function GetSupplier(ByVal Label As String) As Supplier
            Dim SQLCommand As String = "SELECT * FROM " & Supplier.Table & " WHERE Label='" & Label & "';"
            Dim dt As DataTable = Read("table", BasePath, SQLCommand)
            If dt.Rows.Count = 0 Then Return Supplier.Null()
            Return Supplier.GetFrom(dt.Rows(0))

        End Function

        Event ReadedContractList(ByVal sender As Object, ByVal DataTable As DataTable)

        Public Sub BeginGetContractList()
            Dim Thread As New Threading.Thread(AddressOf GetContractList)
            Thread.Start()
        End Sub

        Public Function GetContractList() As Data.DataTable
            Dim SQLCommand As String = "SELECT * FROM " & Contract.Table & ";"
            Dim dt As DataTable = Read("table", BasePath, SQLCommand)
            RaiseEvent ReadedContractList(Me, dt)
            Return dt
        End Function

        Public Function GetSalesListByContractLabel(ByVal Label As String) As Data.DataTable
            Dim SqlCommand As String = "SELECT Contract.Label, Contract.Name, Contract.Discount, Contract.Prepay, Contract.Note " & _
            " FROM (Sales INNER JOIN SalesContract ON Sales.Label = SalesContract.SalesLabel) LEFT JOIN Contract ON SalesContract.ContractLabel = Contract.Label " & _
            " WHERE Contract.Label='" & Label & "'"
            Return Read("table", BasePath, SqlCommand)
        End Function

        Public Function GetPersonnelList() As Data.DataTable
            Dim name() As String = Array.ConvertAll(Change(Personnel.ToColumns(), "Addr", "Note"), Function(c As Column) c.Name)
            Dim SQLCommand As String = "SELECT " & Join(name, ",") & " FROM " & Personnel.Table & ";"
            Return Read("table", BasePath, SQLCommand)
        End Function

        Public Function GetPersonnelByID(ByVal ID As String) As Personnel
            Dim SQLCommand As String = "SELECT * FROM " & Personnel.Table & " WHERE ID='" & ID & "';"
            Dim dt As DataTable = Read("table", BasePath, SQLCommand)

            If dt.Rows.Count = 0 Then Return Personnel.Null()
            Return Personnel.GetFrom(dt.Rows(0))
        End Function

        Public Function GetPersonnelByLabel(ByVal Label As String) As Personnel
            Dim SQLCommand As String = "SELECT * FROM " & Personnel.Table & " WHERE Label='" & Label & "';"
            Dim dt As DataTable = Read("table", BasePath, SQLCommand)

            If dt.Rows.Count = 0 Then Return Personnel.Null()
            Return Personnel.GetFrom(dt.Rows(0))
        End Function

        '更新庫存內容
        Public Overridable Sub ChangeStock(ByVal newStock As Stock)
            Dim goods As Goods = GetGoods(newStock.GoodsLabel)
            Dim stock As Stock = GetStock(newStock.Label)
            Dim SQLCommand As String = newStock.GetUpdateSqlCommand()
            Command(SQLCommand, BasePath)
            AddLog(Now, "修改庫存資料:" & goods.Name & IIf(stock.Number = newStock.Number, "", "(數量:" & stock.Number & "->" & newStock.Number & ")"))
            OnChangedStock(newStock)
        End Sub

        '刪除一筆庫存
        Public Overridable Sub DeleteStock(ByVal dStock As Stock)
            Dim SQLCommand As String = "DELETE FROM " & Stock.Table & " WHERE Label='" & dStock.Label & "';"
            Command(SQLCommand, BasePath)
            Dim goods As Goods = GetGoods(dStock.GoodsLabel)
            AddLog(Now, "刪除庫存:" & goods.Name)
            OnDeletedStock(dStock)
        End Sub

        '讀取庫存資料
        Public Function GetStock(ByVal Label As String) As Stock
            Dim SQLCommand As String = "SELECT * FROM " & Stock.Table & " WHERE Label='" & Label & "';"
            Dim DT As Data.DataTable = Read("table", BasePath, SQLCommand)

            Dim data As New Stock
            If DT.Rows.Count > 0 Then data = Stock.GetFrom(DT.Rows(0))
            Return data
        End Function



        ''' <summary>取得庫存清單</summary>
        Public Function GetStockList() As Data.DataTable
            'Dims SQLCommand As String = "SELECT stock.Label as 庫存編號,IMEI,Kind as 種類, Brand as 廠牌, [Name] as 品名,  Number as 數量 , Price as 售價, stock.Note as 備註 FROM stock LEFT JOIN goods AS [a] ON stock.GoodsLabel = [a].Label;"
            Dim SqlCommand As String = "SELECT Goods.Label as 商品編號, stock.label AS 庫存編號, stock.IMEI, Goods.Kind AS 種類, Goods.Brand AS 廠牌, Goods.Name AS 品名, [stock].[number]-IIf(IsNull([nn]),0,[nn]) AS 數量, stock.Note AS 備註 " & _
            " FROM (stock LEFT JOIN (SELECT StockLabel,sum(number) as nn  From SalesGoods Group By StockLabel )  AS cc ON stock.Label = cc.StockLabel) LEFT JOIN Goods ON stock.GoodsLabel = Goods.Label " & _
            " WHERE ((([stock].[number]-IIf(IsNull([nn]),0,[nn]))>0));"


            Dim DT As Data.DataTable = Read("table", BasePath, SqlCommand)
            Return DT
        End Function

        ''' <summary>取得庫存清單</summary>
        Public Function GetStockListWithHistoryPrice() As Data.DataTable

            Dim SqlCommand As String = "SELECT stock.GoodsLabel as 商品編號, stock.label AS 庫存編號, stock.IMEI, Goods.Kind AS 種類, Goods.Brand AS 廠牌, Goods.Name AS 品名, [stock].[number]-IIf(IsNull([nn]),0,[nn]) AS 數量, history.Price AS 售價, stock.Note AS 備註 " & _
            " FROM ((stock LEFT JOIN (SELECT StockLabel,sum(number) as nn  From SalesGoods Group By StockLabel )  AS cc ON stock.Label = cc.StockLabel) LEFT JOIN (SELECT HistoryPrice.GoodsLabel, HistoryPrice.Price FROM (SELECT HistoryPrice.GoodsLabel, Max(HistoryPrice.Time) AS Time1 FROM HistoryPrice GROUP BY HistoryPrice.GoodsLabel)  AS tmp LEFT JOIN HistoryPrice ON (tmp.Time1=HistoryPrice.Time) AND (tmp.GoodsLabel=HistoryPrice.GoodsLabel))  AS history ON stock.GoodsLabel = history.GoodsLabel) INNER JOIN Goods ON stock.GoodsLabel = Goods.Label " & _
            " WHERE ((([stock].[number]-IIf(IsNull([nn]),0,[nn]))>0));"



            Dim DT As Data.DataTable = Read("table", BasePath, SqlCommand)
            Return DT
        End Function

        Public Function GetStockMoveList(ByVal StartTime As Date, ByVal EndTime As Date) As Data.DataTable
            Dim SqlCommand As String = "SELECT StockMove.Label AS 調貨編號, StockMove.StockLabel AS 庫存編號, StockMove.Date AS 調貨日期, Supplier.Name AS 供應商, Goods.Kind AS 種類, Goods.Brand AS 廠牌, StockMove.IMEI, Goods.Name AS 品名, StockMove.Cost AS 進貨價, StockMove.Number AS 數量, StockMove.SourceShop as 來源, Personnel.Name as 出貨, StockMove.DestineShop as 目地, Personnel_1.Name as [申請/收件] , StockMove.Action AS 狀態 " & _
            "FROM (((StockMove LEFT JOIN Goods ON StockMove.GoodsLabel = Goods.Label) LEFT JOIN Supplier ON StockMove.SupplierLabel = Supplier.Label) LEFT JOIN Personnel ON StockMove.SourcePersonnel = Personnel.Label) LEFT JOIN Personnel AS Personnel_1 ON StockMove.DestinePersonnel = Personnel_1.Label WHERE StockMove.Date BETWEEN " & StartTime.ToString("#yyyy/MM/dd HH:mm:ss#") & " AND " & EndTime.ToString("#yyyy/MM/dd HH:mm:ss#") & " ; "
            Return Read("table", BasePath, SqlCommand)
        End Function

        Public Function GetStockMoveRow(ByVal StockMoveLabel As String) As DataRow
            Dim SqlCommand As String = "SELECT StockMove.Label AS 調貨編號, StockMove.StockLabel AS 庫存編號, StockMove.Date AS 調貨日期, Supplier.Name AS 供應商, Goods.Kind AS 種類, Goods.Brand AS 廠牌, StockMove.IMEI, Goods.Name AS 品名, StockMove.Cost AS 進貨價, StockMove.Number AS 數量, StockMove.SourceShop as 來源, Personnel.Name as 出貨, StockMove.DestineShop as 目地, Personnel_1.Name as [申請/收件] , StockMove.Action AS 狀態 " & _
           "FROM (((StockMove LEFT JOIN Goods ON StockMove.GoodsLabel = Goods.Label) LEFT JOIN Supplier ON StockMove.SupplierLabel = Supplier.Label) LEFT JOIN Personnel ON StockMove.SourcePersonnel = Personnel.Label) LEFT JOIN Personnel AS Personnel_1 ON StockMove.DestinePersonnel = Personnel_1.Label WHERE StockMove.Label='" & StockMoveLabel & "' ; "
            Try
                Return Read("table", BasePath, SqlCommand).Rows(0)
            Catch
                Return Nothing
            End Try
        End Function

        Public Function GetStockMove(ByVal Label As String) As StockMove
            Dim SQLCommand As String = "SELECT * FROM " & StockMove.Table & " WHERE Label='" & Label & "';"
            Dim dt As DataTable = Read("table", BasePath, SQLCommand)
            If dt.Rows.Count = 0 Then Return StockMove.Null()
            Return StockMove.GetFrom(dt.Rows(0))
        End Function


        ''' <summary>取得進貨記錄</summary>
        Public Function GetStockLog(ByVal StartTime As Date, ByVal EndTime As Date, Optional ByVal StockLabel As String = "") As Data.DataTable
            Dim StockCondition As String = ""
            If StockLabel <> "" Then StockCondition = " AND Stock.Label='" & StockLabel & "'"

            Dim SQLCommand As String = "SELECT Stock.Label as 庫存編號, Stock.Date as 進貨日期, Supplier.Name as 供應商, Goods.Kind as 種類, Goods.Brand as 廠牌, Stock.IMEI, Goods.Name as 品名, Stock.Cost as 進貨價,  Stock.Number as 數量, Stock.Note as 備註" & _
            " FROM (Stock LEFT JOIN Goods ON Stock.GoodsLabel = Goods.Label) LEFT JOIN Supplier ON Stock.SupplierLabel = Supplier.Label " & _
            " WHERE ((Stock.[date] Between #" & StartTime.ToString("yyyy/MM/dd HH:mm:ss") & "# And #" & EndTime.ToString("yyyy/MM/dd HH:mm:ss") & "#)" & StockCondition & ");"
            Dim DT As Data.DataTable = Read("table", BasePath, SQLCommand)
            Return DT
        End Function

        Public Function GetStockLogByGoodsLabel(ByVal label As String) As Data.DataTable
            Dim SQLCommand As String = "SELECT Stock.Label as 庫存編號, Stock.Date as 進貨日期, Goods.Kind as 種類, Goods.Brand as 廠牌, Stock.IMEI, Goods.Name as 品名, Stock.Cost as 進貨價,  Stock.Number as 數量, Stock.Note as 備註" & _
            " FROM (Stock LEFT JOIN Goods ON Stock.GoodsLabel = Goods.Label) LEFT JOIN Supplier ON Stock.SupplierLabel = Supplier.Label " & _
            " WHERE Stock.GoodsLabel='" & label & "';"
            Dim DT As Data.DataTable = Read("table", BasePath, SQLCommand)
            Return DT
        End Function

        Public Function GetStockLogBySupplierLabel(ByVal label As String) As Data.DataTable
            Dim SQLCommand As String = "SELECT Stock.Label as 庫存編號, Stock.Date as 進貨日期, Goods.Kind as 種類, Goods.Brand as 廠牌, Stock.IMEI, Goods.Name as 品名, Stock.Cost as 進貨價,  Stock.Number as 數量, Stock.Note as 備註" & _
            " FROM (Stock LEFT JOIN Goods ON Stock.GoodsLabel = Goods.Label) LEFT JOIN Supplier ON Stock.SupplierLabel = Supplier.Label " & _
            " WHERE Stock.SupplierLabel='" & label & "';"
            Dim DT As Data.DataTable = Read("table", BasePath, SQLCommand)
            Return DT
        End Function

        Public Function GetSalesListByCustomer(ByVal Customer As String) As Data.DataTable
            Dim SQLCommand As String = "SELECT sales.label AS 單號, sales.SalesDate AS 時間, Customer.Name AS 銷售人員, Personnel.Name AS 客戶, sales.TypeOfPayment AS 付款方式, sales.Deposit AS 訂金, tmp.金額, sales.note AS 備註" & _
            " FROM ((sales LEFT JOIN (SELECT SalesLabel, sum(SellingPrice*Number) AS 金額 FROM SalesGoods GROUP BY SalesLabel)  AS tmp ON sales.label = tmp.SalesLabel) LEFT JOIN Customer ON sales.CustomerLabel = Customer.Label) LEFT JOIN Personnel ON sales.PersonnelLabel = Personnel.Label " & _
            " WHERE (sales.CustomerLabel='" & Customer & "');"

            Dim DT As Data.DataTable = Read("table", BasePath, SQLCommand)
            Return DT
        End Function

        Public Function GetSalesListByPersonnel(ByVal Personnel As String) As Data.DataTable
            Dim SQLCommand As String = "SELECT sales.label AS 單號, sales.SalesDate AS 時間, Customer.Name AS 銷售人員, Personnel.Name AS 客戶, sales.TypeOfPayment AS 付款方式, sales.Deposit AS 訂金, tmp.金額, sales.note AS 備註" & _
            " FROM ((sales LEFT JOIN (SELECT SalesLabel, sum(SellingPrice*Number) AS 金額 FROM SalesGoods GROUP BY SalesLabel)  AS tmp ON sales.label = tmp.SalesLabel) LEFT JOIN Customer ON sales.CustomerLabel = Customer.Label) LEFT JOIN Personnel ON sales.PersonnelLabel = Personnel.Label " & _
            " WHERE (sales.PersonnelLabel='" & Personnel & "');"

            Dim DT As Data.DataTable = Read("table", BasePath, SQLCommand)
            Return DT
        End Function


        Enum GetSalesListType
            Order = 0
            Sales = 1
            Both = 2
        End Enum

        '讀取銷貨單
        Public Function GetSalesList(ByVal StartTime As Date, ByVal EndTime As Date, ByVal ListType As GetSalesListType) As Data.DataTable

            Dim cnd As String = ""
            If ListType = GetSalesListType.Order Then cnd = " AND TypeOfPayment=" & Payment.Commission
            If ListType = GetSalesListType.Sales Then cnd = " AND TypeOfPayment<>" & Payment.Commission
            Dim condition1 As String = " WHERE ((sales.Orderdate Between #" & StartTime.ToString("yyyy/MM/dd HH:mm:ss") & "# And #" & EndTime.ToString("yyyy/MM/dd HH:mm:ss") & "#) " & cnd & ") "

            'Dim condition2 As String = " TypeOfPayment=" & CType(PayType, Integer)
            Dim SQLCommand As String = "SELECT Sales.Label AS 單號, Sales.OrderDate AS 訂單時間, Sales.SalesDate AS 銷貨時間, Personnel.Name AS 銷售人員, Customer.Name AS 客戶, Sales.TypeOfPayment AS 付款方式, Sales.Deposit AS 訂金, Sum([SellingPrice]*[SalesGoods].[Number]) AS 金額, Sum(([SellingPrice]-[cost])*[SalesGoods].[Number]) AS 利潤, Sales.Note AS 備註 " & _
            " FROM ((Sales LEFT JOIN (SalesGoods LEFT JOIN Stock ON SalesGoods.StockLabel = Stock.Label) ON Sales.Label = SalesGoods.SalesLabel) LEFT JOIN Customer ON Sales.CustomerLabel = Customer.Label) LEFT JOIN Personnel ON Sales.PersonnelLabel = Personnel.Label " & _
            condition1 & _
            " GROUP BY Sales.Label, Sales.OrderDate, Sales.SalesDate, Personnel.Name, Customer.Name, Sales.TypeOfPayment, Sales.Deposit, Sales.Note; "


            Dim DT As Data.DataTable = Read("table", BasePath, SQLCommand)
            Return DT
        End Function

        '讀取銷貨單
        Public Function GetSalesListWithContract(ByVal StartTime As Date, ByVal EndTime As Date, ByVal ListType As GetSalesListType, Optional ByVal SalesLabel As String = "") As Data.DataTable

            Dim cnd As String = ""
            If ListType = GetSalesListType.Order Then cnd = " AND TypeOfPayment=" & Payment.Commission
            If ListType = GetSalesListType.Sales Then cnd = " AND TypeOfPayment<>" & Payment.Commission

            Dim OrderTime As String = "(sales.Orderdate Between #" & StartTime.ToString("yyyy/MM/dd HH:mm:ss") & "# And #" & EndTime.ToString("yyyy/MM/dd HH:mm:ss") & "#)"
            Dim SalesTime As String = "(sales.Salesdate Between #" & StartTime.ToString("yyyy/MM/dd HH:mm:ss") & "# And #" & EndTime.ToString("yyyy/MM/dd HH:mm:ss") & "#)"

            Dim LabelCnd As String = ""
            If SalesLabel <> "" Then LabelCnd = "AND Sales.Label='" & SalesLabel & "' "

            Dim condition1 As String

            Select Case ListType
                Case GetSalesListType.Order
                    condition1 = " WHERE ( " & OrderTime & cnd & LabelCnd & ") "
                Case GetSalesListType.Sales
                    condition1 = " WHERE ( " & SalesTime & cnd & LabelCnd & ") "
                Case Else
                    condition1 = " WHERE (( " & OrderTime & " OR " & SalesTime & ")" & cnd & LabelCnd & ") "
            End Select


            'Dim condition2 As String = " TypeOfPayment=" & CType(PayType, Integer)
            Dim SQLCommand As String = "SELECT Sales.Label AS 單號, Sales.OrderDate AS 訂單時間, Sales.SalesDate AS 銷貨時間, Personnel.Name AS 銷售人員, Customer.Name AS 客戶, Sales.TypeOfPayment AS 付款方式, Sales.Deposit AS 訂金, Sum([SellingPrice]*[SalesGoods].[Number])+IIF(IsNull([Price]),0,[Price]) AS 金額, Sum(([SellingPrice]-[cost])*[SalesGoods].[Number])+IIF(IsNull([profit]),0,[Profit]) AS 利潤, Sales.Note AS 備註 " & _
            " FROM (((Sales LEFT JOIN (SalesGoods LEFT JOIN Stock ON SalesGoods.StockLabel = Stock.Label) ON Sales.Label = SalesGoods.SalesLabel) LEFT JOIN Customer ON Sales.CustomerLabel = Customer.Label) LEFT JOIN Personnel ON Sales.PersonnelLabel = Personnel.Label) LEFT JOIN (SELECT SalesLabel, sum( Contract.Prepay)-sum(SalesContract.Discount) as Price, sum(commission)-sum(SalesContract.Discount) as profit  FROM SalesContract LEFT JOIN Contract ON SalesContract.ContractLabel=Contract.Label  Group by SalesLabel )  AS ContractInfo ON Sales.Label = ContractInfo.SalesLabel " & _
            condition1 & _
            " GROUP BY Sales.Label, Sales.OrderDate, Sales.SalesDate, Personnel.Name, Customer.Name, Sales.TypeOfPayment, Sales.Deposit, Sales.Note, ContractInfo.Price, ContractInfo.profit;"


            Dim DT As Data.DataTable = Read("table", BasePath, SQLCommand)
            Return DT
        End Function

        '讀取訂單資訊
        Public Function GetOrderListWithContract(ByVal StartTime As Date, ByVal EndTime As Date) As Data.DataTable

            Dim OrderTime As String = "(sales.Orderdate Between #" & StartTime.ToString("yyyy/MM/dd HH:mm:ss") & "# And #" & EndTime.ToString("yyyy/MM/dd HH:mm:ss") & "#)"
            Dim condition1 As String = " WHERE ( " & OrderTime & ") "


            'Dim condition2 As String = " TypeOfPayment=" & CType(PayType, Integer)
            Dim SQLCommand As String = "SELECT Sales.Label AS 單號, Sales.OrderDate AS 訂單時間, Sales.SalesDate AS 銷貨時間, Personnel.Name AS 銷售人員, Customer.Name AS 客戶, Sales.TypeOfPayment AS 付款方式, Sales.Deposit AS 訂金, Sum([SellingPrice]*[SalesGoods].[Number])+IIF(IsNull([Price]),0,[Price]) AS 金額, Sum(([SellingPrice]-[cost])*[SalesGoods].[Number])+IIF(IsNull([profit]),0,[Profit]) AS 利潤, Sales.Note AS 備註 " & _
            " FROM (((Sales LEFT JOIN (SalesGoods LEFT JOIN Stock ON SalesGoods.StockLabel = Stock.Label) ON Sales.Label = SalesGoods.SalesLabel) LEFT JOIN Customer ON Sales.CustomerLabel = Customer.Label) LEFT JOIN Personnel ON Sales.PersonnelLabel = Personnel.Label) LEFT JOIN (SELECT SalesLabel, sum( Contract.Prepay)-sum(SalesContract.Discount) as Price, sum(commission)-sum(SalesContract.Discount) as profit  FROM SalesContract LEFT JOIN Contract ON SalesContract.ContractLabel=Contract.Label  Group by SalesLabel )  AS ContractInfo ON Sales.Label = ContractInfo.SalesLabel " & _
            condition1 & _
            " GROUP BY Sales.Label, Sales.OrderDate, Sales.SalesDate, Personnel.Name, Customer.Name, Sales.TypeOfPayment, Sales.Deposit, Sales.Note, ContractInfo.Price, ContractInfo.profit;"


            Dim DT As Data.DataTable = Read("table", BasePath, SQLCommand)
            Return DT
        End Function

        '取得銷貨單資訊
        Public Function GetSales(ByVal Label As String) As Sales
            Dim SQLCommand As String = "SELECT * FROM " & Sales.Table & " WHERE label='" & Label & "';"
            Dim DT As Data.DataTable = Read("table", BasePath, SQLCommand)

            Dim s As New Sales
            If DT Is Nothing OrElse DT.Rows.Count <= 0 Then Return s
            Return Sales.GetFrom(DT.Rows(0))

        End Function

        Public Function GetOrderListBySalesLabel(ByVal label) As Data.DataTable
            Dim SqlCommand As String = "SELECT Goods.Label, Goods.Kind, Goods.Brand, Goods.Name, history.Price, OrderGoods.Price, OrderGoods.Number " & _
            " FROM OrderGoods LEFT JOIN ((SELECT HistoryPrice.GoodsLabel, HistoryPrice.Price " & _
            " FROM (SELECT HistoryPrice.GoodsLabel, Max(HistoryPrice.Time) AS Time1 " & _
            " FROM HistoryPrice " & _
            " GROUP BY HistoryPrice.GoodsLabel )  AS tmp LEFT JOIN HistoryPrice ON (tmp.Time1 = HistoryPrice.Time) AND (tmp.GoodsLabel = HistoryPrice.GoodsLabel) )  AS history RIGHT JOIN Goods ON history.GoodsLabel = Goods.Label) ON OrderGoods.GoodsLabel = Goods.Label " & _
            " WHERE (OrderGoods.SalesLabel='" & label & "') " & _
            " GROUP BY Goods.Label, Goods.Kind, Goods.Brand, Goods.Name, history.Price, OrderGoods.Price, OrderGoods.Number;"

            Return Read("table", BasePath, SqlCommand)
        End Function

        '取得銷貨單的商品清單-根據銷貨單號
        Public Function GetGoodsListBySalesLabel(ByVal Label As String) As Data.DataTable
            Dim SQLCommand As String = "SELECT Goods.Label, SalesGoods.StockLabel, Goods.Kind, Goods.Brand, Goods.Name, SalesGoods.SellingPrice, SalesGoods.Number" & _
            " FROM SalesGoods LEFT JOIN (Stock LEFT JOIN Goods ON Stock.GoodsLabel = Goods.Label) ON SalesGoods.StockLabel = Stock.Label" & _
            " WHERE (((SalesGoods.SalesLabel)=""" & Label & """));"
            Dim DT As Data.DataTable = Read("table", BasePath, SQLCommand)
            Return DT
        End Function

        Public Function GetSalesTip(ByVal Label As String, ByVal style As Database.Payment) As String
            Dim lst As New List(Of String)
            Dim dt As DataTable = GetContractListBySalesLabel(Label)


            If dt IsNot Nothing Then
                For Each r As DataRow In dt.Rows
                    lst.Add(r("Name"))
                Next
            End If

            If style = Payment.Commission Then
                dt = GetOrderListBySalesLabel(Label)
            Else
                dt = GetGoodsListBySalesLabel(Label)
            End If

            If dt IsNot Nothing Then
                For Each r As DataRow In dt.Rows
                    lst.Add(r("Name") & " x " & r("Number"))
                Next
            End If

            If lst.Count = 0 Then Return ""
            Return Join(lst.ToArray, vbCrLf)

        End Function

        '取得銷貨單的商品清單-根據銷貨單號
        Public Function GetGoodsListBySalesLabelWithHistoryPrice(ByVal Label As String) As Data.DataTable
            Dim SQLCommand As String = "SELECT Goods.Label, SalesGoods.StockLabel, Goods.Kind, Goods.Brand, Goods.Name, history.Price, SalesGoods.SellingPrice, SalesGoods.Number " & _
            " FROM (SalesGoods LEFT JOIN Stock ON SalesGoods.StockLabel = Stock.Label) LEFT JOIN ((SELECT HistoryPrice.GoodsLabel, HistoryPrice.Price FROM (SELECT HistoryPrice.GoodsLabel, Max(HistoryPrice.Time) AS Time1 FROM HistoryPrice GROUP BY HistoryPrice.GoodsLabel)  AS tmp LEFT JOIN HistoryPrice ON (tmp.GoodsLabel=HistoryPrice.GoodsLabel) AND (tmp.Time1=HistoryPrice.Time))  AS history RIGHT JOIN Goods ON history.GoodsLabel = Goods.Label) ON Stock.GoodsLabel = Goods.Label " & _
            " GROUP BY Goods.Label, SalesGoods.StockLabel, Goods.Kind, Goods.Brand, Goods.Name, history.Price, SalesGoods.SellingPrice, SalesGoods.Number, SalesGoods.SalesLabel " & _
            " HAVING (((SalesGoods.SalesLabel)='" & Label & "'));"

            Dim DT As Data.DataTable = Read("table", BasePath, SQLCommand)
            Return DT
        End Function

        Public Function GetContractListBySalesLabel(ByVal SalesLabel As String) As Data.DataTable
            Dim SQLCommand As String = "SELECT Contract.Label, Contract.Name, Contract.Prepay, SalesContract.Discount, SalesContract.Phone " & _
            "FROM (SalesContract INNER JOIN Sales ON SalesContract.SalesLabel = Sales.Label) INNER JOIN Contract ON SalesContract.ContractLabel = Contract.Label where SalesLabel='" & SalesLabel & "';"
            Return Read("table", BasePath, SQLCommand)

        End Function

        '取得包含該商品的銷貨單
        Public Function GetSalesListByStockLabel(ByVal Label As String) As Data.DataTable
            Dim SQLCommand As String = "SELECT SalesGoods.StockLabel, Goods.Kind, Goods.Brand, Goods.Name,  SalesGoods.SellingPrice, SalesGoods.Number" & _
            " FROM SalesGoods LEFT JOIN (Stock LEFT JOIN Goods ON Stock.GoodsLabel = Goods.Label) ON SalesGoods.StockLabel = Stock.Label" & _
            " WHERE (((SalesGoods.StockLabel)=""" & Label & """));"
            Dim DT As Data.DataTable = Read("table", BasePath, SQLCommand)
            Return DT
        End Function

        'Public Function GetSalesByCustomerLabel(ByVal CustomerLabel As String) As Data.DataTable
        '    Dim SQLCommand As String = "SELECT SalesGoods.StockLabel, Goods.Kind, Goods.Brand, Goods.Name, Stock.Price, SalesGoods.SellingPrice, SalesGoods.Number" & _
        '    " FROM SalesGoods LEFT JOIN (Stock LEFT JOIN Goods ON Stock.GoodsLabel = Goods.Label) ON SalesGoods.StockLabel = Stock.Label" & _
        '    " WHERE (((SalesGoods.StockLabel)=""" & CustomerLabel & """));"
        '    Dim DT As Data.DataTable = File.Read("table", File.BasePath, SQLCommand)
        '    Return DT
        'End Function



        Public Sub CreateSales(ByVal args As SalesArgs)
            CreateSales(args.Sales, args.GoodsList, args.OrderList, args.SalesContracts)
        End Sub

        Public Sub ChangeSales(ByVal args As SalesArgs)
            ChangeSales(args.Sales, args.GoodsList, args.OrderList, args.SalesContracts)
        End Sub

        '新增銷貨單
        Public Overridable Sub CreateSales(ByVal newSales As Sales, ByVal salesGoods() As SalesGoods, ByVal OrderGoods() As OrderGoods, ByVal SalesContracts() As SalesContract)

            Dim total As Single = 0
            Select Case newSales.TypeOfPayment
                Case Payment.Commission
                    total = Array.ConvertAll(Of OrderGoods, Single)(OrderGoods, Function(o As OrderGoods) o.Number * o.Price).Sum()
                Case Else
                    total = Array.ConvertAll(Of SalesGoods, Single)(salesGoods, Function(o As SalesGoods) o.Number * o.SellingPrice).Sum()
            End Select

            AddLog(Now, "新增" & IIf(newSales.TypeOfPayment = Payment.Commission, "訂單", "銷貨單") & ":" & newSales.Label & "(" & total & ")")

            CreateSalesWithoutEvent(newSales, salesGoods, OrderGoods, SalesContracts)

            OnCreatedSales(newSales, salesGoods, OrderGoods, SalesContracts)
        End Sub

        '刪除銷貨單
        Public Overridable Sub DeleteSales(ByVal dSales As Sales)
            DeleteSalesWithoutEvent(dSales)
            AddLog(Now, "刪除" & IIf(dSales.TypeOfPayment = Payment.Commission, "訂單", "銷貨單") & ":" & dSales.Label)
            OnDeletedSales(dSales)
        End Sub

        '修改銷貨單
        Public Overridable Sub ChangeSales(ByVal newSales As Sales, ByVal SalesGoods() As SalesGoods, ByVal OrderGoods() As OrderGoods, ByVal SalesContracts() As SalesContract)
            Dim sales As Sales = GetSales(newSales.Label)
            Dim total As Single = 0
            Dim title As String = ""
            If sales.TypeOfPayment = Payment.Commission And sales.TypeOfPayment <> newSales.TypeOfPayment Then
                title = "訂單轉銷貨:"
                total = Array.ConvertAll(Of SalesGoods, Single)(SalesGoods, Function(o As SalesGoods) o.Number * o.SellingPrice).Sum()
            ElseIf newSales.TypeOfPayment = Payment.Commission Then
                title = "修改訂單:"
                total = Array.ConvertAll(Of OrderGoods, Single)(OrderGoods, Function(o As OrderGoods) o.Number * o.Price).Sum()
            Else
                title = "修改銷貨單:"
                total = Array.ConvertAll(Of SalesGoods, Single)(SalesGoods, Function(o As SalesGoods) o.Number * o.SellingPrice).Sum()
            End If

            AddLog(Now, title & newSales.Label & "(" & total & ")")
            DeleteSalesWithoutEvent(newSales)
            CreateSalesWithoutEvent(newSales, SalesGoods, OrderGoods, SalesContracts)

            OnChangedSales(newSales, SalesGoods, OrderGoods, SalesContracts)
        End Sub

        Private Sub CreateSalesWithoutEvent(ByVal newSales As Sales, ByVal SalesGoods() As SalesGoods, ByVal OrderGoods() As OrderGoods, ByVal SalesContracts() As SalesContract)
            AddBase(newSales)

            For Each g As SalesGoods In SalesGoods
                AddBase(g)
            Next

            For Each o As OrderGoods In OrderGoods
                AddBase(o)
            Next

            For Each c As SalesContract In SalesContracts
                AddBase(c)
            Next
        End Sub

        Private Sub DeleteSalesWithoutEvent(ByVal dSales As Sales)
            Dim SqlCommand As String = "DELETE FROM sales WHERE label='" & dSales.Label & "';"
            Command(SqlCommand, BasePath)

            SqlCommand = "DELETE FROM salesgoods WHERE saleslabel='" & dSales.Label & "';"
            Command(SqlCommand, BasePath)

            SqlCommand = "DELETE FROM OrderGoods WHERE SalesLabel='" & dSales.Label & "';"
            Command(SqlCommand, BasePath)

            SqlCommand = GetSqlDelete(SalesContract.Table, "SalesLabel", dSales.Label)
            Command(SqlCommand, BasePath)
        End Sub





        'Public Class File

        Public Shared Dir As String
        Public BasePath As String
        Public SalesPath As String
        Public Shared Password As String = "36363636"

        Shared DBWriteLock As String = " DBWriteLock"

#Region "Connect - Access連線"
        ''' <summary>連線資料庫，回傳所連接資料庫元件，若檔案不存在則新增該檔案</summary>
        ''' <param name="FilePath">檔案路徑</param>
        Public Shared Function ConnectBase(ByVal FilePath As String) As OleDb.OleDbConnection
            Dim Dir As String = Left(FilePath, FilePath.LastIndexOf("\"))

            If Not IO.Directory.Exists(Dir) Then IO.Directory.CreateDirectory(Dir)

            If Not IO.File.Exists(FilePath) Then
                Return CreateFileBase(FilePath)
            Else
                Return Open(FilePath)
            End If

        End Function

        '''' <summary>連線資料庫，回傳所連接資料庫元件，若檔案不存在則新增該檔案</summary>
        '''' <param name="FilePath">檔案路徑</param>
        'Public Shared Function ConnectSales(ByVal FilePath As String) As OleDb.OleDbConnection
        '    Dim Dir As String = Left(FilePath, FilePath.LastIndexOf("\"))

        '    If Not IO.Directory.Exists(Dir) Then IO.Directory.CreateDirectory(Dir)

        '    If Not IO.File.Exists(FilePath) Then
        '        Return CreateFileSales(FilePath)
        '    Else
        '        Return Open(FilePath)
        '    End If
        'End Function

        ''' <summary>連接資料庫，回傳所連接的資料庫元件</summary>
        ''' <param name="FilePath">檔案路徑</param>
        Private Shared Function Open(ByVal FilePath As String) As OleDb.OleDbConnection
            Dim DBControl As OleDb.OleDbConnection

            '指定檔案路徑
            DBControl = New OleDb.OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=" & FilePath & ";Jet OLEDB:Database Password=" & Password)

            Try
                DBControl.Open() '開啟與資料庫的連線 
            Catch
            End Try

            Return DBControl
        End Function

        Public Overloads Shared Sub Close(ByVal DBControl As OleDb.OleDbConnection)
            Try : DBControl.Close() : Catch : End Try
            Try : DBControl.Dispose() : Catch : End Try
        End Sub

        ''' <summary>新增Access檔案，同時加入索引，回傳所連接的資料庫元件</summary>
        ''' <param name="FilePath">檔案路徑</param>
        Private Shared Function CreateFileBase(ByVal FilePath As String) As OleDb.OleDbConnection
            CreateAccessFile(FilePath)
            Dim DBControl As OleDb.OleDbConnection = Open(FilePath)
            CreateTable(Supplier.Table, Supplier.ToColumns, DBControl)
            CreateTable(Customer.Table, Customer.ToColumns, DBControl)
            CreateTable(Personnel.Table, Personnel.ToColumns, DBControl)
            CreateTable(Goods.Table, Goods.ToColumns, DBControl)
            CreateTable(Contract.Table, Contract.ToColumns, DBControl)
            CreateTable(Stock.Table, Stock.ToColumns, DBControl)
            CreateTable(Sales.Table, Sales.ToColumns, DBControl)
            CreateTable(SalesGoods.Table, SalesGoods.ToColumns, DBControl)
            CreateTable(SalesContract.Table, SalesContract.ToColumns, DBControl)
            CreateTable(OrderGoods.Table, OrderGoods.ToColumns, DBControl)
            CreateTable(HistoryPrice.Table, HistoryPrice.ToColumns(), DBControl)
            CreateTable(Log.Table, Log.ToColumns, DBControl)
            myDatabase.AddBase(Personnel.Administrator)
            Return DBControl
        End Function



        ''' <summary>新增Access檔案，同時加入索引，回傳所連接的資料庫元件</summary>
        ''' <param name="FilePath">檔案路徑</param>
        Private Shared Function CreateFileSales(ByVal FilePath As String) As OleDb.OleDbConnection
            CreateAccessFile(FilePath)
            Dim DBControl As OleDb.OleDbConnection = Open(FilePath)
            CreateTable(Sales.Table, Sales.ToColumns, DBControl)
            'CreateTable(Order.Table, Order.ToColumns, DBControl)
            Return DBControl
        End Function
        ''' <summary>
        ''' 新增Access檔案
        ''' </summary>
        ''' <param name="FilePath">檔案路徑</param>
        Private Shared Function CreateAccessFile(ByVal FilePath) As String
            Dim newDatabase As New ADOX.Catalog
            Try : newDatabase.Create("PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=" & FilePath & ";Jet OLEDB:Database Password=" & Password) : Catch : End Try
            Return Err.Description
        End Function

        ''' <summary>
        ''' 新增Alarm資料表
        ''' </summary>
        ''' <param name="Table">資料表名稱</param>
        ''' <param name="DBControl">資料庫元件</param>
        Public Shared Function CreateTable(ByVal Table As String, ByVal Columns() As Column, ByVal DBControl As OleDb.OleDbConnection) As String
            Dim RowList() As String = Array.ConvertAll(Columns, Function(c As Column) "[" & c.Name & "] " & c.Type)
            Dim RowText As String = Join(RowList, ",")

            Dim ResMsg As String = Command("CREATE TABLE [" & Table & "] (" & RowText & ")", DBControl)
            For i As Integer = 0 To Columns.Length - 1
                If Columns(i).HaveIndex Then CreateIndex(Table, "idx_" & Columns(i).Name, "[" & Columns(i).Name & "]", DBControl)
            Next
            Return ResMsg
        End Function

        Public Shared Function DeleteTable(ByVal Table As String, ByVal DBControl As OleDb.OleDbConnection) As String
            Return Command("DROP TABLE [" & Table & "];", DBControl)
        End Function
        ''' <summary>
        ''' 加入索引
        ''' </summary>
        ''' <param name="TableName">資料表名塞</param>
        ''' <param name="IndexName">索引名稱</param>
        ''' <param name="IndexClumnList">欲加入索引之欄位</param>
        ''' <param name="DBControl">資料庫元件</param>
        Private Shared Function CreateIndex(ByVal TableName As String, ByVal IndexName As String, ByVal IndexClumnList As String, ByVal DBControl As OleDb.OleDbConnection) As String
            Return Command("CREATE INDEX " & IndexName & " on " & TableName & " (" & IndexClumnList & ")", DBControl)
        End Function

        ''' <summary>對資料庫下達SQL指令</summary>
        ''' <param name="newCMD">SQL字串</param>
        ''' <param name="DBControl">資料庫名稱</param>
        Private Shared Function Command(ByVal newCMD As String, ByVal DBControl As OleDb.OleDbConnection) As Long
            Dim ExecuteCMD As OleDb.OleDbCommand = New OleDb.OleDbCommand(newCMD, DBControl)
            Dim Count As Long = 0
            Try
                Count = ExecuteCMD.ExecuteNonQuery()
            Catch
                MsgBox(Err.Description)
                Count = -1
            End Try
            ExecuteCMD.Dispose()
            Return Count
        End Function

        ''' <summary>對資料庫下達SQL指令</summary>
        ''' <param name="SqlCommand">SQL字串</param>
        ''' <param name="File">檔按路徑</param>
        Public Overridable Function Command(ByVal SqlCommand As String, ByVal File As String) As Long
            Dim Count As Long
            SyncLock Lock
                Dim DBControl As OleDb.OleDbConnection = ConnectBase(File)
                Count = Command(SqlCommand, DBControl)
                Close(DBControl)
            End SyncLock
            Return Count
        End Function

        '''' <summary>對資料庫下達SQL指令</summary>
        '''' <param name="SqlCommand">SQL字串</param>
        '''' <param name="File">檔按路徑</param>
        'Private Shared Function CommandSales(ByVal SqlCommand As String, ByVal File As String) As Long
        '    Dim DBControl As OleDb.OleDbConnection = ConnectSales(File)
        '    Dim Count As Long = Command(SqlCommand, DBControl)
        '    Close(DBControl)
        '    Return Count
        'End Function

#End Region

        Public Function Read(ByVal Table As String, ByVal File As String, ByVal SqlCommand As String) As Data.DataTable
            Return Read(Table, New String() {File}, New String() {SqlCommand})
        End Function

        Public Function Read(ByVal Table As String, ByVal FileList() As String, ByVal SqlCommand As String) As Data.DataTable
            Return Read(Table, FileList, New String() {SqlCommand})
        End Function

        Public Overridable Function Read(ByVal Table As String, ByVal FileList() As String, ByVal SQLCommand() As String) As Data.DataTable



            '暫存表格
            Dim DS As DataSet = New DataSet()
            Dim DA As OleDb.OleDbDataAdapter

            Dim TmpFile As String
            Dim totFile As Int16 = UBound(FileList) + 1
            If totFile = 0 Then Return Nothing

            Dim i As Integer
            For i = 1 To totFile
                TmpFile = FileList(i - 1)
                SyncLock Lock
                    Dim tmpDB As OleDb.OleDbConnection = ConnectBase(TmpFile) ' New OleDb.OleDbConnection("PROVIDER=MICROSOFT.Jet.OLEDB.4.0;DATA SOURCE=" & TmpFile)
                    Try
                        'tmpDB.Open()
                        For j As Integer = 0 To SQLCommand.Length - 1
                            DA = New OleDb.OleDbDataAdapter(SQLCommand(j), tmpDB)
                            DA.Fill(DS, Table)
                            DA.Dispose()
                        Next
                    Catch
                    Finally
                        tmpDB.Close()
                        tmpDB.Dispose()
                    End Try
                End SyncLock
            Next


            Return DS.Tables(Table)
        End Function

        Shared Function GetSqlSelect(ByVal Table As String) As String
            Return "SELECT * FROM [" & Table & "];"
        End Function

        Shared Function GetSqlInsert(ByVal data As Object) As String
            Return GetSqlInsert(data.Table, data.ToColumns, data.ToObjects)
        End Function

        Shared Function GetSqlInsert(ByVal Table As String, ByVal Columns() As Column, ByVal objects As Object()) As String
            Dim ColumnText As String = Table & "(" & Join(Array.ConvertAll(Columns, Function(c As Column) "[" & c.Name & "]"), ",") & ")"
            Dim ValueText As String = Join(Array.ConvertAll(objects, AddressOf GetSqlValue), ",")
            Return "INSERT INTO " & ColumnText & " VALUES (" & ValueText & ");"
        End Function

        Shared Function GetSqlDelete(ByVal Table As String, ByVal ColumnName As String, ByVal Condition As Object) As String
            Return "DELETE FROM " & Table & " WHERE [" & ColumnName & "]=" & GetSqlValue(Condition) & ";"
        End Function

        Shared Function GetConitionSql(ByVal ColumnNames() As String, ByVal Conitions() As Object) As String
            Dim lstConition As New List(Of String)
            For i As Integer = 0 To ColumnNames.Length - 1
                lstConition.Add("[" & ColumnNames(i) & "]=" & GetSqlValue(Conitions(i)))
            Next
            Return Join(lstConition.ToArray, " AND ")
        End Function

        Shared Function GetSqlDelete(ByVal Table As String, ByVal ColumnNames() As String, ByVal Conitions() As Object) As String
            Return "DELETE FROM " & Table & " WHERE " & GetConitionSql(ColumnNames, Conitions) & ";"
        End Function

        Public Shared Function GetUpdateSqlCommand(ByVal Table As String, ByVal column() As String, ByVal value() As String, ByVal ConditionColumn() As String, ByVal Condition() As Object) As String
            Dim SQLCommand As String = "UPDATE " & Table & " SET "
            SQLCommand &= GetSqlColumnChangePart(column, value) & " WHERE " & GetConitionSql(ConditionColumn, Condition) & ";"
            Return SQLCommand
        End Function

        Public Shared Function GetUpdateSqlCommand(ByVal Table As String, ByVal column() As String, ByVal value() As Object, ByVal ConditionColumn As String, ByVal Condition As Object) As String
            Dim SQLCommand As String = "UPDATE " & Table & " SET "
            SQLCommand &= GetSqlColumnChangePart(column, value) & " WHERE [" & ConditionColumn & "]=" & GetSqlValue(Condition) & ";"
            Return SQLCommand
        End Function

        Public Sub DeleteColumn(ByVal Table As String, ByVal ColumnName As String)
            Dim SQLCommand As String = "ALTER TABLE [" & Table & "] DROP [" & ColumnName & "];"
            Command(SQLCommand, BasePath)
        End Sub

        Public Sub AddColumn(ByVal Table As String, ByVal ColumnName As String, ByVal Type As String)
            Dim SqlCommand As String = "ALTER TABLE [" & Table & "] ADD [" & ColumnName & "] " & Type & ";"
            Command(SqlCommand, BasePath)
        End Sub

        Private Shared Function GetSqlColumnChangePart(ByVal Label() As String, ByVal value() As Object) As String
            Dim lst As New List(Of String)

            For i As Integer = 0 To Label.Length - 1
                lst.Add("[" & Label(i) & "]=" & GetSqlValue(value(i)))
            Next i
            Return Join(lst.ToArray, ",")
        End Function

        Shared Function GetSqlValue(ByVal obj As Object) As String
            If obj Is Nothing Then
                Return "''"
            ElseIf obj.GetType() Is GetType(Boolean) Then
                Return obj.ToString
            ElseIf obj Is Nothing OrElse obj.GetType() Is GetType(String) Then
                Return "'" & obj.ToString & "'"
            ElseIf obj.GetType Is GetType(Date) Then
                Return CType(obj, Date).ToString("#yyyy/MM/dd HH:mm:ss#")
            Else
                Return obj.ToString
            End If
        End Function


        Public Sub AddBase(ByVal data As Object)
            Command(GetSqlInsert(data), BasePath)
        End Sub

        'Public Shared Sub AddSales(ByVal data As Object)
        '    CommandSales(data, SalesPath)
        'End Sub


        Delegate Function Conv(Of T)(ByVal d As Data.DataRow) As T

        Public Function Read(Of T)(ByVal Table As String, ByVal C As Conv(Of T)) As T()
            Dim DT As Data.DataTable = Read(Supplier.Table, BasePath, GetSqlSelect(Table))

            Dim lstData As New List(Of T)


            If DT Is Nothing Then Return lstData.ToArray
            For i As Integer = 0 To DT.Rows.Count - 1
                lstData.Add(C(DT.Rows(i)))
            Next
            Return lstData.ToArray
        End Function


        ''' <summary>新增供應商</summary>
        Public Overridable Sub AddSupplier(ByVal data As Supplier, Optional ByVal trigger As Boolean = True)
            AddBase(data)
            AddLog(Now, "新增供應商:" & data.Name)
            If trigger Then OnCreatedSupplier(data)
        End Sub

        Public Overridable Sub DeleteSupplier(ByVal data As Supplier, Optional ByVal trigger As Boolean = True)
            Command(GetSqlDelete(Supplier.Table, "Label", data.Label), BasePath)
            AddLog(Now, "刪除供應商:" & data.Name)
            If trigger Then OnDeletedSupplier(data)
        End Sub

        Public Overridable Sub ChangeSupplier(ByVal data As Supplier, Optional ByVal trigger As Boolean = True)
            Command(data.GetUpdateSqlCommand(), BasePath)
            AddLog(Now, "編輯供應商:" & data.Name)
            If trigger Then OnChangedSupplier(data)
        End Sub

        Public Overridable Sub AddStockMove(ByVal data As StockMove, Optional ByVal trigger As Boolean = True)
            AddBase(data)
            Select Case data.Action
                Case StockMove.Type.Request, StockMove.Type.Sending
                    AddLog(Now, "調貨:" & StockMove.TypeText(data.Action) & " - " & GetGoods(data.Label).Name & " - " & data.SourceShop & "->" & data.DestineShop)
            End Select

            If trigger Then OnCreatedStockMove(data)
        End Sub

        Public Overridable Sub ChangeStockMove(ByVal data As StockMove, Optional ByVal trigger As Boolean = True)
            Command(data.GetUpdateSqlCommand(), BasePath)
            Select Case data.Action
                Case StockMove.Type.Cancel, StockMove.Type.In, StockMove.Type.Sending
                    AddLog(Now, "調貨:" & StockMove.TypeText(data.Action) & " - " & GetGoods(data.Label).Name & " - " & data.SourceShop & "->" & data.DestineShop)
            End Select

            If trigger Then OnChangedStockMove(data)
        End Sub

        'Public Overridable Sub ChangeStockMove(ByVal StockMoveLabel As String, ByVal state As StockMove.Type, Optional ByVal trigger As Boolean = True)
        '    Dim stockMove As StockMove = GetStockMove(StockMoveLabel)
        '    Command(stockMove.GetUpdateStateSqlCommand(StockMoveLabel, state), BasePath)
        '    stockMove.Action = state
        '    If trigger Then OnChangedStockMove(stockMove)
        'End Sub

        Public Overridable Sub DeleteStockMove(ByVal data As StockMove, Optional ByVal trigger As Boolean = True)
            Command(GetSqlDelete(StockMove.Table, "Label", data.Label), BasePath)
            AddLog(Now, "刪除調貨記錄:" & StockMove.TypeText(data.Action) & " - " & GetGoods(data.Label).Name & " - " & data.SourceShop & "->" & data.DestineShop)
            If trigger Then OnDeletedStockMove(data)
        End Sub

        Event ChangedStockMove(ByVal sender As Object, ByVal data As StockMove)
        Event CreatedStockMove(ByVal sender As Object, ByVal data As StockMove)
        Event DeletedStockMove(ByVal sender As Object, ByVal data As StockMove)
        Public Overridable Sub OnCreatedStockMove(ByVal Data As StockMove)
            RaiseEvent CreatedStockMove(Me, Data)
        End Sub
        Public Overridable Sub OnChangedStockMove(ByVal Data As StockMove)
            RaiseEvent ChangedStockMove(Me, Data)
        End Sub
        Public Overridable Sub OnDeletedStockMove(ByVal Data As StockMove)
            RaiseEvent DeletedStockMove(Me, Data)
        End Sub

        Public Overridable Sub AddLog(ByVal data As Log, Optional ByVal trigger As Boolean = True)
            'AddBase(Data)
            Command(data.GetSqlInsert(), BasePath)
            If trigger Then OnCreatedLog(Data)
        End Sub

        Public Overridable Sub DeleteLog(ByVal data As Log, Optional ByVal trigger As Boolean = True)
            Command(GetSqlDelete(Log.Table, New String() {"Date", "Personnel"}, New Object() {data.Date, data.Personnel}), BasePath)
            If trigger Then OnDeletedLog(data)
        End Sub

        Public Overridable Sub DeleteAllLog(Optional ByVal trigger As Boolean = True)
            Command("DELETE FROM " & Log.Table & " ;", BasePath)
            If trigger Then OnDeletedAllLog()
        End Sub


        ''' <summary>新增客戶</summary>
        Public Overridable Sub AddCustomer(ByVal data As Customer, Optional ByVal trigger As Boolean = True)
            AddBase(data)
            AddLog(Now, "新增客戶資料:" & data.Name)
            If trigger Then OnCreatedCustomer(data)
        End Sub

        Public Overridable Sub DeleteCustomer(ByVal data As Customer, Optional ByVal trigger As Boolean = True)
            Command(GetSqlDelete(Customer.Table, "Label", data.Label), BasePath)
            AddLog(Now, "刪除客戶資料:" & data.Name)
            If trigger Then OnDeletedCustomer(data)
        End Sub

        Public Overridable Sub ChangeCustomer(ByVal data As Customer, Optional ByVal trigger As Boolean = True)
            Command(data.GetUpdateSqlCommand(), BasePath)
            AddLog(Now, "修改客戶資料:" & data.Name)
            If trigger Then OnChangedCustomer(data)
        End Sub

        ''' <summary>新增員工</summary>
        Public Overridable Sub AddPersonnel(ByVal data As Personnel, Optional ByVal trigger As Boolean = True)
            AddBase(data)
            AddLog(Now, "新增員工資料:" & data.Name)
            If trigger Then OnCreatedPersonnel(data)
        End Sub

        Public Overridable Sub DeletePersonnel(ByVal data As Personnel, Optional ByVal trigger As Boolean = True)
            Command(GetSqlDelete(Personnel.Table, "Label", data.Label), BasePath)
            AddLog(Now, "刪除員工資料:" & data.Name)
            If trigger Then OnDeletedPersonnel(data)
        End Sub

        Public Overridable Sub ChangePersonnel(ByVal data As Personnel, Optional ByVal trigger As Boolean = True)
            Command(data.GetUpdateSqlCommand(), BasePath)
            AddLog(Now, "修改員工資料:" & data.Name)
            If trigger Then OnChangedPersonnel(data)
        End Sub

        ''' <summary>新增商品</summary>
        Public Overridable Sub AddGoods(ByVal data As Goods, Optional ByVal trigger As Boolean = True)
            AddBase(data)
            AddLog(Now, "新增商品項目:" & data.Name)
            If trigger Then OnCreatedGoods(data)
        End Sub

        Public Overridable Sub DeleteGoods(ByVal data As Goods, Optional ByVal trigger As Boolean = True)
            Command(GetSqlDelete(Goods.Table, "Label", data.Label), BasePath)
            AddLog(Now, "刪除商品項目:" & data.Name)
            If trigger Then OnDeletedGoods(data)
        End Sub

        Public Overridable Sub ChangeGoods(ByVal data As Goods, Optional ByVal trigger As Boolean = True)
            Command(data.GetUpdateSqlCommand(), BasePath)
            AddLog(Now, "編輯商品項目:" & data.Name)
            If trigger Then OnChangedGoods(data)
        End Sub

        ''' <summary>新增門號</summary>
        Public Overridable Sub AddContract(ByVal data As Contract, Optional ByVal trigger As Boolean = True)
            AddBase(data)
            AddLog(Now, "新增合約種類:" & data.Name)
            If trigger Then OnCreatedContract(data)
        End Sub

        Public Overridable Sub ChangeContract(ByVal data As Contract, Optional ByVal trigger As Boolean = True)
            Command(data.GetUpdateSqlCommand(), BasePath)
            AddLog(Now, "編輯合約種類:" & data.Name)
            If trigger Then OnChangedContract(data)
        End Sub

        Public Overridable Sub DeleteContract(ByVal data As Contract, Optional ByVal trigger As Boolean = True)
            Command(GetSqlDelete(Contract.Table, "Label", data.Label), BasePath)
            AddLog(Now, "刪除合約種類:" & data.Name)
            If trigger Then OnDeletedContract(data)
        End Sub


        Public Overridable Sub AddHistoryPrice(ByVal data As HistoryPrice, Optional ByVal trigger As Boolean = True)
            AddBase(data)
            Dim goods As Goods = GetGoods(data.GoodsLabel)
            AddLog(Now, "新增歷史訂價:" & goods.Name & "-" & data.Price)
            If trigger Then OnCreatedHistoryPrice(data)
        End Sub

        Public Overridable Sub ChangeHistoryPrice(ByVal data As HistoryPrice, Optional ByVal trigger As Boolean = True)
            Command(data.GetUpdateSqlCommand(), BasePath)
            Dim goods As Goods = GetGoods(data.GoodsLabel)
            AddLog(Now, "修改歷史訂價:" & goods.Name & "-" & data.Price)
            If trigger Then OnChangedHistoryPrice(data)
        End Sub

        Public Overridable Sub DeleteHistoryPrice(ByVal data As HistoryPrice, Optional ByVal trigger As Boolean = True)
            Command(GetSqlDelete(HistoryPrice.Table, New String() {"GoodsLabel", "Time"}, New Object() {data.GoodsLabel, data.Time}), BasePath)
            Dim goods As Goods = GetGoods(data.GoodsLabel)
            AddLog(Now, "刪除歷史訂價:" & goods.Name & "-" & data.Price)
            If trigger Then OnDeletedHistoryPrice(data)
        End Sub

        Public Overridable Sub DeleteHistoryPriceList(ByVal GoodsLabel As String, Optional ByVal trigger As Boolean = True)
            Command(GetSqlDelete(HistoryPrice.Table, "GoodsLabel", GoodsLabel), BasePath)
            Dim data As New HistoryPrice
            data.GoodsLabel = GoodsLabel
            If trigger Then OnDeletedHistoryPriceList(data)
        End Sub

        ''' <summary>新增庫存</summary>
        Public Overridable Sub AddStock(ByVal data As Stock)
            AddBase(data)
            Dim goods As Goods = GetGoods(data.Label)
            AddLog(Now, "入庫:" & goods.Name & " x " & data.Number)
            OnCreatedStock(data)
        End Sub

        '''' <summary>新增銷貨單</summary>
        'Public Sub AddSales(ByVal data As Sales)
        '    File.AddSales(data)
        'End Sub

        '''' <summary>新增訂單</summary>
        'Public Sub AddOrder(ByVal data As Order)
        '    File.AddSales(data)
        'End Sub

        'Public Function ReadSupplier() As Supplier()
        '    Return ReadSupplier()
        'End Function

        'Public Function ReadPersonnel() As Personnel()
        '    Return ReadPersonnel
        'End Function

        'Public Function ReadGoods() As Goods()
        '    Return ReadGoods
        'End Function

        'Public Function ReadMobile() As Mobile()
        '    Return ReadMobile()
        'End Function

        Public Sub AddLog(ByVal Time As Date, ByVal Message As String)
            AddLog(New Log(User.Label, Time, Strings.Left(Message, 40)))
        End Sub

        Event CreatedLog(ByVal sender As Object, ByVal log As Log)
        Event ChangedLog(ByVal sender As Object, ByVal log As Log)
        Event DeletedLog(ByVal sender As Object, ByVal log As Log)
        Event DeletedAllLog(ByVal sender As Object)

        Friend Sub OnCreatedLog(ByVal log As Log)
            RaiseEvent CreatedLog(Me, log)
        End Sub

        Friend Sub OnChangedLog(ByVal log As Log)
            RaiseEvent ChangedLog(Me, log)
        End Sub

        Friend Sub OnDeletedLog(ByVal log As Log)
            RaiseEvent DeletedLog(Me, log)
        End Sub

        Friend Sub OnDeletedAllLog()
            RaiseEvent DeletedAllLog(Me)
        End Sub

        Friend Sub OnCreatedContract(ByVal con As Contract)
            RaiseEvent CreatedContract(Me, con)
        End Sub

        Friend Sub OnChangedContract(ByVal con As Contract)
            RaiseEvent ChangedContract(Me, con)
        End Sub
        Friend Sub OnDeletedContract(ByVal con As Contract)
            RaiseEvent DeletedContract(Me, con)
        End Sub

        Friend Sub OnCreatedGoods(ByVal goods As Goods)
            RaiseEvent CreatedGoods(Me, goods)
        End Sub
        Friend Sub OnChangedGoods(ByVal goods As Goods)
            RaiseEvent ChangedGoods(Me, goods)
        End Sub
        Friend Sub OnDeletedGoods(ByVal goods As Goods)
            RaiseEvent DeletedGoods(Me, goods)
        End Sub

        Friend Sub OnCreatedSupplier(ByVal sup As Supplier)
            RaiseEvent CreatedSupplier(Me, sup)
        End Sub
        Friend Sub OnChangedSupplier(ByVal sup As Supplier)
            RaiseEvent ChangedSupplier(Me, sup)
        End Sub
        Friend Sub OnDeletedSupplier(ByVal sup As Supplier)
            RaiseEvent DeletedSupplier(Me, sup)
        End Sub

        Friend Sub OnCreatedCustomer(ByVal cus As Customer)
            RaiseEvent CreatedCustomer(Me, cus)
        End Sub
        Friend Sub OnChangedCustomer(ByVal cus As Customer)
            RaiseEvent ChangedCustomer(Me, cus)
        End Sub
        Friend Sub OnDeletedCustomer(ByVal cus As Customer)
            RaiseEvent DeletedCustomer(Me, cus)
        End Sub

        Friend Sub OnCreatedPersonnel(ByVal per As Personnel)
            RaiseEvent CreatedPersonnel(Me, per)
        End Sub
        Friend Sub OnChangedPersonnel(ByVal per As Personnel)
            RaiseEvent ChangedPersonnel(Me, per)
        End Sub
        Friend Sub OnDeletedPersonnel(ByVal per As Personnel)
            RaiseEvent DeletedPersonnel(Me, per)
        End Sub

        Friend Sub OnCreatedStock(ByVal stock As Stock)
            RaiseEvent CreatedStock(Me, stock)
        End Sub
        Friend Sub OnChangedStock(ByVal stock As Stock)
            RaiseEvent ChangedStock(Me, stock)
        End Sub
        Friend Sub OnDeletedStock(ByVal stock As Stock)
            RaiseEvent DeletedStock(Me, stock)
        End Sub

        Friend Sub OnCreatedSales(ByVal sales As SalesArgs)
            OnCreatedSales(sales.Sales, sales.GoodsList, sales.OrderList, sales.SalesContracts)
        End Sub
        Friend Sub OnChangedSales(ByVal sales As SalesArgs)
            OnChangedSales(sales.Sales, sales.GoodsList, sales.OrderList, sales.SalesContracts)
        End Sub
        Friend Sub OnCreatedSales(ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal SalesContracts() As SalesContract)
            RaiseEvent CreatedSales(Me, sales, GoodsList, OrderList, SalesContracts)
        End Sub
        Friend Sub OnChangedSales(ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal SalesContracts() As SalesContract)
            RaiseEvent ChangedSales(Me, sales, GoodsList, OrderList, SalesContracts)
        End Sub
        Friend Sub OnDeletedSales(ByVal sales As Sales)
            RaiseEvent DeletedSales(Me, sales)
        End Sub

        Friend Sub OnCreatedHistoryPrice(ByVal hp As HistoryPrice)
            RaiseEvent CreatedHistoryPrice(Me, hp)
        End Sub
        Friend Sub OnChangedHistoryPrice(ByVal hp As HistoryPrice)
            RaiseEvent ChangedHistoryPrice(Me, hp)
        End Sub
        Friend Sub OnDeletedHistoryPrice(ByVal hp As HistoryPrice)
            RaiseEvent DeletedHistoryPrice(Me, hp)
        End Sub
        Friend Sub OnDeletedHistoryPriceList(ByVal hp As HistoryPrice)
            RaiseEvent DeletedHistoryPriceList(Me, hp)
        End Sub

        Structure RepairAccessResult
            Dim Success As Boolean
            Dim Message As String
        End Structure
        Public Shared Function RepairAccess(ByVal FilePath As String) As RepairAccessResult
            Dim result As New RepairAccessResult
            Try
                Dim tmpFile As String = IO.Path.GetDirectoryName(FilePath) & "\tmp.mdb"
                Dim j As New JRO.JetEngine
                Dim sourceConnect As String = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=" & FilePath & ";Jet OLEDB:Database Password=" & Password
                Dim desConnect As String = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=" & tmpFile & ";Jet OLEDB:Database Password=" & Password
                j.CompactDatabase(sourceConnect, desConnect)
                IO.File.Delete(FilePath)

                IO.File.Move(tmpFile, FilePath)
                result.Success = True
                result.Message = "資料庫壓縮/修復成功!"
            Catch ex As Exception
                result.Success = False
                result.Message = ex.Message
            End Try
            Return result


            'Dim strFile As String = FilePath
            'Try
            '    ' Jet Access (MDB) 連線字串; Jet ( Joint Engine Technology ) 
            '    Dim strCn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}"

            '    ' 或"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Engine Type=5"

            '    ' Path.GetTempFileName 方法: 在磁碟上建立具命之零位元組的唯一暫存檔案，
            '    '   然後傳回該檔案的完整路徑。
            '    Dim strTmpFile As String = IO.Path.GetTempFileName.Replace(".tmp", ".mdb") ' 把tmp 副檔名改成mdb



            '    ' 建立物件陣列存放引數(參數) , 來源, 目的
            '    Dim objPara As Object() = New Object() {String.Format(strCn, strFile), String.Format(strCn, strTmpFile)}


            '    ' Activator 成員: 包含本機或遠端建立物件型別的方法，或者取得對現有遠端物件的參考。
            '    ' Activator.CreateInstance 方法(Type)  : 使用最符合指定參數的建構函式，建立指定型別的執行個體。
            '    Dim objJRO As Object = Activator.CreateInstance(System.Type.GetTypeFromProgID("JRO.JetEngine"))

            '    ' Type.GetTypeFromProgID 方法: 取得與指定的程式識別項(ProgID) 關聯的型別；
            '    '   如果在載入Type 時發生錯誤，則傳回null。
            '    ' JRO.JetEngine 為Microsoft Jet and Replication Objects X.X library  

            '    ' Type.InvokeMember 方法
            '    ' Type.InvokeMember (String, BindingFlags, Binder, Object, Object[]) 
            '    objJRO.GetType.InvokeMember("CompactDatabase", Reflection.BindingFlags.InvokeMethod, Nothing, objJRO, objPara)



            '    ' 使用指定的繫結條件約束並符合指定的引數清單，來叫用指定的成員。
            '    ' BindingFlags 列舉型別,InvokeMethod 指定要叫用方法。


            '    IO.File.Delete(strFile) ' File.Delete 方法: 刪除Compact 前之mdb 檔
            '    IO.File.Move(strTmpFile, strFile) ' File.Move 方法: 將Compact 過的mdb 檔改成(回)正確檔名



            '    ' Marshal.ReleaseComObject 方法釋放JRO COM 物件

            '    Runtime.InteropServices.Marshal.ReleaseComObject(objJRO)

            '    objJRO = Nothing
            '    Return True
            'Catch
            '    'MsgBox(Err.Description)
            '    Return False
            'End Try

        End Function
    End Class
#End Region



End Namespace