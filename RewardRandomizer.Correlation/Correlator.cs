#nullable disable

using Microsoft.FSharp.Collections;
using RewardRandomizer;
using System;
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
                foreach (var rA in bag.Where(x => x.route == route1).ToList())
                {
                    // Try to find a matching item in the B route (preferably one of the same target)
                    var rB = bag
                        .Where(x => x.route == route2)
                        .Where(x => x.item == rA.item)
                        .OrderBy(x => x.unit == rA.unit ? 1 : 2)
                        .ThenBy(x => Math.Abs(x.offset - rA.offset))
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

            foreach (var rA in bag.Where(x => x.route == Route.JoshuaAlive))
            {
                var rB = bag
                    .Where(x => x.route == Route.JoshuaDead)
                    .Where(x => x.item == rA.item)
                    .Single();
                var rC = bag
                    .Where(x => x.route == Route.Ephraim)
                    .Where(x => x.item == rA.item)
                    .Single();
                yield return ListModule.OfArray(new[] { rA, rB, rC });
                bag.Remove(rA);
                bag.Remove(rB);
                bag.Remove(rC);
            }

            // List all items in Eliwood Normal mode
            foreach (var rA in bag.Where(x => x.route == Route.EliwoodNormal).ToList())
            {
                // Try to find a matching item in each of the other routes
                Reward findClosest(Route route2) => bag
                    .Where(x => x.route == route2)
                    .Where(x => x.item == rA.item)
                    .OrderBy(x => Math.Abs(x.offset - rA.offset))
                    .DefaultIfEmpty(null)
                    .First();

                IEnumerable<Reward> getGroup()
                {
                    yield return rA;
                    if (findClosest(Route.HectorNormal) is Reward rB) yield return rB;
                    if (findClosest(Route.EliwoodHard) is Reward rC) yield return rC;
                    if (findClosest(Route.HectorHard) is Reward rD) yield return rD;
                }

                // Return all versions together, and remove them from the set
                var group = ListModule.OfSeq(getGroup());
                yield return group;
                foreach (var i in group)
                    bag.Remove(i);
            }

            // Find duplicates of items given to a specific unit (e.g. Percival's knight crest, depending on join chapter, or the angelic robe from The Liberation of Ilia, with dialogue depending on who survived)
            // These will be randomized as if they were a single item
            foreach (var g in bag.Where(x => x.route == Route.All && x.unit != 0).GroupBy(x => (x.unit, x.item)))
            {
                // Return items, grouped together
                yield return ListModule.OfSeq(g);
                // Remove each of them from the set
                foreach (var i in g)
                    bag.Remove(i);
            }

            foreach (var i in bag)
                if (i.route == Route.All)
                    yield return ListModule.Singleton(i);
                else if (i.route != Route.SpecialBehavior)
                    Console.Error.WriteLine("Unmatched: " + i);
        }
    }
}
