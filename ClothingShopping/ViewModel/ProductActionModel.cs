using ClothingShopping.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingShopping.ViewModel
{
    public class ProductActionModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlPictureBg { get; set; }
        public IFormFile UrlPic { get; set; }

        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        [Display(Name = "Price")]
        [Range(50000,100000000)]
        public double NewPrice { get; set; }
        public double? OldPrice { get; set; }
        public virtual Category? Category { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string ImageCategory { get; set; }
        [NotMapped]
        public string? NameWithIcon
        {
            get
            {
                return this.ImageCategory + "\t\t" + this.CategoryName;
            }
        }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string SizeIDs { get; set; }

        public IEnumerable<SelectListItem> ?CategoryList { get; set; }
        public List<Picture> ?productPictures { get; set; }
        public List<IFormFile>? PicturesFile { get; set; }
        public List<ProductSize> ProductSize { get; set; }

        public List<Size> Sizes { get; set; }


    }
}
