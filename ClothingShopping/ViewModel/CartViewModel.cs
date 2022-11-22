using ClothingShopping.Models;

namespace ClothingShopping.ViewModel
{
    public class CartViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public string urlPicture { get; set; }

        public int? SizeId { get; set; }

        public double ThanhTien => Price * Quantity;

    }
}
