
using BattleRoyale.Controllers;
using BattleRoyale.Models.Players;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace BattleRoyale.Tests.Routes
{
    public class BattleArenaRouteTests
    {
        [Fact]
        public void AllPlayersShouldReturnQueryModel()
           => MyRouting
               .Configuration()
               .ShouldMap("/BattleArena/AllPlayers")
               .To<BattleArenaController>(c => c.AllPlayers(With.No<AllPlayersQueryModel>()));

        [Theory]
        [InlineData("TestId")]
        public void DetailsShouldReturnInfoAboutPlayeHero(string playerId)
           => MyRouting
               .Configuration()
               .ShouldMap($"/BattleArena/Details?playerId={playerId}")
               .To<BattleArenaController>(c => c.Details(playerId));

        [Theory]
        [InlineData("TestId")]
        public void FightShouldReturnCorrectFightingHEroesViewModel(string playerId)
          => MyRouting
              .Configuration()
              .ShouldMap($"/BattleArena/Fight?playerId={playerId}")
              .To<BattleArenaController>(c => c.Fight(playerId));

        [Theory]
        [InlineData(1,100)]
        public void EndFightShouldReturnCorrectAfterFightHrroModel(int heroId,int remainingHealth)
          => MyRouting
              .Configuration()
              .ShouldMap($"/BattleArena/EndFight?heroId={heroId}&&remainingHealth={remainingHealth}")
              .To<BattleArenaController>(c => c.EndFight(heroId,remainingHealth));
    }
}
