using BurgerCodeApp.Areas.Identity.Models;
using BurgerCodeApp.Data;
using BurgerCodeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BurgerCodeApp.Areas.Identity.Controllers
{
    [Area("Identity")]

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly BurgerDbContext _context;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,BurgerDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

      
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserVm userVm)
        {
            //if (ModelState.IsValid)
            {
                //basket aktifliği kontrolu eklenecek
                AppUser appUser = new AppUser
                {
                    UserName = userVm.UserName,
                    
                    Email = userVm.EmailAddress,
                    
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, userVm.Password);
                
                if (result.Succeeded)
                {
                Basket basket = new();
                   // await _userManager.AddToRoleAsync(appUser, "ADMIN");
                   basket.AppUserId = _userManager.Users.SingleOrDefault(x=>x.UserName == userVm.UserName).Id;
                _context.Baskets.Add(basket);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserVm loginVm)
        {

            //if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginVm.EmailAddress);

                var result = await _signInManager.PasswordSignInAsync(user.UserName, loginVm.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "home", new { area = "" });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz oturum açma denemesi");
                }
            }


            return View(loginVm);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "home", new { area = "" });
        }

    }
}


