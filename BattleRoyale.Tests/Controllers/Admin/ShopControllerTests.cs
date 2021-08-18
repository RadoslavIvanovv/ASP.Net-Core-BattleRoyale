

using BattleRoyale.Areas.Admin.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace BattleRoyale.Tests.Controllers.Admin
{
    public class ShopControllerTests
    {
        [Fact]
        public void HeroShouldBeAdded()
        => MyController<ShopController>
        .Calling(c => c.Add())
       .ShouldReturn()
       .View();
    }
}
