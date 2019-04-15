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
            IList<ProductViewModel> products = GetCartFromCookie();
            //IList<ProductViewModel> products = GetCartDataSession();
            return View("Cart", new CartViewModel() { Products = products });
        }

        public bool AddToCart(int? productId)
        {
            return (AccountController.IsLogged) ? AddToUserCart(productId) : AddToCartCookie(productId);
        }

        public bool AddToCartCookie(int? productId)
        {
            if (!productId.HasValue) return false;

            var cart = new HttpCookie("cart");
            cart["product"+ productId.ToString()] = "1";
            cart.Expires.Add(new TimeSpan(1, 0, 0));
            Response.Cookies.Add(cart);

            return true;
        }

        //   Ajax
        public bool AddToUserCart(int? productId)
        {
            //STORE INTO SESSION
            //...

            // change # of items in cart after added

            if (!productId.HasValue) return false;

            var ids = Session["productIds"] as Dictionary<int,int>;
            if (ids == null)
            {
                ids = new Dictionary<int, int>();
            }

            ids.Add((int)productId, 1);
            Session["productIds"] = ids;
            return true;
        }

        public ActionResult RemoveFromCart()    //ajax 
        {
            var cookie = Request.Cookies.AllKeys;
            var items = new CartViewModel();
            if (cookie.Length == 0 || !cookie.Any(x => x.Contains("product")))
            {
                return PartialView("_CartTable", items);
            }

            List<int> productIds = new List<int>();
            foreach (var key in cookie)
            {
                if (key.Contains("product"))
                {
                    var IdString = key.Remove(0, 7);
                    var id = Int32.Parse(IdString);
                    productIds.Add(id);
                }
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

        private IList<ProductViewModel> GetCartFromCookie()
        {
            var cartProducts = new List<ProductViewModel>();
            var cookie = Request.Cookies.AllKeys;   //get all cookie names
            if (cookie == null || !cookie.Any(x => x.Contains("product")))
            {
                return cartProducts;
            }

            List<int> productIds = new List<int>();
            foreach (var key in cookie)
            {
                if (key.Contains("product"))
                {
                    var idString = key.Remove(0, 7);    //remove the "product" string (7 chars) to get prod id
                    var id = Int32.Parse(idString);
                    productIds.Add(id);
                }
            }

            foreach (var id in productIds)
            {
                var product = _service.GetProductInfo(id);
                cartProducts.Add(product);
            }
            return cartProducts;
        }

        private IList<ProductViewModel> GetCartFromSession()
        {
            var cartItems = Session["productIds"] as Dictionary<int, int>;    //id + quantity
            if (cartItems == null || cartItems.Count == 0) return null;

            

            return null;
        }
    }
}