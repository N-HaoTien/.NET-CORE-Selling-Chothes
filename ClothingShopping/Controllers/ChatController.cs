using Microsoft.AspNetCore.Mvc;

namespace ClothingShopping.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
