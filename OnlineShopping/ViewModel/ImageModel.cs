using OnlineShopping.Models;
using System;
using System.Collections.Generic;


namespace OnlineShopping.ViewModel
{
    public class ImageModel
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImageSource { get; set; }
        public ImageType Type { get; set; } = ImageType.Avatar;
    }
}