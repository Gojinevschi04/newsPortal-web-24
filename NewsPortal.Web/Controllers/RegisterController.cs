using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.User;
using NewsPortal.Web.Models;

namespace NewsPortal.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ISession _session;

        public RegisterController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
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

            ULoginResp resp = _session.URegisterAction(URData);

            if (resp.Status == true)
            {
                HttpCookie cookie = _session.GenCookie(login.Email);
                ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Home");
            }

            return Redirect("/Home/Index");
        }
    }
}