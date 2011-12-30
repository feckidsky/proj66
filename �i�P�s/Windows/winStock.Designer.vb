<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winStock
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtLabel = New System.Windows.Forms.TextBox
        Me.cbGoods = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cbSupplier = New System.Windows.Forms.ComboBox
        Me.txtDate = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtIMEI = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtNote = New System.Windows.Forms.TextBox
        Me.txtCost = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtPrice = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.btNumber = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.btAdd = New System.Windows.Forms.Button
        Me.btAddSupplier = New System.Windows.Forms.Button
        Me.btAddGoods = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "識別碼"
        '
        'txtLabel
        '
        Me.txtLabel.Location = New System.Drawing.Point(75, 29)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.Size = New System.Drawing.Size(127, 22)
        Me.txtLabel.TabIndex = 14
        '
        'cbGoods
        '
        Me.cbGoods.FormattingEnabled = True
        Me.cbGoods.Location = New System.Drawing.Point(284, 68)
        Me.cbGoods.Name = "cbGoods"
        Me.cbGoods.Size = New System.Drawing.Size(127, 20)
        Me.cbGoods.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(237, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "商品"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(237, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "供應商"
        '
        'cbSupplier
        '
        Me.cbSupplier.FormattingEnabled = True
        Me.cbSupplier.Location = New System.Drawing.Point(284, 29)
        Me.cbSupplier.Name = "cbSupplier"
        Me.cbSupplier.Size = New System.Drawing.Size(127, 20)
        Me.cbSupplier.TabIndex = 16
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(75, 68)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(127, 22)
        Me.txtDate.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "進貨日期"
        '
        'txtIMEI
        '
        Me.txtIMEI.Location = New System.Drawing.Point(75, 108)
        Me.txtIMEI.Name = "txtIMEI"
        Me.txtIMEI.Size = New System.Drawing.Size(127, 22)
        Me.txtIMEI.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(28, 111)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 12)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "IMEI"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(29, 201)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "備註"
        '
        'txtNote
        '
        Me.txtNote.Location = New System.Drawing.Point(30, 225)
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Size = New System.Drawing.Size(394, 77)
        Me.txtNote.TabIndex = 17
        '
        'txtCost
        '
        Me.txtCost.Location = New System.Drawing.Point(75, 148)
        Me.txtCost.Name = "txtCost"
        Me.txtCost.Size = New System.Drawing.Size(127, 22)
        Me.txtCost.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(28, 151)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "進貨價"
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(284, 145)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(127, 22)
        Me.txtPrice.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(237, 148)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 12)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "定價"
        '
        'btNumber
        '
        Me.btNumber.Location = New System.Drawing.Point(284, 108)
        Me.btNumber.Name = "btNumber"
        Me.btNumber.Size = New System.Drawing.Size(127, 22)
        Me.btNumber.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(237, 111)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 12)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "數量"
        '
        'btAdd
        '
        Me.btAdd.Location = New System.Drawing.Point(349, 325)
        Me.btAdd.Name = "btAdd"
        Me.btAdd.Size = New System.Drawing.Size(75, 23)
        Me.btAdd.TabIndex = 19
        Me.btAdd.Text = "新增"
        Me.btAdd.UseVisualStyleBackColor = True
        '
        'btAddSupplier
        '
        Me.btAddSupplier.Location = New System.Drawing.Point(430, 27)
        Me.btAddSupplier.Name = "btAddSupplier"
        Me.btAddSupplier.Size = New System.Drawing.Size(53, 23)
        Me.btAddSupplier.TabIndex = 20
        Me.btAddSupplier.Text = "新增"
        Me.btAddSupplier.UseVisualStyleBackColor = True
        '
        'btAddGoods
        '
        Me.btAddGoods.Location = New System.Drawing.Point(430, 66)
        Me.btAddGoods.Name = "btAddGoods"
        Me.btAddGoods.Size = New System.Drawing.Size(53, 23)
        Me.btAddGoods.TabIndex = 20
        Me.btAddGoods.Text = "新增"
        Me.btAddGoods.UseVisualStyleBackColor = True
        '
        'winStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(507, 360)
        Me.Controls.Add(Me.btAddGoods)
        Me.Controls.Add(Me.btAddSupplier)
        Me.Controls.Add(Me.btAdd)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtNote)
        Me.Controls.Add(Me.cbSupplier)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbGoods)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btNumber)
        Me.Controls.Add(Me.txtPrice)
        Me.Controls.Add(Me.txtCost)
        Me.Controls.Add(Me.txtIMEI)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.txtLabel)
        Me.Name = "winStock"
        Me.Text = "進貨"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
    Friend WithEvents cbGoods As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbSupplier As System.Windows.Forms.ComboBox
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIMEI As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents txtCost As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btAdd As System.Windows.Forms.Button
    Friend WithEvents btAddSupplier As System.Windows.Forms.Button
    Friend WithEvents btAddGoods As System.Windows.Forms.Button
End Class
