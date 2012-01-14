Imports 進銷存.Database.Access

Namespace Database

    Public Module StructureBase

        Dim DBTypeIMEI As String = "char(20)"
        Dim DBTypeLabel As String = "char(20)"
        Dim DBTypeName As String = "char(10)"
        Dim DBTypeNote As String = "char(20)"
        Dim DBTypeTel As String = "char(15)"
        Dim DBTypeAddr As String = "char(30)"
        Dim DBTypeDate As String = "date"
        Dim DBTypeSingle As String = "single"
        Dim DBTypeInteger As String = "int"

        Dim TimeFormat As String = "#yyyy/MM/dd HH:mm:ss#"

        Private Function Bracket(ByVal Text As String) As String
            Return "'" & Text & """"
        End Function

        Class MyDataRow
            Dim Row As Data.DataRow
            Sub New(ByVal Row As Data.DataRow)
                Me.Row = Row
            End Sub
            Default Public ReadOnly Property Item(ByVal Label As String)
                Get
                    If System.DBNull.Equals(Row.Item(Label), System.DBNull.Value) Then Return ""
                    Return RTrim(Row.Item(Label))
                End Get
            End Property

        End Class

#Region "供應商/客戶/員工"
        ''' <summary>供應商</summary>
        Structure Supplier
            Shared Table As String = "Supplier"
            ''' <summary>供應商識別碼</summary>
            Dim Label As String
            ''' <summary>名稱</summary>
            Dim Name As String
            ''' <summary>電話1</summary>
            Dim Tel1 As String
            ''' <summary>電話2</summary>
            Dim Tel2 As String
            ''' <summary>地址</summary>
            Dim Addr As String
            ''' <summary>備註</summary>
            Dim Note As String
            Shared Function ToColumns() As Column()
                Dim Columns As New List(Of Column)
                Columns.Add(New Column("Label", DBTypeLabel))
                Columns.Add(New Column("Name", DBTypeName))
                Columns.Add(New Column("Tel1", DBTypeTel))
                Columns.Add(New Column("Tel2", DBTypeTel))
                Columns.Add(New Column("Addr", DBTypeAddr))
                Columns.Add(New Column("Note", DBTypeNote))
                Return Columns.ToArray
            End Function

            Function ToObjects() As Object()
                Return New Object() {Label, Name, Tel1, Tel2, Addr, Note}
            End Function

            Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Supplier
                Dim R As New MyDataRow(Row)
                Dim data As Supplier
                data.Label = R("Label")
                data.Name = R("Name")
                data.Tel1 = R("Tel1")
                data.Tel2 = R("Tel2")
                data.Addr = R("Addr")
                data.Note = R("Note")
                Return data
            End Function

            Public Shared Function Null() As Supplier
                Dim s As New Supplier
                s.Label = ""
                Return s
            End Function

            Public Function IsNull() As Boolean
                Return Label = ""
            End Function

            Public Function GetUpdateSqlCommand() As String
                Dim Column As String() = New String() {"Name", "Tel1", "Tel2", "Addr", "Note"}
                Dim Value As String() = New String() {Name, Tel1, Tel2, Addr, Note}
                Return File.GetUpdateSqlCommand(Table, Column, Value, "Label", Label)
            End Function

            Public Overrides Function ToString() As String
                If IsNull() Then Return ""
                Return Join(Array.ConvertAll(ToObjects(), Function(o As Object) o.ToString), ",")
            End Function
        End Structure

        ''' <summary>客戶</summary>
        Structure Customer
            Shared Table As String = "Customer"
            ''' <summary>客戶識別碼</summary>
            Dim Label As String
            ''' <summary>姓名</summary>
            Dim Name As String
            ''' <summary>電話1</summary>
            Dim Tel1 As String
            ''' <summary>電話2</summary>
            Dim Tel2 As String
            ''' <summary>地址</summary>
            Dim Addr As String
            ''' <summary>備註</summary>
            Dim Note As String
            Shared Function ToColumns() As Column()
                Dim Columns As New List(Of Column)
                Columns.Add(New Column("Label", DBTypeLabel))
                Columns.Add(New Column("Name", DBTypeName))
                Columns.Add(New Column("Tel1", DBTypeTel))
                Columns.Add(New Column("Tel2", DBTypeTel))
                Columns.Add(New Column("Addr", DBTypeAddr))
                Columns.Add(New Column("Note", DBTypeNote))
                Return Columns.ToArray
            End Function
            Function ToObjects() As Object()
                Return New Object() {Label, Name, Tel1, Tel2, Addr, Note}
            End Function
            Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Customer
                Dim R As New MyDataRow(Row)
                Dim data As Customer
                data.Label = R("Label")
                data.Name = R("Name")
                data.Tel1 = R("Tel1")
                data.Tel2 = R("Tel2")
                data.Addr = R("Addr")
                data.Note = R("Note")
                Return data
            End Function
        End Structure

        ''' <summary>員工</summary>
        Structure Personnel
            Shared Table As String = "Personnel"
            ''' <summary>員工識別碼</summary>
            Dim Label As String
            ''' <summary>姓名</summary>
            Dim Name As String
            ''' <summary>電話1</summary>
            Dim Tel1 As String
            ''' <summary>電話2</summary>
            Dim Tel2 As String
            ''' <summary>地址</summary>
            Dim Addr As String
            ''' <summary>備註</summary>
            Dim Note As String
            Shared Function ToColumns() As Column()
                Dim Columns As New List(Of Column)
                Columns.Add(New Column("Label", DBTypeLabel))
                Columns.Add(New Column("Name", DBTypeName))
                Columns.Add(New Column("Tel1", DBTypeTel))
                Columns.Add(New Column("Tel2", DBTypeTel))
                Columns.Add(New Column("Addr", DBTypeAddr))
                Columns.Add(New Column("Note", DBTypeNote))
                Return Columns.ToArray
            End Function
            Function ToObjects() As Object()
                Return New Object() {Label, Name, Tel1, Tel2, Addr, Note}
            End Function
            Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Personnel
                Dim R As New MyDataRow(Row)
                Dim data As Personnel
                data.Label = R("Label")
                data.Name = R("Name")
                data.Tel1 = R("Tel1")
                data.Tel2 = R("Tel2")
                data.Addr = R("Addr")
                data.Note = R("Note")
                Return data
            End Function
        End Structure
#End Region

#Region "商品"
        ''' <summary>商品</summary>
        Structure Goods
            Shared Table As String = "Goods"
            ''' <summary>商品識別碼</summary>
            Dim Label As String
            ''' <summary>品名</summary>
            Dim Name As String
            ''' <summary>種類</summary>
            Dim Kind As String
            ''' <summary>品牌</summary>
            Dim Brand As String
            ''' <summary>備註</summary>
            Dim Note As String

            Shared Function ToColumns() As Column()
                Dim Columns As New List(Of Column)
                Columns.Add(New Column("Label", DBTypeLabel))
                Columns.Add(New Column("Name", DBTypeName))
                Columns.Add(New Column("Kind", "char(10)"))
                Columns.Add(New Column("Brand", "char(10)"))
                Columns.Add(New Column("Note", DBTypeNote))
                Return Columns.ToArray
            End Function
            Function ToObjects() As Object()
                Return New Object() {Label, Name, Kind, Brand, Note}
            End Function

            Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Goods
                Dim R As New MyDataRow(Row)
                Dim data As Goods
                data.Label = R("Label")
                data.Name = R("Name")
                data.Kind = R("Kind")
                data.Brand = R("Brand")
                data.Note = R("Note")
                Return data
            End Function

            Public Function GetUpdateSqlCommand() As String
                Dim Column As String() = New String() {"Name", "Kind", "Brand", "Note"}
                Dim Value As String() = New String() {Name, Kind, Brand, Note}
                Return File.GetUpdateSqlCommand(Table, Column, Value, "Label", Label)
            End Function

            Public Function IsNull() As Boolean
                Return Label = ""
            End Function

            Public Shared Function Null() As Goods
                Dim g As New Goods
                g.Label = ""
                Return g
            End Function

            Public Overrides Function ToString() As String
                If IsNull() Then Return ""
                Return Join(Array.ConvertAll(ToObjects(), Function(o As Object) o.ToString), ",")
            End Function
End Structure
#End Region
#Region "門號"
        ''' <summary>門號</summary>
        Structure Mobile
            Shared Table As String = "Mobile"
            ''' <summary>門號識別碼</summary>
            Dim Label As String
            ''' <summary>名稱</summary>
            Dim Name As String
            ''' <summary>佣金</summary>
            Dim Commission As Single
            ''' <summary>折扣</summary>
            Dim Discount As Single
            ''' <summary>備註</summary>
            Dim Note As String

            Shared Function ToColumns() As Column()
                Dim Columns As New List(Of Column)
                Columns.Add(New Column("Label", DBTypeLabel))
                Columns.Add(New Column("Name", DBTypeName))
                Columns.Add(New Column("Commission", DBTypeSingle))
                Columns.Add(New Column("Discount", DBTypeSingle))
                Columns.Add(New Column("Note", DBTypeNote))
                Return Columns.ToArray
            End Function
            Function ToObjects() As Object()
                Return New Object() {Label, Name, Commission, Discount, Note}
            End Function

            Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Mobile
                Dim R As New MyDataRow(Row)
                Dim data As Mobile
                data.Label = R("Label")
                data.Name = R("Name")
                data.Commission = R("Commission")
                data.Discount = R("Discount")
                data.Note = R("Note")
                Return data
            End Function
        End Structure
#End Region

#Region "庫存"
        ''' <summary>庫存</summary>
        Structure Stock
            Shared Table As String = "Stock"
            ''' <summary>庫存識別碼</summary>
            Dim Label As String
            ''' <summary>商品識別碼</summary>
            Dim GoodsLabel As String
            ''' <summary>供應商識別碼</summary>
            Dim SupplierLabel As String
            ''' <summary>進貨日期</summary>
            Dim [Date] As Date
            ''' <summary>IMEI碼</summary>
            Dim IMEI As String
            ''' <summary>進貨價</summary>
            Dim Cost As Single
            ''' <summary>定價</summary>
            Dim Price As Single
            ''' <summary>數量</summary>
            Dim Number As Integer
            ''' <summary>備註</summary>
            Dim Note As String

            Shared Function ToColumns() As Column()
                Dim Columns As New List(Of Column)
                Columns.Add(New Column("Label", DBTypeLabel))
                Columns.Add(New Column("GoodsLabel", DBTypeLabel))
                Columns.Add(New Column("SupplierLabel", DBTypeLabel))
                Columns.Add(New Column("Date", DBTypeDate))
                Columns.Add(New Column("IMEI", DBTypeIMEI))
                Columns.Add(New Column("Cost", DBTypeSingle))
                Columns.Add(New Column("Price", DBTypeSingle))
                Columns.Add(New Column("Number", DBTypeInteger))
                Columns.Add(New Column("Note", DBTypeNote))
                Return Columns.ToArray
            End Function

            Function ToObjects() As Object()
                Return New Object() {Label, GoodsLabel, SupplierLabel, [Date].ToString("yyyy/MM/dd HH:mm:ss"), IMEI, Cost, Price, Number, Note}
            End Function

            Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Stock
                Dim R As New MyDataRow(Row)
                Dim data As Stock
                data.Label = R("Label")
                data.GoodsLabel = R("GoodsLabel")
                data.SupplierLabel = R("SupplierLabel")
                data.Date = R("Date")
                data.IMEI = R("IMEI")
                data.Cost = R("Cost")
                data.Price = R("Price")
                data.Number = R("Number")
                data.Note = R("Note")
                Return data
            End Function


            Public Function GetUpdateSqlCommand()
                Dim SQLCommand As String = "UPDATE " & Table & " SET "
                Dim label() As String = New String() {"GoodsLabel", "SupplierLabel", "Date", "IMEI", "Cost", "Price", "Number", "Note"}
                Dim value() As String = New String() {"'" & GoodsLabel & "'", "'" & SupplierLabel & "'", [Date].ToString("#yyyy/MM/dd HH:mm:ss#"), "'" & IMEI & "'", Cost, Price, Number, "'" & Note & "'"}

                SQLCommand &= GetSqlColumnChangePart(label, value) & " WHERE Label='" & Me.Label & "';"
                Return SQLCommand
            End Function

            Public Function GetUpdateSqlCommand(ByVal Table As String, ByVal column() As String, ByVal value() As String, ByVal ConditionColumn As String, ByVal ConditionText As String) As String
                Dim SQLCommand As String = "UPDATE " & Table & " SET "
                SQLCommand &= GetSqlColumnChangePart(column, value) & " WHERE [" & ConditionColumn & "]='" & ConditionText & "';"
                Return SQLCommand
            End Function

            Private Function GetSqlColumnChangePart(ByVal Label() As String, ByVal value() As String) As String
                Dim lst As New List(Of String)

                For i As Integer = 0 To Label.Length - 1
                    lst.Add("[" & Label(i) & "]=" & value(i))
                Next i
                Return Join(lst.ToArray, ",")
            End Function

        End Structure
#End Region


        ''' <summary>付款方式</summary>
        Enum TypeOfPayment
            Unpaid = 0
            Cash = 1
            Card = 2
            Commission = 3
        End Enum

        Public TypeOfPaymentsDescribe As String() = New String() {"未付", "現金", "刷卡", "訂金"}


        ''' <summary>商品種類：商品/門號</summary>
        Enum TypeOfGoods
            Goods = 0
            MobileNumber = 1
        End Enum

        Structure SalesGoods
            Shared Table As String = "SalesGoods"
            ''' <summary>單號識別碼</summary>
            Dim SalesLabel As String
            ''' <summary>庫存識別碼</summary>
            Dim StockLabel As String
            ''' <summary>單價</summary>
            Dim SellingPrice As Single
            ''' <summary>數量</summary>
            Dim Number As Integer

            Shared Function ToColumns() As Column()
                Dim Columns As New List(Of Column)
                Columns.Add(New Column("SalesLabel", DBTypeLabel))
                Columns.Add(New Column("StockLabel", DBTypeLabel))
                Columns.Add(New Column("SellingPrice", DBTypeSingle))
                Columns.Add(New Column("Number", DBTypeInteger))
                Return Columns.ToArray
            End Function

            Function ToObjects() As Object()
                Return New Object() {SalesLabel, StockLabel, SellingPrice, Number}
            End Function

            Public Shared Function GetFrom(ByVal Row As Data.DataRow) As SalesGoods
                Dim R As New MyDataRow(Row)
                Dim data As SalesGoods
                data.SalesLabel = R("SalesLabel")
                data.StockLabel = R("StockLabel")
                data.SellingPrice = R("SellingPrice")
                data.Number = R("Number")
                Return data
            End Function

        End Structure

        ''' <summary>銷貨</summary>
        Structure Sales
            Shared Table As String = "Sales"
            ''' <summary>單號識別碼</summary>
            Dim Label As String
            ''' <summary>銷貨日期</summary>
            Dim [Date] As Date
            ''' <summary>客戶識別碼</summary>
            Dim CustomerLabel As String
            ''' <summary>員工識別碼</summary>
            Dim PersonnelLabel As String
            ''' <summary>訂金</summary>
            Dim Deposit As Single
            ''' <summary>付款方式</summary>
            Dim TypeOfPayment As TypeOfPayment
            ''' <summary>備註</summary>
            Dim Note As String

            Shared Function ToColumns() As Column()
                Dim Columns As New List(Of Column)
                Columns.Add(New Column("Label", DBTypeLabel))
                Columns.Add(New Column("Date", DBTypeDate))
                Columns.Add(New Column("CustomerLabel", DBTypeLabel))
                Columns.Add(New Column("PersonnelLabel", DBTypeLabel))
                Columns.Add(New Column("Deposit", DBTypeSingle))
                Columns.Add(New Column("TypeOfPayment", DBTypeInteger))
                Columns.Add(New Column("Note", DBTypeNote))
                Return Columns.ToArray
            End Function

            Function ToObjects() As Object()
                Return New Object() {Label, [Date], CustomerLabel, PersonnelLabel, Deposit, CType(TypeOfPayment, Int16), Note}
            End Function

            'Private Function GetTypeOfPaymentNumber() As Integer
            '    TypeOfPayment.
            'End Function


            Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Sales
                Dim R As New MyDataRow(Row)
                Dim data As Sales
                data.Label = R("Label")
                data.Date = R("Date")
                data.CustomerLabel = R("CustomerLabel")
                data.PersonnelLabel = R("PersonnelLabel")
                data.Deposit = R("Deposit")
                data.TypeOfPayment = R("TypeOfPayment")
                data.Note = R("Note")
                Return data
            End Function
        End Structure



#Region "訂單"
        ''' <summary>訂單</summary>
        Structure Order
            Shared Table As String = "Order"
            ''' <summary>訂單識別碼</summary>
            Dim Label As String
            ''' <summary>下訂日期</summary>
            Dim [Date] As Date
            ''' <summary>商品識別碼</summary>
            Dim GoodsLabel As String
            ''' <summary>最終售價</summary>
            Dim SellingPrice As Single
            ''' <summary>訂金</summary>
            Dim Deposit As Single
            ''' <summary>客戶識別碼</summary>
            Dim CustomerLabel As String
            ''' <summary>員工識別碼</summary>
            Dim PersonnelLabel As String
            ''' <summary>備註</summary>
            Dim Note As String

            Shared Function ToColumns() As Column()
                Dim Columns As New List(Of Column)
                Columns.Add(New Column("Label", DBTypeLabel))
                Columns.Add(New Column("Date", DBTypeDate))
                Columns.Add(New Column("GoodsLabel", DBTypeLabel))
                Columns.Add(New Column("SellingPrice", DBTypeSingle))
                Columns.Add(New Column("Deposit", DBTypeSingle))
                Columns.Add(New Column("CustomerLabel", DBTypeLabel))
                Columns.Add(New Column("PersonnelLabel", DBTypeLabel))
                Columns.Add(New Column("Note", DBTypeNote))
                Return Columns.ToArray
            End Function

            Function ToObjects() As Object()
                Return New Object() {Label, [Date], GoodsLabel, SellingPrice, Deposit, CustomerLabel, PersonnelLabel, Note}
            End Function

            Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Order
                Dim R As New MyDataRow(Row)
                Dim data As Order
                data.Label = R("Label")
                data.Date = R("Date")
                data.GoodsLabel = R("GoodsLabel")
                data.SellingPrice = R("SellingPrice")
                data.Deposit = R("Deposit")
                data.CustomerLabel = R("CustomerLabel")
                data.PersonnelLabel = R("PersonnelLabel")
                data.Note = R("Note")
                Return data
            End Function
        End Structure
#End Region
    End Module
End Namespace