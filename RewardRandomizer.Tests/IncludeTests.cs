using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RewardRandomizer.Tests
{
    [TestClass]
    public class IncludeTests
    {
        private static readonly Randomizer.RandomizationParameters ShuffleChestsAndVillages = new(
            Randomizer.Mode.Shuffle,
            Randomizer.ItemCollection.AllItems,
            Randomizer.MethodCollection.NewMethodCollection(new[] { Method.Chest, Method.Village }));

        private static readonly Randomizer.RandomizationParameters ShufflePromotionItemsEverywhere = new(
            Randomizer.Mode.Shuffle,
            Randomizer.ItemCollection.NewAllItemsInCategory(ItemCategory.Promotion),
            Randomizer.MethodCollection.AllMethods);

        [TestMethod]
        public void TestFE8PromoItems()
        {
            var game = GameModule.FE8_US;
            var locations = game.Rewards;
            var promotionItemOffsets = locations
                .Where(x => game.Items.Any(y => y.Id == x.ItemId && y.Category.IsPromotion))
                .SelectMany(x => x.Offsets)
                .Except(new[] { 0x9FAD88 }) // Extra knight crest on Ephraim's route
                .ToList();
            for (int i = 0; i < 10; i++)
            {
                var operations = Randomizer.GenerateOperations(game, new[] { ShufflePromotionItemsEverywhere });
                foreach (var o in operations)
                {
                    int expectedItem = locations
                        .Where(x => x.Offsets.Contains(o.Offset))
                        .Select(x => x.ItemId)
                        .Single();
                    if (o.WriteData.Single() != expectedItem)
                        promotionItemOffsets.Remove(o.Offset);
                }
                if (promotionItemOffsets.Count == 0)
                    break;
            }
            if (promotionItemOffsets.Any())
                Assert.Fail($"Promotion item offset not randomized: {promotionItemOffsets[0]:X6}");
        }

        [TestMethod]
        public void TestSequential()
        {
            var game = GameModule.FE8_US;
            var locations = game.Rewards;
            var promotionItemIds = game.Items
                .Where(y => y.Category.IsPromotion)
                .Select(y => y.Id)
                .ToHashSet();
            var promotionItemOffsetsInChestsAndVillages = locations
                .Where(x => x.Method.IsChest || x.Method.IsVillage)
                .Where(x => promotionItemIds.Contains(x.ItemId))
                .SelectMany(x => x.Offsets)
                .ToHashSet();
            var otherPromotionItemOffsets = locations
                .Where(x => !x.Method.IsChest && !x.Method.IsVillage)
                .Where(x => promotionItemIds.Contains(x.ItemId))
                .SelectMany(x => x.Offsets)
                .ToHashSet();
            var arr1 = new[] { ShuffleChestsAndVillages, ShufflePromotionItemsEverywhere };
            var arr2 = new[] { ShufflePromotionItemsEverywhere, ShuffleChestsAndVillages };
            foreach (var arr in new[] { arr1, arr2 })
            {
                var generatedOperations = Randomizer.GenerateOperations(game, arr);
                if (generatedOperations.GroupBy(x => x.Offset).Any(g => g.Count() > 1))
                    Assert.Fail("Multiple writes to same index");
                if (!generatedOperations.All(x => x.WriteData.Length == 1))
                    Assert.Inconclusive("Writes of lengths other than one byte");
                var operations = generatedOperations
                    .GroupBy(x => x.Offset)
                    .Select(g => g.Last());
                int previousPromoItemChestsAndVillages = 0;
                int previousPromoItemChestsAndVillagesWithPromoItemsStill = 0;
                int previousPromoItemOtherLocations = 0;
                int previousPromoItemOtherLocationsWithPromoItemsStill = 0;
                foreach (var operation in operations)
                {
                    var item = game.Items.Single(x => x.Id == operation.WriteData.Single());
                    if (promotionItemOffsetsInChestsAndVillages.Contains(operation.Offset))
                    {
                        previousPromoItemChestsAndVillages++;
                        if (item.Category.IsPromotion)
                            previousPromoItemChestsAndVillagesWithPromoItemsStill++;
                    }
                    if (otherPromotionItemOffsets.Contains(operation.Offset))
                    {
                        previousPromoItemOtherLocations++;
                        if (item.Category.IsPromotion)
                            previousPromoItemOtherLocationsWithPromoItemsStill++;
                    }
                }
                if (previousPromoItemOtherLocations != previousPromoItemOtherLocationsWithPromoItemsStill)
                    Assert.Inconclusive();
                if (previousPromoItemOtherLocations == 0)
                    Assert.Inconclusive();
                if (previousPromoItemChestsAndVillages == previousPromoItemChestsAndVillagesWithPromoItemsStill)
                    Assert.Inconclusive();
                if (previousPromoItemChestsAndVillagesWithPromoItemsStill == 0)
                    Assert.Inconclusive();
            }
        }
    }
}