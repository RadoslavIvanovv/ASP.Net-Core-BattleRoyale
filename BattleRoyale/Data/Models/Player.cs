

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Data.Models
{
    public class Player
    {
        public int Id { get; init; } 
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public int Gold { get; set; }
        [Required]
        public string UserId { get; init; }
        public ICollection<Item> Inventory { get; set; } = new HashSet<Item>();
        public ICollection<Hero> Heroes { get; set; } = new HashSet<Hero>();
    }
}
