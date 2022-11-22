using ClothingShopping.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingShopping.Models
{
    public class Chatting
    {
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
        [Key]
        [Column(Order = 1)]
        public string FromUserId { get; set; }
        public virtual ApplicationUser FromUser { get; set; }
        
        [Key]
        [Column(Order = 2)]
        public string ToUserId { get; set; }
        public virtual ApplicationUser ToUser { get; set; }

    }
}
