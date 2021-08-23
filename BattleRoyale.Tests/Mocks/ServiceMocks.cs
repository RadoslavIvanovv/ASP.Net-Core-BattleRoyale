

using BattleRoyale.Services.BattleArenaServices;
using Moq;

namespace BattleRoyale.Tests.Mocks
{
    public class ServiceMocks
    {
        public static BattleArenaService CreateBattleArenaService()
        {
            var mockedService = Mock.Of<BattleArenaService>();

            return mockedService;
        }
    }
}
