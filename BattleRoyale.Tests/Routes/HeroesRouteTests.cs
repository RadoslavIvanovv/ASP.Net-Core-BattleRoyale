

using BattleRoyale.Controllers;
using BattleRoyale.Models.Heroes;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace BattleRoyale.Tests.Routes
{
    public class HeroesRouteTests
    {
        [Fact]
        public void GetAddShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Heroes/Add")
               .To<HeroesController>(c => c.Add());


        [Theory]
        [InlineData("TestName", "Mage")]
        public void PostAddShouldBeRoutedCorrectly(string name, string heroType)
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithMethod(HttpMethod.Post)
                   .WithLocation("/Heroes/Add")
                   .WithFormFields(new
                   {
                       Name = name,
                       Herotype = heroType
                   }))
               .To<HeroesController>(c => c.Add(new HeroModel
               {
                   Name = name,
                   HeroType = heroType
               }))
               .AndAlso()
               .ToValidModelState();

        [Theory]
        [InlineData(1)]
        public void GetRemoveShouldBeRoutedCorrectly(int heroId)
            => MyRouting
                .Configuration()
                .ShouldMap($"/Heroes/Remove?heroId={heroId}")
                .To<HeroesController>(c => c.Remove(heroId));
    }
}
