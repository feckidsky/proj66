Imports 進銷存.Database.StructureBase

Public Module Program
#Region "Structure"
    Structure LoginResult
        Dim State As LoginState
        Dim msg As String

        Sub New(ByVal state As LoginState, ByVal msg As String)
            Me.State = state : Me.msg = msg
        End Sub
    End Structure

    Enum LoginState
        IdError = 0
        PasswordError = 1
        Success = 2
    End Enum

    Structure SystemOptional
        Dim OrderBackcolor As Integer
        Dim SalesBackColor As Integer

        Shared ReadOnly Property DefaultConfig()
            Get
                Dim def As SystemOptional
                def.OrderBackcolor = Color.LightPink.ToArgb
                def.SalesBackColor = Color.LightGreen.ToArgb
                Return def
            End Get
        End Property

    End Structure

    'Structure ClientInfo
    '    Dim Name As String
    '    Dim IP As String
    '    Dim Port As Integer
    '    Sub New(ByVal Name As String, ByVal IP As String, ByVal Port As Integer)
    '        Me.Name = Name : Me.IP = IP : Me.Port = Port
    '    End Sub
    'End Structure
#End Region

    Public DB As New Database.Access
    Public Server As New Database.AccessServer
    Public WithEvents Client As New Database.AccessClientMenage()


    Public Config As SystemOptional
    Public ConfigPath As String = My.Application.Info.DirectoryPath & "\Config.xml"
    Public ClientPath As String = My.Application.Info.DirectoryPath & "\Client.xml"

    Public CurrentUser As Database.Personnel = Database.Personnel.Guest

    Event Account_Logout(ByVal personnel As Database.Personnel, ByVal Message As String)
    Event Account_LogIn(ByVal personnel As Database.Personnel, ByVal Message As String)
    Public SystemTitle As String = "進銷存管理系統"

    Public Sub InitialProgram()
        Server.Access = DB
        Server.Open()


        'UpdateDatabase()
        'Database.Access.RepairAccess(Database.Access.BasePath)
        ConfigLoad()

        Client.Load(ClientPath)
        Client.StartConnect()



        LogOut(False)
        Try
            LogIn("kidsky1", "3883", False)
        Catch
        End Try
        'Dim admin As Personnel = DB.GetPersonnelByID("Administrator")
        'LogIn(admin.ID, admin.Password, False)
    End Sub

    Public Sub UpdateDatabase()
        Dim d As OleDb.OleDbConnection = Database.Access.ConnectBase(DB.BasePath)
        Database.Access.DeleteTable("Mobile", d)
        Database.Access.CreateTable(Contract.Table, Contract.ToColumns, d)
        Database.Access.CreateTable(SalesContract.Table, SalesContract.ToColumns, d)
        Database.Access.CreateTable(OrderGoods.Table, OrderGoods.ToColumns, d)
        Database.Access.CreateTable(HistoryPrice.Table, HistoryPrice.ToColumns(), d)
        DB.DeleteColumn(Stock.Table, "Price")
        DB.AddColumn(Supplier.Table, "Modify", Database.DBTypeDate)
        DB.AddColumn(Customer.Table, "Modify", Database.DBTypeDate)
        DB.AddColumn(Personnel.Table, "Modify", Database.DBTypeDate)
        DB.AddColumn(Goods.Table, "Modify", Database.DBTypeDate)
        DB.AddColumn(Contract.Table, "Modify", Database.DBTypeDate)
    End Sub

    Public Sub ConfigLoad()
        Config = Code.Load(Of SystemOptional)(ConfigPath, SystemOptional.DefaultConfig)
    End Sub

    Public Sub ConfigSave()
        Code.Save(Config, ConfigPath)
    End Sub


    Public Function CheckAuthority(ByVal level As Integer, Optional ByVal WithAdmin As Boolean = False) As Boolean
        If Not WithAdmin And CurrentUser.IsAdministrator Then
            MsgBox(CurrentUser.Name & "無法進行此動作", MsgBoxStyle.Exclamation)
            Return False
        End If

        If CurrentUser.Authority >= level Then
            Return True
        Else
            MsgBox(CurrentUser.Name & " - 權限等級[" & CurrentUser.Authority & "]不足, 執行此動作的權限等級為[" & level & "]", MsgBoxStyle.Exclamation)
            Return False
        End If
    End Function

    Public Function LogIn(ByVal ID As String, ByVal Password As String, Optional ByVal TriggerEvent As Boolean = True) As LoginResult

        Dim result As LoginResult
        Dim user As Personnel = DB.GetPersonnelByID(ID)
        If user.IsNull() Then
            result = New LoginResult(LoginState.IdError, "帳號不存在!")
        ElseIf user.Password <> Password Then
            result = New LoginResult(LoginState.PasswordError, "密碼錯誤!")
        Else
            result = New LoginResult(LoginState.Success, "登入成功!")
            CurrentUser = user
        End If

        If TriggerEvent Then RaiseEvent Account_LogIn(user, result.msg)
        Return result
    End Function

    Public Sub LogOut(Optional ByVal TriggerEvent As Boolean = True)
        CurrentUser = Database.Personnel.Guest
        If TriggerEvent Then RaiseEvent Account_Logout(CurrentUser, "已經登出!")
    End Sub

    Public Function GetNewSupplier() As Supplier
        Dim data As Supplier = Nothing
        data.Label = "SU" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function GetNewPersonnel() As Personnel
        Dim data As Personnel = Nothing
        data.Label = "P" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function GetNewCustomer() As Customer
        Dim data As Customer = Nothing
        data.Label = "C" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function GetNewGoods() As Goods
        Dim data As Goods = Nothing
        data.Label = "G" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function GetNewMobile() As Contract
        Dim data As Contract = Nothing
        data.Label = "M" & Now.ToString("yyMMddHHmmss")
        Return data
    End Function

    Public Function GetNewStock() As Stock
        Dim data As Stock = Nothing
        data.Label = "ST" & Now.ToString("yyMMddHHmmss")
        data.Date = Now
        data.Number = 1
        Return data
    End Function

    Public Function GetNewSales() As Sales
        Dim data As Sales = Nothing
        data.Label = "SA" & Now.ToString("yyMMddHHmmss")
        data.OrderDate = Now
        Return data
    End Function

    Public Function GetNewOrder() As Order
        Dim data As Order = Nothing
        data.Label = "O" & Now.ToString("yyMMddHHmmss")
        data.Date = Now
        Return data
    End Function

    Public Function GetNewContract() As Contract
        Dim data As New Contract
        data.Label = "C" & Now.ToString("yyMMddHHmmss")
        data.Enable = True
        Return data
    End Function

    Public Function ToColor(ByVal argb As Integer) As Color
        Return Color.FromArgb(argb)
    End Function


#Region "Code - 序列化 / 壓縮"
    Public Class Code
        Public Shared Function Zip(ByVal Text As String, Optional ByVal ZipCount As Int16 = 1) As String
            If Text = "" Then Return ""
            If ZipCount > 0 Then
                Dim Data As Byte() = System.Text.Encoding.UTF8.GetBytes(Text)
                Return Convert.ToBase64String(Zip(Data, ZipCount))
            Else
                Return Text
            End If
        End Function

        Public Shared Function Unzip(ByVal Text As String, Optional ByVal ZipCount As Int16 = 1) As String
            If Text = "" Then Return ""
            If ZipCount > 0 Then
                Dim Data As Byte() = Convert.FromBase64String(Text)
                Return System.Text.Encoding.UTF8.GetString(Unzip(Data, ZipCount))
            Else
                Return Text
            End If
        End Function


        Public Shared Function Zip(ByVal Data() As Byte, Optional ByVal Count As Int16 = 1) As Byte()
            If Count = 0 Then Return Data
            Dim ms As IO.MemoryStream = New IO.MemoryStream()
            Dim compressedzipStream As IO.Compression.GZipStream = New IO.Compression.GZipStream(ms, IO.Compression.CompressionMode.Compress, True)
            compressedzipStream.Write(Data, 0, Data.Length)
            compressedzipStream.Close()
            compressedzipStream.Dispose()
            Return Zip(ms.ToArray, Count - 1)
            ms.Dispose()
        End Function

        Public Shared Function Unzip(ByVal Data() As Byte, Optional ByVal Count As Int16 = 1) As Byte()
            If Count = 0 Then Return Data
            Dim ms As IO.MemoryStream = New IO.MemoryStream(Data)
            Dim compressedzipStream As IO.Compression.GZipStream = New IO.Compression.GZipStream(ms, IO.Compression.CompressionMode.Decompress, True)

            Dim buff(4095) As Byte
            Dim read As Long = compressedzipStream.Read(buff, 0, buff.Length)
            Dim output As New IO.MemoryStream()
            output.Write(buff, 0, read)
            Do While (read > 0)
                read = compressedzipStream.Read(buff, 0, buff.Length)
                output.Write(buff, 0, read)
            Loop

            ms.Dispose()
            compressedzipStream.Close()
            compressedzipStream.Dispose()
            Return Unzip(output.ToArray, Count - 1)
            output.Dispose()
        End Function

        Public Shared Function SerializeWithZIP(ByVal obj As Object) As String
            Return Zip(Serialize(obj))
        End Function

        Public Shared Function DeserializeWithUnzip(Of T)(ByVal ZipText As String, ByVal Type As Type) As T
            Return Deserialize(Of T)(Unzip(ZipText))
        End Function

        Public Shared Function DeserializeWithUnzip(Of T)(ByVal ZipText As String) As T
            Return Deserialize(Of T)(Unzip(ZipText))
        End Function

        Public Shared Function Serialize(ByVal Obj As Object) As String
            Dim ser As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(Obj.GetType)
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim writer As IO.StringWriter = New IO.StringWriter(sb)
            ser.Serialize(writer, Obj)
            Return sb.ToString()
            writer.Dispose()
        End Function

        Public Shared Function Deserialize(Of T)(ByVal Text As String) As T
            ''將取得的內容進行反序列化
            Dim mySerializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(T)) 'GetType(SerializeData))
            Dim reader As New IO.StringReader(Text)
            Return mySerializer.Deserialize(reader)
            reader.Dispose()
        End Function

        Public Enum ZipMode
            Normal = 0
            ZIP = 1
        End Enum

        Public Shared Sub Save(Of T)(ByVal Data As T, ByVal FilePath As String, Optional ByVal Mode As ZipMode = ZipMode.Normal)
            Dim Dir As String = IO.Path.GetDirectoryName(FilePath)
            If Not IO.Directory.Exists(Dir) Then IO.Directory.CreateDirectory(Dir)
            Dim Text As String = Code.Serialize(Data)
            If Mode = ZipMode.ZIP Then Text = Code.Zip(Text)
            My.Computer.FileSystem.WriteAllText(FilePath, Text, False, System.Text.Encoding.Unicode)
        End Sub

        Public Shared Function Load(Of T)(ByVal FilePath As String, ByVal DefaultData As T, Optional ByVal Mode As ZipMode = ZipMode.Normal) As T
            If IO.File.Exists(FilePath) Then
                Dim Text As String = My.Computer.FileSystem.ReadAllText(FilePath, System.Text.Encoding.Unicode)
                If Mode = ZipMode.ZIP Then Text = Code.Unzip(Text)
                Return Code.Deserialize(Of T)(Text)
            Else
                Return DefaultData
            End If
        End Function

    End Class
#End Region

    WithEvents ccc As Database.AccessClient
    Private Sub Client_BeforeConnect(ByVal sender As Object) Handles Client.BeforeConnect
        Dim Client() As Database.AccessClient = CType(sender, Database.AccessClientMenage).Client
        For i = 0 To Client.Length - 1
            With Client(i)
                AddHandler .CreatedContract, AddressOf CreatedContract
                AddHandler .CreatedCustomer, AddressOf CreatedCustomer
                AddHandler .CreatedSupplier, AddressOf CreatedSupplier
                AddHandler .CreatedPersonnel, AddressOf CreatedPersonnel
                AddHandler .CreatedGoods, AddressOf CreatedGoods
                AddHandler .CreatedHistoryPrice, AddressOf CreatedHistoryPrice

                AddHandler .ChangedContract, AddressOf ChangedContract
                AddHandler .ChangedCustomer, AddressOf ChangedCustomer
                AddHandler .ChangedSupplier, AddressOf ChangedSupplier
                AddHandler .ChangedPersonnel, AddressOf ChangedPersonnel
                AddHandler .ChangedGoods, AddressOf ChangedGoods
                AddHandler .ChangedHistoryPrice, AddressOf ChangedHistoryPrice

                AddHandler .DeletedContract, AddressOf DeletedContract
                AddHandler .DeletedCustomer, AddressOf DeletedCustomer
                AddHandler .DeletedSupplier, AddressOf DeletedSupplier
                AddHandler .DeletedPersonnel, AddressOf DeletedPersonnel
                AddHandler .DeletedGoods, AddressOf DeletedGoods
                AddHandler .DeletedHistoryPrice, AddressOf DeletedHistoryPrice
                AddHandler .DeletedHistoryPriceList, AddressOf DeletedHistoryPriceList
                AddHandler .ConnectSuccess, AddressOf ConnectSuccess
            End With
        Next
    End Sub

    Private Sub Client_BeforeDisconnect(ByVal sender As Object) Handles Client.BeforeDisconnect
        Dim Client() As Database.AccessClient = CType(sender, Database.AccessClientMenage).Client
        For i = 0 To Client.Length - 1
            With Client(i)
                RemoveHandler .CreatedContract, AddressOf CreatedContract
                RemoveHandler .CreatedCustomer, AddressOf CreatedCustomer
                RemoveHandler .CreatedSupplier, AddressOf CreatedSupplier
                RemoveHandler .CreatedPersonnel, AddressOf CreatedPersonnel
                RemoveHandler .CreatedGoods, AddressOf CreatedGoods
                RemoveHandler .CreatedHistoryPrice, AddressOf CreatedHistoryPrice

                RemoveHandler .ChangedContract, AddressOf ChangedContract
                RemoveHandler .ChangedCustomer, AddressOf ChangedCustomer
                RemoveHandler .ChangedSupplier, AddressOf ChangedSupplier
                RemoveHandler .ChangedPersonnel, AddressOf ChangedPersonnel
                RemoveHandler .ChangedGoods, AddressOf ChangedGoods
                RemoveHandler .ChangedHistoryPrice, AddressOf ChangedHistoryPrice

                RemoveHandler .DeletedContract, AddressOf DeletedContract
                RemoveHandler .DeletedCustomer, AddressOf DeletedCustomer
                RemoveHandler .DeletedSupplier, AddressOf DeletedSupplier
                RemoveHandler .DeletedPersonnel, AddressOf DeletedPersonnel
                RemoveHandler .DeletedGoods, AddressOf DeletedGoods
                RemoveHandler .DeletedHistoryPrice, AddressOf DeletedHistoryPrice
                RemoveHandler .DeletedHistoryPriceList, AddressOf DeletedHistoryPriceList
                RemoveHandler .ConnectSuccess, AddressOf ConnectSuccess
            End With
        Next
    End Sub

    Dim UpdateDataLock As String = "UpdateDataLock"

    Private Sub ConnectSuccess(ByVal sender As Object) Handles ccc.ConnectSuccess
        SyncLock UpdateDataLock
            Try
                Dim client As Database.AccessClient = sender

                Dim sDT As DataTable = client.GetGoodsList()
                Dim myDT As DataTable = DB.GetGoodsList()

                For Each r As DataRow In sDT.Rows
                    Select Case CompareModify(myDT, r("Label"), GetTime(r("Modify")))
                        Case Compare.MoreNew : DB.ChangeGoods(Goods.GetFrom(r), False)
                        Case Compare.NoExist : DB.AddGoods(Goods.GetFrom(r), False)
                    End Select
                Next

                sDT = client.GetPersonnelList
                myDT = DB.GetPersonnelList
                For Each r As DataRow In sDT.Rows
                    Select Case CompareModify(myDT, r("Label"), GetTime(r("Modify")))
                        Case Compare.MoreNew : DB.ChangePersonnel(Personnel.GetFrom(r), False)
                        Case Compare.NoExist : DB.AddPersonnel(Personnel.GetFrom(r), False)
                    End Select
                Next

                sDT = client.GetCustomerList
                myDT = DB.GetCustomerList
                For Each r As DataRow In sDT.Rows
                    Select Case CompareModify(myDT, r("Label"), GetTime(r("Modify")))
                        Case Compare.MoreNew : DB.ChangeCustomer(Customer.GetFrom(r), False)
                        Case Compare.NoExist : DB.AddCustomer(Customer.GetFrom(r), False)
                    End Select
                Next

                sDT = client.GetSupplierList
                myDT = DB.GetSupplierList
                For Each r As DataRow In sDT.Rows
                    Select Case CompareModify(myDT, r("Label"), GetTime(r("Modify")))
                        Case Compare.MoreNew : DB.ChangeSupplier(Supplier.GetFrom(r), False)
                        Case Compare.NoExist : DB.AddSupplier(Supplier.GetFrom(r), False)
                    End Select
                Next

                sDT = client.GetContractList()
                myDT = DB.GetContractList()
                For Each r As DataRow In sDT.Rows
                    Select Case CompareModify(myDT, r("Label"), GetTime(r("Modify")))
                        Case Compare.MoreNew : DB.ChangeContract(Contract.GetFrom(r), False)
                        Case Compare.NoExist : DB.AddContract(Contract.GetFrom(r), False)
                    End Select
                Next

                sDT = client.GetHistoryPriceList()
                myDT = DB.GetHistoryPriceList()
                For Each r As DataRow In sDT.Rows
                    If CompareHistoryPrice(myDT, r("GoodsLabel"), GetTime(r("Time"))) = Compare.NoExist Then
                        DB.AddHistoryPrice(HistoryPrice.GetFrom(r), False)
                    End If
                Next
            Catch

            End Try

        End SyncLock
    End Sub

    Public Function GetTime(ByVal obj As Object) As Date
        If obj Is DBNull.Value Then Return New Date(0)
        Return CType(obj, Date)
    End Function

    Enum Compare
        Normal = 0
        NoExist = 1
        MoreNew = 2
    End Enum

    Public Function CompareHistoryPrice(ByVal DT As DataTable, ByVal Label As String, ByVal Time As Date) As Compare
        For Each r As DataRow In DT.Rows
            If r("GoodsLabel") = Label And GetTime(r("Time")) = Time Then Return Compare.Normal
        Next
        Return Compare.NoExist
    End Function

    Public Function CompareModify(ByVal DT As DataTable, ByVal Label As String, ByVal Modify As Date) As Compare
        For Each r As DataRow In DT.Rows
            If r("Label") = Label Then
                If GetTime(r("Modify")) < Modify Then
                    Return Compare.MoreNew
                Else
                    Return Compare.Normal
                End If
            End If
        Next
        Return Compare.NoExist
    End Function

    Private Sub ChangedContract(ByVal sender As Object, ByVal con As Database.StructureBase.Contract) Handles ccc.ChangedContract
        DB.ChangeContract(con, False)
    End Sub

    Private Sub ChangedCustomer(ByVal sender As Object, ByVal cus As Database.StructureBase.Customer) Handles ccc.ChangedCustomer
        DB.ChangeCustomer(cus, False)
    End Sub

    Private Sub ChangedGoods(ByVal sender As Object, ByVal goods As Database.StructureBase.Goods) Handles ccc.ChangedGoods
        DB.ChangeGoods(goods, False)
    End Sub

    Private Sub ChangedHistoryPrice(ByVal sender As Object, ByVal hp As Database.StructureBase.HistoryPrice) Handles ccc.ChangedHistoryPrice
        DB.ChangeHistoryPrice(hp, False)
    End Sub

    Private Sub ChangedPersonnel(ByVal sender As Object, ByVal per As Database.StructureBase.Personnel) Handles ccc.ChangedPersonnel
        DB.ChangePersonnel(per, False)
    End Sub


    Private Sub ChangedSupplier(ByVal sender As Object, ByVal sup As Database.StructureBase.Supplier) Handles ccc.ChangedSupplier
        DB.ChangeSupplier(sup, False)
    End Sub



    Private Sub CreatedContract(ByVal sender As Object, ByVal con As Database.StructureBase.Contract) Handles ccc.CreatedContract
        DB.AddContract(con, False)
    End Sub

    Private Sub CreatedCustomer(ByVal sender As Object, ByVal cus As Database.StructureBase.Customer) Handles ccc.CreatedCustomer
        DB.AddCustomer(cus, False)
    End Sub

    Private Sub CreatedGoods(ByVal sender As Object, ByVal goods As Database.StructureBase.Goods) Handles ccc.CreatedGoods
        DB.AddGoods(goods, False)
    End Sub

    Private Sub CreatedHistoryPrice(ByVal sender As Object, ByVal hp As Database.StructureBase.HistoryPrice) Handles ccc.CreatedHistoryPrice
        DB.AddHistoryPrice(hp, False)
    End Sub

    Private Sub CreatedPersonnel(ByVal sender As Object, ByVal per As Database.StructureBase.Personnel) Handles ccc.CreatedPersonnel
        DB.AddPersonnel(per, False)
    End Sub

    Private Sub CreatedSupplier(ByVal sender As Object, ByVal sup As Database.StructureBase.Supplier) Handles ccc.CreatedSupplier
        DB.AddSupplier(sup, False)
    End Sub

    Private Sub DeletedContract(ByVal sender As Object, ByVal con As Database.StructureBase.Contract) Handles ccc.DeletedContract
        DB.DeleteContract(con, False)
    End Sub

    Private Sub DeletedCustomer(ByVal sender As Object, ByVal cus As Database.StructureBase.Customer) Handles ccc.DeletedCustomer
        DB.DeleteCustomer(cus, False)
    End Sub

    Private Sub DeletedGoods(ByVal sender As Object, ByVal goods As Database.StructureBase.Goods) Handles ccc.DeletedGoods
        DB.DeleteGoods(goods, False)
    End Sub

    Private Sub DeletedHistoryPrice(ByVal sender As Object, ByVal hp As Database.StructureBase.HistoryPrice) Handles ccc.DeletedHistoryPrice
        DB.DeleteHistoryPrice(hp, False)
    End Sub

    Private Sub DeletedHistoryPriceList(ByVal sender As Object, ByVal hp As Database.StructureBase.HistoryPrice) Handles ccc.DeletedHistoryPriceList
        DB.DeleteHistoryPriceList(hp.GoodsLabel, False)
    End Sub

    Private Sub DeletedPersonnel(ByVal sender As Object, ByVal per As Database.StructureBase.Personnel) Handles ccc.DeletedPersonnel
        DB.DeletePersonnel(per, False)
    End Sub

    Private Sub DeletedSupplier(ByVal sender As Object, ByVal sup As Database.StructureBase.Supplier) Handles ccc.DeletedSupplier
        DB.DeleteSupplier(sup, False)
    End Sub
End Module
