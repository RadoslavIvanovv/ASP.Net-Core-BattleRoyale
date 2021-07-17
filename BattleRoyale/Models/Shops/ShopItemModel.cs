

using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Data.Models.ItemTypes;

namespace BattleRoyale.Models.Shop
{
    public class ShopItemModel 
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Stats { get; init; }
        public int Price { get; set; }
        public ItemType ItemType { get; set; }
        public string ImageUrl { get; init; }
        public int PassiveEffect { get; set; }
        public HeroType HeroType { get; set; }
    }
}
