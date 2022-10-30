namespace RewardRandomizer.Tests

open System
open System.Diagnostics
open System.IO
open Microsoft.VisualStudio.TestTools.UnitTesting
open RewardRandomizer.Randomizer
open RewardRandomizer

[<TestClass>]
type RandomizeLimitedTests() =
    [<TestMethod>]
    member _.TestFE6() =
        for game in [Game.FE6_JP; Game.FE6_Localization] do
            let operations = Randomizer.ChooseRandomItems game {
                Mode = RandomizeLimited
                Items = ItemCollection.PromotionItems
                Methods = AllMethods
            }

            let countItem id = operations |> Seq.where (fun o -> o.WriteData = id) |> Seq.length

            let assertMax max actual = Assert.IsTrue(actual <= max, $"Actual: {actual}")

            let hero_crests = countItem 0x5Fuy
            assertMax 8 hero_crests

            let knight_crests = countItem 0x60uy
            assertMax 7 knight_crests

            let orion_bolts = countItem 0x61uy
            assertMax 4 orion_bolts

            let elysian_whips = countItem 0x62uy
            assertMax 4 elysian_whips

            let guiding_rings = countItem 0x63uy
            assertMax 8 guiding_rings

    [<TestMethod>]
    member _.TestSand() =
        for game in [Game.FE6_JP; Game.FE6_Localization; Game.FE8_US] do
            let operations = Randomizer.ChooseRandomItems game {
                Mode = Shuffle
                Items = AllItems
                Methods = MethodCollection [Chest; Village]
            }

            let silverCardId =
                game.Items
                |> Seq.where (fun x -> x.Name = "SILVER_CARD")
                |> Seq.map (fun x -> x.Id)
                |> Seq.head

            Assert.IsFalse(operations |> Seq.exists (fun x -> x.WriteData = silverCardId))

    [<TestMethod>]
    member _.TestAngelicRobe() =
        for game in [Game.FE6_JP; Game.FE6_Localization; Game.FE7_US; Game.FE8_US] do
            let angelicRobe =
                game.Items
                |> Seq.where (fun x -> x.Name = "ANGELIC_ROBE")
                |> Seq.map (fun x -> x.Id)
                |> Seq.head

            let operations1 = Randomizer.ChooseRandomItems game {
                Mode = Shuffle
                Items = AllItems
                Methods = AllMethods
            }

            Assert.IsTrue(operations1 |> Seq.exists (fun x -> x.WriteData = angelicRobe))

            let operations2 = Randomizer.ChooseRandomItems game {
                Mode = Shuffle
                Items = ItemCollection.StatBoosters
                Methods = AllMethods
            }

            Assert.IsTrue(operations2 |> Seq.exists (fun x -> x.WriteData = angelicRobe))

            let operations3 = Randomizer.ChooseRandomItems game {
                Mode = Shuffle
                Items = AllItemsInCategories [StatBooster; RareStatBooster]
                Methods = AllMethods
            }

            Assert.IsFalse(operations3 |> Seq.exists (fun x -> x.WriteData = angelicRobe))

    [<TestMethod>]
    member _.TestAll() =
        for game in Game.All do
            let operations = Randomizer.ChooseRandomItems game {
                Mode = RandomizeLimited
                Items = ItemCollection.PromotionItems
                Methods = AllMethods
            }

            if operations.IsEmpty then Assert.Inconclusive()
