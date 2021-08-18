

using System.ComponentModel.DataAnnotations;

using static BattleRoyale.Data.Constants.DatabaseConstants;

namespace BattleRoyale.Models.Heroes
{
    public class HeroIndexViewModel
    {
        public int Id { get; set; }
        [MinLength(DefaultMinLengthForName)]
        public string Name { get; set; }
        [Required]
        public string Owner { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public int TotalExperiencePoints { get; set; }
        [Required]
        public int OverallPower { get; set; }
    }
}
