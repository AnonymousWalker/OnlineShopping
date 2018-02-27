using OnlineShopping.Models;
using OnlineShopping.Models.DomainModel;
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

        public void UploadProduct(UploadProductModel model)
        {
            //add image to collection property of product
            var product = new Product
            {
                ProductName = model.ProductName,
                Price = model.Price,
                Description = model.Description,
                DateCreated = DateTime.Today,
                 
            };
            Db.Products.Add(product);
            var image = new ProductImage
            {
                ImageName = model.ImageName,
                Content = model.Content,
                Product = product
            };
            Db.Images.Add(image);
        }
    }
}