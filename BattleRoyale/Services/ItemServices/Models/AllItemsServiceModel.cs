

using BattleRoyale.Models.Shop;
using System.Collections.Generic;

namespace BattleRoyale.Services.ItemServices.Models
{
    public class AllItemsServiceModel
    {
        public int CurrentPage { get; init; }

        public int ItemsPerPage { get; init; }

        public int TotalItems { get; init; }

        public IEnumerable<ShopItemModel> Players { get; init; }
    }
}
