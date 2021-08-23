

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BattleRoyale.Data.Models
{
    public class AuctionItem
    {
        public int Id { get; init; }
        public Player ItemOwner { get; init; }
        public Item Item { get; set; }
        public DateTime ExpirationDate { get; init; }
        public ICollection<Bid> Bids { get; init; } = new HashSet<Bid>();
        public int BidsCount { get => Bids.Count; set { } }
    }
}
