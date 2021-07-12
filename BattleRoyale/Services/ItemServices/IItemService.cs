
using BattleRoyale.Data.Models;

namespace BattleRoyale.Services.ItemServices
{
    public interface IItemService
    {
        string GetItemType(Item item);
        void SetItemStats(Item item);
    }
}
