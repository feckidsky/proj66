Imports 進銷存.Database.StructureBase

Public Class winMain

    Private Sub winMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitialProgram()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub 進貨ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 進貨ToolStripMenuItem.Click
        winStockIn.Add(NewStock())
    End Sub

    'Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Database.Access.File.CreateTable()

    'End Sub

    Private Sub 銷貨ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 銷貨ToolStripMenuItem.Click
        winSales.Show()
    End Sub

    Private Sub 庫存ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 庫存ToolStripMenuItem.Click
        winStock.Show()
    End Sub
End Class