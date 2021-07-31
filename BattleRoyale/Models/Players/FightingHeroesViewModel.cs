using System.Collections.Generic;


namespace BattleRoyale.Models.Heroes
{
    public class FightingHeroesViewModel
    {
        public string UserId { get; set; }
        public HeroFightViewModel Attacker { get; set; }
        public HeroFightViewModel Defender { get; set; }
    }
}
