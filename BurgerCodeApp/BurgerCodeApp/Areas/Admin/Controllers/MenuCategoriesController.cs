﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerCodeApp.Data;
using BurgerCodeApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace BurgerCodeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MenuCategoriesController : Controller
    {
        private readonly BurgerDbContext _context;

        public MenuCategoriesController(BurgerDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MenuCategories
        public async Task<IActionResult> Index()
        {
              return _context.MenuCategories != null ? 
                          View(await _context.MenuCategories.ToListAsync()) :
                          Problem("Entity set 'BurgerDbContext.MenuCategories'  is null.");
        }

        // GET: Admin/MenuCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MenuCategories == null)
            {
                return NotFound();
            }

            var menuCategory = await _context.MenuCategories
                .FirstOrDefaultAsync(m => m.MenuCategoryId == id);
            if (menuCategory == null)
            {
                return NotFound();
            }

            return View(menuCategory);
        }

        // GET: Admin/MenuCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MenuCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuCategoryId,Name,Description")] MenuCategory menuCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuCategory);
        }

        // GET: Admin/MenuCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MenuCategories == null)
            {
                return NotFound();
            }

            var menuCategory = await _context.MenuCategories.FindAsync(id);
            if (menuCategory == null)
            {
                return NotFound();
            }
            return View(menuCategory);
        }

        // POST: Admin/MenuCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuCategoryId,Name,Description")] MenuCategory menuCategory)
        {
            if (id != menuCategory.MenuCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuCategoryExists(menuCategory.MenuCategoryId))
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
            return View(menuCategory);
        }

        // GET: Admin/MenuCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MenuCategories == null)
            {
                return NotFound();
            }

            var menuCategory = await _context.MenuCategories
                .FirstOrDefaultAsync(m => m.MenuCategoryId == id);
            if (menuCategory == null)
            {
                return NotFound();
            }

            return View(menuCategory);
        }

        // POST: Admin/MenuCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MenuCategories == null)
            {
                return Problem("Entity set 'BurgerDbContext.MenuCategories'  is null.");
            }
            var menuCategory = await _context.MenuCategories.FindAsync(id);
            if (menuCategory != null)
            {
                _context.MenuCategories.Remove(menuCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuCategoryExists(int id)
        {
          return (_context.MenuCategories?.Any(e => e.MenuCategoryId == id)).GetValueOrDefault();
        }
    }
}
