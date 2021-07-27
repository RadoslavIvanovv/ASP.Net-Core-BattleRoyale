

using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Infrastructure;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Models.Players;
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

            var playerHeroes = this.context.Players.Where(p => p.UserId == this.User.GetId())
           .Select(p => p.Heroes).FirstOrDefault();

            if (playerHeroes.Count == 0)
            {
                heroData.IsMain = true;
            }

            player.Heroes.Add(heroData);

            this.context.Heroes.Add(heroData);

            this.context.SaveChanges();

            return RedirectToAction("All", "Heroes");
        }

        public IActionResult All(int heroId)
        {
            if(heroId!=0)
            {
                var currentMainHero = this.context.Players
                .Where(p => p.UserId == this.User.GetId())
                .Select(p => p.Heroes.Where(h => h.IsMain == true).FirstOrDefault()).FirstOrDefault();

                currentMainHero.IsMain = false;

                var newMainHero = this.context.Players
                .Where(p => p.UserId == this.User.GetId())
                .Select(p => p.Heroes.Where(h => h.Id == heroId).FirstOrDefault()).FirstOrDefault();

                newMainHero.IsMain = true;

                this.context.SaveChanges();
            }

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

        public IActionResult Details(int heroId, int itemId)
        {
            var player = this.context.Players.Where(p => p.UserId == this.User.GetId()).FirstOrDefault();

            var inventory = this.context.Players
               .Where(p => p.UserId == this.User.GetId())
               .Select(pi => new PlayerInventoryViewModel
               {
                   Id = pi.Id,
                   BoughtItems = pi.Inventory
               }).FirstOrDefault();

            var hero = this.context.Heroes
                .Where(h => h.Id == heroId).FirstOrDefault();

            if (hero == null)
            {
                return NotFound();
            }

            var heroDetails = new Hero
            {
                Id = hero.Id,
                Name = hero.Name,
                ImageUrl=hero.ImageUrl,
                Level=hero.Level,
                ExperiencePoints=hero.ExperiencePoints,
                RequiredExperiencePoints=hero.RequiredExperiencePoints,
                Attack=hero.Attack,
                MagicAttack=hero.MagicAttack,
                Health=hero.Health,
                Armor=hero.Armor,
                MagicResistance=hero.MagicResistance,
                Speed=hero.Speed,
                HeroType=hero.HeroType,
                Items=hero.Items
            };


            if (itemId != 0)
            {
                var item = player.Inventory.Where(i => i.Id == itemId).FirstOrDefault();

                if (!item.IsEquipped)
                {
                    heroService.EquipItem(hero, item);
                    hero.Items.Add(item);
                    this.context.SaveChanges();
                }
                else
                {
                    heroService.UnequipItem(hero, item);
                    hero.Items.Remove(item);
                    this.context.SaveChanges();
                }
                
            }


            var playerData = new PlayerHeroViewModel
            {
                Id = player.Id,
                Hero = heroDetails,
                Items = inventory.BoughtItems
            };

            return View(playerData);
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
                    RequiredExperiencePoints=1000,
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
