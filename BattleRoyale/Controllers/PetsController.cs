
using BattleRoyale.Models.Pets;
using BattleRoyale.Services.PetServices;
using Microsoft.AspNetCore.Mvc;


namespace BattleRoyale.Controllers
{
    public class PetsController : Controller
    {
        private readonly IPetService petService;

        public PetsController(IPetService petService)
        {
            this.petService = petService;
        }

        public IActionResult Add() => View();

        [HttpPost]

        public IActionResult Add(AddPetFormModel pet)
        {
            if (!ModelState.IsValid)
            {
                return View(pet);
            }

            this.petService.Add(pet);

            return RedirectToAction("All", "Heroes");
        }

        public IActionResult Remove(int heroId)
        {
            var hero =this.petService.Remove(heroId);

            return View(hero);
        }
    }
}
