using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RewardRandomizer.Tests
{
    [TestClass]
    public class IncludeTests
    {
        [TestMethod]
        public void TestFE8()
        {
            var game = GameModule.FE8_US;
            var locations = game.Rewards;
            var promoOffsets = locations
                .Where(x => game.Items.Any(y => y.Id == x.ItemId && y.Category.IsPromotion))
                .SelectMany(x => x.Offsets)
                .ToList();
            for (int i = 0; i < 10; i++)
            {
                var ops = Randomizer.GenerateOperations(game, new[]
                {
                    new Randomizer.RandomizationParameters(
                        Randomizer.Mode.Shuffle,
                        Randomizer.ItemCollection.NewAllItemsInCategory(ItemCategory.Promotion),
                        Randomizer.MethodCollection.AllMethods)
                });
                foreach (var o in ops)
                {
                    int expectedItem = locations
                        .Where(x => x.Offsets.Contains(o.Offset))
                        .Select(x => x.ItemId)
                        .Single();
                    if (o.WriteData.Single() != expectedItem)
                        promoOffsets.Remove(o.Offset);
                }
                if (promoOffsets.Count == 0) break;
            }
            if (promoOffsets.Except(new[] { 0x9FAD88 }).Any())
            {
                Assert.Fail($"Promotion item offset not randomized: {promoOffsets[0]:X6}");
            }
        }

        [TestMethod]
        public void TestSequential()
        {
            var game = GameModule.FE8_US;
            var locations = game.Rewards;
            var chestVillagePromos = locations
                .Where(x => x.Method.IsChest || x.Method.IsVillage)
                .Where(x => game.Items.Any(y => y.Id == x.ItemId && y.Category.IsPromotion))
                .SelectMany(x => x.Offsets)
                .ToList();
            var nonChestVillagePromos = locations
                .Where(x => !x.Method.IsChest && !x.Method.IsVillage)
                .Where(x => game.Items.Any(y => y.Id == x.ItemId && y.Category.IsPromotion))
                .SelectMany(x => x.Offsets)
                .ToList();
            var arr1 = new[]
            {
                new Randomizer.RandomizationParameters(
                    Randomizer.Mode.Shuffle,
                    Randomizer.ItemCollection.AllItems,
                    Randomizer.MethodCollection.NewMethodCollection(new[]
                    {
                        Method.Chest, Method.Village
                    })),
                new Randomizer.RandomizationParameters(
                    Randomizer.Mode.Shuffle,
                    Randomizer.ItemCollection.NewAllItemsInCategory(ItemCategory.Promotion),
                    Randomizer.MethodCollection.AllMethods),
            };
            var arr2 = arr1.Reverse();
            foreach (var arr in new[] { arr1, arr2 })
            {
                var ops = Randomizer.GenerateOperations(game, arr);
                ops = ops.GroupBy(x => x.Offset).Select(g => g.Last());
                Assert.IsTrue(ops.All(o =>
                {
                    return nonChestVillagePromos.Contains(o.Offset)
                    && game.Items.Single(x => x.Id == o.WriteData.Single()).Category.IsPromotion;
                }));
                Assert.IsTrue(ops.Any(o =>
                {
                    return chestVillagePromos.Contains(o.Offset)
                    && !game.Items.Single(x => x.Id == o.WriteData.Single()).Category.IsPromotion;
                }));
                Assert.IsFalse(ops.All(o =>
                {
                    return chestVillagePromos.Contains(o.Offset)
                    && !game.Items.Single(x => x.Id == o.WriteData.Single()).Category.IsPromotion;
                }));
            }
        }
    }
}