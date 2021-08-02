using BattleRoyale.Data;
using BattleRoyale.Models;
using BattleRoyale.Models.Heroes;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace BattleRoyale.Controllers
{
    public class HomeController : Controller
    {
        private readonly BattleRoyaleDbContext context;

        public HomeController(BattleRoyaleDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var topHeroes = this.context.Heroes
                .Select(h => new HeroIndexViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    Owner = h.Player.Name,
                    Level = h.Level,
                    OverallPower = h.OverallPower
                })
                .OrderByDescending(h => h.OverallPower).Take(10).ToList();           
            
            return View(topHeroes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
