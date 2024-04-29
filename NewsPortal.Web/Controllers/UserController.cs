using System.Web.Mvc;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Web.Models;

namespace NewsPortal.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly ISession _session;

        public UserController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }

        [HttpGet]
        public ActionResult UserDetail(int postId)
        {
            var data = _session.GetUserById(postId);
            using (var db = new UserContext())
            {
                var model = new UserData
                {
                    Id = data.Id,
                    Username = data.Username,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Email = data.Email,
                    Level = data.Level
                };

                return View(model);
            }
        }
    }
}