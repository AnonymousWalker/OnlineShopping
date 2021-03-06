﻿using OnlineShopping.Models;
using OnlineShopping.Service;
using OnlineShopping.ViewModel;
using OnlineShopping.ViewModel.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private ControllerService _service;
        public static bool IsLogged = false;

        public AccountController(ControllerService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(LoginViewModel model)
        {
            var user = _service.AuthenticateUser(model.Username, model.Password);
            if (user != null)
            {
                Session["UserID"] = user.UserId;
                Session["UserFirstName"] = user.FirstName;
                IsLogged = true;
                //Todo: update user authorization
                //

                return RedirectToAction("Index", "Home");
            }
            model.HasError = true;
            model.Message = "Invalid username or password, please try again!";
            model.Password = string.Empty;
            return View("Login", model);
        }   

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return View(model);
            }
                var user = _service.AuthenticateUser(model.Username, model.Password);
            if (user != null && user.Username == model.Username)
            {
                model.HasError = true;
                model.ResultCode = ResultCode.Error;
                model.Message = "This username is already exist.";
                return View("Login", model);
            }
            bool isSuccess = _service.CreateUserAccount(model);
            if (!isSuccess)
            {
                model.HasError = true;
                model.ResultCode = ResultCode.Error;
                model.Message = "An exception has occurred from server database.";
                return View("Login", model);
            }
            ViewBag.Message = "Congratulations, " + model.FirstName + "! You have successfully signed up.";
            return View("SignUpSuccess");
        }

        public ActionResult Logout()
        {
            IsLogged = false;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Transactions()
        {
            if (IsLogged)
            {
                var model = _service.GetUserTransactions(Convert.ToInt32(Session["UserID"]));
                return View(model);
            }
            return RedirectToAction("Login");
        }
    }
}