
using BattleRoyale.Controllers;
using BattleRoyale.Models.Items;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace BattleRoyale.Tests.Controllers
{
    public class ShopControllerTests
    {

        [Theory]
        [InlineData("Mage", "Weapon")]
        public void AllItemsShouldReturnQueryView(
            string heroType,
            string itemType
            )
            => MyController<ShopController>
            .Calling(c => c.All(new AllItemsQueryModel
            {
                HeroType = heroType,
                ItemType = itemType
            }))
            .ShouldReturn()
            .View(view => view.WithModelOfType<AllItemsQueryModel>());
    }
}
