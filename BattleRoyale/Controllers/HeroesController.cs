

using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Infrastructure;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Services.HeroServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BattleRoyale.Controllers
{
    public class HeroesController:Controller
    {
        private readonly BattleRoyaleDbContext context; 
        private readonly HeroService heroService;

        public HeroesController(BattleRoyaleDbContext context)
        {
            this.context = context;
            this.heroService = new HeroService();
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(HeroModel hero)
        {
            if (!ModelState.IsValid)
            {
                return View(hero);
            }

            var heroData = new Hero
            {
                Id = hero.Id,
                Name = hero.Name,
                ImageUrl = hero.ImageUrl,
                HeroType = Enum.Parse<HeroType>(hero.HeroType)
            };

            heroService.SetHeroStats(heroData);

            var player = this.context.Players.Where(p => p.UserId == this.User.GetId()).FirstOrDefault();

            if (player==null)
            {
                player = BecomePlayer();
            }

            player.Heroes.Add(heroData);

            this.context.Heroes.Add(heroData);

            this.context.SaveChanges();

            return RedirectToAction("All", "Heroes");
        }

        public IActionResult All()
        {
            var player = this.context.Players.Where(p => p.UserId == this.User.GetId()).FirstOrDefault();

            var heroes = this.context.Players
                .Where(p => p.UserId == this.User.GetId())
                .SelectMany(p => p.Heroes.Select(h => new HeroModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    ImageUrl = h.ImageUrl,
                    Level = h.Level,
                    Attack = h.Attack,
                    MagicAttack = h.MagicAttack,
                    Health = h.Health,
                    Armor = h.Armor,
                    MagicResistance = h.MagicResistance,
                    Speed = h.Speed,
                    HeroType = h.HeroType.ToString()
                })).ToList();

            return View(heroes);
        }

        private Player BecomePlayer()
        {
            var userId = this.User.GetId();

            var existingUser = this.context.Users.Where(u => u.Id == userId).FirstOrDefault();

            if (existingUser != null)
            {
                var player = new Player
                {
                    Name = this.User.Identity.Name,
                    Level = 1,
                    ExperiencePoints = 0,
                    Gold = 1000,
                    UserId=userId
                };

                this.context.Players.Add(player);
                this.context.SaveChanges();

                return player;
            }

            return null;
        }
    }
}
