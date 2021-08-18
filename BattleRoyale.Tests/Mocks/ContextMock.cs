

using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
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

        public static User CreateFakeUser()
        {
            var mockedUser = Mock.Of<User>();

            return mockedUser;
        }
    }
}
