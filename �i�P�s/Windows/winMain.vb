Imports 進銷存.Database.StructureBase

Public Class winMain

    Private Sub winMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitialProgram()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        winStock.Add(NewStock)
    End Sub
End Class