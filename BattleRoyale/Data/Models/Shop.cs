

using System.Collections.Generic;

namespace BattleRoyale.Data.Models
{
    public class Shop
    {
        public int Id { get; init; }
        public ICollection<Item> Items { get; init; } = new HashSet<Item>();
    }
}
