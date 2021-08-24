

using AutoMapper;
using AutoMapper.QueryableExtensions;
using BattleRoyale.Data;
using BattleRoyale.Models.Heroes;
using System.Collections.Generic;
using System.Linq;

namespace BattleRoyale.Services.HomeServices
{
    public class HomeService:IHomeService
    {
        private readonly BattleRoyaleDbContext context;
        private readonly IConfigurationProvider mapper;

        public HomeService(BattleRoyaleDbContext context,IConfigurationProvider mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<HeroIndexViewModel> GetTopHeroes()
            => this.context.Heroes
                .ProjectTo<HeroIndexViewModel>(this.mapper)
                .OrderByDescending(h => h.OverallPower).ThenByDescending(h=>h.TotalExperiencePoints).Take(10).ToList();
    }
}
