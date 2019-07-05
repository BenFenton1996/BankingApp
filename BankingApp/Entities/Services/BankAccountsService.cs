using BankingApp.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Entities.Services
{
    public interface IBankAccountsService
    {
        /// <summary>
        /// Gets all bank accounts for a specific user
        /// </summary>
        /// <param name="userId">The ID of the User to get the bank accounts for</param>
        /// <returns>All rows in BankAccounts with a matching UserID</returns>
        List<BankAccount> GetBankAccountsForUser(int userId);
    }

    public class BankAccountsService : IBankAccountsService
    {
        private readonly BankingAppDbContext context;
        public BankAccountsService(BankingAppDbContext context)
        {
            this.context = context;
        }

        public List<BankAccount> GetBankAccountsForUser(int userId)
        {
            return context.BankAccounts
                .Where(ba => ba.UserID == userId)
                .ToList();
        }
    }
}
