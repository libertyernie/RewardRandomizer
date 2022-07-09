using System;
using System.Linq;

namespace RewardRandomizer.Procedures
{
    public class Class1
    {
        private static readonly Random r = new Random();

        public static void ShuffleRewards(byte[] data, Game game)
        {
            var sets = Correlator.ExtractAll(game.Locations).ToList();
            var shuffled_sets = sets.OrderBy(_ => r.Next()).ToList();
            for (int i = 0; i < sets.Count; i++)
            {
                int new_item = shuffled_sets[i].Select(x => x.item).Distinct().Single();
                var set = sets[i];
                foreach (var location in set)
                {
                    if (data[location.offset] != location.item) throw new NotImplementedException();
                    data[location.offset] = (byte)new_item;
                }
            }
        }
    }
}
