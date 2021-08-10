

using BattleRoyale.Data.Models;
using BattleRoyale.Data;
using System.Linq;
using BattleRoyale.Models.Shop;
using BattleRoyale.Models.Items;
using BattleRoyale.Data.Models.HeroTypes;
using System.Collections.Generic;
using BattleRoyale.Data.Models.ItemTypes;
using Microsoft.EntityFrameworkCore;
using BattleRoyale.Models.Players;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using static BattleRoyale.Data.Constants.ItemConstants;
using static BattleRoyale.Data.Constants.HeroConstants;
using static BattleRoyale.Data.Constants.ShopControllerConstants;


namespace BattleRoyale.Services.ItemServices
{
    public class ItemService : IItemService
    {
        private readonly BattleRoyaleDbContext context;
        private readonly IConfigurationProvider mapper;

        public ItemService(
            BattleRoyaleDbContext context,
            IConfigurationProvider mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
            ItemSorting sorting = ItemSorting.LowestLevel,
            int currentPage = 1,
            int itemsPerPage = int.MaxValue
            )
        {
            var itemsQuery = this.context.Items.AsQueryable();

            itemsQuery = sorting switch
            {
                ItemSorting.Name => itemsQuery.OrderByDescending(i => i.Name),
                ItemSorting.LowestLevel => itemsQuery.OrderBy(i => i.RequiredLevel),
                ItemSorting.HighestLevel => itemsQuery.OrderByDescending(i => i.RequiredLevel),
                ItemSorting.LowestPrice => itemsQuery.OrderBy(i => i.Price),
                ItemSorting.HighestPrice => itemsQuery.OrderByDescending(i => i.Price),
                ItemSorting.ItemType => itemsQuery.OrderBy(i => i.ItemType),
                ItemSorting.HeroType or _ => itemsQuery.OrderByDescending(i => i.HeroType)
            };

            var totalItems = itemsQuery.Count();

            var items = itemsQuery
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ProjectTo<ShopItemModel>(this.mapper);

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

        public string BuyItem(string userId,int itemId)
        {
            var existingItem = this.context.Items.AsNoTracking().Where(i => i.Id == itemId).FirstOrDefault();

            var player = this.context.Players.Where(p => p.UserId == userId).FirstOrDefault();

            var inventory = GetPlayerInventory(player.UserId);

            var ownedItem = inventory.BoughtItems.Where(oi => oi.Name == existingItem.Name).FirstOrDefault();

            if(ownedItem!=null)
            {
                return OwnedItem;
            }

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
                return NotEnoughGold;
            }
            player.Gold -= itemToBuy.Price;

            player.Inventory.Add(itemToBuy);

            this.context.SaveChanges();

            return null;

        }

        public bool HeroHasItem(Hero hero, Item item)
        {
            if (item.ItemType.ToString() == Weapon)
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

        private void SetItemStats(Item item)
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
        public bool ExistingItem(string itemName)
        {
            var existingItem = this.context.Items.Where(i => i.Name == itemName).FirstOrDefault();

            if (existingItem != null)
            {
                return true;
            }
            return false;
        }

        private PlayerInventoryViewModel GetPlayerInventory(string userId)
           => this.context.Players
             .Where(p => p.UserId == userId)
             .ProjectTo<PlayerInventoryViewModel>(this.mapper)
            .FirstOrDefault();
    }
}
