using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Entities.Models
{
    [Table("BankAccounts")]
    public class BankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankAccountId { get; set; }       

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        [MaxLength(255)]
        public string AccountName { get; set; }

        [Required]
        [MaxLength(255)]
        public string AccountType { get; set; }
    }
}
