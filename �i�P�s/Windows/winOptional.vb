Public Class winOptional

    Private Sub winOptional_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btOrderBackColor.BackColor = ToColor(Config.OrderBackcolor)
        btSalesBackColor.BackColor = ToColor(Config.SalesBackColor)
        UpdateShopList()

    End Sub

    Private Sub UpdateShopList()
        dgShop.Rows.Clear()
        For Each c As Database.AccessClient In Client.Client
            dgShop.Rows.Add(c.Name, c.IP, c.Port)
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
        Config.OrderBackcolor = btOrderBackColor.BackColor.ToArgb
        Config.SalesBackColor = btSalesBackColor.BackColor.ToArgb
        ConfigSave()

        Client.EndConnect()
        Client.Client = GetShopList()
        Client.StartConnect()
        Client.Save(ClientPath)

        Me.Close()
    End Sub

    Private Sub btMdbUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btMdbUpdate.Click
        UpdateDatabase()
        MsgBox("更新完成")
    End Sub

    Private Sub btRepairMDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRepairMDB.Click
        Database.Access.RepairAccess(DB.BasePath)
        MsgBox("修復/壓縮完成")
    End Sub
End Class