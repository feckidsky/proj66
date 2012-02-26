Public Class ProgressDialog
    Dim CancelHandler As Action
    'Dim FinishHandler As Action
    Dim Title As String = "進度"
    Dim Args As Object


    'Public Overloads Shared Sub Show(ByVal Title As String, ByVal CancelAction As Action)
    '    Dim dialog As New ProgressDialog
    '    dialog.Text = Title
    '    dialog.CancelHandler = CancelAction
    '    dialog.Show()
    'End Sub

    Dim FinishHandler As New Action(AddressOf Finish)
    Public Sub Finish()
        If Me.InvokeRequired Then
            Me.Invoke(FinishHandler)
            Exit Sub
        End If
        Me.Close()
    End Sub

    Dim updateProgressHandler As New Action(Of Integer, String)(AddressOf UpdateProgress)
    Public Sub UpdateProgress(ByVal percent As Integer, Optional ByVal msg As String = "")
        If Me.InvokeRequired Then
            Me.Invoke(updateProgressHandler, percent, msg)
            Exit Sub
        End If
        lbPercent.Text = percent & "%"
        lbMessage.Text = msg
        Me.Text = Title & " - " & lbPercent.Text
        ProgressBar1.Value = percent

        'If FinishHandler IsNot Nothing Then FinishHandler(Args)
        'Me.Close()
    End Sub



    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        If CancelHandler IsNot Nothing Then CancelHandler()
        Me.Close()
    End Sub
End Class