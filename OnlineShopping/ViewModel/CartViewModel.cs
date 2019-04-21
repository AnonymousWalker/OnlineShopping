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
            Products = new List<CartProductViewModel>();
        }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double TotalPrice => Products.Sum(x => x.Amount);
        public IList<CartProductViewModel> Products { get; set; }
    }
}