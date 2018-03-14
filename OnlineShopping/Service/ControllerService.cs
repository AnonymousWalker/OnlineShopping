using OnlineShopping.Models;
using OnlineShopping.Models.DomainModel;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OnlineShopping.Service
{
    public class ControllerService
    {
        private OnlineShoppingDbContext Db;
        public ControllerService(OnlineShoppingDbContext db)
        {
            Db = db;
        }

        public IList<ProductViewModel> GetProducts()
        {
            var products = Db.Products.Include(x => x.Images).Select(x => new ProductViewModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Price = x.Price,
                SalePrice = x.SalePrice,
                DateCreated = x.DateCreated,
                Description = x.Description,
                Image = x.Images.FirstOrDefault()
            }).ToList();
            foreach (var item in products)
            {
                item.ImageSource = MapImageModel(item.Image);
            }
            return products;
        }

        public IList<ProductViewModel> GetProductsByCategory(int type)
        {   // should we map the entity to model before ToList or after?
            var listproducts = Db.Products.Where(p => p.Category == (ProductCategoryEnum)type).Include(p => p.Images)
                .Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    DateCreated = p.DateCreated,
                    Description = p.Description,
                    Image = p.Images.FirstOrDefault()
                }).ToList();
            return listproducts;
        }

        public ProductViewModel GetProductInfo(int id)
        {
            var product = Db.Products.Find(id);
            var image = product.Images.FirstOrDefault();
            string imagesrc = "";
            if (image != null)
            {
                var base64img = Convert.ToBase64String(image.Content);
                imagesrc = string.Format("data:image/jpg;base64,{0}", base64img);
            }
            return new ProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Category = product.Category,
                Price = product.Price,
                SalePrice = product.SalePrice,
                Description = product.Description,
                DateCreated = product.DateCreated,
                ImageSource = imagesrc
            };
        }

        public void UploadProduct(UploadProductModel model)
        {
            var product = new Product
            {
                ProductName = model.ProductName,
                Price = Double.Parse(model.Price),
                Description = model.Description,
                DateCreated = DateTime.Today,
            };
            product.Images = new List<ProductImage>();

            var image = new ProductImage
            {
                ImageName = model.ImageName,
                Content = model.Content,
                Type = model.Type
            };
            product.Images.Add(image);
            Db.Products.Add(product);
            Db.SaveChanges();
        }

        private string MapImageModel(ProductImage image)
        {
            string imagesrc = "";
            if (image != null)
            {
                var base64img = Convert.ToBase64String(image.Content);
                imagesrc = string.Format("data:image/jpg;base64,{0}", base64img);
            }
            return imagesrc;
        }
    }
}