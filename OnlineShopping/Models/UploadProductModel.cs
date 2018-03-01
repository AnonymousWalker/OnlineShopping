using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models
{
    public class UploadProductModel
    {
        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Range(1,short.MaxValue,ErrorMessage ="Category is not selected or unavailable")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public string Price { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string ImageName { get; set; }

        public byte[] Content { get; set; }

        public ImageType Type { get; set; } = ImageType.Avatar;
    }
}