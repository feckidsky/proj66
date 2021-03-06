﻿Imports 進銷存.Database.DatabaseType
Imports 進銷存.Database

Public Class winContract

    Dim access As Database.Access
    Enum Mode
        Create = 0
        Open = 1
    End Enum

    Dim Work As Mode

    Private Sub winGoods_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btAdd.Text = IIf(Work = Mode.Create, "新增", "修改")
    End Sub

    Public Sub Open(ByVal Data As Contract, ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub
        Work = Mode.Open
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub Create(ByVal Data As Contract, ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub
        Work = Mode.Create
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub UpdateText(ByVal Data As Contract)
        ckEnable.Checked = Data.Enable
        txtLabel.Text = Data.Label
        txtName.Text = Data.Name
        txtCommission.Text = Data.Commission
        txtDiscount.Text = Data.Discount
        txtPrepay.Text = Data.Prepay
        txtNote.Text = Data.Note
    End Sub

    Public Function GetData() As Contract
        Dim Data As Contract
        Data.Enable = ckEnable.Checked
        Data.Label = txtLabel.Text
        Data.Name = txtName.Text
        Data.Commission = Val(txtCommission.Text)
        Data.Discount = Val(txtDiscount.Text)
        Data.Prepay = Val(txtPrepay.Text)
        Data.Note = txtNote.Text
        Data.Modify = Now
        Return Data
    End Function

    Private Sub btAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAdd.Click
        If Work = Mode.Create Then
            access.AddContract(GetData())
        Else
            access.ChangeContract(GetData())
        End If

        Me.Close()
    End Sub


    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。

        Me.DefaultTextBoxImeMode()

    End Sub
End Class