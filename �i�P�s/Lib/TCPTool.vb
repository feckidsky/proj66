Imports System.net.Sockets
Imports System.Net

Public Class TCPTool

    '訊息編碼
    Dim mEncode As Client.Encoding

    Dim mServer As TcpListener
    Dim ServerPort As Integer
    Dim mClient As TcpClient
    Dim UDP As UdpClient

    Public ClientbyClient As Client

    Dim ServerClientList As New List(Of Client)
    Dim ClientList As New List(Of Client)
    Dim CountClient As Integer = -1

    Dim ListenThread As System.Threading.Thread

    Public Shared OutputErrorFile As Boolean = False
    Public Shared ErrorFilePath As String = IO.Directory.GetCurrentDirectory & "\TCPError.txt"

    Event ReceiveConnected(ByVal Sender As TCPTool, ByVal Client As Client)
    Event RequestConnected(ByVal Sender As TCPTool, ByVal Client As Client)
    Event SetIPAddressFinish(ByVal Sender As TCPTool, ByVal Client As Client)
    '訊息傳送事件
    Event ServerReceive(ByVal Client As Client, ByVal IP As String, ByVal Port As Integer, ByVal Msg As String)
    Event ClientReceive(ByVal Client As Client, ByVal IP As String, ByVal Port As Integer, ByVal Msg As String)
    Event ServerReceiveSplitMessage(ByVal Client As Client, ByVal IP As String, ByVal Port As Integer, ByVal Data() As String)
    Event ClientReceiveSplitMessage(ByVal Client As Client, ByVal IP As String, ByVal Port As Integer, ByVal Data() As String)
    Event ServerConnectedFail(ByVal Client As Client)
    Event ClientConnectedFail(ByVal Client As Client)

    Event ServerReceiveStreamRequest(ByVal Client As Client, ByVal sender As Client.StreamSender)

    '檔案傳送事件
    Event SendFileRequest(ByVal Client As Client, ByVal IP As String, ByVal FileName As String, ByVal FileSize As Long, ByVal Message As String)
    Event SendProgressChanged(ByVal Client As Client, ByVal FilePath As String, ByVal FileName As String, ByVal FinishByte As Long, ByVal Percent As Integer, ByVal Message As String)
    Event ReceiveProgressChanged(ByVal Client As Client, ByVal FilePath As String, ByVal FileName As String, ByVal FinishByte As Long, ByVal Percent As Integer, ByVal Message As String)
    Event SendProgressFinished(ByVal Client As Client, ByVal FilePath As String, ByVal FileName As String, ByVal IsError As Boolean, ByVal Message As String)
    Event ReceiveProgressFinished(ByVal Client As Client, ByVal FilePath As String, ByVal FileName As String, ByVal IsError As Boolean, ByVal Message As String)


    '檔案傳輸訊息
    Private Structure ConfigFileTransfer
        Dim Client As Client
        Dim IP As String
        Dim Port As Integer
        Dim FilePath As String
        Dim FileName As String
        Dim FileSize As Long
        Dim Message As String
    End Structure

    Public Shared Sub OutputError(Optional ByVal Text As String = "")
        If Not OutputErrorFile Then Exit Sub
        Dim FileNumber As Integer = FreeFile()
        Dim Msg As String = Now & "    " & Text & "    " & Err.Description
        Try
            'SyncLock ErrorFilePath
            FileOpen(FileNumber, ErrorFilePath, OpenMode.Append)
            PrintLine(FileNumber, Msg)
            FileClose(FileNumber)
            'End SyncLock
        Catch
        End Try
    End Sub

    Public Sub New(Optional ByVal Encode As Client.Encoding = Client.Encoding.Unicode)
        mEncode = Encode
    End Sub



    Property Encode() As Client.Encoding
        Get
            Return mEncode
        End Get
        Set(ByVal value As Client.Encoding)
            mEncode = value
        End Set
    End Property

    Public Shared ReadOnly Property Config() As IPConfig
        Get
            Return IPConfig.LocalIPConfig
        End Get
    End Property

    ''取得主機板序號
    'Public Shared Function Get_Motherboard_SN() As String
    '    Dim mc As New System.Management.ManagementClass("Win32_BaseBoard") ' 註1
    '    mc.Scope.Options.EnablePrivileges = True ' 註2
    '    Dim sno As String = ""

    '    For Each mo As System.Management.ManagementObject In mc.GetInstances() ' 註3
    '        sno = mo("SerialNumber") ' 註4
    '    Next
    '    mc.Dispose() ' 註5
    '    Return sno
    'End Function

#Region "IPConfig - IP資訊結構"
    Public Structure IPConfig
        Dim IP As String
        Dim Mask As String
        Dim Gateway As String
        Dim Dns1 As String
        Dim Dns2 As String
        Dim CardIndex As Integer
        Dim MacAddress As String
        Public Sub New(ByVal IP As String, ByVal Mask As String, ByVal Gateway As String, ByVal Dns1 As String, ByVal Dns2 As String, ByVal CardIndex As Integer)
            Me.IP = IP : Me.Mask = Mask : Me.Gateway = Gateway : Me.Dns1 = Dns1 : Me.Dns2 = Dns2 : Me.CardIndex = CardIndex
        End Sub
        Public Shared Function LocalIPConfig(Optional ByVal CardIndex As Integer = 0) As IPConfig
            Dim Config As IPConfig = New IPConfig("", "", "", "", "", CardIndex)

            Config.CardIndex = CardIndex
            Try
                Config.IP = Bytes2IP(GetIPv4(Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(CardIndex).GetIPProperties.UnicastAddresses.ToArray).Address.GetAddressBytes) ' Dns.GetHostEntry(SystemInformation.ComputerName).AddressList(0).ToString() 'GetIPAddress(CardIndex) 
            Catch
                WriteErrorLog(Err.GetException)
                If CardIndex = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces.Length Then Return Config 'New IPConfig("", "", "", "", "", 0)
                Return LocalIPConfig(CardIndex + 1)
            End Try
            Try : Config.Mask = Bytes2IP(Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(CardIndex).GetIPProperties.UnicastAddresses(0).IPv4Mask.GetAddressBytes) : Catch : WriteErrorLog(Err.GetException) : End Try
            Try : Config.Gateway = Bytes2IP(Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(CardIndex).GetIPProperties.GatewayAddresses(0).Address.GetAddressBytes) : Catch : WriteErrorLog(Err.GetException) : End Try
            Try : Config.Dns1 = Bytes2IP(Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(CardIndex).GetIPProperties.DnsAddresses(0).GetAddressBytes) : Catch : WriteErrorLog(Err.GetException) : End Try
            Try : Config.Dns2 = Bytes2IP(Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(CardIndex).GetIPProperties.DnsAddresses(1).GetAddressBytes) : Catch : WriteErrorLog(Err.GetException) : End Try
            Try : Config.MacAddress = Bytes2IP(Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(CardIndex).GetPhysicalAddress().GetAddressBytes(), True, "-") : Catch : WriteErrorLog(Err.GetException) : End Try
            Return Config
        End Function
        Private Shared Function GetIPv4(ByVal IPAddressArray() As NetworkInformation.UnicastIPAddressInformation) As NetworkInformation.UnicastIPAddressInformation
            Return Array.Find(IPAddressArray, AddressOf IsIPv4)
        End Function

        Private Shared Function IsIPv4(ByVal ip As NetworkInformation.UnicastIPAddressInformation) As Boolean
            Return ip.IsDnsEligible And ip.Address.AddressFamily = AddressFamily.InterNetwork
        End Function


        Private Shared Sub WriteErrorLog(ByVal e As System.Exception)
            'ErrorLog.WriteErrorLog(e)
        End Sub

        Private Shared Function Bytes2IP(ByVal ArrayBytes As Byte(), Optional ByVal HexFormat As Boolean = False, Optional ByVal Delimiter As String = ".") As String
            Return Join(Array.ConvertAll(ArrayBytes, Function(Value As Byte) IIf(HexFormat, ToHex(Value), Value.ToString)), Delimiter)
        End Function

        Private Shared Function ToHex(ByVal Value As Byte) As String
            Dim strHex As String = Hex(Value)
            If strHex.Length = 1 Then strHex = "0" & strHex
            Return strHex
        End Function

        Private Shared Function GetIPAddress(ByVal CardIndex As Integer) As String
            Dim Addr() As String = Array.ConvertAll(NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(CardIndex).GetIPProperties.UnicastAddresses.ToArray, Function(A As NetworkInformation.UnicastIPAddressInformation) Bytes2IP(A.Address.GetAddressBytes))
            Return Array.Find(Addr, Function(A As String) A <> "127.0.0.1")
        End Function

        Public Overrides Function ToString() As String
            Return Code.XmlSerialize(Me)
        End Function

        Public Shared Function Parse(ByVal s As String) As IPConfig
            Return Code.XmlDeserialize(Of IPConfig)(s)
        End Function

    End Structure
#End Region
#Region "NetConfigArgs - 修改網路設定參數"
    Public Structure NetConfigArgs
        Dim Config As IPConfig
        Dim CallBackFunction As Action
        Sub New(ByVal Config As IPConfig, ByVal CallBackFunction As Action)
            Me.Config = Config : Me.CallBackFunction = CallBackFunction
        End Sub
        Public Overrides Function ToString() As String
            Return Config.ToString
        End Function

        Public Shared Function Parse(ByVal s As String) As NetConfigArgs
            Return New NetConfigArgs(IPConfig.Parse(s), Nothing) 'New NetConfigArgs(IPConfig.Parse(Split(s, "::")(1)), Nothing, Split(s, "::")(0))
        End Function
    End Structure
#End Region
#Region "變更網路設定"
    Public Shared Sub SetNetConfig(ByVal e As NetConfigArgs)
        Dim Name As String = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(e.Config.CardIndex).Name

        Dim Cmd As String = ""
        With e.Config
            Dim style As Microsoft.VisualBasic.AppWinStyle = AppWinStyle.Hide
            Cmd = "Netsh interface ip set address name=""" & Name & """ source=static addr=""" & .IP & """ mask=""" & .Mask & """ gateway=" & IIf(.Gateway = "" Or .Gateway = "0.0.0.0", "none", """" & .Gateway & """ gwmetric=auto")
            'WriteToBat("ip.bat", Cmd)
            Shell(Cmd, style, True, 60000)
            Cmd = "Netsh interface ip set dns name=""" & Name & """ source = static addr =""" & .Dns1 & """"
            'WriteToBat("DNS1.bat", Cmd)
            Shell(Cmd, style, True, 60000)
            Cmd = "netsh interface ip add dns name=""" & Name & """ addr =""" & .Dns2 & """ index=2"
            'WriteToBat("DNS2.bat", Cmd)
            Shell(Cmd, style, True, 60000)
        End With
        If e.CallBackFunction IsNot Nothing Then e.CallBackFunction()

    End Sub

    Private Shared Sub WriteToBat(ByVal File As String, ByVal Cmd As String)
        Dim Dir As String = My.Application.Info.DirectoryPath & "\bat\"
        Dim LogFile As String = Dir & File
        If Not IO.Directory.Exists(Dir) Then IO.Directory.CreateDirectory(Dir)
        My.Computer.FileSystem.WriteAllText(LogFile, Cmd, False)
    End Sub

    Public Shared Sub BeginSetNetConfig(ByVal e As NetConfigArgs)
        Dim SetNetConfigThread As New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf SetNetConfig))
        Try : SetNetConfigThread.Start(e) : Catch : End Try
    End Sub

    Public Shared Sub BeginAutoSetNetConfig(ByVal e As NetConfigArgs)
        Dim SetNetConfigThread As New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf AutoSetNetConfig))
        Try : SetNetConfigThread.Start(e) : Catch : End Try
    End Sub

    Public Shared Sub AutoSetNetConfig(ByVal e As NetConfigArgs)
        Dim Name As String = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(e.Config.CardIndex).Name
        Dim Cmd As String = ""
        Cmd = "netsh interface ip set address """ & Name & """ dhcp"
        Shell(Cmd, AppWinStyle.Hide, True, 60000)
        Cmd = "netsh interface ip set dns """ & Name & """ dhcp"
        Shell(Cmd, AppWinStyle.Hide, True, 60000)
        If e.CallBackFunction IsNot Nothing Then e.CallBackFunction()
    End Sub
#End Region

#Region "UDPClient"
    Public Class UDPClient
        Public Client As Net.Sockets.UdpClient
        Shadows Event Receive(ByVal sender As Object, ByVal e As UdpArgs)
        Event ReceiveByByte(ByVal sender As Object, ByVal Data() As Byte)
        Dim ReceiveThread As Threading.Thread
        Public Port As Integer = 3635

        Public Sub New(Optional ByVal Port As Integer = 3635)
            Me.Port = Port
        End Sub

        Public Sub Start()
            If Client IsNot Nothing Then [Stop]()
            Client = New Net.Sockets.UdpClient(Port)
            ReceiveThread = New Threading.Thread(AddressOf ReceiveLoop)
            ReceiveThread.IsBackground = True
            ReceiveThread.Start()
        End Sub

        Public Sub [Stop]()
            If ReceiveThread IsNot Nothing AndAlso ReceiveThread.IsAlive Then ReceiveThread.Abort()
            Client.Close()
            Client = Nothing
        End Sub

        Public Structure UdpArgs
            Dim IP As String
            Dim Port As Integer
            Dim Message As String
            Sub New(ByVal IP As String, ByVal Port As Integer, ByVal Msg As String)
                Me.IP = IP : Me.Port = Port : Me.Message = Msg
            End Sub
            Public Overrides Function ToString() As String
                Return Code.XmlSerialize(Me)
            End Function

            Public Shared Function Parse(ByVal Text As String) As UdpArgs
                Return Code.XmlDeserialize(Of UdpArgs)(Text)
            End Function
        End Structure


        Public Overloads Sub Send(ByVal Data As String, ByVal IP As String, Optional ByVal Port As Integer = 3635)
            Dim Address As IPConfig = IPConfig.LocalIPConfig
            Dim Args As New UdpArgs(Address.IP, Port, Data)
            Dim lstByte() As Byte = TCPTool.Client.GetSystemEncoding(TCPTool.Client.Encoding.Unicode).GetBytes(Args.ToString)
            If Client IsNot Nothing Then Client.Send(lstByte, lstByte.Length, New IPEndPoint(IPAddress.Parse(IP), Port))
        End Sub

        Private Sub ReceiveLoop()
            Dim Data As Byte()
            Dim Msg As String
            Dim Args As UdpArgs

            Do
                Try
                    Data = Client.Receive(New IPEndPoint(IPAddress.Any, 0))
                    Msg = TCPTool.Client.GetSystemEncoding(TCPTool.Client.Encoding.Unicode).GetString(Data)
                    RaiseEvent ReceiveByByte(Me, Data)

                    Args = UdpArgs.Parse(Msg)
                    RaiseEvent Receive(Me, Args)
                Catch ex As Exception

                End Try
            Loop
        End Sub

    End Class
#End Region

#Region "Server 動作函式"
    '===========================================================================================================
    '                                               Server動作函式
    '===========================================================================================================
    Public Sub ServerOpen(ByVal Port As Integer)
        ServerPort = Port
        Dim localAddr As IPAddress = GetIPv4() ' Dns.GetHostEntry(Dns.GetHostName).AddressList(0) ' Dns.Resolve(My.Computer.Name).AddressList(0)
        If mServer Is Nothing Then mServer = New TcpListener(localAddr, Port)
        If ListenThread Is Nothing Then ListenThread = New System.Threading.Thread(AddressOf Listen)
        ListenThread.IsBackground = True
        ListenThread.Start()
    End Sub

    Public Function GetIPv4() As IPAddress
        Return Array.Find(Dns.GetHostEntry(Dns.GetHostName).AddressList, Function(ip As IPAddress) ip.AddressFamily = AddressFamily.InterNetwork)
    End Function

    Public Sub ServerClose()

        Try : ListenThread.Abort() : Catch : End Try
        Try : mServer.Stop() : Catch : End Try
        ListenThread = Nothing
        mServer = Nothing

        ServerClientList.ForEach(AddressOf CloseClient)
        ServerClientList.Clear()
    End Sub

    Private Sub CloseClient(ByVal Client As TCPTool.Client)
        Try
            Client.Close()
        Catch
            OutputError("主動關閉 Client")
        End Try
    End Sub


    Private Sub Listen()

        'Dim ClientInfo As IPEndPoint
        'Dim IP As String
        Try
            mServer.Start()
        Catch
            OutputError("開新 Listen")
            MsgBox(Err.Description)
        End Try

        Threading.Thread.Sleep(3000)

        Do
            Dim GetClient As New TcpClient
            Try
                '監聽連線
                GetClient = mServer.AcceptTcpClient
                '取得連線資訊
                'ClientInfo = CType(GetClient.Client.RemoteEndPoint, IPEndPoint)
                'IP = ClientInfo.Address.ToString
            Catch
                OutputError("Server 監聽")
                'MsgBox(Err.Description & " Listen")
                Exit Sub
            End Try

            '建立Client元件
            Dim cClient As New Client(GetClient, mEncode)
            '事件委派指定
            AddHandler cClient.GetReceive, AddressOf SReceive
            AddHandler cClient.ConnectedFail, AddressOf SConnectedFail
            AddHandler cClient.ReceiveProgressChanged, AddressOf ReceiveProgressChanged_Trigger
            AddHandler cClient.ReceiveProgressFinished, AddressOf ReceiveProgressFinished_Trigger
            AddHandler cClient.SendFileRequest, AddressOf SendFileRequest_Trigger
            AddHandler cClient.SendProgressChanged, AddressOf SendProgressChanged_Trigger
            AddHandler cClient.SendProgressFinished, AddressOf SendProgressFinished_Trigger
            AddHandler cClient.SetIPAddressFinish, AddressOf OnServerSetIPAddressFinish
            AddHandler cClient.ReceiveStreamRequest, AddressOf OnServerReceiveStreamRequest
            '記錄元件
            ServerClientList.Add(cClient)

            '連線事件觸發
            RaiseEvent ReceiveConnected(Me, cClient)
        Loop
    End Sub

    Friend Sub OnServerReceiveStreamRequest(ByVal Client As Client, ByVal sender As Client.StreamSender)
        RaiseEvent ServerReceiveStreamRequest(Client, sender)
    End Sub

    Friend Sub OnServerSetIPAddressFinish(ByVal Client As Client)
        '取得連線資訊
        ServerClose()
        Threading.Thread.Sleep(10000)
        ServerOpen(ServerPort)
        RaiseEvent SetIPAddressFinish(Me, Client)
    End Sub

    Private Sub SReceive(ByVal ClientClass As TCPTool.Client, ByVal Client As TcpClient, ByVal IP As String, ByVal Port As Integer, ByVal Msg As String)
        RaiseEvent ServerReceiveSplitMessage(ClientClass, IP, Port, Split(Msg, ","))
        RaiseEvent ServerReceive(ClientClass, IP, Port, Msg)
    End Sub

    Private Sub SConnectedFail(ByVal Client As Client)
        RaiseEvent ServerConnectedFail(Client)
        ServerClientList.Remove(Client)
    End Sub

    Public Function ServerSend(ByVal newClient As Client, ByVal Msg As String) As Boolean ', Optional ByVal Level As Client.Level = Client.Level.Low) As Boolean
        Return newClient.Send(Msg) ', Level)
    End Function


    Public Sub ServerSend(ByVal Msg As String)

        Dim lstClient() As Client = ServerClientList.ToArray
        Dim i As Long
        For i = 0 To lstClient.Length - 1
            Try
                lstClient(i).Send(Msg)
            Catch
            End Try
        Next
    End Sub

    Public Sub ServerSend(ByVal CMD As String, ByVal Para As String)

        Dim lstClient() As Client = ServerClientList.ToArray
        Dim i As Long
        For i = 0 To lstClient.Length - 1
            Try
                lstClient(i).Send(CMD, Para)
            Catch
            End Try
        Next
    End Sub

#End Region

#Region "Client 動作函式"
    '===========================================================================================================
    '                                               Client動作函式
    '===========================================================================================================
    Public Function ClientOpen(ByVal IP As String, ByVal Port As Integer) As Boolean
        Dim Connected As Boolean

        If mClient Is Nothing Then
            Connected = False
        Else
            Connected = mClient.Connected
        End If


        If Not Connected Then
            Try

                mClient = New TcpClient(IP, Port)

                Dim cClient As New Client(mClient, mEncode)
                AddHandler cClient.GetReceive, AddressOf CReceive
                AddHandler cClient.ConnectedFail, AddressOf CConnectedFail
                AddHandler cClient.ReceiveProgressChanged, AddressOf ReceiveProgressChanged_Trigger
                AddHandler cClient.ReceiveProgressFinished, AddressOf ReceiveProgressFinished_Trigger
                AddHandler cClient.SendFileRequest, AddressOf SendFileRequest_Trigger
                AddHandler cClient.SendProgressChanged, AddressOf SendProgressChanged_Trigger
                AddHandler cClient.SendProgressFinished, AddressOf SendProgressFinished_Trigger
                OnRequestConnected(cClient)
                ClientList.Add(cClient)
                Return True
            Catch
                mClient = Nothing
                OutputError("主動開啟Client")
                Return False
            End Try
        End If
    End Function

    Public Function StartConnect(ByVal IP, ByVal Port) As Client
        Dim Client As Client = GetClient(IP, Port)
        Client.StartConnect()
        Return Client
    End Function

    Public Function GetClient(ByVal IP, ByVal Port) As Client
        Dim Client As New Client(IP, Port, mEncode)
        AddHandler Client.GetReceive, AddressOf CReceive
        AddHandler Client.ConnectedSuccess, AddressOf OnRequestConnected
        AddHandler Client.ConnectedFail, AddressOf CConnectedFail
        AddHandler Client.ReceiveProgressChanged, AddressOf ReceiveProgressChanged_Trigger
        AddHandler Client.ReceiveProgressFinished, AddressOf ReceiveProgressFinished_Trigger
        AddHandler Client.SendFileRequest, AddressOf SendFileRequest_Trigger
        AddHandler Client.SendProgressChanged, AddressOf SendProgressChanged_Trigger
        AddHandler Client.SendProgressFinished, AddressOf SendProgressFinished_Trigger

        ClientList.Add(Client)
        Return Client
    End Function

    Friend Sub OnRequestConnected(ByVal Client As Client)
        ClientbyClient = Client
        RaiseEvent RequestConnected(Me, Client)
    End Sub

    Public Sub ClientClose()
        ClientList.ForEach(AddressOf ClientClose)
    End Sub

    Public Sub ClientClose(ByVal Client As Client)
        Client.EndConnect()
    End Sub

    Public Function ClientSend(ByVal Message As String, ByVal IP As String, ByVal Port As Integer) As Boolean ', Optional ByVal Level As Client.Level = Client.Level.Low) As Boolean
        ClientOpen(IP, Port)
        Return ClientSend(Message) ', Level)
    End Function

    Public Function ClientSend(ByVal Message As String) As Boolean ', Optional ByVal Level As Client.Level = Client.Level.Low) As Boolean
        Try
            Return ClientbyClient.Send(Message) ', Level)
        Catch
            OutputError("Client發送訊息")
            Return False
        End Try

    End Function

    Private Sub CConnectedFail(ByVal Client As Client)
        RaiseEvent ClientConnectedFail(Client)
    End Sub
    Private Sub CReceive(ByVal ClientClass As TCPTool.Client, ByVal Client As TcpClient, ByVal IP As String, ByVal Port As Integer, ByVal Msg As String)
        RaiseEvent ClientReceiveSplitMessage(ClientClass, IP, Port, Split(Msg, ","))
        RaiseEvent ClientReceive(ClientClass, IP, Port, Msg)
    End Sub

#End Region

#Region "檔案傳送函數/事件"
    '===========================================================================================================
    '                                               檔案傳送函數/事件
    '===========================================================================================================
    '發送傳送檔案請求
    Public Sub SendFile(ByVal Client As Client, ByVal FilePath As String, Optional ByVal Message As String = "")
        Client.SendFile(FilePath, Message)
    End Sub


    '收到檔案傳輸請求
    Private Sub SendFileRequest_Trigger(ByVal Client As Client, ByVal IP As String, ByVal FileName As String, ByVal FileSize As Long, ByVal Message As String)
        RaiseEvent SendFileRequest(Client, IP, FileName, FileSize, Message)
    End Sub

    '接收檔案
    Public Sub SaveFile(ByVal Client As Client, ByVal FilePath As String, ByVal Message As String, Optional ByVal Port As Integer = 3637)
        Client.SaveFile(FilePath, Message, Port)
    End Sub

    '檔案接收進度
    Private Sub ReceiveProgressChanged_Trigger(ByVal Client As Client, ByVal FilePath As String, ByVal FileName As String, ByVal FinishByte As Long, ByVal Percent As Integer, ByVal Message As String)
        RaiseEvent ReceiveProgressChanged(Client, FilePath, FileName, FinishByte, Percent, Message)
    End Sub

    '檔案接收完成
    Private Sub ReceiveProgressFinished_Trigger(ByVal Client As Client, ByVal FilePath As String, ByVal FileName As String, ByVal IsError As Boolean, ByVal Message As String)
        RaiseEvent ReceiveProgressFinished(Client, FilePath, FileName, IsError, Message)
    End Sub

    '傳送檔案進度
    Private Sub SendProgressChanged_Trigger(ByVal Client As Client, ByVal FilePath As String, ByVal FileName As String, ByVal FinishByte As Long, ByVal Percent As Integer, ByVal Message As String)
        RaiseEvent SendProgressChanged(Client, FilePath, FileName, FinishByte, Percent, Message)
    End Sub

    '傳送檔案完成
    Private Sub SendProgressFinished_Trigger(ByVal Client As Client, ByVal FilePath As String, ByVal FileName As String, ByVal IsError As Boolean, ByVal Message As String)
        RaiseEvent SendProgressFinished(Client, FilePath, FileName, IsError, Message)
    End Sub

#End Region

#Region "Client - 客戶端管理元件"
    '===========================================================================================================
    '                                               客戶端管理元件
    '===========================================================================================================
    Public Class Client
        Public State As Mode = Mode.Run
        Public IP As String
        Public Port As Integer
        Public Tag As Object
        Public TcpClient As TcpClient
        Public Time As Date
        Dim ReceiveThread As System.Threading.Thread
        Public IPAddress As IPConfig

        Private SendMessage As String = ""
        Private TempSendMessage As String = ""
        Private TempHighSendMessage As String = ""

        Private SendLock As String = "SendMessageLock"


        Private Sending As Boolean = False


        Private Delegate Sub CheckNumberHandler(ByVal Number As Long, ByRef AllCheckNumberEvent As CheckNumberHandler)
        Private CheckNumberEvent As CheckNumberHandler

        Dim FileWriter As FileTransfer
        Dim FileSender As FileTransfer
        Dim FileWriterThread As System.Threading.Thread
        Dim FileSenderThread As System.Threading.Thread

        Private WithEvents TrendDataRead As New System.ComponentModel.BackgroundWorker

        '檔案傳送變數
        Dim CFG_ReciveFile As ConfigFileTransfer
        Dim CFG_SendFile As ConfigFileTransfer

        '檔案傳送事件
        Event SendFileRequest(ByVal Client As Client, ByVal IP As String, ByVal FileName As String, ByVal FileSize As Long, ByVal Message As String)
        Event SendProgressChanged(ByVal Client As Client, ByVal FilePath As String, ByVal FileName As String, ByVal FinishByte As Long, ByVal Percent As Integer, ByVal Message As String)
        Event SendProgressFinished(ByVal Client As Client, ByVal FilePath As String, ByVal FileName As String, ByVal IsError As Boolean, ByVal Message As String)
        Event ReceiveProgressChanged(ByVal Client As Client, ByVal FilePath As String, ByVal FileName As String, ByVal FinishByte As Long, ByVal Percent As Integer, ByVal Message As String)
        Event ReceiveProgressFinished(ByVal Client As Client, ByVal FilePath As String, ByVal FileName As String, ByVal IsError As Boolean, ByVal Message As String)


        Event ReceiveIPAddress(ByVal Client As Client, ByVal IPAddress As IPConfig)
        Event SetIPAddressFinish(ByVal Client As Client)
        Event ConnectedFail(ByVal Client As Client)
        Event ConnectedSuccess(ByVal Client As Client)
        Event ReceiveSplitMessage(ByVal Client As Client, ByVal IP As String, ByVal Port As Integer, ByVal Data() As String)
        Event ReceiveStreamRequest(ByVal Client As Client, ByVal sender As StreamSender)


        Event GetReceive(ByVal ClientClass As Client, ByVal Client As TcpClient, ByVal IP As String, ByVal Port As Integer, ByVal Msg As String)

        Public MyEncoding As System.Text.Encoding

        'Public Structure ReceiveStreamRequestArgs
        '    Dim cmd As String
        '    Dim args As String
        '    Dim stream As IO.Stream
        'End Structure

        Public Enum Encoding
            AscII = 0
            Unicode = 1
            UTF32 = 2
            UTF7 = 3
            UTF8 = 4
        End Enum

        Public Enum Level
            Low = 0
            High = 1
        End Enum

        Public Enum Mode
            Run = 0
            Quit = 1
        End Enum

        Public Sub New(ByVal IP As String, ByVal Port As Integer, Optional ByVal Encode As Encoding = Encoding.Unicode)
            Me.IP = IP
            Me.Port = Port
            MyEncoding = GetSystemEncoding(Encode)
        End Sub

        Public Sub New(ByVal Client As TcpClient, Optional ByVal Encode As Encoding = Encoding.Unicode) 'Encoding.Default)
            TcpClient = Client
            Dim ClientInfo As IPEndPoint = CType(TcpClient.Client.RemoteEndPoint, IPEndPoint)
            IP = ClientInfo.Address.ToString
            Port = ClientInfo.Port
            MyEncoding = GetSystemEncoding(Encode)
            ReceiveThread = New System.Threading.Thread(AddressOf Receive)
            ReceiveThread.IsBackground = True
            Try
                ReceiveThread.Start()
            Catch
            End Try
        End Sub

        Public Sub New()
            Me.IP = "127.0.0.1"
            Me.Port = 3636
            MyEncoding = GetSystemEncoding(Encoding.Unicode)
        End Sub

        Public Structure Address
            Dim IP As String
            Dim Port As Integer
            Public Sub New(ByVal IP As String, ByVal Port As Integer)
                Me.IP = IP : Me.Port = Port
            End Sub
        End Structure

        Public Sub Save(ByVal Path As String)
            Code.SaveXml(New Address(IP, Port), Path)
        End Sub

        Public Function Load(ByVal Path As String, ByVal DefaultAddress As Address) As Address
            Dim LoadAddress As Address = DefaultAddress
            LoadAddress = Code.LoadXml(Path, DefaultAddress)
            Me.IP = LoadAddress.IP
            Me.Port = LoadAddress.Port
            Return LoadAddress
        End Function

#Region "自動連線"
        Private ConnectThread As Threading.Thread
        Public AutoConnect As Boolean = False

        Public Sub StartConnect()
            AutoConnect = True
            BeginConnect()
        End Sub

        Public Sub EndConnect()
            AutoConnect = False
            Try : TcpClient.Close() : Catch : End Try
            Try : ConnectThread.Abort() : Catch : End Try
            Try : ReceiveThread.Abort() : Catch : End Try
        End Sub

        Friend Sub OnConnectedFail()
            If AutoConnect Then BeginConnect()
            RaiseEvent ConnectedFail(Me)
        End Sub

        Friend Sub OnConnectedSuccess()
            RaiseEvent ConnectedSuccess(Me)
            GetIPAddress()
        End Sub

        Public Sub BeginConnect()
            'If ConnectThread IsNot Nothing AndAlso ConnectThread.IsAlive Then Exit Sub
            ConnectThread = New Threading.Thread(AddressOf Connect)
            ConnectThread.IsBackground = True
            'Try
            ConnectThread.Start()
            'Catch
            '    OnConnectedFail()
            'End Try
        End Sub

        Public Sub Connect()
            Try
                TcpClient = New TcpClient(IP, Port)
                ReceiveThread = New System.Threading.Thread(AddressOf Receive)
                ReceiveThread.IsBackground = True
                Try
                    ReceiveThread.Start()
                Catch
                End Try

            Catch
                OnConnectedFail()
                Exit Sub
            End Try

            OnConnectedSuccess()
        End Sub
#End Region

        Public Sub Close()
            If ReceiveThread IsNot Nothing AndAlso ReceiveThread.IsAlive Then
                ReceiveThread.Abort()
                ReceiveThread = Nothing

                TcpClient.Close()
            End If
        End Sub

        Public Function Connected() As Boolean
            Return TcpClient IsNot Nothing AndAlso TcpClient.Connected
        End Function

        Public Shared Function GetSystemEncoding(ByVal Encode As Encoding) As System.Text.Encoding
            Select Case Encode
                Case Encoding.AscII : Return System.Text.Encoding.ASCII
                Case Encoding.Unicode : Return System.Text.Encoding.Unicode
                Case Encoding.UTF32 : Return System.Text.Encoding.UTF32
                Case Encoding.UTF7 : Return System.Text.Encoding.UTF7
                Case Encoding.UTF8 : Return System.Text.Encoding.UTF8
                Case Else : Return System.Text.Encoding.Default  'Return GetEncoding(DefaultEncode)
            End Select
        End Function

        Public Shared Function GetEncoding(ByVal Encoding As System.Text.Encoding) As Encoding
            Select Case Encoding.ToString
                Case System.Text.Encoding.ASCII.ToString : Return Client.Encoding.AscII
                Case System.Text.Encoding.Unicode.ToString : Return Client.Encoding.Unicode
                Case System.Text.Encoding.UTF32.ToString : Return Client.Encoding.UTF32
                Case System.Text.Encoding.UTF7.ToString : Return Client.Encoding.UTF7
                Case System.Text.Encoding.UTF8.ToString : Return Client.Encoding.UTF8
            End Select
        End Function

#Region "傳送資料"
        Public Function Send(ByVal CMD As String, ByVal Para As String)
            Return Send(CMD & "," & Para)
        End Function



        Public Function Send(ByVal Msg As String) As Boolean ', Optional ByVal SendLevel As Level = Level.Low) As Boolean
            Dim SendLevel As Level = Level.Low
            Dim newMsg As String = "[/" & Msg & "/]"
            Dim Success As Boolean = True

            SyncLock SendLock
                If SendMessage = "" Then
                    SendMessage = newMsg
                    Success = mSend(SendMessage)
                Else
                    Select Case SendLevel
                        Case Level.Low : TempSendMessage = TempSendMessage.Insert(TempSendMessage.Length, newMsg)
                        Case Level.High : TempHighSendMessage = TempHighSendMessage.Insert(TempHighSendMessage.Length, newMsg)
                    End Select
                End If

            End SyncLock


            Try
                Success = Success AndAlso TcpClient.Connected
            Catch
                Success = False
                TCPTool.OutputError("傳送訊息: " & Msg)
            End Try

            Return Success

        End Function

        Private Function mSend(ByVal Text As String) As Boolean
            Dim Cmd() As Byte = MyEncoding.GetBytes(Text) ' System.Text.Encoding.Unicode.GetBytes(Text)
            Dim Success As Boolean = True
            Try
                TcpClient.GetStream.BeginWrite(Cmd, 0, Cmd.Length, AddressOf mSended, Me)
            Catch
                TempHighSendMessage = ""
                TempSendMessage = ""
                SendMessage = ""
                Success = False
                TCPTool.OutputError("傳送訊息: " & Text)
            End Try
            Return Success
        End Function

        Private Sub mSended(ByVal Result As System.IAsyncResult)
            SyncLock SendLock
                If TempHighSendMessage <> "" Then
                    SendMessage = TempHighSendMessage
                    TempHighSendMessage = ""
                    mSend(SendMessage)
                ElseIf TempSendMessage <> "" Then
                    SendMessage = TempSendMessage
                    TempSendMessage = ""
                    mSend(SendMessage)
                Else
                    SendMessage = ""
                End If

            End SyncLock

        End Sub
#End Region

#Region "接收資料"
        Public Sub Receive()
            Dim tmpIP As String = ""
            Dim Msg As String = ""
            Dim GetString As String = ""
            Dim Bytes(1023) As Byte
            Dim tmpBytes As Byte = Nothing
            Dim tmpData As String


            Dim locSt As Long = 0
            Dim locEnd As Long = 0
            Dim ExistElseMsg As Boolean = False
            Dim locCurrent As Long = 1
            Dim i As Integer

            Dim Stream As NetworkStream = TcpClient.GetStream()
            Dim cntByte As Int32

            Dim LastCheckConnectStateTime As Date = Now
            Try
                Do
                    If (Now - LastCheckConnectStateTime).Seconds > 5 Then
                        Send("%CheckConnectState%")
                        LastCheckConnectStateTime = Now
                    End If

                    If Stream.DataAvailable Then
                        cntByte = Stream.Read(Bytes, 0, Bytes.Length)
                        tmpData = MyEncoding.GetString(Bytes, 0, cntByte) 'System.Text.Encoding.Unicode.GetString(Bytes, 0, cntByte)
                        GetString &= tmpData
                        LastCheckConnectStateTime = Now
                    End If

                    Do
                        ExistElseMsg = False

                        '取得字串始位置
                        If GetString.Length > 2 And locSt = 0 Then
                            For i = 1 To GetString.Length
                                If Mid(GetString, i, 2) = "[/" Then
                                    locSt = i + 2
                                    Exit For
                                End If
                            Next
                        End If

                        '取得字串結束位置
                        If GetString.Length > 4 And locEnd = 0 Then
                            For i = locSt To GetString.Length
                                If Mid(GetString, i, 2) = "/]" Then
                                    If locEnd = 0 Then
                                        locEnd = i - 1
                                    Else
                                        ExistElseMsg = True
                                        Exit For
                                    End If
                                End If
                            Next
                        End If

                        '取出字串並回傳
                        If locSt > 0 And locEnd > 0 Then
                            Msg = Mid(GetString, locSt, locEnd - locSt + 1)
                            tmpIP = CType(TcpClient.Client.RemoteEndPoint, IPEndPoint).Address.ToString

                            '取出剩餘字串
                            If GetString.Length >= (Msg.Length + 4) Then
                                GetString = Strings.Right(GetString, GetString.Length - locEnd - 2)
                            End If

                            '觸發接收事件
                            Dim ReceiveThread As New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf ReceiveEvent))
                            Try
                                ReceiveThread.Start(Msg)
                            Catch
                            End Try
                            locEnd = 0
                            locSt = 0
                        End If

                        If Not TcpClient.Connected Then Exit Do
                    Loop While ExistElseMsg


                    Threading.Thread.Sleep(10)
                    If Not TcpClient.Connected Then Exit Do
                Loop
            Catch
                TCPTool.OutputError(System.Threading.Thread.CurrentThread.Name & " 處理接收訊息: " & GetString)
            End Try

            Try
                TcpClient.Close()
            Catch
                TCPTool.OutputError("Server關閉錯誤連線")
            End Try
            OnConnectedFail()
        End Sub


        '接收事件觸發
        Private Sub ReceiveEvent(ByVal Msg As Object)
            Dim mMsg As String = Msg.ToString


            Dim MsgPart() As String = Split(mMsg, ",")
            Dim CMD As String = MsgPart(0)

            Dim IsMyCommand As Boolean = Strings.Left(CMD, 1) = "%" And Strings.Right(CMD, 1) = "%"


            If IsMyCommand Then

                Select Case CMD
                    Case "%ReciveFile%" '上傳檔案請求成功
                        Dim SendPort As Integer = MsgPart(1)
                        Dim Message As String = MsgPart(2)
                        StartSendFile(IP, SendPort, CFG_SendFile.FilePath, Message)

                    Case "%ReciveFileFail%" '上傳檔案請求失敗
                        If FileSenderThread.IsAlive Then
                            FileSender.StopControl = True
                        End If

                        Dim FileName As String = MsgPart(1)
                        Dim FilePath As String = MsgPart(2)
                        Dim Message As String = MsgPart(3)
                        RaiseEvent SendProgressFinished(Me, FilePath, FileName, True, Message)

                    Case "%SendFile%"   '收到傳案傳輸請求
                        Dim FileName As String = MsgPart(1)
                        Dim FilePath As String = MsgPart(2)
                        Dim FileSize As Long = Val(MsgPart(3))
                        Dim Message As String = MsgPart(4)
                        With CFG_ReciveFile 'Command,FileName,FileSize
                            .Client = Me
                            .FileName = FileName
                            .FilePath = FilePath
                            .FileSize = FileSize
                            .IP = IP
                        End With

                        RaiseEvent SendFileRequest(Me, IP, FileName, FileSize, Message)
                    Case "%RequestSetTime%"
                        SetSystemTime(Date.ParseExact(MsgPart(1), "yyyyMMddHHmmss", Nothing))
                    Case "%RequestGetSystemTime%"
                        Send("%ReceiveSystemTime%," & Now.ToString("yyyyMMddHHmmss"))
                    Case "%ReceiveSystemTime%"
                        Try : Me.Time = Date.ParseExact(MsgPart(1), "yyyyMMddHHmmss", Nothing) : Catch : End Try

                    Case "%RequestIPAddress%"
                        Send("%ReceiveIPAddress%," & Config.ToString)
                    Case "%ReceiveIPAddress%"
                        IPAddress = IPConfig.Parse(MsgPart(1))
                        RaiseEvent ReceiveIPAddress(Me, IPAddress)
                    Case "%RequestSetIPAddress%"
                        Dim e As NetConfigArgs = NetConfigArgs.Parse(MsgPart(1))
                        e.CallBackFunction = Function() Send("%ReceiveSetIPAddress%")
                        SetNetConfig(NetConfigArgs.Parse(MsgPart(1)))
                        RaiseEvent SetIPAddressFinish(Me)
                    Case "%ReceiveSetIPAddress%"

                    Case "%FileTransmitter%"
                        lstTransmitter.Receive(Me, MsgPart)
                End Select

                Exit Sub
            End If

            RaiseEvent ReceiveSplitMessage(Me, IP, Port, MsgPart)
            RaiseEvent GetReceive(Me, TcpClient, IP, Port, mMsg)
        End Sub


        Public Sub GetClientTime()
            Send("%RequestGetSystemTime%")
        End Sub

        Public Shared Sub SetSystemTime(ByVal Time As Date)
            Dim command1 As String = "cmd /c time " & Time.ToString("HH:mm:ss")
            Dim command2 As String = "cmd /c date " & Time.ToShortDateString   '.ToString("yyyy/MM/dd")
            Shell(command1)
            Shell(command2)
        End Sub

        Public Sub SetClientSystemTime()
            SetClientSystemTime(Now)
        End Sub

        Public Sub SetClientSystemTime(ByVal Time As Date)
            Send("%RequestSetTime%," & Time.ToString("yyyyMMddHHmmss"))
        End Sub

        Public Sub GetIPAddress()
            Send("%RequestIPAddress%")
        End Sub

        Public Sub SetIPAddress(ByVal e As NetConfigArgs)
            Send("%RequestSetIPAddress%," & e.ToString)
        End Sub

        Public Function Download(ByVal sourceFile As String, ByVal destFile As String) As StreamReceiver


            Try
                Dim fs As IO.FileStream = New IO.FileStream(destFile, IO.FileMode.Create)
                Dim Receiver As New StreamReceiver(Me, StreamTransmitter.GetGuid(), fs)
                lstTransmitter.Add(Receiver)
                Receiver.Request("Download", sourceFile)
                Return Receiver
            Catch ex As Exception
                Return Nothing
            End Try

            'Receiver.RequestData() ' .StartDownload(sourceFile, destFile)
        End Function

        Public Function GetReceiver() As StreamReceiver  'ByVal cmd As String, ByVal args As String) As StreamReceiver
            Dim ms As New IO.MemoryStream
            Dim Receiver As New StreamReceiver(Me, StreamTransmitter.GetGuid, ms)
            lstTransmitter.Add(Receiver)
            'Receiver.Request(cmd, args)
            Return Receiver
        End Function

        Friend Class TransmitterList
            Inherits List(Of StreamTransmitter)
            Dim ReadLock As String = "ReadLock"
            Dim ElseRequestHandler As Action(Of StreamSender)
            Sub New(ByVal ElseRequest As Action(Of StreamSender))
                ElseRequestHandler = ElseRequest
            End Sub


            Public Overloads Sub Add(ByVal Item As StreamTransmitter)
                SyncLock ReadLock
                    MyBase.Add(Item)
                    Item.parent = Me
                End SyncLock
            End Sub

            Public Overloads Sub Remove(ByVal item As StreamTransmitter)
                SyncLock ReadLock
                    MyBase.Remove(item)
                End SyncLock
            End Sub

            Public Sub Receive(ByVal Client As Client, ByVal para() As String)
                Dim Guid As String = para(1)
                Dim Transmitters As List(Of StreamTransmitter)

                SyncLock ReadLock
                    Transmitters = FindAll(Function(i As StreamTransmitter) i.Guid = Guid)
                End SyncLock
                For Each r As StreamTransmitter In Transmitters
                    r.Receive(para)
                Next


                If Transmitters Is Nothing OrElse Transmitters.Count = 0 Then
                    Select Case para(2)
                        Case "Download"
                            Dim Sender As New StreamSender(Client, Guid)
                            Try
                                Dim fs As New IO.FileStream(para(3), IO.FileMode.Open, IO.FileAccess.Read)
                                Add(Sender)
                                Sender.stream = fs
                                Sender.StartSend()
                            Catch
                                Sender.Fail(Err.Description)
                            End Try

                        Case Else
                            Dim Sender As New StreamSender(Client, Guid)
                            Add(Sender)
                            Sender.Cmd = para(2)
                            Sender.Args = para(3)
                            ElseRequestHandler(Sender)
                            Sender.StartSend()
                            'Dim receiver As New Receiver(Client, Guid)
                            'receiver.TotalSize = para(3)
                            'Add(receiver)
                            'receiver.RequestData()
                    End Select

                End If

            End Sub
        End Class

        Public Sub OnReceiveStreamRequest(ByVal e As StreamSender)
            RaiseEvent ReceiveStreamRequest(Me, e)
        End Sub

        Friend lstTransmitter As New TransmitterList(AddressOf OnReceiveStreamRequest)

#End Region

        Public MustInherit Class StreamTransmitter
            Friend parent As TransmitterList
            Public Guid As String
            Public WithEvents Client As Client
            'Friend fs As IO.FileStream
            Friend stream As IO.Stream
            Event TransFail(ByVal sender As Object, ByVal Message As String)

            Public Shared Function GetGuid() As String
                Return Convert.ToBase64String(System.Guid.NewGuid.ToByteArray)
            End Function

            Public Sub Send(ByVal cmd As String, ByVal Args As String)
                Client.Send("%FileTransmitter%," & Guid & "," & cmd, Args)
            End Sub

            Friend Overridable Sub OnTransFail(ByVal Message As String)
                RaiseEvent TransFail(Me, Message)
            End Sub


            Public MustOverride Sub Receive(ByVal msg() As String)

            Private Sub Client_ConnectedFail(ByVal Client As Client) Handles Client.ConnectedFail

                OnTransFail("連線中斷!")
                Try
                    stream.Close()
                    stream.Dispose()
                Catch
                End Try

                If parent IsNot Nothing Then parent.Remove(Me)
            End Sub
        End Class


        Public Class StreamSender
            Inherits StreamTransmitter
            Dim ReadToEnd As Boolean = False
            Dim BufferSize As Integer = 32767
            Public Cmd As String
            Public Args As String

            Sub New(ByVal Client As Client, ByVal Guid As String)
                Me.Client = Client
                Me.Guid = Guid
                'Me.stream = stream
            End Sub

            'Public Sub WriteString(ByVal text As String)
            '    stream = New IO.MemoryStream()
            '    Dim sw As New IO.StreamWriter(stream)
            '    sw.Write(text)
            '    sw.Dispose()
            'End Sub

            Public Sub StartSend()
                'Try
                '    Me.stream = stream ' New IO.FileStream(Path, IO.FileMode.Open, IO.FileAccess.Read)
                'Catch
                '    OnTransFail(Err.Description)
                '    Send("Fail", Err.Description)
                '    If parent IsNot Nothing Then parent.Remove(Me)
                'End Try
                ReadToEnd = False
                Try
                    Send("StartSendFile", stream.Length)
                Catch
                    Fail(Err.Description)
                End Try
            End Sub

            Private Sub Upload()
                If ReadToEnd Then
                    stream.Close()
                    Send("EndSendFile", "")
                    If parent IsNot Nothing Then parent.Remove(Me)
                    Exit Sub
                End If

                Dim data(BufferSize - 1) As Byte

                Try
                    If stream.Length - stream.Position < BufferSize Then  '如果剩下位傳送的位元組不到BufferSize
                        System.Array.Resize(data, stream.Length - stream.Position)
                    End If
                Catch

                End Try

                Try
                    ReadToEnd = stream.Length - stream.Position <= BufferSize
                Catch
                    ReadToEnd = True
                End Try

                Try
                    stream.Read(data, 0, data.Length)
                Catch
                    OnTransFail(Err.Description)
                    Fail(Err.Description)
                    If parent IsNot Nothing Then parent.Remove(Me)
                End Try


                Dim zipByte As Byte() = Code.Zip(data)
                Dim base64 As String = Convert.ToBase64String(zipByte)
                Send("SendFileData", base64)


                'Try
                '    ReadToEnd = stream.Length - stream.Position <= BufferSize
                'Catch
                '    ReadToEnd = True
                'End Try

            End Sub

            Private Sub ReceiveCancel()
                Try
                    stream.Close()
                    stream.Dispose()
                Catch
                    Fail(Err.Description)

                End Try
                If parent IsNot Nothing Then parent.Remove(Me)
            End Sub

            Public Sub Fail(ByVal msg As String)
                Send("Fail", msg)
            End Sub

            Public Overrides Sub Receive(ByVal msg() As String)
                Dim cmd As String = msg(2)
                'Dim args As String = msg(3)

                Select Case cmd
                    Case "RequestData"
                        Upload()
                    Case "Cancel"
                        ReceiveCancel()
                End Select
            End Sub


        End Class

        Public Class StreamReceiver
            Inherits StreamTransmitter
            Public TotalSize As Long
            Public destFile As String
            Public sourceFile As String

            Event Progress(ByVal sender As Object, ByVal percent As Integer)
            Event Received(ByVal sender As Object, ByVal stream As IO.Stream)


            Sub New(ByVal Client As Client, ByVal Guid As String, Optional ByVal stream As IO.Stream = Nothing)
                Me.Client = Client
                Me.Guid = Guid 'Convert.ToBase64String(System.Guid.NewGuid.ToByteArray)
                If stream Is Nothing Then stream = New IO.MemoryStream
                Me.stream = stream
            End Sub

            Public Sub Request(ByVal cmd As String, ByVal Args As String)
                Send(cmd, Args)
            End Sub
            'Public Sub StartDownload(ByVal sourceFile As String, ByVal destFile As String)
            '    Me.destFile = destFile
            '    Me.sourceFile = sourceFile
            '    Send("StartDownload", sourceFile)
            'End Sub

            Private Sub StartReceiveFile(ByVal size As String)
                TotalSize = size
                'Try
                '    stream = New IO.FileStream(destFile, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None)

                'Catch
                '    OnTransFail(Err.Description)
                '    Send("Cancel", "")
                '    If parent IsNot Nothing Then parent.Remove(Me)
                'End Try
                RequestData()
            End Sub

            Private Sub WriteFile(ByVal base64 As String)
                Dim zipByte() As Byte = Convert.FromBase64String(base64)
                Dim data() As Byte = Code.Unzip(zipByte)
                Try
                    stream.Write(data, 0, data.Length)
                Catch
                    OnTransFail(Err.Description)
                    Cancel()
                    If parent IsNot Nothing Then parent.Remove(Me)
                End Try

                Dim percent As Integer
                If TotalSize = 0 Then
                    percent = 0
                Else
                    percent = stream.Position / TotalSize * 100
                End If


                Try
                    RaiseEvent Progress(Me, percent)
                Catch

                End Try
                RequestData()
            End Sub

            Public Sub RequestData()
                Send("RequestData", "")
            End Sub

            Private Sub CloseFile()
                OnReceived()
                Try
                    stream.Close()
                    stream.Dispose()
                Catch

                End Try
                If parent IsNot Nothing Then parent.Remove(Me)
            End Sub

            Public Sub Cancel()
                Send("Cancel", "")
                Fail("使用者已取消動作")
            End Sub

            Friend Sub OnReceived()
                RaiseEvent Received(Me, stream)
            End Sub

            Private Sub Fail(ByVal Message As String)
                Try
                    stream.Close()
                    stream.Dispose()
                Catch
                Finally
                    If parent IsNot Nothing Then parent.Remove(Me)
                End Try
                OnTransFail(Message)
            End Sub


            Public Overrides Sub Receive(ByVal msg() As String)
                Dim cmd As String = msg(2)
                Dim args As String = msg(3)
                Select Case cmd
                    Case "StartSendFile"
                        StartReceiveFile(args)
                    Case "SendFileData"
                        WriteFile(args)
                    Case "EndSendFile"
                        CloseFile()
                    Case "SendFail"
                        Fail(args)
                End Select

            End Sub

           
        End Class

#Region "檔案傳送"
        '發送傳送檔案請求
        Public Sub SendFile(ByVal FilePath As String, Optional ByVal Message As String = "")

            Dim FileSize As Long = My.Computer.FileSystem.GetFileInfo(FilePath).Length
            Dim FileName As String = Mid(FilePath, FilePath.LastIndexOf("\") + 2)

            '記錄等待起始時間
            Dim WaitStartTime As Date = Now

            '檔案傳送中...  等候10秒
            If Not (FileSenderThread Is Nothing) Then
                Do While FileWriterThread.IsAlive
                    If (Now - WaitStartTime).TotalSeconds > 10 Then '逾時-觸發失敗事件
                        RaiseEvent SendProgressFinished(Me, FilePath, FileName, True, Message)
                        Exit Sub
                    End If
                    Threading.Thread.Sleep(50)
                    'Application.DoEvents()
                Loop

            End If

            'Command,FileName,FileSize
            With CFG_SendFile
                .FilePath = FilePath
                .FileName = FileName
                .FileSize = FileSize
                .Message = Message
            End With
            Send("%SendFile%," & FileName & "," & FilePath & "," & FileSize.ToString & "," & Message) ', Client.Level.High)

        End Sub


        '開始傳送檔案
        Private Sub StartSendFile(ByVal IP As String, ByVal Port As Integer, ByVal FilePath As String, ByVal Message As String)
            FileSender = New FileTransfer(IP, Port, FilePath, Message)

            Dim FileSenderThread As New System.Threading.Thread(AddressOf FileSender.SendFile)
            AddHandler FileSender.ProgressChanged, AddressOf FileSendProgressChanged
            AddHandler FileSender.ProgressFinished, AddressOf FileSendProgressFinished
            FileSenderThread.Priority = Threading.ThreadPriority.Lowest
            Try
                FileSenderThread.Start()
            Catch
            End Try
        End Sub

        '更新檔案傳送進度-事件
        Private Sub FileSendProgressChanged(ByVal Sender As FileTransfer, ByVal FilePath As String, ByVal FileName As String, ByVal FinishByte As Long, ByVal Percent As Integer, ByVal Message As String)
            RaiseEvent SendProgressChanged(Me, FilePath, FileName, FinishByte, Percent, Message)
        End Sub

        '檔案傳送結束-事件
        Private Sub FileSendProgressFinished(ByVal Sender As FileTransfer, ByVal FilePath As String, ByVal FileName As String, ByVal IsError As Boolean, ByVal Message As String)
            RaiseEvent SendProgressFinished(Me, FilePath, FileName, IsError, Message)
        End Sub

        '接收檔案
        Public Sub SaveFile(ByVal FilePath As String, ByVal Message As String, Optional ByVal Port As Integer = 3637)

            With CFG_ReciveFile

                If FileWriterThread IsNot Nothing Then
                    Dim SaveFileStartTime As Date = Now
                    Do While FileWriterThread.IsAlive
                        If (Now - SaveFileStartTime).TotalSeconds > 10 Then
                            RaiseEvent ReceiveProgressFinished(.Client, FilePath, FilePath, IsError:=True, Message:=Message)
                            'RaiseEvent ReceiveProgressFinished(FilePath, FilePath, True)
                            Send("%ReciveFileFail%," & .FileName & "," & .FilePath & "," & Message) ', Client.Level.High)
                            Exit Sub
                        End If
                        Threading.Thread.Sleep(50)
                        'Application.DoEvents()
                    Loop
                End If

                FileWriter = New FileTransfer(.IP, Port, FilePath, .FileSize, Message)
                FileWriterThread = New System.Threading.Thread(AddressOf FileWriter.GetFile)
                AddHandler FileWriter.ProgressChanged, AddressOf FileReciveProgressChanged
                AddHandler FileWriter.ProgressFinished, AddressOf FileReciveProgressFinished
                FileWriterThread.Priority = Threading.ThreadPriority.Lowest
                Try
                    FileWriterThread.Start()
                Catch
                End Try
                Send("%ReciveFile%," & Port & "," & Message) ', Client.Level.High)
            End With
        End Sub

        '更新檔案接收進度
        Private Sub FileReciveProgressChanged(ByVal Sender As FileTransfer, ByVal FilePath As String, ByVal FileName As String, ByVal FinishByte As Long, ByVal Percent As Integer, ByVal Message As String)
            RaiseEvent ReceiveProgressChanged(Me, FilePath, FileName, FinishByte, Percent, Message)
        End Sub

        '檔案接收完畢
        Private Sub FileReciveProgressFinished(ByVal Sender As FileTransfer, ByVal FilePath As String, ByVal FileName As String, ByVal IsError As Boolean, ByVal Message As String)
            RaiseEvent ReceiveProgressFinished(Me, FilePath, FileName, IsError, Message)
        End Sub
#End Region

        Public Sub Dispose()
            EndConnect()
            Try
                TcpClient.Close()
            Catch
            End Try
        End Sub

        Public Shared Sub StartConnect(ByVal Client As Client)
            Client.StartConnect()
        End Sub

        Public Shared Sub EndConnect(ByVal Client As Client)
            Client.EndConnect()
        End Sub

        Public Shared Sub Dispose(ByVal Client As Client)
            Client.Dispose()
        End Sub
     

    End Class


#End Region

#Region "FileTransFer - 檔案傳輸元件"
    '===========================================================================================================
    '                                               檔案傳送元件
    '===========================================================================================================
    Friend Class FileTransfer

        Public StopControl As Boolean = False

        Dim ListenSock As Socket

        Private FileReader As IO.FileStream
        Private FileWriter As IO.FileStream
        Private Sock As Socket
        Private IP As String
        Private Port As Integer

        Private FilePath As String
        Private FileName As String
        Private FileSize As Long
        Private BufferSize As Long = 32767
        Private Data(BufferSize - 1) As Byte
        Private ReadSize As UInteger = 0
        Private LastProgress As Integer = 0
        Private Message As String
        ' 當下載進度改變時呼叫
        Public Event ProgressChanged(ByVal Sender As FileTransfer, ByVal FilePath As String, ByVal FileName As String, ByVal FinishByte As Long, ByVal Percent As Integer, ByVal Message As String)
        ' 下載完成時呼叫
        Public Event ProgressFinished(ByVal Sender As FileTransfer, ByVal FilePath As String, ByVal FileName As String, ByVal IsError As Boolean, ByVal Message As String)

        Public Sub New(ByVal IP As String, ByVal Port As Integer, ByVal FilePath As String, ByVal Message As String)
            Me.FilePath = FilePath
            Me.Port = Port
            Me.IP = IP
            FileName = Mid(FilePath, FilePath.LastIndexOf("\") + 2)
            StopControl = False
            Me.Message = Message
        End Sub
        Public Sub New(ByVal IP As String, ByVal Port As Integer, ByVal FilePath As String, ByVal FileSize As Long, ByVal Message As String)
            Me.FilePath = FilePath
            Me.Port = Port
            Me.IP = IP
            Me.FileSize = FileSize
            FileName = Mid(FilePath, FilePath.LastIndexOf("\") + 2)
            StopControl = False
            Me.Message = Message
        End Sub

        Public Sub GetFile()

            Dim IPEn As IPHostEntry = Dns.GetHostEntry(Dns.GetHostName)
            Dim EndPoint = New IPEndPoint(IPAddress.Any, Port)

            If ListenSock Is Nothing Then
                ListenSock = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            Else
                Try
                    ListenSock.Close()
                    ListenSock = Nothing
                    ListenSock = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                Catch
                    'MsgBox(Err.Description)
                    onProgressFinished(FilePath, FileName, True)
                    Exit Sub
                End Try
            End If

            Dim BindError As Boolean = False
            Dim StartBindTime As Date = Now

            Do
                Try
                    ListenSock.Bind(EndPoint)
                Catch
                    BindError = True
                    Threading.Thread.Sleep(100)
                End Try

                If (Now - StartBindTime).TotalSeconds > 10 Then
                    onProgressFinished(FilePath, FileName, True)
                    Exit Sub
                End If

            Loop While BindError


            Try
                ListenSock.Listen(1)
            Catch
                onProgressFinished(FilePath, FileName, True)
                Exit Sub
            End Try

            '檢查檔案是否存在
            If System.IO.File.Exists(FilePath) Then
                Try
                    System.IO.File.Delete(FilePath)
                Catch ex As Exception
                    OutputError("檔案" & FilePath & " 使用中無法刪除!!")
                    onProgressFinished(FilePath, FileName, True)
                    Exit Sub
                End Try

            End If

            Dim DPath As String = IO.Path.GetDirectoryName(FilePath)
            If Not IO.Directory.Exists(DPath) Then IO.Directory.CreateDirectory(DPath)
            FileWriter = New IO.FileStream(FilePath, IO.FileMode.Append)

            Sock = ListenSock.Accept
            Sock.SendBufferSize = BufferSize
            ReadSize = 0

            Dim NStream As New NetworkStream(Sock)

            Do While Not NStream.DataAvailable
                System.Threading.Thread.Sleep(10)
            Loop
            Sock.BeginReceive(Data, 0, Data.Length, SocketFlags.None, AddressOf Recived, Me)
        End Sub

        '我方接收檔案串流
        Private Sub Recived(ByVal result As IAsyncResult)
            '讀取
            Dim Len As Integer = 0
            Try
                Len = Sock.EndReceive(result)
            Catch : End Try
            If Len = 0 Then
                Sock.Shutdown(SocketShutdown.Both)
                Sock.Close()
                If FileWriter IsNot Nothing Then
                    '下載終了
                    FileWriter.Close()
                    FileWriter.Dispose()
                End If
                onProgressFinished(FilePath, FileName, False)
                Exit Sub
            End If
            If FileWriter Is Nothing Then
                FileWriter = New IO.FileStream(FilePath, IO.FileMode.Append)
            End If
            '修改進度!
            ReadSize += Len
            onProgressChanged()
            FileWriter.Write(Data, 0, Len)
            Try
                Sock.BeginReceive(Data, 0, Data.Length, SocketFlags.None, AddressOf Recived, Me)
            Catch
                FileWriter.Close()
                FileWriter.Dispose()
                onProgressFinished(FilePath, FileName, Not ReadSize = Len)
            End Try
        End Sub

        Public Sub SendFile()
            Sock = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            Try
                Sock.Connect(IP, Port)
            Catch
                onProgressFinished(FilePath, FileName, True)
                Exit Sub
            End Try
            Sock.SendBufferSize = BufferSize
            FileSize = My.Computer.FileSystem.GetFileInfo(FilePath).Length


            Try
                FileReader = New IO.FileStream(FilePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                If FileReader.Length < BufferSize Then
                    System.Array.Resize(Data, FileReader.Length)
                End If
                FileReader.BeginRead(Data, 0, Data.Length, AddressOf FileReaded, Me)
            Catch
                onProgressFinished(FilePath, FileName, True)
            End Try

        End Sub

        '讀取串流的End偵聽函式
        Private Sub FileReaded(ByVal result As IAsyncResult)
            If StopControl Then onProgressFinished(FilePath, FileName, True, False)
            FileReader.EndRead(result) '結束非同步讀取Stream
            Try
                Sock.BeginSend(Data, 0, Data.Length, SocketFlags.None, AddressOf StreamSended, Me) '開始傳送資料
            Catch
                FileReader.Close()
                FileReader.Dispose()
                onProgressFinished(FilePath, FileName, True)
            End Try
        End Sub


        '傳送串流的End偵聽函式
        Private Sub StreamSended(ByVal result As IAsyncResult)

            Try
                Sock.EndSend(result)   '結束非同步傳送Stream
            Catch
                '在讀到檔案盡頭前發生錯誤
                If FileReader.Length > FileReader.Position Then onProgressFinished(FilePath, FileName, True)
            End Try


            Dim ReadToEnd As Boolean
            Dim tmpStartTime As Date = Now
            Dim tmpRetryCount As Integer = 3
            Dim WaitTimeMS As Integer = 3000
            Dim CurrentTryCount As Integer = 0

            Do
                Try
                    ReadToEnd = FileReader.Length <= FileReader.Position
                    Exit Do
                Catch ex As Exception
                    CurrentTryCount += 1
                    If CurrentTryCount >= tmpRetryCount Then
                        Sock.Close()
                        FileReader.Close()
                        FileReader.Dispose()
                        onProgressFinished(FilePath, FileName, True)
                        Exit Sub
                    End If
                    Threading.Thread.Sleep(WaitTimeMS)
                End Try
            Loop


            If ReadToEnd Then '如果讀到盡頭
                Sock.Close()
                FileReader.Close()
                FileReader.Dispose()
                onProgressFinished(FilePath, FileName, False)
                Exit Sub
            End If

            If FileReader.Length - FileReader.Position < BufferSize Then  '如果剩下位傳送的位元組不到BufferSize
                System.Array.Resize(Data, FileReader.Length - FileReader.Position)
            End If
            ReadSize += Data.Length '增加進度
            onProgressChanged()
            FileReader.BeginRead(Data, 0, Data.Length, AddressOf FileReaded, Me)
        End Sub

        '進度改變檢測
        Private Sub onProgressChanged()
            If Int(Progress) - LastProgress > 0 Then
                RaiseEvent ProgressChanged(Me, FilePath, FileName, ReadSize, Progress, Message)
            End If
            LastProgress = Int(Progress)
        End Sub

        Private Sub mFinished(ByVal FilePath As String, ByVal FileName As String, ByVal IsError As Boolean, ByVal SendEvent As Boolean)
            Try
                ListenSock.Close()
            Catch
            End Try
            Try
                Sock.Close()
            Catch
            End Try

            Try
                FileReader.Close()
                FileReader.Dispose()
            Catch ex As Exception

            End Try
            Try
                FileWriter.Close()
                FileWriter.Dispose()
            Catch ex As Exception
            End Try

            Threading.Thread.Sleep(1000)

            If SendEvent Then
                RaiseEvent ProgressChanged(Me, FilePath, FileName, ReadSize, 100, Message)
                RaiseEvent ProgressFinished(Me, FilePath, FileName, IsError, Message)
            End If
        End Sub

        '結束下載
        Private Sub onProgressFinished(ByVal FilePath As String, ByVal FileName As String, ByVal IsError As Boolean, Optional ByVal SendEvent As Boolean = True)
            mFinished(FilePath, FileName, IsError, SendEvent)
            'FinishedInvoker(FilePath, FileName, IsError)
        End Sub

        '屬性
        Public ReadOnly Property Progress() As Single
            Get
                If FileSize > 0 Then
                    Return (ReadSize / FileSize) * 100
                Else
                    Return 0
                End If
            End Get
        End Property

        Private Function GetFileSize(ByVal newFile As String) As Long
            Return My.Computer.FileSystem.GetFileInfo(newFile).Length
        End Function

    End Class
#End Region

End Class