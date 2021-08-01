

using BattleRoyale.Data.Models;

namespace BattleRoyale.Services.PetServices
{
    public class PetService : IPetService
    {

        public void SetPetStats(Hero hero,Pet pet)
        {
            pet.Stats = 100;
            if (pet.Type == "Tiger")
            {
                hero.Attack += pet.Stats;
            }
            else if (pet.Type == "Frog")
            {
                hero.MagicAttack += pet.Stats;
            }
            else if (pet.Type == "Elephant")
            {
                hero.Health += pet.Stats;
            }
            else if (pet.Type == "Armadillo")
            {
                hero.Armor += pet.Stats;
            }
            else if (pet.Type == "Turtle")
            {
                hero.MagicResistance += pet.Stats;
            }
            else if (pet.Type == "Cheetah")
            {
                hero.Speed += pet.Stats;
            }

            SetImageForPet(pet);

            hero.OverallPower += pet.Stats;
            hero.HasPet = true;
        }

        public void RemovePetFromHero(Hero hero, Pet pet)
        {
            if (pet.Type == "Tiger")
            {
                hero.Attack -= pet.Stats;
            }
            else if (pet.Type == "Frog")
            {
                hero.MagicAttack -= pet.Stats;
            }
            else if (pet.Type == "Elephant")
            {
                hero.Health -= pet.Stats;
            }
            else if (pet.Type == "Armadillo")
            {
                hero.Armor -= pet.Stats;
            }
            else if (pet.Type == "Turtle")
            {
                hero.MagicResistance -= pet.Stats;
            }
            else if (pet.Type == "Cheetah")
            {
                hero.Speed -= pet.Stats;
            }
            hero.HasPet = false;

        }

        private void SetImageForPet(Pet pet)
        {
            if (pet.Type == "Tiger")
            {
                pet.ImageUrl = "https://static01.nyt.com/images/2019/05/07/science/06SCI-TIGER1/03SCI-TIGER1-superJumbo.jpg";
            }
            else if (pet.Type == "Frog")
            {
                pet.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ949-_Y3Cz4VGphE78mAUN7uLk9-eW1YxOaQ&usqp=CAU";
            }
            else if (pet.Type == "Elephant")
            {
                pet.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTlQMc1DLUbPJ__PX97TJjsKryR7hgSfFxy5g&usqp=CAU";
            }
            else if (pet.Type == "Armadillo")
            {
                pet.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTXRuE920L4iiXwVU848w5-JxAHEuQqWGuJrg&usqp=CAU";
            }
            else if (pet.Type == "Turtle")
            {
                pet.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT1b4VG2NvOc0KsHJgp7b2mePuCSC9q7LWS6A&usqp=CAU";
            }
            else if (pet.Type == "Cheetah")
            {
                pet.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSZdpSgJSam4wvFgwZ44fa33J4xCSs9wzZWNQ&usqp=CAU";
            }
        }
    }
}
