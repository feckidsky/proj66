Namespace Database
    Public Class AccessServer
        Inherits TCPTool
        Public WithEvents Access As Access
        Public Port As Integer = 3600

        Public Name As String

        Public Sub Open()
            ServerOpen(Port)
        End Sub

        Public Sub Close()
            ServerClose()
        End Sub

        Public Sub ChangeName(ByVal newName As String)
            Name = newName
            ServerSend("ServerName", Code.SerializeWithZIP(Name))
        End Sub

        Private Sub Server_ReceiveConnected(ByVal Sender As TCPTool, ByVal Client As TCPTool.Client) Handles MyBase.ReceiveConnected
            Client.Send("ServerName", Code.SerializeWithZIP(Name))
        End Sub


        Private Sub Server_ServerReceiveSplitMessage(ByVal Client As TCPTool.Client, ByVal IP As String, ByVal Port As Integer, ByVal Data() As String) Handles MyBase.ServerReceiveSplitMessage
            Select Case Data(0)
                Case "ReaderRequest"
                    Dim guid As String = Data(1)
                    Dim Cmd As String = Data(2)
                    Dim argsSerialize As String = Data(3)
                    ReaderRequest(Client, guid, Cmd, argsSerialize)
                Case "CreatePersonnel" : Access.AddPersonnel(Code.DeserializeWithUnzip(Of Personnel)(Data(1)))
                Case "DeletePersonnel" : Access.DeletePersonnel(Code.DeserializeWithUnzip(Of Personnel)(Data(1)))
                Case "ChangePersonnel" : Access.ChangePersonnel(Code.DeserializeWithUnzip(Of Personnel)(Data(1)))
                Case "CreateSupplier" : Access.AddSupplier(Code.DeserializeWithUnzip(Of Supplier)(Data(1)))
                Case "DeleteSupplier" : Access.DeleteSupplier(Code.DeserializeWithUnzip(Of Supplier)(Data(1)))
                Case "ChangeSupplier" : Access.ChangeSupplier(Code.DeserializeWithUnzip(Of Supplier)(Data(1)))
                Case "CreateCustomer" : Access.AddCustomer(Code.DeserializeWithUnzip(Of Customer)(Data(1)))
                Case "DeleteCustomer" : Access.DeleteCustomer(Code.DeserializeWithUnzip(Of Customer)(Data(1)))
                Case "ChangeCustomer" : Access.ChangeCustomer(Code.DeserializeWithUnzip(Of Customer)(Data(1)))
                Case "CreateContract" : Access.AddContract(Code.DeserializeWithUnzip(Of Contract)(Data(1)))
                Case "DeleteContract" : Access.DeleteContract(Code.DeserializeWithUnzip(Of Contract)(Data(1)))
                Case "ChangeContract" : Access.ChangeContract(Code.DeserializeWithUnzip(Of Contract)(Data(1)))
                Case "CreateGoods" : Access.AddGoods(Code.DeserializeWithUnzip(Of Goods)(Data(1)))
                Case "DeleteGoods" : Access.DeleteGoods(Code.DeserializeWithUnzip(Of Goods)(Data(1)))
                Case "ChangeGoods" : Access.ChangeGoods(Code.DeserializeWithUnzip(Of Goods)(Data(1)))
                Case "CreateStock" : Access.AddStock(Code.DeserializeWithUnzip(Of Stock)(Data(1)))
                Case "DeleteStock" : Access.DeleteStock(Code.DeserializeWithUnzip(Of Stock)(Data(1)))
                Case "ChangeStock" : Access.ChangeStock(Code.DeserializeWithUnzip(Of Stock)(Data(1)))
                Case "CreateHistoryPrice" : Access.AddHistoryPrice(Code.DeserializeWithUnzip(Of HistoryPrice)(Data(1)))
                Case "DeleteHistoryPrice" : Access.DeleteHistoryPrice(Code.DeserializeWithUnzip(Of HistoryPrice)(Data(1)))
                Case "ChangeHistoryPrice" : Access.ChangeHistoryPrice(Code.DeserializeWithUnzip(Of HistoryPrice)(Data(1)))
                Case "DeleteHistoryPriceList" : Access.DeleteHistoryPriceList(Code.DeserializeWithUnzip(Of String)(Data(1)))
                Case "CreateSales" : Access.CreateSales(Code.DeserializeWithUnzip(Of SalesArgs)(Data(1)))
                Case "ChangeSales" : Access.ChangeSales(Code.DeserializeWithUnzip(Of SalesArgs)(Data(1)))
                Case "DeleteSales" : Access.DeleteSales(Code.DeserializeWithUnzip(Of Sales)(Data(1)))
                Case "CreateStockMove" : Access.AddStockMove(Code.DeserializeWithUnzip(Of StockMove)(Data(1)))
                Case "ChangeStockMove" : Access.ChangeStockMove(Code.DeserializeWithUnzip(Of StockMove)(Data(1)))
                Case "DeleteStockMove" : Access.DeleteStockMove(Code.DeserializeWithUnzip(Of StockMove)(Data(1)))
                Case "CreateLog" : Access.AddLog(Code.DeserializeWithUnzip(Of Log)(Data(1)))
                Case "DeleteLog" : Access.DeleteLog(Code.DeserializeWithUnzip(Of Log)(Data(1)))
                Case "DeleteAllLog" : Access.DeleteAllLog()
                Case "DeleteFile" : Access.DeleteFile(Code.DeserializeWithUnzip(Of String)(Data(1)))
                Case Else
                    Dim msg As String = "不支援的指令:" & Data(0)
                    MsgBox("Server" & msg)
                    Client.Send("MsgBox", Code.SerializeWithZIP(msg))
            End Select
        End Sub

        Private Sub ReaderRequest(ByVal client As TCPTool.Client, ByVal Guid As String, ByVal Cmd As String, ByVal ArgsSerialize As String)

            Dim ResultText As String = ""
            Select Case Cmd
                '讀取資料表
                Case "DataTable"
                    Dim args As ReadArgs = Code.DeserializeWithUnzip(Of ReadArgs)(ArgsSerialize)
                    Dim lstFile As String() = Array.ConvertAll(args.FileList, Function(f As String) Access.Dir & "\" & IO.Path.GetFileName(f))
                    Dim dt As DataTable = Access.Read(args.Table, lstFile, args.SqlCommand)
                    ResultText = Code.SerializeWithZIP(dt)
                    dt.Dispose()
                    'client.Send("ReadResponse", Guid & "," & Code.SerializeWithZIP(dt))
                Case "GetErrorLogFiles"
                    ResultText = Code.SerializeWithZIP(Access.GetErrorLogFileNames())
                Case "GetDir"
                    ResultText = Code.SerializeWithZIP(Access.GetCloneBasePath())
            End Select


            client.Send("ReaderResponse", Guid & "," & ResultText)
        End Sub

        Private Sub Access_ChangedContract(ByVal sender As Object, ByVal con As Contract) Handles Access.ChangedContract
            ServerSend("ChangedContract", Code.SerializeWithZIP(con))
        End Sub

        Private Sub Access_ChangedCustomer(ByVal sender As Object, ByVal cus As Customer) Handles Access.ChangedCustomer
            ServerSend("ChangedCustomer", Code.SerializeWithZIP(cus))
        End Sub

        Private Sub Access_ChangedGoods(ByVal sender As Object, ByVal goods As Goods) Handles Access.ChangedGoods
            ServerSend("ChangedGoods", Code.SerializeWithZIP(goods))
        End Sub

        Private Sub Access_ChangedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice) Handles Access.ChangedHistoryPrice
            ServerSend("ChangedHistoryPrice", Code.SerializeWithZIP(hp))
        End Sub

        Private Sub Access_ChangedPersonnel(ByVal sender As Object, ByVal per As Personnel) Handles Access.ChangedPersonnel
            ServerSend("ChangedPersonnel", Code.SerializeWithZIP(per))
        End Sub

        Private Sub Access_ChangedSales(ByVal sender As Object, ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal SalesContracts() As SalesContract) Handles Access.ChangedSales
            ServerSend("ChangedSales", Code.SerializeWithZIP(New SalesArgs(sales, GoodsList, OrderList, SalesContracts)))
        End Sub

        Private Sub Access_ChangedStock(ByVal sender As Object, ByVal stock As Stock) Handles Access.ChangedStock
            ServerSend("ChangedStock", Code.SerializeWithZIP(stock))
        End Sub

        Private Sub Access_ChangedStockMove(ByVal sender As Object, ByVal data As StockMove) Handles Access.ChangedStockMove
            ServerSend("ChangedStockMove", Code.SerializeWithZIP(data))
        End Sub

        Private Sub Access_ChangedSupplier(ByVal sender As Object, ByVal sup As Supplier) Handles Access.ChangedSupplier
            ServerSend("ChangedSupplier", Code.SerializeWithZIP(sup))
        End Sub
        Private Sub Access_CreatedContract(ByVal sender As Object, ByVal con As Contract) Handles Access.CreatedContract
            ServerSend("CreatedContract", Code.SerializeWithZIP(con))
        End Sub

        Private Sub Access_CreatedCustomer(ByVal sender As Object, ByVal cus As Customer) Handles Access.CreatedCustomer
            ServerSend("CreatedCustomer", Code.SerializeWithZIP(cus))
        End Sub

        Private Sub Access_CreatedGoods(ByVal sender As Object, ByVal goods As Goods) Handles Access.CreatedGoods
            ServerSend("CreatedGoods", Code.SerializeWithZIP(goods))
        End Sub

        Private Sub Access_CreatedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice) Handles Access.CreatedHistoryPrice
            ServerSend("CreatedHistoryPrice", Code.SerializeWithZIP(hp))
        End Sub

        Private Sub Access_CreatedLog(ByVal sender As Object, ByVal log As Log) Handles Access.CreatedLog
            ServerSend("CreatedLog", Code.SerializeWithZIP(log))
        End Sub

        Private Sub Access_CreatedPersonnel(ByVal sender As Object, ByVal per As Personnel) Handles Access.CreatedPersonnel
            ServerSend("CreatedPersonnel", Code.SerializeWithZIP(per))
        End Sub

        Private Sub Access_CreatedSales(ByVal sender As Object, ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal SalesContracts() As SalesContract) Handles Access.CreatedSales
            ServerSend("CreatedSales", Code.SerializeWithZIP(New SalesArgs(sales, GoodsList, OrderList, SalesContracts)))
        End Sub

        Private Sub Access_CreatedStock(ByVal sender As Object, ByVal stock As Stock) Handles Access.CreatedStock
            ServerSend("CreatedStock", Code.SerializeWithZIP(stock))
        End Sub

        Private Sub Access_CreatedStockMove(ByVal sender As Object, ByVal data As StockMove) Handles Access.CreatedStockMove
            ServerSend("CreatedStockMove", Code.SerializeWithZIP(data))
        End Sub

        Private Sub Access_CreatedSupplier(ByVal sender As Object, ByVal sup As Supplier) Handles Access.CreatedSupplier
            ServerSend("CreatedSupplier", Code.SerializeWithZIP(sup))
        End Sub

        Private Sub Access_DeletedAllLog(ByVal sender As Object) Handles Access.DeletedAllLog
            ServerSend("DeletedAllLog")
        End Sub
        Private Sub Access_DeletedContract(ByVal sender As Object, ByVal con As Contract) Handles Access.DeletedContract
            Server.ServerSend("DeletedContract", Code.SerializeWithZIP(con))
        End Sub

        Private Sub Access_DeletedCustomer(ByVal sender As Object, ByVal cus As Customer) Handles Access.DeletedCustomer
            ServerSend("DeletedCustomer", Code.SerializeWithZIP(cus))
        End Sub

        Private Sub Access_DeletedGoods(ByVal sender As Object, ByVal goods As Goods) Handles Access.DeletedGoods
            ServerSend("DeletedGoods", Code.SerializeWithZIP(goods))
        End Sub

        Private Sub Access_DeletedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice) Handles Access.DeletedHistoryPrice
            ServerSend("DeletedHistoryPrice", Code.SerializeWithZIP(hp))
        End Sub

        Private Sub Access_DeletedHistoryPriceList(ByVal sender As Object, ByVal hp As HistoryPrice) Handles Access.DeletedHistoryPriceList
            ServerSend("DeletedHistoryPriceList", Code.SerializeWithZIP(hp))
        End Sub

        Private Sub Access_DeletedLog(ByVal sender As Object, ByVal log As Log) Handles Access.DeletedLog
            ServerSend("DeletedLog", Code.SerializeWithZIP(log))
        End Sub

        Private Sub Access_DeletedPersonnel(ByVal sender As Object, ByVal per As Personnel) Handles Access.DeletedPersonnel
            ServerSend("DeletedPersonnel", Code.SerializeWithZIP(per))
        End Sub

        Private Sub Access_DeletedSales(ByVal sender As Object, ByVal sales As Sales) Handles Access.DeletedSales
            ServerSend("DeletedSales", Code.SerializeWithZIP(sales))
        End Sub

        Private Sub Access_DeletedStock(ByVal sender As Object, ByVal stock As Stock) Handles Access.DeletedStock
            ServerSend("DeletedStock", Code.SerializeWithZIP(stock))
        End Sub

        Private Sub Access_DeletedStockMove(ByVal sender As Object, ByVal data As StockMove) Handles Access.DeletedStockMove
            ServerSend("DeletedStockMove", Code.SerializeWithZIP(data))
        End Sub

        Private Sub Access_DeletedSupplier(ByVal sender As Object, ByVal sup As Supplier) Handles Access.DeletedSupplier
            ServerSend("DeletedSupplier", Code.SerializeWithZIP(sup))
        End Sub
    End Class

    Public Structure ClientInfo
        Dim IP As String
        Dim Port As Integer
        Dim Name As String
        Sub New(ByVal Name As String, ByVal IP As String, ByVal Port As Integer)
            Me.Name = Name : Me.IP = IP : Me.Port = Port
        End Sub
    End Structure
End Namespace

