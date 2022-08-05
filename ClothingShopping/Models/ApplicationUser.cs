using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
namespace ClothingShopping.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Address { get; set; }
        [Required]
        [StringLength(70)]
        public string Imgage { get; set; }

        public DateTime? BirthDay { get; set; }

        public virtual List<Comment> Comments { get; set; }


        public virtual List<Order> Orders { get; set; }
    }
}
