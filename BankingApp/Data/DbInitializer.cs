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

            //If User table is empty then seed with some test data
            if (!context.Users.Any())
            {
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

            //If BankAccounts table is empty then seed with some test data
            if (!context.BankAccounts.Any())
            {
                context.BankAccounts.Add(new BankAccount
                {
                    AccountName = "Cash",
                    AccountType = "Standard",
                    Balance = -130.44M,
                    UserID = 2
                });
                context.BankAccounts.Add(new BankAccount
                {
                    AccountName = "Savings",
                    AccountType = "Savings Builder",
                    Balance = 1422.02M,
                    UserID = 2
                });
                context.SaveChanges();
            }
        }
    }
}