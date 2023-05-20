using BurgerCodeApp.Data.Context;
using BurgerCodeApp.Models;
using BurgerCodeApp.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BurgerCodeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BurgerDbContext _context;
        public HomeController(ILogger<HomeController> logger, BurgerDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {

            ViewBag.AllMenus =_context.Menus.Where(x=>x.SaleStatus==Status.Onsale).ToList();
            return View();
        }
        [Route("/NotFound404")]
        public IActionResult NotFound404()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}