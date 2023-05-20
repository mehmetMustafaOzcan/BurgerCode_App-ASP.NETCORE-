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
       
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderComplate(int BasketId)//sepet ödemesi sepetdetalden post ediliyor
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
        
        private bool BasketExists(int id)
        {
          return (_context.Baskets?.Any(e => e.BasketId == id)).GetValueOrDefault();
        }
    }
}
