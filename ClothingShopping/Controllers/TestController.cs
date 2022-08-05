using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothingShopping.Controllers
{
    public class TestController : Controller
    {
        public readonly ApplicationDbContext db;
        public TestController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Image,Id,Name,Description,Status")] Category category)
        {
            bool check = ModelState.IsValid;
            category.Status = true;
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

    }
}
