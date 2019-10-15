using BankingApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Entities.Services
{
    public class BankAccountsService : Interfaces.IBankAccountsService
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
                .OrderByDescending(ba => ba.BankAccountID)
                .ToList();
        }

        public bool TransferMoneyBetweenAccounts(decimal amountToTransfer, int senderID, int recipientID, string transactionType)
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
                    TransferDate = DateTime.Now,
                    TransactionType = transactionType
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

        public string GetBankAccountName(int accountId)
        {
            return context.BankAccounts
                .Where(ba => ba.BankAccountID == accountId)
                .Select(ba => ba.AccountName)
                .FirstOrDefault();
        }

        public bool CreateBankAccount(int userId, int senderId, string accountName, string accountType, decimal initialDeposit)
        {
            //Make sure that an account with the requested name does not exist
            //Make sure that the senderId belongs to one of the current user's bank accounts
            //Make sure that the sender account has enough funds to complete the initial deposit
            var senderAccount = context.BankAccounts.FirstOrDefault(ba => ba.BankAccountID == senderId);
            if (context.BankAccounts.Any(ba => ba.AccountName == accountName && ba.UserID == userId) && context.BankAccounts.Any(ba => ba.BankAccountID == senderId) || senderAccount.Balance - initialDeposit < 0)
            {
                return false;
            }

            var newBankAccount = new BankAccount
            {
                UserID = userId,
                AccountName = accountName,
                AccountType = accountType,
                Balance = 0
            };
            context.BankAccounts.Add(newBankAccount);
            context.SaveChanges();

            if (initialDeposit > 0)
            {
                //Transfer the initial deposit from the chosen sender account to the new account
                senderAccount.Balance -= initialDeposit;
                newBankAccount.Balance += initialDeposit;
                context.SaveChanges();

                //Log the details of the initial deposit
                context.BankTransferLogs.Add(new BankTransferLog
                {
                    AmountTransferred = initialDeposit,
                    SenderID = senderId,
                    RecipientID = newBankAccount.BankAccountID,
                    TransferDate = DateTime.Now,
                    TransactionType = "INITIAL DEPOSIT"
                });
                context.SaveChanges();
            }
            return true;
        }
    }
}
