using System.Collections.Generic;

using static BattleRoyale.Data.Constants.DatabaseConstants;

namespace BattleRoyale.Models.AuctionItems
{
    public class AllAuctionItemsQueryModel
    {
        public const int ItemsPerPage = ItemPerPage;

        public AuctionItemSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalItems { get; set; }

        public IEnumerable<AuctionItemModel> Items { get; set; }
    }
}
