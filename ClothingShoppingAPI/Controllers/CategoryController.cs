using Microsoft.AspNetCore.Mvc;
using ClothingShopping.Models;
namespace ClothingShoppingAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public static List<Category> list = new List<Category>()
        {
             new Category{Name = "Cafe",Description = "Cafe",Image ="../assets/cafe phin.jpg"},
             new Category{Name = "Water",Description = "Water",Image ="../assets/drink-1.jpg"},
             new Category{Name = "Pizza",Description = "Pizza",Image ="../assets/image_3.jpg"},

        };

        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(getAll());
        }
        [NonAction]
        public List<Category> getAll()
        {

            return list;
        }
        [HttpPost]

        public IActionResult AddCategory(Category category)
        {
            list.Add(category);
            return Ok(category);
        }
    }
}
