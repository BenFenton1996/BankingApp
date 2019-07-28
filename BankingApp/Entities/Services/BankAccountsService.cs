using BankingApp.Entities.Models;
using System;
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

        /// <summary>
        /// Transfers money between two accounts if the sender has the funds
        /// </summary>
        /// <param name="amountToTransfer">The amount of money to transfer</param>
        /// <param name="senderID">The ID of the bank account sending funds</param>
        /// <param name="recipientID">The ID of the bank account recieving funds</param>
        /// <returns>True if the sender had the funds for the transfer, otherwise returns false</returns>
        bool TransferMoneyBetweenAccounts(decimal amountToTransfer, int senderID, int recipientID);

        /// <summary>
        /// Gets the 5 most recent transfers for a given bank account and returns them in a list
        /// </summary>
        /// <param name="accountId">The ID of the bank account to get recent transfers for</param>
        /// <returns>The 5 most recent rows in BankTransferLogs ordered by TransferDate descending in a List</returns>
        List<BankTransferLog> GetRecentTransfersForAccount(int accountId);
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

        public bool TransferMoneyBetweenAccounts(decimal amountToTransfer, int senderID, int recipientID)
        {
            var senderAccount = context.BankAccounts.FirstOrDefault(ba => ba.BankAccountID == senderID);
            if (senderAccount.Balance - amountToTransfer >= 0)
            {
                senderAccount.Balance -= amountToTransfer;
                var recipientAccount = context.BankAccounts.FirstOrDefault(ba => ba.BankAccountID == recipientID);
                recipientAccount.Balance += amountToTransfer;
                context.SaveChanges();

                //Log the details of the bank transfer
                context.BankTransferLogs.Add(new BankTransferLog
                {
                    AmountTransferred = amountToTransfer,
                    SenderID = senderID,
                    RecipientID = recipientID,
                    TransferDate = DateTime.Now
                });
                context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<BankTransferLog> GetRecentTransfersForAccount(int accountId)
        {
            return context.BankTransferLogs
                .Where(btl => btl.RecipientID == accountId || btl.SenderID == accountId)
                .OrderByDescending(bt1 => bt1.TransferDate)
                .Take(5)
                .ToList();
        }
    }
}
