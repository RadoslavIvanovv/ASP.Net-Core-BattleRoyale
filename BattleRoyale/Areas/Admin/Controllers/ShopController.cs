using BattleRoyale.Models.Shop;
using BattleRoyale.Services.ItemServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static BattleRoyale.Data.Constants.ShopControllerConstants;

namespace BattleRoyale.Areas.Admin.Controllers
{
    public class ShopController : AdminController
    {
        private readonly IItemService itemService;

        public ShopController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public IActionResult Add() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Add(ShopItemModel item)
        {
            if (this.itemService.ExistingItem(item.Name))
            {
                this.ModelState.AddModelError(nameof(item.Name), ExistingItem);
            }

            if (!ModelState.IsValid)
            {
                return View(item);
            }

            this.itemService.Add(item);

            return RedirectToAction("AddedItem", "Shop");
        }

        public IActionResult AddedItem() => View();
    }
}
