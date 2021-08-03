

using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Models.Players
{
    public class PlayerListingViewModel
    {
        public string Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
