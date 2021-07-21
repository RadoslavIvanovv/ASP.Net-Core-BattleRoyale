using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Models.Players;
using BattleRoyale.Services.HeroServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BattleRoyale.Controllers
{
    public class BattleArenaController : Controller
    {
        private readonly BattleRoyaleDbContext context;
        private readonly HeroService heroServices;
        private HeroFightViewModel attacker;
        private HeroFightViewModel defender;

        public BattleArenaController(BattleRoyaleDbContext context)
        {
            this.context = context;
            this.heroServices = new HeroService();
        }

        public IActionResult BeginFight() => View();

        [HttpPost]
        public IActionResult BeginFight(Hero attackingHero, Hero defendingHero)
        {
            attackingHero = this.context.Heroes.Where(h => h.Id == 1).FirstOrDefault();
            defendingHero = this.context.Heroes.Where(h => h.Id == 2).FirstOrDefault();

            attacker = new HeroFightViewModel
            {
                Id = attackingHero.Id,
                Name = attackingHero.Name,
                MaxHealth = attackingHero.Health,
                Attack = attackingHero.Attack,
                MagicAttack = attackingHero.MagicAttack,
                RemainingHealth = attackingHero.Health,
                MaxArmor = attackingHero.Armor,
                RemainingArmor = attackingHero.Armor,
                MaxMagicResistance = attackingHero.MagicResistance,
                RemainingMagicResistance = attackingHero.MagicResistance,
                Speed = attackingHero.Speed,
                ImageUrl = attackingHero.ImageUrl
            };

            defender = new HeroFightViewModel
            {
                Id = defendingHero.Id,
                Name = defendingHero.Name,
                Attack = defendingHero.Attack,
                MagicAttack = defendingHero.MagicAttack,
                MaxHealth = defendingHero.Health,
                RemainingHealth = defendingHero.Health,
                MaxArmor = defendingHero.Armor,
                RemainingArmor = defendingHero.Armor,
                MaxMagicResistance = defendingHero.MagicResistance,
                RemainingMagicResistance = defendingHero.MagicResistance,
                Speed = defendingHero.Speed,
                ImageUrl = defendingHero.ImageUrl
            };

            return RedirectToAction("Fight", "BattleArena");
        }

        public IActionResult Fight() => View(new FightingHeroesViewModel
        {
            Heroes = AddHeroes()
        });

        [HttpPost]
        public IActionResult Fight(Hero hero)
        {

            if (attacker.Speed >= defender.Speed)
            {
                heroServices.Attack(attacker, defender);
                if (defender.RemainingHealth <= 0)
                {
                    RedirectToAction("BattleArena","Victory");
                }
                heroServices.Attack(defender, attacker);
                if (attacker.RemainingHealth <= 0)
                {
                    RedirectToAction("BattleArena", "Defeat");
                }
            }
            else
            {
                heroServices.Attack(defender, attacker);
                if (attacker.RemainingHealth <= 0)
                {
                    RedirectToAction("BattleArena", "Defeat");
                }
                heroServices.Attack(attacker, defender);
                if (defender.RemainingHealth <= 0)
                {
                    RedirectToAction("BattleArena", "Victory");
                }               
            }

            return RedirectToAction("BattleArena", "Fight");
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

        public IActionResult Victory(Hero hero)
        {
            hero.ExperiencePoints += 100;
            return View(hero);
        }

        public IActionResult Defeat(Hero hero)
        {
            hero.ExperiencePoints += 50;
            return View(hero);
        }

        private List<HeroFightViewModel> AddHeroes()
        {
            List<HeroFightViewModel> heroes = new List<HeroFightViewModel>();
            heroes.Add(attacker);
            heroes.Add(defender);

            return heroes;
        }
    }
}
