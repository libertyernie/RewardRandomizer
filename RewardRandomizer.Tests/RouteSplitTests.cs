using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RewardRandomizer.Tests
{
    [TestClass]
    public class RouteSplitTests
    {
        [TestMethod]
        public void TestFE6()
        {
            foreach (var game in new[] { GameModule.FE6_JP, GameModule.FE6_Localization })
            {
                var locations = game.Rewards;
                var correlations = Correlator.ExtractAll(locations);
                var correlated = correlations.SelectMany(x => x);
                var expected_exclusives = new[]
                {
                    (Route.Echidna, "RESTORE"),
                    (Route.Bartre, "BARRIER"),

                    (Route.Echidna, "AXEREAVER"),
                    (Route.Echidna, "SPEEDWING"), // Echidna route - Bartre's version requires Lot/Ward (see below)

                    (Route.Bartre, "RED_GEM"),
                    (Route.Bartre, "ENERGY_RING"), // For saving civilians

                    // Lot and Ward's houses on Bartre route chapter 10
                    (Route.Bartre, "ELIXIR"),
                    (Route.Bartre, "SPEEDWING"),
                    (Route.Bartre, "DOOR_KEY"),
                    (Route.Bartre, "SWORDREAVER"),

                    (Route.Ilia, "SECRET_BOOK"),
                    (Route.Ilia, "KNIGHT_CREST"), // This confuses me
                    (Route.Ilia, "AIRCALIBUR"), // This makes more sense

                    (Route.Ilia, "SPEEDWING"),
                    (Route.Sacae, "DRAGON_SHIELD"),

                    (Route.Sacae, "BLUE_GEM"), // Ilia's blue gem is stealable from an enemy

                    (Route.Ilia, "PURGE"),
                    (Route.Sacae, "ECLIPSE"),

                    // Appears twice in Ilia chapter 20 due to different dialogue branches
                    (Route.Ilia, "ANGELIC_ROBE"),
                    (Route.Ilia, "ANGELIC_ROBE"),

                    (Route.Ilia, "ELYSIAN_WHIP"),
                    (Route.Sacae, "ORION_BOLT"),
                };
                var leftover = RewardModule.untag(locations).Except(correlated).ToHashSet();
                foreach (var (route, name) in expected_exclusives)
                {
                    foreach (var x in leftover)
                    {
                        string itemName = game.Items.Where(y => y.Id == x.ItemId).Select(y => y.Name).DefaultIfEmpty("(name unknown)").Single();
                        if (x.Route.Equals(FSharpOption<Route>.Some(route)) && itemName == name)
                        {
                            leftover.Remove(x);
                            break;
                        }
                    }
                }
                foreach (var x in leftover)
                {
                    string itemName = game.Items.Where(y => y.Id == x.ItemId).Select(y => y.Name).Single();
                    Assert.Fail($"No match found for item: {x} {itemName}");
                }
            }
        }

        [TestMethod]
        public void TestFE8()
        {
            var game = GameModule.FE8_US;
            var locations = game.Rewards;
            var correlations = Correlator.ExtractAll(locations);
            Console.WriteLine(correlations.Length);
            var correlated = correlations.SelectMany(x => x);
            var expected_exclusives = new[]
            {
                (Route.Eirika, "DRAGONSPEAR"), // Ephraim's dragonspear is, I think, droppable on an enemy (not included in this randomzier)
                (Route.Eirika, "SHORT_SPEAR"),
                (Route.Eirika, "RAPIER"),
                (Route.Ephraim, "TORCH_STAFF"),
                (Route.Eirika, "ENERGY_RING"), // Eirika has two (Ewan + chapter 14 chest), Ephraim has just one (off an enemy)
                (Route.Ephraim, "KNIGHT_CREST"), // Ephraim has two (reward for saving Dussel's cavs + another off Vigarde), Eirika has one (off Aias)
            };
            var leftover = RewardModule.untag(locations).Except(correlated).ToHashSet();
            foreach (var (route, name) in expected_exclusives)
            {
                foreach (var x in leftover)
                {
                    string itemName = game.Items.Where(y => y.Id == x.ItemId).Select(y => y.Name).Single();
                    if (x.Route.Equals(FSharpOption<Route>.Some(route)) && itemName == name)
                    {
                        leftover.Remove(x);
                        break;
                    }
                }
            }
            foreach (var x in leftover)
            {
                string itemName = game.Items.Where(y => y.Id == x.ItemId).Select(y => y.Name).Single();
                Assert.Fail($"No match found for item: {x} {itemName}");
            }
        }

        private static readonly FSharpOption<Route> NoRoute = FSharpOption<Route>.None;
        private static readonly FSharpOption<Difficulty> NoDifficulty = FSharpOption<Difficulty>.None;

        private static Reward BuildReward(
            Method method,
            byte itemId,
            int offset,
            FSharpOption<Route>? route = null,
            FSharpOption<Difficulty>? difficulty = null,
            FSharpOption<string>? tag = null)
        {
            return new Reward(
                method,
                itemId,
                0,
                ListModule.OfArray(new[] { offset }),
                route ?? FSharpOption<Route>.None,
                difficulty ?? FSharpOption<Difficulty>.None,
                tag ?? FSharpOption<string>.None);
        }

        [TestMethod]
        public void TestRouteCorrleation()
        {
            var eirikaVersion = BuildReward(Method.Chest, 3, 0x3100, route: Route.Eirika);
            var ephraimVersion = BuildReward(Method.Chest, 3, 0x3200, route: Route.Ephraim);
            var eirikaExclusive = BuildReward(Method.Chest, 4, 0x4100, route: Route.Eirika);
            var ephraimExclusive = BuildReward(Method.Chest, 5, 0x5200, route: Route.Ephraim);
            var locations = new []
            {
                BuildReward(Method.Chest, 1, 0x1000),
                BuildReward(Method.Chest, 2, 0x2000),
                eirikaVersion,
                ephraimVersion,
                eirikaExclusive,
                ephraimExclusive,
            };
            var correlations = Correlator.ExtractAll(locations);
            Assert.AreEqual(3, correlations.Count(), $"{correlations}");
            var correlated = correlations.SelectMany(x => x);
            Assert.AreEqual(4, correlated.Count());
            Assert.IsTrue(correlated.Contains(eirikaVersion));
            Assert.IsTrue(correlated.Contains(ephraimVersion));
            var leftover = locations.Except(correlated).ToHashSet();
            Assert.AreEqual(2, leftover.Count);
            Assert.IsTrue(leftover.Contains(eirikaExclusive));
            Assert.IsTrue(leftover.Contains(ephraimExclusive));
        }

        [TestMethod]
        public void TestDifficultyCorrleation()
        {
            var normalSword = BuildReward(Method.Chest, 1, 0x1000, difficulty: Difficulty.Normal);
            var hardSword = BuildReward(Method.Chest, 1, 0x2000, difficulty: Difficulty.Hard);
            var hectorNormalSword = BuildReward(Method.Chest, 1, 0x3000, difficulty: Difficulty.HectorNormal);
            var hectorHardSword = BuildReward(Method.Chest, 1, 0x4000, difficulty: Difficulty.HectorHard);

            var hardLance = BuildReward(Method.Chest, 20, 0x7000, difficulty: Difficulty.Hard);
            var hectorHardLance = BuildReward(Method.Chest, 20, 0x7000, difficulty: Difficulty.HectorHard);

            var normalItem = BuildReward(Method.Chest, 50, 0x6000, difficulty: Difficulty.Normal);
            var hardItem = BuildReward(Method.Chest, 50, 0x7000, difficulty: Difficulty.Hard);

            var locations = new []
            {
                normalSword,
                hardSword,
                hectorNormalSword,
                hectorHardSword,
                hardLance,
                hectorHardLance,
                normalItem,
                hardItem
            };

            var correlations = Correlator.ExtractAll(locations);
            Assert.AreEqual(2, correlations.Length, $"{correlations}");
            Assert.AreEqual(1, correlations[0].Select(x => x.ItemId).Distinct().Single());
            Assert.AreEqual(50, correlations[1].Select(x => x.ItemId).Distinct().Single());
            var correlated = correlations.SelectMany(x => x);
            Assert.AreEqual(6, correlated.Count());
            var leftover = locations.Except(correlated).ToHashSet();
            Assert.IsTrue(leftover.Contains(hardLance));
            Assert.IsTrue(leftover.Contains(hectorHardLance));
            Assert.AreEqual(2, leftover.Count);
        }

        [TestMethod]
        public void TestConditionCorrleation()
        {
            var quickSword = BuildReward(Method.Chest, 1, 0x1000, tag: FSharpOption<string>.Some("tag1"));
            var slowSword = BuildReward(Method.Chest, 1, 0x1100, tag: FSharpOption<string>.Some("tag1"));

            var quickLance = BuildReward(Method.Chest, 20, 0x1200, tag: FSharpOption<string>.Some("tag2"));

            var locations = new []
            {
                quickSword,
                slowSword,
                quickLance
            };

            var correlations = Correlator.ExtractAll(locations);
            Assert.AreEqual(1, correlations[0].Count);
            Assert.AreEqual(2, correlations[0].Single().Offsets.Length);
            Assert.AreEqual(1, correlations[1].Count);
            Assert.AreEqual(1, correlations[1].Single().Offsets.Length);
        }
    }
}