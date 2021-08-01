

namespace BattleRoyale.Models.Heroes
{
    public class HeroModel
    {
        public int Id { get; set; }
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

        public string HeroType { get; init; }
    }
}
