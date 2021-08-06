
using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Data.Models.ItemTypes;
using BattleRoyale.Models.Shop;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static BattleRoyale.Data.Constants.DatabaseConstants;

namespace BattleRoyale.Models.Items
{
        public class AllItemsQueryModel
        {
            public const int ItemsPerPage = ItemPerPage;
            public string HeroType { get; init; }
            public string ItemType { get; init; }

            public ItemSorting Sorting { get; init; }

            public int CurrentPage { get; init; } = 1;

            public int TotalItems { get; set; }

            public IEnumerable<HeroType> HeroTypes { get; set; }
            public IEnumerable<ItemType> ItemTypes { get; set; }

            public IEnumerable<ShopItemModel> Items { get; set; }
        }
}
