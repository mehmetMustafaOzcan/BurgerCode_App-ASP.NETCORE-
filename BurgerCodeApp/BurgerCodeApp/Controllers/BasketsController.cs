using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerCodeApp.Models;
using BurgerCodeApp.Models.Enums;
using BurgerCodeApp.Data.Context;

namespace BurgerCodeApp.Controllers
{
    public class BasketsController : Controller
    {
        private readonly BurgerDbContext _context;

        public BasketsController(BurgerDbContext context)
        {
            _context = context;
        }
        /*
        // GET: Baskets
        public async Task<IActionResult> Index()
        {
            var burgerDbContext = _context.Baskets.Include(b => b.AppUser);
            return View(await burgerDbContext.ToListAsync());
        }
        
        // GET: Baskets/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(basket);
        }
      
        // GET: Baskets/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id");
            return View();
        }
          *//*
        // POST: Baskets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BasketId,AppUserId")] Basket basket)
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
        */
        /*
        // GET: Baskets/Edit/5
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
        */
        // POST: Baskets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderComplate(int BasketId)//sepet ödemesi
        {
            if (BasketId == 0)
            {
                return NotFound();
            }
            Basket basket=_context.Baskets.Find(BasketId);
            if (basket!=null&&basket.TotalPrice!=0)
            {
                basket.Stage = BasketStage.Completed;//sepeti tamamlandı yap
                basket.ComplateDate= DateTime.Now;
                Basket newbasket = new() { AppUserId = basket.AppUserId };//yeni sepet aç
                try
                {
                    _context.Update(basket);
                    _context.Baskets.Add(newbasket);
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
                return RedirectToAction("Basket", "BasketDetails");
            }
            return RedirectToAction("Basket","BasketDetails");
        }
        /*
        // GET: Baskets/Delete/5
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

            return View(basket);
        }
        *//*
        // POST: Baskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Baskets == null)
            {
                return Problem("Entity set 'BurgerDbContext.Baskets'  is null.");
            }
            var basket = await _context.Baskets.FindAsync(id);
            if (basket != null)
            {
                _context.Baskets.Remove(basket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */
        private bool BasketExists(int id)
        {
          return (_context.Baskets?.Any(e => e.BasketId == id)).GetValueOrDefault();
        }
    }
}
