using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerCodeApp.Data;
using BurgerCodeApp.Models;
using BurgerCodeApp.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace BurgerCodeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
   
    public class MenusController : Controller
    {
        private readonly BurgerDbContext _context;

        public MenusController(BurgerDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Menus
        public async Task<IActionResult> Index()
        {
            var burgerDbContext = _context.Menus.Include(m => m.MenuCategory);
            return View(await burgerDbContext.ToListAsync());
        }

        // GET: Admin/Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .Include(m => m.MenuCategory)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Admin/Menus/Create
        public IActionResult Create()
        {
            List<SelectListItem> MenuCategory = _context.MenuCategories.Select(x => new SelectListItem { Value = x.MenuCategoryId.ToString(), Text = x.Name, }).ToList();
            List<SelectListItem> Product = _context.Products.Select(x => new SelectListItem { Value = x.ProductId.ToString(), Text = x.Name, }).ToList();
            ViewBag.ProductId = Product;
            ViewData["MenuCategoryId"] = MenuCategory;
            return View();
        }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,MenuName,MenuCategoryId,MenüPrice")] MenuVm menuvm)
        {
            if (ModelState.IsValid)
            {
                MenuDetail menuDetail = new()
                {
                    ProductId = menuvm.ProductId,
                    Quantity = menuvm.ProductQuantity,
                    UnitPrice = _context.Products.Find(menuvm.ProductId).Price

                };
                Menu menu = new()
                {
                    MenuName = menuvm.MenuName,
                    MenuCategoryId = menuvm.MenuCategoryId,
                    MenüPrice = menuvm.MenüPrice,
                    // MenuDetails = menuDetail yarım kaldı burası
                };
                //_context.Add(menu);
                //await _context.SaveChangesAsync();



                //return RedirectToAction(nameof(Index));

                //ViewData["MenuCategoryId"] = new SelectList(_context.MenuCategories, "MenuCategoryId", "MenuCategoryId", menu.MenuCategoryId);
                //return View(menu);
            }
                return View();
        }


        // GET: Admin/Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            ViewData["MenuCategoryId"] = new SelectList(_context.MenuCategories, "MenuCategoryId", "MenuCategoryId", menu.MenuCategoryId);
            return View(menu);
        }

        // POST: Admin/Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuId,MenuName,MenuCategoryId,MenüPrice")] Menu menu)
        {
            if (id != menu.MenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.MenuId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuCategoryId"] = new SelectList(_context.MenuCategories, "MenuCategoryId", "MenuCategoryId", menu.MenuCategoryId);
            return View(menu);
        }

        // GET: Admin/Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .Include(m => m.MenuCategory)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Menus == null)
            {
                return Problem("Entity set 'BurgerDbContext.Menus'  is null.");
            }
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return (_context.Menus?.Any(e => e.MenuId == id)).GetValueOrDefault();
        }
    }
}
