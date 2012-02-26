Namespace Database
    Public Structure ReadArgs
        Dim Table As String
        Dim FileList() As String
        Dim SqlCommand() As String
    End Structure

    
#Region "AccessClientMenage"
    Public Class AccessClientMenage

        Public Client() As Access

        Event BeforeDisconnect(ByVal sender As Object, ByVal client As AccessClient)
        Event BeforeConnect(ByVal sender As Object, ByVal Client As AccessClient)

        Dim SaveLock As String = "SaveLock"
        Public Sub Save(ByVal Path As String)
            SyncLock SaveLock
                Dim lstInfo As New List(Of ClientInfo)
                For Each c As Access In Client
                    If c.GetType IsNot GetType(AccessClient) Then Continue For
                    Dim cc As AccessClient = c
                    If c.GetType Is GetType(AccessClient) Then lstInfo.Add(New ClientInfo(cc.Name, cc.IP, cc.Port))
                Next

                Code.Save(lstInfo.ToArray, Path)
            End SyncLock
        End Sub

        Public Shared Function Load(ByVal Path As String) As AccessClient()
            Dim lstInfo As ClientInfo() = Code.Load(Path, New ClientInfo() {New ClientInfo("xx店", "192.168.1.132", 3600)})

            Dim lstClient As New List(Of Database.AccessClient)

            Dim idx As Integer = 0
            For Each c As ClientInfo In lstInfo
                If c.Name Is Nothing Then '避免Clien沒有名稱的狀況
                    c.Name = "C" & idx
                    idx += 1
                End If
                lstClient.Add(New AccessClient(c.Name, c.IP, c.Port))
            Next

            Return lstClient.ToArray
        End Function

        Public Sub StartConnect()

            For i As Integer = 0 To Client.Length - 1
                If Client(i).GetType Is GetType(AccessClient) Then
                    RaiseEvent BeforeConnect(Me, Client(i))
                    CType(Client(i), AccessClient).StartConnect()
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

        'Public Function Login(ByVal ID As String, ByVal Password As String, Optional ByVal Trigger As Boolean = True) As LoginResult
        '    For Each c As Access In Client
        '        If c.GetType Is GetType(Access) OrElse (c.GetType Is GetType(AccessClient) AndAlso c.Connected) Then
        '            Return c.LogIn(ID, Password, Trigger)
        '        End If
        '    Next

        '    Return New LoginResult(LoginState.Disconnect, "尚未連線", Personnel.Guest, Nothing)
        'End Function

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
#End Region

    Public Class AccessClient
        Inherits Access

        Event ReceiveServerName(ByVal sender As Object, ByVal Name As String)

        'Dim ResponseDataTable As Data.DataTable = Nothing

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
        End Sub

        Public Sub Disconnect()
            EndConnect()
        End Sub

        Public Overrides Function Command(ByVal SqlCommand As String, ByVal File As String) As Long
            Dim Count As Long
            MsgBox("客戶端不支援Command函數")
            Return Count
        End Function



        Class ReaderList
            Inherits List(Of ReaderBase)
            Dim ReadLock As String = "ReadLock"

            Public Overloads Sub Add(ByVal Item As ReaderBase)
                SyncLock ReadLock
                    MyBase.Add(Item)
                End SyncLock
            End Sub

            Public Overloads Sub Remove(ByVal item As ReaderBase)
                SyncLock ReadLock
                    MyBase.Remove(item)
                End SyncLock
            End Sub

            Public Sub Receive(ByVal Guid As String, ByVal SerializeData As String)
                SyncLock ReadLock
                    Dim Readers As List(Of ReaderBase) = FindAll(Function(i As ReaderBase) i.Guid = Guid)
                    For Each r As ReaderBase In Readers
                        r.Receive(Guid, SerializeData)
                    Next
                End SyncLock
            End Sub
        End Class

        Public lstReader As New ReaderList

        MustInherit Class ReaderBase
            Public Name As String
            Public Guid As String
            Public Waiter As New Threading.AutoResetEvent(True)
            Public TimeOut As Long = 10000
            Public Cmd As String = "ReaderBase"
            Sub New()
                Me.Guid = Convert.ToBase64String(System.Guid.NewGuid.ToByteArray)
            End Sub

            Sub New(ByVal Name As String)
                Me.Name = Name
                Me.Guid = Convert.ToBase64String(System.Guid.NewGuid.ToByteArray)
            End Sub

            Public MustOverride Function Receive(ByVal Guid As String, ByVal SerializeData As String) As Boolean
        End Class

        Class Reader(Of T, ResultT)
            Inherits ReaderBase
            Public Args As T
            Public Result As ResultT
            Public DefaultResult As ResultT
            Public DeserializeFun As Func(Of String, ResultT) = AddressOf Code.DeserializeWithUnzip

            Public Function Read(ByVal SendHandler As Action(Of String, String, String)) As ResultT
                Waiter.Reset()
                SendHandler("ReaderRequest", Guid & "," & Cmd, Code.SerializeWithZIP(Args))
                If Not Waiter.WaitOne(TimeOut, False) Then
                    Result = DefaultResult
                    MsgBox(Name & "沒有回應!", MsgBoxStyle.Exclamation)
                End If

                Return Result
            End Function


            Public Overrides Function Receive(ByVal Guid As String, ByVal SerializeData As String) As Boolean
                If Me.Guid <> Guid Then Return False
                Result = DeserializeFun(SerializeData) 'Repair(Of ResultT)(SerializeData)
                Waiter.Set()
                Return True
            End Function

        End Class

        Public Overrides Function Read(ByVal Table As String, ByVal FileList() As String, ByVal SQLCommand() As String) As Data.DataTable
            If Not Connected() Then
                MsgBox(Name & "尚未連線!", MsgBoxStyle.Exclamation)
                Return New DataTable
            End If

            Dim args As ReadArgs
            args.Table = Table
            args.FileList = FileList
            args.SqlCommand = SQLCommand


            Dim Reader As New Reader(Of ReadArgs, DataTable)
            Reader.Name = Name
            Reader.DefaultResult = Nothing
            Reader.Cmd = "DataTable"
            Reader.Args = args

            lstReader.Add(Reader)
            Dim dt As DataTable = Reader.Read(AddressOf Send)
            lstReader.Remove(Reader)

            Return dt

        End Function

        Public Overrides Function GetErrorLogFileNames() As String()
            Dim reader As New Reader(Of String, String())
            reader.Name = Name
            reader.DefaultResult = New String() {}
            reader.Cmd = "GetErrorLogFiles"
            lstReader.Add(reader)
            Dim files As String() = reader.Read(AddressOf Send)
            lstReader.Remove(reader)
            Return files
        End Function

        Public Function Base64toByteWithZip(ByVal base64 As String) As Byte()
            Dim zipBytes() As Byte = Convert.FromBase64String(base64)
            Return Code.Unzip(zipBytes)
        End Function

        Public Overrides Sub Download(ByVal sourcePath As String, ByVal DestPath As String)
            Dim reader As New Reader(Of String, Byte())
            reader.Name = Name
            reader.DefaultResult = New Byte() {}
            reader.Cmd = "Download"
            reader.Args = sourcePath
            reader.DeserializeFun = AddressOf Base64toByteWithZip

            lstReader.Add(reader)
            Dim data As Byte() = reader.Read(AddressOf Send)
            lstReader.Remove(reader)

            Dim writer As New IO.FileStream(DestPath, IO.FileMode.Create)
            writer.Write(data, 0, data.Length)
            writer.Close()
        End Sub

        Public Overloads Sub Send(Of T)(ByVal cmd As String, ByVal args As T)
            MyBase.Send(cmd, Code.SerializeWithZIP(args))
        End Sub
        Public Overloads Sub Send(ByVal cmd As String, ByVal Guid As String, ByVal args As String)
            MyBase.Send(cmd, Guid & "," & args)
        End Sub
        Private Shared Function Repair(Of T)(ByVal s As String) As T
            Return Code.DeserializeWithUnzip(Of T)(s)
        End Function

        Private Sub Client_ReceiveSplitMessage(ByVal Client As TCPTool.Client, ByVal IP As String, ByVal Port As Integer, ByVal Data() As String) Handles MyBase.ReceiveSplitMessage

            Dim args As String = ""
            If Data.Length > 1 Then args = Data(1)

            Select Case Data(0)
                Case "ServerName"
                    Dim newName As String = Repair(Of String)(args)
                    If Name <> newName Then
                        Name = newName
                        RaiseEvent ReceiveServerName(Me, Name)
                    End If
                Case "ReaderResponse"
                    lstReader.Receive(Data(1), Data(2))
                    'SyncLock ReadLock
                    '    For i As Integer = 0 To lstReader.Count - 1
                    '        If lstReader(i).Receive(Data(1), Data(2)) Then
                    '            'lstReader.RemoveAt(i)
                    '            Exit For
                    '        End If
                    '    Next
                    'End SyncLock
                    'ResponseDataTable = Repair(Of DataTable)(args)
                    'Waiter.Set()
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
                Case "CreatedStockMove" : OnCreatedStockMove(Repair(Of StockMove)(args))
                Case "ChangedStockMove" : OnChangedStockMove(Repair(Of StockMove)(args))
                Case "DeletedStockMove" : OnDeletedStockMove(Repair(Of StockMove)(args))
                Case "CreatedLog" : OnCreatedLog(Repair(Of Log)(args))
                Case "DeletedLog" : OnDeletedLog(Repair(Of Log)(args))
                Case "DeletedAllLog" : OnDeletedAllLog()
                Case "MsgBox" : MsgBox(Code.DeserializeWithUnzip(Of String)(args))
                Case Else
                    MsgBox("Client:不明指令:" & vbCrLf & Data(0))
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
            Send("DeleteContract", data)
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

        Overrides Sub AddStockMove(ByVal data As StockMove, Optional ByVal trigger As Boolean = True)
            Send("CreateStockMove", data)
        End Sub

        Public Overrides Sub ChangeStockMove(ByVal data As StockMove, Optional ByVal trigger As Boolean = True)
            Send("ChangeStockMove", data)
        End Sub

        Public Overrides Sub DeleteStockMove(ByVal data As StockMove, Optional ByVal trigger As Boolean = True)
            Send("DeleteStockMove", data)
        End Sub

        Public Overrides Sub AddLog(ByVal data As Log, Optional ByVal trigger As Boolean = True)
            Send("CreateLog", data)
        End Sub

        Public Overrides Sub DeleteLog(ByVal data As Log, Optional ByVal trigger As Boolean = True)
            Send("DeleteLog", data)
        End Sub

        Public Overrides Sub DeleteAllLog(Optional ByVal trigger As Boolean = True)
            Send("DeleteAllLog")
        End Sub



        'Public Overrides Sub LogOut(Optional ByVal TriggerEvent As Boolean = True)
        '    Send("LogOut")
        'End Sub


    End Class


End Namespace

