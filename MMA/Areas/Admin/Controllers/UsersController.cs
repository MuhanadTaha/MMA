using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMA.Data;
using MMA.Utility;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace MMA.Areas.Admin.Controllers
{
    [Authorize(Roles =SD.ManagerUser)]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext db;
        public UsersController(ApplicationDbContext db)
        {
            this.db = db; 
        }

        public async Task <IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string UserId= claim.Value;

            return View(await db.ApplicationUsers.Where(m=>m.Id != UserId).ToListAsync());
        }


        public async Task<IActionResult> LockUnLock(string? id)
        {

            var user = await db.ApplicationUsers.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }


            if (user.LockoutEnd == null || user.LockoutEnd < DateTime.Now) // هان يعني اليوزر اللي أنا اخترته رح أعمله لوك بحيث إني رح أضيف عليه ألف سنة لقدام يضل مسكّر فيها
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            else // هان معناها اليوزر اللي اخترته بدي أشيل عنه اللوك فيقله اللوك موجود لثانية وبعدها رح ينشال
            {
                user.LockoutEnd = DateTime.Now;
            }
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
