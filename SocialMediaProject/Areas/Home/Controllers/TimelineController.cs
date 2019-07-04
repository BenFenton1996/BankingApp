using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaProject.Areas.Home.Models;
using SocialMediaProject.Entities.Services;
using SocialMediaProject.Utilities;
using System.Collections.Generic;

namespace SocialMediaProject.Controllers
{
    [Area("Home")]
    [Authorize]
    public class TimelineController : Controller
    {
        private readonly ISMPContext SMPContext;
        private readonly IPostsService PostsService;
        public TimelineController(ISMPContext SMPContext, IPostsService PostsService)
        {
            this.SMPContext = SMPContext;
            this.PostsService = PostsService;
        }

        public IActionResult Index()
        {
            var posts = new List<PostViewModel>();
            foreach(var post in PostsService.GetAllPosts(SMPContext.GetUserID()))
            {
                posts.Add(new PostViewModel
                {
                    User = post.UserID,
                    Contents = post.Contents
                });
            }

            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
