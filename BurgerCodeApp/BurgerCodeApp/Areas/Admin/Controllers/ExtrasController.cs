using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerCodeApp.Models;
using Microsoft.AspNetCore.Authorization;
using BurgerCodeApp.Data.Context;

namespace BurgerCodeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ExtrasController : Controller
    {
        private readonly BurgerDbContext _context;

        public ExtrasController(BurgerDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Extras
        public async Task<IActionResult> Index()/**/
        {
              return _context.Extras != null ? 
                          View(await _context.Extras.ToListAsync()) :
                          Problem("Entity set 'BurgerDbContext.Extras'  is null.");
        }

        // GET: Admin/Extras/Create
        public IActionResult Create()/**/
        {
            return View();
        }

        // POST: Admin/Extras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExtraId,Name,Description,Price")] Extra extra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(extra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(extra);
        }

        // GET: Admin/Extras/Edit/5
        public async Task<IActionResult> Edit(int? id)/**/
        {
            if (id == null || _context.Extras == null)
            {
                return NotFound();
            }

            var extra = await _context.Extras.FindAsync(id);
            if (extra == null)
            {
                return NotFound();
            }
            return View(extra);
        }

        // POST: Admin/Extras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExtraId,Name,Description,Price")] Extra extra)/**/
        {
            if (id != extra.ExtraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(extra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExtraExists(extra.ExtraId))
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
            return View(extra);
        }

        // GET: Admin/Extras/Delete/5
        public async Task<IActionResult> Delete(int? id)/**/
        {
            if (id == null || _context.Extras == null)
            {
                return NotFound();
            }

            var extra = await _context.Extras
                .FirstOrDefaultAsync(m => m.ExtraId == id);
            if (extra == null)
            {
                return NotFound();
            }

            return View(extra);
        }

        // POST: Admin/Extras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)/*trycatch*/
        {

            if (_context.Extras == null)
            {
                return Problem("Entity set 'BurgerDbContext.Extras'  is null.");
            }
            try
            {
                var extra = await _context.Extras.FindAsync(id);
                if (extra != null)
                {
                    _context.Extras.Remove(extra);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

              return RedirectToAction(nameof(Index));
            }
            
        }
        private bool ExtraExists(int id)
        {
            return (_context.Extras?.Any(e => e.ExtraId == id)).GetValueOrDefault();
        }

    }
}
