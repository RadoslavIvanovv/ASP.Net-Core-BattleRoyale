

using System.ComponentModel.DataAnnotations;

using static BattleRoyale.Data.Constants.DatabaseConstants;

namespace BattleRoyale.Models.Players
{
    public class PlayerListingViewModel
    {
        public string Id { get; set; }
        [Required]
        [MinLength(DefaultMinLengthForName)]
        public string Name { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
