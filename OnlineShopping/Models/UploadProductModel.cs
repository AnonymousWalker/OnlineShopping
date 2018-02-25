using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models
{
    public class UploadProductModel
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public byte[] Content { get; set; }
        public ImageType Type { get; set; } = ImageType.ProductAvatar;
    }
}