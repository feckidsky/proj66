﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.dgGoodsList = New System.Windows.Forms.DataGridView
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.新增AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.cmsEdit = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.新增CToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.修改CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.刪除DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.dgGoodsList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.cmsEdit.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgGoodsList
        '
        Me.dgGoodsList.AllowUserToAddRows = False
        Me.dgGoodsList.AllowUserToDeleteRows = False
        Me.dgGoodsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgGoodsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgGoodsList.ContextMenuStrip = Me.cmsEdit
        Me.dgGoodsList.Location = New System.Drawing.Point(12, 30)
        Me.dgGoodsList.Name = "dgGoodsList"
        Me.dgGoodsList.ReadOnly = True
        Me.dgGoodsList.RowTemplate.Height = 24
        Me.dgGoodsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgGoodsList.Size = New System.Drawing.Size(577, 350)
        Me.dgGoodsList.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.新增AToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(601, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '新增AToolStripMenuItem
        '
        Me.新增AToolStripMenuItem.Name = "新增AToolStripMenuItem"
        Me.新增AToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.新增AToolStripMenuItem.Text = "新增(&A)"
        '
        'cmsEdit
        '
        Me.cmsEdit.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.新增CToolStripMenuItem1, Me.修改CToolStripMenuItem, Me.刪除DToolStripMenuItem})
        Me.cmsEdit.Name = "cmsEdit"
        Me.cmsEdit.Size = New System.Drawing.Size(118, 70)
        '
        '新增CToolStripMenuItem1
        '
        Me.新增CToolStripMenuItem1.Name = "新增CToolStripMenuItem1"
        Me.新增CToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
        Me.新增CToolStripMenuItem1.Text = "新增(&A)"
        '
        '修改CToolStripMenuItem
        '
        Me.修改CToolStripMenuItem.Name = "修改CToolStripMenuItem"
        Me.修改CToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.修改CToolStripMenuItem.Text = "修改(&C)"
        '
        '刪除DToolStripMenuItem
        '
        Me.刪除DToolStripMenuItem.Name = "刪除DToolStripMenuItem"
        Me.刪除DToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.刪除DToolStripMenuItem.Text = "刪除(&D)"
        '
        'winGoodsList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(601, 392)
        Me.Controls.Add(Me.dgGoodsList)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "winGoodsList"
        Me.Text = "商品項目清單"
        CType(Me.dgGoodsList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.cmsEdit.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgGoodsList As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 新增AToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsEdit As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 新增CToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 修改CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 刪除DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
