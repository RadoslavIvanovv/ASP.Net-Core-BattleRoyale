

using BattleRoyale.Data.Models;

namespace BattleRoyale.Services.PetServices
{
    public class PetService : IPetService
    {
        public void SetPetStats(Hero hero,Pet pet)
        {
            if (pet.Type == "Tiger")
            {
                hero.Attack += 100;
            }
            else if (pet.Type == "Frog")
            {
                hero.MagicAttack += 100;
            }
            else if (pet.Type == "Elephant")
            {
                hero.Health += 100;
            }
            else if (pet.Type == "Armadillo")
            {
                hero.Armor += 100;
            }
            else if (pet.Type == "Turtle")
            {
                hero.MagicResistance += 100;
            }
            else if (pet.Type == "Cheetah")
            {
                hero.Speed += 100;
            }

            hero.HasPet = true;
        }
    }
}
