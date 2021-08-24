
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Data.Models
{
    public class Bid
    {
        public int Id { get; init; }
        [Required]
        public int AuctionItemId { get; init; }
        [Required]
        public string BidderName { get; init; }
        [Required]
        public int BidAmount { get; init; }
    }
}
