

using System.ComponentModel.DataAnnotations;

using static BattleRoyale.Data.Constants.DatabaseConstants;


namespace BattleRoyale.Models.Heroes
{
    public class HeroFightViewModel
    {
        public int Id { get; set; }    
        [MinLength(DefaultMinLengthForName)]
        public string Name { get; set; }  
        [Required]
        public int Attack { get; set; }
        [Required]
        public int MagicAttack { get; set; }
        [Required]
        public int MaxHealth { get; set; }
        [Required]
        public int RemainingHealth { get; set; }
        [Required]
        public int MaxArmor { get; set; }
        [Required]
        public int RemainingArmor { get; set; }
        [Required]
        public int MaxMagicResistance { get; set; }
        [Required]
        public int RemainingMagicResistance { get; set; }
        [Required]
        public int Speed { get; set; }
        [Required]
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public int OverallPower { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
