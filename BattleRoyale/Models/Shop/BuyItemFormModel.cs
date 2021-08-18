

using System.ComponentModel.DataAnnotations;

using static BattleRoyale.Data.Constants.DatabaseConstants;
using static BattleRoyale.Data.Constants.ItemConstants;

namespace BattleRoyale.Models.Shops
{
    public class BuyItemFormModel
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(DefaultMaxLengthForName)]
        [MinLength(DefaultMinLengthForName)]
        public string Name { get; init; }
        [Required]
        [Range(MinPriceForItem,MaxPriceForItem)]
        public int Price { get; set; }
    }
}
