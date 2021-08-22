

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BattleRoyale.Data.Models
{
    public class AuctionItem
    {
        public int Id { get; init; }
        public string ItemOwner { get; init; }
        public Item Item { get; set; }
        public DateTime ExpirationDate { get; init; }
        [NotMapped]
        public Dictionary<string, int> Bids { get; init; } = new Dictionary<string, int>();
        public int BidsCount { get => Bids.Count; set { } }
    }
}
