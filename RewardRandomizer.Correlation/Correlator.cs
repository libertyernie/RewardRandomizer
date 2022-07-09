#nullable disable

using Microsoft.FSharp.Collections;
using RewardRandomizer;
using System.Collections.Generic;
using System.Linq;

namespace RewardRandomizer.Correlation
{
    public static class Correlator
    {
        public static IEnumerable<FSharpList<Reward>> ExtractSets(IEnumerable<Reward> list)
        {
            var bag = new HashSet<Reward>(list);

            var routes = new[]
            {
                (Route.Bartre, Route.Echidna),
                (Route.Ilia, Route.Sacae),
                (Route.Eirika, Route.Ephraim),
            };

            foreach (var (route1, route2) in routes)
            {
                // List all items in the A route
                foreach (var rA in bag.Where(x => x.Route == route1).ToList())
                {
                    // Try to find a matching item in the B route (preferably one of the same target)
                    var rB = bag
                        .Where(x => x.Route == route2)
                        .Where(x => x.item == rA.item)
                        .OrderBy(x => x.unit == rA.unit ? 1 : 2)
                        .DefaultIfEmpty(null)
                        .First();

                    if (rB != null)
                    {
                        // Matching item found - return A route and B route versions together, and remove them from the set
                        yield return ListModule.OfArray(new[] { rA, rB });
                        bag.Remove(rA);
                        bag.Remove(rB);
                    }
                }
            }

            // Find duplicates of items given to a specific unit (e.g. Percival's knight crest, depending on join chapter, or the angelic robe from The Liberation of Ilia, with dialogue depending on who survived)
            // These will be randomized as if they were a single item
            foreach (var g in bag.Where(x => x.IsToUnit).GroupBy(x => (x.unit, x.item)))
            {
                if (g.Count() > 1)
                {
                    // Return items, grouped together
                    yield return ListModule.OfSeq(g);
                    // Remove each of them from the set
                    foreach (var i in g)
                        bag.Remove(i);
                }
            }

            // Add all remaining non-route-specific items to the randomization pool
            foreach (var i in bag)
                if (i.Route == Route.All)
                    yield return ListModule.Singleton(i);
        }
    }
}
