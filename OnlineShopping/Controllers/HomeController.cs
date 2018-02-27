using OnlineShopping.Models;
using OnlineShopping.Models.DomainModel;
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
        private OnlineShoppingDbContext Db;
        public HomeController(OnlineShoppingDbContext db)
        {
            Db = db;
        }

        public HomeController()
        {
        }

        public ActionResult Index()
        {
            var products = Db.Products.Select(x => new ProductViewModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Price = x.Price,
                DateCreated = x.DateCreated,
                Description = x.Description
            }).ToList();
            return View(new List<ProductViewModel>(products));
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