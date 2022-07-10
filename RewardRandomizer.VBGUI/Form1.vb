Imports System.IO

Public Class Form1
    Private Shared Function IsGame(data As Byte(), candidate As Game) As Boolean
        For Each itemLocation In candidate.locations
            If itemLocation.offset < data.Length Then
                If data(itemLocation.offset) <> itemLocation.item Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Private Shared Function GetGame(data As Byte()) As Game
        For Each candidate In GameModule.All
            If IsGame(data, candidate) Then
                Return candidate
            End If
        Next
        Return Nothing
    End Function

    Private Sub BrowseInputButton_Click(sender As Object, e As EventArgs) Handles BrowseInputButton.Click
        Using dialog As New OpenFileDialog
            dialog.Filter = "GBA ROM images (*.gba)|*.gba"
            If dialog.ShowDialog(Me) = DialogResult.OK Then
                InputBox.Text = dialog.FileName

                Dim data = File.ReadAllBytes(dialog.FileName)
                Dim game = GetGame(data)

                TextBox2.Text = game.name
                PromotionItemsList.Items.Clear()
                StatBoostersList.Items.Clear()
                For Each x In game.items
                    If x.category Is ItemCategory.Promotion Then
                        PromotionItemsList.Items.Add(x.name)
                    ElseIf x.category Is ItemCategory.StatBooster Then
                        StatBoostersList.Items.Add(x.name)
                    End If
                Next
            End If
        End Using
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
            step1.Add(Method.Chest)
        End If

        If ShuffleVillages.Checked Then
            step1.Add(Method.Village)
        End If

        If step1.Any() Then
            Procedure.Run(
                Procedure.Mode.Shuffle,
                game,
                Procedure.CategoryCollection.AllCategories,
                Procedure.MethodCollection.NewMethodCollection(step1),
                newData)
        End If

        If ShufflePromoItems.Checked Then
            Procedure.Run(
                Procedure.Mode.Shuffle,
                game,
                Procedure.CategoryCollection.NewCategoryCollection(New ItemCategory() {ItemCategory.Promotion}),
                Procedure.MethodCollection.AllMethods,
                newData)
        End If

        If ShuffleStatBoosters.Checked Then
            Procedure.Run(
                Procedure.Mode.Shuffle,
                game,
                Procedure.CategoryCollection.NewCategoryCollection(New ItemCategory() {ItemCategory.StatBooster}),
                Procedure.MethodCollection.AllMethods,
                newData)
        End If

        If RandomizePromotionItems.Checked Then
            Procedure.Run(
                Procedure.Mode.Randomize,
                game,
                Procedure.CategoryCollection.NewCategoryCollection(New ItemCategory() {ItemCategory.Promotion}),
                Procedure.MethodCollection.AllMethods,
                newData)
        End If

        If RandomizeStatBoosters.Checked Then
            Procedure.Run(
                Procedure.Mode.Randomize,
                game,
                Procedure.CategoryCollection.NewCategoryCollection(New ItemCategory() {ItemCategory.StatBooster}),
                Procedure.MethodCollection.AllMethods,
                newData)
        End If

        File.WriteAllBytes(path, newData)

        MsgBox("Output file written.")
    End Sub
End Class
