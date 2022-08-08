using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace ClothingShopping.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public int Quantity { get; set; }
        [Required]
        [Range(0,int.MaxValue,ErrorMessage = "Giá Trị Phải Lớn Hơn 0")]
        public int Price { get; set; }

        public virtual Category Category { get; set; }
        [Required]

        public int CategoryId { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }

        public virtual List<ProductPicture> ProductPictures { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<ProductSize> ProductSizes { get; set; }


    }
}
