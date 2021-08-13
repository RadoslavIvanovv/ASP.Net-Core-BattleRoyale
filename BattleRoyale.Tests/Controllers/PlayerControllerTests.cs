

using BattleRoyale.Controllers;
using BattleRoyale.Data.Models;
using BattleRoyale.Models.Players;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BattleRoyale.Tests.Controllers
{
    public class PlayerControllerTests
    {
        //[Theory]
        //[InlineData("TestId",null)]
        //public void InventoryShoudReturnPlayerInventoryView(string id, IEnumerable<Item>items)
        //    => MyController<PlayersController>
        //    .Instance(inst => inst.WithUser(id))
        //    .Calling(c => c.Inventory())
        //    .ShouldHave()
        //    .Data(data => data
        //            .WithSet<PlayerInventoryViewModel>(inventory => inventory
        //                .Any(d =>
        //                d.Id==id &&
        //                    d.BoughtItems==items)))
        //    .AndAlso()
        //    .ShouldReturn()
        //    .View(view => view.WithModelOfType<PlayerInventoryViewModel>());

        //[Fact]
        //public void InfoShouldReturnMainHeroDetails()
        //    => MyController<PlayersController>
        //    .Instance(inst => inst.WithUser())
        //    .Calling(c => c.Info())
        //    .ShouldReturn()
        //    .View(view => view.WithModelOfType<PlayerInfoViewModel>());
    }
}
