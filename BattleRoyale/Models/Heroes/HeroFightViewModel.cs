

using BattleRoyale.Data.Models;
using System.Collections.Generic;

namespace BattleRoyale.Models.Heroes
{
    public class HeroFightViewModel
    {
        public int Id { get; set; }       
        public string Name { get; set; }       
        public int Attack { get; set; }
        public int MagicAttack { get; set; }
        public int MaxHealth { get; set; }
        public int RemainingHealth { get; set; }
        public int MaxArmor { get; set; }
        public int RemainingArmor { get; set; }
        public int MaxMagicResistance { get; set; }
        public int RemainingMagicResistance { get; set; }
        public int Speed { get; set; }
        public string ImageUrl { get; set; }
    }
}
