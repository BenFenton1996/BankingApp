using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Entities.Models
{
    public class BankTransferLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankTransferLogID { get; set; }

        [Required]
        public DateTime TransferDate { get; set; }

        [Required]
        public decimal AmountTransferred { get; set; }

        [Required]
        public int SenderID { get; set; }

        [Required]
        public int RecipientID { get; set; }

        [Required]
        public string TransactionType { get; set; }
    }
}
