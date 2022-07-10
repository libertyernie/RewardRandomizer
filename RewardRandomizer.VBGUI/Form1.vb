Imports System.IO

Public Class Form1
    Private RomData As Byte()
    Private Game As Game

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
                RomData = data
                Game = GetGame(data)

                TextBox2.Name = Game.name
                PromotionItemsList.Items.Clear()
                StatBoostersList.Items.Clear()
                For Each x In Game.items
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
        Dim path = OutputBox.Text

        Dim newData = RomData.ToArray()

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
                Game,
                Procedure.CategoryCollection.AllCategories,
                Procedure.MethodCollection.NewMethodCollection(step1),
                newData)
        End If

        Dim step2 As New List(Of ItemCategory)
        If ShufflePromoItems.Checked Then
            step2.Add(ItemCategory.Promotion)
        End If
        If ShuffleStatBoosters.Checked Then
            step2.Add(ItemCategory.StatBooster)
        End If

        If step2.Any() Then
            Procedure.Run(
                Procedure.Mode.Shuffle,
                Game,
                Procedure.CategoryCollection.NewCategoryCollection(step2),
                Procedure.MethodCollection.AllMethods,
                newData)
        End If

        Dim step3 As New List(Of ItemCategory)
        If RandomizePromotionItems.Checked Then
            step3.Add(ItemCategory.Promotion)
        End If
        If RandomizeStatBoosters.Checked Then
            step3.Add(ItemCategory.StatBooster)
        End If

        If step3.Any() Then
            Procedure.Run(
                Procedure.Mode.Randomize,
                Game,
                Procedure.CategoryCollection.NewCategoryCollection(step3),
                Procedure.MethodCollection.AllMethods,
                newData)
        End If

        If File.Exists(path) Then
            If MsgBox("Are you sure you want to overwrite this file?", MsgBoxStyle.YesNo) = DialogResult.No Then
                Return
            End If
        End If

        File.WriteAllBytes(path, newData)

        MsgBox("Output file written.")
    End Sub
End Class
