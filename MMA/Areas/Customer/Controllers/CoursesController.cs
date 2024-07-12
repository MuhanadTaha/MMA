using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMA.Data;
using MMA.Utility;

namespace MMA.Areas.Customer.Controllers
{

    [Area("Customer")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext db;
        public CoursesController(ApplicationDbContext db)
        {
            this.db = db;   
        }
        public async Task <IActionResult> Index()
        {
          
            var res = await db.Courses.ToListAsync();
            
            return View(res);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
    }
}
