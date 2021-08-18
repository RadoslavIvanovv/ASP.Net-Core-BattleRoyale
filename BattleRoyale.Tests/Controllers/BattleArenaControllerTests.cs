
using BattleRoyale.Controllers;
using BattleRoyale.Models.Players;
using MyTested.AspNetCore.Mvc;
using Xunit;
using BattleRoyale.Tests.Mocks;
using BattleRoyale.Data.Models;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Data;

namespace BattleRoyale.Tests
{
    public class BattleArenaControllerTests
    {

        [Theory]
        [InlineData("player", 1)]
        public void AllPlayersShouldReturnQueryView(
            string name,
            int level
            )
            => MyController<BattleArenaController>
            .Calling(c => c.AllPlayers(new AllPlayersQueryModel
            {
                Name = name,
                Level = level
            }))
            .ShouldReturn()
            .View(view => view.WithModelOfType<AllPlayersQueryModel>());

        //[Fact]
        //public void PlayerDetailsShouldReturnView()
        //=> MyController<BattleArenaController>
        //    .Instance(inst => inst.WithUser(u=>u.WithIdentifier(CreateFakeUser().Id)).WithData(CreateFakeUser()))
        //    .Calling(c => c.Details(CreateFakeUser().Id))
        //    .ShouldReturn()
        //    .View(view => view.WithModelOfType<PlayerHeroViewModel>());

        //[Theory]
        //[InlineData("testId")]
        //public void FightActionShouldReturnFightView(string playerId)
        //=> MyController<BattleArenaController>
        //    .Calling(c => c.Fight(playerId))
        //    .ShouldReturn()
        //    .View(view => view.WithModelOfType<FightingHeroesViewModel>());

        //[Theory]
        //[InlineData(1, 2)]
        //public void EndFightActionShouldReturnHeroAfterFightView(int heroId, int remainingHealth)
        //=> MyController<BattleArenaController>
        //    .Instance(inst => inst.WithUser(u=>u.WithIdentifier(ContextMock.CreateFakeUser().Id)).WithData(d=>d.WithEntities<BattleRoyaleDbContext>()))
        //    .Calling(c => c.EndFight(heroId, remainingHealth))
        //    .ShouldReturn()
        //    .View(view =>
        //    view.WithModelOfType<AfterFightHeroModel>());


    }

}
