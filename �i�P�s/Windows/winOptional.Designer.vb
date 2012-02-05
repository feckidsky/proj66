<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winOptional
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btSalesBackColor = New System.Windows.Forms.Button
        Me.btOrderBackColor = New System.Windows.Forms.Button
        Me.btOK = New System.Windows.Forms.Button
        Me.btCancel = New System.Windows.Forms.Button
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.dgShop = New System.Windows.Forms.DataGridView
        Me.cShop = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cIP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cPort = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btRepairMDB = New System.Windows.Forms.Button
        Me.btMdbUpdate = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cbMode = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgShop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btSalesBackColor)
        Me.GroupBox1.Controls.Add(Me.btOrderBackColor)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 270)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(176, 88)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "訂單/銷貨單背景"
        '
        'btSalesBackColor
        '
        Me.btSalesBackColor.Location = New System.Drawing.Point(17, 50)
        Me.btSalesBackColor.Name = "btSalesBackColor"
        Me.btSalesBackColor.Size = New System.Drawing.Size(142, 23)
        Me.btSalesBackColor.TabIndex = 1
        Me.btSalesBackColor.Text = "銷貨單背景"
        Me.btSalesBackColor.UseVisualStyleBackColor = True
        '
        'btOrderBackColor
        '
        Me.btOrderBackColor.Location = New System.Drawing.Point(17, 21)
        Me.btOrderBackColor.Name = "btOrderBackColor"
        Me.btOrderBackColor.Size = New System.Drawing.Size(142, 23)
        Me.btOrderBackColor.TabIndex = 1
        Me.btOrderBackColor.Text = "訂單背景"
        Me.btOrderBackColor.UseVisualStyleBackColor = True
        '
        'btOK
        '
        Me.btOK.Location = New System.Drawing.Point(329, 374)
        Me.btOK.Name = "btOK"
        Me.btOK.Size = New System.Drawing.Size(75, 23)
        Me.btOK.TabIndex = 1
        Me.btOK.Text = "確認"
        Me.btOK.UseVisualStyleBackColor = True
        '
        'btCancel
        '
        Me.btCancel.Location = New System.Drawing.Point(246, 374)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(75, 23)
        Me.btCancel.TabIndex = 1
        Me.btCancel.Text = "取消"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgShop)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 50)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(420, 204)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "連線設定"
        '
        'dgShop
        '
        Me.dgShop.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgShop.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgShop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgShop.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cShop, Me.cIP, Me.cPort})
        Me.dgShop.Location = New System.Drawing.Point(17, 21)
        Me.dgShop.Name = "dgShop"
        Me.dgShop.RowTemplate.Height = 24
        Me.dgShop.Size = New System.Drawing.Size(385, 164)
        Me.dgShop.TabIndex = 0
        '
        'cShop
        '
        Me.cShop.HeaderText = "店名"
        Me.cShop.Name = "cShop"
        '
        'cIP
        '
        Me.cIP.HeaderText = "IP"
        Me.cIP.Name = "cIP"
        '
        'cPort
        '
        Me.cPort.HeaderText = "Port"
        Me.cPort.Name = "cPort"
        '
        'btRepairMDB
        '
        Me.btRepairMDB.Location = New System.Drawing.Point(33, 21)
        Me.btRepairMDB.Name = "btRepairMDB"
        Me.btRepairMDB.Size = New System.Drawing.Size(113, 23)
        Me.btRepairMDB.TabIndex = 3
        Me.btRepairMDB.Text = "修復/壓縮資料庫"
        Me.btRepairMDB.UseVisualStyleBackColor = True
        '
        'btMdbUpdate
        '
        Me.btMdbUpdate.Location = New System.Drawing.Point(33, 50)
        Me.btMdbUpdate.Name = "btMdbUpdate"
        Me.btMdbUpdate.Size = New System.Drawing.Size(113, 23)
        Me.btMdbUpdate.TabIndex = 3
        Me.btMdbUpdate.Text = "更新資料庫結構"
        Me.btMdbUpdate.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btMdbUpdate)
        Me.GroupBox3.Controls.Add(Me.btRepairMDB)
        Me.GroupBox3.Location = New System.Drawing.Point(226, 270)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(178, 88)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "資料庫"
        '
        'cbMode
        '
        Me.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMode.FormattingEnabled = True
        Me.cbMode.Items.AddRange(New Object() {"連線端", "伺服器"})
        Me.cbMode.Location = New System.Drawing.Point(89, 12)
        Me.cbMode.Name = "cbMode"
        Me.cbMode.Size = New System.Drawing.Size(100, 20)
        Me.cbMode.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "工作模式"
        '
        'winOptional
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(445, 414)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.cbMode)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btCancel)
        Me.Controls.Add(Me.btOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "winOptional"
        Me.Text = "選項設定"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgShop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btSalesBackColor As System.Windows.Forms.Button
    Friend WithEvents btOrderBackColor As System.Windows.Forms.Button
    Friend WithEvents btOK As System.Windows.Forms.Button
    Friend WithEvents btCancel As System.Windows.Forms.Button
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgShop As System.Windows.Forms.DataGridView
    Friend WithEvents cShop As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cIP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cPort As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btRepairMDB As System.Windows.Forms.Button
    Friend WithEvents btMdbUpdate As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cbMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
