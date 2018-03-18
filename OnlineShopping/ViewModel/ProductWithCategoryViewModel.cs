using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.ViewModel
{
    public class ProductWithCategoryViewModel
    {
        public ProductWithCategoryViewModel()
        {
            Products = new List<ProductViewModel>();
        }
        public bool IsCategorized { get; set; } = true;
        public string Query { get; set; } = "";
        public ProductCategoryEnum Category { get; set; }
        public ICollection<ProductViewModel> Products { get; set; }
    }
}