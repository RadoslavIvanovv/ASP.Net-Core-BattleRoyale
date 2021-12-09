

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Data.Models
{
    public class AuctionItem
    {
        public int Id { get; init; }
        [Required]
        public Player ItemOwner { get; init; }
        [Required]
        public Item Item { get; set; }
        [Required]
        public DateTime ExpirationDate { get; init; }
        public ICollection<Bid> Bids { get; init; } = new HashSet<Bid>();
    }
}
