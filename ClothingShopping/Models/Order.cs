using System.ComponentModel.DataAnnotations;

namespace ClothingShopping.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime ?ReceivedDate { get; set; }
        public string ?Notes { get; set; }
        [StringLength(1)]
        public char ?Status { get; set; }
        public string ?StatusName { get; set; }
        public bool IsPayBill { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Required]
        public string UserId { get; set; } // người đặt hàng
        public virtual ApplicationUser ?CheckerUser { get; set; }
        public string ?CheckerUserId { get; set; } // người duyệt đơn hàng
        public string ?RejectContent { get; set; } // nguyên nhân hủy (gửi Mail)
        public decimal Total { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
