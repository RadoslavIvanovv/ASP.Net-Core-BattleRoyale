using BattleRoyale.Controllers;
using BattleRoyale.Models.AuctionItems;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace BattleRoyale.Tests.Routes
{
    public class AuctionTests
    {
        [Theory]
        [InlineData(1)]
        public void GetAddShouldBeRoutedCorrectly(int itemId)
           => MyRouting
               .Configuration()
               .ShouldMap($"/Auction/Add?itemId={itemId}")
               .To<AuctionController>(c => c.Add(itemId));
        [Fact]
        public void AllItemsShouldReturnQueryModel()
          => MyRouting
              .Configuration()
              .ShouldMap("/Auction/All")
              .To<AuctionController>(c => c.All(With.No<AllAuctionItemsQueryModel>()));

        [Theory]
        [InlineData(1)]
        public void InfoShouldBeRoutedCorrectly(int itemId)
           => MyRouting
               .Configuration()
               .ShouldMap($"/Auction/Info?itemId={itemId}")
               .To<AuctionController>(c => c.Info(itemId));

        [Theory]
        [InlineData(1)]
        public void BidShouldBeRoutedCorrectly(int itemId)
           => MyRouting
               .Configuration()
               .ShouldMap($"/Auction/Bid?itemId={itemId}")
               .To<AuctionController>(c => c.Bid(itemId));

        [Fact]
        public void BidSuccessShouldReturnQueryModel()
          => MyRouting
              .Configuration()
              .ShouldMap("/Auction/BidSuccess")
              .To<AuctionController>(c => c.BidSuccess());

        [Theory]
        [InlineData(1)]
        public void EndAuctionShouldBeRoutedCorrectly(int itemId)
           => MyRouting
               .Configuration()
               .ShouldMap($"/Auction/EndAuction?itemId={itemId}")
               .To<AuctionController>(c => c.EndAuction(itemId));
    }
}
