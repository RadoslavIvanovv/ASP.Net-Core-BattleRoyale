
using BattleRoyale.Data.Models;
using BattleRoyale.Models.AuctionItems;
using BattleRoyale.Models.Items;
using BattleRoyale.Models.Shop;

namespace BattleRoyale.Services.ItemServices
{
    public interface IItemService
    {
        void Add(ShopItemModel item);
        AllItemsQueryModel All(
    string heroType = null,
    string itemType = null,
    ItemSorting sorting = ItemSorting.Name,
    int currentPage = 1,
    int itemsPerPage = int.MaxValue);
        string BuyItem(string userId, int itemId);
        bool HeroHasItem(Hero hero, Item item);
        bool ExistingItem(string itemName);
        AuctionItemInfoModel GetItem(int itemId);
    }
}
