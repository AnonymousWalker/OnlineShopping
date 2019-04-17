using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShopping.ViewModel
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            Products = new List<ProductViewModel>();
        }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double TotalPrice => Products.Sum(x => (x.SalePrice==0)? x.Price : x.SalePrice);
        public IList<ProductViewModel> Products { get; set; }

    }
}