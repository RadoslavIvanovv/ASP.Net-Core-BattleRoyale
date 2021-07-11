

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
        public int Attack { get; set; }
        public int SpellAttack { get; set; }
        public int Health { get; set; }
        public int Armor { get; set; }    
        public int MagicResistance { get; set; }
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
