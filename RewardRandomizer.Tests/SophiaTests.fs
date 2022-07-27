namespace RewardRandomizer.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open RewardRandomizer.Randomizer
open RewardRandomizer

[<TestClass>]
type SophiaTests() =
    [<TestMethod>]
    member _.TestSophia() =
        for game in [Game.FE6_JP; Game.FE6_Localization] do
            let parameters1 =
                { Mode = Randomize
                  Items = AllItemsInCategory Promotion
                  Methods = AllMethods }
            let parameters2 =
                { Mode = Randomize
                  Items = AllItems
                  Methods = MethodCollection [Village; Chest; Sand] }
            let operations = Randomizer.GenerateOperations game [parameters1; parameters2]
            let operation =
                operations
                |> Seq.where (fun x -> Set.contains 0x67001C x.Offsets)
                |> Seq.exactlyOne
            let item =
                game.Items
                |> Seq.where (fun x -> x.Id = operation.WriteData)
                |> Seq.exactlyOne
            Assert.AreEqual(Promotion, item.Category, string item)
