Namespace Database
    Public Class AccessServer
        Inherits TCPTool
        Public WithEvents Access As Access
        Public Port As Integer = 3600

        Public Name As String

        Public Sub Open(ByVal Index As Integer)
            ServerOpen(Port, Index)
        End Sub

        Public Sub Close()
            ServerClose()
        End Sub

        Public Sub ChangeName(ByVal newName As String)
            Name = newName
            ServerSend("ServerName", Code.XmlSerializeWithZIP(Name))
        End Sub

        Private Sub Server_ReceiveConnected(ByVal Sender As TCPTool, ByVal Client As TCPTool.Client) Handles MyBase.ReceiveConnected
            Client.Send("ServerName", Code.XmlSerializeWithZIP(Name))
        End Sub


        Private Sub Server_ServerReceiveSplitMessage(ByVal Client As TCPTool.Client, ByVal IP As String, ByVal Port As Integer, ByVal Data() As String) Handles MyBase.ServerReceiveSplitMessage
            Select Case Data(0)
                Case "ReaderRequest"
                    Dim guid As String = Data(1)
                    Dim Cmd As String = Data(2)
                    Dim argsSerialize As String = Data(3)
                    ReaderRequest(Client, guid, Cmd, argsSerialize)
                Case "CreatePersonnel" : Access.AddPersonnel(Code.XmlDeserializeWithUnzip(Of Personnel)(Data(1)))
                Case "DeletePersonnel" : Access.DeletePersonnel(Code.XmlDeserializeWithUnzip(Of Personnel)(Data(1)))
                Case "ChangePersonnel" : Access.ChangePersonnel(Code.XmlDeserializeWithUnzip(Of Personnel)(Data(1)))
                Case "CreateSupplier" : Access.AddSupplier(Code.XmlDeserializeWithUnzip(Of Supplier)(Data(1)))
                Case "DeleteSupplier" : Access.DeleteSupplier(Code.XmlDeserializeWithUnzip(Of Supplier)(Data(1)))
                Case "ChangeSupplier" : Access.ChangeSupplier(Code.XmlDeserializeWithUnzip(Of Supplier)(Data(1)))
                Case "CreateCustomer" : Access.AddCustomer(Code.XmlDeserializeWithUnzip(Of Customer)(Data(1)))
                Case "DeleteCustomer" : Access.DeleteCustomer(Code.XmlDeserializeWithUnzip(Of Customer)(Data(1)))
                Case "ChangeCustomer" : Access.ChangeCustomer(Code.XmlDeserializeWithUnzip(Of Customer)(Data(1)))
                Case "CreateContract" : Access.AddContract(Code.XmlDeserializeWithUnzip(Of Contract)(Data(1)))
                Case "DeleteContract" : Access.DeleteContract(Code.XmlDeserializeWithUnzip(Of Contract)(Data(1)))
                Case "ChangeContract" : Access.ChangeContract(Code.XmlDeserializeWithUnzip(Of Contract)(Data(1)))
                Case "CreateGoods" : Access.AddGoods(Code.XmlDeserializeWithUnzip(Of Goods)(Data(1)))
                Case "DeleteGoods" : Access.DeleteGoods(Code.XmlDeserializeWithUnzip(Of Goods)(Data(1)))
                Case "ChangeGoods" : Access.ChangeGoods(Code.XmlDeserializeWithUnzip(Of Goods)(Data(1)))
                Case "CreateStock" : Access.AddStock(Code.XmlDeserializeWithUnzip(Of Stock)(Data(1)))
                Case "DeleteStock" : Access.DeleteStock(Code.XmlDeserializeWithUnzip(Of Stock)(Data(1)))
                Case "ChangeStock" : Access.ChangeStock(Code.XmlDeserializeWithUnzip(Of Stock)(Data(1)))
                Case "CreateHistoryPrice" : Access.AddHistoryPrice(Code.XmlDeserializeWithUnzip(Of HistoryPrice)(Data(1)))
                Case "DeleteHistoryPrice" : Access.DeleteHistoryPrice(Code.XmlDeserializeWithUnzip(Of HistoryPrice)(Data(1)))
                Case "ChangeHistoryPrice" : Access.ChangeHistoryPrice(Code.XmlDeserializeWithUnzip(Of HistoryPrice)(Data(1)))
                Case "DeleteHistoryPriceList" : Access.DeleteHistoryPriceList(Code.XmlDeserializeWithUnzip(Of String)(Data(1)))
                Case "CreateSales" : Access.CreateSales(Code.XmlDeserializeWithUnzip(Of SalesArgs)(Data(1)))
                Case "ChangeSales" : Access.ChangeSales(Code.XmlDeserializeWithUnzip(Of SalesArgs)(Data(1)))
                Case "DeleteSales" : Access.DeleteSales(Code.XmlDeserializeWithUnzip(Of Sales)(Data(1)))
                Case "CreateStockMove" : Access.AddStockMove(Code.XmlDeserializeWithUnzip(Of StockMove)(Data(1)))
                Case "ChangeStockMove" : Access.ChangeStockMove(Code.XmlDeserializeWithUnzip(Of StockMove)(Data(1)))
                Case "DeleteStockMove" : Access.DeleteStockMove(Code.XmlDeserializeWithUnzip(Of StockMove)(Data(1)))
                Case "CreateLog" : Access.AddLog(Code.XmlDeserializeWithUnzip(Of Log)(Data(1)))
                Case "DeleteLog" : Access.DeleteLog(Code.XmlDeserializeWithUnzip(Of Log)(Data(1)))
                Case "DeleteAllLog" : Access.DeleteAllLog()
                Case "DeleteFile" : Access.DeleteFile(Code.XmlDeserializeWithUnzip(Of String)(Data(1)))
                Case "GetSalesInformation"
                    Dim args As Access.GetSalesInformationArgs = Code.XmlDeserializeWithUnzip(Of Access.GetSalesInformationArgs)(Data(1))
                    Client.Send("ResponseSalesInformationArgs", Code.XmlSerializeWithZIP(Access.GetSalesInformation(args.StartTime, args.EndTime)))
                Case Else
                    Dim msg As String = "不支援的指令:" & Data(0)
                    'MsgBox("Server" & msg)
                    Client.Send("MsgBox", Code.XmlSerializeWithZIP(msg))
            End Select
        End Sub

        Private Sub AccessServer_ServerReceiveStreamRequest(ByVal Client As TCPTool.Client, ByVal sender As TCPTool.Client.StreamSender) Handles Me.ServerReceiveStreamRequest
            Select Case sender.Cmd
                Case "DataTable"
                    Dim args As ReadArgs = Code.XmlDeserializeWithUnzip(Of ReadArgs)(sender.Args)
                    Dim lstFile As String() = Array.ConvertAll(args.FileList, Function(f As String) Access.Dir & "\" & IO.Path.GetFileName(f))
                    Dim dt As DataTable = Access.Read(args.Table, lstFile, args.SqlCommand, Nothing)
                    Dim sm As New IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(Code.XmlSerializeWithZIP(dt)))
                    sender.stream = sm
                    dt.Dispose()
                Case "GetErrorLogFiles"
                    sender.stream = New IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(Code.XmlSerializeWithZIP(Access.GetErrorLogFileNames())))
                Case "GetDir"
                    sender.stream = New IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(Code.XmlSerializeWithZIP(Access.GetCloneBasePath())))
                Case "GetSalesInformation"
                    Dim args As Access.GetSalesInformationArgs = Code.XmlDeserializeWithUnzip(Of Access.GetSalesInformationArgs)(sender.Args)
                    sender.stream = New IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(Code.XmlSerializeWithZIP(Access.GetSalesInformation(args.StartTime, args.EndTime))))
            End Select
        End Sub

        Private Sub ReaderRequest(ByVal client As TCPTool.Client, ByVal Guid As String, ByVal Cmd As String, ByVal ArgsSerialize As String)

            Dim ResultText As String = ""
            Select Case Cmd
                '讀取資料表
                Case "DataTable"
                    Dim args As ReadArgs = Code.XmlDeserializeWithUnzip(Of ReadArgs)(ArgsSerialize)
                    Dim lstFile As String() = Array.ConvertAll(args.FileList, Function(f As String) Access.Dir & "\" & IO.Path.GetFileName(f))
                    Dim dt As DataTable = Access.Read(args.Table, lstFile, args.SqlCommand, Nothing)
                    ResultText = Code.XmlSerializeWithZIP(dt)
                    dt.Dispose()
                    'client.Send("ReadResponse", Guid & "," & Code.SerializeWithZIP(dt))
                Case "GetErrorLogFiles"
                    ResultText = Code.XmlSerializeWithZIP(Access.GetErrorLogFileNames())
                Case "GetDir"
                    ResultText = Code.XmlSerializeWithZIP(Access.GetCloneBasePath())
            End Select


            client.Send("ReaderResponse", Guid & "," & ResultText)
        End Sub

        Private Sub Access_ChangedContract(ByVal sender As Object, ByVal con As Contract) Handles Access.ChangedContract
            ServerSend("ChangedContract", Code.XmlSerializeWithZIP(con))
        End Sub

        Private Sub Access_ChangedCustomer(ByVal sender As Object, ByVal cus As Customer) Handles Access.ChangedCustomer
            ServerSend("ChangedCustomer", Code.XmlSerializeWithZIP(cus))
        End Sub

        Private Sub Access_ChangedGoods(ByVal sender As Object, ByVal goods As Goods) Handles Access.ChangedGoods
            ServerSend("ChangedGoods", Code.XmlSerializeWithZIP(goods))
        End Sub

        Private Sub Access_ChangedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice) Handles Access.ChangedHistoryPrice
            ServerSend("ChangedHistoryPrice", Code.XmlSerializeWithZIP(hp))
        End Sub

        Private Sub Access_ChangedPersonnel(ByVal sender As Object, ByVal per As Personnel) Handles Access.ChangedPersonnel
            ServerSend("ChangedPersonnel", Code.XmlSerializeWithZIP(per))
        End Sub

        Private Sub Access_ChangedSales(ByVal sender As Object, ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal SalesContracts() As SalesContract) Handles Access.ChangedSales
            ServerSend("ChangedSales", Code.XmlSerializeWithZIP(New SalesArgs(sales, GoodsList, OrderList, SalesContracts)))
        End Sub

        Private Sub Access_ChangedStock(ByVal sender As Object, ByVal stock As Stock) Handles Access.ChangedStock
            ServerSend("ChangedStock", Code.XmlSerializeWithZIP(stock))
        End Sub

        Private Sub Access_ChangedStockMove(ByVal sender As Object, ByVal data As StockMove) Handles Access.ChangedStockMove
            ServerSend("ChangedStockMove", Code.XmlSerializeWithZIP(data))
        End Sub

        Private Sub Access_ChangedSupplier(ByVal sender As Object, ByVal sup As Supplier) Handles Access.ChangedSupplier
            ServerSend("ChangedSupplier", Code.XmlSerializeWithZIP(sup))
        End Sub
        Private Sub Access_CreatedContract(ByVal sender As Object, ByVal con As Contract) Handles Access.CreatedContract
            ServerSend("CreatedContract", Code.XmlSerializeWithZIP(con))
        End Sub

        Private Sub Access_CreatedCustomer(ByVal sender As Object, ByVal cus As Customer) Handles Access.CreatedCustomer
            ServerSend("CreatedCustomer", Code.XmlSerializeWithZIP(cus))
        End Sub

        Private Sub Access_CreatedGoods(ByVal sender As Object, ByVal goods As Goods) Handles Access.CreatedGoods
            ServerSend("CreatedGoods", Code.XmlSerializeWithZIP(goods))
        End Sub

        Private Sub Access_CreatedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice) Handles Access.CreatedHistoryPrice
            ServerSend("CreatedHistoryPrice", Code.XmlSerializeWithZIP(hp))
        End Sub

        Private Sub Access_CreatedLog(ByVal sender As Object, ByVal log As Log) Handles Access.CreatedLog
            ServerSend("CreatedLog", Code.XmlSerializeWithZIP(log))
        End Sub

        Private Sub Access_CreatedPersonnel(ByVal sender As Object, ByVal per As Personnel) Handles Access.CreatedPersonnel
            ServerSend("CreatedPersonnel", Code.XmlSerializeWithZIP(per))
        End Sub

        Private Sub Access_CreatedSales(ByVal sender As Object, ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal SalesContracts() As SalesContract) Handles Access.CreatedSales
            ServerSend("CreatedSales", Code.XmlSerializeWithZIP(New SalesArgs(sales, GoodsList, OrderList, SalesContracts)))
        End Sub

        Private Sub Access_CreatedStock(ByVal sender As Object, ByVal stock As Stock) Handles Access.CreatedStock
            ServerSend("CreatedStock", Code.XmlSerializeWithZIP(stock))
        End Sub

        Private Sub Access_CreatedStockMove(ByVal sender As Object, ByVal data As StockMove) Handles Access.CreatedStockMove
            ServerSend("CreatedStockMove", Code.XmlSerializeWithZIP(data))
        End Sub

        Private Sub Access_CreatedSupplier(ByVal sender As Object, ByVal sup As Supplier) Handles Access.CreatedSupplier
            ServerSend("CreatedSupplier", Code.XmlSerializeWithZIP(sup))
        End Sub

        Private Sub Access_DeletedAllLog(ByVal sender As Object) Handles Access.DeletedAllLog
            ServerSend("DeletedAllLog")
        End Sub
        Private Sub Access_DeletedContract(ByVal sender As Object, ByVal con As Contract) Handles Access.DeletedContract
            Server.ServerSend("DeletedContract", Code.XmlSerializeWithZIP(con))
        End Sub

        Private Sub Access_DeletedCustomer(ByVal sender As Object, ByVal cus As Customer) Handles Access.DeletedCustomer
            ServerSend("DeletedCustomer", Code.XmlSerializeWithZIP(cus))
        End Sub

        Private Sub Access_DeletedGoods(ByVal sender As Object, ByVal goods As Goods) Handles Access.DeletedGoods
            ServerSend("DeletedGoods", Code.XmlSerializeWithZIP(goods))
        End Sub

        Private Sub Access_DeletedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice) Handles Access.DeletedHistoryPrice
            ServerSend("DeletedHistoryPrice", Code.XmlSerializeWithZIP(hp))
        End Sub

        Private Sub Access_DeletedHistoryPriceList(ByVal sender As Object, ByVal hp As HistoryPrice) Handles Access.DeletedHistoryPriceList
            ServerSend("DeletedHistoryPriceList", Code.XmlSerializeWithZIP(hp))
        End Sub

        Private Sub Access_DeletedLog(ByVal sender As Object, ByVal log As Log) Handles Access.DeletedLog
            ServerSend("DeletedLog", Code.XmlSerializeWithZIP(log))
        End Sub

        Private Sub Access_DeletedPersonnel(ByVal sender As Object, ByVal per As Personnel) Handles Access.DeletedPersonnel
            ServerSend("DeletedPersonnel", Code.XmlSerializeWithZIP(per))
        End Sub

        Private Sub Access_DeletedSales(ByVal sender As Object, ByVal sales As Sales) Handles Access.DeletedSales
            ServerSend("DeletedSales", Code.XmlSerializeWithZIP(sales))
        End Sub

        Private Sub Access_DeletedStock(ByVal sender As Object, ByVal stock As Stock) Handles Access.DeletedStock
            ServerSend("DeletedStock", Code.XmlSerializeWithZIP(stock))
        End Sub

        Private Sub Access_DeletedStockMove(ByVal sender As Object, ByVal data As StockMove) Handles Access.DeletedStockMove
            ServerSend("DeletedStockMove", Code.XmlSerializeWithZIP(data))
        End Sub

        Private Sub Access_DeletedSupplier(ByVal sender As Object, ByVal sup As Supplier) Handles Access.DeletedSupplier
            ServerSend("DeletedSupplier", Code.XmlSerializeWithZIP(sup))
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

