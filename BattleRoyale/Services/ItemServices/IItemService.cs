
using BattleRoyale.Data.Models;
using BattleRoyale.Models.Items;
using BattleRoyale.Models.Shop;

namespace BattleRoyale.Services.ItemServices
{
    public interface IItemService
    {
        AllItemsQueryModel All(
    string heroType = null,
    string itemType = null,
    ItemSorting sorting = ItemSorting.Name,
    int currentPage = 1,
    int itemsPerPage = int.MaxValue);
        void BuyItem(string userId, int itemId);
        string GetItemType(Item item);
        void SetItemStats(Item item);
        bool HeroHasItem(Hero hero, Item item);
        void Add(ShopItemModel item);
        bool ExistingItem(int itemId);
    }
}
