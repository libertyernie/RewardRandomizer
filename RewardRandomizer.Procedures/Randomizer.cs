using System;
using System.Collections.Generic;
using System.Linq;

namespace RewardRandomizer.Procedures
{
    public class Randomizer
    {
        private static readonly Random r = new Random();

        public static void Randomize(byte[] data, Game game, IEnumerable<ItemCategory> itemCategories, IEnumerable<Method> methods)
        {
            if (itemCategories.Contains(ItemCategory.Other))
                throw new NotImplementedException();

            var itemIds = game.Items.Where(x => itemCategories.Contains(x.category)).Select(x => x.id).ToList();
            var locations = game.Locations.Where(x => methods.Contains(x.method) && itemIds.Contains(x.item));
            var sets = Correlator.ExtractAll(locations);
            for (int i = 0; i < sets.Count(); i++)
            {
                int new_item = itemIds[r.Next(itemIds.Count())];
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
