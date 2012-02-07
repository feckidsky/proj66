<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockMoveDialog
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
        Me.cbState = New System.Windows.Forms.ComboBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.cStockLabel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cSupplier = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cKind = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cBrand = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cIMEI = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cCost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cNote = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbSourceShop = New System.Windows.Forms.ComboBox
        Me.cbDestineShop = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtSourcePersonnel = New System.Windows.Forms.TextBox
        Me.txtDestinePersonnel = New System.Windows.Forms.TextBox
        Me.cbOK = New System.Windows.Forms.Button
        Me.cbCancel = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbState
        '
        Me.cbState.FormattingEnabled = True
        Me.cbState.Location = New System.Drawing.Point(66, 25)
        Me.cbState.Name = "cbState"
        Me.cbState.Size = New System.Drawing.Size(121, 20)
        Me.cbState.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cStockLabel, Me.cTime, Me.cSupplier, Me.cKind, Me.cBrand, Me.cIMEI, Me.cName, Me.cCost, Me.cNumber, Me.cNote})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 140)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(569, 137)
        Me.DataGridView1.TabIndex = 1
        '
        'cStockLabel
        '
        Me.cStockLabel.HeaderText = "庫存編號"
        Me.cStockLabel.Name = "cStockLabel"
        Me.cStockLabel.ReadOnly = True
        Me.cStockLabel.Width = 78
        '
        'cTime
        '
        Me.cTime.HeaderText = "進貨日期"
        Me.cTime.Name = "cTime"
        Me.cTime.ReadOnly = True
        Me.cTime.Width = 78
        '
        'cSupplier
        '
        Me.cSupplier.HeaderText = "供應商"
        Me.cSupplier.Name = "cSupplier"
        Me.cSupplier.ReadOnly = True
        Me.cSupplier.Width = 66
        '
        'cKind
        '
        Me.cKind.HeaderText = "種類"
        Me.cKind.Name = "cKind"
        Me.cKind.ReadOnly = True
        Me.cKind.Width = 54
        '
        'cBrand
        '
        Me.cBrand.HeaderText = "廠牌"
        Me.cBrand.Name = "cBrand"
        Me.cBrand.ReadOnly = True
        Me.cBrand.Width = 54
        '
        'cIMEI
        '
        Me.cIMEI.HeaderText = "IMEI"
        Me.cIMEI.Name = "cIMEI"
        Me.cIMEI.ReadOnly = True
        Me.cIMEI.Width = 55
        '
        'cName
        '
        Me.cName.HeaderText = "品名"
        Me.cName.Name = "cName"
        Me.cName.ReadOnly = True
        Me.cName.Width = 54
        '
        'cCost
        '
        Me.cCost.HeaderText = "進貨價"
        Me.cCost.Name = "cCost"
        Me.cCost.ReadOnly = True
        Me.cCost.Width = 66
        '
        'cNumber
        '
        Me.cNumber.HeaderText = "數量"
        Me.cNumber.Name = "cNumber"
        Me.cNumber.Width = 54
        '
        'cNote
        '
        Me.cNote.HeaderText = "備註"
        Me.cNote.Name = "cNote"
        Me.cNote.ReadOnly = True
        Me.cNote.Width = 54
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "狀態"
        '
        'cbSourceShop
        '
        Me.cbSourceShop.FormattingEnabled = True
        Me.cbSourceShop.Location = New System.Drawing.Point(66, 61)
        Me.cbSourceShop.Name = "cbSourceShop"
        Me.cbSourceShop.Size = New System.Drawing.Size(121, 20)
        Me.cbSourceShop.TabIndex = 0
        '
        'cbDestineShop
        '
        Me.cbDestineShop.FormattingEnabled = True
        Me.cbDestineShop.Location = New System.Drawing.Point(328, 61)
        Me.cbDestineShop.Name = "cbDestineShop"
        Me.cbDestineShop.Size = New System.Drawing.Size(121, 20)
        Me.cbDestineShop.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "來源"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(293, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "目地"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 12)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "調貨人"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(281, 97)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 12)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "取貨人"
        '
        'txtSourcePersonnel
        '
        Me.txtSourcePersonnel.Location = New System.Drawing.Point(66, 94)
        Me.txtSourcePersonnel.Name = "txtSourcePersonnel"
        Me.txtSourcePersonnel.Size = New System.Drawing.Size(121, 22)
        Me.txtSourcePersonnel.TabIndex = 5
        '
        'txtDestinePersonnel
        '
        Me.txtDestinePersonnel.Location = New System.Drawing.Point(328, 94)
        Me.txtDestinePersonnel.Name = "txtDestinePersonnel"
        Me.txtDestinePersonnel.Size = New System.Drawing.Size(121, 22)
        Me.txtDestinePersonnel.TabIndex = 5
        '
        'cbOK
        '
        Me.cbOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbOK.Location = New System.Drawing.Point(514, 307)
        Me.cbOK.Name = "cbOK"
        Me.cbOK.Size = New System.Drawing.Size(75, 23)
        Me.cbOK.TabIndex = 6
        Me.cbOK.Text = "確定"
        Me.cbOK.UseVisualStyleBackColor = True
        '
        'cbCancel
        '
        Me.cbCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbCancel.Location = New System.Drawing.Point(433, 307)
        Me.cbCancel.Name = "cbCancel"
        Me.cbCancel.Size = New System.Drawing.Size(75, 23)
        Me.cbCancel.TabIndex = 6
        Me.cbCancel.Text = "取消"
        Me.cbCancel.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtDestinePersonnel)
        Me.GroupBox1.Controls.Add(Me.txtSourcePersonnel)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Controls.Add(Me.cbDestineShop)
        Me.GroupBox1.Controls.Add(Me.cbSourceShop)
        Me.GroupBox1.Controls.Add(Me.cbState)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(597, 290)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "內容"
        '
        'StockMoveDialog
        '
        Me.AcceptButton = Me.cbOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(615, 346)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cbCancel)
        Me.Controls.Add(Me.cbOK)
        Me.Name = "StockMoveDialog"
        Me.Text = "調貨"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbState As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents cStockLabel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cSupplier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cKind As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cBrand As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIMEI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cNote As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbSourceShop As System.Windows.Forms.ComboBox
    Friend WithEvents cbDestineShop As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSourcePersonnel As System.Windows.Forms.TextBox
    Friend WithEvents txtDestinePersonnel As System.Windows.Forms.TextBox
    Friend WithEvents cbOK As System.Windows.Forms.Button
    Friend WithEvents cbCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
