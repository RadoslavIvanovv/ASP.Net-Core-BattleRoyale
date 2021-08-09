using AutoMapper;
using BattleRoyale.Data.Models;
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

            this.CreateMap<Hero, HeroIndexViewModel>();
            this.CreateMap<Player, HeroModel>();

            this.CreateMap<Item, ShopItemModel>();
        }
    }
}
