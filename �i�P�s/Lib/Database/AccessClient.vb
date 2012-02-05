Namespace Database
    Public Structure ReadArgs
        Dim Table As String
        Dim FileList() As String
        Dim SqlCommand() As String
    End Structure

    

    Public Class AccessClientMenage

        Public Client() As Access

        Event BeforeDisconnect(ByVal sender As Object, ByVal client As AccessClient)
        Event BeforeConnect(ByVal sender As Object, ByVal Client As AccessClient)


        Public Sub Save(ByVal Path As String)
            Dim lstInfo As New List(Of ClientInfo)
            For Each c As AccessClient In Client
                If c.GetType Is GetType(AccessClient) Then lstInfo.Add(New ClientInfo(c.Name, c.IP, c.Port))
            Next

            Code.Save(lstInfo.ToArray, Path)
        End Sub

        Public Shared Function Load(ByVal Path As String) As AccessClient()
            Dim lstInfo As ClientInfo() = Code.Load(Path, New ClientInfo() {New ClientInfo("xx店", "192.168.1.132", 3600)})

            Dim lstClient As New List(Of Database.AccessClient)

            For Each c As ClientInfo In lstInfo
                lstClient.Add(New AccessClient(c.Name, c.IP, c.Port))
            Next

            Return lstClient.ToArray
        End Function

        Public Sub StartConnect()

            For i As Integer = 0 To Client.Length - 1
                If Client(i).GetType Is GetType(AccessClient) Then
                    RaiseEvent BeforeConnect(Me, Client(i))
                    CType(Client(i), AccessClient).Connect()
                End If

            Next
        End Sub

        Public Sub EndConnect()

            For i As Integer = 0 To Client.Length - 1
                If Client(i).GetType Is GetType(AccessClient) Then
                    RaiseEvent BeforeDisconnect(Me, Client(i))
                    If Client(i).GetType Is GetType(AccessClient) Then CType(Client(i), AccessClient).Disconnect()
                End If
            Next
        End Sub

        Public Function GetNameList() As String()
            Return Array.ConvertAll(Client, Function(c As Access) c.Name)
        End Function

        Default Public ReadOnly Property Item(ByVal Index As Integer) As Access
            Get
                If Index < Client.Length - 1 Then Return Nothing
                Return Client(Index)
            End Get

        End Property

        Default Public ReadOnly Property Item(ByVal Name As String) As Access
            Get
                For Each c As Access In Client
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
            MyBase.New("遠端資料庫")
        End Sub

        Sub New(ByVal Info As ClientInfo)
            MyClass.New()
            Me.Name = Info.Name : Me.IP = Info.IP : Me.Port = Info.Port
        End Sub
        Sub New(ByVal Name As String, ByVal IP As String, ByVal Port As Integer)
            MyClass.New()
            Me.Name = Name : Me.IP = IP : Me.Port = Port
            'Client.Port = 3600
        End Sub


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

        Public Overrides Function Command(ByVal SqlCommand As String, ByVal File As String) As Long
            Dim Count As Long
            MsgBox("客戶端不支援直Command函數")
            Return Count
        End Function

        Dim ReadLock As String = "ReadLck"

        Dim Waiter As New Threading.AutoResetEvent(True)

        Public Overrides Function Read(ByVal Table As String, ByVal FileList() As String, ByVal SQLCommand() As String) As Data.DataTable
            If Not Client.Connected Then
                MsgBox(Name & "尚未連線!", MsgBoxStyle.Exclamation)
                Return New DataTable
            End If

            SyncLock ReadLock
                Dim args As ReadArgs
                args.Table = Table
                args.FileList = FileList
                args.SqlCommand = SQLCommand
                ResponseDataTable = Nothing

                Waiter.Reset()
                Send("ReadArgs", args)

                If Not Waiter.WaitOne(10000, False) Then
                    ResponseDataTable = New DataTable
                    MsgBox(Name & "沒有回應!", MsgBoxStyle.Exclamation)
                End If

                'While (ResponseDataTable Is Nothing)
                '    Application.DoEvents()
                'End While
            End SyncLock
            Return ResponseDataTable
        End Function

        Public Sub Send(Of T)(ByVal cmd As String, ByVal args As T)
            'If Not Client.Connected Then Client.Connect()
            Client.Send(cmd, Code.SerializeWithZIP(args))
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
                    Waiter.Set()
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
                Case "MsgBox" : MsgBox(Code.DeserializeWithUnzip(Of String)(args))
                Case Else
                    MsgBox("不明指令:" & vbCrLf & Data(0))
            End Select
        End Sub

        Overrides Sub AddPersonnel(ByVal pen As Personnel, Optional ByVal Trigger As Boolean = True)
            Send("CreatePersonnel", pen)
        End Sub

        Overrides Sub DeletePersonnel(ByVal pen As Personnel, Optional ByVal Trigger As Boolean = True)
            Send("DeletePersonnel", pen)
        End Sub
        Overrides Sub ChangePersonnel(ByVal pen As Personnel, Optional ByVal Trigger As Boolean = True)
            Send("ChangePersonnel", pen)
        End Sub

        Overrides Sub AddSupplier(ByVal data As Supplier, Optional ByVal Trigger As Boolean = True)
            Send("CreateSupplier", data)
        End Sub
        Overrides Sub DeleteSupplier(ByVal data As Supplier, Optional ByVal Trigger As Boolean = True)
            Send("DeleteSupplier", data)
        End Sub
        Overrides Sub ChangeSupplier(ByVal data As Supplier, Optional ByVal Trigger As Boolean = True)
            Send("ChangeSupplier", data)
        End Sub

        Overrides Sub AddCustomer(ByVal data As Customer, Optional ByVal Trigger As Boolean = True)
            Send("CreateCustomer", data)
        End Sub
        Overrides Sub DeleteCustomer(ByVal data As Customer, Optional ByVal Trigger As Boolean = True)
            Send("DeleteCustomer", data)
        End Sub

        Overrides Sub ChangeCustomer(ByVal data As Customer, Optional ByVal Trigger As Boolean = True)
            Send("ChangeCustomer", data)
        End Sub
        Overrides Sub AddContract(ByVal data As Contract, Optional ByVal Trigger As Boolean = True)
            Send("CreateContract", data)
        End Sub
        Overrides Sub DeleteContract(ByVal data As Contract, Optional ByVal Trigger As Boolean = True)
            Send("DeleteConract", data)
        End Sub
        Overrides Sub ChangeContract(ByVal data As Contract, Optional ByVal Trigger As Boolean = True)
            Send("ChangeContract", data)
        End Sub
        Overrides Sub AddGoods(ByVal data As Goods, Optional ByVal Trigger As Boolean = True)
            Send("CreateGoods", data)
        End Sub
        Overrides Sub DeleteGoods(ByVal data As Goods, Optional ByVal Trigger As Boolean = True)
            Send("DeleteGoods", data)
        End Sub
        Overrides Sub ChangeGoods(ByVal data As Goods, Optional ByVal Trigger As Boolean = True)
            Send("ChangeGoods", data)
        End Sub
        Overrides Sub AddStock(ByVal data As Stock)
            Send("CreateStock", data)
        End Sub
        Overrides Sub DeleteStock(ByVal data As Stock)
            Send("DeleteStock", data)
        End Sub
        Overrides Sub ChangeStock(ByVal data As Stock)
            Send("ChangeStock", data)
        End Sub
        Overrides Sub AddHistoryPrice(ByVal data As HistoryPrice, Optional ByVal Trigger As Boolean = True)
            Send("CreateHistoryPrice", data)
        End Sub
        Overrides Sub DeleteHistoryPrice(ByVal data As HistoryPrice, Optional ByVal Trigger As Boolean = True)
            Send("DeleteHistoryPrice", data)
        End Sub
        Overrides Sub ChangeHistoryPrice(ByVal data As HistoryPrice, Optional ByVal Trigger As Boolean = True)
            Send("ChangeHistoryPrice", data)
        End Sub
        Overrides Sub DeleteHistoryPriceList(ByVal Label As String, Optional ByVal Trigger As Boolean = True)
            Send("DeleteHistoryPriceList", Label)
        End Sub

        Overrides Sub CreateSales(ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal SalesContracts() As SalesContract)
            Send("CreateSales", New SalesArgs(sales, GoodsList, OrderList, SalesContracts))
        End Sub
        Overrides Sub ChangeSales(ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal SalesContracts() As SalesContract)
            Send("ChangeSales", New SalesArgs(sales, GoodsList, OrderList, SalesContracts))
        End Sub

        Overrides Sub DeleteSales(ByVal sales As Sales)
            Send("DeleteSales", sales)
        End Sub

    End Class


End Namespace

