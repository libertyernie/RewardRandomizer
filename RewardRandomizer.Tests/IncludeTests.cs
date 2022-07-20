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
                        .Where(x => x.Offsets.Equals(o.Offsets))
                        .Select(x => x.ItemId)
                        .Single();
                    if (o.WriteData != expectedItem)
                        foreach (int x in o.Offsets)
                            promotionItemOffsets.Remove(x);
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
                var operations = Randomizer.GenerateOperations(game, arr);
                if (operations.SelectMany(x => x.Offsets).GroupBy(x => x).Any(g => g.Count() > 1))
                    Assert.Fail("Multiple writes to same index");
                int ChestsAndVillages = 0;
                int ChestsAndVillagesWithPromoItemsStill = 0;
                int OtherLocations = 0;
                int OtherLocationsWithPromoItemsStill = 0;
                foreach (var operation in operations)
                {
                    var item = game.Items.Single(x => x.Id == operation.WriteData);
                    foreach (int o in operation.Offsets)
                    {
                        if (promotionItemOffsetsInChestsAndVillages.Contains(o))
                        {
                            ChestsAndVillages++;
                            if (item.Category.IsPromotion)
                                ChestsAndVillagesWithPromoItemsStill++;
                        }
                        if (otherPromotionItemOffsets.Contains(o))
                        {
                            OtherLocations++;
                            if (item.Category.IsPromotion)
                                OtherLocationsWithPromoItemsStill++;
                        }
                    }
                }
                if (ChestsAndVillages == ChestsAndVillagesWithPromoItemsStill)
                    Assert.Inconclusive();
                if (ChestsAndVillagesWithPromoItemsStill == 0)
                    Assert.Inconclusive();
                if (OtherLocations != OtherLocationsWithPromoItemsStill)
                    Assert.Inconclusive();
                if (OtherLocationsWithPromoItemsStill == 0)
                    Assert.Inconclusive();
            }
        }
    }
}