

using BattleRoyale.Controllers;
using BattleRoyale.Models.Pets;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace BattleRoyale.Tests.Routes
{
    public class PetRouteTests
    {
        [Fact]
        public void GetAddShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Pets/Add")
               .To<PetsController>(c => c.Add());


        [Theory]
        [InlineData("TestName", "Tiger")]
        public void PostCreateShouldBeRoutedCorrectly(string name, string petType)
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithMethod(HttpMethod.Post)
                   .WithLocation("/Pets/Add")
                   .WithFormFields(new
                   {
                       Name = name,
                       Type = petType
                   }))
               .To<PetsController>(c => c.Add(new AddPetFormModel
               {
                   Name = name,
                   Type = petType
               }))
               .AndAlso()
               .ToValidModelState();

        [Theory]
        [InlineData(1)]
        public void GetRemoveShouldBeRoutedCorrectly(int heroId)
            => MyRouting
                .Configuration()
                .ShouldMap($"/Pets/Remove?heroId={heroId}")
                .To<PetsController>(c => c.Remove(heroId));
    }
}
