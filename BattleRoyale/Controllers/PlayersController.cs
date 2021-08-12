using BattleRoyale.Data;
using BattleRoyale.Infrastructure;
using BattleRoyale.Services.PlayerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BattleRoyale.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayersController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [Authorize]
        public IActionResult Inventory()
        {
            var inventory = this.playerService.GetPlayerInventory(this.User.GetId());

            return View(inventory);
        }

        public IActionResult Info()
        {
            var playerData = this.playerService.GetPlayerInfo(this.User.GetId());

            return View(playerData);
        }
       
    }
}
