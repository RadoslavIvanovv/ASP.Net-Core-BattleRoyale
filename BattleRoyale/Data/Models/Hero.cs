

using BattleRoyale.Data.Models.HeroTypes;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Data.Models
{
    public class Hero
    {
        public int Id { get; init; } 

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; init; }
        [Required]
        public int Attack { get; set; }
        [Required]
        public int Health { get; set; }
        [Required]
        public int Armor { get; set; }    
        [Required]
        public int MagicResistance { get; set; }
        [Required]
        public int Speed { get; set; }
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public HeroType HeroType { get; init; }
        public bool HasWeapon { get; set; }
        public bool HasArmorItem { get; set; }
        public bool HasMagicResistItem { get; set; }
        public bool HasNeckless { get; set; }
        public bool HasBoots { get; set; }
        public bool HasPet { get; set; }
    }
}
