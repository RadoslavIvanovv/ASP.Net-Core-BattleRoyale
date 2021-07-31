

using BattleRoyale.Data.Models;
using BattleRoyale.Models.Heroes;

namespace BattleRoyale.Services.HeroServices
{
    public interface IHeroService
    {
        string GetHeroType(Hero hero);

        void SetHeroStats(Hero hero);
        void SetHeroImage(Hero hero);

        void EquipItem(Hero hero, Item item);

        void UnequipItem(Hero hero, Item item);
        void Attack(HeroFightViewModel attacker, HeroFightViewModel defender);
        void LevelUp(Hero hero);
    }
}
