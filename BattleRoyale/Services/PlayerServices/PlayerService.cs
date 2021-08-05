

using BattleRoyale.Data.Models;

using static BattleRoyale.Data.Constants.PlayerConstants;


namespace BattleRoyale.Services.PlayerServices
{
    public class PlayerService : IPlayerService
    {
        public void LevelUp(Player player)
        {
            player.Level++;
            player.Gold += GoldOnLevelUp;

            player.RequiredExperiencePoints = player.RequiredExperiencePoints + (int)(player.RequiredExperiencePoints * AdditionalRequiredExperiencePointsOnLevelUp);
        }
    }
}
