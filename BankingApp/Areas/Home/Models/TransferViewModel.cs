using System.ComponentModel.DataAnnotations;

namespace BankingApp.Areas.Home.Models
{
    public class TransferViewModel
    {
        [Required]
        public decimal AmountToTransfer { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public int RecipientId { get; set; }
    }
}
