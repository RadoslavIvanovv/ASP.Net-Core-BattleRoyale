using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Models.Pets;
using BattleRoyale.Services.PetServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BattleRoyale.Controllers
{
    public class PetsController : Controller
    {

        private readonly BattleRoyaleDbContext context;
        private readonly PetService petService;

        public PetsController(BattleRoyaleDbContext context)
        {
            this.context = context;
            this.petService = new PetService();
        }

        public IActionResult Add() => View();

        [HttpPost]

        public IActionResult Add(AddPetFormModel pet)
        {
            var hero = this.context.Heroes
                .Where(h => h.Id == pet.HeroId).FirstOrDefault();

            var petData = new Pet
            {
                Name = pet.Name,
                Stats = pet.Stats,
                Type = pet.Type,
                HeroId = pet.HeroId
            };

            hero.Pet = petData;

            petService.SetPetStats(hero, petData);

            this.context.Pets.Add(petData);

            this.context.SaveChanges();

            return RedirectToAction("All", "Heroes");
        }

        public IActionResult Remove(int heroId)
        {
            var hero = this.context.Heroes.Where(h => h.Id == heroId).FirstOrDefault();

            var pet = this.context.Pets.Where(p => p.HeroId == heroId).FirstOrDefault();

            petService.RemovePetFromHero(hero, pet);

            this.context.Pets.Remove(pet);

            this.context.SaveChanges();

            return View(hero);
        }
    }
}
