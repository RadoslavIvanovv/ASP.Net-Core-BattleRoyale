

using BattleRoyale.Data;
using BattleRoyale.Infrastructure;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Services.HeroServices;
using Microsoft.AspNetCore.Mvc;


namespace BattleRoyale.Controllers
{
    public class HeroesController:Controller
    {
        private readonly IHeroService heroService;

        public HeroesController( IHeroService heroService)
        {
            this.heroService = heroService;
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(HeroModel hero)
        {
            this.heroService.Add(hero, this.User.GetId());

            if (!ModelState.IsValid)
            {
                return View(hero);
            }
            
            return RedirectToAction("All", "Heroes");
        }

        public IActionResult Remove(int heroId)
        {

            this.heroService.Remove(heroId,this.User.GetId());

            return RedirectToAction("All", "Heroes");
        }

        public IActionResult All(int heroId)
        {
            var heroes = this.heroService.All(heroId, this.User.GetId());

            return View(heroes);
        }

        public IActionResult Details(int heroId)
        {
            var playerData = this.heroService.Details(heroId, this.User.GetId());

            return View(playerData);
        }

        public IActionResult Equip(int heroId,int itemId)
        {
            var hero = this.heroService.Equip(heroId, itemId, this.User.GetId());

            return View(hero);
        }

        public IActionResult Unequip(int heroId, int itemId)
        {
            var hero = this.heroService.Unequip(heroId, itemId, this.User.GetId());

            return View(hero);
        }
        
    }
}
