

using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Models.Heroes
{
    public class HeroIndexViewModel
    {
        public int Id { get; set; }
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        public string Owner { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public int OverallPower { get; set; }
    }
}
