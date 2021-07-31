

using BattleRoyale.Data.Models.HeroTypes;
using System.Collections.Generic;
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
        public string ImageUrl { get; set; }
        public int Attack { get; set; }
        public int MagicAttack { get; set; }
        public int Health { get; set; }
        public int Armor { get; set; }    
        public int MagicResistance { get; set; }
        public int Speed { get; set; }
        public bool IsMain { get;set; }
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public int RequiredExperiencePoints { get; set; }
        public Pet Pet { get; set; }
        [Required]
        public HeroType HeroType { get; init; }
        public bool HasWeapon { get; set; }
        public bool HasArmorItem { get; set; }
        public bool HasMagicResistItem { get; set; }
        public bool HasNecklace { get; set; }
        public bool HasBoots { get; set; }
        public bool HasPet { get; set; }
        public string PlayerId { get; init; }
        public ICollection<Item> Items { get; init; } = new HashSet<Item>();
    }
}
