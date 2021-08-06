

using BattleRoyale.Data.Models;
using System;
using BattleRoyale.Data;
using System.Linq;
using BattleRoyale.Models.Shop;
using BattleRoyale.Models.Items;
using BattleRoyale.Data.Models.HeroTypes;
using System.Collections.Generic;
using BattleRoyale.Data.Models.ItemTypes;

using static BattleRoyale.Data.Constants.ItemConstants;
using static BattleRoyale.Data.Constants.HeroConstants;
using Microsoft.EntityFrameworkCore;

namespace BattleRoyale.Services.ItemServices
{
    public class ItemService : IItemService
    {
        private readonly BattleRoyaleDbContext context;

        public ItemService(
            BattleRoyaleDbContext context)
        {
            this.context = context;
        }

        public void Add(ShopItemModel item)
        {
            var itemData = new Item
            {
                Id = item.Id,
                Name = item.Name,
                Stats = item.Stats,
                Price = item.Price,
                RequiredLevel = item.RequiredLevel,
                AdditionalEffect = item.AdditionalEffect,
                ItemType = item.ItemType,
                HeroType = item.HeroType
            };

            SetItemStats(itemData);

            this.context.Items.Add(itemData);

            this.context.SaveChanges();
        }

        public AllItemsQueryModel All(
            string heroType = null,
            string itemType = null,
            ItemSorting sorting = ItemSorting.Name,
            int currentPage = 1,
            int itemsPerPage = int.MaxValue
            )
        {
            var itemsQuery = this.context.Items.AsQueryable();

            if (!string.IsNullOrWhiteSpace(heroType))
            {
                itemsQuery = itemsQuery.Where(i => i.HeroType.ToString() == heroType);
            }

            if (!string.IsNullOrWhiteSpace(itemType))
            {
                itemsQuery = itemsQuery.Where(i => i.HeroType.ToString() == itemType);
            }

            itemsQuery = sorting switch
            {
                ItemSorting.Name => itemsQuery.OrderByDescending(c => c.Name),
                ItemSorting.Level => itemsQuery.OrderBy(c => c.RequiredLevel),
                ItemSorting.ItemType => itemsQuery.OrderBy(c => c.ItemType),
                ItemSorting.HeroType or _ => itemsQuery.OrderByDescending(c => c.HeroType)
            };

            var totalItems = itemsQuery.Count();

            var items = itemsQuery
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(i => new ShopItemModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Stats = i.Stats,
                    Price = i.Price,
                    RequiredLevel = i.RequiredLevel,
                    AdditionalEffect = i.AdditionalEffect,
                    HeroType = i.HeroType,
                    ItemType = i.ItemType,
                })
                .ToList();

            var heroTypes = new List<HeroType>
            {
                HeroType.Assassin,
                HeroType.Tank,
                HeroType.Mage
            };

            var itemTypes = new List<ItemType>
            {
                ItemType.Weapon,
                ItemType.Armor,
                ItemType.MagicResistance,
                ItemType.Necklace,
                ItemType.Boots
            };

            return new AllItemsQueryModel
            {
                HeroTypes = heroTypes,
                ItemTypes = itemTypes,
                TotalItems = totalItems,
                CurrentPage = currentPage,
                Sorting = sorting,
                Items = items
            };
        }

        public void BuyItem(string userId,int itemId)
        {
            var existingItem = this.context.Items.AsNoTracking().Where(i => i.Id == itemId).FirstOrDefault();

            var player = this.context.Players.Where(p => p.UserId == userId).FirstOrDefault();

            var itemToBuy = new Item
            {
                Id = existingItem.Id,
                Name = existingItem.Name,
                Stats = existingItem.Stats,
                Price = existingItem.Price,
                ItemType = existingItem.ItemType,
                RequiredLevel = existingItem.RequiredLevel,
                AdditionalEffect = existingItem.AdditionalEffect,
                HeroType = existingItem.HeroType,
            };

            if (player.Gold < itemToBuy.Price)
            {
                new InvalidOperationException("Not enough gold.");
            }
            player.Gold -= itemToBuy.Price;

            player.Inventory.Add(itemToBuy);

            this.context.SaveChanges();

        }
        public bool ExistingItem(int itemId)
        {
            var existingItem = this.context.Items.Where(i=>i.Id==itemId).FirstOrDefault();

            if (existingItem != null)
            {
                return true;
            }
            return false;
        }

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
