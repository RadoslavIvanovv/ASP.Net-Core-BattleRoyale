

using BattleRoyale.Data.Models;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Models.Players;
using System.Collections.Generic;

namespace BattleRoyale.Services.HeroServices
{
    public interface IHeroService
    {
        void Add(HeroModel hero, string userId);
        void Remove(int heroId, string userId);
        IEnumerable<HeroModel> All(int heroId, string userId);
        PlayerHeroViewModel Details(int heroId, string userId);
        Hero Equip(int heroId, int itemId,string userId);
        Hero Unequip(int heroId, int itemId,string userId);
        string GetHeroType(Hero hero);
        void SetHeroStats(Hero hero);
        void SetHeroImage(Hero hero);
        void EquipItem(Hero hero, Item item);
        void UnequipItem(Hero hero, Item item);
        void Attack(HeroFightViewModel attacker, HeroFightViewModel defender);
        void LevelUp(Hero hero);
    }
}
