
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Data.Models
{
    public class Player:IdentityUser
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public int RequiredExperiencePoints { get; set; }
        public int Gold { get; set; }
        [Required]
        public string UserId { get; init; }
        public ICollection<Item> Inventory { get; init; } = new HashSet<Item>();
        public ICollection<Hero> Heroes { get; init; } = new HashSet<Hero>();
    }
}
