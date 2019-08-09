using BankingApp.Entities.Models;
using System;

namespace BankingApp.Areas.Home.Models
{
    public class TransferLogViewModel
    {
        public TransferLogViewModel(BankTransferLog TransferLog, string description)
        {
            TransferDate = TransferLog.TransferDate;
            AmountTransferred = TransferLog.AmountTransferred;
            Description = description;
            TransactionType = TransferLog.TransactionType;
        }

        public DateTime TransferDate { get; set; }
        public decimal AmountTransferred { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
    }
}
