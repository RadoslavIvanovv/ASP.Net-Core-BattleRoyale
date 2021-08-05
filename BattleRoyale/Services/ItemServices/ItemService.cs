

using BattleRoyale.Data.Models;
using System;

using static BattleRoyale.Data.Constants.ItemConstants;
using static BattleRoyale.Data.Constants.HeroConstants;

namespace BattleRoyale.Services.ItemServices
{
    public class ItemService : IItemService
    {
        public string GetItemType(Item item)
        {
            if (item.ItemType.ToString() == Weapon)
            {
                return Weapon;
            }
            else if (item.ItemType.ToString() == Necklace)
            {
                return Necklace;
            }
            else if (item.ItemType.ToString() == Armor)
            {
                return Armor;
            }
            else if (item.ItemType.ToString() ==MagicResistance)
            {
                return MagicResistance;
            }
            else if (item.ItemType.ToString() == Boots)
            {
                return Boots;
            }
            else
            {
                return new InvalidOperationException(InvalidItem).ToString();
            }
        }
        public void SetItemStats(Item item)
        {
            if (item.HeroType.ToString() == Assassin)
            {
                SetItemStatsForAssassin(item);
            }
            else if(item.HeroType.ToString() == Tank)
            {
                SetItemStatsForTank(item);
            }
            else if(item.HeroType.ToString() == Mage)
            {
                SetItemStatsForMage(item);
            }
        }

        public bool HeroHasItem(Hero hero, Item item)
        {
            if (item.ItemType.ToString() ==Weapon)
            {
                if (hero.HasWeapon)
                {
                    return true;
                }
            }
            else if (item.ItemType.ToString() == Necklace)
            {
                if (hero.HasNecklace)
                {
                    return true;
                }
            }
            else if (item.ItemType.ToString() == Armor)
            {
                if (hero.HasArmorItem)
                {
                    return true;
                }
            }
            else if (item.ItemType.ToString() == MagicResistance)
            {
                if (hero.HasMagicResistItem)
                {
                    return true;
                }
            }
            else if (item.ItemType.ToString() == Boots)
            {
                if (hero.HasBoots)
                {
                    return true;
                }
            }
            return false;
        }

        private void SetItemStatsForAssassin(Item item)
        {
            if (item.ItemType.ToString() == Weapon)
            {
                item.Stats = AssassinWeapon;
            }
            else if (item.ItemType.ToString() == Necklace)
            {
                item.Stats =AssassinNecklace;
            }
            else if (item.ItemType.ToString() == Armor)
            {
                item.Stats = AssassinArmor;
            }
            else if (item.ItemType.ToString() == MagicResistance)
            {
                item.Stats = AssassinMagicResistanceOnLevelUp;
            }
            else if (item.ItemType.ToString() == Boots)
            {
                item.Stats = AssassinBoots;
            }
        }

        private void SetItemStatsForTank(Item item)
        {
            if (item.ItemType.ToString() == Weapon)
            {
                item.Stats = TankWeapon;
            }
            else if (item.ItemType.ToString() == Necklace)
            {
                item.Stats = TankNecklace;
            }
            else if (item.ItemType.ToString() ==Armor)
            {
                item.Stats = TankArmor;
            }
            else if (item.ItemType.ToString() == MagicResistance)
            {
                item.Stats = TankMagicResistanceOnLevelUp;
            }
            else if (item.ItemType.ToString() == Boots)
            {
                item.Stats = TankBoots;
            }
        }

        private void SetItemStatsForMage(Item item)
        {
            if (item.ItemType.ToString() == Weapon)
            {
                item.Stats = MageWeapon;
            }
            else if (item.ItemType.ToString() ==Necklace)
            {
                item.Stats = MageNecklace;
            }
            else if (item.ItemType.ToString() == Armor)
            {
                item.Stats = MageArmor;
            }
            else if (item.ItemType.ToString() == MagicResistance)
            {
                item.Stats = MageMagicResistance;
            }
            else if (item.ItemType.ToString() == Boots)
            {
                item.Stats =MageBoots;
            }
        }
    }
}
