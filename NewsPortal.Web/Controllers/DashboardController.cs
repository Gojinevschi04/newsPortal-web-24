using System.Web.Mvc;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.User;
using NewsPortal.Web.Filters;
using NewsPortal.Web.Models;

namespace NewsPortal.Web.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ISession _session;
        private readonly UserMinimal userAuthenticated;

        public DashboardController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }
        
        public ActionResult UserDetail()
        {
            //SessionStatus();
            //var user = System.Web.HttpContext.Current.GetMySessionObject();
            var userData = _session.GetUserById(userAuthenticated.Id);
            if (userAuthenticated != null)
            {
                var userModel = new UserData()
                {
                    Id = userData.Id,
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    Username = userData.Username,
                    Email = userData.Email
                };
                return View(userModel);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult EditProfile(UserData data)
        {
            //SessionStatus();
            //var user = System.Web.HttpContext.Current.GetMySessionObject();

            if (ModelState.IsValid)
            {
                UEditData existingUser = new UEditData
                {
                    Id = data.Id,
                    Username = data.Username,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Email = data.Email,
                    Level = data.Level
                };
                var response = _session.EditProfileAction(existingUser);
                if (response.Status)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", response.StatusMessage);
                    return View(data);
                }
            }

            return View();
        }

        public ActionResult ChangePassword()
        {
            var model = new UChangePasswordData()
            {
                Id = userAuthenticated.Id
            };
            return View(model);
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult ChangePassword(int? userId)
        {
            if (userId == null)
            {
                userId = userAuthenticated.Id;
            }
            else
            {
                var model = new UChangePasswordData()
                {
                    Id = (int)userId
                };
                return View(model);
            }

            return View(new UChangePasswordData());
        }

        [AuthorizedMod]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UChangePasswordData password)
        {
            var model = new UChangePasswordData();
            if (ModelState.IsValid)
            {
                if (password.NewPassword == password.ConfirmedPassword && userAuthenticated.Id == password.Id)
                {
                    var response = _session.ChangePassword(password);
                    if (response.Status)
                    {
                        ViewBag.ConfirmationMessage = response.StatusMessage;
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Confirmation failed! Try again";
                    return View(model);
                }
            }

            return View();
        }
    }
}