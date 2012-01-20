<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winContract
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
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btAdd = New System.Windows.Forms.Button
        Me.txtNote = New System.Windows.Forms.TextBox
        Me.txtCommission = New System.Windows.Forms.TextBox
        Me.txtLabel = New System.Windows.Forms.TextBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.txtDiscount = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPrepay = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.ckEnable = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(33, 137)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "備註"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(245, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "佣金"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(245, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "識別碼"
        '
        'btAdd
        '
        Me.btAdd.Location = New System.Drawing.Point(354, 244)
        Me.btAdd.Name = "btAdd"
        Me.btAdd.Size = New System.Drawing.Size(75, 23)
        Me.btAdd.TabIndex = 9
        Me.btAdd.Text = "新增"
        Me.btAdd.UseVisualStyleBackColor = True
        '
        'txtNote
        '
        Me.txtNote.Location = New System.Drawing.Point(35, 152)
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Size = New System.Drawing.Size(394, 77)
        Me.txtNote.TabIndex = 4
        '
        'txtCommission
        '
        Me.txtCommission.Location = New System.Drawing.Point(292, 56)
        Me.txtCommission.Name = "txtCommission"
        Me.txtCommission.Size = New System.Drawing.Size(115, 22)
        Me.txtCommission.TabIndex = 7
        '
        'txtLabel
        '
        Me.txtLabel.Location = New System.Drawing.Point(292, 18)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.Size = New System.Drawing.Size(115, 22)
        Me.txtLabel.TabIndex = 6
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(88, 56)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(115, 22)
        Me.txtName.TabIndex = 7
        '
        'txtDiscount
        '
        Me.txtDiscount.Location = New System.Drawing.Point(292, 95)
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.Size = New System.Drawing.Size(115, 22)
        Me.txtDiscount.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(41, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "名稱"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(245, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "折扣"
        '
        'txtPrepay
        '
        Me.txtPrepay.Location = New System.Drawing.Point(88, 98)
        Me.txtPrepay.Name = "txtPrepay"
        Me.txtPrepay.Size = New System.Drawing.Size(115, 22)
        Me.txtPrepay.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(41, 101)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 12)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "預付額"
        '
        'ckEnable
        '
        Me.ckEnable.AutoSize = True
        Me.ckEnable.Location = New System.Drawing.Point(43, 21)
        Me.ckEnable.Name = "ckEnable"
        Me.ckEnable.Size = New System.Drawing.Size(48, 16)
        Me.ckEnable.TabIndex = 15
        Me.ckEnable.Text = "有效"
        Me.ckEnable.UseVisualStyleBackColor = True
        '
        'winContract
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(462, 276)
        Me.Controls.Add(Me.ckEnable)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btAdd)
        Me.Controls.Add(Me.txtNote)
        Me.Controls.Add(Me.txtPrepay)
        Me.Controls.Add(Me.txtDiscount)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtCommission)
        Me.Controls.Add(Me.txtLabel)
        Me.Name = "winContract"
        Me.Text = "合約"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btAdd As System.Windows.Forms.Button
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents txtCommission As System.Windows.Forms.TextBox
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPrepay As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ckEnable As System.Windows.Forms.CheckBox
End Class
