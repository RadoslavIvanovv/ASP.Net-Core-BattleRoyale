

using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Models.Items
{
    public class HeroItemViewModel
    {
        public int Id { get; set; }
        [MinLength(5)]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public int Stats { get; set; }
        [Required]
        public string ItemType { get; set; }
        [Required]
        public string HeroType { get; set; }
        [Required]
        public string HeroId { get; set; }
    }
}
