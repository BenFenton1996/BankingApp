using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "A Username is required.")]
        [MaxLength(255)]
        public string NewUsername { get; set; }

        [Required(ErrorMessage = "An email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [MaxLength(255)]
        public string NewEmail { get; set; }

        [Required(ErrorMessage = "A password is required.")]
        [DataType(DataType.Password)]
        [MaxLength(255)]
        public string NewPassword { get; set; }
    }
}