
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


        Public Sub New()
            File.Dir = My.Application.Info.DirectoryPath & "\data"
            File.BasePath = File.Dir & "\base.mdb"
            File.SalesPath = File.Dir & "\sales.mdb"
        End Sub

        ''' <summary>取得庫存清單</summary>
        Public Function GetStockList() As Data.DataTable
            Dim SQLCommand As String = "SELECT a.Label as 庫存編號,IMEI,Kind as 種類, Brand as 廠牌, [Name] as 品名,  Number as 數量 , Price as 售價, stock.Note as 備註 FROM stock LEFT JOIN goods AS [a] ON stock.GoodsLabel = [a].Label;"
            Dim DT As Data.DataTable = File.Read("table", File.BasePath, SQLCommand)
            Return DT
        End Function


        Public Function GetSalesList() As Data.DataTable
            Dim SQLCommand As String = "SELECT * FROM Sales, SalesGoods;"

        End Function

        Public Class File

            Public Shared Dir As String
            Public Shared BasePath As String
            Public Shared SalesPath As String

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

            ''' <summary>連線資料庫，回傳所連接資料庫元件，若檔案不存在則新增該檔案</summary>
            ''' <param name="FilePath">檔案路徑</param>
            Public Shared Function ConnectSales(ByVal FilePath As String) As OleDb.OleDbConnection
                Dim Dir As String = Left(FilePath, FilePath.LastIndexOf("\"))

                If Not IO.Directory.Exists(Dir) Then IO.Directory.CreateDirectory(Dir)

                If Not IO.File.Exists(FilePath) Then
                    Return CreateFileSales(FilePath)
                Else
                    Return Open(FilePath)
                End If
            End Function

            ''' <summary>連接資料庫，回傳所連接的資料庫元件</summary>
            ''' <param name="FilePath">檔案路徑</param>
            Private Shared Function Open(ByVal FilePath As String) As OleDb.OleDbConnection
                Dim DBControl As OleDb.OleDbConnection

                '指定檔案路徑
                DBControl = New OleDb.OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=" & FilePath)
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
                Try : newDatabase.Create("PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=" & FilePath) : Catch : End Try
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
            Private Shared Function CommandBase(ByVal SqlCommand As String, ByVal File As String) As Long
                Dim DBControl As OleDb.OleDbConnection = ConnectBase(File)
                Dim Count As Long = Command(SqlCommand, DBControl)
                Close(DBControl)
                Return Count
            End Function

            ''' <summary>對資料庫下達SQL指令</summary>
            ''' <param name="SqlCommand">SQL字串</param>
            ''' <param name="File">檔按路徑</param>
            Private Shared Function CommandSales(ByVal SqlCommand As String, ByVal File As String) As Long
                Dim DBControl As OleDb.OleDbConnection = ConnectSales(File)
                Dim Count As Long = Command(SqlCommand, DBControl)
                Close(DBControl)
                Return Count
            End Function

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
                    Dim tmpDB As OleDb.OleDbConnection = New OleDb.OleDbConnection("PROVIDER=MICROSOFT.Jet.OLEDB.4.0;DATA SOURCE=" & TmpFile)
                    Try
                        tmpDB.Open()
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

            Shared Function GetSqlSelect(ByVal Table As String)
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
                CommandBase(GetSqlInsert(data), BasePath)
            End Sub

            Public Shared Sub AddSales(ByVal data As Object)
                CommandSales(data, SalesPath)
            End Sub


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
        End Sub

        ''' <summary>新增門號</summary>
        Public Sub AddMobile(ByVal data As Mobile)
            File.AddBase(data)
            MobileList.Add(data)
        End Sub

        ''' <summary>新增庫存</summary>
        Public Sub AddStock(ByVal data As Stock)
            File.AddBase(data)
        End Sub

        ''' <summary>新增銷貨單</summary>
        Public Sub AddSales(ByVal data As Sales)
            File.AddSales(data)
        End Sub

        ''' <summary>新增訂單</summary>
        Public Sub AddOrder(ByVal data As Order)
            File.AddSales(data)
        End Sub

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