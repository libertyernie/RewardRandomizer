Imports System.IO
Imports RewardRandomizer.Randomizer

Public Class Form1
    Private InputData As Byte() = Nothing

    Private Function InputDataMatchesGame(g As Game) As Boolean
        For Each r In g.Rewards
            For Each x In r.Offsets
                If x >= InputData.Length Then Return False
                If InputData(x) <> r.ItemId Then Return False
            Next
        Next
        Return True
    End Function

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        For Each g In GameModule.All
            ComboBox1.Items.Add(g)
        Next

        Dim arg1 = My.Application.CommandLineArgs.DefaultIfEmpty(Nothing).First()
        If arg1 IsNot Nothing And File.Exists(arg1) Then
            LoadFile(arg1)
        End If
    End Sub

    Private Sub BrowseInputButton_Click(sender As Object, e As EventArgs) Handles BrowseInputButton.Click
        Using dialog As New OpenFileDialog
            dialog.Filter = "GBA ROM images (*.gba)|*.gba|All files (*.*)|*.*"
            If dialog.ShowDialog(Me) = DialogResult.OK Then
                LoadFile(dialog.FileName)
            End If
        End Using
    End Sub

    Private Sub LoadFile(fileName As String)
        InputBox.Text = fileName

        InputData = File.ReadAllBytes(fileName)

        For Each g As Game In ComboBox1.Items
            If InputDataMatchesGame(g) Then
                ComboBox1.SelectedItem = g
                Exit Sub
            End If
        Next
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Using dialog As New SaveFileDialog
            If File.Exists(InputBox.Text) Then
                dialog.Filter = "GBA ROM images (*.gba)|*.gba|IPS patches (*.ips)|*.ips|All files (*.*)|*.*"
            Else
                dialog.Filter = "IPS patches (*.ips)|*.ips|All files (*.*)|*.*"
            End If
            If dialog.ShowDialog(Me) = DialogResult.OK Then
                Dim game = TryCast(ComboBox1.SelectedItem, Game)
                If game Is Nothing Then
                    Exit Sub
                End If

                Dim steps As New List(Of RandomizationParameters)

                If ShufflePromoItems.Checked Then
                    steps.Add(
                        New RandomizationParameters(
                            Mode.Shuffle,
                            ItemCollection.NewAllItemsInCategory(ItemCategory.Promotion),
                            MethodCollection.AllMethods))
                End If

                If ShuffleStatBoosters.Checked Then
                    steps.Add(
                        New RandomizationParameters(
                            Mode.Shuffle,
                            ItemCollection.NewAllItemsInCategory(ItemCategory.StatBooster),
                            MethodCollection.AllMethods))
                End If

                If RandomizePromotionItems.Checked Then
                    steps.Add(
                        New RandomizationParameters(
                            Mode.Randomize,
                            ItemCollection.NewAllItemsInCategory(ItemCategory.Promotion),
                            MethodCollection.AllMethods))
                End If

                If RandomizeStatBoosters.Checked Then
                    steps.Add(
                        New RandomizationParameters(
                            Mode.Randomize,
                            ItemCollection.NewAllItemsInCategory(ItemCategory.StatBooster),
                            MethodCollection.AllMethods))
                End If

                Dim step3 As New List(Of Method)

                If ShuffleChests.Checked Then
                    step3.Add(Method.Chest)
                End If

                If ShuffleVillages.Checked Then
                    step3.Add(Method.Village)
                End If

                If ShuffleDesert.Checked Then
                    step3.Add(Method.Sand)
                End If

                If step3.Any() Then
                    steps.Add(
                        New RandomizationParameters(
                            Mode.Shuffle,
                            If(ExcludeConsumables.Checked,
                                ItemCollection.NewAllItemsNotInCategory(ItemCategory.Consumable),
                                ItemCollection.AllItems),
                            MethodCollection.NewMethodCollection(step3)))
                End If

                Dim operations = GenerateOperations(game, steps)

                Dim extension = Path.GetExtension(dialog.FileName).ToLowerInvariant()

                If extension = ".ips" Then
                    File.WriteAllBytes(dialog.FileName, CreateIPS(operations))
                    MsgBox("Patch file written.")
                    Exit Sub
                End If

                If extension = ".gba" Then
                    If Not File.Exists(InputBox.Text) Then
                        MsgBox("Input ROM not found. (Maybe you want to save an IPS patch instead?)")
                        Exit Sub
                    End If

                    If Not InputDataMatchesGame(game) Then
                        MsgBox("Input ROM data does not match the selected game. It may be a different region or have incompatible patches.")
                        Exit Sub
                    End If

                    Dim newData = ApplyOperations(InputData, operations)
                    File.WriteAllBytes(dialog.FileName, newData)
                    MsgBox("Output ROM written.")
                    Exit Sub
                End If

                MsgBox("File extension not recognized: " + extension)
            End If
        End Using
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        If sender.Text.StartsWith("https://") Then
            Process.Start(sender.Text)
        End If
    End Sub

    Private Sub ValidateButton_Click(sender As Object, e As EventArgs) Handles ValidateButton.Click
        Dim game = TryCast(ComboBox1.SelectedItem, Game)
        If game Is Nothing Then
            MsgBox("No game is selected.")
            Exit Sub
        End If

        If Not File.Exists(InputBox.Text) Then
            MsgBox("No ROM image has been selected to validate.")
            Exit Sub
        End If

        Using summaryDialog As New Form
            Using textbox As New TextBox
                textbox.Dock = DockStyle.Fill
                textbox.ScrollBars = ScrollBars.Both
                textbox.Text = GameModule.SummarizeDifferences(game, InputData)
                textbox.ReadOnly = True
                textbox.Multiline = True
                summaryDialog.Controls.Add(textbox)
                summaryDialog.ShowDialog(Me)
            End Using
        End Using
    End Sub
End Class
