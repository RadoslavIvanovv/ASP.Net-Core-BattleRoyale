using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Models.Shop;
using BattleRoyale.Services.ItemServices;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Add(AddShopItemFormModel item)
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
    }
}
