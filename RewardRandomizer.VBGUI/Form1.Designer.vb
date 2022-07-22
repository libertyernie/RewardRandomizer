<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.InputBox = New System.Windows.Forms.TextBox()
        Me.BrowseInputButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.RandomizeStatBoosters = New System.Windows.Forms.CheckBox()
        Me.RandomizePromotionItems = New System.Windows.Forms.CheckBox()
        Me.ShuffleStatBoosters = New System.Windows.Forms.CheckBox()
        Me.ShufflePromoItems = New System.Windows.Forms.CheckBox()
        Me.ShuffleVillages = New System.Windows.Forms.CheckBox()
        Me.ShuffleChests = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ShuffleDesert = New System.Windows.Forms.CheckBox()
        Me.ValidateButton = New System.Windows.Forms.Button()
        Me.ExcludeConsumables = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Input ROM"
        '
        'InputBox
        '
        Me.InputBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InputBox.Location = New System.Drawing.Point(77, 39)
        Me.InputBox.Name = "InputBox"
        Me.InputBox.ReadOnly = True
        Me.InputBox.Size = New System.Drawing.Size(158, 20)
        Me.InputBox.TabIndex = 3
        '
        'BrowseInputButton
        '
        Me.BrowseInputButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BrowseInputButton.Location = New System.Drawing.Point(241, 39)
        Me.BrowseInputButton.Name = "BrowseInputButton"
        Me.BrowseInputButton.Size = New System.Drawing.Size(75, 20)
        Me.BrowseInputButton.TabIndex = 4
        Me.BrowseInputButton.Text = "Browse..."
        Me.BrowseInputButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Game"
        '
        'SaveButton
        '
        Me.SaveButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveButton.Location = New System.Drawing.Point(322, 206)
        Me.SaveButton.Margin = New System.Windows.Forms.Padding(3, 13, 3, 3)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(75, 23)
        Me.SaveButton.TabIndex = 8
        Me.SaveButton.Text = "Apply"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'RandomizeStatBoosters
        '
        Me.RandomizeStatBoosters.AutoSize = True
        Me.RandomizeStatBoosters.Location = New System.Drawing.Point(173, 98)
        Me.RandomizeStatBoosters.Name = "RandomizeStatBoosters"
        Me.RandomizeStatBoosters.Size = New System.Drawing.Size(142, 17)
        Me.RandomizeStatBoosters.TabIndex = 15
        Me.RandomizeStatBoosters.Text = "Randomize stat boosters"
        Me.RandomizeStatBoosters.UseVisualStyleBackColor = True
        '
        'RandomizePromotionItems
        '
        Me.RandomizePromotionItems.AutoSize = True
        Me.RandomizePromotionItems.Location = New System.Drawing.Point(12, 98)
        Me.RandomizePromotionItems.Name = "RandomizePromotionItems"
        Me.RandomizePromotionItems.Size = New System.Drawing.Size(155, 17)
        Me.RandomizePromotionItems.TabIndex = 13
        Me.RandomizePromotionItems.Text = "Randomize promotion items"
        Me.RandomizePromotionItems.UseVisualStyleBackColor = True
        '
        'ShuffleStatBoosters
        '
        Me.ShuffleStatBoosters.AutoSize = True
        Me.ShuffleStatBoosters.Checked = True
        Me.ShuffleStatBoosters.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShuffleStatBoosters.Location = New System.Drawing.Point(173, 75)
        Me.ShuffleStatBoosters.Name = "ShuffleStatBoosters"
        Me.ShuffleStatBoosters.Size = New System.Drawing.Size(122, 17)
        Me.ShuffleStatBoosters.TabIndex = 14
        Me.ShuffleStatBoosters.Text = "Shuffle stat boosters"
        Me.ShuffleStatBoosters.UseVisualStyleBackColor = True
        '
        'ShufflePromoItems
        '
        Me.ShufflePromoItems.AutoSize = True
        Me.ShufflePromoItems.Checked = True
        Me.ShufflePromoItems.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShufflePromoItems.Location = New System.Drawing.Point(12, 75)
        Me.ShufflePromoItems.Margin = New System.Windows.Forms.Padding(3, 13, 3, 3)
        Me.ShufflePromoItems.Name = "ShufflePromoItems"
        Me.ShufflePromoItems.Size = New System.Drawing.Size(135, 17)
        Me.ShufflePromoItems.TabIndex = 12
        Me.ShufflePromoItems.Text = "Shuffle promotion items"
        Me.ShufflePromoItems.UseVisualStyleBackColor = True
        '
        'ShuffleVillages
        '
        Me.ShuffleVillages.AutoSize = True
        Me.ShuffleVillages.Checked = True
        Me.ShuffleVillages.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShuffleVillages.Location = New System.Drawing.Point(76, 150)
        Me.ShuffleVillages.Name = "ShuffleVillages"
        Me.ShuffleVillages.Size = New System.Drawing.Size(62, 17)
        Me.ShuffleVillages.TabIndex = 8
        Me.ShuffleVillages.Text = "Villages"
        Me.ShuffleVillages.UseVisualStyleBackColor = True
        '
        'ShuffleChests
        '
        Me.ShuffleChests.AutoSize = True
        Me.ShuffleChests.Checked = True
        Me.ShuffleChests.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShuffleChests.Location = New System.Drawing.Point(12, 150)
        Me.ShuffleChests.Name = "ShuffleChests"
        Me.ShuffleChests.Size = New System.Drawing.Size(58, 17)
        Me.ShuffleChests.TabIndex = 7
        Me.ShuffleChests.Text = "Chests"
        Me.ShuffleChests.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 251)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Version 1.0.0"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label7.Location = New System.Drawing.Point(12, 264)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(248, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "https://github.com/libertyernie/RewardRandomizer"
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.DisplayMember = "Name"
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(77, 12)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(320, 21)
        Me.ComboBox1.TabIndex = 1
        '
        'ShuffleDesert
        '
        Me.ShuffleDesert.AutoSize = True
        Me.ShuffleDesert.Checked = True
        Me.ShuffleDesert.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShuffleDesert.Location = New System.Drawing.Point(144, 150)
        Me.ShuffleDesert.Name = "ShuffleDesert"
        Me.ShuffleDesert.Size = New System.Drawing.Size(84, 17)
        Me.ShuffleDesert.TabIndex = 9
        Me.ShuffleDesert.Text = "Desert items"
        Me.ShuffleDesert.UseVisualStyleBackColor = True
        '
        'ValidateButton
        '
        Me.ValidateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ValidateButton.Location = New System.Drawing.Point(322, 39)
        Me.ValidateButton.Name = "ValidateButton"
        Me.ValidateButton.Size = New System.Drawing.Size(75, 20)
        Me.ValidateButton.TabIndex = 5
        Me.ValidateButton.Text = "Validate"
        Me.ValidateButton.UseVisualStyleBackColor = True
        '
        'ExcludeConsumables
        '
        Me.ExcludeConsumables.AutoSize = True
        Me.ExcludeConsumables.Checked = True
        Me.ExcludeConsumables.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ExcludeConsumables.Location = New System.Drawing.Point(12, 173)
        Me.ExcludeConsumables.Name = "ExcludeConsumables"
        Me.ExcludeConsumables.Size = New System.Drawing.Size(276, 17)
        Me.ExcludeConsumables.TabIndex = 10
        Me.ExcludeConsumables.Text = "Exclude vulneraries, elixirs, pure water, and antitoxins"
        Me.ExcludeConsumables.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 131)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 13, 3, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Then, shuffle all:"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(409, 286)
        Me.Controls.Add(Me.ExcludeConsumables)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ShuffleDesert)
        Me.Controls.Add(Me.ShufflePromoItems)
        Me.Controls.Add(Me.ShuffleVillages)
        Me.Controls.Add(Me.ShuffleStatBoosters)
        Me.Controls.Add(Me.ShuffleChests)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.RandomizePromotionItems)
        Me.Controls.Add(Me.RandomizeStatBoosters)
        Me.Controls.Add(Me.InputBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ValidateButton)
        Me.Controls.Add(Me.BrowseInputButton)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.SaveButton)
        Me.Name = "Form1"
        Me.Text = "Reward Randomizer"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents InputBox As TextBox
    Friend WithEvents BrowseInputButton As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents ShuffleVillages As CheckBox
    Friend WithEvents ShuffleChests As CheckBox
    Friend WithEvents ShuffleStatBoosters As CheckBox
    Friend WithEvents ShufflePromoItems As CheckBox
    Friend WithEvents RandomizeStatBoosters As CheckBox
    Friend WithEvents RandomizePromotionItems As CheckBox
    Friend WithEvents SaveButton As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents ShuffleDesert As CheckBox
    Friend WithEvents ValidateButton As Button
    Friend WithEvents ExcludeConsumables As CheckBox
    Friend WithEvents Label3 As Label
End Class
