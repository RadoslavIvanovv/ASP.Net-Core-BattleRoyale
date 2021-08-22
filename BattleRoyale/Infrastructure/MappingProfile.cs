using AutoMapper;
using BattleRoyale.Data.Models;
using BattleRoyale.Models.AuctionItems;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Models.Players;
using BattleRoyale.Models.Shop;

namespace BattleRoyale.Infrastructure
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Player, PlayerListingViewModel>();
            this.CreateMap<Player, PlayerInventoryViewModel>()
                .ForMember(p => p.BoughtItems, cfg => cfg.MapFrom(p => p.Inventory));

            this.CreateMap<Hero, HeroIndexViewModel>()
                .ForMember(h=>h.Owner,cfg=>cfg.MapFrom(h=>h.Player.Name));
            this.CreateMap<Player, HeroModel>();

            this.CreateMap<Item, ShopItemModel>();
            this.CreateMap<Item, AuctionItemInfoModel>()
                .ForMember(i => i.ItemType, cfg => cfg.MapFrom(i => i.ItemType.ToString()))
                .ForMember(i => i.AdditionalEffect, cfg => cfg.MapFrom(i => i.AdditionalEffect.ToString()))
                .ForMember(i => i.HeroType, cfg => cfg.MapFrom(i => i.HeroType.ToString()));

            this.CreateMap<AuctionItem, AuctionItemModel>();

        }
    }
}
