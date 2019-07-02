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
            //If table already exists in the database then return
            if (context.Users.Any())
            {
                return;
            }

            //Seed the table with some test values
            var users = new User[]
            {
                new User{ Username="Admin", Password="fbf826d62fd43f0643e283c27040e2f235ddd68908b0c286c77a456b465ace13", Email="Admin@SMP.com", Role="Administrator" },
                new User{ Username="Test", Password="e7cf3ef4f17c3999a94f2c6f612e8a888e5b1026878e4e19398b23bd38ec221a", Email="Test@SMP.com", Role="User" }
            };
            foreach (User user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
