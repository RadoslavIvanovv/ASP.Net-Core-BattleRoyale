

using BattleRoyale.Models.Heroes;
using System.Collections.Generic;

namespace BattleRoyale.Services.HomeServices
{
    public interface IHomeService
    {
        public IEnumerable<HeroIndexViewModel> GetTopHeroes();
    }
}
