
using BattleRoyale.Models.Pets;
using BattleRoyale.Services.PetServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static BattleRoyale.Data.Constants.PetControllerConstants;


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
        [Authorize]
        public IActionResult Add(AddPetFormModel pet)
        {
            var result = this.petService.Add(pet);

            if (!ModelState.IsValid)
            {
                return View(pet);
            }

            if (result == HeroAlreadyHasPet)
            {
                return BadRequest(result);
            }

            return RedirectToAction("All", "Heroes");
        }

        [Authorize]
        public IActionResult Remove(int heroId)
        {
            var hero =this.petService.Remove(heroId);

            return View(hero);
        }
    }
}
