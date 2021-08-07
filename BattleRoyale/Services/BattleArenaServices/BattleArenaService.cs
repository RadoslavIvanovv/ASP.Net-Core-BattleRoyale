﻿

using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Models.Players;
using BattleRoyale.Services.BattleArenaServices.Models;
using BattleRoyale.Services.HeroServices;
using System.Linq;

using static BattleRoyale.Data.Constants.PlayerConstants;
using static BattleRoyale.Data.Constants.HeroConstants;
using BattleRoyale.Services.PlayerServices;

namespace BattleRoyale.Services.BattleArenaServices
{
    public class BattleArenaService:IBattleArenaService
    {
        private readonly BattleRoyaleDbContext context;
        private readonly IHeroService heroService;
        private readonly IPlayerService playerService;

        public BattleArenaService(
            BattleRoyaleDbContext context,
            IHeroService heroService,
            IPlayerService playerService)
        {
            this.context = context;
            this.heroService = heroService;
            this.playerService = playerService;
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
                .Select(p => new PlayerListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Level = p.Level,
                    UserId=p.UserId
                })
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
                playerData.ExperiencePoints += PlayerExperiencePointsGainOnDefeat;
                playerData.Gold += PlayerGoldGainOnDefeat;
            }
            else
            {
                heroData.ExperiencePoints += HeroExperiencePointsGainOnVictory;
                playerData.ExperiencePoints += PlayerExperiencePointsGainOnVictory;
                playerData.Gold += PlayerGoldGainOnVictory;
            }

            if (playerData.ExperiencePoints >= playerData.RequiredExperiencePoints)
            {
                var remainingExpPoints = playerData.ExperiencePoints - playerData.RequiredExperiencePoints;

                playerService.LevelUp(playerData);
                heroData.ExperiencePoints = remainingExpPoints;
            }

            if (heroData.ExperiencePoints >= heroData.RequiredExperiencePoints)
            {
                var remainingExpPoints = heroData.ExperiencePoints - heroData.RequiredExperiencePoints;

                heroService.LevelUp(heroData);
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