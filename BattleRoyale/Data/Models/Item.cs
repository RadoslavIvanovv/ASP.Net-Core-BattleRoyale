

using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Data.Models.ItemTypes;
using System.ComponentModel.DataAnnotations;

using static BattleRoyale.Data.Constants.DatabaseConstants;

namespace BattleRoyale.Data.Models
{
    public class Item
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(DefaultMaxLengthForName)]
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
        public string PlayerId { get; set; }
        public bool IsUpForAuction { get; set; }
    }
}
