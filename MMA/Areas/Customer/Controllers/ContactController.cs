using Microsoft.AspNetCore.Mvc;
using MMA.Data;
using MMA.Models;

namespace MMA.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext db;

        public ContactController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(ContactUs contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }
            return View();
        }
    }
}

