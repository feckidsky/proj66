<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winGoods
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
        Me.txtName = New System.Windows.Forms.TextBox
        Me.txtLabel = New System.Windows.Forms.TextBox
        Me.txtKind = New System.Windows.Forms.TextBox
        Me.txtBrand = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(37, 115)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "備註"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(38, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "品名"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(38, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "識別碼"
        '
        'btAdd
        '
        Me.btAdd.Location = New System.Drawing.Point(358, 222)
        Me.btAdd.Name = "btAdd"
        Me.btAdd.Size = New System.Drawing.Size(75, 23)
        Me.btAdd.TabIndex = 9
        Me.btAdd.Text = "新增"
        Me.btAdd.UseVisualStyleBackColor = True
        '
        'txtNote
        '
        Me.txtNote.Location = New System.Drawing.Point(39, 130)
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Size = New System.Drawing.Size(394, 77)
        Me.txtNote.TabIndex = 4
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(85, 67)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(115, 22)
        Me.txtName.TabIndex = 7
        '
        'txtLabel
        '
        Me.txtLabel.Location = New System.Drawing.Point(85, 26)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.Size = New System.Drawing.Size(115, 22)
        Me.txtLabel.TabIndex = 6
        '
        'txtKind
        '
        Me.txtKind.Location = New System.Drawing.Point(289, 26)
        Me.txtKind.Name = "txtKind"
        Me.txtKind.Size = New System.Drawing.Size(115, 22)
        Me.txtKind.TabIndex = 7
        '
        'txtBrand
        '
        Me.txtBrand.Location = New System.Drawing.Point(289, 67)
        Me.txtBrand.Name = "txtBrand"
        Me.txtBrand.Size = New System.Drawing.Size(115, 22)
        Me.txtBrand.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(242, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "種類"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(242, 70)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "廠牌"
        '
        'winGoods
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(474, 257)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btAdd)
        Me.Controls.Add(Me.txtNote)
        Me.Controls.Add(Me.txtBrand)
        Me.Controls.Add(Me.txtKind)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtLabel)
        Me.Name = "winGoods"
        Me.Text = "商品"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btAdd As System.Windows.Forms.Button
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
    Friend WithEvents txtKind As System.Windows.Forms.TextBox
    Friend WithEvents txtBrand As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
