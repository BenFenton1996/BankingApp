using BankingApp.Entities;
using BankingApp.Entities.Models;
using System.Linq;

namespace BankingApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(BankingAppDbContext context)
        {
            context.Database.EnsureCreated();

            //If table already exists and is populated then return
            if (context.Users.Any())
            {
                return;
            }

            //Seed the table with some test values
            var salt = Utilities.BankingAppHash.GenerateSalt();
            context.Users.Add(new User
            {
                Username = "Admin",
                Email = "Admin@BankingApp.com",
                Password = Utilities.BankingAppHash.HashText("SuperPassword", salt),
                Salt = salt,
                Role = "Administrator"
            });

            salt = Utilities.BankingAppHash.GenerateSalt();
            context.Users.Add(new User
            {
                Username = "Test",
                Email = "Test@BankingApp.com",
                Password = Utilities.BankingAppHash.HashText("Password", salt),
                Salt = salt,
                Role = "User"
            });
            context.SaveChanges();
        }
    }
}