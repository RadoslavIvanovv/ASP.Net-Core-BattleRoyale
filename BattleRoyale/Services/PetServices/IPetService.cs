﻿

using BattleRoyale.Data.Models;

namespace BattleRoyale.Services.PetServices
{
    public interface IPetService
    {
        void SetPetStats(Hero hero, Pet pet);
    }
}