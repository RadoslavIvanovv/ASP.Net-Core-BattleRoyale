

using BattleRoyale.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace BattleRoyale.Tests.Controllers.Data
{
    public static class TestData
    {
        public static IEnumerable<Hero> TenStrongestHeroes
            => Enumerable.Range(0, 10).Select(h=>new Hero { }).OrderByDescending(h=>h.OverallPower);
    }
}
