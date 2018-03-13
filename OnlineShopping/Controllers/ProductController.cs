﻿using OnlineShopping.Models;
using OnlineShopping.Service;
using OnlineShopping.ViewModel;
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
        private OnlineShoppingDbContext Db;
        public ProductController(ControllerService service,OnlineShoppingDbContext dbcontext)
        {
            Db = dbcontext;
            _service = service;
        }
        public ActionResult Index(int id)
        {
            var productInfo = _service.GetProductInfo(id);
            return View(productInfo);
        }
            
        public ActionResult Category(int type)
        {
            //get from db with specific type
            var model = _service.GetProductsByCategory(type);
            return View("ProductsWithCategory",model);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = Db.Products.Find(model.ProductId);
                var name = product.ProductName;
                var price = product.Price;
                var sale = product.SalePrice;
                var description = product.Description;
                bool isChanged = false;
                if (!name.Equals(model.ProductName))
                {
                    product.ProductName = model.ProductName;
                    isChanged = true;
                }

                if (!price.Equals(model.Price))
                {
                    product.Price = model.Price;
                    isChanged = true;
                }

                if (!sale.Equals(model.SalePrice))
                {
                    product.SalePrice = model.SalePrice;
                    isChanged = true;
                }

                if (!description.Equals(model.Description))
                {
                    product.Description = model.Description;
                    isChanged = true;
                }


                if (isChanged) Db.SaveChanges();
            }
            return RedirectToAction("Index", "Product", new { id = model.ProductId });
        }

        public ActionResult DeleteProduct(int? id)
        {
            if (id != null)
            {
                var product = Db.Products.Find(id);
                Db.Products.Remove(product);
                Db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
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