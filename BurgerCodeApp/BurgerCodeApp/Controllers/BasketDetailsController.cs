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
using Microsoft.AspNetCore.Identity;
using NuGet.Versioning;
using BurgerCodeApp.Models.ViewModels;
using NuGet.ContentModel;

namespace BurgerCodeApp.Controllers
{
    public class BasketDetailsController : Controller
    {
        private readonly BurgerDbContext _context;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<AppUser> _signinManager;
        public BasketDetailsController(BurgerDbContext context, SignInManager<AppUser> signinManager)
        {
            _context = context;
            _signinManager = signinManager;
        }

        // GET: BasketDetails
        public async Task<IActionResult> Basket()
        {
            Basket basket = GetUserActiveBasket();
            decimal totalprice = 0;
            foreach (var item in basket.BasketDetails)
            {
                totalprice += (decimal)(item.Quantity * item.Menu.MenüPrice * ((item.MenuSize > 1 ? 1 + (decimal)item.MenuSize / 10 : 1)));
                foreach (var extra in item.ExtraDetails)
                {
                    totalprice += (decimal)extra.Extra.Price* item.Quantity;
                }

            }
          
            basket.TotalPrice=totalprice;
            _context.SaveChanges();

            return View(basket);
        }

        // GET: BasketDetails/Details/5
      
        public async Task<IActionResult> Details(int? id)//sepete eklenecek ürün gösterme
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
            BasketVm vm = new BasketVm() { MenuName = Menü.MenuName, MenuId = Menü.MenuId ,PicturePath=Menü.Photopath,MenuPrice=(decimal)Menü.MenüPrice};

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Details(BasketVm vm)//sepete ürün ekleme
        {

            if (vm == null)
            {
                return NotFound();
            }
            //TODO BasketId set edilecek kullanıcının girişte aldığı basket id ve basket false edilecek.
            if (_signinManager.IsSignedIn(User))
            {
                Basket basket = GetUserActiveBasket();
                BasketDetail basketDetail = new() { BasketId = basket.BasketId, MenuId = vm.MenuId, MenuSize = vm.Size, Quantity = vm.Quantity, };
                foreach (var item in vm.Extras)
                {
                    Extra extra = _context.Extras.Find(item);
                    ExtraDetail extraDetail = new() { ExtraId = extra.ExtraId, Quantity = 1 };
                    basketDetail.ExtraDetails.Add(extraDetail);
                }

                // ExtraDetail extraDetail = new() { BasketId = basket.BasketId,Quantity=1,ExtraId=vm.}
                _context.BasketDetails.Add(basketDetail);
                _context.SaveChanges();
                return RedirectToAction("Index", "Menus");
            }
            else
            {
                return RedirectToAction( "Login", "Account",new  {area="Identity"}) ;
            }
        }

        private Basket GetUserActiveBasket()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim.Value;
            Basket basket = _context.Baskets.Where(x => x.AppUserId == userId && x.Stage == BasketStage.Active)
                .Include(x=>x.BasketDetails)
                .ThenInclude(x => x.ExtraDetails).ThenInclude(x=>x.Extra)
                .Include(x=>x.BasketDetails).
                ThenInclude(x=>x.Menu)
                .FirstOrDefault();
            return basket;
        }


        // GET: BasketDetails/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BasketId"] = new SelectList(_context.Baskets, "BasketId", "BasketId");
            ViewData["Extra"] = await _context.Extras.ToListAsync();
            ViewData["MenuId"] = new SelectList(_context.Menus, "MenuId", "MenuId");
            return View();
        }

        // POST: BasketDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BasketId,MenuId,Quantity,MenuSize")] BasketDetail basketDetail, List<string> selectedExtras)
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
            Basket basket = GetUserActiveBasket();
            var basketDetail = basket.BasketDetails.Where(x => x.BasketDetailId == id).FirstOrDefault();
            if (basketDetail == null)
            {
                return NotFound();
            }

            decimal totalprice = (decimal)(basketDetail.Menu.MenüPrice * ((basketDetail.MenuSize > 1 ? 1 + (decimal)basketDetail.MenuSize / 10 : 1)));

            foreach (var extra in basketDetail.ExtraDetails)
            {
                totalprice += (decimal)extra.Extra.Price;
            }
            totalprice *= basketDetail.Quantity;
            BasketEditVm basketEdit = new() { MenuName = basketDetail.Menu.MenuName, Size = basketDetail.MenuSize, Photopath = basketDetail.Menu.Photopath, Quantity = basketDetail.Quantity,MenüPrice=(decimal)basketDetail.Menu.MenüPrice,TotalPrice= totalprice };
            ViewBag.Extras = _context.Extras.ToList();
            basketEdit.Extras = basketDetail.ExtraDetails.Select(x => x.ExtraId).ToList();
            return View(basketEdit);
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
                .FirstOrDefaultAsync(m => m.BasketDetailId == id);
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
            return RedirectToAction(nameof(Basket));
        }

        private bool BasketDetailExists(int id)
        {
            return (_context.BasketDetails?.Any(e => e.BasketId == id)).GetValueOrDefault();
        }
    }
}
