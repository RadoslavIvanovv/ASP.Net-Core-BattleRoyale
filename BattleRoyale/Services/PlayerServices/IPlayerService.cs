

using BattleRoyale.Data.Models;
using BattleRoyale.Models.Players;

namespace BattleRoyale.Services.PlayerServices
{
    public interface IPlayerService
    {
        PlayerInventoryViewModel GetPlayerInventory(string userId);
        PlayerInfoViewModel GetPlayerInfo(string userId);
        void LevelUp(Player player);
    }
}
