<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class winGoodsList
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.dgGoodsList = New System.Windows.Forms.DataGridView
        Me.cmsGoodsEdit = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.新增CToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.修改CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.刪除DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.新增歷史售價HToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.新增AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.gbHistory = New System.Windows.Forms.GroupBox
        Me.dgHistory = New System.Windows.Forms.DataGridView
        Me.cmsHistory = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsAddHistoryPrice = New System.Windows.Forms.ToolStripMenuItem
        Me.tsEditHistoryPrice = New System.Windows.Forms.ToolStripMenuItem
        Me.tsDeleteHistoryPrice = New System.Windows.Forms.ToolStripMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.dgGoodsList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsGoodsEdit.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gbHistory.SuspendLayout()
        CType(Me.dgHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsHistory.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgGoodsList
        '
        Me.dgGoodsList.AllowUserToAddRows = False
        Me.dgGoodsList.AllowUserToDeleteRows = False
        Me.dgGoodsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgGoodsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgGoodsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgGoodsList.ContextMenuStrip = Me.cmsGoodsEdit
        Me.dgGoodsList.Location = New System.Drawing.Point(12, 21)
        Me.dgGoodsList.Name = "dgGoodsList"
        Me.dgGoodsList.ReadOnly = True
        Me.dgGoodsList.RowTemplate.Height = 24
        Me.dgGoodsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgGoodsList.Size = New System.Drawing.Size(500, 401)
        Me.dgGoodsList.TabIndex = 0
        '
        'cmsGoodsEdit
        '
        Me.cmsGoodsEdit.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.新增CToolStripMenuItem1, Me.修改CToolStripMenuItem, Me.刪除DToolStripMenuItem, Me.ToolStripSeparator1, Me.新增歷史售價HToolStripMenuItem})
        Me.cmsGoodsEdit.Name = "cmsEdit"
        Me.cmsGoodsEdit.Size = New System.Drawing.Size(166, 98)
        '
        '新增CToolStripMenuItem1
        '
        Me.新增CToolStripMenuItem1.Name = "新增CToolStripMenuItem1"
        Me.新增CToolStripMenuItem1.Size = New System.Drawing.Size(165, 22)
        Me.新增CToolStripMenuItem1.Text = "新增(&A)"
        '
        '修改CToolStripMenuItem
        '
        Me.修改CToolStripMenuItem.Name = "修改CToolStripMenuItem"
        Me.修改CToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.修改CToolStripMenuItem.Text = "修改(&C)"
        '
        '刪除DToolStripMenuItem
        '
        Me.刪除DToolStripMenuItem.Name = "刪除DToolStripMenuItem"
        Me.刪除DToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.刪除DToolStripMenuItem.Text = "刪除(&D)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(162, 6)
        '
        '新增歷史售價HToolStripMenuItem
        '
        Me.新增歷史售價HToolStripMenuItem.Name = "新增歷史售價HToolStripMenuItem"
        Me.新增歷史售價HToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.新增歷史售價HToolStripMenuItem.Text = "新增歷史售價(&H)"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.新增AToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(871, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '新增AToolStripMenuItem
        '
        Me.新增AToolStripMenuItem.Name = "新增AToolStripMenuItem"
        Me.新增AToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.新增AToolStripMenuItem.Text = "新增(&A)"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgGoodsList)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(518, 428)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "商品項目"
        '
        'gbHistory
        '
        Me.gbHistory.Controls.Add(Me.dgHistory)
        Me.gbHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbHistory.Location = New System.Drawing.Point(0, 0)
        Me.gbHistory.Name = "gbHistory"
        Me.gbHistory.Size = New System.Drawing.Size(325, 428)
        Me.gbHistory.TabIndex = 3
        Me.gbHistory.TabStop = False
        Me.gbHistory.Text = "歷史售價"
        '
        'dgHistory
        '
        Me.dgHistory.AllowUserToAddRows = False
        Me.dgHistory.AllowUserToDeleteRows = False
        Me.dgHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgHistory.ContextMenuStrip = Me.cmsHistory
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.NullValue = Nothing
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgHistory.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgHistory.Location = New System.Drawing.Point(11, 21)
        Me.dgHistory.Name = "dgHistory"
        Me.dgHistory.ReadOnly = True
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White
        Me.dgHistory.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgHistory.RowTemplate.Height = 24
        Me.dgHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgHistory.Size = New System.Drawing.Size(308, 401)
        Me.dgHistory.TabIndex = 0
        '
        'cmsHistory
        '
        Me.cmsHistory.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsAddHistoryPrice, Me.tsEditHistoryPrice, Me.tsDeleteHistoryPrice})
        Me.cmsHistory.Name = "cmsEdit"
        Me.cmsHistory.Size = New System.Drawing.Size(166, 70)
        '
        'tsAddHistoryPrice
        '
        Me.tsAddHistoryPrice.Name = "tsAddHistoryPrice"
        Me.tsAddHistoryPrice.Size = New System.Drawing.Size(165, 22)
        Me.tsAddHistoryPrice.Text = "新增歷史售價(&A)"
        '
        'tsEditHistoryPrice
        '
        Me.tsEditHistoryPrice.Name = "tsEditHistoryPrice"
        Me.tsEditHistoryPrice.Size = New System.Drawing.Size(165, 22)
        Me.tsEditHistoryPrice.Text = "修改歷史售價(&C)"
        '
        'tsDeleteHistoryPrice
        '
        Me.tsDeleteHistoryPrice.Name = "tsDeleteHistoryPrice"
        Me.tsDeleteHistoryPrice.Size = New System.Drawing.Size(165, 22)
        Me.tsDeleteHistoryPrice.Text = "刪除歷史售價(&D)"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 30)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.gbHistory)
        Me.SplitContainer1.Size = New System.Drawing.Size(847, 428)
        Me.SplitContainer1.SplitterDistance = 518
        Me.SplitContainer1.TabIndex = 1
        '
        'winGoodsList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(871, 467)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "winGoodsList"
        Me.Text = "商品項目清單"
        CType(Me.dgGoodsList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsGoodsEdit.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.gbHistory.ResumeLayout(False)
        CType(Me.dgHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsHistory.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgGoodsList As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 新增AToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsGoodsEdit As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 新增CToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 修改CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 刪除DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents gbHistory As System.Windows.Forms.GroupBox
    Friend WithEvents dgHistory As System.Windows.Forms.DataGridView
    Friend WithEvents cmsHistory As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsAddHistoryPrice As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsEditHistoryPrice As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsDeleteHistoryPrice As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 新增歷史售價HToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
