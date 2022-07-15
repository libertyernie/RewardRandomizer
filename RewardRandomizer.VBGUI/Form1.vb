Imports System.IO

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
        Dim arg1 = My.Application.CommandLineArgs.DefaultIfEmpty(Nothing).First()
        If arg1 IsNot Nothing And File.Exists(arg1) Then
            LoadFile(arg1)
        End If
    End Sub

    Private Sub BrowseInputButton_Click(sender As Object, e As EventArgs) Handles BrowseInputButton.Click
        Using dialog As New OpenFileDialog
            dialog.Filter = "GBA ROM images (*.gba)|*.gba"
            If dialog.ShowDialog(Me) = DialogResult.OK Then
                LoadFile(dialog.FileName)
            End If
        End Using
    End Sub

    Private Sub LoadFile(fileName As String)
        Dim data = File.ReadAllBytes(fileName)
        Dim game = GetGame(data)

        PromotionItemsList.Items.Clear()
        StatBoostersList.Items.Clear()

        If game Is Nothing Then
            InputBox.Text = ""
            TextBox2.Text = ""
        Else
            InputBox.Text = fileName
            TextBox2.Text = game.name
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
        End If
    End Sub

    Private Sub BrowseOutputButton_Click(sender As Object, e As EventArgs) Handles BrowseOutputButton.Click
        Using dialog As New SaveFileDialog
            dialog.Filter = "GBA ROM images (*.gba)|*.gba"
            If dialog.ShowDialog(Me) = DialogResult.OK Then
                OutputBox.Text = dialog.FileName
            End If
        End Using
    End Sub

    Private Sub PatchButton_Click(sender As Object, e As EventArgs) Handles PatchButton.Click
        Dim newData = File.ReadAllBytes(InputBox.Text)
        Dim game = GetGame(newData)

        Dim path = OutputBox.Text

        Dim step1 As New List(Of Method)

        If ShuffleChests.Checked Then
            Console.WriteLine("Shuffling chests")
            step1.Add(Method.Chest)
        End If

        If ShuffleVillages.Checked Then
            Console.WriteLine("Shuffling villages")
            step1.Add(Method.Village)
        End If

        If step1.Any() Then
            Randomizer.Run(
                Randomizer.Mode.Shuffle,
                game,
                Randomizer.ItemCollection.AllItems,
                Randomizer.MethodCollection.NewMethodCollection(step1),
                newData)
        End If

        Dim selectedPromotionItems = Randomizer.ItemCollection.NewItemCollection(PromotionItemsList.SelectedItems.Cast(Of Item)())
        Dim selectedStatBoosters = Randomizer.ItemCollection.NewItemCollection(StatBoostersList.SelectedItems.Cast(Of Item)())

        If ShufflePromoItems.Checked Then
            Console.WriteLine("Shuffling promotion items")
            Randomizer.Run(
                Randomizer.Mode.Shuffle,
                game,
                selectedPromotionItems,
                Randomizer.MethodCollection.AllMethods,
                newData)
        End If

        If ShuffleStatBoosters.Checked Then
            Console.WriteLine("Shuffling stat boosters")
            Randomizer.Run(
                Randomizer.Mode.Shuffle,
                game,
                selectedStatBoosters,
                Randomizer.MethodCollection.AllMethods,
                newData)
        End If

        If RandomizePromotionItems.Checked Then
            Console.WriteLine("Randomizing: promotion items")
            Randomizer.Run(
                Randomizer.Mode.Randomize,
                game,
                selectedPromotionItems,
                Randomizer.MethodCollection.AllMethods,
                newData)
        End If

        If RandomizeStatBoosters.Checked Then
            Console.WriteLine("Randomizing stat boosters")
            Randomizer.Run(
                Randomizer.Mode.Randomize,
                game,
                selectedStatBoosters,
                Randomizer.MethodCollection.AllMethods,
                newData)
        End If

        File.WriteAllBytes(path, newData)

        MsgBox("Output file written.")
    End Sub
End Class
