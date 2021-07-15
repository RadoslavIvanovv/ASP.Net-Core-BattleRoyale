

using BattleRoyale.Data.Models;
using BattleRoyale.Models.Heroes;

namespace BattleRoyale.Services.HeroServices
{
    public interface IHeroServices
    {
        string GetHeroType(Hero hero);

        void SetHeroStats(Hero hero);

        void EquipItem(Hero hero, Item item);

        void UnequipItem(Hero hero, Item item);
        void Attack(HeroFightViewModel attacker, HeroFightViewModel defender);
    }
}
