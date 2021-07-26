using BattleRoyale.Data;
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

        public BattleArenaController(BattleRoyaleDbContext context)
        {
            this.context = context;
            this.heroServices = new HeroService();
        }
        public IActionResult Fight(FightingHeroesViewModel fight)
        {

            if (!fight.Heroes.Any())
            {
                fight.Heroes = AddHeroes();
            }

            var attacker = fight.Heroes[0];
            var defender = fight.Heroes[1];

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

            return View(fight);
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

            if (hero.RemainingHealth <= 0)
            {
                heroData.ExperiencePoints += 50;
            }
            else
            {
                heroData.ExperiencePoints += 100;
            }

            this.context.SaveChanges();

            return View(hero);
        }

        private List<HeroFightViewModel> AddHeroes()
        {
            var heroes = new List<HeroFightViewModel>();

            var attackingHero = this.context.Heroes.Where(h => h.Id == 1).FirstOrDefault();
            var defendingHero = this.context.Heroes.Where(h => h.Id == 2).FirstOrDefault();

            var attacker = new HeroFightViewModel
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

            var defender = new HeroFightViewModel
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

            heroes.Add(attacker);
            heroes.Add(defender);

            return heroes;
        }
    }
}
