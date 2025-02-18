using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gastronique.BusinessLogic;
using Gastronique.BusinessLogic.Interfaces;
using Gastronique.Domain.Entities.User;
using Gastronique.Web.Models;


namespace Gastronique.Web.Controllers
{
     public class LoginController : BaseController
     {
          private readonly ISession _session;

          public LoginController()
          {
               var bl = new Gastronique.BusinessLogic.BusinessLogic();
               _session = bl.GetSessionBL();
          }

          public ActionResult Index()
          {
               return View();
          }

          [HttpPost]
          //[ValidateAntiForgeryToken]
          public ActionResult Index(UserLogin login)
          {
               if (ModelState.IsValid)
               {
                    ULoginData data = new ULoginData
                    {
                         Email = login.Email,
                         Username = login.Username,
                         Password = login.Password,
                         LoginIp = Request.UserHostAddress,
                         LoginDateTime = DateTime.Now
                    };

                    var userLogin = _session.UserLogin(data);
                    if (userLogin.Status)
                    {
                         HttpCookie cookie = _session.GenCookie(login.Email);
                         ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                         return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                         ModelState.AddModelError("", userLogin.StatusMsg);
                         return View();
                    }
               }

               return View();
          }
     }
}