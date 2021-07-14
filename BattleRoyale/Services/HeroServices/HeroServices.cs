

using BattleRoyale.Data.Models;
using System;

namespace BattleRoyale.Services.HeroServices
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
                hero.MagicAttack = 0;
                hero.Health = 500;
                hero.Armor = 30;
                hero.MagicResistance = 20;
                hero.Speed = 100;
            }
            else if (heroType == "Tank")
            {
                hero.Attack = 30;
                hero.MagicAttack = 10;
                hero.Health = 700;
                hero.Armor = 50;
                hero.MagicResistance = 30;
                hero.Speed = 50;
            }
            else if (heroType == "Mage")
            {
                hero.Attack = 20;
                hero.MagicAttack = 50;
                hero.Health = 400;
                hero.Armor = 30;
                hero.MagicResistance = 20;
                hero.Speed = 90;
            }
        }

        public void EquipItem(Hero hero, Item item)
        {
            if (item.ItemType.ToString() == "Weapon")
            {
                hero.Attack += item.Stats;
                hero.HasWeapon = true;
            }
            else if (item.ItemType.ToString() == "Armor")
            {
                hero.Armor += item.Stats;
                hero.HasArmorItem = true;
            }
            else if (item.ItemType.ToString() == "MagicResistance")
            {
                hero.MagicResistance += item.Stats;
                hero.HasMagicResistItem = true;
            }
            else if (item.ItemType.ToString() == "Necklace")
            {
                hero.MagicAttack += item.Stats;
                hero.HasNecklace = true;
            }
            else if (item.ItemType.ToString() == "Boots")
            {
                hero.Speed += item.Stats;
                hero.HasBoots = true;
            }
            item.IsEquipped = true;
        }

        public void UnequipItem(Hero hero, Item item)
        {
            if (item.ItemType.ToString() == "Weapon")
            {
                hero.Attack -= item.Stats;
                hero.HasWeapon = false;
            }
            else if (item.ItemType.ToString() == "Armor")
            {
                hero.Armor -= item.Stats;
                hero.HasArmorItem = false;
            }
            else if (item.ItemType.ToString() == "MagicResistance")
            {
                hero.MagicResistance -= item.Stats;
                hero.HasMagicResistItem = false;
            }
            else if (item.ItemType.ToString() == "Necklace")
            {
                hero.MagicAttack -= item.Stats;
                hero.HasNecklace = false;
            }
            else if (item.ItemType.ToString() == "Boots")
            {
                hero.Speed -= item.Stats;
                hero.HasBoots = false;
            }
            item.IsEquipped = false;
        }
    }
}
