﻿using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Services.HeroServices;
using Microsoft.AspNetCore.Mvc;

namespace BattleRoyale.Controllers
{
    public class BattleArenaController : Controller
    {
        private readonly BattleRoyaleDbContext context;
        private readonly HeroServices heroServices;
        private HeroFightViewModel attacker;
        private HeroFightViewModel defender;

        public BattleArenaController(BattleRoyaleDbContext context)
        {
            this.context = context;
            this.heroServices = new HeroServices();
        }

        public IActionResult Fight() => View(attacker);

        [HttpPost]
        public IActionResult Fight(Hero attackingHero,Hero defendingHero)
        {
            int turnCount = 1;

            if (turnCount == 1)
            {
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
            }
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
    }
}
