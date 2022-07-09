using System;
using System.Collections.Generic;
using System.Linq;

namespace RewardRandomizer.Procedures
{
    public class Shuffler
    {
        private static readonly Random r = new Random();

        public static void Shuffle(byte[] data, Game game, IEnumerable<ItemCategory> itemCategories, IEnumerable<Method> methods)
        {
            var itemIds = game.Items.Where(x => itemCategories.Contains(x.category)).Select(x => x.id).ToList();
            var locations = game.Locations.Where(x => methods.Contains(x.method) && itemIds.Contains(x.item));
            var sets = Correlator.ExtractAll(locations);
            var shuffled_sets = sets.OrderBy(_ => r.Next()).ToList();
            for (int i = 0; i < sets.Count(); i++)
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
