

using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Infrastructure;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Models.Players;
using BattleRoyale.Services.HeroServices;
using BattleRoyale.Services.ItemServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BattleRoyale.Controllers
{
    public class HeroesController:Controller
    {
        private readonly BattleRoyaleDbContext context; 
        private readonly HeroService heroService;
        private readonly ItemService itemService;

        public HeroesController(BattleRoyaleDbContext context)
        {
            this.context = context;
            this.heroService = new HeroService();
            this.itemService = new ItemService();
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(HeroModel hero)
        {


            var player = this.context.Players.Where(p => p.UserId == this.User.GetId()).FirstOrDefault();


            var heroData = new Hero
            {
                Id = hero.Id,
                Name = hero.Name,
                ImageUrl = hero.ImageUrl,
                Player = player,
                HeroType = Enum.Parse<HeroType>(hero.HeroType),
            };

            heroService.SetHeroStats(heroData);

            if (player == null)
            {
                player = BecomePlayer();
            }
            var playerHeroes = this.context.Players.Where(p => p.UserId == this.User.GetId())
                  .Select(p => p.Heroes).FirstOrDefault();
            var existingHero = playerHeroes.Where(h => h.Name == hero.Name).FirstOrDefault();

            if (existingHero != null)
            {
                this.ModelState.AddModelError(nameof(hero.Name), $"Hero with name '{hero.Name}' already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(hero);
            }

            if (playerHeroes.Count == 0)
            {
                heroData.IsMain = true;
            }

            var playerLevelRequirement = player.Level % 10 == 0;
            var playerHeroesRequirement = player.Heroes.Count < (player.Level / 10 + 1);
            if (!playerLevelRequirement && !playerHeroesRequirement)
            {
                return BadRequest();
            }

            player.Heroes.Add(heroData);

            this.context.Heroes.Add(heroData);

            this.context.SaveChanges();
            
            return RedirectToAction("All", "Heroes");
        }

        public IActionResult Remove(int heroId)
        {
            var heroes = this.context.Players
              .Where(p => p.UserId == this.User.GetId())
              .Select(h => h.Heroes).FirstOrDefault().ToList();

            var hero = heroes.Where(h => h.Id == heroId).FirstOrDefault();

            if (heroes.Count == 1)
            {
                return BadRequest();
            }

            this.context.Heroes.Remove(hero);
            heroes.Remove(hero);

            if (!heroes.Any(h => h.IsMain))
            {
                heroes[0].IsMain = true;
            }

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
                    OverallPower = h.OverallPower,
                    HeroType = h.HeroType.ToString()
                })).ToList();

            return View(heroes);
        }

        public IActionResult Details(int heroId)
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

            var pet = this.context.Pets.Where(p => p.HeroId == hero.Id).FirstOrDefault();

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
                Pet=pet,
                OverallPower=hero.OverallPower,
                HeroType=hero.HeroType,
                Items=hero.Items
            };

            if (pet != null)
            {
                heroDetails.HasPet = true;
            }

            var playerData = new PlayerHeroViewModel
            {
                Id = player.Id,
                Hero = heroDetails,
                Items = inventory.BoughtItems
            };

            return View(playerData);
        }

        public IActionResult Equip(int heroId,int itemId)
        {
            var inventory = this.context.Players
               .Where(p => p.UserId == this.User.GetId())
               .Select(pi => new PlayerInventoryViewModel
               {
                   Id = pi.Id,
                   BoughtItems = pi.Inventory
               }).FirstOrDefault();

            var hero = this.context.Heroes
                .Where(h => h.Id == heroId).FirstOrDefault();

            var item = inventory.BoughtItems.Where(i => i.Id == itemId).FirstOrDefault();

            if (itemService.HeroHasItem(hero, item))
            {
                return BadRequest();
            }

            heroService.EquipItem(hero, item);
                hero.Items.Add(item);
                this.context.SaveChanges();

            return View(hero);
        }

        public IActionResult Unequip(int heroId, int itemId)
        {
            var inventory = this.context.Players
               .Where(p => p.UserId == this.User.GetId())
               .Select(pi => new PlayerInventoryViewModel
               {
                   Id = pi.Id,
                   BoughtItems = pi.Inventory
               }).FirstOrDefault();

            var hero = this.context.Heroes
                .Where(h => h.Id == heroId).FirstOrDefault();

            var item = inventory.BoughtItems.Where(i => i.Id == itemId).FirstOrDefault();

            heroService.UnequipItem(hero, item);
            hero.Items.Remove(item);
            this.context.SaveChanges();

            return View(hero);
        }

        private Player BecomePlayer()
        {
            var userId = this.User.GetId();

            var existingUser = this.context.Users.Where(u => u.Id == userId).FirstOrDefault();

            if (existingUser != null)
            {
                var player = new Player
                {
                    Name = existingUser.FullName,
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
