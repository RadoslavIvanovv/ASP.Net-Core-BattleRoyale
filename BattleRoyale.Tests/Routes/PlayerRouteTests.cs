

using BattleRoyale.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace BattleRoyale.Tests.Routes
{
    public class PlayerRouteTests
    {
        [Fact]
        public void PlayerInventoryShouldBeRoutedCorrectly()
             => MyRouting
                 .Configuration()
                 .ShouldMap($"/Players/Inventory")
                 .To<PlayersController>(c => c.Inventory());

        [Fact]
        public void PlayerInfoShouldBeRoutedCorrectly()
             => MyRouting
                 .Configuration()
                 .ShouldMap($"/Players/Info")
                 .To<PlayersController>(c => c.Info());
    }
}
