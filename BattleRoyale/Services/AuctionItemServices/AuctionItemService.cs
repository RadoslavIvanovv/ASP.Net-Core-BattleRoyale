
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
    }
}
