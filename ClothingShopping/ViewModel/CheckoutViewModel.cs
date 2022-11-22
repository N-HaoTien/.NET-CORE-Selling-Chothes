using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace ClothingShopping.ViewModel
{
    public class CheckoutViewModel
    {
        [Required]
        public List<CartViewModel> Cart  { get;set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsCheckedPayment { get; set; }
        [Required]
        public string Address { get; set; }
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        [Required]
        [Phone(ErrorMessage = "Please enter a valid Phone No")]
        public string Phone { get; set; }
        public string Notes { get; set; }
    }
}
