namespace RewardRandomizer.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open RewardRandomizer.Randomizer
open RewardRandomizer

[<TestClass>]
type IncludeTests() =
    let categoryIn game id =
        game.Items
        |> Seq.where (fun x -> x.Id = id)
        |> Seq.map (fun x -> x.Category)
        |> Seq.exactlyOne

    let shuffleChestsAndVillages = {
        Mode = Shuffle
        Items = AllItems
        Methods = MethodCollection [Chest; Village]
    }

    let shufflePromotionItemsEverywhere = {
        Mode = Shuffle
        Items = ItemCollection.PromotionItems
        Methods = AllMethods
    }

    [<TestMethod>]
    member _.TestFE8PromoItems() =
        let game = Game.FE8_US
        let locations = game.Rewards
        let promotionItemOffsets = ResizeArray [
            for x in locations do
                if x.ItemId |> categoryIn game = Promotion then
                    yield! x.Offsets
        ]
        let remove = promotionItemOffsets.Remove >> ignore
        remove 0x9FAD88
        let mutable loops = 0
        while loops < 10 do
            let operations = Randomizer.GenerateOperations game [shufflePromotionItemsEverywhere]
            for o in operations do
                let expectedItem =
                    locations
                    |> Seq.where (fun x -> x.Offsets = o.Offsets)
                    |> Seq.map (fun x -> x.ItemId)
                    |> Seq.exactlyOne
                if o.WriteData <> expectedItem then
                    for x in o.Offsets do
                        remove x
            loops <- loops + 1
        Assert.IsTrue(Seq.isEmpty promotionItemOffsets, $"Promotion item offset not randomized: {Seq.tryHead promotionItemOffsets}")

    [<TestMethod>]
    member _.TestSequential() =
        let game = Game.FE8_US
        let locations = game.Rewards
        let promotionItemIds =
            game.Items
            |> Seq.where (fun y -> y.Category = Promotion)
            |> Seq.map (fun y -> y.Id)
            |> ResizeArray
        let promotionItemOffsetsInChestsAndVillages =
            locations
            |> Seq.where (fun x -> x.Method = Chest || x.Method = Village)
            |> Seq.where (fun x -> promotionItemIds |> Seq.contains x.ItemId)
            |> Seq.collect (fun x -> x.Offsets)
            |> set
        let otherPromotionItemOffsets =
            locations
            |> Seq.where (fun x -> x.Method <> Chest && x.Method <> Village)
            |> Seq.where (fun x -> promotionItemIds |> Seq.contains x.ItemId)
            |> Seq.collect (fun x -> x.Offsets)
            |> set
        let arr1 = [shuffleChestsAndVillages; shufflePromotionItemsEverywhere]
        let arr2 = List.rev arr1
        for arr in [arr1; arr2] do
            let operations = Randomizer.GenerateOperations game arr
            let multiples =
                operations
                |> Seq.collect (fun x -> x.Offsets)
                |> Seq.groupBy id
                |> Seq.exists (fun (_, g) -> Seq.length g > 1)
            if multiples then
                Assert.Fail("Multiple writes to same index")
            let mutable chestsAndVillages = 0
            let mutable chestsAndVillagesWithPromoItemsStill = 0
            let mutable otherLocations = 0
            let mutable otherLocationsWithPromoItemsStill = 0
            for operation in operations do
                let item =
                    game.Items
                    |> Seq.where (fun x -> x.Id = operation.WriteData)
                    |> Seq.exactlyOne
                for o in operation.Offsets do
                    if Set.contains o promotionItemOffsetsInChestsAndVillages then
                        chestsAndVillages <- chestsAndVillages + 1
                        if item.Category = Promotion then
                            chestsAndVillagesWithPromoItemsStill <- chestsAndVillagesWithPromoItemsStill + 1
                    if Set.contains o otherPromotionItemOffsets then
                        otherLocations <- otherLocations + 1
                        if item.Category = Promotion || item.Category = MasterSeal then
                            otherLocationsWithPromoItemsStill <- otherLocationsWithPromoItemsStill + 1
            if chestsAndVillages = chestsAndVillagesWithPromoItemsStill then
                Assert.Inconclusive()
            if chestsAndVillagesWithPromoItemsStill = 0 then
                Assert.Inconclusive()
            if otherLocations <> otherLocationsWithPromoItemsStill then
                Assert.Inconclusive()
            if otherLocations = 0 then
                Assert.Inconclusive()
