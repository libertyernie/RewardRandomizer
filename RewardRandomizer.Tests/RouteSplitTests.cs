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

                    (Route.Ilia, "ELYSIAN_WHIP"),
                    (Route.Sacae, "ORION_BOLT"),
                };
                var leftover = locations.Except(correlated).ToHashSet();
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
        public void TestFE7()
        {
            var game = GameModule.FE7_US;
            var locations = game.Rewards;
            var correlations = Correlator.ExtractAll(locations);
            var correlated = correlations.SelectMany(x => x);
            var expected_exclusives = new []
            {
                0xCC9E2E, // Hector hard mode ch. 15 dragonshield
                0xCCC325, // Eliwood speedwings A
                0xCCC455, // Eliwood speedwings B
                0xCB6124, // Afa's drops - requires tactician
                0xCD79AD, // Kenneth map Eliwood hard mode speedwings (not in normal mode)
                0xCD7BCD, // Kenneth map Hector hard mode speedwings (not in normal mode)
                0xCA4CBC, // Kenneth map guiding ring
                0xCA4CC8, // Kenneth map blue gem
                0xCA5034, // Jerme map white gem
                0xCA5040, // Jerme map hero crest
                0xCA504C, // Jerme map bolting
                0xCDB081, // Eliwood hard mode orion's bolt in Cog of Destiny
            };
            var leftover = locations.Except(correlated).ToHashSet();
            foreach (int offset in expected_exclusives)
            {
                foreach (var x in leftover)
                {
                    string itemName = game.Items.Where(y => y.Id == x.ItemId).Select(y => y.Name).Single();
                    if (x.Offsets.Contains(offset))
                    {
                        leftover.Remove(x);
                        break;
                    }
                }
            }
            foreach (var x in leftover)
            {
                if (OptionModule.ToObj(x.Route)?.IsHectorNormal == true) continue;
                if (OptionModule.ToObj(x.Route)?.IsHectorHard == true) continue;
                if (OptionModule.ToObj(x.Route)?.IsEliwoodNormal == true && x.ItemId == 0x5B) continue;
                if (OptionModule.ToObj(x.Route)?.IsEliwoodHard == true && x.ItemId == 0x5B) continue;
                string itemName = game.Items.Where(y => y.Id == x.ItemId).Select(y => y.Name).Single();
                Assert.Fail($"No match found for item: {x} {itemName}");
            }
            // Test orion's bolt on Four Fanged Offense
            Assert.IsTrue(correlations.Any(x => x.All(y => y.ItemId == 0x65) && x.SelectMany(y => y.Offsets).Contains(0xCB7828) && x.SelectMany(y => y.Offsets).Count() == 3));
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

        private static Reward BuildReward(
            Method method,
            byte itemId,
            int offset,
            FSharpOption<Route>? route = null)
        {
            return new Reward(
                method,
                itemId,
                0,
                SetModule.OfArray(new[] { offset }),
                route ?? FSharpOption<Route>.None);
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
            Assert.AreEqual(3, correlations.Length, $"{correlations}");
            var correlated = correlations.SelectMany(x => x);
            Assert.AreEqual(4, correlated.Count());
            Assert.IsTrue(correlated.Contains(eirikaVersion));
            Assert.IsTrue(correlated.Contains(ephraimVersion));
            var leftover = locations.Except(correlated).ToHashSet();
            Assert.AreEqual(2, leftover.Count);
            Assert.IsTrue(leftover.Contains(eirikaExclusive));
            Assert.IsTrue(leftover.Contains(ephraimExclusive));
        }
    }
}