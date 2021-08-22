using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static BattleRoyale.Data.Constants.DatabaseConstants;

namespace BattleRoyale.Models.AuctionItems
{
    public class AllAuctionItemsQueryModel
    {
        public const int ItemsPerPage = ItemPerPage;
        public string HeroType { get; init; }
        public string ItemType { get; init; }

        public AuctionItemSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalItems { get; set; }

        public IEnumerable<AuctionItemModel> Items { get; set; }
    }
}
