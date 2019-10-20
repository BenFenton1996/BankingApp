using BankingApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Entities.Services
{
    public class BankAccountsService : Interfaces.IBankAccountsService
    {
        private readonly BankingAppDbContext _context;
        public BankAccountsService(BankingAppDbContext context)
        {
            _context = context;
        }

        public List<BankAccount> GetBankAccountsForUser(int userId)
        {
            return _context.BankAccounts
                .Where(ba => ba.UserId == userId)
                .OrderByDescending(ba => ba.BankAccountId)
                .ToList();
        }

        public bool TransferMoneyBetweenAccounts(decimal amountToTransfer, int senderId, int recipientId, string transactionType)
        {
            var senderAccount = _context.BankAccounts.FirstOrDefault(ba => ba.BankAccountId == senderId);
            if (senderAccount == null || senderAccount.Balance - amountToTransfer < 0)
            {
                return false;
            }

            senderAccount.Balance -= amountToTransfer;
            var recipientAccount = _context.BankAccounts.FirstOrDefault(ba => ba.BankAccountId == recipientId);
            if (recipientAccount == null)
            {
                return false;
            }
            recipientAccount.Balance += amountToTransfer;
            _context.SaveChanges();

            //Log the details of the bank transfer
            _context.BankTransferLogs.Add(new BankTransferLog
            {
                AmountTransferred = amountToTransfer,
                SenderId = senderId,
                RecipientId = recipientId,
                TransferDate = DateTime.Now,
                TransactionType = transactionType
            });
            _context.SaveChanges();
            return true;
        }

        public List<BankTransferLog> GetRecentTransfersForAccount(int accountId)
        {
            return _context.BankTransferLogs
                .Where(btl => btl.RecipientId == accountId || btl.SenderId == accountId)
                .OrderByDescending(bt1 => bt1.TransferDate)
                .Take(5)
                .ToList();
        }

        public string GetBankAccountName(int accountId)
        {
            return _context.BankAccounts
                .Where(ba => ba.BankAccountId == accountId)
                .Select(ba => ba.AccountName)
                .FirstOrDefault();
        }

        public bool CreateBankAccount(int userId, int senderId, string accountName, string accountType, decimal initialDeposit)
        {
            //Make sure that an account with the requested name does not exist
            //Make sure that the senderId belongs to one of the current user's bank accounts
            //Make sure that the sender account has enough funds to complete the initial deposit
            var senderAccount = _context.BankAccounts.FirstOrDefault(ba => ba.BankAccountId == senderId);
            if (senderAccount == null || _context.BankAccounts.Any(ba => ba.AccountName == accountName && ba.UserId == userId) || !_context.BankAccounts.Any(ba => ba.BankAccountId == senderId) || senderAccount.Balance - initialDeposit < 0)
            {
                return false;
            }

            var newBankAccount = new BankAccount
            {
                UserId = userId,
                AccountName = accountName,
                AccountType = accountType,
                Balance = 0
            };
            _context.BankAccounts.Add(newBankAccount);
            _context.SaveChanges();

            if (initialDeposit <= 0)
            {
                return false;
            }

            //Transfer the initial deposit from the chosen sender account to the new account
            senderAccount.Balance -= initialDeposit;
            newBankAccount.Balance += initialDeposit;
            _context.SaveChanges();

            //Log the details of the initial deposit
            _context.BankTransferLogs.Add(new BankTransferLog
            {
                AmountTransferred = initialDeposit,
                SenderId = senderId,
                RecipientId = newBankAccount.BankAccountId,
                TransferDate = DateTime.Now,
                TransactionType = "INITIAL DEPOSIT"
            });
            _context.SaveChanges();
            return true;
        }
    }
}
