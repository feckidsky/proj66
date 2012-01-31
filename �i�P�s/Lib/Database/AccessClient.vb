Namespace Database
    Public Structure ReadArgs
        Dim Table As String
        Dim FileList() As String
        Dim SqlCommand() As String
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

        Public Overrides Function Command(ByVal SqlCommand As String, ByVal File As String) As Long
            Dim Count As Long
            MsgBox("客戶端不支援直Command函數")
            Return Count
        End Function

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
                Case "MsgBox" : MsgBox(Code.DeserializeWithUnzip(Of String)(args))
                Case Else
                    MsgBox("不明指令:" & vbCrLf & Data(0))
            End Select
        End Sub
    End Class


End Namespace

