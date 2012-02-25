Public Class ProgressDialog
    Dim CancelHandler As Action(Of Object)
    Dim FinishHandler As Action(Of Object)
    Dim Title As String = "進度"
    Dim Args As Object

    Dim updateProgressHandler As New Action(Of Integer, String)(AddressOf updateProgress)
    Public Sub UpdateProgress(ByVal percent As Integer, Optional ByVal msg As String = "")
        If Me.InvokeRequired Then
            Me.Invoke(updateProgressHandler, percent, msg)
            Exit Sub
        End If
        lbPercent.Text = percent & "%"
        lbMessage.Text = msg
        Me.Text = Title & " - " & lbPercent.Text

        If FinishHandler IsNot Nothing Then FinishHandler(Args)
        Me.Close()
    End Sub

    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        If CancelHandler IsNot Nothing Then CancelHandler(Args)
        Me.Close()
    End Sub
End Class