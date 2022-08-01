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
    member _.TestAll() =
        for game in Game.All do
            let operations = Randomizer.ChooseRandomItems game {
                Mode = RandomizeLimited
                Items = ItemCollection.PromotionItems
                Methods = AllMethods
            }

            if operations.IsEmpty then Assert.Inconclusive()
