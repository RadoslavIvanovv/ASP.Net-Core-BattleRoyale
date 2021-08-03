

using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Data.Models
{
    public class Pet
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Stats { get; set; }
        [Required]
        public string Type { get; set; }
        public int HeroId { get; set; }
    }
}
