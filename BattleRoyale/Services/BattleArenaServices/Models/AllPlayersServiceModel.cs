

using BattleRoyale.Models.Players;
using System.Collections.Generic;

namespace BattleRoyale.Services.BattleArenaServices.Models
{
    public class AllPlayersServiceModel
    {
        public int CurrentPage { get; init; }

        public int PlayersPerPage { get; init; }

        public int TotalPlayers { get; init; }

        public IEnumerable<PlayerListingViewModel> Players { get; init; }
    }
}
