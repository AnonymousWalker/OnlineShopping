

using OnlineShopping.Service;
using OnlineShopping.ViewModel;
using OnlineShopping.ViewModel.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    public class CartController : Controller
    {
        private ControllerService _service;
        public CartController(ControllerService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            IList<CartProductViewModel> products =
                (AccountController.IsLogged) ? GetUserCartData(Convert.ToInt32(Session["UserID"]))
                                            : GetCartDataFromCookie();
            return View("Cart", new CartViewModel() { Products = products });
        }

        public bool AddToCart(int? productId)
        {
            bool isSuccess = (AccountController.IsLogged)? AddToUserCart(productId) : AddToCartCookie(productId);
            //var result = new RequestResult {
            //    HasError = !isSuccess,
            //    DataString = GetCartCounter().ToString()
            //};
            //return Json(result, JsonRequestBehavior.AllowGet);
            return isSuccess;
        }

        

        public ActionResult RemoveFromCart(int? productId)    //ajax 
        {
            var cartData = new CartViewModel();
            if (!productId.HasValue) return PartialView("_CartTable", cartData);

            if (!AccountController.IsLogged)
            {
                var cookie = Request.Cookies["cart"];
                if (cookie == null || !cookie.HasKeys)
                {
                    return PartialView("_CartTable", cartData);
                }
                string productIdString = productId.ToString();
                if (cookie.Values["product" + productIdString] != null)
                {
                    cookie.Values.Remove("product" + productIdString);
                    Response.Cookies.Set(cookie);   //update cookie to client
                }
            }
            else
            {
                _service.RemoveFromUserCart(Convert.ToInt32(Session["UserID"]), (int)productId);
            }
            return PartialView("_CartTable", cartData);
        }

        public ActionResult Checkout()
        {
            if (!AccountController.IsLogged) return RedirectToAction("Login", "Account");

            var uID = Convert.ToInt32(Session["UserID"]);
            var orderItems = _service.GetUserCartData(uID);
            var cart = new CartViewModel { Products = orderItems };

            if (orderItems.Count == 0)
            {
                return RedirectToAction("Index");
            }
            //create transaction
            var transaction = _service.CreateTransaction(uID, cart);
            ViewBag.OrderId = transaction.TransactionId;
            _service.RemoveFromUserCart(uID);
            return View("OrderSuccess", cart);
        }

        public int GetCartCounter()
        {
            if (AccountController.IsLogged)
            {
                return GetUserCartData(Convert.ToInt32(Session["UserID"])).Count;
            }
            return GetCartDataFromCookie().Count;
        }

        #region private

        private bool AddToCartCookie(int? productId)
        {
            if (!productId.HasValue) return false;
            var currentCookie = Request.Cookies["cart"];    //Request.Cookie is from Client
            string productIdString = productId.ToString();

            if (currentCookie == null)  //create cookie
            {
                var cart = new HttpCookie("cart");
                cart["product" + productIdString] = "1";    //set default quantity = 1
                cart.Expires.Add(new TimeSpan(1, 0, 0));
                Response.Cookies.Add(cart);
                return true;
            }
            else if (currentCookie["product" + productIdString] == null)    //add item
            {
                currentCookie.Values.Add("product" + productIdString, "1");
                Response.Cookies.Set(currentCookie);
                return true;
            }
            else if (currentCookie.HasKeys) //edit 
            {
                var quantity = currentCookie["product" + productIdString];
                int q = int.Parse(quantity.ToString()) + 1;
                currentCookie["product" + productIdString] = q.ToString();
                Response.Cookies.Set(currentCookie);
                return true;
            }

            return false;
        }

        private bool AddToUserCart(int? productId)
        {
            if (!productId.HasValue) return false;

            _service.AddToUserCart(Convert.ToInt32(Session["UserID"]), (int)productId);
            return true;
        }

        private IList<CartProductViewModel> GetCartDataFromCookie()
        {
            var cartProducts = new List<CartProductViewModel>();
            var cartCookie = Request.Cookies["cart"];

            if (cartCookie == null || !cartCookie.HasKeys)
            {
                return cartProducts;
            }

            var productIds = new Dictionary<int, int>();
            foreach (var product in cartCookie.Values.AllKeys)
            {
                var idString = product.ToString().Remove(0, 7);    //remove the "product" string (7 chars) to get prod id
                var id = int.Parse(idString);
                var quant = int.Parse(cartCookie[product]);
                productIds.Add(id, quant);
            }

            foreach (var id in productIds)
            {
                var product = _service.GetProductInfo(id.Key);
                cartProducts.Add(new CartProductViewModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    SalePrice = product.SalePrice,
                    Quantity = id.Value,
                    ImageSource = product.ImageSource
                });
            }
            return cartProducts;
        }

        private IList<CartProductViewModel> GetUserCartData(int userId)
        {
            return (userId == 0) ? new List<CartProductViewModel>() : _service.GetUserCartData(userId);
        }

        #endregion
    }
}