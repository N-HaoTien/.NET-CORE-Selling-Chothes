using System.ComponentModel.DataAnnotations;

namespace ClothingShopping.Models
{
    public class Picture
    {
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }

        /*public virtual List<ProductPicture> ProductPictures { get; set; }*/
        [Required]
        public int ProductId { get; set; }

        public Product Products { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreatedDate { get; set; }

        public int SortPicture { get; set; }


    }
}
