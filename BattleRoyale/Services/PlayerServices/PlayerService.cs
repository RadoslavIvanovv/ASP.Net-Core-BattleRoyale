
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Models.Players;
using System.Linq;

using static BattleRoyale.Data.Constants.PlayerConstants;


namespace BattleRoyale.Services.PlayerServices
{
    public class PlayerService : IPlayerService
    {
        private readonly BattleRoyaleDbContext context;
        private readonly IConfigurationProvider mapper;

        public PlayerService(
            BattleRoyaleDbContext context,
            IConfigurationProvider mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public PlayerInfoViewModel GetPlayerInfo(string userId)
        {
            var player = this.context.Players.Where(p => p.UserId == userId).FirstOrDefault();

            var playerData = new PlayerInfoViewModel
            {
                Username = player.Name,
                Gold = player.Gold,
                Level = player.Level,
                ExperiencePoints = player.ExperiencePoints,
                RequiredExperiencePoints = player.RequiredExperiencePoints
            };

            return playerData;
        }

        public PlayerInventoryViewModel GetPlayerInventory(string userId)
        => this.context.Players
                .Where(p => p.UserId == userId)
                .ProjectTo<PlayerInventoryViewModel>(this.mapper)
            .FirstOrDefault();

        public void LevelUp(Player player)
        {
            player.Level++;
            player.Gold += GoldOnLevelUp;

            player.RequiredExperiencePoints = player.RequiredExperiencePoints + (int)(player.RequiredExperiencePoints * AdditionalRequiredExperiencePointsOnLevelUp);
        }
    }
}
