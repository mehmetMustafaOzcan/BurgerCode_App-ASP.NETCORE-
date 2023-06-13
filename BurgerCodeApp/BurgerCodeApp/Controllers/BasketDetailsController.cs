using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerCodeApp.Models;
using System.Security.Claims;
using BurgerCodeApp.Models.Enums;
using Microsoft.AspNetCore.Identity;
using NuGet.Versioning;
using BurgerCodeApp.Models.ViewModels;
using NuGet.ContentModel;
using Microsoft.AspNetCore.Authorization;
using BurgerCodeApp.Data.Context;

namespace BurgerCodeApp.Controllers
{
    [Authorize]
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

        // Route: /Basket
        public async Task<IActionResult> Basket()//userbasket
        {
            Basket basket = GetUserActiveBasket();
            decimal totalprice = 0;
            
            foreach (var item in basket.BasketDetails)
            {
                totalprice += (item.Quantity * item.Menu.Price * ((item.MenuSize > 1 ? 1 + (decimal)item.MenuSize / 10 : 1)));
                foreach (var extra in item.ExtraDetails)
                {
                    totalprice += extra.Extra.Price* item.Quantity;
                }

            }
          
            basket.TotalPrice=totalprice;
            _context.SaveChanges();

            return View(basket);
        }

        // Route: BasketDetails/Details/5
      
        public async Task<IActionResult> Details(int? id)//sepete eklenecek ürün gösterme
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            ViewBag.Extras = _context.Extras;
            var Menu = await _context.Menus
                .Include(b => b.MenuDetails)
                .Include(b => b.BasketDetails)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (Menu == null)
            {
                return NotFound();
            }
            BasketVm vm = new BasketVm() { MenuName = Menu.Name, MenuId = Menu.MenuId ,PicturePath=Menu.PicturePath,MenuPrice=Menu.Price,Description=Menu.Description};

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Details(BasketVm vm)//sepete ürün ekleme
        {

            if (vm == null)
            {
                return NotFound();
            }
            if (_signinManager.IsSignedIn(User))
            {
                Basket basket = GetUserActiveBasket();
                BasketDetail basketDetail = new() { BasketId = basket.BasketId, MenuId = vm.MenuId, MenuSize = vm.Size, Quantity = vm.Quantity, };
                if (vm.Extras!=null)
                {
                    foreach (var item in vm.Extras)
                    {
                        Extra extra = _context.Extras.Find(item);
                        ExtraDetail extraDetail = new() { ExtraId = extra.ExtraId, Quantity = 1 };
                        basketDetail.ExtraDetails.Add(extraDetail);
                    }

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

       
        // GET: BasketDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)/**/
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

            decimal totalprice = (decimal)(basketDetail.Menu.Price * ((basketDetail.MenuSize > 1 ? 1 + (decimal)basketDetail.MenuSize / 10 : 1)));

            foreach (var extra in basketDetail.ExtraDetails)
            {
                totalprice += (decimal)extra.Extra.Price;
            }
            totalprice *= basketDetail.Quantity;
            BasketEditVm basketEdit = new() { BasketDetailId= basketDetail.BasketDetailId, MenuName = basketDetail.Menu.Name, Size = basketDetail.MenuSize, Photopath = basketDetail.Menu.PicturePath, Quantity = basketDetail.Quantity,MenüPrice=(decimal)basketDetail.Menu.Price,TotalPrice= totalprice };
            ViewBag.Extras = _context.Extras.ToList();
            basketEdit.Extras = basketDetail.ExtraDetails.Select(x => x.ExtraId).ToList();
            return View(basketEdit);
        }

        // POST: BasketDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BasketVm basketvm)/**/
        {
            if (id == 0&& basketvm==null)
            {
                return NotFound();
            }
            if (_signinManager.IsSignedIn(User))
            {
                BasketDetail basketDetail =await _context.BasketDetails.Include(x => x.ExtraDetails).Where(x => x.BasketDetailId == id).FirstOrDefaultAsync();
                basketDetail.Quantity = basketvm.Quantity;basketDetail.MenuSize = basketvm.Size;
                basketDetail.ExtraDetails.Clear();
                if (basketvm.Extras!=null)
                {
                    foreach (var sauce in basketvm.Extras)
                    {
                        Extra extra = _context.Extras.Find(sauce);
                        ExtraDetail extraDetail = new() { ExtraId = extra.ExtraId, Quantity = 1 };
                        basketDetail.ExtraDetails.Add(extraDetail);
                    }
                }
             

                _context.BasketDetails.Update(basketDetail);
                _context.SaveChanges();
                return RedirectToAction("Basket");
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });

        }

        // GET: BasketDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)/**/
        {
            if (_context.BasketDetails == null)
            {
                return Problem("Entity set 'BurgerDbContext.BasketDetails'  is null.");
            }
            var basketDetail = await _context.BasketDetails.FindAsync(id);
            if (basketDetail != null&& GetUserActiveBasket().BasketDetails.Contains(basketDetail))//sadece kendi basketini siler
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
