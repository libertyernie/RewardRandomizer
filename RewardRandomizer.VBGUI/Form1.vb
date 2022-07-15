﻿Imports System.IO
Imports RewardRandomizer.Randomizer

Public Class Form1
    Private Function GetGame(data As Byte()) As Game
        For Each g In GameModule.All
            If GameModule.IsCertainlyMatch(data, g) Then
                Return g
            End If
        Next
        For Each g In GameModule.All
            If GameModule.IsProbablyMatch(data, g) Then
                MsgBox("The game could not be determined with absolute certainty (it may have additional patches applied), but all item locations appear to be in the correct place.")
                Return g
            End If
        Next
        MsgBox("The game could not be determined. It may not be supported.")
        Return Nothing
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

        Dim data = File.ReadAllBytes(fileName)

        For Each g As Game In ComboBox1.Items
            If GameModule.IsCertainlyMatch(data, g) Then
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

        For Each x In game.items
            If x.category Is ItemCategory.Promotion Then
                PromotionItemsList.Items.Add(x)
                If game.rewards.Any(Function(y) y.item = x.id) Then
                    PromotionItemsList.SelectedItems.Add(x)
                End If
            ElseIf x.category Is ItemCategory.StatBooster Then
                StatBoostersList.Items.Add(x)
                If game.rewards.Any(Function(y) y.item = x.id) Then
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

                Dim step1 As New List(Of Method)

                If ShuffleChests.Checked Then
                    Console.WriteLine("Shuffling chests")
                    step1.Add(Method.Chest)
                End If

                If ShuffleVillages.Checked Then
                    Console.WriteLine("Shuffling villages")
                    step1.Add(Method.Village)
                End If

                Dim steps As New List(Of RandomizationParameters)

                If step1.Any() Then
                    steps.Add(
                        New RandomizationParameters(
                            Mode.Shuffle,
                            ItemCollection.AllItems,
                            MethodCollection.NewMethodCollection(step1)))
                End If

                Dim selectedPromotionItems = ItemCollection.NewItemCollection(PromotionItemsList.SelectedItems.Cast(Of Item)())
                Dim selectedStatBoosters = ItemCollection.NewItemCollection(StatBoostersList.SelectedItems.Cast(Of Item)())

                If ShufflePromoItems.Checked Then
                    Console.WriteLine("Shuffling promotion items")
                    steps.Add(
                        New RandomizationParameters(
                            Mode.Shuffle,
                            selectedPromotionItems,
                            MethodCollection.AllMethods))
                End If

                If ShuffleStatBoosters.Checked Then
                    Console.WriteLine("Shuffling stat boosters")
                    steps.Add(
                        New RandomizationParameters(
                            Mode.Shuffle,
                            selectedStatBoosters,
                            MethodCollection.AllMethods))
                End If

                If RandomizePromotionItems.Checked Then
                    Console.WriteLine("Randomizing promotion items")
                    steps.Add(
                        New RandomizationParameters(
                            Mode.Randomize,
                            selectedPromotionItems,
                            MethodCollection.AllMethods))
                End If

                If RandomizeStatBoosters.Checked Then
                    Console.WriteLine("Randomizing stat boosters")
                    steps.Add(
                        New RandomizationParameters(
                            Mode.Randomize,
                            selectedStatBoosters,
                            MethodCollection.AllMethods))
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
                        If game.rewards.Any(Function(r) r.offset > oldData.Length OrElse oldData(r.offset) <> r.item) Then
                            MsgBox("Input ROM data does not match the selected game. It may be a different region or have incompatible patches.")
                        Else
                            Dim newData = ApplyOperations(oldData, operations)
                            File.WriteAllBytes(dialog.FileName, newData)
                            MsgBox("Output ROM written.")
                        End If
                    End If
                Else
                    MsgBox("File extension not recognized: " + extension)
                End If
            End If
        End Using
    End Sub
End Class
