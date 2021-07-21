using BattleRoyale.Data;
using BattleRoyale.Models.Players;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Inventory()
        {
            var inventory = this.context.Players
                .Select(pi => new PlayerInventoryViewModel
                {
                    Id = pi.Id,
                    BoughtItems= pi.Inventory
                }).FirstOrDefault();

            return View(inventory);
        }

       
    }
}
