using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerCodeApp.Data;
using BurgerCodeApp.Models;
using System.Security.Claims;
using BurgerCodeApp.Models.Enums;

namespace BurgerCodeApp.Controllers
{
    public class BasketDetailsController : Controller
    {
        private readonly BurgerDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BasketDetailsController(BurgerDbContext context)
        {
            _context = context;
        }

        // GET: BasketDetails
        public async Task<IActionResult> Index()
        {
            var burgerDbContext = _context.BasketDetails.Include(b => b.Basket).Include(b => b.Menu);
            return View(await burgerDbContext.ToListAsync());
        }

        // GET: BasketDetails/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.BasketDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    var basketDetail = await _context.BasketDetails
        //        .Include(b => b.Basket)
        //        .Include(b => b.Menu)
        //        .FirstOrDefaultAsync(m => m.BasketId == id);
        //    if (basketDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(basketDetail);
        //}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }
         
            ViewBag.Extras = _context.Extras;
            var Menü = await _context.Menus
                .Include(b => b.MenuDetails)
                .Include(b => b.BasketDetails)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (Menü == null)
            {
                return NotFound();
            }
            BasketVm vm = new BasketVm() { MenuName=Menü.MenuName,MenuId=Menü.MenuId};
           
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Details(BasketVm vm)
        {
            //menüıd alınacak
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim.Value;
            
            if (vm == null)
            {
                return NotFound();
            }
            //TODO BasketId set edilecek kullanıcının girişte aldığı basket id ve basket false edilecek.
            Basket basket = _context.Baskets.Where(x => x.AppUserId == userId&& x.Stage==BasketStage.Active).FirstOrDefault();
            BasketDetail basketDetail = new() { BasketId=basket.BasketId, MenuId = vm.MenuId, MenuSize = vm.Size, Quantity = vm.Quantity };
            List<ExtraDetail> extraDetails = new();
            foreach (var item in vm.Extras)
            {
                Extra extra = _context.Extras.Find(item);
                ExtraDetail extraDetail = new() { ExtraId = extra.ExtraId, BasketId = basket.BasketId, Quantity = "1" };
                basket.ExtraDetails.Add(extraDetail);
            }
            
           // ExtraDetail extraDetail = new() { BasketId = basket.BasketId,Quantity=1,ExtraId=vm.}
            _context.BasketDetails.Add(basketDetail);
            _context.SaveChanges();
            return RedirectToAction("Index", "Menus");
        }


        // GET: BasketDetails/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BasketId"] = new SelectList( _context.Baskets, "BasketId", "BasketId");
            ViewData["Extra"] = await _context.Extras.ToListAsync();
            ViewData["MenuId"] = new SelectList(_context.Menus, "MenuId", "MenuId");
            return View();
        }

        // POST: BasketDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BasketId,MenuId,Quantity,MenuSize")] BasketDetail basketDetail,List<string> selectedExtras)
        {
            if (ModelState.IsValid)
            {
                _context.Add(basketDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BasketId"] = new SelectList(_context.Baskets, "BasketId", "BasketId", basketDetail.BasketId);
            ViewData["MenuId"] = new SelectList(_context.Menus, "MenuId", "MenuId", basketDetail.MenuId);
            return View(basketDetail);
        }

        // GET: BasketDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BasketDetails == null)
            {
                return NotFound();
            }

            var basketDetail = await _context.BasketDetails.FindAsync(id);
            if (basketDetail == null)
            {
                return NotFound();
            }
            ViewData["BasketId"] = new SelectList(_context.Baskets, "BasketId", "BasketId", basketDetail.BasketId);
            ViewData["MenuId"] = new SelectList(_context.Menus, "MenuId", "MenuId", basketDetail.MenuId);
            return View(basketDetail);
        }

        // POST: BasketDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BasketId,MenuId,Quantity,MenuSize")] BasketDetail basketDetail)
        {
            if (id != basketDetail.BasketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basketDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasketDetailExists(basketDetail.BasketId))
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
            ViewData["BasketId"] = new SelectList(_context.Baskets, "BasketId", "BasketId", basketDetail.BasketId);
            ViewData["MenuId"] = new SelectList(_context.Menus, "MenuId", "MenuId", basketDetail.MenuId);
            return View(basketDetail);
        }

        // GET: BasketDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BasketDetails == null)
            {
                return NotFound();
            }

            var basketDetail = await _context.BasketDetails
                .Include(b => b.Basket)
                .Include(b => b.Menu)
                .FirstOrDefaultAsync(m => m.BasketId == id);
            if (basketDetail == null)
            {
                return NotFound();
            }

            return View(basketDetail);
        }

        // POST: BasketDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BasketDetails == null)
            {
                return Problem("Entity set 'BurgerDbContext.BasketDetails'  is null.");
            }
            var basketDetail = await _context.BasketDetails.FindAsync(id);
            if (basketDetail != null)
            {
                _context.BasketDetails.Remove(basketDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasketDetailExists(int id)
        {
          return (_context.BasketDetails?.Any(e => e.BasketId == id)).GetValueOrDefault();
        }
    }
}
