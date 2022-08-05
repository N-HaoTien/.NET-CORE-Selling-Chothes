namespace ClothingShopping.Models
{
    public class ProductPicture
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int PictureId { get; set; }

        public virtual Picture Picture { get; set; }

    }
}
