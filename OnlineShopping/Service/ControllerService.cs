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

            return MapImageToModel(products);
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
            return MapImageToModel(products);
        }

        public IList<ProductViewModel> GetProductsByCategory(int type)
        {   // should we map the entity to model before ToList or after?
            var products = Db.Products.Include(p => p.Images).Where(p => p.Category == (ProductCategoryEnum)type)
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
            return MapImageToModel(products);
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
            return MapImageToModel(products);
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

        //public ProductViewModel GetProductsByIds(IEnumerable<int> ids)
        //{
        //    var products = Db.Products.Where(p => idSet.Contains(p.ProductId))
        //                                .ToList();
        //    return products;
        //}

        public User AuthenticateUser(string username, string password)
        {
            //Check from db
            var user = Db.Users.Where(u => u.Username == username && u.Password == password)
                                .FirstOrDefault();
            return user;
        }

        public bool CreateUserAccount(SignUpViewModel account)
        {
            try
            {
                Db.Users.Add(new User
                {
                    Username = account.Username,
                    Password = account.Password,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Email = account.Email
                });
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
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

        public IList<ProductViewModel> GetUserCartData(int userId)
        {
            var cartProduct = Db.Carts.Where(c => c.UserId == userId)
                            .Join(Db.Products, c => c.ProductId, p => p.ProductId,
                                (c, p) => new ProductViewModel
                                {
                                    ProductId = p.ProductId,
                                    ProductName = p.ProductName,
                                    Category = p.Category,
                                    Price = p.Price,
                                    SalePrice = p.SalePrice,
                                    Quantity = c.Quantity,
                                    Description = p.Description,
                                    DateCreated = p.DateCreated,
                                    Image = p.Images.FirstOrDefault()
                                }).ToList();
            //mapping img
            return MapImageToModel(cartProduct);
        }

        public void AddToUserCart(int userId, int productId)
        {
            var isExist = Db.Carts.Any(c => c.UserId == userId && c.ProductId == productId);
            if (!isExist)
            {
                Db.Carts.Add(new Cart
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = 1
                });
            }
            else
            {
                var currentItem = Db.Carts.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
                currentItem.Quantity++;
            }
            Db.SaveChanges();
        }

        public void RemoveFromUserCart(int userId, int productId)
        {
            var isExist = Db.Carts.Any(c => c.UserId == userId && c.ProductId == productId);
            if (isExist)
            {
                var cartItem = Db.Carts.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
                Db.Carts.Remove(cartItem);
            }
            Db.SaveChanges();
        }

        private IList<ProductViewModel> MapImageToModel(IList<ProductViewModel> products)
        {
            string imageSrcString = "";
            foreach (var p in products)
            {
                if (p.Image != null)
                {
                    //concert from binary byte[] to string source
                    var base64img = Convert.ToBase64String(p.Image.Content);
                    imageSrcString = string.Format("data:image/jpg;base64,{0}", base64img);
                }
                p.ImageSource = imageSrcString;
            }
            return products;
        }
    }
}