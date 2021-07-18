
using BattleRoyale.Data.Models;
using System.Collections.Generic;

namespace BattleRoyale.Models.Players
{
    public class PlayerInventoryViewModel
    {
        public int Id { get; set; }
        public ICollection<Item> BoughtItems { get; set; } = new HashSet<Item>();
    }
}
