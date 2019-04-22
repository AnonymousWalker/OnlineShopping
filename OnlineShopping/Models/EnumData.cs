using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models
{
    public enum ImageType
    {
        Avatar = 1,
    }

    public enum ProductCategoryEnum 
    {
        Software = 1,
        USB,
        HDD,
        SSD,
        RAM,
        CPU,
        GraphicCard,
        Laptop,
        Mouse,
        Keyboard,
        Monitor,
        Desktop
    }

    public enum ResultCode
    {
        Success = 0,
        Error = 1,
        Warning = 2
    }
}