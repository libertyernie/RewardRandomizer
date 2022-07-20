Imports System.IO
Imports RewardRandomizer.Randomizer

Public Class Form1
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

        Dim data = File.ReadAllBytes(fileName)

        For Each g As Game In ComboBox1.Items
            If g.Rewards.All(Function(r)
                                 For Each x In r.Offsets
                                     If x >= data.Length Then Return False
                                     If data(x) <> r.ItemId Then Return False
                                 Next
                                 Return True
                             End Function) Then
                ComboBox1.SelectedItem = g
                Exit Sub
            End If
        Next

        PromotionItemsList.Items.Clear()
        StatBoostersList.Items.Clear()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        PromotionItemsList.Items.Clear()
        StatBoostersList.Items.Clear()

        Dim game = TryCast(ComboBox1.SelectedItem, Game)
        If game Is Nothing Then
            Exit Sub
        End If

        For Each x In game.Items
            If x.Category Is ItemCategory.Promotion Then
                PromotionItemsList.Items.Add(x)
                If Correlator.IsItemRandomizable(x.Id, game.Rewards) Then
                    PromotionItemsList.SelectedItems.Add(x)
                End If
            ElseIf x.Category Is ItemCategory.StatBooster Then
                StatBoostersList.Items.Add(x)
                If Correlator.IsItemRandomizable(x.Id, game.Rewards) Then
                    StatBoostersList.SelectedItems.Add(x)
                End If
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
                    Console.WriteLine("Shuffling promotion items")
                    steps.Add(
                        New RandomizationParameters(
                            Mode.Shuffle,
                            ItemCollection.NewAllItemsInCategory(ItemCategory.Promotion),
                            MethodCollection.AllMethods))
                End If

                If ShuffleStatBoosters.Checked Then
                    Console.WriteLine("Shuffling stat boosters")
                    steps.Add(
                        New RandomizationParameters(
                            Mode.Shuffle,
                            ItemCollection.NewAllItemsInCategory(ItemCategory.StatBooster),
                            MethodCollection.AllMethods))
                End If

                If RandomizePromotionItems.Checked Then
                    Console.WriteLine("Randomizing promotion items")
                    steps.Add(
                        New RandomizationParameters(
                            Mode.Randomize,
                            ItemCollection.NewAllItemsInCategory(ItemCategory.Promotion),
                            MethodCollection.AllMethods))
                End If

                If RandomizeStatBoosters.Checked Then
                    Console.WriteLine("Randomizing stat boosters")
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
                ElseIf extension = ".gba" Then
                    If Not File.Exists(InputBox.Text) Then
                        MsgBox("Input ROM not found. (Maybe you want to save an IPS patch instead?)")
                    Else
                        Dim oldData = File.ReadAllBytes(InputBox.Text)
                        For Each r In game.Rewards
                            For Each x In r.Offsets
                                If x >= oldData.Length Or oldData(x) <> r.ItemId Then
                                    MsgBox("Input ROM data does not match the selected game. It may be a different region or have incompatible patches.")
                                    Return
                                End If
                            Next
                        Next
                        Dim newData = ApplyOperations(oldData, operations)
                        File.WriteAllBytes(dialog.FileName, newData)
                        MsgBox("Output ROM written.")

                        Using summaryDialog As New Form
                            Using textbox As New TextBox
                                textbox.Dock = DockStyle.Fill
                                textbox.ScrollBars = ScrollBars.Both
                                textbox.Text = GameModule.SummarizeDifferences(game, newData)
                                textbox.ReadOnly = True
                                textbox.Multiline = True
                                summaryDialog.Controls.Add(textbox)
                                summaryDialog.ShowDialog(Me)
                            End Using
                        End Using
                    End If
                Else
                    MsgBox("File extension not recognized: " + extension)
                End If
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
            Exit Sub
        End If

        Dim oldData = File.ReadAllBytes(InputBox.Text)

        Using summaryDialog As New Form
            Using textbox As New TextBox
                textbox.Dock = DockStyle.Fill
                textbox.ScrollBars = ScrollBars.Both
                textbox.Text = GameModule.SummarizeDifferences(game, oldData)
                textbox.ReadOnly = True
                textbox.Multiline = True
                summaryDialog.Controls.Add(textbox)
                summaryDialog.ShowDialog(Me)
            End Using
        End Using
    End Sub
End Class
