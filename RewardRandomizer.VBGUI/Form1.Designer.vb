﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.InputBox = New System.Windows.Forms.TextBox()
        Me.BrowseInputButton = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ShuffleChests = New System.Windows.Forms.CheckBox()
        Me.ShuffleVillages = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ShuffleStatBoosters = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ShufflePromoItems = New System.Windows.Forms.CheckBox()
        Me.RandomizePromotionItems = New System.Windows.Forms.CheckBox()
        Me.RandomizeStatBoosters = New System.Windows.Forms.CheckBox()
        Me.PatchButton = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.OutputBox = New System.Windows.Forms.TextBox()
        Me.BrowseOutputButton = New System.Windows.Forms.Button()
        Me.PromotionItemsList = New System.Windows.Forms.ListBox()
        Me.StatBoostersList = New System.Windows.Forms.ListBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Input ROM"
        '
        'InputBox
        '
        Me.InputBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InputBox.Location = New System.Drawing.Point(77, 19)
        Me.InputBox.Name = "InputBox"
        Me.InputBox.ReadOnly = True
        Me.InputBox.Size = New System.Drawing.Size(196, 20)
        Me.InputBox.TabIndex = 1
        '
        'BrowseInputButton
        '
        Me.BrowseInputButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BrowseInputButton.Location = New System.Drawing.Point(279, 19)
        Me.BrowseInputButton.Name = "BrowseInputButton"
        Me.BrowseInputButton.Size = New System.Drawing.Size(75, 20)
        Me.BrowseInputButton.TabIndex = 2
        Me.BrowseInputButton.Text = "Browse..."
        Me.BrowseInputButton.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.Location = New System.Drawing.Point(77, 45)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(277, 20)
        Me.TextBox2.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Detected as"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupBox1.Controls.Add(Me.PatchButton)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.OutputBox)
        Me.GroupBox1.Controls.Add(Me.BrowseOutputButton)
        Me.GroupBox1.Controls.Add(Me.RandomizeStatBoosters)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.RandomizePromotionItems)
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.ShuffleStatBoosters)
        Me.GroupBox1.Controls.Add(Me.InputBox)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.ShufflePromoItems)
        Me.GroupBox1.Controls.Add(Me.BrowseInputButton)
        Me.GroupBox1.Controls.Add(Me.ShuffleVillages)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.ShuffleChests)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(360, 387)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Reward Randomizer"
        '
        'ShuffleChests
        '
        Me.ShuffleChests.AutoSize = True
        Me.ShuffleChests.Checked = True
        Me.ShuffleChests.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShuffleChests.Location = New System.Drawing.Point(9, 90)
        Me.ShuffleChests.Name = "ShuffleChests"
        Me.ShuffleChests.Size = New System.Drawing.Size(58, 17)
        Me.ShuffleChests.TabIndex = 0
        Me.ShuffleChests.Text = "Chests"
        Me.ShuffleChests.UseVisualStyleBackColor = True
        '
        'ShuffleVillages
        '
        Me.ShuffleVillages.AutoSize = True
        Me.ShuffleVillages.Checked = True
        Me.ShuffleVillages.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShuffleVillages.Location = New System.Drawing.Point(73, 90)
        Me.ShuffleVillages.Name = "ShuffleVillages"
        Me.ShuffleVillages.Size = New System.Drawing.Size(62, 17)
        Me.ShuffleVillages.TabIndex = 1
        Me.ShuffleVillages.Text = "Villages"
        Me.ShuffleVillages.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 71)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(179, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "First, shuffle all items that come from:"
        '
        'ShuffleStatBoosters
        '
        Me.ShuffleStatBoosters.AutoSize = True
        Me.ShuffleStatBoosters.Checked = True
        Me.ShuffleStatBoosters.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShuffleStatBoosters.Location = New System.Drawing.Point(183, 132)
        Me.ShuffleStatBoosters.Name = "ShuffleStatBoosters"
        Me.ShuffleStatBoosters.Size = New System.Drawing.Size(135, 17)
        Me.ShuffleStatBoosters.TabIndex = 7
        Me.ShuffleStatBoosters.Text = "Shuffle all stat boosters"
        Me.ShuffleStatBoosters.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 113)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Then:"
        '
        'ShufflePromoItems
        '
        Me.ShufflePromoItems.AutoSize = True
        Me.ShufflePromoItems.Checked = True
        Me.ShufflePromoItems.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShufflePromoItems.Location = New System.Drawing.Point(9, 132)
        Me.ShufflePromoItems.Name = "ShufflePromoItems"
        Me.ShufflePromoItems.Size = New System.Drawing.Size(148, 17)
        Me.ShufflePromoItems.TabIndex = 5
        Me.ShufflePromoItems.Text = "Shuffle all promotion items"
        Me.ShufflePromoItems.UseVisualStyleBackColor = True
        '
        'RandomizePromotionItems
        '
        Me.RandomizePromotionItems.AutoSize = True
        Me.RandomizePromotionItems.Location = New System.Drawing.Point(9, 155)
        Me.RandomizePromotionItems.Name = "RandomizePromotionItems"
        Me.RandomizePromotionItems.Size = New System.Drawing.Size(168, 17)
        Me.RandomizePromotionItems.TabIndex = 6
        Me.RandomizePromotionItems.Text = "Randomize all promotion items"
        Me.RandomizePromotionItems.UseVisualStyleBackColor = True
        '
        'RandomizeStatBoosters
        '
        Me.RandomizeStatBoosters.AutoSize = True
        Me.RandomizeStatBoosters.Location = New System.Drawing.Point(183, 155)
        Me.RandomizeStatBoosters.Name = "RandomizeStatBoosters"
        Me.RandomizeStatBoosters.Size = New System.Drawing.Size(155, 17)
        Me.RandomizeStatBoosters.TabIndex = 8
        Me.RandomizeStatBoosters.Text = "Randomize all stat boosters"
        Me.RandomizeStatBoosters.UseVisualStyleBackColor = True
        '
        'PatchButton
        '
        Me.PatchButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PatchButton.Location = New System.Drawing.Point(279, 358)
        Me.PatchButton.Name = "PatchButton"
        Me.PatchButton.Size = New System.Drawing.Size(75, 23)
        Me.PatchButton.TabIndex = 6
        Me.PatchButton.Text = "Patch"
        Me.PatchButton.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.PromotionItemsList)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(168, 142)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Promotion items"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.StatBoostersList)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(177, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(168, 142)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Stat boosters"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 335)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Output ROM"
        '
        'OutputBox
        '
        Me.OutputBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OutputBox.Location = New System.Drawing.Point(77, 332)
        Me.OutputBox.Name = "OutputBox"
        Me.OutputBox.Size = New System.Drawing.Size(196, 20)
        Me.OutputBox.TabIndex = 12
        '
        'BrowseOutputButton
        '
        Me.BrowseOutputButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BrowseOutputButton.Location = New System.Drawing.Point(279, 332)
        Me.BrowseOutputButton.Name = "BrowseOutputButton"
        Me.BrowseOutputButton.Size = New System.Drawing.Size(75, 20)
        Me.BrowseOutputButton.TabIndex = 13
        Me.BrowseOutputButton.Text = "Browse..."
        Me.BrowseOutputButton.UseVisualStyleBackColor = True
        '
        'PromotionItemsList
        '
        Me.PromotionItemsList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PromotionItemsList.Enabled = False
        Me.PromotionItemsList.FormattingEnabled = True
        Me.PromotionItemsList.Location = New System.Drawing.Point(3, 16)
        Me.PromotionItemsList.Name = "PromotionItemsList"
        Me.PromotionItemsList.Size = New System.Drawing.Size(162, 123)
        Me.PromotionItemsList.TabIndex = 0
        '
        'StatBoostersList
        '
        Me.StatBoostersList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StatBoostersList.Enabled = False
        Me.StatBoostersList.FormattingEnabled = True
        Me.StatBoostersList.Location = New System.Drawing.Point(3, 16)
        Me.StatBoostersList.Name = "StatBoostersList"
        Me.StatBoostersList.Size = New System.Drawing.Size(162, 123)
        Me.StatBoostersList.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox3, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(6, 178)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(348, 148)
        Me.TableLayoutPanel1.TabIndex = 14
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 411)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents InputBox As TextBox
    Friend WithEvents BrowseInputButton As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ShuffleVillages As CheckBox
    Friend WithEvents ShuffleChests As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ShuffleStatBoosters As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ShufflePromoItems As CheckBox
    Friend WithEvents RandomizeStatBoosters As CheckBox
    Friend WithEvents RandomizePromotionItems As CheckBox
    Friend WithEvents PatchButton As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents OutputBox As TextBox
    Friend WithEvents BrowseOutputButton As Button
    Friend WithEvents StatBoostersList As ListBox
    Friend WithEvents PromotionItemsList As ListBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
