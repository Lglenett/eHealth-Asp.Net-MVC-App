using eHealth.Data;
using eHealth.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eHealth.Models
{
    public class Product: IEntityBase
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Name")]
        [Required(ErrorMessage = "Product Name required")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Product Name must be between 4 and 20 characters")]
        public string Name { get; set; }

        [Display(Name = "ImageURL")]
        [Required(ErrorMessage = "Image URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Product Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Product price  is required")]
        public double Price { get; set; }


        public ProductCategory ProductCategory { get; set; }
    }
}
