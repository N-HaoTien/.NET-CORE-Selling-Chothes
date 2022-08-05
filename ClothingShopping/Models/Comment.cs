using System.ComponentModel.DataAnnotations;

namespace ClothingShopping.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; } 
        public DateTime CreatedDate { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
