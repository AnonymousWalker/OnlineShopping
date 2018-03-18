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
            var cookie = Request.Cookies.AllKeys;
            if (cookie == null || !cookie.Any(x => x.Contains("product")))
            {
                return View("Cart", new CartViewModel());
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
            var items = new CartViewModel();
            foreach (var id in productIds)
            {
                var product = _service.GetProductInfo(id);
                items.Products.Add(product);
            }
            return View("Cart", items);
        }

        public ActionResult UpdateCart()    //update ajax 
        {
            var cookie = Request.Cookies.AllKeys;
            var items = new CartViewModel();
            if (cookie.Length==0 || !cookie.Any(x => x.Contains("product")))
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
    }
}