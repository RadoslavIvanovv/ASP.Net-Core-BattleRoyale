

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Models.Players
{
    public class AllPlayersQueryModel
    {
        public const int PlayersPerPage = 20;

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
