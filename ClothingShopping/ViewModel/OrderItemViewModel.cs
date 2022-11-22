using ClothingShopping.Models;

namespace ClothingShopping.ViewModel
{
    public class OrderItemViewModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Order Order { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
