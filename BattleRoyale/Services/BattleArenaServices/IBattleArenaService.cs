

using BattleRoyale.Models.Heroes;
using BattleRoyale.Models.Players;
using BattleRoyale.Services.BattleArenaServices.Models;

namespace BattleRoyale.Services.BattleArenaServices
{
    public interface IBattleArenaService
    {
        AllPlayersServiceModel All(
           string name,
           int level,
           string searchTerm,
           PlayerSorting sorting,
           int currentPage,
           int playersPerPage);

        PlayerHeroViewModel Details(string playerId);
        FightingHeroesViewModel Fight(string userId,string playerId);
        AfterFightHeroModel EndFight(string userId,int heroId,int remainingHealth);
    }
}
