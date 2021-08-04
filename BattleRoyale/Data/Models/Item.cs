

using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Data.Models.ItemTypes;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Data.Models
{
    public class Item
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(20)]
        public string Name { get; init; }
        [Required]
        public int Stats { get; set; }
        [Required]
        public int Price { get; set; }
        public bool IsEquipped { get; set; }
        [Required]
        public ItemType ItemType { get; init; }
        [Required]
        public int RequiredLevel { get; set; }
        [Required]
        public StatsType AdditionalEffect { get; set; }
        [Required]
        public HeroType HeroType { get; set; }
        public int? HeroId { get; set; }
        public Hero Hero { get; set; }
    }
}
