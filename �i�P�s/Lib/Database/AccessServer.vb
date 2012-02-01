Namespace Database
    Public Class AccessServer
        Public WithEvents Server As New TCPTool
        Public WithEvents Access As Access
        Public Port As Integer = 3600

        Public Sub Open()
            Server.ServerOpen(Port)
        End Sub
        Public Sub Close()
            Server.ServerClose()
        End Sub

        Private Sub Server_ServerReceiveSplitMessage(ByVal Client As TCPTool.Client, ByVal IP As String, ByVal Port As Integer, ByVal Data() As String) Handles Server.ServerReceiveSplitMessage
            Select Case Data(0)
                Case "ReadArgs"
                    Dim args As ReadArgs = Code.DeserializeWithUnzip(Of ReadArgs)(Data(1))
                    Dim lstFile As String() = Array.ConvertAll(args.FileList, Function(f As String) Access.Dir & "\" & IO.Path.GetFileName(f))
                    Dim dt As DataTable = Access.Read(args.Table, lstFile, args.SqlCommand)
                    Client.Send("ReadResponse", Code.SerializeWithZIP(dt))
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

                Case Else
                    Dim msg As String = "不支援的指令:" & Data(0)
                    MsgBox("Server" & msg)
                    Client.Send("MsgBox", Code.SerializeWithZIP(msg))
            End Select
        End Sub

        Private Sub Access_ChangedContract(ByVal sender As Object, ByVal con As StructureBase.Contract) Handles Access.ChangedContract
            Server.ServerSend("ChangedContract", Code.SerializeWithZIP(con))
        End Sub

        Private Sub Access_ChangedCustomer(ByVal sender As Object, ByVal cus As StructureBase.Customer) Handles Access.ChangedCustomer
            Server.ServerSend("ChangedCustomer", Code.SerializeWithZIP(cus))
        End Sub

        Private Sub Access_ChangedGoods(ByVal sender As Object, ByVal goods As StructureBase.Goods) Handles Access.ChangedGoods
            Server.ServerSend("ChangedGoods", Code.SerializeWithZIP(goods))
        End Sub

        Private Sub Access_ChangedHistoryPrice(ByVal sender As Object, ByVal hp As StructureBase.HistoryPrice) Handles Access.ChangedHistoryPrice
            Server.ServerSend("ChangedHistoryPrice", Code.SerializeWithZIP(hp))
        End Sub

        Private Sub Access_ChangedPersonnel(ByVal sender As Object, ByVal per As StructureBase.Personnel) Handles Access.ChangedPersonnel
            Server.ServerSend("ChangedPersonnel", Code.SerializeWithZIP(per))
        End Sub

        Private Sub Access_ChangedSales(ByVal sender As Object, ByVal sales As StructureBase.Sales, ByVal GoodsList() As StructureBase.SalesGoods, ByVal OrderList() As StructureBase.OrderGoods, ByVal SalesContracts() As StructureBase.SalesContract) Handles Access.ChangedSales
            Server.ServerSend("ChangedSales", Code.SerializeWithZIP(New SalesArgs(sales, GoodsList, OrderList, SalesContracts)))
        End Sub

        Private Sub Access_ChangedStock(ByVal sender As Object, ByVal stock As StructureBase.Stock) Handles Access.ChangedStock
            Server.ServerSend("ChangedStock", Code.SerializeWithZIP(stock))
        End Sub

        Private Sub Access_ChangedSupplier(ByVal sender As Object, ByVal sup As StructureBase.Supplier) Handles Access.ChangedSupplier
            Server.ServerSend("ChangedSupplier", Code.SerializeWithZIP(sup))
        End Sub
        Private Sub Access_CreatedContract(ByVal sender As Object, ByVal con As StructureBase.Contract) Handles Access.CreatedContract
            Server.ServerSend("CreatedContract", Code.SerializeWithZIP(con))
        End Sub

        Private Sub Access_CreatedCustomer(ByVal sender As Object, ByVal cus As StructureBase.Customer) Handles Access.CreatedCustomer
            Server.ServerSend("CreatedCustomer", Code.SerializeWithZIP(cus))
        End Sub

        Private Sub Access_CreatedGoods(ByVal sender As Object, ByVal goods As StructureBase.Goods) Handles Access.CreatedGoods
            Server.ServerSend("CreatedGoods", Code.SerializeWithZIP(goods))
        End Sub

        Private Sub Access_CreatedHistoryPrice(ByVal sender As Object, ByVal hp As StructureBase.HistoryPrice) Handles Access.CreatedHistoryPrice
            Server.ServerSend("CreatedHistoryPrice", Code.SerializeWithZIP(hp))
        End Sub

        Private Sub Access_CreatedPersonnel(ByVal sender As Object, ByVal per As StructureBase.Personnel) Handles Access.CreatedPersonnel
            Server.ServerSend("CreatedPersonnel", Code.SerializeWithZIP(per))
        End Sub

        Private Sub Access_CreatedSales(ByVal sender As Object, ByVal sales As StructureBase.Sales, ByVal GoodsList() As StructureBase.SalesGoods, ByVal OrderList() As StructureBase.OrderGoods, ByVal SalesContracts() As StructureBase.SalesContract) Handles Access.CreatedSales
            Server.ServerSend("CreatedSales", Code.SerializeWithZIP(New SalesArgs(sales, GoodsList, OrderList, SalesContracts)))
        End Sub

        Private Sub Access_CreatedStock(ByVal sender As Object, ByVal stock As StructureBase.Stock) Handles Access.CreatedStock
            Server.ServerSend("CreatedStock", Code.SerializeWithZIP(stock))
        End Sub

        Private Sub Access_CreatedSupplier(ByVal sender As Object, ByVal sup As StructureBase.Supplier) Handles Access.CreatedSupplier
            Server.ServerSend("CreatedSupplier", Code.SerializeWithZIP(sup))
        End Sub
        Private Sub Access_DeletedContract(ByVal sender As Object, ByVal con As StructureBase.Contract) Handles Access.DeletedContract
            Server.ServerSend("DeletedContract", Code.SerializeWithZIP(con))
        End Sub


        Private Sub Access_DeletedCustomer(ByVal sender As Object, ByVal cus As StructureBase.Customer) Handles Access.DeletedCustomer
            Server.ServerSend("DeletedCustomer", Code.SerializeWithZIP(cus))
        End Sub

        Private Sub Access_DeletedGoods(ByVal sender As Object, ByVal goods As StructureBase.Goods) Handles Access.DeletedGoods
            Server.ServerSend("DeletedGoods", Code.SerializeWithZIP(goods))
        End Sub

        Private Sub Access_DeletedHistoryPrice(ByVal sender As Object, ByVal hp As StructureBase.HistoryPrice) Handles Access.DeletedHistoryPrice
            Server.ServerSend("DeletedHistoryPrice", Code.SerializeWithZIP(hp))
        End Sub

        Private Sub Access_DeletedHistoryPriceList(ByVal sender As Object, ByVal hp As StructureBase.HistoryPrice) Handles Access.DeletedHistoryPriceList
            Server.ServerSend("DeletedHistoryPriceList", Code.SerializeWithZIP(hp))
        End Sub

        Private Sub Access_DeletedPersonnel(ByVal sender As Object, ByVal per As StructureBase.Personnel) Handles Access.DeletedPersonnel
            Server.ServerSend("DeletedPersonnel", Code.SerializeWithZIP(per))
        End Sub

        Private Sub Access_DeletedSales(ByVal sender As Object, ByVal sales As StructureBase.Sales) Handles Access.DeletedSales
            Server.ServerSend("DeletedSales", Code.SerializeWithZIP(sales))
        End Sub

        Private Sub Access_DeletedStock(ByVal sender As Object, ByVal stock As StructureBase.Stock) Handles Access.DeletedStock
            Server.ServerSend("DeletedStock", Code.SerializeWithZIP(stock))
        End Sub

        Private Sub Access_DeletedSupplier(ByVal sender As Object, ByVal sup As StructureBase.Supplier) Handles Access.DeletedSupplier
            Server.ServerSend("DeletedSupplier", Code.SerializeWithZIP(sup))
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

