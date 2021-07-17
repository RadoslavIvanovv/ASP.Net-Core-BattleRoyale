using BattleRoyale.Data;
using BattleRoyale.Models.Players;
using Microsoft.AspNetCore.Mvc;
using BattleRoyale.Infrastructure;
using System.Linq;

namespace BattleRoyale.Controllers
{
    public class PlayersController : Controller
    {
        private readonly BattleRoyaleDbContext context;

        public PlayersController(BattleRoyaleDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Inventory()
        {
            var userId = this.User.GetId();

            var inventory = this.context.Players
                .Where(p => p.UserId == userId)
                .Select(pi => new PlayerInventoryViewModel
                {
                    Id = pi.Id,
                    BoughtItems= pi.Inventory
                });

            return View(inventory);
        }
    }
}
