using System.ComponentModel.DataAnnotations;

namespace ClothingShopping.ViewModel
{
    public class UserViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ?Name { get; set; }

        [Required]
        public string ?Address { get; set; }
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        [Required]
        [Phone(ErrorMessage = "Please enter a valid Phone No")]
        public string PhoneNumber { get; set; }
        [Required]

        public string Email { get;set; }

        public string? Imgage { get; set; }

        public DateTime? BirthDay { get; set; }
    }
}
