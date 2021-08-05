

using BattleRoyale.Data.Models;

using static BattleRoyale.Data.Constants.PetConstants;

namespace BattleRoyale.Services.PetServices
{
    public class PetService : IPetService
    {

        public void SetPetStats(Hero hero,Pet pet)
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

        public void RemovePetFromHero(Hero hero, Pet pet)
        {
            if (pet.Type == Tiger)
            {
                hero.Attack -= pet.Stats;
            }
            else if (pet.Type == Frog)
            {
                hero.MagicAttack -= pet.Stats;
            }
            else if (pet.Type ==Elephant)
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
