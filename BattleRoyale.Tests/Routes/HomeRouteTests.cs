

using BattleRoyale.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace BattleRoyale.Tests.Routes
{
    public class HomeRouteTests
    {
        [Fact]
        public void IndexShouldReturnQueryModel()
          => MyRouting
              .Configuration()
              .ShouldMap("/Home/Index")
              .To<HomeController>(c => c.Index());

        [Fact]
        public void ErrorShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
