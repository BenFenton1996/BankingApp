using BankingApp.Entities.Models;
using System.Collections.Generic;

namespace BankingApp.Entities.Services.Interfaces
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
        /// <param name="senderId">The ID of the bank account sending funds</param>
        /// <param name="recipientId">The ID of the bank account receiving funds</param>
        /// <param name="transactionType">The type of the transaction</param>
        /// <returns>True if the sender had the funds for the transfer, otherwise returns false</returns>
        bool TransferMoneyBetweenAccounts(decimal amountToTransfer, int senderId, int recipientId, string transactionType);

        /// <summary>
        /// Gets the 5 most recent transfers for a given bank account and returns them in a list
        /// </summary>
        /// <param name="accountId">The ID of the bank account to get recent transfers for</param>
        /// <returns>The 5 most recent rows in BankTransferLogs ordered by TransferDate descending in a List</returns>
        List<BankTransferLog> GetRecentTransfersForAccount(int accountId);

        /// <summary>
        /// Gets the account name of a bank account
        /// </summary>
        /// <param name="accountId">The ID of the bank account to get the name for</param>
        /// <returns>The name of the first bank account with a matching ID as a string</returns>
        string GetBankAccountName(int accountId);

        /// <summary>
        /// Adds a new bank account to a user's account
        /// </summary>
        /// <param name="userId">The ID of the user to attach the new bank account to</param>
        /// <param name="senderId">The ID of the bank account the initial deposit is to be transferred from</param>
        /// <param name="accountName">The name of the new bank account</param>
        /// <param name="accountType">The type of the bank account</param>
        /// <param name="initialDeposit">The amount of money the bank account will be initialized with</param>
        bool CreateBankAccount(int userId, int senderId, string accountName, string accountType, decimal initialDeposit);
    }
}
