

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Data.Models
{
    public class User
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public int Gold { get; set; }
        public string InventoryId { get; set; }
        public ICollection<Item> Inventory { get; set; } = new HashSet<Item>();
        public ICollection<Hero> Heroes { get; set; } = new HashSet<Hero>();
    }
}
