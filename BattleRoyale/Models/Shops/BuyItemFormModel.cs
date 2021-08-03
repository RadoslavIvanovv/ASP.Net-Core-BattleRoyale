

using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Models.Shops
{
    public class BuyItemFormModel
    {
        public int Id { get; init; }
        [Required]
        [MinLength(5)]
        public string Name { get; init; }
        [Required]
        [Range(100,2000)]
        public int Price { get; set; }
    }
}
