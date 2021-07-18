using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Infrastructure;
using BattleRoyale.Models.Shop;
using BattleRoyale.Models.Shops;
using BattleRoyale.Services.ItemServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BattleRoyale.Controllers
{
    public class ShopsController : Controller
    {
        private readonly BattleRoyaleDbContext context;
        private readonly ItemService itemService;

        public ShopsController(BattleRoyaleDbContext context)
        {
            this.context = context;
            itemService = new ItemService();
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(ShopItemModel item)
        {
            var existingItem = this.context.Items.Where(i => i.Name == item.Name).FirstOrDefault();

            if (existingItem != null)
            {
                this.ModelState.AddModelError(nameof(item.Name),"Item with this name already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(item);
            }

            var itemData = new Item
            {
                Id = item.Id,
                Name = item.Name,
                Stats = item.Stats,
                Price = item.Price,
                ItemType=item.ItemType,
                ImageUrl = item.ImageUrl,
                HeroType = item.HeroType
            };

            itemService.SetItemStats(itemData);

            this.context.Items.Add(itemData);      

            this.context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            var items = this.context.Items
                .Select(i => new ShopItemModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Stats = i.Stats,
                    Price = i.Price,
                    ItemType = i.ItemType,
                    ImageUrl = i.ImageUrl,
                    PassiveEffect = i.PassiveEffect,
                    HeroType = i.HeroType
                }).ToList();

            return View(items);
        }

        public IActionResult BuyItem() => View();

        [HttpPost]
        public IActionResult BuyItem(BuyItemViewModel item)
        {
            var existingItem = this.context.Items.Where(i => i.Id == item.Id).FirstOrDefault();

            if (existingItem == null)
            {
                return BadRequest();
            }

            var player = this.context.Players.Where(p => p.UserId == this.User.GetId()).FirstOrDefault();

            var itemToBuy = new Item
            {
                Id = existingItem.Id,
                Name = existingItem.Name,
                Stats = existingItem.Stats,
                Price = existingItem.Price,
                ItemType = existingItem.ItemType,
                ImageUrl = existingItem.ImageUrl,
                PassiveEffect = existingItem.PassiveEffect,
                HeroType = existingItem.HeroType,
            };

            player.Inventory.Add(itemToBuy);

            if (player.Gold < itemToBuy.Price)
            {
                return BadRequest();
            }
            player.Gold -= itemToBuy.Price;

            return RedirectToAction("All", "Shops");
        }
    }
}
