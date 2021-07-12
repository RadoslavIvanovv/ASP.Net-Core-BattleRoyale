

using BattleRoyale.Data.Models;
using BattleRoyale.Models.Heroes;
using System;

namespace BattleRoyale.Services
{
    public class HeroServices : IHeroServices
    {
        public string GetHeroType(Hero hero)
        {
            if (hero.HeroType.ToString() == "Assassin")
            {
                return "Assassin";
            }
            else if (hero.HeroType.ToString() == "Tank")
            {
                return "Tank";
            }
            else if (hero.HeroType.ToString() == "Mage")
            {
                return "Mage";
            }
            else
            {
                return new InvalidOperationException("Invalid hero type.").ToString();
            }
        }

        public void SetHeroStats(Hero hero)
        {
            var heroType = GetHeroType(hero);

            if (heroType == "Assassin")
            {
                hero.Attack = 50;
                hero.SpellAttack = 0;
                hero.Health = 500;
                hero.Armor = 30;
                hero.MagicResistance = 20;
                hero.Speed = 100;
            }
            else if (heroType == "Tank")
            {
                hero.Attack = 30;
                hero.SpellAttack = 10;
                hero.Health = 700;
                hero.Armor = 50;
                hero.MagicResistance = 30;
                hero.Speed = 50;
            }
            else if (heroType == "Mage")
            {
                hero.Attack = 20;
                hero.SpellAttack = 50;
                hero.Health = 400;
                hero.Armor = 30;
                hero.MagicResistance = 20;
                hero.Speed = 90;
            }
        }
    }
}
