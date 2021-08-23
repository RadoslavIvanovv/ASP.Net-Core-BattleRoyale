
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Data.Models.ItemTypes;
using BattleRoyale.Models.AuctionItems;
using BattleRoyale.Models.Players;
using System.Collections.Generic;
using System.Linq;

namespace BattleRoyale.Services.AuctionItemServices
{
    public class AuctionItemService:IAuctionItemService
    {
        private readonly BattleRoyaleDbContext context;
        private readonly IConfigurationProvider mapper;

        public AuctionItemService(BattleRoyaleDbContext context, IConfigurationProvider mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public AllAuctionItemsQueryModel All(
            AuctionItemSorting sorting = AuctionItemSorting.LowestLevel,
            int currentPage = 1,
            int itemsPerPage = int.MaxValue
            )
        {
            var itemsQuery = this.context.AuctionItems.AsQueryable();

            itemsQuery = sorting switch
            {
                AuctionItemSorting.Name => itemsQuery.OrderByDescending(i => i.Item.Name),
                AuctionItemSorting.LowestLevel => itemsQuery.OrderBy(i => i.Item.RequiredLevel),
                AuctionItemSorting.HighestLevel => itemsQuery.OrderByDescending(i => i.Item.RequiredLevel),
                AuctionItemSorting.ExpirationDate => itemsQuery.OrderByDescending(i => i.ExpirationDate),
                AuctionItemSorting.ItemType => itemsQuery.OrderBy(i => i.Item.ItemType),
                AuctionItemSorting.HeroType or _ => itemsQuery.OrderByDescending(i => i.Item.HeroType)
            };

            var totalItems = itemsQuery.Count();

            var items = itemsQuery
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ProjectTo<AuctionItemModel>(this.mapper);


            return new AllAuctionItemsQueryModel
            {
                TotalItems = totalItems,
                CurrentPage = currentPage,
                Sorting = sorting,
                Items = items
            };
        }

        public string Add(AuctionItemModel auctioniItem, int itemId,string userId)
        {
            var player = this.context.Players.Where(p => p.UserId == userId).FirstOrDefault();

            var playerInventory = this.context.Players
              .Where(p => p.UserId ==userId)
              .Select(pi => new PlayerInventoryViewModel
              {
                  Id = pi.Id,
                  BoughtItems = pi.Inventory
              }).FirstOrDefault();

            var existingItem = playerInventory.BoughtItems.Where(i => i.Id == itemId).FirstOrDefault();

            var itemData = new AuctionItem
            {
                ItemOwner = player,
                Item = existingItem,
                ExpirationDate = auctioniItem.ExpirationDate
            };

            this.context.AuctionItems.Add(itemData);

            this.context.SaveChanges();

            return null;
        }
        public string Bid(Bid bid,int itemId,string userId)
        {
            var player = this.context.Players.Where(p => p.UserId == userId).FirstOrDefault();

            var auctionItem = this.context.AuctionItems.Where(i => i.Id == itemId)
                .Select(ai => ai.Bids).FirstOrDefault();

            var item = this.context.Items.Where(i => i.Id == itemId).FirstOrDefault();

            if (player.Gold < bid.BidAmount)
            {
                return "Not enough gold";
            }

            var finalBid = new Bid
            {
                AuctionItemId = itemId,
                BidderName = player.Name,
                BidAmount = bid.BidAmount
            };

            this.context.Bids.Add(finalBid);

            auctionItem.Add(bid);

            player.Gold -= bid.BidAmount;

            this.context.SaveChanges();

            return null;

        }
        public AuctionItemInfoModel Info(string playerId,int itemId)
        {
            
            var player = this.context.Players
             .Where(p => p.UserId == playerId)
             .ProjectTo<PlayerInventoryViewModel>(this.mapper)
            .FirstOrDefault();

            var item = player.BoughtItems.Where(i => i.Id == itemId).FirstOrDefault();

            var itemData = new AuctionItemInfoModel
            {
                Name=item.Name,
                Stats=item.Stats,
                Price=item.Price,
                ItemType=item.ItemType.ToString(),
                RequiredLevel=item.RequiredLevel,
                AdditionalEffect=item.AdditionalEffect.ToString(),
                HeroType=item.HeroType.ToString()

            };

            return itemData;
        }

        public Bid EndAuction(int itemId)
        {
            var bids = this.context.Bids.Where(ai => ai.AuctionItemId == itemId).ToList();

            var winningBid = bids.Where(ai => ai.AuctionItemId == itemId).OrderByDescending(b => b.BidAmount).Take(1).FirstOrDefault();

            var winner = this.context.Players.Where(p => p.Name == winningBid.BidderName).FirstOrDefault();

            var topBid = new Bid
            {
                AuctionItemId = itemId,
                BidderName = winningBid.BidderName,
                BidAmount = winningBid.BidAmount
            };

            this.context.Bids.Remove(winningBid);

            foreach (var bid in bids)
            {
                var player = this.context.Players.Where(p => p.Name == bid.BidderName).FirstOrDefault();
                player.Gold += bid.BidAmount;
                this.context.Bids.Remove(bid);
            }

            var auctionItem = this.context.AuctionItems.Where(ai => ai.Id == itemId).FirstOrDefault();

            var item = auctionItem.Item;

            item.PlayerId = winner.Id;

            this.context.AuctionItems.Remove(auctionItem);
            this.context.SaveChanges();


            return topBid;
        }
    }
}
