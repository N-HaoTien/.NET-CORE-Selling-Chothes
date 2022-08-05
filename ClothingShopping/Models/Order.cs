using System.ComponentModel.DataAnnotations;

namespace ClothingShopping.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime receiveddate { get; set; }

        public string Status { get; set; }

        public bool IsPayBill { get; set; }

        public virtual ApplicationUser User { get; set; }
        [Required]
        public string UserId { get; set; }
        public decimal Total { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
