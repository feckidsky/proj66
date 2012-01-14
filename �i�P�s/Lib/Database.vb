Namespace Database

    Public Structure Column
        Dim Name As String
        Dim Type As String
        Dim HaveIndex As Boolean
        Sub New(ByVal Name As String, ByVal Type As String, Optional ByVal HaveIndex As Boolean = False)
            Me.Name = Name : Me.Type = Type : Me.HaveIndex = HaveIndex
        End Sub
    End Structure

    Public Class Access

        Dim SupplierList As New List(Of Supplier)
        Dim PersonnelList As New List(Of Personnel)
        Dim GoodsList As New List(Of Goods)
        Dim MobileList As New List(Of Mobile)

        Event CreatedStock(ByVal stock As Stock)
        Event ChangedStock(ByVal stock As Stock)
        Event DeletedStock(ByVal stock As Stock)

        Event CreatedGoods(ByVal goods As Goods)
        Event ChangedGoods(ByVal goods As Goods)
        Event DeletedGoods(ByVal goods As Goods)

        Event CreatedSupplier(ByVal sup As Supplier)
        Event ChangedSupplier(ByVal sup As Supplier)
        Event DeletedSupplier(ByVal sup As Supplier)

        Event CreatedSales(ByVal sales As Sales, ByVal GoodsList() As SalesGoods)
        Event ChangedSales(ByVal sales As Sales, ByVal GoodsList() As SalesGoods)
        Event DeletedSales(ByVal sales As Sales)

        Public Sub New()
            File.Dir = My.Application.Info.DirectoryPath & "\data"
            File.BasePath = File.Dir & "\base.mdb"
            File.SalesPath = File.Dir & "\sales.mdb"
        End Sub

        Public Function GetGoodsList() As Data.DataTable
            Dim SQLCommand As String = "SELECT * FROM " & Goods.Table & ";"
            Return File.Read("table", File.BasePath, SQLCommand)
        End Function

        Public Function GetGoods(ByVal Label As String) As Goods
            Dim SQLCommand As String = "SELECT * FROM " & Goods.Table & " WHERE Label='" & Label & "';"
            Dim dt As DataTable = File.Read("table", File.BasePath, SQLCommand)
            If dt.Rows.Count = 0 Then Return Goods.Null()
            Return Goods.GetFrom(dt.Rows(0))
        End Function

        Public Function GetSupplierList() As Data.DataTable
            Dim SQLCommand As String = "SELECT * FROM " & Supplier.Table & ";"
            Return File.Read("table", File.BasePath, SQLCommand)
        End Function

        Public Function GetSupplier(ByVal Label As String) As Supplier
            Dim SQLCommand As String = "SELECT * FROM " & Supplier.Table & " WHERE Label='" & Label & "';"
            Dim dt As DataTable = File.Read("table", File.BasePath, SQLCommand)
            If dt.Rows.Count = 0 Then Return Supplier.Null()
            Return Supplier.GetFrom(dt.Rows(0))

        End Function



        '更新庫存內容
        Public Sub ChangeStock(ByVal newStock As Stock)
            Dim SQLCommand As String = newStock.GetUpdateSqlCommand()
            File.Command(SQLCommand, File.BasePath)
            RaiseEvent ChangedStock(newStock)
        End Sub

        '刪除一筆庫存
        Public Sub DeleteStock(ByVal dStock As Stock)
            Dim SQLCommand As String = "DELETE FROM " & Stock.Table & " WHERE Label='" & dStock.Label & "';"
            File.Command(SQLCommand, File.BasePath)
            RaiseEvent DeletedStock(dStock)
        End Sub

        '讀取庫存資料
        Public Function GetStock(ByVal Label As String) As Stock
            Dim SQLCommand As String = "SELECT * FROM " & Stock.Table & " WHERE Label='" & Label & "';"
            Dim DT As Data.DataTable = File.Read("table", File.BasePath, SQLCommand)

            Dim data As New Stock
            If DT.Rows.Count > 0 Then data = Stock.GetFrom(DT.Rows(0))
            Return data
        End Function



        ''' <summary>取得庫存清單</summary>
        Public Function GetStockList() As Data.DataTable
            'Dims SQLCommand As String = "SELECT stock.Label as 庫存編號,IMEI,Kind as 種類, Brand as 廠牌, [Name] as 品名,  Number as 數量 , Price as 售價, stock.Note as 備註 FROM stock LEFT JOIN goods AS [a] ON stock.GoodsLabel = [a].Label;"
            Dim SqlCommand As String = "SELECT stock.label AS 庫存編號, stock.IMEI, Goods.Kind AS 種類, Goods.Brand AS 廠牌, Goods.Name AS 品名, [stock].[number]-IIf(IsNull([nn]),0,[nn]) AS 數量, stock.Price AS 售價, stock.Note AS 備註 " & _
            " FROM (stock LEFT JOIN (SELECT StockLabel,sum(number) as nn  From SalesGoods Group By StockLabel )  AS cc ON stock.Label = cc.StockLabel) LEFT JOIN Goods ON stock.GoodsLabel = Goods.Label " & _
            " WHERE ((([stock].[number]-IIf(IsNull([nn]),0,[nn]))>0));"


            Dim DT As Data.DataTable = File.Read("table", File.BasePath, SqlCommand)
            Return DT
        End Function


        ''' <summary>取得進貨記錄</summary>
        Public Function GetStockLog(ByVal StartTime As Date, ByVal EndTime As Date) As Data.DataTable
            Dim SQLCommand As String = "SELECT Stock.Label as 庫存編號, Stock.Date as 進貨日期, Supplier.Name as 供應商, Goods.Kind as 種類, Goods.Brand as 廠牌, Stock.IMEI, Goods.Name as 品名, Stock.Cost as 進貨價, Stock.Price as 定價, Stock.Number as 數量, Stock.Note as 備註" & _
            " FROM (Stock LEFT JOIN Goods ON Stock.GoodsLabel = Goods.Label) LEFT JOIN Supplier ON Stock.SupplierLabel = Supplier.Label " & _
            " WHERE (((Stock.[date]) Between #" & StartTime.ToString("yyyy/MM/dd HH:mm:ss") & "# And #" & EndTime.ToString("yyyy/MM/dd HH:mm:ss") & "#));"
            Dim DT As Data.DataTable = File.Read("table", File.BasePath, SQLCommand)
            Return DT
        End Function

        Public Function GetStockLogByGoodsLabel(ByVal label As String) As Data.DataTable
            Dim SQLCommand As String = "SELECT Stock.Label as 庫存編號, Stock.Date as 進貨日期, Goods.Kind as 種類, Goods.Brand as 廠牌, Stock.IMEI, Goods.Name as 品名, Stock.Cost as 進貨價, Stock.Price as 定價, Stock.Number as 數量, Stock.Note as 備註" & _
            " FROM (Stock LEFT JOIN Goods ON Stock.GoodsLabel = Goods.Label) LEFT JOIN Supplier ON Stock.SupplierLabel = Supplier.Label " & _
            " WHERE Stock.GoodsLabel='" & label & "';"
            Dim DT As Data.DataTable = File.Read("table", File.BasePath, SQLCommand)
            Return DT
        End Function

        Public Function GetStockLogBySupplierLabel(ByVal label As String) As Data.DataTable
            Dim SQLCommand As String = "SELECT Stock.Label as 庫存編號, Stock.Date as 進貨日期, Goods.Kind as 種類, Goods.Brand as 廠牌, Stock.IMEI, Goods.Name as 品名, Stock.Cost as 進貨價, Stock.Price as 定價, Stock.Number as 數量, Stock.Note as 備註" & _
            " FROM (Stock LEFT JOIN Goods ON Stock.GoodsLabel = Goods.Label) LEFT JOIN Supplier ON Stock.SupplierLabel = Supplier.Label " & _
            " WHERE Stock.SupplierLabel='" & label & "';"
            Dim DT As Data.DataTable = File.Read("table", File.BasePath, SQLCommand)
            Return DT
        End Function


        '讀取銷貨單
        Public Function GetSalesList(ByVal StartTime As Date, ByVal EndTime As Date) As Data.DataTable
            Dim SQLCommand As String = "SELECT sales.label AS 單號, sales.Date AS 時間, Customer.Name AS 銷售人員, Personnel.Name AS 客戶, sales.TypeOfPayment AS 付款方式, sales.Deposit AS 訂金, tmp.金額, sales.note AS 備註" & _
            " FROM ((sales LEFT JOIN (SELECT SalesLabel, sum(SellingPrice*Number) AS 金額 FROM SalesGoods GROUP BY SalesLabel)  AS tmp ON sales.label = tmp.SalesLabel) LEFT JOIN Customer ON sales.CustomerLabel = Customer.Label) LEFT JOIN Personnel ON sales.PersonnelLabel = Personnel.Label " & _
            " WHERE (((sales.[date]) Between #" & StartTime.ToString("yyyy/MM/dd HH:mm:ss") & "# And #" & EndTime.ToString("yyyy/MM/dd HH:mm:ss") & "#));"

            Dim DT As Data.DataTable = File.Read("table", File.BasePath, SQLCommand)
            Return DT
        End Function

        '取得銷貨單資訊
        Public Function GetSales(ByVal Label As String) As Sales
            Dim SQLCommand As String = "SELECT * FROM " & Sales.Table & " WHERE label='" & Label & "';"
            Dim DT As Data.DataTable = File.Read("table", File.BasePath, SQLCommand)

            Dim s As New Sales
            If DT Is Nothing OrElse DT.Rows.Count <= 0 Then Return s
            Return Sales.GetFrom(DT.Rows(0))

        End Function

        '取得銷貨單的商品清單-根據銷貨單號
        Public Function GetGoodsListBySalesLabel(ByVal Label As String) As Data.DataTable
            Dim SQLCommand As String = "SELECT SalesGoods.StockLabel, Goods.Kind, Goods.Brand, Goods.Name, Stock.Price, SalesGoods.SellingPrice, SalesGoods.Number" & _
            " FROM SalesGoods LEFT JOIN (Stock LEFT JOIN Goods ON Stock.GoodsLabel = Goods.Label) ON SalesGoods.StockLabel = Stock.Label" & _
            " WHERE (((SalesGoods.SalesLabel)=""" & Label & """));"
            Dim DT As Data.DataTable = File.Read("table", File.BasePath, SQLCommand)
            Return DT
        End Function

        '取得包含該商品的銷貨單
        Public Function GetGoodsListByStockLabel(ByVal Label As String) As Data.DataTable
            Dim SQLCommand As String = "SELECT SalesGoods.StockLabel, Goods.Kind, Goods.Brand, Goods.Name, Stock.Price, SalesGoods.SellingPrice, SalesGoods.Number" & _
            " FROM SalesGoods LEFT JOIN (Stock LEFT JOIN Goods ON Stock.GoodsLabel = Goods.Label) ON SalesGoods.StockLabel = Stock.Label" & _
            " WHERE (((SalesGoods.StockLabel)=""" & Label & """));"
            Dim DT As Data.DataTable = File.Read("table", File.BasePath, SQLCommand)
            Return DT
        End Function

        '新增銷貨單
        Public Sub CreateSales(ByVal newSales As Sales, ByVal Goods() As SalesGoods)
            CreateSalesWithoutEvent(newSales, Goods)
            RaiseEvent CreatedSales(newSales, Goods)

        End Sub

        '刪除銷貨單
        Public Sub DeleteSales(ByVal dSales As Sales)
            DeleteSalesWithoutEvent(dSales)
            RaiseEvent DeletedSales(dSales)
        End Sub

        '修改銷貨單
        Public Sub ChangeSales(ByVal newSales As Sales, ByVal Goods() As SalesGoods)
            DeleteSalesWithoutEvent(newSales)
            CreateSalesWithoutEvent(newSales, Goods)
            RaiseEvent ChangedSales(newSales, Goods)
        End Sub

        Private Sub CreateSalesWithoutEvent(ByVal newSales As Sales, ByVal Goods() As SalesGoods)
            Database.Access.File.AddBase(newSales)

            For Each g As SalesGoods In Goods
                Database.Access.File.AddBase(g)
            Next
        End Sub

        Private Sub DeleteSalesWithoutEvent(ByVal dSales As Sales)
            Dim SqlCommand As String = "DELETE FROM sales WHERE label='" & dSales.Label & "';"
            File.Command(SqlCommand, File.BasePath)

            SqlCommand = "DELETE FROM salesgoods WHERE saleslabel='" & dSales.Label & "';"
            File.Command(SqlCommand, File.BasePath)
        End Sub



        Public Class File

            Public Shared Dir As String
            Public Shared BasePath As String
            Public Shared SalesPath As String
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

            Public Shared Sub Close(ByVal DBControl As OleDb.OleDbConnection)
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
                CreateTable(Mobile.Table, Mobile.ToColumns, DBControl)
                CreateTable(Stock.Table, Stock.ToColumns, DBControl)
                CreateTable(Sales.Table, Sales.ToColumns, DBControl)
                CreateTable(SalesGoods.Table, SalesGoods.ToColumns, DBControl)
                Return DBControl
            End Function



            ''' <summary>新增Access檔案，同時加入索引，回傳所連接的資料庫元件</summary>
            ''' <param name="FilePath">檔案路徑</param>
            Private Shared Function CreateFileSales(ByVal FilePath As String) As OleDb.OleDbConnection
                CreateAccessFile(FilePath)
                Dim DBControl As OleDb.OleDbConnection = Open(FilePath)
                CreateTable(Sales.Table, Sales.ToColumns, DBControl)
                CreateTable(Order.Table, Order.ToColumns, DBControl)
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
            Public Shared Function Command(ByVal SqlCommand As String, ByVal File As String) As Long
                Dim DBControl As OleDb.OleDbConnection = ConnectBase(File)
                Dim Count As Long = Command(SqlCommand, DBControl)
                Close(DBControl)
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

            Public Shared Function Read(ByVal Table As String, ByVal File As String, ByVal SqlCommand As String) As Data.DataTable
                Return Read(Table, New String() {File}, New String() {SqlCommand})
            End Function

            Public Shared Function Read(ByVal Table As String, ByVal FileList() As String, ByVal SqlCommand As String) As Data.DataTable
                Return Read(Table, FileList, New String() {SqlCommand})
            End Function

            Public Shared Function Read(ByVal Table As String, ByVal FileList() As String, ByVal SQLCommand() As String) As Data.DataTable
                '暫存表格
                Dim DS As DataSet = New DataSet()
                Dim DA As OleDb.OleDbDataAdapter

                Dim TmpFile As String
                Dim totFile As Int16 = UBound(FileList) + 1
                If totFile = 0 Then Return Nothing

                Dim i As Integer
                For i = 1 To totFile
                    TmpFile = FileList(i - 1)

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

            Shared Function GetSqlDelete(ByVal Table As String, ByVal ColumnName As String, ByVal Text As String) As String
                Return "DELETE FROM " & Table & " WHERE [" & ColumnName & "]='" & Text & "';"
            End Function

            Public Shared Function GetUpdateSqlCommand(ByVal Table As String, ByVal column() As String, ByVal value() As String, ByVal ConditionColumn As String, ByVal ConditionText As String) As String
                Dim SQLCommand As String = "UPDATE " & Table & " SET "
                SQLCommand &= GetSqlColumnChangePart(column, value) & " WHERE [" & ConditionColumn & "]='" & ConditionText & "';"
                Return SQLCommand
            End Function

            Private Shared Function GetSqlColumnChangePart(ByVal Label() As String, ByVal value() As String) As String
                Dim lst As New List(Of String)

                For i As Integer = 0 To Label.Length - 1
                    lst.Add("[" & Label(i) & "]='" & value(i) & "'")
                Next i
                Return Join(lst.ToArray, ",")
            End Function

            Shared Function GetSqlValue(ByVal obj As Object) As String
                If obj Is Nothing Then
                    Return "''"
                ElseIf obj Is Nothing OrElse obj.GetType() Is GetType(String) Then
                    Return "'" & obj.ToString & "'"
                ElseIf obj.GetType Is GetType(Date) Then
                    Return CType(obj, Date).ToString("#yyyy/MM/dd HH:mm:ss#")
                Else
                    Return obj.ToString
                End If
            End Function


            Public Shared Sub AddBase(ByVal data As Object)
                Command(GetSqlInsert(data), BasePath)
            End Sub

            'Public Shared Sub AddSales(ByVal data As Object)
            '    CommandSales(data, SalesPath)
            'End Sub


            Delegate Function Conv(Of T)(ByVal d As Data.DataRow) As T

            Public Shared Function Read(Of T)(ByVal Table As String, ByVal C As Conv(Of T)) As T()
                Dim DT As Data.DataTable = Read(Supplier.Table, BasePath, GetSqlSelect(Table))

                Dim lstData As New List(Of T)


                If DT Is Nothing Then Return lstData.ToArray
                For i As Integer = 0 To DT.Rows.Count - 1
                    lstData.Add(C(DT.Rows(i)))
                Next
                Return lstData.ToArray
            End Function

            Public Shared Function ReadSupplier() As Supplier()
                Return Read(Of Supplier)(Supplier.Table, AddressOf Supplier.GetFrom)
            End Function

            Public Shared Function ReadPersonnel() As Personnel()
                Return Read(Of Personnel)(Personnel.Table, AddressOf Personnel.GetFrom)
            End Function

            Public Shared Function ReadGoods() As Goods()
                Return Read(Of Goods)(Goods.Table, AddressOf Goods.GetFrom)
            End Function

            Public Shared Function ReadMobile() As Mobile()
                Return Read(Of Mobile)(Mobile.Table, AddressOf Mobile.GetFrom)
            End Function
#Region "hide"
            '''' <summary>新增供應商</summary>
            'Public Shared Sub AddSupplier(ByVal data As Supplier)
            '    CommandBase(GetSqlInsert(data), BasePath)
            'End Sub

            '''' <summary>新增客戶</summary>
            'Public Shared Sub AddCustomer(ByVal data As Customer)
            '    CommandBase(GetSqlInsert(data), BasePath)
            'End Sub

            '''' <summary>新增員工</summary>
            'Public Shared Sub AddPersonnel(ByVal data As Personnel)
            '    CommandBase(GetSqlInsert(data), BasePath)
            'End Sub

            '''' <summary>新增商品</summary>
            'Public Shared Sub AddGoods(ByVal data As Goods)
            '    CommandBase(GetSqlInsert(data), BasePath)
            'End Sub

            '''' <summary>新增門號</summary>
            'Public Shared Sub AddMobile(ByVal data As Mobile)
            '    CommandBase(GetSqlInsert(data), BasePath)
            'End Sub

            '''' <summary>新增庫存</summary>
            'Public Shared Sub AddStock(ByVal data As Stock)
            '    CommandBase(GetSqlInsert(data), BasePath)
            'End Sub

            '''' <summary>新增銷貨單</summary>
            'Public Shared Sub AddSales(ByVal data As Sales)
            '    CommandSales(GetSqlInsert(data), SalesPath)
            'End Sub

            '''' <summary>新增訂單</summary>
            'Public Shared Sub AddOrder(ByVal data As Order)
            '    CommandSales(GetSqlInsert(data), SalesPath)
            'End Sub
#End Region

        End Class

        ''' <summary>新增供應商</summary>
        Public Sub AddSupplier(ByVal data As Supplier)
            File.AddBase(data)
            SupplierList.Add(data)
            RaiseEvent CreatedSupplier(data)
        End Sub

        Public Sub DeleteSupplier(ByVal data As Supplier)
            File.Command(File.GetSqlDelete(Supplier.Table, "Label", data.Label), File.BasePath)
            RaiseEvent DeletedSupplier(data)
        End Sub

        Public Sub ChangeSupplier(ByVal data As Supplier)
            File.Command(data.GetUpdateSqlCommand(), File.BasePath)
            RaiseEvent ChangedSupplier(data)
        End Sub

        ''' <summary>新增客戶</summary>
        Public Sub AddCustomer(ByVal data As Customer)
            File.AddBase(data)
        End Sub

        ''' <summary>新增員工</summary>
        Public Sub AddPersonnel(ByVal data As Personnel)
            File.AddBase(data)
            PersonnelList.Add(data)
        End Sub

        ''' <summary>新增商品</summary>
        Public Sub AddGoods(ByVal data As Goods)
            File.AddBase(data)
            GoodsList.Add(data)
            RaiseEvent CreatedGoods(data)
        End Sub

        Public Sub DeleteGoods(ByVal data As Goods)
            File.Command(File.GetSqlDelete(Goods.Table, "Label", data.Label), File.BasePath)
            RaiseEvent DeletedGoods(data)
        End Sub

        Public Sub ChangeGoods(ByVal data As Goods)
            File.Command(data.GetUpdateSqlCommand(), File.BasePath)
            RaiseEvent ChangedGoods(data)
        End Sub

        ''' <summary>新增門號</summary>
        Public Sub AddMobile(ByVal data As Mobile)
            File.AddBase(data)
            MobileList.Add(data)
        End Sub

        ''' <summary>新增庫存</summary>
        Public Sub AddStock(ByVal data As Stock)
            File.AddBase(data)
            RaiseEvent CreatedStock(data)
        End Sub

        '''' <summary>新增銷貨單</summary>
        'Public Sub AddSales(ByVal data As Sales)
        '    File.AddSales(data)
        'End Sub

        '''' <summary>新增訂單</summary>
        'Public Sub AddOrder(ByVal data As Order)
        '    File.AddSales(data)
        'End Sub

        Public Function ReadSupplier() As Supplier()
            Return File.ReadSupplier()
        End Function

        Public Function ReadPersonnel() As Personnel()
            Return File.ReadPersonnel
        End Function

        Public Function ReadGoods() As Goods()
            Return File.ReadGoods
        End Function

        Public Function ReadMobile() As Mobile()
            Return File.ReadMobile()
        End Function




    End Class


End Namespace