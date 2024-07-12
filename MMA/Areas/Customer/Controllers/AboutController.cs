using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMA.Data;

namespace MMA.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext db;
        public AboutController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.InformationsWeb.OrderByDescending(i=>i.Id).FirstOrDefaultAsync());
        }
    }
}
