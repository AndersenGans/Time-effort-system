using ETS.BLL.Infrastructure.Abstract;
using ETS.BLL.Infrastructure.Concrete;
using ETS.Contracts.DataContracts;
using ETS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;
using ETS.BLL;
using Ninject;
using ETS.BLL.Infrastructure;

namespace WebSite.Controllers
{
    public class AccountController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        IAuthProvider authProvider;

        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }

        public ViewResult Login()
        {
            // Запоминаем с какой страницы перешли
            LoginViewModel model = new LoginViewModel();
            model.PreviousUrl = System.Web.HttpContext.Current.Request.UrlReferrer?.ToString();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.Login, model.Password))
                {
                    //return Redirect(returnUrl ?? Url.Action("Index", "ProjectManagement"));
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    if (model.PreviousUrl == null) return RedirectToAction("Index","TimeReports");
                     return Redirect(model.PreviousUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public RedirectResult Logout()
        {
            authProvider.DeAuthenticate();
            return Redirect("/Account/Login");
        }

        public ViewResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(SignupModel model, string returnUrl)
        {
            AccountEntity newAccount = new AccountEntity();
            newAccount.Name = model.Name;
            newAccount.MiddleName = model.MiddleName;
            newAccount.Surname = model.Surname;
            newAccount.Email = model.Email;
            newAccount.Login = model.Login;
            //newAccount.Password = Feature.SetPass(10);
            newAccount.AccessLevel = 3;
            //Feature.SendEmail(newAccount.Email, "Your credentials!", 
            //     String.Format("Hello, {0} {1}! You recieved this email because You have registered on the Effort Time Tracking System earlier. \nPlease, use this credentials to sign in:\nLogin: {2}\nPassword: {3}\nPlease do not forget your password.", 
            //    newAccount.Name, newAccount.Surname, newAccount.Login, newAccount.Password));           
            unitOfWork.AccountRepository.Insert(newAccount);
            return Redirect(returnUrl ?? Url.Action("Login", "Account"));
        }

        
    }
}