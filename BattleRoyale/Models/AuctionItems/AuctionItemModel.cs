

using BattleRoyale.Data.Models;
using System;

namespace BattleRoyale.Models.AuctionItems
{
    public class AuctionItemModel
    {
        public int Id { get; init; }
        public Player ItemOwner { get; init; }
        public Item Item { get; init; }
        public DateTime ExpirationDate { get; init; }
    }
}
