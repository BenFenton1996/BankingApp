using System.ComponentModel.DataAnnotations;

namespace BankingApp.Areas.Home.Models
{
    public class TransferViewModel
    {
        [Required]
        public decimal AmountToTransfer { get; set; }

        [Required]
        public int SenderID { get; set; }

        [Required]
        public int RecipientID { get; set; }
    }
}
