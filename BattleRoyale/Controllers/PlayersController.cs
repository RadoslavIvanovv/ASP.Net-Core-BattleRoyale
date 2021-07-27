using BattleRoyale.Data;
using BattleRoyale.Infrastructure;
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
                .Where(p=>p.UserId==this.User.GetId())
                .Select(pi => new PlayerInventoryViewModel
                {
                    Id = pi.Id,
                    BoughtItems= pi.Inventory
                }).FirstOrDefault();

            return View(inventory);
        }

        public IActionResult Info()
        {
            var player = this.context.Players.Where(p => p.UserId == this.User.GetId()).FirstOrDefault();

            var playerData = new PlayerInfoViewModel
            {
                Username = player.UserName,
                Gold = player.Gold,
                Level = player.Level,
                ExperiencePoints = player.ExperiencePoints,
                RequiredExperiencePoints=player.RequiredExperiencePoints
            };

            return View(playerData);
        }
       
    }
}
