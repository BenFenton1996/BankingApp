using System.ComponentModel.DataAnnotations;

namespace SocialMediaProject.Models
{
    public class LoginStageOneViewModel
    {
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(255)]
        public string Password { get; set; }

        public bool AccountCreated { get; set; }
    }
}
