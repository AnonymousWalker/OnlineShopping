using OnlineShopping.Models;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext Db;
        public HomeController(DatabaseContext db)
        {
            Db = db;
        }

        public HomeController()
        {
        }

        public ActionResult Index()
        {
            var getproducts = Db.Products.Select(x => new ProductViewModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Price = x.Price,
                DateCreated = x.DateCreated,
                Description = x.Description
            }).ToList();
            var products = new List<ProductViewModel>(getproducts);
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}