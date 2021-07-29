
using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Data.Models.ItemTypes;
using BattleRoyale.Models.Shop;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Models.Items
{
        public class AllItemsQueryModel
        {
            public const int ItemsPerPage = 20;

            public string HeroType { get; init; }
            public string ItemType { get; init; }

            [Display(Name = "Search by text")]
            public string SearchTerm { get; init; }

            public ItemSorting Sorting { get; init; }

            public int CurrentPage { get; init; } = 1;

            public int TotalItems { get; set; }

            public IEnumerable<HeroType> HeroTypes { get; set; }
            public IEnumerable<ItemType> ItemTypes { get; set; }

            public IEnumerable<ShopItemModel> Items { get; set; }
        }
}
