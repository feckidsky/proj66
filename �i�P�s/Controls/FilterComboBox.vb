Public Class FilterComboBox
    Inherits ComboBox

    Public Class ItemList
        Inherits List(Of String)
        Event AddedItem(ByVal sender As Object, ByVal Item As String)
        Event AddedRangeItem(ByVal sender As Object, ByVal items As String())

        Public Overloads Sub Add(ByVal Text As String)
            MyBase.Add(Text)
            RaiseEvent AddedItem(Me, Text)
        End Sub

        Public Overloads Sub AddRange(ByVal Texts As String())
            MyBase.AddRange(Texts)
            RaiseEvent AddedRangeItem(Me, Texts)
        End Sub

    End Class

    Public Shadows WithEvents Items As New ItemList

    Dim disableEvent = False
    Private Sub FilterComboBox_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DropDown
        If disableEvent Then Exit Sub
        MyBase.Items.Clear()
        MyBase.Items.AddRange(Items.ToArray)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        If disableEvent Then Exit Sub
        MyBase.OnTextChanged(e)
    End Sub

    Private Sub FilterComboBox_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DropDownClosed
        If MyBase.Items.Count = 0 Then
            disableEvent = True
            MyBase.Items.Add(tmpText)
            Me.Text = tmpText
            disableEvent = False
        Else
            disableEvent = True
            Me.Text = SelectedItem
            disableEvent = False
        End If
    End Sub


    Dim tmpText As String = Text


    Private Sub FilterComboBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.TextChanged
        If disableEvent Then Exit Sub

        Dim tmpPos As Point = Cursor.Position
        Cursor.Hide()
        Cursor.Position = Me.PointToScreen(Size.Add(Me.Size, New Size(-5, -5)))
        Me.SuspendLayout()
        Dim loc As Integer = Me.SelectionStart
        tmpText = Text
        disableEvent = True
        'Me.DroppedDown = False

        MyBase.Items.Clear()
        If Text <> "" Then
            MyBase.Items.AddRange(Items.FindAll(Function(s As String) Strings.InStr(s, Text)).ToArray)
        Else
            MyBase.Items.AddRange(Items.ToArray)
        End If



        Me.DroppedDown = True
        Me.ResetCursor()
     
        Cursor.Position = tmpPos
        Cursor.Show()
        Me.Text = tmpText

        disableEvent = False
        Me.SelectionStart = loc
        Me.ResumeLayout(False)
    End Sub


End Class

