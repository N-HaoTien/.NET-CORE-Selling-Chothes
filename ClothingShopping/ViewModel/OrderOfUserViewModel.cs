using ClothingShopping.Models;

namespace ClothingShopping.ViewModel
{
    public class OrderOfUserViewModel
    {
        public string UserId { get; set; }  

        public string Name {get; set;}
        public string Address {get; set;}
        public string PhoneNumber { get; set; }
        public string Email {get; set; }
        public List<Order> Orders { get; set;}
    }
}
