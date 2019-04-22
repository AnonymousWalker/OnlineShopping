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
                Quantity = product.Quantity,
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

        public IList<CartProductViewModel> GetUserCartData(int userId)
        {
            var products= Db.Carts.Where(c => c.UserId == userId)
                            .Join(Db.Products, c => c.ProductId, p => p.ProductId,
                                (c, p) => new ProductViewModel
                                {
                                    ProductId = p.ProductId,
                                    ProductName = p.ProductName,
                                    Price = p.Price,
                                    SalePrice = p.SalePrice,
                                    Quantity = c.Quantity,
                                    Image = p.Images.FirstOrDefault()
                                }).ToList();
            //mapping img
            products = MapImageToModel(products).ToList();
            var cartItems = products.Select(p => new CartProductViewModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                SalePrice = p.SalePrice,
                Quantity = p.Quantity,
                ImageSource = p.ImageSource
            }).ToList();

            return cartItems;
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

        public void RemoveFromUserCart(int userId, int? productId = null)
        {
            if (!productId.HasValue)    //remove all 
            {
                Db.Carts.RemoveRange(Db.Carts.Where(c => c.UserId == userId).ToList());
            }
            else
            {
                var isExist = Db.Carts.Any(c => c.UserId == userId && c.ProductId == productId);
                if (isExist)
                {
                    var cartItem = Db.Carts.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
                    Db.Carts.Remove(cartItem);
                }
            }
            Db.SaveChanges();
        }

        public Transaction CreateTransaction(int userId, CartViewModel cart)
        {
            var newTransaction = Db.Transactions.Add(new Transaction
            {
                UserId = userId,
                TotalAmount = cart.TotalPrice,
                Date = DateTime.Now
            });
            var transDetails = new List<TransactionDetail>();
            foreach (var item in cart.Products)
            {
                transDetails.Add(new TransactionDetail
                {
                    ProductId = item.ProductId,
                    TransactionId = newTransaction.TransactionId,
                    Quantity = item.Quantity,
                    Amount = item.Amount
                });
            }
            var tDetails = Db.TransactionDetails.AddRange(transDetails);
            Db.SaveChanges();
            newTransaction.TransactionDetail = tDetails.ToList();

            return newTransaction;
        }

        public IList<TransactionViewModel> GetUserTransactions(int userId)
        {
            var trans = Db.Transactions.Where(t => t.UserId == userId)
                                    .Include(t => t.TransactionDetail)
                                    .OrderByDescending(t => t.Date).ToList();

            var listTrans = new List<TransactionViewModel>();
            foreach (var tr in trans)
            {
                // map trans --> transViewModel
                var transactionVM = new TransactionViewModel() {
                    TransactionId = tr.TransactionId,
                    DatePurchased = tr.Date,
                    TotalAmount = tr.TotalAmount
                };
                listTrans.Add(transactionVM);
            }

            foreach (var tr in trans)
            {
                var transProducts = new List<CartProductViewModel>();
                CartProductViewModel transItem;
                foreach (var item in tr.TransactionDetail)
                {
                    transItem = new CartProductViewModel();
                    //map product info <--> transDetail
                    transItem.ProductId = item.ProductId;
                    transItem.Quantity = item.Quantity;
                    transItem.Price = item.Amount;
                    var product = GetProductImage(item.ProductId);
                    transItem.ProductName = product.ProductName;
                    transItem.ImageSource = product.ImageSource;

                    //map Trans.TransDetail <--> TransVM.TransItems
                    transProducts.Add(transItem);
                }
                listTrans.Where(trVM => trVM.TransactionId == tr.TransactionId).FirstOrDefault()
                                                    .TransactionProducts = transProducts;
            }

            return listTrans;
        }


        #region private

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

        private ProductViewModel GetProductImage(int id)
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
                Price = product.Price,
                SalePrice = product.SalePrice,
                ImageSource = imagesrc
            };
        }
        #endregion
    }
}