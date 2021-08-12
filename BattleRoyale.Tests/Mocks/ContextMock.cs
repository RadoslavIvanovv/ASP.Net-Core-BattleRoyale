

using BattleRoyale.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace BattleRoyale.Tests.Mocks
{
    public class ContextMock
    {
        public static BattleRoyaleDbContext Instance
        {
            get
            {
                var contextMock = new DbContextOptionsBuilder<BattleRoyaleDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

                return new BattleRoyaleDbContext(contextMock);
            }
        }
    }
}
