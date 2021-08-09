using BattleRoyale.Models.Shop;
using BattleRoyale.Services.ItemServices;
using Microsoft.AspNetCore.Mvc;

namespace BattleRoyale.Areas.Admin.Controllers
{
    public class ItemsController : AdminController
    {

        public IActionResult AddedItem() => View();
        
    }
}
