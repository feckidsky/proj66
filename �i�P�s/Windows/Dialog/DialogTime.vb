Imports System.Windows.Forms

Public Class DialogTime

    Property Value() As Date
        Get
            If ckNothing.Checked Then Return Nothing
            Return dtpDate.Value.Date.Add(New TimeSpan(lstHour.Value, lstMinute.Value, lstSecond.Value))
        End Get
        Set(ByVal value As Date)
            ckNothing.Checked = value = Nothing
            If value = Nothing Then
                dtpDate.Value = Today

                lstHour.Value = Now.Hour
                lstMinute.Value = Now.Minute
                lstSecond.Value = Now.Second
            Else
                dtpDate.Value = value.Date
                lstHour.Value = value.Hour
                lstMinute.Value = value.Minute
                lstSecond.Value = value.Second
            End If


        End Set
    End Property

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DialogTime_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ckNothing_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckNothing.CheckedChanged
        dtpDate.Enabled = Not ckNothing.Checked
        lstHour.Enabled = Not ckNothing.Checked
        lstMinute.Enabled = Not ckNothing.Checked
        lstSecond.Enabled = Not ckNothing.Checked
    End Sub
End Class
