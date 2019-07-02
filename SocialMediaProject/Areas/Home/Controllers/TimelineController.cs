using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaProject.Utilities;

namespace SocialMediaProject.Controllers
{
    [Area("Home")]
    [Authorize]
    public class TimelineController : Controller
    {
        private ISMPContext smpContext;
        public TimelineController(ISMPContext smpContext)
        {
            this.smpContext = smpContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
