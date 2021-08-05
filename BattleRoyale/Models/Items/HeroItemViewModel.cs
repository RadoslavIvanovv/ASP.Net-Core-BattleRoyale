

using System.ComponentModel.DataAnnotations;

using static BattleRoyale.Data.Constants.DatabaseConstants;

namespace BattleRoyale.Models.Items
{
    public class HeroItemViewModel
    {
        public int Id { get; set; }
        [MinLength(DefaultMinLengthForName)]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public int Stats { get; set; }
        [Required]
        public string ItemType { get; set; }
        [Required]
        public string HeroType { get; set; }
        [Required]
        public string HeroId { get; set; }
    }
}
