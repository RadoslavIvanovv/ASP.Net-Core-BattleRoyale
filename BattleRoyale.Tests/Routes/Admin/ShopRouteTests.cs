

using BattleRoyale.Areas.Admin.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace BattleRoyale.Tests.Routes.Admin
{
    public class ShopRouteTests
    {
        [Fact]
        public void GetAddShouldBeRoutedCorrectly()
   => MyRouting
       .Configuration()
       .ShouldMap("Admin/Shop/Add")
       .To<ShopController>(c => c.Add());
    }
}
