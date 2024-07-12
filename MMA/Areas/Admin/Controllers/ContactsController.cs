using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMA.Data;
using MMA.Utility;
using System.Data;

namespace MMA.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext db;
        public ContactsController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task <IActionResult> Index()
        {
            return View(await db.Contacts.ToListAsync());
        }



      
    }
}
