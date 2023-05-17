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
using Microsoft.CodeAnalysis;

namespace BurgerCodeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class MenusController : Controller
    {
        private readonly BurgerDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MenusController(BurgerDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
            ViewData["MenuCategoryId"] = MenuCategory;
            return View();
        }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("MenuId,MenuName,MenuCategoryId,MenüPrice")]*/ MenuVm menuvm, IFormFile photo)
        {
            var fileName = "";
            if (photo != null && photo.Length > 0)
            {
                 fileName = Path.GetFileName(photo.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

            }

            if (menuvm != null)
            {
                Menu menu = new()
                {
                    MenuName = menuvm.MenuName,
                    MenüPrice = menuvm.MenüPrice,
                    MenuCategoryId = menuvm.MenuCategoryId!=0 ? menuvm.MenuCategoryId:null,
                    Photopath = "/Uploads"+"/" +fileName

                };
                _context.Menus.Add(menu);
                _context.SaveChanges();
                return RedirectToAction(nameof(AddProduct));

            }
            List<SelectListItem> MenuCategory = _context.MenuCategories.Select(x => new SelectListItem { Value = x.MenuCategoryId.ToString(), Text = x.Name, }).ToList();
            List<SelectListItem> Product = _context.Products.Select(x => new SelectListItem { Value = x.ProductId.ToString(), Text = x.Name, }).ToList();
            ViewBag.ProductId = Product;
            ViewData["MenuCategoryId"] = MenuCategory;
            return View(menuvm);
        }
        public async Task<IActionResult> AddProduct()
        {
         
            List<SelectListItem> Product = _context.Products.Select(x => new SelectListItem { Value = x.ProductId.ToString(), Text = x.Name, }).ToList();
            ViewBag.ProductId = Product;
            List<SelectListItem> Menus = _context.Menus.Select(x => new SelectListItem { Value = x.MenuId.ToString(), Text = x.MenuName}).ToList();
            ViewBag.Menus = Menus;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(MenuVm menuvm)
        {
            if (menuvm != null)
            {
               
                MenuDetail menudt = new()
                {
                    MenuId = menuvm.MenuId,
                    ProductId = menuvm.ProductId,
                    Quantity=menuvm.ProductQuantity,
                    UnitPrice=_context.Products.Find(menuvm.ProductId).Price
                };
                 _context.Menus.Find(menuvm.MenuId).MenuDetails.Add(menudt);
                await _context.SaveChangesAsync();
            }
            List<SelectListItem> Product = _context.Products.Select(x => new SelectListItem { Value = x.ProductId.ToString(), Text = x.Name, }).ToList();
            ViewBag.ProductId = Product;
            return RedirectToAction("AddProduct");
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
