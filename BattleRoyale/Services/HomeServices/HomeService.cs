

using BattleRoyale.Data;
using BattleRoyale.Models.Heroes;
using System.Collections.Generic;
using System.Linq;

namespace BattleRoyale.Services.HomeServices
{
    public class HomeService:IHomeService
    {
        private readonly BattleRoyaleDbContext context;

        public HomeService(BattleRoyaleDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<HeroIndexViewModel> GetTopHeroes()
            => this.context.Heroes
                .Select(h => new HeroIndexViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    Owner = h.Player.Name,
                    Level = h.Level,
                    OverallPower = h.OverallPower
                })
                .OrderByDescending(h => h.OverallPower).Take(10).ToList();
    }
}
