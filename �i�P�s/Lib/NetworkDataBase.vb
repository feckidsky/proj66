Namespace Database
    Public Structure ReadArgs
        Dim Table As String
        Dim FileList() As String
        Dim SqlCommand() As String

    End Structure

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
            Server.ServerSend("ChangedSales", Code.SerializeWithZIP(New Access.SalesArgs(sales, GoodsList, OrderList, SalesContracts)))
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
            Server.ServerSend("CreatedSales", Code.SerializeWithZIP(New Access.SalesArgs(sales, GoodsList, OrderList, SalesContracts)))
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

    Public Class AccessClientMenage

        Public Client() As AccessClient

        Event BeforeDisconnect(ByVal sender As Object)
        Event BeforeConnect(ByVal sender As Object)


        Public Sub Save(ByVal Path As String)
            Dim lstInfo As New List(Of ClientInfo)
            For Each c As AccessClient In Client
                lstInfo.Add(New ClientInfo(c.Name, c.IP, c.Port))
            Next

            Code.Save(lstInfo.ToArray, Path)
        End Sub

        Public Sub Load(ByVal Path As String)
            Dim lstInfo As ClientInfo() = Code.Load(Path, New ClientInfo() {New ClientInfo("xx店", "192.168.1.132", 3600)})

            Dim lstClient As New List(Of Database.AccessClient)

            For Each c As ClientInfo In lstInfo
                lstClient.Add(New AccessClient(c.Name, c.IP, c.Port))
            Next

            Client = lstClient.ToArray
        End Sub

        Public Sub StartConnect()
            RaiseEvent BeforeConnect(Me)
            For i As Integer = 0 To Client.Length - 1
                Client(i).Connect()
            Next
        End Sub

        Public Sub EndConnect()
            RaiseEvent BeforeDisconnect(Me)
            For i As Integer = 0 To Client.Length - 1
                Client(i).Disconnect()
            Next
        End Sub

        Public Function GetNameList() As String()
            Return Array.ConvertAll(Client, Function(c As AccessClient) c.Name)
        End Function

        Default Public ReadOnly Property Item(ByVal Index As Integer) As AccessClient
            Get
                If Index < Client.Length - 1 Then Return Nothing
                Return Client(Index)
            End Get

        End Property

        Default Public ReadOnly Property Item(ByVal Name As String) As AccessClient
            Get
                For Each c As AccessClient In Client
                    If c.Name = Name Then Return c
                Next
                Return Nothing
            End Get
        End Property

    

    End Class

    Public Class AccessClient
        Inherits Access

        Event ConnectSuccess(ByVal sender As Object)

        Public WithEvents Client As New TCPTool.Client()

        Dim ResponseDataTable As Data.DataTable = Nothing

        Sub New()

        End Sub

        Sub New(ByVal Info As ClientInfo)
            Me.Name = Info.Name : Me.IP = Info.IP : Me.Port = Info.Port
        End Sub
        Sub New(ByVal Name As String, ByVal IP As String, ByVal Port As Integer)
            Me.Name = Name : Me.IP = IP : Me.Port = Port
            'Client.Port = 3600
        End Sub

        Public Name As String = "DefaultName"
        ReadOnly Property Connected() As Boolean
            Get
                Return Client.Connected
            End Get
        End Property
        Property IP() As String
            Get
                Return Client.IP
            End Get
            Set(ByVal value As String)
                Client.IP = value
            End Set
        End Property

        Property Port() As Integer
            Get
                Return Client.Port
            End Get
            Set(ByVal value As Integer)
                Client.Port = value
            End Set
        End Property

        Public Sub Connect()
            Client.StartConnect()
        End Sub

        Public Sub Disconnect()
            Client.EndConnect()
        End Sub

        Public Overrides Function Read(ByVal Table As String, ByVal FileList() As String, ByVal SQLCommand() As String) As Data.DataTable
            If Not Client.Connected Then
                MsgBox(Name & "尚未連線!", MsgBoxStyle.Exclamation)
                Return New DataTable
            End If

            Dim args As ReadArgs
            args.Table = Table
            args.FileList = FileList
            args.SqlCommand = SQLCommand

            Send("ReadArgs", Code.SerializeWithZIP(args))
            ResponseDataTable = Nothing

            While (ResponseDataTable Is Nothing)
                Application.DoEvents()
            End While

            Return ResponseDataTable
        End Function

        Public Sub Send(ByVal cmd As String, ByVal args As String)
            'If Not Client.Connected Then Client.Connect()
            Client.Send(cmd, args)
        End Sub


        Private Function Repair(Of T)(ByVal s As String) As T
            Return Code.DeserializeWithUnzip(Of T)(s)
        End Function

        Private Sub Client_ConnectedSuccess(ByVal Client As TCPTool.Client) Handles Client.ConnectedSuccess
            RaiseEvent ConnectSuccess(Me)
        End Sub

        Private Sub Client_ReceiveSplitMessage(ByVal Client As TCPTool.Client, ByVal IP As String, ByVal Port As Integer, ByVal Data() As String) Handles Client.ReceiveSplitMessage


            Dim args As String = ""
            If Data.Length > 1 Then args = Data(1)

            Select Case Data(0)
                Case "ReadResponse"
                    ResponseDataTable = Repair(Of DataTable)(args)

                Case "CreatedContract" : OnCreatedContract(Repair(Of Contract)(args))
                Case "DeletedContract" : OnDeletedContract(Repair(Of Contract)(args))
                Case "ChangedContract" : OnChangedContract(Repair(Of Contract)(args))
                Case "CreatedCustomer" : OnCreatedCustomer(Repair(Of Customer)(args))
                Case "DeletedCustomer" : OnDeletedCustomer(Repair(Of Customer)(args))
                Case "ChangedCustomer" : OnChangedCustomer(Repair(Of Customer)(args))
                Case "CreatedSupplier" : OnCreatedSupplier(Repair(Of Supplier)(args))
                Case "DeletedSupplier" : OnDeletedSupplier(Repair(Of Supplier)(args))
                Case "ChangedSupplier" : OnChangedSupplier(Repair(Of Supplier)(args))
                Case "CreatedPersonnel" : OnCreatedPersonnel(Repair(Of Personnel)(args))
                Case "DeletedPersonnel" : OnDeletedPersonnel(Repair(Of Personnel)(args))
                Case "ChangedPersonnel" : OnChangedPersonnel(Repair(Of Personnel)(args))
                Case "CreatedGoods" : OnCreatedGoods(Repair(Of Goods)(args))
                Case "DeletedGoods" : OnDeletedGoods(Repair(Of Goods)(args))
                Case "ChangedGoods" : OnChangedGoods(Repair(Of Goods)(args))
                Case "CreatedStock" : OnCreatedStock(Repair(Of Stock)(args))
                Case "DeletedStock" : OnDeletedStock(Repair(Of Stock)(args))
                Case "ChangedStock" : OnChangedStock(Repair(Of Stock)(args))
                Case "CreatedHistoryPrice" : OnCreatedHistoryPrice(Repair(Of HistoryPrice)(args))
                Case "DeletedHistoryPrice" : OnDeletedHistoryPrice(Repair(Of HistoryPrice)(args))
                Case "ChangedHistoryPrice" : OnChangedHistoryPrice(Repair(Of HistoryPrice)(args))
                Case "DeletedHistoryPriceList" : OnDeletedHistoryPriceList(Repair(Of HistoryPrice)(args))
                Case "CreatedSales" : OnCreatedSales(Repair(Of SalesArgs)(args))
                Case "ChangedSales" : OnChangedSales(Repair(Of SalesArgs)(args))
                Case "DeletedSales" : OnDeletedSales(Repair(Of Sales)(args))

                Case Else
                    MsgBox("不明指令:" & vbCrLf & Data(0))
            End Select
        End Sub
    End Class


End Namespace

