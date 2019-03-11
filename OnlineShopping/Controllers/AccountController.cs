using OnlineShopping.Models;
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
        private ControllerService controllerService;

        public AccountController(ControllerService service)
        {
            controllerService = service;
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
            var user = controllerService.AuthenticateUser(model.Username, model.Password);
            if (user != null)
            {
                Session["UserID"] = user.UserId;
                Session["UserFirstName"] = user.FirstName;

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
                var user = controllerService.AuthenticateUser(model.Username, model.Password);
            if (user != null && user.Username == model.Username)
            {
                model.HasError = true;
                model.ResultCode = ResultCode.Error;
                model.Message = "This username is already exist.";
                return View("Login", model);
            }
            bool isSuccess = controllerService.CreateUserAccount(model);
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
    }
}