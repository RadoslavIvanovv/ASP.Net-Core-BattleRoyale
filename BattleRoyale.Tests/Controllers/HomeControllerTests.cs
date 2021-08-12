

using BattleRoyale.Controllers;
using BattleRoyale.Data.Models;
using FluentAssertions;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

using static BattleRoyale.Tests.Controllers.Data.TestData;

namespace BattleRoyale.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexShouldReturnTopHeroes()
        => MyController<HomeController>
                .Instance(controller => controller
                    .WithData(TenStrongestHeroes))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<Hero>>()
                    .Passing(model => model.Should().HaveCount(10)));

        [Fact]
        public void ErrorShouldReturnView()
           => MyController<HomeController>
               .Instance()
               .Calling(c => c.Error())
               .ShouldReturn()
               .View();
    }
}
