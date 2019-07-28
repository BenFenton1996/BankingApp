using BankingApp.Entities.Models;
using System;

namespace BankingApp.Areas.Home.Models
{
    public class TransferLogViewModel
    {
        public TransferLogViewModel(BankTransferLog TransferLog)
        {
            TransferDate = TransferLog.TransferDate;
            AmountTransferred = TransferLog.AmountTransferred;
            SenderID = TransferLog.SenderID;
            RecipientID = TransferLog.RecipientID;
        }

        public DateTime TransferDate { get; set; }
        public decimal AmountTransferred { get; set; }
        public int SenderID { get; set; }
        public int RecipientID { get; set; }
    }
}
