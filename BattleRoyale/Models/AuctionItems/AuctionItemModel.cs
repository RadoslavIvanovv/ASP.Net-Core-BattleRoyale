

using BattleRoyale.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Models.AuctionItems
{
    public class AuctionItemModel
    {
        public int Id { get; init; }
        public Player ItemOwner { get; init; }
        public Item Item { get; init; }
        [Required]
        public DateTime ExpirationDate { get; init; }
    }
}
