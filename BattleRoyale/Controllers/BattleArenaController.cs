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

        public IActionResult AllPlayers()
        {
            var players = this.context.Players
                .Select(i => new PlayerListingViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Level = i.Level
                }).ToList();

            return View(players);
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
                ImageUrl = hero.ImageUrl,
                Level=hero.Level,
                ExperiencePoints=hero.ExperiencePoints,
                UserId=hero.PlayerId
            };

            return heroData;

        }
    }
}
