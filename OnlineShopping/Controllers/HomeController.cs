using OnlineShopping.Models;
using OnlineShopping.Models.DomainModel;
using OnlineShopping.Service;
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
        private ControllerService _service;     //DI - unity configured with constructor
        public HomeController(ControllerService service)
        {
            _service = service;
            //using (var context = new OnlineShoppingDbContext())
            //{
            //    context.Products.Add(new Product { ProductName = "USB 3.0 Kingston 32GB", Price = 5.0, Description = "Fast drive storage", DateCreated = DateTime.Now.Date });
            //    context.SaveChanges();
            //}
        }
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            var products = _service.GetAllProducts();
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

        public ActionResult Sales()
        {
            var viewModel = _service.GetSaleOffProducts();
            return View(viewModel);
        }

    }
}