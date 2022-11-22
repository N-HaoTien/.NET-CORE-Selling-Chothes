using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
namespace ClothingShopping.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        [StringLength(30)]
        public string Name { get; set; }
        
        [StringLength(30)]
        public string ?Address { get; set; }
        
        [StringLength(70)]
        public string ?Imgage { get; set; }
         
        public DateTime? BirthDay { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Chatting> MessagesFrom { get; set; }
        public virtual List<Chatting> MessagesTo { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<Order> CheckerOrders { get; set; } // Danh sách duyệt đơn hàng của người đó

    }
}
