

namespace BattleRoyale.Models.AuctionItems
{
    public class AuctionItemInfoModel
    { 
        public string Name { get; init; }

        public int Stats { get; init; }

        public int Price { get; set; }

        public string ItemType { get; set; }

        public int RequiredLevel { get; set; }

        public string AdditionalEffect { get; set; }

        public string HeroType { get; set; }
    }
}
