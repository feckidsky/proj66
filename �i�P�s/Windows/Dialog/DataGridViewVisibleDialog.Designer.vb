<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataGridViewVisibleDialog
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ckList = New System.Windows.Forms.CheckedListBox
        Me.btCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'ckList
        '
        Me.ckList.FormattingEnabled = True
        Me.ckList.Location = New System.Drawing.Point(12, 12)
        Me.ckList.Name = "ckList"
        Me.ckList.Size = New System.Drawing.Size(172, 191)
        Me.ckList.TabIndex = 0
        '
        'btCancel
        '
        Me.btCancel.Location = New System.Drawing.Point(84, 209)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(100, 30)
        Me.btCancel.TabIndex = 4
        Me.btCancel.Text = "確定"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'DataGridViewVisibleDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(192, 248)
        Me.Controls.Add(Me.btCancel)
        Me.Controls.Add(Me.ckList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "DataGridViewVisibleDialog"
        Me.Text = "欄位顯示"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ckList As System.Windows.Forms.CheckedListBox
    Friend WithEvents btCancel As System.Windows.Forms.Button
End Class
