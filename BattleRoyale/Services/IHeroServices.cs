

using BattleRoyale.Data.Models;

namespace BattleRoyale.Services
{
    public interface IHeroServices
    {
        string GetHeroType(Hero hero);

        void SetHeroStats(Hero hero);
    }
}
