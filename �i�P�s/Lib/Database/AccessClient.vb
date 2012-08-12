Namespace Database
    Public Structure ReadArgs
        Dim Table As String
        Dim FileList() As String
        Dim SqlCommand() As String
    End Structure

    
#Region "AccessClientMenage"
    ''' <summary>
    ''' AccessClient連線類別管理元件
    ''' </summary>
    Public Class AccessClientMenage

        ''' <summary>Client列表</summary>
        Public Client() As Access
        ''' <summary>斷線事件-在主動斷線前觸發</summary>
        Event BeforeDisconnect(ByVal sender As Object, ByVal client As AccessClient)
        ''' <summary>連線事件-在主動連線前觸發</summary>
        Event BeforeConnect(ByVal sender As Object, ByVal Client As AccessClient)
        ''' <summary>執行緒鎖定Key</summary>
        Dim SaveLock As String = "SaveLock"

        ''' <summary>儲存Client清單資訊</summary>
        Public Sub Save(ByVal Path As String)
            SyncLock SaveLock
                Dim lstInfo As New List(Of ClientInfo)
                For Each c As Access In Client
                    If c.GetType IsNot GetType(AccessClient) Then Continue For
                    Dim cc As AccessClient = c
                    If c.GetType Is GetType(AccessClient) Then lstInfo.Add(New ClientInfo(cc.Name, cc.IP, cc.Port))
                Next

                Code.SaveXml(lstInfo.ToArray, Path)
            End SyncLock
        End Sub

        ''' <summary>讀取Client清單資訊</summary>
        Public Shared Function Load(ByVal Path As String) As AccessClient()
            Dim lstInfo As ClientInfo() = Code.LoadXml(Path, New ClientInfo() {New ClientInfo("xx店", "192.168.1.132", 3600)})

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

        ''' <summary>開始連線，啟動所有Client的連線，並在連線之前觸發BeforeConnect事件。</summary>
        Public Sub StartConnect()

            For i As Integer = 0 To Client.Length - 1
                If Client(i).GetType Is GetType(AccessClient) Then
                    RaiseEvent BeforeConnect(Me, Client(i))
                    CType(Client(i), AccessClient).StartConnect()
                End If
            Next


        End Sub

        ''' <summary>結束連線，關閉所有Client連線，在關閉之前將觸發BeforeDisconnect事件。</summary>
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
            Dim names As New List(Of String)
            For i As Integer = 0 To Client.Length - 1
                names.Add(Client(i).Name)
            Next
            Return names.ToArray
            'Return Array.ConvertAll(Client, Function(c As Access) c.Name)
        End Function

        Default Public ReadOnly Property Item(ByVal Index As Integer) As Access
            Get
                If Index > Client.Length - 1 Then Return Nothing
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
        Public SyncWorking As Boolean = False
        Event ReceiveServerName(ByVal sender As Object, ByVal Name As String)

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

            Public Sub Desconnect()
                SyncLock ReadLock
                    For Each r As ReaderBase In Me
                        r.Desconnect()
                    Next
                    Me.Clear()
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

            Public Sub Desconnect()
                Try
                    Waiter.Set()
                Catch ex As Exception

                End Try
                BeginMsgBox(Name & "已經斷線!", MsgBoxStyle.Exclamation)
            End Sub
        End Class

        'Class Reader(Of T, ResultT)
        '    Inherits ReaderBase
        '    Public Args As T
        '    Public Result As ResultT
        '    Public DefaultResult As ResultT
        '    Public DeserializeFun As Func(Of String, ResultT) = AddressOf Code.XmlDeserializeWithUnzip

        '    Public Function Read(ByVal SendHandler As Action(Of String, String, String)) As ResultT
        '        Waiter.Reset()
        '        SendHandler("ReaderRequest", Guid & "," & Cmd, Code.XmlSerializeWithZIP(Args))
        '        Waiter.WaitOne()
        '        'If Not Waiter.WaitOne(TimeOut, False) Then
        '        '    Result = DefaultResult
        '        '    MsgBox(Name & "沒有回應!", MsgBoxStyle.Exclamation)
        '        'End If

        '        Return Result
        '    End Function

        '    Public Overrides Function Receive(ByVal Guid As String, ByVal SerializeData As String) As Boolean
        '        'If Me.Guid <> Guid Then Return False
        '        Result = DeserializeFun(SerializeData) 'Repair(Of ResultT)(SerializeData)
        '        Waiter.Set()
        '        Return True
        '    End Function
        'End Class



        Public Overrides Function Read(ByVal Table As String, ByVal FileList() As String, ByVal SQLCommand() As String, Optional ByVal ProgressAction As Progress = Nothing) As Data.DataTable
            If Not Connected() Then
                BeginMsgBox(Name & "尚未連線!", MsgBoxStyle.Exclamation)
                Return New DataTable
            End If

            Dim args As ReadArgs
            args.Table = Table
            args.FileList = FileList
            args.SqlCommand = SQLCommand


            Dim receiver As StreamReceiver = GetReceiver()

            Dim reader As New DataTableReader
            reader.Set(receiver, ProgressAction)
            Dim dt = reader.Read("DataTable", args)
            Return dt

        End Function

        Public Function FarRead(Of T, ResultT)(ByVal Cmd As String, ByVal args As T, Optional ByVal Progress As Progress = Nothing) As ResultT
            If Not Connected() Then
                BeginMsgBox(Name & "尚未連線!", MsgBoxStyle.Exclamation)
                Return Nothing
            End If

            Dim receiver As StreamReceiver = GetReceiver()
            Dim r As New Reader(Of T, ResultT)
            r.Set(receiver, Progress)
            Return r.Read(Cmd, args)
        End Function

        Class Reader(Of ArgsT, ResultT)
            WithEvents receiver As StreamReceiver
            Public Waiter As New Threading.AutoResetEvent(True)
            Public Progress As Progress

            Dim result As ResultT

            Public Serialize As Func(Of ArgsT, String) = AddressOf Code.XmlSerializeWithZIP(Of ArgsT)
            Public Deserialize As Func(Of String, ResultT) = AddressOf Code.XmlDeserializeWithUnzip(Of ResultT)

            Dim Readed As Boolean = False

            Sub New()

            End Sub

            Public Sub [Set](ByVal receiver As StreamReceiver, ByVal ProgressAction As Progress)
                Try
                    Me.receiver = receiver
                Catch

                End Try
                Me.Progress = ProgressAction
                'If Progress IsNot Nothing AndAlso Progress.CancelHandler IsNot Nothing Then
                If Progress IsNot Nothing Then
                    Progress.Report(0)
                    Dim newCancelHandler As New Progress.CancelAction(AddressOf receiver.Cancel)
                    Progress.CancelHandler = [Delegate].Combine(Progress.CancelHandler, newCancelHandler)
                End If

            End Sub

            Private ReadSyncLock As String = "ReadSyncLock"
            Public Function Read(ByVal cmd As String, ByVal args As ArgsT) As ResultT

                Readed = False
                receiver.Request(cmd, Serialize(args))


                Do Until Readed
                    Waiter.Reset()
                    Waiter.WaitOne(1000, False)
                Loop
                Return result
            End Function

            Private Sub receiver_Progress(ByVal sender As Object, ByVal percent As Integer) Handles receiver.Progress
                If Progress IsNot Nothing Then Progress.Report(percent)
            End Sub

            Private Sub receiver_Received(ByVal sender As Object, ByVal stream As System.IO.Stream) Handles receiver.Received

                Dim bytes() As Byte = CType(stream, IO.MemoryStream).ToArray '     stream.Read(bytes, 0, bytes.Length)
                Dim text As String = System.Text.Encoding.ASCII.GetString(bytes)
                result = Deserialize(text)
                Readed = True
                Waiter.Set()
                If Progress IsNot Nothing Then Progress.Finish()
            End Sub

            Private Sub receiver_TransFail(ByVal sender As Object, ByVal Message As String) Handles receiver.TransFail

                Readed = True
                Waiter.Set()

            End Sub
        End Class

        Class DataTableReader
            Inherits Reader(Of ReadArgs, DataTable)
        End Class

        Public Overrides Function GetSalesInformation(ByVal St As Date, ByVal Ed As Date) As Access.SalesInformation
            Dim args As GetSalesInformationArgs
            args.StartTime = St
            args.EndTime = Ed
            Return FarRead(Of GetSalesInformationArgs, SalesInformation)("GetSalesInformation", args)
        End Function

        Public Overrides Function GetErrorLogFileNames() As String()
            Return FarRead(Of String, String())("GetErrorLogFiles", "")
        End Function

        Public Overrides Function GetCloneBasePath() As String
            Return FarRead(Of String, String)("GetDir", "")
        End Function

        Public Overrides Sub DeleteFile(ByVal File As String)
            Send("DeleteFile", File)
        End Sub

        'Public Function Base64toByteWithZip(ByVal base64 As String) As Byte()
        '    Dim zipBytes() As Byte = Convert.FromBase64String(base64)
        '    Return Code.Unzip(zipBytes)
        'End Function


        Public Overloads Sub Send(Of T)(ByVal cmd As String, ByVal args As T)
            MyBase.Send(cmd, Code.XmlSerializeWithZIP(args))
        End Sub
        Public Overloads Sub Send(ByVal cmd As String, ByVal Guid As String, ByVal args As String)
            MyBase.Send(cmd, Guid & "," & args)
        End Sub
        Private Shared Function Repair(Of T)(ByVal s As String) As T
            Return Code.XmlDeserializeWithUnzip(Of T)(s)
        End Function

        Private Sub AccessClient_ConnectedFail(ByVal Client As TCPTool.Client) Handles Me.ConnectedFail
            lstReader.Desconnect()
        End Sub

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
                Case "CreatedAgendm" : OnCreatedAgendum(Repair(Of Agendum)(args))
                Case "ChangedAgendum" : OnChangedAgendum(Repair(Of Agendum)(args))
                Case "DeletedAgendum" : OnDeletedAgendum(Repair(Of Agendum)(args))
                Case "CreatedBulletin" : OnCreatedBulletin(Repair(Of Bulletin)(args))
                Case "ChangedBulletin" : OnChangedBulletin(Repair(Of Bulletin)(args))
                Case "DeletedBulletin" : OnDeletedBulletin(Repair(Of Bulletin)(args))
                Case "CreatedLog" : OnCreatedLog(Repair(Of Log)(args))
                Case "DeletedLog" : OnDeletedLog(Repair(Of Log)(args))
                Case "DeletedAllLog" : OnDeletedAllLog()
                Case "ServerVersion" : Version = Repair(Of String)(args)
                Case "MsgBox" : OnErrorMessage(Code.XmlDeserializeWithUnzip(Of String)(args)) 'MsgBox(Code.XmlDeserializeWithUnzip(Of String)(args))
                Case Else
                    'MsgBox("Client:不明指令:" & vbCrLf & Data(0))
                    OnErrorMessage("Client:不明指令:" & vbCrLf & Data(0))
            End Select
        End Sub

        Friend Overrides Sub OnConnectedSuccess()
            MyBase.OnConnectedSuccess()
            Send("GetServerVersion")
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

        Overrides Sub CreateSales(ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal ReturnList() As ReturnGoods, ByVal SalesContracts() As SalesContract)
            Send("CreateSales", New SalesArgs(sales, GoodsList, OrderList, ReturnList, SalesContracts))
        End Sub
        Overrides Sub ChangeSales(ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal ReturnList() As ReturnGoods, ByVal SalesContracts() As SalesContract)
            Send("ChangeSales", New SalesArgs(sales, GoodsList, OrderList, ReturnList, SalesContracts))
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

        Public Overrides Sub AddAgendum(ByVal data As Agendum, Optional ByVal trigger As Boolean = True)
            Send("CreateAgendum", data)
        End Sub

        Public Overrides Sub ChangeAgendum(ByVal data As Agendum, Optional ByVal trigger As Boolean = True)
            Send("ChangeAgendum", data)
        End Sub
        Public Overrides Sub DeleteAgendum(ByVal data As Agendum, Optional ByVal trigger As Boolean = True)
            Send("DeleteAgendum", data)
        End Sub

        Public Overrides Sub AddBulletin(ByVal data As Bulletin, Optional ByVal trigger As Boolean = True)
            Send("CreateBulletin", data)
        End Sub

        Public Overrides Sub ChangeBulletin(ByVal data As Bulletin, Optional ByVal trigger As Boolean = True)
            Send("ChangeBulletin", data)
        End Sub

        Public Overrides Sub DeleteBulletin(ByVal data As Bulletin, Optional ByVal trigger As Boolean = True)
            Send("DeleteBulletin", data)
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

