

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static BattleRoyale.Data.Constants.DatabaseConstants;

namespace BattleRoyale.Models.Players
{
    public class AllPlayersQueryModel
    {
        public const int PlayersPerPage = PlayerPerPage;

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }
        public string Name { get; init; }
        public int Level { get; init; }

        public PlayerSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalPlayers { get; set; }

        public IEnumerable<PlayerListingViewModel> Players { get; set; }
    }
}
