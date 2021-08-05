

using System.ComponentModel.DataAnnotations;

using static BattleRoyale.Data.Constants.DatabaseConstants;

namespace BattleRoyale.Data.Models
{
    public class Pet
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(DefaultMaxLengthForName)]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Stats { get; set; }
        [Required]
        public string Type { get; set; }
        public int HeroId { get; set; }
    }
}
