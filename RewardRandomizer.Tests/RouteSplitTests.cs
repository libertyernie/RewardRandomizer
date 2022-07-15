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
                    (Route.SpecialBehavior, "ELIXIR"),
                    (Route.SpecialBehavior, "SPEEDWING"),
                    (Route.SpecialBehavior, "DOOR_KEY"),
                    (Route.SpecialBehavior, "SWORDREAVER"),

                    (Route.Ilia, "SECRET_BOOK"),
                    (Route.Ilia, "KNIGHT_CREST"), // This confuses me
                    (Route.Ilia, "AIRCALIBUR"), // This makes more sense

                    (Route.Ilia, "SPEEDWING"),
                    (Route.Sacae, "DRAGON_SHIELD"),

                    (Route.Sacae, "BLUE_GEM"), // Ilia's blue gem is stealable from an enemy

                    (Route.Ilia, "PURGE"),
                    (Route.Sacae, "ECLIPSE"),

                    // Appears twice in Ilia chapter 20 due to different dialogue branches
                    (Route.SpecialBehavior, "ANGELIC_ROBE"),
                    (Route.SpecialBehavior, "ANGELIC_ROBE"),

                    (Route.Ilia, "ELYSIAN_WHIP"),
                    (Route.Sacae, "ORION_BOLT"),

                    (Route.SpecialBehavior, "BINDING_BLADE"),
                    (Route.SpecialBehavior, "BINDING_BLADE"),
                };
                var leftover = locations.Except(correlated).ToHashSet();
                foreach (var (route, name) in expected_exclusives)
                {
                    foreach (var x in leftover)
                    {
                        string itemName = game.Items.Where(y => y.Id == x.ItemId).Select(y => y.Name).DefaultIfEmpty("(name unknown)").Single();
                        if (x.Route == route && itemName == name)
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
                    if (x.Route == route && itemName == name)
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
}