using BattleRoyale.Infrastructure;
using BattleRoyale.Models.Players;
using Microsoft.AspNetCore.Mvc;

using BattleRoyale.Services.BattleArenaServices;
using Microsoft.AspNetCore.Authorization;

namespace BattleRoyale.Controllers
{
    public class BattleArenaController : Controller
    {
        private readonly IBattleArenaService battleArenaService;

        public BattleArenaController(IBattleArenaService battleArenaService)
        {
            this.battleArenaService = battleArenaService;
        }
        public IActionResult AllPlayers([FromQuery] AllPlayersQueryModel query)
        {
            var queryResult = this.battleArenaService.All(
                query.Name,
                query.Level,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllPlayersQueryModel.PlayersPerPage);

            query.TotalPlayers = queryResult.TotalPlayers;
            query.Players = queryResult.Players;

            return View(query);
        }
        public IActionResult Details(string playerId)
        {
            var playerData = this.battleArenaService.Details(playerId);

            return View(playerData);
        }

        [Authorize]
        public IActionResult Fight(string playerId)
        {
            var fight = this.battleArenaService.Fight(this.User.GetId(), playerId);

            return View(fight);
        }
        [Authorize]
        public IActionResult EndFight(int heroId,int remainingHealth)
        {
            var hero = this.battleArenaService.EndFight(this.User.GetId(), heroId, remainingHealth);

            return View(hero);
        }
    }
}
