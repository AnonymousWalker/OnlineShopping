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
        private ControllerService _service;
        public HomeController(ControllerService service)
        {
            _service = service;
        }
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            var products = _service.GetProducts();
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