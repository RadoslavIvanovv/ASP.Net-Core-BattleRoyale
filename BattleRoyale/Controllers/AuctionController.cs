using BattleRoyale.Data.Models;
using BattleRoyale.Infrastructure;
using BattleRoyale.Models.AuctionItems;
using BattleRoyale.Models.Items;
using BattleRoyale.Services.AuctionItemServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BattleRoyale.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionItemService auctionItemService;

        public AuctionController(IAuctionItemService auctionItemService)
        {
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
            var result = this.auctionItemService.Add(auctioniItem, itemId, this.User.GetId());

            if (!ModelState.IsValid)
            {
                return View(auctioniItem);
            }
            if (result != null)
            {
                return BadRequest(result);
            }

            return RedirectToAction("Inventory","Players");
        }

        public IActionResult All([FromQuery] AllAuctionItemsQueryModel query)
        {
            var queryResult = this.auctionItemService.All(
                this.User.GetId(),
            query.Sorting,
            query.CurrentPage,
            AllItemsQueryModel.ItemsPerPage);

            query.TotalItems = queryResult.TotalItems;
            query.Items = queryResult.Items;

            return View(query);
        }

        public IActionResult Info(int itemId)
        {
            var playerData = this.auctionItemService.Info(itemId);

            return View(playerData);
        }

        [Authorize]
        public IActionResult Bid(int itemId)
        {
            var item = new Bid
            {
                AuctionItemId = itemId
            };

            return View(item);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Bid(Bid bid,int itemId)
        {
            var result = this.auctionItemService.Bid(bid, itemId, this.User.GetId());

            if (result != null) 
            {
                return BadRequest(result);
            }

            return RedirectToAction("BidSuccess", "Auction");
        }

        public IActionResult BidSuccess() => View();

        [Authorize]
        public IActionResult EndAuction(int itemId)
        {
            var topBid = this.auctionItemService.EndAuction(this.User.GetId(),itemId);

            if (topBid == null)
            {
                return View();
            }

            return View(topBid);
        }
    }
}
