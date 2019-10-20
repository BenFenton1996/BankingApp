using BankingApp.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Entities.Services
{
    public class UsersService : Interfaces.IUsersService
    {
        private readonly BankingAppDbContext _context;
        public UsersService(BankingAppDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User CheckUserDetails(string email, string password)
        {
            var hashedPassword = Utilities.BankingAppHash.HashText(
                password,
                _context.Users.Where(u => u.Email == email).Select(u => u.Salt).FirstOrDefault());

            return _context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == hashedPassword);
        }

        public bool CreateNewAccount(string username, string email, string password)
        {
            if (_context.Users.Any(u => u.Username == username || u.Email == email))
            {
                return false;
            }

            byte[] salt = Utilities.BankingAppHash.GenerateSalt();
            var user = new User
            {
                Username = username,
                Email = email,
                Password = Utilities.BankingAppHash.HashText(password, salt),
                Salt = salt,
                Role = "User"
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            //Set up default bank account for ease of use when testing since this project is not intended for development
            _context.BankAccounts.Add(new BankAccount
            {
                AccountName = "Default Account",
                AccountType = "Default",
                Balance = 1000M,
                UserId = user.UserId
            });
            _context.SaveChanges();
            return true;
        }
    }
}
