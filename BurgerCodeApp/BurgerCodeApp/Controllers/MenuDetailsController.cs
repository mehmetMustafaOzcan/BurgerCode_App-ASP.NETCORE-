using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerCodeApp.Models;
using BurgerCodeApp.Data.Context;

namespace BurgerCodeApp.Controllers
{
    public class MenuDetailsController : Controller
    {
        private readonly BurgerDbContext _context;

        public MenuDetailsController(BurgerDbContext context)
        {
            _context = context;
        }
        /*
        // GET: MenuDetails
        public async Task<IActionResult> Index()
        {
            var burgerDbContext = _context.MenuDetails.Include(m => m.Menu).Include(m => m.Product);
            return View(await burgerDbContext.ToListAsync());
        }
        */
        // GET: MenuDetails/Details/5
        /*
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MenuDetails == null)
            {
                return NotFound();
            }

            var menuDetail = await _context.MenuDetails
                .Include(m => m.Menu)
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menuDetail == null)
            {
                return NotFound();
            }

            return View(menuDetail);
        }
        *//*
        // GET: MenuDetails/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_context.Menus, "MenuId", "MenuId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: MenuDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,ProductId,Quantity,UnitPrice")] MenuDetail menuDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "MenuId", "MenuId", menuDetail.MenuId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", menuDetail.ProductId);
            return View(menuDetail);
        }
        */
        /*
        // GET: MenuDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MenuDetails == null)
            {
                return NotFound();
            }

            var menuDetail = await _context.MenuDetails.FindAsync(id);
            if (menuDetail == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "MenuId", "MenuId", menuDetail.MenuId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", menuDetail.ProductId);
            return View(menuDetail);
        }
        
        // POST: MenuDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuId,ProductId,Quantity,UnitPrice")] MenuDetail menuDetail)
        {
            if (id != menuDetail.MenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuDetailExists(menuDetail.MenuId))
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
            ViewData["MenuId"] = new SelectList(_context.Menus, "MenuId", "MenuId", menuDetail.MenuId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", menuDetail.ProductId);
            return View(menuDetail);
        }
        */
        /*
        // GET: MenuDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MenuDetails == null)
            {
                return NotFound();
            }

            var menuDetail = await _context.MenuDetails
                .Include(m => m.Menu)
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menuDetail == null)
            {
                return NotFound();
            }

            return View(menuDetail);
        }

        // POST: MenuDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MenuDetails == null)
            {
                return Problem("Entity set 'BurgerDbContext.MenuDetails'  is null.");
            }
            var menuDetail = await _context.MenuDetails.FindAsync(id);
            if (menuDetail != null)
            {
                _context.MenuDetails.Remove(menuDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuDetailExists(int id)
        {
          return (_context.MenuDetails?.Any(e => e.MenuId == id)).GetValueOrDefault();
        }
        */
    }
}
