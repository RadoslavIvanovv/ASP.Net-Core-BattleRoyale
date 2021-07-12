using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Models.Shop;
using BattleRoyale.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BattleRoyale.Controllers
{
    public class ShopsController : Controller
    {
        private readonly BattleRoyaleDbContext context;

        public ShopsController(BattleRoyaleDbContext context)
        {
            this.context = context;
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddShopItemFormModel item)
        {
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
                ImageUrl = item.ImageUrl,
                HeroType = item.HeroType
            };

            this.context.Items.Add(itemData);      

            this.context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
