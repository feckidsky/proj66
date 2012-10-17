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
                    If c.GetType Is GetType(AccessClient) Then lstInfo.Add(New ClientInfo(cc.Name, cc.Client.IP, cc.Client.Port))
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
                    'CType(Client(i), AccessClient).StartConnect()
                    CType(Client(i), AccessClient).Client.StartConnect()
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
        Public WithEvents Client As New TCPTool.Client
        Public SyncWorking As Boolean = False
        'Event ReceiveServerName(ByVal sender As Object, ByVal Name As String)



        Sub New()
            MyBase.New("遠端資料庫")
        End Sub


        Sub New(ByVal Info As ClientInfo)
            MyClass.New()
            Me.Name = Info.Name : Me.Client.IP = Info.IP : Me.Client.Port = Info.Port
        End Sub
        Sub New(ByVal Name As String, ByVal IP As String, ByVal Port As Integer)
            MyClass.New()
            Me.Name = Name : Me.Client.IP = IP : Me.Client.Port = Port
        End Sub


        Public Sub Disconnect()
            Client.EndConnect()
        End Sub

        Public Overrides Function Connected() As Boolean
            Return Client.Connected()
        End Function

        Public o

        Public Overrides Function Download(ByVal sourcePath As String, ByVal DestPath As String) As TCPTool.Client.StreamReceiver
            Return Client.Download(sourcePath, DestPath)
        End Function

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


        Public Overrides Function Read(ByVal Table As String, ByVal FileList() As String, ByVal SQLCommand() As String, Optional ByVal ProgressAction As Progress = Nothing) As Data.DataTable
            If Not Connected() Then
                BeginMsgBox(Name & "尚未連線!", MsgBoxStyle.Exclamation)
                Return New DataTable
            End If

            Dim args As ReadArgs
            args.Table = Table
            args.FileList = FileList
            args.SqlCommand = SQLCommand


            Dim dt As DataTable
            If Version = "v1.0.7" Then
                Dim reader = New DataTableReader(Client)
                dt = reader.Read(args, ProgressAction)
            Else
                Dim reader = New AccessReader(Client)
                dt = reader.Read(args, ProgressAction)
            End If


            Return dt
        End Function

        Public Function FarRead(Of T, ResultT)(ByVal Cmd As String, ByVal args As T, Optional ByVal Progress As Progress = Nothing) As ResultT
            If Not Connected() Then
                BeginMsgBox(Name & "尚未連線!", MsgBoxStyle.Exclamation)
                Return Nothing
            End If

            Dim r As New Reader(Of T, ResultT)(Client)
            Return r.Read(Cmd, args, Progress)
        End Function

        Class Reader(Of ArgsT, ResultT)
            Inherits TCPTool.Client.StreamReceiver
            Public Waiter As New Threading.AutoResetEvent(True)

            Public Progresser As Progress

            Public result As ResultT

            'Public Serialize As Func(Of ArgsT, String) = AddressOf Code.XmlSerializeWithZIP(Of ArgsT)
            'Public Deserialize As Func(Of String, ResultT) = AddressOf Code.XmlDeserializeWithUnzip(Of ResultT)

            Public Readed As Boolean = False

            Sub New(ByVal client As TCPTool.Client)
                MyBase.New(client)
            End Sub

            Friend Overridable Function Serialize(ByVal args As ArgsT) As String  'As Func(Of ArgsT, String) = AddressOf Code.XmlSerializeWithZIP(Of ArgsT)
                Return Code.XmlSerializeWithZIP(Of ArgsT)(args)
            End Function

            Friend Overridable Function Deserialize(ByVal Str As String) As ResultT  'As Func(Of String, ResultT) = AddressOf Code.XmlDeserializeWithUnzip(Of ResultT)
                Return Code.XmlDeserializeWithUnzip(Of ResultT)(Str)
            End Function


            Private ReadSyncLock As String = "ReadSyncLock"
            Public Overridable Function Read(ByVal cmd As String, ByVal args As ArgsT, ByVal Progresser As Progress) As ResultT

                '設定進度回報元件
                Me.Progresser = Progresser
                If Progresser IsNot Nothing Then
                    Progresser.Report(0)
                    Dim newCancelHandler As New Progress.CancelAction(AddressOf Cancel)
                    Progresser.CancelHandler = [Delegate].Combine(Progresser.CancelHandler, newCancelHandler)
                End If

                Readed = False
                Request(cmd, Serialize(args))


                Do Until Readed
                    Waiter.Reset()
                    Waiter.WaitOne(1000, False)
                Loop
                Return result
            End Function

            Private Sub Reader_Progress(ByVal sender As Object, ByVal percent As Integer) Handles Me.Progress
                If Progresser IsNot Nothing Then Progresser.Report(percent)
            End Sub

            Private Sub Reader_Received(ByVal sender As Object, ByVal stream As System.IO.Stream) Handles Me.Received
                Dim bytes() As Byte = CType(stream, IO.MemoryStream).ToArray '     stream.Read(bytes, 0, bytes.Length)
                Dim text As String = System.Text.Encoding.ASCII.GetString(bytes)
                result = Deserialize(text)
                Readed = True
                Waiter.Set()
                If Progresser IsNot Nothing Then Progresser.Finish()
            End Sub

            Private Sub Reader_TransFail(ByVal sender As Object, ByVal Message As String) Handles Me.TransFail
                Readed = True
                Waiter.Set()
                If Progresser IsNot Nothing Then Progresser.Finish()
            End Sub

            Public Overrides Sub Cancel()
                MyBase.Cancel()
                Readed = True
                Waiter.Set()
            End Sub
        End Class

        Public Class DataTableReader
            Inherits Reader(Of ReadArgs, DataTable)


            Sub New(ByVal client As TCPTool.Client)
                MyBase.New(client)
            End Sub

            Public Overloads Function Read(ByVal args As ReadArgs, ByVal Progresser As Access.Progress) As System.Data.DataTable
                Return MyBase.Read("DataTable", args, Progresser)
            End Function

        End Class

        Public Class AccessReader
            Inherits Reader(Of ReadArgs, DataTable)

            Dim DataTable As DataTable
            Dim TotalRowsCount As Integer = 0
            Dim CursorRowIndex As Integer = 0

            Sub New(ByVal client As TCPTool.Client)
                MyBase.New(client)
            End Sub

            Public Overloads Function Read(ByVal args As ReadArgs, ByVal Progresser As Access.Progress) As System.Data.DataTable
                Return MyBase.Read("AccessReader", args, Progresser)
            End Function

            Structure ReceiveData
                Dim cmd As String
                Dim msg() As String
            End Structure

            Dim ReceiveBuffLock As String = "ReceiveBuffLock"
            Dim ReceiveBuff As New List(Of ReceiveData)

            Public Overrides Sub Receive(ByVal cmd As String, ByVal msg() As String)
                Select Case cmd
                    Case "DataTableStructure"
                        TotalRowsCount = msg(0)
                        DataTable = Code.XmlDeserializeWithUnzip(Of DataTable)(msg(1))
                        Report("取得資料表結構", 1)
                        result = DataTable
                        If TotalRowsCount = 0 Then Finish()
                        ProccessReceiveBuff()

                        CursorRowIndex = 0
                        RequestRow(CursorRowIndex)
                    Case "Row"

                        If DataTable IsNot Nothing Then
                            AddRow(cmd, msg)
                            ProccessReceiveBuff()
                        Else
                            Dim data As ReceiveData
                            data.cmd = cmd
                            data.msg = msg
                            SyncLock ReceiveBuffLock
                                ReceiveBuff.Add(data)
                            End SyncLock
                        End If
                        RequestRow(CursorRowIndex)
                End Select

                MyBase.Receive(cmd, msg)
            End Sub



            Public Sub RequestRow(ByVal Index As Integer)
                Send("RequestRow", Index)
            End Sub

            Public Sub ProccessReceiveBuff()
                If DataTable Is Nothing Then Exit Sub
                SyncLock ReceiveBuffLock
                    For i As Integer = 0 To ReceiveBuff.Count - 1
                        Dim data As ReceiveData = ReceiveBuff(i)
                        AddRow(data.cmd, data.msg)
                    Next
                    ReceiveBuff.Clear()
                End SyncLock
            End Sub

            Private AddRowLock As String = "AddRowLock"



            Public Sub AddRow(ByVal cmd As String, ByVal msg() As String)
                Dim Index As Integer = msg(0)
                Dim items As Object() = Array.ConvertAll(Split(msg(1), ":"), AddressOf Code.FromBase64)

                'Dim value(items.Length - 1) As Object
                'For i As Integer = 0 To items.Length - 1
                '    Dim t As Type = DataTable.Columns(i).DataType
                '    value(i) = CType(items(i), t)
                'Next

                SyncLock AddRowLock
                    'DataTable.Rows.Add(items)
                    Dim r As DataRow = DataTable.Rows.Add()

                    For i As Integer = 0 To items.Length - 1
                        Try
                            r.Item(i) = items(i)
                        Catch
                        End Try
                    Next
                End SyncLock
                Report("讀取進度", 1 + IIf(TotalRowsCount > 0, DataTable.Rows.Count / TotalRowsCount, 0) * 99)

                '完成
                If TotalRowsCount = DataTable.Rows.Count Then
                    result = DataTable
                    Finish()
                End If
                CursorRowIndex += 1

            End Sub

            Private Function GetValue(ByVal str As String) As Object
                Dim text As Object = Code.FromBase64(str)
                If text = "" Then Return Nothing
                Return text
            End Function

            Public Sub Finish()
                Send("Finish", "")
                result = DataTable
                Readed = True
                Waiter.Set()
                If Progresser IsNot Nothing Then Progresser.Finish()

            End Sub

            Public Sub Report(ByVal msg As String, ByVal percent As Integer)
                If Progresser IsNot Nothing Then Progresser.Report(msg, percent)
            End Sub



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

        Public Overloads Sub Send(ByVal cmd As String)
            Client.Send(cmd)
        End Sub
        Public Overloads Sub Send(Of T)(ByVal cmd As String, ByVal args As T)
            Client.Send(cmd, Code.XmlSerializeWithZIP(args))
        End Sub
        Public Overloads Sub Send(ByVal cmd As String, ByVal Guid As String, ByVal args As String)
            Client.Send(cmd, Guid & "," & args)
        End Sub
        Private Shared Function Repair(Of T)(ByVal s As String) As T
            Return Code.XmlDeserializeWithUnzip(Of T)(s)
        End Function

        Private Sub AccessClient_ConnectedFail(ByVal Client As TCPTool.Client) Handles Client.ConnectedFail
            lstReader.Desconnect()
            OnConnectedFail()
        End Sub



        Private Sub Client_ReceiveSplitMessage(ByVal Client As TCPTool.Client, ByVal IP As String, ByVal Port As Integer, ByVal Data() As String) Handles Client.ReceiveSplitMessage

            Dim args As String = ""
            If Data.Length > 1 Then args = Data(1)

            Select Case Data(0)
                Case "ServerName"
                    Dim newName As String = Repair(Of String)(args)
                    If Name <> newName Then
                        Name = newName
                        'RaiseEvent ReceiveServerName(Me, Name)
                        OnReceiveServerName(Name)
                    End If
                Case "ReaderResponse"
                    lstReader.Receive(Data(1), Data(2))
                Case "CreatedContract" : OnCreatedContract(Repair(Of Contract)(args), Source.Far)
                Case "DeletedContract" : OnDeletedContract(Repair(Of Contract)(args), Source.Far)
                Case "ChangedContract" : OnChangedContract(Repair(Of Contract)(args), Source.Far)
                Case "CreatedCustomer" : OnCreatedCustomer(Repair(Of Customer)(args), Source.Far)
                Case "DeletedCustomer" : OnDeletedCustomer(Repair(Of Customer)(args), Source.Far)
                Case "ChangedCustomer" : OnChangedCustomer(Repair(Of Customer)(args), Source.Far)
                Case "CreatedSupplier" : OnCreatedSupplier(Repair(Of Supplier)(args), Source.Far)
                Case "DeletedSupplier" : OnDeletedSupplier(Repair(Of Supplier)(args), Source.Far)
                Case "ChangedSupplier" : OnChangedSupplier(Repair(Of Supplier)(args), Source.Far)
                Case "CreatedPersonnel" : OnCreatedPersonnel(Repair(Of Personnel)(args), Source.Far)
                Case "DeletedPersonnel" : OnDeletedPersonnel(Repair(Of Personnel)(args), Source.Far)
                Case "ChangedPersonnel" : OnChangedPersonnel(Repair(Of Personnel)(args), Source.Far)
                Case "CreatedGoods" : OnCreatedGoods(Repair(Of Goods)(args), Source.Far)
                Case "DeletedGoods" : OnDeletedGoods(Repair(Of Goods)(args), Source.Far)
                Case "ChangedGoods" : OnChangedGoods(Repair(Of Goods)(args), Source.Far)
                Case "CreatedStock" : OnCreatedStock(Repair(Of Stock)(args))
                Case "DeletedStock" : OnDeletedStock(Repair(Of Stock)(args))
                Case "ChangedStock" : OnChangedStock(Repair(Of Stock)(args))
                Case "CreatedHistoryPrice" : OnCreatedHistoryPrice(Repair(Of HistoryPrice)(args), Source.Far)
                Case "DeletedHistoryPrice" : OnDeletedHistoryPrice(Repair(Of HistoryPrice)(args), Source.Far)
                Case "ChangedHistoryPrice" : OnChangedHistoryPrice(Repair(Of HistoryPrice)(args), Source.Far)
                Case "DeletedHistoryPriceList" : OnDeletedHistoryPriceList(Repair(Of HistoryPrice)(args), Source.Far)
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
                Case "Account_Login" : OnLogin(Code.XmlDeserializeWithUnzip(Of Database.LoginResult)(args))
                Case "Account_Logout" : OnLogout(Code.XmlDeserializeWithUnzip(Of Database.LoginResult)(args))
                Case "ServerVersion" : Version = Repair(Of String)(args) : GotServerVersion()
                Case "MsgBox" : OnErrorMessage(Code.XmlDeserializeWithUnzip(Of String)(args)) 'MsgBox(Code.XmlDeserializeWithUnzip(Of String)(args))
                Case "Messaged" : OnMessaged(Code.XmlDeserializeWithUnzip(Of MsgArgs)(args))
                Case Else
                    'MsgBox("Client:不明指令:" & vbCrLf & Data(0))
                    OnErrorMessage("Client:不明指令:" & vbCrLf & Data(0))
            End Select
        End Sub

        Public Overrides Sub LogIn(ByVal ID As String, ByVal Password As String)
            Dim e As LogInArgs
            e.ID = ID
            e.Password = Password
            Send("Login", e)
        End Sub

        Public Overrides Sub LogOut()
            If Client.Connected Then
                Send("Logout")
            Else
                MyBase.LogOut()
            End If
        End Sub


        Private Sub Client_ConnectedSuccess(ByVal Client As TCPTool.Client) Handles Client.ConnectedSuccess
            Client.Send(IIf(Mode = FarKind.Server, "IsServer", "IsClient"))
            Client.Send("GetServerVersion") '不觸發連線成功事件，直到取得Client版本資訊才觸發
            'Client.Send("IsServer")
        End Sub

        Private Sub GotServerVersion()
            OnConnectedSuccess() '取得版本資訊，觸發ConnectedSuccess事件
        End Sub

        Public Overrides Sub StockMoveIn(ByVal mStock As Stock, ByVal number As Integer)
            Dim e As StockMoveArgs
            e.Stock = mStock
            e.Number = number
            Send("StockMoveIn", e)
        End Sub

        Public Overrides Sub StockMoveOut(ByVal mStock As Stock, ByVal number As Integer)
            Dim e As StockMoveArgs
            e.Stock = mStock
            e.Number = number
            Send("StockMoveOut", e)
        End Sub

        Overrides Sub AddPersonnel(ByVal pen As Personnel, Optional ByVal source As Source = Source.Local)
            Send("CreatePersonnel", pen)
        End Sub

        Overrides Sub DeletePersonnel(ByVal pen As Personnel, Optional ByVal source As Source = Source.Local)
            Send("DeletePersonnel", pen)
        End Sub
        Overrides Sub ChangePersonnel(ByVal pen As Personnel, Optional ByVal source As Source = Source.Local)
            Send("ChangePersonnel", pen)
        End Sub

        Overrides Sub AddSupplier(ByVal data As Supplier, Optional ByVal source As Source = Source.Local)
            Send("CreateSupplier", data)
        End Sub
        Overrides Sub DeleteSupplier(ByVal data As Supplier, Optional ByVal source As Source = Source.Local)
            Send("DeleteSupplier", data)
        End Sub
        Overrides Sub ChangeSupplier(ByVal data As Supplier, Optional ByVal source As Source = Source.Local)
            Send("ChangeSupplier", data)
        End Sub

        Overrides Sub AddCustomer(ByVal data As Customer, Optional ByVal source As Source = Source.Local)
            Send("CreateCustomer", data)
        End Sub
        Overrides Sub DeleteCustomer(ByVal data As Customer, Optional ByVal source As Source = Source.Local)
            Send("DeleteCustomer", data)
        End Sub

        Overrides Sub ChangeCustomer(ByVal data As Customer, Optional ByVal source As Source = Source.Local)
            Send("ChangeCustomer", data)
        End Sub
        Overrides Sub AddContract(ByVal data As Contract, Optional ByVal source As Source = Source.Local)
            Send("CreateContract", data)
        End Sub
        Overrides Sub DeleteContract(ByVal data As Contract, Optional ByVal source As Source = Source.Local)
            Send("DeleteContract", data)
        End Sub
        Overrides Sub ChangeContract(ByVal data As Contract, Optional ByVal source As Source = Source.Local)
            Send("ChangeContract", data)
        End Sub
        Overrides Sub AddGoods(ByVal data As Goods, Optional ByVal source As Source = Source.Local)
            Send("CreateGoods", data)
        End Sub
        Overrides Sub DeleteGoods(ByVal data As Goods, Optional ByVal source As Source = Source.Local)
            Send("DeleteGoods", data)
        End Sub
        Overrides Sub ChangeGoods(ByVal data As Goods, Optional ByVal source As Source = Source.Local)
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
        Overrides Sub AddHistoryPrice(ByVal data As HistoryPrice, Optional ByVal source As Source = Source.Local)
            Send("CreateHistoryPrice", data)
        End Sub
        Overrides Sub DeleteHistoryPrice(ByVal data As HistoryPrice, Optional ByVal source As Source = Source.Local)
            Send("DeleteHistoryPrice", data)
        End Sub
        Overrides Sub ChangeHistoryPrice(ByVal data As HistoryPrice, Optional ByVal source As Source = Source.Local)
            Send("ChangeHistoryPrice", data)
        End Sub
        Overrides Sub DeleteHistoryPriceList(ByVal Label As String, Optional ByVal source As Source = Source.Local)
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

