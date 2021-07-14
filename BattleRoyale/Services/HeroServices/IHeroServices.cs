

using BattleRoyale.Data.Models;

namespace BattleRoyale.Services.HeroServices
{
    public interface IHeroServices
    {
        string GetHeroType(Hero hero);

        void SetHeroStats(Hero hero);

        void EquipItem(Hero hero, Item item);

        void UnequipItem(Hero hero, Item item);
    }
}
