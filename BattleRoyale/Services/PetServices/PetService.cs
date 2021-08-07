

using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Models.Pets;
using System.Linq;
using static BattleRoyale.Data.Constants.PetConstants;

namespace BattleRoyale.Services.PetServices
{
    public class PetService : IPetService
    {
        private readonly BattleRoyaleDbContext context;

        public PetService(BattleRoyaleDbContext context)
        {
            this.context = context;
        }

        public void Add(AddPetFormModel pet)
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

            SetPetStats(hero, petData);

            this.context.Pets.Add(petData);

            this.context.SaveChanges();
        }

        public Hero Remove(int heroId)
        {
            var hero = this.context.Heroes.Where(h => h.Id == heroId).FirstOrDefault();

            var pet = this.context.Pets.Where(p => p.HeroId == heroId).FirstOrDefault();

            RemovePetFromHero(hero, pet);

            this.context.Pets.Remove(pet);

            this.context.SaveChanges();

            return hero;
        }
    

    private void SetPetStats(Hero hero, Pet pet)
        {
            pet.Stats = PetStats;
            if (pet.Type == Tiger)
            {
                hero.Attack += pet.Stats;
            }
            else if (pet.Type == Frog)
            {
                hero.MagicAttack += pet.Stats;
            }
            else if (pet.Type == Elephant)
            {
                hero.Health += pet.Stats;
            }
            else if (pet.Type == Armadillo)
            {
                hero.Armor += pet.Stats;
            }
            else if (pet.Type == Turtle)
            {
                hero.MagicResistance += pet.Stats;
            }
            else if (pet.Type == Cheetah)
            {
                hero.Speed += pet.Stats;
            }

            SetImageForPet(pet);

            hero.OverallPower += pet.Stats;
            hero.HasPet = true;
        }

        private void RemovePetFromHero(Hero hero, Pet pet)
        {
            if (pet.Type == Tiger)
            {
                hero.Attack -= pet.Stats;
            }
            else if (pet.Type == Frog)
            {
                hero.MagicAttack -= pet.Stats;
            }
            else if (pet.Type == Elephant)
            {
                hero.Health -= pet.Stats;
            }
            else if (pet.Type == Armadillo)
            {
                hero.Armor -= pet.Stats;
            }
            else if (pet.Type == Turtle)
            {
                hero.MagicResistance -= pet.Stats;
            }
            else if (pet.Type == Cheetah)
            {
                hero.Speed -= pet.Stats;
            }
            hero.HasPet = false;

        }

        private void SetImageForPet(Pet pet)
        {
            if (pet.Type == Tiger)
            {
                pet.ImageUrl = TigerImage;
            }
            else if (pet.Type == Frog)
            {
                pet.ImageUrl = FrogImage;
            }
            else if (pet.Type == Elephant)
            {
                pet.ImageUrl = ElephantImage;
            }
            else if (pet.Type == Armadillo)
            {
                pet.ImageUrl = ArmadilloImage;
            }
            else if (pet.Type == Turtle)
            {
                pet.ImageUrl = TurtleImage;
            }
            else if (pet.Type == Cheetah)
            {
                pet.ImageUrl = CheetahImage;
            }
        }
    }
}
