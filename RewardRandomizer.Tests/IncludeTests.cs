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
                .Except(new[] { 0x9FAD88 })
                .ToList();
            var correlations = Correlator.ExtractAll(locations);
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
                    if (o.WriteData[0] != expectedItem)
                        promoOffsets.Remove(o.Offset);
                }
                if (promoOffsets.Count == 0) break;
            }
            if (promoOffsets.Any())
            {
                Assert.Fail($"Promotion item offset not randomized: {promoOffsets[0]:X6}");
            }
        }
    }
}