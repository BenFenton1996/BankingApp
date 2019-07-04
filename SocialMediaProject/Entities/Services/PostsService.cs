using SocialMediaProject.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace SocialMediaProject.Entities.Services
{
    public interface IPostsService
    {
        /// <summary>
        /// Gets all posts for a user
        /// </summary>
        /// <param name="userId">The user to get all posts for</param>
        /// <returns>A List of all posts for a given User</returns>
        List<Post> GetAllPosts(int userId);
    }

    public class PostsService : IPostsService
    {
        private readonly SMPDbContext context;
        public PostsService(SMPDbContext context)
        {
            this.context = context;
        }
      
        public List<Post> GetAllPosts(int userId)
        {
            return context.Posts.Where(p => p.UserID == userId).ToList();
        }
    }
}
