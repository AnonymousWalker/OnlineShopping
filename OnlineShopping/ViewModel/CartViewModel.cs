using System;
using System.Collections.Generic;
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
        public double TotalPrice => Products.Sum(x => x.Price);
        public IList<ProductViewModel> Products { get; set; }

    }
}