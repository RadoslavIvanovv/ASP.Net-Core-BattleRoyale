

using BattleRoyale.Data.Models;

namespace BattleRoyale.Tests.Controllers.Models
{
    class HeroModelTest
    {
        public int Id => 1;
        public string Name => "TestName";
        public Player Player => new Player { Id = "TestId" };
        public string HeroType => "Mage";
    }
}
