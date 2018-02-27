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
            _service.GetProductInfo(id);
            return View();
        }

        public ActionResult UploadProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadProduct(UploadProductModel model, HttpPostedFileBase image =null)
        {
            if(ModelState.IsValid)
            if (image != null)
            {
                    model.ImageName = image.FileName;
                    model.Content = new byte[image.ContentLength];
                    image.InputStream.Read(model.Content, 0, image.ContentLength);
            }
            return View();
        }

    }
}