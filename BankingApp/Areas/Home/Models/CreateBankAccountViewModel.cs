using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.Areas.Home.Models
{
    public class CreateBankAccountViewModel
    {
        [Required]
        public string AccountName { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal InitialDeposit { get; set; }

        [Required]
        public int AccountToDepositFromID { get; set; }

        public List<BankAccountViewModel> BankAccounts { get; set; }
    }
}
