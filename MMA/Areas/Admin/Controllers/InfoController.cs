using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMA.Data;
using MMA.Models;

namespace MMA.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InfoController : Controller
    {
        private readonly ApplicationDbContext db;
        public InfoController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InformationWeb informationWeb)
        {
            if(ModelState.IsValid) 
            {
                db.InformationsWeb.Add(informationWeb);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            return View();
        }


        public async Task<IActionResult> Edit()
        {
            return View(await db.InformationsWeb.OrderByDescending(i => i.Id).FirstOrDefaultAsync());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InformationWeb informationWeb)
        {
            if (ModelState.IsValid)
            {
                db.InformationsWeb.Update(informationWeb);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "About", new { area = "Customer" });
            }
            return View();
        }

    }
}
