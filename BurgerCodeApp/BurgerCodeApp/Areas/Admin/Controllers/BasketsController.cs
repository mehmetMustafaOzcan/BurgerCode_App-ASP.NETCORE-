using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerCodeApp.Data;
using BurgerCodeApp.Models;
using BurgerCodeApp.Models.Enums;

namespace BurgerCodeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BasketsController : Controller
    {
        private readonly BurgerDbContext _context;

        public BasketsController(BurgerDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Baskets
        public async Task<IActionResult> Index()
        {
            var burgerDbContext = _context.Baskets.Include(b => b.AppUser).GroupBy(x => new { x.AppUser.Email,x.AppUser.UserName }).Select(b => new {User=b.Key.UserName,Email=b.Key.Email,Total=b.Sum(x=>x.TotalPrice)});
            return View(await burgerDbContext.ToListAsync());
        }

        // GET: Admin/Baskets/Details/5
        public async Task<IActionResult> UserBasketDetails(string? email)
        {
            if (email == null || _context.Baskets == null)
            {
                return NotFound();
            }

            var Userbaskets =  _context.Baskets
                .Include(b => b.AppUser)
                .Where(m => m.AppUser.Email == email);
            if (Userbaskets == null)
            {
                return NotFound();
            }

            return View(await Userbaskets.ToListAsync());
        }
        //GET: Admin/Baskets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Baskets == null)
            {
                return NotFound();
            }

            var basket = await _context.Baskets
                .Include(b => b.AppUser)
                .FirstOrDefaultAsync(m => m.BasketId == id);
            if (basket == null)
            {
                return NotFound();
            }
            else if (basket.Stage>0)
            {
                return NotFound();
            }

            return View(basket);
        }

        // POST: Admin/Baskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Baskets == null)
            {
                return Problem("Entity set 'BurgerDbContext.Baskets'  is null.");
            }
            var basket = await _context.Baskets.FindAsync(id);
            if (basket != null&&basket.Stage==BasketStage.Active)
            {
                Basket newbasket = new() { AppUserId=basket.AppUserId};
                _context.Baskets.Add(newbasket);
                _context.Baskets.Remove(basket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        /*
        // GET: Admin/Baskets/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id");
            return View();
        }

        // POST: Admin/Baskets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BasketId,AppUserId,TotalPrice,Stage")] Basket basket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(basket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", basket.AppUserId);
            return View(basket);
        }

        // GET: Admin/Baskets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Baskets == null)
            {
                return NotFound();
            }

            var basket = await _context.Baskets.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", basket.AppUserId);
            return View(basket);
        }

        // POST: Admin/Baskets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BasketId,AppUserId,TotalPrice,Stage")] Basket basket)
        {
            if (id != basket.BasketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasketExists(basket.BasketId))
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
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", basket.AppUserId);
            return View(basket);
        }

        
        private bool BasketExists(int id)
        {
          return (_context.Baskets?.Any(e => e.BasketId == id)).GetValueOrDefault();
        }
        */
    }
}
