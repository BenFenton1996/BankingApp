using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Entities.Models
{
    public class BankTransferLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankTransferLogId { get; set; }

        [Required]
        public DateTime TransferDate { get; set; }

        [Required]
        public decimal AmountTransferred { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public int RecipientId { get; set; }

        [Required]
        public string TransactionType { get; set; }
    }
}
