using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    public class ProductController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductInformation()   //database
        {
            return View();
        }

        public ActionResult UploadProduct()
        {
            return View();
        }

    }
}