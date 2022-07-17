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
                var leftover = locations.Except(correlated).ToHashSet();
                foreach (var (route, name) in expected_exclusives)
                {
                    foreach (var x in leftover)
                    {
                        string itemName = game.Items.Where(y => y.Id == x.ItemId).Select(y => y.Name).DefaultIfEmpty("(name unknown)").Single();
                        if (x.Branch.Route.Equals(FSharpOption<Route>.Some(route)) && itemName == name)
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
            var leftover = locations.Except(correlated).ToHashSet();
            foreach (var (route, name) in expected_exclusives)
            {
                foreach (var x in leftover)
                {
                    string itemName = game.Items.Where(y => y.Id == x.ItemId).Select(y => y.Name).Single();
                    if (x.Branch.Route.Equals(FSharpOption<Route>.Some(route)) && itemName == name)
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
        private static readonly FSharpOption<Tuple<string, bool>> NoCondition = FSharpOption<Tuple<string, bool>>.None;
        private static readonly FSharpOption<Difficulty> NoDifficulty = FSharpOption<Difficulty>.None;

        [TestMethod]
        public void TestRouteCorrleation()
        {
            var eirikaVersion = new Reward(Method.Chest, 3, 0, 0x3100, new Branch(FSharpOption<Route>.Some(Route.Eirika), NoCondition, NoDifficulty));
            var ephraimVersion = new Reward(Method.Chest, 3, 0, 0x3200, new Branch(FSharpOption<Route>.Some(Route.Ephraim), NoCondition, NoDifficulty));
            var eirikaExclusive = new Reward(Method.Chest, 4, 0, 0x4100, new Branch(FSharpOption<Route>.Some(Route.Eirika), NoCondition, NoDifficulty));
            var ephraimExclusive = new Reward(Method.Chest, 5, 0, 0x5200, new Branch(FSharpOption<Route>.Some(Route.Ephraim), NoCondition, NoDifficulty));
            var locations = new Reward[]
            {
                new Reward(Method.Chest, 1, 0, 0x1000, BranchModule.none),
                new Reward(Method.Chest, 2, 0, 0x2000, BranchModule.none),
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
            var normalSword = new Reward(Method.Chest, 1, 0, 0x1000, new Branch(NoRoute, NoCondition, FSharpOption<Difficulty>.Some(Difficulty.Normal)));
            var hardSword = new Reward(Method.Chest, 1, 0, 0x2000, new Branch(NoRoute, NoCondition, FSharpOption<Difficulty>.Some(Difficulty.Hard)));
            var hectorNormalSword = new Reward(Method.Chest, 1, 0, 0x3000, new Branch(NoRoute, NoCondition, FSharpOption<Difficulty>.Some(Difficulty.HectorNormal)));
            var hectorHardSword = new Reward(Method.Chest, 1, 0, 0x4000, new Branch(NoRoute, NoCondition, FSharpOption<Difficulty>.Some(Difficulty.HectorHard)));

            var hardLance = new Reward(Method.Chest, 20, 0, 0x7000, new Branch(NoRoute, NoCondition, FSharpOption<Difficulty>.Some(Difficulty.Hard)));
            var hectorHardLance = new Reward(Method.Chest, 20, 0, 0x7000, new Branch(NoRoute, NoCondition, FSharpOption<Difficulty>.Some(Difficulty.HectorHard)));

            var normalItem = new Reward(Method.Chest, 50, 0, 0x6000, new Branch(NoRoute, NoCondition, FSharpOption<Difficulty>.Some(Difficulty.Normal)));
            var hardItem = new Reward(Method.Chest, 50, 0, 0x7000, new Branch(NoRoute, NoCondition, FSharpOption<Difficulty>.Some(Difficulty.Hard)));

            var locations = new Reward[]
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
            var quickSword = new Reward(Method.Chest, 1, 0, 0x1000, new Branch(NoRoute, FSharpOption<Tuple<string, bool>>.Some(new Tuple<string, bool>("Cleared quickly", true)), NoDifficulty));
            var slowSword = new Reward(Method.Chest, 1, 0, 0x1000, new Branch(NoRoute, FSharpOption<Tuple<string, bool>>.Some(new Tuple<string, bool>("Cleared quickly", false)), NoDifficulty));

            var quickLance = new Reward(Method.Chest, 20, 0, 0x1000, new Branch(NoRoute, FSharpOption<Tuple<string, bool>>.Some(new Tuple<string, bool>("Cleared quickly", true)), NoDifficulty));

            var locations = new Reward[]
            {
                quickSword,
                slowSword,
                quickLance
            };

            var correlations = Correlator.ExtractAll(locations);
            Assert.AreEqual(1, correlations.Count(), $"{correlations}");
            var correlated = correlations.SelectMany(x => x);
            Assert.AreEqual(2, correlated.Count());
            var leftover = locations.Except(correlated).ToHashSet();
            Assert.AreEqual(1, leftover.Count);
            Assert.IsTrue(leftover.Contains(quickLance));
        }
    }
}