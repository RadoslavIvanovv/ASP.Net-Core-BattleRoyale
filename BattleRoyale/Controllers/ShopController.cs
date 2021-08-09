using BattleRoyale.Infrastructure;
using BattleRoyale.Models.Items;
using BattleRoyale.Models.Shop;
using BattleRoyale.Services.ItemServices;
using Microsoft.AspNetCore.Mvc;

namespace BattleRoyale.Controllers
{
    public class ShopController : Controller
    {
        private readonly IItemService itemService;

        public ShopController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(ShopItemModel item)
        {
            if (this.itemService.ExistingItem(item.Name))
            {
                this.ModelState.AddModelError(nameof(item.Name), $"Item with name '{item.Name}' already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(item);
            }

            this.itemService.Add(item);

            return RedirectToAction("All", "Shop");
        }

        public IActionResult All([FromQuery] AllItemsQueryModel query)
        {
            var queryResult = this.itemService.All(
             query.HeroType,
             query.ItemType,
            query.Sorting,
            query.CurrentPage,
            AllItemsQueryModel.ItemsPerPage);

            query.TotalItems = queryResult.TotalItems;
            query.HeroTypes = queryResult.HeroTypes;
            query.ItemTypes = queryResult.ItemTypes;
            query.Items = queryResult.Items;

            return View(query);
        }
    

        public IActionResult BuyItem(int itemId)
        {
            this.itemService.BuyItem(this.User.GetId(), itemId);

            return View();
        }
    }
}
