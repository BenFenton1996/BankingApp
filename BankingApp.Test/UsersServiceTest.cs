using BankingApp.Entities;
using BankingApp.Entities.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace BankingApp.Test
{
    [TestFixture]
    class UsersServiceTest
    {
        [Test]
        public void CreateReadUserTest()
        {
            var options = new DbContextOptionsBuilder<BankingAppDbContext>()
                .UseInMemoryDatabase(databaseName: "BankingAppTest")
                .Options;

            using (var context = new BankingAppDbContext(options))
            {
                var service = new UsersService(context);
                service.CreateNewAccount("TestUsername", "TestEmail", "TestPassword");
                context.SaveChanges();
            }

            using (var context = new BankingAppDbContext(options))
            {
                Assert.AreEqual(1, context.Users.Where(ba => ba.Username == "TestUsername").Count());
            }          
        }
    }
}
