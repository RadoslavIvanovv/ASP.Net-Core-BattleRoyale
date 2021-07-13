

namespace BattleRoyale.Models.Shops
{
    public class ItemListingViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Stats { get; init; }
        public int Price { get; set; }
        public string ItemType { get; set; }
        public string ImageUrl { get; init; }
        public int PassiveEffect { get; set; }
        public string HeroType { get; set; }
    }
}
