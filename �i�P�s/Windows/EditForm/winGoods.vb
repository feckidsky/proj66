Imports 進銷存.Database.DatabaseType
Imports 進銷存.Database

Public Class winGoods
    Enum Mode
        Create = 0
        Open = 1
    End Enum

    Dim Work As Mode

    Dim access As Database.Access

    Private Sub winGoods_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btAdd.Text = IIf(Work = Mode.Create, "新增", "修改")
        txtLabel.Enabled = Work = Mode.Create
        txtPrice.Enabled = txtLabel.Enabled
        txtCost.Enabled = txtLabel.Enabled
        cbBrand.Items.Clear()
        cbBrand.Items.AddRange(CurrentAccess.GetBrandList())
        cbKind.Items.Clear()
        cbKind.Items.AddRange(CurrentAccess.GetKindList())
    End Sub

    Public Sub Open(ByVal Data As Goods, ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub
        Work = Mode.Open
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub Create(ByVal Data As Goods, ByVal DB As Database.Access)
        access = DB
        If Not CheckAuthority(2) Then Exit Sub
        Work = Mode.Create
        UpdateText(Data)
        MyBase.ShowDialog()
    End Sub

    Public Sub UpdateText(ByVal Data As Goods)
        txtLabel.Text = Data.Label
        txtName.Text = Data.Name
        cbKind.Text = Data.Kind
        cbBrand.Text = Data.Brand
        txtNote.Text = Data.Note
    End Sub

    Public Function GetData() As Goods
        Dim Data As Goods
        Data.Label = txtLabel.Text
        Data.Name = txtName.Text
        Data.Kind = cbKind.Text
        Data.Brand = cbBrand.Text
        Data.Note = txtNote.Text
        Data.Modify = Now
        Return Data
    End Function

    Public Function GetHistoryPrice() As HistoryPrice
        Dim data As New HistoryPrice
        Single.TryParse(txtCost.Text, data.Cost)
        Single.TryParse(txtPrice.Text, data.Price)
        data.GoodsLabel = txtLabel.Text
        data.Time = Now
        Return data
    End Function

    Private Sub btAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAdd.Click

        If txtCost.Text = "" And Work = Mode.Create Then
            MsgBox("尚未輸入進貨價", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If txtPrice.Text = "" And Work = Mode.Create Then
            MsgBox("尚未輸入建議售價", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Work = Mode.Create Then
            access.AddGoods(GetData())
            access.AddHistoryPrice(GetHistoryPrice())
        Else
            access.ChangeGoods(GetData())
        End If

        Me.Close()
        'UpdateText(GetNewGoods)


    End Sub


    Public Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Me.DefaultTextBoxImeMode()
    End Sub
End Class