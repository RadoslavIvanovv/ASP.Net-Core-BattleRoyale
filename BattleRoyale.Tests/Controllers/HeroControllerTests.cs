

using BattleRoyale.Controllers;
using BattleRoyale.Data.Models;
using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Tests.Controllers.Models;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BattleRoyale.Tests.Controllers
{
    public class HeroControllerTests
    {
        private HeroModelTest hero=new HeroModelTest();

        [Fact]
        public void HeroShouldBeAdded()
            => MyController<HeroesController>
            .Calling(c => c.Add())
            .ShouldReturn()
            .View();

        //[Fact]
        //public void CreatePostShouldReturnViewWithSameModelWhenInvalidModelState()
        //    => MyController<HeroesController>
        //    .Instance(inst=>inst.WithUser())
        //        .Calling(c => c.Add(new HeroModel { Id=hero.Id,Name=hero.Name,Player=hero.Player,HeroType=hero.HeroType }))
        //        .ShouldHave()
        //        .InvalidModelState()
        //        .AndAlso()
        //        .ShouldReturn()
        //        .View(With.Default<Hero>());

        //[Fact]
        //public void AddHeroShouldWorkProperlyWithCorrectData()
        //=>MyController<HeroesController>
        //    .Instance(inst=>inst.WithUser(u=>u.WithIdentifier(hero.Player.Id)))
        //    .Calling(c=>c.Add(new HeroModel
        //    {
        //        Id=hero.Id,
        //        Name=hero.Name,
        //        Player = hero.Player,
        //        HeroType=hero.HeroType,
        //    }))
        //    .ShouldHave()
        //        .ActionAttributes(attributes => attributes
        //            .RestrictingForHttpMethod(HttpMethod.Post)
        //            .RestrictingForAuthorizedRequests())
        //        .ValidModelState()
        //        .Data(data => data
        //            .WithSet<Hero>(dealers => dealers
        //                .Any(d =>
        //                    d.Name == hero.Name &&
        //                    d.HeroType.ToString()==hero.HeroType &&
        //                    d.PlayerId ==hero.Player.Id)))
        //    .AndAlso()
        //        .ShouldReturn()
        //        .Redirect(redirect => redirect
        //            .To<HeroesController>(c => c.All(hero.Id)));

        //[Fact]
        //public void RemoveShouldWorkProperlyWithCorrectData()
        //    =>MyController<HeroesController>
        //    .Instance(inst=>inst.WithUser(hero.Player.Id))
        //    .Calling(c=>c.Remove(hero.Id))
        //    .ShouldHave()
        //    .Data(data => data
        //        .WithSet<Hero>(dealers => dealers
        //            .Any(d =>
        //                d.Name == hero.Name &&
        //                d.HeroType.ToString() == hero.HeroType &&
        //                d.PlayerId == hero.Player.Id)))
        //    .AndAlso()
        //    .ShouldReturn()
        //    .Redirect(redirect => redirect
        //            .To<HeroesController>(c => c.All(hero.Id)));

        //[Fact]
        //public void AllShouldReturnQueryView()
        //    => MyController<HeroesController>
        //    .Instance(inst => inst.WithUser(hero.Player.Id))
        //    .Calling(c => c.All(hero.Id))
        //    .ShouldReturn()
        //    .View(view=>view.WithModelOfType<HeroModel>());


        //[Fact]
        //public void DetailsShouldReturnHeroDetails()
        //  => MyController<HeroesController>
        //  .Instance(inst => inst.WithUser(hero.Player.Id))
        //  .Calling(c => c.Details(hero.Id))
        //    .ShouldHave().
        //    Data(data => data
        //                     .WithSet<Hero>(dealers => dealers
        //                         .Any(d =>
        //                             d.Name == hero.Name &&
        //                             d.HeroType.ToString() == hero.HeroType &&
        //                             d.PlayerId == hero.Player.Id)))
        //    .AndAlso()
        //  .ShouldReturn()
        //  .View(view => view.WithModelOfType<HeroModel>());
    }
}
