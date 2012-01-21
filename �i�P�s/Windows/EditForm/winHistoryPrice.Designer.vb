<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winHistoryPrice
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btAdd = New System.Windows.Forms.Button
        Me.txtTime = New System.Windows.Forms.TextBox
        Me.txtLabel = New System.Windows.Forms.TextBox
        Me.txtPrice = New System.Windows.Forms.TextBox
        Me.txtCost = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(234, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "日期"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "識別碼"
        '
        'btAdd
        '
        Me.btAdd.Location = New System.Drawing.Point(321, 116)
        Me.btAdd.Name = "btAdd"
        Me.btAdd.Size = New System.Drawing.Size(75, 23)
        Me.btAdd.TabIndex = 9
        Me.btAdd.Text = "新增"
        Me.btAdd.UseVisualStyleBackColor = True
        '
        'txtTime
        '
        Me.txtTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtTime.Location = New System.Drawing.Point(281, 30)
        Me.txtTime.Name = "txtTime"
        Me.txtTime.ReadOnly = True
        Me.txtTime.Size = New System.Drawing.Size(115, 22)
        Me.txtTime.TabIndex = 7
        '
        'txtLabel
        '
        Me.txtLabel.BackColor = System.Drawing.SystemColors.Window
        Me.txtLabel.Location = New System.Drawing.Point(70, 27)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.ReadOnly = True
        Me.txtLabel.Size = New System.Drawing.Size(115, 22)
        Me.txtLabel.TabIndex = 6
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(281, 70)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(115, 22)
        Me.txtPrice.TabIndex = 7
        '
        'txtCost
        '
        Me.txtCost.Location = New System.Drawing.Point(70, 70)
        Me.txtCost.Name = "txtCost"
        Me.txtCost.Size = New System.Drawing.Size(115, 22)
        Me.txtCost.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(222, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "建議售價"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 12)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "進貨價"
        '
        'btCancel
        '
        Me.btCancel.Location = New System.Drawing.Point(236, 116)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(75, 23)
        Me.btCancel.TabIndex = 15
        Me.btCancel.Text = "取消"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'winHistoryPrice
        '
        Me.AcceptButton = Me.btAdd
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(423, 156)
        Me.Controls.Add(Me.btCancel)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btAdd)
        Me.Controls.Add(Me.txtCost)
        Me.Controls.Add(Me.txtPrice)
        Me.Controls.Add(Me.txtTime)
        Me.Controls.Add(Me.txtLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "winHistoryPrice"
        Me.Text = "歷史售價"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btAdd As System.Windows.Forms.Button
    Friend WithEvents txtTime As System.Windows.Forms.TextBox
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtCost As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btCancel As System.Windows.Forms.Button
End Class
