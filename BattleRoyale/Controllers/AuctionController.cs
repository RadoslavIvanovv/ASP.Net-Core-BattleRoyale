using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Infrastructure;
using BattleRoyale.Models.AuctionItems;
using BattleRoyale.Models.Items;
using BattleRoyale.Models.Players;
using BattleRoyale.Services.AuctionItemServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BattleRoyale.Controllers
{
    public class AuctionController : Controller
    {
        private readonly BattleRoyaleDbContext context;
        private readonly IAuctionItemService auctionItemService;

        public AuctionController(BattleRoyaleDbContext context, IAuctionItemService auctionItemService)
        {
            this.context = context;
            this.auctionItemService = auctionItemService;
        }

        [Authorize]
        public IActionResult Add(int itemId)
        {
            var item = new AuctionItemModel
            {
                Id = itemId
            };

            return View(item);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AuctionItemModel auctioniItem, int itemId)
        {
            var player = this.context.Players.Where(p => p.UserId == this.User.GetId()).FirstOrDefault();

            var playerInventory=this.context.Players
              .Where(p => p.UserId == this.User.GetId())
              .Select(pi => new PlayerInventoryViewModel
              {
                  Id = pi.Id,
                  BoughtItems = pi.Inventory
              }).FirstOrDefault();

            var existingItem = playerInventory.BoughtItems.Where(i => i.Id==itemId).FirstOrDefault();

            var itemData = new AuctionItem
            {
                ItemOwner= player.Name,
                Item= existingItem,
                ExpirationDate= auctioniItem.ExpirationDate,
            };

            this.context.AuctionItems.Add(itemData);

            this.context.SaveChanges();

            return RedirectToAction("Inventory","Players");
        }

        public IActionResult All([FromQuery] AllAuctionItemsQueryModel query)
        {
            var queryResult = this.auctionItemService.All(
            query.Sorting,
            query.CurrentPage,
            AllItemsQueryModel.ItemsPerPage);

            query.TotalItems = queryResult.TotalItems;
            query.Items = queryResult.Items;

            return View(query);
        }
    }
}
