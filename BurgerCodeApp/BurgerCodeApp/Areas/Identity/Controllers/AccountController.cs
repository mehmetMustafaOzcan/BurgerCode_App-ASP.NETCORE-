using BurgerCodeApp.Areas.Identity.Models;
using BurgerCodeApp.Controllers;
using BurgerCodeApp.Persistence.Context;
using BurgerCodeApp.Models;
using BurgerCodeApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Mail;

namespace BurgerCodeApp.Areas.Identity.Controllers
{
    [Area("Identity")]

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly BurgerDbContext _context;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, BurgerDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }


        public IActionResult Register()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserVm userVm)
        {


            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userVm.EmailAddress);//email unique mi kontrol
                if (user != null)
                {
                    // E-posta adresi zaten kayıtlı ise hata mesajı ekleyin
                    ModelState.AddModelError("EmailAddress", "Bu e-posta adresi zaten kayıtlı.");
                    return View();
                }
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

                   // await _userManager.AddToRoleAsync(appUser ,"ADMIN");---Admin eklerken açılabilir
                    /*
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

                    var emailDogrulamaLinki = Url.Action(nameof(ConfirmEmail), "User", new { token, email = appUser.Email });


                    var emailDogrulamaMesaji = new MailMessage(new Dictionary<string, string>() { { appUser.UserName!, appUser.Email! } }, "Email Doğrulama Linki", $"<b>Uygulamamıza giriş yapabilmeniz için doğrulama linki:</b> {emailDogrulamaLinki!}");
                    _emailService.SendEmail(emailDogrulamaMesaji);
                    */

                    // await _userManager.AddToRoleAsync(appUser, "ADMIN");
                    basket.AppUserId = _userManager.Users.SingleOrDefault(x => x.UserName == userVm.UserName).Id;
                    _context.Baskets.Add(basket);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }




            }
            return View();
        }
        public IActionResult Login()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserVm loginVm)
        {

            //if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginVm.EmailAddress);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, loginVm.Password, false, lockoutOnFailure: false);


                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Reports", new { area = "Admin" });
                        }
                        return RedirectToAction("Index", "home", new { area = "" });
                    }
                    else
                    {
                        ModelState.AddModelError("LoginError", "Invalid login attempt");

                    }
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Invalid login attempt");

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


