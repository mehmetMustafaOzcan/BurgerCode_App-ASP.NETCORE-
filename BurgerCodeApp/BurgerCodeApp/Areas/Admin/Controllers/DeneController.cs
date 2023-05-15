using Microsoft.AspNetCore.Mvc;

namespace BurgerCodeApp.Areas.Admin.Controllers
{
    public class DeneController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
