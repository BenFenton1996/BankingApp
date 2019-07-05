using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Utilities;

namespace BankingApp.Controllers
{
    [Area("Home")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IBankingAppContext BankingAppContext;
        public HomeController(IBankingAppContext BankingAppContext)
        {
            this.BankingAppContext = BankingAppContext;
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
