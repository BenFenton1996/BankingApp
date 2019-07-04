using System.ComponentModel.DataAnnotations;

namespace SocialMediaProject.Areas.Home.Models
{
    public class PostViewModel
    {
        [Required]
        public int User { get; set; }

        [Required]
        public string Contents { get; set; }
    }
}
