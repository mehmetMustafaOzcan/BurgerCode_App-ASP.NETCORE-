using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerCodeApp.Data;
using BurgerCodeApp.Models;

namespace BurgerCodeApp.Controllers
{
    public class ExtraDetailsController : Controller
    {
        private readonly BurgerDbContext _context;

        public ExtraDetailsController(BurgerDbContext context)
        {
            _context = context;
        }

        // GET: ExtraDetails
        public async Task<IActionResult> Index()
        {
            var burgerDbContext = _context.ExtraDetails.Include(e => e.Basket).Include(e => e.Extra);
            return View(await burgerDbContext.ToListAsync());
        }

        // GET: ExtraDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExtraDetails == null)
            {
                return NotFound();
            }

            var extraDetail = await _context.ExtraDetails
                .Include(e => e.Basket)
                .Include(e => e.Extra)
                .FirstOrDefaultAsync(m => m.BasketId == id);
            if (extraDetail == null)
            {
                return NotFound();
            }

            return View(extraDetail);
        }

        // GET: ExtraDetails/Create
        public IActionResult Create()
        {
            ViewData["BasketId"] = new SelectList(_context.Baskets, "BasketId", "BasketId");
            ViewData["ExtraId"] = new SelectList(_context.Extras, "ExtraId", "ExtraId");
            return View();
        }

        // POST: ExtraDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BasketId,ExtraId,Quantity")] ExtraDetail extraDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(extraDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BasketId"] = new SelectList(_context.Baskets, "BasketId", "BasketId", extraDetail.BasketId);
            ViewData["ExtraId"] = new SelectList(_context.Extras, "ExtraId", "ExtraId", extraDetail.ExtraId);
            return View(extraDetail);
        }

        // GET: ExtraDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExtraDetails == null)
            {
                return NotFound();
            }

            var extraDetail = await _context.ExtraDetails.FindAsync(id);
            if (extraDetail == null)
            {
                return NotFound();
            }
            ViewData["BasketId"] = new SelectList(_context.Baskets, "BasketId", "BasketId", extraDetail.BasketId);
            ViewData["ExtraId"] = new SelectList(_context.Extras, "ExtraId", "ExtraId", extraDetail.ExtraId);
            return View(extraDetail);
        }

        // POST: ExtraDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BasketId,ExtraId,Quantity")] ExtraDetail extraDetail)
        {
            if (id != extraDetail.BasketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(extraDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExtraDetailExists(extraDetail.BasketId))
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
            ViewData["BasketId"] = new SelectList(_context.Baskets, "BasketId", "BasketId", extraDetail.BasketId);
            ViewData["ExtraId"] = new SelectList(_context.Extras, "ExtraId", "ExtraId", extraDetail.ExtraId);
            return View(extraDetail);
        }

        // GET: ExtraDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExtraDetails == null)
            {
                return NotFound();
            }

            var extraDetail = await _context.ExtraDetails
                .Include(e => e.Basket)
                .Include(e => e.Extra)
                .FirstOrDefaultAsync(m => m.BasketId == id);
            if (extraDetail == null)
            {
                return NotFound();
            }

            return View(extraDetail);
        }

        // POST: ExtraDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExtraDetails == null)
            {
                return Problem("Entity set 'BurgerDbContext.ExtraDetails'  is null.");
            }
            var extraDetail = await _context.ExtraDetails.FindAsync(id);
            if (extraDetail != null)
            {
                _context.ExtraDetails.Remove(extraDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExtraDetailExists(int id)
        {
          return (_context.ExtraDetails?.Any(e => e.BasketId == id)).GetValueOrDefault();
        }
    }
}
