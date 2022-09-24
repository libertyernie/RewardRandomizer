namespace RewardRandomizer.Tests

open System
open System.Diagnostics
open System.IO
open Microsoft.VisualStudio.TestTools.UnitTesting
open RewardRandomizer.Randomizer
open RewardRandomizer

[<TestClass>]
type RouteSplitTests() =
    let getItemName id game =
        game.Items
        |> Seq.where (fun y -> y.Id = id)
        |> Seq.map (fun y -> y.Name)
        |> Seq.tryHead

    [<TestMethod>]
    member _.TestFE6() =
        for game in [Game.FE6_JP; Game.FE6_Localization] do
            let locations = game.Rewards
            let correlations = Correlator.ExtractAll locations
            let correlated = correlations |> Seq.collect id
            let expected_exclusives = [
                (Route.Echidna, "KILLER_BOW") // Chapter 8 (the Restore staff for Bartre's route matches with one on Echidna's chapter 11)
                (Route.Bartre, "BARRIER") // Chapter 10

                (Route.Echidna, "AXEREAVER")
                (Route.Echidna, "SPEEDWING") // Echidna route - Bartre's version requires Lot/Ward (see below)

                (Route.Bartre, "RED_GEM")
                (Route.Bartre, "ENERGY_RING") // For saving civilians

                // Lot and Ward's houses on Bartre route chapter 10
                (Route.Bartre, "ELIXIR")
                (Route.Bartre, "SPEEDWING")
                (Route.Bartre, "DOOR_KEY")
                (Route.Bartre, "SWORDREAVER")

                (Route.Ilia, "SECRET_BOOK")
                (Route.Ilia, "KNIGHT_CREST") // This confuses me
                (Route.Ilia, "AIRCALIBUR") // This makes more sense

                (Route.Ilia, "SPEEDWING")
                (Route.Sacae, "DRAGON_SHIELD")

                (Route.Sacae, "BLUE_GEM") // Ilia's blue gem is stealable from an enemy

                (Route.Ilia, "PURGE")
                (Route.Sacae, "ECLIPSE")

                // Appears twice in Ilia chapter 20 due to different dialogue branches
                (Route.Ilia, "ANGELIC_ROBE")

                (Route.Ilia, "ELYSIAN_WHIP")
                (Route.Sacae, "ORION_BOLT")
            ]
            let leftover =
                locations
                |> Seq.except correlated
                |> ResizeArray
            for (route, name) in expected_exclusives do
                for x in List.ofSeq leftover do
                    let itemName = getItemName x.ItemId game
                    if x.Route = Some route && itemName = Some name then
                        leftover.Remove x |> ignore
            for x in leftover do
                let itemName = getItemName x.ItemId game
                Assert.Fail($"No match found for item: {x} {itemName}")

    [<TestMethod>]
    member _.TestFE7() =
        for game in [Game.FE7_US] do
            let locations = game.Rewards
            let correlations = Correlator.ExtractAll locations
            let correlated = correlations |> Seq.collect id
            let expected_exclusives = [
                0xCCC325 // Eliwood speedwings A
                0xCCC455 // Eliwood speedwings B
                0xCB6124 // Afa's drops - requires tactician
                0xCA4CBC // Kenneth map guiding ring
                0xCA4CC8 // Kenneth map blue gem
                0xCA5034 // Jerme map white gem
                0xCA5040 // Jerme map hero crest
                0xCA504C // Jerme map bolting
                0xCDB081 // Eliwood hard mode orion's bolt in Cog of Destiny
            ]
            let leftover =
                locations
                |> Seq.except correlated
                |> ResizeArray
            for offset in expected_exclusives do
                for x in List.ofSeq leftover do
                    if x.Offsets |> Set.contains offset then
                        leftover.Remove x |> ignore
            let mismatches = [
                for x in leftover do
                    let itemName = getItemName x.ItemId game
                    match (x.Route, itemName) with
                    | (Some EliwoodNormal, Some "ENERGY_RING")
                    | (Some EliwoodHard, Some "ENERGY_RING")
                    | (Some HectorNormal, _)
                    | (Some HectorHard, _) -> ()
                    | _ -> 
                        for o in x.Offsets do
                            yield $"{o:X6} {itemName}"
            ]
            match mismatches with
            | [] -> ()
            | x -> Assert.Fail($"No matches found for: {x}")

    [<TestMethod>]
    member _.TestFE8() =
        for game in [Game.FE8_US] do
            let locations = game.Rewards
            let correlations = Correlator.ExtractAll locations
            let correlated = correlations |> Seq.collect id
            let expected_exclusives = [
                (Route.Eirika, "DRAGONSPEAR") // Ephraim's dragonspear is, I think, droppable on an enemy (not included in this randomzier)
                (Route.Eirika, "SHORT_SPEAR")
                (Route.Eirika, "RAPIER")
                (Route.Ephraim, "TORCH_STAFF")
                (Route.Eirika, "ENERGY_RING") // Eirika has two (Ewan + chapter 14 chest), Ephraim has just one (off an enemy)
                (Route.Ephraim, "KNIGHT_CREST") // Ephraim has two (reward for saving Dussel's cavs + another off Vigarde), Eirika has one (off Aias)
            ]
            let leftover =
                locations
                |> Seq.except correlated
                |> ResizeArray
            for (route, name) in expected_exclusives do
                for x in List.ofSeq leftover do
                    let itemName = getItemName x.ItemId game
                    if x.Route = Some route && itemName = Some name then
                        leftover.Remove x |> ignore
            for x in leftover do
                let itemName = getItemName x.ItemId game
                Assert.Fail($"No match found for item: {x} {itemName}")
