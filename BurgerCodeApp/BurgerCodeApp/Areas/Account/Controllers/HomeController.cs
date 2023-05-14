using Microsoft.AspNetCore.Mvc;

namespace BurgerCodeApp.Areas.Account.Controllers
{
    public class HomeController : Controller
    {
        [Area("Account")]
        public IActionResult Index()
        {
            return View();
        }
        [Area("Account")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
