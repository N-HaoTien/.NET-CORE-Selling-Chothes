using System.ComponentModel.DataAnnotations;

namespace ClothingShopping.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public virtual List<ProductPicture> ProductPictures { get; set; }

    }
}
