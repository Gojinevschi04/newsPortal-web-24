using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsPortal.BusinessLogic;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.User;
using NewsPortal.Web.Models;

namespace NewsPortal.Controllers
{
     public class RegisterController : Controller
     {
          private readonly ISession _sesion;

          public RegisterController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _sesion = bl.GetSessionBL();

          }

          // GET: Register
          public ActionResult Index()
          {
               return View();
          }


          [HttpPost]
          public ActionResult Index(UDataRegister login)
          {
               var URData = new URegisterData
               {
                    FirstName = login.FirstName,
                    LastName = login.LastName,
                    Password = login.Password,
                    Username = login.Username,
                    Email = login.Email,
                    LoginDataTime = DateTime.Now,
                    Ip = Request.UserHostAddress
               };

               ULoginResp resp = _sesion.URegisterAction(URData);


               return Redirect("/Home/Index");
          }
     }
}