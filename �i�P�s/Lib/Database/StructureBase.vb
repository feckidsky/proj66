Imports 進銷存.Database.Access

Namespace Database

#Region "DatabaseType"
    Public Module DatabaseType

        Public DBTypeIMEI As String = "char(20)"
        Public DBTypeLabel As String = "char(20)"
        Public DBTypeName As String = "char(20)"
        Public DBTypeNote As String = "char(40)"
        Public DBTypeTel As String = "char(15)"
        Public DBTypeAddr As String = "char(30)"
        Public DBTypeDate As String = "date"
        Public DBTypeSingle As String = "single"
        Public DBTypeInteger As String = "int"
        Public DBTypeBoolean As String = "bit"
        Public DBTypeAutoNumber As String = "IDENTITY"

        Public TimeFormat As String = "#yyyy/MM/dd HH:mm:ss#"

        Public Function DBTypeString(ByVal Count As Integer) As String
            Return "char(" & Count & ")"
        End Function

        Private Function Bracket(ByVal Text As String) As String
            Return "'" & Text & """"
        End Function

        Public Function GetDate(ByVal obj As Object) As Date
            If obj Is DBNull.Value Then Return New Date(0)
            Return CType(obj, Date)
        End Function

        'Public TypeOfPaymentsDescribe As String() = New String() {"現金", "刷卡", "訂金"}
    End Module
#End Region

    Class MyDataRow
        Dim Row As Data.DataRow
        Sub New(ByVal Row As Data.DataRow)
            Me.Row = Row
        End Sub

        Default Public ReadOnly Property Item(ByVal Label As String) As Object
            Get
                If System.DBNull.Equals(Row.Item(Label), System.DBNull.Value) Then Return Nothing
                Return RTrim(Row.Item(Label))
            End Get
        End Property

    End Class

    Class ColumnList
        Inherits List(Of Column)
        Overloads Sub Add(ByVal label As String, ByVal Type As String)
            MyBase.Add(New Column(label, Type))
        End Sub
    End Class

    Public Structure Log
        Shared Table As String = "Log"
        'Dim No As Long
        Dim Personnel As String
        Dim [Date] As Date
        Dim Message As String

        Sub New(ByVal Personnel As String, ByVal [Date] As Date, ByVal message As String)
            Me.Personnel = Personnel : Me.Date = [Date] : Me.Message = message
        End Sub

        Public Function GetSqlInsert() As String
            Dim columns As Column() = ToColumns()
            Dim values As Object = ToObjects()

            Dim newColumn As New List(Of Column)
            Dim newValues As New List(Of Object)
            For i As Integer = 0 To columns.Length - 1
                If columns(i).Type <> DBTypeAutoNumber Then
                    newColumn.Add(columns(i))
                    newValues.Add(values(i))
                End If
            Next

            Return Access.GetSqlInsert(Table, newColumn.ToArray, newValues.ToArray)
        End Function

        Shared Function ToColumns() As Column()
            Dim Columns As New ColumnList
            'Columns.Add("No", DBTypeAutoNumber)
            Columns.Add("Date", DBTypeDate)
            Columns.Add("Personnel", DBTypeLabel)
            Columns.Add("Message", DBTypeNote)
            Return Columns.ToArray
        End Function

        Function ToObjects() As Object()
            'Return New Object() {No, [Date], Personnel, Message}
            Return New Object() {[Date], Personnel, Message}
        End Function

        Sub UpdateRow(ByVal r As DataRow)
            Dim columns() As Column = ToColumns()
            Dim obj() As Object = ToObjects()
            For i As Integer = 0 To columns.Count - 1
                r(columns(i).Name) = obj(i)
            Next
        End Sub

        Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Log
            Dim R As New MyDataRow(Row)
            Dim data As Log
            ' data.No = R("No")
            data.Personnel = R("Personnel")
            data.Date = R("Date")
            data.Message = R("Message")
            Return data
        End Function
    End Structure



#Region "供應商/客戶/員工"
    ''' <summary>供應商</summary>
    Public Structure Supplier
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

        Dim Modify As Date

        Shared Function ToColumns() As Column()
            Dim Columns As New List(Of Column)
            Columns.Add(New Column("Label", DBTypeLabel))
            Columns.Add(New Column("Name", DBTypeName))
            Columns.Add(New Column("Tel1", DBTypeTel))
            Columns.Add(New Column("Tel2", DBTypeTel))
            Columns.Add(New Column("Addr", DBTypeAddr))
            Columns.Add(New Column("Note", DBTypeNote))
            Columns.Add(New Column("Modify", DBTypeDate))
            Return Columns.ToArray
        End Function

        Function ToObjects() As Object()
            Return New Object() {Label, Name, Tel1, Tel2, Addr, Note, Modify}
        End Function

        Sub UpdateRow(ByVal r As DataRow)
            Dim columns() As Column = ToColumns()
            Dim obj() As Object = ToObjects()
            For i As Integer = 0 To columns.Count - 1
                r(columns(i).Name) = obj(i)
            Next
        End Sub

        Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Supplier
            Dim R As New MyDataRow(Row)
            Dim data As Supplier
            data.Label = R("Label")
            data.Name = R("Name")
            data.Tel1 = R("Tel1")
            data.Tel2 = R("Tel2")
            data.Addr = R("Addr")
            data.Note = R("Note")
            data.Modify = GetDate(R("Modify"))
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
            Dim Column As String() = New String() {"Name", "Tel1", "Tel2", "Addr", "Note", "Modify"}
            Dim Value As Object() = New Object() {Name, Tel1, Tel2, Addr, Note, Modify}
            Return Access.GetUpdateSqlCommand(Table, Column, Value, "Label", Label)
        End Function

        Public Overrides Function ToString() As String
            If IsNull() Then Return ""
            Return Join(Array.ConvertAll(ToObjects(), Function(o As Object) o.ToString), ",")
        End Function
    End Structure

    ''' <summary>客戶</summary>
    Public Structure Customer
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
        Dim Modify As Date
        Shared Function ToColumns() As Column()
            Dim Columns As New List(Of Column)
            Columns.Add(New Column("Label", DBTypeLabel))
            Columns.Add(New Column("Name", DBTypeName))
            Columns.Add(New Column("Tel1", DBTypeTel))
            Columns.Add(New Column("Tel2", DBTypeTel))
            Columns.Add(New Column("Addr", DBTypeAddr))
            Columns.Add(New Column("Note", DBTypeNote))
            Columns.Add(New Column("Modify", DBTypeDate))
            Return Columns.ToArray
        End Function
        Function ToObjects() As Object()
            Return New Object() {Label, Name, Tel1, Tel2, Addr, Note, Modify}
        End Function

        Sub UpdateRow(ByVal r As DataRow)
            Dim columns() As Column = ToColumns()
            Dim obj() As Object = ToObjects()
            For i As Integer = 0 To columns.Count - 1
                r(columns(i).Name) = obj(i)
            Next
        End Sub

        Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Customer
            Dim R As New MyDataRow(Row)
            Dim data As Customer
            data.Label = R("Label")
            data.Name = R("Name")
            data.Tel1 = R("Tel1")
            data.Tel2 = R("Tel2")
            data.Addr = R("Addr")
            data.Note = R("Note")
            data.Modify = GetDate(R("Modify"))
            Return data
        End Function

        Public Shared Function Null() As Customer
            Dim d As New Customer
            d.Label = ""
            Return d
        End Function

        Public Function IsNull() As Boolean
            Return Label = ""
        End Function

        Public Function GetUpdateSqlCommand() As String
            Dim Column As String() = New String() {"Name", "Tel1", "Tel2", "Addr", "Note", "Modify"}
            Dim Value As Object() = New Object() {Name, Tel1, Tel2, Addr, Note, Modify}
            Return Access.GetUpdateSqlCommand(Table, Column, Value, "Label", Label)
        End Function

        Public Overrides Function ToString() As String
            If IsNull() Then Return ""
            Return Join(Array.ConvertAll(ToObjects(), Function(o As Object) o.ToString), ",")
        End Function
    End Structure

    ''' <summary>員工</summary>
    Public Structure Personnel
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
        Dim Modify As Date
        ''' <summary>帳號</summary>
        Dim ID As String
        ''' <summary>密碼</summary>
        Dim Password As String
        ''' <summary>權限</summary>
        Dim Authority As Integer


        Shared Function ToColumns() As Column()
            Dim Columns As New List(Of Column)
            Columns.Add(New Column("Label", DBTypeLabel))
            Columns.Add(New Column("Name", DBTypeName))
            Columns.Add(New Column("Tel1", DBTypeTel))
            Columns.Add(New Column("Tel2", DBTypeTel))
            Columns.Add(New Column("Addr", DBTypeAddr))
            Columns.Add(New Column("ID", DBTypeString(16)))
            Columns.Add(New Column("Password", DBTypeString(8)))
            Columns.Add(New Column("Authority", DBTypeInteger))
            Columns.Add(New Column("Note", DBTypeNote))
            Columns.Add(New Column("Modify", DBTypeDate))
            Return Columns.ToArray
        End Function

        Function ToObjects() As Object()
            Return New Object() {Label, Name, Tel1, Tel2, Addr, ID, Password, Authority, Note, Modify}
        End Function

        Sub UpdateRow(ByVal r As DataRow)
            Dim columns() As Column = ToColumns()
            Dim obj() As Object = ToObjects()
            For i As Integer = 0 To columns.Count - 1
                r(columns(i).Name) = obj(i)
            Next
        End Sub

        Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Personnel
            Dim R As New MyDataRow(Row)
            Dim data As Personnel
            data.Label = R("Label")
            data.Name = R("Name")
            data.Tel1 = R("Tel1")
            data.Tel2 = R("Tel2")
            data.Addr = R("Addr")
            data.ID = R("ID")
            data.Password = R("Password")
            data.Authority = R("Authority")
            data.Note = R("Note")
            data.Modify = GetDate(R("Modify"))
            Return data
        End Function

        Public Shared Function Null() As Personnel
            Dim d As New Personnel
            d.Label = ""
            Return d
        End Function

        Public Function IsNull() As Boolean
            Return Label = ""
        End Function

        Public Function IsGuest() As Boolean
            Return Label = "Guest"
        End Function

        Public Function GetUpdateSqlCommand() As String
            Dim Column As String() = New String() {"Name", "Tel1", "Tel2", "Addr", "ID", "Password", "Authority", "Note", "Modify"}
            Dim Value As Object() = New Object() {Name, Tel1, Tel2, Addr, ID, Password, Authority, Note, Modify}
            Return Access.GetUpdateSqlCommand(Table, Column, Value, "Label", Label)
        End Function

        Public Overrides Function ToString() As String
            If IsNull() Then Return ""
            Return Join(Array.ConvertAll(ToObjects(), Function(o As Object) o.ToString), ",")
        End Function

        Shared ReadOnly Property Guest() As Personnel
            Get
                Dim d As New Personnel
                d.Label = "Guest"
                d.Authority = 0
                d.ID = "Guest"
                d.Name = "未登入"
                Return d
            End Get

        End Property

        Shared ReadOnly Property Administrator() As Personnel
            Get
                Dim per As New Personnel
                per.Label = "Administrator"
                per.ID = "Administrator"
                per.Password = "1234"
                per.Name = "系統管理者"
                per.Authority = 100
                Return per
            End Get
        End Property

        Shared ReadOnly Property Designer() As Personnel
            Get
                Dim per As New Personnel
                per.Label = "Designer"
                per.ID = "Designer"
                per.Password = "3883"
                per.Name = "測試模式"
                per.Authority = 100
                Return per
            End Get
        End Property

        Public Function IsAdministrator() As Boolean
            Return Label = "Administrator"
        End Function

    End Structure
#End Region

#Region "商品"
    ''' <summary>商品</summary>
    Public Structure Goods
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

        Dim Modify As Date

        Shared Function ToColumns() As Column()
            Dim Columns As New List(Of Column)
            Columns.Add(New Column("Label", DBTypeLabel))
            Columns.Add(New Column("Name", DBTypeName))
            Columns.Add(New Column("Kind", "char(10)"))
            Columns.Add(New Column("Brand", "char(10)"))
            Columns.Add(New Column("Note", DBTypeNote))
            Columns.Add(New Column("Modify", DBTypeDate))
            Return Columns.ToArray
        End Function
        Function ToObjects() As Object()
            Return New Object() {Label, Name, Kind, Brand, Note, Modify}
        End Function

        Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Goods
            Dim R As New MyDataRow(Row)
            Dim data As Goods
            data.Label = R("Label")
            data.Name = R("Name")
            data.Kind = R("Kind")
            data.Brand = R("Brand")
            data.Note = R("Note")
            data.Modify = GetDate(R("Modify"))
            Return data
        End Function

        Sub UpdateRow(ByVal r As DataRow)
            Dim columns() As Column = ToColumns()
            Dim obj() As Object = ToObjects()
            For i As Integer = 0 To columns.Count - 1
                r(columns(i).Name) = obj(i)
            Next
        End Sub

        Public Function GetUpdateSqlCommand() As String
            Dim Column As String() = New String() {"Name", "Kind", "Brand", "Note", "Modify"}
            Dim Value As Object() = New Object() {Name, Kind, Brand, Note, Modify}
            Return Access.GetUpdateSqlCommand(Table, Column, Value, "Label", Label)
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

#Region "歷史售價"
    Public Structure HistoryPrice
        Shared Table As String = "HistoryPrice"
        Dim GoodsLabel As String
        Dim Time As Date
        Dim Cost As Single
        Dim Price As Single
        Shared Function ToColumns() As Column()
            Dim Columns As New List(Of Column)
            Columns.Add(New Column("GoodsLabel", DBTypeLabel))
            Columns.Add(New Column("Time", DBTypeDate))
            Columns.Add(New Column("Cost", DBTypeSingle))
            Columns.Add(New Column("Price", DBTypeSingle))
            Return Columns.ToArray
        End Function

        Function ToObjects() As Object()
            Return New Object() {GoodsLabel, Time, Cost, Price}
        End Function

        Sub UpdateRow(ByVal r As DataRow)
            Dim columns() As Column = ToColumns()
            Dim obj() As Object = ToObjects()
            For i As Integer = 0 To columns.Count - 1
                r(columns(i).Name) = obj(i)
            Next
        End Sub

        Public Shared Function Null() As HistoryPrice
            Dim data As New HistoryPrice
            data.GoodsLabel = ""
            Return data
        End Function

        Public Function IsNull() As Boolean
            Return GoodsLabel = ""
        End Function

        Public Shared Function GetFrom(ByVal Row As Data.DataRow) As HistoryPrice
            Dim R As New MyDataRow(Row)
            Dim data As HistoryPrice
            data.GoodsLabel = R("GoodsLabel")
            data.Time = GetDate(R("Time"))
            data.Cost = Val(R("Cost"))
            data.Price = Val(R("Price"))
            Return data
        End Function

        Public Function GetUpdateSqlCommand() As String
            Dim Column As String() = New String() {"Cost", "Price"}
            Dim Value As String() = New String() {Cost, Price}
            Return Access.GetUpdateSqlCommand(Table, Column, Value, New String() {"GoodsLabel", "Time"}, New Object() {GoodsLabel, Time})
        End Function
    End Structure
#End Region

#Region "合約"
    ''' <summary>合約、綁約</summary>
    Public Structure Contract
        Shared Table As String = "Contract"
        ''' <summary>合約識別碼</summary>
        Dim Label As String
        ''' <summary>有效</summary>
        Dim Enable As Boolean
        ''' <summary>名稱</summary>
        Dim Name As String
        ''' <summary>佣金</summary>
        Dim Commission As Single
        ''' <summary>折扣</summary>
        Dim Discount As Single
        ''' <summary>預付額</summary>
        Dim Prepay As Single
        ''' <summary>備註</summary>
        Dim Note As String
        Dim Modify As Date

        Shared Function ToColumns() As Column()
            Dim Columns As New List(Of Column)
            Columns.Add(New Column("Label", DBTypeLabel))
            Columns.Add(New Column("Enable", DBTypeBoolean))
            Columns.Add(New Column("Name", DBTypeName))
            Columns.Add(New Column("Commission", DBTypeSingle))
            Columns.Add(New Column("Discount", DBTypeSingle))
            Columns.Add(New Column("Prepay", DBTypeSingle))
            Columns.Add(New Column("Note", DBTypeNote))
            Columns.Add(New Column("Modify", DBTypeDate))
            Return Columns.ToArray
        End Function
        Function ToObjects() As Object()
            Return New Object() {Label, Enable, Name, Commission, Discount, Prepay, Note, Modify}
        End Function

        Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Contract
            Dim R As New MyDataRow(Row)
            Dim data As Contract
            data.Label = R("Label")
            data.Enable = R("Enable")
            data.Name = R("Name")
            data.Commission = R("Commission")
            data.Discount = R("Discount")
            data.Prepay = R("Prepay")
            data.Note = R("Note")
            data.Modify = GetDate(R("Modify"))
            Return data
        End Function

        Sub UpdateRow(ByVal r As DataRow)
            Dim columns() As Column = ToColumns()
            Dim obj() As Object = ToObjects()
            For i As Integer = 0 To columns.Count - 1
                r(columns(i).Name) = obj(i)
            Next
        End Sub

        Public Function GetUpdateSqlCommand() As String
            Dim Column As String() = New String() {"Enable", "Name", "Commission", "Discount", "Prepay", "Note", "Modify"}
            Dim Value As Object() = New Object() {Enable, Name, Commission, Discount, Prepay, Note, Modify}
            Return Access.GetUpdateSqlCommand(Table, Column, Value, "Label", Label)
        End Function

        Public Function IsNull() As Boolean
            Return Label = ""
        End Function

        Public Shared Function Null() As Contract
            Dim g As New Contract
            g.Label = ""
            Return g
        End Function

        Public Overrides Function ToString() As String
            If IsNull() Then Return ""
            Return Join(Array.ConvertAll(ToObjects(), Function(o As Object) o.ToString), ",")
        End Function
    End Structure
#End Region
#Region "庫存"
    ''' <summary>庫存</summary>
    Public Structure Stock
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
        '''' <summary>定價</summary>
        'Dim Price As Single
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
            'Columns.Add(New Column("Price", DBTypeSingle))
            Columns.Add(New Column("Number", DBTypeInteger))
            Columns.Add(New Column("Note", DBTypeNote))
            Return Columns.ToArray
        End Function

        Function ToObjects() As Object()
            Return New Object() {Label, GoodsLabel, SupplierLabel, [Date].ToString("yyyy/MM/dd HH:mm:ss"), IMEI, Cost, Number, Note}
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
            ' data.Price = R("Price")
            data.Number = R("Number")
            data.Note = R("Note")
            Return data
        End Function

        Sub UpdateRow(ByVal r As DataRow)
            Dim columns() As Column = ToColumns()
            Dim obj() As Object = ToObjects()
            For i As Integer = 0 To columns.Count - 1
                r(columns(i).Name) = obj(i)
            Next
        End Sub


        Public Function GetUpdateSqlCommand()
            Dim SQLCommand As String = "UPDATE " & Table & " SET "
            Dim label() As String = New String() {"GoodsLabel", "SupplierLabel", "Date", "IMEI", "Cost", "Number", "Note"}
            Dim value() As String = New String() {"'" & GoodsLabel & "'", "'" & SupplierLabel & "'", [Date].ToString("#yyyy/MM/dd HH:mm:ss#"), "'" & IMEI & "'", Cost, Number, "'" & Note & "'"}
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

        Shared ReadOnly Property Null() As Stock
            Get
                Dim item As New Stock
                item.Label = ""
                Return item
            End Get
        End Property

        Public Function IsNull() As Boolean
            Return Label = ""
        End Function

    End Structure
#End Region

    Public Structure StockMove
        Shared Table As String = "StockMove"
        Dim Label As String
        Dim [Date] As Date
        Dim GoodsLabel As String
        Dim StockLabel As String
        Dim SupplierLabel As String
        Dim Number As Integer
        Dim Cost As Integer
        Dim IMEI As String
        Dim SourceShop As String
        Dim DestineShop As String
        Dim SourcePersonnel As String
        Dim DestinePersonnel As String
        Dim Action As Type

        Public Enum Type
            Request = 0
            Out = 1
            Sending = 2
            Receiving = 3
            [In] = 4
            Cancel = 5
        End Enum
        Shared TypeText As String() = {"申請", "調出", "調出(未送達)", "調貨中", "入庫", "取消"}

        Shared Function ToColumns() As Column()
            Dim Columns As New ColumnList
            Columns.Add("Label", DBTypeLabel)
            Columns.Add("Date", DBTypeDate)
            Columns.Add("GoodsLabel", DBTypeLabel)
            Columns.Add("StockLabel", DBTypeLabel)
            Columns.Add("SupplierLabel", DBTypeLabel)
            Columns.Add("Number", DBTypeInteger)
            Columns.Add("Cost", DBTypeSingle)
            Columns.Add("IMEI", DBTypeIMEI)
            Columns.Add("SourceShop", DBTypeLabel)
            Columns.Add("DestineShop", DBTypeLabel)
            Columns.Add("SourcePersonnel", DBTypeLabel)
            Columns.Add("DestinePersonnel", DBTypeLabel)
            Columns.Add("Action", DBTypeInteger)
            Return Columns.ToArray
        End Function

        Function ToObjects() As Object()
            Return New Object() {Label, Me.Date, GoodsLabel, StockLabel, SupplierLabel, Number, Cost, IMEI, SourceShop, DestineShop, SourcePersonnel, DestinePersonnel, CType(Action, Int16)}
        End Function

        Public Shared Function GetFrom(ByVal Row As Data.DataRow) As StockMove
            Dim R As New MyDataRow(Row)
            Dim data As StockMove
            data.Label = R("Label")
            data.Date = R("Date")
            data.GoodsLabel = R("GoodsLabel")
            data.StockLabel = R("StockLabel")
            data.SupplierLabel = R("SupplierLabel")
            data.Number = R("Number")
            data.Cost = R("Cost")
            data.IMEI = R("IMEI")
            data.SourceShop = R("SourceShop")
            data.DestineShop = R("DestineShop")
            data.SourcePersonnel = R("SourcePersonnel")
            data.DestinePersonnel = R("DestinePersonnel")
            data.Action = CType(R("Action"), Type)
            Return data
        End Function

        Public Shared ReadOnly Property Null() As StockMove
            Get
                Dim s As New StockMove
                s.Label = ""
                Return s
            End Get

        End Property

        Public Function IsNull() As Boolean
            Return Label = ""
        End Function

        Public Function GetUpdateSqlCommand() As String
            Dim Column As String() = New String() {"Date", "GoodsLabel", "StockLabel", "SupplierLabel", "Number", "Cost", "IMEI", "SourceShop", "DestineShop", "SourcePersonnel", "DestinePersonnel", "Action"}
            Dim Value As String() = New String() {Me.Date, GoodsLabel, StockLabel, SupplierLabel, Number, Cost, IMEI, SourceShop, DestineShop, SourcePersonnel, DestinePersonnel, Action}
            Return Access.GetUpdateSqlCommand(Table, Column, Value, "Label", Label)
        End Function

        Public Shared Function GetUpdateStateSqlCommand(ByVal Label As String, ByVal newState As Type) As String
            Dim Column As String() = New String() {"Action"}
            Dim Value As String() = New String() {newState}
            Return Access.GetUpdateSqlCommand(Table, Column, Value, "Label", Label)
        End Function
    End Structure



    ''' <summary>付款方式</summary>
    Public Enum Payment
        Cash = 0
        Card = 1
        Deposit = 2
        Cancel = 3
    End Enum



    Public Structure SalesGoods
        Shared Table As String = "SalesGoods"
        ''' <summary>單號識別碼</summary>
        Dim SalesLabel As String
        ''' <summary>庫存識別碼</summary>
        Dim StockLabel As String
        ''' <summary>賣價</summary>
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

    Sub UpdateRow(ByVal r As DataRow)
        Dim columns() As Column = ToColumns()
        Dim obj() As Object = ToObjects()
        For i As Integer = 0 To columns.Count - 1
            r(columns(i).Name) = obj(i)
        Next
    End Sub

    End Structure


    Public Structure SalesContract
        Shared Table As String = "SalesContract"
        Dim SalesLabel As String
        Dim ContractLabel As String
        Dim Discount As Single
        Dim Phone As String
        Shared Function ToColumns() As Column()
            Dim Columns As New List(Of Column)
            Columns.Add(New Column("SalesLabel", DBTypeLabel))
            Columns.Add(New Column("ContractLabel", DBTypeLabel))
            Columns.Add(New Column("Discount", DBTypeSingle))
            Columns.Add(New Column("Phone", DBTypeTel))
            Return Columns.ToArray()
        End Function

        Function ToObjects() As Object()
            Return New Object() {SalesLabel, ContractLabel, Discount, Phone}
        End Function

        Public Shared Function GetFrom(ByVal Row As Data.DataRow) As SalesContract
            Dim R As New MyDataRow(Row)
            Dim data As SalesContract
            data.SalesLabel = R("SalesLabel")
            data.ContractLabel = R("ContractLabel")
            data.Discount = R("Discount")
            data.Phone = R("Phone")
            Return data
        End Function

        Sub UpdateRow(ByVal r As DataRow)
            Dim columns() As Column = ToColumns()
            Dim obj() As Object = ToObjects()
            For i As Integer = 0 To columns.Count - 1
                r(columns(i).Name) = obj(i)
            Next
        End Sub

    End Structure

    Public Structure OrderGoods
        Shared Table As String = "OrderGoods"
        Dim SalesLabel As String
        Dim GoodsLabel As String
        Dim PurchaseLabel As String
        Dim Price As Single
        Dim Number As Integer

        Shared Function ToColumns() As Column()
            Dim Columns As New List(Of Column)
            Columns.Add(New Column("SalesLabel", DBTypeLabel))
            Columns.Add(New Column("GoodsLabel", DBTypeLabel))
            Columns.Add(New Column("PurchaseLabel", DBTypeLabel))
            Columns.Add(New Column("Price", DBTypeSingle))
            Columns.Add(New Column("Number", DBTypeInteger))
            Return Columns.ToArray()
        End Function

        Function ToObjects() As Object()
            Return New Object() {SalesLabel, GoodsLabel, PurchaseLabel, Price, Number}
        End Function

        Sub UpdateRow(ByVal r As DataRow)
            Dim columns() As Column = ToColumns()
            Dim obj() As Object = ToObjects()
            For i As Integer = 0 To columns.Count - 1
                r(columns(i).Name) = obj(i)
            Next
        End Sub

        Public Shared Function GetFrom(ByVal Row As Data.DataRow) As OrderGoods
            Dim R As New MyDataRow(Row)
            Dim data As OrderGoods
            data.SalesLabel = R("SalesLabel")
            data.GoodsLabel = R("GoodsLabel")
            data.PurchaseLabel = R("PurchaseLabel")
            data.Price = R("Price")
            data.Number = R("Number")
            Return data
        End Function
    End Structure

    ''' <summary>銷貨</summary>
    Public Structure Sales
        Shared Table As String = "Sales"
        ''' <summary>單號識別碼</summary>
        Dim Label As String

        Dim OrderDate As Date
        ''' <summary>銷貨日期</summary>
        Dim SalesDate As Date
        ''' <summary>客戶識別碼</summary>
        Dim CustomerLabel As String
        ''' <summary>員工識別碼</summary>
        Dim PersonnelLabel As String
        ''' <summary>訂金-現金</summary>
        Dim DepositByCash As Single
        ''' <summary>訂金-刷卡</summary>
        Dim DepositByCard As Single
        ''' <summary>付款方式</summary>
        Dim TypeOfPayment As Payment
        ''' <summary>備註</summary>
        Dim Note As String

        Public Shared PaymentDescribe As String() = New String() {"現金", "刷卡", "訂金", "退訂"}

        Public Function GetPayment() As String
            Return PaymentDescribe(TypeOfPayment)
        End Function

        Shared Function ToColumns() As Column()
            Dim Columns As New ColumnList ' List(Of Column)
            Columns.Add("Label", DBTypeLabel)
            Columns.Add("OrderDate", DBTypeDate)
            Columns.Add("SalesDate", DBTypeDate)
            Columns.Add("CustomerLabel", DBTypeLabel)
            Columns.Add("PersonnelLabel", DBTypeLabel)
            Columns.Add("Deposit", DBTypeSingle)
            Columns.Add("TypeOfPayment", DBTypeInteger)
            Columns.Add("Note", DBTypeNote)
            Columns.Add("DepositByCard", DBTypeSingle)
            Return Columns.ToArray
        End Function

        Function ToObjects() As Object()
            Return New Object() {Label, OrderDate, SalesDate, CustomerLabel, PersonnelLabel, DepositByCash, CType(TypeOfPayment, Int16), Note, DepositByCard}
        End Function

        Sub UpdateRow(ByVal r As DataRow)
            Dim columns() As Column = ToColumns()
            Dim obj() As Object = ToObjects()
            For i As Integer = 0 To columns.Count - 1
                r(columns(i).Name) = obj(i)
            Next
        End Sub


        Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Sales
            Dim R As New MyDataRow(Row)
            Dim data As Sales
            data.Label = R("Label")
            data.OrderDate = R("OrderDate")
            Date.TryParse(R("SalesDate"), data.SalesDate)
            data.CustomerLabel = R("CustomerLabel")
            data.PersonnelLabel = R("PersonnelLabel")
            data.DepositByCash = R("Deposit")
            data.DepositByCard = R("DePositByCard")
            data.TypeOfPayment = R("TypeOfPayment")
            data.Note = R("Note")
            Return data
        End Function
    End Structure



#Region "訂單"
    '''' <summary>訂單</summary>
    'Structure Order
    '    Shared Table As String = "Order"
    '    ''' <summary>訂單識別碼</summary>
    '    Dim Label As String
    '    ''' <summary>下訂日期</summary>
    '    Dim [Date] As Date
    '    ''' <summary>商品識別碼</summary>
    '    Dim GoodsLabel As String
    '    ''' <summary>最終售價</summary>
    '    Dim SellingPrice As Single
    '    ''' <summary>訂金</summary>
    '    Dim Deposit As Single
    '    ''' <summary>客戶識別碼</summary>
    '    Dim CustomerLabel As String
    '    ''' <summary>員工識別碼</summary>
    '    Dim PersonnelLabel As String
    '    ''' <summary>備註</summary>
    '    Dim Note As String

    '    Shared Function ToColumns() As Column()
    '        Dim Columns As New List(Of Column)
    '        Columns.Add(New Column("Label", DBTypeLabel))
    '        Columns.Add(New Column("Date", DBTypeDate))
    '        Columns.Add(New Column("GoodsLabel", DBTypeLabel))
    '        Columns.Add(New Column("SellingPrice", DBTypeSingle))
    '        Columns.Add(New Column("Deposit", DBTypeSingle))
    '        Columns.Add(New Column("CustomerLabel", DBTypeLabel))
    '        Columns.Add(New Column("PersonnelLabel", DBTypeLabel))
    '        Columns.Add(New Column("Note", DBTypeNote))
    '        Return Columns.ToArray
    '    End Function

    '    Function ToObjects() As Object()
    '        Return New Object() {Label, [Date], GoodsLabel, SellingPrice, Deposit, CustomerLabel, PersonnelLabel, Note}
    '    End Function

    '    Public Shared Function GetFrom(ByVal Row As Data.DataRow) As Order
    '        Dim R As New MyDataRow(Row)
    '        Dim data As Order
    '        data.Label = R("Label")
    '        data.Date = R("Date")
    '        data.GoodsLabel = R("GoodsLabel")
    '        data.SellingPrice = R("SellingPrice")
    '        data.Deposit = R("Deposit")
    '        data.CustomerLabel = R("CustomerLabel")
    '        data.PersonnelLabel = R("PersonnelLabel")
    '        data.Note = R("Note")
    '        Return data
    '    End Function
    'End Structure
#End Region
    'End Module
End Namespace