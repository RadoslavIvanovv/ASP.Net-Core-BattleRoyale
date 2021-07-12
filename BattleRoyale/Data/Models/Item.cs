

using BattleRoyale.Data.Models.HeroTypes;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Data.Models
{
    public class Item
    {
        public int Id { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public int Stats { get; init; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string ImageUrl { get; init; }
        public int PassiveEffect { get; set; }
        [Required]
        public HeroType HeroType { get; set; }
    }
}
