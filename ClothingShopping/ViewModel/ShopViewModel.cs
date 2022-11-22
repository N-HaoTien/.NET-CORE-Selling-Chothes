using ClothingShopping.Models;

namespace ClothingShopping.ViewModel
{
    public class ShopViewModel
    {
        public List<Product> products { get; set; }

        public List<Category> ?categories { get; set; }

        public List<Size> ?sizes { get; set; }

        public int FromPrice { get; set; }
        public int ToPrice { get; set; }
    }
}
