

using BattleRoyale.Infrastructure;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Services.HeroServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static BattleRoyale.Data.Constants.HeroControllerConstants;


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
        [Authorize]
        public IActionResult Add(HeroModel hero)
        {
            var result =this.heroService.Add(hero, this.User.GetId());

            if (!ModelState.IsValid)
            {
                return View(hero);
            }
            if (result == RequirementsNotMet)
            {
                return BadRequest(result);
            }
            
            return RedirectToAction("All", "Heroes");
        }


        [Authorize]
        public IActionResult Remove(int heroId)
        {
            var result = this.heroService.Remove(heroId,this.User.GetId());

            if (result == HeroCountCannotBeLessThanOne)
            {
                return BadRequest(result);
            }

            return RedirectToAction("All", "Heroes");
        }

        [Authorize]
        public IActionResult All(int heroId)
        {
            var heroes = this.heroService.All(heroId, this.User.GetId());

            return View(heroes);
        }

        [Authorize]
        public IActionResult Details(int heroId)
        {
            var playerData = this.heroService.Details(heroId, this.User.GetId());

            return View(playerData);
        }

        [Authorize]
        public IActionResult Equip(int heroId,int itemId)
        {
            var hero = this.heroService.Equip(heroId, itemId, this.User.GetId());

            if (hero == null)
            {
                return BadRequest(HeroHasItem);
            }

            return View(hero);
        }

        [Authorize]
        public IActionResult Unequip(int heroId, int itemId)
        {
            var hero = this.heroService.Unequip(heroId, itemId, this.User.GetId());

            return View(hero);
        }
        
    }
}
