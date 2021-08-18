

using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Models.Players;
using BattleRoyale.Services.BattleArenaServices.Models;
using BattleRoyale.Services.HeroServices;
using System.Linq;
using BattleRoyale.Services.PlayerServices;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using static BattleRoyale.Data.Constants.PlayerConstants;
using static BattleRoyale.Data.Constants.HeroConstants;
using static BattleRoyale.Data.Constants.PlayerControllerConstants;
using static BattleRoyale.Data.Constants.HeroControllerConstants;

namespace BattleRoyale.Services.BattleArenaServices
{
    public class BattleArenaService:IBattleArenaService
    {
        private readonly BattleRoyaleDbContext context;
        private readonly IHeroService heroService;
        private readonly IPlayerService playerService;
        private readonly IConfigurationProvider mapper;

        public BattleArenaService(
            BattleRoyaleDbContext context,
            IHeroService heroService,
            IPlayerService playerService,
            IConfigurationProvider mapper)
        {
            this.context = context;
            this.heroService = heroService;
            this.playerService = playerService;
            this.mapper = mapper;
        }

        public AllPlayersServiceModel All(
            string name = null,
            int level=1,
            string searchTerm = null,
            PlayerSorting sorting = PlayerSorting.Name,
            int currentPage = 1,
            int playersPerPage = int.MaxValue)
        {
            var playersQuery = this.context.Players.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                playersQuery = playersQuery.Where(p =>
                    (p.Name).ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                playersQuery = playersQuery.Where(p =>
                    (p.Name).ToLower().Contains(searchTerm.ToLower()));
            }

            playersQuery = sorting switch
            {
                PlayerSorting.Level => playersQuery.OrderByDescending(p => p.Level),
                PlayerSorting.Name => playersQuery.OrderBy(p => p.Name),
                _ => playersQuery.OrderByDescending(p => p.Id)
            };

            var totalPlayers = playersQuery.Count();

            var players = playersQuery
                .Skip((currentPage - 1) * playersPerPage)
                .Take(playersPerPage)
                .ProjectTo<PlayerListingViewModel>(this.mapper)
                .ToList();

            return new AllPlayersServiceModel
            {
                TotalPlayers=totalPlayers,
                CurrentPage=currentPage,
                PlayersPerPage=playersPerPage,
                Players=players
            };
        }

        public PlayerHeroViewModel Details(string playerId)
        {
            var hero = this.context.Players
              .Where(p => p.Id == playerId)
              .Select(h => h.Heroes.Where(h => h.IsMain == true).FirstOrDefault()).FirstOrDefault();

            var heroDetails = new Hero
            {
                Id = hero.Id,
                Name = hero.Name,
                ImageUrl = hero.ImageUrl,
                Level = hero.Level,
                ExperiencePoints = hero.ExperiencePoints,
                RequiredExperiencePoints = hero.RequiredExperiencePoints,
                TotalExperiencePoints = hero.TotalExperiencePoints,
                Attack = hero.Attack,
                MagicAttack = hero.MagicAttack,
                Health = hero.Health,
                Armor = hero.Armor,
                MagicResistance = hero.MagicResistance,
                Speed = hero.Speed,
                OverallPower = hero.OverallPower,
                HeroType = hero.HeroType,
                Items = hero.Items
            };

            var playerData = new PlayerHeroViewModel
            {
                Id = playerId,
                Hero = heroDetails
            };

            return playerData;
        }

        public FightingHeroesViewModel Fight(string userId,string playerId)
        {
            var attackingHero = this.context.Players
                .Where(p => p.UserId == userId)
                .Select(p => p.Heroes.Where(h => h.IsMain == true).FirstOrDefault()).FirstOrDefault();

            var attacker = GetHero(attackingHero);

            var defendingHero = this.context.Players
          .Where(p => p.Id == playerId)
          .Select(p => p.Heroes.Where(h => h.IsMain == true).FirstOrDefault()).FirstOrDefault();

            var defender = GetHero(defendingHero);

            var fight = new FightingHeroesViewModel
            {
                UserId = userId,
                Attacker = attacker,
                Defender = defender
            };

            while (attacker.RemainingHealth > 0 && defender.RemainingHealth > 0)
            {

                if (attacker.Speed >= defender.Speed)
                {
                    heroService.Attack(attacker, defender);
                    if (defender.RemainingHealth <= 0)
                    {
                        return fight;
                    }
                    heroService.Attack(defender, attacker);
                    if (attacker.RemainingHealth <= 0)
                    {
                        return fight;
                    }
                }
                else
                {
                    heroService.Attack(defender, attacker);
                    if (attacker.RemainingHealth <= 0)
                    {
                        return fight;
                    }
                    heroService.Attack(attacker, defender);
                    if (defender.RemainingHealth <= 0)
                    {
                        return fight;
                    }
                }
            }
            return null;
        }

        public AfterFightHeroModel EndFight(string userId,int heroId, int remainingHealth)
        {
            var heroData = this.context.Heroes.Where(h => h.Id == heroId).FirstOrDefault();

            var playerData = this.context.Players.Where(p => p.UserId == userId).FirstOrDefault();

            if (remainingHealth <= 0)
            {
                heroData.ExperiencePoints += HeroExperiencePointsGainOnDefeat;
                heroData.TotalExperiencePoints += HeroExperiencePointsGainOnDefeat;
                playerData.ExperiencePoints += PlayerExperiencePointsGainOnDefeat;
                playerData.Gold += PlayerGoldGainOnDefeat;
            }
            else
            {
                heroData.ExperiencePoints += HeroExperiencePointsGainOnVictory;
                heroData.TotalExperiencePoints += HeroExperiencePointsGainOnVictory;
                playerData.ExperiencePoints += PlayerExperiencePointsGainOnVictory;
                playerData.Gold += PlayerGoldGainOnVictory;
            }

            if (playerData.ExperiencePoints >= playerData.RequiredExperiencePoints)
            {
                var remainingExpPoints = playerData.ExperiencePoints - playerData.RequiredExperiencePoints;
                if (playerData.Level< MaxPlayerLevel)
                {
                    playerService.LevelUp(playerData);
                }
                
                playerData.ExperiencePoints = remainingExpPoints;
            }

            if (heroData.ExperiencePoints >= heroData.RequiredExperiencePoints)
            {
                var remainingExpPoints = heroData.ExperiencePoints - heroData.RequiredExperiencePoints;

                if (heroData.Level < MaxHeroLevel)
                {
                    heroService.LevelUp(heroData);
                }            
                heroData.ExperiencePoints = remainingExpPoints;
            }

            var hero = new AfterFightHeroModel
            {
                Id = heroId,
                RemainingHealth = remainingHealth
            };

            this.context.SaveChanges();

            return hero;
        }

        private HeroFightViewModel GetHero(Hero hero)
        {
            var heroData = new HeroFightViewModel
            {
                Id = hero.Id,
                Name = hero.Name,
                MaxHealth = hero.Health,
                Attack = hero.Attack,
                MagicAttack = hero.MagicAttack,
                RemainingHealth = hero.Health,
                MaxArmor = hero.Armor,
                RemainingArmor = hero.Armor,
                MaxMagicResistance = hero.MagicResistance,
                RemainingMagicResistance = hero.MagicResistance,
                Speed = hero.Speed,
                OverallPower = hero.OverallPower,
                ImageUrl = hero.ImageUrl,
                Level = hero.Level,
                ExperiencePoints = hero.ExperiencePoints,
                UserId = hero.PlayerId
            };

            return heroData;

        }
    }
}
