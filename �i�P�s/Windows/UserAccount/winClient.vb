Public Class winClient

    WithEvents access As Database.Access
    WithEvents server As Database.AccessServer = server

    Private Sub winClient_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cbClient.Items.Clear()
        cbClient.Items.AddRange(ClientManager.GetNameList())
        cbClient_UpdateState()
        'cbClient.SelectedIndex = 0
    End Sub

    Overloads Sub Show(ByVal db As Database.Access)
        Me.access = db
        Me.Show()
        Me.txtError.Clear()
        Me.Text = access.Name & " - " & access.Version
        cbClient.SelectedIndex = Array.IndexOf(ClientManager.Client, db)
        'UpdateMessageLog()
    End Sub

    Private Sub cbClient_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbClient.DropDown
        cbClient_UpdateState()
    End Sub

    Private Sub cbClient_UpdateState()
        For i As Integer = 0 To cbClient.Items.Count - 1
            Dim client As Database.Access = ClientManager(i)
            cbClient.Items(i) = client.Name & " - " & IIf(client.Connected, "已連線", "斷線")
        Next
    End Sub

    Private Sub cbClient_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbClient.SelectedIndexChanged
        Dim changed As Boolean = access IsNot ClientManager(cbClient.SelectedIndex)
        access = ClientManager(cbClient.SelectedIndex)
        Me.Text = access.Name & " - " & access.Version
        If changed Then
            UpdateMessageLog()
            TabControl1.Focus()
        End If

    End Sub


    Dim receiveCount As Long
    Dim SendCount As Long
    Private Sub UpdateMessageLog()
        Dim msg As TCPTool.Client.MessageLog
        receiveCount = 0
        SendCount = 0
        txtReceive.Clear()
        Dim s As New System.Text.StringBuilder()
        For i As Integer = 0 To access.MessageLogList.Count - 1
            msg = access.MessageLogList(i)
            'AddReceive(access.MessageLogList(i))
            s.AppendLine(GetDisplayMsg(access.MessageLogList(i)))
            If access.MessageLogList(i).SR = TCPTool.Client.SR.Send Then
                SendCount += 1
            Else
                receiveCount += 1
            End If
        Next
        txtReceive.AppendText(s.ToString)
        Label1.Text = "接收:" & receiveCount & "  傳送:" & SendCount
    End Sub

    'Public Function GetDisplayText()


    Private Sub AddReceive(ByVal e As TCPTool.Client.MessageLog)
        'If ckShowCheckMessage.Checked OrElse e.Message <> "%CheckConnectState%" Then txtReceive.AppendText(GetDisplayMsg(e) & vbCrLf)
        Dim msg As String = GetDisplayMsg(e)
        If msg <> "" Then txtReceive.AppendText(msg & vbCrLf)
        If e.SR = TCPTool.Client.SR.Send Then
            SendCount += 1
        Else
            receiveCount += 1
        End If
        Label1.Text = "接收:" & receiveCount & "  傳送:" & SendCount
    End Sub

    Public Function GetDisplayMsg(ByVal e As TCPTool.Client.MessageLog) As String
        'If ckShowCheckMessage.Checked OrElse e.Message = "%CheckConnectState%" Then
        If Not ckShowCheckMessage.Checked And e.Message = "%CheckConnectState%" Then Return ""
        Dim strTime As String = IIf(e.SR = TCPTool.Client.SR.Send, "s", "r") & "[" & e.Time.ToString("HH:mm:ss") & "." & e.Time.Millisecond.ToString("000") & "] "
        If ckSimpleMsg.Checked Then
            Dim lst As String() = Split(e.Message, ",")
            lst = Array.ConvertAll(lst, AddressOf GetShortMsg)
            Return strTime & Join(lst, "," & vbTab)
        Else
            Return strTime & e.Message
        End If
    End Function

    Public Function GetShortMsg(ByVal s As String) As String
        ' Return Strings.Left(s, 25) & IIf(s.Length > 25, "...[" & s.Length & "]", "")
        Return IIf(s.Length > 25, Strings.Left(s, 18) & "...[" & s.Length & "]", s)
    End Function

    Private Sub AddError(ByVal err As String)
        txtError.AppendText(err & vbCrLf)
    End Sub

    Delegate Sub delRec(ByVal s As TCPTool.Client.MessageLog)
    Dim DelegateReceive As New delRec(AddressOf AddReceive)  'New Action(Of String)(AddressOf AddReceive)
    Dim DelegateError As New Action(Of String)(AddressOf AddError)

    Private Sub access_ErrorMessage(ByVal client As TCPTool.Client, ByVal Message As String) Handles access.ErrorMessage
        If Me.IsDisposed Then Exit Sub
        If Me.InvokeRequired Then
            Me.Invoke(DelegateError, Message)
        Else
            AddError(Message)
        End If
    End Sub

    Private Sub server_ErrorMessage(ByVal sender As TCPTool, ByVal ErrorMessage As String) Handles server.ErrorMessage
        If Me.IsDisposed Then Exit Sub
        If Me.InvokeRequired Then
            Me.Invoke(DelegateError, ErrorMessage)
        Else
            AddError(ErrorMessage)
        End If
    End Sub


    Private Sub access_LogMessage(ByVal client As TCPTool.Client, ByVal e As TCPTool.Client.MessageLog) Handles access.LogMessage
        If Me.IsDisposed Then Exit Sub
        If Me.InvokeRequired Then
            Me.Invoke(DelegateReceive, e)
        Else
            AddReceive(e)
        End If
    End Sub



End Class