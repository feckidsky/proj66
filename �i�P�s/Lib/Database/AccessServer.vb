Namespace Database
    Public Class AccessServer
        Inherits TCPTool
        Public WithEvents Access As Access
        Public WithEvents WriterByClient As Access
        Public Port As Integer = 3600
        Public Version As String = "none"
        Public Name As String

        Public AccessList As New List(Of ServiceClient)


        Public Sub Open(ByVal Index As Integer)
            ServerOpen(Port, Index)
        End Sub

        Public Sub Close()
            ServerClose()
        End Sub

        '未完成，如果Access是本機元件對所有Client發送訊息，如果不是則只對非Server的連線發送訊息
        Public Overloads Sub ServerSend(ByVal source As Access.Source, ByVal CMD As String, ByVal Para As String)
            For Each c As ServiceClient In AccessList
                If source = Database.Access.Source.Local Or (source = Database.Access.Source.Far And c.Access.Mode = Database.Access.FarKind.Client) Then
                    c.Client.Send(CMD, Para)
                End If
            Next

        End Sub

        Public Overloads Sub ServerSend(ByVal source As Access.Source, ByVal Para As String)
            For Each c As ServiceClient In AccessList
                If source = Database.Access.Source.Local Or (source = Database.Access.Source.Far And c.Access.Mode = Database.Access.FarKind.Client) Then
                    c.Client.Send(Para)
                End If
            Next

        End Sub

        '未完成
        Public Sub SendByServer()

        End Sub



        Public Sub ChangeName(ByVal newName As String)
            Name = newName
            ServerSend( "ServerName", Code.XmlSerializeWithZIP(Name))
        End Sub


        '有Client連線，建立ServiceClient並加入AccessList清單中以利追蹤
        Private Sub Server_ReceiveConnected(ByVal Sender As TCPTool, ByVal Client As TCPTool.Client) Handles MyBase.ReceiveConnected
            Dim access As New ServiceClient(Me, Client, Name, myDatabase)
            access.Access.Version = Version
            AccessList.Add(access)
            Client.Send("ServerName", Code.XmlSerializeWithZIP(Name))
        End Sub

        '連線中斷，從AccessList中移除
        Private Sub AccessServer_ClientConnectedFail(ByVal Client As TCPTool.Client) Handles Me.ClientConnectedFail
            Dim access As ServiceClient = AccessList.Find(Function(c As ServiceClient) c.Client Is Client)
            If access IsNot Nothing Then AccessList.Remove(access)
        End Sub

        Public Sub ChangedAgendum(ByVal sender As Object, ByVal Agendum As Agendum) Handles Access.ChangedAgendum, WriterByClient.ChangedAgendum
            ServerSend("ChangedAgendm", Code.XmlSerializeWithZIP(Agendum))
        End Sub

        Public Sub ChangedBulletin(ByVal sender As Object, ByVal bulletin As Bulletin) Handles Access.ChangedBulletin, WriterByClient.ChangedBulletin
            ServerSend("ChangedBulletin", Code.XmlSerializeWithZIP(bulletin))
        End Sub

        Public Sub ChangedContract(ByVal sender As Object, ByVal con As Contract, ByVal source As Access.Source) Handles Access.ChangedContract, WriterByClient.ChangedContract
            ServerSend(source, "ChangedContract", Code.XmlSerializeWithZIP(con))
        End Sub

        Public Sub ChangedCustomer(ByVal sender As Object, ByVal cus As Customer, ByVal source As Access.Source) Handles Access.ChangedCustomer, WriterByClient.ChangedCustomer
            ServerSend(source, "ChangedCustomer", Code.XmlSerializeWithZIP(cus))
        End Sub

        Public Sub ChangedGoods(ByVal sender As Object, ByVal goods As Goods, ByVal source As Access.Source) Handles Access.ChangedGoods, WriterByClient.ChangedGoods
            ServerSend(source, "ChangedGoods", Code.XmlSerializeWithZIP(goods))
        End Sub

        Public Sub ChangedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice, ByVal source As Access.Source) Handles Access.ChangedHistoryPrice, WriterByClient.ChangedHistoryPrice
            ServerSend(source, "ChangedHistoryPrice", Code.XmlSerializeWithZIP(hp))
        End Sub

        Public Sub ChangedPersonnel(ByVal sender As Object, ByVal per As Personnel, ByVal source As Access.Source) Handles Access.ChangedPersonnel, WriterByClient.ChangedPersonnel
            ServerSend(source, "ChangedPersonnel", Code.XmlSerializeWithZIP(per))
        End Sub

        Public Sub ChangedSales(ByVal sender As Object, ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal ReturnList() As ReturnGoods, ByVal SalesContracts() As SalesContract) Handles Access.ChangedSales, WriterByClient.ChangedSales
            ServerSend("ChangedSales", Code.XmlSerializeWithZIP(New SalesArgs(sales, GoodsList, OrderList, ReturnList, SalesContracts)))
        End Sub

        Public Sub ChangedStock(ByVal sender As Object, ByVal stock As Stock) Handles Access.ChangedStock, WriterByClient.ChangedStock
            ServerSend("ChangedStock", Code.XmlSerializeWithZIP(stock))
        End Sub

        Public Sub ChangedStockMove(ByVal sender As Object, ByVal data As StockMove) Handles Access.ChangedStockMove, WriterByClient.ChangedStockMove
            ServerSend("ChangedStockMove", Code.XmlSerializeWithZIP(data))
        End Sub

        Public Sub ChangedSupplier(ByVal sender As Object, ByVal sup As Supplier, ByVal source As Access.Source) Handles Access.ChangedSupplier, WriterByClient.ChangedSupplier
            ServerSend(source, "ChangedSupplier", Code.XmlSerializeWithZIP(sup))
        End Sub

        Public Sub CreatedAgendum(ByVal sender As Object, ByVal Agendum As Agendum) Handles Access.CreatedAgendum, WriterByClient.CreatedAgendum
            ServerSend("CreatedAgendum", Code.XmlSerializeWithZIP(Agendum))
        End Sub

        Public Sub CreatedBulletin(ByVal sender As Object, ByVal bulletin As Bulletin) Handles Access.CreatedBulletin, WriterByClient.CreatedBulletin
            ServerSend("CreatedBulletin", Code.XmlSerializeWithZIP(bulletin))
        End Sub
        Public Sub CreatedContract(ByVal sender As Object, ByVal con As Contract, ByVal source As Access.Source) Handles Access.CreatedContract, WriterByClient.CreatedContract
            ServerSend(source, "CreatedContract", Code.XmlSerializeWithZIP(con))
        End Sub

        Public Sub CreatedCustomer(ByVal sender As Object, ByVal cus As Customer, ByVal source As Access.Source) Handles Access.CreatedCustomer, WriterByClient.CreatedCustomer
            ServerSend(source, "CreatedCustomer", Code.XmlSerializeWithZIP(cus))
        End Sub

        Public Sub CreatedGoods(ByVal sender As Object, ByVal goods As Goods, ByVal source As Access.Source) Handles Access.CreatedGoods, WriterByClient.CreatedGoods
            ServerSend(source, "CreatedGoods", Code.XmlSerializeWithZIP(goods))
        End Sub

        Public Sub CreatedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice, ByVal source As Access.Source) Handles Access.CreatedHistoryPrice, WriterByClient.CreatedHistoryPrice
            ServerSend(source, "CreatedHistoryPrice", Code.XmlSerializeWithZIP(hp))
        End Sub

        Public Sub CreatedLog(ByVal sender As Object, ByVal log As Log) Handles Access.CreatedLog, WriterByClient.CreatedLog
            ServerSend("CreatedLog", Code.XmlSerializeWithZIP(log))
        End Sub

        Public Sub CreatedPersonnel(ByVal sender As Object, ByVal per As Personnel, ByVal source As Access.Source) Handles Access.CreatedPersonnel, WriterByClient.CreatedPersonnel
            ServerSend(source, "CreatedPersonnel", Code.XmlSerializeWithZIP(per))
        End Sub

        Public Sub CreatedSales(ByVal sender As Object, ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal ReturnList() As ReturnGoods, ByVal SalesContracts() As SalesContract) Handles Access.CreatedSales, WriterByClient.CreatedSales
            ServerSend("CreatedSales", Code.XmlSerializeWithZIP(New SalesArgs(sales, GoodsList, OrderList, ReturnList, SalesContracts)))
        End Sub

        Public Sub CreatedStock(ByVal sender As Object, ByVal stock As Stock) Handles Access.CreatedStock, WriterByClient.CreatedStock
            ServerSend("CreatedStock", Code.XmlSerializeWithZIP(stock))
        End Sub

        Public Sub CreatedStockMove(ByVal sender As Object, ByVal data As StockMove) Handles Access.CreatedStockMove, WriterByClient.CreatedStockMove
            ServerSend("CreatedStockMove", Code.XmlSerializeWithZIP(data))
        End Sub

        Public Sub CreatedSupplier(ByVal sender As Object, ByVal sup As Supplier, ByVal source As Access.Source) Handles Access.CreatedSupplier, WriterByClient.CreatedSupplier
            ServerSend(source, "CreatedSupplier", Code.XmlSerializeWithZIP(sup))
        End Sub

        Public Sub DeletedAgendum(ByVal sender As Object, ByVal Agendum As Agendum) Handles Access.DeletedAgendum, WriterByClient.DeletedAgendum
            ServerSend("DeletedAgendum", Code.XmlSerializeWithZIP(Agendum))
        End Sub

        Public Sub DeletedAllLog(ByVal sender As Object) Handles Access.DeletedAllLog, WriterByClient.DeletedAllLog
            ServerSend(sender, "DeletedAllLog")
        End Sub

        Public Sub DeletedBulletin(ByVal sender As Object, ByVal bulletin As Bulletin) Handles Access.DeletedBulletin, WriterByClient.DeletedBulletin
            ServerSend("DeletedBulletin", Code.XmlSerializeWithZIP(bulletin))
        End Sub
        Public Sub DeletedContract(ByVal sender As Object, ByVal con As Contract, ByVal source As Access.Source) Handles Access.DeletedContract, WriterByClient.DeletedContract
            ServerSend(source, "DeletedContract", Code.XmlSerializeWithZIP(con))
        End Sub

        Public Sub DeletedCustomer(ByVal sender As Object, ByVal cus As Customer, ByVal source As Access.Source) Handles Access.DeletedCustomer, WriterByClient.DeletedCustomer
            ServerSend(source, "DeletedCustomer", Code.XmlSerializeWithZIP(cus))
        End Sub

        Public Sub DeletedGoods(ByVal sender As Object, ByVal goods As Goods, ByVal source As Access.Source) Handles Access.DeletedGoods, WriterByClient.DeletedGoods
            ServerSend(source, "DeletedGoods", Code.XmlSerializeWithZIP(goods))
        End Sub

        Public Sub DeletedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice, ByVal source As Access.Source) Handles Access.DeletedHistoryPrice, WriterByClient.DeletedHistoryPrice
            ServerSend(source, "DeletedHistoryPrice", Code.XmlSerializeWithZIP(hp))
        End Sub

        Public Sub DeletedHistoryPriceList(ByVal sender As Object, ByVal hp As HistoryPrice, ByVal source As Access.Source) Handles Access.DeletedHistoryPriceList, WriterByClient.DeletedHistoryPriceList
            ServerSend(source, "DeletedHistoryPriceList", Code.XmlSerializeWithZIP(hp))
        End Sub

        Public Sub DeletedLog(ByVal sender As Object, ByVal log As Log) Handles Access.DeletedLog, WriterByClient.DeletedLog
            ServerSend("DeletedLog", Code.XmlSerializeWithZIP(log))
        End Sub

        Public Sub DeletedPersonnel(ByVal sender As Object, ByVal per As Personnel, ByVal source As Access.Source) Handles Access.DeletedPersonnel, WriterByClient.DeletedPersonnel
            ServerSend(source, "DeletedPersonnel", Code.XmlSerializeWithZIP(per))
        End Sub

        Public Sub DeletedSales(ByVal sender As Object, ByVal sales As Sales) Handles Access.DeletedSales, WriterByClient.DeletedSales
            ServerSend("DeletedSales", Code.XmlSerializeWithZIP(sales))
        End Sub

        Public Sub DeletedStock(ByVal sender As Object, ByVal stock As Stock) Handles Access.DeletedStock, WriterByClient.DeletedStock
            ServerSend("DeletedStock", Code.XmlSerializeWithZIP(stock))
        End Sub

        Public Sub DeletedStockMove(ByVal sender As Object, ByVal data As StockMove) Handles Access.DeletedStockMove, WriterByClient.DeletedStockMove
            ServerSend("DeletedStockMove", Code.XmlSerializeWithZIP(data))
        End Sub

        Public Sub DeletedSupplier(ByVal sender As Object, ByVal sup As Supplier, ByVal source As Access.Source) Handles Access.DeletedSupplier, WriterByClient.DeletedSupplier
            ServerSend(source, "DeletedSupplier", Code.XmlSerializeWithZIP(sup))
        End Sub

        Private Sub Access_Messaged(ByVal sender As Object, ByVal e As Access.MsgArgs) Handles Access.Messaged, WriterByClient.Messaged
            MsgBox(e.Text, e.style, e.Title)
        End Sub

        ''' <summary>連接遠端Client與本地Access各種行為的中介元件 </summary> 
        Public Class ServiceClient
            Public WithEvents Access As Access
            Public LocalAccess As Access
            Public WithEvents Client As TCPTool.Client
            Dim server As TCPTool

            Sub New(ByVal server As TCPTool, ByVal client As TCPTool.Client, ByVal Name As String, ByVal local As Access)
                Access = New Access(Name)
                Me.Client = client
                Me.server = server
                Me.LocalAccess = local
            End Sub

            Sub New(ByVal server As TCPTool, ByVal access As Access, ByVal local As Access)
                Me.Access = access
                Client = Nothing  'New Client()
                Me.server = server
                Me.LocalAccess = local
            End Sub


            'Public Sub ServerSend(ByVal cmd As String, ByVal args As String)
            '    server.ServerSend(cmd, args)
            'End Sub

            'Public Sub ServerSend(ByVal text As String)
            '    server.ServerSend(text)
            'End Sub



            Private Sub ClientReceiveSplitMessage(ByVal Client As TCPTool.Client, ByVal IP As String, ByVal Port As Integer, ByVal Data() As String) Handles Client.ReceiveSplitMessage
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
                    Case "CreateAgendum" : Access.AddAgendum(Code.XmlDeserializeWithUnzip(Of Agendum)(Data(1)))
                    Case "ChangeAgendum" : Access.ChangeAgendum(Code.XmlDeserializeWithUnzip(Of Agendum)(Data(1)))
                    Case "DeleteAgendum" : Access.DeleteAgendum(Code.XmlDeserializeWithUnzip(Of Agendum)(Data(1)))
                    Case "CreateBulletin" : Access.AddBulletin(Code.XmlDeserializeWithUnzip(Of Bulletin)(Data(1)))
                    Case "ChangeBulletin" : Access.ChangeBulletin(Code.XmlDeserializeWithUnzip(Of Bulletin)(Data(1)))
                    Case "DeleteBulletin" : Access.DeleteBulletin(Code.XmlDeserializeWithUnzip(Of Bulletin)(Data(1)))
                    Case "CreateLog" : Access.AddLog(Code.XmlDeserializeWithUnzip(Of Log)(Data(1)))
                    Case "DeleteLog" : Access.DeleteLog(Code.XmlDeserializeWithUnzip(Of Log)(Data(1)))
                    Case "DeleteAllLog" : Access.DeleteAllLog()
                    Case "DeleteFile" : Access.DeleteFile(Code.XmlDeserializeWithUnzip(Of String)(Data(1)))
                    Case "Login" : Access.LogIn(Code.XmlDeserializeWithUnzip(Of Access.LogInArgs)(Data(1)))
                    Case "Logout" : Access.LogOut()
                    Case "StockMoveIn" : Access.StockMoveIn(Code.XmlDeserializeWithUnzip(Of Access.StockMoveArgs)(Data(1)))
                    Case "StockMoveOut" : Access.StockMoveOut(Code.XmlDeserializeWithUnzip(Of Access.StockMoveArgs)(Data(1)))
                    Case "GetSalesInformation"
                        Dim args As Access.GetSalesInformationArgs = Code.XmlDeserializeWithUnzip(Of Access.GetSalesInformationArgs)(Data(1))
                        Client.Send("ResponseSalesInformationArgs", Code.XmlSerializeWithZIP(Access.GetSalesInformation(args.StartTime, args.EndTime)))
                    Case "GetServerVersion"
                        Client.Send("ServerVersion", Code.XmlSerializeWithZIP(Access.Version))
                    Case "IsServer" : Access.Mode = Database.Access.FarKind.Server
                    Case "IsClient" : Access.Mode = Database.Access.FarKind.Client
                    Case Else
                        Dim msg As String = "不支援的指令:" & Data(0)
                        'MsgBox("Server" & msg)
                        Access.OnErrorMessage("AccessServer.ServerReceiveSplitMessage:" & msg)
                        Client.Send("MsgBox", Code.XmlSerializeWithZIP(msg))
                End Select
            End Sub

            Private Sub ClientReceiveStreamRequest(ByVal Client As TCPTool.Client, ByVal sender As TCPTool.Client.StreamSender) Handles Client.ReceiveStreamRequest
                Select Case sender.Cmd
                    Case "DataTable"
                        Try
                            Dim args As ReadArgs = Code.XmlDeserializeWithUnzip(Of ReadArgs)(sender.Args)
                            Dim lstFile As String() = Array.ConvertAll(args.FileList, Function(f As String) Access.Dir & "\" & IO.Path.GetFileName(f))
                            Dim dt As DataTable = Access.Read(args.Table, lstFile, args.SqlCommand, Nothing)
                            Dim sm As New IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(Code.XmlSerializeWithZIP(dt)))
                            sender.stream = sm
                            dt.Dispose()
                        Catch
                            sender.Fail(Err.Description)
                        End Try
                    Case "GetErrorLogFiles"
                        sender.stream = New IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(Code.XmlSerializeWithZIP(Access.GetErrorLogFileNames())))
                    Case "GetDir"
                        sender.stream = New IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(Code.XmlSerializeWithZIP(Access.GetCloneBasePath())))
                    Case "GetSalesInformation"
                        Dim args As Access.GetSalesInformationArgs = Code.XmlDeserializeWithUnzip(Of Access.GetSalesInformationArgs)(sender.Args)
                        sender.stream = New IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(Code.XmlSerializeWithZIP(Access.GetSalesInformation(args.StartTime, args.EndTime))))

                    Case "AccessReader"
                        Dim args As ReadArgs = Code.XmlDeserializeWithUnzip(Of ReadArgs)(sender.Args)
                        Dim lstFile As String() = Array.ConvertAll(args.FileList, Function(f As String) Access.Dir & "\" & IO.Path.GetFileName(f))
                        Dim dt As DataTable = Access.Read(args.Table, lstFile, args.SqlCommand, Nothing)
                        Dim dtSender As New AccessSender(Client, sender.Guid, dt)
                        Client.lstTransmitter.Add(dtSender)
                        dtSender.StartSend()
                End Select
            End Sub

            Class AccessSender
                Inherits TCPTool.Client.StreamSender
                Public DataTable As DataTable
                Dim CursorRow As Integer = 0

                Sub New(ByVal client As Client, ByVal guid As String, ByVal DataTable As DataTable)
                    MyBase.New(client, guid)
                    Me.DataTable = DataTable
                End Sub

                Public Shadows Sub StartSend()
                    If DataTable Is Nothing Then
                        Fail("查無資料")
                        Exit Sub
                    End If

                    Dim dt As DataTable = DataTable.Clone
                    Send("DataTableStructure", DataTable.Rows.Count & "," & Code.XmlSerializeWithZIP(dt))


                    'For i As Integer = 0 To DataTable.Rows.Count - 1
                    '    'Send("Row", i & "," & Code.Zip(Join(Array.ConvertAll(DataTable.Rows(i).ItemArray, Function(s As Object) Strings.Trim(s)), ",")))
                    '    Send("Row", i & "," & Join(Array.ConvertAll(DataTable.Rows(i).ItemArray, AddressOf GetBase64), ":"))
                    'Next

                End Sub

                Private Function GetBase64(ByVal obj As Object) As String
                    Return Code.ToBase64(obj.ToString)
                End Function


                Private Sub SendRow(ByVal index As Integer)

                    Try
                        Send("Row", index & "," & Join(Array.ConvertAll(DataTable.Rows(index).ItemArray, AddressOf GetBase64), ":"))
                    Catch
                        Fail(Err.Description)
                    End Try


                    CursorRow += 1
                End Sub


                Public Overrides Sub Receive(ByVal cmd As String, ByVal msg() As String)
                    MyBase.Receive(cmd, msg)
                    Select Case cmd
                        Case "RequestRow"
                            Dim index As Integer = msg(0)
                            SendRow(index)
                        Case "Finish", "Cancel"
                            parent.Remove(Me)
                            DataTable.Dispose()
                    End Select


                End Sub

            End Class

            Private Sub ReaderRequest(ByVal client As TCPTool.Client, ByVal Guid As String, ByVal Cmd As String, ByVal ArgsSerialize As String)

                Dim ResultText As String = ""
                Select Case Cmd
                    '讀取資料表
                    Case "DataTable"
                        Dim args As ReadArgs = Code.XmlDeserializeWithUnzip(Of ReadArgs)(ArgsSerialize)
                        Dim lstFile As String() = Array.ConvertAll(args.FileList, Function(f As String) Access.Dir & "\" & IO.Path.GetFileName(f))
                        Dim dt As DataTable = Access.Read(args.Table, lstFile, args.SqlCommand, Nothing)
                        ResultText = Code.XmlSerializeWithZIP(dt)
                        If dt IsNot Nothing Then dt.Dispose()

                        'client.Send("ReadResponse", Guid & "," & Code.SerializeWithZIP(dt))
                    Case "GetErrorLogFiles"
                        ResultText = Code.XmlSerializeWithZIP(Access.GetErrorLogFileNames())
                    Case "GetDir"
                        ResultText = Code.XmlSerializeWithZIP(Access.GetCloneBasePath())
                End Select


                client.Send("ReaderResponse", Guid & "," & ResultText)
            End Sub

            Private ReadOnly Property NotLocal() As Boolean
                Get
                    Return LocalAccess IsNot Access
                End Get
            End Property


            Private Sub Access_Account_LogIn(ByVal sender As Object, ByVal result As LoginResult) Handles Access.Account_LogIn
                If Client IsNot Nothing Then Client.Send("Account_Login", Code.XmlSerializeWithZIP(result))
            End Sub

            Private Sub Access_Account_Logout(ByVal sender As Object, ByVal result As LoginResult) Handles Access.Account_Logout
                If Client IsNot Nothing Then Client.Send("Account_Logout", Code.XmlSerializeWithZIP(result))
            End Sub

            Private Sub Access_Messaged(ByVal sender As Object, ByVal e As Access.MsgArgs) Handles Access.Messaged
                If Client IsNot Nothing Then Client.Send("Messaged", Code.XmlSerializeWithZIP(e))
            End Sub

            Private Sub Access_ChangedAgendum(ByVal sender As Object, ByVal Agendum As Agendum) Handles Access.ChangedAgendum
                LocalAccess.OnChangedAgendum(Agendum)
            End Sub

            Private Sub Access_ChangedBulletin(ByVal sender As Object, ByVal bulletin As Bulletin) Handles Access.ChangedBulletin
                LocalAccess.OnChangedBulletin(bulletin)
            End Sub

            Private Sub Access_ChangedContract(ByVal sender As Object, ByVal con As Contract, ByVal source As Access.Source) Handles Access.ChangedContract
                LocalAccess.OnChangedContract(con, source)
            End Sub

            Private Sub Access_ChangedCustomer(ByVal sender As Object, ByVal cus As Customer, ByVal source As Access.Source) Handles Access.ChangedCustomer
                LocalAccess.OnChangedCustomer(cus, source)
            End Sub

            Private Sub Access_ChangedGoods(ByVal sender As Object, ByVal goods As Goods, ByVal source As Access.Source) Handles Access.ChangedGoods
                LocalAccess.OnChangedGoods(goods, source)
            End Sub

            Private Sub Access_ChangedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice, ByVal source As Access.Source) Handles Access.ChangedHistoryPrice
                LocalAccess.OnChangedHistoryPrice(hp, source)
            End Sub

            Private Sub Access_ChangedPersonnel(ByVal sender As Object, ByVal per As Personnel, ByVal source As Access.Source) Handles Access.ChangedPersonnel
                LocalAccess.OnChangedPersonnel(per, source)
            End Sub

            Private Sub Access_ChangedSales(ByVal sender As Object, ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal ReturnList() As ReturnGoods, ByVal SalesContracts() As SalesContract) Handles Access.ChangedSales
                LocalAccess.OnChangedSales(sales, GoodsList, OrderList, ReturnList, SalesContracts)
            End Sub

            Private Sub Access_ChangedStock(ByVal sender As Object, ByVal stock As Stock) Handles Access.ChangedStock
                LocalAccess.OnChangedStock(stock)
            End Sub

            Private Sub Access_ChangedStockMove(ByVal sender As Object, ByVal data As StockMove) Handles Access.ChangedStockMove
                LocalAccess.OnChangedStockMove(data)
            End Sub

            Private Sub Access_ChangedSupplier(ByVal sender As Object, ByVal sup As Supplier, ByVal source As Access.Source) Handles Access.ChangedSupplier
                LocalAccess.OnChangedSupplier(sup, source)
            End Sub

            Private Sub Access_CreatedAgendum(ByVal sender As Object, ByVal Agendum As Agendum) Handles Access.CreatedAgendum
                LocalAccess.OnCreatedAgendum(Agendum)
            End Sub

            Private Sub Access_CreatedBulletin(ByVal sender As Object, ByVal bulletin As Bulletin) Handles Access.CreatedBulletin
                LocalAccess.OnCreatedBulletin(bulletin)
            End Sub
            Private Sub Access_CreatedContract(ByVal sender As Object, ByVal con As Contract, ByVal source As Access.Source) Handles Access.CreatedContract
                LocalAccess.OnCreatedContract(con, source)
            End Sub

            Private Sub Access_CreatedCustomer(ByVal sender As Object, ByVal cus As Customer, ByVal source As Access.Source) Handles Access.CreatedCustomer
                LocalAccess.OnCreatedCustomer(cus, source)
            End Sub

            Private Sub Access_CreatedGoods(ByVal sender As Object, ByVal goods As Goods, ByVal source As Access.Source) Handles Access.CreatedGoods
                LocalAccess.OnCreatedGoods(goods, source)
            End Sub

            Private Sub Access_CreatedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice, ByVal source As Access.Source) Handles Access.CreatedHistoryPrice
                LocalAccess.OnCreatedHistoryPrice(hp, source)
            End Sub

            Private Sub Access_CreatedLog(ByVal sender As Object, ByVal log As Log) Handles Access.CreatedLog
                LocalAccess.OnCreatedLog(log)
            End Sub

            Private Sub Access_CreatedPersonnel(ByVal sender As Object, ByVal per As Personnel, ByVal source As Access.Source) Handles Access.CreatedPersonnel
                LocalAccess.OnCreatedPersonnel(per, source)
            End Sub

            Private Sub Access_CreatedSales(ByVal sender As Object, ByVal sales As Sales, ByVal GoodsList() As SalesGoods, ByVal OrderList() As OrderGoods, ByVal ReturnList() As ReturnGoods, ByVal SalesContracts() As SalesContract) Handles Access.CreatedSales
                LocalAccess.OnCreatedSales(sales, GoodsList, OrderList, ReturnList, SalesContracts)
            End Sub

            Private Sub Access_CreatedStock(ByVal sender As Object, ByVal stock As Stock) Handles Access.CreatedStock
                LocalAccess.OnCreatedStock(stock)
            End Sub

            Private Sub Access_CreatedStockMove(ByVal sender As Object, ByVal data As StockMove) Handles Access.CreatedStockMove
                LocalAccess.OnCreatedStockMove(data)
            End Sub

            Private Sub Access_CreatedSupplier(ByVal sender As Object, ByVal sup As Supplier, ByVal source As Access.Source) Handles Access.CreatedSupplier
                LocalAccess.OnCreatedSupplier(sup, source)
            End Sub

            Private Sub Access_DeletedAgendum(ByVal sender As Object, ByVal Agendum As Agendum) Handles Access.DeletedAgendum
                LocalAccess.OnDeletedAgendum(Agendum)
            End Sub

            Private Sub Access_DeletedAllLog(ByVal sender As Object) Handles Access.DeletedAllLog
                LocalAccess.OnDeletedAllLog()
            End Sub

            Private Sub Access_DeletedBulletin(ByVal sender As Object, ByVal bulletin As Bulletin) Handles Access.DeletedBulletin
                LocalAccess.OnDeletedBulletin(bulletin)
            End Sub
            Private Sub Access_DeletedContract(ByVal sender As Object, ByVal con As Contract, ByVal source As Access.Source) Handles Access.DeletedContract
                LocalAccess.OnDeletedContract(con, source)
            End Sub

            Private Sub Access_DeletedCustomer(ByVal sender As Object, ByVal cus As Customer, ByVal source As Access.Source) Handles Access.DeletedCustomer
                LocalAccess.OnDeletedCustomer(cus, source)
            End Sub

            Private Sub Access_DeletedGoods(ByVal sender As Object, ByVal goods As Goods, ByVal source As Access.Source) Handles Access.DeletedGoods
                LocalAccess.OnDeletedGoods(goods, source)
            End Sub

            Private Sub Access_DeletedHistoryPrice(ByVal sender As Object, ByVal hp As HistoryPrice, ByVal source As Access.Source) Handles Access.DeletedHistoryPrice
                LocalAccess.OnDeletedHistoryPrice(hp, source)
            End Sub

            Private Sub Access_DeletedHistoryPriceList(ByVal sender As Object, ByVal hp As HistoryPrice, ByVal source As Access.Source) Handles Access.DeletedHistoryPriceList
                LocalAccess.OnDeletedHistoryPriceList(hp, source)
            End Sub

            Private Sub Access_DeletedLog(ByVal sender As Object, ByVal log As Log) Handles Access.DeletedLog
                LocalAccess.OnDeletedLog(log)
            End Sub

            Private Sub Access_DeletedPersonnel(ByVal sender As Object, ByVal per As Personnel, ByVal source As Access.Source) Handles Access.DeletedPersonnel
                LocalAccess.OnDeletedPersonnel(per, source)
            End Sub

            Private Sub Access_DeletedSales(ByVal sender As Object, ByVal sales As Sales) Handles Access.DeletedSales
                LocalAccess.OnDeletedSales(sales)
            End Sub

            Private Sub Access_DeletedStock(ByVal sender As Object, ByVal stock As Stock) Handles Access.DeletedStock
                LocalAccess.OnDeletedStock(stock)
            End Sub

            Private Sub Access_DeletedStockMove(ByVal sender As Object, ByVal data As StockMove) Handles Access.DeletedStockMove
                LocalAccess.OnDeletedStockMove(data)
            End Sub

            Private Sub Access_DeletedSupplier(ByVal sender As Object, ByVal sup As Supplier, ByVal source As Access.Source) Handles Access.DeletedSupplier
                LocalAccess.OnDeletedSupplier(sup, source)
            End Sub



        End Class
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

