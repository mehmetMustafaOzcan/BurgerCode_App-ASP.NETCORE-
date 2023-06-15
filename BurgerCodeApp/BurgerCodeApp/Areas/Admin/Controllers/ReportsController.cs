using BurgerCodeApp.Areas.Admin.Models;
using BurgerCodeApp.Persistence.Context;
using BurgerCodeApp.Domain.Entities.Enums;
using BurgerCodeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Composition;

namespace BurgerCodeApp.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ReportsController : Controller
    {
        BurgerDbContext _context;
        public ReportsController(BurgerDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ReportVm report = GetAllReport();

            return View(report);

           
        }
        public ReportVm GetAllReport()
        {
            ReportVm report = new();
            var Baskets = _context.Baskets.Include(x => x.BasketDetails).ThenInclude(x => x.Menu).Include(x => x.BasketDetails).ThenInclude(x => x.ExtraDetails).ThenInclude(x => x.Extra);
            report.TotalEarning = Baskets
                .Where(x => x.Stage == BasketStage.Completed)
                .Sum(x => x.TotalPrice);

            report.ExtraEarning = (decimal)_context.ExtraDetails
                .Include(e => e.Extra)
                .Include(bd => bd.BasketDetail)
                .Sum(e => (e.Extra.Price * e.BasketDetail.Quantity));

            report.MenuEarning = report.TotalEarning - report.ExtraEarning;
            var Menus = _context.BasketDetails.Include(x => x.Menu)
                .GroupBy(x => x.MenuId)
                .Select(m => new
                {
                    TotalQuantity = m.Sum(s => s.Quantity),
                    MenuName = m.Select(mn => mn.Menu.Name)
                .FirstOrDefault()
                })
                .OrderByDescending(a => a.TotalQuantity);

            report.FirstMenu = Menus.FirstOrDefault().MenuName;
            report.FirstMenuQuantity = Menus.FirstOrDefault().TotalQuantity;
            return report;
        }
        public IActionResult SalesReports(string report)
        {
            if (report == "extras")
            {
                var extras = _context.ExtraDetails.
                    Include(e => e.Extra)
                    .Include(bd => bd.BasketDetail)
                    .GroupBy(g => g.ExtraId)
                    .Select(ex =>
                    new
                    {
                        Name = ex.Select(x => x.Extra.Name).FirstOrDefault(),
                        Total = ex.Sum(e => (e.Extra.Price * e.BasketDetail.Quantity))
                    }).ToList();
                ViewBag.totals = extras.Sum(x => x.Total);
                return View(Json(extras));
            }
            else if (report == "menus")
            {
                var Menus = _context.BasketDetails.
                    Include(e => e.Menu)
                    .GroupBy(g => g.MenuId)
                    .Select(md =>
                    new
                    {
                        Name = md.Select(x => x.Menu.Name).FirstOrDefault(),
                        UnitPrice = md.Select(c => c.Menu.Price).FirstOrDefault(),
                        Total = md.Sum(m => (m.Menu.Price * m.Quantity))
                    }).ToList();

                ViewBag.totals = Menus.Sum(x => x.Total);
                return View(Json(Menus));
            }
            else if (report == "totals")
            {
                var TotalSales = _context.Baskets.
                    Include(a => a.AppUser).GroupBy(e => e.AppUser.Email)
                    .Select(s => new { EMail = s.Key, Total = s.Sum(t => t.TotalPrice) }).ToList();
                ViewBag.totals = TotalSales.Sum(x => x.Total);

                return View(Json(TotalSales));
            }
            return NotFound();
        }

    }
}

