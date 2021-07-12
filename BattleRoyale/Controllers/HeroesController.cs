

using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BattleRoyale.Controllers
{
    public class HeroesController:Controller
    {
        private readonly BattleRoyaleDbContext context; 
        private readonly HeroServices heroServices=new HeroServices();

        public HeroesController(BattleRoyaleDbContext context)
        {
            this.context = context;
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddHeroFormModel hero)
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

            heroServices.SetHeroStats(heroData);

            this.context.Heroes.Add(heroData);

            this.context.SaveChanges();

            return RedirectToAction("Index", "Home");
        } 
    }
}
