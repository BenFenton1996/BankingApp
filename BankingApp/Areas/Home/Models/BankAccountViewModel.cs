using System.Collections.Generic;

namespace BankingApp.Areas.Home.Models
{
    public class BankAccountViewModel
    {
        public int BankAccountId { get; set; }
        public decimal Balance { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public List<TransferLogViewModel> RecentTransfers { get; set; }
    }
}
