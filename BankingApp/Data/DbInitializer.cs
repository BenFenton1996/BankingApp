using BankingApp.Entities;
using BankingApp.Entities.Models;
using System;
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
                    UserId = 2
                });
                context.BankAccounts.Add(new BankAccount
                {
                    AccountName = "Savings",
                    AccountType = "Savings Builder",
                    Balance = 1422.02M,
                    UserId = 2
                });
                context.BankAccounts.Add(new BankAccount
                {
                    AccountName = "Emergency Money",
                    AccountType = "Savings Builder",
                    Balance = 0M,
                    UserId = 2
                });
                context.SaveChanges();
            }

            //If BankTransferLog table table is empty then seed with some test data
            if (!context.BankTransferLogs.Any())
            {
                context.BankTransferLogs.Add(new BankTransferLog
                {
                    SenderId = 1,
                    RecipientId = 2,
                    TransferDate = DateTime.Now,
                    AmountTransferred = 144.02M,
                    TransactionType = "Transfer"
                });
                context.BankTransferLogs.Add(new BankTransferLog
                {
                    SenderId = 2,
                    RecipientId = 1,
                    TransferDate = DateTime.Now,
                    AmountTransferred = 50.10M,
                    TransactionType = "Transfer"
                });
                context.BankTransferLogs.Add(new BankTransferLog
                {
                    SenderId = 3,
                    RecipientId = 2,
                    TransferDate = DateTime.Now,
                    AmountTransferred = 100.00M,
                    TransactionType = "Transfer"
                });
                context.SaveChanges();
            }
        }
    }
}