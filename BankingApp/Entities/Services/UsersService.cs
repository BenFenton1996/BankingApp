using BankingApp.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Entities.Services
{   
    public class UsersService : Interfaces.IUsersService
    {
        private readonly BankingAppDbContext context;
        public UsersService(BankingAppDbContext context)
        {
            this.context = context;
        }

        public List<User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        public User CheckUserDetails(string Email, string Password)
        {
            return context.Users
                .FirstOrDefault(u => u.Email == Email && u.Password.SequenceEqual(Utilities.BankingAppHash.HashText(Password, u.Salt)));
        }

        public bool CreateNewAccount(string Username, string Email, string Password)
        {
            if (!context.Users.Where(u => u.Username == Username || u.Email == Email).Any())
            {
                byte[] salt = Utilities.BankingAppHash.GenerateSalt();

                var user = new User
                {
                    Username = Username,
                    Email = Email,
                    Password = Utilities.BankingAppHash.HashText(Password, salt),
                    Salt = salt,
                    Role = "User"
                };

                context.Users.Add(user);
                context.SaveChanges();

                //Set up default bank account for ease of use when testing since this project is not intended for development
                context.BankAccounts.Add(new BankAccount
                {
                    AccountName = "Default Account",
                    AccountType = "Default",
                    Balance = 1000M,
                    UserID = user.UserID
                });
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
