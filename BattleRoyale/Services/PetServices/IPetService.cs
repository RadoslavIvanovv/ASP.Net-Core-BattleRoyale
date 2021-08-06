

using BattleRoyale.Data.Models;
using BattleRoyale.Models.Pets;

namespace BattleRoyale.Services.PetServices
{
    public interface IPetService
    {
        void Add(AddPetFormModel pet);
        Hero Remove(int heroId);
        void SetPetStats(Hero hero, Pet pet);
        void RemovePetFromHero(Hero hero, Pet pet);
    }
}
