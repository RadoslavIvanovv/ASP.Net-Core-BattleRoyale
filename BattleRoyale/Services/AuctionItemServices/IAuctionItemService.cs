

using BattleRoyale.Data.Models;
using BattleRoyale.Models.AuctionItems;

namespace BattleRoyale.Services.AuctionItemServices
{
    public interface IAuctionItemService
    {
        AllAuctionItemsQueryModel All
            (string userId,
            AuctionItemSorting sorting = AuctionItemSorting.LowestLevel,
            int currentPage = 1,
            int itemsPerPage = int.MaxValue
            );

        AuctionItemInfoModel Info(string playerId, int itemId);

        string Add(AuctionItemModel auctioniItem, int itemId,string userId);
        string Bid(Bid bid, int itemId, string userId);

        Bid EndAuction(string userId,int itemId);
    }
}
