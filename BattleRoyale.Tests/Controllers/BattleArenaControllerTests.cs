
using BattleRoyale.Controllers;
using BattleRoyale.Models.Players;
using MyTested.AspNetCore.Mvc;
using Xunit;

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

        //[Theory]
        //[InlineData("TestId")]
        //public void PlayerDetailsShouldReturnView(string userId)
        //=>  MyController<BattleArenaController>
        //    .Instance(inst => inst.WithData(userId))
        //    .Calling(c => c.Details(userId))
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
        //[InlineData(1, 2, "TestId")]
        //public void EndFightActionShouldReturnHeroAfterFightView(int heroId, int remainingHealth, string userId)
        //=> MyController<BattleArenaController>
        //    .Instance(inst => inst.WithUser(userId))
        //    .Calling(c => c.EndFight(heroId, remainingHealth))
        //    .ShouldHave()
        //    .ActionAttributes(att => att
        //    .RestrictingForAuthorizedRequests())
        //    .AndAlso()
        //    .ShouldReturn()
        //    .View(view =>
        //    view.WithModelOfType<AfterFightHeroModel>());


    }

}
