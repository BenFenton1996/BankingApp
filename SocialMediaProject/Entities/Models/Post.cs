using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaProject.Entities.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public string Contents { get; set; }
    }
}
