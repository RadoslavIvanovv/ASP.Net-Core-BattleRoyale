

using BattleRoyale.Controllers;
using BattleRoyale.Data.Models;
using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Data.Models.ItemTypes;
using BattleRoyale.Models.Items;
using BattleRoyale.Models.Shop;
using MyTested.AspNetCore.Mvc;
using System;
using Xunit;

namespace BattleRoyale.Tests.Routes
{
    public class ShopRouteTests
    {
        [Fact]
        public void GetAddShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Shop/Add")
               .To<ShopController>(c => c.Add());


        //[Theory]
        //[InlineData("TestName", 123,1,ItemType.MagicResistance,StatsType.Health,HeroType.Mage)]
        //public void PostCreateShouldBeRoutedCorrectly(string name, int price,int requiredLevel,ItemType itemType,StatsType additionalEffect,HeroType heroType)
        //   => MyRouting
        //       .Configuration()
        //       .ShouldMap(request => request
        //           .WithMethod(HttpMethod.Post)
        //           .WithLocation("/Shop/Add")
        //           .WithFormFields(new
        //           {
        //               Name = name,
        //               Price = price,
        //               RequiredLevel=requiredLevel,
        //               ItemType=itemType,
        //               AdditionalEffect=additionalEffect,
        //               HeroType=heroType
        //           }))
        //       .To<ShopController>(c => c.Add(new ShopItemModel
        //       {
        //           Name = name,
        //           Price = price,
        //           RequiredLevel = requiredLevel,
        //           ItemType =itemType,
        //           AdditionalEffect = additionalEffect,
        //           HeroType = heroType

        //       }))
        //       .AndAlso()
        //       .ToValidModelState();


        [Fact]
        public void AllShouldReturnQueryModel()
            => MyRouting
                .Configuration()
                .ShouldMap("/Shop/All")
                .To<ShopController>(c => c.All(With.No<AllItemsQueryModel>()));


        [Theory]
        [InlineData(1)]
        public void BuyItemShoudRouteCorrectly(int itemId)
            => MyRouting
            .Configuration()
            .ShouldMap($"/Shop/BuyItem?itemId={itemId}")
            .To<ShopController>(c => c.BuyItem(itemId));
    }
}
