using System.Collections.Generic;


namespace BattleRoyale.Models.Heroes
{
    public class FightingHeroesViewModel
    {
        public string UserId { get; set; }
        public List<HeroFightViewModel> Heroes { get; set; } = new List<HeroFightViewModel>();
    }
}
