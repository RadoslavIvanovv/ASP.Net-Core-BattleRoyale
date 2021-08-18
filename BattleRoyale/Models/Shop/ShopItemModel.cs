

using BattleRoyale.Data.Models;
using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Data.Models.ItemTypes;
using System.ComponentModel.DataAnnotations;

using static BattleRoyale.Data.Constants.DatabaseConstants;
using static BattleRoyale.Data.Constants.ItemConstants;

namespace BattleRoyale.Models.Shop
{
    public class ShopItemModel 
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(DefaultMaxLengthForName)]
        [MinLength(DefaultMinLengthForName)]
        public string Name { get; init; }
        [Required]
        public int Stats { get; init; }
        [Required]
        [Range(MinPriceForItem,MaxPriceForItem)]
        public int Price { get; set; }
        [Required]
        public ItemType ItemType { get; set; }
        [Required]
        [Range(MinRequiredLevelForItem,MaxRequiredLevelForItem)]
        public int RequiredLevel { get; set; }
        [Required]
        public StatsType AdditionalEffect { get; set; }
        [Required]
        public HeroType HeroType { get; set; }
    }
}
