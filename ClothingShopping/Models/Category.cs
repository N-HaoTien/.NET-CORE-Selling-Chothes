﻿ using ClothingShopping.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingShopping.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public bool Status { get; set; }
        [StringLength(40)]
        public string? Image { get; set; }
        [NotMapped]
        public string? NameWithIcon
        {
            get
            {
                return this.Image + "\t\t" + this.Name;
            }
        }
        public virtual List<Product>? Products { get; set; }

        public virtual List<Size>? Sizes { get; set; }
        
    }
}
