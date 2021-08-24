using BattleRoyale.Models;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Services.HomeServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BattleRoyale.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly IMemoryCache cache;

        public HomeController(IHomeService homeService,IMemoryCache cache)
        {
            this.homeService = homeService;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var strongestHeroes = this.cache.Get<List<HeroIndexViewModel>>("");

            if (strongestHeroes == null)
            {
                strongestHeroes = this.homeService.GetTopHeroes();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set("", strongestHeroes, cacheOptions);
            }

            return View(strongestHeroes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
