

using BattleRoyale.Data.Models.HeroTypes;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Data.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public int Effect { get; set; }
        [Required]
        public string ImageUrl { get; init; }
        public HeroType HeroType { get; set; }
    }
}
