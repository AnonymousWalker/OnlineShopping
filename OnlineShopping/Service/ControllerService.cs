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

        public IList<ProductViewModel> GetSaleOffProducts()
        {
            var products = Db.Products.Include(p => p.Images)
                                    .Where(p => p.SalePrice != 0)
                                    .Select(p => new ProductViewModel
                                    {
                                        ProductId = p.ProductId,
                                        ProductName = p.ProductName,
                                        Price = p.Price,
                                        SalePrice = p.SalePrice,
                                        DateCreated = p.DateCreated,
                                        Description = p.Description,
                                        Image = p.Images.FirstOrDefault()
                                    }).ToList();

            //mapping images 
            foreach (var item in products)
            {
                item.ImageSource = MapToImageModel(item.Image);
            }
            return products;
        }
        
        public IList<ProductViewModel> GetAllProducts()
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

            //mapping images 
            foreach (var item in products)
            {
                item.ImageSource = MapToImageModel(item.Image);
            }
            return products;
        }
  
        public IList<ProductViewModel> GetProductsByCategory(int type)
        {   // should we map the entity to model before ToList or after?
            var listproducts = Db.Products.Include(p => p.Images).Where(p => p.Category == (ProductCategoryEnum)type)
                .Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    SalePrice = p.SalePrice,
                    DateCreated = p.DateCreated,
                    Description = p.Description,
                    Image = p.Images.FirstOrDefault()
                }).ToList();

            //mapping images 
            foreach (var item in listproducts)
            {
                item.ImageSource = MapToImageModel(item.Image);
            }
            return listproducts;
        }
        
        public IList<ProductViewModel> GetProductsByName(string name)
        {
            var products = Db.Products.Where(p => p.ProductName.Contains(name))
                                    .Select(p => new ProductViewModel
                                    {
                                        ProductId = p.ProductId,
                                        ProductName = p.ProductName,
                                        Price = p.Price,
                                        SalePrice = p.SalePrice,
                                        DateCreated = p.DateCreated,
                                        Description = p.Description,
                                        Image = p.Images.FirstOrDefault()
                                    }).ToList();
            
            //mapping images 
            foreach (var item in products)
            {
                item.ImageSource = MapToImageModel(item.Image);
            }
            return products;
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

        //public ProductViewModel GetProductInfoWithIds(IEnumerable<int> idSet)
        //{
        //    var products = Db.Products.Where(p => idSet.Contains(p.ProductId))
        //                                .ToList();
        //    return products;
        //}

        public void UploadProduct(UploadProductModel model)
        {
            var product = new Product
            {
                ProductName = model.ProductName,
                Quantity = model.Quantity,
                Price = Double.Parse(model.Price),
                Category = model.Category,
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

        private string MapToImageModel(ProductImage image)
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