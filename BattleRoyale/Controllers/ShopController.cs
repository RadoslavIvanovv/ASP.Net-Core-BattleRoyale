using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Data.Models.ItemTypes;
using BattleRoyale.Infrastructure;
using BattleRoyale.Models.Items;
using BattleRoyale.Models.Shop;
using BattleRoyale.Services.ItemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BattleRoyale.Controllers
{
    public class ShopController : Controller
    {
        private readonly BattleRoyaleDbContext context;
        private readonly IItemService itemService;

        public ShopController(
            BattleRoyaleDbContext context,
            IItemService itemService)
        {
            this.context = context;
            this.itemService = itemService;
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(ShopItemModel item)
        {

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
