

using BattleRoyale.Data.Models;
using BattleRoyale.Models.Items;
using System.Collections.Generic;

namespace BattleRoyale.Models.Players
{
    public class PlayerHeroViewModel
    {
        public int Id { get; set; }
        public Hero Hero { get; set; }
        public ICollection<Item> Items { get; set; } = new HashSet<Item>();

    }
}
