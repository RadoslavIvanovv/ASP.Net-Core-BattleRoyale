

using BattleRoyale.Controllers;
using BattleRoyale.Data.Models;
using BattleRoyale.Models.Pets;
using BattleRoyale.Tests.Controllers.Models;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace BattleRoyale.Tests.Controllers
{
    public class PetControllerTests
    {
        private HeroModelTest hero= new HeroModelTest();

        [Fact]
        public void HeroShouldBeAdded()
           => MyController<PetsController>
           .Calling(c => c.Add())
           .ShouldReturn()
           .View();

        [Fact]
        public void AddPetShouldWorkProperlyWithCorrectData()
        => MyController<PetsController>
            .Instance(inst => inst.WithUser(u => u.WithIdentifier(hero.Player.Id)))
            .Calling(c => c.Add(new AddPetFormModel
            {
                Name="TestName",
                Stats=100,
                Type="Tiger",
                HeroId=hero.Id
            }))
            .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Pet>(pet => pet
                        .Any(d =>
                            d.Name == hero.Name &&
                            d.Stats == 100 &&
                            d.Type=="Tiger"&&
                        d.HeroId==hero.Id)))
            .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HeroesController>(c => c.All(hero.Id)));


        [Fact]
        public void RemoveShouldSuccessfullyRemovePetFormHeroAndReturnView()
        => MyController<PetsController>
            .Instance(inst => inst.WithUser(hero.Player.Id))
            .Calling(c => c.Remove(hero.Id))
            .ShouldReturn()
            .View();
    }
}
