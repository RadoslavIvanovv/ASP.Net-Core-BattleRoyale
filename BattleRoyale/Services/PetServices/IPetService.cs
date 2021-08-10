

using BattleRoyale.Data.Models;
using BattleRoyale.Models.Pets;

namespace BattleRoyale.Services.PetServices
{
    public interface IPetService
    {
        string Add(AddPetFormModel pet);
        Hero Remove(int heroId);
    }
}
