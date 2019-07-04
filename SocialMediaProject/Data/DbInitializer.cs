using SocialMediaProject.Entities;
using SocialMediaProject.Entities.Models;
using System.Linq;

namespace SocialMediaProject.Data
{
    public class DbInitializer
    {
        public static void Initialize(SMPDbContext context)
        {
            context.Database.EnsureCreated();

            //If table already exists and is populated then return
            if (context.Users.Any())
            {
                return;
            }

            //Seed the table with some test values
            var salt = Utilities.SMPHash.GenerateSalt();
            context.Users.Add(new User
            {
                Username = "Admin",
                Email = "Admin@SMP.com",
                Password = Utilities.SMPHash.HashText("SuperPassword", salt),
                Salt = salt,
                Role = "Administrator"
            });

            salt = Utilities.SMPHash.GenerateSalt();
            context.Users.Add(new User
            {
                Username = "Test",
                Email = "Test@SMP.com",
                Password = Utilities.SMPHash.HashText("Password", salt),
                Salt = salt,
                Role = "User"
            });
            context.SaveChanges();

            context.Posts.Add(new Post
            {
                UserID = 2,
                Contents = "TEST"
            });
            context.Posts.Add(new Post
            {
                UserID = 2,
                Contents = "TEST2"
            });

            context.SaveChanges();
        }
    }
}