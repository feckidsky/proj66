Public Class winNotify
    Public Overloads Sub Show(ByVal msg As String)
        Message = msg
        MyBase.Show()
        Me.Location = Point.Subtract(My.Computer.Screen.WorkingArea.Size, Me.Size)
    End Sub

    Property Message() As String
        Get
            Return Label1.Text
        End Get
        Set(ByVal value As String)
            Label1.Text = value
        End Set
    End Property

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        OnClick(e)
    End Sub

    Private Sub winNotify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        winMain.access = CurrentAccess
        winStockMoveList.Show(CurrentAccess)
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.BackColor = IIf(Me.BackColor = Color.Orange, Drawing.SystemColors.Control, Color.Orange)
    End Sub
End Class