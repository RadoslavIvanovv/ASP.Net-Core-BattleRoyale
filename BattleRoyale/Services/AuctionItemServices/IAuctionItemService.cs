

using BattleRoyale.Models.AuctionItems;

namespace BattleRoyale.Services.AuctionItemServices
{
    public interface IAuctionItemService
    {
        AllAuctionItemsQueryModel All(AuctionItemSorting sorting = AuctionItemSorting.LowestLevel,
            int currentPage = 1,
            int itemsPerPage = int.MaxValue);

        AuctionItemInfoModel Info(string playerId, int itemId);
    }
}
