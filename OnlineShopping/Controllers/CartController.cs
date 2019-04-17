

using OnlineShopping.Service;
using OnlineShopping.ViewModel;
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
            IList<ProductViewModel> products = 
                (AccountController.IsLogged) ? GetUserCart() : GetCartFromCookie();
            return View("Cart", new CartViewModel() { Products = products });
        }

        public bool AddToCart(int? productId)
        {
            return (AccountController.IsLogged) ? AddToUserCart(productId) : AddToCartCookie(productId);
        }

        public bool AddToCartCookie(int? productId)
        {
            if (!productId.HasValue) return false;
            var currentCookie = Request.Cookies["cart"];    //Request.Cookie is from Client
            string productIdString = productId.ToString();

            if (currentCookie == null)
            {
                var cart = new HttpCookie("cart");
                cart["product" + productIdString] = "1";    //set default quantity = 1
                cart.Expires.Add(new TimeSpan(1, 0, 0));
                Response.Cookies.Add(cart);
                return true;
            }
            else if (currentCookie.HasKeys && currentCookie["product" + productIdString] == null)
            {
                currentCookie.Values.Add("product" + productIdString, "1");
                Response.Cookies.Set(currentCookie);
                return true;
            }
            else if (currentCookie.HasKeys)
            {
                var quantity = currentCookie["product" + productIdString];
                int q = int.Parse(quantity.ToString()) + 1;
                currentCookie["product" + productIdString] = q.ToString();
                Response.Cookies.Set(currentCookie);
                return true;
            }

            return false;
        }

        //   Ajax
        public bool AddToUserCart(int? productId)
        {
            //STORE INTO SESSION
            //...

            // change # of items in cart after added

            if (!productId.HasValue) return false;

            var ids = Session["productIds"] as Dictionary<int, int>;
            if (ids == null)
            {
                ids = new Dictionary<int, int>();
            }

            ids.Add((int)productId, 1);
            Session["productIds"] = ids;
            return true;
        }

        public ActionResult RemoveFromCart(int? productId)    //ajax 
        {
            var cookie = Request.Cookies["cart"];
            var items = new CartViewModel();

            if (cookie == null|| !cookie.HasKeys)
            {
                return PartialView("_CartTable", items);
            }

            List<int> productIds = new List<int>();
            foreach (var key in cookie.Values)
            {
                //if (key.Contains("product"))
                //{
                //    var IdString = key.Remove(0, 7);
                //    var id = int.Parse(IdString);
                //    productIds.Add(id);
                //}
            }
            if (!productIds.Any())
            {
                return RedirectToAction("Index");
            }

            foreach (var id in productIds)
            {
                var product = _service.GetProductInfo(id);
                items.Products.Add(product);
            }
            return PartialView("_CartTable", items);
        }

        public ActionResult Checkout()
        {
            if (!AccountController.IsLogged) return RedirectToAction("Login", "Account");

            //create transaction


            //show success page
            return View("OrderSuccess");
        }


        #region private

        private IList<ProductViewModel> GetCartFromCookie()
        {
            var cartProducts = new List<ProductViewModel>();
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
                product.Quantity = id.Value;
                cartProducts.Add(product);
            }
            return cartProducts;
        }

        private IList<ProductViewModel> GetUserCart()
        {
            var items = new List<ProductViewModel>();
            var cartItems = Session["productIds"] as Dictionary<int, int>;    //id + quantity
            if (cartItems == null || cartItems.Count == 0) return items;



            return items;
        }

        #endregion
    }
}