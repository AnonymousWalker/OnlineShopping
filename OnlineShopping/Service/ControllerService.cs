using OnlineShopping.Models;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.Service
{
    public class ControllerService
    {
        private OnlineShoppingDbContext Db;
        public ControllerService(OnlineShoppingDbContext db)
        {
            Db = db;
        }

        public List<ProductViewModel> GetProducts()
        {
            var products = Db.Products.Select(x => new ProductViewModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Price = x.Price,
                DateCreated = x.DateCreated,
                Description = x.Description
            }).ToList();
            return products;
        }

        public ProductViewModel GetProductInfo(int id)
        {
            var product = Db.Products.Find(id);
            return new ProductViewModel
            {
                ProductName = product.ProductName,
                ProductId = product.ProductId,
                Price = product.Price,
                Description = product.Description
            };
        }
    }
}