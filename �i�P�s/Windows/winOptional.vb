﻿Public Class winOptional

    Private Sub winOptional_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cbMode.SelectedIndex = Config.Mode

        btOrderBackColor.BackColor = ToColor(Config.OrderBackcolor)
        btSalesBackColor.BackColor = ToColor(Config.SalesBackColor)

        txtServerName.Text = Config.ServerName
        txtPort.Text = Config.ServerPort
        UpdateShopList()

    End Sub

    Private Sub UpdateShopList()
        dgShop.Rows.Clear()
        For Each c As Database.Access In Client.Client
            If c.GetType() Is GetType(Database.AccessClient) Then
                Dim cc As Database.Access = c
                dgShop.Rows.Add(cc.Name, cc.IP, cc.Port)
            End If
        Next
    End Sub

    Private Function GetShopList() As Database.AccessClient()
        Dim lstClient As New List(Of Database.AccessClient)

        For i As Integer = 0 To dgShop.Rows.Count - 2
            Dim r As DataGridViewRow = dgShop.Rows(i)
            Dim Info As New Database.ClientInfo(r.Cells(cShop.Index).Value, r.Cells(cIP.Index).Value, r.Cells(cPort.Index).Value)
            lstClient.Add(New Database.AccessClient(info))
        Next
        Return lstClient.ToArray
    End Function

    Private Sub btOrderBackColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOrderBackColor.Click, btSalesBackColor.Click
        ColorDialog1.Color = sender.backcolor
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then sender.backcolor = ColorDialog1.Color
    End Sub

    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        Me.Close()
    End Sub

    Private Sub btOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOK.Click
        Dim ModeChanged As Boolean = Config.Mode <> cbMode.SelectedIndex

        Config.OrderBackcolor = btOrderBackColor.BackColor.ToArgb
        Config.SalesBackColor = btSalesBackColor.BackColor.ToArgb

        Config.ServerName = txtServerName.Text
        If Config.Mode = Connect.Server Then
            Server.ChangeName(Config.ServerName)
            myDatabase.Name = Config.ServerName
        End If


        Dim newPort As Integer
        If Not Integer.TryParse(txtPort.Text, newPort) Then
            MsgBox("通訊埠設定錯誤!")
            Exit Sub
        End If

        If Config.ServerPort <> newPort Then
            Config.ServerPort = newPort
            Server.Port = Config.ServerPort
            Server.Close()
            Server.Open()
        End If

        Config.Mode = cbMode.SelectedIndex
        ConfigSave()


        Client.EndConnect()
        Dim lstClient As New List(Of Database.Access)
        If Config.Mode = Connect.Server Then lstClient.Add(myDatabase)
        lstClient.AddRange(GetShopList())
        Client.Client = lstClient.ToArray()
        Client.StartConnect()
        Client.Save(ClientPath)


        If ModeChanged Then
            'If MsgBox("您已經改變工作模式，必須重新啟動才能正式生效，您現在要重新啟動？", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "重新啟動") = MsgBoxResult.Yes Then
            '    FinishProgram()
            '    Application.Restart()
            'End If
            MsgBox("您已經改變工作模式，現在的模式是[" & cbMode.Text & "]。")

            FinishProgram()
            InitialProgram()
        End If


        Me.Close()
    End Sub

    Private Sub btMdbUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btMdbUpdate.Click
        If Config.Mode = Connect.Server Then
            UpdateDatabase()
            MsgBox("更新完成")
        Else
            MsgBox("Client無此功能")
        End If
    End Sub

    Private Sub btRepairMDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRepairMDB.Click
        If Config.Mode = Connect.Server Then
            Dim res As Database.Access.RepairAccessResult = Database.Access.RepairAccess(myDatabase.BasePath)
            MsgBox(res.Message)
        Else
            MsgBox("Client無此功能")
        End If
    End Sub

    Private Sub cbMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbMode.SelectedIndexChanged
        txtServerName.Enabled = cbMode.SelectedIndex = 1
        txtPort.Enabled = cbMode.SelectedIndex = 1
    End Sub
End Class