
using BattleRoyale.Data.Models.HeroTypes;

namespace BattleRoyale.Models.Heroes
{
    public class AddHeroFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ImageUrl { get; init; }

        public int Attack { get; set; }

        public int SpellAttack { get; set; }

        public int Health { get; set; }

        public int Armor { get; set; }

        public int MagicResistance { get; set; }

        public int Speed { get; set; }

        public string HeroType { get; init; }
    }
}
