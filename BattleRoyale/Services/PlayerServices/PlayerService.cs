

using BattleRoyale.Data.Models;


namespace BattleRoyale.Services.PlayerServices
{
    public class PlayerService : IPlayerService
    {
        public void LevelUp(Player player)
        {
            player.Level++;
            player.Gold += 1000;

            player.RequiredExperiencePoints = player.RequiredExperiencePoints + (int)(player.RequiredExperiencePoints * 0.33);
        }
    }
}
