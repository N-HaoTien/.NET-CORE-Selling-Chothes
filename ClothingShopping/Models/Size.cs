using System.ComponentModel.DataAnnotations;

namespace ClothingShopping.Models
{
    public class Size
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Category Category { get; set; }
        [Required]
        public int CategoryId { get; set; }


        public virtual List<ProductSize> ProductSizes { get; set; }
    }
}
