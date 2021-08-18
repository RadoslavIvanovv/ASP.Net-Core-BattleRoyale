using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Tests.Mocks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTested.AspNetCore.Mvc;


namespace BattleRoyale.Tests
{
    class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.ReplaceTransient<User>(_ => ContextMock.CreateFakeUser());
        }
    }
}
