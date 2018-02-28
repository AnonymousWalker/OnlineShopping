using OnlineShopping.Models;
using OnlineShopping.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    public class ProductController : Controller
    {
        private ControllerService _service;
        public ProductController(ControllerService service)
        {
            _service = service;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductInformation(int id)   //database
        {
            var productInfo = _service.GetProductInfo(id);
            return View(productInfo);
        }

        public ActionResult UploadProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadProduct(UploadProductModel model, HttpPostedFileBase image =null)
        {
            if (ModelState.IsValid && model != null && image != null) //get uploaded image
            {
                model.ImageName = image.FileName;
                model.Content = new byte[image.ContentLength];
                image.InputStream.Read(model.Content, 0, image.ContentLength);
                _service.UploadProduct(model);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

    }
}