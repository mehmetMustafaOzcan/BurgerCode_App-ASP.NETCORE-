using Microsoft.AspNetCore.Mvc;

namespace Teori2804.Areas.Employee.Controllers
{
    public class HomeController : Controller
    {
        [Area("Employee")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
