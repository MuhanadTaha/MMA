using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MMA.Data;
using MMA.Models;
using MMA.Utility;
using System.Data;

namespace MMA.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty] //لما اعمل سابميت هاي رح تستقبل الداتا اللي رح تيجي من الفورم
        public Course course { get; set; }
        public CoursesController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;

            course = new Course();
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Create()
        {

            return View(course);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost()
        {
            //if (!ModelState.IsValid)
            //{
            //    var errors = ModelState.Values.SelectMany(v => v.Errors);
            //    // Log or inspect errors
            //    return View(course);
            //}

            if (ModelState.IsValid)
            {
                string ImagePath = @"\images\course_image.png"; // الديفولت باث للايميج في حال ما كان في صورة محملة 
                var files = HttpContext.Request.Form.Files; //كل الفايلات اللي حملتها من التشوز فايل
                if (files.Count > 0) // في حال كان في صورة محملة
                {
                    string webRootPath = webHostEnvironment.WebRootPath; // هيك رح يجيبلي روت الدابيلو دابيلو دابيلو روت فولدار

                    string ImageName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName); //بعطي اسم للصورة يكون يونيك
                    FileStream fileStream = new FileStream(Path.Combine(webRootPath, "images", ImageName), FileMode.Create);//فايل مود كرييت بتنعمل لانشاء صورة داخل هذا الباث //هيك رح أدمج الباث كامل الدابليو روت مع الايميج فولدر مع اسم الصورة
                    files[0].CopyTo(fileStream);//حعمل نسخة جديدة بالاسم اليونيك

                    ImagePath = @"\images\" + ImageName; // الباث اللي رح أخزنه بالداتا بيس
                }
               course.Image = ImagePath; //لما أعمل سابميت رح  أعطي البروبيرتي اللي اسمها ايميج قيمة اليميج باث 

                db.Courses.Add(course);
                await db.SaveChangesAsync();

                return RedirectToAction("Index", "Courses", new { area = "Customer" });
            }
            return View(course);


        }
    }
}
