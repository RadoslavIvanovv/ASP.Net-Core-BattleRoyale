

using BattleRoyale.Data.Models;
using System;

namespace BattleRoyale.Services.ItemServices
{
    public class ItemService : IItemService
    {
        public string GetItemType(Item item)
        {
            if (item.ItemType.ToString() == "Weapon")
            {
                return "Weapon";
            }
            else if (item.ItemType.ToString() == "Armor")
            {
                return "Armor";
            }
            else if (item.ItemType.ToString() == "MagicResistance")
            {
                return "MagicResistance";
            }
            if (item.ItemType.ToString() == "Necklace")
            {
                return "Necklace";
            }
            else if (item.ItemType.ToString() == "Boots")
            {
                return "Boots";
            }
            else
            {
                return new InvalidOperationException("Invalid hero type.").ToString();
            }
        }
        public void SetItemStats(Item item)
        {
            if (item.HeroType.ToString() == "Assassin")
            {
                SetItemStatsForAssassin(item);
            }
            else if(item.HeroType.ToString() == "Tank")
            {
                SetItemStatsForTank(item);
            }
            else if(item.HeroType.ToString() == "Mage")
            {
                SetItemStatsForMage(item);
            }
            
        }


        private string SetItemStatsForAssassin(Item item)
        {
            if (item.ItemType.ToString() == "Weapon")
            {
                item.Stats = 50;
            }
            else if (item.ItemType.ToString() == "Armor")
            {
                item.Stats = 40;
            }
            else if (item.ItemType.ToString() == "MagicResistance")
            {
                item.Stats = 60;
            }
            else if (item.ItemType.ToString() == "Necklace")
            {
                item.Stats = 50;
            }
            else if (item.ItemType.ToString() == "Boots")
            {
                item.Stats = 70;
            }
            return null;
        }

        
        private string SetItemStatsForMage(Item item)
        {
            if (item.ItemType.ToString() == "Weapon")
            {
                item.Stats = 30;
            }
            else if (item.ItemType.ToString() == "Armor")
            {
                item.Stats = 60;
            }
            else if (item.ItemType.ToString() == "MagicResistance")
            {
                item.Stats = 80;
            }
            else if (item.ItemType.ToString() == "Necklace")
            {
                item.Stats = 60;
            }
            else if (item.ItemType.ToString() == "Boots")
            {
                item.Stats = 50;
            }
            return null;
        }

        private string SetItemStatsForTank(Item item)
        {
            if (item.ItemType.ToString() == "Weapon")
            {
                item.Stats = 40;
            }
            else if (item.ItemType.ToString() == "Armor")
            {
                item.Stats = 40;
            }
            else if (item.ItemType.ToString() == "MagicResistance")
            {
                item.Stats = 60;
            }
            else if (item.ItemType.ToString() == "Necklace")
            {
                item.Stats = 80;
            }
            else if (item.ItemType.ToString() == "Boots")
            {
                item.Stats = 60;
            }
            return null;
        }
    }
}
