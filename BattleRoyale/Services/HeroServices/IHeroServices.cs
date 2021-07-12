

using BattleRoyale.Data.Models;

namespace BattleRoyale.Services.HeroServices
{
    public interface IHeroServices
    {
        string GetHeroType(Hero hero);

        void SetHeroStats(Hero hero);
    }
}
