﻿Public Class NumberBox
    Inherits TextBox

    Private Sub NumberBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        'If Not Char.IsDigit(e.KeyChar) Then e.Handled = True
        'Dim tmp As Single
        'If Not Single.TryParse(Text & e.KeyChar, tmp) Then e.Handled = True
    End Sub

    Private Sub NumberBox_TextAlignChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.TextAlignChanged

    End Sub

    Private Sub NumberBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.TextChanged
        Static lastText As String = ""

        Dim tmpValue As Single
        If Not Single.TryParse(Text, tmpValue) Then
            Dim loc As Integer = Me.SelectionStart
            Dim c As Integer = Text.Length - lastText.Length
            Text = lastText
            Me.SelectionStart = loc - c
        End If

        lastText = Text
    End Sub
End Class
