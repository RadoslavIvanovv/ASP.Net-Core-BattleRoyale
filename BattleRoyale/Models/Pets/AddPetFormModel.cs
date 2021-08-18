

using System.ComponentModel.DataAnnotations;

using static BattleRoyale.Data.Constants.DatabaseConstants;

namespace BattleRoyale.Models.Pets
{
    public class AddPetFormModel
    {
        [Required]
        [MaxLength(DefaultMaxLengthForName)]
        [MinLength(DefaultMinLengthForName)]
        public string Name { get; set; }
        [Required]
        public int Stats { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int HeroId { get; set; }
    }
}
