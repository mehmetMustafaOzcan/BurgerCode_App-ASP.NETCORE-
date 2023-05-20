using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerCodeApp.Models;
using BurgerCodeApp.Data.Context;
using BurgerCodeApp.Models.Enums;

namespace BurgerCodeApp.Controllers
{
    public class MenusController : Controller
    {
        private readonly BurgerDbContext _context;

        public MenusController(BurgerDbContext context)
        {
            _context = context;
        }

        // Route: /Menus
        public async Task<IActionResult> Index(string filter)/**/
        {
            if (filter == "Menus")
            {
                var burgerDbContext = _context.Menus.Include(m => m.MenuCategory).Where(c => c.MenuCategory.Name.Contains("Menu")&&c.SaleStatus==Status.Onsale);
                return View(await burgerDbContext.ToListAsync());
            }
            else if (filter == "Burger")
            {
                var burgerDbContext = _context.Menus.Include(m => m.MenuCategory).Where(c => c.MenuCategory.Name.Contains("Burger") && c.SaleStatus == Status.Onsale);
                return View(await burgerDbContext.ToListAsync());
            }
            else
            {
                var burgerDbContext = _context.Menus.Include(m => m.MenuCategory);
            return View(burgerDbContext);

            }

        }
       
       
    }
}
