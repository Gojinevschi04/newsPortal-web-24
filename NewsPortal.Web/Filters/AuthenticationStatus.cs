using System.Web;
using System.Web.Mvc;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Web.Extension;

namespace NewsPortal.Web.Filters
{
    public class AuthenticationStatus : ActionFilterAttribute
    {
        private readonly ISession _session;

        public AuthenticationStatus()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];

            if (apiCookie != null)
            {
                var user = _session.GetUserByCookie(apiCookie.Value);

                if (user != null)
                {
                    HttpContext.Current.SetMySessionObject(user);
                    filterContext.Controller.TempData["AuthenticatedUser"] = user;
                    base.OnActionExecuting(filterContext);
                }
            }
        }
    }
}