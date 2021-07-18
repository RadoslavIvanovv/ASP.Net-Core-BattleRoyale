﻿

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
        private readonly HeroServices heroServices;

        public HeroesController(BattleRoyaleDbContext context)
        {
            this.context = context;
            this.heroServices = new HeroServices();
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddHeroFormModel hero)
        {
            var player =BecomePlayer();

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

            heroServices.SetHeroStats(heroData);
            player.Heroes.Add(heroData);

            this.context.Heroes.Add(heroData);

            this.context.SaveChanges();

            return RedirectToAction("Index", "Home");
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

        private void HeroesCountCheck(Player player)
        {
            if (player.Heroes.Count == 0)
            {
                player =BecomePlayer();
            }
            else if(player.Heroes.Count==1 && player.Level == 20)
            {

            }
            else if(player.Heroes.Count==50 && player.Level == 50)
            {

            }
        }
    }
}
