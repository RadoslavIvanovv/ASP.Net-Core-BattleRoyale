using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Infrastructure;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Models.Players;
using BattleRoyale.Services.HeroServices;
using BattleRoyale.Services.PlayerServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BattleRoyale.Controllers
{
    public class BattleArenaController : Controller
    {
        private readonly BattleRoyaleDbContext context;
        private readonly HeroService heroServices;
        private readonly PlayerService playerServices;

        public BattleArenaController(BattleRoyaleDbContext context)
        {
            this.context = context;
            this.heroServices = new HeroService();
            this.playerServices = new PlayerService();
        }
        public IActionResult Fight(string playerId)
        {

            var attackingHero = this.context.Players
                .Where(p => p.UserId == this.User.GetId())
                .Select(p => p.Heroes.Where(h => h.IsMain == true).FirstOrDefault()).FirstOrDefault();

            var attacker = GetHero(attackingHero);

            var defendingHero = this.context.Players
          .Where(p => p.Id == playerId)
          .Select(p => p.Heroes.Where(h => h.IsMain == true).FirstOrDefault()).FirstOrDefault();

            var defender = GetHero(defendingHero);

            while (attacker.RemainingHealth > 0 && defender.RemainingHealth > 0)
            {

                if (attacker.Speed >= defender.Speed)
                {
                    heroServices.Attack(attacker, defender);
                    if (defender.RemainingHealth <= 0)
                    {
                        return RedirectToAction("EndFight", "BattleArena",attacker);
                    }
                    heroServices.Attack(defender, attacker);
                    if (attacker.RemainingHealth <= 0)
                    {
                        return RedirectToAction("EndFight", "BattleArena",attacker);
                    }
                }
                else
                {
                    heroServices.Attack(defender, attacker);
                    if (attacker.RemainingHealth <= 0)
                    {
                        return RedirectToAction("EndFight", "BattleArena", attacker);
                    }
                    heroServices.Attack(attacker, defender);
                    if (defender.RemainingHealth <= 0)
                    {
                        return RedirectToAction("EndFight", "BattleArena", attacker);
                    }
                }
            }

            return View();
        }

        public IActionResult AllPlayers([FromQuery] AllPlayersQueryModel query)
        {
            var playersQuery = this.context.Players.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                playersQuery = playersQuery.Where(p =>
                    (p.Name).ToLower().Contains(query.SearchTerm.ToLower()));
            }

            playersQuery = query.Sorting switch
            {
                PlayerSorting.Level => playersQuery.OrderByDescending(p => p.Level),
                PlayerSorting.Name => playersQuery.OrderBy(p => p.Name),
                _ => playersQuery.OrderByDescending(p => p.Id)
            };

            var totalPlayers = playersQuery.Count();

            var players = playersQuery
                .Skip((query.CurrentPage - 1) * AllPlayersQueryModel.PlayersPerPage)
                .Take(AllPlayersQueryModel.PlayersPerPage)
                .Select(p => new PlayerListingViewModel
                {
                    Id = p.Id,
                    Name=p.Name,
                    Level=p.Level
                })
                .ToList();

            query.TotalPlayers = totalPlayers;
            query.Players = players;

            return View(query);
        }
        public IActionResult Details(string playerId)
        {

            var hero = this.context.Players
               .Where(p => p.Id == playerId)
               .Select(h => h.Heroes.Where(h => h.IsMain == true).FirstOrDefault()).FirstOrDefault();

            if (hero == null)
            {
                return NotFound();
            }

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

            return View(playerData);
        }

        public IActionResult EndFight(HeroFightViewModel hero)
        {
            var heroData = this.context.Heroes.Where(h => h.Id == hero.Id).FirstOrDefault();

            var playerData = this.context.Players.Where(p=>p.UserId==this.User.GetId()).FirstOrDefault();

            if (hero.RemainingHealth <= 0)
            {
                heroData.ExperiencePoints += 50;
                playerData.ExperiencePoints += 50;
                playerData.Gold += 50;
            }
            else
            {
                heroData.ExperiencePoints += 100;
                playerData.ExperiencePoints += 100;
                playerData.Gold += 100;
            }

            if (playerData.ExperiencePoints >= playerData.RequiredExperiencePoints)
            {
                var remainingExpPoints = playerData.ExperiencePoints - playerData.RequiredExperiencePoints;

                playerServices.LevelUp(playerData);
                heroData.ExperiencePoints = remainingExpPoints;
            }

            if (heroData.ExperiencePoints >= heroData.RequiredExperiencePoints)
            {
                var remainingExpPoints = heroData.ExperiencePoints - heroData.RequiredExperiencePoints;

                heroServices.LevelUp(heroData);
                heroData.ExperiencePoints = remainingExpPoints;
            }

            this.context.SaveChanges();

            return View(hero);
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
                Level=hero.Level,
                ExperiencePoints=hero.ExperiencePoints,
                UserId=hero.PlayerId
            };

            return heroData;

        }
    }
}
