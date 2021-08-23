
namespace BattleRoyale.Data.Models
{
    public class Bid
    {
        public int Id { get; init; }
        public int AuctionItemId { get; init; }
        public string BidderName { get; init; }
        public int BidAmount { get; init; }
    }
}
