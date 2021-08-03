

using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Data.Models.ItemTypes;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Models.Shop
{
    public class ShopItemModel 
    {
        public int Id { get; init; }
        [Required]
        [MinLength(5)]
        public string Name { get; init; }
        [Required]
        [Range(50,200)]
        public int Stats { get; init; }
        [Required]
        [Range(100,2000)]
        public int Price { get; set; }
        [Required]
        public ItemType ItemType { get; set; }
        public string ImageUrl { get; init; }
        [Required]
        [Range(1,100)]
        public int RequiredLevel { get; set; }
        [Required]
        public HeroType HeroType { get; set; }
    }
}
