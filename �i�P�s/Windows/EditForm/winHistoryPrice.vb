﻿Imports 進銷存.Database.StructureBase

Public Class winHistoryPrice
    Enum Mode
        Create = 0
        Open = 1
    End Enum

    Dim Work As Mode

    Private Sub winGoods_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btAdd.Text = IIf(Work = Mode.Create, "新增", "修改")
    End Sub

    Public Sub Open(ByVal Data As HistoryPrice, ByVal GoodsName As String)
        If Not CheckAuthority(2) Then Exit Sub
        Work = Mode.Open
        UpdateText(Data)
        Me.Text = "歷史售價 - " & GoodsName
        MyBase.ShowDialog()
    End Sub

    Public Sub Create(ByVal GoodsLabel As String, ByVal GoodsName As String)
        If Not CheckAuthority(2) Then Exit Sub
        Work = Mode.Create
        Dim data As New HistoryPrice
        data.Time = Now
        data.GoodsLabel = GoodsLabel
        UpdateText(data)
        Me.Text = "歷史售價 - " & GoodsName
        MyBase.ShowDialog()
    End Sub

    Public Sub UpdateText(ByVal Data As HistoryPrice)
        txtLabel.Text = Data.GoodsLabel
        txtTime.Text = Data.Time.ToString("yyyy/MM/dd HH:mm:ss")
        txtPrice.Text = Data.Price
        txtCost.Text = Data.Cost
    End Sub

    Public Function GetData() As HistoryPrice
        Dim Data As HistoryPrice
        Data.GoodsLabel = txtLabel.Text
        Data.Time = txtTime.Text
        Data.Price = txtPrice.Text
        Data.Cost = txtCost.Text
        Return Data
    End Function

    Private Sub btAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAdd.Click

        If Work = Mode.Create Then
            DB.AddHistoryPrice(GetData())
        Else
            DB.ChangeHistoryPrice(GetData())
        End If

        Me.Close()
    End Sub


    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        Me.Close()
    End Sub
End Class