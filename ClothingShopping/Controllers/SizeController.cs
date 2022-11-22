using ClothingShopping.Areas.Identity.Data;
using ClothingShopping.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothingShopping.Controllers
{
    public class SizeController : Controller
    {
        //public static List<string>
        public IActionResult Index()
        {
            return View();
        }
    }
}
