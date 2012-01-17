Public Class winOptional

    Private Sub winOptional_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btOrderBackColor.BackColor = ToColor(Config.OrderBackcolor)
        btSalesBackColor.BackColor = ToColor(Config.SalesBackColor)
    End Sub

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
        Me.Close()
    End Sub
End Class