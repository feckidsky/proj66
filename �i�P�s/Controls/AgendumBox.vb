Public Class AgendumBox

    Public Enum Type
        Create = 0
        Edit = 1
    End Enum

    Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        tempData.Label = "A" & Now.ToString("yyyyMMddHHmmss")
    End Sub

    Public Mode As Type = Type.Create

    Public Changed As Boolean = False
    Public tempData As Database.Agendum
    Property Data() As Database.Agendum
        Get
            Return GetData()
        End Get
        Set(ByVal value As Database.Agendum)
            tempData = value
            txtKind.Text = value.Kind
            txtMessage.Text = value.Message
            dtpStart.Value = value.Modify
            ckFinished.Checked = value.Finished
            Changed = False
        End Set
    End Property

    Public Function GetData() As Database.Agendum
        Dim data As Database.Agendum
        data.Label = tempData.Label
        data.Kind = txtKind.Text
        data.Message = txtMessage.Text
        data.Modify = dtpStart.Value
        data.Finished = ckFinished.Checked
        Return data
    End Function

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKind.TextChanged, txtMessage.TextChanged, dtpStart.ValueChanged, ckFinished.CheckedChanged
        Changed = True
    End Sub

End Class
