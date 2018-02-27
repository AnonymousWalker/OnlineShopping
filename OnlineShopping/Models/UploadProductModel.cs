using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models
{
    public class UploadProductModel
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Price { get; set; }
        public string Description { get; set; }
        [Required]
        public string ImageName { get; set; }
        public byte[] Content { get; set; }
        public ImageType Type { get; set; } = ImageType.ProductAvatar;
    }
}