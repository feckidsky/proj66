Public Class ProgressDialog
    'Delegate Sub CancelEventHandler()
    Public CancelHandler As New Database.Access.Progress.CancelAction(AddressOf Cancel)  'New Action(AddressOf Cancel)
    'Dim FinishHandler As Action
    Public Title As String = "進度"
    Dim Args As Object
    Public Thread As Threading.Thread
    Public AutoClose As Boolean = True

    Sub New()

        ' 此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        SubBarVisible = False
        TopMost = True
    End Sub

    Public Sub Cancel()

    End Sub

    Property SubBarVisible() As Boolean
        Get
            Return PartProgressBar.Visible
        End Get
        Set(ByVal value As Boolean)
            PartProgressBar.Visible = value
            lbPartPercent.Visible = value
            lbPartMessage.Visible = value
            Me.Height = IIf(value, PartProgressBar.Bottom, TotalProgressBar.Bottom) + 80
        End Set
    End Property


    Public Sub Start(ByVal Text As String)
        Me.Show()
        Thread.Start(GetProgress(Text))
    End Sub
    Public Function GetProgress(ByVal Text As String) As Database.Access.Progress
        Dim progress As New Database.Access.Progress(AddressOf UpdateProgress, Text, AddressOf Finish)
        Return progress
    End Function

    Dim updateProgressHandler As New Action(Of String, Integer)(AddressOf UpdateProgress)
    Public Sub UpdateProgress(ByVal msg As String, ByVal percent As Integer)
        Try
            If Me.InvokeRequired Then

                If Not Me.IsDisposed Then Me.Invoke(updateProgressHandler, msg, percent)

                Exit Sub
            End If
            lbPercent.Text = percent & "%"
            lbMessage.Text = msg
            Me.Text = Title & " - " & lbPercent.Text
            TotalProgressBar.Value = percent
        Catch
        End Try

    End Sub

    Dim FinishHandler As New Action(AddressOf Finish)
    Public Sub Finish()
        Try
            If Me.IsDisposed Then Exit Sub
            If Me.InvokeRequired Then
                If Not Me.IsDisposed Then Me.Invoke(FinishHandler)
                Exit Sub
            End If

            If AutoClose Then Me.Close()
        Catch

        End Try
    End Sub

    Public Sub UpdatePartProgress(ByVal msg As String, ByVal percent As Integer)

        If Me.InvokeRequired Then
            Try
                If Not Me.IsDisposed Then Me.Invoke(New Action(Of String, Integer)(AddressOf UpdatePartProgress), msg, percent)
            Catch
            End Try
            Exit Sub
        End If
        lbPartMessage.Text = msg
        lbPartPercent.Text = percent & "%"
        PartProgressBar.Value = percent
    End Sub

    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        If Thread IsNot Nothing Then
            Try
                Thread.Abort()
            Catch ex As Exception

            End Try

        End If
        CancelHandler()
        Me.Close()
    End Sub


    Dim CloseHandler As New Action(AddressOf Close)
    Public Overloads Sub Close()
        If Me.InvokeRequired Then
            Try
                If Not Me.IsDisposed Then Me.Invoke(CloseHandler)
            Catch
            End Try
            Exit Sub
        End If
        MyBase.Close()
    End Sub

    Dim ShowHandler As New Action(AddressOf Show)
    Public Overloads Sub Show()
        If Me.InvokeRequired Then
            Try
                If Not Me.IsDisposed Then Me.Invoke(ShowHandler)
            Catch
            End Try
            Exit Sub
        End If
        MyBase.Show()
    End Sub

End Class