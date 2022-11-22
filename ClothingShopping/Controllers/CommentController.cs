using Microsoft.AspNetCore.Mvc;

namespace ClothingShopping.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
