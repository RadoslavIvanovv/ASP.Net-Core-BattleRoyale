

using BattleRoyale.Data.Models;
using System.ComponentModel.DataAnnotations;

using static BattleRoyale.Data.Constants.DatabaseConstants;

namespace BattleRoyale.Models.Heroes
{
    public class HeroModel
    {
        public int Id { get; set; }
        [MinLength(DefaultMinLengthForName)]
        public string Name { get; set; }
        public string ImageUrl { get; init; }
        public int Level { get; init; }
        public bool IsMain { get; set; }
        public int Attack { get; set; }
        public int MagicAttack { get; set; }
        public int Health { get; set; }
        public int Armor { get; set; }
        public int MagicResistance { get; set; }
        public int Speed { get; set; }
        public int OverallPower { get; set; }
        public Player Player { get; set; }
        [Required]
        public string HeroType { get; init; }
    }
}
