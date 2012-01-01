<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winSales
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
        Me.btAddGood = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtNote = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtDate = New System.Windows.Forms.TextBox
        Me.txtLabel = New System.Windows.Forms.TextBox
        Me.dgList = New System.Windows.Forms.DataGridView
        Me.btDelete = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.lbTotal = New System.Windows.Forms.Label
        Me.btCheck = New System.Windows.Forms.Button
        Me.cbPayMode = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cLabel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cKind = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cBrand = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSPrice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSubTotal = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btAddGood
        '
        Me.btAddGood.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btAddGood.Location = New System.Drawing.Point(417, 311)
        Me.btAddGood.Name = "btAddGood"
        Me.btAddGood.Size = New System.Drawing.Size(75, 23)
        Me.btAddGood.TabIndex = 39
        Me.btAddGood.Text = "新增"
        Me.btAddGood.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 408)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "備註"
        '
        'txtNote
        '
        Me.txtNote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNote.Location = New System.Drawing.Point(21, 423)
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Size = New System.Drawing.Size(368, 77)
        Me.txtNote.TabIndex = 37
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(221, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "日期"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "識別碼"
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(256, 27)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(127, 22)
        Me.txtDate.TabIndex = 25
        '
        'txtLabel
        '
        Me.txtLabel.Location = New System.Drawing.Point(80, 24)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.Size = New System.Drawing.Size(127, 22)
        Me.txtLabel.TabIndex = 24
        '
        'dgList
        '
        Me.dgList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cLabel, Me.cKind, Me.cBrand, Me.cName, Me.cPrice, Me.cSPrice, Me.cNumber, Me.cSubTotal})
        Me.dgList.Location = New System.Drawing.Point(17, 26)
        Me.dgList.Name = "dgList"
        Me.dgList.RowTemplate.Height = 24
        Me.dgList.Size = New System.Drawing.Size(556, 279)
        Me.dgList.TabIndex = 42
        '
        'btDelete
        '
        Me.btDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDelete.Location = New System.Drawing.Point(498, 311)
        Me.btDelete.Name = "btDelete"
        Me.btDelete.Size = New System.Drawing.Size(75, 23)
        Me.btDelete.TabIndex = 39
        Me.btDelete.Text = "刪除"
        Me.btDelete.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dgList)
        Me.GroupBox1.Controls.Add(Me.btDelete)
        Me.GroupBox1.Controls.Add(Me.btAddGood)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(588, 343)
        Me.GroupBox1.TabIndex = 43
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "商品清單"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(422, 423)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "合計"
        '
        'lbTotal
        '
        Me.lbTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTotal.AutoSize = True
        Me.lbTotal.Location = New System.Drawing.Point(479, 423)
        Me.lbTotal.Name = "lbTotal"
        Me.lbTotal.Size = New System.Drawing.Size(11, 12)
        Me.lbTotal.TabIndex = 45
        Me.lbTotal.Text = "0"
        '
        'btCheck
        '
        Me.btCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btCheck.Location = New System.Drawing.Point(424, 488)
        Me.btCheck.Name = "btCheck"
        Me.btCheck.Size = New System.Drawing.Size(75, 23)
        Me.btCheck.TabIndex = 46
        Me.btCheck.Text = "確定"
        Me.btCheck.UseVisualStyleBackColor = True
        '
        'cbPayMode
        '
        Me.cbPayMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbPayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPayMode.FormattingEnabled = True
        Me.cbPayMode.Location = New System.Drawing.Point(481, 451)
        Me.cbPayMode.Name = "cbPayMode"
        Me.cbPayMode.Size = New System.Drawing.Size(121, 20)
        Me.cbPayMode.TabIndex = 47
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(422, 454)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 44
        Me.Label3.Text = "付款方式"
        '
        'cLabel
        '
        Me.cLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cLabel.HeaderText = "商品編號"
        Me.cLabel.Name = "cLabel"
        Me.cLabel.ReadOnly = True
        Me.cLabel.Width = 78
        '
        'cKind
        '
        Me.cKind.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cKind.HeaderText = "種類"
        Me.cKind.Name = "cKind"
        Me.cKind.Width = 54
        '
        'cBrand
        '
        Me.cBrand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cBrand.HeaderText = "廠牌"
        Me.cBrand.Name = "cBrand"
        Me.cBrand.Width = 54
        '
        'cName
        '
        Me.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cName.HeaderText = "品名"
        Me.cName.Name = "cName"
        Me.cName.Width = 54
        '
        'cPrice
        '
        Me.cPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cPrice.HeaderText = "定價"
        Me.cPrice.Name = "cPrice"
        Me.cPrice.Width = 54
        '
        'cSPrice
        '
        Me.cSPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cSPrice.HeaderText = "賣價"
        Me.cSPrice.Name = "cSPrice"
        Me.cSPrice.Width = 54
        '
        'cNumber
        '
        Me.cNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cNumber.HeaderText = "數量"
        Me.cNumber.Name = "cNumber"
        Me.cNumber.Width = 54
        '
        'cSubTotal
        '
        Me.cSubTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.cSubTotal.HeaderText = "小計"
        Me.cSubTotal.Name = "cSubTotal"
        Me.cSubTotal.ReadOnly = True
        Me.cSubTotal.Width = 54
        '
        'winSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 523)
        Me.Controls.Add(Me.cbPayMode)
        Me.Controls.Add(Me.btCheck)
        Me.Controls.Add(Me.lbTotal)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtNote)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.txtLabel)
        Me.Name = "winSales"
        Me.Text = "銷貨單"
        CType(Me.dgList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btAddGood As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
    Friend WithEvents dgList As System.Windows.Forms.DataGridView
    Friend WithEvents btDelete As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbTotal As System.Windows.Forms.Label
    Friend WithEvents btCheck As System.Windows.Forms.Button
    Friend WithEvents cbPayMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cKind As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cBrand As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSubTotal As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
