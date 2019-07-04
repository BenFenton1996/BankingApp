using Microsoft.EntityFrameworkCore;
using SocialMediaProject.Entities.Models;

namespace SocialMediaProject.Entities
{
    public class SMPDbContext : DbContext
    {
        public SMPDbContext(DbContextOptions<SMPDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
