Imports 進銷存.Database.StructureBase

Public Module Program
    Public DB As New Database.Access

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

    Public Config As SystemOptional
    Public ConfigPath As String = My.Application.Info.DirectoryPath & "\Config.xml"

    Public CurrentUser As Database.Personnel = Database.Personnel.Guest

    Event Account_Logout(ByVal personnel As Database.Personnel, ByVal Message As String)
    Event Account_LogIn(ByVal personnel As Database.Personnel, ByVal Message As String)
    Public SystemTitle As String = "進銷存管理系統"

    Public Sub InitialProgram()


        'UpdateDatabase()
        'Database.Access.RepairAccess(Database.Access.BasePath)
        ConfigLoad()

        LogOut(False)

        LogIn("kidsky1", "3883", False)
        'Dim admin As Personnel = DB.GetPersonnelByID("Administrator")
        'LogIn(admin.ID, admin.Password, False)
    End Sub

    Public Sub UpdateDatabase()
        Dim d As OleDb.OleDbConnection = Database.Access.ConnectBase(Database.Access.BasePath)
        Database.Access.DeleteTable("Mobile", d)
        Database.Access.CreateTable(Contract.Table, Contract.ToColumns, d)
        Database.Access.CreateTable(SalesContract.Table, SalesContract.ToColumns, d)
        Database.Access.CreateTable(OrderGoods.Table, OrderGoods.ToColumns, d)
        Database.Access.CreateTable(HistoryPrice.Table, HistoryPrice.ToColumns(), d)
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
End Module
